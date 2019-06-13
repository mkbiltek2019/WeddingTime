'use strict'
var SeatsRectService = function (seatFactory, itemsDict, utils, ballroomEnums) {
    SeatsServiceBase.call(this, seatFactory, itemsDict, utils, ballroomEnums);
    this.strategy['stateToggle'] = this.onToggleVisibility;
    this.strategy['stateDeleteRow'] = this.onDeleteSeatsRow;    
};

SeatsRectService.prototype = $.extend(Object.create(SeatsServiceBase.prototype), {

    // because this is callback function - we can't use for instance this.updateWhenUnassigned
    // we have to pass SeatsRectService object defined as thisObj in callback data
    onUnassigned: function (btn) {                                                          // overloaded method
        this.setState(btn, 'stateToggle');
        this.utils.adjustIconToAvailability(btn);
    },

    render: function (data, tid) {                                                          // overloaded method
        var tmpl = $.render['seatsRow'](data);
        var area = $('#seatsCollection-' + tid);
        area.append(tmpl);

        var addedItem = area.children().last();
        this.joinTableRows(addedItem);

        $(this).trigger('seatsRowRendered', [addedItem, tid]);                              // this trigger is subscribed by rect service obj created in manager
    },

    getState: function (btn) {                                                              // overloaded method
        return btn.hasClass('btn-delete-row') ? 'stateDeleteRow' :
               btn.hasClass('btn-seat-add') ? 'stateAdd' : 'stateToggle';
    },

    getLayout: function (seat) {                                                            // overloaded method
        var location = seat.parent().data('location');
        return { Layout: 'chair-person-' + location };
    },

    addSeats: function (itemModel, tid) {    
        var data = {};                                                                      // have to prepare render data model which has specific structure
        this.addSeatToModel(itemModel, data);
        this.addSeatToModel(itemModel, data);

        this.render(data, itemModel.Id);
    },

    canAdd: function (tid) {
        var count = this.seatsCount(tid);
        return count < 25;
    },

    canDelete: function (tid) {        
        var count = this.seatsCount(tid);
        return count > 1;                                                                   // because 2 always available are side seats
    },

    seatsCount: function (tid) {
        var area = $('#item-' + tid);
        return area.find('.seats-row').length;
    },

    onToggleVisibility: function (btn) {                                                    // methods specific for current 'class'
        var tid = this.getTableId(btn);
        var sid = this.getId(btn);
        
        btn.parent().find('div.chair').toggle();
        this.utils.adjustIconToVisibility(btn);

        var seat = this.getSeat(tid, sid);
        seat.Hidden = !seat.Hidden;

        return true;
    },

    onDeleteSeatsRow: function (btn) {
        var tid = this.getTableId(btn);
        
        if (!this.canDelete(tid))
            return false;

        var seatsRow = btn.closest('.seats-row');
        var ids = this.utils.getIds(seatsRow);

        this.detachTableRows(seatsRow);                     // maybe should be moved to ballroom manager, but since I'm removing seat row here it would be harder to do it on manager side (I would have to pass some more informations)
        seatsRow.remove();
        
        var itemModel = this.itemsDict[tid];
        var bEnums = this.ballroomEnums;
        var personsIds = [];                                // to remember and pass list of ids for which draggable should be enabled
        $.each(ids, function (i, id) {
            var pid = itemModel.Seats[id].PersonId !== null ? itemModel.Seats[id].PersonId : bEnums.newlywedsType[itemModel.Seats[id].TakenBy];
            if (pid != null)
                personsIds.push(pid);

            delete itemModel.Seats[id];
        });

        $(this).trigger('seatsRowDeleted', [tid, personsIds]);

        return true;
    },

    joinTableRows: function (addedRow) {
        if (addedRow.index() === 0)
            return;

        addedRow.find('.table-rect').addClass('join-up');
        addedRow.prev().find('.table-rect').addClass('join-down');
    },

    detachTableRows: function (seatsRow) {                                                  // works only for extreme tableRow (bottom and top)
        var rowIndex = seatsRow.index();
        var rowsCount = seatsRow.parent().children().length;

        if (rowIndex === rowsCount - 1) {                                                   // always end up here in case we have one seats row (even if row index is 0 and seems like second condition should be called). In that case do nothing when it comes to other items
            if (rowIndex !== 0)
                seatsRow.prev().find('.table-rect').removeClass('join-down');
        }
        else if (rowIndex === 0) {                                                          // code will be executed only if there are more than 1 rows, so there always be next item available
            seatsRow.next().find('.table-rect').removeClass('join-up');
        }
    },

    hideSeat: function (sid) {                                                              // used when page is rendered
        var seat = $('div[sid=' + sid + ']')
        seat.hide();
        var btn = seat.parent().find('.btn-seats-action');                                  // change btn state to be able to unassign person
        seatUtils.adjustIconToVisibility(btn);                                              // btn contatins data side element which is used to call strategy to toggle icon
    },

    addSeatToModel: function (itemModel, data) {
        var seatData = this.seatFactory.createSeat(itemModel);
        data[seatData.id] = seatData.seat;                                                  // complement render data model
    }
});