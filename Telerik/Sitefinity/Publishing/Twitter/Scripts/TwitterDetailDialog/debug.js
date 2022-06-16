Type.registerNamespace("Telerik.Sitefinity.Publishing.Twitter");

Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog = function (element) {
    Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog.initializeBase(this, [element]);
    this._app = {};
    this._dataFieldNameControlIdMap = null;
    this._clientManager = null;
    this._manageLanguagesList = null;
    this._saveButton = null;
    this._serviceUrl = null;
};

Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog.callBaseMethod(this, "initialize");
        if (this._loadDelegate == null) {
            this._loadDelegate = Function.createDelegate(this, this.loadDialogHandler);
        }

        Sys.Application.add_load(Function.createDelegate(this, function () {
            for (var dataFieldName in this._dataFieldNameControlIdMap) {
                var cnt = this._getFieldControl(dataFieldName);
                if (cnt != null) cnt.set_value(this._app[dataFieldName]);
            }
        }));

        $(this._saveButton).bind("click", Function.createDelegate(this, this._saveClientSelectionHandler));
    },
    // tear down
    dispose: function () {
        Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog.callBaseMethod(this, "dispose");


    },
    validate: function () {
        var isValid = true;

        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            //if (this.isEnabledElement(cnt.get_element())) {
            isValid = isValid && cnt.validate();
            //}
        }

        return isValid;
    },
    _getClientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }

        return this._clientManager;
    },
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataFieldNameControlIdMap[dataFieldName]);
    },
    // Expects as parameter an object that contains fields : PulicationDate and ExpirationDate

    _saveClientSelectionHandler: function () {
        if (!this.validate()) return;
        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            this._app[dataFieldName] = cnt.get_value();
        }

        itemContext = new Object();
        itemContext.Item = this._app;
        this._getClientManager().InvokePut(this._serviceUrl, null, null, itemContext, this._saveChangesSuccess, this._saveChangesFailure, this);

    },
    _saveChangesSuccess: function () {
        dialogBase.closeAndRebind();
    },
    _saveChangesFailure: function (sender, args) {
        alert(sender.Detail);
    },
    get_saveButton: function () {
        return this._saveButton;
    },
    set_saveButton: function (value) {
        this._saveButton = value;
    },
    set_dataFieldNameControlIdMap: function (value) {
        this._dataFieldNameControlIdMap = value;
    },
    get_dataFieldNameControlIdMap: function () {
        return this._dataFieldNameControlIdMap;
    },
    set_app: function (value) {
        this._app = value;
    },
    get_app: function () {
        return this._app;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    },
    loadDialogHandler: function () {
    }
};

Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog.registerClass('Telerik.Sitefinity.Publishing.Twitter.TwitterDetailDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);