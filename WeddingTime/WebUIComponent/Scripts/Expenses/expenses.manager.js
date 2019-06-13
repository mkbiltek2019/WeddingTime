'use strict';
var expensesManager = function () {

    var _expensesList,                              // ul element, assign after expenses are fetched from server (getExpenes method)
        _expensesPanel = $('#expensesPanel'),
        _expenses = {},                             // dictionary that holds prices for each expense

    init = function () {
        initCreateModal();
        initEditModal();
        enhanceExpensesPanelBtns();

        $('#expensesHeader').stick_to_top();
        hideExpenseHeader();                        // hide header at the beginning, show it only if any item is rendered

        disableExpenseExportBtn();                  // disable it at the beginning, show it only if any items are available

        _expensesPanel.stick_to_bottom({
            offsetBottom: 5
        });

        initFilterEvents();
        initExpenses();
    },

    initExpenses = function () {
        getExpenses(onExpensesFetchedToInit);
    },

    initFilterEvents = function () {
        $(filterManager).on('filterCleared', enableSortable)
                        .on('filterApplied', onFilterApplied)
                        .on('filterProcessed', recalculateItems);
    },

    enableSortable = function () {
        if (_expensesList.sortable('option', 'disabled'))
            _expensesList.sortable('enable');
    },

    disableSortable = function () {
        if (!_expensesList.sortable('option', 'disabled'))
            _expensesList.sortable('disable');
    },

    onFilterApplied = function () {        
        disableSortable();
        clearSelectedExpenses();                                // after applying filter clear all selected items and hide expenses bottom stick panel
        hideExpensesPanel();
    },

    hideExpenseHeader = function () {
        $('#expensesHeader').hide();
    },

    showExpenseHeader = function () {
        $('#expensesHeader').show();
    },

    refreshExpenses = function () {
        getExpenses(onExpensesFetchedToRefresh);
    },

    getExpenses = function (successFunc) {                      // to pass specific function on first load (without budget update), and then on reload (with budget update)
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: successFunc }, ['.expenses-area']);
        expensesService.getExpenses(ajaxSettings);
    },

    createSingleExpenseUIItem = function (eid) {
        var expense = $('li[eid=' + eid + ']');
        return expense;
    },

    onExpensesFetchedToInit = function (data) {
        processExpensesData(data);
    },

    onExpensesFetchedToRefresh = function (data) {
        processExpensesData(data);
        updateBudgetInfo();
    },

    processExpensesData = function (data) {
        $('#expenses').html(data.View);

        if ($.isEmptyObject(data.Expenses)) {
            //hideExpenseHeader();
            //disableExpenseExportBtn();            // trigger it on delete!
            return;
        }

        _expensesList = $('#expensesList');
        _expensesList.find('.list-item-checkbox').change(onCheckBoxStateChanged);

        applySorting();
        enhanceButtons();                                   // btn that are visible next to the item
        
        createExpensesMap(data.Expenses);                   // creates dictionary where key is eid and value is expense price

        filterManager.populate(data.Expenses);              // add to filter list
        filterManager.applyFilter();
        recalculateItems();
        showExpenseHeader();
        enableExpenseExportBtn();
    },

    createExpensesMap = function (expenses) {
        _expenses = {};                                     // first always invalidate dictionary
        $.each(expenses, function (key, value) {
            _expenses[key] = value.Price;
        });
    },

    deleteFromExpensesMap = function (eid) {
        delete _expenses[eid];
    },

    hasAnyExpenses = function () {
        return !$.isEmptyObject(_expenses);
    },

    updateExpensesMap = function (item, value) {            // this is called after value is bound, this function is defined as data-bind-callback for appropriate item
        var eid = getExpenseId(item);
        _expenses[eid] = $.isNumeric(value) ? parseInt(value, 10) : utils.toFloat(value);
    },

    updateFilterMap = function (item, value) {              // this is called after value is bound, this function is defined as data-bind-callback for appropriate item
        var eid = getExpenseId(item);
        filterManager.update(eid, value);
    },

    getExpensesTotalPrice = function () {
        var totalPrice = 0;
        for (var key in _expenses) {
            totalPrice += _expenses[key];
        }
        return totalPrice;
    },

    disableExpenseExportBtn = function () {        
        $('#btnExpensesExport').addClass('disabled');
    },

    enableExpenseExportBtn = function () {
        $('#btnExpensesExport').removeClass('disabled');
    },

    recalculateItems = function () {
        $(document.body).trigger('sticky_top:recalc');
        $(document.body).trigger('sticky_bottom:recalc');
    },

    initCreateModal = function () {
        var settings = { beforeOpenFunc: tryHidePopover, populateFunc: expensesService.createNewExpense, preparedFunc: enhanceEditInputs, successFunc: onCreateDialogActionSucceded };
        dialogAnimation.init('createExpensesModal', settings);
    },

    initEditModal = function () {
        var settings = { type: 'UPDATE', beforeOpenFunc: tryHidePopover, populateFunc: expensesService.prepareEdit, preparedFunc: enhanceEditInputs, ajaxDataFunc: complementEditAjaxData, successFunc: onUpdateDialogActionSucceded };
        dialogAnimation.init('editExpensesModal', settings);
    },

    complementEditAjaxData = function (item, ajaxData) {
        var selected = formUtils.getSelectedCheckboxes(_expensesList);
        var ids = getExpensesIds(selected);

        ajaxData.ids = ids;
    },

    onCheckBoxStateChanged = function () {                                      
        var btn = $(this);
        btn.children().first().toggleClass('active');

        tryHidePopover();

        var isAnySelected = formUtils.getSelectedCheckboxes(_expensesList).length > 0;
        if (isAnySelected)
            showExpensesPanel();
        else
            hideExpensesPanel({ duration: 120, effect: 'blind' });
    },

    showExpensesPanel = function () {
        if (!_expensesPanel.data('isVisible')) {            
            disableExpensesBtns();
            removeEditFields();
            _expensesPanel.trigger('sticky_bottom:tick');
            _expensesPanel.show('blind', 200);
            _expensesPanel.data('isVisible', true);
        }
    },

    hideExpensesPanel = function (options) {
        enableExpensesBtns();
        if ($.isPlainObject(options))                                           // in some cases animate while hiding
            _expensesPanel.hide(options);
        else
            _expensesPanel.hide();

        _expensesPanel.data('isVisible', false);
    },

    removeEditFields = function () {
        var editExpense = $('.edit-expense');

        $.each(editExpense, function (i, item) {
            var eid = $(item).closest('.list-group-item').attr('eid');
            var area = $('#expenseBtnArea-' + eid);
            area.show();
        })
        
        editExpense.remove();
    },

    disableExpensesBtns = function () {
        $('.btn-edit-expense').addClass('disabled');
        $('.btn-remove-expense').addClass('disabled');
    },

    enableExpensesBtns = function () {
        $('.btn-edit-expense').removeClass('disabled');
        $('.btn-remove-expense').removeClass('disabled');
    },

    onCreateDialogActionSucceded = function (data) {
        refreshExpenses();
        hideExpensesPanel();        
    },

    onUpdateDialogActionSucceded = function (data) {
        $.each($.parseJSON(data), function (i, obj) {
            var bindArea = $('li[eid=' + obj.id + ']');
            $.each(obj, function (key, value) {
                utils.bind(key, value, bindArea);
            });
        });

        filterManager.applyFilter();                            // apply filter after updating single row
        updateBudgetInfo();
    },

    applySorting = function () {
        _expensesList.sortable({
            start: onStartSorting,
            update: updateSortNumber,
            handle: '.list-item-handle'
        });
    },

    enhanceExpensesPanelBtns = function () {
        $('#btnClearSelected').click(function () {
            clearSelectedExpenses();
            hideExpensesPanel({ duration: 120, effect: 'blind' });
        });

        $('#btnDeleteExpenses').click(function () {
            tryHidePopover();

            var selected = formUtils.getSelectedCheckboxes(_expensesList);
            var ids = getExpensesIds(selected);

            var settings = { type: 'POST', traditional: true, success: onExpensesDeleted, successCallbackData: { ids: ids } };
            var ajaxSettings = uiManager.wrappAjaxWithUI(settings, ['.expenses-area']);

            var data = { ids: ids };
            expensesService.deleteExpenses(data, ajaxSettings);
        });

        $('#btnSumupExpenses').click(function () {
            if (tryHidePopover())
                return;

            var btn = $(this);
            btn.addClass('active');

            var selected = formUtils.getSelectedCheckboxes(_expensesList);
            var ids = getExpensesIds(selected);

            var sum = 0;
            $.each(ids, function (i, id) { sum += _expenses[id]; });
            btn.popover({ content: utils.toCurrencyFormat(sum) }).popover('show');
        });

        $('#btnExpensesToPdf').click(function () {
            var selected = formUtils.getSelectedCheckboxes(_expensesList);
            var ids = getExpensesIds(selected);

            var href = '/Pdf/ExpensesByIdPdf?';

            for (var i = 0; i < ids.length; i++) {
                href += 'ids=' + ids[i];
                if (i < ids.length - 1)
                    href += '&';
            }
            window.open(href, '_blank');
        });
    },

    clearSelectedExpenses = function () {
        tryHidePopover();

        var selected = formUtils.getSelectedCheckboxes(_expensesList);      // because bootstrap sets active class on parent elements
        selected.removeAttr('checked');
        selected.parent().removeClass('active');
    },

    tryHidePopover = function () {
        if (!_expensesPanel.find('div.popover').is(':visible'))
            return false;
        
        var btn = $('#btnSumupExpenses');
        btn.removeClass('active');
        btn.popover('hide').popover('destroy');
        return true;        
    },

    enhanceButtons = function () {
        $('.btn-edit-expense').click(function () {
            var eid = getExpenseId($(this));

            var uiItems = [createSingleExpenseUIItem(eid)];
            var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onEditPrepared, successCallbackData: { eid: eid } }, uiItems);

            var data = { id: eid };
            expensesService.prepareSingleEdit(data, ajaxSettings);
        });
        $('.btn-remove-expense').click(function () {
            var eid = getExpenseId($(this));
            var ids = [eid];

            var settings = {
                type: 'POST',
                traditional: true,
                beforeSend: beforeExpenseDelete,
                success: onExpenseDeleted,
                successCallbackData: { eid: eid },                
                error: onDeleteFailed
            };
            var uiItems = [createSingleExpenseUIItem(eid)];
            var ajaxSettings = uiManager.wrappAjaxWithUI(settings, uiItems);

            var data = { ids: ids };
            expensesService.deleteExpenses(data, ajaxSettings);
        });
    },

    onStartSorting = function (e, ui) {
        var draggable = ui.item;
        draggable.startIndex = draggable.index();
    },

    updateSortNumber = function (e, ui) {
        var draggable = ui.item;
        var eid = draggable.attr('eid');
        var baseItemId = draggable.is(':last-child') ? null : draggable.next().attr('eid');

        var settings = {
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            error: onUpdateSortNumberFailure,
            errorCallbackData: { draggable: draggable }
        };
        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [draggable]);

        var data = JSON.stringify({ UpdateId: eid, BaseItemId: baseItemId, UpdateType: baseItemId == null ? 'SortAsLast' : 'Sort' });        // // create structure that represents update model on server side
        expensesService.updateExpenseOrderNo(data, ajaxSettings);
    },

    onUpdateSortNumberFailure = function (xhr, callbackData) {
        var draggable = callbackData.draggable;
        var startIndex = draggable.startIndex;
        var item = $('#expensesList li:eq(' + (draggable.startIndex) + ')');    // item used to place draggable after or before it

        if (draggable.index() < startIndex)
            item.after(draggable);
        else
            item.before(draggable);                                             // start index is saved after starting dragging item
    },

    onEditPrepared = function (data, callbackData) {
        var eid = callbackData.eid;
        var item = getExpenseItem(eid).find('.expenses-content');
        item.append(data);

        var formSelector = '#editExpenseForm-' + eid;
        $.validator.unobtrusive.parseDynamicContent(formSelector);                      // rebind form validation

        var form = $(formSelector);
        formUtils.initListener(form);                                                   // init form listener and trigger initialize event to save original values
        form.trigger('initialize.formListener');

        enhanceEditInputs(form);

        var area = $('#expenseBtnArea-' + eid);
        area.hide();                                                                    // hide btns behind edit panel

        var btnCancel = $('#btnCancelEdit-' + eid);
        btnCancel.click(function () {
            form.trigger('detach.formListener');
            $(this).closest('.edit-expense').remove();
            area.show();                                                                // make visible btns that were hidden behind edit panel
        });
    },

    enhanceEditInputs = function (area) {
        area.find('.float-input').keyup(function (e) {
            if (e.keyCode !== 190)                                          // 190 - key code for dot
                return;

            var value = $(this).val();
            $(this).val(value.replace('.', ','));                           // replace dot with comma
        });

        area.find('.int-input').keyup(function (e) {
            if (e.keyCode !== 190 && e.keyCode !== 188)                     // 190 - key code for dot, 188 key code for comma
                return;

            var value = $(this).val();
            $(this).val(value.replace('.', '').replace(',', ''));           // replace dot or comma with empty string
        });
    },

    hideExpenseBtnArea = function (eid) {
        $('#expenseBtnArea-' + eid).hide();
    },

    onExpenseDeleted = function (data, callbackData) {
        var eid = callbackData.eid;
        removeExpenseItem(eid);
        deleteFromExpensesMap(eid);

        disableExpenseItems();

        filterManager.remove(eid);                                  // remove this item from filter list
        recalculateItems();

        updateBudgetInfo();
        showUndo(data);
        enableSortable();                                           // since we block sortable before deleting
    },

    onExpensesDeleted = function (data, callbackData) {        
        hideExpensesPanel();

        $(callbackData.ids).each(function (i, eid) {                // instead of getting all data iterate through deleted items and remove them from DOM
            removeExpenseItem(eid);
            deleteFromExpensesMap(eid);                             // remove also item from expenses map
            filterManager.remove(eid);                              // remove this item from filter list
        });

        disableExpenseItems();
        recalculateItems();
        updateBudgetInfo();
        showUndo(data);
    },

    disableExpenseItems = function () {
        if (hasAnyExpenses())
            return;
        hideExpenseHeader();
        disableExpenseExportBtn();
    },

    removeExpenseItem = function (eid) {
        var item = getExpenseItem(eid);
        item.remove();
    },

    getExpenseId = function (item) {
        return item.closest('.list-group-item').attr('eid');
    },

    getExpensesIds = function (elements) {
        var items = elements.closest('.list-group-item');
        return $.map(items, function (item) {
            return $(item).attr('eid');
        });
    },

    onExpenseUpdateSuccess = function (data) {                      // json data is returned from update controller function

        var json = $.parseJSON(data);
        var bindArea = $('li[eid=' + json.id + ']');

        $.each(json, function (key, value) {
            utils.bind(key, value, bindArea);                       // MOZE zrobic strategie of key zeby dobrze pokazywac currency? use getInputValue for value to get currency format? NIE BO TEJ INFROMACJI JUZ NIE MA....MOGE POLEGAC JEDYNE NA isNumeric i decydować czy tp jest currency? chyba tez nie bo mam ilosc ktora nie jest currency
        });

        var form = $('#editExpenseForm-' + json.id);
        form.parent().fadeOut(300, function () {                    // delete update form - it is not longer needed            
            filterManager.applyFilter();                            // apply filter after updating single row
            updateBudgetInfo();
            form.trigger('detach.formListener');
            $(this).remove();

            $('#expenseBtnArea-' + json.id).fadeIn(300);            // show hidden btns
        });
    },

    updateBudgetInfo = function () {
        var totalPrice = getExpensesTotalPrice();
        budgetInfoManager.updateInfo(totalPrice);
    },

    beforeExpenseUpdate = function (eid) {
        var uiItems = [createSingleExpenseUIItem(eid)];
        uiManager.blockUI(uiItems);
    },

    onExpenseUpdateFailure = function (xhr, eid) {                  // on complete func takes care about unblocking ui
        managePriceField(eid);
        errorUtils.showError(xhr);
    },

    onExpenseUpdateCompleted = function (eid) {
        var uiItems = [createSingleExpenseUIItem(eid)];
        uiManager.unblockUI(uiItems);
    },

    getExpenseItem = function (eid) {
        return $('li[eid=' + eid + ']');
    },

    showUndo = function (view) {
        var undoSettings = { 'success': 'expensesManager.undo' };
        var data = { view: view, uiItems: ['#expensesContainer'] };
        undoUtils.showUndo(data, undoSettings);
    },

    undo = function () {
        undoUtils.hideUndo();
        refreshExpenses();
    },

    manageEditFields = function (eid) {                                         // must be passed to appropriately manage fileds, especially while editing couple expenses 
        managePriceField(eid);
        $('.calcField-' + eid).keyup({ eid: eid }, onCalcFieldKeyUp);
    },

    managePriceField = function (eid) {
        var isQuantityEmpty = !($('#' + eid + '-Quantity').val())
        var isUnitPriceEmpty = !($('#' + eid + '-UnitPrice').val());

        if (isQuantityEmpty && isUnitPriceEmpty)
            return;

        formUtils.disableInput($('#' + eid + '-Price'));
    },

    beforeExpenseDelete = function () {
        disableSortable();                                      // this action is required to disable sorting while deleting single item
    },

    onDeleteFailed = function () {
        enableSortable();                                       // if something went wrong enable sortable
    },

    onCalcFieldKeyUp = function (e) {
        var eid = e.data.eid;

        var quantity = $('#' + eid + '-Quantity').val();
        var unitPrice = $('#' + eid + '-UnitPrice').val();

        var isQuantityEmpty = !quantity;
        var isUnitPriceEmpty = !unitPrice;

        var priceField = $('#' + eid + '-Price');

        if (!isQuantityEmpty || !isUnitPriceEmpty) {
            formUtils.disableInput(priceField);
        }
        if (!isQuantityEmpty && !isUnitPriceEmpty) {
            var quantityNo = parseInt(quantity, 10);
            var unitPriceNo = utils.toFloat(unitPrice);

            var price = utils.toCurrencyFormat(quantityNo * unitPriceNo);
            priceField.val(price);
        }
        if (isQuantityEmpty && isUnitPriceEmpty) {
            formUtils.enableInput(priceField);
        }
    };

    return {
        init: init,
        beforeExpenseUpdate: beforeExpenseUpdate,
        onExpenseUpdateSuccess: onExpenseUpdateSuccess,
        onExpenseUpdateFailure: onExpenseUpdateFailure,
        onExpenseUpdateCompleted: onExpenseUpdateCompleted,
        undo: undo,
        manageEditFields: manageEditFields,
        updateExpensesMap: updateExpensesMap,
        updateFilterMap: updateFilterMap
    };
}();