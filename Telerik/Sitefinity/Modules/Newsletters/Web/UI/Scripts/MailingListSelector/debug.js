Type.registerNamespace("Telerik.Sitefinity.Modules.Newsletters.Web.UI");

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector = function (element) {
    Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.initializeBase(this, [element]);

    this._lnkCancelSelecting = null;
    this._lnkDoneSelecting = null;
    this._listsSelector = null;
    this._selectedMailingListId = null;

    this._cancelDelegate = null;
    this._doneDelegate = null;
    this._selectorReadyDelegate = null;

    this._isDataBound = null;
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.callBaseMethod(this, "initialize");

        this._cancelDelegate = Function.createDelegate(this, this._cancelHandler);
        this._doneDelegate = Function.createDelegate(this, this._doneHandler);
        this._selectorReadyDelegate = Function.createDelegate(this, this._selectorReadyHandler);

        $addHandler(this.get_lnkCancelSelecting(), 'click', this._cancelDelegate);
        $addHandler(this.get_lnkDoneSelecting(), 'click', this._doneDelegate);
        this.get_listsSelector().add_selectorReady(this._selectorReadyDelegate);
    },

    dispose: function () {
        if (this._cancelDelegate) {
            if (this.get_lnkCancelSelecting()) {
                $removeHandler(this.get_lnkCancelSelecting(), 'click', this._cancelDelegate);
            }
            delete this._cancelDelegate;
        }

        if (this._doneDelegate) {
            if (this.get_lnkCancelSelecting()) {
                $removeHandler(this.get_lnkDoneSelecting(), 'click', this._doneDelegate);
            }
            delete this._doneDelegate;
        }

        if (this._selectorReadyDelegate) {
            if (this.get_listsSelector()) {
                this.get_listsSelector().remove_selectorReady(this._selectorReadyDelegate);
            }
            delete this._selectorReadyDelegate;
        }
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.callBaseMethod(this, "dispose");
    },

    /* *************************** public methods *************************** */

    reset: function () {
        this.set_selectedMailingListId(null);
        this.get_listsSelector().set_selectedKeys([]);
        this._isDataBound = false;
    },

    show: function () {
        if (!this._isDataBound) {
            this.dataBind();
            this._isDataBound = true;
        }
        Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.callBaseMethod(this, "show");
    },

    dataBind: function () {
        this.get_listsSelector().dataBind();
    },

    validate: function () {
        return this.get_listsSelector().get_selectedItems().length > 0;
    },

    /* *************************** event handlers *************************** */

    _cancelHandler: function (sender, args) {
        this.close();
    },

    _doneHandler: function (sender, args) {
        var selItems = this.get_listsSelector().get_selectedItems();
        if (selItems.length > 0) {
            var selItem = selItems[0];
            this.close(selItem);
        }
    },

    _selectorReadyHandler: function () {
        if (this._selectedMailingListId) {
            this.get_listsSelector().set_selectedKeys([this._selectedMailingListId]);
        }
        // in order to call selectorReady on every data bound
        this.get_listsSelector()._selectorInitialized = false;
    },

    /* *************************** public properties *************************** */

    get_lnkCancelSelecting: function () {
        return this._lnkCancelSelecting;
    },
    set_lnkCancelSelecting: function (value) {
        this._lnkCancelSelecting = value;
    },
    get_lnkDoneSelecting: function () {
        return this._lnkDoneSelecting;
    },
    set_lnkDoneSelecting: function (value) {
        this._lnkDoneSelecting = value;
    },
    get_listsSelector: function () {
        return this._listsSelector;
    },
    set_listsSelector: function (value) {
        this._listsSelector = value;
    },
    get_selectedMailingListId: function () {
        return this._selectedMailingListId;
    },
    set_selectedMailingListId: function (value) {
        this._selectedMailingListId = value;
    }
}

Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector.registerClass('Telerik.Sitefinity.Modules.Newsletters.Web.UI.MailingListSelector', 
    Telerik.Sitefinity.Web.UI.Kendo.KendoWindow);