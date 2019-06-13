'use strict'
var bEnums = function () {

    var newlywedsType = {
        0: 'bride',
        1: 'groom'
    },

    takenBy = {
        'bride': 0,
        'groom': 1
    },

    assignmentType = {
        Person: 0,
        Newlyweds: 1
    };

    return {
        assignmentType: assignmentType,
        takenBy: takenBy,
        newlywedsType: newlywedsType
    };
}();