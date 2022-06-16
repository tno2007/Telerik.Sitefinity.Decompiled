Type.registerNamespace("Telerik.Sitefinity.Publishing.Twitter");

var urlShortDialog;

Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog = function (element) {
    Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog.initializeBase(this, [element]);
    this._acc = {};
    this._dataFieldNameControlIdMap = null;
    this._clientManager = null;
    this._manageLanguagesList = null;
    this._saveButton = null;
    this._serviceUrl = null;
    this._saveClientSelectionDelegate = null;
};

Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog.prototype = {

    initialize: function () {

        urlShortDialog = this;

        Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog.callBaseMethod(this, "initialize");

        this._saveClientSelectionDelegate = Function.createDelegate(this, this._saveClientSelectionHandler);

        $(this._saveButton).bind("click", this._saveClientSelectionDelegate);
    },

    intializeDialog: function (dataItem) {

        this._acc["AccountName"] = dataItem.AccountName;
        this._acc["ProviderName"] = dataItem.ProviderName;
        this._acc["ShortenningServiceUrl"] = dataItem.ShortenningServiceUrl;
        this._acc["ApiKey"] = dataItem.ApiKey;

        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            if (cnt != null) cnt.set_value(this._acc[dataFieldName]);
        }
    },

    // tear down
    dispose: function () {

        $(this._saveButton).unbind("click", this._saveClientSelectionDelegate);
        
        this._saveClientSelectionDelegate = null;

        Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog.callBaseMethod(this, "dispose");
    },
    validate: function () {
        var isValid = true;

        for (var dataFieldName in this._dataFieldNameControlIdMap) {

            var cnt = this._getFieldControl(dataFieldName);  
            isValid = isValid && cnt.validate();    
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
            this._acc[dataFieldName] = cnt.get_value();
        }

        var urlParams = [];
        urlParams["shortServiceConfig"] = "twitter";

        itemContext = new Object();
        itemContext.Item = this._acc;
        this._getClientManager().InvokePut(this._serviceUrl, null, urlParams, itemContext, this._saveChangesSuccess, this._saveChangesFailure, this);

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
    set_acc: function (value) {
        this._acc = value;
    },
    get_acc: function () {
        return this._acc;
    },
    set_serviceUrl: function (value) {
        this._serviceUrl = value;
    },
    get_serviceUrl: function () {
        return this._serviceUrl;
    }
};

Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog.registerClass('Telerik.Sitefinity.Publishing.Twitter.TwitterUrlShortConfigDialog', Telerik.Sitefinity.Web.UI.AjaxDialogBase);