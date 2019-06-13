'use strict'
var SeatsRoundService = function (seatFactory, itemsDict, utils, ballroomEnums) {
    SeatsServiceBase.call(this, seatFactory, itemsDict, utils, ballroomEnums);
    this.strategy['stateDelete'] = this.onDelete;
};

SeatsRoundService.prototype = $.extend(Object.create(SeatsServiceBase.prototype), {

    onUnassigned: function (btn) {                                  // overloaded method
        this.setState(btn, 'stateDelete');
    },

    render: function (data, tid) {                                  // overloaded method
        var tmplData = $.map(data, function (value, key) { return { key: key, value: value }; });       // this is how object should be constructed to be able to use jsrender template
        var tmpl = $.render['seatRound'](tmplData);

        var area = $('#item-' + tid);
        area.append(tmpl);

        var addedItem = area.children().last();
        
        $(this).trigger('roundSeatRendered', [addedItem, tid]);                                         // must be invoked before organize in circle func, because first it has to be resized
        this.organizeInCircle(tid);
    },

    getState: function (btn) {                                      // overloaded method
        return btn.hasClass('btn-seat-add') ? 'stateAdd' : 'stateDelete';
    },

    getLayout: function (seat) {                                    // overloaded method
        var chord = seat.closest('.table-round-area').width() / 2;  // to know where is the circle chord (cięciwa), since circle is always placed in the center of the area the same value is applicable for both width and height  which means I can also use it to decide if name should be placed in the top or bottom
        var seatArea = seat.closest('.chair-round-area');           // base on this item I know what is the seat position

        return this.decideLayout(chord, seatArea);
    },

    addSeats: function (itemModel) {
        var data = {};                                              // have to prepare render data model which has specific structure
        var seatData = this.seatFactory.createSeat(itemModel);
        data[seatData.id] = seatData.seat;
       
        this.render(data, itemModel.Id);
    },

    canAdd: function (tid) {
        var count = this.seatsCount(tid);
        return count < 12;
    },

    canDelete: function (tid) {
        var count = this.seatsCount(tid);
        return count > 3;
    },

    seatsCount: function (tid) {
        var area = $('#item-' + tid);
        return area.find('.chair-round-area').length;
    },

    onDelete: function (btn) {
        var tid = this.getTableId(btn);
       
        if (!this.canDelete(tid))
            return false;

        var sid = this.getId(btn);
        delete this.itemsDict[tid].Seats[sid]                       // method to delete element from js object (dict)

        btn.closest('.chair-round-area').remove();                  // not needed if table will be refreshed? or I should relay on data...don't know yet 

        $(this).trigger('roundSeatDeleted', [tid]);                 // must be invoked before organize in circle func, because first area has to be resized
        this.organizeInCircle(tid);

        return true;
    },

    organizeInCircle: function (tid) {
        var tableArea = $('#item-' + tid);                          // table round area
        var seatsAreas = tableArea.find('.chair-round-area');

        var count = seatsAreas.length;

        var radius = count < 7 ? 40 : count < 9 ? 50 : count < 11 ? 66 : 75;
        var angle = 0;
        var step = (2 * Math.PI) / count;

        var centerX = tableArea.width() / 2;
        var centerY = tableArea.height() / 2;

        var thisObj = this;

        seatsAreas.each(function () {
            var area = $(this);
            var x = Math.round(centerX + radius * Math.cos(angle) - area.width() / 2);
            var y = Math.round(centerY + radius * Math.sin(angle) - area.height() / 2);

            area.css({ left: x + 'px', top: y + 'px' });
            angle += step;

            thisObj.reorganizeNameLayout(centerX, area);            // centerX in that case represents circle chord
        });
    },

    decideLayout: function (chord, seatArea) {                      // chord - cięciwa (in other words center x and y)
        var chairLeft = seatArea.position().left;
        var areaWidth = seatArea.width();                           
                                                                   
        var positionX = chairLeft + (areaWidth / 2);                // if I add this value to the chair area position I have exact position of the center of the chair area. Based on that value I can decide where seat is located and which style should be applied (left, right or middle)
        var positionY = seatArea.position().top;

        var contiguousPoint = chairLeft + areaWidth;                // in some cases if right side of the chair (contiguousPoint) is contiguous with circle chord I have to place name above or below seat. This is the first condition which is checked while deciding about the style

        var middleBoundary = (contiguousPoint - 5) < chord && chord < (contiguousPoint + 5);

        var side = middleBoundary ? (chord > positionY ? 'top' : 'bottom') : chord > positionX ? 'left' : chord < positionX ? 'right' : chord > positionY ? 'top' : 'bottom';
        return { Layout: 'chair-round-person-' + side };
    },
    
    reorganizeNameLayout: function (chord, seatArea) {
        var personName = seatArea.find('.chair-person');
        if (personName.length === 0)
            return;

        personName.removeClass(function (index, css) {              // first remove class that decides about name layout
            return css.match(/(^|\s)chair-round-person-\S+/g)[0];   // ^ - must starts with, \s - white space, \S+ - any number of non white space characters, /g - global means that this will be applied for the whole string not just the first matching inside string. If more then one occurence this is solution - (css.match(/(^|\s)chair-round-person-\S+/g) || []).join(' ')
        });

        var data = this.decideLayout(chord, seatArea);              // decide about new layout class and apply it
        personName.addClass(data.Layout);
    }
});