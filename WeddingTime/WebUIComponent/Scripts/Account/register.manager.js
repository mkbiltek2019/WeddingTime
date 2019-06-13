'use strict';
var registerManager = function () {

    var uiItems,
        checkBox = $('#TermsAccepted'),
        form = $('#registerForm'),
             
    init = function () {
        form.on('submit', function () {
            if (isTermsAccepted())
                return true;

            if (isTermsMarkedInvalid())
                return false;

            markTermsInvalid();
            return false;
        });

        checkBox.change(function () {
            if (isTermsAccepted())
                markTermsValid();
            else
                markTermsInvalid();
        });
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
	        addValidationSummary(data.ErrorMsgs);
	        uiManager.unblockUI(getUIItems());
	    }
	},

	onFailure = function (xhr) {
	    uiManager.unblockUI(getUIItems());
	    errorUtils.showError(xhr);
	},

    getUIItems = function () {
        return uiItems === undefined ? (uiItems = ['.form-panel']) : uiItems;
    },

    isTermsAccepted = function () {
        return checkBox.is(':checked');
    },

    isTermsMarkedInvalid = function () {
        return checkBox.parent().hasClass('form-checkbox-invalid');
    },

    markTermsInvalid = function () {
        checkBox.parent().addClass('form-checkbox-invalid');
    },

    markTermsValid = function () {
        checkBox.parent().removeClass('form-checkbox-invalid');
    },
        
    addValidationSummary = function (errorMsgs) {
        var summary = $('<div>', { 'class': 'validation-summary' });
      
        $.each(errorMsgs, function (i, error) {
            var errorDiv = $('<div>').html(error);
            summary.append(errorDiv);
        });

        $('#registerForm').find('.form-inputs').append(summary);
    },
        
    removeValidationSummary = function () {
        $('#registerForm').find('.form-inputs').find('.validation-summary').remove();
    };

    return {
        init: init,
        onBegin: onBegin,
        onSuccess: onSuccess,
        onFailure: onFailure
    };
}();