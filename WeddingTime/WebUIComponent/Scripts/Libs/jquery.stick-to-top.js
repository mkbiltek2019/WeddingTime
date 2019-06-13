'use strict';
(function (win) {
    var $ = win.jQuery;
    var win = $(win);

    $.fn.stick_to_top = function (options) {
        if (options == null) {
            options = {};
        }
        var _offsetTop = options.offsetTop === undefined ? 0 : options.offsetTop;
        var _animate = options.animate === undefined ? false : options.animate;

        var _fn = function (elm) {
            var wrapper, elmWidth, elmHeight, initialOffset, limit, isAnchored, isBottomed;
            
            isBottomed = false;
            isAnchored = true;                        
            elm.data('sticky_top', true);                           // helps to call custom event using sticky_bottom prefix (?)

            var tick = function () {
                var value = win.scrollTop();

                if (value > initialOffset) {
                    if (value >= limit) {                           // if top of region in which panel should be visible is reached - stop moving it                        
                        if (!isBottomed) {                           
                            elm.css({
                                position: 'absolute',
                                width: elmWidth + 'px',             // cause if we change resolution while item is bottomed item after recalculation would have wrong width
                                bottom: '0',
                                top: 'auto'
                            });
                            if (_animate) {
                                elm.hide();
                            }
                            isBottomed = true;
                            isAnchored = true;
                        }
                    }
                    else if (isAnchored) {                          // when item is going to be shown for the first time in the middle of the list we additionally check if isAnchored is undefined to show panel for the first time and assign variable                 
                        elm.css({
                            position: 'fixed',
                            width: elmWidth + 'px',
                            top: _offsetTop + 'px',
                            bottom: ''
                        });
                        if (_animate) {
                            if (!isBottomed)                        // it is not needed to hide element if it's bottomed - it is already hidden
                                elm.hide()
                            elm.fadeIn();
                        }
                        isAnchored = false;
                        isBottomed = false;
                    }
                }
                else {                                              // can't use isAnchored flag becasue of recalc function and value change possibility of initPosition variable
                    if (!isAnchored) {
                        elm.css({
                            position: '',
                            top: ''
                        });
                        if (_animate) {
                            elm.hide().show();                      // trick to get rid of blinking while anchoring top
                        }
                        isAnchored = true;
                    }
                }
            };

            var recalc = function () {                              // recalculate all initialPosition, initialOffset, etc.
                wrapper = elm.closest('.sticky-top-wrapper');       // item to be able to stick to top in special position - like float right
                if (wrapper.length === 0)                        
                    return;

                var parent = wrapper.parent();                      // parent in which this item will be moving
                if (parent.length === 0)                            // in case parent can't be found, or in situation I have to initialize item but items are not loaded yet (expenses case)
                    return;

                invalidateCss();                                    // needed to get appropriate items dimensions
                invalidateVariables();

                var parentHeight = parent.height();
                var parentOffsetTop = parent.offset().top;

                elmHeight = elm.outerHeight(true);                  // height with padding and margin 
                elmWidth = elm.outerWidth(true);                    // width with padding and margin 

                initialOffset = parentOffsetTop - _offsetTop;       // offsetTop + height + elmHeight + _offsetBottom;
                limit = (parentOffsetTop + parentHeight) - elmHeight - _offsetTop;

                wrapper.css({
                    width: elmWidth,
                    height: elmHeight
                });
            };

            var invalidateCss = function () {
                wrapper.css({
                    width: '',
                    height: ''
                });
                elm.css({                                           // doing that causes that element becomes docked at the top (is like initialized)
                    position: '',
                    top: '',
                    bottom: '',
                    width: ''
                });
            };

            var invalidateVariables = function () {
                isBottomed = false;
                isAnchored = true;
            };

            var recalcAndTick = function () {
                recalc();
                tick();
            };

            var detach = function () {
                win.off('touchmove', tick);
                win.off('scroll', tick);
                win.off('resize', recalcAndTick);
                $(document.body).off("sticky_top:recalc", recalcAndTick);
                elm.off("sticky_top:detach", detach);
                elm.removeData('sticky_top');
            };

            recalc();                                               // initial calculation

            win.on('touchmove', tick);
            win.on('scroll', tick);
            win.on('resize', recalcAndTick);
            elm.on("sticky_top:detach", detach);
            $(document.body).on("sticky_top:recalc", recalcAndTick);                        

            return;
        };

        for (var i = 0; i < this.length; i++) {
            var elm = this[i];
            _fn($(elm));
        }
        return this;
    };
})(window);