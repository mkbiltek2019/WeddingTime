'use strict';
var personManager = function () {
    
    var init = function () {
        initCreateModal();
        initEditModal();
        initFilterEvents();
        initReports();
    },

    initCreateModal = function () {
        var settings = { populateFunc: guestsService.createNewPerson, ajaxDataFunc: complementCreateAjaxData, successFunc: onDialogActionSucceded };
        dialogAnimation.init('createPersonsModal', settings);
    },

    initEditModal = function () {
        var settings = { type: 'UPDATE', populateFunc: guestsService.prepareEdit, ajaxDataFunc: complementEditAjaxData, successFunc: onDialogActionSucceded };
        dialogAnimation.init('editPersonsModal', settings);
    },

    initFilterEvents = function () {
        $(filterManager).on('filterCleared', onFilterCleared)
                        .on('filterApplied', onFilterApplied)
                        .on('filterProcessed', stickyManager.recalculateItems);
    },
    
    initReports = function () {
        $('#btnReports').click(function () {
            $(this).toggleClass('active');
            $('#reports').toggle('blind', 300, function () {
                stickyManager.recalculateItems();               // has to recalculate items after options are shown
            });            
        });
    },

    complementCreateAjaxData = function (item, ajaxData) {      // function used to complement ajax data for creating person modal
        ajaxData.groupId = getGroupId($(item));
    },

    complementEditAjaxData = function (item, ajaxData) {        // function used to complement ajax data for updating person modal
        var gid = getGroupId($(item));

        var selected = formUtils.getSelectedCheckboxes($('#members-' + gid));
        var ids = getPersonIds('up', selected);

        ajaxData.groupId = gid;
        ajaxData.ids = ids;
    },

    onDialogActionSucceded = function (item) {          // I am sure that item (relatedTarget) is btn for adding or updating persons     
        var gid = getGroupId($(item));
        getPersonsInternally(gid);                      // on create, update - because of complicated structure and on update some new items can be added
    },

    getPersons = function (gid) {                       // this is function called by group manager
        return callGetPersons(gid, showPersons);        // return is needed for deferreds on group manager side 
    },

    getPersonsInternally = function (gid) {             // this function is called internally by create, update, delete, undo, inner members actions
        callGetPersons(gid, showPersonsWithActions);
    },

    callGetPersons = function (gid, callbackFunc) {
        var ajaxSettings = { success: callbackFunc, successCallbackData: { gid: gid } };
        return fetchPersons(gid, ajaxSettings);        
    },

    fetchPersons = function (gid, ajaxSettings) {
        var groupItem = getGroupItem(gid);
        var exAjaxSettings = uiManager.wrappAjaxWithUI(ajaxSettings, [groupItem]);

        var data = { groupId: gid };
        return guestsService.getPersons(data, exAjaxSettings);
    },

    showPersons = function (data, callbackData) {
        processPersons(data, callbackData);
    },

    showPersonsWithActions = function (data, callbackData) {                    // each time when we reload persons list
        var gid = callbackData.gid;
        filterManager.remove(gid);
        membersInfoManager.remove(gid);
        processPersons(data, callbackData);

        triggerMembersInfoRefresh(gid);                                         // refresh members info panels for group if needed
        hideMembersPanel(gid);        
        stickyManager.recalculateItems();
    },

    processPersons = function (data, callbackData) {
        var gid = callbackData.gid;
        var members = $('#members-' + gid);                                     // ul element that holds all group members
        
        members.html(data.View);

        enhanceMembers(members);
        applyDragAndDrop(gid);                                                  // must be done using gid cause we want to allow to create inner groups only inside specyfic group

        $('#membersCount-' + gid).html(data.Members.length);                    // ???!!!

        membersInfoManager.populate(gid, data.Members);                         // add to members info
        filterManager.populate(gid, data.Members);                              // add to filter list
        filterManager.applyFilter();
    },

    enhanceMembers = function (area) {                                     
        area.find('.btn-unchain').click(detachFromInnerGroup);                  // only in the scope of newly added element to the dom
        area.find('.btn-person-details').click(togglePersonDetails);
        area.find('.list-item-checkbox').change(onCheckBoxStateChanged);
    },

    triggerMembersInfoRefresh = function (gid) {
        $('#btnMembersInfo-' + gid).trigger('refresh');
    },

    undo = function (data) {
        undoUtils.hideUndo();
        getPersonsInternally(data);                                             // data is group id, call it since again I can undo inner groups, and it is easier to generate it from the begining        
    },

    onCheckBoxStateChanged = function () {                                      // on checkbox changed
        var btn = $(this);
        btn.children().first().toggleClass('active');

        var groupMembersArea = btn.closest('.group-members');
        toggleMembersPanelVisibility(groupMembersArea);
    },

    toggleMembersPanelVisibility = function (groupMembersArea) {
        var gid = getGroupId(groupMembersArea);

        var isAnySelected = formUtils.getSelectedCheckboxes(groupMembersArea).length > 0;
        if (isAnySelected)
            showMembersPanel(gid);
        else
            hideMembersPanel(gid, { duration: 120, effect: 'blind' });
    },

    showMembersPanel = function (gid) {
        var panel = $('#membersPanel-' + gid);
        if (!panel.data('isVisible')) {
            stickyManager.tickMembers(panel);                               // trigger event only for specyfic element                             
            panel.show('blind', 200);
            panel.data('isVisible', true);                                  // introduced isVisible data to be able to animate sticky bottom item for mobile devices
        }
    },

    hideMembersPanel = function (gid, options) {
        var panel = $('#membersPanel-' + gid);
        if ($.isPlainObject(options))                                       // in some cases animate while hiding
            panel.hide(options);        
        else
            panel.hide();        

        panel.data('isVisible', false);
    },

    enhanceMembersArea = function (target) {
        var members = target === undefined ? $('.group-members') : target.find('.group-members');
        members.sortable({
            update: updateSortNumber,
            handle: '.list-item-handle',
            connectWith: '.list-group',
            receive: moveBetweenGroups,
            start: onStartMembersSorting,
            stop: onStopMembersSorting
        });
    },

    enhanceMembersBtns = function (target) {
        var btnClearSelected = target === undefined ? $('.btn-clear-selected') : target.find('.btn-clear-selected');
        btnClearSelected.click(function () {
            var gid = getGroupId($(this));
            clearSelectedPersons(gid);
            hideMembersPanel(gid, { duration: 120, effect: 'blind' });
        });

        var btnDelMembers = target === undefined ? $('.btn-delete-members') : target.find('.btn-delete-members');
        btnDelMembers.click(function () {
            var gid = getGroupId($(this));
            var groupItem = getGroupItem(gid);

            var selected = formUtils.getSelectedCheckboxes($('#members-' + gid));
            var ids = getPersonIds('up', selected);

            var ajaxSettings = uiManager.wrappAjaxWithUI(createBaseAjaxSettings(), [groupItem]);
            var data = JSON.stringify(createPersonModificationModel(gid, ids));

            $.when(guestsService.deletePersons(data, ajaxSettings)).done(function (data) {
                onPersonsDeleted(data, gid);                            // done in this way to be able to block the same ui area once again
            });
        });
    },
            
    clearSelectedPersons = function (gid) {                             // used by clear btn (gid required) or while filtering (then clear all selection)
        var items = gid === undefined ? $('.group-members') : $('#members-' + gid);
        var selected = formUtils.getSelectedCheckboxes(items);          // because bootstrap sets active class on parent elements
        selected.removeAttr('checked');
        selected.parent().removeClass('active');
    },

    onPersonsDeleted = function (data, gid) {                           // do not show undo yet, pass it to the get persons success func
        var ajaxSettings = {
            success: deleteProcessCompleted,
            successCallbackData: { view: data, gid: gid },
            error: getPersonsOfDeleteProcessFailed,                     // if error occured in this step do some actions and show undo panel which is available in this state
            errorCallbackData: { view: data, gid: gid }
        };
        fetchPersons(gid, ajaxSettings);
    },

    deleteProcessCompleted = function (data, callbackData) {
        showPersonsWithActions(data, callbackData);
        showUndo(callbackData.view);
    },

    getPersonsOfDeleteProcessFailed = function (xhr, callbackData) {
        clearSelectedPersons(callbackData.gid);                         // additionally clear selected items and hide panel
        hideMembersPanel(callbackData.gid);                             
        showUndo(callbackData.view);        
    },

    showUndo = function (view) {
        var undoSettings = { 'success': 'personManager.undo' };
        var data = { view: view, uiItems: ['#guestsContainer'] };
        undoUtils.showUndo(data, undoSettings);
    },

    applyDragAndDrop = function (gid) {
        $('.btn-chain-' + gid).draggable({
            revert: 'invalid',
            revertDuration: 200,
            zIndex: 999
        });

        $('.single-person-' + gid).droppable({                          // now we can drop connect icon on the whole row, not only on the icon - it is better when it comes to small devices
            accept: function (droppable) {
                var gid = getGroupId($(this));
                var pid = getPersonId('down', $(this));
                var droppPid = getPersonId('up', droppable);

                return pid != droppPid && droppable.hasClass('btn-chain-' + gid);
            },
            hoverClass: 'can-drop',
            drop: createInnerGroup
        });

        $('.connect-to-inner-group-' + gid).droppable({
            accept: '.btn-chain-' + gid,
            hoverClass: 'can-drop',
            drop: addInnerGroupMember
        });
    },

    togglePersonDetails = function () {
        var btn = $(this);
        btn.toggleClass('active');
        btn.children().first().toggleClass('icon-expand icon-collapse');

        var pid = getPersonId('up', btn);
        $('#person-details-' + pid).toggle();
        stickyManager.recalculateItems();                                           // has to recalculate
    },

    // function invoked on sorting elements
    // is also invoked while moving between groups but first codition recognizes this situation and stops processing   
    // on item start dragging, count of items is saved as data attr inside ul element
    updateSortNumber = function (e, ui) {        
        var parent = $(e.target);                                                   // gets ul element
        if (ui.sender != null || membersCountChanged(parent))
            return;

        var gid = getGroupId(parent);

        var draggable = ui.item;
        var ids = getPersonIds('down', draggable);                                  // items to update sort no
        var baseItemId = getBaseItemId(parent.children(), draggable.index());

        var data = JSON.stringify({
            groupId: gid,
            model: createPersonUpdateModel(ids, baseItemId, baseItemId == null ? 'SortAsLast' : 'Sort')
        });
        
        var settings = createBaseAjaxSettings();
        settings.success = bringBackMembersPanel;
        settings.error = onUpdateSortNumberFailure;
        settings.errorCallbackData = { draggable: draggable, gid: gid };
        
        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [draggable]);
        guestsService.updatePersonOrderNo(data, ajaxSettings);
        
        draggable.data('processed', true);                                          // create this element in ui object to indicate that action on item was performed - means that on stop func we are not going to perform any additional action
    },

    onStartMembersSorting = function (e, ui) {
        saveMembersCount($(this));                                                  // this represents parent - ul element
        saveMembersIndex(ui.item);                                                  // ui item represents draggable
        $('.stick-bottom-panel').hide().data('isVisible', false);                   // hide all stick bottom panels
    },

    onStopMembersSorting = function (e, ui) {
        if (ui.item.data('processed'))
            return;

        bringBackMembersPanel();                        // show panel again if no any action was performed, if so panel will be shown on action succeeded
    },

    bringBackMembersPanel = function () {               // on start sorting I hide all panels, after sorting is done (internal or move between group) this func is invoked to show members panel for area that has selected members
        var groupMembersAreas = $('.group-members');
        $.each(groupMembersAreas, function () {
            toggleMembersPanelVisibility($(this));
        });
    },

    saveMembersCount = function (parent) {              // parent is ul element
        var count = getMembersCount(parent);
        parent.data('count', count);
    },

    saveMembersIndex = function (draggable) {
        draggable.startIndex = draggable.index();
    },

    membersCountChanged = function (parent) {
        var count = getMembersCount(parent);
        var prevCount = parent.data('count');

        return prevCount == count ? false : true;
    },

    getMembersCount = function (parent) {
        return parent.find('li').not('.ui-sortable-placeholder').length;
    },

    moveBetweenGroups = function (e, ui) {
        var target = $(e.target);
        var draggable = ui.item;

        var baseItemId = getBaseItemId(target.children(), draggable.index());      // index on new list

        var gidFrom = getGroupId($(ui.sender));
        var gidTo = getGroupId(target);

        var ids = getPersonIds('down', draggable);

        var modelFrom = createPersonModificationModel(gidFrom, ids, getKey('down', draggable));
        var updateModel = createPersonUpdateModel(ids, baseItemId, baseItemId == null ? 'SortAsLast' : 'Sort');

        var data = JSON.stringify({ groupIdTo: gidTo, modelFrom: modelFrom, updateModel: updateModel });
        
        var settings = createBaseAjaxSettings();
        settings.successCallbackData = { gidFrom: gidFrom, gidTo: gidTo, ids: ids };
        settings.success = onMovedBetweenGroups;
        settings.error = onMoveBetweenGroupsFailure;
        settings.errorCallbackData = { draggable: draggable, gidFrom: gidFrom };

        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [getGroupItem(gidFrom), getGroupItem(gidTo)]);
        guestsService.moveBetweenGroups(data, ajaxSettings);

        draggable.data('processed', true);
    },

    onMovedBetweenGroups = function (callbackData) {
        var gidFrom = callbackData.gidFrom;
        var gidTo = callbackData.gidTo;
        var ids = callbackData.ids;

        membersInfoManager.move(gidFrom, gidTo, ids);
        filterManager.move(gidFrom, gidTo, ids);

        triggerMembersInfoRefresh(gidFrom);
        triggerMembersInfoRefresh(gidTo);

        updateMembersCount(gidFrom, ids.length, 'decrease');
        updateMembersCount(gidTo, ids.length, 'increase');

        updateMovedItems(ids, gidFrom, gidTo);

        stickyManager.recalculateItems();
        bringBackMembersPanel();
    },

    onMoveBetweenGroupsFailure = function (xhr, callbackData) {
        var draggable = callbackData.draggable;
        var startIndex = draggable.startIndex;
        var parent = $('#members-' + callbackData.gidFrom);
                                                                                        // parent count data is saved after starting dragging item
        if ((parent.data('count') - 1) === startIndex)                                  // case for last element, covers also single element case, which can be treated also as last one
            parent.append(draggable);
        else
            parent.find('li:eq(' + startIndex + ')').before(draggable);                 // start index is saved after starting dragging item

        bringBackMembersPanel();
    },

    onUpdateSortNumberFailure = function (xhr, callbackData) {
        var draggable = callbackData.draggable;
        var startIndex = draggable.startIndex;
        var item = $('#members-' + callbackData.gid + ' li:eq(' + startIndex + ')');    // item used to place draggable after or before it

        if (draggable.index() < startIndex)                                             // draggable index is current index after item has been moved, if less than start index means that it is moving up the list
            item.after(draggable);                                                      // draggable has to placed after appropriate item
        else
            item.before(draggable);                                                     // draggable has to placed before appropriate item

        bringBackMembersPanel();
    },

    updateMembersCount = function (gid, changeCount, actionType) {
        var placeholder = $('#membersCount-' + gid);
        var newCount = membersCountStrategy[actionType](placeholder.html(), changeCount);
        placeholder.html(newCount);
    },

    membersCountStrategy = {
        'increase': function (value, increaseCount) { return parseInt(value) + increaseCount; },
        'decrease': function (value, decreaseCount) { return parseInt(value) - decreaseCount; }
    },

    updateMovedItems = function (ids, gidFrom, gidTo) {
        var pid = ids[0];                                                           // at least one item is moved, if inner group is moved I need just first item to get parent, and based on parent I will find all others needed elements
        var item = $('div[pid=' + pid + ']');                                       // get parent of first element which in case of having more than one means that this is parent for all items

        var parent = item.parent();                                                 // there is no need to detach droppable or draggable events 
        toggleItemClass(parent.find('.btn-chain-' + gidFrom), gidFrom, gidTo, 'btn-chain');        

        if (ids.length === 1) {
            toggleItemClass(parent, gidFrom, gidTo, 'single-person');
        }
        else {                                                                      // meanse we have more that one item
            toggleItemClass(parent, gidFrom, gidTo, 'connect-to-inner-group');
            parent.droppable({ accept: '.btn-chain-' + gidTo });                    // change also in this step accept class for droppable
        }
    },

    toggleItemClass = function (target, gidFrom, gidTo, className) {
        target.toggleClass(className + '-' + gidFrom + ' ' + className + '-' + gidTo);
    },

    // gets an item id from which we should update sort numbers in database, item above dropped element is taken
    // if itemsOnList.length == indexOnList + 1 means that this is the last element on list - return null which means sort as last
    // else get an item above dropped element on new list which index is defined by droppedIndex
    getBaseItemId = function (itemsOnList, droppedIndex) {
        return (itemsOnList.length == droppedIndex + 1) ? null : getPersonId('down', itemsOnList.eq(droppedIndex + 1));
    },

    createInnerGroup = function (e, ui) {
        var gid = getGroupId($(this));
        var draggable = ui.draggable;
        var droppId = getPersonIds('down', $(this));
        var draggId = getPersonIds('up', draggable);

        var modificationModel = createPersonModificationModel(gid, $.merge(droppId, draggId));
        var updateModel = createPersonUpdateModel(draggId, droppId[0], 'Join');

        var data = JSON.stringify({ personModel: modificationModel, updateModel: updateModel });       
        var ajaxSettings = uiManager.wrappAjaxWithUI(getInnerGroupActionAjaxSettings(gid, draggable), [getGroupItem(gid)]);

        $.when(guestsService.createInnerGroup(data, ajaxSettings))
         .done(function () { getPersonsInternally(gid); });
    },

    addInnerGroupMember = function (e, ui) {
        var key = $(this).attr('key');

        var draggable = ui.draggable;
        var gid = getGroupId(draggable);
        var ids = getPersonIds('up', draggable);

        var baseItemId = getPersonId('down', $(this));          // base element id, from which order numbers will be changed

        var modificationModel = createPersonModificationModel(gid, ids, key);
        var updateModel = createPersonUpdateModel(ids, baseItemId, 'Join');

        var data = JSON.stringify({ personModel: modificationModel, updateModel: updateModel });        
        var ajaxSettings = uiManager.wrappAjaxWithUI(getInnerGroupActionAjaxSettings(gid, draggable), [getGroupItem(gid)]);

        $.when(guestsService.addInnerGroupMember(data, ajaxSettings))
         .done(function () { getPersonsInternally(gid); });
    },

    detachFromInnerGroup = function () {
        var gid = getGroupId($(this));
        var key = getKey('up', $(this));
        var ids = getPersonIds('up', $(this));

        var modificationModel = createPersonModificationModel(gid, ids, key);
        var updateModel = createPersonUpdateModel(ids, null, 'Detach');

        var data = JSON.stringify({ personModel: modificationModel, updateModel: updateModel });
        var ajaxSettings = uiManager.wrappAjaxWithUI(createBaseAjaxSettings(), [getGroupItem(gid)]);
                
        $.when(guestsService.detachInnerGroupMember(data, ajaxSettings))
         .done(function () { getPersonsInternally(gid); });
    },

    getInnerGroupActionAjaxSettings = function (gid, draggable) {
        var ajaxSettings = createBaseAjaxSettings();
        ajaxSettings.error = onInnerGroupActionFailure;
        ajaxSettings.errorCallbackData = { draggable: draggable };
        return ajaxSettings;
    },

    onInnerGroupActionFailure = function (xhr, callbackData) {
        callbackData.draggable.css({ top: 0, left: 0 });                    // revert draggable to initial position
    },

    createBaseAjaxSettings = function () {
        return { type: 'POST', contentType: 'application/json; charset=utf-8' };
    },

    createPersonModificationModel = function (gid, ids, key) {              // used to create structure that represents model on server side
        return {
            GroupId: gid,
            ModifiedPersonIds: ids,
            InnerGroupKey: key
        };
    },

    createPersonUpdateModel = function (ids, baseItemId, updateType) {      // used to create structure that represents model on server side
        return {    
            PersonIdsToUpdate: ids,
            BaseItemId: baseItemId,
            UpdateType: updateType
        };
    },

    onFilterCleared = function () {
        var groupMembers = $('.group-members');

        if (groupMembers.sortable('option', 'disabled'))
            groupMembers.sortable('enable');
    },

    onFilterApplied = function () {
        var groupMembers = $('.group-members');

        if (!groupMembers.sortable('option', 'disabled'))
            groupMembers.sortable('disable');

        clearSelectedPersons();                                         // clear selected expenses becasue if item is selected and will be filtered out, panel would stay visible
        $('.stick-bottom-panel').hide().data('isVisible', false);       // hide all stick bottom panels
    },

    // if key is equals to up means get closest elements, oposite is the down key
    getPersonIds = function (key, element) {       
        var items = getPersonItems(key, element);
        return $.map(items, function (item) {
            return $(item).attr('pid');
        });
    },
    
    getPersonId = function (key, element) {
        var item = getPersonItems(key, element).first();                // always down!
        return item.attr('pid');
    },

    getPersonItems = function (key, element, selector) {
        var selector = '.list-item-row';
        return getStrategy(key, element, selector);
    },

    getKey = function (key, element) {
        var selector = 'div[class*=connect-to-inner-group]';            // attribute contains selector, other options: attribute starts with selector - ^=
        var item = getStrategy(key, element, selector).first();

        return item.attr('key');   
    },

    getGroupItem = function (gid) {
        return $('div[gid=' + gid + ']');
    },

    getGroupId = function (element) {
        return element.closest('.group-item').attr('gid');
    },

    getStrategy = function (key, element, selector) {
        return (key === 'down') ? element.find(selector) : ((key === 'up') ? element.closest(selector) : null);
    };

    return {
        init: init,
        getPersons: getPersons,
        enhanceMembersBtns: enhanceMembersBtns,
        enhanceMembersArea: enhanceMembersArea,
        undo: undo
    };
}();