'use strict';
var expensesService = function () {

    var serviceBase = '/Expenses/',

    getInfo = function (options) {
        ajaxUtils.ajax(serviceBase + 'GetExpensesInfo', null, options);
    },

	getExpenses = function (options) {
	    ajaxUtils.ajax(serviceBase + 'GetExpenses', null, options);
	},

    createNewExpense = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'CreateNewExpense', data, options);
    },

    updateExpenseOrderNo = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'UpdateExpenseOrderNo', data, options);
    },

    deleteExpenses = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'DeleteExpenses', data, options);
    },

    prepareSingleEdit = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'PrepareSingleEdit', data, options);
    },

    prepareEdit = function (data, options) {
        ajaxUtils.ajax(serviceBase + 'PrepareEdit', data, options);
    };

	return {
	    getInfo: getInfo,
	    getExpenses: getExpenses,
	    createNewExpense: createNewExpense,
	    updateExpenseOrderNo: updateExpenseOrderNo,
	    deleteExpenses: deleteExpenses,
	    prepareEdit: prepareEdit,
	    prepareSingleEdit: prepareSingleEdit
	};
}();