'use strict';
var tasksManager = function () {

    var _updateTaskModal = $('#updateTaskModal'),
        _updateTaskContent = $('#updateTaskContent'),

        _newTasksArea = $('#newTasksArea'),
        _ongoingTasksArea = $('#ongoingTasksArea'),
        _doneTasksArea = $('#doneTasksArea'),
        _switchDisplay = $('.switch-display'),
        _isLayoutExpanded = false,

        draggableOptions = { revert: 'invalid', zIndex: 100, handle: '.task-item-cell.handle' },

        appendByStateStrategy = {
            '0': function (task, taskId) { _newTasksArea.append(task); changeCardSkin(taskId, 'green'); },
            '1': function (task, taskId) { _ongoingTasksArea.append(task); changeCardSkin(taskId, 'blue'); },
            '2': function (task, taskId) { _doneTasksArea.append(task); changeCardSkin(taskId, 'powder'); }
        },
        isMobile = $.browser.mobile,

    init = function () {
        enhanceModal();
        enhanceBtns();
        enhanceTasksBoard();
        enhanceForm();
        bindCardManagerEvents();
        expandLayout();
        
        getTasks();
    },

    expandLayout = function () {
        var data = cookieManager.get('taskslayout');
        if (data !== null && data.isExpanded === true) {
            toggleTaskLayout();
        }
    },

    changeCardSkin = function (taskId, skin) {
        var taskItem = $('#' + taskId).find('.task-item');
        var handle = taskItem.find('.handle');

        exchangeSkinForItem(taskItem, skin);
        exchangeSkinForItem(handle, skin);
    },

    exchangeSkinForItem = function (item, skinType) {
        item.removeClass(function (index, css) {
            return (css.match(/(^|\s)skin-\S+/g) || [])[0];         // ^ - must starts with, \s - white space, \S+ - any number of non white space characters, /g - global means that this will be applied for the whole string not just the first matching inside string.       
        }).addClass('skin-' + skinType);
    },

    bindCardManagerEvents = function () {
        $(cardManager).on('cardInserted', rescanFrom);              // these events are triggered by cardManager - I did it like this cause I don't want to seach for form that belongs to taskManager in cardManager
        $(cardManager).on('cardRemoved', dirtyForm);
    },

    rescanFrom = function () {
        getTaskModalForm().trigger('rescan.formListener');
    },

    dirtyForm = function () {
        getTaskModalForm().trigger('dirty.formListener');
    },

    getTasks = function () {
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onTasksFetched }, ['#tasksBoard']);
        tasksService.getTasks(ajaxSettings);
    },

    enhanceModal = function () {
        _updateTaskModal.on('shown.bs.modal', function (e) {
            var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onEditViewCreated }, [getItemsForUIManager()]);
            var data = { taskId: $(e.relatedTarget).closest('.task-box').attr('id') };      // e.relatedTarget is an item that triggers modal - task-title

            tasksService.prepareUpdate(data, ajaxSettings);
        })
        .on('hidden.bs.modal', function () {
            if (_updateTaskModal.data('updated')) {
                updateTask();
                _updateTaskModal.data('updated', false);

                uiManager.unblockUI([getItemsForUIManager()]);
            }
            _updateTaskContent.empty();
        });
    },

    enhanceTasksBoard = function () {
        wireDroppableTo(_newTasksArea);
        wireDroppableTo(_ongoingTasksArea);
        wireDroppableTo(_doneTasksArea);
    },

    enhanceBtns = function () {
        $('#btnAddTask').click(function () {
            $(this).fadeOut(100, function () {
                $('#newTaskArea').show('slide', 500, function () {
                    formUtils.setFocusById('txtBudgetValue');
                });
            });
        });

        $('#btnCancelAdd').click(function () {
            hideAddTaskInput();
        });

        $('#btnToggleLayout').click(function () {
            toggleTaskLayout();
            makeTasksSmaller();
            cookieManager.set('taskslayout', { isExpanded: _isLayoutExpanded }, '/Tasks');
        });
    },

    toggleTaskLayout = function () {
        var container = $('#tasksContainer');

        utils.toggleLayout(container);        
        toggleLayoutAdditionalClasses(container);

        _isLayoutExpanded = !_isLayoutExpanded;
    },

    makeTasksSmaller = function () {
        $('.task-item-cell.details').toggleClass('full-screen');
    },

    makeSingleTaskSmaller = function (item) {
        if (!_isLayoutExpanded)
            return;

        item.find('.task-item-cell.details').addClass('full-screen');
    },

    toggleLayoutAdditionalClasses = function (container) {        
        $('.tasks-cell.outside').toggleClass('tasks-single-line tasks-double-line');
        container.toggleClass('tasks-expanded');
    },

    enhanceForm = function () {                                                     // initiazlize form listener for capturing any form changes
        var form = getTaskModalForm();
        formUtils.initListener(form, { dynamicAreaMarksDirty: true, dynamicAreasIdentifier: 'cid' });
    },

    hideAddTaskInput = function () {
        $('#newTaskArea').hide('slide', 500, function () {
            $('#btnAddTask').fadeIn(200);
            formUtils.clearInput('txtBudgetValue');
            uiManager.unblockUI(['#newTaskArea']);
        });
    },

    onTasksFetched = function (data) {
        appendTasks(data);        
        $('.task-box').draggable(draggableOptions);
        $('.btn-delete-task').click(deleteTask);
    },

    wireDroppableTo = function (area) {
        area.droppable({
            accept: function (draggable) {                                  // if parent of draggable item is the same as the droppable - do not accept
                var draggableParentId = draggable.parent().attr('id');
                var droppableId = $(this).attr('id');
                return draggableParentId != droppableId;
            },
            drop: function (e, ui) {
                var droppable = $(this);                                    // whole droppable area - data-state describes the line (NewTask, OngoingTask or DoneTask)
                var newState = droppable.data('state');
                
                var draggable = ui.draggable;
                var fromArea = draggable.parent();                          // for error handling purpose area from where item is dragged must be remembered 
                var fromState = fromArea.data('state');

                moveTaskToNewLine(newState, draggable);
                updateTaskState(newState, fromState, draggable);
            },
            hoverClass: 'tasks-hover'
        });
    },

    moveTaskToNewLine = function (newState, draggable) {
        draggable.css({ top: 0, left: 0 });
        appendByStateStrategy[newState](draggable, draggable.attr('id'));
    },

    updateTaskState = function (newState, fromState, draggable) {
        var tid = draggable.attr('id');

        var settings = {
            type: 'POST',
            error: onUpdateTaskStateFailure,
            errorCallbackData: { fromState: fromState, draggable: draggable, tid: tid }
        };
        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [draggable]);
        var data = { taskId: tid, newState: newState };

        tasksService.updateState(data, ajaxSettings);
    },

    onUpdateTaskStateFailure = function (xhr, callbackData) {        
        appendByStateStrategy[callbackData.fromState](callbackData.draggable, callbackData.tid);    // bring back task to the area from where it was dragged
    },

    beforeTaskSave = function () {        
        uiManager.blockUI(['#newTaskArea']);
    },

    onTaskSaved = function (data) {
        pinTask(data);
        hideAddTaskInput();
    },

    onTaskSaveFailure = function (xhr) {                                        // called if ajax request failed
        errorUtils.showError(xhr);
        uiManager.unblockUI(['#newTaskArea']);
    },

    pinTask = function (data) {
        var tmpl = $.render['task'](data);
        appendByStateStrategy[data.State](tmpl, data.Id);

        var newTask = $('#' + data.Id);    
        makeSingleTaskSmaller(newTask);

        newTask.draggable(draggableOptions).hide().fadeIn();
        newTask.find('.btn-delete-task').click(deleteTask);
    },

    beforeTaskUpdate = function () {
        uiManager.blockUI([getItemsForUIManager()]);
    },

    onTaskUpdated = function () {
        _updateTaskModal.data('updated', true);
        _updateTaskModal.modal('hide');
    },

    onTaskUpdateFailure = function (xhr) {
        errorUtils.showError(xhr);
        uiManager.unblockUI([getItemsForUIManager()]);
    },

    getItemsForUIManager = function () {
        return _updateTaskModal.find('.modal-content');
    },

    appendTasks = function (items) {                                            // function called to add tasks to specyfic area on page init
        $.each(items, function (i, item) {
            var tmpl = $.render['task'](item);
            appendByStateStrategy[item.State](tmpl, item.Id);

            var task = $('#' + item.Id);            
            makeSingleTaskSmaller(task)

            if (!isMobile) {                                                    // skip animation on mobile devices
                var time = utils.getRandomInt(500, 1500);
                task.hide().fadeIn(time);
            }
        });
    },

    updateTask = function () {
        var formData = getFormData();
        var taskId = formData.id;
        var task = $('#' + taskId);

        task.fadeOut(300, function () {
            utils.bind('title', formData.title, task);                          // task is used also here as a bindArea 
            utils.bind('reminderDate', formData.reminderDate, task);
            changeStateLine(task, taskId, formData.state);                      // formData.State - it is new state
            task.fadeIn(300);
        });
    },

    deleteTask = function () {
        var btn = $(this);
        var taskBox = btn.closest('.task-box');

        var settings = { type: 'POST', success: onTaskDeleted, successCallbackData: { task: taskBox } };
        var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [taskBox]);      // block the whole task box
        var data = { taskId: taskBox.attr('id') };

        tasksService.deleteTask(data, ajaxSettings);
    },

    onTaskDeleted = function (data, callbackData) {                             // undo data (rendered view) and callback
        callbackData.task.fadeOut(300, function () {
            $(this).remove();
        });

        showUndo(data);
    },

    showUndo = function (view) {                                                // view is rendered view that is going to be shown
        var undoSettings = { 'success': 'tasksManager.undo' };
        var data = { view: view, uiItems: ['#tasksContainer'] };
        undoUtils.showUndo(data, undoSettings);
    },

    undo = function (data) {
        undoUtils.hideUndo();
        pinTask(data);
    },

    changeStateLine = function (task, taskId, newState) {                       // change only if state was updated 
        var currentState = task.parent().data('state');
        if (currentState == newState)
            return;

        appendByStateStrategy[newState](task, taskId);
    },

    getFormData = function () {
        var array = getTaskModalForm().serializeArray();                        // is key value pair of items that are placed on the form
        return utils.serializeArrayToJson(array);
    },

    getTaskModalForm = function () {
        return _updateTaskModal.find('form');
    },

    onEditViewCreated = function (data, callbackData) {                         // used by both create and update modal, callbackData.content determines in which area prepared view should be added        
        _updateTaskContent.html(data).hide().show('blind', 300, function () {
            if (!isMobile)
                formUtils.setFocusByRef($(this).find('input').first());         // set focus on the first input - in that case it is task title
        });
        $.validator.unobtrusive.parseDynamicContent(_updateTaskContent);        // add prepared edit view to the dialog content area

        $('#txtReminderDate').datepicker({
            //changeMonth: true,
            //changeYear: true,
            minDate: 1,                                                         // the minimum selectable date, a number of days from today
            firstDay: 1,
            dateFormat: 'dd/mm/yy',
            monthNames: ['Styczeń', 'Luty', 'Marzec', 'Kwiecień', 'Maj', 'Czerwiec', 'Lipiec', 'Sierpień', 'Wrzesień', 'Październik', 'Listopad', 'Grudzień'],
            //monthNamesShort: ['Sty', 'Lu', 'Mar', 'Kw', 'Maj', 'Cze', 'Lip', 'Sie', 'Wrz', 'Pa', 'Lis', 'Gru'],
            //dayNames: ['Niedziela', 'Poniedzialek', 'Wtorek', 'Środa', 'Czwartek', 'Piątek', 'Sobota'],
            //dayNamesShort: ['Nie', 'Pn', 'Wt', 'Śr', 'Czw', 'Pt', 'So'],
            dayNamesMin: ['N', 'Pn', 'Wt', 'Śr', 'Cz', 'Pt', 'So']
        });

        $('#btnReminderDate').click(function () {
            $('#txtReminderDate').datepicker('show');
        });
        $('#btnClearReminderDate').click(function () {
            formUtils.clearInputWithEvent('txtReminderDate');
            formUtils.clearInput('reminderDateHidden');
        });

        getTaskModalForm().trigger('initialize.formListener');
    };

    return {
        init: init,
        beforeTaskSave: beforeTaskSave,
        onTaskSaved: onTaskSaved,
        onTaskSaveFailure: onTaskSaveFailure,
        beforeTaskUpdate: beforeTaskUpdate,
        onTaskUpdated: onTaskUpdated,
        onTaskUpdateFailure: onTaskUpdateFailure,
        undo: undo
    };
}();