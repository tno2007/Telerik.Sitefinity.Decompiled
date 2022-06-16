Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs = function (fieldControl, validationGroup) {
    // <summary>
    // Event args for validation of a field control
    // If the event is Validated the result from the validation is taken, otherwise the standart processing is applied
    // If the event is canceled the whole validation process is canceled - no more controls will be validated
    // </summary>
    this._fieldControl = fieldControl;
    this._validated = false;
    this._isValid = true;
    this._validationGroup = validationGroup;
    Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs.initializeBase(this);
};

Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs.prototype = {
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs.callBaseMethod(this, 'dispose');
    },

    /* -------------------- properties ---------------- */
    get_fieldControl: function () {
        /// <summary>Gets the field control that is validated</summary>
        /// <returns>fieldControl</returns>
        return this._fieldControl;
    },
    set_fieldControl: function (value) {
        /// <summary>Sets the field control that is validated</summary>
        this._fieldControl = value;
    },

    get_isValid: function () {
        /// <summary>Gets the result of the validation of the control</summary>
        /// <returns>boolean</returns>
        return this._isValid;
    },
    set_isValid: function (value) {
        /// <summary>Result of the field validation - true/false</summary>
        this._isValid = value;
    },

    get_validated: function () {
        /// <summary>If processed by external handler should return true</summary>
        /// <returns>boolean</returns>
        return this._validated;
    },
    set_validated: function (value) {
        /// <summary>Set by the external handler when the control was validated</summary>
        this._validated = value;
    },
    get_validationGroup: function () {
        /// <summary>Gets the validation group which the field control is part of</summary>
        return this._validationGroup;
    },
    set_validationGroup: function (value) {
        /// <summary>Sets the validation group of the control</summary>
        this._validationGroup = value;
    }
};
Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs.registerClass('Telerik.Sitefinity.Web.UI.Fields.FormFieldValidationEventArgs', Sys.EventArgs);




Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs = function (violationMessages, validationGroup, validationResult) {
    ///<summary>Args fired by the event that is raised after a form is validated</summary>
    Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs.initializeBase(this);
    this._validationGroup = validationGroup;
    this._violationMessages = violationMessages;
    this._validationResult = validationResult;
}

Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs.callBaseMethod(this, 'dispose');
    },

    get_validationGroup: function () {
        /// <summary>Gets the validation group which the field control is part of</summary>
        return this._validationGroup;
    },
    set_validationGroup: function (value) {
        /// <summary>Sets the validation group of the control</summary>
        this._validationGroup = value;
    },
    get_violationMessages: function () {
        /// <summary>Gets the error messages</summary>
        return this._violationMessages;
    },
    set_violationMessages: function (value) {
        /// <summary>Sets the error messages</summary>
        this._violationMessages = value;
    },

    get_validationResult: function () {
        /// <summary>Sets the result of the form fields validation</summary>
        return this._validationResult;
    }

}

Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs.registerClass('Telerik.Sitefinity.Web.UI.Fields.ValidationCompletedEventArgs', Sys.EventArgs);

