Type.registerNamespace("Telerik.Sitefinity.Web.UI.Notifications");

Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl = function (element) {
    Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl.initializeBase(this, [element]);

    this._labelManager = null;
    this._userEmail = null;

    this._onLoadDelegate = null;
};

Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl.callBaseMethod(this, 'initialize');

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);

        Sys.Application.add_load(this._onLoadDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {
    },

    // Handles the changes in the notifications choice field control
    _notificationsValueChangedHandler: function (sender, args) {
        this._emailListTextField.set_visible(this._notificationsCheckboxControl.checked);
        if (this._notificationsCheckboxControl.checked && (this._emailListTextField.get_value() === undefined || this._emailListTextField.get_value() === "")) {
            this._emailListTextField.set_value($find(this._emailNotificationsControlId)._userEmail);
        }
    },

    /* ------------------ Private methods --------------*/

    _getValidEmails: function (emails) {
        var RegEx = new Telerik.Sitefinity.Web.UI.Validation.Validator().emailAddressRegexPattern;
        var validEmails = [];
        emails = $.grep(emails, function (n) { return (n) });

        for (var i = 0; i < emails.length; i++) {
            var validateEmail = RegEx.test(emails[i], 'i');
            if (validateEmail === true) {
                validEmails.push(emails[i]);
            }

        }
        return validEmails;
    },

    _configureEmails: function (emails) {
        var emailsArray = [];

        if (typeof emails !== 'undefined') {

            //// We need to check if the email length is more than one, as the symbols in the email are counted. If no emails are entered, the length would be 0;
            if (emails.length > 1) {
                emailsArray = emails.split(/\n/);
            }
        }

        return emailsArray;
    },

    _formatEmails: function (emails) {
        var emailsArray = '';
        for (var i = 0; i < emails.length; i++) {
            var element = emails[i];
            emailsArray += element + "\n";
        }
        return emailsArray;
    },

    /* ------------------ Properies --------------*/
    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    }
};

Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl.registerClass('Telerik.Sitefinity.Web.UI.Notifications.EmailNotificationsControl', Sys.UI.Control);
