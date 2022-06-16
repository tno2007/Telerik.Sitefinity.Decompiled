Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");
var pageViewVersinDialog ;

function GetViewVersionDialog(){
    if(pageViewVersinDialog == null)
        pageViewVersinDialog = new Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog();
    return pageViewVersinDialog;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog = function () {

    this._pageServiceUrl = null;
    this._versionServiceUrl = null;
    this._dataItem = null;
    this._pageInfo = null;
    this._uiCulture = null;
    this._clientManager = null;
    this._versionPageUrl = null;
    this._clientLabelManager = null;
    this._messageControl = null;
    this._versionUrlQueryString = null;
    this._revertPrompt = null;
    this._isAutoBind = false;
    this._pageVersionRevert = null;
    this._deleteCurrentVersion = null;

    this._beforeRevertVersionDelegate = null;
    Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog.initializeBase(this);
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog.callBaseMethod(this, "initialize");
        pageViewVersinDialog = this;
        this._beforeRevertVersionDelegate = Function.createDelegate(this, this._beforeRevertVersion);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog.callBaseMethod(this, "dispose");
        pageViewVersinDialog = null;
    },
    /* ==================== Methods  ============ */

    LoadItem: function (pageId, versionId, dataItem, uiCulture) {
        var clientManager = this.get_clientManager();
        if (uiCulture) {
            this._uiCulture = uiCulture;
            this._setManagerUICulture();
        }
        var serviceUrl = this.get_pageServiceUrl() + "versions/";
        if (dataItem != null)
            this._pageInfo = dataItem;
        //        if (pageId.constructor.toString().match(/array/i))
        //            itemId = pageId['Id'];
        //        else
        //            itemId = pageId;

        var urlParams = [];
        urlParams['itemId'] = pageId;
        urlParams['versionId'] = versionId + '';

        var success = Function.createDelegate(this, this._successfulLoadItem);
        var failure = Function.createDelegate(this, this._operationFailure);

        clientManager.InvokeGet(serviceUrl, urlParams, null, success, failure);

        return null;
    },

    NavigatePrev: function () {
        var version = this.get_currentItem().VersionInfo;
        if (version.PreviousId != "" && version.PreviousId != "undefined") {
            this.LoadItem(version.ItemId, version.PreviousId);
        }
    },
    NavigateNext: function () {
        var version = this.get_currentItem().VersionInfo;
        if (version.NextId != "" && version.NextId != "undefined") {
            this.LoadItem(version.ItemId, version.NextId);
        }
    },

    CopyAsNewDraft: function () {
        if (this.get_currentItem().HasConflict)
            this.get_revertPrompt().show_prompt(null, null, this._beforeRevertVersionDelegate);
        else
            this._copyAsNewDraftInternal();
    },

_copyAsNewDraftInternal: function() {
        var serviceUrl = this.get_pageServiceUrl() + "versions/copyversiontopage/";
        var urlParams = [];
        urlParams["itemId"] = this.get_currentItem().VersionInfo.ItemId;
        urlParams["versionId"] = this.get_currentItem().VersionInfo.Id + '';

        var success = Function.createDelegate(this, this._successfulCopyAsNew);
        var failure = Function.createDelegate(this, this._operationFailure);

        this.get_clientManager().InvokePut(serviceUrl, urlParams, null, this.get_currentItem(), success, failure);
    },

    //      this.get_messageControl().showPositiveMessage(this.get_clientLabelManager().getLabel("PageResources", "HomepageSuccessfullySetMessage"));
    DeleteCurrentVersion: function () {
        var answer = confirm(this.get_clientLabelManager().getLabel("PageResources", "DeleteDraftMessageConfirm"));
        if (answer) {
            var serviceUrl = this.get_versionServiceUrl() + this.get_currentItem().VersionInfo.Id + '/';
            var urlParams = [];
            var success = Function.createDelegate(this, this._successfulDeleteVersion);
            var failure = Function.createDelegate(this, this._operationFailure);
            this.get_clientManager().InvokeDelete(serviceUrl, urlParams, null, success, failure);
            
            if (this._isAutoBind) {
                this._deleteCurrentVersion();
            }
        }
    },

    _successfulDeleteVersion: function () {

        //        this.get_messageControl().showPositiveMessage(this.get_clientLabelManager().getLabel("PageResources", "DeleteDraftSuccessfull"));
        //        if (this.get_currentItem().VersionInfo.NextId != "")
        //            this.NavigateNext();
        //        else {
        //            if (this.get_currentItem().VersionInfo.PreviousId != "")
        //                this.NavigatePrev();
        //            else {
        dialogBase.close("deleteHistoryItem");
        //            }
        //        }
    },
    _successfulCopyAsNew: function () {
        var topWindow = window.top;

        if (this._pageInfo.PageEditUrl) {
            if (this._pageInfo.LocalizationStrategy == 2 && this._pageInfo.PageEditLanguageUrls) {
                for (var i = 0; i < this._pageInfo.PageEditLanguageUrls.length; i++) {
                    var currentItem = this._pageInfo.PageEditLanguageUrls[i];
                    if (currentItem.Key == this.get_clientManager().get_uiCulture()) {
                        topWindow.location = currentItem.Value;
                        return;
                    }
                }
            }
            var editUrl = this._pageInfo.PageEditUrl;
            topWindow.location = editUrl;
        }
        else if (this._pageInfo.EditUrl) {
            if (this._uiCulture) {
                topWindow.location = this._pageInfo.EditUrl + "/" + this._uiCulture;
            }
            else {
                topWindow.location = this._pageInfo.EditUrl;
            }
        }
        else {
            dialogBase.close("CopyAsNew");
            if (this._isAutoBind) {
                this._pageVersionRevert();
            } else {
                topWindow.zoneEditorShared.set_isPageRefreshControlled(true);
                topWindow.location.reload();
            }

        }
    },

    _successfulLoadItem: function (sender, result) {
        this._dataItem = result;
        bind();
    },

    _operationFailure: function (e) {
        alert(e.Detail);
    },

    _setManagerUICulture: function () {
        var clientManager = this.get_clientManager();
        if (this._uiCulture && !clientManager.get_uiCulture())
            clientManager.set_uiCulture(this._uiCulture);
    },

    _beforeRevertVersion: function (sender, args) {
        var command = args.get_commandName();
        if (command == 'revert')
            this._copyAsNewDraftInternal();
    },
    
    /* ==================== End Methods  ============ */

    /* ==================== Get/Set Members ============ */

    get_versionPageUrl: function () {
        return this._versionPageUrl;
    },
    set_versionPageUrl: function (value) {
        if (this._versionPageUrl != value)
            this._versionPageUrl = value;
    },
    get_currentItem: function () {
        return this._dataItem;
    },
    set_currentItem: function (value) {
        if (this._dataItem != value)
            this._dataItem = value;
    },
    get_clientManager: function () {
        if (this._clientManager == null)
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        return this._clientManager;
    },

    /* Page Service URL */
    get_pageServiceUrl: function () {
        return this._pageServiceUrl;
    },
    set_pageServiceUrl: function (value) {
        if (this._pageServiceUrl != value) {
            this._pageServiceUrl = value;
        }
    },

    get_versionServiceUrl: function () {
        return this._versionServiceUrl;
    },
    set_versionServiceUrl: function (value) {
        if (this._versionServiceUrl != value)
            this._versionServiceUrl = value;
    },
    /* Data Item  */
    get_dataItem: function () {
        return this._dataItem;
    },
    set_dataItem: function (value) {
        if (this._dataItem != value) {
            this._dataItem = value;
        }
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        if (this._messageControl != value)
            this._messageControl = value;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },
    get_versionUrlQueryString: function () {
        return this._versionUrlQueryString;
    },
    set_versionUrlQueryString: function (value) {
        this._versionUrlQueryString = value;
    },

    get_revertPrompt: function () {
        return this._revertPrompt;
    },
    set_revertPrompt: function (value) {
        this._revertPrompt = value;
    }

    /* ==================== End Get/Set Members ============ */

};


Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.PageViewVersionDialog', Sys.Component);
