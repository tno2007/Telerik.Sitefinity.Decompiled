///<reference path="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.6.4.js" />
/**
* @name Sitefinity validation
*
* @description
* This types provides functionality for validating inputs
* 
*/
function sitefinityValidation() {

    this.validationDefinitions = null;

}

sitefinityValidation.prototype = {

    /* This function takes html element as an argument and validates all the inputs inside of it.
    * If all the inputs are valid it returns true. If at least one input is not valid it will
    * return false.
    */
    validateContainer: function (container) {
        if (container != null) {
            var isValid = true;
            var elementsToValidate = this.getElementsToValidate(container);
            for (var i = 0; i < elementsToValidate.length; i++) {
                var validationDefinition = elementsToValidate[i];
                if ($(validationDefinition.element).is(":visible")) {
                    // Validate element if it is visible
                    if (this.validateElement(validationDefinition) == false) {
                        isValid = false;
                        this.displayErrorMessage(validationDefinition);
                    }
                }
            }
            this.validationDefinitions = null;
            return isValid;
        }
    },

    /* Validates a single element. If the element is valid it returns true; otherwise it will
    * return false.
    */
    validateElement: function (validationDefinition) {
        this.removeDisplayErrorMessages(validationDefinition);
        switch (validationDefinition.validationType) {
            case "required":
                return $(validationDefinition.element).val().length > 0;
            case "regex":
                var regEx = new RegExp(validationDefinition.pattern);
                return regEx.test($(validationDefinition.element).val());
            case "length":
                var allowedCharacters = parseInt(validationDefinition.maxAllowedCharacters);
                return $(validationDefinition.element).val().length < allowedCharacters;
            default:
                alert("Validation type '" + validationDefinition.validationType + "' is not supported.");
        }
        return false;
    },

    /* Displays the error message below the element, when element is not valid. */
    displayErrorMessage: function (validationDefinition) {
        //this.removeDisplayErrorMessages(validationDefinition);
        var errorMessage = document.createElement("div");
        $(errorMessage).addClass("sfError");
        $(errorMessage).html(validationDefinition.violationMessage);
        $(validationDefinition.element).after(errorMessage);

        if (validationDefinition.errorElements == null) {
            validationDefinition.errorElements = [];
        }
        validationDefinition.errorElements.push(errorMessage);
    },

    /* Removes all the display error messages associated with the element */
    removeDisplayErrorMessages: function (validationDefinition) {
        if (validationDefinition.errorElements != null) {
            for (var err = 0; err < validationDefinition.errorElements.length; err++) {
                $(validationDefinition.errorElements[err]).detach();
            }
            validationDefinition.errorElements = [];
        }
    },

    /* Returns an array of elements to validate */
    getElementsToValidate: function (container) {

        var val = this;

        if (this.validationDefinitions == null) {
            this.validationDefinitions = [];
        }

        /* get the elements for required validation */
        $(container).find("[required]").each(function () {
            var validationDefinition = val.getRequiredValidationDefinition(this);
            if (validationDefinition) {
                val.validationDefinitions.push(validationDefinition);
            }
        });

        /* get the elements for regular expression validation */
        $(container).find("[pattern]").each(function () {
            var validationDefinition = val.getPatternValidationDefinition(this);
            if (validationDefinition) {
                val.validationDefinitions.push(validationDefinition);
            }
        });

        /* get the elements for max length validation */
        $(container).find("[max-length]").each(function () {
            var validationDefinition = val.getMaxFieldLengthValidationDefinition(this);
            if (validationDefinition) {
                val.validationDefinitions.push(validationDefinition);
            }
        });

        return this.validationDefinitions;
    },

    getElementDefinitions: function (element) {
        var definitions = [];

        var requiredDefinition = this.getRequiredValidationDefinition(element);
        if (requiredDefinition != null) {
            definitions.push(requiredDefinition);
        }

        var patternDefinition = this.getPatternValidationDefinition(element);
        if (patternDefinition != null) {
            definitions.push(patternDefinition);
        }

        var maxLengthDefinition = this.getMaxFieldLengthValidationDefinition(element);
        if (maxLengthDefinition != null) {
            definitions.push(maxLengthDefinition);
        }

        return definitions;
    },

    getRequiredValidationDefinition: function (element) {
        if ($(element).attr("required") !== undefined) {
            var requiredErrorMessage = null;
            var fieldName = $(element).attr("error-field-name");
            var requiredViolationMessage = $(element).attr("required-violation-message");
            if (requiredViolationMessage !== undefined && fieldName !== undefined) {
                requiredErrorMessage = fieldName + ' ' + requiredViolationMessage;
            } else {
                requiredErrorMessage = "This field cannot be empty.";
            }
            var validationDefinition = {
                element: $(element),
                validationType: "required",
                violationMessage: requiredErrorMessage
            };
            return validationDefinition;
        } else {
            return null;
        }
    },

    getPatternValidationDefinition: function (element) {
        if ($(element).attr("pattern") !== undefined) {
            var errorMessage = null;
            var fieldName = $(element).attr("error-field-name");
            var patternViolationMessage = $(element).attr("pattern-violation-message");
            if (patternViolationMessage !== undefined && fieldName !== undefined) {
                errorMessage = fieldName + ' ' + patternViolationMessage;
            } else {
                errorMessage = "The value is not in a valid format.";
            }
            var validationDefinition = {
                element: $(element),
                validationType: "regex",
                pattern: $(element).attr("pattern"),
                violationMessage: errorMessage
            };
            return validationDefinition;
        } else {
            return null;
        }
    },

    getMaxFieldLengthValidationDefinition: function (element) {
        if ($(element).attr("max-length") !== undefined) {
            var errorMessage = null;
            var allowedCharacters = $(element).attr("max-length");
            var fieldName = $(element).attr("error-field-name");
            if (allowedCharacters !== undefined && fieldName !== undefined) {
                errorMessage = fieldName + ' is longer than the allowed ' + allowedCharacters + ' characters. Make it shorter.';
            } else {
                errorMessage = "Entered value is longer than allowed.";
            }
            var validationDefinition = {
                element: $(element),
                validationType: "length",
                maxAllowedCharacters: allowedCharacters,
                violationMessage: errorMessage
            };
            return validationDefinition;
        } else {
            return null;
        }
    },

    // NOTE: Replace the displaying of the message with the displayMessage function, so it can be used 
    // for any types of integer inputd 
    validateMinMaxInputs: function (firstFieldId, secondFieldId) {
        //var minInputString = parseInt($("#typeFieldEditor_minLength").val());
        //var maxInputString = parseInt($("#typeFieldEditor_maxLength").val());
        var minInputString = parseInt($(firstFieldId).val());
        var maxInputString = parseInt($(secondFieldId).val());
        if (minInputString != "" && maxInputString != "") {
            var minInput = parseInt($(firstFieldId).val());
            var maxInput = parseInt($(secondFieldId).val());
            if (minInput > maxInput) {
                var errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html("Max should be bigger than Min");
                $("#typeFieldEditor_maxLength").after(errorMessage);
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }
    },

    validateMinMaxRanges: function (firstFieldId, secondFieldId) {
        //var minInputString = $("#typeFieldEditor_minRange").val();
        //var maxInputString = $("#typeFieldEditor_maxRange").val();
        var minInputString = $(firstFieldId).val();
        var maxInputString = $(secondFieldId).val();
        if (minInputString != "" && maxInputString != "") {
            var minInput = parseFloat(($(firstFieldId).val()).replace(/,/g, ''));
            var maxInput = parseFloat(($(secondFieldId).val()).replace(/,/g, ''));
            if (minInput > maxInput) {
                var errorMessage = document.createElement("div");
                $(errorMessage).addClass("sfError");
                $(errorMessage).html("Max range should be bigger than Min range");
                $("#typeFieldEditor_maxRange").after(errorMessage);
                return false;
            }
            else {
                return true;
            }
        }
        else {
            return true;
        }

    }
};