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

    // CHANGED
    populate = function (expenses) {        
        $.each(expenses, function (eid, expense) {
            elements[eid] = { desc: expense.Desc, eid: eid };
        });        
    },

    remove = function (eid) {
        if (elements[eid] != null)
            delete elements[eid];                   // delete from dictionary
    },

    update = function (eid, desc) {
        if (elements[eid] != null)
            elements[eid].desc = desc;
    },

    clear = function () {
        formUtils.clearInput(input.attr('id'));
    },

    applyFilter = function () {                     // is invoked by expenses manager after getting expenses (after delete, update, adding)
        var value = getValue();                     // just after applying filter, function to recalculate sticky items is invoked, it is not needed to call this function from here
        if (!value.trim())
            return;

        hideAll();
        filter(value.toLowerCase());
        triggerEvent('filterApplied');
    },

    // private functions

    // CHANGED
    process = function () {                         // disable sorting when filtering, creating and removing from inner groups..
        var value = getValue();

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

    // CHANGED
    hideAll = function () {
        var expensesList = $('#expensesList');
        expensesList.find('li').hide();
    },

    // CHANGED
    showAll = function () {
        var expensesList = $('#expensesList');
        expensesList.find('li').show();
    },

    // CHANGED
    filter = function (value) {
        var dictValues = $.map(elements, function (val, key) { return val; });          // extract only values from dictionary
    
        var matched = $.grep(dictValues, function (m) {                                 // finds the elements of an array which satisfy a filter function. The original array is not affected
            return m.desc.indexOf(value) >= 0;                                          // this is how I check for containing text
        });

        $.each(matched, function (i, obj) {
            var li = $('li[eid=' + obj.eid + ']');
            li.show(); 
        });
    },

    triggerEvent = function (eventName) {
        $(filterManager).trigger(eventName);
    };

    return {
        init: init,
        populate: populate,
        remove: remove,
        update: update,
        clear: clear,
        applyFilter: applyFilter
    };
}();