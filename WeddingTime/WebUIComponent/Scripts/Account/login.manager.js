'use strict';
var loginManager = function () {

    var inputsArea,
        uiItems,

    init = function () {
        enhanceExternalLoginBtns();
    },

	onBegin = function () {
	    uiManager.blockUI(getUIItems());
		removeValidationSummary();
	},

	onSuccess = function (data) {
	    if (data.IsValid) {
	        window.location.href = data.Url;
	    }
	    else {
	        addValidationSummary(data.ErrorMsg);
	        uiManager.unblockUI(getUIItems());
	    }
	},

	onFailure = function (xhr) {
	    uiManager.unblockUI(getUIItems());
	    errorUtils.showError(xhr);
	},
    
	addValidationSummary = function (errorMsg) {
		var summary = $('<div>', { 'class': 'validation-summary' }).html(errorMsg);
		getInputsArea().append(summary);
	},

	removeValidationSummary = function () {
		getInputsArea().find('.validation-summary').remove();
	},

    getUIItems = function () {
        return uiItems === undefined
            ? (uiItems = ['.login-internal', '.login-external', false])
            : uiItems;
    },

	getInputsArea = function () {
		return inputsArea === undefined ? (inputsArea = $('#loginForm').find('.form-inputs')) : inputsArea;
	},
        
	enhanceExternalLoginBtns = function () {
	    $('.btn-external').click(function () {
	        uiManager.blockUI(['.login-external', '.login-internal', false]);
	    });
	};

    return {
        init: init,
	    onBegin: onBegin,
	    onSuccess: onSuccess,
	    onFailure: onFailure
	};
}();