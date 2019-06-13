'use strict'
var formUtils = function () {

    //bindUnobtrusiveValidation = function (formId) {
    //    var form = $('#' + formId);
    //    form.data("validator", null);

    //    $.validator.unobtrusive.parse(document);
    //    form.validate(form.data("unobtrusiveValidation").options);
    //},

    //bindUnobtrusiveValidation_TRY = function (formId) {
    //    var form = $('#' + formId);
        
    //    if (form.length == 0)
    //        return;

    //    form.removeData('validator');
    //    form.removeData('unobtrusiveValidation');

    //    $.validator.unobtrusive.parse(form);
    //    form.validate(form.data("unobtrusiveValidation").options);
    //    form.validate();
    //},

    var setFocusById = function (id) {
        setFocus($('#' + id));        
    },

    setFocusByRef = function (item) {
        setFocus(item);
    },

    setFocus = function (item) {
        item.focus(0, function () {
            $(this).select();
        });
    },

    clearInput = function (id) {
        $('#' + id).val('');
    },

    clearInputWithEvent = function (id) {
        $('#' + id).val('').change();
    },

    hideModal = function (id) {
        $('#' + id).modal('hide');
    },

    disableInput = function (selector) {
        if (selector.is('input'))
            selector.prop('disabled', true);
        else
            selector.find('input').prop('disabled', true);
    },

    enableInput = function (selector) {
        if (selector.is('input'))
            selector.prop('disabled', false);
        else
            selector.find('input').prop('disabled', false);
    },

    disableChild = function (areaId) {
        $('#' + areaId).find('input, textarea, button, select').prop('disabled', true);
    },

    enableChild = function (areaId) {
        $('#' + areaId).find('input, textarea, button, select').prop('disabled', false);
    },

    initListener = function (form, settings) {
        form.formListener(settings)
            .on('unclean.formListener', function () {
                $(this).find('button[type=submit]').removeClass('disabled');
            })
            .on('clean.formListener', function () {
                $(this).find('button[type=submit]').addClass('disabled');
            })
            .on('submit', function () {                               // to prevent submiting form if button is disabled
                if ($(this).find('button[type=submit]').hasClass('disabled'))
                    return false;                              
            });
    },

    ehnanceNonAjaxForm = function () {
        $('.form-body-btn').closest('form').submit(function () {
            if ($(this).valid()) {
                uiManager.blockUI(['.form-panel']);
            }
        });
    },

    getSelectedCheckboxes = function (area) {
        return area.find('input:checkbox:checked');
    };

    return {     
        //bindUnobtrusiveValidation: bindUnobtrusiveValidation,
        //bindUnobtrusiveValidation_TRY: bindUnobtrusiveValidation_TRY,        

        setFocusById: setFocusById,
        setFocusByRef: setFocusByRef,
        clearInput: clearInput,
        clearInputWithEvent: clearInputWithEvent,
        hideModal: hideModal,
        disableInput: disableInput,
        enableInput: enableInput,
        disableChild: disableChild,
        enableChild: enableChild,
        getSelectedCheckboxes: getSelectedCheckboxes,
        initListener: initListener,
        ehnanceNonAjaxForm: ehnanceNonAjaxForm
    };
}();