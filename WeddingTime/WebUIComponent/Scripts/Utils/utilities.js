'use strict';
var utils = function () {

    var toFloat = function (value) {
        return parseFloat(value.replace(',', '.'));
    },

    toCurrencyFormat = function (value) {
        return value.toFixed(2).replace('.', ',');
    },

    bind = function (bindTo, value, area) {
        var dataBind = '[data-bind=' + bindTo + ']';
        var itemsToBind = area == undefined ? $(dataBind) : area.find(dataBind);

        itemsToBind.each(function () {
            var bound = $(this);
            if (bound.is('input, textarea, select'))
                bound.val(value);                                                   // this is new value
            else
                bound.html(value);

            processVisibilityData(bound, value);
            processCallbackData(bound, value);
        });
    },

    serializeArrayToJson = function (array) {
        var obj = {};
        for (var key in array) {
            var item = array[key];
            var name = item.name.charAt(0).toLowerCase() + item.name.slice(1);      // always start from small character
            obj[name] = item.value;
        }
        return obj;
    },
    
    getUrlParamByName = function (url, param) {                                 // it additionally returns only value from brackets that is followed by what is specified before = sign + the whole match
        var match = new RegExp('[?&]' + param + '=([^&]*)').exec(url);          // starts optionally from ? or & then add specified param name with = sign. [^&] - matches any single character that is not in character_group. * - matches the previous element zero or more times.
        return match && match[1];                                               // match && decodeURIComponent(match[1].replace(/\+/g, ' '));
    },

    getRandomInt = function (min, max) {
        return Math.floor(Math.random() * max) + min;
    },

    scrollToItem = function (target) {
        $('html, body').animate({ scrollTop: target.offset().top }, 1000);
    },

    toggleLayout = function (item) {
        var section = item.closest('section');                           // appropriate section
        var imgSection = section.prev();                                // section that displays img in lg/md mode

        section.toggleClass('col-md-9 col-md-12');                      // expand item to full screen
        imgSection.toggleClass('visible-md visible-lg hidden');         // trick to hide img section - remove visible-* classes and instead add hidden class

        $('#btnToggleLayout').find('span').toggleClass('icon-left icon-right');
    },

    removeDialogAnimation = function () {
        if ($.browser.mobile)
            $('.modal').removeClass('fade');
    },

    // private functions connected to bind function

    processCallbackData = function (bound, value) {
        var bindCallback = bound.data('bind-callback');
        if (bindCallback === undefined)
            return;

        var func = getBindCallbackFunc(bindCallback);
        if ($.isFunction(func))
            func(bound, value);
    },

    getBindCallbackFunc = function(functionName) {
        var context = window;
        var namespaces = functionName.split(".");
        var func = namespaces.pop();                                // removes the last element from an array
        for (var i = 0; i < namespaces.length; i++) {
            context = context[namespaces[i]];
        }
        return context[func];
    },

    processVisibilityData = function (bound, value) {               // means that if this data attr is defined I set parent or item visibility
        var bindVs = bound.data('bind-vs');                         // decision is based on item value, if empty - hide, else - show
        if (bindVs === undefined)
            return;

        visibilityStrategy[bindVs](bound, value);
    },

    parentVisibilityStrategy = function (bound, value) {
        var parent = bound.parent();
        changeOnBindVisibility(parent, value);
    },

    changeOnBindVisibility = function (item, value) {
        if (!value)
            item.addClass('hidden');
        else
            item.removeClass('hidden');
    },

    visibilityStrategy = {                                          // functions used in strategy must be defined above strategy itself
        'parent': parentVisibilityStrategy,
        'self': changeOnBindVisibility                              // no need to create seperate function
    };

    return {        
        toFloat: toFloat,
        toCurrencyFormat: toCurrencyFormat,
        bind: bind,
        serializeArrayToJson: serializeArrayToJson,
        getUrlParamByName: getUrlParamByName,
        getRandomInt: getRandomInt,
        scrollToItem: scrollToItem,
        toggleLayout: toggleLayout,
        removeDialogAnimation: removeDialogAnimation
    };
}();