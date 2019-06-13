'use strict';
var groupManager = function () {

    var _groups = $('#groups'),
        _createGroupModal = $('#createGroupModal'),
        _editGroupModal = $('#editGroupModal'),
        _updateModel = { gid: null, value: null },                      // created to not call database for fetching updated name
        _createModel = {},                                              // item to hold data after creating new group
        _isMobile = $.browser.mobile,

    init = function () {
        getGroups();

        enhanceCreateModal();
        enhanceEditModal();
        enhanceForms();                                                 // adds to dialog's forms changes listener
    },

    beforeCreate = function () {
        uiManager.blockUI([getItemsForUIManager('createGroupModal')]);
    },

    onCreateSuccess = function (data) {
        _createGroupModal.data('created', true);                        // set flag to know that after modal is hidden data should be added to the view
        _createModel = data;        
        formUtils.hideModal('createGroupModal');
    },

    onCreateFailure = function (xhr) {
        errorUtils.showError(xhr);
        uiManager.unblockUI([getItemsForUIManager('createGroupModal')]);
    },

    beforeUpdate = function () {                                        // just before sending ajax call
        uiManager.blockUI([getItemsForUIManager('editGroupModal')]);
        _updateModel.gid = $('#editGroupId').val();
        _updateModel.value = $('#txtUpdateGroupName').val();
    },

    onUpdateSuccess = function () {
        _editGroupModal.data('updated', true);
        formUtils.hideModal('editGroupModal');        
    },

    onUpdateFailure = function (xhr) {
        errorUtils.showError(xhr);
        uiManager.unblockUI([getItemsForUIManager('editGroupModal')]);
    },

    undo = function (data) {                                    // data is groupId and index where group should be added
        undoUtils.hideUndo();
        undoGroup(data);
    },

    // private methods

    getGroups = function () {                                   // this is called only on page load
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: showGroups }, ['#groups']);
        guestsService.getGroups(ajaxSettings);
    },

    undoGroup = function (undoData) {
        createFakeGroupArea(undoData);                          // fake group area to indicate undoing operation    

        var gid = undoData.GroupId;
        var undoArea = getGroupItem(gid);

        var data = { groupId: gid };
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: revertGroup, successCallbackData: { gid: gid } }, [undoArea]);
        guestsService.getGroup(data, ajaxSettings);
    },

    createFakeGroupArea = function (undoData) {
        var index = undoData.Index;
        var undoArea = $('<div>').attr({ gid: undoData.GroupId, class: 'group-item' });

        if (index === 0)
            $('#groups').prepend(undoArea);
        else
            $('#groups .group-item:eq(' + (index - 1) + ')').after(undoArea);
    },

    revertGroup = function (data, callbackData) {
        var gid = callbackData.gid;

        var undoArea = getGroupItem(gid);
        undoArea.replaceWith(data);

        var group = getGroupItem(gid);
        enhanceGroupRelatedItems(group);

        $.when(personManager.getPersons(gid))
         .then(function () {
             stickyManager.recalculateItems();                  // after adding appropriate group recalculate all items
         });
    },

    showGroups = function (data) {                              // this is called only on page load
        $('#groups').append(data.Html);                         // groups partial view rendered and returned from controller

        personManager.enhanceMembersArea();                     // apply sorting for all visible group members area
        personManager.enhanceMembersBtns();                     // btn are already available - enhance them with click event
        enhanceDeleteBtn();
        enhanceMembersInfoBtn();

        var deferreds = [];
        $.each(data.GroupIds, function (i, gid) {               // next step is to take persons list for concrete group id
            var promise = personManager.getPersons(gid);
            deferreds.push(promise);
        });

        $.when.apply($, deferreds)                              // when all functions are done enhance stick items
              .then(function () {
                  stickyManager.enhanceItems();                 // enhance items also means recalculate
              });
    },

    enhanceDeleteBtn = function (target) {
        var items = target === undefined ? $('.btn-delete-group') : target.find('.btn-delete-group');
        items.on('click', function () {
            var group = getClosestGroupItem($(this));
            var gid = group.attr('gid');

            var settings = {
                type: 'POST',
                success: onGroupDeleted,
                successCallbackData: { gid: gid },
            };
            var ajaxSettings = uiManager.wrappAjaxWithUI(settings, [group]);
            var data = { groupId: gid, index: group.index() };
            guestsService.deleteGroup(data, ajaxSettings);
        });
    },

    enhanceMembersInfoBtn = function (target) {
        var items = target === undefined ? $('.btn-members-info') : target.find('.btn-members-info');
        items.click(function () {
            var btn = $(this);
            var gid = getClosestGroupItem(btn).attr('gid');
            
            if (btn.data('expanded')) {                
                btn.data('expanded', false);
            }
            else {
                var infoPanel = getInfoPanel(gid);
                populateMembersInfoPanel(gid, infoPanel);
                btn.data('expanded', true);                
            }

            toggleInfoPanel(btn, gid);
            stickyManager.recalculateItems();                   // in xs view recalculation has to be done also for bottom stick

        }).on('refresh', function () {
            var btn = $(this);
            var gid = getClosestGroupItem(btn).attr('gid');

            if (btn.data('expanded')) {
                var infoPanel = getInfoPanel(gid);
                infoPanel.fadeOut(300, function () {
                    populateMembersInfoPanel(gid, infoPanel);
                    infoPanel.fadeIn(300);
                });
            }            
        });
    },

    populateMembersInfoPanel = function (gid, infoPanel) {
        var info = membersInfoManager.getMembersInfo(gid);
        $.each(info, function (name, val) {
            utils.bind(name, val, infoPanel);
        });
    },

    getInfoPanel = function(gid){
        return $('#membersInfoPanel-' + gid);
    },

    onGroupDeleted = function (data, callbackData) {
        var gid = callbackData.gid;
        filterManager.remove(gid);                              // remove group from filter manager
        membersInfoManager.remove(gid);

        var item = getGroupItem(gid);
        stickyManager.unwireEvents(item);
        item.remove();
        stickyManager.recalculateItems();                       // after deleting recalculate items

        showUndo(data);
    },

    toggleInfoPanel = function (btn, gid) {
        $('#membersInfoPanel-' + gid).toggle();
        btn.toggleClass('active');
        btn.children('span').toggleClass('icon-expand icon-collapse');
    },
    
    showUndo = function (view) {
        var undoSettings = { 'success': 'groupManager.undo' };
        var data = { view: view, uiItems: ['#guestsContainer'] };
        undoUtils.showUndo(data, undoSettings);
    },
        
    enhanceEditModal = function () {
        _editGroupModal
            .on('show.bs.modal', function (e) {
                var btn = $(e.relatedTarget);                                       // clicked btn

                var item = getClosestGroupItem(btn);
                var name = item.find('.group-name').text();
                var gid = item.attr('gid');

                $('#txtUpdateGroupName').val($.trim(name));                         // trim the white spaces at the beginig and at the end
                $('#editGroupId').val(gid);

                _editGroupModal.find('form').trigger('initialize.formListener');    // to disable submit btn before showing form
            })
            .on('shown.bs.modal', function () {                
                if (!_isMobile) formUtils.setFocusById('txtUpdateGroupName');
            })
            .on('hidden.bs.modal', function () {
                formUtils.clearInput('txtUpdateGroupName');
                $('#editGroupId').val('0');                                         // reset hidden field

                if (_editGroupModal.data('updated')) {
                    onGroupUpdated();
                    uiManager.unblockUI([getItemsForUIManager('editGroupModal')]);
                    _editGroupModal.data('updated', false);
                }
            });
    },
        
    getItemsForUIManager = function (modalId) {
        return $('#' + modalId).find('.modal-content');
    },

    enhanceCreateModal = function () {
        _createGroupModal
            .on('hidden.bs.modal', function () {
                formUtils.clearInput('txtGroupName');

                if (_createGroupModal.data('created')) {
                    onGroupCreated();
                    uiManager.unblockUI([getItemsForUIManager('createGroupModal')]);
                    _createGroupModal.data('created', false);
                }
            })
            .on('show.bs.modal', function () {
                _createGroupModal.find('form').trigger('initialize.formListener');      // to disable submit btn before showing form
            })
            .on('shown.bs.modal', function () {
                if (!_isMobile) formUtils.setFocusById('txtGroupName');
            });
    },
    
    enhanceForms = function () {
        var createForm = _createGroupModal.find('form');
        formUtils.initListener(createForm);

        var updateForm = _editGroupModal.find('form');
        formUtils.initListener(updateForm);
    },

    onGroupCreated = function () {
        _groups.append(_createModel.Html);

        var group = getGroupItem(_createModel.Id);          // choose specyfic element to enhance appropriate items (below functions)                          
        enhanceGroupRelatedItems(group);
        
        utils.scrollToItem(group);
        _createModel = {};
    },

    enhanceGroupRelatedItems = function (group) {        
        personManager.enhanceMembersArea(group);
        personManager.enhanceMembersBtns(group);            // btn are already available - enhance them with click event
        enhanceDeleteBtn(group);
        enhanceMembersInfoBtn(group);
        stickyManager.enhanceItems(group);
    },

    onGroupUpdated = function () {
        var item = getGroupItem(_updateModel.gid).find('.group-name');

        item.fadeOut(300, function () {
            $(this).text(_updateModel.value.toUpperCase()).fadeIn(300, function () {
                invalidateUpdateModel();
                stickyManager.recalculateGroupBoxes();
            });
        });
    },

    invalidateUpdateModel = function () {
        _updateModel.gid = null;
        _updateModel.value = null;
    },

    getClosestGroupItem = function (item) {
        return item.closest('.group-item');
    },

    getGroupItem = function (gid) {
        return $('div[gid=' + gid + ']');
    };

	return {
	    init: init,
	    beforeCreate: beforeCreate,
	    onCreateSuccess: onCreateSuccess,
	    onCreateFailure: onCreateFailure,
	    beforeUpdate: beforeUpdate,
	    onUpdateSuccess: onUpdateSuccess,
        onUpdateFailure: onUpdateFailure,
	    undo: undo
	};
}();