'use strict';
var ballroomService = function () {

    var serviceBase = '/Ballroom/',

	getPersons = function (options) {
	    return ajaxUtils.ajax(serviceBase + 'GetPersons', null, options);
	},

    getLayout = function (options) {
        return ajaxUtils.ajax(serviceBase + 'GetLayout', null, options);
    },

    saveLayout = function (data, options) {
        return ajaxUtils.ajax(serviceBase + 'SaveLayout', data, options);
    },

    registerTmpls = function (registerItem) {
        registerItem.location = serviceBase
        tmplUtils.register(registerItem);
    };

	return {
	    getPersons: getPersons,
	    getLayout: getLayout,
	    saveLayout: saveLayout,
	    registerTmpls: registerTmpls
	};
}();