'use strict';
var ajaxUtils = function () {

    var defaultSettings = {
        type: 'GET',
        contentType: 'application/x-www-form-urlencoded; charset=UTF-8',
        traditional: false,
        beforeSend: null,
        beforeSendCallbackData: null,
        success: null,
        successCallbackData: null,
        complete: null,
        completeCallbackData: null,
        error: null,
        errorCallbackData: null
    },

    ajax = function (url, data, customSettings) {
        var settings = { };
        $.extend(settings, defaultSettings, customSettings);

        return $.ajax({
            url: url,
            data: data,
            type: settings.type,
            contentType: settings.contentType,
            //cache: false,
            traditional: settings.traditional,
            beforeSend: function () {
                call(settings.beforeSend, settings.beforeSendCallbackData);
            },
            success: function (result) {
                callWithData(settings.success, result, settings.successCallbackData);
            },
            complete: function () {
                call(settings.complete, settings.completeCallbackData);                
            },
            error: function (xhr) {
                callWithData(settings.error, xhr, settings.errorCallbackData);                
            }
        });
    },

    ajaxSetup = function () {
        $.ajaxSetup({ cache: false });
    },

    call = function (func, callbackData) {
        if ($.isFunction(func)) {
            if ($.isPlainObject(callbackData))
                func(callbackData);
            else
                func();
        }
    },

    callWithData = function (func, data, callbackData) {
        if ($.isFunction(func)) {
            if (data && $.isPlainObject(callbackData))          // if there is data returned from controller + callback data
                func(data, callbackData);
            else if (data)                                      // if there is only data returned from controller
                func(data)
            else                                                // if only callback data or just func execution
                call(func, callbackData);
        }
    };

    return {
        ajax: ajax,
        ajaxSetup: ajaxSetup
    };
}();