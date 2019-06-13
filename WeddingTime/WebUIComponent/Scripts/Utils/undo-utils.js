'use strict';
var undoUtils = function () {
    var undoTimer,
        activeTimeout = 30000,
        undoArea = '#undoArea',
        undoInfo = '#undoInfo',

    defaultSettings = {                                     // undefined is ignored by extend function
        'confirm': undefined,
        'method': 'POST',
        'mode': undefined,
        'loading-duration': undefined,
        'loading': undefined,
        'begin': 'undoUtils.begin',                         // functions needed to process undo appropriately
        'complete': 'undoUIManager.unblockUI',
        'failure': 'undoUtils.failure',
        'success': undefined,
        'update': undefined,
        'url': undefined
    },

    settingsStrategy = {                                    // added to make settings creation easier
        'confirm': 'data-ajax-confirm',                     // Confirm
        'method': 'data-ajax-method',                       // HttpMethod
        'mode': 'data-ajax-mode',                           // InsertionMode (data-ajax-mode will only be present if UpdateTargetId is set)
        'loading-duration': 'data-ajax-loading-duration',   // LoadingElementDuration (data-ajax-loading-duration will only be present if LoadingElementId is set)
        'loading': 'data-ajax-loading',                     // LoadingElementId
        'begin': 'data-ajax-begin',                         // OnBegin
        'complete': 'data-ajax-complete',                   // OnComplete
        'failure': 'data-ajax-failure',                     // OnFailure
        'success': 'data-ajax-success',                     // OnSuccess
        'update': 'data-ajax-update',                       // UpdateTargetId
        'url': 'data-ajax-url'                              // Url
    },

    showUndo = function (data, customSettings) {            // data contains view generated on server side and ui data
        removeUndo();                                       // before rendering new undo delete existing one 

        $(undoArea).html(data.view);                        // insert view into undo area
        undoUIManager.setUIItems(data.uiItems);             // set ui items that will be used for blocking appropriate area if undo will be performed
        applyAttributesToForm(customSettings);
        enhanceBtns();
        setupTimeout();
    },

    begin = function () {
        clearTimeout(undoTimer);
        undoUIManager.blockUI();
    },

    failure = function (xhr) {
        setupTimeout();
        undoUIManager.manageUIOnFailure(xhr);
    },

    removeUndo = function () {       
        var info = $(undoInfo);
        if (info.length === 0)                              // undo is not visible on the page
            return;

        undoUIManager.clearUIItems();
        deleteUndo();
        clearTimeout(undoTimer);                            // moved from showUndo function
        info.remove();
    },

    applyAttributesToForm = function (customSettings) {
        var settings = {};
        $.extend(settings, defaultSettings, customSettings);

        var undoForm = $(undoInfo).find('form');

        $.each(settings, function (key, value) {
            undoForm.attr(settingsStrategy[key], value);    // here I use strategy for translating simpler form into appropriate one
        });
    },

    enhanceBtns = function () {
        var btnClose = $(undoInfo).find('.btn-close-undo');
        btnClose.click(closeUndo);
    },

    closeUndo = function () {
        undoUIManager.clearUIItems();
        undoUIManager.blockUndoBtns();
        deleteUndo();
        hideUndo();
    },

    setupTimeout = function () {
        undoTimer = setTimeout(closeUndo, activeTimeout);
    },

    hideUndo = function () {                                // is invoked when undo btn is clicked and undo was performed successfully
        clearTimeout(undoTimer);                            // clear timeout because undo was performed
        hideStripe();
    },
 
    deleteUndo = function () {
        var url = $(undoInfo).find('form').attr('action');
        var ajaxData = { key: utils.getUrlParamByName(url, 'key') };
        var ajaxSettings = { type: 'POST' };

        ajaxUtils.ajax('/Undo/Delete', ajaxData, ajaxSettings);
    },

    hideStripe = function () {
        $(undoInfo).hide('blind', 400, function () { $(this).remove(); });
    };

    return {
        showUndo: showUndo,
        hideUndo: hideUndo,                                 // is invoked when undo btn is clicked and undo was performed successfully
        removeUndo: removeUndo,                             // for any other action performed after undo is shown
        begin: begin,
        failure: failure
    };
}();