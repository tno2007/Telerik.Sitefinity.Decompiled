Type.registerNamespace("Telerik.Sitefinity.Web.UI.ItemLists");

// ------------------------------------------------------------------------
// Class ItemsListBase
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase = function (element) {
    /// <summary>Creates a new instance of ItemsListBase</summary>
    Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.initializeBase(this, [element]);
    this._element = element;
    window.__Telerik_Sitefinity_Web_UI_ItemLists_ItemsListBase_Instance = this;
    this._serviceBaseUrl = null;
    this._serviceCallUrl = null;
    this._binderId = null;
    this._binder = null;
    this._keys = null;
    this._manager = null;
    this._providerName = null;
    this._bindOnSuccess = true;
    this._radWindowManagerId = null;
    this._radWindowManager = null;
    this._dataItem = null;
    this._commandItems = null;
    this._deleteMultipleConfirmationMessage = null;
    this._deleteSingleConfirmationMessage = null;
    this._checkRelatingDataMessageSingle = null;
    this._checkRelatingDataMessageMultiple = null;
    this._recycleBinEnabled = null;
    this._sendToRecycleBinSingleConfirmationMessage = null;
    this._sendToRecycleBinMultipleConfirmationMessage = null;
    this._linkDescriptions = null;
    this._constantFilter = null;
    this._queryStringParts = null;
    this._dialogParameters = null;
    this._showCallback = null;
    this._loadCallback = null;
    this._dataItemProperty = null;
    this._formItemName = null;
    this._scrollOpenedDialogsToTop = null;
    this._bindOnLoad = null;
    this._promptWindowId = null;
    this._culture = null;
    this._uiCulture = null;
    this._managerType = null;
    this._translationHandlers = null;
    this._itemTitleSelector = ".sfItemTitle";
    this._itemsToHideSelector = ".sfMoreActions,.sfRegular,.sfDate, .sfViewStats"; //hides "More actions", "Author" and "Date" columns
    this._confirmDeleteMessage = null;
    this._deletedItems = [];
    this._recycleBinServiceUrl = null;
    this._recyclableContentItemType = null;

    this._promptDialogNamesJson = null;
    this._promptDialogCommandsJson = null;
    this._promptDialogNames = null;
    this._promptDialogCommands = null;
    this._promptDialogs = [];

    this._noEditPermissionsConfirmationMessage = null;
    this._noEditPermissionsViewDialogTitle = null;
    this._nonEditableItemToView = null;
    this._nonEditableItemKey = null;
    this._nonEditableItemElement = null;
    this._translationDelegate = null;
    this._backToLabel = null;
    this._titleText = null;
    this._reloadDialogs = false;
    this._supportsMultilingual = false;
    this._filterExpression = null;

    this._deleteItemsDelegate = null;
    this._itemsDeletedDelegate = null;
    this._dropDownChangeHandlerDelegate = null;

    //deletion messages
    this._youDoNotHaveThePermissionsToDeleteThisItem = null;
    this._youDoNotHaveThePermissionsToDeleteTheseItems = null;
    this._youDoNotHaveThePermissionsToDeleteSomeOfTheItems = null;
    //publish/unpublish aware messages
    this._publishAwareMessage = null;
    this._unPublishAwareMessage = null;

    this._blackListedWindows = null;
    this._contentLocationPreviewUrl = null;
    this._currentSiteId = null;

}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.prototype = {
    // ------------------------------------------------------------------------
    // initialization and clean-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.callBaseMethod(this, 'initialize');

        this._linkDescriptions = Sys.Serialization.JavaScriptSerializer.deserialize(this._linkDescriptions);
        this._dialogParameters = Sys.Serialization.JavaScriptSerializer.deserialize(this._dialogParameters);
        this._commandItems = Sys.Serialization.JavaScriptSerializer.deserialize(this._commandItems);
        this._keys = Sys.Serialization.JavaScriptSerializer.deserialize(this._keys);
        this._blackListedWindows = Sys.Serialization.JavaScriptSerializer.deserialize(this._blackListedWindows);
        this._manager = new Telerik.Sitefinity.Data.ClientManager();
        this._manager.set_culture(this._culture);
        this._manager.set_uiCulture(this._uiCulture);
        this._queryStringParts = this._splitQueryString(location.search);

        this._promptDialogNames = Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogNamesJson);
        this._promptDialogCommands = Sys.Serialization.JavaScriptSerializer.deserialize(this._promptDialogCommandsJson);

        if (this._serviceBaseUrl) {
            this._serviceCallUrl = this._replaceQueryStringValues(this._serviceBaseUrl);
        }

        if (this._constantFilter) {
            this._constantFilter = this._replaceQueryStringValues(this._constantFilter);
        }
        this._bindToolboxCommands();

        // delegates
        this._handleCommandDelegate = Function.createDelegate(this, this._handleCommand);
        this._handlePageLoadDelegate = Function.createDelegate(this, this._handlePageLoad);
        this._handleItemCommandDelegate = Function.createDelegate(this, this._handleItemCommand)
        this._handleBinderDataBindingDelegate = Function.createDelegate(this, this._handleBinderDataBinding)
        this._handleBinderDataBoundDelegate = Function.createDelegate(this, this._handleBinderDataBound);
        this._handleOperationSuccessDelegate = Function.createDelegate(this, this._handleOperationSuccess);
        this._handleDialogClosedDelegate = Function.createDelegate(this, this._handleDialogClosed);
        this._handleProvidersListChangeDelegate = Function.createDelegate(this, this._handleProvidersListChange);
        this._handleMenuItemClickedDelegate = Function.createDelegate(this, this._handleMenuItemClicked);
        this._handleOperationFailureDelegate = Function.createDelegate(this, this._handleOperationFailure);
        this._translationDelegate = Function.createDelegate(this, this._translationHandler);
        this._deleteItemsDelegate = Function.createDelegate(this, this._deleteItemsFinal);
        this._itemsDeletedDelegate = Function.createDelegate(this, this._itemsDeletedHandler);
        this._groupPublishDelegate = Function.createDelegate(this, this._groupPublishHandler);
        this._undoDeletedItemsDelegate = Function.createDelegate(this, this._undoDeletedItemsHandler);

        this.add_command(this._handleCommandDelegate);
        Sys.Application.add_load(this._handlePageLoadDelegate);
    },
    dispose: function () {
        // clean-up
        this.remove_command(this._handleCommandDelegate);
        Sys.Application.remove_load(this._handlePageLoadDelegate);
        if (this._binder) {
            this._binder.remove_onItemCommand(this._handleItemCommandDelegate);
            this._binder.remove_onItemDataBinding(this._handleBinderDataBindingDelegate);
            this._binder.remove_onDataBound(this._handleBinderDataBoundDelegate);
            this._binder.remove_onDeleted(this._handleOperationSuccessDelegate);
        }

        this._handleCommandDelegate = null;
        this._handlePageLoadDelegate = null;
        this._handleItemCommandDelegate = null;
        this._handleBinderDataBindingDelegate = null;
        this._handleBinderDataBoundDelegate = null;
        this._handleOperationSuccessDelegate = null;
        this._handleProvidersListChangeDelegate = null;
        this._handleMenuItemClickedDelegate = null;
        this._handleOperationFailureDelegate = null;
        this._undoDeletedItemsDelegate = null;
        delete this._translationDelegate;
        delete this._handleDialogClosedDelegate;
        delete this._itemsDeletedDelegate;

        Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // public functions
    // ------------------------------------------------------------------------
    dataBind: function (commandName, context) {
        /// <summary>Data binds the list.</summary>
        /// <param name="commandName">Optionally, the command that caused the data binding</param>
        /// <param name="context">User context to pass to the event handler</param>
        if (!this._onDataBinding(commandName).get_cancel()) {
            var filter = null;
            if (this._constantFilter) {
                filter = this._constantFilter;
            }
            if (this._filterExpression != null && this._filterExpression != "") {
                if (filter && filter != "") {
                    filter = "(" + filter + ") And (" + this._filterExpression + ")";
                }
                else {
                    filter = this._filterExpression;
                }
                this._binder.ClearPager();
                this._binder.set_isFiltering(true);
            }
            else if (!filter) {
                filter = this._filterExpression;
            }

            this._binder.set_filterExpression(filter);
            this._binder.DataBind(null, context);
        }
    },

    sort: function (sortExpression) {
        /// <summary>Sorts the list.</summary>
        this._binder.Sort(sortExpression);
    },

    applyFilter: function (filterExpression) {
        /// <summary>Applies a filter expression and data-binds the grid</summary>
        /// <param name="filterExpression">Filter expression to apply</param>
        /// <remarks>Applies the constant filter, if present.</remarks>

        this._filterExpression = filterExpression;
        if (this._bindOnSuccess) {
            this.dataBind();
        }
    },

    applySorting: function (sortExpression) {
        /// applies the sorting expression and binds the grid
        this._binder.set_sortExpression(sortExpression);
        if (this._bindOnSuccess) {
            this.dataBind();
        }
    },

    openDialog: function (commandName, dataItem, params, key, commandArgument) {
        /// <summary>Open a dialog and call a <c>createDialog</c> function on it, if it is present</summary>
        /// <param name="commandName">Command name that is causing the opening of this dialog, or the dialog's name</param>
        /// <param name="dataItem">The data item for which the command was fired.</param>
        /// <param name="params">Optional parameters to pass to the <c>createDialog</c> callback.</param>
        /// <remarks>
        /// The <c>createDialog</c> callback has the following signature:
        /// <c>createDialog(commandName, dataItem, itemsList, dialog, params)</c>
        /// </remarks>

        var dialog = this._radWindowManager.getWindowByName(commandName);

        if (dialog) {
            if (this._blackListedWindows.indexOf(commandName) > -1) {
                var dialogManager = window.top.GetDialogManager();
                if (dialogManager) {
                    dialogManager.blacklistWindow(dialog);
                }
            }

            dialog.set_skin("Default");
            dialog.set_showContentDuringLoad(false);

            // have to set dialog url here, because it's too late on show!
            var url = dialog.get_navigateUrl();
            if (this._dialogParameters[commandName]) {
                if (url.indexOf("?") != -1) {
                    url = url.substring(0, url.indexOf("?")) + this._dialogParameters[commandName];
                } else {
                    url = url + this._dialogParameters[commandName];
                }
            }

            if (dataItem) {
                url = this._replacePropertyValues(dataItem, url);
            }

            url = this._replaceQueryStringValues(url);

            if (this.get_uiCulture()) {
                url = Telerik.Sitefinity.setUrlParameter(url, "language", this.get_uiCulture());
            }
            if (this.get_providerName()) {
                url = Telerik.Sitefinity.setUrlParameter(url, "provider", this.get_providerName());
            }

            var currentMultisiteSelector = this._getCurrentMultisiteSelector();
            if (currentMultisiteSelector) {
                url = Telerik.Sitefinity.setUrlParameter(url, currentMultisiteSelector.get_siteIdParamKey(), currentMultisiteSelector.get_selectedSite());
            }

            dialog.set_navigateUrl(url);

            if (this._onDialogOpened(commandName, dataItem, this, dialog, params, key).get_cancel() == true) {
                // dialog opening was cancelled
                return;
            }

            // we extend the dialog object instead of using local variables
            // thus we are sure they are per-instance
            if (typeof (dialog._sfArgs) != "undefined") {
                delete dialog._sfArgs;
            }
            if (((params != null) && ((typeof (params["backLabelText"]) == "undefined") || (params["backLabelText"] == null))) && (this._titleText != null) && (this._titleText != "")) {
                params["backLabelText"] = this.getFormattedBackLabel(this._titleText);
            }
            dialog._sfArgs = new Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs(
                commandName, dataItem, this, dialog, params, key, commandArgument
                );

            if (!dialog._sfShowDialogExtension) {
                dialog._sfShowDialogExtension = this._showDialogExtension;
                dialog.add_show(dialog._sfShowDialogExtension);
            }
            //check if the the dialog is set to reload on each showing.
            //If that's the case - the _loadDialogExtension handler should be reattached.
            if (!dialog._sfLoadDialogExtension || dialog.get_reloadOnShow() || this._reloadDialogs) {
                dialog._sfLoadDialogExtension = this._loadDialogExtension;
                dialog.add_pageLoad(dialog._sfLoadDialogExtension);
            }
            if (!dialog._sfCloseDialogExtension) {
                dialog._sfCloseDialogExtension = this._closeDialogExtension;
                dialog.add_close(dialog._sfCloseDialogExtension);
            }

            if ((dialog.get_width() == 100 && dialog.get_height() == 100) || (dialog._sfShouldMaximize)) {
                $("body").addClass("sfLoadingTransition");
                dialog._sfShouldMaximize = true;
            }

            dialog.show();
            Telerik.Sitefinity.centerWindowHorizontally(dialog);
            // Ivan's note: RadWindow does not pass the units here, so we'll assume
            // that 100 x 100 is percents and maxize only in that case
            if (dialog._sfShouldMaximize) {
                dialog.maximize();
                $("body").removeClass("sfLoadingTransition");
            }
        }
    },
    _getCurrentMultisiteSelector: function () {
        if (typeof GetCurrentMultisiteSelector !== "undefined") {
            return GetCurrentMultisiteSelector();
        }
        else if (window.top && typeof window.top.GetCurrentMultisiteSelector !== "undefined") {
            return window.top.GetCurrentMultisiteSelector();
        }
        return null;
    },
    getBinder: function () {
        /// <summary>Returns the ClientBinder instance used by the ItemsList</summary>
        /// <returns>The internal ClientBinder</returns>
        if (this._binder == null) {
            this._binder = $find(this._binderId);
            this._binder.set_culture(this._culture);
            this._binder.set_uiCulture(this._uiCulture);
        }
        return this._binder;
    },
    getRadWindowManager: function () {
        if (this._radWindowManager == null) {
            this._radWindowManager = $find(this._radWindowManagerId);
        }
        return this._radWindowManager;
    },
    deleteItems: function (multikey, noPrompt, dataItems, deleteCurrentLanguageOnly) {
        /// <summary>Delete one or more items</summary>
        /// <param name="key">Key of the item to delete</param>
        /// <param name="noPrompt">Determines whether to show a confirm or not.</param>
        var keysCount = multikey.length;
        var undeletableItems = new Array();
        var undeletableMessage = "";
        var checkRelatingDataOptionText = this.get_checkRelatingDataMessageSingle();

        //check which of the items is not deletable
        if ((typeof (dataItems) != "undefined") && (dataItems != null) && (typeof (dataItems.length) != "undefined") && (dataItems.length > 0)) {
            for (var i = 0; i < dataItems.length; i++) {
                if (!this._isDeletable(dataItems[i]))
                    undeletableItems[undeletableItems.length] = dataItems[i];
            }
        }
        //if there are undeletable items..
        if (undeletableItems.length > 0) {
            //if some items are deletable and some are not, construct the message to show after deleiton
            if (undeletableItems.length != keysCount) {
                undeletableMessage = String.format(this._youDoNotHaveThePermissionsToDeleteSomeOfTheItems, undeletableItems.length);
                var undeletableKeys = this._getKeyFromItems(undeletableItems);
                //remove the undeletable keys from the array, in order to avoid server exceptions
                for (var key = 0; key < undeletableKeys.length; key++) {
                    for (var pendingDelete = 0; pendingDelete < multikey.length; pendingDelete++) {
                        if (this._areKeysEqual(multikey[pendingDelete], undeletableKeys[key])) {
                            multikey.splice(pendingDelete, 1);
                        }
                    }
                }
            }
                //if all items are undeletable, show a message and do not try to delete
            else {
                if (undeletableItems.length > 1) {
                    undeletableMessage = this._youDoNotHaveThePermissionsToDeleteTheseItems;
                }
                else {
                    undeletableMessage = this._youDoNotHaveThePermissionsToDeleteThisItem;
                }
                var noPermissionsDialog = this.getPromptDialogByName("permissionRestriction");
                noPermissionsDialog.show_prompt('', undeletableMessage);
                return;
            }
        }

        if (noPrompt) {
            this._binder.deleteItems(multikey, this._itemsDeletedDelegate, this);
        }
        else {
            var basicDialog = this.getPromptDialogByName("confirmDeleteSingle");
            var singleItemDeleteCommandButtonId = basicDialog._commands[0].ButtonClientId;
            var singleItemDeleteButton = jQuery('#' + singleItemDeleteCommandButtonId);
            var multipleItemsDeleteCommandButtonId = basicDialog._commands[1].ButtonClientId;
            var multipleItemsDeleteButton = jQuery('#' + multipleItemsDeleteCommandButtonId);
            if (this._supportsMultilingual && dataItems) {

                //If in multilingual and the item is translated in more than one language - show advanced confirm dialog
                var showAdvancedDialog = true;

                if (keysCount == 1) {
                    var dataItem = dataItems[0];
                    if (dataItem.AvailableLanguages) {
                        var availableLanguagesCount = dataItem.AvailableLanguages.length;
                        if (jQuery.inArray("", dataItem.AvailableLanguages) > -1) availableLanguagesCount--;
                        showAdvancedDialog = (availableLanguagesCount > 1);
                    } else {
                        showAdvancedDialog = false;
                    }
                    multipleItemsDeleteButton.attr("style", "display: none !important");
                    singleItemDeleteButton.removeAttr("style");
                } else {
                    // More than one items selected. Check if this items support multilingual.
                    showAdvancedDialog = false;
                    checkRelatingDataOptionText = this.get_checkRelatingDataMessageMultiple();
                    singleItemDeleteButton.attr("style", "display: none !important");
                    multipleItemsDeleteButton.removeAttr("style");
                    for (var i = 0; i < dataItems.length; i++) {
                        var dataItem = dataItems[i];
                        if (dataItem.AvailableLanguages && dataItem.AvailableLanguages.length > 0) {
                            showAdvancedDialog = true;
                            break;
                        }
                    }
                }
                if (showAdvancedDialog) {
                    var dialog = this.getPromptDialogByName("confirmDelete");
                    //var relatingDataCheckBox = jQuery('#' + dialog.get_checkRelatingDataCheckBox().id);
                    //var relatingDataLabel = jQuery(String.format("label[for='{0}']", dialog.get_checkRelatingDataCheckBox().id));
                    var command = jQuery.grep(dialog._commands, function (command, index) { return command.CommandName == "all" })[0];
                    var commandButton = null;
                    if (command) {
                        commandButton = jQuery('#' + command.ButtonClientId);
                    }
                    if (commandButton) {
                        if (deleteCurrentLanguageOnly) {
                            //hide delete all command when only one translation is to be deleted.
                            //relatingDataLabel.hide();
                            //relatingDataCheckBox.prop('checked', false);
                            //relatingDataCheckBox.hide();
                            commandButton.hide();
                        } else {
                            //relatingDataLabel.show();
                            //relatingDataCheckBox.prop('checked', true);
                            //relatingDataCheckBox.show();
                            commandButton.show();
                        }
                    }
                    //Insert the current language in the message
                    var singleLanguageDeleteCommand = null;
                    for (var i = 0; i < dialog._commands.length; i++) {
                        if (dialog._commands[i].CommandName === "language") {
                            singleLanguageDeleteCommand = dialog._commands[i];
                            break;
                        }
                    }
                    
                    var commandButtonId = singleLanguageDeleteCommand.ButtonClientId;
                    var button = jQuery('#' + commandButtonId).get(0);
                    var textContainer = button.children[0];
                    if (!this._confirmDeleteMessage) {
                        this._confirmDeleteMessage = textContainer.innerHTML;
                    }
                    textContainer.innerHTML = String.format(this._confirmDeleteMessage, this.get_uiCulture().toUpperCase());

                    var deleteMessage = '';

                    if (this.get_recycleBinEnabled()) {
                        if (keysCount == 1) {
                            deleteMessage = this._getSingleDeleteMessage(true);
                        }
                        else {
                            deleteMessage = this._getMultipleDeleteMessage(true, keysCount);
                        }
                    }

                    dialog.show_prompt('', deleteMessage, this._deleteItemsDelegate, multikey, checkRelatingDataOptionText);

                    return true;
                }
            }

            //If monolingual or multi but the item has only one language - show standard confirm dialog
            var confirmationMsg;
            if (keysCount > 1) {
                confirmationMsg = this._getMultipleDeleteMessage(this.get_recycleBinEnabled(), keysCount);
                singleItemDeleteButton.attr("style", "display: none !important");
                multipleItemsDeleteButton.removeAttr("style");
            }
            else {
                confirmationMsg = this._getSingleDeleteMessage(this.get_recycleBinEnabled());
                checkRelatingDataOptionText = this.get_checkRelatingDataMessageMultiple();
                multipleItemsDeleteButton.attr("style", "display: none !important");
                singleItemDeleteButton.removeAttr("style");
            }

            dialog = this.getPromptDialogByName("confirmDeleteSingle");

            dialog.show_prompt('', confirmationMsg, this._deleteItemsDelegate, multikey, checkRelatingDataOptionText);
        }

        //To be sure that it is always array.
        //Later this items can be restored from the Recycle Bin.
        //Folders currently can't be restored.
        this._deletedItems = [].concat(dataItems)
        .filter(function (item) {
            return !item.IsFolder
        });

        //if (undeletableMessage != "")
        //    alert(undeletableMessage);

        return true;
    },

    _deleteItemsFinal: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName == 'cancel') return;

        var multikey = args.get_commandArgument();

        var deleteCurrentLanguageOnly = (commandName == 'language');

        var lang = null;
        if (deleteCurrentLanguageOnly == true) {
            lang = this.get_uiCulture();
        }

        var checkRelatingData = args.get_checkRelatingData();

        this._binder.deleteItems(multikey, this._itemsDeletedDelegate, this, lang, checkRelatingData);
    },

    _itemsDeletedHandler: function (sender, args) {
        this._raiseItemsDeleted(sender, args);
    },

    getPromptDialogByCommandName: function (commandName) {
        if (commandName in this._promptDialogCommands) {
            return this.getPromptDialogByName(this._promptDialogCommands[commandName]);
        }
        else {
            return null;
        }
    },
    getPromptDialogByName: function (name) {
        if (name in this._promptDialogNames) {
            return this._getPromptDialogById(this._promptDialogNames[name]);
        }
        else {
            return null;
        }
    },

    // Visualizes the PromptDialog on the screen absolutely centered.
    // Only the name argument is required, others are optional.
    // The 'handlerFunction' argument should be a function with two arguments, e.g. myHandler(sender, args) 
    showPromptDialogByName: function (name, title, message, handlerFunction, context) {
        var dialog = this.getPromptDialogByName(name);
        dialog.show_prompt(title, message, handlerFunction, context);
    },
    _getPromptDialogById: function (id) {
        if (!this._promptDialogs[id]) {
            this._promptDialogs[id] = $find(id);
        }

        return this._promptDialogs[id];
    },

    groupPublish: function (multiKey, toPublish) {
        this._groupPublishHandler(null, null, this, multiKey, toPublish);
    },

    _groupPublishHandler: function (sender, args, thisList, multiKey, toPublish) {
        var thisList = this;
        // If method is called as a handler - NOT directly
        if (sender) {
            if (args.get_commandName() == "cancel") {
                return;
            }
            multiKey = sender._lastContext.multiKey;
            toPublish = sender._lastContext.toPublish;
            thisList = sender._lastContext.thisList;
        }

        if (toPublish == undefined)
            toPublish = true;
        var listOfIds = [];
        var iter = multiKey.length;
        while (iter--) {
            var item = multiKey[iter];
            if (item.Id)
                listOfIds.push(item.Id);
        }

        // callback.Failure(error, callbacks.Caller);
        var errorCallback = function (error, caller) {
            caller._binder._errorHandler(error);
        };

        // callbacks.Caller, data, executor._webRequest)
        var successCallback = function (caller, data, request) {
            caller._handleOperationSuccess(caller, data);
        }

        // url, urlParams, keys, data, successDelegate, failureDelegate, caller, enableValidation, validationGroup, context
        // /batch/publish/?provider={providerName}
        var publish = toPublish ? "publish" : "unpublish";
        thisList._manager.InvokePut(
            this._serviceBaseUrl,
            { "provider": thisList._binder.get_provider() },
            ["batch", publish],
            listOfIds, successCallback, errorCallback, thisList);
    },

    unlockItem: function (dataItem) {
        /// <summary>Invokes a web service to remove the lock from the item, and reload the list on success on success.</summary>

        // TODO: fix this mess
        // base url should be BASE. If it is a modified version that should be used by the binder,
        // then service CALL url should be set to that. 
        // Here, it is relied that BASE is really the service base

        var url = this._binder.get_serviceBaseUrl();
        var idx = url.indexOf(".svc");
        if (idx != -1)
            url = url.substr(0, url.indexOf(".svc") + 4) + '/';
        if (url.charAt(url.length - 1) != '/')
            url += '/';

        var itemType = "";
        var serviceBaseUrl = this._binder.get_serviceBaseUrl();
        if (serviceBaseUrl.indexOf("itemType=") != -1) {
            var itemTypeIndex = serviceBaseUrl.indexOf("itemType=");
            var tokenString = serviceBaseUrl.substring(itemTypeIndex);
            var ampIndex = tokenString.indexOf("&") + itemTypeIndex;
            var itemTypeParam = serviceBaseUrl.substring(itemTypeIndex, ampIndex);
            var valueIndex = itemTypeParam.indexOf("=");
            itemType = itemTypeParam.substring(valueIndex + 1);
        }

        // url, urlParams, keys, successDelegate, failureDelegate, caller, context
        // /temp/{contentId}/?providerName={providerName}&force={force}
        this._manager.InvokeDelete(
            url,
            { "providerName": this._providerName, "provider": this._providerName, "force": "true", "item_type": itemType },
            ["temp", dataItem.Id],
            this._handleOperationSuccessDelegate,
            this._binder._errorDelegate,
            this);
    },

    // Should be implemented on the binder.
    get_selectedItems: function () {
        // returns data items for all the selected items to be used in bulk operations
        this._binder.get_selectedItems();
    },

    show: function () {
        $(this._element).show();
    },

    hide: function () {
        $(this._element).hide();
    },

    setTranslations: function (dataItem, key, availableLangs, containerElement, handlers) {
        var commands = this._findTranslationCommands(containerElement);
        if (!commands) return;
        var _this = this;
        var isEditable = this._isEditable(dataItem);
        if (!isEditable) {
            commands.addClass("sfDisplayNone");
            return;
        }

        var actionsWrp = $(commands).parent(".sfMediaItemDetailsWrp");
        if (actionsWrp) {
            if (actionsWrp.find(".sfToggleLangListBtn").length > 0)
                actionsWrp.addClass("sfHasManyTranslations");
            else
                actionsWrp.addClass("sfHasTranslations");

        }

        commands.find("[class^=sfLang]").not(".sfLangAdd,.sfLangEdit").each(function (i, el) {
            var jElement = $(el);
            var classes = jElement.attr("class").split(" ");
            var langClass = null;
            if (classes.length > 1) {
                jQuery.each(classes, function (j, val) {
                    if (val.indexOf("sfLang") == 0) {
                        langClass = val;
                        return false;
                    }
                });
            }
            if (!langClass)
                return false;
            var lang = langClass.substring("sfLang".length);
            var mode = "create";
            if (jQuery.inArray(lang, availableLangs) > -1) {
                jElement.removeClass("sfNotTranslated");
                jElement.find(".sfLangEdit").removeClass("sfDisplayNone");
                jElement.find(".sfLangAdd").addClass("sfDisplayNone");
                mode = "edit";
            }
            var data = { mode: mode, dataItem: dataItem, key: key, element: containerElement, lang: lang, handlers: handlers };
            jElement.bind("click", data, _this._translationDelegate);
        });

        var hasTranslation = jQuery.inArray(this._uiCulture, availableLangs) > -1;
        var jcntElem = $(containerElement);
        $(jcntElem).toggleClass("sfNotTranslatedRow", !hasTranslation);
        var jTitle = jcntElem.find(this._itemTitleSelector).toggleClass("sfNotTranslated", !hasTranslation);
        jcntElem.find(this._itemsToHideSelector).toggleClass("sfVisibilityHidden", !hasTranslation); // hides the specified columns
        if (!hasTranslation) {
            var data = { mode: "create", dataItem: dataItem, key: key, element: containerElement, lang: this._uiCulture, handlers: handlers };
            jTitle.unbind("click").bind("click", data, this._translationDelegate);
        }
    },

    // Remove the specified command 
    removeActionsMenuItems: function (commandsToHide, containerElement) {
        var actionsPopupMenu = this._findActionsPopupMenu(containerElement);
        this._removeActionsMenuItems(commandsToHide, actionsPopupMenu);
        this._cleanSeparators(actionsPopupMenu);
    },

    // Keeps only the specified commands
    keepActionsMenuItems: function (commandsToKeep, containerElement) {
        var actionsPopupMenu = this._findActionsPopupMenu(containerElement);
        this._keepActionsMenuItems(commandsToKeep, actionsPopupMenu);
        this._cleanSeparators(actionsPopupMenu);
    },

    // Selects items by specified ids.
    selectByIds: function (ids) {
        this._binder.selectByIds(ids);
    },

    // Restores items that have been deleted
    undoDeletedItems: function () {
        var me = this;
        var url = this.get_recycleBinServiceUrl();
        var data = {
            ItemIdentifications: this._deletedItems
                .map(function (currentValue) {
                    var itemId = null;
                    if (currentValue.Id)
                        itemId = currentValue.Id;
                    else
                        itemId = currentValue;

                    return {
                        Id: itemId,
                        TypeName: me.get_recyclableContentItemType(),
                        ProviderName: me.get_providerName()
                    };
                })
        };
        this._manager.InvokePost(url, null, null, data, this._undoDeletedItemsDelegate, this._undoDeletedItemsDelegate);
    },

    // ------------------------------------------------------------------------
    // Events
    // ------------------------------------------------------------------------
    // not used; should probably add it to client manager
    //    add_appendUrlParams: function(delegate) {
    //        this.get_events().addHandler('appendUrlParams', delegate);
    //    },
    //    remove_appendUrlParams: function(delegate) {
    //        this.get_events().removeHandler('appendUrlParams', delegate);
    //    },
    add_dataBinding: function (delegate) {
        /// <summary>Happens before data binding, giving an option to cancel the event.</summary>
        this.get_events().addHandler("dataBinding", delegate);
    },
    remove_dataBinding: function (delegate) {
        /// <summary>Happens before data binding, giving an option to cancel the event.</summary>
        this.get_events().removeHandler("dataBinding", delegate);
    },

    add_dataBound: function (delegate) {
        /// <summary>Happens after the list is data bound.</summary>
        this.get_events().addHandler("dataBound", delegate);
    },
    remove_dataBound: function (delegate) {
        /// <summary>Happens after the list is data bound.</summary>
        this.get_events().removeHandler("dataBound", delegate);
    },

    add_command: function (delegate) {
        /// <summary>Happens when a custom command was fired outside the container. Can be cancelled.</summary>
        this.get_events().addHandler('command', delegate);
    },
    remove_command: function (delegate) {
        /// <summary>Happens when a custom command was fired outside the container. Can be cancelled.</summary>
        this.get_events().removeHandler('command', delegate);
    },

    add_beforeCommand: function (delegate) {
        /// <summary>Happens before a custom command was fired outside the container. Can be cancelled.</summary>
        this.get_events().addHandler('beforeCommand', delegate);
    },
    remove_beforeCommand: function (delegate) {
        /// <summary>Happens before a custom command was fired outside the container. Can be cancelled.</summary>
        this.get_events().removeHandler('beforeCommand', delegate);
    },

    add_itemCommand: function (delegate) {
        /// <summary>Happens when a custom command was fired inside the container. Can be cancelled.</summary>
        this.get_events().addHandler('itemCommand', delegate);
    },
    remove_itemCommand: function (delegate) {
        /// <summary>Happens when a custom command was fired inside the container. Can be cancelled.</summary>
        this.get_events().removeHandler('itemCommand', delegate);
    },

    add_dialogClosed: function (delegate) {
        /// <summary>Happens when the dialog was closed. Can be cancelled. If not cancelled, will rebind.</summary>
        this.get_events().addHandler('dialogClosed', delegate);
    },
    remove_dialogClosed: function (delegate) {
        /// <summary>Happens when the dialog was closed. Can be cancelled. If not cancelled, will rebind.</summary>
        this.get_events().removeHandler('dialogClosed', delegate);
    },

    add_dailogOpened: function (delegate) {
        /// <summary>Happens when a dialog should be opened (before opening). If cancelled, the dialog won't be opened.</summary>
        this.get_events().addHandler("dialogOpened", delegate);
    },
    remove_dialogOpened: function (delegate) {
        /// <summary>Happens when a dialog should be opened (before opening). If cancelled, the dialog won't be opened.</summary>
        this.get_events().removeHandler("dialogOpened", delegate);
    },

    add_dialogShowed: function (delegate) {
        /// <summary>Happens when the dialog needs to be showed, just before the createDialog callback. If cancelled,
        /// the callback won't be called, but the dialog will still be shown.</summary>
        this.get_events().addHandler("dialogShowed", delegate);
    },
    remove_dialogShowed: function (delegate) {
        /// <summary>Happens when the dialog needs to be showed, just before the createDialog callback. If cancelled,
        /// the callback won't be called, but the dialog will still be shown.</summary>
        this.get_events().removeHandler("dialogShowed", delegate);
    },

    add_linkActivated: function (delegate) {
        /// <summary>Happens when a link is clicked, just before the navigation. If cancelled, won't navigate.</summary>
        this.get_events().addHandler("linkActivated", delegate);
    },
    remove_linkActivated: function (delegate) {
        /// <summary>Happens when a link is clicked, just before the navigation. If cancelled, won't navigate.</summary>
        this.get_events().removeHandler("linkActivated", delegate);
    },

    add_itemsDeleted: function (delegate) {
        /// <summary>Happens when items have been deleted.</summary>
        this.get_events().addHandler("onItemsDeleted", delegate);
    },
    remove_itemsDeleted: function (delegate) {
        /// <summary>Happens when items have been deleted.</summary>
        this.get_events().removeHandler("onItemsDeleted", delegate);
    },

    _raiseItemsDeleted: function (sender, args) {
        /// <summary>Raises the items deleted handler.</summary>
        var h = this.get_events().getHandler('onItemsDeleted');
        if (h) h(sender, args);
        return args;
    },

    add_deletedItemsRestored: function (delegate) {
        /// <summary>Happens when items have been restored.</summary>
        this.get_events().addHandler("onDeletedItemsRestored", delegate);
    },
    remove_deletedItemsRestored: function (delegate) {
        /// <summary>Happens when items have been restored.</summary>
        this.get_events().removeHandler("onDeletedItemsRestored", delegate);
    },
    _raiseDeletedItemsRestored: function (sender, args) {
        /// <summary>Raises the event when the deleted items are restored.</summary>
        var h = this.get_events().getHandler('onDeletedItemsRestored');
        if (h) h(sender, args);
        return args;
    },

    // Happens when selection of the pages is changed (node is checked or unchecked)
    add_selectionChanged: function (delegate) {
        this.get_events().addHandler("selectionChanged", delegate);
    },
    // Happens when selection of the pages is changed (node is checked or unchecked)
    remove_selectionChanged: function (delegate) {
        this.get_events().removeHandler("selectionChanged", delegate);
    },

    // raises the selectionChanged event
    _raiseSelectionChanged: function (selectedItems) {
        var eventArgs = selectedItems;
        var handler = this.get_events().getHandler("selectionChanged");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    add_operationFailure: function (delegate) {
        this.get_events().addHandler("operationFailure", delegate);
    },
    remove_operationFailure: function (delegate) {
        this.get_events().removeHandler("operationFailure", delegate);
    },
    _raiseOperationFailure: function (originalSender, originalArgs) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs(originalSender, originalArgs);
        var handler = this.get_events().getHandler("operationFailure");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // Executes the specified command
    executeItemCommand: function (commandName, dataItem, key, commandArgument) {
        var dlg = this._radWindowManager.getWindowByName(commandName);
        if (dlg) {
            var argument = {};
            if (this._uiCulture)
                argument.language = this._uiCulture;
            if (commandArgument != 'undefined' && commandArgument != null) {
                $.extend(argument, commandArgument);
            }
            this.openDialog(commandName, dataItem, this._getDialogParameters(commandName, dataItem), key, argument);
        }
        var link = this._linkDescriptions[commandName];
        if (link) {
            link = link.replace("{{ProviderName}}", this._binder._provider);
            link = this._replacePropertyValues(dataItem, link);
            link = this._replaceQueryStringValues(link);
            if (this._supportsMultilingual) {
                // Adds the lang parameter to keep the language after creating a list in a non-default language
                link = Telerik.Sitefinity.setUrlParameter(link, "lang", this.get_uiCulture());
            }

            location.href = link;
        }
    },

    executeItemCommandInLanguage: function (language, commandName, dataItem, key, commandArgument) {
        var lang = this.get_uiCulture();
        this.set_uiCulture(language);
        try {
            this.executeItemCommand(commandName, dataItem, key, commandArgument);
        }
        finally {
            this.set_uiCulture(lang);
        }
    },

    // ------------------------------------------------------------------------
    // Event handlers
    // ------------------------------------------------------------------------
    _handlePageLoad: function (sender, args) {
        this._radWindowManager = this.getRadWindowManager();
        this._binder = this.getBinder();
        this._binder.set_provider(this._providerName);
        this._binder.set_dataKeyNames(this._keys);
        this._binder.set_serviceBaseUrl(this._serviceCallUrl);

        this._binder.add_onItemCommand(this._handleItemCommandDelegate);
        this._binder.add_onItemDataBinding(this._handleBinderDataBindingDelegate);
        this._binder.add_onDataBound(this._handleBinderDataBoundDelegate);
        this._binder.add_onDeleted(this._handleOperationSuccessDelegate);
        this._binder.add_onError(this._handleOperationFailureDelegate);

        //this._binder.set_unescapeHtml(true);        
        if (this._bindOnLoad) {
            this.applyFilter(""); // apply the constant filter on startup and data bind
        }

        //Needed for the DialogManager refactoring.
        //TODO should not subscribe for the closing event of all dialogs.
        //Expose functionality to subscribe for dialogs opened only by this component. (Maybe pass a callback to the openDialog function?)
        //GetDialogManager().add_dialogClosed(this._handleDialogClosedDelegate);
    },
    _handleBinderDataBinding: function (sender, args) {
        this._dataItem = args;
    },
    _handleBinderDataBound: function (sender, args) {
        this._onDataBound(this._dataItem);
    },
    _handleItemCommand: function (sender, args) {
        var originalCommandName = args.get_commandName();
        var commandName = this._matchTaxonomyCommand(originalCommandName);
        var commandArgument = args.get_commandArgument();
        var dataItem = args.get_dataItem();
        var itemElement = args.get_itemElement();
        if (args.get_cancel() === false && this._onItemCommand(commandName, dataItem).get_cancel() === false) {
            if (this._dataItemProperty) {
                dataItem = dataItem[this._dataItemProperty];
            }
            var key = args.get_key();
            switch (commandName) {
                case "delete":
                    this.deleteItems([key], false, [dataItem], false);
                    break;
                case "deleteLanguage":
                    this.deleteItems([key], false, [dataItem], true);
                    break;
                case "groupDelete":
                    var selectedItems = this._binder.get_selectedItems();
                    if (selectedItems && selectedItems.length > 0) {
                        var multiKey = this._getKeyFromItems(selectedItems);
                        this.deleteItems(multiKey, false, selectedItems);
                    }
                    break;
                case "tagFilter":
                case "categoryFilter":
                    var taxonId = originalCommandName.substring(originalCommandName.indexOf('[') + 1, originalCommandName.indexOf(']'));
                    var propertyName = originalCommandName.substring(originalCommandName.indexOf('(') + 1, originalCommandName.indexOf(')'));
                    var filterExpression = 'TaxonId.' + propertyName + '==' + taxonId;
                    this.applyFilter(filterExpression);
                    break;
                case "publish":
                    if (this._isEditable(dataItem)) {
                        var multiKey = this._getKeyFromItems([dataItem]);
                        this.groupPublish(multiKey, true);
                    }
                    break;
                case "unpublish":
                    if (this._isEditable(dataItem)) {
                        var multiKey = this._getKeyFromItems([dataItem]);
                        this.groupPublish(multiKey, false);
                    }
                    break;
                case "showMoreTranslations":
                case "hideMoreTranslations":
                    var hide = commandName == "hideMoreTranslations";
                    this.showHideTranslationCommands(hide, itemElement);
                    break;
                case "preview":
                    if (dataItem) {
                        this.openLocationWindow(dataItem)
                    }
                    break;
                default:
                    if (this._isEditable(dataItem) || (!this._isEditable(dataItem) && commandName != "edit")) {
                        this.executeCommand(commandName, dataItem, key, itemElement, null, commandArgument);
                    }
                    break;
            }
        }
    },

    openLocationWindow: function (dataItem) {
        var itemType = "";
        var serviceBaseUrl = this._binder.get_serviceBaseUrl();

        if (serviceBaseUrl.indexOf("itemType=") != -1) {
            var itemTypeIndex = serviceBaseUrl.indexOf("itemType=");
            var tokenString = serviceBaseUrl.substring(itemTypeIndex);
            var ampIndex = tokenString.indexOf("&") + itemTypeIndex;
            var itemTypeParam = serviceBaseUrl.substring(itemTypeIndex, ampIndex);
            var valueIndex = itemTypeParam.indexOf("=");
            itemType = itemTypeParam.substring(valueIndex + 1);
        }

        var previewHandlerUrl = this.get_contentLocationPreviewUrl() + "?item_id=" + dataItem.Id + "&item_type=" + itemType + "&item_provider=" + dataItem.ProviderName;
        if (this._supportsMultilingual) {
            previewHandlerUrl += "&item_culture=" + this.get_uiCulture();
        }

        window.open(previewHandlerUrl);
    },


    executeCommand: function (commandName, dataItem, key, itemElement, additionalParams, commandArgument) {
        if (dataItem != null) {
            var dlg = this._radWindowManager.getWindowByName(commandName);
            if (dlg) {
                var params = this._getDialogParameters(commandName, dataItem);
                if (additionalParams != 'undefined' && additionalParams != null) {
                    $.extend(params, additionalParams);
                }
                var argument = {};
                if (this._uiCulture)
                    argument.language = this._uiCulture;
                if (commandArgument != 'undefined' && commandArgument != null) {
                    $.extend(argument, commandArgument);
                }
                this.openDialog(commandName, dataItem, params, key, argument);
            }
        }
        var link = this._linkDescriptions[commandName];

        var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
        var provider = "";

        if (link) {

            var appender = link[link.length - 1] !== "/" ? "/" : "";
            var providerInQueryString = queryString.get("provider");
            var providerInTheBinder = this._binder.get_provider();
            var providerName = providerInQueryString == providerInTheBinder ? providerInQueryString : providerInTheBinder;

            //if (queryString.contains("provider") && link.indexOf('provider') == -1) {
            appender += link.indexOf("?") === -1 ? "?" : "&";
            provider = appender + "provider=" + providerName;
            if (providerName != "") {
                if (link.indexOf("{{ProviderName}}") != -1) {
                    link = link.replace("{{ProviderName}}", providerName);
                }
                else {
                    link = link + provider;
                }
            }
            //}

            link = this._replacePropertyValues(dataItem, link);
            link = this._replaceQueryStringValues(link);
            if (this._supportsMultilingual) {
                // Adds the lang parameter to keep the language when going from lists to list items on edit command
                link = Telerik.Sitefinity.setUrlParameter(link, "lang", this.get_uiCulture());
            }
            if (this._onLinkActivated(commandName, dataItem, this, itemElement, link).get_cancel() != true) {
                location.href = link;
            }
        }
    },

    // show/hides the expander buttons for translations
    showHideTranslationCommands: function (hide, element) {
        var jElem = $(element);
        if (hide) {
            jElem.find(".sfMoreLangs").addClass("sfDisplayNoneImportant");
            jElem.find(".sf_binderCommand_hideMoreTranslations").addClass("sfDisplayNone");
            jElem.find(".sf_binderCommand_showMoreTranslations").removeClass("sfDisplayNone");
        }
        else {
            jElem.find(".sfMoreLangs").removeClass("sfDisplayNoneImportant");
            jElem.find(".sf_binderCommand_showMoreTranslations").addClass("sfDisplayNone");
            jElem.find(".sf_binderCommand_hideMoreTranslations").removeClass("sfDisplayNone");
        }
    },

    _isEditable: function (item) {
        var isEditable = true;
        if ((typeof (item.IsEditable) != typeof (undefined)) && (item.IsEditable != null) && (!item.IsEditable)) {
            isEditable = false;
        }
        return isEditable;
    },

    _isDeletable: function (item) {
        var isDeletable = false;
        //        if ((typeof (item.IsDeletable) != typeof (undefined)) && (item.IsDeletable != null) && (!item.IsDeletable)) {
        //            isDeletable = false;
        //        } else {

        if (item.IsDeletable) {
            if (item.IsDeletable == true) {
                isDeletable = true;
            }
        }
        if (item.WorkflowOperations) {
            for (var i = 0; i < item.WorkflowOperations.length; i++) {
                if (item.WorkflowOperations[i].OperationName == "Delete") {
                    isDeletable = true;
                }
            }
            //            }
        }

        if ((!item.WorkflowOperations) && (typeof (item.IsDeletable) == typeof (undefined))) {
            isDeletable = true;
        }


        return isDeletable;
    },

    //if fiter by tag or category replace the commandName with exact command expression: "tagFilter"  or "categotyFilter"
    _matchTaxonomyCommand: function (commandName) {
        var tagFilter = "tagFilter";
        var categoryFilter = "categoryFilter";
        if (commandName.substring(0, tagFilter.length) == tagFilter) {
            commandName = tagFilter;
        }
        else if (commandName.substring(0, categoryFilter.length) == categoryFilter) {
            commandName = categoryFilter;
        }
        return commandName;
    },

    _handleCommand: function (sender, args) {
        var commandName = args.get_commandName();
        var argument = args.get_commandArgument();
        switch (commandName) {
            case "groupDelete":
                var originalBindOnSuccess = this._bindOnSuccess;
                this._bindOnSuccess = false;
                var selectedItems = this._binder.get_selectedItems();
                if (selectedItems && selectedItems.length > 0) {
                    var multiKey = this._getKeyFromItems(selectedItems);
                    this.deleteItems(multiKey, false, selectedItems);
                }
                this._bindOnSuccess = originalBindOnSuccess;
                break;
            case "groupPublish":
            case "groupUnpublish":
                var selected = this._binder.get_selectedItems();
                var itemsIter = selected.length - 1;
                while (itemsIter >= 0) {
                    if (!this._isEditable(selected[itemsIter])) {
                        selected.splice(itemsIter, 1);
                    }
                    itemsIter--;
                }
                if (selected && selected.length > 0) {
                    var originalBindOnSuccess = this._bindOnSuccess;
                    this._bindOnSuccess = false;
                    var multiKey = this._getKeyFromItems(selected);
                    this.groupPublish(multiKey, commandName == "groupPublish");
                    this._bindOnSuccess = originalBindOnSuccess;
                }
                break;
            case "filterByTag":
                var taxonId = args.get_commandArgument().get_dataItem().Id;
                var filterExpression = 'TaxonId.Tags==' + taxonId;
                this.applyFilter(filterExpression);
                break;
            case "filterByCategory":
                var taxonId = args.get_commandArgument().get_dataItem().Id;
                var filterExpression = 'TaxonId.Category==' + taxonId;
                this.applyFilter(filterExpression);
                break;
            case "showSectionsExceptAndResetFilter":
                var filterExpression = '';
                this.applyFilter(filterExpression);
                break;
            case "editLibraryProperties": // should do nothing.
            case "parentProperties": // should do nothing.
                break;
            case "changeLanguage":
                this.set_uiCulture(argument);
                this.dataBind();
                break;
            case "manageContentLocations":
                var link = this._linkDescriptions[commandName];
                var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                var provider;
                if (queryString.contains("provider"))
                    provider = queryString.get("provider");
                else if (this._binder._provider)
                    provider = this._binder._provider;

                if (link) {
                    if (link.indexOf('provider') == -1 && provider) {
                        var appender = link.indexOf("?") === -1 ? "?" : "&";
                        if (appender == "?" && link[link.length - 1] !== "/") {
                            appender = "/?";
                        }
                        var qsProvider = appender + "provider=" + provider;
                        link = link + qsProvider;
                    }

                    if (this._onLinkActivated(commandName, null, this, null, link).get_cancel() != true) {
                        location.href = link;
                    }
                }
                break;
            default:
                var baseCustomFieldName = "filterBy_Classification_";
                var filterByCustomClassification = commandName.indexOf(baseCustomFieldName) == 0;
                if (filterByCustomClassification) {
                    var taxonId = args.get_commandArgument().get_dataItem().Id;
                    var classificationType = commandName.substring(baseCustomFieldName.length);
                    var filterExpression = 'TaxonId.' + classificationType + '==' + taxonId;
                    this.applyFilter(filterExpression);
                    break;
                }
                var promptDialog = this.getPromptDialogByCommandName(commandName);
                if (promptDialog) {
                    promptDialog.show_prompt();
                }

                var dlg = this._radWindowManager.getWindowByName(commandName);
                if (dlg) {
                    this.openDialog(commandName, null, this._getDialogParameters(commandName), null, { language: this._uiCulture });
                }

                var link = this._linkDescriptions[commandName];
                var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                var provider = "";

                if (link) {
                    if (link.indexOf('/Videos/Videos') != -1) {
                        link = link.replace('/Videos/Videos', '/Videos');
                    }
                    if (link.indexOf('/Images/Images') != -1) {
                        link = link.replace('/Images/Images', '/Images');
                    }
                    if (link.indexOf('/Documents/Documents') != -1) {
                        link = link.replace('/Documents/Documents', '/Documents');
                    }

                    if (queryString.contains("provider") && link.indexOf('provider') == -1) {
                        var appender = link.indexOf("?") === -1 ? "?" : "&";
                        if (appender == "?" && link[link.length - 1] !== "/") {
                            appender = "/?";
                        }
                        provider = appender + "provider=" + queryString.get("provider");
                        link = link + provider;
                    }
                }

                if (link) {
                    link = this._replaceQueryStringValues(link);
                    if (this._onLinkActivated(commandName, null, this, null, link).get_cancel() != true) {
                        location.href = link;
                    }
                }
                break;
        }
    },

    _handleDialogClosed: function (sender, eventArgs) {
        var dialogArgument = eventArgs.get_argument();
        // TODO: remove this approach, when time allows
        // backwards compatibility for the closeAndRebind method
        //        if (dialogArgument != null) {
        //            if (dialogArgument === "rebind") {
        //                if (this._onDialogClosed(false, true, null).get_cancel() == false) {
        //                    setTimeout('this.dataBind()', 0);
        //                }
        //            }
        //            else if (dialogArgument.DataItem) { // uses the new approach
        //                if (this._onDialogClosed(dialogArgument.IsCreated, dialogArgument.IsUpdated, dialogArgument.DataItem, dialogArgument.Context).get_cancel() == false) {
        //                    this.dataBind("dialogClosed", dialogArgument.DataItem);
        //                }
        //            }
        //            // We have argument but there is no data item or command so we just rethrow the argument as context with no binding
        //            else {
        //                this._onDialogClosedWithCustomArgument(sender, dialogArgument);
        //            }
        //        }

        //HACK: fix 78949 grid items rebind ('GET' requests) fail with status 0 at Chrome
        //The most posible reason is that previous('PUT') request on save didnt completed it's callback.
        // At this situation all next requests have status 0.

        var timeout = 0;
        if (sender && sender.DetailFormView && !sender.DetailFormView._deleteTempCompleted && dialogArgument === "rebind") {
            timeout = 300;
        }

        window.setTimeout(Function.createDelegate(this, function () { this._dataBindAfterDialogClosed(sender, dialogArgument) }), timeout);
        sender.remove_close(this._handleDialogClosedDelegate);
    },

    _dataBindAfterDialogClosed: function (sender, dialogArgument) {
        if (dialogArgument != null) {
            if (dialogArgument === "rebind") {
                if (this._onDialogClosed(false, true, null).get_cancel() == false) {
                    this.dataBind();
                }
            }
            else if (dialogArgument.DataItem) { // uses the new approach
                var eventArgs = this._onDialogClosed(dialogArgument.IsCreated, dialogArgument.IsUpdated, dialogArgument.DataItem, dialogArgument.Context);

                if (!eventArgs.get_cancel() &&  (!dialogArgument.IsDuplicated || !dialogArgument.DoNotRebind)) {
                    this.dataBind("dialogClosed", dialogArgument.DataItem);
                }
            }
                // We have argument but there is no data item or command so we just rethrow the argument as context with no binding
            else {
                this._onDialogClosedWithCustomArgument(sender, dialogArgument);
            }
        }
    },

    _showDialogExtension: function (sender, e, isLoad) {
        var args = sender._sfArgs;
        // in some rare cases _sfArgs is undefined

        if (args) {
            var itemsList = typeof args.get_itemsList === "undefined" ? args.itemsList: args.get_itemsList();
            var dataItem = typeof args.get_dataItem === "undefined" ? args.dataItem : args.get_dataItem();
            var commandName = typeof args.get_commandName === "undefined" ? args.commandName : args.get_commandName();
            var dialog = typeof args.get_dialog === "undefined" ? args.dialog : args.get_dialog();
            var params = typeof args.get_params === "undefined" ? args.params : args.get_params();
            var key = typeof args.get_key === "undefined" ? args.key : args.get_key();

            // in case if we come back from a history point we reload the dialog with the item that it is currently bound to
            if (dialog.OpeningFromHistory && dialog.DetailFormView) {
                var detailFormView = dialog.DetailFormView;
                key = detailFormView.get_dataKey();
                commandName = detailFormView.get_createFormCommandName();
                dataItem = detailFormView.get_dataItem();
            }

            var commandArgument;
            if (typeof args.get_commandArgument !== "undefined") {
                commandArgument = args.get_commandArgument();
            }
            if (itemsList._onDialogShowed(commandName, dataItem, itemsList, dialog, params, key, commandArgument).get_cancel() != true) {
                var frameHandle = sender.get_contentFrame().contentWindow;
                if (frameHandle) {
                    if (itemsList.get_scrollOpenedDialogsToTop()) {
                        frameHandle.scrollTo(0, 0);
                    }
                    //check if the show is called on dialog that is reloaded on each showing.
                    //If this is the case the createDialog method must be called on load, not on show.
                    if (frameHandle.createDialog && (!dialog.get_reloadOnShow() || isLoad)) {
                        if (!$telerik.isChrome) {
                            frameHandle.createDialog(commandName, dataItem, itemsList, dialog, params, key, commandArgument);
                        }
                        else {
                            window.setTimeout(function () { frameHandle.createDialog(commandName, dataItem, itemsList, dialog, params, key, commandArgument); }, 0);
                        }
                    }
                }
            }
        }
    },

    _loadDialogExtension: function (sender, e) {
        var args = sender._sfArgs;
        var dialog = args.get_dialog();
        var itemsList = args.get_itemsList();

        dialog.remove_pageLoad(dialog._sfLoadDialogExtension);
        dialog._sfShowDialogExtension(sender, args, true);
    },

    _closeDialogExtension: function (sender, originalArgs) {
        var args = sender._sfArgs;
        var itemsList;
        if (typeof args.get_itemsList === "undefined") {
            itemsList = args.itemsList;
        } else {
            itemsList = args.get_itemsList();
        }
        itemsList._handleDialogClosed(sender, originalArgs);
    },

    _handleOperationSuccess: function (sender, successData) {
        if (this._bindOnSuccess == true) {
            this.dataBind();
        }
    },

    _handleOperationFailure: function (sender, args) {
        var eventArgs = this._raiseOperationFailure(sender, args);
        if (!eventArgs.get_cancel()) {
            var error = args.get_error();
            alert(error);
        }
    },

    // handles click of the transaltion buttons
    _translationHandler: function (jEvent) {
        var data = jEvent.data;
        var mode = data.mode;
        var lang = data.lang;
        var dataItem = data.dataItem;
        var key = data.key;
        var element = data.element;
        var handlers = data.handlers;
        jEvent.preventDefault();
        jEvent.stopImmediatePropagation(); // prevents execution of other click handlers
        var handler = this._getTranslationHanlder(mode, handlers);
        var cancelled = false;
        if (handler) {
            var args = new Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs(mode, lang, dataItem, key, this, element);
            handler(this, args);
            cancelled = args.get_cancel();
        }
        if (!cancelled) {
            var editCommand = dataItem.EditCommandName ? dataItem.EditCommandName : "edit";
            this.executeCommand(editCommand, dataItem, key, element, null, { language: lang, languageMode: mode });
        }
        return false;
    },

    _undoDeletedItemsHandler: function (sender, args) {
        this._raiseDeletedItemsRestored()
        this.dataBind();
    },

    // ------------------------------------------------------------------------
    // Private functions
    // ------------------------------------------------------------------------
    _bindToolboxCommands: function () {
        var commandItemsCount = this._commandItems.length;
        var self = this;
        var commandButtonId = '';
        for (var i = 0; i < commandItemsCount; i++) {
            commandButtonId = this._commandItems[i].ButtonId;
            switch (this._commandItems[i].ButtonName) {
                case "ProvidersListToolboxItem":
                    var providersList = $find(commandButtonId);
                    providersList.add_providerNameChanged(this._handleProvidersListChangeDelegate);
                    break;
                case "MenuToolboxItem":
                    var menu = $find(commandButtonId);
                    if (this._handleMenuItemClickedDelegate == null) {
                        this._handleMenuItemClickedDelegate = Function.createDelegate(this, this._handleMenuItemClicked);
                    }
                    menu.add_itemClicked(this._handleMenuItemClickedDelegate);
                    break;
                case "DropDownToolboxItem":
                    var dropDown = $get(commandButtonId);
                    this._dropDownChangeHandlerDelegate = Function.createDelegate(this, this._dropDownChangeHandler);
                    $addHandler(dropDown, "change", this._dropDownChangeHandlerDelegate);
                    break;
                default:
                    $('#' + commandButtonId).each(function () {
                        $(this).click(function () {
                            if (!$(this).hasClass('sfDisabledLinkBtn')) {
                                var commandName = self._getCommandName($(this).attr('id'));
                                if (!self._onCommanding(commandName, null).get_cancel()) {
                                    self._onCommand(commandName, null);
                                }
                            }
                        });
                        // prevent memory leaks
                        $(this).on("unload", function (e) {
                            jQuery.event.remove(this);
                            jQuery.removeData(this);
                        });
                    });
                    break;
            }
        }
    },
    _handleMenuItemClicked: function (sender, args) {
        var commandName = args.get_item().get_value();
        if (!(commandName == null || commandName.length == 0)) {
            if (!this._onCommanding(commandName, null).get_cancel()) {
                this._onCommand(commandName, null);
            }
            args.get_item().get_menu().close();
        }
    },
    _handleProvidersListChange: function (sender, args) {
        var providerName = args.get_newProviderName();
        var commandName = this._getCommandName(sender.get_element().id);
        if (!this._onCommanding(commandName, providerName).get_cancel()) {
            this._onCommand(commandName, providerName);
        }
    },

    _dropDownChangeHandler: function (event) {
        var commandName = this._getCommandName(event.target.id);
        var selectedValue = $(event.target).val();
        if (!this._onCommanding(commandName, selectedValue).get_cancel()) {
            this._onCommand(commandName, selectedValue);
        }
    },

    _getCommandName: function (senderId) {
        var commandItemsCount = this._commandItems.length;
        for (var i = 0; i < commandItemsCount; i++) {
            var commandButtonId = this._commandItems[i].ButtonId;
            var commandName = this._commandItems[i].CommandName;
            if (commandButtonId == senderId) {
                return commandName;
            }
        }
        return 'no such command';
    },
    _getKeyFromItems: function (items) {
        var multiKey = [];
        var keysCount = this._keys.length;
        var itemsIter = items.length;

        while (itemsIter--) {
            var key = [];

            for (var keysIter = 0; keysIter < keysCount; keysIter++) {
                var name = this._keys[keysIter].replace(' ', '');
                key[name] = items[itemsIter][name];
            }
            multiKey.push(key);
        }
        return multiKey;

    },

    _areKeysEqual: function (item1, item2) {
        var keysCount = this._keys.length;
        for (var keysIter = 0; keysIter < keysCount; keysIter++) {
            var name = this._keys[keysIter].replace(' ', '');
            if (item1[name] != item2[name])
                return false;
        }
        return true;
    },

    _replacePropertyValues: function (dataItem, literal) {
        if (literal && dataItem) {
            var matches = literal.match(/{{[\w.]+}}/g);
            if (matches) {
                var matchIndex = matches.length;
                var current = null;
                var propName = null;
                var propValue = null;
                while (matchIndex--) {
                    current = matches[matchIndex];
                    propName = current.slice(2, -2);
                    propValue = this._getPropertyValue(dataItem, propName);
                    literal = literal.replace(current, propValue);
                }
            }
        }
        return literal;
    },
    _getPropertyValue: function (dataItem, propertyPath) {
        var value = dataItem;
        if (dataItem != null && typeof (dataItem) != "undefined"
            && propertyPath != null && typeof (propertyPath) != "undefined" && propertyPath != "") {
            var dotIndex = propertyPath.indexOf(".");
            var propertyName = propertyPath;
            if (dotIndex == -1) {
                value = dataItem[propertyName];
            }
            else {
                propertyName = propertyName.slice(0, dotIndex);
                var compoundObj = dataItem[propertyName];
                var subPropertyName = propertyPath.slice(dotIndex + 1);
                value = this._getPropertyValue(compoundObj, subPropertyName);
            }
        }
        return value;
    },
    _replaceQueryStringValues: function (literal) {
        if (literal) {
            var matches = literal.match(/\[\[\w+\]\]/g);
            if (matches) {
                var matchIndex = matches.length;
                var name = "";
                var value = null;
                while (matchIndex--) {
                    current = matches[matchIndex];
                    name = current.slice(2, -2).toLowerCase();
                    for (var queryParam in this._queryStringParts) {
                        if (queryParam.toLowerCase() == name) {
                            value = this._queryStringParts[queryParam];
                            break;
                        }
                    }
                    literal = literal.replace(current, value);
                }
            }
        }
        return literal;
    },
    _getDialogParameters: function (dialogName, dataItem) {
        var params = this._dialogParameters[dialogName];
        params = this._replacePropertyValues(dataItem ? dataItem : this._dataItem, params);
        params = this._replaceQueryStringValues(params);
        params = this._splitQueryString(params);
        var editable = true;
        if ((dataItem != null) && (typeof (dataItem) != "undefined")) {
            editable = dataItem.IsEditable;
        }
        params["IsEditable"] = editable;
        params["listFormItemName"] = this._formItemName;
        params["providerName"] = this._binder._provider;

        return params;
    },
    _splitQueryString: function (queryString) {
        var queryStringParts = [];
        if (queryString && queryString.length > 0) {
            if (queryString.indexOf("?") == 0) {
                queryString = queryString.substring(1);
            }
            if (queryString) {
                var pairs = queryString.split("&");
                var i = pairs.length;
                var keyValuePair = null;
                while (i--) {
                    keyValuePair = pairs[i].split("=");
                    queryStringParts[keyValuePair[0]] = keyValuePair[1];
                }
            }
        }
        return queryStringParts;
    },
    _setProperty: function (name, value) {
        var fieldName = "_" + name;
        var fieldValue = this[fieldName];
        if (typeof (fieldValue) == "undefined" || fieldValue != value) {
            this[fieldName] = value;
            this.raisePropertyChanged(name);
        }
    },

    _findTranslationCommands: function (containerElement) {
        var commands = jQuery(containerElement).find("div.sfTranslationCommands").not(jQuery(this).parents("div.translationCommands"));
        if (commands.length == 0) {
            return null;
        }
        if (commands.length > 1) {
            commands = jQuery(commands[0]);
        }
        return commands;
    },

    _getTranslationHanlder: function (key, handlers) {
        if (handlers && handlers[key] && typeof handlers[key] == "function")
            return handlers[key];
        var translationHandlers = this._translationHandlers;
        if (translationHandlers && translationHandlers[key] && typeof translationHandlers[key] == "function")
            return translationHandlers[key];
        return null;
    },

    // Finds the DOM elelemt that represents the actions menu in the given container element
    _findActionsPopupMenu: function (containerElement) {
        //TODO is .not(jQuery(this).parents("ul.actionsMenu")) ok?
        var menu = jQuery(containerElement).find("ul.actionsMenu").not(jQuery(this).parents("ul.actionsMenu"));
        if (menu.length == 0) {
            throw Error.create("Actions menu not found!");
        }
        if (menu.length > 1) {
            menu = jQuery(menu[0]);
        }

        var result = menu.find("li ul");
        if (result.length != 1) {
            throw Error.create("Actions menu pupup element not found!");
        }

        return result;
    },

    // Remove the specified command from the popup menu
    _removeActionsMenuItems: function (commandsToHide, actionsPopupMenu) {
        var commandCount = commandsToHide.length;
        while (commandCount--) {
            var commandCss = commandsToHide[commandCount];
            var commandElement = actionsPopupMenu.find("li:has(a." + commandCss + ")");
            if (commandElement.length == 1) {
                commandElement.remove();
            }
        }
    },

    // Keeps only the specified commands
    _keepActionsMenuItems: function (commandsToKeep, actionsPopupMenu) {
        var commandElements = actionsPopupMenu.find("li a");
        var commandCount = commandsToKeep.length;
        var elementsCount = commandElements.length;
        while (elementsCount--) {
            var commandElement = jQuery(commandElements[elementsCount]);
            var hide = true;
            var counter = commandCount;
            while (counter--) {
                var commandCss = commandsToKeep[counter];
                if (commandElement.hasClass(commandCss) && hide) {
                    hide = false;
                }
            }
            if (hide) {
                commandElement.parent().remove();
            }
        }
    },

    // Cleans the unnecessary separators
    _cleanSeparators: function (actionsPopupMenu) {
        // finds all empty seperators - if the next element is seperator gets the previous one/the empty one
        var emptySeparators = actionsPopupMenu.find("li.sfSeparator").next(".sfSeparator").prev();
        if (emptySeparators.length > 0) {
            emptySeparators.remove();
        }

        // check if the last item is an empty seperator
        emptySeparators = actionsPopupMenu.find("li:last");
        if (emptySeparators.hasClass("sfSeparator")) {
            emptySeparators.remove();
        }
    },

    _getSingleDeleteMessage: function (recycleBinEnabled) {
        if (recycleBinEnabled) {
            return this._sendToRecycleBinSingleConfirmationMessage;
        }
        else {
            return this._deleteSingleConfirmationMessage;
        }
    },

    _getMultipleDeleteMessage: function (recycleBinEnabled, itemsCount) {
        if (recycleBinEnabled) {
            return String.format(this.get_sendToRecycleBinMultipleConfirmationMessage(), itemsCount);
        }
        else {
            return String.format(this._deleteMultipleConfirmationMessage, itemsCount);
        }
    },

    //Needed for the DialogManager refactoring.
    //_getDialogContext: function (commandName, dataItem, itemsList, dialog, params, key, commandArgument) {
    //    return { commandName: commandName, dataItem: dataItem, itemsList: itemsList, dialog: dialog, params: params, key: key, commandArgument: commandArgument };
    //},

    // ------------------------------------------------------------------------
    // Event firing
    // ------------------------------------------------------------------------
    // not used; should probably add it to client manager
    //    _onAppendUrlParams: function(commandName, params) {
    //        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.AppendUrlParamsEventArgs(commandName, params);
    //        var handler = this.get_events().getHandler('appendUrlParams');
    //        if (handler) handler(this, eventArgs);
    //        return eventArgs;
    //    },
    _onDataBinding: function () {
        var eventArgs = new Sys.CancelEventArgs();
        var handler = this.get_events().getHandler("dataBinding");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onDataBound: function (dataItem) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs(dataItem);
        var handler = this.get_events().getHandler("dataBound");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onCommanding: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('commanding');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('command');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onBeforeCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('beforeCommand');
        if (handler) handler(this, eventArgs);
        commandArgument.set_cancel(eventArgs.get_cancel());
        return eventArgs;
    },
    _onItemCommand: function (commandName, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs(commandName, commandArgument);
        var handler = this.get_events().getHandler('itemCommand');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onDialogClosed: function (isCreated, isUpdated, dataItem, context) {
        var eventArgs = new Telerik.Sitefinity.DialogClosedEventArgs(isCreated, isUpdated, dataItem, context);
        var handler = this.get_events().getHandler('dialogClosed');
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onDialogClosedWithCustomArgument: function (sender, eventArgs) {
        var handler = this.get_events().getHandler('dialogClosed');
        if (handler) handler(sender, eventArgs);
        return eventArgs;
    },
    _onDialogOpened: function (commandName, dataItem, itemsList, dialog, params, key) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs(commandName, dataItem, itemsList, dialog, params, key);
        var handler = this.get_events().getHandler("dialogOpened");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onDialogShowed: function (commandName, dataItem, itemsList, dialog, params, key, commandArgument) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs(commandName, dataItem, itemsList, dialog, params, key, commandArgument);
        var handler = this.get_events().getHandler("dialogShowed");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onLinkActivated: function (commandName, dataItem, itemsList, link, navigateUrl) {
        var eventArgs = new Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs(commandName, dataItem, itemsList, link, navigateUrl);
        var handler = this.get_events().getHandler("linkActivated");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },
    _onSelectedItemsChanged: function () {
        var eventArgs = Sys.EventArgs.Empty;
        var handler = this.get_events().getHandler("selectedItemsChanged");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // ------------------------------------------------------------------------
    // properties
    // ------------------------------------------------------------------------ 

    get_promptDialogIds: function () {
        return this._promptDialogIds;
    },
    set_promptDialogIds: function (value) {
        this._setProperty("promptDialogIds", value);
    },

    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._setProperty("providerName", value);
        if (this._binder) {
            this._binder.set_provider(value);
        }
    },

    get_managerType: function () {
        return this._managerType;
    },
    set_managerType: function (value) {
        this._setProperty("managerType", value);
        if (this._binder) {
            this._binder.set_managerType(value);
        }
    },
    get_bindOnSuccess: function () {
        return this._bindOnSuccess;
    },
    set_bindOnSuccess: function (value) {
        //this._bindOnSuccess = value;
        this._setProperty("bindOnSuccess", value);
    },
    get_radWindowManagerId: function () {
        return this._radWindowManagerId;
    },
    set_radWindowManagerId: function (value) {
        this._radWindowManagerId = value;
    },
    get_serviceBaseUrl: function () {
        return this._serviceBaseUrl;
    },
    set_serviceBaseUrl: function (value) {
        //this._serviceBaseUrl = value;
        this._setProperty("serviceBaseUrl", value);
    },
    get_binderId: function () {
        return this._binderId;
    },
    set_binderId: function (value) {
        //this._binderId = value;
        this._setProperty("binderId", value);
    },
    get_keys: function () {
        return this._keys;
    },
    set_keys: function (value) {
        //this._keys = value;
        this._setProperty("keys", value);
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_deleteMultipleConfirmationMessage: function () {
        return this._deleteMultipleConfirmationMessage;
    },
    set_deleteMultipleConfirmationMessage: function (value) {
        this._setProperty("deleteMultipleConfirmationMessage", value);
    },
    get_deleteSingleConfirmationMessage: function () {
        return this._deleteSingleConfirmationMessage;
    },
    set_deleteSingleConfirmationMessage: function (value) {
        this._setProperty("deleteSingleConfirmationMessage", value);
    },
    get_checkRelatingDataMessageSingle: function () {
        return this._checkRelatingDataMessageSingle;
    },
    set_checkRelatingDataMessageSingle: function (value) {
        this._setProperty("checkRelatingDataMessageSingle", value);
    },
    get_checkRelatingDataMessageMultiple: function () {
        return this._checkRelatingDataMessageMultiple;
    },
    set_checkRelatingDataMessageMultiple: function (value) {
        this._setProperty("checkRelatingDataMessageMultiple", value);
    },
    get_recycleBinEnabled: function () {
        return this._recycleBinEnabled;
    },
    set_recycleBinEnabled: function (value) {
        this._setProperty("_recycleBinEnabled", value);
    },
    get_sendToRecycleBinSingleConfirmationMessage: function () {
        return this._sendToRecycleBinSingleConfirmationMessage;
    },
    set_sendToRecycleBinSingleConfirmationMessage: function (value) {
        this._setProperty("_sendToRecycleBinSingleConfirmationMessage", value);
    },
    get_sendToRecycleBinMultipleConfirmationMessage: function () {
        return this._sendToRecycleBinMultipleConfirmationMessage;
    },
    set_sendToRecycleBinMultipleConfirmationMessage: function (value) {
        this._setProperty("_sendToRecycleBinMultipleConfirmationMessage", value);
    },
    get_linkDescriptions: function () {
        return this._linkDescriptions;
    },
    set_linkDescriptions: function (value) {
        //this._linkDescriptions = value;
        this._setProperty("linkDescriptions", value);
    },
    get_constantFilter: function () {
        return this._constantFilter;
    },
    set_constantFilter: function (value) {
        // this._constantFilter = value;
        this._setProperty("constantFilter", value);
    },
    get_dialogParameters: function () {
        return this._dialogParameters;
    },
    set_dialogParameters: function (value) {
        // this._dialogParameters = value;
        this._setProperty("dialogParameters", value);
    },
    get_selectedItems: function () {
        return this._binder.get_selectedItems();
    },
    get_serviceCallUrl: function () {
        return this._serviceCallUrl;
    },
    set_serviceCallUrl: function (value) {
        // this._serviceCallUrl = value;
        this._setProperty("serviceCallUrl", value);
    },
    get_formItemName: function () {
        return this._formItemName;
    },
    set_formItemName: function (value) {
        this._setProperty("formItemName", value);
    },
    get_scrollOpenedDialogsToTop: function () {
        return this._scrollOpenedDialogsToTop;
    },
    set_scrollOpenedDialogsToTop: function (value) {
        this._setProperty("scrollOpenedDialogsToTop", value);
    },
    get_commandItems: function () {
        return this._commandItems;
    },
    set_commandItems: function (value) {
        this._commandItems = value;
    },
    // Specifies the culture that will be used on the server as CurrentThread when processing the request
    set_culture: function (culture) {
        this._culture = culture;
        if (this._manager != null)
            this._manager.set_culture(this._culture);
        if (this._binder != null)
            this._binder.set_culture(this._culture);
    },
    // Gets the culture that will be used on the server when processing the request
    get_culture: function () {
        return this._culture;
    },

    // Specifies the culture that will be used on the server as UICulture when processing the request
    set_uiCulture: function (culture) {
        this._uiCulture = culture;
        if (this._manager != null)
            this._manager.set_uiCulture(this._uiCulture);
        if (this._binder != null)
            this._binder.set_uiCulture(this._uiCulture);
    },
    // Gets the culture that will be used on the server as UICulture when processing the request
    get_uiCulture: function () {
        return this._uiCulture;
    },
    // Gets or sets the handlers that are called for translation commands
    get_translationHandlers: function () {
        return this._translationHandlers;
    },
    set_translationHandlers: function (value) {
        this._translationHandlers = value;
    },
    // Gets or sets the selector that will be used to hide not translated items
    get_itemsToHideSelector: function () {
        return this._itemsToHideSelector;
    },
    set_itemsToHideSelector: function (value) {
        this._itemsToHideSelector = value;
    },
    // Gets or sets the selector that will be used to bind the edit handler
    get_itemTitleSelector: function () {
        return this._itemTitleSelector;
    },
    set_itemTitleSelector: function (value) {
        this._itemTitleSelector = value;
    },

    getFormattedBackLabel: function (backText) {
        return String.format(this._backToLabel, backText);
    },
    get_titleText: function () {
        return this._titleText;
    },
    set_titleText: function (value) {
        this._titleText = value;
    },

    get_reloadDialogs: function () {
        return this._reloadDialogs;
    },
    set_reloadDialogs: function (value) {
        this._reloadDialogs = value;
    },

    get_translationDelegate: function () {
        return this._translationDelegate;
    },

    get_contentLocationPreviewUrl: function () {
        return this._contentLocationPreviewUrl;
    },
    set_contentLocationPreviewUrl: function (val) {
        if (this._contentLocationPreviewUrl != val) {
            this._contentLocationPreviewUrl = val;
        }
    },
    get_filterExpression: function () {
        return this._filterExpression;
    },

    get_currentSiteId: function () {
        return this._currentSiteId;
    },
    set_currentSiteId: function (value) {
        this._currentSiteId = value;
    },

    get_recycleBinServiceUrl: function () {
        return this._recycleBinServiceUrl;
    },
    set_recycleBinServiceUrl: function (value) {
        this._recycleBinServiceUrl = value;
    },

    get_deletedItems: function () {
        return this._deletedItems;
    },

    get_recyclableContentItemType: function () {
        return this._recyclableContentItemType;
    },
    set_recyclableContentItemType: function (value) {
        this._recyclableContentItemType = value;
    }
}
Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.ItemsListBase', Sys.UI.Control);

// not used: probably should be moved to ClientManager
//// ------------------------------------------------------------------------
//// AppendUrlParamsEventArgs class
//// ------------------------------------------------------------------------
//Telerik.Sitefinity.Web.UI.ItemLists.AppendUrlParamsEventArgs = function(command, params) {
//    this._commandName = command;
//    this._urlParams = params;
//    Telerik.Sitefinity.Web.UI.ItemLists.ClientBinderEventArgs.initializeBase(this);
//}
//Telerik.Sitefinity.Web.UI.ItemLists.AppendUrlParamsEventArgs.prototype = {
//    // ------------------------------------------------------------------------
//    // Set-up
//    // ------------------------------------------------------------------------
//    initialize: function() {
//        Telerik.Sitefinity.Web.UI.ItemLists.ClientBinderEventArgs.callBaseMethod(this, 'initialize');
//    },
//    dispose: function() {
//        Telerik.Sitefinity.Web.UI.ItemLists.ClientBinderEventArgs.callBaseMethod(this, 'dispose');
//    },
//    
//    // ------------------------------------------------------------------------
//    // Public Functions
//    // ------------------------------------------------------------------------
//    hasUrlParam: function(urlParamName) {
//        if (this._urlParams[urlParamName]) {
//            return true;
//        }
//        else {
//            return false;
//        }
//    },
//    setUrlParam: function(urlParamName, value) {
//        this._urlParams[urlParamName] = value;
//    },
//    removeUrlParam: function(urlParamName) {
//        if (this.hasUrlParam(urlParamName)) {
//            delete this._urlParams[urlParamName];
//        }
//    },
//    
//    // ------------------------------------------------------------------------
//    // Properties
//    // ------------------------------------------------------------------------
//    get_commandName: function() {
//        return this._commandName;
//    },
//    get_urlParams: function() {
//        return this._urlParams;
//    }    
//}
//Telerik.Sitefinity.Web.UI.ItemLists.AppendUrlParamsEventArgs.registerClass("Telerik.Sitefinity.Web.UI.ItemLists.AppendUrlParamsEventArgs", Sys.EventArgs);

// ------------------------------------------------------------------------
// DataBoundEventArgs class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs = function (dataItem) {
    this._dataItem = dataItem;
    Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_dataItem: function () {
        return this._dataItem;
    }
}
Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs.registerClass("Telerik.Sitefinity.Web.UI.ItemLists.DataBoundEventArgs", Sys.EventArgs);

// ------------------------------------------------------------------------
// Command event args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs = function (commandName, commandArgument) {
    Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.initializeBase(this);
    //HACK: check if the commandArgument is a CommandEventArgs object.
    if (commandArgument && commandArgument.get_commandName && commandArgument.get_commandArgument && commandArgument.get_cancel) {
        this._commandName = commandArgument.get_commandName();
        this._commandArgument = commandArgument.get_commandArgument();
        this.set_cancel(commandArgument.get_cancel());
    }
    else {
        this._commandName = commandName;
        this._commandArgument = commandArgument;
    }
}

Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs', Sys.CancelEventArgs);

// ------------------------------------------------------------------------
// Dialog Event Args
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs =
function (
    commandName,
    dataItem,
    itemsList,
    dialog,
    params,
    key,
    commandArgument
    ) {
    this._commandName = commandName;
    this._dataItem = dataItem;
    this._itemsList = itemsList;
    this._dialog = dialog;
    this._params = params;
    this._key = key;
    this._commandArgument = commandArgument;
    Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs.initializeBase(this);
}

Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.CommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_itemsList: function () {
        return this._itemsList;
    },
    get_params: function () {
        return this._params;
    },
    get_key: function () {
        return this._key;
    },
    get_dialog: function () {
        return this._dialog;
    },
    get_commandArgument: function () {
        return this._commandArgument;
    }
};
Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.DialogEventArgs', Sys.CancelEventArgs);

// ------------------------------------------------------------------------
// LinkEventArgs class
// ------------------------------------------------------------------------
Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs = function (commandName, dataItem, itemsList, link, navigateUrl) {
    this._commandName = commandName;
    this._dataItem = dataItem;
    this._itemsList = itemsList;
    this._link = link;
    this._navigateUrl = navigateUrl;
    Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs.prototype = {
    // ------------------------------------------------------------------------
    // Set-up
    // ------------------------------------------------------------------------
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        this._link = null;
        Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs.callBaseMethod(this, 'dispose');
    },

    // ------------------------------------------------------------------------
    // Properties
    // ------------------------------------------------------------------------
    get_dataItem: function () {
        return this._dataItem;
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_itemsList: function () {
        return this._itemsList;
    },
    get_link: function () {
        return this._link;
    },
    get_navigateUrl: function () {
        return this._navigateUrl;
    }
}
Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs.registerClass("Telerik.Sitefinity.Web.UI.ItemLists.LinkEventArgs", Sys.CancelEventArgs);

// ------------------------------------------------------------------------
// Helper functions
// ------------------------------------------------------------------------

function getItemsListOnPage(pageHandle) {
    /// <summary>
    /// If there is an instance of ItemsListBase on this page, this function will return it.
    /// If there is more than one instance, this will call the last-registered one.
    /// </summary>
    /// <param name="pageHandle">window object for the page to get the ItemsListBase. This parameter is optional.</param>
    /// <returns>Instance of ItemsListBase or null if not found</returns>
    if (!pageHandle) {
        return window.__Telerik_Sitefinity_Web_UI_ItemLists_ItemsListBase_Instance;
    }
    else {
        return pageHandle.__Telerik_Sitefinity_Web_UI_ItemLists_ItemsListBase_Instance;
    }
}
Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs = function (commandName, language, dataItem, key, list, element) {
    Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs.initializeBase(this);
    //dataItem, key, containerElement, null, { language: lang, languageMode: mode }
    this._commandName = commandName;
    this._language = language;
    this._dataItem = dataItem;
    this._key = key;
    this._element = element;
    this._list = list;
}

Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs.callBaseMethod(this, 'dispose');
    },
    get_commandName: function () {
        return this._commandName;
    },
    get_language: function () {
        return this._language;
    },
    get_dataItem: function () {
        return this._dataItem;
    },
    get_key: function () {
        return this._key;
    },
    get_element: function () {
        return this._element;
    },
    get_list: function () {
        return this._list;
    }
};
Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.TranslationCommandEventArgs', Sys.CancelEventArgs);

Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs = function (originalSender, originalArgs) {
    Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs.initializeBase(this);

    this._originalSender = originalSender;
    this._originalArgs = originalArgs;
}

Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs.callBaseMethod(this, 'dispose');
    },
    get_originalSender: function () {
        return this._originalSender;
    },
    get_originalArgs: function () {
        return this._originalArgs;
    }
};
Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs.registerClass('Telerik.Sitefinity.Web.UI.ItemLists.OperationEventArgs', Sys.CancelEventArgs);