'use strict'
var autoLoginManager = function () {

    var init = function () {
        countdownManager.init('countdown', countdownFinished);
        countdownManager.start();
        enhanceAutoLoginBtn();        
    },
        
    countdownFinished = function () {
        $('#autoLogin').fadeOut(200, function () {
            $(this).remove();
            $('#normalLogin').fadeIn(300);
        });
    },
        
    enhanceAutoLoginBtn = function () {
        var btn = $('#btnAutoLogin');
        if (btn.length === 0)
            return;

        btn.click(function () {
            uiManager.blockUI(['.page-desc']);
            countdownManager.stop();
            document.getElementById('autoLoginForm').submit();            
        });        
    };

    return {
        init: init
    };
}();