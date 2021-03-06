function formHiddenFieldsInitialization(id) {
    $('[data-sf-key="' + id + '"]').each(function (i, formIdentifierElement) {
        var formContainer = $(formIdentifierElement).closest('[data-sf-role="form-container"]');
        if ((!formContainer || formContainer.length === 0) && window.location.href.indexOf("/Preview") !== -1) {
            formContainer = $(formIdentifierElement).closest("#PublicWrapper");
        }

        if (formContainer) {
            var hiddenFieldsInputValue = $(formContainer).find('[data-sf-role="form-rules-hidden-fields"]').val();
            if (hiddenFieldsInputValue && hiddenFieldsInputValue !== '') {
                var hiddenFields = hiddenFieldsInputValue.split(',');

                // for cases when input is added in script template, ex. 'File Upload' field
                $(formContainer).find('script[data-sf-role$="-template"]').each(function (i, script) {
                    var tempDiv = $('<div>').html($(script).html());
                    $.each(hiddenFields, function (index, fieldName) {
                        var templateField = $(tempDiv).find('[name="' + fieldName + '"]');
                        if (templateField && templateField.length > 0) {
                            templateField.attr('disabled', 'disabled');
                            $(script).html($(tempDiv).html());
                            return false;
                        }
                    });
                });

                $.each(hiddenFields, function (index, fieldControlId) {
                    var scriptWrapper = $(formContainer).find('script[data-sf-role="start_field_' + fieldControlId + '"]');
                    if (scriptWrapper && scriptWrapper.length > 0) {
                        var fieldName = $(scriptWrapper)[0].getAttribute('data-sf-role-field-name');
                        $(scriptWrapper).nextUntil('script[data-sf-role="end_field_' + fieldControlId + '"]').each(function (i, element) {
                            var inputField = $(element).find('[name="' + fieldName + '"]');
                            if (inputField && inputField.length > 0) {
                                inputField.attr('disabled', 'disabled');
                            }

                            $(element).hide();
                        });
                    }
                });
            }

            if (typeof $.fn.processFormRules === 'function') {
                formContainer.processFormRules();
            }

            var wrapper = formContainer.closest('[data-sf-role="form-visibility-wrapper"]');
            $(wrapper).children().insertBefore(wrapper);
            $(wrapper).remove();
        }
    });
}