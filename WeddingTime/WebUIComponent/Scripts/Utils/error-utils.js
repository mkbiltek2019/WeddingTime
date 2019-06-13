'use strict'
var errorUtils = function () {
    var messageTimer,
        messageTimeout = 5000,
        infoArea = '#infoArea',
        errorInfo = '#errorInfo',

    showError = function (xhr) {
        var registerObj = {
            location: '/Shared/',
            names: ['ajaxError'],
            callback: onTmplRegistered
            //callbackData: { message: xhr.responseText }
        };  
        tmplUtils.register(registerObj);                // first render tmpl (if registered, callback method will be called immediately)
    },

    onTmplRegistered = function () {                    // callback data is in format appropriate for ajax error tmpl (defined under shared folder)      
        renderTmpl();                       
        enhanceBtns();
        clearTimeout(messageTimer);                     // some other error can be thrown in the meantime
        setupTimeout();
    },

    renderTmpl = function () {
        var tmpl = $.render['ajaxError']();
        $('body').append(tmpl);
    },

    enhanceBtns = function () {
        var btnClose = $(errorInfo).find('.btn-close-error');
        btnClose.click(hideError);
    },

    setupTimeout = function () {
        messageTimer = setTimeout(removeError, messageTimeout);
    },

    hideError = function () {
        clearTimeout(messageTimer);                     // clear timeout because close btn was clicked
        removeError();
    },

    removeError = function () {
        var info = $(errorInfo);
        info.find('button').addClass('disabled');
        info.hide('blind', 400, function () { $(this).remove(); });
    };

    return {
        showError: showError
    };
}();