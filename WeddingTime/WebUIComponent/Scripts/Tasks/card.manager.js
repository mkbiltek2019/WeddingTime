'use strict';
var cardManager = function () {

    var _cardModal = $('#taskCardModal'),
        _cardItems = $('#cardItems'),
        _cardItemInputArea = $('#cardItemInputArea'),
        _cardItemInput = $('#cardItemInput'),
        validationStrategy = {
            'Email': function (value) { return validateEmail(value); },     // regex validation
            'Link': function (value) { return validateLink(value); },       // can't have any spaces
            'Phone': function (value) { return validatePhone(value); }      // only digits and some signs (-, +, space, brackets)
        },
        formatStrategy = {
            'Link': function (value) { return formatLink(value); },         // must have http before specyfic address
        },
        tmplNameStrategy = {
            'Email': 'emailItem',
            'Link': 'linkItem',
            'Phone': 'phoneItem',
            'ContactPerson': 'contactPersonItem',
            'Address': 'addressItem'
        },
        isMobile = $.browser.mobile,

    init = function () {
        enhanceModal();
        enhanceBtns();
        enhanceInputs();
        enhanceAreas();
        enhanceForm();
    },

    renderCards = function (cards) {                        // I could pass here also taskId instead of have it in the model...but maybe it's not a good idea...
        for (var i = 0; i < cards.length; i++) {
            renderCard(cards[i], i);
        };
    },

    beforeCardUpdate = function () {
        uiManager.blockUI([getItemsForUIManager()]);
    },

    onCardUpdated = function (data) {        
        _cardModal.data('model', data);
        _cardModal.modal('hide');
    },

    onCardUpdateFailure = function (xhr) {
        errorUtils.showError(xhr);
        uiManager.unblockUI([getItemsForUIManager()]);
    },

    getItemsForUIManager = function () {
        return _cardModal.find('.modal-content');
    },

    renderCard = function (data, index) {
        data.Index = index;                                             // this is the trick to pass and index for hidden fields, can't use the one provided by jsrender (#index) because not always list of items is used and I need different indexes for instance when adding new item in the dialog
        var tmpl = $.render['card'](data);

        var cardsArea = $('#cardsArea');
        cardsArea.append(tmpl);
        var addedCard = cardsArea.children().last();
        addedCard.find('button.btn-card-remove').click(removeCard);     // this is remove button for card. can't do the same as for _cardItems button.close since this is dynamic content and can't be initialized at the begining
        addedCard.hide().fadeIn(150);
    },

    updateCard = function (data, cid) {
        var card = getCardById(cid);

        data.Index = card.index();                                      // this is the trick to pass and index for hidden fields, can't use the one provided by jsrender (#index) because not always list of items is used and I need different indexes for instance when adding new item in the dialog
        var tmpl = $.render['card'](data);

        card.fadeOut(150, function () {
            card.replaceWith(tmpl);
            card = getCardById(cid);                                    // have to get it again since it was replaced with new item
            card.find('button.btn-card-remove').click(removeCard);      // this is remove button for card. can't do the same as for _cardItems button.close since this is dynamic content and can't be initialized at the begining
            card.hide().fadeIn(150);
        });        
    },

    removeCard = function () {
        $(this).closest('.card').remove();                              // this is button
        recalculateCardsIndexes();
        $(cardManager).trigger('cardRemoved');
    },

    removeCardItem = function () {
        $(this).closest('.card-item').remove();
        recalculateCardItemsIndexes();
        getCardModalForm().trigger('check.formListener');
    },

    showPopover = function () {
        $(this).popover('show');
    },
    
    hidePopover = function () {
        $(this).popover('hide');
    },

    getCardById = function (cid) {
        return $('#cardsArea').find('div[cid=' + cid + ']');
    },

    enhanceModal = function () {
        _cardModal.on('shown.bs.modal', function (e) {
            var btn = $(e.relatedTarget);
            var tid = btn.data('task-id');                                  // button that fires modal has data attr - task id
            var cid = btn.data('card-id');                                  // if card id is defined - let's fetch data
            _cardModal.find('#TaskId').val(tid);                            // then I rewrite its value to hidden field in task card modal

            if (cid != undefined) {
                _cardModal.data('update', true);                            // add info that this is going to be an update
                _cardModal.find('#Id').val(cid);                            // hidden field
                fetchCardData(tid, cid);                
            }
            else {
                _cardModal.data('insert', true);                            // ...or new item will be created
                getCardModalForm().trigger('initialize.formListener');      // have to reinitialize form to get the valid origing fields values
                showCardContent();
                if (!isMobile) formUtils.setFocusById('cardTitle');
            }
        })
        .on('hidden.bs.modal', function () {
            var model = _cardModal.data('model');                           // model is data assigned in callback function after performing update or insert
            tryUpdate(model);                                               // update/insert flag is assigned on modal show, if card id is specified - update other insert
            tryInsert(model);

            uiManager.unblockUI([getItemsForUIManager()]);

            formUtils.clearInput('cardTitle');

            // hide and clear input
            $('.card-select-actions').show();
            hideCardItemInputArea();

            _cardItems.empty();
            _cardModal.find('input[type=hidden]').val('');                  // always clear hidden field (TaksId and CardId)
            hideCardContent();
            invalidateCardModalData();         
        });
    },

    hideCardContent = function () {
        $('#taskCardContent').children().first().addClass('hidden');
    },

    showCardContent = function () {
        $('#taskCardContent').children().first().removeClass('hidden');
    },

    tryUpdate = function (model) {
        if (model == null || !_cardModal.data('update'))
            return;

        updateCard(model, _cardModal.find('[id=Id]').val());            // find hidden Id field with card id value   
    },

    getCardModalForm = function () {
        return _cardModal.find('form');
    },

    tryInsert = function (model) {
        if (model == null || !_cardModal.data('insert'))
            return;

        renderCard(model, getNextCardIndex());
        $(cardManager).trigger('cardInserted');
    },

    invalidateCardModalData = function () {
        _cardModal.data('model', null);
        _cardModal.data('update', false);
        _cardModal.data('insert', false);
    },

    getNextCardIndex = function () {
        return $('#cardsArea').find('div').length;
    },

    enhanceBtns = function () {
        $('.btn-card-item').click(function () {
            var btn = $(this);

            _cardItemInputArea.show('blind', 150);
            _cardItemInput.attr('placeholder', btn.data('title'));
            _cardItemInput.data('item', btn.data('item'));

            $('.card-select-actions').hide();
            $('.card-apply-actions').fadeIn(function () {
                _cardItemInput.focus();
            });
        });        

        $('#btnSaveCardItem').click(function (e) {
            tryAddCardItem();            
        });

        $('#btnCancelCardItem').click(function () {
            animateCardActions();
        });
    },

    enhanceInputs = function () {
        _cardItemInput.keydown(function (e) {
            var charCode = e.charCode || e.keyCode || e.which;
            if (charCode == 13) {                                                   // enter key
                tryAddCardItem();
                return false;
            }
        });
    },

    animateCardActions = function () {
        $('.card-select-actions').fadeIn();
        hideCardItemInputArea();
    },

    hideCardItemInputArea = function () {        
        $('.card-apply-actions').hide();        
        _cardItemInput.val('');
        _cardItemInputArea.hide();
        _cardItemInput.removeClass('input-validation-error');
    },

    enhanceAreas = function () {
        _cardItems.on('click', '.card-item button', removeCardItem);                // this is remove button for card item, now it is not needed to attach event to newly added item  
        _cardItems.on('mouseenter', '.card-item a', showPopover)
        _cardItems.on('mouseleave', '.card-item a', hidePopover)
    },

    enhanceForm = function () {                                                     // initiazlize form listener for capturing any form changes
        var form = getCardModalForm();
        formUtils.initListener(form, { dynamicAreaMarksDirty: true, dynamicAreasIdentifier: 'ciid' });
    },

    tryAddCardItem = function () {
        var itemType = _cardItemInput.data('item');
        var value = _cardItemInput.val();

        if (isInputValid(value, itemType)) {
            var nextIndex = _cardItems.find('div.card-item').length;

            processCardItem({ Type: itemType, Value: value }, nextIndex);        // here based on strategy i'm producing appropriate html
            animateCardActions();
            getCardModalForm().trigger('dirty.formListener');
        }
        else {
            _cardItemInput.addClass('input-validation-error');
        }
    },

    fetchCardData = function (tid, cid) {
        var ajaxSettings = uiManager.wrappAjaxWithUI({ success: onCardFetched }, [getItemsForUIManager()]);   // workaround (added seperate div - taskCardContent for showing loading info)
        var data = { taskId: tid, cardId: cid };
        
        tasksService.getCard(data, ajaxSettings);
    },

    onCardFetched = function (data) {                               // invoked when we open card for editing
        var title = $('#cardTitle');
        title.val(data.Title);                                      // first fill in card title
        $.each(data.Items, function (i, item) {
            processCardItem(item, i);
        });
        showCardContent();
        if (!isMobile) formUtils.setFocusByRef(title);
        getCardModalForm().trigger('initialize.formListener');      // have to reinitialize form to get the valid origing fields values
    },

    isInputValid = function (value, itemType) {       
        if (!value.trim())                                          // string is null or empty
            return false;

        var func = validationStrategy[itemType];
        return $.isFunction(func) ? func(value) : true;
    },

    formatValue = function (value, itemType) {
        var func = formatStrategy[itemType];        
        return $.isFunction(func) ? func(value) : value;
    },

    processCardItem = function (item, nextIndex) {
        var itemType = item.Type;
        var value = formatValue(item.Value, itemType);

        var data = {
            Id: item.Id,
            TmplName: tmplNameStrategy[itemType],
            ViewData: { Value: value },
            HiddenData: { Index: nextIndex, Id: item.Id, Value: value, Type: itemType }
        };
        var tmpl = $.render['cardItem'](data);
        _cardItems.append(tmpl);
    },

    recalculateCardsIndexes = function () {
        var hiddenAreas = $('#cardsArea').children('.card').find('.card-hidden-input');

        for (var i = 0; i < hiddenAreas.length; i++) {
            var area = $(hiddenAreas[i]);
            var id = area.find('input[name*=Id]').val();                        // start with selector

            var hiddenData = { Index: i, Id: id };                              // prepare model, render template and replace whole div with new data
            area.replaceWith($.render['cardHidden'](hiddenData));
        }
    },

    recalculateCardItemsIndexes = function () {
        var hiddenAreas = _cardItems.children('div.card-item').find('.card-item-hidden-input');

        for (var i = 0; i < hiddenAreas.length; i++) {
            var area = $(hiddenAreas[i]);

            var id = area.find('input[name*=Id]').val();                                // start with selector
            var value = area.find('input[name*=Value]').val();                          
            var type = area.find('input[name*=Type]').val();

            var hiddenData = { Index: i, Id: id, Value: value, Type: type };            // prepare model, render template and replace whole div with new data
            area.replaceWith($.render['cardItemHidden'](hiddenData));
        }
    },

    validateEmail = function (value) {
        var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        return regex.test(value);
    },
        
    validatePhone = function (value) {
        var regex = /^((\(\+[0-9]*\))?)((\([0-9]*\))?)((\+[0-9]*)?)[0-9 \-]*$/;
        return regex.test(value);
    },

    validateLink = function (value) {
        var regex = /\s/g;              // 's' stands for whitespaces, 'g' global so it look for whitespaces in whole string
        return !regex.test(value);
    },
        
    formatLink = function (value) {
        var regex = /(^(http|https):\/\/){1}/;
        var prefix = 'http://';
        return regex.test(value) ? value : prefix.concat(value);
    };

    return {
        init: init,
        renderCards: renderCards,
        beforeCardUpdate: beforeCardUpdate,
        onCardUpdated: onCardUpdated,
        onCardUpdateFailure: onCardUpdateFailure
    };
}();