Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.FolderSelector = function (element) {
    this._foldersGenericSelector = null;
    this._lnkDoneSelecting = null;
    this._lnkCancel = null;

    this._doneSelectingDelegate = null;
    this._doneClientSelectionDelegate = null;
    this._cancelDelegate = null;

    this._includeChildLibraryItemsCheckbox = null;
    this._includesChildLibraryItems = true;
    this._itemsCount = null;

    Telerik.Sitefinity.Web.UI.FolderSelector.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.FolderSelector.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FolderSelector.callBaseMethod(this, "initialize");

        this._doneClientSelectionDelegate = Function.createDelegate(this, this._doneClientSelectionHandler);

        if (this.get_lnkDoneSelecting()) {
            this._doneSelectingDelegate = Function.createDelegate(this, this._doneSelectingHandler);
            $addHandler(this.get_lnkDoneSelecting(), "click", this._doneSelectingDelegate);
        }

        if (this.get_lnkCancel()) {
            this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);
            $addHandler(this.get_lnkCancel(), "click", this._cancelDelegate);
        }
    },

    dispose: function () {
        if (this._doneSelectingDelegate) {
            if (this.get_lnkDoneSelecting())
                $removeHandler(this.get_lnkDoneSelecting(), "click", this._doneSelectingDelegate);
            delete this._doneSelectingDelegate;
        }

        if (this._cancelDelegate) {
            if (this.get_lnkCancel())
                $removeHandler(this.get_lnkCancel(), "click", this._cancelDelegate);
            delete this._cancelDelegate;
        }

        // Clean up events
        if (this._doneClientSelectionDelegate) {
            delete this._doneClientSelectionDelegate;
        }

        Telerik.Sitefinity.Web.UI.FolderSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------------------- public methods ---------------------------------- */

    // Binds the generic selector to the data.
    dataBind: function (query) {

        var urlParams = this.get_foldersGenericSelector().get_treeBinder().get_urlParams();
        if (urlParams['take'] === undefined || urlParams['take'] === null) {
            urlParams['take'] = this.get_itemsCount()
            this.get_foldersGenericSelector().get_treeBinder().set_urlParams(urlParams);
        }
        this.get_foldersGenericSelector().dataBind(query);
    },

    getSelectedKeys: function () {
        return this.get_foldersGenericSelector().get_selectedItemIds();
    },

    getSelectedItems: function () {
        return this.get_foldersGenericSelector().get_selectedItems();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _doneSelectingHandler: function () {
        var items = this.getSelectedKeys();
        if ((!items || items.length == 0) && this.get_selectedItem()) {
            items = [this.get_selectedItem().Id];
        }
        this._doneClientSelectionHandler(items);
    },

    _cancelHandler: function () {
        this._doneClientSelectionHandler(null);
    },

    /* Event handlers  */
    _doneClientSelectionHandler: function (items) {
        var h = this.get_events().getHandler('doneClientSelection');
        if (h) h(items);
    },

    /* events binding and unbinding */
    add_doneClientSelection: function (delegate) {
        this.get_events().addHandler('doneClientSelection', delegate);
    },
    remove_doneClientSelection: function (delegate) {
        this.get_events().removeHandler('doneClientSelection', delegate);
    },

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties -------------------------------------- */

    get_foldersGenericSelector: function () {
        return this._foldersGenericSelector;
    },
    set_foldersGenericSelector: function (value) {
        this._foldersGenericSelector = value;
    },
    get_lnkDoneSelecting: function () {
        return this._lnkDoneSelecting;
    },
    set_lnkDoneSelecting: function (value) {
        if (this._lnkDoneSelecting != value) {
            this._lnkDoneSelecting = value;
            this.raisePropertyChanged('lnkDoneSelecting');
        }
    },
    get_lnkCancel: function () {
        return this._lnkCancel;
    },
    set_lnkCancel: function (value) {
        if (this._lnkCancel != value) {
            this._lnkCancel = value;
            this.raisePropertyChanged('lnkCancel');
        }
    },
    get_selectedItem: function () {
        return this.get_foldersGenericSelector().get_selectedItem();
    },
    get_includesChildLibraryItems: function () {
        if (this.get_includeChildLibraryItemsCheckbox()) {
            this._includesChildLibraryItems = this.get_includeChildLibraryItemsCheckbox().checked;
        }

        return this._includesChildLibraryItems;
    },
    set_includesChildLibraryItems: function (value) {
        this._includesChildLibraryItems = value;

        if (this.get_includeChildLibraryItemsCheckbox()) {
            this.get_includeChildLibraryItemsCheckbox().checked = value;
        }
    },
    get_providerName: function () {
        return this.get_foldersGenericSelector().get_provider();
    },
    set_providerName: function (value) {
        this.get_foldersGenericSelector().set_provider(value);
    },

    get_includeChildLibraryItemsCheckbox: function () {
        return this._includeChildLibraryItemsCheckbox;
    },
    set_includeChildLibraryItemsCheckbox: function (value) {
        this._includeChildLibraryItemsCheckbox = value;
    },

    get_itemsCount: function () {
        return this._itemsCount;
    },

    set_itemsCount: function (value) {
        this._itemsCount = value;
    }
}

Telerik.Sitefinity.Web.UI.FolderSelector.registerClass('Telerik.Sitefinity.Web.UI.FolderSelector', Sys.UI.Control);