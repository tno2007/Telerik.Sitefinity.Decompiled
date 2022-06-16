Type.registerNamespace("Telerik.Sitefinity.Modules.GenericContent.Web.UI");

// ------------------------------------------------------------------------
// ContentBlockDesigner class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner = function (element) {
    this._propertyEditor = null;
    this._htmlEditor = null;
    this._sharedContentSelector = null;
    this._editViewCommandBar = null;
    this._shareContentDialog = null;
    this._unshareContentDialog = null;
    this._contentItemsServiceUrl = null;
    this._commandBar = null;
    this._viewPagesLink = null;
    this._contentLabel = null;
    this._uiCulture = null;
    this._contentEditor = null;
    this._blankDataItem = null;
    this._itemType = null;
    this._windowManager = null;
    this._clientLabelManager = null;
    this._sharedContentLabel = null;
    this._affectedPages = null;
    this._affectedPageTemplates = null;
    this._currentLanguage = null;

    this._clientManager = null;
    this._currentView = null;
    this._contentSelectorIsBinded = false;
    this._contentSelectorBinder = null;
    this._itemContext = null;

    this._commandDelegate = null;
    this._selectContentDelegate = null;
    this._selectContentSuccessDelegate = null;
    this._binderItemCommandDelegate = null;
    this._binderItemDataBoundDelegate = null;
    this._promptCommandDelegate = null;
    this._shareContentSuccessDelegate = null;
    this._saveChangesSuccessDelegate = null;
    this._getContentSuccessDelegate = null;
    this._serviceFailureDelegate = null;
    this._viewPagesDelegate = null;
    this._beforeSaveChangesDelegate = null;
    this._completeSaveChangesDelegate = null;
    this._closeDialogExtensionDelegate = null;
    this._beforeCloseDelegate = null;
    this._isMissingSharedContent = null;

    this._selectorTag = null;
    this._dialog = null;

    this._providersSelector = null;
    this._providersSelectorClickedDelegate = null;
    this._isControlDefinitionProviderCorrect = null;
    this._message = null;

    this._sharedHtml = null;
    this._sharedContentTitleLabel = null;

    this._sharedSelectorButton = null;
    this._sharedEditButton = null;

    Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner.initializeBase(this, [element]);
}

Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner.prototype = {

    /* ************************* set up and tear down ************************* */
    initialize: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner.callBaseMethod(this, 'initialize');

        if (this._sharedContentInitialized) {
            if (this._blankDataItem) {
                this._blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._blankDataItem);
            }

            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
            this._editViewCommandBar.add_command(this._commandDelegate);
            this._commandBar.add_command(this._commandDelegate);

            this._selectContentDelegate = Function.createDelegate(this, this._selectContent);
            this._sharedContentSelector.add_doneClientSelection(this._selectContentDelegate);

            this._selectContentSuccessDelegate = Function.createDelegate(this, this._selectContentSuccess);

            this._promptCommandDelegate = Function.createDelegate(this, this._promptCommandHandler);
            this._shareContentDialog.add_command(this._promptCommandDelegate);
            this._unshareContentDialog.add_command(this._promptCommandDelegate);

            this._shareContentSuccessDelegate = Function.createDelegate(this, this._shareContentSuccess);
            this._saveChangesSuccessDelegate = Function.createDelegate(this, this._saveChangesSuccess);
            this._getContentSuccessDelegate = Function.createDelegate(this, this._getContentSuccess);
            this._serviceFailureDelegate = Function.createDelegate(this, this._serviceFailure);

            this._viewPagesDelegate = Function.createDelegate(this, this._viewPages);
            $addHandler(this._viewPagesLink, "click", this._viewPagesDelegate);

            this._binderItemCommandDelegate = Function.createDelegate(this, this._binderItemCommandHandler);
            this._binderItemDataBoundDelegate = Function.createDelegate(this, this._binderItemDataBoundHandler);

            this._beforeSaveChangesDelegate = Function.createDelegate(this, this._beforeSaveChangesHandler);
            this.get_propertyEditor().add_beforeSaveChanges(this._beforeSaveChangesDelegate);

            this._completeSaveChangesDelegate = Function.createDelegate(this, this._completeSaveChangesHandler);
            this.get_propertyEditor().add_completeSaveChanges(this._completeSaveChangesDelegate);

            this._closeDialogExtensionDelegate = Function.createDelegate(this, this._closeDialogExtension);

            this._beforeCloseDelegate = Function.createDelegate(this, this._beforeCloseHandler);
            dialogBase.get_radWindow().add_beforeClose(this._beforeCloseDelegate);

            if (this._providersSelector) {
                this._providersSelectorClickedDelegate = Function.createDelegate(this, this._handleProvidersChanged);
                this._providersSelector.add_onProviderSelected(this._providersSelectorClickedDelegate);
            }

            jQuery('#countersDetailsLink').click(function () {
                jQuery('#countersDetails').toggleClass("sfDisplayNone");
            });

            this._dialog = jQuery(this._selectorTag).dialog({
                autoOpen: false,
                modal: false,
                width: 355,
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                classes: {
                    "ui-dialog": "sfZIndexL"
                }
            });
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner.callBaseMethod(this, 'dispose');

        if (this._commandDelegate) {
            if (this._editViewCommandBar) {
                this._editViewCommandBar.remove_command(this._commandDelegate);
            }
            if (this._commandBar) {
                this._commandBar.remove_command(this._commandDelegate);
            }
            delete this._commandDelegate;
        }
        if (this._selectContentDelegate) {
            if (this._sharedContentSelector) {
                this._sharedContentSelector.remove_doneClientSelection(this._selectContentDelegate);
            }
            delete this._selectContentDelegate;
        }
        if (this._selectContentSuccessDelegate) {
            delete this._selectContentSuccessDelegate;
        }
        if (this._binderItemCommandDelegate) {
            if (this.get_contentSelectorBinder()) {
                this.get_contentSelectorBinder().remove_onItemCommand(this._binderItemCommandDelegate);
            }
            delete this._binderItemCommandDelegate;
        }
        if (this._binderItemDataBoundDelegate) {
            if (this.get_contentSelectorBinder()) {
                this.get_contentSelectorBinder().remove_onItemDataBound(this._binderItemDataBoundDelegate);
            }
            delete this._binderItemDataBoundDelegate;
        }
        if (this._promptCommandDelegate) {
            if (this._shareContentDialog) {
                this._shareContentDialog.remove_command(this._promptCommandDelegate);
            }
            if (this._unshareContentDialog) {
                this._unshareContentDialog.remove_command(this._promptCommandDelegate);
            }
            delete this._promptCommandDelegate;
        }
        if (this._shareContentSuccessDelegate) {
            delete this._shareContentSuccessDelegate;
        }
        if (this._saveChangesSuccessDelegate) {
            delete this._saveChangesSuccessDelegate;
        }
        if (this._getContentSuccessDelegate) {
            delete this._getContentSuccessDelegate;
        }
        if (this._serviceFailureDelegate) {
            delete this._serviceFailureDelegate;
        }
        if (this._viewPagesDelegate) {
            if (this._viewPagesLink) {
                $removeHandler(this._viewPagesLink, "click", this._viewPagesDelegate);
            }
            delete this._viewPagesDelegate;
        }
        if (this._beforeSaveChangesDelegate) {
            if (this.get_propertyEditor()) {
                this.get_propertyEditor().remove_beforeSaveChanges(this._beforeSaveChangesDelegate);
            }
            delete this._beforeSaveChangesDelegate;
        }
        if (this._completeSaveChangesDelegate) {
            if (this.get_propertyEditor()) {
                this.get_propertyEditor().remove_completeSaveChanges(this._completeSaveChangesDelegate);
            }
            delete this._completeSaveChangesDelegate;
        }
        if (this._closeDialogExtensionDelegate) {
            delete this._closeDialogExtensionDelegate;
        }
        if (this._beforeCloseDelegate) {
            if (dialogBase.get_radWindow()) {
                dialogBase.get_radWindow().remove_beforeClose(this._beforeCloseDelegate);
            }
            delete this._beforeCloseDelegate;
        }
        if (this._providersSelectorClickedDelegate) {
            if (this._providersSelector) {
                this._providersSelector.remove_onProviderSelected(this._providersSelectorClickedDelegate);
            }
            delete this._providersSelectorClickedDelegate;
        }
    },

    /* ************************* public methods ************************* */

    applyChanges: function () {
        if (this._sharedContentInitialized) {
            switch (this._currentView) {
                case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Edit:
                    this.get_controlData().Html = this._htmlEditor.get_value();
                    break;
                case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview:
                    break;
                case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.EditSharedContent:
                    this.get_controlData().Html = this._contentEditor.get_value();
                    break;
            }
        }
    },

    refreshUI: function () {
        if (this._sharedContentInitialized) {
            var controlData = this.get_controlData();
            var displayMode = controlData.IsShared ?
                Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview :
                Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Edit;

            if (this._isMissingSharedContent) {
                controlData.SharedContentID = '00000000-0000-0000-0000-000000000000';
                controlData.Html = '';
            }

            this._initializeCurrentView(displayMode, this._sharedHtml ? this._sharedHtml : controlData.Html);

            if (this.get_contentSelectorBinder()) {
                this.get_contentSelectorBinder().add_onItemCommand(this._binderItemCommandDelegate);
                this.get_contentSelectorBinder().add_onItemDataBound(this._binderItemDataBoundDelegate);
            }

            this.get_sharedContentSelector().set_providerName(controlData.ProviderName);
        }
        else {
            jQuery(this.get_propertyEditor().get_saveButton()).hide();
            jQuery(this.get_propertyEditor().get_saveAllTranslationsButton()).hide();
            jQuery(this.get_propertyEditor().get_orLabel()).hide();
            jQuery(this.get_propertyEditor().get_cancelButton()).hide();
            jQuery(this.get_propertyEditor().get_advancedModeButton()).hide();

            jQuery(this.get_propertyEditor().get_okButton()).removeClass("sfSave").show();
        }
    },

    openDialog: function (commandName, dataItem, params, key, query, assignCloseEvent) {
        var dialog = this.get_windowManager().getWindowByName(commandName);
        if (dialog) {
            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);

            if (query) {
                var url = dialog.get_navigateUrl();
                var idx = url.indexOf("?");
                if (idx > -1) {
                    url = url.substring(0, idx);
                }
                dialog.set_navigateUrl(url + query);
            }

            if (assignCloseEvent) {
                if (!dialog._sfCloseDialogExtension) {
                    dialog._sfCloseDialogExtension = this._closeDialogExtensionDelegate;
                    dialog.add_close(dialog._sfCloseDialogExtension);
                }
            }

            dialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(dialog);

            if (dialog.get_width() == 100 && dialog.get_height() == 100) {
                dialog.maximize();
            }
        }
    },

    /* ************************* event handlers ************************* */

    // handles the content selected event of the shared content selector
    _selectContent: function (items) {
        //jQuery('#selectorTag').hide();
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (items == null) return;

        if (this._sharedEditButton && this._sharedSelectorButton) {
            this._sharedEditButton.show();
            this._sharedSelectorButton.addClass("sfDisplayNone");
        }

        var selectedItems = this._sharedContentSelector.getSelectedItems();
        if (selectedItems != null) {
            if (selectedItems.length > 0) {
                var dataItem = selectedItems[0];

                this.get_controlData().SharedContentID = dataItem.Id;
                this.get_controlData().ProviderName = dataItem.ProviderName;
                this.get_controlData().ContentVersion = dataItem.Version;
                this.get_controlData().IsShared = true;
                this._affectedPages = dataItem.PagesCount;
                this._affectedPageTemplates = dataItem.PageTemplatesCount;

                this._getContent(dataItem.Id, false, true, this._selectContentSuccessDelegate);
            }
        }
    },

    _commandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case 'selectSharedContent':
                if (!this._contentSelectorIsBinded && this.get_isControlDefinitionProviderCorrect()) {
                    this._bindSharedContentSelector();                    
                }
                //jQuery('#selectorTag').show();
                if (this.get_clientManager().GetEmptyGuid() == this.get_controlData().SharedContentID)
                {
                    this._sharedContentSelector.set_selectedKeys([]);
                }
                else
                {
                    this._sharedContentSelector._selectedKeys = [this.get_controlData().SharedContentID];
                }
                this._dialog.dialog("open");
                jQuery("body > form").hide();
                dialogBase.resizeToContent();
                break;
            case 'shareContent':
                this._shareContentDialog.show_prompt();
                break;
            case 'editContent':
                this._sharedSelectorButton = jQuery("#" + this._commandBar.findItemByCommandName("selectSharedContent").ButtonClientId);
                this._sharedEditButton = jQuery("#" + this._commandBar.findItemByCommandName("editContent").ButtonClientId);
                this._initializeCurrentView(Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.EditSharedContent,
                    this._sharedHtml ? this._sharedHtml : this.get_controlData().Html);
                dialogBase.resizeToContent();
                break;
            case 'unshareContent':
                this._unshareContentDialog.show_prompt();
                break;
        }
    },

    _bindSharedContentSelector: function () {
        this._sharedContentSelector.dataBind();
        this._contentSelectorIsBinded = true;
    },

    _binderItemCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();

        if (commandName == "viewContentPages") {
            var dataItem = args.get_dataItem();
            if (dataItem) {
                this._openContentPagesDialog(dataItem.Id, dataItem.ProviderName, Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Selector);
            }
        }
    },

    _binderItemDataBoundHandler: function (sender, args) {
        var dataItem = args.get_dataItem();
        if (dataItem) {
            if (dataItem.PagesCount == 0 && dataItem.PageTemplatesCount == 0) {
                var row = args.get_itemElement();
                if (row) {
                    jQuery(row).find(".sf_binderCommand_viewContentPages").removeClass("sf_binderCommand_viewContentPages sfLnk");
                }
            }
        }
    },

    _promptCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        switch (commandName) {
            case "shareContent":
                this._shareContent(sender);
                break;
            case "unshareContent":
                this._unshareContent();
                break;
        }
    },

    _shareContent: function (sender) {
        var urlParams = [];
        urlParams["provider"] = this.get_controlData().ProviderName;
        var key = this.get_clientManager().GetEmptyGuid();
        if (this._blankDataItem.PublicationDate)
            this._blankDataItem.PublicationDate = new Date();
        var title = sender.get_inputText();
        var content = this._htmlEditor.get_value();
        this._blankDataItem.Title.Value = title;
        this._blankDataItem.Title.PersistedValue = title;
        this._blankDataItem.Content.Value = content;
        this._blankDataItem.Content.PersistedValue = content;
        var itemContext = new Object();
        itemContext["Item"] = this._blankDataItem;
        itemContext["ItemType"] = this._itemType;

        this._setLoading(true);
        this.get_clientManager().InvokePut(this._contentItemsServiceUrl, urlParams, [key], itemContext, this._shareContentSuccessDelegate, this._serviceFailureDelegate, this);
    },

    _shareContentSuccess: function (caller, sender, args) {
        // set content selector isBinded to false, so that when it is selected later,
        // it can rebind and get the newly shared item from current provider
        this._contentSelectorIsBinded = false;
        var html = caller._htmlEditor.get_value();
        var controlData = caller.get_controlData();
        controlData.SharedContentID = sender.Item.Id;
        controlData.ContentVersion = sender.Item.Version;
        controlData.IsShared = true;
        controlData.Html = html;
        caller._initializeCurrentView(Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview, html);
        this._setLoading(false);
    },

    _saveChangesSuccess: function (caller, sender, args) {
        caller.get_propertyEditor().remove_beforeSaveChanges(caller._beforeSaveChangesDelegate);
        dialogBase.get_radWindow().remove_beforeClose(caller._beforeCloseDelegate);

        if (caller._affectedPages > 0 || caller._affectedPageTemplates > 0) {
            var id = sender.Item.Id;
            var providerName = caller.get_controlData().ProviderName;
            this._setLoading(false);
            caller._openContentPagesDialog(id, providerName, Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Save, true);
        }
        else {
            this._setLoading(true);
            caller.get_propertyEditor().saveEditorChanges();
        }
    },

    _getContentSuccess: function (caller, result) {
        caller._itemContext = result;
        caller._itemContext.ProviderName = caller.get_controlData().ProviderName;

        caller._contentEditor.set_value(caller._itemContext.Item.Content.Value);

        this._setLoading(false);
    },

    _selectContentSuccess: function (caller, result) {
        var item = result.Item;
        if (caller.get_sharedContentTitleLabel())
            caller.get_sharedContentTitleLabel().innerText = item.Title.Value;
        caller.get_controlData().Html = item.Content.Value;
        caller._initializeCurrentView(Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview, item.Content.Value);
        this._setLoading(false);

        if (result.SfAdditionalInfo != null && result.SfAdditionalInfo.length != 0) {
            var editAccess = $.grep(result.SfAdditionalInfo, function (e) { return e.Key == "PermissionsEdit"; });

            if (editAccess.length == 1) {
                this._modifyAllowed = editAccess[0].Value;                

                if (!this._modifyAllowed)
                {
                    jQuery(".sfContentBlocksBtnAreaTop").hide();
                }
            }                        
        }

        dialogBase.resizeToContent();
    },

    _serviceFailure: function (sender, args) {
        alert(sender.Detail);
        this._setLoading(false);
    },

    _viewPages: function (sender, args) {
        var controlData = this.get_controlData();
        var id = controlData.SharedContentID;
        var providerName = controlData.ProviderName;
        this._openContentPagesDialog(id, providerName, Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Preview);
    },

    // the event is fired when the user choses save, before the data processing
    _beforeSaveChangesHandler: function (sender, eventArgs) {
        if (this._currentView == Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.EditSharedContent) {
            eventArgs.set_cancel(true);
            this._saveChanges();
        }
        else {
            this._setLoading(true);
        }
    },

    // the event is fired when the user choses saved, after the data processing
    //arg - contains the result from the executed service call
    _completeSaveChangesHandler: function (arg) {
        this._setLoading(false);
    },

    _loadDialogExtension: function (sender, e) {
        var args = sender._sfArgs;

        sender.remove_pageLoad(sender._sfLoadDialogExtension);
        sender._sfShowDialogExtension(sender, args, true);
    },

    _showDialogExtension: function (sender, e, isLoad) {
        var args = sender._sfArgs;
        var commandName = args.get_commandName();
        var dataItem = args.get_dataItem();
        var itemsList = args.get_itemsList();
        var dialog = args.get_dialog();
        var params = args.get_params();
        var key = args.get_key();
        var commandArgument = args.get_commandArgument();

        var frameHandle = sender.get_contentFrame().contentWindow;
        if (frameHandle) {
            frameHandle.scrollTo(0, 0);
            //check if the show is called on dialog that is reloaded on each showing.
            //If this is the case the createDialog method must be called on load, not on show.
            if (frameHandle.createDialog && (!dialog.get_reloadOnShow() || isLoad)) {
                if (frameHandle.detailFormView) {
                    frameHandle.detailFormView.set_displayMode("1");
                }
                frameHandle.createDialog(commandName, dataItem, itemsList, dialog, params, key, commandArgument);
            }
        }
    },

    _closeDialogExtension: function (sender, args) {
        var argument = args.get_argument();
        if (argument && argument == "saveEditorChanges") {
            this._setLoading(true);
            this.get_propertyEditor().saveEditorChanges();
            sender.remove_close(this._closeDialogExtensionDelegate);
        }
    },

    _beforeCloseHandler: function (sender, args) {
        if (this._itemContext) {
            args.set_cancel(true);
            var context = { argument: args.get_argument() };

            this._unlockContent(this._unlockContentSuccess, this._serviceFailure, context);
        }
    },

    _unlockContentSuccess: function (caller, data, executer, context) {
        var argument = (context) ? context.argument : null;
        dialogBase.close(argument);
        caller._setLoading(false);
    },

    _handleProvidersChanged: function (sender, args) {
        var controlData = this.get_controlData();
        if (controlData && controlData.hasOwnProperty('ProviderName')) {
            var oldProviderName = controlData.ProviderName;
            controlData.ProviderName = args.ProviderName;
            if (args.ProviderName != oldProviderName) {
                this.get_sharedContentSelector().set_providerName(args.ProviderName);
                this._bindSharedContentSelector();
                if (this.get_message()) {
                    this.get_message().hide();
                }
            }
        }
        dialogBase.resizeToContent();
    },

    /* ************************* private methods ************************* */

    _setLoading: function (isLoading) {
        if (isLoading) {
            jQuery(".rwWindowContent", top.document).addClass("rwLoading");
            jQuery("iframe[name='PropertyEditorDialog']", top.document).css("position", "absolute");
        }
        else {
            jQuery(".rwWindowContent", top.document).removeClass("rwLoading");
            jQuery("iframe[name='PropertyEditorDialog']", top.document).css("position", "");
        }
    },

    _initializeCurrentView: function (displayMode, html) {
        var item = this._commandBar.findItemByCommandName("editContent");

        switch (displayMode) {
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Edit:
                this._initializeEditView(html);
                break;
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview:
                this._initializePreviewView(html, item);
                break;
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.EditSharedContent:
                this._initializeEditSharedContentView(html, item);
                break;
        }
    },

    _unshareContent: function () {
        var controlData = this.get_controlData();
        controlData.SharedContentID = this.get_clientManager().GetEmptyGuid();
        //controlData.ProviderName = null;
        controlData.ContentVersion = 0;
        controlData.IsShared = false;
        this._affectedPages = 0;
        this._affectedPageTemplates = 0;
        if (this.get_sharedContentTitleLabel())
            this.get_sharedContentTitleLabel().innerHTML = "";

        this._initializeCurrentView(
            Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Edit,
            this._sharedHtml ? this._sharedHtml : controlData.Html);

        if (this._itemContext) {
            var that = this;
            var unlockContentCallback = function () {
                that._setLoading(false);
            }
            this._unlockContent(unlockContentCallback, unlockContentCallback);
        }
    },

    _openContentPagesDialog: function (id, contentProviderName, openContentPagesMode, isSuccessfullyUpdatedDialog) {
        var query = String.format("?contentId={0}&contentProviderName={1}&uiCulture={2}", id, contentProviderName, this._currentLanguage);
        if (isSuccessfullyUpdatedDialog) {
            query += "&isSuccessfullyUpdatedDialog=true";
        }
        this.openDialog("contentPages", null, [], [], query, isSuccessfullyUpdatedDialog);

        switch (openContentPagesMode) {
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Selector:
                var openedWnd = this._windowManager.getWindowByName("contentPages");
                openedWnd.set_modal(false);
                openedWnd.set_cssClass("sfWindowInWindow");
                jQuery(this._dialog).hide();
                jQuery("body > form").show();
                jQuery("body").addClass("sf_RadWinOpenedFromKendoDlg");
                openedWnd.maximize();
                break;
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Preview:
            case Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.Save:
                var openedWnd = this._windowManager.getWindowByName("contentPages");
                openedWnd.removeCssClass("sfWindowInWindow");
                openedWnd.restore();
                openedWnd.set_width(425);
                openedWnd.set_height(250);
                openedWnd.set_modal(true);
                openedWnd.set_autoSizeBehaviors(5);
                Telerik.Sitefinity.centerWindowHorizontally(openedWnd);
                jQuery("body").removeClass("sf_RadWinOpenedFromKendoDlg");
                break;
        }
    },

    _getContent: function (contentId, checkOut, published, successDelegate) {
        var urlParams = [];
        var controlData = this.get_controlData();
        urlParams["provider"] = controlData.ProviderName;
        urlParams["checkOut"] = checkOut;
        urlParams["published"] = published;

        var that = this;
        window.setTimeout(function () { that._setLoading(true) }, 0);
        this.get_clientManager().InvokeGet(this._contentItemsServiceUrl, urlParams, [contentId], successDelegate, this._serviceFailureDelegate, this);
    },

    _saveChanges: function () {
        var urlParams = [];
        var controlData = this.get_controlData();
        urlParams["provider"] = controlData.ProviderName;
        if (typeof (this.get_propertyEditor) != "undefined") {
            var propEdit = this.get_propertyEditor();
            if ((typeof (propEdit) != "undefined") && (propEdit != null) && (typeof (propEdit._pageId) != "undefined") && (propEdit._pageId != null)) {
                urlParams["draftPageId"] = propEdit._pageId;
            }
        }

        var key = controlData.SharedContentID;
        var content = this._contentEditor.get_value();
        this._itemContext.Item.Content.Value = content;
        this._itemContext.Item.Content.PersistedValue = content;

        this._setLoading(true);
        this.get_clientManager().InvokePut(this._contentItemsServiceUrl, urlParams, [key], this._itemContext, this._saveChangesSuccessDelegate, this._serviceFailureDelegate, this);
    },

    _initializeEditView: function (html) {
        this._currentView = Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Edit;

        if (html) {
            this._htmlEditor.set_value(html);
        }

        jQuery("#editView").show();
        jQuery("#previewAndEditContentView").hide();

        jQuery(this.get_propertyEditor().get_saveButton()).find(".sfLinkBtnIn").html(this._clientLabelManager.getLabel("Labels", "Save"));
    },

    _initializePreviewView: function (html, item) {
        this._currentView = Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.Preview;

        this._contentLabel.innerHTML = html;

        jQuery("#editView").hide();
        jQuery("#previewAndEditContentView").show();

        jQuery(this._contentLabel).show();
        jQuery(this._contentEditor.get_element()).hide();
        if (item) {
            jQuery("#" + item.ButtonClientId).show();
        }

        this._sharedContentLabel.innerHTML = this._clientLabelManager.getLabel("ContentResources", "ThisContentIsShared");
        if (this._affectedPages == 0 && this._affectedPageTemplates == 0) {
            jQuery(this._viewPagesLink).hide();
        }
        else {
            jQuery(this._viewPagesLink).show();
            this._viewPagesLink.innerHTML = this._clientLabelManager.getLabel("ContentResources", "ViewAffectedPages");
        }

        if (this._modifyAllowed) {
            jQuery(this.get_propertyEditor().get_saveButton()).find(".sfLinkBtnIn").html(this._clientLabelManager.getLabel("Labels", "SaveChanges"));
        }
        else {
            jQuery(this.get_propertyEditor().get_saveButton()).hide();
            jQuery(this.get_propertyEditor().get_saveAllTranslationsButton()).hide();
            jQuery(this.get_propertyEditor().get_orLabel()).hide();
            jQuery(this.get_propertyEditor().get_cancelButton()).hide();

            jQuery(this.get_propertyEditor().get_okButton()).removeClass("sfSave").show();
            jQuery(this.get_propertyEditor().get_okButton()).find(".sfLinkBtnIn").html(this._clientLabelManager.getLabel("Labels", "Close"));
        }
    },

    _initializeEditSharedContentView: function (html, item) {
        this._currentView = Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.EditSharedContent;

        if (html) {
            this._contentEditor.set_value(html);
        }

        jQuery("#editView").hide();
        jQuery("#previewAndEditContentView").show();

        jQuery(this._contentLabel).hide();
        jQuery(this._contentEditor.get_element()).show();
        this._contentEditor.get_editControl().repaint();

        if (this._sharedEditButton && this._sharedSelectorButton) {
            this._sharedEditButton.hide();
            this._sharedSelectorButton.removeClass("sfDisplayNone");
        }

        this._sharedContentLabel.innerHTML = this._clientLabelManager.getLabel("ContentResources", "ThisContentIsShared");
        if (this._affectedPages == 0 && this._affectedPageTemplates == 0) {
            jQuery(this._viewPagesLink).hide();
        }
        else {
            jQuery(this._viewPagesLink).show();
            this._viewPagesLink.innerHTML = this._clientLabelManager.getLabel("ContentResources", "ViewAffectedPages");
        }

        jQuery(this.get_propertyEditor().get_saveButton()).find(".sfLinkBtnIn").html(this._clientLabelManager.getLabel("Labels", "SaveChanges"));

        this._getContent(this.get_controlData().SharedContentID, true, null, this._getContentSuccessDelegate);
    },

    _unlockContent: function (successDelegate, failureDelegate, context) {
        var itemId = this._itemContext.Item.Id;
        var providerName = this._itemContext.ProviderName;
        this._itemContext = null;

        var urlParams = [];
        urlParams["provider"] = providerName;
        var keys = ["temp", itemId];

        this._setLoading(true);
        this.get_clientManager().InvokeDelete(
            this._contentItemsServiceUrl,
            urlParams,
            keys,
            successDelegate,
            failureDelegate,
            this,
            context);
    },

    /* ************************* properties ************************* */

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    get_htmlEditor: function () {
        return this._htmlEditor;
    },
    // gets the reference to the rad editor control used to edit the
    // html content of the ContentBlock control
    set_htmlEditor: function (value) {
        this._htmlEditor = value;
    },

    get_sharedContentSelector: function () {
        return this._sharedContentSelector;
    },
    set_sharedContentSelector: function (value) {
        this._sharedContentSelector = value;
    },

    get_editViewCommandBar: function () {
        return this._editViewCommandBar;
    },
    set_editViewCommandBar: function (value) {
        this._editViewCommandBar = value;
    },

    get_shareContentDialog: function () {
        return this._shareContentDialog;
    },
    set_shareContentDialog: function (value) {
        this._shareContentDialog = value;
    },

    get_unshareContentDialog: function () {
        return this._unshareContentDialog;
    },
    set_unshareContentDialog: function (value) {
        this._unshareContentDialog = value;
    },

    get_clientManager: function () {
        if (this._clientManager == null) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
            this._clientManager.set_uiCulture(this._uiCulture);
        }
        return this._clientManager;
    },

    get_contentItemsServiceUrl: function () {
        return this._contentItemsServiceUrl;
    },
    set_contentItemsServiceUrl: function (value) {
        this._contentItemsServiceUrl = value;
    },

    get_commandBar: function () {
        return this._commandBar;
    },
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    get_viewPagesLink: function () {
        return this._viewPagesLink;
    },
    set_viewPagesLink: function (value) {
        this._viewPagesLink = value;
    },

    get_contentLabel: function () {
        return this._contentLabel;
    },
    set_contentLabel: function (value) {
        this._contentLabel = value;
    },

    get_uiCulture: function () {
        return this._uiCulture;
    },
    set_uiCulture: function (value) {
        this._uiCulture = value;
    },

    get_contentEditor: function () {
        return this._contentEditor;
    },
    set_contentEditor: function (value) {
        this._contentEditor = value;
    },

    get_windowManager: function () {
        return this._windowManager;
    },
    set_windowManager: function (value) {
        this._windowManager = value;
    },

    get_contentSelectorBinder: function () {
        if (this._contentSelectorBinder == null) {
            this._contentSelectorBinder = this._sharedContentSelector.get_binder();
        }
        return this._contentSelectorBinder;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_sharedContentLabel: function () {
        return this._sharedContentLabel;
    },
    set_sharedContentLabel: function (value) {
        this._sharedContentLabel = value;
    },

    get_currentLanguage: function () {
        return this._currentLanguage;
    },
    set_currentLanguage: function (value) {
        this._currentLanguage = value;
    },
    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },

    get_providersSelector: function () {
        return this._providersSelector;
    },
    set_providersSelector: function (value) {
        this._providersSelector = value;
    },

    get_isControlDefinitionProviderCorrect: function () {
        return this._isControlDefinitionProviderCorrect;
    },
    set_isControlDefinitionProviderCorrect: function (value) {
        this._isControlDefinitionProviderCorrect = value;
    },

    get_message: function () {
        return this._message;
    },
    set_message: function (value) {
        this._message = value;
    },

    get_sharedContentTitleLabel: function () {
        return this._sharedContentTitleLabel;
    },
    set_sharedContentTitleLabel: function (value) {
        this._sharedContentTitleLabel = value;
    }
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner.registerClass('Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

// ------------------------------------------------------------------------
// Display mode enum
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode = function () {
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.prototype = {
    Edit: 0,
    Preview: 1,
    EditSharedContent: 2
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode.registerEnum("Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockDesignerDisplayMode");
// ------------------------------------------------------------------------
// Content pages dialog display mode enum
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode = function () {
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.prototype = {
    Selector: 0,
    Preview: 1,
    Save: 2
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode.registerEnum("Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentPagesDisplayMode");

// ------------------------------------------------------------------------
// Dialog Event Args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Modules.GenericContent.Web.UI.DialogEventArgs =
function (
    commandName,
    dataItem,
    itemsList,
    dialog,
    params,
    key,
    commandArgument) {
    this._commandName = commandName;
    this._dataItem = dataItem;
    this._itemsList = itemsList;
    this._dialog = dialog;
    this._params = params;
    this._key = key;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.Modules.GenericContent.Web.UI.DialogEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Modules.GenericContent.Web.UI.DialogEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.CommandEventArgs.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.GenericContent.Web.UI.CommandEventArgs.callBaseMethod(this, 'dispose');
    },

    get_commandName: function () {
        return this._commandName;
    },

    get_dataItem: function () {
        return this._dataItem;
    },

    get_itemsList: function () {
        return this._itemsList;
    },

    get_params: function () {
        return this._params;
    },

    get_key: function () {
        return this._key;
    },

    get_dialog: function () {
        return this._dialog;
    },

    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Modules.GenericContent.Web.UI.DialogEventArgs.registerClass('Telerik.Sitefinity.Modules.GenericContent.Web.UI.DialogEventArgs', Sys.CancelEventArgs);