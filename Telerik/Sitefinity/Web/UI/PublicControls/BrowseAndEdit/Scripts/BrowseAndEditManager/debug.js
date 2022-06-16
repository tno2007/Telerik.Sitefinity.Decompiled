Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit");

//Global variable storing the current instance of the BrowseAndEditManager.
Telerik.Sitefinity.BrowseAndEditManager = null;
function GetBrowseAndEditManager() {
    return Telerik.Sitefinity.BrowseAndEditManager;
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager.initializeBase(this, [element]);

    this._toolbarIds = [];
    this._toolbars = null;
    this._workflowServiceUrl = null;
    this._isMultilingual = false;
    this._uiCulture = null;
    this._pageUrl = null;
    this._browseAndEditCookieName = null;
    this._loadDelegate = null;
    this._toolbarCommandDelegate = null;
    this._dialogClosedDelegate = null;
    this._messageWorkflowSuccessDelegate = null;
    this._messageWorkflowFailureDelegate = null;
    this._checkPageSuccessDelegate = null;
    this._checkPageFailureDelegate = null;
    this._dialogBeforeShowDelegate = null;
    this._toolbarArgs = null;
    this._pageChangedWarningDialog = null;

    this._pageVersion = null;
    this._pageStatus = null;
    this._pageId = null;
    this._pagesServiceUrl = "";

    Telerik.Sitefinity.BrowseAndEditManager = this;
}

Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager.callBaseMethod(this, 'initialize');
        this._loadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        Sys.Application.add_load(this._loadDelegate);

        this._messageWorkflowFailureDelegate = Function.createDelegate(this, this._messageWorkflow_Failure);
        this._messageWorkflowSuccessDelegate = Function.createDelegate(this, this._messageWorkflow_Success);

        this._checkPageSuccessDelegate = Function.createDelegate(this, this._checkPageSuccessHandler);
        this._checkPageFailureDelegate = Function.createDelegate(this, this._checkPageFailureHandler);

        this._toolbarCommandDelegate = Function.createDelegate(this, this._toolbarCommandHandler);
        this._dialogBeforeShowDelegate = Function.createDelegate(this, this._dialogBeforeShowHandler);

    },
    dispose: function () {
        if (this._loadDelegate) {
            Sys.Application.remove_load(this._loadDelegate);
            delete this._loadDelegate;
        }
        if (this._toolbarCommandDelegate) {
            this._unregisterToolbars();
            delete this._toolbarCommandDelegate;
        }
        if (this._bindWorkflowVisualsFailureDelegate) {
            delete this._bindWorkflowVisualsFailureDelegate;
        }
        if (this._bindWorkflowVisualsSuccessDelegate) {
            delete this._bindWorkflowVisualsSuccessDelegate;
        }
        var dialogManager = GetDialogManager();
        if (this._dialogBeforeShowDelegate) {
            if (dialogManager) {
                dialogManager.remove_dialogBeforeShow(this._dialogBeforeShowDelegate);
            }
            delete this._dialogBeforeShowDelegate;
        }
        if (this._dialogClosedDelegate) {
            if (dialogManager) {
                dialogManager.remove_dialogClosed(this._dialogClosedDelegate);
            }
            delete this._dialogClosedDelegate;
        }

        Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    toggleBrowseAndEdit: function (display) {
        if (!this._toolbars || this._toolbars.length == 0)
            return;
        var toolbar, i, l;
        if (typeof display === "undefined") {
            for (i = 0, l = this._toolbars.length; i < l; i++) {
                toolbar = this._toolbars[i];
                if (typeof toolbar.get_alwaysVisible === "function" && !toolbar.get_alwaysVisible()) {
                    display = !toolbar.get_visible();
                    break;
                }
            }
        }
        if (typeof display !== "undefined") {
            for (i = 0, l = this._toolbars.length; i < l; i++) {
                toolbar = this._toolbars[i];
                if (typeof toolbar.get_alwaysVisible === "function" && !toolbar.get_alwaysVisible())
                    toolbar.set_visible(display);
            }
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    _handlePageLoad: function (sender, args) {
        this._registerToolbars();
        //TODO should not subscribe for the closing event of all dialogs.
        //Expose functionality to subscribe for dialogs opened only by this component. (Maybe pass a callback to the openDialog function?)
        //Shows all browse and edit components if required (settings are stored with cookie)
        this._checkBrowseAndEditState();
        var dialogManager = GetDialogManager();
        dialogManager.add_dialogBeforeShow(this._dialogBeforeShowDelegate);
        dialogManager.add_dialogClosed(this._dialogClosedDelegate);
    },

    _toolbarCommandHandler: function (sender, args) {
        this._checkPageForChanges({ "Sender": sender, "Args": args });
    },

    _executeToolbarCommand: function (sender, args) {
        var dialogManager = GetDialogManager();
        var commandName = args.get_commandName();
        var dialogName = args.get_dialogName ? args.get_dialogName() : null;
        var parentId = args.get_parentId ? args.get_parentId() : null;
        dialogManager.set_windowNameGenerator(null);
        var dialog;
        if (dialogName)
            dialog = dialogManager.getDialogByName(dialogName);

        if (dialog) {
            this._toolbarArgs = args;
            var item = null, key = null;
            var params = { IsEditable: true };
            var itemId = args.get_itemId();
            if (itemId) {
                item = { Id: itemId };
                key = { Id: itemId };
                if (parentId)
                    item.ParentId = parentId;
            }
            if (parentId)
                params.parentId = parentId;

            var itemsList = new Object();
            itemsList.getBinder = function () {
                var binder = new Object();
                binder.get_provider = function () {
                    return args.get_providerName();
                }
                return binder;
            };

            var dialogContext = this._getDialogContext(commandName, item, itemsList, dialog, params, key, { languageMode: commandName, language: this._uiCulture }, args.get_additionalProperties());
            //always maximizes the dialogs for editing. fixes a problem in ie with incorrect position of the dialogs
            var settings = { };
            if (dialog._name == "PropertyEditor") {
                dialog.set_initialBehaviors(Telerik.Web.UI.WindowBehaviors.None);
                dialog.set_width(425);
                dialog.set_height(250);
                dialog.set_modal(true);
                dialog._cssClass = "";
                settings = { maximize: false };
            } else {
                dialog.set_initialBehaviors(Telerik.Web.UI.WindowBehaviors.Maximize);
                dialog.set_height(100);
                dialog.set_width(100);
                dialog.set_modal(false);
                dialog._cssClass = "sfMaximizedWindow";
                settings = { maximize: true };
            }
            dialogManager.openDialog(dialogName, this, dialogContext, settings);

            var dialogPrefix = args.get_additionalProperties().dialogPrefix;
            if (dialogPrefix) {
                var windowNameGenerator = function (name) {
                    return dialogPrefix + name;
                }
                dialogManager.set_windowNameGenerator(windowNameGenerator);
            }
        }
        else {
            this._handleNondialogCommand(commandName, args);
        }
    },


    //TODO remove when createDialog() arguments refactoring is done;
    _getDialogContext: function (commandName, dataItem, itemsList, dialog, params, key, commandArgument, additionalProperties) {
        if (additionalProperties) {
            //Merging the additionalProperties in the commandArgument object.
            commandArgument = jQuery.extend(true, commandArgument, additionalProperties);
        }
        return { commandName: commandName, dataItem: dataItem, itemsList: itemsList, dialog: dialog, params: params, key: key, commandArgument: commandArgument };
    },

    _dialogClosedHandler: function (sender, args) {
        var dialogArgument = args.get_originalDialogEventArgument().get_argument();
        if (dialogArgument != null && (dialogArgument === "rebind" || dialogArgument === "reload" || dialogArgument.DataItem))
            this._reloadPage();
    },
    // Callback function called if the workflow service successfully complete workflow operation
    _messageWorkflow_Success: function (caller, result, request, context) {
        //this._afterRequest();
        if (context.url) {
            this._loadPage(context.url);
        }
        this._reloadPage();
    },

    // Callback function called if the workflow service failed to return workflow visuals
    _messageWorkflow_Failure: function (sender, result) {
        //this._afterRequest();
        alert('Failed workflow operation');
    },
    /* --------------------------------- private methods --------------------------------- */

    _checkBrowseAndEditState: function () {
        browseAndEditState = jQuery.cookie(this._browseAndEditCookieName);
        browseAndEditEnabled = false;
        if (browseAndEditState != null && browseAndEditState != "") {
            browseAndEditEnabled = (browseAndEditState == 'enabled' ? true : false);
        }
        if (browseAndEditEnabled) {
            // Now toogle the browse and edit mode and show all hidden toolbars.
            this.toggleBrowseAndEdit(true);
        }
    },

    _registerToolbars: function () {
        this._toolbars = [];
        var toolbar;
        for (var i = 0, l = this._toolbarIds.length; i < l; i++) {
            toolbar = $find(this._toolbarIds[i]);
            if (toolbar) {
                this._toolbars.push(toolbar);
                toolbar.add_command(this._toolbarCommandDelegate);
            }
        }
    },

    _unregisterToolbars: function () {
        if (this._toolbars && this._toolbars.length > 0) {
            for (var i = 0, l = this._toolbars.length; i < l; i++) {
                this._toolbars[i].remove_command(this._toolbarCommandDelegate);
            }
        }
    },
    _reloadPage: function () {
        var url = location.href;
        if (location.hash)
            url = url.substr(0, url.indexOf("#"));
        location.href = url;
    },
    _loadPage: function (url) {
        location.href = url;
    },
    _handleNondialogCommand: function (commandName, args) {
        switch (commandName) {
            case "toggleBrowseAndEdit":
                this.toggleBrowseAndEdit();
                break;
            case "delete":
                this._sendMessageWorkflow("Delete", args, { url: this._pageUrl });
                break;
            case "unpublish":
                this._sendMessageWorkflow("Unpublish", args, { url: this._pageUrl });
                break;
        }
    },

    _sendMessageWorkflow: function (workflowOperation, args, context) {
        var clientManager = this.get_clientManager();
        var urlParams = [];
        urlParams["itemType"] = args.get_itemType();
        urlParams["providerName"] = args.get_providerName();
        urlParams["workflowOperation"] = workflowOperation;
        var keys = ["MessageWorkflow", args.get_itemId()];
        var contextBag = [];
        clientManager.InvokePut(this._workflowServiceUrl, urlParams, keys, contextBag, this._messageWorkflowSuccessDelegate, this._messageWorkflowFailureDelegate, this, false, null, context);
    },

    _checkPageForChanges: function (context) {
        var clientManager = this.get_clientManager();
        var urlParams = [];
        urlParams["pageStatus"] = this._pageStatus;
        urlParams["pageVersion"] = this._pageVersion;
        urlParams["provider"] = null;

        var keys = ["CheckPageForChanges", this._pageId];
        clientManager.InvokeGet(this._pagesServiceUrl, urlParams, keys, this._checkPageSuccessDelegate, this._checkPageFailureDelegate, context);
    },

    //TODO refactor this.
    _calculateUrl: function (dialog, args) {
        if (!args.get_urlParameters)
            return;
        var paramsArray = args.get_urlParameters();
        if (paramsArray && paramsArray.length > 0) {
            var url = new Sys.Uri(dialog.get_navigateUrl());
            paramsLength = paramsArray.length;
            for (var i = 0; i < paramsLength; i++) {
                var paramKeyValuePair = paramsArray[i];
                if (("Key" in paramKeyValuePair) && ("Value" in paramKeyValuePair))
                    url.get_query()[paramKeyValuePair.Key] = paramKeyValuePair.Value;
            }
            dialog.set_navigateUrl(url.toString());
        }
    },

    _dialogBeforeShowHandler: function (sender, args) {
        var dialog = args.get_dialog();
        this._calculateUrl(dialog, this._toolbarArgs);
    },

    _checkPageSuccessHandler: function (context, args) {
        switch (args.ItemState) {
            case 0: //Ready
                this._executeToolbarCommand(context.Sender, context.Args);
                break;
            case 1: //Locked
            case 2: //Deleted
            case 3: //Updated                
                this.get_pageChangedWarningDialog().show_prompt(null, args.Message);
                break;
        }

    },

    _checkPageFailureHandler: function (sender, args) {
        alert(args.Detail);
    },

    /* --------------------------------- properties -------------------------------------- */

    get_clientManager: function () {
        if (this._clientManager == null) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this._isMultilingual) {
                this._clientManager.set_uiCulture(this._uiCulture);
            }
        }
        return this._clientManager;
    },
    get_pageChangedWarningDialog: function () { return this._pageChangedWarningDialog; },
    set_pageChangedWarningDialog: function (value) { this._pageChangedWarningDialog = value; },

    get_toolbarArgs: function () { return this._toolbarArgs; },

    get_toolbarIds: function () { return this._toolbarIds; },
    set_toolbarIds: function (value) { this._toolbarIds = value; },

    get_workflowServiceUrl: function () { return this._workflowServiceUrl; },
    set_workflowServiceUrl: function (value) { this._workflowServiceUrl = value; },

    get_isMultilingual: function () { return this._isMultilingual; },
    set_isMultilingual: function (value) { this._isMultilingual = value; },

    get_uiCulture: function () { return this._uiCulture; },
    set_uiCulture: function (value) { this._uiCulture = value; },

    get_pageUrl: function () { return this._pageUrl; },
    set_pageUrl: function (value) { this._pageUrl = value; },

    get_browseAndEditCookieName: function () { return this._browseAndEditCookieName; },
    set_browseAndEditCookieName: function (value) { this._browseAndEditCookieName = value; },

    get_pageVersion: function () { return this._pageVersion; },
    set_pageVersion: function (value) { this._pageVersion = value; },

    get_pageStatus: function () { return this._pageStatus; },
    set_pageStatus: function (value) { this._pageStatus = value; },

    get_pageId: function () { return this._pageId; },
    set_pageId: function (value) { this._pageId = value; },

    get_pagesServiceUrl: function () { return this._pagesServiceUrl; },
    set_pagesServiceUrl: function (value) { this._pagesServiceUrl = value; }
}
Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit.BrowseAndEditManager', Sys.UI.Control);

