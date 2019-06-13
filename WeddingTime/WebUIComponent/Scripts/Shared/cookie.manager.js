'use strict'
var cookieManager = function (win) {
    var win = $(win),
        cookieName = 'cookieclosed',
        isTopStick = false,

    showAlert = function () {
        var value = get(cookieName);
        if (value !== null && value.closed === true)
            return;

        show();                
    },

    set = function (name, json, path) {
        var data = {};

        var value = get(name);
        if (value !== null) {                   // if cookie exists get all data and update or add new value
            $.extend(data, value, json);
        }

        var value = JSON.stringify($.isEmptyObject(data) ? json : data);
        $.cookie(name, value, { expires: 365, path: path });
    },

    get = function (name) {
        var value = $.cookie(name);
        if (value === undefined)
            return null;

        return JSON.parse(value);
    },

    // private methods

    show = function () {
        var registerObj = {
            location: '/Shared/',
            names: ['cookieInfo'],
            callback: onTmplRegistered
        };
        tmplUtils.register(registerObj);        // first render tmpl (if registered, callback method will be called immediately)
    },

    onTmplRegistered = function () {            // callback data is in format appropriate for ajax error tmpl (defined under shared folder)      
        renderTmpl();
        enhanceAlert();
        attachScroll();
    },

    renderTmpl = function () {
        var tmpl = $.render['cookieInfo']();
        $('body').prepend(tmpl);
    },

    enhanceAlert = function () {
        $('#cookieAlert').on('closed.bs.alert', function () {
            set(cookieName, { closed: true }, '/');
            win.off('touchmove', tick);
            win.off('scroll', tick);
            win.off('resize', tick);
        });
    },

    attachScroll = function () {
        win.on('touchmove', tick);
        win.on('scroll', tick);
        win.on('resize', tick);
    },

    tick = function () {
        if ($(document).scrollTop() == 0) {
            $('#cookieAlert').removeClass('stick-top');
            isTopStick = false;
        }
        else if (!isTopStick) {
            $('#cookieAlert').addClass('stick-top');
            isTopStick = true;
        }
    };

    return {
        showAlert: showAlert,
        set: set,
        get: get
    };
}(window);