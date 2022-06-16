Type.registerNamespace("Telerik.Sitefinity.Web.UI.Validation");

Telerik.Sitefinity.Web.UI.Validation.Validator = function (validatorDefinition) {
    this._expectedFormat = null;
    this._maxLength = null;
    this._minLength = null;
    this._maxValue = null;
    this._minValue = null;
    this._regularExpression = null;
    this._required = null;
    this._alphaNumericViolationMessage = null;
    this._currencyViolationMessage = null;
    this._emailAddressViolationMessage = null;
    this._integerViolationMessage = null;
    this._internetUrlViolationMessage = null;
    this._maxLengthViolationMessage = null;
    this._maxValueViolationMessage = null;
    this._messageCssClass = null;
    this._messageTagName = null;
    this._minLengthViolationMessage = null;
    this._minValueViolationMessage = null;
    this._nonAlphaNumericViolationMessage = null;
    this._numericViolationMessage = null;
    this._percentageViolationMessage = null;
    this._regularExpressionViolationMessage = null;
    this._requiredViolationMessage = null;
    this._uSocialSecurityNumberViolationMessage = null;
    this._uSZipCodeViolationMessage = null;
    this._validateIfInvisible = null;
    this._comparingValidatorDefinitions = null;
    this._regularExpressionSeparator = null;

    this._violationMessages = [];
    this._violationMessageElement = null;
    this._validatorDefinition = validatorDefinition;

    this.alphaNumericRegexPattern = /[-_a-zA-Z0-9]+$/;
    this.currencyRegexPattern = /[+-]?[0-9]{1,3}(?:[0-9]*(?:[.,][0-9]{2})?|(?:,[0-9]{3})*(?:\.[0-9]{2})?|(?:\.[0-9]{3})*(?:,[0-9]{2})?)/;

    // Consider using <see cref="Telerik.Sitefinity.Constants.EmailAddressRegexPattern"/>
    this.emailAddressRegexPattern = /^[a-zA-Z0-9._%+'-]+@(?:[a-zA-Z0-9-]+\.)+[a-zA-Z]{2,63}$/i;
    this.integerRegexPattern = /^[-+]?\d+$/;
    this.internetUrlRegexPattern = /(?:(?:https?|ftp|file):\/\/|www\.|ftp\.)[-A-Za-z0-9+&@#\/%=~_|$?!:,.]*[A-Za-z0-9+&@#\/%=~_|$]/;
    this.nonAlphaNumericRegexPattern = /[^-_a-zA-Z0-9]+/;
    this.numericRegexPattern = /^[-+]?[0-9]+((,|\.)[0-9]+)?/;
    this.percentRegexPattern = /100$|^\s*(\d{0,2})((\.|\,)(\d*))?\s*\%?\s*/;
    this.uSSocialSecurityRegexPattern = /(?!000)(?!666)(?:[0-6]\d{2}|7(?:[0-356]\d|7[012]))[- ](?!00)\d{2}[- ](?!0000)\d{4}/;
    this.uSZipCodeRegexPattern = /[0-9]{5}(?:-[0-9]{4})?/;

    this._violationMessageElementWasCreated = false;

    Telerik.Sitefinity.Web.UI.Validation.Validator.initializeBase(this);
    this.initialize();
}

Telerik.Sitefinity.Web.UI.Validation.Validator.prototype =
{
    initialize: function () {
        this.configure(this._validatorDefinition);
        delete this._validatorDefinition;
        Telerik.Sitefinity.Web.UI.Validation.Validator.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Validation.Validator.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // Determines whether the specified value is valid.
    validate: function (value) {
        var isValid = true;
        this.set_violationMessages([]);
        var validatingArgs = this._onValidatingHandler(value);
        isValid = validatingArgs.get_isValid();

        if (!validatingArgs.get_cancel())
            isValid = this._evaluateIsValid(value);

        this._onValidatedHandler(isValid, value);
        return isValid;
    },

    // Configures the validator instance using validatorDefinition
    configure: function (definition) {
        for (var validatorDefPropertyName in definition) {
            this._setPropertyIfChanged(validatorDefPropertyName, definition[validatorDefPropertyName]);
        }
    },
    /* -------------------- events -------------------- */
    add_onValidating: function (delegate) {
        this.get_events().addHandler('onValidating', delegate);
    },

    remove_onValidating: function (delegate) {
        this.get_events().removeHandler('onValidating', delegate);
    },

    add_onValidated: function (delegate) {
        this.get_events().addHandler('onValidated', delegate);
    },

    remove_onValidated: function (delegate) {
        this.get_events().removeHandler('onValidated', delegate);
    },
    /* -------------------- event handlers ------------ */

    _onValidatingHandler: function (value) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs(value);
        var h = this.get_events().getHandler('onValidating');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    _onValidatedHandler: function (isValid, value) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs(isValid, value);
        var h = this.get_events().getHandler('onValidated');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    /* -------------------- private methods ----------- */

    _evaluateIsValid: function (value) {
        if (typeof value === typeof "")
            value = value.trim();

        if (this._validateRequired(value)) {
            var isValid = true;
            isValid = this._validateRange(value) && isValid;
            isValid = this._validateRegex(value) && isValid;
            isValid = this._validateComparisons(value) && isValid;

            return isValid;
        }

        return false;
    },
    _validateRequired: function (value) {
        if (this.get_required()) {

            if (!value || value === "" || value === false || value === '00000000-0000-0000-0000-000000000000') {
                this.get_violationMessages().push(this.get_requiredViolationMessage());
                return false;
            }
            else if (Array.prototype.isPrototypeOf(value)) {
                if (value.length == 0) {
                    this.get_violationMessages().push(this.get_requiredViolationMessage());
                    return false;
                }
            }
        }
        return true;
    },

    _validateRange: function (value) {
        if (value !== null && value !== undefined) {
            if (!isNaN(value)  && (this.get_minValue() != null || this.get_maxValue() != null)) {
                return this._validateNumericDateRange(value);
            }
            if (String.isInstanceOfType(value) || (typeof value) === (typeof "")) {
                return this._validateStringRange(value);
            }
            if (!isNaN(value) && Array.isInstanceOfType(value)) {
                return this._validateArrayRange(value);
            }
        }
        return true;
    },

    _validateStringRange: function (value) {
        if (this.get_minLength() > 0) {
            var isMinLengthValid = value.length >= this.get_minLength();
            if (!isMinLengthValid) {
                this.get_violationMessages().push(this.get_minLengthViolationMessage());
                return false;
            }
        }
        if (this.get_maxLength() > 0) {
            var isMaxLengthValid = value.length <= this.get_maxLength();
            if (!isMaxLengthValid) {
                this.get_violationMessages().push(this.get_maxLengthViolationMessage());
                return false;
            }
        }
        return true;
    },

    _validateNumericDateRange: function (value) {

        if (this.get_minValue() != null) {
            var isMinValueValid = value >= this.get_minValue();
            if (!isMinValueValid) {
                this.get_violationMessages().push(this.get_minValueViolationMessage());
                return false;
            }
        }

        if (this.get_maxValue() != null) {
            var isMaxValueValid = value <= this.get_maxValue();
            if (!isMaxValueValid) {
                this.get_violationMessages().push(this.get_maxValueViolationMessage());
                return false;
            }
        }

        return true;
    },

    _validateArrayRange: function (value) {
        if (value.length || value.length === 0) {
            var isMaxLengthValid = true;
            var isMinLengthValid = true;
            if (this.get_maxLength() > 0) {
                isMaxLengthValid = this.get_maxLength() <= value.length;
            }
            if (this.get_minLength() > 0) {
                isMinLengthValid = this.get_minLength() >= value.length;
            }
            if (isMinLengthValid && isMaxLengthValid) {
                return true;
            }
            this.get_violationMessages().push(this.get_maxLengthViolationMessage());
            return false;
        }

        return false;
    },

    _validateRegex: function (value) {
        if (String.isInstanceOfType(value) || (typeof value) === (typeof "")) {
            return this._validateStringRegex(value);
        }
        return true;
    },

    _validateStringRegex: function (value) {
        var regex = this.get_regularExpression();
        if (regex) {
            return this._validateCustomRegex(value, regex);
        }
        var expectedFormat = this.get_expectedFormat();
        if (expectedFormat > 0)
            return this._validateExpectedFormat(value, expectedFormat);
        return true;
    },

    _validateExpectedFormat: function (value, expectedFormat) {
        var isValid = true;
        switch (expectedFormat) {
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.None:
                return true;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.AlphaNumeric:
                isValid = this.alphaNumericRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_alphaNumericViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Currency:
                isValid = this.currencyRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_currencyViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.EmailAddress:               
                isValid = this.emailAddressRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_emailAddressViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Integer:
                isValid = this.integerRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_integerViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.InternetUrl:
                isValid = this.internetUrlRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_internetUrlViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.NonAlphaNumeric:
                isValid = this.nonAlphaNumericRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_nonAlphaNumericViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Numeric:
                isValid = this.numericRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_numericViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.Percentage:
                isValid = this.percentRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_percentageViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.USSocialSecurityNumber:
                isValid = this.uSSocialSecurityRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_uSocialSecurityNumberViolationMessage());
                break;
            case Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.USZipCode:
                isValid = this.uSZipCodeRegexPattern.test(value);
                if (!isValid)
                    this.get_violationMessages().push(this.get_uSZipCodeViolationMessage());
                break;
        }
        return isValid;
    },

    _validateCustomRegex: function (value, regex) {
        var regexPattern = new XRegExp(regex, "g"); // new RegExp(regex);
        var isRegexValid = false;
        if (this.get_regularExpressionSeparator()) {
            isRegexValid = true;
            if (value) {
                regexPattern = new XRegExp(regex);
                var sepRegex = new RegExp(this.get_regularExpressionSeparator());
                var splitArray = value.split(sepRegex);
                for (var i = 0; i < splitArray.length; i++) {
                    var currentPart = splitArray[i];
                    isRegexValid = regexPattern.test(currentPart);
                    if (isRegexValid == false) break;
                }
            }
        } else {
            isRegexValid = regexPattern.test(value);
        }
        if (!isRegexValid) {
            var msg = this.get_regularExpressionViolationMessage();
            if (msg == null) msg = this.get_regularExpression();
            this.get_violationMessages().push(msg);
            return false;
        }
        return true;

    },

    _validateComparisons: function (value) {
        var isValid = true;
        if (value !== null && value !== undefined && this.get_comparingValidatorDefinitions()) {
            var comparisonsCount = this.get_comparingValidatorDefinitions().length;
            while (comparisonsCount--) {
                if (this.get_comparingValidatorDefinitions()[comparisonsCount])
                    isValid = this._validateComparison(this.get_comparingValidatorDefinitions()[comparisonsCount], value) && isValid;
            }
        }
        return isValid;
    },

    _validateComparison: function (comparison, value) {
        var validationOperator = comparison['Operator'];
        var controlToCompareId = comparison['ControlToCompare'];
        var violationMessage = comparison['ValidationViolationMessage'];
        var validationDataType = comparison['ValidationDataType'];

        value = this._getValueAsType(value, validationDataType);

        var isValid = true;
        if (controlToCompareId && validationOperator >= 0) {
            var valueToCompareTo = this._get_controlValue(controlToCompareId);
            valueToCompareTo = this._getValueAsType(valueToCompareTo, validationDataType);
            isValid = this._compare(value, validationOperator, valueToCompareTo);
            if (!isValid)
                this.get_violationMessages().push(violationMessage);
        }
        return isValid;
    },

    _getValueAsType: function (value, type) {
        switch (type) {
            case "Number": return Number(value);
        }
        return value;
    },

    _get_controlValue: function (controlToCompareId) {
        var clientID = $get_clientId(controlToCompareId);
        if (!clientID)
            throw new "The client Id could not be resolved."

        var componentToCompareTo = $find(clientID);
        if (componentToCompareTo) {
            return this._get_componentValue(componentToCompareTo);
        }
        else {
            var controlToCompareTo = $get(clientID);
            if (controlToCompareTo) {
                return this._get_controlValue(controlToCompareTo);
            }
        }
        throw new "There is no control with id: " + controlID + " to use for comparison validation."
    },

    _get_componentValue: function (componentToCompareTo) {
        if (componentToCompareTo.get_value) {
            // TODO: just because a value can be parsed as a number doesn't necessarily mean that it should be.
            return componentToCompareTo.get_value();
        }
        if (componentToCompareTo.get_element)
            return this._get_elementValue(componentToCompareTo.get_element());
        throw new "The value of the control could not be resolved.";
    },

    _get_elementValue: function (controlToCompareTo) {
        if (controlToCompareTo.value) {
            return controlToCompareTo.value;
        }
        throw new "The value of the control could not be resolved.";
    },

    _compare: function (firstValue, validationOperator, secondValue) {
        if (typeof firstValue != typeof secondValue)
            return false;
        var validationCompareOperator = Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationCompareOperator;
        switch (validationOperator) {
            case validationCompareOperator.Equal:
                return firstValue === secondValue;
            case validationCompareOperator.NotEqual:
                return firstValue != secondValue;
            case validationCompareOperator.GreaterThan:
                return firstValue > secondValue;
            case validationCompareOperator.GreaterThanEqual:
                return firstValue >= secondValue;
            case validationCompareOperator.LessThan:
                return firstValue < secondValue;
            case validationCompareOperator.LessThanEqual:
                return firstValue <= secondValue;
            default:
                throw "This operator is not supported";
        }
    },

    _setPropertyIfChanged: function () {
        var propertyName = arguments[0];
        propertyName = propertyName.charAt(0).toLowerCase() + propertyName.slice(1);
        var value = arguments.length == 3 ? arguments[2] : arguments[1];
        var fieldName = arguments.length == 3 ? arguments[1] : null;

        if (!fieldName)
            fieldName = "_" + propertyName;

        if (!propertyName || !this.hasOwnProperty(fieldName)) return;

        var currentValue = this[fieldName];
        if (currentValue != value) {
            this[fieldName] = value;
            this.raisePropertyChanged(propertyName);
        }
    },

    _createViolationMessageElement: function () {
        var errorMessageElement = document.createElement(this.get_messageTagName());
        jQuery(errorMessageElement).addClass(this.get_messageCssClass());
        return errorMessageElement;
    },
    /* -------------------- properties ---------------- */

    //Gets the violation message dom element
    get_violationMessageElement: function () {
        if (!this._violationMessageElement)
            this._violationMessageElement = this._createViolationMessageElement();

        this._violationMessageElement.innerText = '';
        var violationMessagesCount = this.get_violationMessages().length;
        while (violationMessagesCount--) {
            //TODO don't concat messages but use array of created elements and push messages in their innerHTML;
            var message = ' ' + this.get_violationMessages()[violationMessagesCount];
            this._violationMessageElement.innerText += message;
        }
        this._violationMessageElement.innerText = this._violationMessageElement.innerText.trim();
        return this._violationMessageElement;
    },

    // Gets the violation message.
    get_violationMessages: function () {
        return this._violationMessages;
    },
    // Gets the violation message.
    set_violationMessages: function (value) {
        this._violationMessages = value;
    },

    // Sets an expected format of the validated value.
    set_expectedFormat: function (value) {
        this._setPropertyIfChanged("expectedFormat", value);
    },
    // Gets an expected format of the validated value.
    get_expectedFormat: function () {
        return this._expectedFormat;
    },
    // Sets the maximum length.
    set_maxLength: function (value) {
        this._setPropertyIfChanged("maxLength", value);
    },
    // Gets the maximum length.
    get_maxLength: function () {
        return this._maxLength;
    },
    // Sets the minimum length.
    set_minLength: function (value) {
        this._setPropertyIfChanged("minLength", value);
    },
    // Gets the minimum length.
    get_minLength: function () {
        return this._minLength;
    },
    // Sets the maximum value to use when validating data.
    set_maxValue: function (value) {
        this._setPropertyIfChanged("maxValue", value);
    },
    // Gets the maximum value to use when validating data.
    get_maxValue: function () {
        return this._maxValue;
    },
    // Sets the minimum value to use when validating data.
    set_minValue: function (value) {
        this._setPropertyIfChanged("minValue", value);
    },
    // Gets the minimum value to use when validating data.
    get_minValue: function () {
        return this._minValue;
    },
    // Sets the regular expression to use when evaluating string.
    set_regularExpression: function (value) {
        this._setPropertyIfChanged("regularExpression", value);
    },
    // Gets the regular expression to use when evaluating string.
    get_regularExpression: function () {
        return this._regularExpression;
    },
    // Sets whether the validated data must have a value.
    set_required: function (value) {
        this._setPropertyIfChanged("required", value);
    },
    // Gets whether the validated data must have a value.
    get_required: function () {
        return this._required;
    },
    // Sets the message shown when alpha numeric validation has failed.
    set_alphaNumericViolationMessage: function (value) {
        this._setPropertyIfChanged("alphaNumericViolationMessage", value);
    },
    // Gets the message shown when alpha numeric validation has failed.
    get_alphaNumericViolationMessage: function () {
        return this._alphaNumericViolationMessage;
    },
    // Sets the message shown when currency validation has failed.
    set_currencyViolationMessage: function (value) {
        this._setPropertyIfChanged("currencyViolationMessage", value);
    },
    // Gets the message shown when currency validation has failed.
    get_currencyViolationMessage: function () {
        return this._currencyViolationMessage;
    },
    // Sets the message shown when email address validation has failed.
    set_emailAddressViolationMessage: function (value) {
        this._setPropertyIfChanged("emailAddressViolationMessage", value);
    },
    // Gets the message shown when email address validation has failed.
    get_emailAddressViolationMessage: function () {
        return this._emailAddressViolationMessage;
    },
    // Sets the message shown when integer validation has failed.
    set_integerViolationMessage: function (value) {
        this._setPropertyIfChanged("integerViolationMessage", value);
    },
    // Gets the message shown when integer validation has failed.
    get_integerViolationMessage: function () {
        return this._integerViolationMessage;
    },
    // Sets the message shown when internet url validation has failed.
    set_internetUrlViolationMessage: function (value) {
        this._setPropertyIfChanged("internetUrlViolationMessage", value);
    },
    // Gets the message shown when internet url validation has failed.
    get_internetUrlViolationMessage: function () {
        return this._internetUrlViolationMessage;
    },
    // Sets the message shown when max length validation has failed.
    set_maxLengthViolationMessage: function (value) {
        this._setPropertyIfChanged("maxLengthViolationMessage", value);
    },
    // Gets the message shown when max length validation has failed.
    get_maxLengthViolationMessage: function () {
        return this._maxLengthViolationMessage;
    },
    // Sets the message shown when max value validation has failed.
    set_maxValueViolationMessage: function (value) {
        this._setPropertyIfChanged("maxValueViolationMessage", value);
    },
    // Gets the message shown when max value validation has failed.
    get_maxValueViolationMessage: function () {
        return this._maxValueViolationMessage;
    },
    // Sets the violation message CSS class.
    set_messageCssClass: function (value) {
        this._setPropertyIfChanged("messageCssClass", value);
    },
    // Gets the violation message CSS class.
    get_messageCssClass: function () {
        return this._messageCssClass;
    },
    // Sets the name of the violation message tag.
    set_messageTagName: function (value) {
        this._setPropertyIfChanged("messageTagName", value);
    },
    // Gets the name of the violation message tag.
    get_messageTagName: function () {
        return this._messageTagName;
    },
    // Sets the message shown when minimum length validation has failed.
    set_minLengthViolationMessage: function (value) {
        this._setPropertyIfChanged("minLengthViolationMessage", value);
    },
    // Gets the message shown when minimum length validation has failed.
    get_minLengthViolationMessage: function () {
        return this._minLengthViolationMessage;
    },
    // Sets the message shown when minimum value validation has failed.
    set_minValueViolationMessage: function (value) {
        this._setPropertyIfChanged("minValueViolationMessage", value);
    },
    // Gets the message shown when minimum value validation has failed. 
    get_minValueViolationMessage: function () {
        return this._minValueViolationMessage;
    },
    // Sets the message shown when non alphanumeric validation has failed.
    set_nonAlphaNumericViolationMessage: function (value) {
        this._setPropertyIfChanged("nonAlphaNumericViolationMessage", value);
    },
    // Gets the message shown when non alphanumeric validation has failed.
    get_nonAlphaNumericViolationMessage: function () {
        return this._nonAlphaNumericViolationMessage;
    },
    // Sets the message shown when numeric validation has failed.
    set_numericViolationMessage: function (value) {
        this._setPropertyIfChanged("numericViolationMessage", value);
    },
    // Gets the message shown when numeric validation has failed.
    get_numericViolationMessage: function () {
        return this._numericViolationMessage;
    },
    // Sets the message shown when percentage validation has failed.
    set_percentageViolationMessage: function (value) {
        this._setPropertyIfChanged("percentageViolationMessage", value);
    },
    // Gets the message shown when percentage validation has failed.
    get_percentageViolationMessage: function () {
        return this._percentageViolationMessage;
    },
    // Sets the message shown when regular expression validation has failed.
    set_regularExpressionViolationMessage: function (value) {
        this._setPropertyIfChanged("regularExpressionViolationMessage", value);
    },
    // Gets the message shown when regular expression validation has failed.
    get_regularExpressionViolationMessage: function () {
        return this._regularExpressionViolationMessage;
    },
    // Sets the separator used before evaluating with custom regular expression.
    // If empty, the value is evaluated as a whole, otherwise each part is evaluated.
    set_regularExpressionSeparator: function (value) {
        this._setPropertyIfChanged("regularExpressionSeparator", value);
    },
    // Gets the separator used to split the value between validating with custom regular expression.
    get_regularExpressionSeparator: function () {
        return this._regularExpressionSeparator;
    },
    // Sets the message shown when required validation has failed.
    set_requiredViolationMessage: function (value) {
        this._setPropertyIfChanged("requiredViolationMessage", value);
    },
    // Gets the message shown when required validation has failed.
    get_requiredViolationMessage: function () {
        return this._requiredViolationMessage;
    },
    // Sets the message shown when US social security number validation has failed.
    set_uSocialSecurityNumberViolationMessage: function (value) {
        this._setPropertyIfChanged("uSocialSecurityNumberViolationMessage", value);
    },
    // Gets the message shown when US social security number validation has failed.
    get_uSocialSecurityNumberViolationMessage: function () {
        return this._uSocialSecurityNumberViolationMessage;
    },
    // Sets the message shown when US zip code validation has failed.
    set_uSZipCodeViolationMessage: function (value) {
        this._setPropertyIfChanged("uSZipCodeViolationMessage", value);
    },
    // Gets the message shown when US zip code validation has failed.
    get_uSZipCodeViolationMessage: function () {
        return this._uSZipCodeViolationMessage;
    },
    // Sets wheater to validate if the control is invisible.
    set_validateIfInvisible: function (value) {
        this._setPropertyIfChanged("validateIfInvisible", value);
    },
    // Gets wheater to validate if the control is invisible.
    get_validateIfInvisible: function () {
        return this._validateIfInvisible;
    },
    // Sets the comparing validator definitions
    set_comparingValidatorDefinitions: function (value) {
        this._setPropertyIfChanged("comparingValidatorDefinitions", value);
    },
    // Gets the comparing validator definitions
    get_comparingValidatorDefinitions: function () {
        return this._comparingValidatorDefinitions;
    }
};
Telerik.Sitefinity.Web.UI.Validation.Validator.registerClass("Telerik.Sitefinity.Web.UI.Validation.Validator", Sys.Component);

/* Validation event args classes */

Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs = function (value) {
    this._value = value;
    this._isValid = false;
    Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs.callBaseMethod(this, 'dispose');
    },

    /* -------------------- properties ---------------- */
    // Gets the value that has been validated.
    get_value: function () {
        return this._value;
    },
    // Gets a value indicating whether this instance is valid.
    get_isValid: function () {
        return this._isValid;
    },
    // Sets a value indicating whether this instance is valid.
    set_isValid: function (value) {
        if (this._isValid != value) {
            this._isValid = value;
        }
    }
};

Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Validation.ValidatingEventArgs", Sys.CancelEventArgs);


Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs = function (isValid, value) {
    this._value = value;
    this._isValid = false;
    Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs.callBaseMethod(this, 'dispose');
    },

    /* -------------------- properties ---------------- */
    // Gets the value that has been validated.
    get_value: function () {
        return this._value;
    },
    // Gets a value indicating whether this instance is valid.
    get_isValid: function () {
        return this._isValid;
    },
    // Sets a value indicating whether this instance is valid.
    set_isValid: function (value) {
        if (this._isValid != value) {
            this._isValid = value;
        }
    }
};

Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs.registerClass("Telerik.Sitefinity.Web.UI.Validation.ValidatedEventArgs", Sys.EventArgs);


Telerik.Sitefinity.Web.UI.Validation.ValidationResult = function () {
    this._isValid = false;
    this._value = value;
    Telerik.Sitefinity.Web.UI.Validation.ValidationResult.initializeBase(this);
}

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Validation.Enums");

Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat = function () {
};
Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.prototype = {
    /// none by default
    None: 0,
    /// Validates alpha numerics
    AlphaNumeric: 1,
    /// Validates currency
    Currency: 2,
    /// Validates email addresses
    EmailAddress: 3,
    /// Validates integers
    Integer: 4,
    /// Validates internet urls
    InternetUrl: 5,
    /// Validates non aplhanumerics
    NonAlphaNumeric: 6,
    /// Validates numerics
    Numeric: 7,
    /// Validates percentages
    Percentage: 8,
    /// Validates US social security numbers
    USSocialSecurityNumber: 9,
    /// Validates US zip codes
    USZipCode: 10,
    /// Validates against custom regular expression
    Custom: 11
};
Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat.registerEnum("Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationFormat");

Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationCompareOperator = function () {
};
Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationCompareOperator.prototype = {
    // A comparison for equality.
    Equal: 0,
    // A comparison for inequality.
    NotEqual: 1,
    // A comparison for greater than.
    GreaterThan: 2,
    // A comparison for greater than or equal to.
    GreaterThanEqual: 3,
    // A comparison for less than.
    LessThan: 4,
    // A comparison for less than or equal to.
    LessThanEqual: 5,
    // A comparison for data type only.
    DataTypeCheck: 6
};
Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationCompareOperator.registerEnum("Telerik.Sitefinity.Web.UI.Validation.Enums.ValidationCompareOperator");
