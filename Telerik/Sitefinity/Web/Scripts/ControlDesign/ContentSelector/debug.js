Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector = function (element) {
    this._itemType = null;
    this._allowMultipleSelection = null;
    this._itemSelector = null;
    this._titleText = null;
    this._lblTitle = null;
    this._lnkDoneSelecting = null;
    this._lnkCancel = null;
    this._itemsFilter = null;
    this._bindOnLoad = null;

    this._selectedKeys = null;
    this._isSelectorReady = null;

    this._selectorReadyDelegate = null;
    this._selectorBinderDataboundDelegate = null;
    this._doneClientSelectionDelegate = null;
    this._doneSelectingDelegate = null;
    this._cancelDelegate = null; 
    this._providerName = null;

    Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector.callBaseMethod(this, "initialize");

        if (this._doneClientSelectionDelegate === null) {
            this._doneClientSelectionDelegate = Function.createDelegate(this, this._doneClientSelectionHandler);
        }

        if (this._lnkDoneSelecting) {
            this._doneSelectingDelegate = Function.createDelegate(this, this.doneSelecting);
            $addHandler(this._lnkDoneSelecting, "click", this._doneSelectingDelegate);
        }

        if (this._lnkCancel) {
            this._cancelDelegate = Function.createDelegate(this, this.cancel);
            $addHandler(this._lnkCancel, "click", this._cancelDelegate);
        }

        if (this._lblTitle && this._titleText) {
            this.setLabelText(this._lblTitle, this._titleText);
        }

        jQuery('a[data-load-more-link-button]').hide();

        Sys.Application.add_load(Function.createDelegate(this, this.onload));

        this._selectorReadyDelegate = Function.createDelegate(this, this.selectorReady);
        this._selectorBinderDataboundDelegate = Function.createDelegate(this, this.selectorBinderDatabound);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector.callBaseMethod(this, "dispose");

        if (this._lnkDoneSelecting && this._doneSelectingDelegate) {
            $removeHandler(this._lnkDoneSelecting, "click", this._doneSelectingDelegate);
        }
        if (this._lnkCancel && this._cancelDelegate) {
            $removeHandler(this._lnkCancel, "click", this._cancelDelegate);
        }

        // Clean up events
        if (this._doneClientSelectionDelegate) {
            delete this._doneClientSelectionDelegate;
        }

        if (this._selectorReadyDelegate) {
            delete this._selectorReadyDelegate;
        }

        if (this._selectorBinderDataboundDelegate) {
            delete this._selectorBinderDataboundDelegate;
        }
    },

    onload: function () {
        this.get_itemSelector().set_itemType(this._itemType);

        this.get_itemSelector().add_selectorReady(this._selectorReadyDelegate);
        this.get_itemSelector().add_binderDataBound(this._selectorBinderDataboundDelegate);

        this.updateSelectorFilter(this._itemsFilter);
        this.get_itemSelector().set_bindOnLoad(this.get_bindOnLoad());
    },

    dataBind: function () {
        this.get_itemSelector().bindSelector();
    },

    selectorBinderDatabound: function () {
        if (this._selectedKeys != null) {
            this.get_itemSelector().set_selectedKeys(this._selectedKeys);
            this._selectedKeys = null;
        }
    },

    selectorReady: function () {
        this._isSelectorReady = true;
        if (this._selectedKeys != null) {
            this.get_itemSelector().set_selectedKeys(this._selectedKeys);
            this._selectedKeys = null;
        }
        this.updateSelectorFilter(this._itemsFilter);
    },

    updateSelectorFilter: function (newFilter) {
        var selector = this.get_itemSelector();
        if (selector != null)
            selector.set_itemsFilter(this._itemsFilter);
    },

    doneSelecting: function () {
        this._doneClientSelectionHandler(this.getSelectedKeys());
    },

    cancel: function () {
        this._doneClientSelectionHandler(null);
    },

    getSelectedKeys: function () {
        return this.get_itemSelector().get_selectedKeys();
    },

    getSelectedItems: function () {
        return this.get_itemSelector().getSelectedItems();
    },

    setLabelText: function (LabelElement, newText) {
        LabelElement.innerHTML = newText;
    },

    set_selectedKeys: function (keys) {
        this._selectedKeys = keys;
        if (this._isSelectorReady) {
            this.get_itemSelector().set_selectedKeys(this._selectedKeys);
        }
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

    get_itemType: function () {
        return this._itemType;
    },

    set_itemType: function (value) {
        if (this._itemType != value) {
            this._itemType = value;
            if (this.get_itemSelector()) {
                this.get_itemSelector().set_itemType(value);
            }
            this.raisePropertyChanged('itemType');
        }
    },

    get_allowMultipleSelection: function () {
        return this._allowMultipleSelection;
    },

    set_allowMultipleSelection: function (value) {
        if (this._allowMultipleSelection != value) {
            this._allowMultipleSelection = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('allowMultipleSelection');
        }
    },

    get_itemSelector: function () {
        return this._itemSelector;
    },

    set_itemSelector: function (value) {
        if (this._itemSelector != value) {
            this._itemSelector = value;
            this.raisePropertyChanged('itemSelector');
        }
    },

    get_binder: function () {
        if (this._itemSelector) {
            return this._itemSelector.get_binder();
        }
        return null;
    },

    get_titleText: function () {
        return this._titleText;
    },

    set_titleText: function (value) {
        if (this._titleText != value) {
            this._titleText = value;
            this.raisePropertyChanged('titleText');
        }
    },

    get_lblTitle: function () {
        return this._lblTitle;
    },

    set_lblTitle: function (value) {
        if (this._lblTitle != value) {
            this._lblTitle = value;
            this.raisePropertyChanged('lblTitle');
        }
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

    get_itemsFilter: function () {
        return this._itemsFilter;
    },

    set_itemsFilter: function (value) {
        if (this._itemsFilter != value) {
            this._itemsFilter = value;
            this.raisePropertyChanged('itemsFilter');
            this.updateSelectorFilter(value);
        }
    },

    get_bindOnLoad: function () {
        return (String(this._bindOnLoad).toUpperCase() == "TRUE");
    },

    set_bindOnLoad: function (value) {
        if (this._bindOnLoad != value) {
            this._bindOnLoad = (String(value).toUpperCase() == "TRUE");
            this.raisePropertyChanged('bindOnLoad');
        }
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
        if (this._itemSelector) {
            this._itemSelector.set_providerName(value);
        }
    }
};

Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelector', Sys.UI.Control);