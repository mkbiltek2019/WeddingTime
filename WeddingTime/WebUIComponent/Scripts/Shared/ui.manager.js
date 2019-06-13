'use strict';
var uiManager = function () {

    var wrappAjaxWithUI = function (settings, items) {      // items is a list of items to block/unblock       
        var uiData = createUIData(items);

        var exSettings = $.extend({}, settings);
        extendBeforeSendFunc(exSettings, settings, uiData);
        extendSuccessFunc(exSettings, settings, uiData);
        extendErrorFunc(exSettings, settings, uiData);

        return exSettings;
    },

    blockUI = function (toBlock) {
        var uiData = createUIData(toBlock);
        blockEach(uiData);
    },

    unblockUI = function (toUnblock) {
        var uiData = createUIData(toUnblock);
        unblockEach(uiData);
    },

    createUIData = function (items) {
        var uiData = [];
        for (var i = 0; i < items.length; i++) {
            if (typeof items[i] === 'boolean')                      // bool must be always after selector (string) or dom object
                uiData[i - 1].showLoader = items[i];                // assign show loader value to the previous element...
            else
                uiData.push(createUIDataStructure(items[i]));       // add default show loader true!
        }
        return uiData;
    },

    blockEach = function (uiData) {
        if ($.isEmptyObject(uiData))
            return;

        // based on uiData decide if message should be shown        
        // in perosn list view if items are moved between groups there will be need to block two panels...
       $.each(uiData, function () { block(this); });       
    },

    unblockEach = function (uiData) {
        if ($.isEmptyObject(uiData))
            return;

        if ($.isArray(uiData)) {
            $.each(uiData, function () {
                if (this.item.length !== 0)     // for instance when single item is deleted there is no item to unblock
                    unblock(this);
            });
        }
    },

    block = function (uiData) {
        uiData.item.block({
            message: uiData.showLoader ? $('<div>', { 'class': 'loader' }) : null,
            fadeIn: 0,
            overlayCSS: {                           // call it once!!
                backgroundColor: '#fff',
                opacity: 0.6,
                cursor: 'auto'
            },
            css: {                                  // call it once!!   $.blockUI.defaults.css = {}; 
                border: 'none',
                width: '55px',
                height: '40px',
                'background-color': 'transparent'
            },
            baseZ: 750                              // undo has 780, should be below undo
        });
    },

    createUIDataStructure = function (toBlock) {
        return {
            showLoader: true,
            item: typeof toBlock === 'string' ? $(toBlock) : toBlock
        };
    },

    unblock = function (uiData) {
        uiData.item.unblock({ fadeOut: 0 });
    },

    extendBeforeSendFunc = function (exSettings, settings, uiData) {
        exSettings.beforeSend = function () {
            blockEach(uiData);
            if ($.isFunction(settings.beforeSend)) settings.beforeSend.apply(this, arguments);
        };
    },

    extendSuccessFunc = function (exSettings, settings, uiData) {
        exSettings.success = function () {
            if ($.isFunction(settings.success)) settings.success.apply(this, arguments);
            unblockEach(uiData);                                                            // moved from complete func since deffered.promise is ment to be done after success function ends
        };
    },

    extendErrorFunc = function (exSettings, settings, uiData) {
        exSettings.error = function () {
            if ($.isFunction(settings.error)) settings.error.apply(this, arguments);
            unblockEach(uiData);
            errorUtils.showError(arguments[0]);                                             // I also assume that this is ui related action, first arg in error function is always xhr
        };
    };

    return {
        wrappAjaxWithUI: wrappAjaxWithUI,
        blockUI: blockUI,
        unblockUI: unblockUI
    };
}();