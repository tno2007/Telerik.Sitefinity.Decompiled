/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ImageField = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.ImageField.initializeBase(this, [element]);
    this._element = element;
    this._imageElement = null;
    this._viewOriginalSizeButtonElement = null;
    this._originalSizeValue = null;
    this._size = null;
    this._sizeInPx = null;

    this._fieldMode = null;
    this._viewPanelID = null;
    this._uploadPanelID = null;
    this._replaceImageButtonElement = null;
    this._deleteImageButtonElement = null;
    this._cancelUploadButtonElement = null;

    this._replaceImageDelegate = null;
    this._cancelUploadDelegate = null; // This is the delegate used InputField mode

    this._uploadDialog = null;
    this._cancelDelegate = null; // This is the delegate used in Dialog mode

    this._albumId;

    this._imageBinder = null;

    this._firstItemText = null;

    this._selectedImageKey = null;
    this._selectedImageElement = null;

    this._asyncImageSelector = null;
    this._selectedImageItem = null;
    this._dataFieldType = null;
    this._blankLink = {};
    this._dataItem = null;
    this._contentLink = null;
    this._itemId = "00000000-0000-0000-0000-000000000000";
    this._defaultSrc = "";
    this._boundOnServer = false;
    this._imageServiceUrl = null;
    this._editorShowLibFilterWrp = null;
    this._asyncImageSelectorInsertDelegate = null;
    this._onLoadDelegate = null;

    this._hasRadWindowSelectorDialogParent = false;
}

Telerik.Sitefinity.Web.UI.Fields.ImageField.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.ImageField.callBaseMethod(this, "initialize");

        if (this.get_displayMode() == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Write) {
            this._commandDelegate = Function.createDelegate(this, this._commandHandler);
            // subscribe to the click event of the button
            if (this._viewOriginalSizeButtonElement) {
                $addHandler(this._viewOriginalSizeButtonElement, "click", this._commandDelegate);
            }
        }

        if (this._replaceImageButtonElement) {
            this._replaceImageDelegate = Function.createDelegate(this, this._replaceImageHandler);
            $addHandler(this._replaceImageButtonElement, "click", this._replaceImageDelegate);
        }

        if (this._cancelUploadButtonElement) {
            this._cancelUploadDelegate = Function.createDelegate(this, this._cancelUploadHandler);
            $addHandler(this._cancelUploadButtonElement, "click", this._cancelUploadDelegate);
        }

        if (this._asyncImageSelector) {
            this._asyncImageSelectorInsertDelegate = Function.createDelegate(this, this._asyncImageSelectorInsertHandler);
            this._asyncImageSelector.set_customInsertDelegate(this._asyncImageSelectorInsertDelegate);

            var windowWidth = this._editorShowLibFilterWrp ? 930 : 430;
            if (this.get_fieldMode() == Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode.Dialog && this._asyncImageSelector) {
                this._uploadDialog = jQuery(this._asyncImageSelector.get_element()).dialog({
                    autoOpen: false,
                    modal: true,
                    width: windowWidth,
                    height: "auto",
                    closeOnEscape: true,
                    resizable: false,
                    draggable: false,
                    classes: {
                        "ui-dialog": "sfSelectorDialog sfZIndexL"
                    },
                    close: this._asyncImageSelectorInsertDelegate
                });
            }
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.ImageField.callBaseMethod(this, "dispose");

        if (this._commandDelegate) {
            if (this._viewOriginalSizeButtonElement) {
                $removeHandler(this._viewOriginalSizeButtonElement, "click", this._commandDelegate);
            }
            delete this._commandDelegate;
        }

        if (this._replaceImageDelegate) {
            if (this._replaceImageButtonElement) {
                $removeHandler(this._replaceImageButtonElement, "click", this._replaceImageDelegate);
            }
            delete this._replaceImageDelegate;
        }

        if (this._cancelUploadDelegate) {
            if (this._cancelUploadButtonElement) {
                $removeHandler(this._cancelUploadButtonElement, "click", this._cancelUploadDelegate);
            }
            delete this._cancelUploadDelegate;
        }

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }

        this._imageElement = null;
        this._viewOriginalSizeButtonElement = null;
        this._replaceImageButtonElement = null;
        this._cancelUploadButtonElement = null;
    },

    /* ------------------ Events --------------*/
    _onLoad: function () {
        if (typeof dialogBase != "undefined" && dialogBase != null && jQuery(dialogBase._element).closest(".sfDialog").hasClass("sfSelectorDialog")) {
            this._hasRadWindowSelectorDialogParent = true;
        }
    },

    /* --------------------  public methods ----------- */

    reset: function () {
        this.set_value(null);
        if (this._defaultSrc) {
            this._imageElement.src = this._defaultSrc;
        }
        else {
            this._imageElement.src = "";
        }
        Telerik.Sitefinity.Web.UI.Fields.ImageField.callBaseMethod(this, "reset");
    },

    /* -------------------- events -------------------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _commandHandler: function (e) {
        if (this._originalSizeValue) {
            window.open(this._originalSizeValue, "_blank");
        }
    },

    _cancelUploadHandler: function (e) {
        jQuery("#" + this._viewPanelID).show();
        jQuery("#" + this._uploadPanelID).hide();
        this._resizeSelectorDialog();
    },

    _replaceImageHandler: function (e) {
        if (this.get_fieldMode() == Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode.InputField) {
            jQuery("#" + this._viewPanelID).hide();
            jQuery("#" + this._uploadPanelID).show();
        }
        else if (this.get_fieldMode() == Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode.Dialog) {
            this._hiddenElements = jQuery("body>form>div, body>form>span, body>div, body>span").filter(":visible");
            this._uploadDialog.dialog("open");
            var scrollTopHtml = jQuery("html").eq(0).scrollTop();
            var scrollTopBody = jQuery("body").eq(0).scrollTop();
            var scrollTop = ((scrollTopHtml > scrollTopBody) ? scrollTopHtml : scrollTopBody) + 50;
            jQuery(this._uploadDialog).parent().css({ "top": scrollTop });
            this._uploadDialog.dialog().parent().css("min-width", "930px");
            if (this._hiddenElements) {
                this._hiddenElements.hide();
            }
            if (this._asyncImageSelector) {
                this._asyncImageSelector.resizeTopRadWindow();
            }
        this._resizeSelectorDialog();
            //jQuery(".ui-dialog-titlebar").hide();
        }
    },

    /* -------------------- private methods ----------- */
    _asyncImageSelectorInsertHandler: function (selectedItem) {
        if (selectedItem && selectedItem.ThumbnailUrl) {
            this.get_imageElement().src = selectedItem.ThumbnailUrl;
            this._selectedImageItem = selectedItem;
        }
        this._uploadDialog.dialog("close");
        if (this._hiddenElements) {
            this._hiddenElements.show();
        }
        this._resizeSelectorDialog();
    },

    _resolveImageFromContentLink: function (dataItem, contentLink) {
        var imageJson = dataItem["__$Context$imageLink$" + this._dataFieldName];
        if (imageJson) { // serialized with UserProfilesSerialzier, there is a custom property that contains the image
            var image = Sys.Serialization.JavaScriptSerializer.deserialize(imageJson);
            this._bindImage(image);
        } else { //serialized with DataContractSerializer, no context field 

            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            var urlParams = [];
            urlParams["itemType"] = contentLink.ChildItemType;
            urlParams["provider"] = contentLink.ChildItemProviderName;
            urlParams["filter"] = "Id = \"" + contentLink.ChildItemId + "\" AND Status = Live";

            var keys = [contentLink.ChildItemId];

            if (this._loadImageSuccessDelegate === undefined) {
                this._loadImageSuccessDelegate = Function.createDelegate(this, this._loadImageSuccessHandler);
                this._loadImageFailureDelegate = Function.createDelegate(this, this._loadImageFailureHandler);
            }
            clientManager.InvokeGet(this._imageServiceUrl, urlParams, null,
                              this._loadImageSuccessDelegate, this._loadImageFailureDelegate, this);

            this._selectedImageItem = null;
            this._imageElement.src = this._defaultSrc;
        }
    },

    _bindImage: function (imageViewModel) {
        if (this.get_sizeInPx()) {
            var jelm = jQuery(this._imageElement);
            if (imageViewModel.Width < imageViewModel.Height) {
                jelm.removeAttr("height");
                jelm.attr("width", this.get_sizeInPx());
            } else {
                jelm.removeAttr("width");
                jelm.attr("height", this.get_sizeInPx());
            }
        }
        this._imageElement.src = imageViewModel.ThumbnailUrl;
        this._selectedImageItem = imageViewModel;
        this._resizeSelectorDialog();
    },

    _loadImageSuccessHandler: function (sender, args) {
        this._bindImage(args.Items[0]);
    },
    _loadImageFailureHandler: function (sender, args) {
        alert(sender.Detail);
    },

    _resizeSelectorDialog: function () {
        if (this._hasRadWindowSelectorDialogParent) {
            dialogBase.resizeToContent();
        }
    },

    /* -------------------- properties ---------------- */

    // Gets the value of the image control.
    get_value: function () {
        var item = this.get_selectedImageItem();
        switch (this._dataFieldType) {
            case "ContentLink":

                if (item) {
                    var resultLink;
                    if (this._contentLink) {
                        resultLink = this._contentLink;
                    }
                    else {
                        resultLink = Telerik.Sitefinity.cloneObject(this._blankLink);
                    }
                    var dataItem = {};
                    if (this._dataItem) {
                        dataItem = this._dataItem;
                    }
                    resultLink.ChildItemId = item.Id;
                    resultLink.ChildItemProviderName = item.ProviderName;
                    resultLink.ParentItemId = dataItem.Id;
                    resultLink.ParentItemProviderName = dataItem.__providerName;
                    resultLink.ParentItemType = dataItem.__type;
                    return resultLink;
                }
                else {
                    return null;
                }
                break;
            case "String":
                if (this._imageElement) {
                    return this._imageElement.src;
                }
                return "";
                break;
            case "Guid":
                if (item) {
                    return item.Id;
                }
                else {
                    return Telerik.Sitefinity.getEmptyGuid();
                }
                break;
            default:
                break;
        }
    },

    // Sets the value of the image control.
    set_value: function (value) {
        if (this._boundOnServer) {
            return;
        }
        this._originalSizeValue = value;
        var dataItem = this.get_dataItem();
        switch (this._dataFieldType) {
            case "ContentLink":
                this._contentLink = value;

                if (dataItem && value) {
                    this._resolveImageFromContentLink(dataItem, value);
                } else if (!value) {
                    this._selectedImageItem = null;
                    this._imageElement.src = this._defaultSrc;
                }

                break;
            case "String":
                if (value) {
                    if (dataItem && dataItem.Width && dataItem.Height) {
                        this._resizeImage(this._imageElement, dataItem.Width, dataItem.Height, this.get_size());
                    }
                    var appender = "?";
                    if (value.indexOf('?') > 0) { appender = "&"; }
                    this._imageElement.src = value; //+ appender + "size=" + this.get_size();
                }
                break;
            case "Guid":
                this._itemId = value;
                break;
            default:
                break;
        }

        if (this._imageElement.src == "") {
            if (this._defaultSrc) {
                this._imageElement.src = this._defaultSrc;
            }
        }

        this.raisePropertyChanged("value");
        this._valueChangedHandler();
    },

    _resizeImage: function (imgElement, w, h, size) {
        var img = $(imgElement);
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h < w) {
                img.attr("width", size);
                // IE fix
                img.removeAttr("height");
            }
            else {
                img.attr("height", size);
                // IE fix
                img.removeAttr("width");
            }
        }
    },

    // Gets the reference to the DOM element used to display the image control.
    get_imageElement: function () {
        return this._imageElement;
    },

    // Sets the reference to the DOM element used to display the image control.
    set_imageElement: function (value) {
        this._imageElement = value;
    },

    // Gets the reference to the DOM element used to display the view original size button.
    get_viewOriginalSizeButtonElement: function () {
        return this._viewOriginalSizeButtonElement;
    },

    // Sets the reference to the DOM element used to display the view original size button.
    set_viewOriginalSizeButtonElement: function (value) {
        this._viewOriginalSizeButtonElement = value;
    },

    get_size: function () {
        if (this._size == null)
            this._size = 555;
        else
            this._size = "";
        return this._size;
    },

    set_size: function (value) {
        this._size = value;
    },

    get_fieldMode: function () {
        return this._fieldMode;
    },

    set_fieldMode: function (value) {
        this._fieldMode = value;
    },

    get_viewPanelID: function () {
        return this._viewPanelID;
    },

    set_viewPanelID: function (value) {
        this._viewPanelID = value;
    },

    get_uploadPanelID: function () {
        return this._uploadPanelID;
    },

    set_uploadPanelID: function (value) {
        this._uploadPanelID = value;
    },

    // Gets the reference to the DOM element used to display the Replace image button.
    get_replaceImageButtonElement: function () {
        return this._replaceImageButtonElement;
    },

    // Sets the reference to the DOM element used to display the Replace image button.
    set_replaceImageButtonElement: function (value) {
        this._replaceImageButtonElement = value;
    },

    // Gets the reference to the DOM element used to display the Delete image button.
    get_deleteImageButtonElement: function () {
        return this._deleteImageButtonElement;
    },

    // Sets the reference to the DOM element used to display the Delete image button.
    set_deleteImageButtonElement: function (value) {
        this._deleteImageButtonElement = value;
    },

    // Gets the reference to the DOM element used to display the Cancel upload button.
    get_cancelUploadButtonElement: function () {
        return this._cancelUploadButtonElement;
    },

    // Sets the reference to the DOM element used to display the Cancel upload button.
    set_cancelUploadButtonElement: function (value) {
        this._cancelUploadButtonElement = value;
    },

    get_albumId: function () {
        return this._albumId;
    },

    set_albumId: function (value) {
        this._albumId = value;
    },
    get_sizeInPx: function () {
        return this._sizeInPx;
    },

    set_sizeInPx: function (value) {
        this._sizeInPx = value;
    },


    get_firstItemText: function () {
        return this._firstItemText;
    },

    set_firstItemText: function (value) {
        this._firstItemText = value;
    },

    isChanged: function () {
        return false;
    },

    get_asyncImageSelector: function () {
        return this._asyncImageSelector;
    },

    set_asyncImageSelector: function (value) {
        this._asyncImageSelector = value;
    },
    get_selectedImageItem: function () {
        return this._selectedImageItem;
    },
    set_dataItem: function (value) {
        this._dataItem = value;
    },
    get_dataItem: function () { return this._dataItem; }

};
Telerik.Sitefinity.Web.UI.Fields.ImageField.registerClass("Telerik.Sitefinity.Web.UI.Fields.ImageField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem);
