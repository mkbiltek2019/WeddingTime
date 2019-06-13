
(function ($) {
    $.validator.addMethod('requiredif', function (value, element, params) {
        if ($(element).val() != '')
            return true;

        return propertyValidation(element, params);
    });

    var propertyValidation = function (element, params) {
        
        var obj;

        if (params.allowmultiple == 'true') {
            var no = $(element).attr('id').split('-')[0];           //split name to get the number
            obj = $('#' + no + '-' + params.dependentprop)
        }
        else {
            obj = $('#' + params.dependentprop);
        }

        var dependentPropVal = '';
        var dependentPropType = obj.prop('type').toLowerCase();

        if (dependentPropType == 'checkbox') {
            dependentPropVal = obj.attr('checked') ? 'true' : 'false';
        }
        else if (dependentPropType == 'radio') {
            dependentPropVal = $('input[@name="' + params.other + '"]:checked').val();          //does it work?
        }
        else {
            dependentPropVal = obj.val();
        }

        return params.comparison == 'isequalto' ? (dependentPropVal != params.value) : (dependentPropVal == params.value);
    };

    $.validator.unobtrusive.adapters.add('requiredif', ['dependentprop', 'comparison', 'value', 'allowmultiple'],
        function (options) {
            options.rules['requiredif'] = {
                dependentprop: options.params.dependentprop,
                comparison: options.params.comparison,
                value: options.params.value,
                allowmultiple: options.params.allowmultiple
            };
            options.messages['requiredif'] = options.message;
        }
    );
}(jQuery));

