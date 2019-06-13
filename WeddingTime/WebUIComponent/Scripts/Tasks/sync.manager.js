'use strict'
var syncManager = function () {             // created to sync task and card manager
                                            // the place where all templates are initialized
    var init = function () {
        initTemplates();
    },

    initTemplates = function () {        
        var registerObj = {
            names: ['task', 'cardItem', 'cardItemHidden', 'emailItem', 'linkItem', 'phoneItem', 'contactPersonItem', 'addressItem', 'card', 'cardHidden'],
            callback: onTmplRegistered
        };
        tasksService.registerTmpls(registerObj);
    },

    onTmplRegistered = function () {
        cardManager.init();
        tasksManager.init();
    };

    return {
        init: init
    };
}();