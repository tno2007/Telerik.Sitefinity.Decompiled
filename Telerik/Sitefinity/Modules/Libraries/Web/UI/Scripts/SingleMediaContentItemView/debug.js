
Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers");

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView.initializeBase(this, [element]);

    this._titleLabel = null;
    this._altTextLabel = null;
    this._extensionLabel = null;
    this._sizeLabel = null;
    this._libraryLabel = null;
    this._imageElement = null;
    this._providerName = null;
    this._useSmallItemPreview = null;
    this._viewMode = null;
    this._hiddenElements = null;
    this._skipBlankItemView = null;
    this._isMediaItemPublished = null;

    this._selectedDataItem = null;
    this._dataItemId = null;
    this._mediaUrl = null;
    this._selectImageButton = null;
    this._uploadImageButton = null;
    this._imageSelector = null;
    this._imageSelectorDialog = null;
    this._imageSelectorSaveDelegate = null;
    this._imageSelectorCloseDelegate = null;
    this._windowManager = null;
    this._clientManager = null;
    this._serviceUrl = "";
    this._imageEditorDialogUrl = null;
    this._changeImageButton = null;
    this._cropResizeRotateButton = null;
    this._editImageButton = null;
    this.mediaDialogOpenMode = null;
    this._cropResizeRotateDialog = null;
    this._cropResizeRotateDialogCloseDelegate = null;

    this._imageEditorDialog = null;
    this._imageEditorName = null;

    this._src = null;
    this._urlVersionQueryParam = "sfvrsn";
    this._editorBackPhrase = null;
    this._dialogManager = null;
    this._uiCulture = null;
    this._mediaField = null;
    this._documentLink = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView.callBaseMethod(this, 'initialize');

        this.mediaDialogOpenMode = {
            UPLOAD: 1,
            SELECT: 2
        };

        jQuery(this.get_imageElement()).on("load", function () {
            if (dialogBase._implementsDesigner || dialogBase._hostedInRadWindow) {
                dialogBase.resizeToContent();
            }
        });

        this._imageSelectorSaveDelegate = Function.createDelegate(this, this._imageSelectorSaveHandler);
        this._imageSelector.set_customInsertDelegate(this._imageSelectorSaveDelegate);
        $addHandler(this._imageSelector._cancelLink, "click", this._imageSelectorCloseHandler);
        this._imageSelectorCloseDelegate = Function.createDelegate(this, this._imageSelectorCloseHandler);

        if (this._imageSelector) {
            this._imageSelectorDialog = jQuery(this.get_imageSelector().get_element()).dialog({
                autoOpen: false,
                modal: false,
                width: 930,
                height: "auto",
                closeOnEscape: true,
                resizable: false,
                draggable: false,
                close: this._imageSelectorCloseDelegate,
                classes: {
                    "ui-dialog": "sfWindowInWindow sfZIndexL"
                }
            });
        }

        this.attachEventHandlers(true);
        this._loadDelegate = Function.createDelegate(this, this._load);

        Sys.Application.add_load(this._loadDelegate);
    },

    dispose: function () {
        this.attachEventHandlers(false);

        if (this._imageSelectorSaveDelegate)
            delete this._imageSelectorSaveDelegate;

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView.callBaseMethod(this, 'dispose');
    },

    /* ---------------------------------- event handlers --------------------------------- */
    _load: function () {
        this._arrangeMediaItemDetailsView();
        this._dialogManager = GetDialogManager();
        this._initImageBackendEditDialog();

        var dataItemId = this.get_dataItemId();

        if (this.get_skipBlankItemView() && (!dataItemId || dataItemId === Telerik.Sitefinity.getEmptyGuid())) {
            this._openMediaSelectorDialog();
        }
        if (this.get_useSmallItemPreview()) {
            jQuery("#sf_imgIsSelected").addClass("sfSmallPreviewWrp");
        }
        switch (this.get_viewMode()) {
            case "Image":
                jQuery("#sf_imgIsEmpty .sfMediaItemViewIcn").addClass("sfImageViewIcn").removeClass("sfVideoIcn sfDownloadLinkIcn");
                break;
            case "Media":
                jQuery("#sf_imgIsEmpty .sfMediaItemViewIcn").addClass("sfVideoIcn").removeClass("sfImageViewIcn sfDownloadLinkIcn");
                break;
            case "Document":
                jQuery("#sf_imgIsEmpty .sfMediaItemViewIcn").addClass("sfDownloadLinkIcn").removeClass("sfImageViewIcn sfVideoIcn");
                break;
        }
    },

    add_onImageLoadedCommand: function (delegate) {
        this.get_events().addHandler('onImageLoaded', delegate);
    },
    remove_onImageLoadedCommand: function (delegate) {
        this.get_events().removeHandler('onImageLoaded', delegate);
    },

    add_onItemSelectedCommand: function (delegate) {
        this.get_events().addHandler('onItemSelected', delegate);
    },

    remove_onItemSelectedCommand: function (delegate) {
        this.get_events().addHandler('onItemSelected', delegate);
    },

    attachEventHandlers: function (toAttach) {
        this._attachButtonHandlers(toAttach);

        if (toAttach) {

            // prevent memory leaks
            $(window).on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });

            this._closeDialogExtensionDelegate = Function.createDelegate(this, this._closeDialogExtension);

            this._cropResizeRotateDialogCloseDelegate = Function.createDelegate(this, this._cropResizeRotateDialogCloseHandler);
        }
        else {
            if (this._closeDialogExtensionDelegate)
                delete this._closeDialogExtensionDelegate;

            if (this._closeCropResizeRotateDialogCloseDelegate) {
                this.get_cropResizeRotateDialog().remove_close(this._cropResizeRotateDialogCloseDelegate);
                delete this._closeCropResizeRotateDialogCloseDelegate;
            }

            if (this._editMediaDialogLoadDelegate) {
                this._imageEditorDialog.remove_pageLoad(this._editMediaDialogLoadDelegate);
                delete this._editMediaDialogLoadDelegate;
            }

            if (this._editMediaDialogCloseDelegate) {
                this._imageEditorDialog.remove_close(this._editMediaDialogCloseDelegate);
                delete this._editMediaDialogCloseDelegate;
            }
        }
    },

    _attachButtonHandlers: function (toAttach) {
        var selectImgBtn = this.get_selectImageButton();
        var uploadImgBtn = this.get_uploadImageButton();
        var changeImgBtn = this.get_changeImageButton();
        var editImgBtn = this.get_editImageButton();
        var cropResizeRotateBtn = this.get_cropResizeRotateButton();

        if (toAttach) {

            if (selectImgBtn) {
                this._selectImageDelegate = Function.createDelegate(this, this._selectImageClickHandler);
                $addHandler(selectImgBtn, "click", this._selectImageDelegate);
            }

            if (uploadImgBtn) {
                this._uploadImageDelegate = Function.createDelegate(this, this._uploadImageClickHandler);
                $addHandler(uploadImgBtn, "click", this._uploadImageDelegate);
            }

            if (changeImgBtn) {
                this._changeImageDelegate = Function.createDelegate(this, this._changeImageClick);
                $addHandler(changeImgBtn, "click", this._changeImageDelegate);
            }

            if (editImgBtn) {
                this._editImageDelegate = Function.createDelegate(this, this._editImageClick);
                $addHandler(editImgBtn, "click", this._editImageDelegate);
            }

            if (cropResizeRotateBtn) {
                this._cropResizeRotateImageDelegate = Function.createDelegate(this, this._cropResizeRotateImageClick);
                $addHandler(cropResizeRotateBtn, "click", this._cropResizeRotateImageDelegate);
            }
        }
        else {
            if (this._selectImageDelegate) {
                $removeHandler(selectImgBtn, "click", this._selectImageDelegate);
                delete this._selectImageDelegate;
            }

            if (this._uploadImageDelegate) {
                $removeHandler(uploadImgBtn, "click", this._uploadImageDelegate);
                delete this._uploadImageDelegate;
            }

            if (this._changeImageDelegate) {
                $removeHandler(changeImgBtn, "click", this._changeImageDelegate);
                delete this._changeImageDelegate;
            }

            if (this._editImageDelegate) {
                $removeHandler(editImgBtn, "click", this._editImageDelegate);
                delete this._editImageDelegate;
            }

            if (this._cropResizeRotateImageDelegate) {
                $removeHandler(cropResizeRotateBtn, "click", this._cropResizeRotateImageDelegate);
                delete this._cropResizeRotateImageDelegate;
            }
        }
    },

    _cropResizeRotateImageClick: function () {
        if (dialogBase) {
            dialogBase.setWndWidth("1000px");
            dialogBase.setWndHeight("700px");
            var oWnd = dialogBase.get_radWindow();
            if (oWnd) {
                if ($(oWnd._popupElement).closest(".sfSelectorDialog").length > 0) {
                    oWnd._popupElement.style.top = "0";
                    oWnd._popupElement.style.left = "0";
                } else {
                    oWnd.center();
                    oWnd._popupElement.style.top = "50px";
                }
                oWnd.get_contentFrame().contentWindow.document.body.style.position = "static";
            }
        }

        this.openDialog("cropResizeRotateDialog", "?Id=" + this.get_dataItemId() + "&Status=Live&provider=" + this.get_providerName(), true, this._cropResizeRotateDialogCloseDelegate);

        this._resizeDesignerWindow();
    },

    _cropResizeRotateDialogCloseHandler: function (sender, args) {
        var argument = args.get_argument();
        sender.remove_close(this._cropResizeRotateDialogCloseDelegate);
        if (argument.CommandName == "save") {
            this.set_dataItemId(argument.ImageId, false);
            this.refreshUI(true);
        }

        if (dialogBase) {
            dialogBase.setWndHeight("550px");
            dialogBase.setWndWidth("365px");
            var oWnd = dialogBase.get_radWindow();
            if (oWnd) {
                oWnd.center();
                oWnd._popupElement.style.top = "50px";
                oWnd.get_contentFrame().contentWindow.document.body.style.position = "";
            }
        }

        //Modify the crop resize rotate dialog
        this.get_windowManager().getWindowByName("cropResizeRotateDialog").set_modal(false);

        this._resizeDesignerWindow();

        dialogBase.resizeToContent();
    },

    _resizeDesignerWindow: function () {
        //Resize and place the designer property editor dialog
        var propertyEditor = dialogBase.get_radWindow().get_contentFrame().contentWindow.top.$find("PropertyEditorDialog");
        if (propertyEditor) {
            var propertyEditorDialogBase = propertyEditor.AjaxDialog;
            propertyEditorDialogBase.resizeToContent();
            propertyEditorDialogBase.get_radWindow().center();
            propertyEditorDialogBase.get_radWindow()._popupElement.style.top = "50px";
        }
    },

    _selectImageClickHandler: function (args) {
        this.get_imageSelector()._loadCurrentModeView(this.mediaDialogOpenMode.SELECT);
        return this._changeImageClick();
    },

    _uploadImageClickHandler: function (args) {
        this.get_imageSelector()._loadCurrentModeView(this.mediaDialogOpenMode.UPLOAD);
        return this._changeImageClick(args);
    },

    _changeImageClick: function (args) {
        this._openMediaSelectorDialog();

        this.get_imageSelector().resizeTopRadWindow();
        return false;
    },

    _openMediaSelectorDialog: function () {
        this._hiddenElements = jQuery("body>form>div, body>form>span, body>div, body>span").filter(":visible").not(".RadAjax.RadAjax_Sitefinity");
        this._imageSelectorDialog.dialog("open");
        this._imageSelectorDialog.dialog().parent().css("border", "0px");
        this._imageSelectorDialog.dialog().parent().css("padding", "0px");
        this._imageSelectorDialog.dialog().parent().css("min-width", "930px");
        if (this._hiddenElements) {
            this._hiddenElements.hide();
        }
    },

    _editImageClick: function (args) {
        var dataItem = this.get_selectedDataItem();

        var data = {
            Id: this.get_dataItemId(),
            ProviderName: this.get_providerName()
        };

        this._dialogManager.blacklistWindowManager(this.get_windowManager());

        var params = {
            IsEditable: true,
            parentId: dataItem.ParentId
        };
        var commandArgument = { languageMode: "edit", language: this.get_uiCulture() };

        var itemsList = new Object();
        itemsList.getBinder = function () {
            var binder = new Object();
            binder.get_provider = function () {
                return data.ProviderName;
            }
            return binder;
        };

        var dialogContext = {
            commandName: "edit",
            itemsList: itemsList,
            dataItem: data,
            dialog: this._imageEditorDialog,
            params: params,
            key: { Id: this.get_dataItemId() },
            commandArgument: commandArgument
        };

        this._imageEditorDialog.setUrl(this.get_imageEditorDialogUrl() + "&parentId=" + dataItem.ParentId);
        this._dialogManager.openDialog(this.get_imageEditorName(), this, dialogContext);
        if (!this._imageEditorDialog.isMaximized())
            this._imageEditorDialog.maximize();
    },

    _editMediaContentDialogLoadHandler: function (sender, args) {
        sender._iframe.contentWindow.jQuery(".sfBack").html(this.get_editorBackPhrase());
    },

    _editMediaDialogCloseHandler: function (sender, args) {
        if (args && args.get_argument() && args.get_argument() == "rebind") {
            this.refreshUI(this);
        }
    },

    _closeDialogExtension: function (sender, args) {
        var argument = args.get_argument();

        sender.remove_close(this._closeDialogExtensionDelegate);
        if (argument) {
            if (argument.CommandName === "save") {
                this.set_dataItemId(argument.DataItemId, true);
            }
            var dataItem = argument.get_commandArgument();

            if (dataItem) {
                this.setData(dataItem);

                this._showButtons(true);

                this._onItemSelectedHandler(dataItem);
            }
        }
    },

    _imageSelectorSaveHandler: function (selectedItem) {
        if (selectedItem) {
            this.setData(selectedItem);
            this._showButtons(true);
            this._onItemSelectedHandler(selectedItem);
        }
        this._imageSelectorCloseHandler();
    },

    _imageSelectorCloseHandler: function () {
        if (this._imageSelectorDialog) {
            this._imageSelectorDialog.dialog("close");
        }
        if (this._hiddenElements) {
            this._hiddenElements.show();
        }

        if (this.get_dataItemId && this.get_skipBlankItemView) {
            if (this.get_skipBlankItemView()) {
                var imageId = this.get_dataItemId();
                if (!imageId || imageId === Telerik.Sitefinity.getEmptyGuid()) {
                    dialogBase.close();
                    return false;
                }
            }
            dialogBase.resizeToContent();
            dialogBase.get_radWindow().center();
        }
    },

    _onItemSelectedHandler: function (image) {
        var args = image;
        var h = this.get_events().getHandler('onItemSelected');
        if (h) h(this, args);
    },

    _onImageLoadedHandler: function (image) {
        var args = image;
        var h = this.get_events().getHandler('onImageLoaded');
        if (h) h(this, args);
    },

    _isVectorGraphics: function (image) {
        if (image && image.IsVectorGraphics === true) {
            return true;
        }
        return false;
    },

    /* --------------------------------- public methods --------------------------------- */
    //Set data from item selected by the media selector
    //Sys.Debug.trace("createDialog");
    setData: function (selectedItem) {
        if (this.isImage()) {
            this.set_selectedDataItem(selectedItem);
            this.set_dataItemId(selectedItem.Id);
            if (this._isVectorGraphics(selectedItem)) {
                this.set_src(selectedItem.MediaUrl);
            }
            else {
                var src = selectedItem.MediaUrl;
                if (selectedItem.ThumbnailUrl) {
                    src = selectedItem.ThumbnailUrl;
                }
                this.set_src(src);
            }

            if (dialogBase._itemSettingsView) {
                dialogBase._itemSettingsView.set_isVectorGraphics(this._isVectorGraphics(this._selectedDataItem));
                dialogBase._itemSettingsView.set_blobStorageProviderName(this._selectedDataItem.BlobStorageProvider);
                dialogBase._itemSettingsView._onLoad();
            }

            this.setTitle(selectedItem.Title);
            this.setAltText(selectedItem.AlternativeText);
            this.setExtension(selectedItem.Extension);
            this.setSize(selectedItem.TotalSize);
            this.setLibrary(selectedItem.LibraryTitle);
            this.set_providerName(selectedItem.ProviderName);
        }
        else if (this.isDocument()) {
            this.set_selectedDataItem(selectedItem);
            this.set_dataItemId(selectedItem.Id);
            this.setTitle(selectedItem.Title);
            this.setExtension(selectedItem.Extension);
            this.setSize(selectedItem.TotalSize);
            this.setLibrary(selectedItem.LibraryTitle);
            this.set_providerName(selectedItem.ProviderName);
            this.set_documentParams(selectedItem);
        }
        else if (this.isVideo()) {
            if (this.get_mediaField()) {
                var mediaData = {
                    // Chrome does not support 2 <video> tags with the same URL.
                    url: this.get_mediaField()._appendRnd(selectedItem.MediaUrl),
                    title: selectedItem.Title,
                    description: ''
                };
                this.set_mediaParams(mediaData);
            }

            this.set_mediaUrl(selectedItem.MediaUrl);
            this.set_selectedDataItem(selectedItem);
            this.set_dataItemId(selectedItem.Id);
            this.setTitle(selectedItem.Title);
            this.setExtension(selectedItem.Extension);
            this.setSize(selectedItem.TotalSize);
            this.setLibrary(selectedItem.LibraryTitle);
            this.set_providerName(selectedItem.ProviderName);
        }

        if (!this.get_isMediaItemPublished() &&
            selectedItem.LastApprovalTrackingRecord != null &&
            selectedItem.LastApprovalTrackingRecord.Status == "Published") {
            this.set_isMediaItemPublished(true);
        }
        else if (this.get_isMediaItemPublished() &&
            selectedItem.LastApprovalTrackingRecord != null &&
            selectedItem.LastApprovalTrackingRecord.Status == "Unpublished") {
            this.set_isMediaItemPublished(false);
        }
    },

    getData: function () {
        if (this.isVideo()) {
            var mediaUrl = this.get_mediaUrl();
        }
        else {
            mediaUrl = this.get_src();
        }

        return {
            AlternateText: this.getAltText(),
            Title: this.getTitle(),
            DataItemId: this.get_dataItemId(),
            MediaUrl: mediaUrl,
            Extension: this.getExtension(),
            Library: this.getLibrary(),
            ProviderName: this.get_providerName()
        };
    },

    _invokeGet: function (arg) {
        var dataItemId = this.get_dataItemId();
        if (dataItemId && dataItemId != Telerik.Sitefinity.getEmptyGuid()) {
            var url = this._serviceUrl + "?provider=" + this.get_providerName() + "&skip=0&take=1&filter="
                + encodeURIComponent("Status == Live AND Id == " + dataItemId);
            var succeeded = Function.createDelegate(this, this._successGet);
            var failed = Function.createDelegate(this, this._failGet);
            this.get_clientManager().InvokeGet(url, [], [], succeeded, failed, arg, this);
        }
        else {
            //If there is no selected item and empty screen should not be displayed,
            //don't resize the base dialog because it is unneeded 
            var preventDialogBaseResize = this.get_skipBlankItemView();
            this._showButtons(false, preventDialogBaseResize);
        }
    },

    _successGet: function (arg, result, obj, context) {
        if (!result || !result.Items || !result.Items.length) {
            this._failGet("Unable to find the selected item.");
            return;
        }

        var data = result.Items[0];
        this.setData(data);
        this._showButtons(true);
        this._onImageLoadedHandler(data);
    },

    _failGet: function (error, arg, context) {
        console.log(error.Detail);
        this._showButtons(false);
    },

    rebind: function (providerName) {

    },

    openSelectorDialog: function (mediaDlgOpenMode) {
        if (dialogBase) {
            dialogBase.setWndWidth("1000px");
            dialogBase.setWndHeight("700px");
            var oWnd = dialogBase.get_radWindow();
            if (oWnd) {
                if ($(oWnd._popupElement).closest(".sfSelectorDialog").length > 0) {
                    oWnd._popupElement.style.top = "0";
                    oWnd._popupElement.style.left = "0";
                } else {
                    oWnd.center();
                    oWnd._popupElement.style.top = "50px";
                }
                oWnd.get_contentFrame().contentWindow.document.body.style.position = "static";
            }
        }

        var modeParam = mediaDlgOpenMode ? "&dialogOpenMode=" + mediaDlgOpenMode : "";

        var uiCultureParam = this.get_uiCulture() ? "&uiCulture=" + this.get_uiCulture() : "";
        this.openDialog("imageSelector", "?mode=" + this.get_viewMode() + modeParam + uiCultureParam, true, this._closeDialogExtensionDelegate);
    },

    openDialog: function (commandName, queryString, assignCloseEvent, closeDelegate) {
        var dialog = this.get_windowManager().getWindowByName(commandName);
        if (dialog) {
            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);

            if (queryString) {
                var url = dialog.get_navigateUrl();
                var idx = url.indexOf("?");
                if (idx > -1) {
                    url = url.substring(0, idx);
                }

                dialog.set_navigateUrl(url + queryString);
            }
            if (assignCloseEvent) {
                if (!dialog._sfCloseDialogExtension) {
                    dialog._sfCloseDialogExtension = closeDelegate;
                }
                dialog.add_close(dialog._sfCloseDialogExtension);
            }

            dialog.show();
            dialog.maximize();
        }
    },

    refreshUI: function (forceRefresh) {
        if (forceRefresh) {
            this._invokeGet();
        }
    },

    /*--------------------------------- private methods --------------------------------- */
    _arrangeMediaItemDetailsView: function () {
        var altWrapper = this.selectors.alternativeTextLabelWrapper;

        switch (this.get_viewMode()) {
            case "Image":
                $(altWrapper).toggle(true);
                break;
            case "Media":
                $(altWrapper).toggle(false);
                break;
            case "Document":
                $(altWrapper).toggle(false);
                break;
            default:
        }
    },

    isImage: function () {
        return this.get_viewMode() == "Image";
    },

    isDocument: function () {
        return this.get_viewMode() == "Document";
    },

    isVideo: function () {
        return this.get_viewMode() == "Media";
    },

    _initImageBackendEditDialog: function () {
        this._imageEditorDialog = this._dialogManager.getDialogByName(this.get_imageEditorName());

        if (this._imageEditorDialog) {
            this._editMediaDialogLoadDelegate = Function.createDelegate(this, this._editMediaContentDialogLoadHandler);
            this._imageEditorDialog.add_pageLoad(this._editMediaDialogLoadDelegate);

            this._editMediaDialogCloseDelegate = Function.createDelegate(this, this._editMediaDialogCloseHandler);
            this._imageEditorDialog.add_close(this._editMediaDialogCloseDelegate);
        }
    },


    setSize: function (value, isInBytes) {
        //by default the value is sent in Kb
        if (isInBytes) value = parseInt(value / 1024);

        if (value < 1024) this.get_sizeLabel().innerHTML = value + " " + this.get_clientLabelManager().getLabel("LibrariesResources", "Kb");
        else this.get_sizeLabel().innerHTML = parseInt(value / 1024) + " " + this.get_clientLabelManager().getLabel("LibrariesResources", "Mb");
    },

    _showButtons: function (toShow, preventDialogBaseResize) {
        if (toShow) {
            jQuery(this.get_selectImageButton()).hide();
            jQuery(this.get_uploadImageButton()).hide();
            jQuery(this.get_changeImageButton()).show();
            jQuery(this.get_editImageButton()).show();
            jQuery(this.get_cropResizeRotateButton()).show();
            jQuery("#sf_imgIsSelected").show();
            jQuery("#sf_imgIsEmpty").hide();

            if (!this.get_isMediaItemPublished()) {
                jQuery("#sf_docSelected").hide();
                jQuery("#sf_videoSelected").hide();
                jQuery("#sf_imgSelected").hide();
                jQuery("#sf_unpublishedMediaItemMessage").show();
            }
            else {
                jQuery("#sf_unpublishedMediaItemMessage").hide();
                if (this.isImage()) {
                    if (this._isVectorGraphics(this._selectedDataItem)) {
                        jQuery("[id$='cropResizeRotateImageButton']").hide();
                    }
                    else {
                        jQuery("[id$='cropResizeRotateImageButton']").show();
                    }
                    jQuery("#sf_imgSelected").show();
                    jQuery("#sf_docSelected").hide();
                    jQuery("#sf_videoSelected").hide();
                }
                else if (this.isVideo()) {
                    jQuery("#sf_docSelected").hide();
                    if (this.get_useSmallItemPreview()) {
                        jQuery("#sf_imgSelected").show();
                        jQuery("#sf_videoSelected").hide();
                    }
                    else {
                        jQuery("#sf_imgSelected").hide();
                        jQuery("#sf_videoSelected").show();
                    }
                }
                else if (this.isDocument()) {
                    jQuery("#sf_docSelected").show();
                    jQuery("#sf_imgSelected").hide();
                    jQuery("#sf_videoSelected").hide();
                }
            }
        }
        else {
            jQuery(this.get_selectImageButton()).show();
            jQuery(this.get_uploadImageButton()).show();
            jQuery(this.get_changeImageButton()).hide();
            jQuery(this.get_editImageButton()).hide();
            jQuery(this.get_cropResizeRotateButton()).hide();
            jQuery("#sf_imgIsSelected").hide();
            jQuery("#sf_imgIsEmpty").show();
        }

        //Ability to disable dialog resizing if it is causing issues
        if (!preventDialogBaseResize)
            dialogBase.resizeToContent();

    },

    selectors: {
        alternativeTextLabelWrapper: "#alternativeTextLabelWrapper"
    },

    /* --------------------------------- properties --------------------------------- */
    getTitle: function () {
        return this.get_titleLabel().innerText;
    },
    setTitle: function (value) {
        this.get_titleLabel().innerText = value;
    },
    getAltText: function () {
        return this.get_altTextLabel().innerText;
    },
    setAltText: function (value) {
        this.get_altTextLabel().innerText = value;
    },
    getExtension: function () {
        return this.get_extensionLabel().innerText;
    },
    setExtension: function (value) {
        this.get_extensionLabel().innerText = value;
    },
    getSize: function () {
        return this.get_sizeLabel().innerText;
    },
    getLibrary: function (value) {
        return this.get_libraryLabel().innerText;
    },
    setLibrary: function (value) {
        this.get_libraryLabel().innerText = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    },

    get_titleLabel: function () {
        return this._titleLabel;
    },
    set_titleLabel: function (value) {
        this._titleLabel = value;
    },
    get_altTextLabel: function () {
        return this._altTextLabel;
    },
    set_altTextLabel: function (value) {
        this._altTextLabel = value;
    },
    get_extensionLabel: function () {
        return this._extensionLabel;
    },
    set_extensionLabel: function (value) {
        this._extensionLabel = value;
    },
    get_sizeLabel: function () {
        return this._sizeLabel;
    },
    set_sizeLabel: function (value) {
        this._sizeLabel = value;
    },
    get_libraryLabel: function () {
        return this._libraryLabel;
    },
    set_libraryLabel: function (value) {
        this._libraryLabel = value;
    },
    get_imageElement: function () {
        return this._imageElement;
    },
    set_imageElement: function (value) {
        this._imageElement = value;
    },
    get_src: function () {
        return this._src;
    },
    set_src: function (value) {
        if (this.get_imageElement()) {
            this.get_imageElement().src = value;
        }
    },
    //sets video player data
    set_mediaParams: function (value) {
        if (this.get_mediaField()) {
            this.get_mediaField().get_mediaPlayer().setMediaParams(value);
			 $("#sf_videoSelected").addClass("sfHtml5Player");
        }
    },

    ////sets document data
    set_documentParams: function (selectedItem) {
        if (this.get_documentLink()) {
            this.get_documentLink().href = selectedItem.MediaUrl;
            this.get_documentLink().innerText = selectedItem.Title + selectedItem.Extension;
            jQuery(this.get_documentLink()).siblings(".sfext").removeClass().addClass("sfext sf" + selectedItem.Extension.substr(1).toLowerCase());
            dialogBase.resizeToContent();
        }
    },

    //gets the client WebService manager
    get_clientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this.get_uiCulture()) {
                this._clientManager.set_uiCulture(this.get_uiCulture());
            }
        }
        return this._clientManager;
    },

    //gets the Id of the edited item
    get_dataItemId: function () { return this._dataItemId; },
    set_dataItemId: function (value, forceRefreshUI) {
        if (value != this._dataItemId) {
            this._dataItemId = value;
            this.refreshUI(forceRefreshUI);
        }
    },

    get_mediaUrl: function () { return this._mediaUrl; },
    set_mediaUrl: function (value, forceRefreshUI) {
        if (value != this._mediaUrl) {
            this._mediaUrl = value;
            this.refreshUI(forceRefreshUI);
        }
    },

    //gets the client label manager that manages the localization strings 
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_mediaContentSelector: function () {
        return this._mediaContentSelector;
    },
    set_mediaContentSelector: function (value) {
        this._mediaContentSelector = value;
    },

    get_selectedDataItem: function () {
        return this._selectedDataItem;
    },
    set_selectedDataItem: function (value) {
        this._selectedDataItem = value;
    },

    //determines whether the preview of the selected item should be large (in the designers) or small (in the media manager of the content block)
    get_useSmallItemPreview: function () { return this._useSmallItemPreview },
    set_useSmallItemPreview: function (value) { this._useSmallItemPreview = value },

    //gets the button that changes the current image of the view
    get_selectImageButton: function () { return this._selectImageButton; },
    set_selectImageButton: function (value) { this._selectImageButton = value; },

    //gets the button that changes the current image of the view
    get_uploadImageButton: function () { return this._uploadImageButton; },
    set_uploadImageButton: function (value) { this._uploadImageButton = value; },

    //gets the button that changes the current image of the view
    get_changeImageButton: function () { return this._changeImageButton; },
    set_changeImageButton: function (value) { this._changeImageButton = value; },

    //gets the button that changes the current image of the view
    get_editImageButton: function () { return this._editImageButton; },
    set_editImageButton: function (value) { this._editImageButton = value; },

    get_cropResizeRotateButton: function () { return this._cropResizeRotateButton; },
    set_cropResizeRotateButton: function (value) { this._cropResizeRotateButton = value; },

    //gets the image selector dialog
    get_imageSelector: function () {
        return this._imageSelector;
    },
    set_imageSelector: function (value) {
        this._imageSelector = value;
    },

    //gets the window manager that manages window that contains the select image dialog
    get_windowManager: function () { return this._windowManager; },
    set_windowManager: function (value) { this._windowManager = value; },

    get_imageEditorName: function () { return this._imageEditorName; },
    set_imageEditorName: function (value) { this._imageEditorName = value; },

    // The url to the image editor dialog
    get_imageEditorDialogUrl: function () { return this._imageEditorDialogUrl; },
    set_imageEditorDialogUrl: function (value) { this._imageEditorDialogUrl = value; },

    // The back phrase for the media item editor dialog
    get_editorBackPhrase: function () { return this._editorBackPhrase; },
    set_editorBackPhrase: function (value) { this._editorBackPhrase = value; },

    get_uiCulture: function () { return this._uiCulture; },
    set_uiCulture: function (value) { this._uiCulture = value; },

    get_viewMode: function () { return this._viewMode; },
    set_viewMode: function (value) { this._viewMode = value; },

    //gets the option that determines whether to show blank item screen if there is no selected item or to open the items selector
    get_skipBlankItemView: function () { return this._skipBlankItemView },
    set_skipBlankItemView: function (value) { this._skipBlankItemView = value },

    get_isMediaItemPublished: function () { return this._isMediaItemPublished },
    set_isMediaItemPublished: function (value) { this._isMediaItemPublished = value },

    get_cropResizeRotateDialog: function () { return this._cropResizeRotateDialog; },
    set_cropResizeRotateDialog: function (value) { this._cropResizeRotateDialog = value; },

    // Gets the media field control that displays the media url
    get_mediaField: function () { return this._mediaField; },
    // Sets the media field control that displays the media url
    set_mediaField: function (value) { this._mediaField = value; },

    // Gets the selected document link
    get_documentLink: function () { return this._documentLink; },
    // Sets the selected document link
    set_documentLink: function (value) { this._documentLink = value; }
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.SingleMediaContentItemView', Sys.UI.Control);