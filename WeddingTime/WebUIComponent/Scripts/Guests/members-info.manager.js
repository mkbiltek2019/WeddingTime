
var membersInfoManager = function () {

    var elements = {},                                  // dict which holds data for each group

    getMembersInfo = function (gid) {
        var model = getModel();

        var items = elements[gid];
        if (items === undefined)
            return model;                               // return empty model

        var temp = { accepted: { count: 0, genres: [] }, declined: 0, unconfirmed: 0 };

        $.each(items, function (i, item) {
            confirmationStatusStrategy[item.status](temp, item.genre);
        });

        mapToModel(model, temp);
        
        return model;
    },

    mapToModel = function (model, temp) {
        model.accepted = temp.accepted.count;
        model.declined = temp.declined;
        model.unconfirmed = temp.unconfirmed;
        model.acceptedAdults = getGenreCount(temp.accepted.genres, 0);      // 0 means adult in enumeration
        model.acceptedChild = getGenreCount(temp.accepted.genres, 1);       // 1 means child in enumeration
    },

    getGenreCount = function (genres, type) { 
        return $.grep(genres, function (n) {
            return n === type;
        }).length;
    };

    addToUnconfirmed = function (model) {
        model.unconfirmed += 1;
    },

    addToAccepted = function (model, genre) {
        model.accepted.count += 1;
        model.accepted.genres.push(genre);                                  // collect genres of accepted persons
    },

    addToDeclined = function (model) {
        model.declined += 1;
    },

    confirmationStatusStrategy = {
        0: addToUnconfirmed,
        1: addToAccepted,
        2: addToDeclined
    },

    getModel = function () {                                                // this is empty model
        return {
            accepted: 0,
            acceptedAdults: 0,
            acceptedChild: 0,
            declined: 0,
            unconfirmed: 0
        }
    },

    populate = function (gid, members) {
        var list = {};
        $.each(members, function (i, m) {
            list[m.Id] = { genre: m.Genre, status: m.Status };              // id is needed for detect items while moving between groups
        });

        elements[gid] = list;
    },

    move = function (gidFrom, gidTo, ids) {                                 // ids are person ids moved between groups
        $.each(ids, function (i, id) {
            var value = parseInt(id);

            var itemToMove = elements[gidFrom][value];

            if (elements[gidTo] === undefined)                              // if group is empty and we move item we have to create element before moving it
                elements[gidTo] = {};

            elements[gidTo][value] = itemToMove;

            delete elements[gidFrom][value];
        });
    },
    
    remove = function (gid) {
        if (elements[gid] != null)
            delete elements[gid];                                           // delete from dictionary
    };

    return {
        populate: populate,
        remove: remove,
        move: move,
        getMembersInfo: getMembersInfo
    };
}();