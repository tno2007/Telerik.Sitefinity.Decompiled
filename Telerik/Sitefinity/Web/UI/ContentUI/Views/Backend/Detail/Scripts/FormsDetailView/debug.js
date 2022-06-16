/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail");

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView = function (element) {
    this._element = element;
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.initializeBase(this, [element]);
    this._bindOnLoad = null;
    this._binder = null;
    this._fieldControlIds = [];
    this._sectionIds = [];
    this._dataItem = null;
    this._deleteConfirmationMessage = null;
    this._referralCodeLabel = null;

    this._baseBackendUrl = null;

    this._deleteEntryClickDelegate = null;
    this._deleteEntryButtonId = null;
    this._editEntryClickDelegate = null;
    this._editEntryButtonId = null;

    this._deleteButton = null;
    this._editButton = null;
    this._messageControl = null;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.callBaseMethod(this, "initialize");
    },

    dispose: function () {
        if (this._deleteEntryClickDelegate) {
            delete this._deleteEntryClickDelegate;
        }
        if (this._editEntryClickDelegate) {
            delete this._editEntryClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods --------------------  */

    // Binds the formDetailView
    dataBind: function (dataItem, key) {        

        var clientManager = this._binder.get_manager();
        var urlParams = [];
        if (this._providerName != null) {
            urlParams['provider'] = this._providerName;
        }

        if (this._itemHistoryVersion != null) {
            urlParams['version'] = this._itemHistoryVersion;
        }

        var keys = [];
        for (var keyName in key) {
            var val = key[keyName];
            if (typeof val !== "function") {
                keys.push(val);
            }
        }
        clientManager.InvokeGet(this._binder.get_serviceBaseUrl(), urlParams, key, this._dataBindSuccess, this._dataBindFailure, this);
    },

    //rebinds the data from the server
    rebind: function () {
        this._binder.reset();
        this.dataBind(this._callerDataItem, this._dataKey);
    },

    //resets the form
    reset: function () {
        this._binder.reset();
    },

    /* -------------------- events -------------------- */

    // Happens when details are changed
    add_detailsChanged: function (delegate) {
        this.get_events().addHandler("detailsChanged", delegate);
    },
    remove_detailsChanged: function (delegate) {
        this.get_events().removeHandler("detailsChanged", delegate);
    },
    _raiseDetailsChanged: function () {
        var handler = this.get_events().getHandler("detailsChanged");
        if (handler) handler(this);
    },
    add_detailViewCommand: function (delegate) {
        this.get_events().addHandler("detailViewCommand", delegate);
    },
    remove_detailViewCommand: function (delegate) {
        this.get_events().removeHandler("detailViewCommand", delegate);
    },
    _raiseDetailViewCommand: function (commandName, commandArgument) {
        var args = new Telerik.Sitefinity.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler("detailViewCommand");
        if (handler) handler(this, args);
    },

    /* -------------------- event handlers -------------------- */

    // fired when page has been loaded
    _handlePageLoad: function (sender, args) {
        this._deleteEntryClickDelegate = Function.createDelegate(this, this._deleteEntryClickHandler);
        this._deleteButton = $get(this._deleteEntryButtonId);
        if(this._deleteButton)
            $addHandler(this._deleteButton, "click", this._deleteEntryClickDelegate);

        this._editEntryClickDelegate = Function.createDelegate(this, this._editEntryClickHandler);
        this._editButton = $get(this._editEntryButtonId);
        if(this._editButton)
            $addHandler(this._editButton, "click", this._editEntryClickDelegate);

        this._binder.set_fieldControlIds(this._fieldControlIds);
        this._binder.set_requireDataItemControlIds(this._requireDataItemControlIds);

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.callBaseMethod(this, "_handlePageLoad");
    },

    _deleteEntryClickHandler: function (e) {
        if (confirm(this._deleteConfirmationMessage)) {
            var keys = new Array();
            keys.push(this._dataItem.Item.Id);

            var urlParams = [];
            if (this._providerName != null) {
                urlParams['provider'] = this._providerName;
            }

            if (this._itemHistoryVersion != null) {
                urlParams['version'] = this._itemHistoryVersion;
            }

            var url = this._binder.get_serviceBaseUrl();
            var clientManager = this._binder.get_manager();
            clientManager.InvokeDelete(url, urlParams, keys, this._deleteSuccess, this._deleteFailure, this, this);
        }
    },
    _deleteSuccess: function (sender, result) {
        sender._raiseDetailsChanged();
    },

    _deleteFailure: function (result) {
        alert(result.Details);
    },

    _editEntryClickHandler: function (e) {
        this._raiseDetailViewCommand("edit", this.get_binder().get_dataItem());
    },

    // called when data binding was successfull
    _dataBindSuccess: function (sender, result) {
        this.Caller._binder.BindItem(result);
        this.Caller._dataItem = result;
        jQuery(this.Caller.get_referralCodeLabel()).text(result.Item.ReferralCode);
    },

    // called when data binding was not successful
    _dataBindFailure: function (sender, result) {
        alert(result.Detail);
    },

    /* -------------------- private methods -------------------- */

    /* -------------------- properties -------------------- */

    // gets the reference to the field controls binder
    get_binder: function () {
        return this._binder;
    },
    // sets the reference to the field controls binder
    set_binder: function (value) {
        this._binder = value;
    },

    // IDs of the field controls which require the current data item when binding
    get_requireDataItemControlIds: function () {
        return this._requireDataItemControlIds;
    },

    set_requireDataItemControlIds: function (value) {
        this._requireDataItemControlIds = value;
    },

    get_messageControl: function () {
        return this._messageControl;
    },

    set_messageControl: function (value) {
        this._messageControl = value;
    },

    get_referralCodeLabel: function () { return this._referralCodeLabel; },
    set_referralCodeLabel: function (value) { this._referralCodeLabel = value; }
};

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Detail.FormsDetailView.js", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase);
