/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail");

detailFormView = null;

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView = function (element) {
    this._element = element;
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.initializeBase(this, [element]);
    this._widgetCommandDelegate = null;
    this._fieldCommandDelegate = null;
    this._widgetBarIds = null;
    this._buttonBarIds = null;
    this._bindOnLoad = null;
    this._binder = null;
    this._translationBinder = null;
    this._mediator = null;
    this._topWorkflowMenu = null;
    this._bottomWorkflowMenu = null;
    this._fieldControlIds = [];
    this._translationFieldControlIds = null;
    this._sectionIds = [];
    this._requireDataItemControlIds = [];
    this._compositeFieldControlIds = [];
    this._bulkEditFieldControlIds = [];
    this._commandFieldControlIds = [];
    this._languageSelector = null;
    this._languageList = null;
    this._translationLanguageSelector = null;
    this._showTranslationControl = null;
    this._hideTranslationControl = null;
    this._translationWrapper = null;
    this._sectionToolbarWrapper = null;
    this._isMultilingual = false;
    this._providerName = null;
    this._isDirty = false;
    this._multipleSaving = false;
    this._blankDataItem = null;
    this._isNew = false;
    this._duplicate = false;
    this._messageControl = null;
    this._windowManager = null;
    this._closeOnError = true;
    this._backToLabel = null;
    this._additionalCreateCommands = null;
    this._additionalCreateCommandsArray = [];
    this._formWasSaved = false;
    this._youHaveUnsavedChangesWantToLeavePage = null;
    this._alternativeTitle = null;
    this._serviceBaseUrl = null;

    this._defaultLanguage = null;

    this._cancelChangesServiceUrl = null;
    this._contentLocationPreviewUrl = null;
    //stores the command which is used to create the form/dialog
    this._createFormCommandName = null;
    //stores the command which is fired by the widgets in the toolbar
    this._widgetCommandName = false;

    // supported commands
    this._createCommandName = null;
    this._createAndUploadCommandName = null;
    this._editCommandName = null;
    this._saveCommandName = null;
    this._saveAndContinueCommandName = null;
    this._cancelCommandName = null;
    this._publishCommandName = null;
    this._deleteCommandName = null;
    this._historyCommandName = null;
    this._permissionsCommandName = null;
    this._previewCommandName = null;
    this._saveTempAndOpenLinkCommandName = null;
    this._deleteConfirmationMessage = null;
    this._deleteVersionCommandName = null;
    this._restoreVersionAsNewCommandName = null;
    this._createChildCommandName = null;
    this._duplicateCommandName = null;
    this._closeDialogCommandName = null;

    this._firstTextFieldControl = null;
    this._textFieldControlsCount = 0;
    this._htmlFieldControlsCount = 0;
    this._onkeyDownDelegate = null;
    this._setFocusToFirstTextFieldDelegate = null;

    this._focusDelegate = null;
    this._currentFocusedComponent = null;

    this._displayMode = null;
    this._backToAllItemsButton = null;
    this._backToAllItemsCommandButton = null;

    this.baseBackendUrl = null;

    this._initialTitle = null;

    //for permissions
    this._managerType = null;
    this._contentType = null;
    this._permissionsDialogUrl = null;
    this._itemHistoryVersion = null;
    this._dataItem = null;
    this._dataKey = null;
    this._titleElement = null;
    this._baseList = null;
    this._callParams = null;
    this._previousButton = null;
    this._nextButton = null;
    this._callerDataItem = null;
    this._currentDialog = null;

    this._windowCloseDelegate = null;
    this._checkForChanges = false;

    this._dataBindSuccessDelegate = null;
    this._translationBindSuccessDelegate = null;
    this._beforeDialogCloseDelegate = null;
    this._backToAllItemsDelegate = null;
    this._formSavedDelegate = null;
    this._formDeletedDelegate = null;
    this._formErrorDelegate = null;
    this._nextButtonClickDelegate = null;
    this._previousButtonClickDelegate = null;
    this._windowClosedDelegate = null;
    this._translationSelectorValueChangedDelegate = null;
    this._showTranslationControlClickDelegate = null;
    this._hideTranslationControlClickDelegate = null;
    this._languageSelectorValueChangedDelegate = null;
    this._deleteItemsDelegate = null;

    this._noWorkflowActionsDelegate = null;
    this._noWorkflowActionsDialogDelegate = null;
    this._cannotModifyDialogId = null;

    this._promptDialogNamesJson = null;
    this._promptDialogCommandsJson = null;
    this._promptDialogNames = null;
    this._promptDialogCommands = null;
    this._promptDialogs = [];

    this._backButtonClicked = false;
    //this is a marker flag, used while retrieving a blank item from a web service, and changes the dataBindSuccess behaviour
    this._loadingBlankItem = false;
    this._unlockMode = false;
    this._disableUnlocking = false;

    this._isEditable = true;
    this._isUnlockable = false;
    this._widgetCommandNamesToHideIfItemIsNotEditableArr = null;
    this._localization = "";
    // Command argument that is passed to createForm
    this._commandArgument = null;
    // Used to store temporary data when the dialog is closed
    this._dialogContext = null;

    // Gets a value indicating whether the "Back to items" link text is to be kept unchanged.
    this._suppressBackToButtonLabelModify = null;
    this._backLabelText = null; //the label "back to..." to appear on this view

    this._itemTemplate = null;
    this._getItemFromServer = true;
    this._homePageId = null;
    this._clientLabelManager = null;
    this._doNotUseContentItemContext = null;

    this._appLoaded = false;
    this._newItemContext = null;
    this._lastBindedItemKeyId = null;
    this._sectionToggleDelegate = null;

    this._isOpenedInEditingWindow = false;
    this._showMoreActionsWorkflowMenu = true;
    this._hideLanguageList = false;
    this._workflowMenuSuccessDelegate = null;
    this._revertUrl = null;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.callBaseMethod(this, "initialize");
        detailFormView = this;

        if (this._widgetBarIds) {
            this._widgetBarIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._widgetBarIds);
        }
        if (this._buttonBarIds) {
            this._buttonBarIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._buttonBarIds);
        }
        if (this._fieldControlIds) {
            this._fieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldControlIds);
        }
        if (this._translationFieldControlIds) {
            this._translationFieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._translationFieldControlIds);
        }
        if (this._sectionIds) {
            this._sectionIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._sectionIds);
        }
        if (this._blankDataItem) {
            this._blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._blankDataItem);
            this._blankDataItem.isPopulatedWithDefaultValues = true;
        }

        if (this._backToAllItemsButton) {
            this._backToAllItemsDelegate = Function.createDelegate(this, this._backToAllItems);
            $addHandler(this._backToAllItemsButton, "click", this._backToAllItemsDelegate);
        }

        if (this._blankDataItem) {// TODO: make this handle more than just id - idea is to have an empty object without the identity
            if (this._blankDataItem.hasOwnProperty('Id')) {
                delete this._blankDataItem.Id;
            }
            this._binder.setBlankDataItem(this._blankDataItem);
        }

        this._promptDialogNames = Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogNamesJson);
        this._promptDialogCommands = Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogCommandsJson);

        this._formSavedDelegate = Function.createDelegate(this, this._formSaved);
        this._formDeletedDelegate = Function.createDelegate(this, this._formDeleted);
        this._formErrorDelegate = Function.createDelegate(this, this._formError);

        this._binder.add_onSaved(this._formSavedDelegate);
        this._binder.add_onDeleted(this._formDeletedDelegate);
        this._binder.add_onError(this._formErrorDelegate);

        this._deleteItemsDelegate = Function.createDelegate(this, this._deleteItemsFinal);

        this._onkeyDownDelegate = Function.createDelegate(this, this._onkeyDownHandler);
        $addHandler(document, "keydown", this._onkeyDownDelegate);

        //If the dialog is set to be reloaded ot each show do not subscribe for show.
        if (!this._isOpenedInEditingWindow && dialogBase.get_radWindow() && !dialogBase.get_radWindow().get_reloadOnShow() && !this._setFocusToFirstTextFieldDelegate) {
            this._setFocusToFirstTextFieldDelegate = Function.createDelegate(this, this._setFocusToFirstTextField);
            dialogBase.get_radWindow().add_show(this._setFocusToFirstTextFieldDelegate);
        }

        this._focusDelegate = Function.createDelegate(this, this._tabFocusHandler);

        if (this._nextButton) {
            this._nextButtonClickDelegate = Function.createDelegate(this, this._nextButtonClick);
            $addHandler(this._nextButton, "click", this._nextButtonClickDelegate);
        }
        if (this._previousButton) {
            this._previousButtonClickDelegate = Function.createDelegate(this, this._previousButtonClick);
            $addHandler(this._previousButton, "click", this._previousButtonClickDelegate);
        }
        this._windowCloseDelegate = Function.createDelegate(this, this._windowCloseHandler);
        this._windowClosedDelegate = Function.createDelegate(this, this._windowClosedHandler);
        window.onbeforeunload = this._windowCloseDelegate;
        window.onunload = this._windowClosedDelegate;

        this._beforeDialogCloseDelegate = Function.createDelegate(this, this._beforeDialogCloseHandler);
        this._dataBindSuccessDelegate = Function.createDelegate(this, this._dataBindSuccess);
        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read) {
            $("body").addClass("sfItemPreview");
        }

        this._showTranslationControlClickDelegate = Function.createDelegate(this, this._showTranslationControlClickHandler);
        this._hideTranslationControlClickDelegate = Function.createDelegate(this, this._hideTranslationControlClickHandler);
        this._translationSelectorValueChangedDelegate = Function.createDelegate(this, this._translationSelectorValueChangedHandler);
        this._translationBindSuccessDelegate = Function.createDelegate(this, this._translationBindSuccessHandler);
        this._languageSelectorValueChangedDelegate = Function.createDelegate(this, this._languageSelectorValueChangedHandler);

        if (this._widgetCommandNamesToHideIfItemIsNotEditableArr) {
            this._widgetCommandNamesToHideIfItemIsNotEditableArr = Sys.Serialization.JavaScriptSerializer.deserialize(this._widgetCommandNamesToHideIfItemIsNotEditableArr);
        }
        if (this._localization) {
            this._localization = Sys.Serialization.JavaScriptSerializer.deserialize(this._localization);
        }

        if (this._topWorkflowMenu) {
            if (!this._backToAllItemsDelegate) {
                this._backToAllItemsDelegate = Function.createDelegate(this, this._backToAllItems);
            }
            $addHandler(this._topWorkflowMenu.get_cancelLink(), "click", this._backToAllItemsDelegate);

            if (!this._workflowMenuSuccessDelegate) {
                this._workflowMenuSuccessDelegate = Function.createDelegate(this, this._messageWorkflow_Success);
            }
            this._topWorkflowMenu._messageWorkflowSuccessDelegate = this._workflowMenuSuccessDelegate;
        }

        if (this._bottomWorkflowMenu) {
            if (!this._backToAllItemsDelegate) {
                this._backToAllItemsDelegate = Function.createDelegate(this, this._backToAllItems);
            }
            $addHandler(this._bottomWorkflowMenu.get_cancelLink(), "click", this._backToAllItemsDelegate);

            if (!this._workflowMenuSuccessDelegate) {
                this._workflowMenuSuccessDelegate = Function.createDelegate(this, this._messageWorkflow_Success);
            }
            this._bottomWorkflowMenu._messageWorkflowSuccessDelegate = this._workflowMenuSuccessDelegate;
        }

        if (!this._isOpenedInEditingWindow && dialogBase.get_radWindow()) {
            dialogBase.get_radWindow().DetailFormView = this;
        }

        this._noWorkflowActionsDelegate = Function.createDelegate(this, this._noWorkflowActionsHandler);
        if (this.get_topWorkflowMenu()) {
            this.get_topWorkflowMenu().add_onNoWorkflowActions(this._noWorkflowActionsDelegate);
        }
        this._noWorkflowActionsDialogDelegate = Function.createDelegate(this, this._noWorkflowActionsDialogHandler);

        this._sectionToggleDelegate = Function.createDelegate(this, this._sectionToggleHandler);
    },

    dispose: function () {

        if (this._widgetCommandDelegate) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetBarIds[wCounter]);
                if (widget !== null) {
                    widget.remove_command(this._widgetCommandDelegate);
                }
            }
            delete this._widgetCommandDelegate;
        }
        if (this._fieldCommandDelegate) {
            var commandFieldControlsCount = this._commandFieldControlIds.length;
            for (var fieldsCounter = 0; fieldsCounter < commandFieldControlsCount; fieldsCounter++) {
                var commandField = $find(this._commandFieldControlIds[fieldsCounter]);
                if (commandField !== null) {
                    commandField.remove_command(this._fieldCommandDelegate);
                }
            }
            delete this._fieldCommandDelegate;
        }

        if (this._dataBindSuccessDelegate) {
            delete this._dataBindSuccessDelegate;
        }

        if (this._translationBindSuccessDelegate) {
            delete this._translationBindSuccessDelegate;
        }
        if (this._languageSelectorValueChangedDelegate) {
            delete this._languageSelectorValueChangedDelegate;
        }

        if (!this._isOpenedInEditingWindow && this._setFocusToFirstTextFieldDelegate) {
            if (dialogBase.get_radWindow()) {
                dialogBase.get_radWindow().remove_show(this._setFocusToFirstTextFieldDelegate);
            }
            delete this._setFocusToFirstTextFieldDelegate;
        }

        if (this._onkeyDownDelegate) {
            $removeHandler(document, "keydown", this._onkeyDownDelegate);
            delete this._onkeyDownDelegate;
        }

        if (this._formSavedDelegate) {
            if (this._binder) {
                this._binder.remove_onSaved(this._formSavedDelegate);
            }
            delete this._formSaveDelegate;
        }

        if (this._formDeletedDelegate) {
            if (this._binder) {
                this._binder.remove_onDeleted(this._formDeletedDelegate);
            }
            delete this._formDeletedDelegate;
        }
        if (this._formErrorDelegate) {
            if (this._binder) {
                this._binder.remove_onError(this._formErrorDelegate);
            }
            delete this._formErrorDelegate;
        }

        if (this._focusDelegate) {
            delete this._focusDelegate;
        }

        if (this._nextButtonClickDelegate)
            delete this._nextButtonClickDelegate;
        if (this._previousButtonClickDelegate)
            delete this._previousButtonClickDelegate;

        if (this._backToAllItemsButton) {
            $removeHandler(this._backToAllItemsButton, "click", this._backToAllItemsDelegate);
            delete this._backToAllItemsDelegate;
            delete this._backToAllItemsButton;
        }

        if (this._currentDialog && this._beforeDialogCloseDelegate) {
            this._currentDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
            delete this._beforeDialogCloseDelegate;
        }

        if (this._windowCloseDelegate) {
            window.onbeforeunload = null;
            delete this._windowCloseDelegate;
        }
        if (this._windowClosedDelegate) {
            window.onunload = null;
            delete this._windowClosedDelegate;
        }

        if (this._translationSelectorValueChangedDelegate) {
            delete this._translationSelectorValueChangedDelegate;
        }
        if (this._showTranslationControlClickDelegate) {
            delete this._showTranslationControlClickDelegate;
        }
        if (this._hideTranslationControlClickDelegate) {
            delete this._hideTranslationControlClickDelegate;
        }

        if (this._noWorkflowActionsDelegate) {
            delete this._noWorkflowActionsDelegate;
        }

        if (this._noWorkflowActionsDialogDelegate) {
            delete this._noWorkflowActionsDialogDelegate;
        }

        if (this._topWorkflowMenu) {
            $removeHandler(this._topWorkflowMenu.get_cancelLink(), "click", this._backToAllItemsDelegate);
            if (this._backToAllItemsDelegate) {
                delete this._backToAllItemsDelegate;
            }
        }

        if (this._bottomWorkflowMenu) {
            $removeHandler(this._bottomWorkflowMenu.get_cancelLink(), "click", this._backToAllItemsDelegate);
            if (this._backToAllItemsDelegate) {
                delete this._backToAllItemsDelegate;
            }
        }

        if (this._sectionToggleDelegate) {
            delete this._sectionToggleDelegate;
        }

        if (this._workflowMenuSuccessDelegate) {
            delete this._workflowMenuSuccessDelegate;
        }

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods --------------------  */
    getPromptDialogByCommandName: function (commandName) {
        if (commandName in this._promptDialogCommands) {
            return this.getPromptDialogByName(this._promptDialogCommands[commandName]);
        }
        else {
            return null;
        }
    },
    getPromptDialogByName: function (name) {
        if (name in this._promptDialogNames) {
            return this._getPromptDialogById(this._promptDialogNames[name]);
        }
        else {
            return null;
        }
    },
    // Visualizes the PromptDialog on the screen absolutely centered.
    // Only the name argument is required, others are optional.
    // The 'handlerFunction' argument should be a function with two arguments, e.g. myHandler(sender, args) 
    showPromptDialogByName: function (name, title, message, handlerFunction) {
        var dialog = this.getPromptDialogByName(name);
        dialog.show_prompt(title, message, handlerFunction);
    },
    _getPromptDialogById: function (id) {
        if (!this._promptDialogs[id]) {
            this._promptDialogs[id] = $find(id);
        }

        return this._promptDialogs[id];
    },

    saveChanges: function (workflowOperation, contextBag) {

        this._isDirty = true;
        this._binder.set_workflowOperation(workflowOperation);
        this._binder.set_contextBag(contextBag);
        var validationSucceeded = this._binder.SaveChanges();
        if (validationSucceeded) {
            this._formWasSaved = true;
        }
        this._checkForChanges = true;
        // _lastBindedItemKeyId needs to be updated so that when item is updated 
        // and saved as draft, it can be rebinded in compare translations dialog
        this._lastBindedItemKeyId = null;
    },

    saveTemp: function (success, failure) {

        this._isDirty = true;
        this._binder.set_workflowOperation("SaveTemp");

        delegates = { Success: success, Failure: failure, Caller: this };
        var validationSucceeded = this._binder.SaveChanges(delegates);
        if (validationSucceeded) {
            this._formWasSaved = true;
        }
        this._checkForChanges = true;
        // _lastBindedItemKeyId needs to be updated so that when item is updated 
        // and saved as draft, it can be rebinded in compare translations dialog
        this._lastBindedItemKeyId = null;
    },

    _saveTempSuccess: function (target, data, webRequest) {
        this._dataItem = data;
        this.Caller.openLocationWindow(data);
        this.Caller.get_binder()._endProcessingHandler();
    },

    _saveTempFailure: function (result) {

        this.Caller.get_binder()._endProcessingHandler();
        this.Caller.get_binder()._errorHandler(result.Detail);

        alert(result.Detail);
    },


    asyncEndProcessing: function () {
        // TODO: remove these calls, once the IAsync thing is modified
        this._binder._endProcessingHandler();
        this._showHideToolbars(true);
    },

    viewCommand: function (command) {
        switch (command) {

            case this._permissionsCommandName:
                if (!this._verifyChangesOnExit()) break;
                this._openPermissionsDialog("permissions", this._binder.get_dataItem(), []);
                break;

            case this._historyCommandName:
                if (!this._verifyChangesOnExit()) break;
                var controlParams = new Object;
                controlParams["backLabelText"] = this._backLabelText;
                controlParams["parentId"] = this._callParams.parentId;
                var cmdArg = this._prepareCommandArgument();
                this._openDialog(command, this._doNotUseContentItemContext ? this.get_dataItem() : this.get_dataItem().Item, controlParams, cmdArg);
                break;
            case this._previewCommandName:
                this.saveTemp(this._saveTempSuccess, this._saveTempFailure);
                break;
            default:
                alert('Operation "' + command + '" is not supported at the moment.');
        }

        // TODO: remove these calls, once the IAsync thing is modified
        this.asyncEndProcessing();
    },

    openLocationWindow: function (data) {
        if (this._formWasSaved) {
            var dataItem;
            if (data && data.Item) {
                dataItem = data;
            }
            else {
                dataItem = this._dataItem;
            }

            if (!dataItem) {
                dataItem = this._binder._getJsonData();
            }

            var providerName = "";
            var serviceBaseUrl = this._binder.get_serviceBaseUrl();

            if (serviceBaseUrl.indexOf("providerName=") != -1) {
                var providerIndex = serviceBaseUrl.indexOf("providerName=");
                var tokenString = serviceBaseUrl.substring(providerIndex);
                var ampIndex = tokenString.indexOf("&") + providerIndex;
                var providerParam = serviceBaseUrl.substring(providerIndex, ampIndex);
                var valueIndex = providerParam.indexOf("=");
                providerName = providerParam.substring(valueIndex + 1);
            }
            var itemType = dataItem.ItemType;
            if (itemType == null) {
                if (serviceBaseUrl.indexOf("itemType=") != -1) {
                    var itemTypeIndex = serviceBaseUrl.indexOf("itemType=");
                    var tokenString = serviceBaseUrl.substring(itemTypeIndex);
                    var ampIndex = tokenString.indexOf("&") + itemTypeIndex;
                    var itemTypeParam = serviceBaseUrl.substring(itemTypeIndex, ampIndex);
                    var valueIndex = itemTypeParam.indexOf("=");
                    itemType = itemTypeParam.substring(valueIndex + 1);
                }
            }
            // see also: DashboardLogEntryViewModel - PreviewLink property is generated the same way
            var previewHandlerUrl = this.get_contentLocationPreviewUrl() + "?item_id=" + dataItem.Item.Id + "&item_type=" + itemType + "&item_provider=" + providerName;
            if (this._isMultilingual && this._binder.get_uiCulture()) {
                previewHandlerUrl += "&item_culture=" + this._binder.get_uiCulture();
            }

            window.open(previewHandlerUrl);
        }
    },

    getParentId: function () {
        var dataItem = this._binder.get_lastModifiedDataItem();
        if (dataItem && dataItem.hasOwnProperty('ParentId')) {
            return dataItem.ParentId;
        }
        return null;
    },

    _openPermissionsDialog: function (commandName, dataItem, key) {
        var dialog = this._windowManager.getWindowByName(commandName);

        if (dialog) {
            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);
            // have to set dialog url here, because it's too late on show!
            var url = this._permissionsDialogUrl; //dialog.get_navigateUrl();
            if (this._permissionsDialogUrl.indexOf("?") > -1) {
                url += "&dialogMode=" + "Template"
            }
            else {
                url += "?dialogMode=" + "Template";
            }
            var title = dataItem.Title.hasOwnProperty('Value') ? dataItem.Title.Value : dataItem.Title;
            var additionalParams =
            "&" + "securedObjectID=" + dataItem.Id +
            "&" + "title=" + title +
            "&" + "backLabelText=" + this._backLabelText +
            "&" + "securedObjectTypeName=" + this._contentType +
            "&" + "showPermissionSetNameTitle=true";
            if ((typeof (additionalParams) != "undefined") && (additionalParams != null) && (additionalParams != "")) {
                if (additionalParams.indexOf("&") != 0)
                    url += "&";
                url += additionalParams;
            }
            dialog.SetUrl(url);

            if (dialog.get_width() == 100 && dialog.get_height() == 100) {
                $("body").addClass("sfLoadingTransition");
            }
            dialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(dialog);
            // Ivan's note: RadWindow does not pass the units here, so we'll assume
            // that 100 x 100 is percents and maxize only in that case
            if (dialog.get_width() == 100 && dialog.get_height() == 100) {
                dialog.maximize();
                $("body").removeClass("sfLoadingTransition");
            }
        }
    },

    timer: function (handler, seconds) {

        return setInterval(handler, seconds * 1000);

    },

    deleteItem: function (closeDialog, noPrompt) {
        /// <summary>Delete one or more items</summary>
        /// <param name="key">Key of the item to delete</param>
        /// <param name="noPrompt">Determines whether to show a confirm or not.</param>

        this._isDirty = true;
        this._disableUnlocking = true;

        if (!noPrompt) {

            var showAdvancedDialog = false;

            var basicDialog = this.getPromptDialogByName("confirmDeleteSingle");
            var multipleItemsDeleteCommandButtonId = basicDialog._commands[1].ButtonClientId;
            var multipleItemsDeleteButton = jQuery('#' + multipleItemsDeleteCommandButtonId);
            multipleItemsDeleteButton.attr("style", "display: none !important");

            if (this._isMultilingual) {
                var dataItem = this.get_dataItem();
                if (dataItem.Item && typeof (dataItem.Id) === "undefined") {
                    dataItem = dataItem.Item;
                }
                if (dataItem.AvailableLanguages) {
                    var availableLanguagesCount = dataItem.AvailableLanguages.length;
                    if (jQuery.inArray("", dataItem.AvailableLanguages) > -1) availableLanguagesCount--;
                    showAdvancedDialog = (availableLanguagesCount > 1);
                } else {
                    showAdvancedDialog = false;
                }
            }

            if (showAdvancedDialog == true) {
                var dialog = this.getPromptDialogByName("confirmDelete");

                //Insert the current language in the message
                var singleLanguageDeleteCommand = null;
                for (var i = 0; i < dialog._commands.length; i++) {
                    if (dialog._commands[i].CommandName === "language") {
                        singleLanguageDeleteCommand = dialog._commands[i];
                        break;
                    }
                }

                var commandButtonId = singleLanguageDeleteCommand.ButtonClientId;
                var button = $('#' + commandButtonId).get(0);
                var textContainer = button.children[0];
                if (!this._confirmDeleteMessage) {
                    this._confirmDeleteMessage = textContainer.innerHTML;
                }

                var uiCulture = this.get_uiCulture ? this.get_uiCulture() : this._binder.get_uiCulture();
                textContainer.innerHTML = String.format(this._confirmDeleteMessage, uiCulture.toUpperCase());

                dialog.show_prompt('', '', this._deleteItemsDelegate);

                return true;
            } else {
                var dialog = this.getPromptDialogByName("confirmDeleteSingle");
                dialog.show_prompt('', '', this._deleteItemsDelegate);
            }

        }
        else {
            this._binder.DeleteItem();
        }
    },

    _deleteItemsFinal: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName == 'cancel') {
            this.asyncEndProcessing();
            return;
        }

        var deleteCurrentLanguageOnly = (commandName == 'language');

        var lang = null;
        if (deleteCurrentLanguageOnly == true) {
            lang = this._binder.get_uiCulture();
        }

        this._binder.DeleteItem(lang, this._itemHistoryVersion);
    },

    dataBind: function (dataItem, key) {
        var clientManager = this._binder.get_manager();
        var urlParams = [];
        var serviceBaseUrl = this._binder.get_serviceBaseUrl();
        this._binder.set_duplicate(this._duplicate);

        if (this._providerName != null) {
            urlParams['provider'] = this._providerName;
        }

        if (dataItem) {
            this._isUnlockable = dataItem.IsUnlockable;
            if (dataItem.ProviderName) {
                urlParams['provider'] = dataItem.ProviderName;

                if (serviceBaseUrl.indexOf("providerName=") != -1) {
                    var providerIndex = serviceBaseUrl.indexOf("providerName=");
                    var tokenString = serviceBaseUrl.substring(providerIndex);
                    var ampIndex = tokenString.indexOf("&") + providerIndex;
                    var providerParam = serviceBaseUrl.substring(providerIndex, ampIndex);

                    var valueIndex = providerParam.indexOf("=");
                    var providerSetOnServer = providerParam.substring(valueIndex + 1);

                    if (providerSetOnServer != urlParams['provider']) {
                        var newProviderParam;
                        if (providerSetOnServer == "") {
                            newProviderParam = providerParam + urlParams['provider'];
                        }
                        else {
                            newProviderParam = providerParam.replace(providerSetOnServer, urlParams['provider']);
                        }

                        serviceBaseUrl = serviceBaseUrl.replace(providerParam, newProviderParam);
                        if (this._binder._serviceBaseUrl) {
                            this._binder._serviceBaseUrl = serviceBaseUrl;
                        }
                    }
                }
                else {
                    urlParams['providerName'] = dataItem.ProviderName;
                }
                this._binder._provider = dataItem.ProviderName;
                if (this._binder.hasOwnProperty('_providerName')) {
                    this._binder._providerName = dataItem.ProviderName;
                }
                this._providerName = dataItem.ProviderName;
            }
        }

        if (this._itemHistoryVersion != null) {
            urlParams['version'] = this._itemHistoryVersion;
        }

        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write && !this._duplicate) {
            urlParams["checkOut"] = "true";
        }

        if (this._duplicate) {
            urlParams["duplicate"] = "true";
        }

        if (this._itemTemplate)
            urlParams['itemTemplate'] = this._itemTemplate;

        var keys = [];
        if (key == null) {
            urlParams['createNew'] = true;
            keys.push("0");
        }
        else {
            for (var keyName in key) {
                var val = key[keyName];
                // TODO: add more checks
                if (typeof val != "function")
                    keys.push(key[keyName]);
            }
        }

        if (!this._callParams["skipLoadingItemFromServer"] && this._getItemFromServer) {
            clientManager.InvokeGet(serviceBaseUrl, urlParams, keys, this._dataBindSuccessDelegate, this._dataBindFailure, this);
        } else {
            this._dataBindSuccess(this, dataItem);
            this._getItemFromServer = true;
        }
    },

    createForm: function (commandName, dataItem, self, dialog, params, key, commandArgument, isOpenedInEditingWindow, showMoreActionsWorkflowMenu, hideLanguageList) {
        this._dataKey = key;
        this._baseList = self;
        this._createFormCommandName = commandName;
        this._isDirty = false;
        this._duplicate = false;
        this._checkForChanges = false;
        this._callerDataItem = dataItem;
        this._binder._dataItem = dataItem;
        this._callParams = params;
        this._currentDialog = dialog;
        this._backButtonClicked = false;
        this._formWasSaved = false;
        if (isOpenedInEditingWindow !== undefined) {
            this._isOpenedInEditingWindow = isOpenedInEditingWindow;
        }
        if (showMoreActionsWorkflowMenu !== undefined) {
            this._showMoreActionsWorkflowMenu = showMoreActionsWorkflowMenu;
        }
        if (hideLanguageList !== undefined) {
            this._hideLanguageList = hideLanguageList;
        }

        this._disableUnlocking = false;
        this._isEditable = params.IsEditable;
        this._setDisplayOfEditWidget(this._isEditable);
        if (this._dialogContext) {
            commandArgument = this._dialogContext;
            this._dialogContext = null;
        }
        this._commandArgument = commandArgument;
        if (params.VersionId) {
            this._itemHistoryVersion = params.VersionId;
        }
        else {
            this._itemHistoryVersion = null;
        }

        if (this._additionalCreateCommands) {
            this._additionalCreateCommandsArray = this._additionalCreateCommands.split(',');
        }

        var fieldControlsCount = this._fieldControlIds.length;
        if (fieldControlsCount > 0) {
            while (fieldControlsCount--) {
                var fieldControl = $find(this._fieldControlIds[fieldControlsCount]);
                var fieldControlType = Object.getType(fieldControl);
                if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.ISelfExecutableField)) {
                    if (fieldControl && typeof (fieldControl.set_provider) === 'function') {
                        fieldControl.set_provider(this._baseList.getBinder().get_provider());
                    }
                }
                if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.IParentSelectorField)) {
                    if (fieldControl && typeof (fieldControl.set_provider) === 'function' && typeof (fieldControl.dataBind) === 'function') {
                        fieldControl.set_provider(this._baseList.getBinder().get_provider());
                        fieldControl.dataBind();
                    }
                }
                if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider)) {
                    if (fieldControl && typeof (fieldControl.set_providerName) === 'function') {
                        fieldControl.set_providerName(this._baseList.getBinder().get_provider());
                    }
                }
                if (fieldControl && typeof (fieldControl.set_isPreviewMode) === 'function') {
                    fieldControl.set_isPreviewMode(commandName == 'preview');
                }
                if (fieldControlType.implementsInterface(Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture)) {
                    if (fieldControl && typeof (fieldControl.set_uiCulture) === 'function' && commandArgument && commandArgument.language) {
                        fieldControl.set_uiCulture(commandArgument.language);
                    }
                }
            }
        }

        this.reset();
        // those are shown when bound
        if (this.get_bottomWorkflowMenu()) {
            this.get_bottomWorkflowMenu().hide();
        }
        if (this.get_topWorkflowMenu()) {
            this.get_topWorkflowMenu().hide();
        }
        this._bindSectionsDoToggleEvent();
        this._setupDialogMode(commandName, dataItem, key, commandArgument);
        this._hideDynamicContentInstructionalText(commandName);
        // bind bulk edit field controls
        for (var i = 0, length = this._bulkEditFieldControlIds.length; i < length; i++) {
            var fieldControl = $find(this._bulkEditFieldControlIds[i]);
            if (fieldControl) {

                if (fieldControl && typeof (fieldControl.set_provider) === 'function') {
                    fieldControl.set_provider(this._baseList.getBinder().get_provider());
                }
                fieldControl.get_itemsBinder().set_uiCulture(this._binder.get_uiCulture());
                fieldControl.dataBind(this._baseList.get_selectedItems());
            }
        }

        this._setFocusToFirstTextField();
        this._raiseFormCreated(this._isNew,
                               this._dataKey,
                               this._isNew ? null : dataItem,
                               commandName,
                               params,
                               null,
                               commandArgument);

        if (!this._isOpenedInEditingWindow) {
            this._currentDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
            this._currentDialog.add_beforeClose(this._beforeDialogCloseDelegate);
        }
        if (this._hideLanguageList) {
            if (this.get_languageList()) {
                this.get_languageList().set_visible(false);
            }
            if (this.get_showTranslationControl()) {
                $(this.get_showTranslationControl()).hide();
            }
        }
    },

    _setupDialogMode: function (commandName, dataItem, key, commandArgument) {
        var radWindow = dialogBase.get_radWindow();
        var isEditMode = this._determineEditMode(commandArgument, commandName, dataItem);

        var serviceUrl = this._replacePropertyValues(this._callParams, this._serviceBaseUrl);
        this._translationBinder.set_serviceBaseUrl(serviceUrl);
        this._binder.set_serviceBaseUrl(serviceUrl);

        this._configureLanguageVersionOperationMode(commandName, commandArgument);
        var jTitleElement = jQuery(this.get_titleElement());
        var baseList = this.get_baseList();
        if (baseList && baseList.getFormattedBackLabel)
            this._backLabelText = baseList.getFormattedBackLabel(jTitleElement.text());

        //Edit a data item
        if (isEditMode) {
            this._isNew = false;

            //Check for duplication mode.
            if (commandName == this._duplicateCommandName) {
                this._duplicate = true;
                this._isNew = true;
            }

            this.dataBind(dataItem, key);
        }
            //Create
        else {
            if (this._baseList)
                this._providerName = this._baseList.getBinder().get_provider();
            else {
                try {
                    this._providerName = window.parent.GetBrowseAndEditManager().get_toolbarArgs().get_providerName();
                    this._binder.set_providerName(this._providerName);
                } catch (e) { }
            }
            this._isNew = true;
            if (this._blankDataItem != null) {
                // Reset the default publication date
                if (this._blankDataItem.PublicationDate)
                    this._blankDataItem.PublicationDate = new Date();
                this._setupCreateMode();
            }
            else {
                this._loadingBlankItem = true;
                this.dataBind(null, null);
            }
        }

        if (!this._isOpenedInEditingWindow &&
            (typeof (radWindow) != "undefined") &&
            (typeof (radWindow._sfArgs) != "undefined") &&
            (typeof (radWindow._sfArgs.get_params) !== "undefined") &&
            (typeof (radWindow._sfArgs.get_params()["backLabelText"]) != "undefined") &&
            (radWindow._sfArgs.get_params()["backLabelText"] != null)) {
            this._setBackButtonText(radWindow._sfArgs.get_params()["backLabelText"]);
        }

        // bind translation language selector if we are opening the form view for inserting new translation
        if (commandArgument && commandArgument.languageMode == this._createCommandName) {
            var that = this;
            setTimeout(function () { that._bindTranslationLanguageSelector(); }, 700);
        }
    },

    _determineEditMode: function (commandArgument, commandName, dataItem) {
        // HACK: When commandName == this._createCommandName, it is considered that isEditMode = true
        // This is for the case when openning a content item for edit and creating a translation from there.
        var isEditMode = (!this._isCreateCommand(commandName) || commandName == this._createCommandName) && !!dataItem;
        if (this._isMultilingual && commandArgument) {
            isEditMode = isEditMode || commandArgument.languageMode == "edit" || commandArgument.mode && commandArgument.mode == "edit";
        }
        return isEditMode;
    },

    _isCreateCommand: function (commandName) {
        if (commandName == this._createCommandName
                || commandName == this._createChildCommandName
                || (this._additionalCreateCommandsArray !== null && Array.indexOf(this._additionalCreateCommandsArray, commandName) >= 0)
                || commandName.substring(0, 26) == "insert_command_sf_ec_prdct") {
            return true;
        }
        return false;
    },

    _configureLanguageVersionOperationMode: function (commandName, commandArgument) {
        if (this._isMultilingual) {
            //Hack for hiding the language selector if the dialog was opened for adding a language version.
            //The language list in the grid is always sending "edit" command (in order to open the correct dialog).
            //This means that if the commandName is "edit" but the commandArgument.languageMode is "create" then the dialog is
            //adding another language version.
            var isToHideLanguageSelector = (commandName === this._editCommandName || (commandArgument && commandArgument.language));
            commandArgument = this._getLanguageCommandArgumentOrDefault(commandName, commandArgument);
            this._applyContentCultureToWorkflowMenus(commandArgument.language);
            var oldLang = this._binder.get_uiCulture();

            this._binder.set_uiCulture(commandArgument.language);

            this._setLanguageSpecificTitle(commandName, commandArgument);
            switch (commandArgument.languageMode) {
                case this._createCommandName:
                    if (oldLang && oldLang !== commandArgument.language) {
                        this._unlockItemCulture(oldLang);
                    }

                    this._binder.set_fallbackMode("NoFallback");
                case this._duplicateCommandName:
                    jQuery(this._sectionToolbarWrapper).hide();
                    if (this.get_languageSelector()) {
                        this.get_languageSelector().enable();
                        if (isToHideLanguageSelector) {
                            this.get_languageSelector().hide();
                        }
                    }
                    this._showTranslationControlClickHandler();
                    if (jQuery(".sfTranslateFromLang .sfTxtLbl").length > 0) {
                        jQuery("body").addClass("sfCompareTranslationDialog sfHasLanguageBarDialog");
                    }
                    else {
                        jQuery("body").removeClass("sfHasLanguageBarDialog sfCompareTranslationDialog");
                    }
                    break;
                case this._editCommandName:
                    if (oldLang && oldLang !== commandArgument.language) {
                        this._unlockItemCulture(oldLang);
                    }

                    jQuery(this._sectionToolbarWrapper).show();
                    if (this.get_languageSelector()) {
                        this.get_languageSelector().disable();
                    }
                    this._hideTranslationControlClickHandler();
                    if (this._contentType.indexOf("Telerik.Sitefinity.Pages") != -1) {
                        jQuery("body").removeClass("sfHasLanguageBarDialog sfCompareTranslationDialog");
                    }
                    else {
                        jQuery("body").addClass("sfHasLanguageBarDialog").removeClass("sfCompareTranslationDialog");
                    }

                    this._binder.set_fallbackMode("NoFallback");
                    break;
            }
        }
    },

    _unlockItemCulture: function (oldLang) {
        var url = this._serviceBaseUrl;
        var idx = url.indexOf(".svc");
        if (idx != -1)
            url = url.substr(0, url.indexOf(".svc") + 4) + '/';
        if (url.charAt(url.length - 1) != '/')
            url += '/';
        var realDataItem = this._doNotUseContentItemContext ? this._dataItem : this._dataItem.Item;
        $.ajax({
            method: "DELETE",
            url: url + "temp/" + realDataItem.Id + '/' + '?provider=' + this._providerName + "&item_type=" + this._contentType,
            headers: {
                SF_UI_CULTURE: oldLang
            }
        }).fail(function(msg) {
            console.log("Error discarding draft: " + msg);
        });
    },

    _getLanguageCommandArgumentOrDefault: function (commandName, commandArgument) {
        if (!commandArgument) {
            commandArgument = {};
        }
        if (!commandArgument.languageMode) {
            if (commandName == this._duplicateCommandName) {
                commandArgument.languageMode = this._duplicateCommandName;
            }
            else if (!this._isCreateCommand(commandName)) {
                commandArgument.languageMode = this._editCommandName;
            }
            else {
                commandArgument.languageMode = this._createCommandName;
            }
        }
        if (!commandArgument.language) {
            //Attempt to get the selected language in the list that have opened the dialog.
            if (commandArgument.languageMode == this._createCommandName) {
                // if you create a new item the language is not passed and it should be taken from the list
                if (this.get_baseList() && this.get_baseList().get_uiCulture && this.get_baseList().get_uiCulture()) {
                    commandArgument.language = this.get_baseList().get_uiCulture();
                }
            }
            else {
                // if you edit an item you should get the language from the selector because the user can change it meanwhile
                if (this._languageSelector) {
                    commandArgument.language = this._languageSelector.get_value();
                }
            }
            if (!commandArgument.language) {
                commandArgument.language = this._defaultLanguage;
            }
        }
        return commandArgument;
    },

    _setLanguageSpecificTitle: function (commandName, commandArgument) {
        if (commandArgument && commandArgument.language) {
            var jTitleElement = jQuery(this.get_titleElement());
            if (!this._initialTitle) {
                this._initialTitle = jTitleElement.text();
            }
            if (commandArgument.languageMode === "create" && this._alternativeTitle) {
                jTitleElement.html(this._alternativeTitle + " (" + commandArgument.language.toUpperCase() + ")");
            }
            else {
                jTitleElement.text(this._initialTitle + " (" + commandArgument.language.toUpperCase() + ")");
            }
        }
    },

    //shows/hides widgets according to the editablity  of the viewed item
    _setDisplayOfEditWidget: function (isEditable) {
        if (this._widgetBarIds) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widgetBar = $find(this._widgetBarIds[wCounter]);
                if (widgetBar !== null) {
                    var widgets = widgetBar.getAllWidgets();
                    if (widgets != null) {
                        for (var i = 0; i < widgets.length; i++) {
                            var isRestricted = false;
                            if (typeof (widgets[i].get_commandName) != "undefined") {
                                var commandName = widgets[i].get_commandName();
                                for (var restrictedCommand = 0; restrictedCommand < this._widgetCommandNamesToHideIfItemIsNotEditableArr.length; restrictedCommand++) {
                                    if (this._widgetCommandNamesToHideIfItemIsNotEditableArr[restrictedCommand] == commandName) {
                                        isRestricted = true;
                                        break;
                                    }
                                }
                            }
                            if (!isEditable && isRestricted) {
                                widgets[i].set_visible(false);
                            }
                        }
                    }
                }
            }
        }
        //$find($find(this._sectionIds[0]).get_fieldControlIds()[0]).constructor.getName() ==Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl
        //find note controls and lock them too:
        //go over the sections defined for this contro
        for (var s = 0; s < this._sectionIds.length; s++) {
            var section = $find(this._sectionIds[s]);
            if (section != null) {
                //get field controls on the secion
                var ctlIds = section.get_fieldControlIds();
                for (var c = 0; c < ctlIds.length; c++) { // c++ ;-)
                    var fieldcontrol = $find(ctlIds[c]);
                    if ((fieldcontrol != null) && (fieldcontrol.constructor != null) && (typeof (fieldcontrol.constructor) != "undefined")) {
                        var controlNamespace = fieldcontrol.constructor.getName();
                        switch (controlNamespace) {
                            case "Telerik.Sitefinity.Versioning.Web.UI.VersionNoteControl":
                                this._toggleControlDisplay(fieldcontrol.get_buttonArea(), isEditable);
                                //this._toggleControlDisplay(fieldcontrol.get_deleteButton(), isEditable);
                                break;
                        }
                    }
                }
            }
        }
    },

    _toggleControlDisplay: function (ctlElement, bIsDisplayed) {
        if (ctlElement != null)
            ctlElement.style.display = ((bIsDisplayed) ? "block" : "none");
    },

    _enforceContentLifecycle: function (dataItem) {
        /// <summary>Makes sure the item can not be edited if it is locked and the currently logged in user cannot unlock it.</summary>
        /// <param name="dataItem">Item the form is bound to</param>
        /// <remarks>If there is an error, the form will be closed.</remarks>        

        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write && dataItem && dataItem.LifecycleStatus) {
            var mustCloseWindow = false;
            if (dataItem.LifecycleStatus.ErrorMessage) {
                // can't use this._messageControl.showNegativeMessage(), because it is not modal and 
                // we need the user to see the message
                this._formError();
                alert(dataItem.LifecycleStatus.ErrorMessage);
                mustCloseWindow = true;
            }
            else if (dataItem.LifecycleStatus.IsLocked && !dataItem.LifecycleStatus.IsLockedByMe) {
                this._formError();
                this._handleLockedItem(dataItem.Item.Id, this._isUnlockable, dataItem.LifecycleStatus);
                mustCloseWindow = true;
            }

            if (mustCloseWindow) {
                if (dialogBase)
                    dialogBase.closeAndRebind();
                else
                    window.close();
            }
        }
    },

    _handleLockedItem: function (Id, IsUnlockable, Lifecycle) {
        var since = Lifecycle.LockedSince;
        since = since ? " since " + since.sitefinityLocaleFormat() : "";

        if (IsUnlockable) {
            var msg = this._clientLabelManager.getLabel('Labels', 'ItemIsLockedFormat') + "\n\n" + this._clientLabelManager.getLabel('Labels', 'ItemUnlockQuestion');
            if (confirm(String.format(msg, Lifecycle.LockedByUsername, since))) {
                var url = this._serviceBaseUrl;
                var idx = url.indexOf(".svc");
                if (idx != -1)
                    url = url.substr(0, url.indexOf(".svc") + 4) + '/';
                if (url.charAt(url.length - 1) != '/')
                    url += '/';

                // url, urlParams, keys, successDelegate, failureDelegate, caller, context
                // /temp/{contentId}/?providerName={providerName}&force={force}

                var self = this;
                var callback = function () {
                    window.top.location.reload(true);
                };
                setTimeout(function () {
                     self._binder._manager.InvokeDelete(
                         url,
                         { "providerName": self._providerName, "provider": self._providerName, "force": "true", "item_type": self._contentType },
                        ["temp", Id],
                        callback,
                        self._binder._errorDelegate,
                        self);
                }, 1);
            }
        }
        else {
            var msg = this._clientLabelManager.getLabel('Labels', 'ItemIsLockedFormat') + "\n\n" + this._clientLabelManager.getLabel('Labels', 'ItemUnlockCloseAlert');
            alert(String.format(msg, Lifecycle.LockedByUsername, since));
        }
    },

    //setups the binding of the form in create mode
    _setupCreateMode: function (itemContext) {
        var clonedBlankDataItem = Telerik.Sitefinity.cloneObject(this._blankDataItem);
        var newItemContext = new Object();
        if (itemContext) {
            newItemContext = itemContext;
        }
        else {
            if (this._doNotUseContentItemContext) {
                newItemContext = clonedBlankDataItem;
            }
            else {
                newItemContext['Item'] = clonedBlankDataItem;
            }
        }

        if (this._createFormCommandName == this._createChildCommandName) {
            if (this._callerDataItem) {
                this._blankDataItem.Parent = this._callerDataItem;
                clonedBlankDataItem.Parent = this._callerDataItem;
            }
            else {
                if (this._baseList.get_selectedItems().length > 0) {
                    this._blankDataItem.Parent = this._baseList.get_selectedItems()[0];
                    clonedBlankDataItem.Parent = this._baseList.get_selectedItems()[0];
                }
            }
        }
        else {
            this._blankDataItem.Parent = null;
            clonedBlankDataItem.Parent = null;
        }
        // bind the workflow buttons against the item
        this._bindWorkflowMenus(newItemContext, "OriginalContentId");

        if (this._appLoaded == true) {
            this._binder.BindItem(newItemContext, false, this._isNew);
        } else {
            this._newItemContext = newItemContext;
        }

        if (this._isMultilingual && this.get_languageSelector() && this._binder.get_uiCulture()) {
            this.get_languageSelector().set_selectedLanguage(this._binder.get_uiCulture());
        }

        if (this._commandArgument && this._commandArgument.TaxonomyFilters) {
            this._preselectTaxonomies(this._commandArgument.TaxonomyFilters);
        }

        this._checkForChanges = true;
        this._hideTranslationControlClickHandler();
    },

    //the method is called after databind of fields and its purpose is to expand sections 
    //if fields controls in the sections have value(or different than default value).
    configureExpandableSections: function () {
        for (var i = 0, length = this._sectionIds.length; i < length; i++) {
            var section = $find(this._sectionIds[i]);
            section.configureExpandableSection();
        }
    },

    //reset the sections according to definition
    resetSections: function () {
        for (var i = 0, length = this._sectionIds.length; i < length; i++) {
            var section = $find(this._sectionIds[i]);
            section.reset();
        }
    },

    _bindSectionsDoToggleEvent: function () {
        for (var i = 0; i < this._sectionIds.length; i++) {
            var section = $find(this._sectionIds[i]);
            section.add_doToggle(this._sectionToggleDelegate);
        }
    },

    _sectionToggleHandler: function (sender, args) {
        var sectionControlIDs = sender._fieldControlClientIDs;
        for (var j = 0; j < sectionControlIDs.length; j++) {
            var _fieldControl = $find(sectionControlIDs[j]);
            if (Object.getTypeName(_fieldControl) == "Telerik.Sitefinity.Web.UI.Fields.AddressField") {
                _fieldControl._sectionDoToggleHandler(args);
            }
        }
    },

    //returns a widget by command name from the toolbar
    getWidgetByCommandName: function (commandNameParam) {

        if (this._widgetBarIds) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widgetBar = $find(this._widgetBarIds[wCounter]);
                if (widgetBar !== null) {
                    var widgets = widgetBar.getAllWidgets();
                    if (widgets != null) {
                        for (var i = 0; i < widgets.length; i++) {
                            var isRestricted = false;
                            if (typeof (widgets[i].get_commandName) != "undefined") {
                                var commandName = widgets[i].get_commandName();
                                if (commandName == commandNameParam) {
                                    return widgets[i];
                                }
                            }
                        }
                    }
                }
            }
        }

        return null;

    },

    _resetLocalization: function () {
        if (this._isMultilingual) {
            var langSelector = this.get_languageSelector();
            if (langSelector) {
                // In preview mode we don't have language selectors
                langSelector.disable();
            }
        }
        if (this._initialTitle) {
            var jTitleElement = jQuery(this.get_titleElement());
            jTitleElement.text(this._initialTitle);
        }
        jQuery(this._sectionToolbarWrapper).show();
    },

    unlockContent: function (isCancel) {
        /// <summary>Unlocks the content and closes the window</summary>   
        if (this._disableUnlocking) return;
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._dataItem != null && this._dataItem.LifecycleStatus
                && this._dataItem.LifecycleStatus.SupportsContentLifecycle
                && this._unlockMode
                && this._dataItem
                && this._dataKey
                && this._dataKey.Id
                && this._dataItem.LifecycleStatus.IsLockedByMe) {

                //try to unlock with item id becouse of the case when unlock a blog post dataKey contains the id of the parent blog;

                var realDataItem;
                if (this._doNotUseContentItemContext) {
                    realDataItem = this._dataItem;
                }
                else {
                    realDataItem = this._dataItem.Item;
                }
                if (realDataItem && realDataItem.Id) {
                    this.sendUnlockRequest(realDataItem.Id);
                }
                else {
                    this.sendUnlockRequest(this._dataKey.Id);
                }
            }
            if (this._unlockMode && this._bulkEditFieldControlIds.length > 0) {
                for (var i = 0, length = this._bulkEditFieldControlIds.length; i < length; i++) {
                    var fieldControl = $find(this._bulkEditFieldControlIds[i]);
                    if (fieldControl) {
                        var items = fieldControl.getDataItems();
                        var provider = "";
                        if (typeof (fieldControl.get_provider) === 'function') {
                            provider = fieldControl.get_provider();
                        }
                        var j = items.length;
                        while (j--) {
                            var item = this._doNotUseContentItemContext ? items[j] : items[j].Item;
                            this.sendUnlockRequest(item.Id, provider);
                        }
                    }
                }
            }
        }
    },

    getUnlockParameters: function (providerName) {
        return {
            "provider": providerName,
            "managerType": this._managerType
        }
    },

    sendUnlockRequest: function (itemId, provider) {
        var manager = new Telerik.Sitefinity.Data.ClientManager();

        var culture = this._binder.get_uiCulture();
        manager.set_uiCulture(culture);

        var providerName = this._providerName;
        if (provider) {
            providerName = provider;
        }
        if (!providerName && this._callParams && this._callParams['providerName']) {
            providerName = this._callParams['providerName'];
        }
        manager.InvokeDelete(
            this.get_cancelChangesServiceUrl(),           // base url
            this.getUnlockParameters(providerName),       // query string parameters
            ["temp", itemId],                 // url segments to prepend
        // we don't need these callbacks
        // this behavior fixes some issues in the browse and edit dialogs
            function () { this._deleteTempCompleted = true; }, // success callback
            function () { this._deleteTempCompleted = true; }, // failure callback
            this);                                         // caller

    },

    //rebinds the data from the server
    rebind: function (culture) {
        this._binder.reset(culture);
        this.dataBind(this._callerDataItem, this._dataKey);
    },

    //resets the form
    reset: function () {
        this.get_messageControl().hide();
        this._resetLocalization();
        this._binder.reset();
        this.resetSections();
        this._lastBindedItemKeyId = null;
    },

    // Happens when after the form is created by the createForm method
    add_formCreated: function (delegate) {

        this.get_events().addHandler("formCreated", delegate);
    },
    // Happens when after the form is created by the createForm method
    remove_formCreated: function (delegate) {

        this.get_events().removeHandler("formCreated", delegate);
    },
    // Happens before the form is closed, cancelable
    add_formClosing: function (delegate) {
        this.get_events().addHandler("formClosing", delegate);
    },
    remove_formClosing: function (delegate) {
        this.get_events().removeHandler("formClosing", delegate);
    },
    add_closeInlineEditingWindow: function (delegate) {
        this.get_events().addHandler("closeInlineEditingWindow", delegate);
    },
    remove_closeInlineEditingWindow: function (delegate) {
        this.get_events().removeHandler("closeInlineEditingWindow", delegate);
    },

    // Hides the instructional text in dynamic content preview and compare dialogs
    // Overridden in DynamicContentDetailFormView
    _hideDynamicContentInstructionalText: function (commandName) { },
    /* -------------------- events -------------------- */
    // Raises a formcreated event
    _raiseFormCreated: function (isNew, dataKey, dataItem, commandName, params, context, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.FormCreatedEventArgs(isNew, dataKey, dataItem, commandName, params, context, commandArgument);
        var handler = this.get_events().getHandler("formCreated");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // Raises a formcreated event
    _raiseFormClosing: function (isNew, isDirty, dataItem, context, commandArgument, commandName) {
        var eventArgs = new Telerik.Sitefinity.FormClosingEventArgs(isNew, isDirty, dataItem, context, commandArgument, commandName);
        var handler = this.get_events().getHandler("formClosing");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // Raises event when DetailFormView is closed in InlineEditingMode
    _raiseCloseInlineEditingWindow: function (args) {
        var handler = this.get_events().getHandler("closeInlineEditingWindow");
        if (handler) handler(this, args);
        return args;
    },

    /* -------------------- event handlers -------------------- */

    // fired when page has been loaded
    _handlePageLoad: function (sender, args) {
        this._binder.set_fieldControlIds(this._fieldControlIds);
        this._binder.set_requireDataItemControlIds(this._requireDataItemControlIds);
        this._binder.set_bulkEditFieldControlIds(this._bulkEditFieldControlIds);
        this._binder.set_compositeFieldControlIds(this._compositeFieldControlIds);

        if (this._isMultilingual) {
            if (this.get_languageSelector()) {
                this.get_languageSelector().add_valueChanged(this._languageSelectorValueChangedDelegate);
            }
            this._translationBinder.set_fieldControlIds(this._translationFieldControlIds);
            jQuery(this.get_showTranslationControl()).click(this._showTranslationControlClickDelegate);
            jQuery(this.get_hideTranslationControl()).hide().click(this._hideTranslationControlClickDelegate);
            if (this.get_translationLanguageSelector()) {
                this.get_translationLanguageSelector().add_valueChanged(this._translationSelectorValueChangedDelegate);
            }
        }

        this._subscribeToWidgetBarEvents();
        this._subscribeToFieldControlsCommandEvents();
        this._initFirstTextField();
        this._setFocusToFirstTextField();
        this._initializeTabIndexFocusNotifier();

        if (this._newItemContext != null) {
            this._binder.BindItem(this._newItemContext);
        }
        this._appLoaded = true;

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.callBaseMethod(this, "_handlePageLoad");
    },

    // fired when field controls binder has successfully saved the item
    _formSaved: function (sender, args) {
        if (this._multipleSaving) {
            this.reset();
            this._showHideToolbars(true);
        }
        else {
            var shouldColse = true;

            if (this.get_bottomWorkflowMenu() != null)
                shouldColse = this.get_bottomWorkflowMenu().operationClosesDialog(sender.get_workflowOperation());
            if (shouldColse) {
                this._closeDialog();
            }
            else {
                this._formWasSaved = true;
                var result = sender.get_lastModifiedData();
                this._dataItem = result;
                if (!this._dataKey) {
                    this._dataKey = new Object;
                    this._dataKey.Id = this._doNotUseContentItemContext ? result.OriginalContentId : result.Item.OriginalContentId;
                }
                var lang = null;
                if (this._isMultilingual && this._languageSelector)
                    lang = this._languageSelector.get_value();
                if (!this._commandArgument)
                    this._commandArgument = {};
                this._commandArgument.languageMode = this._editCommandName;
                if (lang) this._commandArgument.language = lang;
                else lang = this._commandArgument.language;
                this._configureLanguageVersionOperationMode(this._editCommandName, { language: lang });
                this._binder.BindItem(result);
                this._setLanguages();
                this._rebindWorkflowMenus(result);
            }
        }
    },

    // fired when field controls binder has successfully deleted the item
    _formDeleted: function () {
        this._closeDialog();
    },

    //fired when field controls binder fired onError event, e.g. error during save
    _formError: function () {
        this._showHideToolbars(true);
        this._checkForChanges = true;
    },

    // fired when one of the toolbars fires a command event
    _widgetCommandHandler: function (sender, args) {
        var isEnterPressed = false;
        if (args && args.enterKey) {
            isEnterPressed = true;
        }

        this._callExternalHandlers('command', sender, args);
        if (args.get_cancel()) {
            this.asyncEndProcessing();
            return;
        }

        this._widgetCommandName = args.get_commandName();
        var workflowOperationName = args.get_commandArgument();
        var contextBag = null;
        if (args.get_contextBag) {
            contextBag = args.get_contextBag();
        }

        switch (this._widgetCommandName) {
            case this._createCommandName:
                this._multipleSaving = false;
                this.saveChanges(workflowOperationName, contextBag);
                break;
            case this._createAndUploadCommandName:
            case this._saveCommandName:
                this._multipleSaving = false;
                this.saveChanges(workflowOperationName, contextBag);
                break;
            case this._saveAndContinueCommandName:
                this._multipleSaving = true;
                //this.viewCommand(this._saveAndContinueCommandName);                 
                this.saveChanges(workflowOperationName, contextBag);
                break;
            case this._cancelCommandName:
                {
                    //this._binder._endProcessingHandler();
                    this._backToAllItems();
                    //dialogBase.close();
                    break;
                }
            case this._publishCommandName:
                alert('Operation "' + this._publishCommandName + '" is not supported at the moment.');
                break;
            case this._deleteCommandName:
                this.deleteItem();
                break;
            case this._historyCommandName:
                this.viewCommand(this._historyCommandName);
                break;
            case this._permissionsCommandName:
                this.viewCommand(this._permissionsCommandName);
                break;
            case this._previewCommandName:
                this.viewCommand(this._previewCommandName);
                break;
            case this._closeDialogCommandName:
                this._closeDialog();
                break;
            case "reportDirty":
                //this is raised from widgets that change the data item from servives other than the content service
                //for example workflow menu calls the workflow with unpublish and informs us so we can mark the item dirty and rebind on exits
                this._formWasSaved = true;
                break;
        }
    },

    _fieldCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var commandArgument = args.get_commandArgument();
        switch (commandName) {
            case this._historyCommandName:
                this.viewCommand(this._historyCommandName);
                break;
            case this._saveTempAndOpenLinkCommandName:
                this._handleSaveTempAndOpenLinkCommand(commandArgument);
                break;
            default:
                this._setupDialogMode(commandName, this._callerDataItem, this._dataKey, commandArgument);
                break;
        }
    },
        
    _handleSaveTempAndOpenLinkCommand: function (linkUrl){
         this.saveTemp( 
            function () {
                if (linkUrl){
                    window.open(linkUrl);
                }
            }, 
            this._saveTempFailure);
    },

    //calls external handlers of an events
    _callExternalHandlers: function (eventName, sender, args) {
        var h = this.get_events().getHandler(eventName);
        if (h) h(sender, args);
        return;
    },

    // Handles the focus event 
    _tabFocusHandler: function (e) {
        if (!e.target.id) {
            return;
        }
        // Find the component to which the element belongs
        var fieldControl = null;
        for (var i = 0, length = this._fieldControlIds.length; i < length; i++) {
            var el = $get(this._fieldControlIds[i]);
            var isChild = jQuery(el).find("#" + e.target.id);
            if (isChild.length > 0) {
                fieldControl = $find(this._fieldControlIds[i]);
                break;
            }
        }
        // If the focused element is part of a field control we blur the last focused control

        if (fieldControl == this._currentFocusedComponent) {
            return;
        }

        if (this._currentFocusedComponent) {
            // force blur on the last selected element or component
            this._currentFocusedComponent.blur();
        }
        this._currentFocusedComponent = fieldControl;
    },

    _translationSelectorValueChangedHandler: function (sender, args) {
        this._bindTranslation(sender.get_value());
    },

    _showTranslationControlClickHandler: function (sender, args) {
        if (this._isMultilingual) {
            jQuery(this._translationWrapper).show();
            jQuery("body").addClass("sfCompareTranslationDialog");
            this._hideDynamicContentInstructionalText("showTranslationWrapper");
            jQuery(this._showTranslationControl).hide();
            jQuery(this._hideTranslationControl).show();
            this._bindTranslationLanguageSelector();
        }
    },

    _bindTranslationLanguageSelector: function () {
        if (this.get_translationLanguageSelector()) {
            var translationLanguageSelectorValue = this.get_translationLanguageSelector().get_value();
            var bindTranslation = false;
            if ((translationLanguageSelectorValue instanceof Array && translationLanguageSelectorValue.length > 0) ||
                (typeof translationLanguageSelectorValue == 'string' && translationLanguageSelectorValue.length > 0)) {
                bindTranslation = true;
            }
            if (bindTranslation) {
                this._bindTranslation(translationLanguageSelectorValue);
            }
        }
    },

    _hideTranslationControlClickHandler: function (sender, args) {
        if (this._isMultilingual) {
            jQuery(this._translationWrapper).hide();
            jQuery(this._showTranslationControl).show();
            jQuery(this._hideTranslationControl).hide();
            jQuery("body").removeClass("sfCompareTranslationDialog");
        }
    },

    _languageSelectorValueChangedHandler: function (sender, args) {
        if (sender.get_enabled()) {
            var culture = sender.get_value();
            this.get_binder().set_uiCulture(culture);
            this._applyContentCultureToWorkflowMenus(culture);
        }
    },

    /* -------------------- private methods -------------------- */

    // Callback function called if the workflow service successfully complete workflow operation
    _messageWorkflow_Success: function (caller, data, request, context) {
        if (caller) {
            caller._afterRequest(false, context);
            caller._raiseCommand("reportDirty", null);
            if (caller._closeOnSuccess) {
                if (caller.get_returnUrl()) {
                    window.location = this.get_returnUrl();
                }
                else {
                    if (this._isOpenedInEditingWindow) {
                        this._checkForChanges = false;
                        var args = { deleteTemp: false, workflowOperationWasExecuted: true };
                        if (context == "DiscardDraft") {
                            args.deleteTemp = true;
                        }
                        this._raiseCloseInlineEditingWindow(args);
                        return;
                    } else {
                        dialogBase.closeAndRebind();
                    }
                }
            }
            else {
                caller._showMessage = true;
                caller._bindWorkflowVisuals();
            }
        }
    },

    _initFirstTextField: function () {
        for (var i = 0, length = this._fieldControlIds.length; i < length; i++) {
            var control = $find(this._fieldControlIds[i]);
            if (Object.getTypeName(control) == "Telerik.Sitefinity.Web.UI.Fields.TextField") {
                if (this._firstTextFieldControl == null) {
                    this._firstTextFieldControl = control;

                }
                this._textFieldControlsCount++;
            }
        }
    },

    _setFocusToFirstTextField: function () {
        // RadEditor takes the focus by default
        for (var i = 0, length = this._fieldControlIds.length; i < length; i++) {
            var control = $find(this._fieldControlIds[i]);
            if (Object.getTypeName(control) == "Telerik.Sitefinity.Web.UI.Fields.HtmlField") {
                if (control.get_displayMode() == 0) return;

                this._htmlFieldControlsCount++;

                if (control._editControl && control._editControl.get_textArea()) {
                    control._editControl.get_textArea().blur();
                }
            }
        }
        window.focus();

        //focus on the first text field
        window.setTimeout(Function.createDelegate(this, this._hideFirstTextField), 200);
    },

    _hideFirstTextField: function () {
        if (this._firstTextFieldControl) {
            this._firstTextFieldControl.focus();
        }
    },

    _onkeyDownHandler: function (e) {
        if (e.keyCode == Sys.UI.Key.esc) {
            e.stopPropagation();
            e.preventDefault();
            dialogBase.close();
        }
        else if (e.keyCode == Sys.UI.Key.enter) {
            if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
                if (this._textFieldControlsCount == 1 && this._htmlFieldControlsCount == 0) {
                    this.saveChanges();
                }
            }
        }
    },

    _subscribeToFieldControlsCommandEvents: function () {
        if (this._commandFieldControlIds) {
            var commandFieldControlsCount = this._commandFieldControlIds.length;
            for (var fieldsCounter = 0; fieldsCounter < commandFieldControlsCount; fieldsCounter++) {
                if (this._fieldCommandDelegate === null) {
                    this._fieldCommandDelegate = Function.createDelegate(this, this._fieldCommandHandler);
                }
                var commandField = $find(this._commandFieldControlIds[fieldsCounter]);
                if (commandField !== null) {
                    commandField.add_command(this._fieldCommandDelegate);
                }
            }
        }
    },

    _subscribeToWidgetBarEvents: function () {
        if (this._widgetBarIds) {
            var wLength = this._widgetBarIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetBarIds[wCounter]);
                if (widget !== null) {
                    if (this._widgetCommandDelegate == null) {
                        this._widgetCommandDelegate = Function.createDelegate(this, this._widgetCommandHandler);
                    }
                    widget.add_command(this._widgetCommandDelegate);
                }
            }
        }
    },

    //Shows or hides configured toolbars.
    _showHideToolbars: function (toShow) {
        var bars = this.get_buttonBarIds();
        if (bars !== null) {
            for (var i = 0, length = bars.length; i < length; i++) {
                var barId = bars[i];
                if (toShow)
                    $('#' + barId).show();
                else
                    $('#' + barId).hide();
            }
        }
    },

    // called when data binding was successfull
    _dataBindSuccess: function (sender, result) {
        if (this._duplicate) {
            if (this._doNotUseContentItemContext) {
                if (result) {
                    result.IsDuplicate = true;
                }
            }
            else {
                if (result.Item) {
                    result.Item.IsDuplicate = true;
                } else if (result.Id) {
                    result.IsDuplicate = true;
                }
            }
        }

        this._enforceContentLifecycle.apply(this, [result]);

        //TODO extract FallbackMode enumeration
        this._binder.set_fallbackMode(null);

        if (this._loadingBlankItem) {
            this._blankDataItem = this._doNotUseContentItemContext ? result : result.Item;
            if (this._blankDataItem.hasOwnProperty('Id')) {
                delete this._blankDataItem.Id;
            }
            this._binder.setBlankDataItem(this._blankDataItem);
            this._setupCreateMode(result);
            this._loadingBlankItem = false;
            return;
        }

        this._dataItem = result;
        this._binder.BindItem(result, false, this._isNew);
        this.configureExpandableSections();

        this._callExternalHandlers('databind', this, this._dataItem);
        if (this._backButtonClicked == false) {
            this._checkForChanges = true;
        }
        this._formatBackButton(sender, result);

        var itemTitle = "";
        if (this._doNotUseContentItemContext) {
            if (result) {
                itemTitle = result.Title;
            }
        }
        else {
            if (result.Item)
                itemTitle = result.Item.Title;
        }
        if (result.Items)
            itemTitle = result.Items[0].Title;

        this._setLanguages();
    },

    _formatBackButton: function (sender, result) { //TODO/6
        var radWindow = dialogBase.get_radWindow();
        if (!this._isOpenedInEditingWindow &&
            (typeof (radWindow) != "undefined") &&
            (typeof (radWindow._sfArgs) != "undefined") &&
            (typeof (radWindow._sfArgs.get_params) !== "undefined") &&
            (typeof (radWindow._sfArgs.get_params()["backLabelText"]) != "undefined") &&
            (radWindow._sfArgs.get_params()["backLabelText"] != null)) {
            this._setBackButtonText(radWindow._sfArgs.get_params()["backLabelText"]);
        }
        else {
            if (!this._suppressBackToButtonLabelModify) {
                var itemTitle = null;
                if (this._doNotUseContentItemContext) {
                    if (result) {
                        itemTitle = result.Title;
                    }
                }
                else {
                    if (result.Item) {
                        itemTitle = result.Item.Title
                    }
                }
                if (result.Items)
                    itemTitle = result.Items[0].Title

                if ((typeof itemTitle != 'object') && (typeof itemTitle != 'undefined')) {
                    if (sender._callParams.ControlDefinitionName == 'PublishingBackend')
                        return null;

                    var baseList = this.get_baseList();
                    if (baseList && baseList.getFormattedBackLabel)
                        this._setBackButtonText(this.get_baseList().getFormattedBackLabel(this._backToLabel, itemTitle));
                    //this._backToAllItemsButton.innerHTML = backButtonTitle;
                }   // of if ((typeof itemTitle != 'object')...
            }
        }
        if (!this._isOpenedInEditingWindow &&
            (typeof (radWindow) != "undefined") &&
            (typeof (radWindow._sfArgs) != "undefined") &&
            (typeof (radWindow._sfArgs.get_params) !== "undefined") &&
            (typeof (radWindow._sfArgs.get_params()["backLabelText"]) == "undefined")) {
            radWindow._sfArgs.get_params()["backLabelText"] = this._backToAllItemsButton.innerHTML;
            var globalStack = window.top.GetDialogManager().get_globalHistoryContextStack();
            globalStack[globalStack.length - 1].get_params()["backLabelText"] = this._backToAllItemsButton.innerHTML;
            this._setBackButtonText(this._backToAllItemsButton.innerHTML);
        }
        this._bindWorkflowMenus(result, "Id");

        if (this._isMultilingual && this.get_languageSelector()) {
            this.get_languageSelector().set_selectedLanguage(this._binder.get_uiCulture());
        }
    },

    _setBackButtonText: function (newText) { //TODO/6
        newText = newText.trim();
        var shortenedText = ((newText.length <= 30) ? (newText) : (newText.substring(0, 30) + "..."));
        if (this._backToAllItemsButton) {
            this._backToAllItemsButton.innerHTML = shortenedText;
            this._backToAllItemsButton.title = newText;
        }
        if (this.get_topWorkflowMenu()) {
            var topCancel = this.get_topWorkflowMenu().get_cancelLink();
            if (topCancel != null) {
                topCancel.innerHTML = shortenedText;
                topCancel.title = newText;
            }
        }
        if (this.get_bottomWorkflowMenu()) {
            var bottomCancel = this.get_bottomWorkflowMenu().get_cancelLink();
            if (bottomCancel != null) {
                bottomCancel.innerHTML = shortenedText;
                bottomCancel.title = newText;
            }
        }
        var cancelWidget = this.getWidgetByCommandName("cancel");
        if (cancelWidget) {
            cancelWidget.get_linkElement().get(0).getElementsByTagName('span')[0].innerHTML = shortenedText;
        }
        for (var i = 0; i < this._widgetBarIds.length; i++) {
            if ($find(this._widgetBarIds[i]) != null) {
                var cancelCommand = $find(this._widgetBarIds[i]).getWidgetByCommandName("cancel");
                if (cancelCommand != null) {
                    cancelCommand.get_linkElement().get(0).getElementsByTagName('span')[0].innerHTML = shortenedText;
                    cancelCommand.get_linkElement().get(0).getElementsByTagName('span')[0].title = newText;
                }
            }
        }
    },

    _translationBindSuccessHandler: function (sender, result) {
        this._translationBinder.BindItem(result);
        this._lastBindedItemKeyId = this._doNotUseContentItemContext ? result.Id : result.Item.Id;
    },

    // called when data binding was not successful
    _dataBindFailure: function (error, caller) {
        this.Caller._loadingBlankItem = false;
        if (this.Caller._closeOnError)
            this.Caller._formError();
        alert(error.Detail);
        if (this.Caller._closeOnError) {
            if (dialogBase)
                dialogBase.closeAndRebind();
            else
                window.close();
        }
    },

    // closes the dialog
    _closeDialog: function () {
        if (this._isOpenedInEditingWindow) {
            this._checkForChanges = false;
            var args = { deleteTemp: false, workflowOperationWasExecuted: true }
            if (this._isNew && this._isDirty) {
                args.isNew = true;
                args.lastModifiedDataItem = this.get_binder().get_lastModifiedDataItem();
            }
            this._raiseCloseInlineEditingWindow(args);
            return;
        }
        var args = this._raiseFormClosing(this._isNew, this._isDirty, this.get_binder().get_lastModifiedDataItem(), null, this._commandArgument, this._createFormCommandName);
        if (args.get_cancel())
            return;
        if (this._isNew && this._isDirty) {
            dialogBase.closeCreated(this.get_binder().get_lastModifiedDataItem(), this);
        }
        else if (this._isDirty) {
            if (this._callParams.NotifyUrlChanged) {
                var dataItem = this.get_binder().get_lastModifiedDataItem();
                var urlName = "";
                if (dataItem) {
                    urlName = dataItem.UrlName.Value;
                }
                dialogBase.close("redirect:" + urlName);
            }
            else if (this._multipleSaving == false) {
                dialogBase.closeAndRebind();
            }
        }
        else {
            dialogBase.close();
        }
    },

    // opens a dialog for the given command
    _openDialog: function (commandName, dataItem, params, cmdArg) {
        var baseList = this.get_baseList();
        if (baseList && baseList.executeCommand)
            baseList.executeCommand(commandName, dataItem, this.get_dataKey(), null, params, cmdArg);
        else {
            var dialogManager = window.top.GetDialogManager();
            var dialog = dialogManager.getDialogByName(commandName);
            var context = { commandName: commandName, dataItem: dataItem, itemsList: null, dialog: dialog, params: params, key: this.get_dataKey(), commandArgument: cmdArg };
            dialogManager.openDialog(commandName, this, context);
        }
    },

    // We listen to all focus events in order to blur the last focusd field control
    _initializeTabIndexFocusNotifier: function () {
        this._currentFocusedComponent = null;
        // Everything without the templates
        // jQuery(":not(.sys-template, .sys-template *, .sys-container, .sys-container *)").focus(this._focusDelegate);
    },

    _nextButtonClick: function (sender) {
        if (Telerik.Sitefinity.UI) {
            var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs("navigateNext", null);
        }
        else {
            var commandEventArgs = new Telerik.Sitefinity.CommandEventArgs("navigateNext", null);
        }
        this._widgetCommandHandler(this, commandEventArgs);
    },

    _previousButtonClick: function (sender) {
        if (Telerik.Sitefinity.UI) {
            var commandEventArgs = new Telerik.Sitefinity.UI.CommandEventArgs("navigatePrevious", null);
        }
        else {
            var commandEventArgs = new Telerik.Sitefinity.CommandEventArgs("navigatePrevious", null);
        }
        this._widgetCommandHandler(this, commandEventArgs);
    },

    _noWorkflowActionsHandler: function (sender, eventArgs) {
        var prompt = $find(this.get_cannotModifyDialogId());
        if (prompt) {
            prompt.add_command(this._noWorkflowActionsDialogDelegate);
            prompt.show_prompt();
        }
    },

    _noWorkflowActionsDialogHandler: function (sender, eventArgs) {
        if (eventArgs.get_commandName().toLowerCase() == "ok") {
            this._closeDialog();
        }
    },

    _unlockContentSuccessCallback: function (caller, data) {
        caller._unlockContentSuccess.apply(caller, [data]);
    },

    _unlockContentFailureCallback: function (error, caller) {
        caller._unlockContentFailure.apply(caller, [error]);
    },

    _unlockContentSuccess: function (data) {
        // TODO: remove these calls, once the IAsync thing is modified
        this._binder._endProcessingHandler();

    },

    _unlockContentFailure: function (error) {
        alert(error.Detail);
        if (this.Caller)
            this.Caller._formError();
    },

    _windowCloseHandler: function (sender) {
        if (!this._backButtonClicked) {
            if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
                if (this._checkForChanges && this._binder.hasChangedFieldValues()) {
                    this._checkForChanges = false;
                    var self = this;
                    setTimeout(function () {
                        self._checkForChanges = true;
                    }, 100);
                    return "You have unsaved changes!";
                }
            }
        }
        this._backButtonClicked = false;
    },

    _windowClosedHandler: function (sender) {
        this.unlockContent();
    },

    _beforeDialogCloseHandler: function (sender, args) {
        this._checkForChanges = false;
        this._callExternalHandlers('beforeDialogClose', this, args);
        //this._currentDialog.remove_beforeClose(this._beforeDialogCloseDelegate);
        this.unlockContent();
    },

    _backToAllItems: function () {
        if (this._isOpenedInEditingWindow) {
            if (this._checkForChanges && this._binder.hasChangedFieldValues()) {
                if (confirm(this._youHaveUnsavedChangesWantToLeavePage)) {
                    this._checkForChanges = false;
                    this._raiseCloseInlineEditingWindow({ deleteTemp: true, workflowOperationWasExecuted: false });
                }
            } else {
                this._raiseCloseInlineEditingWindow({ deleteTemp: true, workflowOperationWasExecuted: false });
            }
        }
        else {
            var radWindow = dialogBase.get_radWindow();
            if (radWindow && radWindow._sfArgs && radWindow._sfArgs.set_cancel) {
                radWindow._sfArgs.set_cancel(true);
            }
            if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
                if (this._checkForChanges && this._binder.hasChangedFieldValues()) {
                    if (confirm(this._youHaveUnsavedChangesWantToLeavePage)) {
                        this._checkForChanges = false;
                        dialogBase.closeAndRebind();
                    }
                }
                else {
                    this._checkForChanges = false;
                    if (this._formWasSaved)
                        dialogBase.closeAndRebind();
                    else
                        dialogBase.close();
                }

            }
            else {
                this._checkForChanges = false;
                dialogBase.close();
            }
            this._backButtonClicked = true;
        }
    },

    //returns true if it is ok to leave the dialog (user confirms or there are no changes)
    _verifyChangesOnExit: function () {
        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._checkForChanges && this._binder.hasChangedFieldValues()) {
                if (!confirm(this._youHaveUnsavedChangesWantToLeavePage)) {
                    //there are changes and user cancels the exit
                    return false;
                }
            }
        }
        return true;
    },

    _bindTranslation: function (language) {
        if (this.get_translationBinder().get_uiCulture() !== language || this._dataKey.Id != this._lastBindedItemKeyId) {
            this.get_translationBinder().set_uiCulture(language);
            var clientManager = this.get_translationBinder().get_manager();
            var urlParams = [];
            if (this._providerName != null) {
                urlParams['provider'] = this._providerName;
                this.get_translationBinder().set_provider(this._providerName);
            }
            clientManager.InvokeGet(this.get_translationBinder().get_serviceBaseUrl(), urlParams, this._dataKey, this._translationBindSuccessDelegate, this._dataBindFailure, this);
        }
    },

    _rebindWorkflowMenus: function (context) {
        var topWorkflowMenu = this.get_topWorkflowMenu();
        if (topWorkflowMenu) {
            topWorkflowMenu.rebindWorkflowVisuals(context);
        }

        var bottomWorkflowMenu = this.get_bottomWorkflowMenu();
        if (bottomWorkflowMenu) {
            bottomWorkflowMenu.rebindWorkflowVisuals(context);
        }
    },

    _bindWorkflowMenus: function (itemContext, propertyName) {
        var fields = this._findFieldControlsByTypeName(this._commandFieldControlIds, "Telerik.Sitefinity.Web.UI.Fields.ContentWorkflowStatusInfoField");
        var contentWorkflowStatusInfoField = null;
        if (fields.length >= 1) {
            contentWorkflowStatusInfoField = fields[0];
        }

        var realDataItem = this._doNotUseContentItemContext ? itemContext : itemContext.Item;

        var that = this;
        var validationFunction = function () {
            if (that._binder) {
                return that._binder.validate();
            }
            return true;
        }

        var topWorkflowMenu = this.get_topWorkflowMenu();
        if (topWorkflowMenu) {
            topWorkflowMenu.set_messageControl(this.get_messageControl());
            topWorkflowMenu.set_contentWorkflowStatusInfoField(contentWorkflowStatusInfoField);
            topWorkflowMenu.set_showMoreActions(this._showMoreActionsWorkflowMenu);
            topWorkflowMenu.bindWorkflowVisuals(this._contentType, this._providerName, realDataItem.Id, realDataItem, validationFunction);
        }

        var bottomWorkflowMenu = this.get_bottomWorkflowMenu();
        if (bottomWorkflowMenu) {
            bottomWorkflowMenu.set_messageControl(this.get_messageControl());
            bottomWorkflowMenu.set_contentWorkflowStatusInfoField(contentWorkflowStatusInfoField);
            bottomWorkflowMenu.set_showMoreActions(this._showMoreActionsWorkflowMenu);
            bottomWorkflowMenu.bindWorkflowVisuals(this._contentType, this._providerName, realDataItem.Id, realDataItem, validationFunction);
        }
    },

    _applyContentCultureToWorkflowMenus: function (culture) {
        var topWorkflowMenu = this.get_topWorkflowMenu();
        if (topWorkflowMenu) {
            topWorkflowMenu.set_contentCulture(culture);
        }

        var bottomWorkflowMenu = this.get_bottomWorkflowMenu();
        if (bottomWorkflowMenu) {
            bottomWorkflowMenu.set_contentCulture(culture);
        }
    },

    _findFieldControlsByTypeName: function (fieldControlIds, typeName) {
        var fields = [];
        for (var i = 0; i <= fieldControlIds.length; i++) {
            var field = $find(fieldControlIds[i]);
            if (field && Object.getTypeName(field) == typeName) {
                fields.push(field);
            }
        }

        return fields;
    },

    _setLanguages: function () {
        if (this._isMultilingual) {
            if (this.get_languageSelector() && this._binder.get_uiCulture()) {
                this.get_languageSelector().set_selectedLanguage(this._binder.get_uiCulture());
            }
            if (this.get_languageList())
                this.get_languageList().hideLanguage(this._binder.get_uiCulture());
        }
    },

    _preselectTaxonomies: function (taxonomyFilters) {
        if (taxonomyFilters && taxonomyFilters.length) {
            var filtersLength = taxonomyFilters.length;
            for (var i = 0; i < filtersLength; i++) {
                var filter = taxonomyFilters[i];
                //Finding the taxonomy field by checking if its dataFieldName is equal to the TaxonomyName is a workaround.
                if (filter.TaxonomyName && filter.Taxons && filter.Taxons.length) {
                    var taxonsField = this._binder.getFieldControlByDataFieldName(filter.TaxonomyName);
                    if (taxonsField && taxonsField.addTaxon) {
                        var taxonsLength = filter.Taxons.length;
                        for (var j = 0; j < taxonsLength; j++) {
                            taxonsField.addTaxon(filter.Taxons[j]);
                        }
                    }
                }
            }
        }
    },
    _replacePropertyValues: function (dataItem, literal) {
        if (literal && dataItem) {
            var matches = literal.match(/{{\w+}}/g);
            if (matches) {
                var matchIndex = matches.length;
                var current = null;
                var propName = null;
                var propValue = null;
                while (matchIndex--) {
                    current = matches[matchIndex];
                    propName = current.slice(2, -2);
                    propValue = this._getPropertyValue(dataItem, propName);
                    literal = literal.replace(current, propValue);
                }
            }
        }
        return literal;
    },
    _getPropertyValue: function (dataItem, propertyName) {
        var dotIndex = propertyName.indexOf(".");
        var property = dataItem[propertyName];
        if (property && dotIndex != -1) {
            var itemName = propertyName.slice(0, dotIndex);
            var itemValueName = propertyName.substring(dotIndex);
            if (itemName) {
                var item = dataItem[itemName];
                return this._getPropertyValue(item, itemValueName);
            }
        }
        return property;
    },
    _prepareCommandArgument: function () {
        var lang, cmdArg;
        if (this._isMultilingual && this._languageSelector) {
            lang = this._binder.get_uiCulture();
            cmdArg = { language: lang };
            this._commandArgument.language = lang;
        }
        this._dialogContext = this._commandArgument;
        return cmdArg;
    },
    /* -------------------- properties -------------------- */

    // gets the reference to the field controls binder
    get_binder: function () {
        return this._binder;
    },
    // sets the reference to the field controls binder
    set_binder: function (value) {
        this._binder = value;
    },

    // gets the reference to the translation field controls binder
    get_translationBinder: function () {
        return this._translationBinder;
    },
    // sets the reference to the translation field controls binder
    set_translationBinder: function (value) {
        this._translationBinder = value;
    },

    // gets the reference to the asyncronious command mediator
    get_mediator: function () {
        return this._mediator;
    },
    // sets the reference to  the asyncronious command mediator
    set_mediator: function (value) {
        this._mediator = value;
    },

    // gets the reference to the workflow menu component
    get_topWorkflowMenu: function () {
        return this._topWorkflowMenu;
    },
    // sets the reference to the workflow menu component
    set_topWorkflowMenu: function (value) {
        this._topWorkflowMenu = value;
    },

    // gets the reference to the workflow menu component
    get_bottomWorkflowMenu: function () {
        return this._bottomWorkflowMenu;
    },
    // sets the reference to the workflow menu component
    set_bottomWorkflowMenu: function (value) {
        this._bottomWorkflowMenu = value;
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },
    set_bindOnLoad: function (value) {
        this._bindOnLoad = value;
    },

    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },

    get_sectionIds: function () {
        return this._sectionIds;
    },
    set_sectionIds: function (value) {
        this._sectionIds = value;
    },

    get_translationFieldControlIds: function () {
        return this._translationFieldControlIds;
    },
    set_translationFieldControlIds: function (value) {
        this._translationFieldControlIds = value;
    },

    get_buttonBarIds: function () {
        return this._buttonBarIds;
    },
    set_buttonBarIds: function (value) {
        this._buttonBarIds = value;
    },

    get_widgetBarIds: function () {
        return this._widgetBarIds;
    },

    set_widgetBarIds: function (value) {
        this._widgetBarIds = value;
    },

    get_commandFieldControlIds: function () {
        return this._commandFieldControlIds;
    },
    set_commandFieldControlIds: function (value) {
        this._commandFieldControlIds = value;
    },

    get_deleteConfirmationMessage: function () {
        return this._deleteConfirmationMessage;
    },
    set_deleteConfirmationMessage: function (value) {
        this._deleteConfirmationMessage = value;
    },

    get_permissionsCommandName: function () {
        return this._permissionsCommandName;
    },
    set_permissionsCommandName: function (value) {
        this._permissionsCommandName = value;
    },

    //returns the command name used to create the form (create, createPost, etc)
    get_createFormCommandName: function () {
        return this._createFormCommandName;
    },

    //returns the command name fired by the widgets in the toolbar
    get_widgetCommandName: function () {
        return this._widgetCommandName;
    },

    // IDs of the field controls which require the current data item when binding
    get_requireDataItemControlIds: function () {
        return this._requireDataItemControlIds;
    },

    set_requireDataItemControlIds: function (value) {
        this._requireDataItemControlIds = value;
    },

    // Client IDs of the bulk edit field controls
    get_bulkEditFieldControlIds: function () {
        return this._bulkEditFieldControlIds;
    },

    set_bulkEditFieldControlIds: function (value) {
        this._bulkEditFieldControlIds = value;
    },

    // Client IDs of the composite field controls
    get_compositeFieldControlIds: function () {
        return this._compositeFieldControlIds;
    },
    set_compositeFieldControlIds: function (value) {
        this._compositeFieldControlIds = value;
    },

    //gets the view title html element
    get_titleElement: function () {
        return this._titleElement;
    },

    //sets the view title html element
    set_titleElement: function (value) {
        this._titleElement = value;
    },

    //gets the current data item the view is showing
    get_dataItem: function () {
        return this._dataItem;
    },
    //gets the data item key
    get_dataKey: function () {
        return this._dataKey;
    },
    //gets the the data item passed by the caller
    get_callerDataItem: function () {
        return this._callerDataItem;
    },
    //subscribes for the detail form view widget commands events
    add_command: function (handler) {
        this.get_events().addHandler('command', handler);
    },
    //unsubscribes for the detial form view widget commands events
    remove_command: function (handler) {
        this.get_events().removeHandler('command', handler);
    },
    //subscribes for the detial form view detabind event
    add_onDataBind: function (handler) {
        this.get_events().addHandler('databind', handler);
    },
    //unsubscribes for the detial form view detabind event
    remove_onDataBind: function (handler) {
        this.get_events().removeHandler('databind', handler);
    },
    //subscribes for the detial form view dialog close event
    add_onBeforeDialogClose: function(handler) {
        this.get_events().addHandler('beforeDialogClose', handler);
    },
    //unsubscribes for the detial form view dialog close event
    remove_onBeforeDialogClose: function(handler) {
        this.get_events().removeHandler('beforeDialogClose', handler);
    },
    //returns the base list that opened the dialog
    get_baseList: function () {
        return this._baseList;
    },
    //returns the paramters colelction the dialog was opened with
    get_callParameters: function () {
        return this._callParams;
    },
    //returns the previous button(navigation buttons) html element
    get_previousButton: function () {
        return this._previousButton;
    },
    //sets the previous button(navigation buttons) html element
    set_previousButton: function (value) {
        this._previousButton = value;
    },
    //returns the next button(navigation buttons) html element
    get_nextButton: function () {
        return this._nextButton;
    },
    //sets the next button(navigation buttons) html element
    set_nextButton: function (value) {
        this._nextButton = value
    },
    // get field display mode
    get_displayMode: function () {
        return this._displayMode;
    },
    // set field display mode
    set_displayMode: function (val) {
        if (this._displayMode != val) {
            this._displayMode = val;
            this.raisePropertyChanged("displayMode");
        }
    },
    // Sets the message control component
    get_messageControl: function () {
        return this._messageControl;
    },
    // Gets the message control component
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_cancelChangesServiceUrl: function () {
        return this._cancelChangesServiceUrl;
    },
    set_cancelChangesServiceUrl: function (val) {
        if (this._cancelChangesServiceUrl != val) {
            this._cancelChangesServiceUrl = val;
            this.raisePropertyChanged("cancelChangesServiceUrl");
        }
    },
    get_contentLocationPreviewUrl: function () {
        return this._contentLocationPreviewUrl;
    },
    set_contentLocationPreviewUrl: function (val) {
        if (this._contentLocationPreviewUrl != val) {
            this._contentLocationPreviewUrl = val;
            this.raisePropertyChanged("contentLocationPreviewUrl");
        }
    },
    get_backToAllItemsButton: function () {
        return this._backToAllItemsButton;
    },
    set_backToAllItemsButton: function (val) {
        if (this._backToAllItemsButton != val) {
            this._backToAllItemsButton = val;
            this.raisePropertyChanged("backToAllItemsButton");
        }
    },

    get_languageSelector: function () {
        return this._languageSelector;
    },
    set_languageSelector: function (value) {
        this._languageSelector = value;
    },

    get_languageList: function () {
        return this._languageList;
    },
    set_languageList: function (value) {
        this._languageList = value;
    },

    get_translationLanguageSelector: function () {
        return this._translationLanguageSelector;
    },
    set_translationLanguageSelector: function (value) {
        this._translationLanguageSelector = value;
    },

    get_showTranslationControl: function () {
        return this._showTranslationControl;
    },
    set_showTranslationControl: function (value) {
        this._showTranslationControl = value;
    },

    get_hideTranslationControl: function () {
        return this._hideTranslationControl;
    },
    set_hideTranslationControl: function (value) {
        this._hideTranslationControl = value;
    },

    get_translationWrapper: function () {
        return this._translationWrapper;
    },
    set_translationWrapper: function (value) {
        this._translationWrapper = value;
    },

    get_sectionToolbarWrapper: function () {
        return this._sectionToolbarWrapper;
    },
    set_sectionToolbarWrapper: function (value) {
        this._sectionToolbarWrapper = value;
    },

    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },

    get_windowManager: function () {
        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },
    get_localization: function () {
        return this._localization;
    },
    set_localization: function (val) {
        this._localization = val;
    },

    // Returns the command argument used to create the dialog (createForm method)
    get_commandArgument: function () {
        return this._commandArgument;
    },

    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function (value) {
        this._serviceBaseUrl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_createCommandName: function () {
        return this._createCommandName;
    },

    get_cannotModifyDialogId: function () {
        return this._cannotModifyDialogId;
    },
    set_cannotModifyDialogId: function (value) {
        this._cannotModifyDialogId = value;
    }
};

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.DetailFormView", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase);
