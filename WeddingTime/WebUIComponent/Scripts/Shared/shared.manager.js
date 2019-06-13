'use strict';

var sharedManager = function () {

    var enhanceNav = function () {        
        enhanceUserOptionsBtn();
        enhanceMenuBtn();
        enhanceLogOutBtn();
    },

    selectMenuItem = function () {
        var url = window.location.pathname;
        if (/\/Index$/i.test(url))                                                  // if url ends with /Index, remove it from url
            url = url.replace('/Index', '');

        var nav = $('ul.nav a[href="' + url + '"]').parent();                       // this is li element

        if (nav.parent().hasClass('navbar-page'))                                   // this is check on ul element
            nav.addClass('active-menu');
        else
            nav.addClass('active-login');
    },

    enhanceUserOptionsBtn = function () {
        $('#btnNavLogin').click(function () {
            var btn = $(this);
            btn.toggleClass('icon-user-active');
        });
    },

    enhanceMenuBtn = function () {
        $('#btnNavMenu').click(function () {
            var btn = $(this);
            toogleNavIcon(btn, 'icon-menu-inactive icon-menu-active');
        });
    },

    enhanceLogOutBtn = function () {
        var btn = $('#btnLogOut');
        if (btn.length == 0)
            return;

        btn.click(function () {
            document.getElementById('logoutForm').submit();
        });        
    },

    toogleNavIcon = function (btn, className) {
        btn.find('span:not(.sr-only)').toggleClass(className);
    };

    return {
        enhanceNav: enhanceNav,
        selectMenuItem: selectMenuItem
    };
}();