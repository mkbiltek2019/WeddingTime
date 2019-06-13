'use strict'
var BallroomItemFactory = function (ballroomSeatFactory) {

    var seatFactory = ballroomSeatFactory,
        nextIndex = 0,                      // last index is passed after calculating max index value for items taken from server
        TableRect = 0,                      // const values that correspond to item type enum values
        TableRound = 1,

        create = function (itemType) {
            var model = createBaseItemModel(itemType);
            complementItemModel(itemType, model);

            reportItemCreated();
            return model;
        },

        setIndex = function (index) {       // sets next available index for items
            nextIndex = index + 1;
        },

        complementItemModel = function (itemType, model) {
            var complementFunc = complementItemModelStrategy[itemType];
            if ($.isFunction(complementFunc))
                complementFunc(model);
        },

        complementTableRectModel = function (model) {
            model.Seats = {};                               // contains also top and bottom seats
            seatFactory.createSeat(model);
            seatFactory.createSeat(model);

            seatFactory.createTopSeat(model);
            seatFactory.createBottomSeat(model);
        },

        complementTableRoundModel = function (model) {
            model.Seats = {};
            seatFactory.createSeat(model);
            seatFactory.createSeat(model);
            seatFactory.createSeat(model);
        },

        createBaseItemModel = function (itemType) {         // itemType should be of type int taken from item type enum
            return {
                Id: nextIndex,
                PositionX: 40,
                PositionY: 0,
                Rotation: 0,
                ItemType: itemType
            };
        },
        
        reportItemCreated = function () {
            nextIndex++;
        };

    var complementItemModelStrategy = {};
    complementItemModelStrategy[TableRect] = complementTableRectModel;
    complementItemModelStrategy[TableRound] = complementTableRoundModel;

    return {
        create: create,
        setIndex: setIndex
    };
};