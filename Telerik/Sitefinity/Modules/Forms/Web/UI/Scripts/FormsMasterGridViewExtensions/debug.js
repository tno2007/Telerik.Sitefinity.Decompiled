// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    var masterView = sender;
    masterView.get_binder().set_unescapeHtml(true);
    var extender = new Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension(masterView);
    extender.initialize();
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI");

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension = function (masterView) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension.initializeBase(this);

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsGrid = {};

    // Event delegates
    this._masterCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._dialogClosedDelegate = null;

    this._serviceSuccessDelegate = null;
    this._serviceFailureDelegate = null;

    this._actionCommandPrefix = "sf_binderCommand_";

    this._clientManager = null;
    this._translationHandlers = null;
    this._editTranslationDelegate = null;
    this._createTranslationDelegate = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension.callBaseMethod(this, 'initialize');

        this._masterCommandDelegate = Function.createDelegate(this, this._masterCommandHandler);
        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBoundHandler);
        this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        this._serviceSuccessDelegate = Function.createDelegate(this, this._serviceSuccess);
        this._serviceFailureDelegate = Function.createDelegate(this, this._serviceFailure);
        this._itemsGrid = this._masterView.get_itemsGrid();
        this._editTranslationDelegate = Function.createDelegate(this, this._editTranslationHandler);
        this._createTranslationDelegate = Function.createDelegate(this, this._createTranslationHandler);
        this._translationHandlers = { create: this._createTranslationDelegate, edit: this._editTranslationDelegate };
        var notTranslatedItemsToHide = ".sfView,.sfShort,.sfMoreActions,.sfDateAuthor"; //hides "View", "Responses", "More actions" and "Date/Author" columns

        if (this._itemsGrid) {
            this._itemsGrid.add_command(this._masterCommandDelegate);
            this._itemsGrid.add_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsGrid.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(this._translationHandlers);
            this._itemsGrid.set_itemsToHideSelector(notTranslatedItemsToHide);
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension.callBaseMethod(this, 'dispose');

        if (this._itemsGrid) {
            this._itemsGrid.remove_command(this._masterCommandDelegate);
            this._itemsGrid.remove_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.remove_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(null);
        }

        delete this._masterCommandDelegate;
        delete this._itemCommandDelegate;
        delete this._dialogClosedDelegate;
        if (this._serviceSuccessDelegate) {
            delete this._serviceSuccessDelegate;
        }
        if (this._serviceFailureDelegate) {
            delete this._serviceFailureDelegate;
        }
        delete this._translationHandlers;
        delete this._editTranslationDelegate;
        delete this._createTranslationDelegate;
    },
    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // handles commands fired from the master view
    _masterCommandHandler: function (sender, args) {
        var currentList = this._masterView.get_currentItemsList();
        var binder = currentList.getBinder();

        switch (args.get_commandName()) {
            case 'batchPublishPage':
                this._batchPublishForm();
                break;
            case 'batchUnpublishPage':
                this._batchUnpublishForm();
                break;
            default:
                break;
        }
    },

    // handles the commands fired by a single item
    _itemCommandHandler: function (sender, args) {
        var form = args.get_commandArgument();
        var formId = args.get_commandArgument().Id;
        var currentList = this._masterView.get_currentItemsList();
        var binder = currentList.getBinder();

        switch (args.get_commandName()) {
            case 'editFormContent':
                var editUrl = form.EditUrl;
                if (editUrl) {
                    var uiCulture = currentList.get_uiCulture();
                    if (uiCulture)
                        editUrl += "/" + uiCulture;
                    window.location = editUrl;
                }
                break;
            case 'publishForm':
                this._publishDraft(sender, formId, formId);
                break;
            case 'unpublishForm':
                this._unpublishForm(sender, formId, formId);
                break;
            case 'unlockForm':
                var dataItem = args.get_commandArgument();
                args.get_commandArgument().LockedByUsername = dataItem.LifecycleStatus.LockedByUsername;
                break;
            case 'subscribe':
                this._subscribeForm(sender, formId, formId);
                break;
            case 'unsubscribe':
                this._unsubscribeForm(sender, formId, formId);
                break;
            default:
                break;
        }
    },

    _editTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        window.location = args.get_dataItem().EditUrl + "/" + args.get_language();
    },

    _createTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        var list = args.get_list();
        var item = args.get_dataItem();
        list.executeItemCommand("create", item, args.get_key(), { language: args.get_language(), languageMode: args.get_commandName(), sourceObjectId: item.Id, mode: "edit" });
    },

    // Fired when item is bound
    _itemDataBoundHandler: function (sender, args) {
        var dataItem = args.get_dataItem();

        var itemElement = args.get_itemElement();
        this._configureMoreActionsMenu(dataItem, itemElement);

        if (dataItem.Status == "Draft" || (dataItem.Status == "Hidden" && dataItem.EntriesCount == 0)) {
            var anchorElement = jQuery(itemElement).find("a.sfResponsesCount");
            anchorElement.attr("href", "javascript:void(0)");
        }
    },

    // Fires when a dialog is closed
    _dialogClosedHandler: function (sender, args) {
        if (Telerik.Sitefinity.DialogClosedEventArgs.isInstanceOfType(args)) {
            var context = args.get_context();
            if (args.get_isCreated && args.get_isCreated()
                && context && context.get_widgetCommandName
                && context.get_widgetCommandName() == 'create') {
                this._navigateToEditForm(sender, args);
            }
        }
        else {
            var itemsGrid = this._masterView.get_currentItemsList();
            var binder = itemsGrid.getBinder();
            var windowName = sender.get_name();
            switch (windowName) {
                case "unlockForm":
                    //If the window is an unlockForm window - check if the unlock button was clicked 
                    if (args.Action && args.Action == 'unlock') {
                        //If yes - refresh grid (ToDo: better refresh just unlocked item?)
                        binder.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
    },

    _navigateToEditForm: function (sender, args) {
        $('body').addClass('sfLoadingTransition');

        var dataItem = args.get_dataItem();
        var language = args.get_context().get_binder().get_uiCulture();
        var dialog = args.get_context();
        if (dialog) {
            var commandArgument = dialog.get_commandArgument();
            // Take the language from the arguments whit which the dialog was created.
            if (commandArgument) {
                if (commandArgument.languageMode == "create") {
                    language = commandArgument.language;
                }
            }
        }
        if (dataItem != null && dataItem.hasOwnProperty('EditUrl')) {
            window.location.href = this._getLocalizedEditUrl(dataItem, language);
            args.set_cancel(true);
        }
    },

    _getLocalizedEditUrl: function (dataItem, language) {
        var url = dataItem.EditUrl;
        if (language) {
            url += "/" + language;
        }
        return url;
    },

    /* -------------------- private methods ----------- */

    _configureMoreActionsMenu: function (dataItem, containerElement) {
        var itemsList = this._masterView.get_currentItemsList();

        if (dataItem.Status && dataItem.Status == "Live") {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "publish"], containerElement);
        }

        if (!dataItem.IsEditable) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "editFormContent"], containerElement);
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "edit"], containerElement);
        }
        if (dataItem.LifecycleStatus && dataItem.LifecycleStatus.IsLocked == true && dataItem.IsUnlockable) {

        }
        else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unlockForm"], containerElement);
        }

        if (dataItem.Status && dataItem.Status == "Published") {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "publishForm"], containerElement);
        }
        else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unpublishForm"], containerElement);
        }

        if (dataItem.HasSubscription)
        {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "subscribe"], containerElement);
        }
        else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unsubscribe"], containerElement);
        }
    },

    // publish draft
    _publishDraft: function (itemsListBase, pageId, context) {
        this._showGridLoadingPanel();
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'publish/';
        var clientManager = this._getClientManager();
        clientManager.InvokePut(serviceUrl, null, null, pageId, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },

    // unpublish draft
    _unpublishForm: function (itemsListBase, pageId, context) {
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'unpublish/';
        var clientManager = this._getClientManager();
        clientManager.InvokePut(serviceUrl, null, null, pageId, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },

    // subscribe from
    _subscribeForm: function (itemsListBase, formId, context) {
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'subscribe/';
        var clientManager = this._getClientManager();
        clientManager.InvokePut(serviceUrl, null, null, formId, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },

    // unsubscribe from
    _unsubscribeForm: function (itemsListBase, formId, context) {
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'unsubscribe/';
        var clientManager = this._getClientManager();
        clientManager.InvokePut(serviceUrl, null, null, formId, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsListBase, null, null, context);
    },

    _batchPublishForm: function () {
        var itemsList = this._masterView.get_currentItemsList();
        var binder = itemsList.getBinder();
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var serviceUrl = this._getBaseUrl(binder);
            serviceUrl += 'batch/publish/';

            var urlParams = [];
            var keys = [];
            var data = [];
            var count = selectedItems.length;
            while (count--) {
                data.push(selectedItems[count].Id);
            }
            var clientManager = this._getClientManager();
            clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsList);
        }
    },

    _batchUnpublishForm: function () {
        var itemsList = this._masterView.get_currentItemsList();
        var binder = itemsList.getBinder();
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var serviceUrl = this._getBaseUrl(binder);
            serviceUrl += 'batch/unpublish/';

            var urlParams = [];
            var keys = [];
            var data = [];
            var count = selectedItems.length;
            while (count--) {
                data.push(selectedItems[count].Id);
            }
            var clientManager = this._getClientManager();
            clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccessDelegate, this._serviceFailureDelegate, itemsList);
        }
    },

    _serviceSuccess: function (caller, sender, args) {
        this._hideGridLoadingPanel();
        this._itemsGrid.dataBind();
    },

    _batchServiceSuccess: function (caller, sender, args) {
        var masterView = this.Context.MasterView;

        masterView._movedItemsCount++;
        if (masterView._movedItemsCount == masterView._itemsToMoveCount) {
            var key = this.Context.DataKey;
            this.Caller.dataBind(null, key);
            masterView._movedItemsCount = 0;
        }
    },

    _serviceFailure: function (sender, args) {
        this._hideGridLoadingPanel();
        alert(sender.Detail);
    },

    _isMultilingual: function () {
        var binder = this._masterView.get_currentItemsList().getBinder();
        return binder._isMultilingual;
    },

    _getCurrentLanguage: function () {
        var binder = this._masterView.get_currentItemsList().getBinder();
        return binder.get_uiCulture();
    },

    //TODO change the way the loading panel is constructed in the itemsGrid
    _hideGridLoadingPanel: function () {
        $('.RadAjax').hide();
    },

    _showGridLoadingPanel: function () {
        $('.RadAjax').show();
    },

    _getClientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }

        if (this._isMultilingual()) {
            this._clientManager.set_uiCulture(this._getCurrentLanguage());
        }

        return this._clientManager;
    },

    _getBaseUrl: function (itemsListBase) {
        var baseUrl = itemsListBase.get_serviceBaseUrl();
        var serviceUrl = baseUrl.toString().substr(0, baseUrl.toString().indexOf('?'));
        return serviceUrl;
    },
    _getZoneEditorServiceUrl: function (itemsListBase) {
        var baseUrl = this._getBaseUrl(itemsListBase);
        var serviceUrl = baseUrl.replace('PagesService.svc', 'ZoneEditorService.svc');
        return serviceUrl;
    }
    /* -------------------- properties ---------------- */

}

Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.FormsMasterGridViewExtension', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
