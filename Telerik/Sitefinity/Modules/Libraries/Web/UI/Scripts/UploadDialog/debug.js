Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog.initializeBase(this, [element]);

    this._backButton = null;
    this._fileUpload = null;
    this._librarySelector = null;
    this._taxonomySelectorId = null;
    this._parentLibrarySelector = null;
    this._successCommandBar = null;
    this._messageControl = null;

    this._dialogTitle = null;
    this._uploadText = null;
    this._itemsName = null;
    this._itemName = null;
    this._libraryName = null;
    this._selecteALibraryText = null;
    this._webServiceUrl = null;
    this._parentWebServiceUrl = null;

    this._itemType = null;
    this._libraryType = null;

    this._itemsToUploadCount = 0;
    this._itemsPersistedSoFar = 0;
    this._persistedContentIds = [];
    this._isUploading = false;
    this._filesSelectedDelegate = null;
    this._uploadCompleteDelegate = null;
    this._fileRemovedDelegate = null;
    this._commandBarCommandDelegate = null;
    this._closeDialogDelegate = null;
    this._handlePageLoadDelegate = null;

    this._blankDataItem = null;
    this._blankLibraryDataItem = null;
    this._isDialogDirty = false;
    this._selectedLibraryId = null;
    this._isChanged = false;
    this._clientLabelManager = null;
    this._baseList = null;
    this._isMultilingual = false;
    this._defaultLanguage = null;
    this._commandArgument = null;
    this._workflowMenuCommandDelegate = null;
    this._currentWorkflowOperation = "";
    this._workflowMenu = null;
    this._itemsCount = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        if (this._filesSelectedDelegate == null) {
            this._filesSelectedDelegate = Function.createDelegate(this, this._filesSelected);
        }
        if (this._uploadCompleteDelegate == null) {
            this._uploadCompleteDelegate = Function.createDelegate(this, this._uploadCompleted);
        }
        if (this._fileUploadFailedDelegate == null) {
            this._fileUploadFailedDelegate = Function.createDelegate(this, this._fileUploadFailed);
        }
        if (this._fileRemovedDelegate == null) {
            this._fileRemovedDelegate = Function.createDelegate(this, this._fileRemoved);
        }

        if (this._fileUpload != null) {
            this._fileUpload.add_filesSelected(this._filesSelectedDelegate);
            this._fileUpload.add_uploadCompleted(this._uploadCompleteDelegate);
            this._fileUpload.add_fileUploadFailed(this._fileUploadFailedDelegate);
            this._fileUpload.add_fileRemoved(this._fileRemovedDelegate);
        }

        if (this._closeDialogDelegate == null) {
            this._closeDialogDelegate = Function.createDelegate(this, this._closeDialog);
            this._windowCloseDelegate = Function.createDelegate(this, this._windowCloseHandler);
            //window.onbeforeunload = this._windowCloseDelegate;
        }

        if (this._workflowMenu != null) {
            this._workflowMenuCommandDelegate = Function.createDelegate(this, this._workflowMenuCommandHandler);
            this._workflowMenu.set_messageControl(this.get_messageControl());
            this._workflowMenu.add_command(this._workflowMenuCommandDelegate);
            $addHandler(this._workflowMenu.get_cancelLink(), 'click', this._closeDialogDelegate);
        }

        if (this._successCommandBar != null) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBar_Command);
            this._successCommandBar.add_command(this._commandBarCommandDelegate);
        }

        if (this._backButton != null) {
            $addHandler(this._backButton, 'click', this._closeDialogDelegate);
        }

        if (this._blankDataItem) {
            this._blankDataItem = this._deserializeBlankDataItem(this._blankDataItem);
        }

        if (this._blankLibraryDataItem) {
            this._blankLibraryDataItem = this._deserializeBlankDataItem(this._blankLibraryDataItem);
        }

        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        Sys.Application.add_load(this._handlePageLoadDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._filesSelectedDelegate != null) {
            this._fileUpload.remove_filesSelected(this._filesSelectedDelegate);
            delete this._filesSelectedDelegate;
        }
        if (this._uploadCompleteDelegate != null) {
            this._fileUpload.remove_uploadCompleted(this._uploadCompletedDelegate);
            delete this._uploadCompletedDelegate;
        }
        if (this._fileUploadFailedDelegate != null) {
            this._fileUpload.remove_fileUploadFailed(this._fileUploadFailedDelegate);
            delete this._fileUploadFailedDelegate;
        }
        if (this._fileRemovedDelegate != null) {
            this._fileUpload.remove_fileRemoved(this._fileRemovedDelegate);
            delete this._fileRemovedDelegate;
        }

        if (this._successCommandBar != null) {
            this._successCommandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }
        if (this._workflowMenu != null) {
            this._workflowMenu.remove_command(this._workflowMenuCommandDelegate);
            delete this._workflowMenuCommandDelegate;
            $removeHandler(this._workflowMenu.get_cancelLink(), 'click', this._closeDialogDelegate);
        }

        if (this._backButton != null) {
            $removeHandler(this._backButton, 'click', this._closeDialogDelegate);
        }

        if (this._closeDialogDelegate != null) {
            delete this._closeDialogDelegate;
            $removeHandler(window, 'beforeunload', this._windowCloseHandler);
            delete this._windowCloseHandler;
        }
        if (this._handlePageLoadDelegate) {
            Sys.Application.remove_load(this._handlePageLoadDelegate);
            delete this._handlePageLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog.callBaseMethod(this, "dispose");
    },

    _handlePageLoad: function (sender, args) {
        var handler = this.get_events().getHandler('viewLoaded');
        if (handler) {
            handler(this);
        }
        

    },

    /* --------------------  public methods ----------- */

    // Prepares the dialog.
    prepareDialog: function (commandName, dataItem, self, dialog, params, key, commandArgument) {
        this._itemsName = dialogBase.getQueryValue('itemsName', true);
        this._itemName = dialogBase.getQueryValue('itemName', true);
        this._libraryName = dialogBase.getQueryValue('libraryTypeName', true);
        this._dialogTitle.innerHTML = String.format(this._uploadText, this._itemsName);

        this._baseList = self;
        this._commandArgument = this._getLanguageCommandArgumentOrDefault(commandName, commandArgument);
        
        if (this._workflowMenu) {
            if (this._baseList && this._baseList.get_uiCulture && this._baseList.get_uiCulture()) {
                this._workflowMenu.set_contentCulture(this._baseList.get_uiCulture());
            }
            this._workflowMenu.bindWorkflowVisuals(this._itemType, null, Telerik.Sitefinity.getEmptyGuid(), null);
        }

        this.get_parentLibrarySelector().reset();

        if (dataItem && dataItem.FolderId) {
            this._selectedLibraryId = dataItem.FolderId;
        }
        // this processes upload button clicked from a list of Libraries screen
        else if (params && params["LibraryId"] && params["LibraryId"] != "{{Id}}") {
            this._selectedLibraryId = params["LibraryId"];
        }
        // this processes upload button clicked in Images from library screen
        else if (params && params["parentId"]) {
            this._selectedLibraryId = params["parentId"];
        }

        if (this._selectedLibraryId) {
            this.get_parentLibrarySelector().set_providerName(params['providerName']);
            this.get_parentLibrarySelector().set_value(this._selectedLibraryId);
        }
        var urlParams = [];
        urlParams['itemType'] = this._libraryType;
        urlParams['provider'] = this.get_parentLibrarySelector().get_providerName();

        var binderUrlParams = this.get_parentLibrarySelector().get_pageSelector().get_treeBinder().get_urlParams();
        if (binderUrlParams['take'] === undefined || binderUrlParams['take'] === null) {
            binderUrlParams['take'] = this.get_itemsCount();
        }

        this._fileUpload.set_libraryProviderName(params['providerName']);

        this.get_parentLibrarySelector().rebind(params['providerName']);

        this._enableDisableWorkflowMenu(false);
        $addHandler(window, 'beforeunload', this._windowCloseHandler);

        //TeamPulse #162336: force Firefox to redraw the element to make it responding again.
        if (jQuery.browser.mozilla) {
            var pluginObj = jQuery("#slPlugin");
            pluginObj.attr("width", "99%");
            window.setTimeout(function () { pluginObj.attr("width", "100%"); }, 0);
        }

        var taxonomySelector = $find(this._taxonomySelectorId.id);
        var fieldControls = taxonomySelector.get_fieldControlIds();
        for (var i = 0, length = fieldControls.length; i < length; i++) {
            var control = $find(fieldControls[i]);
            if (this._baseList && this._baseList.get_uiCulture && this._baseList.get_uiCulture() && control.set_uiCulture) {    
                control.set_uiCulture(this._baseList.get_uiCulture());
            }
        }
        //===================================================================================
    },

    /* -------------------- events -------------------- */

    _enableDisableWorkflowMenu: function (toEnable) {
        if (toEnable)
            $(this._workflowMenu.get_element()).show();
        else
            $(this._workflowMenu.get_element()).hide();
        //TODO: enable disable after RC        

        //        var elements = this._workflowMenu._widgetDomElements;
        //        if (toEnable) {
        //            jQuery.each(elements, function () {
        //                var el = this;
        //                $telerik.removeCssClasses(el, ['sfDisabledLinkBtn']);
        //                $(el).attr("href", " javascript: __doPostBack('" + el.id + "', '')");
        //            });
        //        }
        //        else {
        //            jQuery.each(elements, function () {
        //                var el = this;
        //                $telerik.addCssClasses(el, ['sfDisabledLinkBtn']);
        //                //TODO: this fix removes the link with postback error at FF, but still a  new window is opened when "Middle" mouse button is pressed.
        //                //Better aproach would be link to be replaced with span with appropriate styles.
        //                //$(el).replaceWith("<span class='" + $(el).css() +  "'>" + $(el).text() + "<span/>");
        //                $(el).attr("href", "");
        //            });
        //        }
    },

    /* -------------------- event handlers ------------ */
    _filesSelected: function () {
        $(this._librarySelector).show();
        $(this._taxonomySelectorId).show();
        this._isChanged = true;

        this._workflowMenu._afterRequest();
        this._enableDisableWorkflowMenu(true);
        this._currentWorkflowOperation = "";

    },

    _uploadCompleted: function () {
        this._isUploading = false;
        this._isDialogDirty = true;
        this._isChanged = false;
        this._prepareStepThree();
        this._enableDisableWorkflowMenu(false);
        this._fileUpload.clearSelectedFiles();
    },

    _fileUploadFailed: function (sender, fileUploadFailedArgs) {
        //TODO: gather exceptions like this (fileUploadFailedArgs._exception) and remove alert;
    },

    _fileRemoved: function () {
        if (this.get_fileUpload().get_selectedFiles().length === 0) {
            $(this._librarySelector).hide();
            $(this._taxonomySelectorId).hide();
            this._isChanged = false;

            this._enableDisableWorkflowMenu(false);
            this._currentWorkflowOperation = "";
        }
    },

    _commandBar_Command: function (sender, args) {
        switch (args.get_commandName()) {
            case 'upload':
                this._saveMediaItems();
                break;
            case 'cancel':
                //this._prepareStepOne();
                this._closeDialog();
                break;
            case 'batchEdit':
                alert('In process of implementation.');
                break;
            case 'viewAll':
                this._prepareStepOne();
                this._closeDialog();
                break;
            case 'uploadOther':
                this._prepareStepOne();
                break;
        }
    },

    _workflowMenuCommandHandler: function (sender, args) {
        var contentCommandName = args.get_commandName();
        var workflowOperation = args.get_commandArgument();

        switch (contentCommandName) {
            case 'Upload':
            case 'UploadDraft':
                this._currentWorkflowOperation = workflowOperation;
                this._saveMediaItems();
                break;
        }
    },

    _contentItemSave_Success: function (caller, result, requestContext) {

        var uploadOrdinal = requestContext.get_userContext().Context.uploadOrdinal;
        // Keep the upload order
        caller._persistedContentIds[uploadOrdinal] = result.Item.Id;
        caller._itemsPersistedSoFar++;
        if (caller._itemsPersistedSoFar == caller._itemsToUploadCount) {
            //get SF_UI_CULTURE from the headers of the request
            var langauge = requestContext._headers[Telerik.Sitefinity.Data.ClientManager.UiCultureHeaderKey];
            caller._fileUpload.uploadFiles(caller._persistedContentIds, caller.getWorkflowOperation(), langauge);
            caller._itemsPersistedSoFar = 0;
        }
    },

    _contentItemSave_Failure: function (result) {
        var caller = this.Caller;
        caller._isUploading = false;
        caller._messageControl.showNegativeMessage(result.Detail);
        caller._enableDisableWorkflowMenu(false);
    },

    getWorkflowOperation: function () {
        return this._currentWorkflowOperation;
    },


    /* -------------------- private methods ----------- */

    _promptCommandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case "createLibrary":
                var newLibraryName = args.get_commandArgument();
                this._createAlbumSimple(newLibraryName);
                args.set_cancel(true);
                break;
        }
    },

    _createAlbumSimple: function (albumName) {
        var dataObject = this._clone(this._blankLibraryDataItem);

        dataObject.Name = albumName;
        dataObject.Title = { PersistedValue: albumName };

        // ensure that you have last modified set
        var currentDate = new Date();
        if (dataObject['LastModified'] == null) {
            dataObject['LastModified'] = currentDate;
        }
        if (dataObject['DateCreated'] == null) {
            dataObject['DateCreated'] = currentDate;
        }
        if (dataObject['PublicationDate'] == null) {
            dataObject['PublicationDate'] = currentDate;
        }

        var itemContext = {
            'Item': dataObject,
            'ItemType': this._libraryType
        };

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._parentWebServiceUrl;
        var urlParams = [];
        urlParams['provider'] = this.get_parentLibrarySelector().get_providerName();
        var key = clientManager.GetEmptyGuid();
        clientManager.InvokePut(serviceUrl, urlParams, [key], itemContext, this._createAlbumSuccess, this._createAlbumFailure, this);
    },

    _createAlbumSuccess: function (target, data, webRequest) {
        if (data && data.Item) {
            target._selectedLibraryId = data.Item.Id;
        }
    },

    _createAlbumFailure: function (result) {
        alert(result.Detail);
    },

    _getLanguageCommandArgumentOrDefault: function (commandName, commandArgument) {
        if (!commandArgument) {
            commandArgument = {};
        }
        if (!commandArgument.language) {
            //Attempt to get the selected language in the list that have opened the dialog.
            if (this._baseList && this._baseList.get_uiCulture && this._baseList.get_uiCulture()) {
                commandArgument.language = this._baseList.get_uiCulture();
            }
            else {
                commandArgument.language = this._defaultLanguage;
            }
        }
        if (!commandArgument.languageMode) {
            commandArgument.languageMode = commandName;
        }
        return commandArgument;
    },

    _saveMediaItems: function () {
        this._fileUpload.set_libraryProviderName(this.get_parentLibrarySelector().get_providerName());
        var parentId = this._getLibraryId();
        if (parentId == null || parentId === "00000000-0000-0000-0000-000000000000") { // YouMustSelectLibraryNameInWhichToUploadItemName
            var msg = this.get_clientLabelManager().getLabel("LibrariesResources", "YouMustSelectLibraryNameInWhichToUploadItemName");
            var msg = String.format(msg, this._libraryName.toLowerCase(), this._itemsName.toLowerCase());

            alert(msg);

            if (this._workflowMenu) {
                this._workflowMenu.cancelCommand();
            }

            return;
        }

        this._fileUpload.set_libraryId(parentId);

        this._prepareStepTwo();

        var contentId = Telerik.Sitefinity.getEmptyGuid();
        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        var serviceUrl = this._webServiceUrl;
        var urlParams = [];
        urlParams['itemType'] = this._itemType;
        urlParams['provider'] = this.get_parentLibrarySelector().get_providerName();
        urlParams['parentItemType'] = this._libraryType;
        urlParams['newParentId'] = parentId;
        var keys = ['parent', parentId, contentId];
        if (this._commandArgument && this._commandArgument.language)
            clientManager.set_uiCulture(this._commandArgument.language);

        var blankItem = this._createCommonMediaItem(this._blankDataItem);

        var selectedFiles = this._fileUpload.get_selectedFiles();
        this._itemsToUploadCount = selectedFiles.length;
        if (this._itemsToUploadCount > 0) {
            this._isUploading = true;
            for (filesIter = 0; filesIter < this._itemsToUploadCount; filesIter++) {
                var mediaItem = this._createMediaItem(selectedFiles[filesIter], blankItem);
                var context = { uploadOrdinal: filesIter };
                clientManager.InvokePut(serviceUrl, urlParams, keys, mediaItem, this._contentItemSave_Success, this._contentItemSave_Failure, this, null, null, context);
            }
        }
    },

    _createMediaItem: function (fileName, blankItem) {
        var itemContext = new Object();

        var dataObject = this._clone(blankItem);
        dataObject.Id = Telerik.Sitefinity.getEmptyGuid();
        var extension = "";
        var extensionIndex = fileName.lastIndexOf('.');
        if (extensionIndex > 0) {
            fileName = fileName.substring(0, extensionIndex);
            extension = fileName.substring(extensionIndex, fileName.length);
        }
        dataObject.Title.PersistedValue = fileName;
        dataObject.Title.Value = fileName;
        dataObject.Extension = extension;

        itemContext['Item'] = dataObject;
        return itemContext;
    },

    _createCommonMediaItem: function (blankItem) {
        var dataObject = this._clone(blankItem);

        var taxonomySelector = $find(this._taxonomySelectorId.id);
        var fieldControls = taxonomySelector.get_fieldControlIds();
        for (var i = 0, length = fieldControls.length; i < length; i++) {
            var control = $find(fieldControls[i]);

            var propertyName = control.get_dataFieldName();
            var propertyValue = control.get_value();
            if (propertyValue != null) {
                if (dataObject[propertyName] && dataObject[propertyName].hasOwnProperty('Value')) {
                    dataObject[propertyName].Value = propertyValue;
                } else {
                    dataObject[propertyName] = propertyValue;
                }
            }

        }
        dataObject.UIStatus = 0;
        return dataObject;
    },

    _resetFieldControls: function () {
        var taxonomySelector = $find(this._taxonomySelectorId.id);
        taxonomySelector.reset();
        var fieldControls = taxonomySelector.get_fieldControlIds();
        for (var i = 0, length = fieldControls.length; i < length; i++) {
            var control = $find(fieldControls[i]);
            control.reset();

            var propertyName = control.get_dataFieldName();
            var propertyValue = this._blankDataItem[propertyName];

            if (propertyValue && propertyValue.hasOwnProperty('Value'))
                propertyValue = this._blankDataItem[propertyName].Value;

            control.set_value(propertyValue);
        }
    },

    _clone: function (obj) {
        var clone = jQuery.extend(true, {}, obj);
        this._fixClone(clone, obj);
        return clone;
    },

    _fixClone: function (obj, original) {
        for (var property in obj) {
            var val = original[property];
            if (val && (typeof val == 'object')) {
                if (val.constructor == Date) {
                    obj[property] = val;
                }
                else {
                    this._fixClone(obj[property], val);
                }
            }
        }
    },

    _getLibraryId: function () {
        return this.get_parentLibrarySelector().get_value();
    },

    _prepareStepOne: function () {
        $(this._successCommandBar.get_element()).hide();
        $(this._librarySelector).hide();
        $(this._taxonomySelectorId).hide();
        this._messageControl.hide();
        var title = String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "WhichItemNameToUpload"), this._itemsName);
        this._fileUpload.set_title(title);
        this._fileUpload.resetUploader();
        this._persistedContentIds = [];
        this._resetFieldControls();
    },

    _prepareStepTwo: function () {
        $(this._librarySelector).hide();
        $(this._taxonomySelectorId).hide();
        this._fileUpload.set_title(this.get_clientLabelManager().getLabel("LibrariesResources", "Uploading") + "...");
    },

    _prepareStepThree: function () {
        this._workflowMenu._afterRequest();
        $(this._workflowMenu.get_element()).hide();
        this._fileUpload.set_title(this.get_clientLabelManager().getLabel("LibrariesResources", "UploadDone"));
        var filesCount = this._fileUpload.get_selectedFilesCount();
        if (filesCount > 0) {
            var format;
            if (filesCount == 1) {
                format = this.get_clientLabelManager().getLabel("LibrariesResources", "OneItemHasBeenSuccessfullyUploaded");
            }
            else {
                format = this.get_clientLabelManager().getLabel("LibrariesResources", "MultipleItemsHaveBeenSuccessfullyUploaded");
            }

            message = String.format(format, filesCount, this._itemName.toLowerCase());

            this._messageControl.showPositiveMessage(message);
        }
        $(this._successCommandBar.get_element()).show();
    },

    _closeDialog: function () {
        if (this._isUploading == true) {
            var msg = this.get_clientLabelManager().getLabel("Labels", "YouWantToLeavePage");
            if (!confirm(String.format(msg, this.get_clientLabelManager().getLabel("LibrariesResources", "YouHaveUnuploadedItemsWantToInterruptUploading")))) {
                return;
            }
            else {
                this._isDialogDirty = true;
            }
        }
        else if (this._isChanged == true) {
            if (!confirm(this.get_clientLabelManager().getLabel("LibrariesResources", "YouHaveUnsavedChangesWantToLeavePage"))) {
                return;
            }
        }
        this._prepareStepOne();
        $removeHandler(window, 'beforeunload', this._windowCloseHandler);
        if (this._isDialogDirty == true) {
            dialogBase.closeAndRebind();
        } else {
            dialogBase.close();
        }
    },

    _windowCloseHandler: function (sender) {
        var currentDialog = this.dialogBase;
        if (currentDialog._isUploading == true) {
            var msg = currentDialog.get_clientLabelManager().getLabel("LibrariesResources", "YouHaveUnuploadedItemsWantToInterruptUploading")
            sender.rawEvent.returnValue = msg;
            sender.rawEvent.defaultPrevented = true;
            return msg;
        }
    },


    _deserializeBlankDataItem: function (blankDataItem) {
        blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(blankDataItem);
        if (blankDataItem.hasOwnProperty('Id')) {
            delete blankDataItem.Id;
        }
        return blankDataItem;
    },

    /* -------------------- properties ---------------- */
    get_fileUpload: function () {
        return this._fileUpload;
    },
    set_fileUpload: function (value) {
        this._fileUpload = value;
    },
    get_librarySelector: function () {
        return this._librarySelector;
    },
    set_librarySelector: function (value) {
        this._librarySelector = value;
    },
    get_parentLibrarySelector: function () {
        return this._parentLibrarySelector;
    },
    set_parentLibrarySelector: function (value) {
        this._parentLibrarySelector = value;
    },
    get_taxonomySelectorId: function () {
        return this._taxonomySelectorId;
    },
    set_taxonomySelectorId: function (value) {
        this._taxonomySelectorId = value;
    },
    get_dialogTitle: function () {
        return this._dialogTitle;
    },
    set_dialogTitle: function (value) {
        this._dialogTitle = value;
    },
    get_successCommandBar: function () {
        return this._successCommandBar;
    },
    set_successCommandBar: function (value) {
        this._successCommandBar = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_backButton: function () {
        return this._backButton;
    },
    set_backButton: function (value) {
        this._backButton = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_isMultilingual: function () {
        return this._isMultilingual;
    },
    set_isMultilingual: function (value) {
        this._isMultilingual = value;
    },
    get_workflowMenu: function () {
        return this._workflowMenu;
    },
    set_workflowMenu: function (value) {
        this._workflowMenu = value;
    },
    get_itemsCount: function () {
        return this._itemsCount;
    },
    set_itemsCount: function (value) {
        this._itemsCount = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.UploadDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);
