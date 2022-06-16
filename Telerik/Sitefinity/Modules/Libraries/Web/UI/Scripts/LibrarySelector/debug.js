﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI");

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector = function(element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector.initializeBase(this, [element]);

    this._selectorBinderItemDataBoundDelegate = null;
    this._pageLoadDelegate == null

    this._itemsName = null;
    this._itemName = null;
    this._libraryTypeName = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector.prototype =
{
    /* --------------------  set up and tear down ----------- */
    initialize: function () {
        if (this._selectorBinderItemDataBoundDelegate == null) {
            this._selectorBinderItemDataBoundDelegate = Function.createDelegate(this, this._handleSelectorBinderItemDataBound);
        }

        if (this._pageLoadDelegate == null) {
            this._pageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        }

        Sys.Application.add_load(this._pageLoadDelegate);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._selectorBinderItemDataBoundDelegate != null) {
            if (this._contentSelector != null) {
                this._contentSelector.get_itemSelector().remove_binderItemDataBound(this._selectorBinderItemDataBoundDelegate);
            }
            delete this._selectorBinderItemDataBoundDelegate;
        }


        if (this._pageLoadDelegate) {
            Sys.Application.remove_load(this._pageLoadDelegate);
            delete this._pageLoadDelegate;
        }

        Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handlePageLoad: function (sender, args) {
        dialogBase.resizeToContent();
        if (this._itemType && this._itemType.indexOf("DocumentLibrary") != -1) {
            Sys.UI.DomElement.addCssClass(document.body, 'sfDocLibSelector');
        }

        this.get_itemSelector().add_binderItemDataBound(this._selectorBinderItemDataBoundDelegate);
    },

    _handleSelectorBinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();

        // resizing Albums thumbnails
        var img = $(args.get_itemElement()).find(".thumbnailImg");
        if (img && img.length > 0) {
            var thumbnailUrl = dataItem.ThumbnailUrl;
            if (thumbnailUrl == "") {
                img.hide();
            }
            else {
                var width = dataItem.Width;
                var height = dataItem.Height;
                this._resizeImage(img, width, height, 32);
            }
        }

        // setting items count
        var imgInAlbum = $(args.get_itemElement()).find(".sfCount");
        if (imgInAlbum && imgInAlbum.length > 0) {
            var itemsCount = dataItem.ItemsCount;
            if (itemsCount == 1) {
                imgInAlbum.html(itemsCount + " " + this._itemName.toLowerCase());
            }
            else {
                imgInAlbum.html(itemsCount + " " + this._itemsName.toLowerCase());
            }
        }
    },

    /* -------------------- private methods ----------- */

    _resizeImage: function (img, w, h, size) {
        if (h > size || w > size) {
            if (h == w) {
                img.attr("height", size);
                img.attr("width", size);
            }
            else if (h > w) {
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

    /* -------------------- properties ---------------- */

    get_itemsName: function () {
        return this._itemsName;
    },

    set_itemsName: function (value) {
        this._itemsName = value;
    },

    get_itemName: function () {
        return this._itemName;
    },

    set_itemName: function (value) {
        this._itemName = value;
    },

    get_libraryTypeName: function () {
        return this._libraryTypeName;
    },

    set_libraryTypeName: function (value) {
        this._libraryTypeName = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector.registerClass("Telerik.Sitefinity.Modules.Libraries.Web.UI.LibrarySelector", Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector);