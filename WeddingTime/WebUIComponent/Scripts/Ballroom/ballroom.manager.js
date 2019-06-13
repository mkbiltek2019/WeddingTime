'use strict';
var ballroomManager = function () {
    var persons = {},                   // dictionary that holds persons data. key is person id, value is person obj (groupId, assigned, name and surname)
        seatsStrategy = {},
        items = {},
        layout = {},                    // layout is initialized inside initLayoutObj function

        seatsRectService = null,
        seatsRoundService = null,

        seatFactory = null,
        itemFactory = null,
        lastIndexValues = { itemIndex: 0, seatIndex: 0 },
        isDirty = false;

    var init = function () {
        bindOnUnloadEvent();            // user will get notification if something was changed
        createFactories();
        createAndSetupSeatServices();
        initLayoutObj();
        initTemplates();
        enhanceItemsList();
        enhanceSaveLayoutBtn();
        enhanceLayoutBtns();
        enhanceSwitchDisplayBtn();
    },

    initTemplates = function () {       // first step is to load all available tmpls, then call load ballroom data func
        var registerObj = {
            names: ['personList', 'ballroomItem', 'tableRect', 'tableRound', 'seatsRow', 'seat', 'seatUtmost', 'seatRound', 'personName', 'stageRect', 'stageHalfCircle', 'pillarRect', 'pillarRound', 'tableRectPanelBtns', 'tableRoundPanelBtns', 'stagePanelBtns', 'mobileInfo'],
            callback: onTemplateRegistered
        };

        ballroomService.registerTmpls(registerObj);
    },

    initLayoutObj = function () {
        var item = $('#ballroomLayout');

        layout = {
            height: function () {
                return item.height();
            },
            width: function () {
                return item[0].scrollWidth;
            },
            setHeight: function (height) {
                item.css('height', height + 'px');
            },
            setWidth: function (width) {
                item.css('width', width + 'px');
            },
            offset: 100,                // by this value area will be expanded or narrowed
            fullScreen: false           // information used for expanding ballroom area to full screen
        };                                
    },

    bindOnUnloadEvent = function () {
        $(window).bind('beforeunload', function () {
            if (isDirty)
                return 'Zostały wprowadzone zmiany na stronie.';        // create as hidden localized string on page?
        });
    },

    enhanceSaveLayoutBtn = function () {
        $('#btnSaveLayout').click(function () { saveLayout(); });
    },

    enhanceLayoutBtns = function () {
        $('.btn-layout').click(function () {
            var btn = $(this);
            var name = btn.attr('name');
            layoutBtnStrategy[name]();
            setDirty();
        });
    },

    enhanceSwitchDisplayBtn = function () {
        $('#btnToggleLayout').click(function () {
            var ballroomLayout = $('#ballroomLayout');

            utils.toggleLayout(ballroomLayout);
            toggleBallroomLayout(ballroomLayout);

            setDirty();
        });
    },

    toggleBallroomLayout = function (ballroomLayout) {
        ballroomLayout.toggleClass('narrow wide');                  // toggles two classes that change min-width
        layout.fullScreen = !layout.fullScreen;                     // save info about layout size
    },

    setLayoutSize = function (data) {
        if (data.Width > layout.width())
            layout.setWidth(data.Width);

        if (data.Height > layout.height())
            layout.setHeight(data.Height);

        if (data.IsExpanded != layout.fullScreen) {                 // only toogle layout if data from server is different than data in js - since this func is called on every area reload I don't want to toggle are each time
            var ballroomLayout = $('#ballroomLayout');
            utils.toggleLayout(ballroomLayout);
            toggleBallroomLayout(ballroomLayout);
        }
    },

    decreaseLayoutWidth = function () {
        var width = layout.width() - layout.offset;

        if (areaOverlapsAnyItemWidth(width))
            return;
        
        layout.setWidth(width);
    },

    decreaseLayoutHeight = function () {        
        var height = layout.height() - layout.offset;

        if (areaOverlapsAnyItemHeight(height))
            return;
        
        layout.setHeight(height);
    },

    increaseLayoutWidth = function () {
        var width = layout.width() + layout.offset;
        layout.setWidth(width);
    },

    increaseLayoutHeight = function () {
        var height = layout.height() + layout.offset;
        layout.setHeight(height);
    },

    areaOverlapsAnyItemHeight = function (layoutHeight) {
        var overlap = false;
        $.each(items, function (i, item) {                                      // items list represents global dictionary that holds ballroom items 
            if (areaOverlapsItemHeight(item, layoutHeight)) {
                overlap = true;
                return;
            }
        });
        return overlap;
    },

    areaOverlapsAnyItemWidth = function (layoutWidth) {
        var overlap = false;
        $.each(items, function (i, item) {                                      // items list represents global dictionary that holds ballroom items 
            if (areaOverlapsItemWidth(item, layoutWidth)) {
                overlap = true;
                return;
            }
        });
        return overlap;
    },

    areaOverlapsItemWidth = function (item, layoutWidth) {
        return (getItemWidth(item.Id) + item.PositionX > layoutWidth) ? true : false;
    },

    areaOverlapsItemHeight = function (item, layoutHeight) {
        return (getItemHeight(item.Id) + item.PositionY > layoutHeight) ? true : false;
    },

    getItemWidth = function (id) {                                              // gets actual width of ballroom item
        return $('#' + id).width();        
    },

    getItemHeight = function (id) {                                             // gets actual height of ballroom item
        return $('#' + id).height();
    },

    layoutBtnStrategy = {
        'narrowV': decreaseLayoutHeight,
        'narrowH': decreaseLayoutWidth,
        'expandV': increaseLayoutHeight,
        'expandH': increaseLayoutWidth
    },

    saveLayout = function () {
        var settings = {
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            success: reloadLayout
        };
        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, ['#ballroomContainer']);

        var model = prepareBallroomModel();           
        var data = JSON.stringify({ ballroom: model });

        ballroomService.saveLayout(data, ajaxSettings);
    },

    prepareBallroomModel = function () {
        var ballroomItems = $.map(items, function (value, key) {    // get only values from items dictionary
            return value;
        });

        return {                                                    // must be the same as model defined in code
            Width: layout.width(),
            Height: layout.height(),
            IsExpanded: layout.fullScreen,
            BallroomItems: $.isEmptyObject(ballroomItems) ? null : ballroomItems
        };
    },

    reloadLayout = function () {
        invalidateLayout();
        invalidateItems();
        invalidateSaveBtn();

        disableItemsBtns();                                 // disable items btn while saving/refreshing ballroom area

        $.when(getBallroomLayout()).then(onBallroomDataLoaded);
    },

    invalidateLayout = function () {
        $('#ballroomLayout').empty();
    },

    invalidateItems = function () {
        var itemIds = $.map(items, function (value, key) {
            return key;
        });
        $.each(itemIds, function (i, id) {
            delete items[id];                               // have to delete each item using loop to not loose reference to items dict inside seats services
        });
    },

    invalidateSaveBtn = function () {
        $('#btnSaveLayout').addClass('disabled');
        isDirty = false;
    },

    setDirty = function () {
        if (isDirty)
            return;

        $('#btnSaveLayout').removeClass('disabled');
        isDirty = true;
    },

    enhanceItemsList = function () {
        $('#btnBallroomItems').click(function () {
            $(this).toggleClass('active');
            $('.ballroom-items').toggle('blind', 300);
        });

        $('.ballroom-items button').click(function () {
            var type = $(this).data('item-type');
            var data = itemFactory.create(type);
            onAddItem(data);
        });
    },

    onAddItem = function (data) {
        renderItem(data);
        var addedItem = $('#' + data.Id);
        applySeatsDroppable(addedItem);
        applySeatsMouseEvents(addedItem);
        enhanceSeatsActionBtns(addedItem);
        applyTableMouseEvents(addedItem);
        enhanceItemPanelBtns(addedItem);
        setDirty();
    },

    onTemplateRegistered = function () {
        showMobileBallroomInfo();
        loadBallroomData();
    },

    showMobileBallroomInfo = function () {
        if (!$.browser.mobile)
            return;

        var value = cookieManager.get('ballroominfo');
        if (value !== null && value.infoClosed === true)
            return;

        $('#ballroomContainer').prepend($.render['mobileInfo']());
        $('#mobileInfo').on('closed.bs.alert', function () {
            cookieManager.set('ballroominfo', { infoClosed: true }, '/Ballroom');
        });
    },

    loadBallroomData = function () {
        $.when(getBallroomPersons(), getBallroomLayout()).then(onBallroomDataLoaded);
    },

    getBallroomPersons = function () {
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onGetPersonsSuccess }, ['.guests-panel-action']);
        return ballroomService.getPersons(ajaxSettings);
    },

    getBallroomLayout = function () {
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: processLayout }, ['#layoutContainer']);
        return ballroomService.getLayout(ajaxSettings)
    },

    onGetPersonsSuccess = function (data) {
        createPersonsDataMap(data);

        $('#guests').append($.render['personList'](data));                              // append because of two static elements (groom and bride)

        $('li.list-group-item').draggable(personDraggableSettings);                     // apply draggable to all li elements

        $('.btn-expand-group').click(function () {
            var btn = $(this);
            btn.toggleClass('active');
            btn.children('span').toggleClass('icon-expand icon-collapse');

            $(this).closest('.ballroom-guests-group').next('ul').slideToggle(300);
        });
        $('#btnPersonsPanel').click(function () {
            var btn = $(this);

            if (btn.hasClass('active'))
                $('.guests-panel').hide('slide', { direction: 'right' }, 200);
            else
                $('.guests-panel').show('slide', { direction: 'right' }, 200);

            btn.toggleClass('active');
        });
    },

    createPersonsDataMap = function (data) {                                            // creates persons dict where key is person id value is person obj, is used when assigning/unassigning persons to seats
        $.each(data, function (i, group) {
            $.each(group.Persons, function (personId, personObj) {
                persons[personId] = personObj;
            });
        });
    },

    personDraggableSettings = {
        revert: 'invalid',
        revertDuration: 200,
        handle: '.list-item-handle',
        zIndex: 100,
        cursorAt: { top: 10, left: 5 },
        opacity: 0.8,
        cursor: 'move',
        helper: function () { return $('<div class="color-grey">').append($(this).text()); }               // helper: 'clone',
    },

    processLayout = function (data) {      
        setLayoutSize(data);

        if (data.HasItems) {
            $.each(data.BallroomItems, function (key, value) {
                renderItem(value);
            });
        }

        applySeatsDroppable();                      // applies droppable to all seats
        applySeatsMouseEvents();
        enhanceSeatsActionBtns();
        applyTableMouseEvents();
        enhanceItemPanelBtns();

        setIndexsForFactories();
        enableItemsBtns();
    },

    enableItemsBtns = function () {
        $('#btnBallroomItems').prop('disabled', false);
    },

    disableItemsBtns = function () {
        $('#btnBallroomItems').prop('disabled', true);
    },

    setIndexsForFactories = function () {
        itemFactory.setIndex(lastIndexValues.itemIndex);
        seatFactory.setIndex(lastIndexValues.seatIndex);
    },

    onPersonDroppedToSeat = function (event, ui) {
        var pid = ui.draggable.attr('pid');
        var sid = $(this).attr('sid');

        assignPersonToSeat(sid, pid);
        setDirty();
    },

    registerBallroomItem = function (data) {
        items[data.Id] = data;
        updateLastItemIndex(data);
        updateLastSeatIndex(data);
    },

    updateLastItemIndex = function (data) {
        var index = data.Id;
        if (lastIndexValues.itemIndex > index)
            return;

        lastIndexValues.itemIndex = index;
    },

    updateLastSeatIndex = function (data) {
        if (data.Seats === undefined)
            return;

        $.each(data.Seats, function (key, value) {          // seats dict for rect table contains all seats (including top and bottom)
            var index = parseInt(key);
            if (lastIndexValues.seatIndex > index)
                return;

            lastIndexValues.seatIndex = index;
        });
    },

    appendBallroomItem = function (data, itemTmpl, tmplPanel) {             // used to show newly added ballroom item on the page
        var tmplData = {                                                    // this is data format used by ballroom item tmpl
            ItemTmpl: itemTmpl,                                             // item tmpl is specyfic ballroom item tmpl
            PanelTmpl: tmplPanel === undefined ? null : tmplPanel,          // panel tmpl contains additional btn for ballroom item panel 
            Data: data
        };

        $('#ballroomLayout').append($.render['ballroomItem'](tmplData));    // render template that was register on page load
    },

    renderItem = function (data) {
        registerBallroomItem(data);
        ballroomItemsStrategy[data.ItemType](data);

        $('#' + data.Id)                                                    // set up added item, this is applicable for all ballroom items
            .css({ top: data.PositionY, left: data.PositionX })
            .draggable({                                                    // applicable for all ballroom items (drag and drop)
                stop: onBallroomItemDragEnd,
                containment: '#ballroomLayout',
                handle: '.drag-handle'
            })
            .find('.drag-handle').mousedown(function () {
                var btn = $(this);
                bringToFront(btn);
            });
    },

    renderTableRect = function (data) {
        var model = { SideSeats: {} };                                      // side seats are all seats except top and bottom, create side seats model and extend orginal data with additional information (needed for rendering purpose)
        $.each(data.Seats, function (key, value) {
            if (value.Location == null)                                     // must be == specified cause we can have null or undefined value
                model.SideSeats[key] = value;
        });

        var tableData = $.extend({}, data, model);                          // copy data and add side seats which are needed only for rendering rect table

        appendBallroomItem(tableData, 'tableRect', 'tableRectPanelBtns');
        rotateTableRect(tableData);

        var tid = tableData.Id;
        enhanceRotateBtn(tid, rotationStrategy, resizeRectTableArea);
    },

    renderTableRound = function (data) {
        appendBallroomItem(data, 'tableRound', 'tableRoundPanelBtns');

        var tid = data.Id;
        resizeCircleTableRelatedItems(tid);
        seatsStrategy['round'].organizeInCircle(tid);                       // must be after applying appropriate size to table and table area in function above, is only applicable for round table that's why it is safe to call round seats strategy       
    },

    renderStageRect = function (data) {
        appendBallroomItem(data, 'stageRect', 'stagePanelBtns');
        
        rotateStage(data, rotationStrategy);
        enhanceRotateBtn(data.Id, rotationStrategy, resizeStageArea);
    },

    renderStageHalfCircle = function (data) {
        appendBallroomItem(data, 'stageHalfCircle', 'stagePanelBtns');

        rotateStage(data, stageHalfCircleRotationStrategy);
        enhanceRotateBtn(data.Id, stageHalfCircleRotationStrategy, resizeStageArea);
    },

    ballroomItemsStrategy = {                                                                   // key values (int) correspond to the enum defined on server side
        0: renderTableRect,                                                                     // table rect
        1: renderTableRound,                                                                    // table round
        2: function (data) { appendBallroomItem(data, 'pillarRect'); },                         // pillar rect
        3: function (data) { appendBallroomItem(data, 'pillarRound'); },                        // pillar round
        4: renderStageRect,                                                                     // stage rect
        5: renderStageHalfCircle                                                                // stage half circle   
    },

    rotateTableRect = function (data) {                                                         // data - table data
        var table = $('#item-' + data.Id);
        transformItem(table, rotationStrategy[data.Rotation]);
        resizeRectTableArea(table, data.Rotation);
    },

    rotateStage = function (data, strategy) {
        var stage = $('#item-' + data.Id);
        transformItem(stage, strategy[data.Rotation]);
        resizeStageArea(stage, data.Rotation);
    },

    enhanceRotateBtn = function (itemId, strategy, resizeItemFunc) {
        $('#btnRotate-' + itemId).click(function () {
            var item = $('#item-' + itemId);
            var currentRotationData = strategy[item.data('rotation')];
            var nextRotation = currentRotationData.next;

            items[itemId].Rotation = nextRotation;
            transformItem(item, strategy[nextRotation]);
            resizeItemFunc(item, nextRotation);

            setDirty();
        });
    },

    transformItem = function (item, rotationData) {        
        item.css('transform', 'rotate(' + rotationData.angle + 'deg)')
             .data('rotation', rotationData.current);
    },

    resizeRectTableArea = function (table, rotation) {
        var data = resizeRectTableStrategy[rotation];           // all data for rotation is taken based on enum value (represented by int) taken from database                   
        table.css('top', data['top'](table) + 'px');
        table.css('left', data['left'](table) + 'px');

        table.closest('.table-rect-area').css('width', data['width'](table) + 'px');                   // uses func from strategy to change width of the table place holder
        table.closest('.table-rect-area').css('height', data['height'](table) + 'px');
    },

    resizeStageArea = function (stage, rotation) {
        var data = resizeStageStrategy[rotation];
        stage.parent().css('height', data.height + 'px');
        
        removeStgPrefixedCssClasses(stage);                                     // always as a first step remove all additional stg- prefixed css classes

        if (data.css !== undefined) 
            stage.addClass(data.css);                                           // applying this css class I am able to move stage img into appropriate position
    },

    removeStgPrefixedCssClasses = function (stage) {
        var prefix = 'stg-';
        var classes = stage.attr('class').split(' ').filter(function (c) {
            return c.lastIndexOf(prefix, 0) !== 0;
        });
        stage.attr('class', $.trim(classes.join(' ')));
    },

    calcDiagonalLeft = function (item) {
        return 22 + calcFormula(item);
    },

    calcDiagonalTop = function (item) {
        var count = getSeatsCount(item);
        return 30 - (((count / 2) - 1) * 5);       
    },

    calculateDiagonal = function (item) {
        var width = item.width();
        var height = item.height();

        return Math.ceil(Math.sqrt(Math.pow(width, 2) + Math.pow(height, 2)));  // rect diagonal
    },

    calcVerticalWidth = function () {
        return 150;
    },

    calcVerticalHeight = function (item) {
        return item.height();
    },

    calcVerticalLeft = function(){
        return 15;
    },

    calcVerticalTop = function () {
        return 0;
    },

    calcHorizontalWidth = function (item) {
        var height = item.height() + 38;
        return height > 150 ? height : 150;
    },

    calHorizontalHeight = function (item) {
        return item.width() + 60;
    },

    calcHorizontalLeft = function (item) {
        return 14 + calcFormula(item);
    },

    calcHorizontalTop = function (item) {
        return 60 - calcFormula(item);
    },

    getSeatsCount = function (table) {
        var itemId = getItemId($(table));                       
        var item = items[itemId];
        return Object.keys(item.Seats).length - 2;                              // without bottom and top seats
    },

    calcFormula = function (item) {
        var count = getSeatsCount(item);
        return (((count / 2) - 1) * 16);
    },
                                                                                // used by table retc and stage rect items
    rotationStrategy = {                                                        // current and next correspond to rotation value (represented as enum/int), next is used to know what next state will be after pressing rotate btn (state machine)
        0: { current: 0, next: 1, angle: 0 },                                   // 0 - vertical
        1: { current: 1, next: 2, angle: 45 },                                  // 1 - slant right
        2: { current: 2, next: 3, angle: -90 },                                 // 2 - horizontal
        3: { current: 3, next: 0, angle: -45 }                                  // 3 - slant left
    },

    stageHalfCircleRotationStrategy = {
        0: { current: 0, next: 4, angle: 0 },                                   // 0 - vertical (meaning vertical left)
        4: { current: 4, next: 2, angle: 180 },                                 // 4 - vertical right
        2: { current: 2, next: 5, angle: -90 },                                 // 2 - horizontal (meaning horizontal bottom)
        5: { current: 5, next: 0, angle: 90 }                                   // 5 - horizontal top
    },

    resizeRectTableStrategy = {
        0: { 'width': calcVerticalWidth, 'height': calcVerticalHeight, left: calcVerticalLeft, top: calcVerticalTop },              // 0 - vertical
        1: { 'width': calculateDiagonal, 'height': calculateDiagonal, left: calcDiagonalLeft, top: calcDiagonalTop },               // 1 - slant right
        2: { 'width': calcHorizontalWidth, 'height': calHorizontalHeight, left: calcHorizontalLeft, top: calcHorizontalTop },       // 2 - horizontal
        3: { 'width': calculateDiagonal, 'height': calculateDiagonal, left: calcDiagonalLeft, top: calcDiagonalTop }                // 3 - slant left
    },

    resizeStageStrategy = {
        0: { height: 120 },                                                     // 0 - vertical
        1: { height: 120 },                                                     // 1 - slant right
        2: { css: 'stg-horizontal', height: 70 },                               // 2 - horizontal
        3: { height: 120 },                                                     // 3 - slant left
        4: { css: 'stg-vertical-right', height: 120 },                          // 4 - vertical right
        5: { css: 'stg-horizontal-top', height: 70 }                            // 5 - horizontal top
    },

    changeState = function () {                                                 // is applied for all items of type btn-seats-action, uses seats services to perform some action
        var btn = $(this);
        var type = getStrategyType(btn);                                        // strategy type - rect or round

        var hasChanged = seatsStrategy[type].process(btn.data('state'), btn);   // state data is only applicable for btn which has more that one function in other cases we rely on defined css classes, at the beginning btn which has multiple functions can have no any data defined - in that case we also rely on css class

        if (hasChanged)
            setDirty();
    },

    getStrategyType = function (btn) {
        return btn.attr('name');
    },

    onBallroomDataLoaded = function () {                                    // last step of first loading page - go through seats dict and if seat is occupied get value from persons dict and add it on the page        
        //manageLayoutBtns();                                               // todo maybe? disable or enable btns        
        manageTablesSeats();
        
        if (!$.isEmptyObject(items))                                        // let's assume that if some items are present user has already used ballroom and even if there is no cookie set we don't display image
            return;                                                         // this method is also run after each save so this is also good check to not go further
        
        ballroomIntro.init();                                               // show info if page is first time visited
    },

    createFactories = function () {
        seatFactory = new BallroomSeatFactory();
        itemFactory = new BallroomItemFactory(seatFactory);
    },

    createAndSetupSeatServices = function () {                              // factories objects are needed in this step, they are create before this func call
        seatsRoundService = new SeatsRoundService(seatFactory, items, seatUtils, bEnums);
        $(seatsRoundService).on('roundSeatRendered', onRoundSeatRendered)                               // event attached to seatsRoundService object, triggered when seat was successfully added
                            .on('roundSeatDeleted', onRoundSeatDeleted)
                            .on('seatUnassigned', onSeatUnassigned);

        seatsRectService = new SeatsRectService(seatFactory, items, seatUtils, bEnums);
        $(seatsRectService).on('seatsRowRendered', onSeatsRowRendered)                                  // triggered when seats row was successfully added
                           .on('seatsRowDeleted', onSeatsRowDeleted)
                           .on('seatUnassigned', onSeatUnassigned);

        seatsStrategy['rect'] = seatsRectService;                                                       // creates two strategies
        seatsStrategy['round'] = seatsRoundService;
    },

    manageTablesSeats = function () {
        $.each(items, function (id, itemObj) {
            if (itemObj.Seats === undefined)
                return true;

            $.each(itemObj.Seats, function (sid, seatObj) {                     // seats is a dictionary where sid (seat id) is key and seatObj is value
                if (seatObj.Hidden)
                    seatsStrategy['rect'].hideSeat(sid);                        // it is only possible to hide seat for rect table

                if (!seatObj.Occupied)
                    return true;

                var pid = seatObj.TakenBy !== null ? bEnums.newlywedsType[seatObj.TakenBy] : seatObj.PersonId;
                assignPersonToSeat(sid, pid);
            });
        });
    },

    assignPersonToSeat = function (sid, pid) {                                  // pid is id from droppable, can be number (from db) or string in case of dragging newlyweds        
        var seat = $('div[sid=' + sid + ']');                                   // based on seat class decide what strategy should be used
        var type = seat.hasClass('chair-round') ? 'round' : 'rect';

        var data = $.isNumeric(pid) ?
                   createDataForPersonAssignment(sid, pid) :
                   createDataForNewlywedsAssignment(sid, pid);

        seatsStrategy[type].onAssign(data);
        disablePerson(pid);        
    },

    createDataForPersonAssignment = function (sid, pid) {               // pid is a number in that case
        return {
            assignmentType: bEnums.assignmentType.Person,
            sid: sid,
            pid: pid,                                                   // pid must be passed cause in person obj there is no id
            person: persons[pid]
        };
    },

    createDataForNewlywedsAssignment = function (sid, pid) {            // pid is 'groom' or 'bride' string in this case 
        return {
            assignmentType: bEnums.assignmentType.Newlyweds,            // for choosing appropriate strategy inside seats.service.base
            sid: sid,
            person: { Name: getPerson(pid).text() },                      // text is taken from dom element cause it can be translated,
            takenBy: bEnums.takenBy[pid]
        };
    },

    getPerson = function (pid) {
        return $('li[pid=' + pid + ']');
    },

    onRoundSeatRendered = function (event, addedItem, tid) {
        var layoutWidth = layout.width();                               // because of scroll-y, width must be assigned before calling resize func. scroll-y causes area being expanded automatically
        var layoutHeight = layout.height();

        enhanceRenderedItem(addedItem);
        resizeCircleTableRelatedItems(tid);

        expandLayout(tid, layoutWidth, layoutHeight);
    },

    onRoundSeatDeleted = function (event, tid) {
        resizeCircleTableRelatedItems(tid);
    },

    onSeatsRowRendered = function (event, addedItem, tid) {             // we apply here just events for seat row 
        var layoutWidth = layout.width();                               // because of scroll-y, width must be assigned before calling resize func. scroll-y causes area being expanded automatically
        var layoutHeight = layout.height();

        enhanceRenderedItem(addedItem);
        resizeOnRectSeatsRowEvent(tid);

        expandLayout(tid, layoutWidth, layoutHeight);
    },

    onSeatsRowDeleted = function (event, tid, personsIds) {
        enablePersonsDraggable(personsIds);
        resizeOnRectSeatsRowEvent(tid);
    },

    expandLayout = function (tid, layoutWidth, layoutHeight) {
        var item = items[tid];                                          // ballroom item                 

        if (areaOverlapsItemWidth(item, layoutWidth))
            increaseLayoutWidth();
        
        if (areaOverlapsItemHeight(item, layoutHeight))
            increaseLayoutHeight();      
    },

    enablePersonsDraggable = function (personsIds) {
        $.each(personsIds, function (i, pid) {
            enablePerson(pid);            
        });
    },

    onSeatUnassigned = function (event, pid) {
        enablePerson(pid);                                              // after seat is unassigned enable person on the list
    },

    enablePerson = function (pid) {
        var li = getPerson(pid);
        li.draggable('enable');
        li.find('.list-item-handle').removeClass('assigned');
    },

    disablePerson = function (pid) {
        var li = getPerson(pid);
        li.draggable('disable');                                        // disable person on the list
        li.find('.list-item-handle').addClass('assigned');              // mark item as selected
    },

    resizeOnRectSeatsRowEvent = function (tid) {
        var table = $('#item-' + tid);
        resizeRectTableArea(table, table.data('rotation'));
    },

    enhanceRenderedItem = function (addedItem) {
        applySeatsDroppable(addedItem);
        applySeatsMouseEvents(addedItem);
        enhanceSeatsActionBtns(addedItem);
        applyTableMouseEvents(addedItem);
    },

    applySeatsDroppable = function (area) {
        var chairs = area === undefined ? $('.chair') : area.find('.chair');
        chairs.droppable({                                                      // assigns droppable functionality during initialization or inside added element
            accept: '.list-group-item',
            drop: onPersonDroppedToSeat,
            activeClass: 'seat-active',
            hoverClass: 'seat-hover',
            tolerance: 'pointer'
        });
    },

    applySeatsMouseEvents = function (area) {
        var seatsArea = area === undefined ? $('.chair-area') : area.find('.chair-area');
        seatsArea.mouseenter(function () { $(this).find('.btn-seats-action').show(); })
                 .mouseleave(function () { $(this).find('.btn-seats-action').hide(); });
    },

    enhanceSeatsActionBtns = function (area) {
        var actionBtns = area === undefined ? $('.btn-seats-action') : area.find('.btn-seats-action');
        actionBtns.click(changeState);
    },

    applyTableMouseEvents = function (area) {
        var table = area === undefined ? $('.table-rect') : area.find('.table-rect');
        table.mouseenter(function () { $(this).find('.btn-delete-row').show(); })
             .mouseleave(function () { $(this).find('.btn-delete-row').hide(); });
    },

    enhanceItemPanelBtns = function (area) {
        var btnShow = area === undefined ? $('.btn-show-items') : area.find('.btn-show-items');
        btnShow.click(function () {
            bringToFront($(this));

            $(this).parent().hide('slide', 200, function () {
                $(this).next().fadeIn(200);
            });
        });

        var btnHide = area === undefined ? $('.btn-hide-items') : area.find('.btn-hide-items');
        btnHide.click(function () {
            $(this).parent().fadeOut(200, function () {
                $(this).prev().show('slide', 200);
            });
        });

        var btnDelete = area === undefined ? $('.btn-delete-item') : area.find('.btn-delete-item');
        btnDelete.click(deleteItem);
    },

    bringToFront = function (btn) {
        var item = btn.closest('.ballroom-item');               // trick to brign item to the front
        item.parent().append(item);
    },

    deleteItem = function () {
        var itemId = getItemId($(this));                        // this is btn

        var item = items[itemId];
        managePersonsPanel(item.Seats);                         // applicable only for tables, enable draggable for persons that were assigned to deleted items seats
        delete items[itemId];                                   // remove from dictionary

        var toDelete = $('#' + itemId);
        toDelete.remove();
        setDirty();
    },

    managePersonsPanel = function (seats) {
        if (seats === undefined)
            return;

        $.each(seats, function (sid, seatObj) {
            var pid = seatObj.PersonId;
            if (pid != null)
                enablePerson(pid);
        });
    },

    onBallroomItemDragEnd = function (event, ui) {
        var itemId = ui.helper.attr('id');                                      // ui is draggable item
        var positionX = parseInt(ui.position.left);
        var positionY = parseInt(ui.position.top);

        items[itemId].PositionX = positionX;
        items[itemId].PositionY = positionY;
        setDirty();
    },

    resizeCircleTableRelatedItems = function (tid) {
        var tableArea = $('#item-' + tid);                                      // table round area     
        var count = tableArea.find('.chair-round-area').length;

        resizeRoundTableArea(tableArea, count);
        resizeCircleTable(tableArea, count);
    },

    resizeRoundTableArea = function (tableArea, count) {
        var size = count < 7 ? 150 : count < 9 ? 170 : count < 11 ? 180 : 220;
        tableArea.css({ width: size + 'px', height: size + 'px' });
    },

    resizeCircleTable = function (tableArea, count) {
        var table = tableArea.find('.table-round');
        var size = count < 7 ? 46 : count < 9 ? 66 : count < 11 ? 96 : 116;
        table.css({ width: size + 'px', height: size + 'px' });
    },

    getItemId = function (item) {
        return item.closest('.ballroom-item').attr('id');
    };

    return {
        init: init
    };
}();