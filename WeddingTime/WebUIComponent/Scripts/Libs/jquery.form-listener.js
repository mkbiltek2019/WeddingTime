'use strict';
(function ($) {
    $.fn.formListener = function (options) {

        var settings = $.extend({
            selector: 'input:not(input[type=submit]):not(input[type=button]):not(input[type=hidden]):not(input[fl-omit]), textarea, select',
            events: 'change keyup input propertychange',
            dirtyClass: 'dirty',
            dynamicAreaMarksDirty: false,
            dynamicAreasSelector: '[data-fl-dynamic-area]',         // this data attr should be applied to area we want to monitor
            dynamicAreasIdentifier: 'id'                            // this is how we find unique item id inside dynamic area
        }, options);

        var initForm = function (form) {

            var processDynamicArea = function () {
                if (!settings.dynamicAreaMarksDirty)
                    return;
                var areas = form.find(settings.dynamicAreasSelector);
                $(areas).each(function () {
                    storeOriginalItems($(this));
                });
            };

            var fields = form.find(settings.selector);
            $(fields).each(function () { storeOriginalValue($(this)); });
            $(fields).unbind(settings.events, checkForm);
            $(fields).bind(settings.events, checkForm);

            processDynamicArea();            
            setDirtyStatus(form, false);
        };

        var rescan = function () {
            var form = $(this);
            var fields = form.find(settings.selector);
            $(fields).each(function () {
                var field = $(this);
                if (field.data('fl-orig-value') === undefined) {
                    storeOriginalValue(field);
                    field.bind(settings.events, checkForm);
                }
            });

            if (!settings.dynamicAreaMarksDirty)
                return;
            var areas = form.find(settings.dynamicAreasSelector);
            $(areas).each(function () {
                var area = $(this);
                var originalItems = area.data('fl-area-items');             // behaves like reference, if I add value to it it will be available through data attribute, don't need to rewrite it again
                area.children().each(function () {
                    var identifer = $(this).attr(settings.dynamicAreasIdentifier);
                    if ($.inArray(identifer, originalItems) == -1)
                        originalItems.push(identifer);
                });
            });
        };

        var storeOriginalValue = function (field) {
            field.data('fl-orig-value', getValue(field));
        };         

        var storeOriginalItems = function (area) {
            var items = [];            
            area.children().each(function () {
                items.push($(this).attr(settings.dynamicAreasIdentifier));
            });
            area.data('fl-area-items', items);
        };

        var getValue = function (field) {
            if (field.data('fl-ignore'))
                return null;

            var type = field.is('select') ? 'select' : field.attr('type'); 
            
            switch (type) {
                case 'checkbox':
                case 'radio':
                    return field.is(':checked');
                case 'select':
                    var value = '';
                    field.find('option').each(function () {
                        var option = $(this);
                        if (option.is(':selected')) {
                            value = option.val();                       // in my case only one option can be selected       
                            return false;
                        }
                    });
                    return value;
                default:
                    return field.val().toLowerCase();
            }
        };

        var checkForm = function (e) {

            var isFieldDirty = function (field) {
                var originalValue = field.data('fl-orig-value');
                if (originalValue === undefined)
                    return false;                
                return originalValue != getValue(field);
            };

            var hasDynamicAreaContentChanged = function(areas) {
                var differs = false;
                
                $(areas).each(function () {
                    var items = [];
                    $(this).children().each(function () {
                        items.push($(this).attr(settings.dynamicAreasIdentifier));
                    });
                    var originalItems = $(this).data('fl-area-items');
                    if ($(originalItems).not(items).length != 0 || $(items).not(originalItems).length != 0) {
                        differs = true;
                        return false;                                           // break
                    }
                });
                return differs;
            };

            var item = $(e.target);
            var form = item.is('form') ? item : item.closest('form');           // if we trigger event e.target can be directly a form

            if (isFieldDirty(item)) {
                setDirtyStatus(form, true);
                return;
            }

            if (settings.dynamicAreaMarksDirty) {
                var areas = form.find(settings.dynamicAreasSelector);
                if (hasDynamicAreaContentChanged(areas)) {
                    setDirtyStatus(form, true);
                    return;
                }
            }

            var fields = form.find(settings.selector);
            var isDirty = false;    
            fields.each(function () {                           // if we don't iterate through all fields we can get into situation where we change back one value to it's orginal value and dispate of the fact other fields are dirty we check form as clean
                if (isFieldDirty($(this))) {
                    isDirty = true;
                    return false;                               // break
                }
            });

            setDirtyStatus(form, isDirty);
        };

        var setDirtyStatus = function (form, isDirty) {
            var changed = isDirty != form.hasClass(settings.dirtyClass);
            form.toggleClass(settings.dirtyClass, isDirty);

            if (changed) {
                if (isDirty) form.trigger('unclean.formListener', [form]);
                if (!isDirty) form.trigger('clean.formListener', [form]);
                form.trigger('change.formListener', [form]);
            }
        };

        var detach = function (form) {
            var fields = form.find(settings.selector);
            $(fields).unbind(settings.events, checkForm);           // unbind all field events becasue there is no way to get back to clean state
            form.unbind('initialize.formListener');
            form.unbind('rescan.formListener');
            form.unbind('check.formListener');
            form.unbind('detach.formListener');
            form.unbind('dirty.formListener');
            form.unbind('clear.formListener');
        };

        var reinitialize = function () {
            initForm($(this));
        }

        this.each(function () {
            var form = $(this);

            if (!form.is('form'))
                return;

            form.bind('initialize.formListener', reinitialize);
            form.bind('rescan.formListener', rescan);
            form.bind('check.formListener', checkForm);
            form.bind('detach.formListener', function () { detach(form); });
            form.bind('dirty.formListener', function () { setDirtyStatus(form, true); });
            form.bind('clear.formListener', function () { setDirtyStatus(form, false); });
            //initForm(form);           // don't really need it here since I always have to invoke reinitialize event on dialog shown
        });

        return this;                    // for providing fluent api
    };
})(jQuery);
