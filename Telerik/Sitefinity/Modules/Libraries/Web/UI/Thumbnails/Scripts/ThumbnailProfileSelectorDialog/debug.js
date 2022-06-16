﻿Type.registerNamespace("Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails");

/* ThumbnailProfileSelectorDialog class */
Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog = function (element) {
    Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.initializeBase(this, [element]);
    this._itemSelector = null;
    this._lnkDone = null;
    this._lnkCancel = null;
    this._selectedKeys = null;
    this._isSelectorReady = false;
    this._binderBound = false;

    this._doneSelectingDelegate = null;
    this._cancelDelegate = null;
    this._onloadDelegate = null;
    this._selectorReadyDelegate = null;
    this._binderDataBoundDelegate = null;
}

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.callBaseMethod(this, "initialize");

        if (this._lnkDone) {
            this._doneSelectingDelegate = Function.createDelegate(this, this.doneSelecting);
            $addHandler(this._lnkDone, "click", this._doneSelectingDelegate);
        }

        if (this._lnkCancel) {
            this._cancelDelegate = Function.createDelegate(this, this.cancel);
            $addHandler(this._lnkCancel, "click", this._cancelDelegate);
        }
        this._selectorReadyDelegate = Function.createDelegate(this, this.selectorReady);
        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBoundHandler);
        this._itemSelector.add_binderDataBound(this._binderDataBoundDelegate);
        this._onloadDelegate = Function.createDelegate(this, this.onload);
        Sys.Application.add_load(this._onloadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.callBaseMethod(this, "dispose");
        if (this._lnkDone) {
            $removeHandler(this._lnkDone, "click", this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }
        if (this._lnkCancel) {
            $removeHandler(this._lnkCancel, "click", this._cancelDelegate);
            delete this._cancelDelegate;
        }
        this._itemSelector.remove_selectorReady(this._selectorReadyDelegate);
        delete this._selectorReadyDelegate;
        this._itemSelector.remove_binderDataBound(this._binderDataBoundDelegate);
        delete this._binderDataBoundDelegate;
        Sys.Application.remove_load(this._onloadDelegate);
        delete this._onloadDelegate;
    },

    /* -------------------- event handlers ------------ */
    onload: function () {        
        this._itemSelector.get_binder().set_clearSelectionOnRebind(false);

        this._itemSelector.add_selectorReady(this._selectorReadyDelegate);
    },

    /* -------------------- events -------------------- */
    add_doneClientSelection: function (delegate) {
        this.get_events().addHandler("doneClientSelection", delegate);
    },
    remove_doneClientSelection: function (delegate) {
        this.get_events().removeHandler("doneClientSelection", delegate);
    },

    /* -------------------- event handlers ------------ */
    selectorReady: function () {
        this._isSelectorReady = true;
        this.updateSelectorFilter(this._itemsFilter);
    },

    updateSelectorFilter: function (newFilter) {
        if (this._itemSelector)
            this._itemSelector.set_itemsFilter(newFilter);
    },

    doneSelecting: function () {
        this._doneClientSelectionHandler("done", this.get_selectedKeys());
        this.close();
    },
    cancel: function () {
        //this._doneClientSelectionHandler("cancel", null);
        this.close();
    },

    show: function (items) {
        if (jQuery("body").hasClass("sfFormDialog")) {
            if (!this.get_kendoWindow().options["modal"])
                this.get_kendoWindow().setOptions({ modal: true });
            if (!this.get_kendoWindow().options["width"] != "425px")
                this.get_kendoWindow().setOptions({ width: "425px" });
        } else {
            if (!jQuery(this.get_kendoWindow().wrapper).hasClass("sfWindowInWindow"))
                jQuery(this.get_kendoWindow().wrapper).addClass("sfWindowInWindow");

            if (this.get_kendoWindow().options["modal"])
                this.get_kendoWindow().setOptions({ modal: false });
            if (!this.get_kendoWindow().options["width"] != "355px")
                this.get_kendoWindow().setOptions({ width: "355px" });
        }

        this.set_selectedKeys(items);

        Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.callBaseMethod(this, "show");
    },
        
    _doneClientSelectionHandler: function (command, items) {
        var h = this.get_events().getHandler('doneClientSelection');
        var args = new Telerik.Sitefinity.CommandEventArgs(command, items);
        if (h) h(this, args);
    },
    _binderDataBoundHandler: function (sender, args) {
        this._binderBound = true;
        this._itemSelector.selectItemsInternal(this._selectedKeys);
    },
    
    /* -------------------- private methods ----------- */
    _getBinder: function () {
        return this._itemSelector.get_binder();
    },

    /* -------------------- properties ---------------- */
    get_selectedKeys: function () {
        return this._itemSelector.get_selectedKeys();
    },
    set_selectedKeys: function (keys) {
        this._selectedKeys = keys;

        this._itemSelector.cleanUp();
        this._itemSelector.selectItemsInternal(this._selectedKeys);

        if (this._binderBound) {
            this._itemSelector.bindSelector();
        }
    },
    get_itemSelector: function () {
        return this._itemSelector;
    },
    set_itemSelector: function (value) {
        this._itemSelector = value;
    },
    get_lnkDone: function () {
        return this._lnkDone;
    },
    set_lnkDone: function (value) {
        this._lnkDone = value;
    },
    get_lnkCancel: function () {
        return this._lnkCancel;
    },
    set_lnkCancel: function (value) {
        this._lnkCancel = value;
    }
};

Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog.registerClass('Telerik.Sitefinity.Modules.Libraries.Web.UI.Thumbnails.ThumbnailProfileSelectorDialog', Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);