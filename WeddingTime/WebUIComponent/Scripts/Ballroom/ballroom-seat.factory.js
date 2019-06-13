'use strict'
var BallroomSeatFactory = function () {

    var nextIndex = 0,                      // last index is passed from manager after calculating max index for seats taken from server
        TopSeat = 0,                        // values correspond to side seat enum
        BottomSeat = 1,

        createSeat = function (model) {     // model is and specyfic item model
            var seat = model.Seats[nextIndex] = createSeatBaseModel(model);
            var seatModel = { id: nextIndex, seat: seat };

            reportSeatCreated();
            return seatModel;
        },

        createTopSeat = function (model) {
            createSideSeat(model, TopSeat);
        },

        createBottomSeat = function (model) {
            createSideSeat(model, BottomSeat);
        },

        setIndex = function (index) {       // sets next available index for items
            nextIndex = index + 1;
        },

        createSideSeat = function (model, location) {
            var seat = model.Seats[nextIndex] = createSeatBaseModel(model);
            seat.Location = location;

            utmostSeatCreatePropStrategy[location](model, seat);
            reportSeatCreated();
        },

        createTopSeatProp = function (model, seat) {
            model.TopSeat = createUtmostSeatPropModel(seat);
        },

        createBottomSeatProp = function (model, seat) {
            model.BottomSeat = createUtmostSeatPropModel(seat);
        },

        createSeatBaseModel = function (model) {
            return {
                PersonId: null,
                TakenBy: null,
                TableId: model.Id,
                Hidden: false,
                Occupied: false
            };
        },

        createUtmostSeatPropModel = function (seat) {
            return { Key: nextIndex, Value: seat };
        },

        reportSeatCreated = function () {
            nextIndex++;
        };

    var utmostSeatCreatePropStrategy = {};
    utmostSeatCreatePropStrategy[TopSeat] = createTopSeatProp;
    utmostSeatCreatePropStrategy[BottomSeat] = createBottomSeatProp;

    return {
        createSeat: createSeat,
        createTopSeat: createTopSeat,
        createBottomSeat: createBottomSeat,
        setIndex: setIndex
    };
};