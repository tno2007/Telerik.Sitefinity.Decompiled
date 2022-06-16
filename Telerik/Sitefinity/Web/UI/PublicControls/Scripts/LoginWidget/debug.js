Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls");

Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget.initializeBase(this, [element]);

    this._submitButton = null;
    this._userNameTextField = null;
    this._passwordTextField = null;
    this._errorMessageLabel = null;
    this._incorrectLoginMessage = null;
    this._membershipProvider = null;
    this._rememberMeCheckbox = null;

    this._submitButtonClickDelegate = null;
    this._textFieldKeyPressDelegate = null;
}

Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget.callBaseMethod(this, 'initialize');

        jQuery.support.cors = true;

        if (this.get_submitButton()) {
            this._submitButtonClickDelegate = Function.createDelegate(this, this._submitButtonClick);
            $addHandler(this.get_submitButton(), "click", this._submitButtonClickDelegate);
        }

        if (this.get_userNameTextField() && this.get_passwordTextField()) {
            this._textFieldKeyPressDelegate = Function.createDelegate(this, this._textFieldKeyPress);
            $addHandler(this.get_userNameTextField().get_element(), "keypress", this._textFieldKeyPressDelegate);
            $addHandler(this.get_passwordTextField().get_element(), "keypress", this._textFieldKeyPressDelegate);
        }

        var error = QueryStringManager.getValue('err', window.location.href);
        if (error === "true") {
            this._showErrorMessage(this.get_incorrectLoginMessage());
        }
    },

    dispose: function () {
        if (this._submitButtonClickDelegate) {
            if (this.get_submitButton())
                $removeHandler(this.get_submitButton(), "click", this._submitButtonClickDelegate);

            delete this._submitButtonClickDelegate;
        }

        if (this._textFieldKeyPressDelegate) {
            if (this.get_userNameTextField() && this.get_userNameTextField().get_element())
                $removeHandler(this.get_userNameTextField().get_element(), "keypress", this._textFieldKeyPressDelegate);

            if (this.get_passwordTextField() && this.get_passwordTextField().get_element())
                $removeHandler(this.get_passwordTextField().get_element(), "keypress", this._textFieldKeyPressDelegate);

            delete this._textFieldKeyPressDelegate;
        }

        Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget.callBaseMethod(this, 'dispose');
    },

    setEnabled: function (value) {
        this.get_submitButton().disabled = !value;
    },

    _textFieldKeyPress: function (event) {
        //if Enter is pressed
        if (13 == event.charCode) {
            this.get_submitButton().click();
        }
    },

    _submitButtonClick: function (event) {
        if (this.get_submitButton().disabled) {
            return false;
        }

        this.get_errorMessageLabel().style.display = "none";

        if (!this.validate()) {
            event.preventDefault ? event.preventDefault() : (event.returnValue = false);
            return false;
        }
    },

    _showErrorMessage: function (message) {
        this.get_errorMessageLabel().innerHTML = message;
        this.get_errorMessageLabel().style.display = "";
    },

    validate: function () {
        var validationResult = this.get_userNameTextField().validate();
        validationResult = this.get_passwordTextField().validate() && validationResult;
        return validationResult;
    },

    get_submitButton: function () {
        return this._submitButton;
    },
    set_submitButton: function (value) {
        this._submitButton = value;
    },

    get_userNameTextField: function () {
        return this._userNameTextField;
    },
    set_userNameTextField: function (value) {
        this._userNameTextField = value;
    },

    get_passwordTextField: function () {
        return this._passwordTextField;
    },
    set_passwordTextField: function (value) {
        this._passwordTextField = value;
    },

    get_errorMessageLabel: function () {
        return this._errorMessageLabel;
    },
    set_errorMessageLabel: function (value) {
        this._errorMessageLabel = value;
    },

    get_incorrectLoginMessage: function () {
        return this._incorrectLoginMessage;
    },
    set_incorrectLoginMessage: function (value) {
        this._incorrectLoginMessage = value;
    },

    get_membershipProvider: function () {
        return this._membershipProvider;
    },
    set_membershipProvider: function (value) {
        this._membershipProvider = value;
    },

    get_rememberMeCheckbox: function () {
        return this._rememberMeCheckbox;
    },
    set_rememberMeCheckbox: function (value) {
        this._rememberMeCheckbox = value;
    },
}

Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.LoginWidget', Sys.UI.Control);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();