﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
//Type._registerScript("ContentSelectorsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI");

Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView = function (element) {
    Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView.initializeBase(this, [element]);

    this._radWindowManager = null;
    this._widgetEditorDialog = null;
    this._clientLabelManager = null;
    this._parentDesigner = null;
    this._membershipProviderSelector = null;
    this._templateSelector = null;
    this._widgetEditorDialog = null;
    this._widgetEditorDialogUrl = null;
    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._roleSelectorWrapper = null;
    this._roleSelector = null;
    this._pageSelector = null;
    this._pageSelectorWrapper = null;
    this._pagesPanel = null;
    this._selectRolesButton = null;
    this._selectRolesButtonLiteral = null;
    this._selectPageButton = null;
    this._confirmationTextField = null;
    this._afterSubmissionRadioChoices = null;
    this._cssClassField = null;
    this._itemTypesServiceUrl = null;

    this._selectedRoles = null;
    this._selectedPageId = null;
    this._registerUserSuccessAction = null;
    this._registrationFormTypeName = null;

    this._loadDelegate = null;
    this._editTemplateLinkClickDelegate = null;
    this._createTemplateLinkClickDelegate = null;
    this._onWidgetEditorClosedDelegate = null;
    this._widgetEditorShowDelegate = null;
    this._afterFormSubmissionActionSelectionDelegate = null;
    this._rolesSelectorClickDelegate = null;
    this._roleSelectedDelegate = null;
    this._pageSelectorSelectionDoneDelegate = null;

    this._selectedTemplateId = null;
    this._roleSelectorDialog = null;
    this._pageSelectorDialog = null;
    this._modifyWidgetTemplatePermission = null;
}

Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView.callBaseMethod(this, 'initialize');

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

        if (this._editTemplateLinkClickDelegate) {
            delete this._editTemplateLinkClickDelegate;
        }

        if (this._createTemplateLinkClickDelegate) {
            delete this._createTemplateLinkClickDelegate;
        }

        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }

        if (this._onWidgetEditorClosedDelegate) {
            delete this._onWidgetEditorClosedDelegate;
        }

        if (this._afterFormSubmissionActionSelectionDelegate) {
            delete this._afterFormSubmissionActionSelectionDelegate;
        }

        if (this._rolesSelectorClickDelegate) {
            delete this._rolesSelectorClickDelegate;
        }

        if (this._roleSelectedDelegate) {
            delete this._roleSelectedDelegate;
        }

        if (this._pageSelectorSelectionDoneDelegate) {
            delete this._pageSelectorSelectionDoneDelegate;
        }

        Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();


        if (this._membershipProviderSelector)
            this._membershipProviderSelector.set_value(controlData.MembershipProvider);

        if (controlData.TemplateKey) {
            this.get_templateSelector().set_value(controlData.TemplateKey);
        }

        if (controlData.Roles) {
            this._selectedRoles = Sys.Serialization.JavaScriptSerializer.deserialize(controlData.Roles);
            if (this._selectedRoles.length > 0) {
                var selectedRoleIds = [];
                for (var i = 0, selectedRolesLength = this._selectedRoles.length; i < selectedRolesLength; i++) {
                    selectedRoleIds.push(this._selectedRoles[i].ItemId);
                }
                this._roleSelector.set_selectedKeys(selectedRoleIds);
                if (this.get_clientLabelManager())
                    jQuery(this.get_selectRolesButtonLiteral()).html(this.get_clientLabelManager().getLabel("Labels", "ChangeEllipsis"));

                var selectedRoleNames = "";
                for (var i = 0, len = this._selectedRoles.length; i < len; i++) {
                    selectedRoleNames += '<span class="sfSelectedItem">' + this._selectedRoles[i].Title + '</span>';
                    if (i + 1 == len)
                        selectedRoleNames += '';
                }
                jQuery("#selectedRoleLabel").html(selectedRoleNames).show();
            }
        }

        switch (controlData.RegistratingUserSuccessAction) {
            case "ShowMessage":
                jQuery(this.get_element()).find(':radio[id$=ShowMessage]').click();
                if (this.get_confirmationTextField())
                    this.get_confirmationTextField().set_value(controlData.ConfirmationText);
                break;
            case "RedirectToPage":
                jQuery(this.get_element()).find(':radio[id$=RedirectToPage]').click();
                if (controlData.RedirectOnSubmitPageId && controlData.RedirectOnSubmitPageId != "00000000-0000-0000-0000-000000000000") {
                    jQuery("#selectedPageLabel").show().text(controlData.RedirectOnSubmitPageTitle);
                    if (this.get_clientLabelManager())
                        jQuery(this.get_selectPageButtonLiteral()).text(this.get_clientLabelManager().getLabel("UserProfilesResources", "ChangePage"));
                    this._selectedPageId = controlData.RedirectOnSubmitPageId;
                }
                break;
            default:
                jQuery(this.get_element()).find(':radio[id$=ShowMessage]').click();
                if (this.get_confirmationTextField())
                    this.get_confirmationTextField().set_value(controlData.ConfirmationText);
                break;
        }

        if (this._cssClassField)
            this._cssClassField.set_value(controlData.CssClass);

        this._registrationFormTypeName = controlData.RegistrationFormTypeName;

        this._updateEditButtons();
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();

        if (this._membershipProviderSelector)
            controlData.MembershipProvider = this._membershipProviderSelector.get_value();

        if (this._selectedRoles) {
            controlData.Roles = Sys.Serialization.JavaScriptSerializer.serialize(this._selectedRoles);
        }

        controlData.TemplateKey = this.get_templateSelector().get_value();

        if (this._registerUserSuccessAction) {
            controlData.RegistratingUserSuccessAction = this._registerUserSuccessAction;

            switch (this._registerUserSuccessAction) {
                case "ShowMessage":
                    if (this.get_confirmationTextField())
                        controlData.ConfirmationText = this.get_confirmationTextField().get_value();
                    break;
                case "RedirectToPage":
                    if (this._selectedPageId)
                        controlData.RedirectOnSubmitPageId = this._selectedPageId;
                    break;
            }
        }

        if (this._cssClassField)
            controlData.CssClass = this._cssClassField.get_value();
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    _afterFormSubmissionActionSelectionHandler: function (e) {
        var radioID = e.target.value;

        switch (radioID) {
            case "ShowMessage":

                jQuery(this._pagesPanel).hide();
                this._registerUserSuccessAction = "ShowMessage";
                break;
            case "RedirectToPage":

                jQuery(this._pagesPanel).show();
                this._registerUserSuccessAction = "RedirectToPage";
                break;
        }

        dialogBase.resizeToContent();
    },

    _loadHandler: function (sender, args) {
        this._createSelectorDialogs();
        this._configureTemplateEditorDialog();
    },

    _createTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = null;
            this._controlType = this._registrationFormTypeName;
            this._openCreateTemplateDialog();
        } else {
            alert("You don't have the permissions to create new widgets templates.");
        }
    },

    _editTemplateLinkClicked: function (sender, args) {
        if (this._modifyWidgetTemplatePermission) {
            this._selectedTemplateId = this.get_templateSelector().get_value();
            this._controlType = this._registrationFormTypeName;
            this._openEditTemplateDialog();
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _openEditTemplateDialog: function () {
        if (this._widgetEditorDialog) {
            var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
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
            if (condition) {
                var url = new Sys.Uri(dialogUrl);
                url.get_query().Condition = condition;
                dialogUrl = url.toString();
            }
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

                    this.get_templateSelector().addListItem(widgetId, widgetName);
                    this.get_templateSelector().set_value(widgetId);
                    this._updateEditButtons();
                }
                else if (arg.IsUpdated) {
                    var selectedChoices = this.get_templateSelector()._get_selectedListItemsElements();
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
                    ItemTypesServiceUrl: this._itemTypesServiceUrl,
                    ItemTypeFullNameField: "DynamicTypeName",
                    ItemTypeTitleField: "Name"
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    _rolesSelectedClickHandler: function (sender, args) {
        this._roleSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _roleSelectedHandler: function (sender, args) {
        var controlData = this.get_controlData();
        this._roleSelectorDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (!this._roleSelector)
            return;

        var selectedItems = this.get_roleSelector().getSelectedItems();
        var selectedRoleNames = "";
        var selectedRoles = [];
        for (var i = 0, len = selectedItems.length; i < len; i++) {
            selectedRoleNames += '<li>' + selectedItems[i].Name + '</li>';
            if (i + 1 == len)
                selectedRoleNames += '';

            var obj = { ProviderName: selectedItems[i].ProviderName, ItemId: selectedItems[i].Id, Title: selectedItems[i].Name };
            obj.__type = controlData.RolesItemInfoName;
            selectedRoles[selectedRoles.length] = obj;
        }
        jQuery("#selectedRoleLabel").html(selectedRoleNames).show();
        jQuery(this.get_selectRolesButtonLiteral()).text(this.get_clientLabelManager().getLabel("Labels", "ChangeEllipsis"));
        this._selectedRoles = selectedRoles;

        dialogBase.resizeToContent();
    },

    _roleSelectionCanceledHandler: function (sender, args) {
        this._roleSelectorDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _pageSelectorSelectionDoneHandler: function (sender, args) {
        if (this._pageSelectorDialog)
            this._pageSelectorDialog.dialog("close");

        var selectedItem = this.get_pageSelector().getSelectedItems()[0];
        if (selectedItem) {
            jQuery("#selectedPageLabel").show().text(selectedItem.Title);
            jQuery(this.get_selectPageButtonLiteral()).text(this.get_clientLabelManager().getLabel("UserProfilesResources", "ChangePage"));
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

        if (isValidValue(this.get_templateSelector().get_value())) {
            jQuery(this.get_editTemplateLink()).show();
        }
        else {
            jQuery(this.get_editTemplateLink()).hide();
        }
    },

    get_afterFormSubmissionActionChoices: function () {
        if (!this._afterSubmissionRadioChoices) {
            this._afterSubmissionRadioChoices = jQuery(this.get_element()).find(':radio[name$=AfterFormSubmissionAction]');
        }
        return this._afterSubmissionRadioChoices;
    },

    _createSelectorDialogs: function () {
        this._roleSelectorDialog = jQuery(this.get_roleSelectorWrapper()).dialog(
			{ autoOpen: false,
			    modal: false,
			    width: 355,
			    closeOnEscape: true,
			    resizable: false,
			    draggable: false,
			    classes: {
			        "ui-dialog": "sfZIndexM"
			    }
			});

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
        this._rolesSelectorClickDelegate = Function.createDelegate(this, this._rolesSelectedClickHandler);
        jQuery(this.get_selectRolesButton()).click(this._rolesSelectorClickDelegate);

        this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        jQuery(this.get_editTemplateLink()).click(this._editTemplateLinkClickDelegate);

        this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        jQuery(this.get_createTemplateLink()).click(this._createTemplateLinkClickDelegate);

        this._roleSelectedDelegate = Function.createDelegate(this, this._roleSelectedHandler);
        this._roleSelectionCanceledDelegate = Function.createDelegate(this, this._roleSelectionCanceledHandler);

        jQuery("#doneSelectingRoleButton").click(this._roleSelectedDelegate);
        jQuery("#cancelSelectingRoleButton").click(this._roleSelectionCanceledDelegate);

        this._afterFormSubmissionActionSelectionDelegate = Function.createDelegate(this, this._afterFormSubmissionActionSelectionHandler);
        this.get_afterFormSubmissionActionChoices().click(this._afterFormSubmissionActionSelectionDelegate);

        this._pageSelectorClickDelegate = Function.createDelegate(this, this._pageSelectorClickHandler);
        jQuery(this.get_selectPageButton()).click(this._pageSelectorClickDelegate);

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

    get_membershipProviderSelector: function () {
        return this._membershipProviderSelector;
    },

    set_membershipProviderSelector: function (value) {
        this._membershipProviderSelector = value;
    },

    get_editTemplateLink: function () {
        return this._editTemplateLink;
    },

    set_editTemplateLink: function (value) {
        this._editTemplateLink = value;
    },

    get_createTemplateLink: function () {
        return this._createTemplateLink;
    },

    set_createTemplateLink: function (value) {
        this._createTemplateLink = value;
    },

    get_roleSelectorWrapper: function () {
        return this._roleSelectorWrapper;
    },

    set_roleSelectorWrapper: function (value) {
        this._roleSelectorWrapper = value;
    },

    get_roleSelector: function () {
        return this._roleSelector;
    },

    set_roleSelector: function (value) {
        this._roleSelector = value;
    },

    get_selectRolesButton: function () {
        return this._selectRolesButton;
    },

    set_selectRolesButton: function (value) {
        this._selectRolesButton = value;
    },

    get_selectRolesButtonLiteral: function () {
        return this._selectRolesButtonLiteral;
    },

    set_selectRolesButtonLiteral: function (value) {
        this._selectRolesButtonLiteral = value;
    },

    get_templateSelector: function () {
        return this._templateSelector;
    },

    set_templateSelector: function (value) {
        this._templateSelector = value;
    },

    get_selectPageButton: function () {
        return this._selectPageButton;
    },

    set_selectPageButton: function (value) {
        this._selectPageButton = value;
    },

    get_selectPageButtonLiteral: function () {
        return this._selectPageButtonLiteral;
    },

    set_selectPageButtonLiteral: function (value) {
        this._selectPageButtonLiteral = value;
    },

    get_pagesPanel: function () {
        return this._pagesPanel;
    },

    set_pagesPanel: function (value) {
        this._pagesPanel = value;
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

    get_confirmationTextField: function () {
        return this._confirmationTextField;
    },

    set_confirmationTextField: function (value) {
        this._confirmationTextField = value;
    },

    get_cssClassField: function () {
        return this._cssClassField;
    },

    set_cssClassField: function (value) {
        this._cssClassField = value;
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

Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView.registerClass('Telerik.Sitefinity.Security.Web.UI.RegistrationFormGeneralView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);