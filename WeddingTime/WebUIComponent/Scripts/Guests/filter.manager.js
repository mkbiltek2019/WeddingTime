'use strict';
var filterManager = function () {

    var input, btn, icon,
    innerHidden = [],
    elements = {},                                                          // dict which holds data for each group
    //clearFilter = false,                                                  // to not invoke showAll method if it has been already called
    keyCodesAllowed = [8, 9, 16, 17, 33, 34, 35, 36, 37, 38, 39, 40, 46],   // backspace, tab, shift, ctrl, page up, page down, end, home, left arrow, up arrow, right arrow, down arrow, delete

    init = function () {
        initVariables();

        input.keyup(process)
             .keydown(isKeyAllowed);

        btn.click(function () {
            if (icon.hasClass('icon-cancel')) {
                formUtils.clearInput('txtSearch');
                process();
            }
        });
    },

    initVariables = function () {
        input = $('#txtSearch');
        btn = $('#btnSearch');
        icon = btn.children().first();
    },

    populate = function (gid, members) {
        var list = {};                                                      // create list as a json object to have quicker possibility to remove specyfic persons
        $.each(members, function (i, m) {                                   // have to duplicate id due to process function
            list[m.Id] = { gid: m.GroupId, name: concat(m.Name, m.Surname), isInnerGroupMember: m.IsInnerGroupMember };
        });

        elements[gid] = list;
    },

    remove = function (gid) {
        if (elements[gid] != null)
            delete elements[gid];                                           // delete from dictionary
    },

    move = function (gidFrom, gidTo, ids) {                                 // ids are person ids moved between groups
        $.each(ids, function (i, id) {
            var value = parseInt(id);

            var itemToMove = elements[gidFrom][value];
            itemToMove.gid = parseInt(gidTo);                               // have to update group id inside item

            if (elements[gidTo] === undefined)                              // if group is empty and we move item we have to create element before moving it
                elements[gidTo] = {};

            elements[gidTo][value] = itemToMove;

            delete elements[gidFrom][value];
        });
    },

    applyFilter = function () {                     // is invoked by person manager after getting persons (after delete, update, adding)
        var value = getValue();                     // just after applying, filter function to recalculate sticky items is invoked, it is not needed to call this function from here
        if (!value.trim())
            return;

        showInner();
        hideAll();
        filter(value.toLowerCase());
        triggerEvent('filterApplied');
    },

    // private functions

    process = function () {                         // disable sorting when filtering, creating and removing from inner groups..
        var value = getValue();

        showInner();                                // always make visible hidden elments from inner group - they will be hidden if needed again in filter function 

        if (!value.trim()) {                        // string.empty or white space trick | if field is empty show all elements
            showSearchIcon();
            showAll();
            triggerEvent('filterCleared');
        }
        else {
            showClearIcon();
            hideAll();
            filter(value.toLowerCase());
            triggerEvent('filterApplied');
        }

        triggerEvent('filterProcessed');            // this event is used to call recalculate sticky items functions
    },

    isKeyAllowed = function (e) {
        if ($.inArray(e.keyCode, keyCodesAllowed) != -1)
            return true;

        var value = getValue();
        return value.length < 50;                     // max string length that can be insert into search field
    },

    showSearchIcon = function () {
        if (icon.hasClass('icon-search'))
            return;

        btn.removeClass('btn-dismiss').addClass('btn-turquoise');
        icon.removeClass('icon-cancel').addClass('icon-search');
    },

    showClearIcon = function () {
        if (icon.hasClass('icon-cancel'))
            return;

        btn.removeClass('btn-turquoise').addClass('btn-dismiss');
        icon.removeClass('icon-search').addClass('icon-cancel');
    },

    getValue = function () {
        return input.val();
    },

    hideAll = function () {
        var groupItem = $('.group-item');
        groupItem.find('li').hide();
        groupItem.hide();
    },

    showAll = function () {
        var groupItem = $('.group-item');
        groupItem.show();
        groupItem.find('li').show();
    },

    showInner = function () {
        $.each(innerHidden, function (i, item) { item.show(); });
        innerHidden.length = 0;                                                 // clear the array
    },

    filter = function (value) {
        var dictValues = $.map(elements, function (obj, key) {                  // since inside group dict we have another dict grouped by person id we have to invoke map twice
            return $.map(obj, function (val, id) {                              // construct new object that contains also id
                return { id: id, gid: val.gid, name: val.name, isInnerGroupMember: val.isInnerGroupMember };
            });
        });

        var matched = $.grep(dictValues, function (m) {                         // finds the elements of an array which satisfy a filter function. The original array is not affected
            return m.name.indexOf(value) >= 0;                                  // this is how I check for containing text
        });
        var idsMatched = $.map(matched, function (n) { return n.id });          // if item is in inner group we want to hide all others inner group members, but first we have to chek if any of them is on matched ids list - which means that it should stay visible

        $.each(matched, function (i, obj) {
            var li = $('div[pid=' + obj.id + ']').closest('li');

            processInnerGroupMember(idsMatched, obj, li);

            $('div[gid=' + obj.gid + ']').show();                               // first show group
            li.show();                                                          // then show li
        });
    },

    processInnerGroupMember = function (idsMatched, obj, li) {
        if (!obj.isInnerGroupMember)
            return;

        var items = li.find('.list-item-row').not('[pid=' + obj.id + ']');      // get all html elments execpt the current one
        var innerToHide = $.grep(items, function (n) {
            return $.inArray($(n).attr('pid'), idsMatched) == -1;
        });
        $.each(innerToHide, function (i, toHide) {
            var item = $(toHide);
            item.hide();
            innerHidden.push(item);                                             // add to the hidden list to make it visible again inside clear filter function
        });
    },

    triggerEvent = function (eventName) {
        $(filterManager).trigger(eventName);
    },

    concat = function (name, surname) {
        var value = name.toLowerCase();
        return !surname ? value : value + ' ' + surname.toLowerCase();
    };

    return {
        init: init,
        populate: populate,
        remove: remove,
        move: move,
        applyFilter: applyFilter
    };
}();