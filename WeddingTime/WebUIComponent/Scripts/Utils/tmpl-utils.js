
var tmplUtils = function () {

    registerDefault = {
        names: null,
        location: null,
        callback: null,                                         // callback func is called when all templates are loaded correctly
        callbackData: null                                      // this is callback data passed to callback func
    },

    register = function (registerItem) {                        // register and compile templates
        var item = {};
        $.extend(item, registerDefault, registerItem);

        var deferreds = [];
        $.each(item.names, function (i, name) {
            if ($.templates[name])                              // means that tmpl is already registered
                return;

            var path = getPath(item.location, name);
            deferreds.push($.get(path).success(function (tmplData) {
                $.templates(name, tmplData);
            }));
        });

        $.when
         .apply($, deferreds)       // when all tmpls are loaded this list is empty, but call function is loaded correctly
         .done(function () {
             call(item.callback, item.callbackData);
         });
    },

    call = function (func, data) {
        if ($.isFunction(func)) {
            if ($.isPlainObject(data))
                func(data);
            else
                func();
        }
    },

    getPath = function (location, name) {
        return '/Views' + location + 'Templates/_' + getViewName(name) + '.tmpl.html';
    },

    getViewName = function (name) {
        return name.charAt(0).toUpperCase() + name.slice(1);
    };

    return {
        register: register
    };
}();