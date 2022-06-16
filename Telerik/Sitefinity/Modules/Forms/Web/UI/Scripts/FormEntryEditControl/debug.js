Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");
formEntryEditControl = null;
Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl = function (element) {
    this._fieldControlIds = null;
    this._requireDataItemFieldControlIds = null;
    this._binder = null;
    this._submitButton = null;
    this._cancelLink = null;
    this._blankDataItem = null;
    this._isNew = true;

    this._siteId = null;

    this._dataBindSuccessDelegate = null;
    this._submitButtonClickDelegate = null;
    this._cancelLinkClickDelegate = null;
    this._handlePageLoadDelegate = null;
    this._itemSavedDelegate = null;
    Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl.initializeBase(this, [element]);
};

Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        formEntryEditControl = this;
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl.callBaseMethod(this, 'initialize');
        if (this._fieldControlIds)
            this._fieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._fieldControlIds);
        if (this._requireDataItemFieldControlIds)
            this._requireDataItemFieldControlIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._requireDataItemFieldControlIds);
        if (this._blankDataItem) {
            this._blankDataItem = Sys.Serialization.JavaScriptSerializer.deserialize(this._blankDataItem);
            if (this._blankDataItem.hasOwnProperty('Id')) {
                delete this._blankDataItem.Id;
            }
        }
        this._dataBindSuccessDelegate = Function.createDelegate(this, this._dataBindSuccess);
        this._submitButtonClickDelegate = Function.createDelegate(this, this._submitClickHandler);
        this._cancelLinkClickDelegate = Function.createDelegate(this, this._cancelClickHandler);
        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._itemSavedDelegate = Function.createDelegate(this, this._itemSavedHandler);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl.callBaseMethod(this, 'dispose');
        if (this._dataBindSuccessDelegate) {
            delete this._dataBindSuccessDelegate;
        }
        if (this._submitButtonClickDelegate) {
            delete this._submitButtonClickDelegate;
        }
        if (this._cancelLinkClickDelegate) {
            delete this._cancelLinkClickDelegate;
        }
        if (this._handlePageLoad) {
            delete this._handlePageLoad;
        }
        if (this._itemSavedDelegate) {
            delete this._itemSavedDelegate;
        }
    },

    /* --------------------------------- public methods ---------------------------------- */
    createForm: function (commandName, dataItem, self, dialog, params, key) {
        this._binder.reset();

        if (params["siteId"]) {
            this._siteId = params["siteId"];
        }
        if (params["siteName"]) {
            this._siteName = params["siteName"];
        }

        if (dataItem) {
            this.dataBind(dataItem, { "Id": key });
            this._isNew = false;
        }
        else {
            this._setupCreateMode();
            this._isNew = true;
        }
    },

    reset: function () {
        this._binder.reset();
    },

    dataBind: function (dataItem) {
        if (dataItem) {
            var clientManager = this._binder.get_manager();
            clientManager.InvokeGet(this._binder.get_serviceBaseUrl(), null, [dataItem.Id], this._dataBindSuccessDelegate, this._dataBindFailure, this);
        }
        else {
            this._binder.reset();
        }
    },

    save: function () {
        this._binder.SaveChanges();
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _dataBindFailure: function (sender, result) {
        alert(result.Detail);
    },

    _dataBindSuccess: function (sender, result) {
        this._binder.DataBind(result, { "Id": result.Item.Id });
    },

    _handlePageLoad: function () {
        this._binder.set_fieldControlIds(this._fieldControlIds);
        this._binder.set_requireDataItemControlIds(this._requireDataItemFieldControlIds);
        this._binder.add_onSaved(this._itemSavedDelegate);
        jQuery(this._submitButton).click(this._submitButtonClickDelegate);
        jQuery(this._cancelLink).click(this._cancelLinkClickDelegate);
    },

    _submitClickHandler: function (sender, eventArgs) {
        if (Telerik.Sitefinity.Web.UI.Fields.FormManager.validateAll()) {
            this.save();
            this.reset();
        }
    },
    //TODO: attach this to the cancel button.
    _cancelClickHandler: function (sender, eventArgs) {
        this.reset();
        dialogBase.close();
    },

    /* --------------------------------- private methods --------------------------------- */
    //setups the binding of the form in create mode
    _setupCreateMode: function (itemContext) {
        var newItemContext = new Object();
        if (itemContext) {
            newItemContext = itemContext;
        }
        else {
            if (this._siteId) {
                this._blankDataItem["SourceSiteId"] = this._siteId
            }
            if (this._siteName) {
                this._blankDataItem["SourceSiteDisplayName"] = this._siteName;
            }
            newItemContext['Item'] = this._blankDataItem;
        }
        this._binder.BindItem(newItemContext);
    },

    _itemSavedHandler: function (sender, args) {
        var dataItem = this._binder.get_dataItem();
        if (this._isNew) {
            dialogBase.closeCreated(dataItem);
        }
        else {
            dialogBase.closeUpdated(dataItem);
        }
    },

    /* --------------------------------- properties -------------------------------------- */
    get_fieldControlIds: function () {
        return this._fieldControlIds;
    },
    set_fieldControlIds: function (value) {
        this._fieldControlIds = value;
    },
    get_binder: function () {
        return this._binder;
    },
    set_binder: function (value) {
        this._binder = value;
    },
    get_submitButton: function () {
        return this._submitButton;
    },
    set_submitButton: function (value) {
        this._submitButton = value;
    },
    get_cancelLink: function () {
        return this._cancelLink;
    },
    set_cancelLink: function (value) {
        this._cancelLink = value;
    },
    get_requireDataItemFieldControlIds: function () {
        return this._requireDataItemFieldControlIds;
    },
    set_requireDataItemFieldControlIds: function (value) {
        this._requireDataItemFieldControlIds = value;
    }
};

Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControl', Sys.UI.Control);
