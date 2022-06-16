﻿setNotificationDialog = null;
window.createDialog = function (commandName, dataItem) {
    if (setNotificationDialog) {
        setNotificationDialog.createDialog(commandName, dataItem);
    }
};

Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.SetNotificationDialog = function (element) {
    Telerik.Sitefinity.Workflow.UI.SetNotificationDialog.initializeBase(this, [element]);
    this._selector = {
        customRecipientsWrapper: "#customRecipientsWrapper"
    };

    this._dialogManager = null;
    this._doneButton = null;
    this._cancelButton = null;
    this._labelManager = null;
    this._setNotificationDialogLabel = null;
    this._setNotificationDesc = null;
    this._notifyApprovers = null;
    this._notifyAdministrators = null;
    this._customRecipients = null;
    this._customRecipientsCheck = null;

    this._registeredHandlers = [];
    this._dataItem = null;

    this._doneClickDelegate = null;
    this._closeButtonDelegate = null;
}
Telerik.Sitefinity.Workflow.UI.SetNotificationDialog.prototype = {

    /* **************** setup & teardown **************** */

    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.SetNotificationDialog.callBaseMethod(this, "initialize");
        setNotificationDialog = this;
        jQuery("body").addClass("sfSelectorDialog");

        this._initializeEventHandlers();
    },

    dispose: function () {
        Telerik.Sitefinity.Workflow.UI.SetNotificationDialog.callBaseMethod(this, "dispose");
        setNotificationDialog = null; 
        this._registeredHandlers.forEach(function (handler) {
            $removeHandler(handler.element, handler.event, handler.delegate);
        });
    },

    /* **************** private methods **************** */
    _addHandler: function (element, handler, event) {
        var delegate = Function.createDelegate(this, handler);
        $addHandler(element, event, delegate);

        this._registeredHandlers.push({
            element: element,
            event: event,
            delegate: delegate
        });
    },

    _setDialogHeader: function (level) {
        if (!level)
            return;

        var setNotificationDialogHeader = jQuery(this.get_setNotificationDialogLabel());
        var label = String.format(this._getWorkflowRes("NotificationsForLavel"), level);
        setNotificationDialogHeader.text(label);
    },

    _setDialogDesc: function (action) {
        var actionName = $workflow.getActionUIName(action, this.get_labelManager()).toLocaleLowerCase();
        var setNotificationDescLabel = jQuery(this.get_setNotificationDesc());
        var label = String.format(this._getWorkflowRes("SetNotificationDialogDesc"), actionName);
        setNotificationDescLabel.text(label);
    },

    _getWorkflowRes: function (key) {
        return this.get_labelManager().getLabel("WorkflowResources", key)
    },

    _populateDialogFields: function (dataItem) {
        this.get_notifyAdministrators().set_value(dataItem.NotifyAdministrators);
        this.get_notifyApprovers().set_value(dataItem.NotifyApprovers);

        if (!dataItem.CustomEmailRecipients.length) {
            jQuery(this.get_selector().customRecipientsWrapper).hide();
        } else {
            this.get_customRecipientsCheck().set_value(true);
            this.get_customRecipients().set_value(dataItem.CustomEmailRecipients);
        }
    },

    _isValidEmails: function (emails) {
        if (emails.length > 0) {
            var RegEx = new Telerik.Sitefinity.Web.UI.Validation.Validator().emailAddressRegexPattern;
            emails = $.grep(emails, function (n) { return (n) });
            var invalidEntries = [];
            for (var i = 0; i < emails.length; i++) {
                var validateEmail = RegEx.test(emails[i], 'i');
                if (validateEmail == false) {
                    invalidEntries.push(emails[i]);
                }
            }
            if (invalidEntries.length) {
                message = String.format(this._getWorkflowRes("WorkflowEmailListError"), invalidEntries);
                this._showError(message);

                return false;
            }
            else
                this._hideError();
        }

        return true;
    },

    _showError: function (error) {
        $(this.get_errorMessageWrapper()).show();
        $(this.get_errorMessageLabel()).text(error);
        this.resizeToContent();
    },

    _hideError: function () {
        $(this.get_errorMessageWrapper()).hide();
        $(this.get_errorMessageLabel()).text("");
        this.resizeToContent();
    },

    _resetUI: function () {
        this._hideError();
    },

    /* **************** public methods **************** */

    createDialog: function (dataItem) {
        this._dataItem = dataItem;
        this._setDialogHeader(dataItem.Ordinal);
        this._setDialogDesc(dataItem.ActionName)
        this._populateDialogFields(dataItem);
        this._resetUI();
    },

    /* **************** event handlers **************** */

    _initializeEventHandlers: function () {
        this._addHandler(this.get_doneButton(), this._doneClickHandler, "click");
        this._addHandler(this.get_cancelButton(), this._closeButtonClickHandler, "click");
        this._addHandler(this.get_notifyApprovers()._choiceElement, this._changeNotifyApproversHandler, "change");
        this._addHandler(this.get_notifyAdministrators()._choiceElement, this._changeNotifyAdministratorsHandler, "change");
        this._addHandler(this.get_customRecipientsCheck()._choiceElement, this._changeShowCustomRecipientsHandler, "change");
    },

    _doneClickHandler: function (sender, args) {
        if (!this._dataItem)
            return;

        if (!Boolean.parse(this._customRecipientsCheck.get_value()))
            this.get_customRecipients().set_value(null);

        var emails = this.get_customRecipients().get_value();
        if (!this._isValidEmails(emails))
            return;

        this._dataItem.CustomEmailRecipients = emails;

        this.close(this._dataItem);
    },

    _closeButtonClickHandler: function () {
        this.close();
    },

    _changeNotifyApproversHandler: function (ev) {
        if (!this._dataItem)
            return;

        var toNotifyApprovers = this.get_notifyApprovers().get_value();
        this._dataItem.NotifyApprovers = Boolean.parse(toNotifyApprovers);
    },

    _changeNotifyAdministratorsHandler: function (ev) {
        if (!this._dataItem)
            return;

        var toNotifyAdmin = this.get_notifyAdministrators().get_value();
        this._dataItem.NotifyAdministrators = Boolean.parse(toNotifyAdmin);
    },

    _changeShowCustomRecipientsHandler: function (ev) {
        if (!this._dataItem)
            return;

        var wrapper = jQuery(this.get_selector().customRecipientsWrapper);
        var isChecked = Boolean.parse(this.get_customRecipientsCheck().get_value());
        if (!isChecked) {
            wrapper.hide();
            this.resizeToContent();
        }
        else {
            wrapper.show();
            this.resizeToContent();
        }
    },

    /* **************** properties **************** */

    get_dialogManager: function () {
        return this._dialogManager;
    },
    set_dialogManager: function (value) {
        this._dialogManager = value;
    },

    get_doneButton: function () {
        return this._doneButton;
    },
    set_doneButton: function (value) {
        this._doneButton = value;
    },

    get_cancelButton: function () {
        return this._cancelButton;
    },
    set_cancelButton: function (value) {
        this._cancelButton = value;
    },

    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },

    get_errorMessageLabel: function () {
        return this._errorMessageLabel;
    },
    set_errorMessageLabel: function (value) {
        this._errorMessageLabel = value;
    },

    get_errorMessageWrapper: function () {
        return this._errorMessageWrapper;
    },
    set_errorMessageWrapper: function (value) {
        this._errorMessageWrapper = value;
    },

    get_setNotificationDialogLabel: function () {
        return this._setNotificationDialogLabel;
    },
    set_setNotificationDialogLabel: function (value) {
        this._setNotificationDialogLabel = value;
    },

    get_setNotificationDesc: function () {
        return this._setNotificationDesc;
    },
    set_setNotificationDesc: function (value) {
        this._setNotificationDesc = value;
    },

    get_notifyApprovers: function () {
        return this._notifyApprovers;
    },
    set_notifyApprovers: function (value) {
        this._notifyApprovers= value;
    },

    get_notifyAdministrators: function () {
        return this._notifyAdministrators;
    },
    set_notifyAdministrators: function (value) {
        this._notifyAdministrators = value;
    },

    get_customRecipients: function () {
        return this._customRecipients;
    },
    set_customRecipients: function (value) {
        this._customRecipients = value;
    },

    get_customRecipientsCheck: function () {
        return this._customRecipientsCheck;
    },
    set_customRecipientsCheck: function (value) {
        this._customRecipientsCheck = value;
    },

    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    }
}

Telerik.Sitefinity.Workflow.UI.SetNotificationDialog.registerClass('Telerik.Sitefinity.Workflow.UI.SetNotificationDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);