﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.FileField = function (element) {
    this._element = element;
	this._uploadServiceUrl = null;
    this._uploaderContainer = null;
    this._commandsContainer = null;    
    this._clientLabelManager = null;
    this._uploadInput = null;
    this._mediaContentItemsList = null;
    this._mediaContentBinder = null;
    this._originalSizeUrl = null;
    this._size = null;
    this._dataItem = null;
    this._windowManager = null;
    this._menuList = null;
    this._clientLabelManager = null;
    this._uploadItemMethodChangedDelegate = null;
    this._uploadNewItemButton = null;
    this._mediaContainer = null;
    this._radioButtonList = null;
    this._moreTranslationsLbl = null;
    this._commandBar = null;
    this._mediaContentBinderServiceUrl = null;
    this._copyMediaFileLinkServiceUrl = null;

    this._libraryContentType = null;
    this._libraryProviderName = null;
    this._filter = null;
    this._isMultiselect = null;
    this._maxFileCount = null;
    this._itemNamePlural = null;
    this._itemName = null;
    this._usedByMediaContentUploader = null;
    this._isMultilingual = null;
    this._commandBarCommandDelegate = null;

    this._callbacks = null;
    this._isChanged = null;
    this._providerChanged = false;
    this._showCancelBtn = false;
    this._kendoUploadWidget = null;
    this._selectedFiles = [];
    this._successfullyUploadedFiles = [];
    this._currentlyUploadingIndex = null;
    this._workflowOperation = null;
    this._uploadedItemsLanguage = null;
    this._libraryId = null;
    this._skipWorkflow = false;
    this._uploadAndReplace = false;

    this._urlVersionQueryParam = "sfvrsn";
    this._updateList = true;
    this._fileDescriptionId = null;

    this._disableFileModifications = false;
    this._cancelUploadContainer = null;

    Telerik.Sitefinity.Web.UI.Fields.FileField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.FileField.prototype = {
    /* --------------------  set up and tear down ----------- */

    initialize: function () {

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {           

            var that = this; // keep a reference to the instance of the control to access from callbacks in Kendo

            $(this._uploaderContainer).hide();

            this._cancelUploadContainer = $(this._element).find('#cancelUploadContainer_write');

            function _kendoUploadHandler(e) {
                if (that._currentlyUploadingIndex === null)
                    that._currentlyUploadingIndex = 0;
                e.data = {
                    ContentType: that._libraryContentType,
                    LibraryId: that._libraryId,
                    ContentId: that._contentIds[that._currentlyUploadingIndex],
                    UploadAndReplace: that._uploadAndReplace,
                    Workflow: that._workflowOperation,
                    Culture: that._uploadedItemsLanguage,
                    ProviderName: that._libraryProviderName,
                    SkipWorkflow: that._skipWorkflow
                };
                that._currentlyUploadingIndex++;
            };

            function _kendoUploadError(e) {
                var files = e.files;
                if (e.operation == "upload") {
                    for (var i = 0; i < files.length; i++) {
                        alert(String.format("'{0}' could not be uploaded. {1}", files[i].name, files[i].name));
                    }
                }

                $("#" + that.get_mediaContainer().id).show();
                that._cancelUploadContainer.show();
            };

            function _kendoUploadSuccess(e) {
                if (!e.response[0].UploadResult) {
                    alert(e.response[0].ErrorMessage);

                    // Hack when used from the media content uploader - invoke remove handler when the upload is unsuccessful
                    if (that._usedByMediaContentUploader)
                        that._fileRemovedHandler();
                }
                else {
                    that._successfullyUploadedFiles.push(e.files[0].name);
                    that.fileUploaded(e.response[0].ContentItem);
                }
            };

            function _kendoUploadFileSelected(e) {
                if (e.files.length + that._selectedFiles.length > that._maxFileCount) {
                    if (that._maxFileCount > 1) {
                        alert(String.format(that.get_clientLabelManager().getLabel("LibrariesResources", "UploadFileLimitReachedPlural"), that._maxFileCount));
                    }
                    else {
                        alert(that.get_clientLabelManager().getLabel("LibrariesResources", "UploadFileLimitReachedSingular"));
                    }
                    e.preventDefault();

                    // Hack when used from the media content uploader - the view should not be reset
                    if (!that._usedByMediaContentUploader)
                        that.reset();

                    return;
                }
                for (var fileCount = 0; fileCount < e.files.length; fileCount++) {
                    if (that._allowedExtensions && that._allowedExtensions.toLowerCase().indexOf(e.files[fileCount].extension.toLowerCase()) === -1) {
                        that._selectedFiles = [];
                        alert(String.format(that.get_clientLabelManager().getLabel("LibrariesResources", "AllowedExtensionsErrorMessage"), that._allowedExtensions));
                        e.preventDefault();
                        return;
                    }

                    /**
                     * Instead of decoding the file name that Kendo encodes internally, 
                     * we should get it straight from the "input" element. The database should not contain encoded values
                     * and we want to be consistent with values saved after renaming an item through the UI
                    **/
                    // that.get_selectedFiles().push(that.get_uploadInput.files[fileCount].name);


                    var $div = $('<div/>');
                    var decodedValue = $div.html(e.files[fileCount].name).text();
                    $div = null;
                    that._selectedFiles.push(decodedValue);
                }

                $("#" + that.get_mediaContainer().id).hide();
                that._cancelUploadContainer.hide();

                if (that._selectedFiles.length > 0)
                    that._kendoUploadWidget.wrapper.removeClass("sfNoFilesSelected").addClass("sfFilesSelected");
                that.switchToFilesSelectedMode(this, e);
            };

            function _kendoUploadComplete(e) {
                that._currentlyUploadingIndex = null;
                that.uploadFinished();
                that._kendoUploadWidget.wrapper.find(".k-upload-files .k-file .k-button").remove();
                that._kendoUploadWidget.wrapper.find("span.sfFilesSelectedLbl").hide();
            };

            function _kendoUploadRemove(e) {
                if (that._selectedFiles.length == 0)
                    that._kendoUploadWidget.wrapper.removeClass("sfFilesSelected").addClass("sfNoFilesSelected");
                var removedFileIndex = that._selectedFiles.indexOf(e.files[0].name);
                that._selectedFiles.splice(removedFileIndex, 1);
                $("#" + that.get_mediaContainer().id).show();
                if (that._showCancelBtn)
                    that._cancelUploadContainer.show();

                if (that._selectedFiles.length === 0 && that.get_selectedFilesCount() === 0) {
                    that.resetUploader();
                }

                that._fileRemovedHandler();
            };

            function _kendoUploadCancel(e) {
                $("#" + that.get_mediaContainer().id).show();
                that._cancelUploadContainer.show();
                that._fileRemovedHandler();
            };

            if (this.get_uploadInput()) {
                //$(this.get_uploadInput()).attr("accept", this._filter);
                jQuery(this.get_uploadInput()).kendoUpload({
                    async: {
                        autoUpload: false,
                        saveUrl: this._uploadServiceUrl,
                        removeUrl: "#",
                        removeField: ""
                    },
                    localization: {
                        remove: "Remove"
                    },
                    multiple: this._isMultiselect,
                    upload: _kendoUploadHandler,
                    success: _kendoUploadSuccess,
                    error: _kendoUploadError,
                    select: _kendoUploadFileSelected,
                    complete: _kendoUploadComplete,
                    remove: _kendoUploadRemove,
                    cancel: _kendoUploadCancel,
                    localization: {
                        select: String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "DragAndDropBeforeDragging"), this._itemName),
                        dropFilesHere: String.format(this.get_clientLabelManager().getLabel("LibrariesResources", "DragAndDropWhileDragging"), this._itemNamePlural)
                    }
                });
                this._kendoUploadWidget = jQuery(this.get_uploadInput()).data("kendoUpload");
                this._kendoUploadWidget.wrapper.addClass("sfLibUploadWrp sfNoFilesSelected sfClearfix");
                // TODO: Increase z-index from CSS.
                $(this.get_uploadInput()).css("z-index", 20000);
            }

            if (this.get_disableFileModifications()) {
                $(this._element).find('#' + this.get_radioButtonList().id).hide();
                this._cancelUploadContainer.hide();
            }
            

            if (this.get_dataFieldName() != null) {
                $(this._uploaderContainer).hide();
                $(this._kendoUploadWidget.wrapper).hide();
			}
            else {
                $(this._commandsContainer).hide();
            }

            if (window.hasOwnProperty("detailFormView") && detailFormView) {
                this._detailFormViewDataBindDelegate = Function.createDelegate(this, this._detailFormViewDataBind);
                detailFormView.add_onDataBind(this._detailFormViewDataBindDelegate);
            }
        }

        if (this._commandBar) {
            if (this._commandBarCommandDelegate == null) {
                this._commandBarCommandDelegate = Function.createDelegate(this, this._handleCommandBarCommand);
            }
            this._commandBar.add_command(this._commandBarCommandDelegate);
        }

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._attachHandlers(true);
            this._cancelUploadContainer.hide();

            if (!this._isMultiselect) {
                // Remove the Add more items link if it is not in multiple select mode
                $(this._element).find('span.sfFilesSelectedLbl').remove();
            }
        }        

        Telerik.Sitefinity.Web.UI.Fields.FileField.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._commandBarCommandDelegate != null) {
            this._commandBar.remove_command(this._commandBarCommandDelegate);
            delete this._commandBarCommandDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.FileField.callBaseMethod(this, "dispose");
    },

    _onLoad: function () {
        $(this._radioButtonList).hide();
        $(this._mediaContentItemsList).hide();
    },

    _detailFormViewDataBind: function (sender, args) {
        if (this._isMultilingual) {
            var replaceItem = this._menuList.findItemByAttribute("command", "replace");

            this._menuList.trackChanges();
            replaceItem.set_visible(true);
            this._menuList.commitChanges();
        }
    },

    _bindList: function(binder, serviceUrl) {
        var urlParams = [];
        if (binder && this._updateList) {
            binder.set_serviceBaseUrl(serviceUrl);
            binder.set_serviceChildItemsBaseUrl("");
            urlParams["contentId"] = detailFormView.get_dataItem().Item.Id;
            urlParams["provider"] = detailFormView._providerName;
            urlParams["culture"] = detailFormView._binder._uiCulture;
            binder.set_urlParams(urlParams);
            binder.DataBind();
        }
    },

    _uploadItemMethodChanged: function (sender, args) {
        this.chooseSelectedOrDefault(undefined, false, undefined, true);
        $("#" + this._uploadNewItemButton.id).attr('checked', true);
        $(this.get_moreTranslationsLbl()).hide();
        this._configureUploadDialog(true);
        this._cancelUploadContainer.show();
    },

    _initializeLists: function () {        

        this._bindList(this.get_mediaContentBinder(), this.get_mediaContentBinderServiceUrl())

        $("#" + this.get_mediaContainer().id).show();
        this._cancelUploadContainer.show();
    },

    _mediaContentBinderItemSelect: function (sender, args) {
        this._uploadNewItemButton.checked = false;
        this.chooseSelectedOrDefault(sender, true, args);

        //Check whether id could be serialized from server
        //$("#" + args.get_itemElement().lastChild.id).attr('checked', 'checked');
        $(this._uploaderContainer).hide();
        $(this._commandsContainer).hide();
        $(this._kendoUploadWidget.wrapper).hide();
		args.set_cancel(true);
    },

    _mediaContentBinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();
        if (dataItem.IsDefault) {
            this._defaultFileId = dataItem.FileId;
            for (var elementCount = 0; elementCount < sender.get_currentItems().length; elementCount++) {
                var currentItem = sender.get_currentItems()[elementCount];
                if (currentItem.FileId == dataItem.FileId) {
                    var currentElement = sender.get_currentItemElements()[elementCount];
                    var selectInput = $(currentElement).find('input.selectCommand')[0];
                    if (selectInput) {
                        selectInput.checked = true;
                    }
                    break;
                }                
            }
        }
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this.resetUploader();
            if (this.get_dataFieldName() != null) {
                $(this._uploaderContainer).hide();
                $(this._commandsContainer).show();
                $(this._kendoUploadWidget.wrapper).hide();
            }

            $(this._radioButtonList).hide();
            $(this._mediaContentItemsList).hide();
            this._cancelUploadContainer.hide();
        }
        this._isChanged = null;
        Telerik.Sitefinity.Web.UI.Fields.FileField.callBaseMethod(this, "reset");
    },

    _attachHandlers: function (toAttach) {
        var menuList = this.get_menuList();
        var cancelBtn = $(this._element).find("#cancelUploadBtn");
        if (toAttach) {
            this._menuListClickedDelegate = Function.createDelegate(this, this._menuList_clickedHandler)
            menuList.add_itemClicked(this._menuListClickedDelegate);

            this._cancelBtnClickedDelegate = Function.createDelegate(this, this._cancelBtnClickedHandler);
            cancelBtn.click(this._cancelBtnClickedDelegate);

            this._mediaContentBinderItemSelectDelegate = Function.createDelegate(this, this._mediaContentBinderItemSelect);
            this._mediaContentBinderItemDataBoundDelegate = Function.createDelegate(this, this._mediaContentBinderItemDataBound);

            this._uploadItemMethodChangedDelegate = Function.createDelegate(this, this._uploadItemMethodChanged);

            jQuery('#' + this._uploadNewItemButton.id).click(this._uploadItemMethodChangedDelegate);

            this.get_mediaContentBinder().add_onItemSelectCommand(this._mediaContentBinderItemSelectDelegate);
            this.get_mediaContentBinder().add_onItemDataBound(this._mediaContentBinderItemDataBoundDelegate);

            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
            Sys.Application.add_load(this._onLoadDelegate);

            if (window.hasOwnProperty("detailFormView") && detailFormView) {
                this._formClosingDelagate = Function.createDelegate(this, this._backToAllItems);
                detailFormView.add_formClosing(this._formClosingDelagate);

                this._backToAllItemsDelegate = Function.createDelegate(this, this._backToAllItems);
                $addHandler(detailFormView._backToAllItemsButton, "click", this._backToAllItemsDelegate);
            }
        }
        else {
            menuList.remove_itemClicked(this._menuListClickedDelegate);
            delete this._menuListClickedDelegate;

            this.get_mediaContentBinder().remove_onItemSelectCommand(this._mediaContentBinderItemSelectDelegate);
            delete this._mediaContentBinderItemSelectDelegate;

            this.get_mediaContentBinder().remove_onItemDataBound(this._mediaContentBinderItemDataBoundDelegate);
            delete this._mediaContentBinderItemDataBoundDelegate;

            jQuery('#' + this._uploadNewItemButton.id).unbind("click");
        }
    },

    _clearMenuSelection: function () {
        var selectedItem = this.get_menuList().get_selectedItem();
        if (selectedItem) {
            this.get_menuList().close();
            selectedItem.set_selected(false);
            selectedItem.blur();
        }
    },

    _cancelBtnClickedHandler: function (sender, args) {
        this._resetUploadView(true);
    },

    _resetUploadView: function (configureUploadDialog) {
        this._cancelUploadContainer.hide();
        $(this._element).find("#" + this._uploadNewItemButton.id).attr('checked', false);
        this._mediaContentItemsList.getBinder()._selectedItems = [];
        var selectedItem = this.get_menuList().get_selectedItem();
        if (selectedItem) {
            selectedItem.set_selected(false);
        }
        $(this._radioButtonList).hide();
        $(this._mediaContentItemsList).hide();
        if (configureUploadDialog) {
            this._configureUploadDialog();
        }
    },

    _configureUploadDialog: function (show) {
        if (show) {
            $(this._uploaderContainer).show();
            $(this._commandsContainer).hide();
            $(this._kendoUploadWidget.wrapper).show();
        }
        else {
            $(this._uploaderContainer).hide();
            $(this._commandsContainer).show();
            $(this._kendoUploadWidget.wrapper).hide();
		}
    },

    _menuList_clickedHandler: function (sender, args) {
        var menuItem = args.get_item();
        var command = menuItem.get_attributes().getAttribute("command");
        switch (command) {
            default:
                break;
            case "viewOriginalFile":
                this.viewOriginalFile();
                this._clearMenuSelection();
                break;
            case "openFile":
                window.open(this.get_value(), "_blank");
                this._clearMenuSelection();
                break;
            case "replace":
                var replaceItemlbl = this.get_clientLabelManager().getLabel('LibrariesResources', 'ReplaceFile');
                $(this._element).find('#replaceFileLbl').text(replaceItemlbl);
                this._prepareUploadLbl();
                this._configureUploadDialog(true);
                this._cancelUploadContainer.show();
                this._uploadAndReplace = true;
                this._showCancelBtn = true;
                break;
            case "useAnother":
                var chooseAnotherlbl = this.get_clientLabelManager().getLabel('LibrariesResources', 'ChooseFromAlreadyUploaded');
                $(this._uploaderContainer).hide();
                $(this._commandsContainer).hide();
                $(this._radioButtonList).show();
                $("#" + this.get_mediaContainer().id).show();
                $(this._element).find('#replaceFileLbl').text(chooseAnotherlbl);
                this._uploadNewItemButton.checked = false;
                this._initializeLists();
                this._showCancelBtn = true;

                // Sometimes the commandContainer contains a video and when it is hidden some drawing issues of the page can be observed.
                // Centering the dialog forces a redraw which solves the issues.
                setTimeout(function () { dialogBase.get_radWindow().Center() }, 0);

                this._uploadAndReplace = false;
                break;
        }        
    },

    _prepareUploadLbl: function () {
        var label = $(this.get_moreTranslationsLbl());
        label.show();
        var item = detailFormView.get_dataItem();
        var additionalInfo = item.SfAdditionalInfo;
        var translations = this._findValueByKey(additionalInfo, 'NumberOfTranslations');
        if (translations != 0) {
            label.text(translations + ' ' + this.get_clientLabelManager().getLabel("LibrariesResources", "MoreTranslations"));
        }
        else {
            label.hide();
        }
    },

    chooseSelectedOrDefault: function (binder, checked, args, wipe) {
        var index = 0;
        var sender;
        var currentItem;
        if (binder) {
            sender = binder;
        }
        else {
            sender = this._mediaContentBinder;
        }

        if (args) {
            index = args._itemIndex;
        }
        else if (sender.get_selectedItems().length > 0) {
            var id = sender.get_selectedItems()[0].FileId;
            for (var elementCount = 0; elementCount < sender.get_currentItems().length; elementCount++) {
                currentItem = sender.get_currentItems()[elementCount];
                if (currentItem.FileId == id) {
                    index = elementCount;
                }
            }
        }

        this._clearSelectedItems();

        if (wipe) {
            for (elementCount = 0; elementCount < sender.get_currentItems().length; elementCount++) {
                currentItem = sender.get_currentItems()[elementCount];
                var currentElement = sender.get_currentItemElements()[elementCount];
                var selectInput = $(currentElement).find('input.selectCommand')[0];
                if (selectInput) {
                    selectInput.checked = false;
                }
            }
        }

        if (sender.get_currentItemElements().length > 0) {
            var selectedElement = sender.get_currentItemElements()[index];
            if (selectedElement) {
                var selectInputElement = $(sender.get_currentItemElements()[index]).find('input.selectCommand')[0];
                if (selectInputElement) {
                    selectInputElement.checked = checked;
                }
            }
        }

        currentItem = sender.get_currentItems()[index];
        if (currentItem) {
            if (checked) {
                sender._addRemoveSelectedItem(currentItem.FileId, currentItem, "select");
            }
            else {
                sender._addRemoveSelectedItem(currentItem.FileId, currentItem, "deselect");
            }
        }
    },

    _clearSelectedItems: function () {
        this._mediaContentBinder._selectedItems = [];
    },

    _findValueByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }

        return null;
    },

    _setValueByKey: function (dictionary, key, value) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key) {
                item.Value = value;
                return;
            }
        }

        dictionary.push({ Key: key, Value: value });
    },

    // switches the upload container to the files selected mode
    switchToFilesSelectedMode: function (sender, files) {
        this._isChanged = true;
        this._filesSelectedHandler(files);
    },

    uploadFinished: function () {
        this._uploadCompletedHandler();
        if (this._callbacks) {
            this.reset();
            this._callbacks.Success({ uploadAndReplace: this._uploadAndReplace });
            this._callbacks = null;
        }
    },

    fileUploaded: function (file) {
        this._fileUploadedHandler(file);
    },

    fileUploadFailed: function (exception) {
        this._fileUploadFailedHandler(exception);
    },

    uploadFiles: function (contentIds, workflowOperation, language, skipWorkflow) {
        this._contentIds = contentIds;
        this._workflowOperation = workflowOperation;
        this._uploadedItemsLanguage = language;
        this._skipWorkflow = typeof skipWorkflow !== "undefined" ? skipWorkflow : false;
        this._kendoUploadWidget._module.onSaveSelected();
    },

    resetUploader: function () {
        this.clearSelectedFiles();
        this._successfullyUploadedFiles = [];
        $(this._element).find(".k-widget.k-upload").find("ul").remove();
        this._kendoUploadWidget.wrapper.removeClass("sfFilesSelected").addClass("sfNoFilesSelected");
        this._kendoUploadWidget.wrapper.find("span.sfFilesSelectedLbl").show();
    },

    clearSelectedFiles: function () {
        this._selectedFiles = [];
    },

    saveChanges: function (dataItem, successCallback, failureCallback, caller) {

        var shouldCopyLink = this._mediaContentItemsList.get_selectedItems().length > 0;
        
        if(shouldCopyLink) {
            var selectedFileItem = this._mediaContentItemsList.get_selectedItems()[0];
            var contentId = detailFormView.get_dataItem().Item.Id;
            var providerName = detailFormView._providerName;
            var fileId = selectedFileItem.FileId;

            var urlParams = [];
            urlParams["contentId"] = contentId;
            urlParams["provider"] = providerName;
            urlParams["fileId"] = fileId;
            urlParams["uploadAndReplace"] = true;
        
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            clientManager._uiCulture = detailFormView.get_binder()._uiCulture;

            clientManager.InvokeGet(this._copyMediaFileLinkServiceUrl, urlParams, [],
                successCallback, failureCallback, this);
        }
        else {
            if (jQuery(this._uploaderContainer).is(":visible")) {
                var contentIds = [];
                contentIds.push(dataItem.Item.Id);
                this._callbacks = { Caller: caller, Success: successCallback };
                this.uploadFiles(contentIds, "", caller._uiCulture);
            }
            else {
                successCallback();
            }
        }
    },

    isChanged: function () {
        var isAnotherItemSelected = false;

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            if (this._mediaContentItemsList.get_selectedItems().length > 0) {
                var fileId = this._mediaContentItemsList.get_selectedItems()[0].FileId;                
                isAnotherItemSelected = fileId != this._defaultFileId;
            }
        }

        var isItemUploaded = (this.get_selectedFilesCount() > 0);

        var isChanged = false;
        if (this._isChanged == true) {
            isChanged = true;
        }

        return isChanged || isAnotherItemSelected || isItemUploaded;
    },

    //function is intended to be used at subclasses.
    viewOriginalFile: function () {

    },

    _backToAllItems: function (sender) {
        this._resetUploadView();
    },


    /* -------------------- events -------------------- */

    add_filesSelected: function (delegate) {
        this.get_events().addHandler('filesSelected', delegate);
    },

    remove_filesSelected: function (delegate) {
        this.get_events().removeHandler('filesSelected', delegate);
    },

    add_fileRemoved: function (delegate) {
        this.get_events().addHandler('fileRemoved', delegate);
    },

    remove_fileRemoved: function (delegate) {
        this.get_events().removeHandler('fileRemoved', delegate);
    },

    add_uploadCompleted: function (delegate) {
        this.get_events().addHandler('uploadCompleted', delegate);
    },

    remove_uploadCompleted: function (delegate) {
        this.get_events().removeHandler('uploadCompleted', delegate);
    },

    add_fileUploaded: function (delegate) {
        this.get_events().addHandler('fileUploaded', delegate);
    },

    remove_fileUploaded: function (delegate) {
        this.get_events().removeHandler('fileUploaded', delegate);
    },

    add_fileUploadFailed: function (delegate) {
        this.get_events().addHandler('fileUploadFailed', delegate);
    },

    remove_fileUploadFailed: function (delegate) {
        this.get_events().removeHandler('fileUploadFailed', delegate);
    },

    /* -------------------- event handlers ------------ */

    _handleCommandBarCommand: function (sender, args) {
        switch (args.get_commandName()) {
            case 'viewOriginalFile':
                this.viewOriginalFile();
                break;
            case 'openFile':
                window.open(this.get_value(), "_blank");
                break;
            case 'replaceFile':
                $(this._uploaderContainer).show();
                $(this._commandsContainer).hide();
                $(this._kendoUploadWidget.wrapper).show();
				break;
        }
    },

    /* -------------------- private methods ----------- */
	
    _filesSelectedHandler: function (files) {
        var args = files || Sys.EventArgs.Empty;
        var h = this.get_events().getHandler('filesSelected');
        if (h)
            h(this, args);

        return args;
    },

    _fileRemovedHandler: function () {
        var h = this.get_events().getHandler('fileRemoved');
        if (h)
            h(this, Sys.EventArgs.Empty);
        return Sys.EventArgs.Empty;
    },

    _uploadCompletedHandler: function () {
        var h = this.get_events().getHandler('uploadCompleted');
        if (h)
            h(this, Sys.EventArgs.Empty);
        return Sys.EventArgs.Empty;
    },

    _fileUploadedHandler: function (file) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs(file);
        var h = this.get_events().getHandler('fileUploaded');
        if (h)
            h(this, eventArgs);
        return eventArgs;
    },

    _fileUploadFailedHandler: function (exception) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.Fields.FileUploadFailedEventArgs(exception);
        var h = this.get_events().getHandler('fileUploadFailed');
        if (h)
            h(this, eventArgs);
        return eventArgs;
    },

    // Chrome does not support 2 <video> tags with the same URL even if in different tabs.
    _appendRnd: function (value) {
        var qIndex = value.lastIndexOf("?");
        if (qIndex > 0) {
            var query = value.substring(qIndex + 1);
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(query);
            if (queryString.contains(this._urlVersionQueryParam)) {
                queryString.set(this._urlVersionQueryParam, Math.random());
                return (value.substring(0, qIndex)) + queryString.toString(true);
            }
        }
        value = qIndex > 0 ? value + "&" : value + "?";
        return value + this._urlVersionQueryParam + "=" + Math.random();
    },

    //NOTE: This method is copied from FileField.js - it can be extracted in a common utilities file.
    readablizeBytes: function (bytes) {
        var result = "";
        if (bytes == "0")
            result = "0 KB";

        else {
            var s = ['bytes', 'KB', 'MB', 'GB', 'TB', 'PB'];
            var range = Math.floor(Math.log(bytes) / Math.log(1024));

            //when range is in bytes present in KB and round to nearest integer.
            if (range == 0) {
                range++;
                result = Math.ceil((bytes / Math.pow(1024, Math.floor(range)))) + " " + s[range];
            }
            else {
                result = (bytes / Math.pow(1024, Math.floor(range))).toFixed(2) + " " + s[range];
            }
        }
        return result;
    },

    _findValueByKey: function (dictionary, key) {
        for (var i = 0; i < dictionary.length; i++) {
            var item = dictionary[i];
            if (item.Key == key)
                return item.Value;
        }

        return null;
    },

    /* -------------------- properties ---------------- */

    // Sets the value of the file field control.
    set_value: function (value) {
        var extension = null;
        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write && value != null) {
            var idx = value.lastIndexOf(".");
            if (idx > -1) {
                extension = value.substring(idx + 1);
                var idy = extension.indexOf("?");
                if (idy > -1) {
                    extension = extension.substring(0, idy);
                }
            }
        }

        if (this._fileDescriptionId != null && typeof detailFormView != 'undefined' && detailFormView != null) {
            var dataItem = detailFormView.get_dataItem().Item;
            var additionalInfo = detailFormView.get_dataItem().SfAdditionalInfo;
            if (typeof additionalInfo != 'undefined' && additionalInfo != null) {
                var descriptionPanel = $("#" + this._fileDescriptionId);
                if (dataItem && dataItem.Extension) {
                    var dataItemExtension = dataItem.Extension.substring(1);
                    extension = extension == null || (extension !== dataItemExtension) ? dataItemExtension : extension;
                }
    
                var descClass = extension != null ? 'sf' + extension.toLowerCase() + ' sfext' : ' sfext';
                descriptionPanel.attr("class", descClass);
                descriptionPanel.find("a").attr("href", value);
                descriptionPanel.find(".sfItemTitle").html(this._findValueByKey(additionalInfo, "DefaultFileName"));
                descriptionPanel.find(".sfLine").html(this.readablizeBytes(dataItem.TotalSize));
            }

            if (this._commandBar) {
                this._commandBar.set_visible(false);
            }
        }

        Telerik.Sitefinity.Web.UI.Fields.FileField.callBaseMethod(this, "set_value", [value]);
    },

    // Gets the reference to the control which serves as a container for the uploader control.
    get_uploaderContainer: function () {
        return this._uploaderContainer;
    },

    // Sets the reference to the control which serves as a container for the uploader control.
    set_uploaderContainer: function (value) {
        this._uploaderContainer = value;
    },

    // Gets the number of files selected by the upload control to be uploaded.
    get_selectedFilesCount: function () {
         return this._successfullyUploadedFiles.length;
    },

    // Gets the files that have been selected to be uploaded.
    get_selectedFiles: function () {
		 return this._selectedFiles;
    },

    // Gets the reference to the control which serves as a container for the command bar and the file icon.
    get_commandsContainer: function () {
        return this._commandsContainer;
    },

    // Sets the reference to the control which serves as a container for the command bar and the file icon.
    set_commandsContainer: function (value) {
        this._commandsContainer = value;
    },    

    // Gets the reference to the command bar.
    get_commandBar: function () {
        return this._commandBar;
    },

    // Sets the reference to the command bar.
    set_commandBar: function (value) {
        this._commandBar = value;
    },

    // Gets the reference to the HTML5 upload input element.
    get_uploadInput: function () {
        return this._uploadInput;
    },

    // Sets the reference to the HTML5 upload input element.
    set_uploadInput: function (value) {
        this._uploadInput = value;
    },

    get_libraryProviderName: function () {
        return this._libraryProviderName;
    },

    set_libraryProviderName: function (val) {
        this._libraryProviderName = val;
		this._providerChanged = true;
        this.raisePropertyChanged("libraryProviderName");
    },

    set_provider: function (val) {
        this.set_libraryProviderName(val);
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },

    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_libraryId: function () {
        return this._libraryId;
    },

    set_libraryId: function (value) {
        this._libraryId = value;
    },

    get_mediaContentItemsList: function () {
        return this._mediaContentItemsList;
    },
    set_mediaContentItemsList: function (value) {
        this._mediaContentItemsList = value;
    },

    get_mediaContentBinder: function () {
        return this._mediaContentBinder;
    },
    set_mediaContentBinder: function (value) {
        this._mediaContentBinder = value;
    },

    get_mediaContentBinderServiceUrl: function () {
        return this._mediaContentBinderServiceUrl;
    },
    set_mediaContentBinderServiceUrl: function (value) {
        this._mediaContentBinderServiceUrl = value;
    },

    get_copyMediaFileLinkServiceUrl: function () {
        return this._copyMediaFileLinkServiceUrl;
    },
    set_copyMediaFileLinkServiceUrl: function (value) {
        this._copyMediaFileLinkServiceUrl = value;
    },

    get_menuList: function () {
        return this._menuList;
    },

    set_menuList: function (value) {
        this._menuList = value;
    },

    get_uploadNewItemButton: function () {
        return this._uploadNewItemButton;
    },

    set_uploadNewItemButton: function (value) {
        this._uploadNewItemButton = value;
    },

    get_mediaContainer: function () {
        return this._mediaContainer;
    },

    set_mediaContainer: function (value) {
        this._mediaContainer = value;
    },

    get_radioButtonList: function () {
        return this._radioButtonList;
    },

    set_radioButtonList: function (value) {
        this._radioButtonList = value;
    },

    get_moreTranslationsLbl: function () {
        return this._moreTranslationsLbl;
    },

    set_moreTranslationsLbl: function (value) {
        this._moreTranslationsLbl = value;
    },

    get_disableFileModifications: function () {
        return this._disableFileModifications;
    },

    set_disableFileModifications: function (value) {
        this._disableFileModifications = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.FileField.registerClass("Telerik.Sitefinity.Web.UI.Fields.FileField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.ISelfExecutableField);

// Telerik.Sitefinity.Web.UI.Fields.FileField.getLabel is defined in the ascx

Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs = function (file) {
    this._file = file;
    Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Fields.FileUploadFailedEventArgs = function (exception) {
    this._exception = exception;
    Telerik.Sitefinity.Web.UI.Fields.FileUploadFailedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.callBaseMethod(this, 'dispose');
    },
    get_file: function () {
        return this._file;
    }
};

Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.registerClass('Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs', Sys.EventArgs);

Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs = function (file) {
    this._file = file;
    Telerik.Sitefinity.Web.UI.Fields.FileUploadedEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.Fields.FileUploadFailedEventArgs = function (exception) {
    this._exception = exception;
    Telerik.Sitefinity.Web.UI.Fields.FileUploadFailedEventArgs.initializeBase(this);
}