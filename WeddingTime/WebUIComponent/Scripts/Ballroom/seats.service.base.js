'use strict'
function SeatsServiceBase(seatFactory, itemsDict, utils, ballroomEnums) {
    this.seatFactory = seatFactory;
    this.itemsDict = itemsDict;
    this.utils = utils;
    this.ballroomEnums = ballroomEnums;
    this.strategy = {                                               // common for both classes, this dict is complemented in classes
        'stateUnassign': this.onUnassign,
        'stateAdd': this.onAdd
    };

    this.assignmentStrategy = {};
    this.assignmentStrategy[ballroomEnums.assignmentType.Person] = this.reportPersonAssignment;
    this.assignmentStrategy[ballroomEnums.assignmentType.Newlyweds] = this.reportNewlywedsAssignment;
};

SeatsServiceBase.prototype = {
    onUnassigned: null,                                             // to override (abstract)
    getState: null,                                                 // to override (abstract)
    getLayout: null,                                                // to override (abstract)
    addSeats: null,                                                 // to override (abstract) 
    canAdd: null,                                                   // to override (abstract)

    process: function (state, btn) {
        return this.strategy[state === undefined ? this.getState(btn) : state].call(this, btn);
    },

    onAssign: function (data) {
        var seat = $('div[sid=' + data.sid + ']');
        var tid = this.getTableId(seat);

        this.reportSeatAssignment(tid, data);

        this.renderPersonName(seat, data.person);
        seat.droppable('disable');                                  // disable seat droppable to be not able to assign person once again

        var btn = seat.parent().find('.btn-seats-action');          // change btn state to be able to unassign person
        this.setState(btn, 'stateUnassign');
        this.utils.adjustIconToAvailability(btn);
    },

    onUnassign: function (btn) {
        var tid = this.getTableId(btn);
        var sid = this.getId(btn);                                  // get seat id
        var seat = this.getSeat(tid, sid);
        var pid = seat.PersonId === null ? this.ballroomEnums.newlywedsType[seat.TakenBy] : seat.PersonId;

        var seatDiv = $('div[sid=' + sid + ']');
        seatDiv.droppable('enable');
        seatDiv.parent().find('div.chair-person').remove();

        this.reportSeatUnassignment(seat);
        this.onUnassigned(btn);                                     // additional action (type specyfic) on person unassigned

        $(this).trigger('seatUnassigned', [pid]);                   // trigger event to enable person on the list (managed by ballroom manager)

        return true;
    },

    onAdd: function (btn) {
        var tid = this.getTableId(btn);

        if (!this.canAdd(tid))                                      // check if another seats can be added
            return false;

        var itemModel = this.itemsDict[tid];
        this.addSeats(itemModel);

        return true;
    },

    renderPersonName: function (seat, person) {        
        var layout = this.getLayout(seat);                          // getLayout is overriden function
        var data = $.extend({}, layout, person);                    // create new data tmpl obj - join layout and person object properties needed for person name tmpl

        seat.after($.render['personName'](data));                   // insert tmpl after seat obj
    },

    reportSeatAssignment: function (tid, data) {
        var seat = this.getSeat(tid, data.sid);        
        if (seat.Occupied)                                          // true when page is first time rendered and we have already all information, false for new seat assignment
            return;                                                 // had to comment it out since if seat is taken by newlyweds it must be reported here as well, so I have to repeat this operation for different items

        this.assignmentStrategy[data.assignmentType](seat, data);
    },
    
    reportPersonAssignment: function (seat, data) {
        seat.Occupied = true;
        seat.PersonId = data.pid;
        seat.GroupId = data.person.GroupId;
    },

    reportNewlywedsAssignment: function (seat, data) {
        seat.Occupied = true;
        seat.TakenBy = data.takenBy;
    },

    reportSeatUnassignment: function (seat) {
        seat.Occupied = false;
        seat.PersonId = null;
        seat.TakenBy = null;
        seat.GroupId = null;
    },

    getId: function (btn) {                                         // to utilites?
        return btn.parent().find('div.chair').attr('sid');
    },

    getTableId: function (item) {
        return item.closest('.ballroom-item').attr('id')
    },

    getSeat: function (tid, sid) {
        return this.itemsDict[tid].Seats[sid];
    },

    setState: function (btn, newState) {                            // set new button state
        btn.data('state', newState);
    }
};