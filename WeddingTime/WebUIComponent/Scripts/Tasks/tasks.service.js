'use strict';
var tasksService = function () {

    var serviceBase = '/Tasks/',

    getTasks = function (options) {
        ajaxUtils.ajax(serviceBase + 'GetTasks', null, options);
    },

    prepareUpdate = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'PrepareUpdate', data, options);
    },

    updateState = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'UpdateState', data, options);
    },

    deleteTask = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'Delete', data, options);
    },

    getCard = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'GetCard', data, options);
    },

    registerTmpls = function (registerItem) {
        registerItem.location = serviceBase
        tmplUtils.register(registerItem);
    };

    return {
        getTasks: getTasks,
        prepareUpdate: prepareUpdate,
        updateState: updateState,
        deleteTask: deleteTask,
        getCard: getCard,
        registerTmpls: registerTmpls
    };
}();