'use strict';
var stickyManager = function() {

    var enhanceItems = function (target) {
        var groupBoxes = getGroupBoxes(target);
        var isMobile = $.browser.mobile;

        groupBoxes.stick_to_top({
            //offsetTop: 0                                       // TO CONSIDER! FOR SMALL MUST BE 0, FOR BIGGER COULD BE SET
            animate: isMobile
        });

        var stickPanels = getStickPanels(target);
        stickPanels.stick_to_bottom({
            offsetBottom: 5,
            animate: isMobile
        });
    },

    recalculateItems = function () {
        recalculateGroupBoxes();
        recalculateMembers();
    },

    recalculateMembers = function () {
        $(document.body).trigger('sticky_bottom:recalc');
    },

    recalculateGroupBoxes = function () {
        $(document.body).trigger('sticky_top:recalc');
    },

    tickMembers = function (target) {
        target.trigger('sticky_bottom:tick');
    },

    getGroupBoxes = function (target) {
        return target === undefined ? $('.group-box') : target.find('.group-box');
    },

    getStickPanels = function (target) {
        return target === undefined ? $('.stick-bottom-panel') : target.find('.stick-bottom-panel');
    },

    unwireEvents = function (target) {
        var groupBoxes = getGroupBoxes(target);
        groupBoxes.trigger('sticky_top:detach');                // detach also all events for group boxes

        var stickPanels = getStickPanels(target);
        stickPanels.trigger('sticky_bottom:detach');            // detach all events for all members panels        
    };

    return {
        enhanceItems: enhanceItems,
        unwireEvents: unwireEvents,
        recalculateItems: recalculateItems,
        recalculateGroupBoxes: recalculateGroupBoxes,
        tickMembers: tickMembers
    };
}();