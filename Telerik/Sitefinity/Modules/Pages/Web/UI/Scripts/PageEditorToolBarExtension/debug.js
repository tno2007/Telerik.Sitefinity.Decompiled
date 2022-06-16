// called by the EditorToolBar when it is loaded
function OnEditorToolBarLoaded(sender, args) {
    var toolBar = sender;
    var extender = new Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension(toolBar);
    extender.initialize();
    if(toolBar)
        toolBar.Extender = extender;
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension = function (toolBar) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension.initializeBase(this);

    // Main components
    this._toolBar = toolBar;
    this._clientManager = this._toolBar.get_clientManager();
    this._clientLabelManager = this._toolBar.get_clientLabelManager();

    this._pageId = this._toolBar.get_pageNodeId();
    this._page = null;

    this._cancelUrl = this._toolBar.get_cancelUrl();
    this._basePageServiceUrl = this._toolBar.get_baseItemServiceUrl();

    // Event delegates
    this._handleCommandDelegate = null;
    this._handleDialogClosedDelegate = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension.callBaseMethod(this, 'initialize');

        if (this._handleCommandDelegate == null) {
            this._handleCommandDelegate = Function.createDelegate(this, this._handleCommand);
        }
        this._toolBar.add_command(this._handleCommandDelegate);

        if (this._handleDialogClosedDelegate == null) {
            this._handleDialogClosedDelegate = Function.createDelegate(this, this._handleDialogClosed);
        }
        this._toolBar.add_dialogClosed(this._handleDialogClosedDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension.callBaseMethod(this, 'dispose');

        if (this._handleCommandDelegate) {
            this._toolBar.remove_command(this._handleCommandDelegate);
            delete this._handleCommandDelegate;
        }

        if (this._handleDialogClosedDelegate) {
            this._toolBar.remove_dialogClosed(this._handleDialogClosedDelegate);
            delete this._handleDialogClosedDelegate;
        }
    },

    /* --------------------  public methods ----------- */

    getPage: function (serviceSuccess) {
        var success = Function.createDelegate(this, serviceSuccess);
        var failure = Function.createDelegate(this, this._serviceFailure);
        var urlParams = [];
        urlParams["providerName"] = null;
        this._clientManager.InvokeGet(this._basePageServiceUrl, urlParams, [this._pageId], success, failure, this);
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _handleCommand: function (sender, args) {
        if (args.get_cancel() === false) {
            switch (args.get_commandName()) {
                case "edit":
                    this.getPage(this._openPropertiesDialogSuccess);
                    args.set_cancel(true);
                    break;
                default:
                    break;
            }
        }
    },

    _handleDialogClosed: function (sender, args) {
        if (args.get_cancel() === false && args.get_dialog()._sfArgs.get_cancel() === false) {
            var commandName = args.get_commandName();
            var dataItem = args.get_dataItem();
            switch (commandName) {
                case "edit":
                    //HACK: fix 78949 grid items rebind ('GET' requests) fail with status 0 at Chrome
                    //The most posible reason is that previous('PUT') request on save didnt completed it's callback.
                    // At this situation all next requests have status 0.
                    window.setTimeout(Function.createDelegate(this, function () { this.getPage(this._redirectSuccess) }), 0);
                    args.set_cancel(true);
                    break;
                default:
                    break;
            }
        }
    },

    /* -------------------- private methods ----------- */

    _openPropertiesDialogSuccess: function (caller, result) {
        caller._openDialog(caller, result, "edit", { HideTemplate: true });
    },

    _redirectSuccess: function (caller, result) {
        var page = result;
        caller._redirect(page.Item, caller._cancelUrl);
    },

    _serviceFailure: function (sender, args) {
        // when deleting a page from 'Title & Properties' dialog, an exception is thrown because in _handleDialogClosed we are trying to access item that no longer exists. 
        // In this specific case, I'm redirecting to Pages instead of alerting.
        var errorMsg = this._clientLabelManager.getLabel("ErrorMessages", "ItemNotFound");
        if (sender.Detail == errorMsg) {
            this._redirect(null, this._cancelUrl);
        }
        else {
            alert(sender.Detail);
        }
    },

    _redirect: function (dataItem, cancelUrl) {
        $('body').addClass('sfLoadingTransition');

        if (dataItem != null) {
            if ((dataItem.hasOwnProperty('IsGroup') && dataItem.IsGroup) ||
                (dataItem.hasOwnProperty('IsExternal') && dataItem.IsExternal)) {
                if (zoneEditorShared)
                    zoneEditorShared.set_isPageRefreshControlled(true);
                window.location.href = cancelUrl;
            }
            else {
                if (dataItem.hasOwnProperty('NavigateUrl')) {
                    var url = dataItem.NavigateUrl;
                    if (this._toolBar.get_currentLanguage()) {
                        url += "/" + this._toolBar.get_currentLanguage();
                    }
                    if (zoneEditorShared)
                        zoneEditorShared.set_isPageRefreshControlled(true);
                    window.location.href = url;
                }
            }
        }
        else {
            if (cancelUrl) {
                if (zoneEditorShared)
                    zoneEditorShared.set_isPageRefreshControlled(true);
                window.location.href = cancelUrl;
            }
            else {
                if (zoneEditorShared)
                    zoneEditorShared.set_isPageRefreshControlled(true);
                window.location.href = window.location.href;
            }
        }
    },

    _openDialog: function (caller, page, windowName, params) {
        if (!params) params = [];
        var key = [];
        key["Id"] = caller._pageId;
        caller._toolBar.openDialog(windowName, page.Item, params, key);
    }

    /* -------------------- properties ---------------- */
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.PageEditorToolBarExtension', Sys.Component);

