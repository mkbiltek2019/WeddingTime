'use strict';
var guestsService = function () {
        
    var serviceBase = '/Guests/',

    getGroups = function (options) {
        ajaxUtils.ajax(serviceBase + 'GetGroups', null, options);
    },

    getGroup = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'GetGroup', data, options);
    },

    deleteGroup = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'DeleteGroup', data, options);
    },

    //----------------------------------------------------------------------------------------

    getPersons = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'GetPersons', data, options);
    },

    deletePersons = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'DeletePersons', data, options);
    },

    createInnerGroup = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'CreateInnerGroup', data, options);
    },

    addInnerGroupMember = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'AddInnerGroupMemeber', data, options);
    },
    
    detachInnerGroupMember = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'DetachInnerGroupMemeber', data, options);
    },
    
    prepareEdit = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'PrepareEdit', data, options);
    },

    updatePersonOrderNo = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'UpdatePersonOrderNo', data, options);
    },
    
    moveBetweenGroups = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'MoveBetweenGroups', data, options);
    },
    
    createNewPerson = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'CreateNewPerson', data, options);
    };

    return {
        getGroups: getGroups,
        getGroup: getGroup,
        deleteGroup: deleteGroup,
        getPersons: getPersons,
        createInnerGroup: createInnerGroup,
        deletePersons: deletePersons,
        addInnerGroupMember: addInnerGroupMember,
        detachInnerGroupMember: detachInnerGroupMember,
        prepareEdit: prepareEdit,
        updatePersonOrderNo: updatePersonOrderNo,
        moveBetweenGroups: moveBetweenGroups,
        createNewPerson: createNewPerson
    };
}();