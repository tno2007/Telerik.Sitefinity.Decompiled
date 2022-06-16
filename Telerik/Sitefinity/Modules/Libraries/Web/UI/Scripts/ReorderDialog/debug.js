Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

var reorderDialog;

Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog.initializeBase(this, [element]);

    this._imageListView = null;
    this._imageBinder = null;
    this._topCommandBar = null;
    this._bottomCommandBar = null;
    this._itemType = null;
    this._webServiceUrl = null;
    this._dialogTitle = null;
    this._dialogTitlePattern = null;
    this._providerName = null;


    this._imageBinderItemDataBoundDelegate = null;
    this._imageBinderItemReorderedDelegate = null;
    this._commandBarCommandDelegate = null;
    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        reorderDialog = this;

        if (this._imageBinderItemDataBoundDelegate == null) {
            this._imageBinderItemDataBoundDelegate = Function.createDelegate(this, this._imageBinderItemDataBoundHandler);
        }

        if (this._imageBinderItemReorderedDelegate == null) {
            this._imageBinderItemReorderedDelegate = Function.createDelegate(this, this._imageBinderItemReorderedHandler);
        }

        if (this._imageBinderItemSelectDelegate == null) {
            this._imageBinderItemSelectDelegate = Function.createDelegate(this, this._imageBinderItemSelectHandler);
        }

        if (this._imageBinder != null) {
            this._imageBinder.add_onItemDataBound(this._imageBinderItemDataBoundDelegate);
            this._imageBinder.add_onItemReordered(this._imageBinderItemReorderedDelegate);
            this._imageBinder.add_onItemSelectCommand(this._imageBinderItemSelectDelegate);
        }

        if (this._commandBarCommandDelegate == null) {
            this._commandBarCommandDelegate = Function.createDelegate(this, this._commandBarCommandHandler);
        }

        if (this._topCommandBar && this._bottomCommandBar) {
            this._topCommandBar.add_command(this._commandBarCommandDelegate);
            this._bottomCommandBar.add_command(this._commandBarCommandDelegate);
        }

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog.callBaseMethod(this, "dispose");

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    // rebinds the library selector

    prepareDialog: function (dataItem, params) {
        var parentId;
        if (typeof params["folderId"] !== typeof undefined && params["folderId"] && params["folderId"] !== "null") {
            parentId = params["folderId"];
        } else if (dataItem.ParentId && dataItem.ParentId != this.GetEmptyGuid()) {
            parentId = dataItem.ParentId;
        }
        else {
            parentId = dataItem.Id;
        }

        var title = dataItem.LibraryTitle ? dataItem.LibraryTitle : dataItem.Title;
        this._dialogTitle.innerHTML = String.format(this._dialogTitlePattern, title.htmlEncode());
        this._imageBinder.set_globalDataKeys([]);

        if (params && params["providerName"]) {
            this._providerName = params["providerName"];
            if (this._providerName) {
                this._imageBinder.get_urlParams()["provider"] = this._providerName;
            }
        }
        this._imageBinder.get_urlParams()["excludeFolders"] = true;
        this._imageBinder.get_urlParams()["includeSubFoldersItems"] = true;
        this._imageBinder.get_urlParams()["itemType"] = this._itemType;
        this._imageBinder.get_globalDataKeys()["Id"] = parentId;
        this._imageBinder.set_sortExpression("Ordinal ASC");
        this._imageBinder.set_take(500);
        this._imageBinder.DataBind(parentId);
    },

    /* -------------------- events -------------------- */
    _onLoad: function () {
        jQuery("body").addClass("sfMediumDialog");
        if (this._itemType == "Telerik.Sitefinity.Libraries.Model.Document")
            jQuery("body").addClass("sfReorderDocsBdy");
    },
    /* -------------------- event handlers ------------ */

    // Fired when an item order has changed
    _imageBinderItemReorderedHandler: function (sender, args) {
        var dataItem = args.dataItem;
        var oldIndex = args.oldIndex;
        var newIndex = args.newIndex;

        if (oldIndex == newIndex) {
            return;
        }
        var itemElements = this._imageBinder.get_currentItemElements();
        // if the initial position is different from the current
        var newOrdinal;
        if (newIndex == 0) {
            newOrdinal = jQuery(itemElements[1]).data("dataItem").Ordinal / 2;
        }
        else if (newIndex == itemElements.length - 1) { // last position
            newOrdinal = jQuery(itemElements[newIndex - 1]).data("dataItem").Ordinal + 1;
        }
        else {
            var prev = jQuery(itemElements[newIndex - 1]).data("dataItem").Ordinal;
            var next = jQuery(itemElements[newIndex + 1]).data("dataItem").Ordinal;
            newOrdinal = (prev + next) / 2;
        }

        // store the initial ordinal
        var initialOrdinal = jQuery(sender).data("initialOrdinal");
        if (initialOrdinal == null) {
            jQuery(sender).data("initialOrdinal", dataItem.Ordinal);
        }

        dataItem.Ordinal = newOrdinal;
    },

    // Fired after the list is bound to its data
    _imageBinderItemDataBoundHandler: function (sender, args) {
        var maxImgSize = 60;
        var defaultImgSize = 160;
        var dataItem = args.get_dataItem();
        var width = dataItem.Width > 0 ? dataItem.Width : dataItem.SnapshotWidth > 0 ? dataItem.SnapshotWidth : defaultImgSize;
        var height = dataItem.Height > 0 ? dataItem.Height : dataItem.SnapshotHeight > 0 ? dataItem.SnapshotHeight : defaultImgSize;
        var img = $(args.get_itemElement()).find("img");
        if (img) {
            this._resizeImage(img, width, height, maxImgSize);
        }
    },

    // Fired when a command button is clicked
    _commandBarCommandHandler: function (sender, args) {
        switch (args.get_commandName()) {
            case 'saveChanges':
                this._saveChanges();
                break;
            case 'cancel':
                dialogBase.close();
                break;
        }
    },

    // Fired when an image is selected
    _imageBinderItemSelectHandler: function (sender, args) {
        this._selectedImageKey = args.get_key();
        var selectedElement = args.get_itemElement();
        this._highlightSelectedImage(selectedElement);
    },

    /* -------------------- private methods ----------- */

    _saveChanges: function () {
        var result = [];
        var itemElements = this._imageBinder.get_currentItemElements();
        for (var i = 0, length = itemElements.length; i < length; i++) {
            var element = itemElements[i];
            var dataItem = jQuery(element).data("dataItem");
            var ordinal = dataItem.Ordinal;
            var initialOrdinal = jQuery(element).data("initialOrdinal");

            if (initialOrdinal != null && initialOrdinal != dataItem.Ordinal) {
                result.push({ Key: dataItem.Id, Value: dataItem.Ordinal });
            }
        }

        var clientManager = this._imageBinder.get_manager();
        var serviceUrl = this._webServiceUrl;
        var urlParams = [];

        if (this._itemType != null) {
            urlParams["itemType"] = this._itemType;
        }

        if (this._providerName) {
            urlParams["provider"] = this._providerName;
        }

        var keys = [];
        serviceUrl += "batchReorder/";
        var data = result;
        clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._saveChangesSuccess, this._saveChangesFailure, this);
    },

    _highlightSelectedImage: function (selectedElement) {

        //remove selection
        var container = $("#ItemContainer");
        container.find("span").removeClass("sfSelImgBorder");
        container.find("a").removeClass("sfSel");

        if (selectedElement) {
            $(selectedElement).find(".imgSelect").addClass("sfSelImgBorder");
            $(selectedElement).find("a").addClass("sfSel");
        }
    },

    _saveChangesSuccess: function (caller, data, request) {
        dialogBase.closeAndRebind();
    },

    _saveChangesFailure: function (caller, data, request) {
        alert(result.Detail);
    },

    //    _setTitle: function () {
    //                var titleElement = $get(this._librarySelector.get_lblTitleID())
    //                if (titleElement) {
    //                    var itemsName = null;
    //                    var selectedItemsCount = 0;
    //                    var args = this.get_radWindow()._sfArgs;
    //                    if (args) {
    //                        var itemsList = args.get_itemsList();
    //                        if (itemsList) {
    //                            selectedItemsCount = itemsList.get_selectedItems().length;
    //                        }
    //                    }
    //                    var libraryTypeName = this._librarySelector.get_libraryTypeName();

    //                    if (selectedItemsCount == 1) {
    //                        itemsName = this._librarySelector.get_itemName();
    //                    }
    //                    else {
    //                        itemsName = this._librarySelector.get_itemsName();
    //                    }

    //                    titleElement.innerHTML = String.format(this._librarySelector.get_titleText(), libraryTypeName, selectedItemsCount, itemsName);
    //                }
    //    },

    _resizeImage: function (img, w, h, size) {
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h > w) {
                //ie fix
                img.removeAttr("height");
                img.attr("width", size);
            }
            else {
                //ie fix
                img.removeAttr("width");
                img.attr("height", size);
            }
        }
    },

    GetEmptyGuid: function () {
        return '00000000-0000-0000-0000-000000000000';
    },

    /* -------------------- properties ---------------- */

    get_imageBinder: function () { return this._imageBinder; },
    set_imageBinder: function (value) { this._imageBinder = value; },

    get_imageListView: function () { return this._imageListView; },
    set_imageListView: function (value) { this._imageListView = value; },

    get_topCommandBar: function () { return this._topCommandBar; },
    set_topCommandBar: function (value) { this._topCommandBar = value; },

    get_bottomCommandBar: function () { return this._bottomCommandBar; },
    set_bottomCommandBar: function (value) { this._bottomCommandBar = value; },

    get_dialogTitle: function () { return this._dialogTitle; },
    set_dialogTitle: function (value) { this._dialogTitle = value; }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.ReorderDialog", Telerik.Sitefinity.Web.UI.AjaxDialogBase);