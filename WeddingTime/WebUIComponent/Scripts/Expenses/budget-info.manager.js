'use strict';
var budgetInfoManager = function () {

    var _updateArea,
        _displayArea,
        _txtBudget,
        _budgetModel = {},                                                  // initialized after loading budget info for the first time
                                                                            // has three properties: budgetValue, totalPrice and budgetLeft
    getInfo = function () {
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onInfoFetched }, getExpenseInfoUIItem());
        expensesService.getInfo(ajaxSettings);
    },

    updateInfo = function (totalPrice) {
        _budgetModel.totalPrice = totalPrice;
        updateBudgetLeft();
        updateView();
    },

    beforeUpdate = function () {
        uiManager.blockUI(getExpenseInfoUIItem());
    },

    onUpdateSuccess = function () {        
        hideEdit(function () {                                              // animates hiding txt that is used for entering new budget value
            _budgetModel.budgetValue = utils.toFloat(_txtBudget.val());
            updateBudgetLeft();
            updateView();          
        });
        uiManager.unblockUI(getExpenseInfoUIItem());
    },

    onUpdateFailure = function (xhr) {
        uiManager.unblockUI(getExpenseInfoUIItem());
        errorUtils.showError(xhr);
    },

    onInfoFetched = function (data) {                                       // bind budget value like on expenses form?
        $('#expensesInfo').html(data.View);
        createBudgetModel(data);
        finalize();
    },

    createBudgetModel = function (data) {                                   // don't need to calculate budget left here - it has to be calculated at first budget update or total price update
        _budgetModel.budgetValue = data.BudgetValue;
        _budgetModel.totalPrice = data.TotalPrice;
    },

    getExpenseInfoUIItem = function () {
        return ['#expensesInfo'];
    },

    updateBudgetLeft = function () {
        _budgetModel.budgetLeft = _budgetModel.budgetValue - _budgetModel.totalPrice;
    },

    finalize = function () {                                                // parses form to add validation, enhance btns and initialize variables
        var formSelector = '#updateBudgetForm';
        $.validator.unobtrusive.parseDynamicContent(formSelector);        
        formUtils.initListener($(formSelector));

        initializeFields();
        enhenceButtons();
        enhanceBudgetInput();
    },

    updateView = function () {                                              // perform binding while updating view
        $('#expensesInfo').fadeOut(200, function () {
            for (var key in _budgetModel) {
                var value = utils.toCurrencyFormat(_budgetModel[key]);      // this is new value to bind
                utils.bind(key, value);
            }
            $(this).fadeIn(200);
        });
    },

    initializeFields = function () {
        _updateArea = $('#updateBudgetArea');
        _displayArea = $('#displayBudgetArea');
        _txtBudget = $('#txtBudgetValue');
    },

    enhenceButtons = function () {
        $('#btnEditBudget').click(showEdit);
        $('#btnCancelBudget').click(hideEdit);
    },

    enhanceBudgetInput = function () {
        _txtBudget.keyup(function (e) {
            if (e.keyCode !== 190)                                          // 190 - key code for dot
                return;
            
            var value = $(this).val();
            $(this).val(value.replace(/\./g, ','));                         // replace dot with comma
        });
    },

    showEdit = function () {
        _txtBudget.val(utils.toCurrencyFormat(_budgetModel.budgetValue));   // set actual value which is saved in budget model

        _updateArea.find('form').trigger('initialize.formListener');        // because this is called before showing form I don't need to set manually submit btn to disabled state when hiding form, it will be done by setDirtyStatus function placed inside initForm func inside listener library
        _updateArea.show('blind', 200);
        _displayArea.animate({ top: '50px', opacity: '0' }, 200);
    },

    hideEdit = function (callbackFunc) {
        _updateArea.hide('blind', 200);

        if ($.isFunction(callbackFunc)) {
            _displayArea.animate({ top: '0', opacity: '1' }, 200, callbackFunc);
        }
        else {
            _displayArea.animate({ top: '0', opacity: '1' }, 200);
        }
    };

    return {
        getInfo: getInfo,
        updateInfo: updateInfo,
        beforeUpdate: beforeUpdate,
        onUpdateSuccess: onUpdateSuccess,
        onUpdateFailure: onUpdateFailure
    };
}();