'use strict';
(function (win) {
    var $ = win.jQuery;
    var win = $(win);

    $.fn.stick_to_bottom = function (options) {
        if (options == null) {
            options = {};
        }
        var _offsetBottom = options.offsetBottom === undefined ? 0 : options.offsetBottom;
        var _animate = options.animate === undefined ? false : options.animate;

        var _fn = function (elm) {           
            var elmHeight, parentWidth, initPosition, initialOffset, limit, isAnchored, isOnTop;

            isAnchored = true;                      // introduced to limit already invoked code
            isOnTop = false;
            elmHeight = elm.outerHeight();          // height with padding 
            elm.data('sticky_bottom', true);        // helps to call custom event using sticky_bottom prefix (?)

            var tickIfVisible = function () {
                if (!elm.data('isVisible')) {       // introduced isVisible data to be able to animate this item on mobile devices (to get rid of blinking)
                    return;
                }
                tick();
            };

            var tick = function () {
                var scrollTop = win.scrollTop();
                var viewportHeight = win.height();                          // returns height of browser viewport

                var value = viewportHeight + scrollTop;

                if (value < initialOffset) {
                    if (value <= limit) {                                   // if top of region in which panel should be visible is reached - stop moving it                        
                        if (!isOnTop) {                                     // true also if isAnchored equals undefined
                            anchorElement(0);
                            isOnTop = true;
                            isAnchored = true;

                            if (_animate) {
                                elm.hide();
                            }
                        }
                    }
                    else if (isAnchored) {      
                        elm.css({
                            'position': 'fixed',
                            'top': '',
                            'bottom': _offsetBottom + 'px',
                            'width': parentWidth + 'px'
                        });
                        if (_animate && isOnTop) {
                            elm.show();
                        }
                        isAnchored = false;
                        isOnTop = false;
                    }                    
                }
                else {
                    if (!isAnchored) {
                        anchorElement(initPosition);
                    }
                }
            };

            var recalc = function () {                          // recalculate all initialPosition, initialOffset, etc.
                var parent = elm.parent().find('ul');           // ul element must the parent of panel to show it correctly when there is small number of items in group
                if (parent.length === 0)                        // in case parent can't be found, or in situation I have to initialize item but items are not loaded yet (expenses case)
                    return;

                parentWidth = parent.width();
                var height = parent.height();
                var offsetTop = parent.offset().top;

                initPosition = height;
                initialOffset = offsetTop + height + elmHeight + _offsetBottom;
                limit = offsetTop + elmHeight + _offsetBottom;

                anchorElement(initPosition);                    // invalidate element + isAnchored variable
                isOnTop = false;                                // invalidate variable
            };

            var recalcAndTick = function () {
                recalc();
                tickIfVisible();
            };

            var anchorElement = function (value) {
                elm.css({
                    'position': 'absolute',
                    'top': value + 'px',
                    'bottom': '',
                    'width': ''
                });
                isAnchored = true;
            };

            var detach = function () {
                win.off('touchmove', tickIfVisible);
                win.off('scroll', tickIfVisible);
                win.off('resize', recalcAndTick);                
                $(document.body).off('sticky_bottom:recalc', recalcAndTick);
                elm.off('sticky_bottom:tick', tick);
                elm.off('sticky_bottom:detach', detach);
                elm.removeData('sticky_bottom');          
            };

            recalc();                                           // initial calculation

            win.on('touchmove', tickIfVisible);
            win.on('scroll', tickIfVisible);
            win.on('resize', recalcAndTick);
            elm.on('sticky_bottom:tick', tick);
            elm.on('sticky_bottom:detach', detach);             // on group delete - detach this event + sticky-kit for group box
            $(document.body).on('sticky_bottom:recalc', recalcAndTick);            

            return;
        };
        for (var i = 0; i < this.length; i++) {
            var elm = this[i];
            _fn($(elm));
        }
        return this;
    };
})(window);