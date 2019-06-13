'use strict'
var countdownManager = function () {
    var _countdown,
    
    // default options
    // radius: 15.5,                     // radius of arc
    // strokeStyle: "#477050",           // the color of the stroke
    // strokeWidth: undefined,           // the stroke width, dynamically calulated if omitted in options
    // fillStyle: "#8ac575",             // the fill color
    // fontColor: "#477050",             // the font color
    // fontFamily: "sans-serif",         // the font family
    // fontSize: undefined,              // the font size, dynamically calulated if omitted in options
    // fontWeight: 700,                  // the font weight
    // autostart: true,                  // start the countdown automatically
    // seconds: 10,                      // the number of seconds to count down
    // label: ["second", "seconds"],     // the label to use or false if none, first is singular form, second is plural
    // startOverAfterAdding: true,       // start the timer over after time is added with addSeconds

    init = function (id, onCompleteFunc) {
        var item =  $('#' + id);
        if (item.length === 0)
            return;

        _countdown = item.countdown360({
            radius: 30,
            seconds: 30,
            strokeWidth: 2,
            fillStyle: '#eeb9b3',
            strokeStyle: '#61bfbd',
            fontSize: 30,
            fontColor: '#ffffff',
            fontFamily: 'Englebert',
            autostart: false,
            label: '',
            onComplete: onCompleteFunc
        });
    },
        
    start = function () {
        if (_countdown === undefined)
            return;

        _countdown.start();
    },
        
    stop = function () {
        if (_countdown === undefined)
            return;

        _countdown.stop();
    };

    return {
        init: init,
        start: start,
        stop: stop
    };
}();