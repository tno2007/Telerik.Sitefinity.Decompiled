/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Type._registerScript("UserProfileDesigner.js", ["IControlDesigner.js"]);
Type.registerNamespace("Telerik.Sitefinity.Security.Web.UI.Designers");

// ------------------------------------------------------------------------
// UserProfileDesigner class
// ------------------------------------------------------------------------

Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner = function (element) {
    Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.initializeBase(this, [element]);
    this._userSelector = null;
    this._templateSelector = null;
    this._templateAnonymousUserSelector = null;
    this._editModeTemplateSelector = null;
    this._changePasswordTemplateSelector = null;
    this._changePasswordQuestionAndAnswerTemplateSelector = null;
    this._profileTypeSelector = null;
    this._pageSelector = null;
    this._changePasswordProfilePageSelector = null;
    this._userSelectorButton = null;
    this._userSelectorWrapper = null;
    this._cssClassTextBox = null;
    this._profileUpdatedMessageTextArea = null;
    this._editTemplateLink = null;
    this._createTemplateLink = null;
    this._createAnonymousUserTemplateLink = null;
    this._editAnonymousUserTemplateLink = null;
    this._editEditModeTemplateLink = null;
    this._createEditModeTemplateLink = null;
    this._editChangePasswordTemplateLink = null;
    this._createChangePasswordTemplateLink = null;
    this._editChangePasswordQuestionAndAnswerTemplateLink = null;
    this._createChangePasswordQuestionAndAnswerTemplateLink = null;
    this._moreOptionsLink = null;
    this._showEditProfilePageSelectorLink = null;
    this._showChangePasswordProfilePageSelectorLink = null;
    this._showRedirectPageSelector = null;
    this._selectedEditProfilePageLabel = null;
    this._selectedChangePasswordPageSelectorLabel = null;
    this._selectedRedirectPageLabel = null;
    this._showMessageRadioLabel = null;
    this._moreOptionsWrapper = null;
    this._pageSelectorWrapper = null;
    this._pageSelectorsWrapper = null;
    this._submittingUserProfileSuccessActionWrapper = null;
    this._changePasswordProfilePageSelectorWrapper = null;
    this._anonymousUserSelectorWrapper = null;
    this._changePasswordTemplateSelectorWrapper = null;
    this._changePasswordQuestionAndAnswerTemplateSelectorWrapper = null;
    this._redirectOnSubmitChoiceWrapper = null;
    this._editModeTemplateSelectorWrapper = null;
    this._writeModeTemplateSelectorWrapper = null;
    this._modifyWidgetTemplatePermission = null;
    this._userSelectionWrapper = null;
    this._switchToReadModeRadioWrapper = null;
    this._openViewsInExternalPages = null;
    this._userSelectorDialog = null;
    this._pageSelectorDialog = null;
    this._widgetEditorDialog = null;
    this._widgetEditorDialogUrl = null;
    this._radWindowManager = null;
    this._clientLabelManager = null;

    this._selectedTemplateId = null;
    this._controlType = null;
    this._editTemplateViewName = null;
    this._createTemplateViewName = null;
    this._openedPageSelector = null;
    this.openedPageSelectorEnum = null;
    this._openedTemplateSelector = null;
    this.openedTemplateSelectorEnum = null;

    this._readViewName = null;
    this._writeViewName = null;
    this._changePasswordViewName = null;
    this._changePasswordQuestionAndAnswerViewName = null;
    this._loadDelegate = null;
    this._viewModeClickedDelegate = null;
    this._userSelectionClickedDelegate = null;
    this._userSelectorClickDelegate = null;
    this._userSelectedDelegate = null;
    this._userSelectionCanceledDelegate = null;
    this._editTemplateLinkClickDelegate = null;
    this._createTemplateLinkClickDelegate = null;
    this._editAnonymousUserTemplateLinkClickDelegate = null;
    this._createAnonymousUserTemplateLinkClickDelegate = null;
    this._editEditModeTemplateLinkClickDelegate = null;
    this._createEditModeTemplateLinkClickDelegate = null;
    this._editChangePasswordTemplateLinkClickDelegate = null;
    this._createChangePasswordTemplateLinkClickDelegate = null;
    this._editChangePasswordQuestionAndAnswerTemplateLinkClickDelegate = null;
    this._createChangePasswordQuestionAndAnswerTemplateLinkClickDelegate = null;
    this._onWidgetEditorClosedDelegate = null;
    this._widgetEditorShowDelegate = null;
    this._moreOptionsSectionClickDelegate = null;
    this._openViewsInExternalPagesClickDelegate = null;
    this._showChangePasswordProfilePageSelectorLinkClickDelegate = null;
    this._showEditProfilePageSelectorLinkClickDelegate = null;
    this._submitSuccessActionClickedDelegate = null;
    this._showRedirectPageSelectorLinkClickDelegate = null;

    this._pageSelectorSelectionDoneDelegate = null;
};

Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.callBaseMethod(this, 'initialize');
        this.openedPageSelectorEnum = Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedPageSelector;
        this.openedTemplateSelectorEnum = Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedTemplateSelector;
        this._assignHandlers();
        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

        this._pageSelectorDialog = jQuery(this.get_pageSelectorWrapper()).dialog({
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

    dispose: function () {
        if (this._loadDelegate) {
            delete this._loadDelegate;
        }
        if (this._viewModeClickedDelegate) {
            delete this._viewModeClickedDelegate;
        }
        if (this._userSelectionClickedDelegate) {
            delete this._userSelectionClickedDelegate;
        }
        if (this._userSelectorClickDelegate) {
            delete this._userSelectorClickDelegate;
        }
        if (this._userSelectedDelegate) {
            delete this._userSelectedDelegate;
        }
        if (this._userSelectionCanceledDelegate) {
            delete this._userSelectionCanceledDelegate;
        }
        if (this._editTemplateLinkClickDelegate) {
            delete this._editTemplateLinkClickDelegate;
        }
        if (this._createTemplateLinkClickDelegate) {
            delete this._createTemplateLinkClickDelegate;
        }
        if (this._editAnonymousUserTemplateLinkClickDelegate) {
            delete this._editAnonymousUserTemplateLinkClickDelegate;
        }
        if (this._createAnonymousUserTemplateLinkClickDelegate) {
            delete this._createAnonymousUserTemplateLinkClickDelegate;
        }
        if (this._editEditModeTemplateLinkClickDelegate) {
            delete this._editEditModeTemplateLinkClickDelegate;
        }
        if (this._createEditModeTemplateLinkClickDelegate) {
            delete this._createEditModeTemplateLinkClickDelegate;
        }
        if (this._editChangePasswordTemplateLinkClickDelegate) {
            delete this._editChangePasswordTemplateLinkClickDelegate;
        }
        if (this._createChangePasswordTemplateLinkClickDelegate) {
            delete this._createChangePasswordTemplateLinkClickDelegate;
        }
        if (this._editChangePasswordQuestionAndAnswerTemplateLinkClickDelegate) {
            delete this._editChangePasswordQuestionAndAnswerTemplateLinkClickDelegate;
        }
        if (this._createChangePasswordQuestionAndAnswerTemplateLinkClickDelegate) {
            delete this._createChangePasswordQuestionAndAnswerTemplateLinkClickDelegate;
        }
        if (this._widgetEditorShowDelegate) {
            delete this._widgetEditorShowDelegate;
        }
        if (this._moreOptionsSectionClickDelegate) {
            delete this._moreOptionsSectionClickDelegate;
        }
        if (this._onWidgetEditorClosedDelegate) {
            delete this._onWidgetEditorClosedDelegate;
        }
        if (this._showChangePasswordProfilePageSelectorLinkClickDelegate) {
            delete this._showChangePasswordProfilePageSelectorLinkClickDelegate;
        }
        if (this._onWidgetEditorClosedDelegate) {
            delete this._showEditProfilePageSelectorLinkClickDelegate;
        }
        if (this._pageSelectorSelectionDoneDelegate) {
            delete this._pageSelectorSelectionDoneDelegate;
        }
        if (this._submitSuccessActionClickedDelegate) {
            delete this._submitSuccessActionClickedDelegate;
        }
        Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // refreshes the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this designer.
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }
        if (this._getCurrentView().ProfileTypeFullName) {
            this.get_profileTypeSelector().set_value(this._getCurrentView().ProfileTypeFullName);
        }
        this.get_cssClassTextBox().set_value(controlData.CssClass);
        var profileViewMode = controlData.ProfileViewMode;
        if (!profileViewMode) {
            profileViewMode = "ReadWrite";
            this._getView(this._readViewName).ShowAdditionalModesLinks = true;
        }
        if (profileViewMode === "Read" && this._getView(this._readViewName).ShowAdditionalModesLinks === true) {
            //The profile mode is read as the initial view is read but the radio should show ReadWrite
            profileViewMode = "ReadWrite";
        }
        var displayCurrentUser = controlData.DisplayCurrentUser;
        if (displayCurrentUser === null || displayCurrentUser === undefined) {
            displayCurrentUser = true;
        }
        var readViewTemaplateKey = this._getView(this._readViewName).TemplateKey;
        if (readViewTemaplateKey) {
            this.get_templateSelector().set_value(readViewTemaplateKey);
        }
        var writeViewTemaplateKey = this._getView(this._writeViewName).TemplateKey;
        if (writeViewTemaplateKey) {
            this.get_editModeTemplateSelector().set_value(writeViewTemaplateKey);
        }
        var changePasswordTemaplateKey = this._getView(this._changePasswordViewName).TemplateKey;
        if (changePasswordTemaplateKey) {
            this.get_changePasswordTemplateSelector().set_value(changePasswordTemaplateKey);
        }

        var changePasswordQuestionAndAnswerTemplateKey = this._getView(this._changePasswordQuestionAndAnswerViewName).TemplateKey;
        if (changePasswordQuestionAndAnswerTemplateKey) {
            this.get_changePasswordQuestionAndAnswerTemplateSelector().set_value(changePasswordQuestionAndAnswerTemplateKey);
        }

        var notLoggedReadModeTemplateKey = this._getView(this._readViewName).NotLoggedTemplateKey;
        if (notLoggedReadModeTemplateKey) {
            this.get_templateAnonymousUserSelector().set_value(notLoggedReadModeTemplateKey);
        }
        this._clickRadioChoice("UserSelection", displayCurrentUser);
        var openViewsInExternalPages = !!this._getView(this._readViewName).OpenViewsInExternalPages;
        if (openViewsInExternalPages) {
            this.get_openViewsInExternalPages().set_value(true);

            if (controlData.EditProfilePageTitle) {
                var label = this.get_selectedEditProfilePageLabel();
                var showSelectorLink = this.get_showEditProfilePageSelectorLink();
                var pageTitle = controlData.EditProfilePageTitle;
                jQuery(label).show().text(pageTitle);
                jQuery(showSelectorLink).html("<span class='sfLinkBtnIn'>" + this.get_clientLabelManager().getLabel('UserProfilesResources', 'ChangePage') + "</span>");
            }

            if (controlData.ChangePasswordPageTitle) {
                var label = this.get_selectedChangePasswordPageSelectorLabel();
                var showSelectorLink = this.get_showChangePasswordProfilePageSelectorLink();
                var pageTitle = controlData.ChangePasswordPageTitle;
                jQuery(label).show().text(pageTitle);
                jQuery(showSelectorLink).html("<span class='sfLinkBtnIn'>" + this.get_clientLabelManager().getLabel('UserProfilesResources', 'ChangePage') + "</span>");
            }
        }
        else {
            this.get_openViewsInExternalPages().set_value(false);
        }
        this._clickRadioChoice("WidgetMode", profileViewMode);
        var redirectAction = this._getView(this._writeViewName).SubmittingUserProfileSuccessAction;
        if (!redirectAction) {
            redirectAction = "ShowMessage";
        }
        if (!this._getView(this._writeViewName).SubmitSuccessMessage) {
            jQuery(this.get_profileUpdatedMessageTextArea()).val(this.get_clientLabelManager().getLabel('UserProfilesResources', 'ChangesAreSuccessfullySaved'));
        }
        this._clickRadioChoice("SubmittingUserProfileSuccessAction", redirectAction);

        if (redirectAction == "RedirectToPage" && controlData.RedirectOnSubmitPageTitle) {
            var label = this.get_selectedRedirectPageLabel();
            var showSelectorLink = this.get_showRedirectPageSelector();
            var pageTitle = controlData.RedirectOnSubmitPageTitle;
            jQuery(label).show().text(pageTitle);
            jQuery(showSelectorLink).html("<span class='sfLinkBtnIn'>" + this.get_clientLabelManager().getLabel('UserProfilesResources', 'ChangePage') + "</span>");
        }

        if (this._getView(this._readViewName).UserId && this._getView(this._readViewName).UserId != Telerik.Sitefinity.getEmptyGuid) {
            var selectedUserLabelText = this._getView(this._readViewName).SelectedUserFullName;
            if (selectedUserLabelText != null)
                jQuery("#selectedUserLabel").text(selectedUserLabelText).show();
            jQuery(this.get_userSelectorButton()).text("Change");
        }

        this._updateEditButtons();
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        var data = this.get_controlData();
        //If the user has selected to display a specific user but have not selected one select the displayCurrentUser option.
        if (!data.DisplayCurrentUser && (!data.UserId || data.UserId === Telerik.Sitefinity.getEmptyGuid)) {
            data.DisplayCurrentUser = true;
            this._clickRadioChoice("UserSelection", data.DisplayCurrentUser);
        }
        this._getView(this._readViewName).ProfileTypeFullName = this.get_profileTypeSelector().get_value();
        this._getView(this._writeViewName).ProfileTypeFullName = this.get_profileTypeSelector().get_value();
        this._getView(this._readViewName).TemplateKey = this.get_templateSelector().get_value();
        this._getView(this._readViewName).NotLoggedTemplateKey = this.get_templateAnonymousUserSelector().get_value();
        this._getView(this._writeViewName).TemplateKey = this.get_editModeTemplateSelector().get_value();
        this._getView(this._writeViewName).NotLoggedTemplateKey = this.get_templateAnonymousUserSelector().get_value();
        this._getView(this._changePasswordViewName).TemplateKey = this.get_changePasswordTemplateSelector().get_value();
        data.CssClass = this.get_cssClassTextBox().get_value();
        this._getView(this._writeViewName).SubmitSuccessMessage = jQuery(this.get_profileUpdatedMessageTextArea()).val();
        var openViewsInExternalPages = this.get_openViewsInExternalPages().get_value();
        if (openViewsInExternalPages) {
            this._getView(this._readViewName).OpenViewsInExternalPages = Boolean.parse(openViewsInExternalPages);
        }
    },

    /* --------------------------------- event handlers --------------------------------- */
    _loadHandler: function (sender, args) {
        this._createUserSelectorDialog();
        this._configureTemplateEditorDialog();
    },

    _viewModeClickHandler: function (sender, args) {
        var data = this.get_controlData();
        var value = sender.target.value;
        switch (sender.target.value) {
            case "Read":
                data.DetailViewName = data.ReadModeViewName;
                jQuery(this.get_openViewsInExternalPages().get_element()).hide();
                //jQuery(this.get_pageSelectorWrapper()).hide();
                data.DetailViewName = data.ReadModeViewName;
                this._getView(this._readViewName).ShowAdditionalModesLinks = false;
                jQuery(this.get_submittingUserProfileSuccessActionWrapper()).hide();
                data.ProfileViewMode = value;
                jQuery(this.get_pageSelectorsWrapper()).hide();
                jQuery(this.get_userSelectionWrapper()).show();

                this.get_templateSelector().set_title(this.get_clientLabelManager().getLabel('UserProfilesResources', 'Template'));
                jQuery(this.get_editModeTemplateSelectorWrapper()).show();
                jQuery(this.get_writeModeTemplateSelectorWrapper()).hide();
                jQuery(this.get_anonymousUserSelectorWrapper()).hide();
                jQuery(this.get_changePasswordTemplateSelectorWrapper()).hide();
                jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).hide();
                break;
            case "Write":
                jQuery(this.get_openViewsInExternalPages().get_element()).hide();
                //jQuery(this.get_pageSelectorWrapper()).hide();
                data.DetailViewName = data.WriteModeViewName;
                var selectedAction = this._getView(this._writeViewName).SubmittingUserProfileSuccessAction;
                if (selectedAction === "SwitchToReadMode") {
                    this._clickRadioChoice("SubmittingUserProfileSuccessAction", "ShowMessage");
                }
                jQuery(this.get_anonymousUserSelectorWrapper()).hide();
                jQuery(this.get_changePasswordTemplateSelectorWrapper()).hide();
                jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).hide();
                jQuery(this.get_switchToReadModeRadioWrapper()).hide();
                jQuery(this.get_submittingUserProfileSuccessActionWrapper()).show();
                data.ProfileViewMode = value;
                jQuery(this.get_pageSelectorsWrapper()).hide();
                jQuery(this.get_userSelectionWrapper()).hide();

                jQuery(this.get_showMessageRadioLabel()).text(this.get_clientLabelManager().getLabel('UserProfilesResources', 'ShowMessageAboveTheForm'));
                this.get_editModeTemplateSelector().set_title(this.get_clientLabelManager().getLabel('UserProfilesResources', 'Template'));
                jQuery(this.get_writeModeTemplateSelectorWrapper()).show();
                jQuery(this.get_editModeTemplateSelectorWrapper()).hide();
                jQuery(this.get_anonymousUserSelectorWrapper()).hide();
                jQuery(this.get_changePasswordTemplateSelectorWrapper()).hide();
                jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).hide();
                break;
            case "ReadWrite":
                jQuery(this.get_openViewsInExternalPages().get_element()).show();
                this._openViewsInExternalPagesClickHandler();
                data.DetailViewName = data.ReadModeViewName;
                this._getView(this._readViewName).ShowAdditionalModesLinks = true;
                jQuery(this.get_switchToReadModeRadioWrapper()).show();

                data.ProfileViewMode = "Read";
                jQuery(this.get_userSelectionWrapper()).hide();
                jQuery(this.get_anonymousUserSelectorWrapper()).show();
                jQuery(this.get_changePasswordTemplateSelectorWrapper()).show();
                jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).show();
                jQuery(this.get_showMessageRadioLabel()).text(this.get_clientLabelManager().getLabel('UserProfilesResources', 'StayInTheSameScreenDisplayMessage'));
                this.get_templateSelector().set_title(this.get_clientLabelManager().getLabel('UserProfilesResources', 'ReadModeTemplate'));
                this.get_editModeTemplateSelector().set_title(this.get_clientLabelManager().getLabel('UserProfilesResources', 'EditModeTemplate'));
                jQuery(this.get_anonymousUserSelectorWrapper()).show();
                jQuery(this.get_changePasswordTemplateSelectorWrapper()).show();
                jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).show();
                jQuery(this.get_editModeTemplateSelectorWrapper()).show();
                jQuery(this.get_writeModeTemplateSelectorWrapper()).show();
                var openViewsInExternalPages = Boolean.parse(this.get_openViewsInExternalPages().get_value());
                this.get_openViewsInExternalPages().set_value(openViewsInExternalPages);
                break;
            default:
                break;
        }
        dialogBase.resizeToContent();
    },

    _userSelectionClickHandler: function (sender, args) {
        var data = this.get_controlData();
        var value = sender.target.value;
        data.DisplayCurrentUser = value;
        if (Boolean.parse(value)) {
            jQuery("#userSelectZone").hide();
        }
        else {
            jQuery("#userSelectZone").show();
        }
        dialogBase.resizeToContent();
    },

    _submitSuccessActionClickedHandler: function (sender, args) {
        var value = sender.target.value;
        this._getView(this._writeViewName).SubmittingUserProfileSuccessAction = value;
        if (value === "ShowMessage") {
            jQuery(this.get_profileUpdatedMessageTextArea()).show();
        }
        else {
            jQuery(this.get_profileUpdatedMessageTextArea()).hide();
        }
        if (value === "RedirectToPage") {
            jQuery(this.get_redirectOnSubmitChoiceWrapper()).show();
        }
        else {
            jQuery(this.get_redirectOnSubmitChoiceWrapper()).hide();
        }
        dialogBase.resizeToContent();

    },

    _userSelectorClickHandler: function (sender, args) {
        this._userSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _userSelectedHandler: function (sender, args) {
        this._userSelectorDialog.dialog("close");
        jQuery("body > form").show();

        var selectedUserId = this.get_userSelector().get_selectedKeys()[0];
        var selectedItem = this.get_userSelector().getSelectedItems()[0];
        var selectedUserLabelText = String.format("{0} ({1})", selectedItem.DisplayName, selectedItem.Email);
        jQuery("#selectedUserLabel").text(selectedUserLabelText).show();
        jQuery(this.get_userSelectorButton()).text("Change");
        this._getView(this._readViewName).UserId = selectedUserId;
        this._getView(this._readViewName).Provider = selectedItem.ProviderName;

        dialogBase.resizeToContent();
    },

    _userSelectionCanceledHandler: function (sender, args) {
        this._userSelectorDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _createTemplateLinkClicked: function (sender, args) {
        this._selectedTemplateId = null;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ReadMode;
        this._controlType = this._getView(this._readViewName).ViewType;
        this._openCreateTemplateDialog();
    },

    _editTemplateLinkClicked: function (sender, args) {
        this._selectedTemplateId = this.get_templateSelector().get_value();
        this._controlType = this._getView(this._readViewName).ViewType;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ReadMode;
        this._openEditTemplateDialog();
    },

    _createAnonymousUserTemplateLinkClickHandler: function (sender, args) {
        this._selectedTemplateId = null;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.NoUserMode;
        this._controlType = this._getView(this._readViewName).ViewType;
        this._openCreateTemplateDialog("anonymousUser");
    },

    _editAnonymousUserTemplateLinkClickHandler: function (sender, args) {
        this._selectedTemplateId = this.get_templateAnonymousUserSelector().get_value();
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.NoUserMode;
        this._controlType = this._getView(this._readViewName).ViewType;
        this._openEditTemplateDialog();
    },

    _createEditModeTemplateLinkClickHandler: function (sender, args) {
        this._selectedTemplateId = null;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.WriteMode;
        this._controlType = this._getView(this._writeViewName).ViewType;
        this._openCreateTemplateDialog("editMode");
    },

    _editEditModeTemplateLinkClickHandler: function (sender, args) {
        this._selectedTemplateId = this.get_editModeTemplateSelector().get_value();
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.WriteMode;
        this._controlType = this._getView(this._writeViewName).ViewType;
        this._openEditTemplateDialog();
    },

    _createChangePasswordTemplateLinkClickHadnler: function (sender, args) {
        this._selectedTemplateId = null;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ChangePasswordMode;
        this._controlType = this._getView(this._changePasswordViewName).ViewType;
        this._openCreateTemplateDialog("changePasswordMode");
    },

    _createChangePasswordQuestionAndAnswerTemplateLinkClickHadnler: function (sender, args) {
        this._selectedTemplateId = null;
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ChangePasswordQuestionAndAnswerMode;
        this._controlType = this._getView(this._changePasswordQuestionAndAnswerViewName).ViewType;
        this._openCreateTemplateDialog("changeQuestionAndAnswerMode");
    },

    _editChangePasswordTemplateLinkClickHandler: function (sender, args) {
        this._selectedTemplateId = this.get_changePasswordTemplateSelector().get_value();
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ChangePasswordMode;
        this._controlType = this._getView(this._changePasswordViewName).ViewType;
        this._openEditTemplateDialog();
    },

    _editChangePasswordQuestionAndAnswerTemplateLinkClickHadnler: function (sender, args) {
        this._selectedTemplateId = this.get_changePasswordQuestionAndAnswerTemplateSelector().get_value();
        this._openedTemplateSelector = this.openedTemplateSelectorEnum.ChangePasswordQuestionAndAnswerMode;
        this._controlType = this._getView(this._changePasswordQuestionAndAnswerViewName).ViewType;
        this._openEditTemplateDialog();
    },

    _openViewsInExternalPagesClickHandler: function () {
        var openViewsInExternalPages = Boolean.parse(this.get_openViewsInExternalPages().get_value());
        jQuery(this.get_pageSelectorsWrapper()).toggle(openViewsInExternalPages);
        jQuery(this.get_submittingUserProfileSuccessActionWrapper()).toggle(!openViewsInExternalPages);
        jQuery(this.get_editModeTemplateSelectorWrapper()).toggle(!openViewsInExternalPages);
        jQuery(this.get_changePasswordTemplateSelectorWrapper()).toggle(!openViewsInExternalPages);
        jQuery(this.get_changePasswordQuestionAndAnswerTemplateSelectorWrapper()).toggle(!openViewsInExternalPages);
        dialogBase.resizeToContent();
    },

    _openEditTemplateDialog: function () {
        if (this._modifyWidgetTemplatePermission) {
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._editTemplateViewName);
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
            }
        } else {
            alert("You don't have the permissions to edit widgets templates.");
        }
    },

    _openCreateTemplateDialog: function (condition) {
        if (this._modifyWidgetTemplatePermission) {
            if (this._widgetEditorDialog) {
                var dialogUrl = String.format(this._widgetEditorDialogUrl, this._createTemplateViewName);
                if (condition) {
                    var url = new Sys.Uri(dialogUrl);
                    url.get_query().Condition = condition;
                    dialogUrl = url.toString();
                }
                this._widgetEditorDialog.set_navigateUrl(dialogUrl);
                $("body").removeClass("sfSelectorDialog");
                dialogBase.get_radWindow().maximize();
                this._widgetEditorDialog.show();
                this._widgetEditorDialog.maximize();
            }
        } else {
            alert("You don't have the permissions to create new widgets templates.");
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

                    this._getOpenedTemplateSelector().addListItem(widgetId, widgetName);
                    this._getOpenedTemplateSelector().set_value(widgetId);
                    this._updateEditButtons();
                }
                else if (arg.IsUpdated) {
                    var selectedChoices = this._getOpenedTemplateSelector()._get_selectedListItemsElements();
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
                    ItemTypeFullName: Telerik.Sitefinity.encodeWcfString(this.get_profileTypeSelector().get_value()),
                    BlackListControlTemplateEditor: true
                };
                frameHandle.createDialog(null, null, null, dialogBase, params, null);
            }
        }
    },

    _showEditProfilePageSelectorLinkClickHandler: function () {
        this._openedPageSelector = this.openedPageSelectorEnum.EditProfilePageSelector;

        var selectedItemId = this._getView(this._readViewName)['EditProfilePageId'];
        if (selectedItemId) {
            this.get_pageSelector().setSelectedItems([{ Id: selectedItemId }]);
        }

        this._pageSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _showChangePasswordProfilePageSelectorLinkClickHandler: function () {
        this._openedPageSelector = this.openedPageSelectorEnum.ChangePasswordProfilePageSelector;

        var selectedItemId = this._getView(this._readViewName)['ChangePasswordPageId'];
        if (selectedItemId) {
            this.get_pageSelector().setSelectedItems([{ Id: selectedItemId }]);
        }

        this._pageSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _showRedirectPageSelectorLinkClickHandler: function () {
        this._openedPageSelector = this.openedPageSelectorEnum.RedirectOnSubmitPageSelector;

        var selectedItemId = this._getView(this._writeViewName)['RedirectOnSubmitPageId'];
        if (selectedItemId) {
            this.get_pageSelector().setSelectedItems([{ Id: selectedItemId }]);
        }

        this._pageSelectorDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _pageSelectorSelectionDoneHandler: function () {
        this._pageSelectorDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        var selectedItem = this.get_pageSelector().getSelectedItems()[0];
        if (selectedItem) {
            var label = null;
            var showSelectorLink = null;
            var pageIdPropertyName = null;
            var viewName = null;
            switch (this._openedPageSelector) {
                case this.openedPageSelectorEnum.ChangePasswordProfilePageSelector:
                    label = this.get_selectedChangePasswordPageSelectorLabel();
                    showSelectorLink = this.get_showChangePasswordProfilePageSelectorLink();
                    pageIdPropertyName = 'ChangePasswordPageId';
                    viewName = this._readViewName;
                    break;
                case this.openedPageSelectorEnum.EditProfilePageSelector:
                    label = this.get_selectedEditProfilePageLabel();
                    showSelectorLink = this.get_showEditProfilePageSelectorLink();
                    pageIdPropertyName = 'EditProfilePageId';
                    viewName = this._readViewName;
                    break;
                case this.openedPageSelectorEnum.RedirectOnSubmitPageSelector:
                    label = this.get_selectedRedirectPageLabel();
                    showSelectorLink = this.get_showRedirectPageSelector();
                    pageIdPropertyName = 'RedirectOnSubmitPageId';
                    viewName = this._writeViewName;
                    break;
                default:
                    throw "Unsupported page selector mode: " + this._openedPageSelector;
            }
            jQuery(label).show().text(selectedItem.Title);
            jQuery(showSelectorLink).html("<span class='sfLinkBtnIn'>" + this.get_clientLabelManager().getLabel('UserProfilesResources', 'ChangePage') + "</span>");
            this._getView(viewName)[pageIdPropertyName] = selectedItem.Id;
        }
        dialogBase.resizeToContent();
    },

    /* --------------------------------- private methods --------------------------------- */

    _updateEditButtons: function () {
        var isValidValue = function (value) {
            return (value && !((value instanceof Array) && value.length == 0));
        };
        var showHideSelector = function (selector, editLink) {
            if (isValidValue(selector.get_value())) {
                jQuery(editLink).show();
            }
            else {
                jQuery(editLink).hide();
            }
        };

        showHideSelector(this.get_templateSelector(), this.get_editTemplateLink());
        showHideSelector(this.get_editModeTemplateSelector(), this.get_editEditModeTemplateLink());
        showHideSelector(this.get_templateAnonymousUserSelector(), this.get_editAnonymousUserTemplateLink());
        showHideSelector(this.get_changePasswordTemplateSelector(), this.get_editChangePasswordTemplateLink());
        showHideSelector(this.get_changePasswordQuestionAndAnswerTemplateSelector(), this.get_editChangePasswordQuestionAndAnswerTemplateLink());
    },

    _assignHandlers: function () {
        this._assignClickHandlers();
        this._pageSelectorSelectionDoneDelegate = Function.createDelegate(this, this._pageSelectorSelectionDoneHandler);
        this.get_pageSelector().add_doneClientSelection(this._pageSelectorSelectionDoneDelegate);
    },

    _assignClickHandlers: function () {
        this._userSelectorClickDelegate = Function.createDelegate(this, this._userSelectorClickHandler);
        jQuery(this.get_userSelectorButton()).click(this._userSelectorClickDelegate);
        this._viewModeClickedDelegate = Function.createDelegate(this, this._viewModeClickHandler);
        this._setRadioClickHandler("WidgetMode", this._viewModeClickedDelegate);
        this._userSelectionClickedDelegate = Function.createDelegate(this, this._userSelectionClickHandler);
        this._setRadioClickHandler("UserSelection", this._userSelectionClickedDelegate);
        this._submitSuccessActionClickedDelegate = Function.createDelegate(this, this._submitSuccessActionClickedHandler);
        this._setRadioClickHandler("SubmittingUserProfileSuccessAction", this._submitSuccessActionClickedDelegate);

        this._userSelectedDelegate = Function.createDelegate(this, this._userSelectedHandler);
        this._userSelectionCanceledDelegate = Function.createDelegate(this, this._userSelectionCanceledHandler);
        jQuery("#doneSelectingUserButton").click(this._userSelectedDelegate);
        jQuery("#cancelSelectingUserButton").click(this._userSelectionCanceledDelegate);

        this._showEditProfilePageSelectorLinkClickDelegate = Function.createDelegate(this, this._showEditProfilePageSelectorLinkClickHandler);
        jQuery(this.get_showEditProfilePageSelectorLink()).click(this._showEditProfilePageSelectorLinkClickDelegate);
        this._showChangePasswordProfilePageSelectorLinkClickDelegate = Function.createDelegate(this, this._showChangePasswordProfilePageSelectorLinkClickHandler);
        jQuery(this.get_showChangePasswordProfilePageSelectorLink()).click(this._showChangePasswordProfilePageSelectorLinkClickDelegate);

        this._showRedirectPageSelectorLinkClickDelegate = Function.createDelegate(this, this._showRedirectPageSelectorLinkClickHandler);
        jQuery(this.get_showRedirectPageSelector()).click(this._showRedirectPageSelectorLinkClickDelegate);

        this._editTemplateLinkClickDelegate = Function.createDelegate(this, this._editTemplateLinkClicked);
        jQuery(this.get_editTemplateLink()).click(this._editTemplateLinkClickDelegate);
        this._createTemplateLinkClickDelegate = Function.createDelegate(this, this._createTemplateLinkClicked);
        jQuery(this.get_createTemplateLink()).click(this._createTemplateLinkClickDelegate);

        this._editAnonymousUserTemplateLinkClickDelegate = Function.createDelegate(this, this._editAnonymousUserTemplateLinkClickHandler);
        jQuery(this.get_editAnonymousUserTemplateLink()).click(this._editAnonymousUserTemplateLinkClickDelegate);
        this._createAnonymousUserTemplateLinkClickDelegate = Function.createDelegate(this, this._createAnonymousUserTemplateLinkClickHandler);
        jQuery(this.get_createAnonymousUserTemplateLink()).click(this._createAnonymousUserTemplateLinkClickDelegate);

        this._editEditModeTemplateLinkClickDelegate = Function.createDelegate(this, this._editEditModeTemplateLinkClickHandler);
        jQuery(this.get_editEditModeTemplateLink()).click(this._editEditModeTemplateLinkClickDelegate);
        this._createEditModeTemplateLinkClickDelegate = Function.createDelegate(this, this._createEditModeTemplateLinkClickHandler);
        jQuery(this.get_createEditModeTemplateLink()).click(this._createEditModeTemplateLinkClickDelegate);

        this._editChangePasswordTemplateLinkClickDelegate = Function.createDelegate(this, this._editChangePasswordTemplateLinkClickHandler);
        jQuery(this.get_editChangePasswordTemplateLink()).click(this._editChangePasswordTemplateLinkClickDelegate);
        this._createChangePasswordTemplateLinkClickDelegate = Function.createDelegate(this, this._createChangePasswordTemplateLinkClickHadnler);
        jQuery(this.get_createChangePasswordTemplateLink()).click(this._createChangePasswordTemplateLinkClickDelegate);

        //delegate for create and edit change question and answer template
        this._editChangePasswordQuestionAndAnswerTemplateLinkClickDelegate = Function.createDelegate(this, this._editChangePasswordQuestionAndAnswerTemplateLinkClickHadnler);
        jQuery(this.get_editChangePasswordQuestionAndAnswerTemplateLink()).click(this._editChangePasswordQuestionAndAnswerTemplateLinkClickDelegate);
        this._createChangePasswordQuestionAndAnswerTemplateLinkClickDelegate = Function.createDelegate(this, this._createChangePasswordQuestionAndAnswerTemplateLinkClickHadnler);
        jQuery(this.get_createChangePasswordQuestionAndAnswerTemplateLink()).click(this._createChangePasswordQuestionAndAnswerTemplateLinkClickDelegate);

        this._openViewsInExternalPagesClickDelegate = Function.createDelegate(this, this._openViewsInExternalPagesClickHandler);
        this.get_openViewsInExternalPages().add_valueChanged(this._openViewsInExternalPagesClickDelegate);

        this._moreOptionsSectionClickDelegate = Function.createDelegate(this, this._moreOptionsSectionClickHandler);
        jQuery(this.get_moreOptionsLink()).click(this._moreOptionsSectionClickDelegate);
    },

    _configureTemplateEditorDialog: function () {
        this._assignTemplateEditorDialogHandlers();

        this._widgetEditorDialog = this.get_radWindowManager().GetWindowByName("widgetEditorDialog");
        if (this._widgetEditorDialog) {
            var dialogUrl = this._widgetEditorDialogUrl;
            this._widgetEditorDialog.set_navigateUrl(dialogUrl);
            this._widgetEditorDialog.add_close(this._onWidgetEditorClosedDelegate);
            this._widgetEditorDialog.add_pageLoad(this._widgetEditorShowDelegate);
        }
    },

    _moreOptionsSectionClickHandler: function () {
        jQuery(this.get_moreOptionsWrapper()).toggle();
        jQuery("#moreDesignSettings").toggleClass("sfExpandedSection");
        dialogBase.resizeToContent();
    },

    _assignTemplateEditorDialogHandlers: function () {
        if (this._widgetEditorShowDelegate === null) {
            this._widgetEditorShowDelegate = Function.createDelegate(this, this._onWidgetEditorShown);
        }

        if (this._onWidgetEditorClosedDelegate === null) {
            this._onWidgetEditorClosedDelegate = Function.createDelegate(this, this._onWidgetEditorClosed);
        }
    },

    _createUserSelectorDialog: function () {
        this._userSelectorDialog = jQuery(this.get_userSelectorWrapper()).dialog(
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

    },

    _getView: function (viewName) {
        var data = this.get_controlData();
        return data.ControlDefinition.Views[viewName];
    },

    _getCurrentView: function () {
        var data = this.get_controlData();
        return data.ControlDefinition.Views[data.DetailViewName];
    },

    //utility method to set radio group click handler
    _setRadioClickHandler: function (groupName, delegate) {
        jQuery(this.get_element()).find("input[name='" + groupName + "']").click(delegate);
    },

    //utility method to get a radio button option
    _getRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").get(0);
    },

    //utility method to to click a radio group option
    _clickRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").click();
    },

    _getOpenedTemplateSelector: function () {
        switch (this._openedTemplateSelector) {
            case this.openedTemplateSelectorEnum.ReadMode:
                return this.get_templateSelector();
            case this.openedTemplateSelectorEnum.WriteMode:
                return this.get_editModeTemplateSelector();
            case this.openedTemplateSelectorEnum.ChangePasswordMode:
                return this.get_changePasswordTemplateSelector();
            case this.openedTemplateSelectorEnum.NoUserMode:
                return this.get_templateAnonymousUserSelector();
            case this.openedTemplateSelectorEnum.ChangePasswordQuestionAndAnswerMode:
                return this.get_changePasswordQuestionAndAnswerTemplateSelector();
            default:
                throw 'openedTemplateSelector not set';
        }
    },

    /* --------------------------------- properties --------------------------------- */
    get_userSelector: function () {
        return this._userSelector;
    },
    set_userSelector: function (value) {
        this._userSelector = value;
    },

    get_profileTypeSelector: function () {
        return this._profileTypeSelector;
    },
    set_profileTypeSelector: function (value) {
        this._profileTypeSelector = value;
    },

    get_templateSelector: function () {
        return this._templateSelector;
    },
    set_templateSelector: function (value) {
        this._templateSelector = value;
    },

    get_templateAnonymousUserSelector: function () {
        return this._templateAnonymousUserSelector;
    },
    set_templateAnonymousUserSelector: function (value) {
        this._templateAnonymousUserSelector = value;
    },

    get_editModeTemplateSelector: function () {
        return this._editModeTemplateSelector;
    },
    set_editModeTemplateSelector: function (value) {
        this._editModeTemplateSelector = value;
    },

    get_changePasswordTemplateSelector: function () {
        return this._changePasswordTemplateSelector;
    },
    set_changePasswordTemplateSelector: function (value) {
        this._changePasswordTemplateSelector = value;
    },

    get_changePasswordQuestionAndAnswerTemplateSelector: function () {
        return this._changePasswordQuestionAndAnswerTemplateSelector;
    },
    set_changePasswordQuestionAndAnswerTemplateSelector: function (value) {
        this._changePasswordQuestionAndAnswerTemplateSelector = value;
    },

    get_pageSelector: function () {
        return this._pageSelector;
    },
    set_pageSelector: function (value) {
        this._pageSelector = value;
    },

    get_userSelectorWrapper: function () {
        return this._userSelectorWrapper;
    },
    set_userSelectorWrapper: function (value) {
        this._userSelectorWrapper = value;
    },

    get_userSelectorButton: function () {
        return this._userSelectorButton;
    },
    set_userSelectorButton: function (value) {
        this._userSelectorButton = value;
    },

    get_cssClassTextBox: function () {
        return this._cssClassTextBox;
    },
    set_cssClassTextBox: function (value) {
        this._cssClassTextBox = value;
    },

    get_profileUpdatedMessageTextArea: function () {
        return this._profileUpdatedMessageTextArea;
    },
    set_profileUpdatedMessageTextArea: function (value) {
        this._profileUpdatedMessageTextArea = value;
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

    get_editAnonymousUserTemplateLink: function () {
        return this._editAnonymousUserTemplateLink;
    },
    set_editAnonymousUserTemplateLink: function (value) {
        this._editAnonymousUserTemplateLink = value;
    },

    get_createAnonymousUserTemplateLink: function () {
        return this._createAnonymousUserTemplateLink;
    },
    set_createAnonymousUserTemplateLink: function (value) {
        this._createAnonymousUserTemplateLink = value;
    },

    get_editEditModeTemplateLink: function () {
        return this._editEditModeTemplateLink;
    },
    set_editEditModeTemplateLink: function (value) {
        this._editEditModeTemplateLink = value;
    },

    get_createEditModeTemplateLink: function () {
        return this._createEditModeTemplateLink;
    },
    set_createEditModeTemplateLink: function (value) {
        this._createEditModeTemplateLink = value;
    },

    get_editChangePasswordTemplateLink: function () {
        return this._editChangePasswordTemplateLink;
    },
    set_editChangePasswordTemplateLink: function (value) {
        this._editChangePasswordTemplateLink = value;
    },

    get_createChangePasswordTemplateLink: function () {
        return this._createChangePasswordTemplateLink;
    },
    set_createChangePasswordTemplateLink: function (value) {
        this._createChangePasswordTemplateLink = value;
    },

    get_editChangePasswordQuestionAndAnswerTemplateLink: function () {
        return this._editChangePasswordQuestionAndAnswerTemplateLink;
    },
    set_editChangePasswordQuestionAndAnswerTemplateLink: function (value) {
        this._editChangePasswordQuestionAndAnswerTemplateLink = value;
    },

    get_createChangePasswordQuestionAndAnswerTemplateLink: function () {
        return this._createChangePasswordQuestionAndAnswerTemplateLink;
    },
    set_createChangePasswordQuestionAndAnswerTemplateLink: function (value) {
        this._createChangePasswordQuestionAndAnswerTemplateLink = value;
    },

    get_moreOptionsLink: function () {
        return this._moreOptionsLink;
    },
    set_moreOptionsLink: function (value) {
        this._moreOptionsLink = value;
    },

    get_selectedEditProfilePageLabel: function () {
        return this._selectedEditProfilePageLabel;
    },
    set_selectedEditProfilePageLabel: function (value) {
        this._selectedEditProfilePageLabel = value;
    },

    get_selectedChangePasswordPageSelectorLabel: function () {
        return this._selectedChangePasswordPageSelectorLabel;
    },
    set_selectedChangePasswordPageSelectorLabel: function (value) {
        this._selectedChangePasswordPageSelectorLabel = value;
    },

    get_selectedRedirectPageLabel: function () {
        return this._selectedRedirectPageLabel;
    },
    set_selectedRedirectPageLabel: function (value) {
        this._selectedRedirectPageLabel = value;
    },

    get_showMessageRadioLabel: function () {
        return this._showMessageRadioLabel;
    },
    set_showMessageRadioLabel: function (value) {
        this._showMessageRadioLabel = value;
    },

    get_showEditProfilePageSelectorLink: function () {
        return this._showEditProfilePageSelectorLink;
    },
    set_showEditProfilePageSelectorLink: function (value) {
        this._showEditProfilePageSelectorLink = value;
    },

    get_showChangePasswordProfilePageSelectorLink: function () {
        return this._showChangePasswordProfilePageSelectorLink;
    },
    set_showChangePasswordProfilePageSelectorLink: function (value) {
        this._showChangePasswordProfilePageSelectorLink = value;
    },

    get_showRedirectPageSelector: function () {
        return this._showRedirectPageSelector;
    },
    set_showRedirectPageSelector: function (value) {
        this._showRedirectPageSelector = value;
    },

    get_moreOptionsWrapper: function () {
        return this._moreOptionsWrapper;
    },
    set_moreOptionsWrapper: function (value) {
        this._moreOptionsWrapper = value;
    },

    get_pageSelectorsWrapper: function () {
        return this._pageSelectorsWrapper;
    },
    set_pageSelectorsWrapper: function (value) {
        this._pageSelectorsWrapper = value;
    },

    get_pageSelectorWrapper: function () {
        return this._pageSelectorWrapper;
    },
    set_pageSelectorWrapper: function (value) {
        this._pageSelectorWrapper = value;
    },

    get_submittingUserProfileSuccessActionWrapper: function () {
        return this._submittingUserProfileSuccessActionWrapper;
    },
    set_submittingUserProfileSuccessActionWrapper: function (value) {
        this._submittingUserProfileSuccessActionWrapper = value;
    },

    get_anonymousUserSelectorWrapper: function () {
        return this._anonymousUserSelectorWrapper;
    },
    set_anonymousUserSelectorWrapper: function (value) {
        this._anonymousUserSelectorWrapper = value;
    },

    get_changePasswordTemplateSelectorWrapper: function () {
        return this._changePasswordTemplateSelectorWrapper;
    },
    set_changePasswordTemplateSelectorWrapper: function (value) {
        this._changePasswordTemplateSelectorWrapper = value;
    },

    get_changePasswordQuestionAndAnswerTemplateSelectorWrapper: function () {
        return this._changePasswordQuestionAndAnswerTemplateSelectorWrapper;
    },
    set_changePasswordQuestionAndAnswerTemplateSelectorWrapper: function (value) {
        this._changePasswordQuestionAndAnswerTemplateSelectorWrapper = value;
    },

    get_redirectOnSubmitChoiceWrapper: function () {
        return this._redirectOnSubmitChoiceWrapper;
    },
    set_redirectOnSubmitChoiceWrapper: function (value) {
        this._redirectOnSubmitChoiceWrapper = value;
    },

    get_userSelectionWrapper: function () {
        return this._userSelectionWrapper;
    },
    set_userSelectionWrapper: function (value) {
        this._userSelectionWrapper = value;
    },

    get_editModeTemplateSelectorWrapper: function () {
        return this._editModeTemplateSelectorWrapper;
    },
    set_editModeTemplateSelectorWrapper: function (value) {
        this._editModeTemplateSelectorWrapper = value;
    },

    get_writeModeTemplateSelectorWrapper: function () {
        return this._writeModeTemplateSelectorWrapper;
    },
    set_writeModeTemplateSelectorWrapper: function (value) {
        this._writeModeTemplateSelectorWrapper = value;
    },

    get_switchToReadModeRadioWrapper: function () {
        return this._switchToReadModeRadioWrapper;
    },
    set_switchToReadModeRadioWrapper: function (value) {
        this._switchToReadModeRadioWrapper = value;
    },

    get_openViewsInExternalPages: function () {
        return this._openViewsInExternalPages;
    },
    set_openViewsInExternalPages: function (value) {
        this._openViewsInExternalPages = value;
    },

    get_radWindowManager: function () {
        return this._radWindowManager;
    },
    set_radWindowManager: function (value) {
        this._radWindowManager = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }
};
Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.registerClass('Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);


Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedPageSelector = function () {
};
Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedPageSelector.prototype = {
    EditProfilePageSelector: 0,
    ChangePasswordProfilePageSelector: 1,
    RedirectOnSubmitPageSelector: 2
};
Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedPageSelector.registerEnum("Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedPageSelector");

Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.registerClass('Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);


Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedTemplateSelector = function () {
};
Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedTemplateSelector.prototype = {
    ReadMode: 0,
    WriteMode: 1,
    ChangePasswordMode: 2,
    NoUserMode: 3,
    ChangePasswordQuestionAndAnswerMode: 4
};
Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedTemplateSelector.registerEnum("Telerik.Sitefinity.Security.Web.UI.Designers.UserProfileDesigner.OpenedTemplateSelector");
