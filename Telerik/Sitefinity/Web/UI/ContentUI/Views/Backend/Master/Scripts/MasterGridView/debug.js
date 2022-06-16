﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master");

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView = function (element) {
    this._actionItems = null;
    this._element = element;
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.initializeBase(this, [element]);
    this._decisionScreens = null;
    this._binder = null;
    this._targetGrid = null;
    this._openedDecisionScreen = null;
    this._titleID = null;
    this._toolbarID = null;
    this._sidebarID = null;
    this._sidebar = null;
    this._toolbar = null;
    this._contextBar = null;
    this._widgetIds = [];
    this._currentUserId = null;
    this._hasSidebar = null;
    this._hasToolbar = null;

    //Indicates whether the current user is admin
    this._isAdmin = null;

    this._itemsGrid = null;
    this._itemsList = null;
    this._itemsTreeTable = null;
    this._binderSearch = null;
    this._messageControl = null;
    this._baseServiceUrl = null;

    this._isFirstLoad = true;
    // supported commands
    this._showAllItemsCommandName = null;
    this._showMyItemsCommandName = null;
    this._showDraftItemsCommandName = null;
    this._showPublishedItemsCommandName = null;
    this._showScheduledItemsCommandName = null;
    this._pendingApprovalItemsCommandName = null;
    this._pendingReviewItemsCommandName = null;
    this._pendingPublishingItemsCommandName = null;
    this._awaitingMyActionPagesCommandName = null;
    this._rejectedItemsCommandName = null;
    this._pendingApprovalPagesCommandName = null;
    this._pendingReviewPagesCommandName = null;
    this._pendingPublishingPagesCommandName = null;
    this._rejectedPagesCommandName = null;
    this._filterCommandName = null;
    this._searchCommandName = null;
    this._gridViewStateCommandName = null;
    this._listViewStateCommandName = null;
    this._treeViewStateCommandName = null;
    this._tagFilterCommandName = null;
    this._categoryFilterCommandName = null;
    this._notImplementedCommandName = null;
    this._closeSearchCommandName = null;
    this._parentPropertiesCommandName = null;
    this._showMoreTranslationsCommandName = null;
    this._hideMoreTranslationsCommandName = null;
    this._sortCommandName = null;
    this._showHierarchicalCommandName = null;
    this._showNotSharedCommandName = null;

    this._clientMappedCommnadNames = null;
    this._promptWindowId = null;
    this._providerSelectorId = null;
    this._noEditPermissionsViewDialogTitle = null;
    this._hasBeenLockedForEditingBySince = null;
    this._noPermissionsToSetAsHomepage = null;
    this._since = null;
    this._noEditPermissionsConfirmationMessage = null;
    this._noEditPermissionsPreviewOnlyConfirmationNoViewOption = null;

    this._nonEditableItemToView = null;
    this._nonEditableItemKey = null;
    this._nonEditableItemElement = null;

    this._draftFilter = null;
    this._publishedFilter = null;
    this._scheduledFilter = null;
    this._pendingApprovalFilter = null;

    this._contentLifecycleStatusName = null;
    this._workflowStateName = null;
    this._publishedDraftStatusFilterExpression = null;
    this._publishedDraftWorkflowStateFilterExpression = null;
    this._notPublishedDraftStatusFilterExpression = null;
    this._notPublishedDraftWorkflowStateFilterExpression = null;
    this._scheduledItemsStatusFilterExpression = null;
    this._scheduledItemsWorkflowStateFilterExpression = null;
    this._pendingApprovalItemsStatusFilterExpression = null;
    this._pendingApprovalItemsWorkflowStateFilterExpression = null;
    this._showHierarchicalExpression = null;
    this._sortExpression = null;

    this._lockWindowId = null;
    this._lockedItemToView = null;
    this._lockedItemKey = null;
    this._lockedItemElement = null;
    this._parentId = null;
    this._baseViewPath = null;

    //Holds the widget that was last used to filter
    this._previousFilterWidget = null;
    this._selectedItemFilterCssClass = null;
    this._localization = "";
    this._supportsMultilingual = false;
    this._supportsApprovalWorkflow = false;
    this._defaultLanguage = null;
    this._definedLanguages = [];
    this._titleText = null;
    this._parentTitleFormat = null;

    this.cookieKey = null;

    this._clientLabelManager = null;
    this._managerType = null;
    this._customParameters = null;
    this._doNotBindOnClientWhenPageIsLoaded = null;
    this._currentSiteId = null;
    this._siteIdParamKey = null;
    this._appPath = "";
    this._currentItemsList = null;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.callBaseMethod(this, "initialize");
        window.masterGridView = this;
        if (this._decisionScreens) {
            this._decisionScreens = Sys.Serialization.JavaScriptSerializer.deserialize(this._decisionScreens);
        }
        if (this._widgetIds && this._widgetIds.length > 0) {
            this._widgetIds = Sys.Serialization.JavaScriptSerializer.deserialize(this._widgetIds);
        }

        if (this._customParameters) {
            this._customParameters = Sys.Serialization.JavaScriptSerializer.deserialize(this._customParameters);
        }

        this._handleCommandDelegate = Function.createDelegate(this, this._handleCommand);
        this._handleDialogClosedDelegate = Function.createDelegate(this, this._handleDialogClosed);
        this._handleItemCommandDelegate = Function.createDelegate(this, this._handleItemCommand)
        this._handleBinderDataBoundDelegate = Function.createDelegate(this, this._handleBinderDataBound);
        this._handleBinderItemDataBoundDelegate = Function.createDelegate(this, this._handleBinderItemDataBound);
        this._handleDecisionScreenCommandDelegate = Function.createDelegate(this, this._handleDecisionScreenCommand);
        this._handleWidgetCommandDelegate = Function.createDelegate(this, this._handleWidgetBarCommand);
        this._handleItemSelectedDelegate = Function.createDelegate(this, this._handleItemSelected);
        this._clientMappedCommnadNames = Sys.Serialization.JavaScriptSerializer.deserialize(this._clientMappedCommnadNames);
        this._listItemCommandDelegate = Function.createDelegate(this, this._interceptListItemCommandAndLaunchAlternative);
        this._listItemSelectionChangedDelegate = Function.createDelegate(this, this._listItemSelectionChanged);
        this._itemsDeletedDelegate = Function.createDelegate(this, this._itemsDeletedHandler);
        this._messageControlCommandDelegate = Function.createDelegate(this, this._messageControlCommandHandler);
        this._deletedItemsRestoredDelegate = Function.createDelegate(this, this._deletedItemsRestoredHandler);

        this._localization = Sys.Serialization.JavaScriptSerializer.deserialize(this._localization);
        this._providerSelectorClickedDelegate = Function.createDelegate(this, this._handleProviderSelectorClicked);
    },

    dispose: function () {
        if (this._binder) {
            this._binder.remove_onItemCommand(this._handleItemCommandDelegate);
            this._binder.remove_onDataBound(this._handleBinderDataBoundDelegate);
            this._binder.remove_onItemDataBound(this._handleBinderItemDataBoundDelegate);
            this._binder.remove_onItemSelectCommand(this._handleItemSelectedDelegate);
        }
        if (this._sidebar) {
            this._sidebar.remove_command(this._handleSidebarCommandDelegate);
        }
        if (this._toolbar) {
            this._toolbar.remove_command(this._handleWidgetCommandDelegate);
        }
        if (this._contextBar) {
            this._contextBar.remove_command(this._handleWidgetCommandDelegate);
        }
        if (this.get_currentItemsList()) {
            this.get_currentItemsList().remove_itemsDeleted(this._itemsDeletedDelegate);
            this.get_currentItemsList().remove_deletedItemsRestored(this._deletedItemsRestoredDelegate);
        }

        if (this.get_messageControl()) {
            this.get_messageControl().remove_command(this._messageControlCommandDelegate);
        }

        this._handleCommandDelegate = null;
        this._handleItemCommandDelegate = null;
        this._handleItemCommandDelegate = null;
        this._handleBinderDataBoundDelegate = null;
        this._handleBinderItemDataBoundDelegate = null;
        this._handleDecisionScreenCommandDelegate = null;
        this._handleSidebarCommandDelegate = null;
        this._handleWidgetCommandDelegate = null;
        this._handleItemSelectedDelegate = null;
        this._handleCustomDelegate = null;
        this._listItemCommandDelegate = null;
        this._listItemSelectionChangedDelegate = null;
        this._providerSelectorClickedDelegate = null;
        this._itemsDeletedDelegate = null;
        this._messageControlCommandDelegate = null;
        this._deletedItemsRestoredDelegate = null;

        this._decisionScreens = null;
        this._itemsGrid = null;
        this._itemsList = null;
        this._itemsTreeTable = null;
        this._targetGrid = null;

        if (this._widgetCommandDelegate) {
            var wLength = this._widgetIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetIds[wCounter]);
                if (widget !== null) {
                    widget.remove_command(this._widgetCommandDelegate);
                }
            }
            delete this._widgetCommandDelegate;
        }

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods --------------------  */
    findDecisionScreen: function (key) {
        if (this._decisionScreens) {
            var dsID = this._decisionScreens[key];
            if (dsID) {
                return $find(dsID);
            }
        }
        return null;
    },

    // Happens when selection of the pages is changed (node is checked or unchecked)
    add_selectionChanged: function (delegate) {
        this.get_events().addHandler("selectionChanged", delegate);
    },
    // Happens when selection of the pages is changed (node is checked or unchecked)
    remove_selectionChanged: function (delegate) {
        this.get_events().removeHandler("selectionChanged", delegate);
    },

    get_selectedItems: function () {
        return this._currentItemsList.get_selectedItems();
    },

    /* -------------------- events -------------------- */

    add_itemCommand: function (handler) {
        this.get_events().addHandler('itemCommand', handler);
    },

    remove_itemCommand: function (handler) {
        this.get_events().removeHandler('itemCommand', handler);
    },

    _raiseSelectionChanged: function (selectedItems) {
        var eventArgs = selectedItems;
        var handler = this.get_events().getHandler("selectionChanged");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* -------------------- event handlers -------------------- */
    _handlePageLoad: function (sender, args) {

        if (this._itemsGrid != null) {
            this._bindAllEvents(this._itemsGrid);
            this._binder = this._itemsGrid._binder;
        }
        if (this._itemsList != null) {
            this._bindAllEvents(this._itemsList);
            this._binder = this._itemsList._binder;
        }
        if (this._itemsTreeTable != null) {
            this._bindAllEvents(this._itemsTreeTable);
            this._binder = this._itemsTreeTable._binder;
        }

        this._hideAllItemsLists();

        //TODO refactor this. When the TemplateEvalutionMode is client the currentItemsList should be configurable on the server.
        // The check for itemstTreeTable is placed first in order to bind the correct list for pages.
        if (this._itemsTreeTable) {
            this.set_currentItemsList(this._itemsTreeTable);
            this._subscribeItemsListBaseEvents(this._itemsTreeTable);
        }
        else
            if (this._itemsGrid) {
                this.set_currentItemsList(this._itemsGrid);
                this._subscribeItemsListBaseEvents(this._itemsGrid);
            }
            else if (this._itemsList) {
                this.set_currentItemsList(this._itemsList)
                this._subscribeItemsListBaseEvents(this._itemsList);
            }
        if (this._currentItemsList != null) {
            this._currentItemsList.set_titleText(this._titleText);

            //        this._currentItemsList.add_command(this._handleCommandDelegate);
            //        this._currentItemsList.add_dialogClosed(this._handleDialogClosedDelegate);

            //        this._currentItemsList.add_itemCommand(this._listItemCommandDelegate);

            if (this._sidebar) {
                this._sidebar.add_command(this._handleWidgetCommandDelegate);
            }

            if (this._toolbar) {
                this._toolbar.add_command(this._handleWidgetCommandDelegate);
            }

            if (this._contextBar) {
                this._contextBar.add_command(this._handleWidgetCommandDelegate);
            }

            var providerSelector = $find(this._providerSelectorId);
            if (providerSelector) {
                providerSelector.add_onProviderSelected(this._providerSelectorClickedDelegate);
                this._currentItemsList.set_providerName(providerSelector.get_selectedProvider());
            }

            this._subscribeToWidgetEvents();

            if (false == this._doNotBindOnClientWhenPageIsLoaded) {
                this._currentItemsList.dataBind();
            }

            this._executeCommandFromUrl();

            this._subscribeToMessageControlCommands();
        }

        Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.callBaseMethod(this, "_handlePageLoad");
    },

    _listItemSelectionChanged: function (sender, eventArgs) {
        var selectedItems = eventArgs;
        this._raiseSelectionChanged(eventArgs);
    },

    _itemsDeletedHandler: function (sender, eventArgs) {
        if (!this.get_currentItemsList().get_recycleBinEnabled()) {
            return;
        }

        var deletedItemsCount = sender.get_deletedItems().length;

        if (deletedItemsCount === 0) {
            return;
        }

        var undoLabel = this.get_clientLabelManager().getLabel("Labels", "UndoLabel");
        var message;

        var isSingle = deletedItemsCount === 1;

        if (isSingle) {
            message = this.get_clientLabelManager().getLabel("Labels", "UndoDeleteMessageSingle");
        }
        else {
            message = this.get_clientLabelManager().getLabel("Labels", "UndoDeleteMessagePlural");
        }

        message += " | ";

        var commands = [{
            CommandName: "undoDeletedItems",
            Title: undoLabel,
            CssClass: "sfMoreDetails"
        }];

        this.get_messageControl().showNeutralMessage(message, commands);
    },

    _deletedItemsRestoredHandler: function () {
        this.get_messageControl().hide();
    },

    _interceptListItemCommandAndLaunchAlternative: function (sender, args) {

        var originalCommandName = args.get_commandName();
        var dataItem = args.get_commandArgument();

        if (!dataItem) {
            return;
        }

        if ((originalCommandName == "edit") ||
            ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("edit")) &&
                (this._clientMappedCommnadNames["edit"] == originalCommandName))) {

            if (!this._isEditable(dataItem)) {
                args.set_cancel(true);

                this._nonEditableItemToView = dataItem;
                this._nonEditableItemKey = [];
                this._nonEditableItemKey[sender.get_keys()[0]] = dataItem[sender.get_keys()[0]];
                this._nonEditableItemElement = sender.get_element();

                var oPDialog = $find(this._promptWindowId);
                var itemTitle = dataItem.Title;
                if ((typeof (dataItem.Title.Value) != typeof (undefined)) && (dataItem.Title.Value != null) && (dataItem.Title.Value != "")) {
                    itemTitle = dataItem.Title.Value;
                }
                oPDialog.set_title(String.format(this._noEditPermissionsViewDialogTitle, itemTitle));

                var previewCommand = "viewProperties";
                var dialogText = this._noEditPermissionsPreviewOnlyConfirmationNoViewOption;
                if ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("preview"))) {
                    previewCommand = this._clientMappedCommnadNames["preview"];
                }
                var previewWin = window.GetRadWindowManager().getWindowByName(previewCommand);

                if (previewWin != null) {

                    dialogText = this._noEditPermissionsConfirmationMessage;
                    oPDialog.setButtonDisplay("view", true);
                }
                else {
                    oPDialog.setButtonDisplay("view", false);
                }
                oPDialog.set_textFieldTitle(dialogText);
                oPDialog.show_prompt(null, dialogText, Function.createDelegate(this, this._handleUnEditableObject));
            }
            else {

                if (dataItem.LifecycleStatus && this._isLockedBySomebodyElse(dataItem)) {
                    args.set_cancel(true);
                    this._lockedItemToView = dataItem;
                    this._lockedItemKey = [];
                    this._lockedItemKey["Id"] = dataItem.Id;

                    var oLDialog = $find(this._lockWindowId);

                    if (dataItem.IsUnlockable) {
                        oLDialog.setButtonDisplay("unlock", true);
                    }
                    else {
                        oLDialog.setButtonDisplay("unlock", false);
                    }

                    var editCommand = "edit";
                    if ((this._clientMappedCommnadNames != null) && (this._clientMappedCommnadNames.hasOwnProperty("edit"))) {
                        editCommand = this._clientMappedCommnadNames["edit"];
                    }

                    oLDialog.set_textFieldTitle(String.format(this._hasBeenLockedForEditingBySince, dataItem.Title, dataItem.LifecycleStatus.LockedByUsername, dataItem.LifecycleStatus.LockedSince.sitefinityLocaleFormat("f")));
                    //oLDialog.setButtonDisplay("view", true);

                    oLDialog.show_prompt(null, null, Function.createDelegate(this, this._handleLockedObject));
                }
            }
        }
    },

    _isLockedBySomebodyElse: function (item) {

        if (item.LifecycleStatus && item.LifecycleStatus.IsLocked == true && item.LifecycleStatus.IsLockedByMe == false) {
            return true;
        }
        else {
            return false;
        }
    },

    _handleLockedObject: function (sender, args) {
        if (args.get_commandName() != "cancel") {

            if (args.get_commandName() == "view") {
                var previewCommandName = "viewProperties";
                if ((this._clientMappedCommnadNames) && (this._clientMappedCommnadNames.hasOwnProperty("preview")))
                    previewCommandName = this._clientMappedCommnadNames["preview"];
                this._currentItemsList.executeCommand(previewCommandName, this._lockedItemToView, this._lockedItemKey, null);
            }
            if (args.get_commandName() == "unlock") {
                var editCommandName = "edit";
                if ((this._clientMappedCommnadNames) && (this._clientMappedCommnadNames.hasOwnProperty("edit")))
                    editCommandName = this._clientMappedCommnadNames["edit"];

                this._currentItemsList.unlockItem(this._lockedItemToView);
                //this._currentItemsList.executeCommand(editCommandName, this._lockedItemToView, this._lockedItemKey, null);
            }
        }
    },

    _handleUnEditableObject: function (sender, args) {
        if (args.get_commandName() != "cancel") {
            var previewCommandName = "viewProperties";
            if ((this._clientMappedCommnadNames) && (this._clientMappedCommnadNames.hasOwnProperty("preview")))
                previewCommandName = this._clientMappedCommnadNames["preview"];

            this._currentItemsList.executeCommand(previewCommandName, this._nonEditableItemToView, this._nonEditableItemKey, null);
        }
    },

    _isViewable: function (item) {
        var isViewable = true;
        if ((typeof (item.IsViewable) != typeof (undefined)) && (item.IsViewable != null) && (!item.IsViewable)) {
            isViewable = false;
        }
        return isViewable;
    },

    _isEditable: function (item) {
        var isEditable = true;
        if ((typeof (item.IsEditable) != typeof (undefined)) && (item.IsEditable != null) && (!item.IsEditable)) {
            isEditable = false;
        }
        return isEditable;
    },

    _handleBinderItemDataBound: function (sender, args) {
        var commandLinks = $(args.get_itemElement()).find("[class*='sf_binderCommand']");
        var item = args.get_dataItem();
        var itemIsEditable = item.IsEditable;
        var isPublishable = false;
        var isUnpublishable = false;
        var list = this.get_currentItemsList();

        if (item.WorkflowOperations) {
            for (var curOp = 0; curOp < item.WorkflowOperations.length; curOp++) {
                if (item.WorkflowOperations[curOp].OperationName == "Publish") {
                    isPublishable = true;
                }
                if (item.WorkflowOperations[curOp].OperationName == "Unpublish") {
                    isUnpublishable = true;
                }
            }
        } else {
            isPublishable = true;
            isUnpublishable = true;
        }

        //fix for 69684: hide "create post" command links if the item is not editable
        for (var curCmd = 0; curCmd < commandLinks.length; curCmd++) {
            var cmdNameMatch = commandLinks[curCmd].className.match(/sf_binderCommand_(\S*)/i);
            if (cmdNameMatch && cmdNameMatch[1]) {
                var commandName = cmdNameMatch[1];
                if ((!itemIsEditable) && (commandName == "createPost")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isPublishable) && (commandName == "publish")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isPublishable) && (commandName == "publishDraft")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!isUnpublishable) && (commandName == "unpublish")) {
                    commandLinks[curCmd].style.display = "none";
                }

                if ((!itemIsEditable) && (commandName == "setAsHomepage")) {
                    commandLinks[curCmd].style.display = "none";
                }
            }
        }
        if (item.LifecycleStatus) {
            var isPublished = item.LifecycleStatus.IsPublished;
            var commands;
            if (isPublished)
                commands = ["sf_binderCommand_publish"];
            else
                commands = ["sf_binderCommand_unpublish"];
            list.removeActionsMenuItems(commands, args.get_itemElement());
        }
        this.setTranslations(item, list, args.get_key(), args.get_itemElement());
    },

    setTranslations: function (item, list, key, element) {
        var availableLangs = item.AvailableLanguages;
        if (availableLangs && availableLangs.length > 0) {
            list.setTranslations(item, key, availableLangs, element);
        } else if (item.HasTranslationSiblings) {
            list.setTranslations(item, key, [], element);
        }
    },

    _handleBinderDataBound: function (sender, args) {
        this._hideOpendDecisionScreen();
        if (sender.get_hasNoData()) {
            if (sender.get_isFiltering()) {
                this._openedDecisionScreen = this.findDecisionScreen("NoItemsDisplayed");
            }
            else {
                this._openedDecisionScreen = this.findDecisionScreen("NoItemsExist");
            }
            if (this._openedDecisionScreen != null) {
                this._openedDecisionScreen.show();
                $(this.get_element()).show();
                $("body").addClass("sfEmpty");
                this._openedDecisionScreen.add_actionCommand(this._handleDecisionScreenCommandDelegate);
                this._currentItemsList.hide();
            }
        }
        this._bindDynamicWidgetsInSidebar();
        this._setToolbarButtonsEnabledState();
        if (this._isFirstLoad) {
            $(this.get_element()).show();
            if (this.get_hasSidebar())
                $("body").addClass("sfHasSidebar").removeClass("sfNoSidebar");
            else
                $("body").removeClass("sfHasSidebar").addClass("sfNoSidebar");
        }
        this.isFirstLoad = false;
    },

    _bindDynamicWidgetsInSidebar: function (sender, args) {
        if (this.get_sidebar()) {
            var widgetsInSidebar = this.get_sidebar()._widgets;
            if (widgetsInSidebar) {
                for (var i = 0; i < widgetsInSidebar.length; i++) {
                    if (Object.getTypeName(widgetsInSidebar[i]) == "Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.DynamicCommandWidget") {
                        widgetsInSidebar[i].getBinder().set_uiCulture(this.get_binder().get_uiCulture());
                        widgetsInSidebar[i].dataBind();
                    }
                }

            }
        }
    },

    _handleCommand: function (sender, args) {
        var commandName = args.get_commandName();
        var argument = args.get_commandArgument();
        // Handle command from the items grid
    },

    _handleDialogClosed: function (sender, args) {
        if ((args.get_isCreated && args.get_isCreated()) || (args.get_isUpdated && args.get_isUpdated())) {
            this._hideOpendDecisionScreen();
        }
        else if (typeof args == "string") {
            var redirect = "redirect:";
            if (args.indexOf(redirect) > -1) {
                var redirectUrl = args.substr(redirect.length);
                if (redirectUrl.length > 0) {
                    window.location.href = String.format("{0}{1}/", this._baseViewPath, redirectUrl);
                }
                else {
                    this._currentItemsList.executeItemCommand("view");
                }
            }
        }
        if (args && args.hasOwnProperty("Message")) {
            if (args.Success)
                this.get_messageControl().showPositiveMessage(args.Message);
            else
                this.get_messageControl().showNegativeMessage(args.Message);
        }
    },

    _navigateTo: function (url) {
        if (window.location.href == url) return;
        window.location.href = url;
    },

    _handleProviderSelectorClicked: function (sender, args) {
        var binder = this.get_binder();
        var providerName = args.ProviderName;

        var url = window.location.href;
        var qIndex = url.lastIndexOf("?");
        if (qIndex > 0) {
            var query = url.substring(qIndex + 1);
            var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring(query);
            queryString.set("provider", providerName);
            this._navigateTo((url.substring(0, qIndex)) + queryString.toString(true));
        }
        else {
            this._navigateTo(url + "?provider=" + providerName);
        }

        return;
    },

    // this is a handler for the itemCommand event of the masterGrid view
    // the event should be fired when the binder's itemCommand event is handled
    _itemCommandHandler: function (sender, args) {
        var handler = this.get_events().getHandler('itemCommand');
        if (handler) {
            handler(this, args);
        }
    },

    _handleItemCommand: function (sender, args) {
        var commandName = args.get_commandName();
        var dataItem = args.get_dataItem();
        this._itemCommandHandler(this, args);
    },

    _handleDecisionScreenCommand: function (sender, args) {
        this._currentItemsList._onCommand(args.get_commandName(), args);
    },

    _handleWidgetBarCommand: function (sender, args) {
        var handleCommand = true;
        var commandName = args.get_commandName();
        var isFilterCommand = false;
        var isExternalStatusFilterCommand = false;
        var widget;
        if (args.get_cancel() === false) {
            switch (commandName) {
                case this._showNotSharedCommandName:
                    isFilterCommand = true;
                    this._currentItemsList.applyFilter('[NotShared]');
                    break;
                case this._showAllItemsCommandName:
                    isFilterCommand = true;
                    this._currentItemsList.applyFilter("");
                    break;
                case this._showMyItemsCommandName:
                    isFilterCommand = true;
                    this._currentItemsList.applyFilter("Owner == (" + this._currentUserId + ")");
                    break;
                case this._showDraftItemsCommandName:
                    isFilterCommand = true;
                    //TODO: Fix ASAP, use definitions
                    if (this._supportsApprovalWorkflow) {
                        this._currentItemsList.applyFilter('[Drafts]');
                    }
                    else {
                        this._filterWithLanguageSpecificState(commandName);
                    }
                    break;
                case this._showPublishedItemsCommandName:
                    isFilterCommand = true;
                    //TODO: Fix ASAP, use definitions
                    if (this._supportsApprovalWorkflow) {
                        this._currentItemsList.applyFilter('[PublishedDrafts]');
                    }
                    else {
                        this._filterWithLanguageSpecificState(commandName);
                    }
                    break;
                case this._showScheduledItemsCommandName:
                    isFilterCommand = true;
                    //TODO: Fix ASAP, use definitions
                    if (this._supportsApprovalWorkflow) {
                        this._currentItemsList.applyFilter('[ScheduledDrafts]');
                    }
                    else {
                        this._filterWithLanguageSpecificState(commandName);
                    }
                    break;
                case this._pendingApprovalItemsCommandName:
                    isFilterCommand = true;
                    this._filterWithLanguageSpecificState(commandName);
                    break;
                case this._pendingReviewItemsCommandName:
                    isFilterCommand = true;
                    this._filterWithLanguageSpecificState(commandName);
                    break;
                case this._pendingPublishingItemsCommandName:
                    isFilterCommand = true;
                    this._filterWithLanguageSpecificState(commandName);
                    break;
                case this._rejectedItemsCommandName:
                    isFilterCommand = true;
                    this._filterWithLanguageSpecificState(commandName);
                    break;
                case this._filterCommandName:
                    isFilterCommand = true;
                    this._currentItemsList.applyFilter(args.get_commandArgument().filterExpression);
                    args.set_cancel(true);
                    break;
                case this._searchCommandName:
                    isFilterCommand = true;
                    widget = this._sidebar.getWidgetByCommandName(this._showAllItemsCommandName);
                    this._binderSearch.search(args.get_commandArgument().get_query());
                    args.set_cancel(true);
                    break;
                case this._closeSearchCommandName:
                    this._currentItemsList.applyFilter("");
                    args.set_cancel(true);
                    break;
                case this._gridViewStateCommandName:
                    this.set_currentItemsList(this._itemsGrid);
                    break;
                case this._listViewStateCommandName:
                    this.set_currentItemsList(this._itemsList);
                    break;
                case this._treeViewStateCommandName:
                    this.set_currentItemsList(this._itemsTreeTable);
                    break;
                case this._parentPropertiesCommandName:
                    var dataObject = { Id: this._parentId }; // used as a data item and a keys collection
                    this._currentItemsList.executeCommand(commandName, dataObject, dataObject, null, null);
                    args.set_cancel(true);
                    break;
                case this._showMoreTranslationsCommandName:
                case this._hideMoreTranslationsCommandName:
                    var hide = commandName == this._hideMoreTranslationsCommandName;
                    var widgetToShow = "HideMoreTranslations";
                    if (hide) widgetToShow = "ShowMoreTranslations";
                    widgetToShow = sender.getWidgetByName(widgetToShow);
                    this._currentItemsList.showHideTranslationCommands(hide, this._element);
                    $(widgetToShow.get_element().parentNode).removeClass("sfDisplayNone");
                    $(args.Widget.get_element().parentNode).addClass("sfDisplayNone");
                    break;

                case 'stateButtonClicked':
                    var queryString = new Telerik.Sitefinity.Web.SitefinityJS.Utility.Querystring();
                    var viewState = args._commandArgument;
                    var oldUriMode = "";
                    var oldUriProvider = "";
                    var uriMode = "";
                    var newMode = "";
                    var uriProvider = "";
                    var newUri = "";
                    var baseHref = window.location.href;
                    var binderProvider = this._binder._provider;

                    if (viewState == "gridViewState") {
                        uriMode = "mode=Grid";
                        newMode = "Grid";
                        Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, "mode", newMode);
                    }

                    if (viewState == "listViewState") {
                        uriMode = "mode=List";
                        newMode = "List";
                        Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, "mode", newMode);
                    }

                    if (binderProvider) {
                        uriProvider = "provider=" + binderProvider;
                    }

                    if (queryString.contains("mode")) {
                        oldUriMode = "mode=" + queryString.get("mode");
                        baseHref = baseHref.replace(oldUriMode, uriMode);
                    }
                    else {
                        if (baseHref.indexOf('?') == -1) {
                            baseHref += "?mode=" + newMode;
                        }
                        else {
                            baseHref += "&mode=" + newMode;
                        }
                    }

                    if (queryString.contains("provider")) {
                        oldUriProvider = "provider=" + queryString.get("provider");
                        baseHref = baseHref.replace(oldUriProvider, uriProvider);
                    }
                    else {
                        if (binderProvider) {
                            if (baseHref.indexOf('?') == -1) {
                                baseHref += "?provider=" + binderProvider;
                            }
                            else {
                                baseHref += "&provider=" + binderProvider;
                            }
                        }
                    }
                    window.location.href = baseHref;
                    break;

                case this._tagFilterCommandName:
                    alert("In process of implementation.");
                    break;
                case this._categoryFilterCommandName:
                    alert("In process of implementation.");
                    break;
                case 'filterByTag':
                    isFilterCommand = true;
                    break;
                case 'filterByCategory':
                    isFilterCommand = true;
                    break;
                case this._sortCommandName:
                    // set property value in the cookie
                    Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, this._sortCommandName, args.get_commandArgument());
                    Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, this._showHierarchicalCommandName, null);
                    this._currentItemsList.applySorting(args.get_commandArgument());
                    break;
                case this._showHierarchicalCommandName:
                    Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, this._showHierarchicalCommandName, args.get_commandArgument());
                    Telerik.Sitefinity.setPropertyValueInCookie(this._cookieKey, this._sortCommandName, null);
                    break;
                //Filter commands for pages
                case 'showAllPages':
                case 'showMyPages':
                case 'showAllTemplates':
                case 'showMyTemplates':
                case 'showPublishedPages':
                case 'showDraftPages':
                case 'showScheduledPages':
                case this._pendingApprovalPagesCommandName:
                case this._pendingReviewPagesCommandName:
                case this._pendingPublishingPagesCommandName:
                case this._rejectedPagesCommandName:
                case this._awaitingMyActionPagesCommandName:
                case 'showPagesWithNoTitles':
                case 'showPagesWithNoKeywords':
                case "showPagesWithNoDescriptions":
                case "customDateFilterCommand":

                //Filter commands for comments
                case "ShowAllComments":
                case "ShowTodayComments":
                case "ShowHiddenComments":
                case "ShowPublishedComments":
                case "ShowSpamComments":

                //Filter for feeds
                case "activeFeeds":
                case "inactiveFeeds":
                case "filterContent":

                    //Set that this is a filter command so that highlighting is handled for the widget
                    isFilterCommand = true;
                    break;

                //             case this._notImplementedCommandName:
                //                 alert("In process of implementation.");
                //                 break;
                default:
                    if (commandName.substring(0, 'sf_status_filter'.length) == 'sf_status_filter') {
                        isFilterCommand = true;
                        isExternalStatusFilterCommand = true;
                    }
                    break;
            }
        }



        if (!isFilterCommand) {
            if (typeof args.isFilterCommand == "boolean" && args.isFilterCommand == true) {
                isFilterCommand = true;
            }
        }

        //Manage highlight of selected filter
        if (isFilterCommand == true) {
            widget = widget || args.Widget;
            if (args.get_commandArgument && (cmdArg = args.get_commandArgument())) {
                if (cmdArg.get_itemElement) {
                    var linkElm = null;
                    var elm = cmdArg.get_itemElement();
                    linkElm = jQuery(elm).find(".sf_binderCommand_" + commandName).get(0);
                    widget = { LinkElement: linkElm };
                }
            }

            if (widget && this._selectedItemFilterCssClass) {
                //Unselect previous widget
                if (this._previousFilterWidget) {
                    this._markItemSelected(this._previousFilterWidget, false);
                }
                else {
                    if (sender.getAllWidgets) {
                        var widgets = sender.getAllWidgets();
                        for (var i = 0; i < widgets.length; i++) {
                            var w = widgets[i];
                            this._markItemSelected(w, false);
                        }
                    }
                }

                //Select current widget
                this._markItemSelected(widget, true);

                //Update previous widget with current
                this._previousFilterWidget = widget;
            }
        }

        if (handleCommand) {
            //If the command is changeLanguage, we need to change the UiCulture of all lists so that the language selection is persisted
            if (commandName == "changeLanguage") {
                var argument = args.get_commandArgument();

                if (this._itemsGrid) this._itemsGrid.set_uiCulture(argument);
                if (this._itemsList) this._itemsList.set_uiCulture(argument);
                if (this._itemsTreeTable) this._itemsTreeTable.set_uiCulture(argument);
            }

            this._currentItemsList._onBeforeCommand(commandName, args);
            if (args.get_cancel() == false) {
                var onCommandArgs = this._currentItemsList._onCommand(commandName, args);
                if (!onCommandArgs.get_cancel() && isExternalStatusFilterCommand)
                    this._currentItemsList.applyFilter('[' + commandName + ']');
            }
        }
    },

    _markItemSelected: function (widget, value) {
        if (widget) {
            var element = null;
            if (widget.LinkElement) {
                element = widget.LinkElement;
            }
            else if (widget.get_linkElement) {
                element = widget.get_linkElement();
            }
            if (element) {
                if (value == true) {
                    jQuery(element).addClass(this._selectedItemFilterCssClass);
                }
                else {
                    jQuery(element).removeClass(this._selectedItemFilterCssClass);
                }
            }
        }
    },

    _widgetCommandHandler: function (sender, args) {
        this._currentItemsList._onCommand(args.get_commandName(), args);
    },

    _handleItemSelected: function (grid, args) {
        this._setToolbarButtonsEnabledState();
    },

    /* -------------------- private methods -------------------- */

    // This method checks for a
    _executeCommandFromUrl: function () {
        var serviceUrl = new Sys.Uri(location.href);

        var commandName = serviceUrl.get_query()["command"];
        var providerName = serviceUrl.get_query()["provider"];
        var contentId = serviceUrl.get_query()["contentId"];
        var backLabelText = serviceUrl.get_query()["backLabelText"];

        if (commandName && providerName && contentId && commandName == "edit") {
            var item = { ProviderName: providerName, Id: contentId, IsEditable: true };
            var key = [];
            key["Id"] = contentId;
            var additionalParams = [];
            if (backLabelText) {
                additionalParams["backLabelText"] = unescape(backLabelText);
            }

            this._currentItemsList.executeCommand("edit", item, key, null, additionalParams, null);
        }
    },

    _subscribeToWidgetEvents: function () {
        if (this._widgetIds) {
            var wLength = this._widgetIds.length;
            for (var wCounter = 0; wCounter < wLength; wCounter++) {
                var widget = $find(this._widgetIds[wCounter]);
                if (widget !== null) {
                    if (this._widgetCommandDelegate == null) {
                        this._widgetCommandDelegate = Function.createDelegate(this, this._widgetCommandHandler);
                    }
                    widget.add_command(this._widgetCommandDelegate);
                }
            }
        }
    },

    _subscribeToMessageControlCommands: function () {
        this.get_messageControl().add_command(this._messageControlCommandDelegate);
    },

    _messageControlCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        if (commandName === "undoDeletedItems") {
            this.get_currentItemsList().undoDeletedItems();
        }
    },

    _subscribeItemsListBaseEvents: function (itemsListbase) {
        itemsListbase.add_selectionChanged(this._listItemSelectionChangedDelegate);
        itemsListbase.add_itemsDeleted(this._itemsDeletedDelegate);
        itemsListbase.add_deletedItemsRestored(this._deletedItemsRestoredDelegate);
    },

    _setToolbarButtonsEnabledState: function () {
        var toEnable = this._binder.get_selectedItemsCount && (this._binder.get_selectedItemsCount() > 0);

        $('.sfGroupBtn').each(function () {
            var elements = this.getElementsByTagName('a');
            for (var i = 0; i < elements.length; i++) {
                var el = elements[i];
                if (toEnable) {
                    $telerik.removeCssClasses(el, ['sfDisabledLinkBtn']);
                    //restore the removed postback when link disabled
                    $(el).attr("href", " javascript: __doPostBack('" + el.id + "', '')");
                }
                else {
                    $telerik.addCssClasses(el, ['sfDisabledLinkBtn']);
                    //TODO: this fix removes the link with postback error at FF, but still a  new window is opened when "Middle" mouse button is pressed.
                    //Better aproach would be link to be replaced with span with appropriate styles.
                    //$(el).replaceWith("<span class='" + $(el).css() +  "'>" + $(el).text() + "<span/>");
                    $(el).attr("href", "");
                }
            }
        });
        // Disables Actions menu in the toolbar when there are no selected items
        $('.sfActionsDDL').each(function () {
            var clientId = this.id;
            var menu = $find(clientId);
            if (toEnable || $(this).hasClass('sfAlwaysOn') || $(this).parent().parent().hasClass('sfAlwaysOn')) {
                if (menu) {
                    menu.enable();
                    jQuery(this).find("a.rmDisabled").removeClass("sfDisabledLinkBtn");
                }
            }
            else {
                if (menu) {
                    menu.close();
                    menu.disable();
                    jQuery(this).find("a.rmDisabled").addClass("sfDisabledLinkBtn");
                }
            }
        });
    },

    _hideOpendDecisionScreen: function () {
        if (this._openedDecisionScreen != null) {
            this._openedDecisionScreen.hide();
            $("body").removeClass("sfEmpty");
            this._currentItemsList.show();
            this._openedDecisionScreen.remove_actionCommand(this._handleDecisionScreenCommandDelegate);
            this._openedDecisionScreen = null;
        }
    },

    _hideAllItemsLists: function () {
        if (this._itemsGrid != null) {
            this._itemsGrid.hide();
        }
        if (this._itemsList != null) {
            this._itemsList.hide();
        }
        if (this._itemsTreeTable != null) {
            this._itemsTreeTable.hide();
        }
    },

    _bindAllEvents: function (itemsListBase) {
        itemsListBase.add_command(this._handleCommandDelegate);
        itemsListBase.add_dialogClosed(this._handleDialogClosedDelegate);
        itemsListBase.add_itemCommand(this._listItemCommandDelegate);

        var binder = itemsListBase.getBinder();
        binder.add_onItemCommand(this._handleItemCommandDelegate);
        binder.add_onDataBound(this._handleBinderDataBoundDelegate);
        binder.add_onItemDataBound(this._handleBinderItemDataBoundDelegate);
        binder.add_onItemSelectCommand(this._handleItemSelectedDelegate);
    },

    //Hack that handles filtering items created in monolingual mode.
    //TODO remove when the language to column mapping is done and this bug is fixed.
    _filterWithLanguageSpecificState: function (filterCommand) {
        var filter = "";
        var currentLanguage = this._currentItemsList.get_uiCulture();
        var currentLanguageIsDefault = currentLanguage === this._defaultLanguage;
        var generateFilterExpression = this._supportsMultilingual && currentLanguageIsDefault && this._supportsApprovalWorkflow;
        switch (filterCommand) {
            case this._showDraftItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._notPublishedDraftStatusFilterExpression, this._notPublishedDraftWorkflowStateFilterExpression);
                else
                    filter = this._draftFilter;
                break;
            case this._showPublishedItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._publishedDraftStatusFilterExpression, this._publishedDraftWorkflowStateFilterExpression);
                else
                    filter = this._publishedFilter;
                break;
            case this._showScheduledItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._scheduledItemsStatusFilterExpression, this._scheduledItemsWorkflowStateFilterExpression);
                else
                    filter = this._scheduledFilter;
                break;
            case this._pendingApprovalItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._pendingApprovalItemsStatusFilterExpression, this._pendingApprovalItemsWorkflowStateFilterExpression);
                else
                    filter = this._pendingApprovalFilter;
                break;
            case this._pendingReviewItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._pendingReviewItemsStatusFilterExpression, this._pendingReviewItemsWorkflowStateFilterExpression);
                else
                    filter = this._pendingReviewFilter;
                break;
            case this._pendingPublishingItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._pendingPublishingItemsStatusFilterExpression, this._pendingPublishingItemsWorkflowStateFilterExpression);
                else
                    filter = this._pendingPublishingFilter;
                break;
            case this._rejectedItemsCommandName:
                if (generateFilterExpression)
                    filter = this._generateLanguageSpecificFilterExpression(this._rejectedItemsStatusFilterExpression, this._rejectedItemsWorkflowStateFilterExpression);
                else
                    filter = this._rejectedFilter;
                break;
            default:
                throw "Not supported filtering command: '" + filterCommand + "'";
        }
        this._currentItemsList.applyFilter(filter);
    },

    //TODO remove when the language to column mapping is done and this bug is fixed.
    _generateLanguageSpecificFilterExpression: function (contentLifecycleStatus, workflowState) {
        var filterExpression = "";
        filterExpression += String.format(
            "({0} = {1} AND ({2} = \"{3}\" OR ",
            this._contentLifecycleStatusName,
            contentLifecycleStatus,
            this._workflowStateName,
            workflowState
        );

        filterExpression += this._generateLanguagesNullStatements(this._workflowStateName);
        filterExpression += String.format("{0}[\"\"] = \"{1}\")))", this._workflowStateName, workflowState);
        return filterExpression;
    },

    _generateLanguagesNullStatements: function (field) {
        var filterExpression = "(";
        for (var i = 0, l = this._definedLanguages.length; i < l; i++) {
            var lang = this._definedLanguages[i];
            filterExpression += String.format("{0}[\"{1}\"] = null AND ", field, lang);
        }
        return filterExpression;
    },

    /* -------------------- properties -------------------- */
    get_titleID: function () {
        return this._titleID;
    },

    set_titleID: function (value) {
        this._titleID = value;
    },

    get_titleText: function () {
        return this._titleText;
    },

    set_titleText: function (value) {
        this._titleText = value;
    },

    get_parentTitleFormat: function () {
        return this._parentTitleFormat;
    },

    set_parentTitleFormat: function (value) {
        this._parentTitleFormat = value;
    },

    get_sidebarID: function () {
        return this._sidebarID;
    },

    set_sidebarID: function (value) {
        this._sidebarID = value;
    },

    get_toolbarID: function () {
        return this._toolbarID;
    },

    set_toolbarID: function (value) {
        this._toolbarID = value;
    },

    get_hasSidebar: function () {
        return this._hasSidebar;
    },

    set_hasSidebar: function (value) {
        this._hasSidebar = value;
    },

    get_hasToolbar: function () {
        return this._hasToolbar;
    },

    set_hasToolbar: function (value) {
        this._hasToolbar = value;
    },

    set_sidebar: function (value) {
        this._sidebar = value;
    },

    get_sidebar: function () {
        return this._sidebar;
    },

    set_toolbar: function (value) {
        this._toolbar = value;
    },

    get_toolbar: function () {
        return this._toolbar;
    },

    get_isAdmin: function () {
        return this._isAdmin;
    },

    set_isAdmin: function (value) {
        this._isAdmin = value;
    },

    get_contextBar: function () {
        return this._contextBar;
    },

    set_contextBar: function (value) {
        this._contextBar = value;
    },

    get_decisionScreens: function () {
        return this._decisionScreens;
    },

    set_decisionScreens: function (value) {
        this._decisionScreens = value;
    },

    get_widgetIds: function () {
        return this._widgetIds;
    },

    set_widgetIds: function (value) {
        this._widgetIds = value;
    },

    get_binder: function () {
        return this._binder;
    },

    set_binder: function (value) {
        this._binder = value;
    },

    get_provider: function () {
        return this._binder.get_provider();
    },

    set_provider: function (value) {
        this._binder.set_provider(value);
    },

    get_itemsGrid: function () {
        return this._itemsGrid;
    },
    set_itemsGrid: function (value) {
        this._itemsGrid = value;
    },
    get_itemsList: function () {
        return this._itemsList;
    },
    set_itemsList: function (value) {
        this._itemsList = value;
    },
    get_itemsTreeTable: function () {
        return this._itemsTreeTable;
    },
    set_itemsTreeTable: function (value) {
        this._itemsTreeTable = value;
    },
    get_binderSearch: function () {
        return this._binderSearch;
    },
    set_binderSearch: function (value) {
        this._binderSearch = value;
    },
    get_messageControl: function () {
        return this._messageControl;
    },
    set_messageControl: function (value) {
        this._messageControl = value;
    },
    get_currentItemsList: function () {
        return this._currentItemsList;
    },
    set_currentItemsList: function (value) {
        if (this._currentItemsList != value) {
            this._hideAllItemsLists();
            this._currentItemsList = value;
            this._binder = this._currentItemsList.getBinder();
            this._currentItemsList.show();
            this._binderSearch.set_binder(this._binder);
        }
    },
    get_localization: function () {
        return this._localization;
    },
    set_localization: function (val) {
        this._localization = val;
    },
    get_previousFilterWidget: function () {
        return this._previousFilterWidget;
    },
    set_previousFilterWidget: function (val) {
        this._previousFilterWidget = val;
    },
    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (val) {
        this._clientLabelManager = val;
    },
    get_customParameters: function () {
        return this._customParameters;
    },
    set_customParameters: function (val) {
        this._customParameters = val;
    },
    get_showHierarchicalExpression: function () {
        return this._showHierarchicalExpression;
    },
    set_showHierarchicalExpression: function (val) {
        this._showHierarchicalExpression = val;
    },
    get_sortExpression: function () {
        return this._sortExpression;
    },
    set_sortExpression: function (val) {
        this._sortExpression = val;
    },
    get_sortCommandName: function () {
        return this._sortCommandName;
    },
    set_sortCommandName: function (val) {
        this._sortCommandName = val;
    },
    get_showHierarchicalCommandName: function () {
        return this._showHierarchicalCommandName;
    },
    set_showHierarchicalCommandName: function (val) {
        this._showHierarchicalCommandName = val;
    },
    get_siteIdParamKey: function () {
        return this._siteIdParamKey;
    },
    set_siteIdParamKey: function (value) {
        this._siteIdParamKey = value;
    },
    get_appPath: function () {
        return this._appPath;
    },
    set_appPath: function (value) {
        this._appPath = value;
    },
    get_currentSiteId: function () {
        return this._currentSiteId;
    },
    set_currentSiteId: function (value) {
        this._currentSiteId = value;
    },
    get_parentId: function () {
        return this._parentId;
    }
};

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.MasterGridView", Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.ViewBase);

function getDateTemplate(date, format, resource) {
    if (date != null)
        return String.format("<span class='sfLastActivityDate'>{0} {1}</span>", resource, date.sitefinityLocaleFormat(format));
    return "";
}

function trimUrl(url, strToTrim) {
    var index = url.indexOf(strToTrim);

    if (index > -1) {
        return url.substring(0, index - 1);
    }

    return url;
}

Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.LockItemEventArgs = function (dataItem, dataKey) {
    Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.initializeBase(this);
    var _dataItem = dataItem;
    var _dataKey = dataKey;
};
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.LockItemEventArgs.prototype = {
    "get_dataItem": function () {
        return _dataItem;
    },
    "get_dataKey": function () {
        return _dataKey;
    }
};
Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.LockItemEventArgs.registerClass("Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.LockItemEventArgs", Sys.EventArgs);
