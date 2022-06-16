/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI");

Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView = function (element) {
    Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView.initializeBase(this, [element]);

    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._clientLabelManager = null;
    this._parentDesigner = null;

    this._selectConfirmationPageButton = null;
    this._selectConfirmationPageButtonLiteral = null;
    this._confirmationPanel = null;
    this._editConfirmationEmailTemplateLink = null;
    this._createConfirmationEmailTemplateLink = null;
    this._editSuccessEmailTemplateLink = null;
    this._createSuccessEmailTemplateLink = null;

    this._pageSelector = null;
    this._pageSelectorWrapper = null;
    this._confirmationPanel = null;
    this._activateAccountsActionRadioChoices = null;
    this._widgetEditorDialogUrl = null;
    this._confirmationEmailTemplateSelector = null;
    this._successEmailTemplateSelector = null;
    this._registrationFormTypeName = null;
    this._openedTemplateSelector = null;

    this._smtpIsSet = null;
    this._condition = null;

    this._sendRegistrationEmail = null;
    this._selectedPageId = null;
    this._successEmailTemplateId = null;
    this._confirmationEmailTemplateId = null;

    this._loadDelegate = null;
    this._activateAccountsActionSelectionDelegate = null;
    this._pageSelectorSelectionDoneDelegate = null;
    this._sendSuccessEmailClickDelegate = null;

    this._editSuccessTemplateLinkClickDelegate = null;
    this._createSuccessTemplateLinkClickDelegate = null;

    this._editConfirmationTemplateLinkClickDelegate = null;
    this._createConfirmationTemplateLinkClickDelegate = null;

    this._widgetEditorShowDelegate = null;
    this._onWidgetEditorClosedDelegate = null;

    this._pageSelectorDialog = null;

    this.SUCCESS = "Success";
    this.CONFIRMATION = "Confirmation";
}

Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView.callBaseMethod(this, 'initialize');

        this._assignClickHandlers();

        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);

        // Prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        if (this._loadDelegate) {
            delete this._loadDelegate;
        }

        if (this._activateAccountsActionSelectionDelegate) {
            delete this._activateAccountsActionSelectionDelegate;
        }

        if (this._pageSelectorSelectionDoneDelegate) {
            delete this._pageSelectorSelectionDoneDelegate;
        }

        if (this._editSuccessTemplateLinkClickDelegate) {
            delete this._editSuccessTemplateLinkClickDelegate;
        }

        if (this._createSuccessTemplateLinkClickDelegate) {
            delete this._createSuccessTemplateLinkClickDelegate;
        }

        if (this._editConfirmationTemplateLinkClickDelegate) {
            delete this._editConfirmationTemplateLinkClickDelegate;
        }

        if (this._createConfirmationTemplateLinkClickDelegate) {
            delete this._createConfirmationTemplateLinkClickDelegate;
        }

        if (this._sendSuccessEmailClickDelegate) {
            delete this._sendSuccessEmailClickDelegate;
        }

        Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        if (controlData.SendRegistrationEmail) {
            jQuery(this.get_element()).find(':radio[id$=AfterConfirmation]').click();
            if (controlData.ConfirmationPageId && controlData.ConfirmationPageId != "00000000-0000-0000-0000-000000000000") {
                jQuery("#selectedConfirmationPageLabel").show().text(controlData.ConfirmationPageTitle);
                this._selectedPageId = controlData.ConfirmationPageId;
            }

            if (controlData.ConfirmationEmailTemplateId && controlData.ConfirmationEmailTemplateId != "00000000-0000-0000-0000-000000000000") {
                this.get_confirmationEmailTemplateSelector().set_value(controlData.ConfirmationEmailTemplateId);
            }
        } else {
            jQuery(this.get_element()).find(':radio[id$=Immediately]').click();
        }

        if (controlData.SendEmailOnSuccess) {
            jQuery(this.get_element()).find(":checkbox[id$=sendEmailOnSuccess]").attr("checked", true);
        }

        if (controlData.SuccessEmailTemplateId && controlData.SuccessEmailTemplateId != "00000000-0000-0000-0000-000000000000") {
            this.get_successEmailTemplateSelector().set_value(controlData.SuccessEmailTemplateId);
        }

        this._registrationFormTypeName = controlData.RegistrationFormTypeName;

        if (!this._smtpIsSet) {
            if (controlData.SendEmailOnSuccess || controlData.SendRegistrationEmail) {
                jQuery("#noSmtpSettingsPanel").show();
            }
        }

        this._updateEditButtons();
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.ConfirmationEmailTemplateId = this.get_confirmationEmailTemplateSelector().get_value();
        controlData.SendRegistrationEmail = this._sendRegistrationEmail;
        controlData.SendEmailOnSuccess = jQuery("#sendEmailOnSuccess").is(":checked");
        controlData.SuccessEmailTemplateId = this.get_successEmailTemplateSelector().get_value();

        if (controlData.SendRegistrationEmail && this._selectedPageId) {
            controlData.ConfirmationPageId = this._selectedPageId;
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    _sendSuccessEmailClickHandler: function (e) {
        if (!this._smtpIsSet) {
            if (e.target.checked) {
                jQuery("#noSmtpSettingsPanel").show();
            } else {
                if (this._sendRegistrationEmail)
                    jQuery("#noSmtpSettingsPanel").show();
                else
                    jQuery("#noSmtpSettingsPanel").hide();
            }
        }

        dialogBase.resizeToContent();
    },

    _activateAccountsActionSelectionHandler: function (e) {
        var radioID = e.target.value;

        switch (radioID) {
            case "Immediately":
                jQuery(this._confirmationPanel).hide();
                this._sendRegistrationEmail = false;
                jQuery("#afterTheConfirmationAppendText").hide();

                if (!this._smtpIsSet) {
                    if (jQuery(this.get_element()).find(":checkbox[id$=sendEmailOnSuccess]").attr("checked")) {
                        jQuery("#noSmtpSettingsPanel").show();
                    } else {
                        jQuery("#noSmtpSettingsPanel").hide();
                    }
                } else {
                    jQuery("#noSmtpSettingsPanel").hide();
                }

                jQuery("#successEmailPanel").show();

                break;
            case "AfterConfirmation":
                jQuery(this._confirmationPanel).show();
                this._sendRegistrationEmail = true;
                jQuery("#afterTheConfirmationAppendText").show();

                if (!this._smtpIsSet)
                    jQuery("#noSmtpSettingsPanel").show();

                jQuery("#successEmailPanel").hide();

                break;
        }

        dialogBase.resizeToContent();
    },

    _loadHandler: function (sender, args) {
        this._createSelectorDialogs();
        this._configureTemplateEditorDialog();
    },

    _createSuccessTemplateLinkClicked: function (sender, args) {
        this._openedTemplateSelector = this.get_successEmailTemplateSelector();
        this._selectedTemplateId = null;
        this._controlType = this._registrationFormTypeName;
        this._condition = this.SUCCESS;
        this._openCreateTemplateDialog();
    },

    _editSuccessTemplateLinkClicked: function (sender, args) {
        this._openedTemplateSelector = this.get_successEmailTemplateSelector();
        this._selectedTemplateId = this.get_templateSelector("Success").get_value();
        this._controlType = this._registrationFormTypeName;
        this._condition = this.SUCCESS;
        this._openEditTemplateDialog();
    },

    _createConfirmationTemplateLinkClicked: function (sender, args) {
        this._openedTemplateSelector = this.get_confirmationEmailTemplateSelector();
        this._selectedTemplateId = null;
        this._controlType = this._registrationFormTypeName;
        this._condition = this.CONFIRMATION;
        this._openCreateTemplateDialog();
    },

    _editConfirmationTemplateLinkClicked: function (sender, args) {
        this._openedTemplateSelector = this.get_confirmationEmailTemplateSelector();
        this._selectedTemplateId = this.get_templateSelector("Confirmation").get_value();
        this._controlType = this._registrationFormTypeName;
        this._condition = this.CONFIRMATION;
        this._openEditTemplateDialog();
    },

    _openEditTemplateDialog: function () {
        if (this._widgetEditorDialog) {
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
            var url = new Sys.Uri(dialogUrl);
            url.get_query().ControlType = this._controlType;
            url.get_query().TemplateId = this._selectedTemplateId;
            dialogUrl = url.toString();
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            dialogBase.get_radWindow().maximize();
            this._widgetEditorDialog.show();
            this._widgetEditorDialog.maximize();
            $("body").removeClass("sfSelectorDialog");
        }
    },

    _openCreateTemplateDialog: function (condition) {
        if (this._widgetEditorDialog) {
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
            var url = new Sys.Uri(dialogUrl);
            url.get_query().ControlType = this._controlType;
            url.get_query().TemplateId = this._selectedTemplateId;
            if (condition) {
                url.get_query().Condition = condition;
            }
            dialogUrl = url.toString();
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            dialogBase.get_radWindow().maximize();
            this._widgetEditorDialog.show();
            this._widgetEditorDialog.maximize();
            $("body").removeClass("sfSelectorDialog");
        }
    },

    _onWidgetEditorClosed: function (sender, args) {
        dialogBase.get_radWindow().Restore();
        $("body").addClass("sfSelectorDialog");

        if (args && args.get_argument) {
            var arg = args.get_argument();
            if (arg) {
                if (arg.IsCreated) {
                    var widgetName = arg.DataItem.Name;
                    var widgetId = arg.DataItem.Id;

                    this._openedTemplateSelector.addListItem(widgetId, widgetName);
                    this._openedTemplateSelector.set_value(widgetId);
                    this._updateEditButtons();
                }
                else if (arg.IsUpdated) {
                    var selectedChoices = this._openedTemplateSelector._get_selectedListItemsElements();
                    if (selectedChoices) {
                        var selectedChoice = selectedChoices[0];
                        var newName = arg.DataItem.Name;

                        selectedChoice.text = newName;
                    }
                }
            }
        }
    },

    _onWidgetEditorShown: function (sender, args) {
        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            if (frameHandle.createDialog) {
                var params =
                {
                    TemplateId: this._selectedTemplateId,
                    ControlType: this._controlType,
                    DataType: "EMAIL_TEMPLATE",
                    Condition: this._condition
                    //TODO
                    //, BlackListControlTemplateEditor: true
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    _pageSelectorSelectionDoneHandler: function (sender, args) {
        if (this._pageSelectorDialog)
            this._pageSelectorDialog.dialog("close");

        var selectedItem = this.get_pageSelector().getSelectedItems()[0];
        if (selectedItem) {
            jQuery("#selectedConfirmationPageLabel").show().text(selectedItem.Title);
            jQuery(this.get_selectConfirmationPageButtonLiteral()).text(this.get_clientLabelManager().getLabel("UserProfilesResources", "ChangePage"));
            this._selectedPageId = selectedItem.Id;
        }
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _pageSelectorClickHandler: function (sender, args) {
        if (this._selectedPageId) {
            this.get_pageSelector().setSelectedItems([{ Id: this._selectedPageId }]);
        }
        this._pageSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateEditButtons: function () {
        var isValidValue = function (value) {
            return (value && !((value instanceof Array) && value.length == 0));
        };

        if (isValidValue(this.get_successEmailTemplateSelector().get_value())) {
            jQuery(this.get_editSuccessEmailTemplateLink()).show();
        }
        else {
            jQuery(this.get_editSuccessEmailTemplateLink()).hide();
        }

        if (isValidValue(this.get_confirmationEmailTemplateSelector().get_value())) {
            jQuery(this.get_editConfirmationEmailTemplateLink()).show();
        }
        else {
            jQuery(this.get_editConfirmationEmailTemplateLink()).hide();
        }
    },

    get_activateAccountActionChoices: function () {
        if (!this._activateAccountsActionRadioChoices) {
            this._activateAccountsActionRadioChoices = jQuery(this.get_element()).find(':radio[name$=ActivateAccounts]');
        }
        return this._activateAccountsActionRadioChoices;
    },

    _createSelectorDialogs: function () {
        this._pageSelectorDialog = jQuery(this.get_pageSelectorWrapper()).dialog(
			{
			    autoOpen: false,
			    modal: false,
			    width: 355,
			    closeOnEscape: true,
			    resizable: false,
			    draggable: false,
			    classes: {
			        "ui-dialog": "sfZIndexM"
			    }
			});
    },

    _assignClickHandlers: function () {
        this._sendSuccessEmailClickDelegate = Function.createDelegate(this, this._sendSuccessEmailClickHandler);
        jQuery(this.get_element()).find(":checkbox[id$=sendEmailOnSuccess]").click(this._sendSuccessEmailClickDelegate);

        this._activateAccountsActionSelectionDelegate = Function.createDelegate(this, this._activateAccountsActionSelectionHandler);
        this.get_activateAccountActionChoices().click(this._activateAccountsActionSelectionDelegate);

        this._pageSelectorClickDelegate = Function.createDelegate(this, this._pageSelectorClickHandler);
        jQuery(this.get_selectConfirmationPageButton()).click(this._pageSelectorClickDelegate);

        this._editSuccessTemplateLinkClickDelegate = Function.createDelegate(this, this._editSuccessTemplateLinkClicked);
        this._createSuccessTemplateLinkClickDelegate = Function.createDelegate(this, this._createSuccessTemplateLinkClicked);

        jQuery(this.get_editSuccessEmailTemplateLink()).click(this._editSuccessTemplateLinkClickDelegate);
        jQuery(this.get_createSuccessEmailTemplateLink()).click(this._createSuccessTemplateLinkClickDelegate);

        this._editConfirmationTemplateLinkClickDelegate = Function.createDelegate(this, this._editConfirmationTemplateLinkClicked);
        this._createConfirmationTemplateLinkClickDelegate = Function.createDelegate(this, this._createConfirmationTemplateLinkClicked);

        jQuery(this.get_editConfirmationEmailTemplateLink()).click(this._editConfirmationTemplateLinkClickDelegate);
        jQuery(this.get_createConfirmationEmailTemplateLink()).click(this._createConfirmationTemplateLinkClickDelegate);

        this._pageSelectorSelectionDoneDelegate = Function.createDelegate(this, this._pageSelectorSelectionDoneHandler);
        if (this.get_pageSelector())
            this.get_pageSelector().add_doneClientSelection(this._pageSelectorSelectionDoneDelegate);
    },

    _configureTemplateEditorDialog: function () {
        this._assignTemplateEditorDialogHandlers();

        var dialogUrl = this._widgetEditorDialogUrl;
        this.get_widgetEditorDialog().set_navigateUrl(dialogUrl);
        this.get_widgetEditorDialog().add_close(this._onWidgetEditorClosedDelegate);
        this.get_widgetEditorDialog().add_pageLoad(this._widgetEditorShowDelegate);
    },

    _assignTemplateEditorDialogHandlers: function () {
        if (this._widgetEditorShowDelegate === null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._onWidgetEditorClosedDelegate === null) {
            this._onWidgetEditorClosedDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }
    },

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    get_selectConfirmationPageButton: function () {
        return this._selectPageButton;
    },

    set_selectConfirmationPageButton: function (value) {
        this._selectPageButton = value;
    },

    get_selectConfirmationPageButtonLiteral: function () {
        return this._selectPageButtonLiteral;
    },

    set_selectConfirmationPageButtonLiteral: function (value) {
        this._selectPageButtonLiteral = value;
    },

    get_confirmationPanel: function () {
        return this._confirmationPanel;
    },

    set_confirmationPanel: function (value) {
        this._confirmationPanel = value;
    },

    get_editConfirmationEmailTemplateLink: function () {
        return this._editConfirmationEmailTemplateLink;
    },

    set_editConfirmationEmailTemplateLink: function (value) {
        this._editConfirmationEmailTemplateLink = value;
    },

    get_createConfirmationEmailTemplateLink: function () {
        return this._createConfirmationEmailTemplateLink;
    },

    set_createConfirmationEmailTemplateLink: function (value) {
        this._createConfirmationEmailTemplateLink = value;
    },

    get_editSuccessEmailTemplateLink: function () {
        return this._editSuccessEmailTemplateLink;
    },

    set_editSuccessEmailTemplateLink: function (value) {
        this._editSuccessEmailTemplateLink = value;
    },

    get_createSuccessEmailTemplateLink: function () {
        return this._createSuccessEmailTemplateLink;
    },

    set_createSuccessEmailTemplateLink: function (value) {
        this._createSuccessEmailTemplateLink = value;
    },

    get_pageSelector: function () {
        return this._pageSelector;
    },

    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_pageSelectorWrapper: function () {
        return this._pageSelectorWrapper;
    },

    set_pageSelectorWrapper: function (value) {
        this._pageSelectorWrapper = value;
    },

    get_confirmationEmailTemplateSelector: function () {
        return this._confirmationEmailTemplateSelector;
    },

    set_confirmationEmailTemplateSelector: function (value) {
        this._confirmationEmailTemplateSelector = value;
    },

    get_successEmailTemplateSelector: function () {
        return this._successEmailTemplateSelector;
    },

    set_successEmailTemplateSelector: function (value) {
        this._successEmailTemplateSelector = value;
    },

    get_templateSelector: function (condition) {
        switch (condition) {
            case "Confirmation":
                return this.get_confirmationEmailTemplateSelector();
                break;
            case "Success":
                return this.get_successEmailTemplateSelector();
                break;
        }
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },

    set_radWindowManager: function (value) {
        this._radWindowManager = value;
    },

    get_widgetEditorDialog: function () {
        return this._widgetEditorDialog;
    },

    set_widgetEditorDialog: function (value) {
        this._widgetEditorDialog = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
}

Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView.registerClass('Telerik.Sitefinity.Security.Web.UI.RegistrationFormAccountActivationView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
