'use strict';
var seatUtils = function () {

    var getIds = function (area) {                                      // maybe some common, utils function? similar used by person.manager
        var items = area.find('div[sid]');
        return $.map(items, function (item, i) {
            return $(item).attr('sid');
        });
    },

    adjustIconToVisibility = function (btn) {
        var location = btn.parent().data('location');
        adjustToVisibilityStrategy[location](btn);
    },

    adjustIconToAvailability = function (btn) {
        var location = btn.parent().data('location');
        adjustToAvailabilityStrategy[location](btn);
    },

    // private methods

    toggleRightLeftIcon = function (btn) {
        btn.children().first().toggleClass('icon-arrow-right-r icon-arrow-left-g');
    },

    toggleLeftRightIcon = function (btn) {
        btn.children().first().toggleClass('icon-arrow-left-r icon-arrow-right-g');
    },

    toggleDownUpIcon = function (btn) {
        btn.children().first().toggleClass('icon-arrow-down-r icon-arrow-up-g');
    },

    toggleUpDownIcon = function (btn) {
        btn.children().first().toggleClass('icon-arrow-up-r icon-arrow-down-g');
    },

    toggleAvailabilityIcon = function (btn, toReplaceClass) {
        btn.children().first().toggleClass(toReplaceClass + ' icon-remove');
    },

    adjustToVisibilityStrategy = {                                      // this strategy is applicable only for rect table
        'left' : toggleRightLeftIcon,
        'right': toggleLeftRightIcon,
        'top': toggleDownUpIcon,
        'bottom' : toggleUpDownIcon
    },

    adjustToAvailabilityStrategy = {
        'left': function (btn) { toggleAvailabilityIcon(btn, 'icon-arrow-right-r'); },
        'right': function (btn) { toggleAvailabilityIcon(btn, 'icon-arrow-left-r'); },
        'top': function (btn) { toggleAvailabilityIcon(btn, 'icon-arrow-down-r'); },
        'bottom': function (btn) { toggleAvailabilityIcon(btn, 'icon-arrow-up-r'); },
        undefined: function (btn) { }                                   // do nothing - default behaviour, since this strategy is called in base class for seats services and this is not applied for round table
    };

    return {
        getIds: getIds,
        adjustIconToAvailability: adjustIconToAvailability,
        adjustIconToVisibility: adjustIconToVisibility
    };
}();