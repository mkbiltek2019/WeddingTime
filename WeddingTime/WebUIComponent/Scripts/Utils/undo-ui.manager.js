'use strict';
var undoUIManager = function () {

    var uiItems = null,
        undoInfo = '#undoInfo',

    blockUI = function () {
        blockUndoBtns();
        uiManager.blockUI(uiItems);
    },

    unblockUI = function () {
        uiManager.unblockUI(uiItems);
        clearUIItems();
    },

    manageUIOnFailure = function (xhr) {
        unblockUndoBtns();
        errorUtils.showError(xhr);
    },

    setUIItems = function (data) {
        uiItems = data;
    },
        
    clearUIItems = function () {
        uiItems = null;
    },
        
    blockUndoBtns = function () {
        $(undoInfo).find('button').addClass('disabled');
    },
        
    unblockUndoBtns = function () {
        $(undoInfo).find('button').removeClass('disabled');
    };

    return {
        blockUI: blockUI,
        unblockUI: unblockUI,
        setUIItems: setUIItems,
        clearUIItems: clearUIItems,
        manageUIOnFailure: manageUIOnFailure,
        blockUndoBtns: blockUndoBtns
    };
}();