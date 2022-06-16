Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* LanguageSelector class */
Telerik.Sitefinity.Web.UI.LanguageSelector = function (element) {
    Telerik.Sitefinity.Web.UI.LanguageSelector.initializeBase(this, [element]);
    this._itemSelector = null;
    this._lnkDone = null;
    this._lnkCancel = null;
    this._showCultures = null;
    this._showLanguages = null;
    this._selectedKeys = null;
    this._selectedItems = null;
    this._isSelectorReady = false;
    this._languagesServiceUrl = null;
    this._culturesServiceUrl = null;
    this._binderBound = false;
    this._useGlobal = false;

    this._doneSelectingDelegate = null;
    this._cancelDelegate = null;
    this._onloadDelegate = null;
    this._selectorReadyDelegate = null;
    this._showLanguagesDelegate = null;
    this._showCulturesDelegate = null;
    this._binderDataBindingDelegate = null;
    this._binderDataBoundDelegate = null;
}

Telerik.Sitefinity.Web.UI.LanguageSelector.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.LanguageSelector.callBaseMethod(this, "initialize");

        if (this._lnkDone) {
            this._doneSelectingDelegate = Function.createDelegate(this, this.doneSelecting);
            $addHandler(this._lnkDone, "click", this._doneSelectingDelegate);
        }

        if (this._lnkCancel) {
            this._cancelDelegate = Function.createDelegate(this, this.cancel);
            $addHandler(this._lnkCancel, "click", this._cancelDelegate);
        }
        this._showLanguagesDelegate = Function.createDelegate(this, this._showLanguagesHandler);
        $addHandler(this._showLanguages, "click", this._showLanguagesDelegate);
        this._showCulturesDelegate = Function.createDelegate(this, this._showCulturesHandler);
        $addHandler(this._showCultures, "click", this._showCulturesDelegate);
        this._selectorReadyDelegate = Function.createDelegate(this, this.selectorReady);
        this._binderDataBindingDelegate = Function.createDelegate(this, this._binderDataBindingHandler);
        this._itemSelector.add_binderDataBinding(this._binderDataBindingDelegate);
        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBoundHandler);
        this._itemSelector.add_binderDataBound(this._binderDataBoundDelegate);
        this._onloadDelegate = Function.createDelegate(this, this.onload);
        $find(this._itemSelector._binderId).get_urlParams()["useGlobal"] = this.get_useGlobal();

        Sys.Application.add_load(this._onloadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.LanguageSelector.callBaseMethod(this, "dispose");
        if (this._lnkDone) {
            $removeHandler(this._lnkDone, "click", this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }
        if (this._lnkCancel) {
            $removeHandler(this._lnkCancel, "click", this._cancelDelegate);
            delete this._cancelDelegate;
        }
        $removeHandler(this._showLanguages, "click", this._showLanguagesDelegate);
        delete this._showLanguagesDelegate;
        $removeHandler(this._showCultures, "click", this._showCulturesDelegate);
        delete this._showCulturesDelegate;
        this._itemSelector.remove_selectorReady(this._selectorReadyDelegate);
        delete this._selectorReadyDelegate;
        this._itemSelector.remove_binderDataBinding(this._binderDataBindingDelegate);
        delete this._binderDataBindingDelegate;
        this._itemSelector.remove_binderDataBound(this._binderDataBoundDelegate);
        delete this._binderDataBoundDelegate;
        Sys.Application.remove_load(this._onloadDelegate);
        delete this._onloadDelegate;
    },

    /* -------------------- event handlers ------------ */
    onload: function () {
        this._itemSelector.set_itemType(this._itemType);
        
        this._itemSelector.get_binder().set_clearSelectionOnRebind(false);

        this._itemSelector.add_selectorReady(this._selectorReadyDelegate);

        if (this.get_useGlobal()) {
            $(this._showLanguages).addClass("sfDisplayNone");
            $(this._showCultures).addClass("sfDisplayNone");
            $(this._itemSelector._selectorSearchBox._element).addClass("sfDisplayNone");
        }
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
        this._doneClientSelectionHandler("done", this.get_selectedItems());
    },
    cancel: function () {
        this._doneClientSelectionHandler("cancel", null);
    },

    _doneClientSelectionHandler: function (command, items) {
        var h = this.get_events().getHandler('doneClientSelection');
        var args = new Telerik.Sitefinity.CommandEventArgs(command, items);
        if (h) h(this, args);
    },
    _showLanguagesHandler: function () {
        $(this._showLanguages).addClass("sfDisplayNone");
        $(this._showCultures).removeClass("sfDisplayNone");
        this._getBinder().set_serviceBaseUrl(this._languagesServiceUrl);
        this._itemSelector.bindSelector();
    },
    _showCulturesHandler: function () {
        $(this._showCultures).addClass("sfDisplayNone");
        $(this._showLanguages).removeClass("sfDisplayNone");
        this._getBinder().set_serviceBaseUrl(this._culturesServiceUrl);
        this._itemSelector.bindSelector();
    },
    _binderDataBindingHandler: function (sender, args) {
        if (this._selectedItems) {
            var items = args.get_dataItem().Items;
            for (var i = 0, l = this._selectedItems.length; i < l; i++) {
                var selectedItem = this._selectedItems[i];
                var index = items.length;
                while (index--) {
                    var item = items[index];
                    if (item.Culture == selectedItem.Culture && item.UICulture == selectedItem.UICulture) {
                        items.splice(index, 1);
                    }
                }
            }
        }
    },
    _binderDataBoundHandler: function (sender, args) {
        this._binderBound = true;
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
    },

    get_selectedItems: function () {
        return this._itemSelector.getSelectedItems();
    },
    set_selectedItems: function (items) {
        this._selectedItems = items;
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
    },
    get_showCultures: function () {
        return this._showCultures;
    },
    set_showCultures: function (value) {
        this._showCultures = value;
    },
    get_showLanguages: function () {
        return this._showLanguages;
    },
    set_showLanguages: function (value) {
        this._showLanguages = value;
    },
    get_languagesServiceUrl: function () {
        return this._languagesServiceUrl;
    },
    set_languagesServiceUrl: function (value) {
        this._languagesServiceUrl = value;
    },
    get_culturesServiceUrl: function () {
        return this._culturesServiceUrl;
    },
    set_culturesServiceUrl: function (value) {
        this._culturesServiceUrl = value;
    },
    get_useGlobal: function () {
        return this._useGlobal;
    },
    set_useGlobal: function (value) {
        this._useGlobal = value;
    }
};

Telerik.Sitefinity.Web.UI.LanguageSelector.registerClass('Telerik.Sitefinity.Web.UI.LanguageSelector', Sys.UI.Control);