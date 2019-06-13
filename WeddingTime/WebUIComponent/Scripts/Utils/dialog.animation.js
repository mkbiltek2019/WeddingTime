'use strict';
var dialogAnimation = function () {

    var defaultSettings = {
        type: 'CREATE',                 // CREATE, UPDATE (default: CREATE)
        beforeOpenFunc: null,           // function called just before dialog is opened
        populateFunc: null,             // function to get create or update view
        preparedFunc: null,             // function called when all data from server are applied to dialog
        ajaxDataFunc: null,             // to fill in additional ajax data
        successFunc: null,              // called on ajax success - after items are added or updated
        content: null,                  // place where items are added
        current: 0,                     // index of currently visible view 
        relatedTarget: null,            // item which causes modal to show
        indicators: null,               // area for adding/removing indicators
        btnNext: null,                  // btn for moving to/creating next item
        btnPrev: null,                  // btn for moving to the previous item
        btnDelete: null,                // btn for deleting form item (available only for CREATE type)
        btnSubmit: null,                // btn for submitting the form, used for changing availability status
        isSubmitted: false,             // flag to indicate if all data were successfully saved on the server
        form: null,                     // modal's form
        invalidInput: null,             // holds invalid input dom object
        data: null                      // holds json data after successful update or items creation
    },

    isMobile = $.browser.mobile,
    modals = {},                        // list of registered modals
    currentId = null,                   // remember currently opened modal
    direction = null,                   // animation direction - Left or Right
    maxCreateCount = 15,

    animations = {
        'hiddenRightToVisible': { start: 500, end: 0 },
        'hiddenLeftToVisible': { start: -500, end: 0 },
        'visibleToHiddenLeft': { start: 0, end: -500 },
        'visibleToHiddenRight': { start: 0, end: 500 }
    },

    enhanceModal = function (modal) {
        modal.find('.btn-anim-modal-prev').click(function () {
            direction = 'Right';
            processBackward();
        });

        modal.find('.btn-anim-modal-next').click(function () {
            direction = 'Left';
            processForward();
        });

        modal.find('.btn-anim-modal-close').click(function () {
            formUtils.hideModal(currentId);
        });

        modal.find('.btn-anim-modal-del-last').click(function () {              // delete last form (available only when type equals CREATE and last element is selected)
            var settings = modals[currentId];
            settings.content.children().last().remove();
            settings.indicators.children().last().remove();

            settings.current--;                                                 // decrement by one - last item is removed now
            deactivateItems(settings);
            animateItems(settings.current);                                     // second argument is not needed - just move hidden item on the left side          
        });

        modal.on('show.bs.modal', function (e) {
            currentId = e.currentTarget.id;
            modals[currentId].relatedTarget = e.relatedTarget;                  // always remember related target (is used when ajaxDataFunc is defined)            

            callBeforeOpenFunc();
        })
        .on('shown.bs.modal', function () {
            processForward();
        })
        .on('hidden.bs.modal', function () {
            invalidateContent();

            var settings = modals[currentId];
            deactivateItems(settings);

            if (settings.isSubmitted) {
                if ($.isFunction(settings.successFunc)) {                       // only call successFunc if dialog was successfully submitted)
                    if (settings.data === null || !settings.data.trim())
                        settings.successFunc(settings.relatedTarget);
                    else
                        settings.successFunc(settings.data, settings.relatedTarget);
                }
                uiManager.unblockUI(getItemsForUIManager());                    // unblock ui only if form was successfully submitted
            }              

            settings.form.trigger('clear.formListener');                        // always set form as not dirty, has to be done before invalidate
            invalidate();            
        });
    },

    callBeforeOpenFunc = function () {
        var settings = modals[currentId];
        if ($.isFunction(settings.beforeOpenFunc))
            settings.beforeOpenFunc();
    },

    setInputFocus = function () {
        if (isMobile)
            return;

        var settings = modals[currentId];
        
        var inputId;
        if (settings.invalidInput != null) {
            inputId = settings.invalidInput;
            settings.invalidInput = null;
        }
        else {
            inputId = settings.content.find('#' + settings.current + '-modal-slice').find('input').first().attr('id');
        }

        formUtils.setFocusById(inputId);
    },

    manageBtnDelete = function () {                                             // manage delete items btn
        var settings = modals[currentId];
        if (settings.type != 'CREATE')
            return;

        var btn = settings.btnDelete;
        if (settings.current != 0 && settings.current == itemsCount(settings.content) - 1) {
            btn.removeClass('disabled');
            btn.show();
        }
        else {
            btn.hide();
        }
    },

    manageNavBtns = function () {
        var settings = modals[currentId];
        if (settings.current != 0)
            settings.btnPrev.removeClass('disabled');
        if (settings.type == 'CREATE' && settings.current != maxCreateCount - 1)
            settings.btnNext.removeClass('disabled');
        if (settings.type == 'UPDATE' && settings.current != itemsCount(settings.content) - 1)
            settings.btnNext.removeClass('disabled');
    },

    invalidate = function () {                      // must be called at the end of ajax process because relateTarget object can be used
        var settings = modals[currentId];
        settings.relatedTarget = null;
        settings.data = null;
        settings.isSubmitted = false;
        currentId = null;
    },

    invalidateContent = function () {
        var settings = modals[currentId];
        settings.content.children().remove();
        settings.indicators.children().remove();
        settings.current = 0;
    },

    processForward = function () {
        var settings = modals[currentId];
        deactivateItems(settings);

        if (settings.type == 'CREATE' && !itemExists(settings)) {
            processCreate(settings);
            return;
        }
        if (settings.type == 'UPDATE' && itemsCount(settings.content) == 0) {       // get only if content contains NO elmeents - if there is at least 1 element just move forward and backward
            processUpdate(settings);
            return;
        }
        
        animateItems(settings.current + 1, settings.current);                       // just move forward - applicable for both CREATE and UPDATE
        settings.current++;
    },

    processCreate = function (settings) {
        var nextIndex = itemsCount(settings.content);

        var ajaxSettings = { success: onItemCreated, successCallbackData: { nextIndex: nextIndex } };
        var ajaxData = { nextIndex: nextIndex };        

        if ($.isFunction(settings.ajaxDataFunc))                                    // complement ajax data if needed
            settings.ajaxDataFunc(settings.relatedTarget, ajaxData);

        var exAjaxSettings = uiManager.wrappAjaxWithUI(ajaxSettings, getItemsForUIManager());
        settings.populateFunc(ajaxData, exAjaxSettings);                            // populate func is ajax function, since we pass ui data to this function, ajax utils functionality takes care about blocking ui and releasing it on success or error
    },

    processUpdate = function (settings) {
        var ajaxSettings = { traditional: true, success: onUpdatePrepared };
        var ajaxData = {};

        if ($.isFunction(settings.ajaxDataFunc))
            settings.ajaxDataFunc(settings.relatedTarget, ajaxData);

        var exAjaxSettings = uiManager.wrappAjaxWithUI(ajaxSettings, getItemsForUIManager());
        settings.populateFunc(ajaxData, exAjaxSettings);                              // populate func is ajax function, since we pass ui data to this function, ajax utils functionality takes care about blocking ui and releasing it on success or error
    },

    processBackward = function () {
        var settings = modals[currentId];
        var current = settings.current;
        if (current == 0)
            return;

        deactivateItems(settings);
        animateItems(settings.current - 1, settings.current);

        settings.current--;
    },

    itemsCount = function (content) {
        return content.children().length;                                               // children are .modal-slice elements
    },

    revertHiddenStateForItems = function () {
        var settings = modals[currentId];
        var current = settings.current;
        settings.content.children().not(':eq(' + current + ')').addClass('hidden');     // set back againg hidden class for all items except current one
    },

    enhanceForm = function (form) {
        form.bind('invalid-form.validate', function (event, validator) {
            form.data('validator').settings.focusInvalid = false;                       // disable auto focus - focus will be managed after animating to invalid element - must be moved here due to plugin changes: http://stackoverflow.com/questions/24575443/jquery-validation-dataform0-validator-settings-returns-undefined

            var elementId = validator.errorList[0].element.id;
            var invalidIndex = $('#' + elementId).closest('.modal-slice').index();         // get index of invalid item (0 base index)

            var settings = modals[currentId];
            settings.invalidInput = elementId;

            revertHiddenStateForItems();                                                // because on submit we remove all hidden classes - to check if any others hidden items are valid we have to set it back here to provide correct behaviour 
            moveToAppointedItem(invalidIndex);
        });
        form.formListener()
            .on('unclean.formListener', function () {
                modals[currentId].btnSubmit.removeClass('disabled');
            })
            .on('clean.formListener', function () {
                modals[currentId].btnSubmit.addClass('disabled');
            })
            .on('submit', function () {                                                 // to prevent from submiting form if button is disabled
                var settings = modals[currentId];
                if (settings.form.data('validator').errorList.length || settings.btnSubmit.hasClass('disabled'))
                    return false;
                                                                                        // check if form is valid - case when current item form is invalid - first invalid event is called then this one and we would remove all hidden classes from all not visible items 
                settings.content.find('.modal-slice').removeClass('hidden');               // if items are hidden they are not taken into account while validating, make them visible                
            });
    },

    moveToAppointedItem = function (moveTo) {                                           // moveTo is an item index
        var settings = modals[currentId];
        var current = settings.current;

        if (moveTo === current) {
            setInputFocus();
            return;
        }

        direction = moveTo < current ? 'Right' : 'Left';

        deactivateItems(settings);
        animateItems(moveTo, settings.current);             // hide currently visible item and show invalid/selected item

        updateItemsBetween(moveTo, current);                // update state data for all items between curent and invalid/selected
        settings.current = moveTo;
    },

    updateItemsBetween = function (moveTo, current) {
        if (isMobile)
            return;

        var startIndex = moveTo < current ? moveTo + 1 : current + 1;
        var stopIndex = moveTo < current ? current : moveTo;

        for (var i = startIndex; i < stopIndex; i++) {
            var item = getItem(i);
            item.data('state', 'hidden' + direction);                           // direction is set just before this func is executed
        }
    },

    complementSettings = function (modal, settings) {
        settings.content = modal.find('.modal-body-content');                   // and content where items are placed
        settings.indicators = modal.find('.items-indicators');
        settings.form = modal.find('form');
        settings.btnPrev = modal.find('.btn-anim-modal-prev');
        settings.btnNext = modal.find('.btn-anim-modal-next');
        settings.btnDelete = modal.find('.btn-anim-modal-del-last');
        settings.btnSubmit = modal.find('form button[type=submit]');
    },

    itemExists = function (settings) {
        return settings.current < itemsCount(settings.content) - 1;
    },

    animateItem = function (current) {
        var item = getItem(current);
        if (item.length === 0)
            return;

        return stateStrategy[item.data('state')](item);
    },

    hiddenRightStrategy = function (item) {
        item.data('state', 'visible');
        item.removeClass('hidden');
        return animate(item, animations['hiddenRightToVisible']);
    },

    hiddenLeftStrategy = function (item) {
        item.data('state', 'visible');
        item.removeClass('hidden');
        return animate(item, animations['hiddenLeftToVisible']);
    },

    visibleStrategy = function (item) {
        item.data('state', 'hidden' + direction);
        return animate(item, animations[direction === 'Left' ? 'visibleToHiddenLeft' : 'visibleToHiddenRight']);
    },

    stateStrategy = {
        'hiddenRight': hiddenRightStrategy,
        'hiddenLeft': hiddenLeftStrategy,
        'visible': visibleStrategy
    },

    onUpdatePrepared = function (data) {
        if (currentId === null)                             // in case dialog was closed before data was returned
            return;

        var settings = modals[currentId];
        settings.content.append(data);

        var count = itemsCount(settings.content);

        for (var i = 0; i < count; i++) {
            manageAppendedData(i);
            createIndicator(settings.indicators, i);
            if (!isMobile) getItem(i).addClass('dialog-edit-item');     // add class that moves all items to the right - it is necessery for edit since a lot of items are added in the same time - all not visible are moved to the right
        }

        settings.indicators.children().show();              // because all indicators are hidded by default
        animateItems(settings.current);
        settings.form.trigger('initialize.formListener');   // when update is prepared, initialize form listener

        onDialogPreparedFunc(settings, settings.form);
    },

    manageAppendedData = function (index) {
        // wyrzucanie oraz ponowne dodawanie, parsowanie walidacji (bindUnobtrusiveValidation_TRY) powowdowalo, 
        // ze zdarzenie oninvalid (przed wywolaniem zdarzenia submit na formie) bylo podpinane kilkukrotnie co 
        // powodowalo niepoprawne zachowanie. dodanie rozszerzenia, ktore udostepnia fnkcje parseDynamicContent
        // nie wyrzucamy walidacji (nigdy) tylko dodajemy nowe wpisy (nowe pola do zwalidowania). Ciekawe jak
        // zachowuje sie w przypadku usuwania? (wydaje sie, ze ok)

        var selector = '#' + index + '-modal-slice';
        $.validator.unobtrusive.parseDynamicContent(selector);                  // parse form to add validation
        if (!isMobile)
            $(selector).data('state', 'hiddenRight');                           // add initial state to the item, only if is not mobile
    },

    onItemCreated = function (data, callbackData) {
        if (currentId === null)                                                 // in case dialog was closed before data was returned
            return;

        var settings = modals[currentId];
        settings.content.append(data);

        var index = callbackData.nextIndex;
        manageAppendedData(index);

        settings.current = index;

        createIndicator(settings.indicators, index);                            // append new indicator
        animateItems(settings.current, settings.current - 1, showIndicator);
        settings.form.trigger('rescan.formListener');                           // rescan form after new items is added (rescan also causes binding)

        onDialogPreparedFunc(settings, $('#' + index + '-modal-slice'));
    },

    animateItems = function (indexA, indexB, callback) {                        // indexA is always item to show, indexB is item to hide
        if (isMobile) {
            var toShow = prepareItemToShow(indexA);
            $.when(hideItemMobile(indexB).promise().pipe(function () { return toShow.animate({ opacity: 1 }, 700); }))
             .done(function () { onAnimationDone(callback); });
        }
        else {
            $.when(animateItem(indexA), animateItem(indexB))
             .done(function () { onAnimationDone(callback); });                 // this is trick to pass arguments inside then func
        }
    },

    prepareItemToShow = function (index) {
        var toShow = getItem(index);
        toShow.removeClass('hidden').css('opacity', 0);                         // setting opacity 0 is only applicable for new items, removing class is only applicable for existing items but it is hard to distinguish this situation, that's why it is done for all cases
        return toShow;
    },

    getItem = function (index) {
        return $('#' + index + '-modal-slice');
    },

    hideItemMobile = function (index) {
        return getItem(index).animate({ opacity: 0 }, 300, function () { $(this).addClass('hidden'); });    // always hide an element to make it not possible to navigate to it using tab
    },

    onAnimationDone = function (callback) {
        activateIndicator();
        manageBtnDelete();
        manageNavBtns();
        setInputFocus();

        if ($.isFunction(callback))
            callback();
    },

    showIndicator = function () {
        var settings = modals[currentId];

        var li = settings.indicators.children().last();
        li.fadeIn(800);
    },

    createIndicator = function (indicators, index) {
        var indicator = $('<li>', { text: index + 1 }).click(function () {      // add click event to indicator
            var index = $(this).index();
            moveToAppointedItem(index);
        });

        indicator.appendTo(indicators).hide();
    },

    onDialogPreparedFunc = function (settings, area) {                          // area is context in which items are placed 
        if ($.isFunction(settings.preparedFunc)) {
            settings.preparedFunc(area);                                        // call this function to be able to attach some events to form fields
        }
    },

    getItemsForUIManager = function () {
        var modalBody = $('#' + currentId).find('.modal-content');
        return [modalBody];
    },

    deactivateItems = function (settings) {                                     // disable del btn and remove active calss from indicator
        deactivateIndicator(settings);
        deactivateBtnDelete(settings);
        deactivateNavBtns(settings);
    },

    deactivateIndicator = function (settings) {
        var activeItem = settings.indicators.find('.active');
        if (activeItem.length != 0)
            activeItem.removeClass('active');
    },

    deactivateBtnDelete = function (settings) {
        if (settings.type != 'CREATE')
            return;

        settings.btnDelete.addClass('disabled');
    },

    deactivateNavBtns = function (settings) {
        settings.btnPrev.addClass('disabled');
        settings.btnNext.addClass('disabled');
    },

    activateIndicator = function () {
        var settings = modals[currentId];
        var item = settings.indicators.children().eq(settings.current);     // current is increment or decrement while moving forward or backward, so it is current position
        item.addClass('active');                                            // item is always available in this step, either new is added or we navigate through existing items
    },

    animate = function (item, animation) {
        return $({ t: animation.start }).animate({
            t: animation.end
        },
		{
		    duration: 400,
		    //easing: 'easeOutBounce',
		    step: function (now) {
		        item.css('transform', 'translate(' + now + 'px, 0)');
		        //item.css('transform', 'rotate(' + now + 'deg)');
		        //item.css('transform', 'scale(' + now + ')');
		        //item.css('transform', 'rotateY(' + now + 'deg)');
		    },
		    done: function () {
		        if (item.data('state').indexOf('hidden') == 0)
		            item.addClass('hidden');                                // hide an element to make it not possible to navigate to it using tab
		    }
		});
    },

    init = function (modalId, customSettings) {     // custom settings should contains only type (if different than default) and populateFunc
        var settings = {};
        $.extend(settings, defaultSettings, customSettings);

        var modal = $('#' + modalId);

        complementSettings(modal, settings);
        modals[modalId] = settings;                 // remeber setting for registered dialog (modal) 

        enhanceModal(modal);                        // wire events
        enhanceForm(settings.form);                 // enhance form placed inside modal
    },

    begin = function () {
        uiManager.blockUI(getItemsForUIManager());
    },

    error = function (xhr) {
        revertHiddenStateForItems();                    // before submit we remove all hidden classes from form items - if error occurs we have to revert this state to get the correct behaviour while using tab key
        uiManager.unblockUI(getItemsForUIManager());    // unblock dialog on error
        errorUtils.showError(xhr);
    },

    success = function (data) {
        var settings = modals[currentId];
        settings.isSubmitted = true;
        settings.data = data;

        formUtils.hideModal(currentId);
    };

    return {
        init: init,
        begin: begin,
        success: success,
        error: error
    };
}();