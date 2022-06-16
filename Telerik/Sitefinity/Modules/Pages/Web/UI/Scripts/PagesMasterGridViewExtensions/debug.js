// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    var masterView = sender;
    var extender = new Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension(masterView);
    extender.initialize();
}

Type.registerNamespace("Telerik.Sitefinity.Modules.Pages.Web.UI");

Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension = function (masterView) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.initializeBase(this);
    // Constants
    var COMMAND_CREATE_CHILD = "createChild";
    var COMMAND_DELETE = "delete";
    var COMMAND_PERMISSIONS = "permissions";
    var COMMAND_EDIT = "edit";
    var COMMAND_CHANGE_PAGE_OWNER = "changePageOwner";
    var COMMAND_MOVE_UP = "moveUp";
    var COMMAND_MOVE_DOWN = "moveDown";
    var COMMAND_DUPLICATE = "duplicate";
    var COMMAND_SEND_FOR_TRANSLATION = "sf_send_for_translation";

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsGrid = {};
    this._itemsList = {};
    this._itemsTreeTable = {};

    this._movedItemsCount = 0;
    this._itemsToMoveCount = 0;

    this._actionCommandPrefix = "sf_binderCommand_";
    // commands for tree mode
    this._actionCommandsCssForGroupPages = [this._actionCommandPrefix + COMMAND_CREATE_CHILD, this._actionCommandPrefix + COMMAND_DELETE, this._actionCommandPrefix + COMMAND_PERMISSIONS, this._actionCommandPrefix + COMMAND_EDIT, this._actionCommandPrefix + COMMAND_CHANGE_PAGE_OWNER, this._actionCommandPrefix + COMMAND_MOVE_UP, this._actionCommandPrefix + COMMAND_MOVE_DOWN, this._actionCommandPrefix + COMMAND_DUPLICATE, this._actionCommandPrefix + COMMAND_SEND_FOR_TRANSLATION];
    this._actionCommandsCssForRedirectingPages = [this._actionCommandPrefix + COMMAND_CREATE_CHILD, this._actionCommandPrefix + COMMAND_DELETE, this._actionCommandPrefix + COMMAND_EDIT, this._actionCommandPrefix + COMMAND_PERMISSIONS, this._actionCommandPrefix + COMMAND_CHANGE_PAGE_OWNER, this._actionCommandPrefix + COMMAND_MOVE_UP, this._actionCommandPrefix + COMMAND_MOVE_DOWN, this._actionCommandPrefix + COMMAND_DUPLICATE, this._actionCommandPrefix + COMMAND_SEND_FOR_TRANSLATION];
    // commands for flat mode
    this._actionCommandsCssForGroupPagesFlatMode = [this._actionCommandPrefix + COMMAND_CREATE_CHILD, this._actionCommandPrefix + COMMAND_DELETE, this._actionCommandPrefix + COMMAND_PERMISSIONS, this._actionCommandPrefix + COMMAND_EDIT, this._actionCommandPrefix + COMMAND_CHANGE_PAGE_OWNER, this._actionCommandPrefix + COMMAND_DUPLICATE, this._actionCommandPrefix + COMMAND_SEND_FOR_TRANSLATION];
    this._actionCommandsCssForRedirectingPagesFlatMode = [this._actionCommandPrefix + COMMAND_CREATE_CHILD, this._actionCommandPrefix + COMMAND_DELETE, this._actionCommandPrefix + COMMAND_EDIT, this._actionCommandPrefix + COMMAND_PERMISSIONS, this._actionCommandPrefix + COMMAND_CHANGE_PAGE_OWNER, this._actionCommandPrefix + COMMAND_DUPLICATE, this._actionCommandPrefix + COMMAND_SEND_FOR_TRANSLATION];
    // commands to hide in flat mode
    this._actionCommandsToHideInFlatMode = [this._actionCommandPrefix + COMMAND_MOVE_UP, this._actionCommandPrefix + COMMAND_MOVE_DOWN];

    // Event delegates
    this._dialogClosedDelegate = null;
    this._masterCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._selectionChangedDelegate = null;
    this._itemsDeletedDelegate = null;
    this._workAreaClickDelegate = null;
    this._nodeDraggingDelegate = null;
    this._nodesPlacedDelegate = null;
    this._translationHandlers = null;
    this._editTranslationDelegate = null;
    this._createTranslationDelegate = null;

    this._serviceSuccessDelegate = null;
    this._onItemDomCreatedDelegate = null;
    this._itemsListBinderItemCommandDelegate = null;

    this._clientManager = null;

    this._previousMasterViewModeSelection = null;
    this._userAllowedToCreatePagsOnTheCurrentSite = true;
    this._deleteSingleItem = false;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType = { None: 0, Single: 1, Multiple: 2 };
Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.callBaseMethod(this, 'initialize');

        this._masterCommandDelegate = Function.createDelegate(this, this._masterCommandHandler);
        this._masterBeforeCommandDelegate = Function.createDelegate(this, this._masterBeforeCommandHandler);
        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);

        this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChangedHandler);
        this._itemsDeletedDelegate = Function.createDelegate(this, this._itemsDeletedHandler);
        this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBoundHandler);
        this._editTranslationDelegate = Function.createDelegate(this, this._editTranslationHandler);
        this._createTranslationDelegate = Function.createDelegate(this, this._createTranslationHandler);
        this._serviceSuccessDelegate = Function.createDelegate(this, this._serviceSuccess);
        this._onItemDomCreatedDelegate = Function.createDelegate(this, this._onItemDomCreated);
        this._publishPromptDelegate = Function.createDelegate(this, this._publishPromptHandler);
        this._unpublishPromptDelegate = Function.createDelegate(this, this._unpublishPromptHandler);
        this._publishDraftPromptDelegate = Function.createDelegate(this, this._publishDraftPromptHandler);
        this._unpublishPagePromptDelegate = Function.createDelegate(this, this._unpublishPagePromptHandler);

        this._itemsListBinderItemCommandDelegate = Function.createDelegate(this, this._itemsListBinderItemCommandHandler);

        this._itemsGrid = this._masterView.get_itemsGrid();
        this._itemsList = this._masterView.get_itemsList();
        this._itemsTreeTable = this._masterView.get_itemsTreeTable();
        this._translationHandlers = { create: this._createTranslationDelegate, edit: this._editTranslationDelegate };
        var notTranslatedItemsToHide = ".sfView,.sfMoreActions,.sfDateAuthor,.sfViewStats,.sfPersonalized"; //hides "View, More actions", "Stats" and "Date/Author" columns

        if (this._itemsGrid) {
            this._itemsGrid.add_command(this._masterCommandDelegate);
            this._itemsGrid.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsGrid.add_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.add_selectionChanged(this._selectionChangedDelegate);
            this._itemsGrid.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsGrid.set_translationHandlers(this._translationHandlers);
            this._itemsGrid.set_itemsToHideSelector(notTranslatedItemsToHide);
            this._itemsGrid.getBinder().set_unescapeHtml(true);
            this._itemsGrid.getBinder().add_onItemCommand(this._itemsListBinderItemCommandDelegate);
        }
        if (this._itemsList) {
            this._itemsList.add_command(this._masterCommandDelegate);
            this._itemsList.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsList.add_itemCommand(this._itemCommandDelegate);
            this._itemsList.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsList.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsList.set_translationHandlers(this._translationHandlers);
            this._itemsList.set_itemsToHideSelector(notTranslatedItemsToHide);
            this._itemsList.getBinder().set_unescapeHtml(true);
            this._itemsList.getBinder().add_onItemCommand(this._itemsListBinderItemCommandDelegate);
        }
        if (this._itemsTreeTable) {
            this._itemsTreeTable.add_command(this._masterCommandDelegate);
            this._itemsTreeTable.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsTreeTable.add_itemCommand(this._itemCommandDelegate);
            this._itemsTreeTable.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsTreeTable.add_selectionChanged(this._selectionChangedDelegate);
            this._itemsTreeTable.add_itemsDeleted(this._itemsDeletedDelegate);
            this._itemsTreeTable.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsTreeTable.set_translationHandlers(this._translationHandlers);
            this._itemsTreeTable.set_itemsToHideSelector(notTranslatedItemsToHide);
            this._itemsTreeTable.getBinder().set_unescapeHtml(true);

            this._nodeDraggingDelegate = Function.createDelegate(this, this._nodeDraggingHandler);
            this._itemsTreeTable.getBinder().add_treeViewNodeDragging(this._nodeDraggingDelegate);

            this._nodesPlacedDelegate = Function.createDelegate(this, this._nodesPlacedHandler);
            this._itemsTreeTable.getBinder().add_nodesPlaced(this._nodesPlacedDelegate);
            this._itemsTreeTable.getBinder().add_onItemDomCreated(this._onItemDomCreatedDelegate);
        }

        if ((typeof (this._masterView._userAllowedToCreatePagsOnTheCurrentSite) != "undefined") &&
            (this._masterView._userAllowedToCreatePagsOnTheCurrentSite != null)) {
            this._userAllowedToCreatePagsOnTheCurrentSite = this._masterView._userAllowedToCreatePagsOnTheCurrentSite;
        }

        if ((this._masterView._currentSiteMapRootId) && (this._masterView._currentSiteMapRootId != "00000000-0000-0000-0000-000000000000")) {
            var sitemapRootId = this._masterView._currentSiteMapRootId;
            this._setPermissionsForAllPagesDialogSecuredObjectIdToCurrentSite(sitemapRootId, this._itemsGrid);
            this._setPermissionsForAllPagesDialogSecuredObjectIdToCurrentSite(sitemapRootId, this._itemsList);
            this._setPermissionsForAllPagesDialogSecuredObjectIdToCurrentSite(sitemapRootId, this._itemsTreeTable);
        }

        if (this._masterView.get_sortExpression() != null && this._masterView.get_showHierarchicalExpression() == null) {
            this._sortPages(this._masterView.get_sortExpression());
        }
        else {
            this._configureWidgets(null);
        }

        this._workAreaClickDelegate = Function.createDelegate(this, this._workAreaClickHandler);

        jQuery(".sfWorkArea").click(this._workAreaClickDelegate);
        $(".sfWorkArea").on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
        //Commented as the MasterGridView is defaulted to TreeView and then data bound.
        //this._masterView.set_currentItemsList(this._masterView.get_itemsTreeTable());
        //this._masterView.get_currentItemsList().getBinder().DataBind();

        if (this._masterView._definedLanguages.length > 1) {
            jQuery("body").addClass("sfMultilingualTreeview");
        }
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.callBaseMethod(this, 'dispose');

        if (this._itemsGrid) {
            this._itemsGrid.remove_command(this._masterCommandDelegate);
            this._itemsGrid.remove_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.remove_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(null);
            if (this._itemsListBinderItemCommandDelegate) {
                this._itemsGrid.getBinder().remove_onItemCommand(this._itemsListBinderItemCommandDelegate);
            }
        }
        if (this._itemsList) {
            this._itemsList.remove_command(this._masterCommandDelegate);
            this._itemsList.remove_itemCommand(this._itemCommandDelegate);
            this._itemsList.remove_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.set_translationHandlers(null);

            if (this._itemsListBinderItemCommandDelegate) {
                this._itemsTreeTable.getBinder().remove_onItemCommand(this._itemsListBinderItemCommandDelegate);
            }
        }

        if (this._itemsTreeTable) {

            this._itemsTreeTable.remove_command(this._masterCommandDelegate);
            this._itemsTreeTable.remove_itemCommand(this._itemCommandDelegate);
            this._itemsTreeTable.remove_dialogClosed(this._dialogClosedDelegate);
            this._itemsTreeTable.set_translationHandlers(null);
            if (this._selectionChangedDelegate) {
                this._itemsTreeTable.remove_selectionChanged(this._selectionChangedDelegate);
                delete this._selectionChangedDelegate;
            }

            if (this._itemsDeletedDelegate) {
                this._itemsTreeTable.remove_itemsDeleted(this._itemsDeletedDelegate);
                delete this._itemsDeletedDelegate;
            }

            if (this._nodeDraggingDelegate) {
                this._itemsTreeTable.getBinder().remove_treeViewNodeDragging(this._nodeDraggingDelegate);
                delete this._nodeDraggingDelegate;
            }

            if (this._nodesPlacedDelegate) {
                this._itemsTreeTable.getBinder().remove_nodesPlaced(this._nodesPlacedDelegate);
                delete this._nodesPlacedDelegate;
            }


            this._itemsTreeTable.getBinder().remove_onItemDomCreated(this._onItemDomCreatedDelegate);

            if (this._itemsListBinderItemCommandDelegate) {
                this._itemsTreeTable.getBinder().remove_onItemCommand(this._itemsListBinderItemCommandDelegate);
            }
        }

        delete this._itemsListBinderItemCommandDelegate;
        delete this._masterCommandDelegate;
        delete this._itemCommandDelegate;
        delete this._dialogClosedDelegate;
        delete this._translationHandlers;
        delete this._editTranslationDelegate;
        delete this._createTranslationDelegate;
        delete this._onItemDomCreatedDelegate;
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _masterBeforeCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var binder = this._masterView.get_currentItemsList().getBinder();
        var selectedItems = binder.get_selectedItems();
        var selectedItem = null;

        if (commandName == "groupDelete") {
            if (selectedItems.length == 1) {
                var dataItem = selectedItems[0];
                var key = dataItem.Id;
                if (dataItem.HasChildren && dataItem.AvailableLanguages.length > 1) {
                    this._masterView.get_currentItemsList().deleteItems([key], false, [dataItem], true);
                    args.set_cancel(true);
                    return;
                }
            }
        }

        // check if there is group page to show restriction
        if (commandName == "groupDelete") {
            var itemKeys = [];
            var deleteHomePage = false;
            var deleteParentPage = false;
            var homePageTranslated = false;
            var parentPageTranslated = false;
            for (var i = 0; i < selectedItems.length; i++) {
                selectedItem = selectedItems[i];
                // When the item is in monolingual it has 0 translations so we cannot delete it if it has children.
                // when the item is in multilingual and it has more than one translations we allow deletion of the page.
                if (selectedItem.HasChildren) {
                    deleteParentPage = true;
                    if (selectedItem.AvailableLanguages.length > 1) {
                        parentPageTranslated = true;
                    }
                }

                if (selectedItem.IsHomePage) {
                    deleteHomePage = true;
                    if (selectedItem.AvailableLanguages.length > 1) {
                        homePageTranslated = true;
                    }
                }
                itemKeys.push(selectedItem.Id);
            }

            var binder = this._masterView.get_currentItemsList().getBinder();
            if (deleteParentPage && !parentPageTranslated) {
                args.set_cancel(true);
                this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteParentPageDialog");
                return;
            } else if (deleteHomePage && !deleteParentPage) {
                if (binder.get_displayedItemsCount() == selectedItems.length) {
                    args.set_cancel(true);
                    this._masterView.get_currentItemsList().deleteItems(itemKeys, false, selectedItems, false);
                    return;
                } else if (binder.get_displayedItemsCount() > 1) {
                    args.set_cancel(true);
                    if (homePageTranslated) {
                        this._masterView.get_currentItemsList().deleteItems(itemKeys, false, selectedItems, true);
                    } else {
                        this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteHomepageDialog");
                    }
                    return;
                }
            } else {
                var translationOnly = deleteParentPage || deleteHomePage;
                args.set_cancel(true);
                this._masterView.get_currentItemsList().deleteItems(itemKeys, false, selectedItems, translationOnly);
                return;
            }
        }

        if (this._masterView.get_isAdmin() == false) {
            if (args.get_cancel() === false) {
                if (commandName == "groupDelete") {
                    if (selectedItems.length == 1) {
                        selectedItem = selectedItems[0];
                        this._processDeleteCommand(selectedItem, args);
                    }
                }
            }
        }

        //if (args.get_cancel() === false) {
        //    // check if this is an attempt to delete the home page only.
        //    if (commandName === "groupDelete") {
        //        for (var i = 0; i < selectedItems.length; i++) {
        //            selectedItem = selectedItems[i];
        //            if (selectedItems[0].IsHomePage) {
        //                args.set_cancel(true);
        //                this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteHomepageDialog");
        //                return;
        //            }
        //        }
        //    }
        //}
    },

    // handles commands fired the master view
    _masterCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        if (args.get_cancel() === false) {
            switch (commandName) {
                case 'showAllPages':
                    this._switchToHierarchical();
                    var binder = this._masterView.get_currentItemsList().getBinder();
                    binder.get_urlParams()['pageFilter'] = 'AllPages';
                    if (binder.get_isFiltering()) {
                        binder.set_filterExpression('');
                    }
                    else {
                        binder.set_isFiltering(true);
                    }
                    binder.DataBind();
                    break;
                case 'showMyPages':
                    this._filterPagesBy('MyPages');
                    break;
                case 'showPublishedPages':
                    this._filterPagesBy('Published');
                    break;
                case 'showDraftPages':
                    this._filterPagesBy('Draft');
                    break;
                case 'showScheduledPages':
                    //this._configureMasterView(true);
                    //var binder = currentList.getBinder();
                    //binder.get_urlParams()['pageFilter'] = 'Scheduled';
                    //binder.set_isFiltering(true);
                    //binder.DataBind();
                    alert('In process of implementation.');
                    break;
                case this._masterView._pendingApprovalPagesCommandName:
                    this._filterPagesBy('AwaitingApproval');
                    break;
                case this._masterView._pendingReviewPagesCommandName:
                    this._filterPagesBy('AwaitingReview');
                    break;
                case this._masterView._pendingPublishingPagesCommandName:
                    this._filterPagesBy('AwaitingPublishing');
                    break;
                case this._masterView._rejectedPagesCommandName:
                    this._filterPagesBy('Rejected');
                    break;
                case this._masterView._awaitingMyActionPagesCommandName:
                    this._filterPagesBy('AwaitingMyAction');
                    break;
                case 'showPagesWithNoTitles':
                    this._filterPagesBy('WithNoTitle');
                    break;
                case 'showPagesWithNoKeywords':
                    this._filterPagesBy('WithNoKeywords');
                    break;
                case "showPagesWithNoDescriptions":
                    this._filterPagesBy('WithNoDescriptions');
                    break;

                case "changeTemplate":
                    var binder = this._masterView.get_currentItemsList().getBinder();
                    binder.get_urlParams()['language'] = binder.get_uiCulture();
                    break;
                case "groupDelete":
                case 'manageTemplates':
                case 'create':
                case "createChild":
                case "batchChangeTemplate":
                case 'batchChangeOwner':
                    // do nothing, let the master view handle that 
                    break;
                case "batchPublishPage":
                    this._batchPublishPage();
                    break;
                case "batchUnpublishPage":
                    this._batchUnpublishPage();
                    break;
                case 'searchGrid':
                    if (this._previousMasterViewModeSelection == null) {
                        this._previousMasterViewModeSelection = this._getCurrentGridViewModeSelection();
                    }
                    this._configureMasterView(true);
                    var binder = this._masterView.get_currentItemsList().getBinder();
                    delete binder.get_urlParams()['pageFilter'];
                    //HACK: the order of execution flow should be changed
                    this._masterView.get_binderSearch().search(args.get_commandArgument().get_query());
                    var sidebar = this._masterView.get_sidebar();
                    var allPagesWidget = sidebar.getWidgetByName("AllPages");
                    this._masterView._markItemSelected(this._masterView._previousFilterWidget, false);
                    this._masterView._markItemSelected(allPagesWidget, true);
                    this._masterView.set_previousFilterWidget(allPagesWidget);
                    break;
                case 'closeSearchGrid':
                    //mode of the master view is set back to the mode which was valid before the execution of the searchGrid command               
                    this._setGridViewModeSelection(this._previousMasterViewModeSelection);
                    this._previousMasterViewModeSelection = null;
                    break;
                case 'moveUp':
                    var binder = this._masterView.get_currentItemsList().getBinder();
                    this._tryMovingSelectedPages(binder, "up");
                    break;
                case 'moveDown':
                    var binder = this._masterView.get_currentItemsList().getBinder();
                    this._tryMovingSelectedPages(binder, "down");
                    break;
                case "sort":
                    var sortExpression = args.get_commandArgument();
                    this._sortPages(sortExpression);
                    break;
                case this._masterView.get_showHierarchicalCommandName():
                    this._showHierarchical();
                    break;
                case 'customDateFilterCommand':
                    {
                        this._configureMasterView(true);
                        var cmdArg = args.get_commandArgument();
                        if (cmdArg.hasOwnProperty("propertyToFilter") && cmdArg.propertyToFilter == "LastModified") {
                            var filterExpression = '(LastModified>(' + cmdArg.dateFrom + ') OR Page.LastModified>(' + cmdArg.dateFrom + '))';
                            this._masterView.get_currentItemsList().applyFilter(args.get_commandArgument().filterExpression);
                        }
                        else {
                            this._masterView.get_currentItemsList().applyFilter(args.get_commandArgument().filterExpression);
                        }
                        args.set_cancel(true);
                    }
                    break;
                case 'customFields':
                    alert('In process of implementation.');
                    break;
                case 'notImplemented':
                    alert('In process of implementation.');
                    break;
                default:
                    if (commandName.substring(0, 'sf_status_filter'.length) == 'sf_status_filter') {
                        this._filterPagesBy(commandName);
                        args.set_cancel(true);
                    }
                    break;
            }
        }
        else {
            switch (commandName) {
                case 'showSectionsExcept':
                    this._configureMasterView(true);
                    break;
                case "showCustomRange":
                case 'hideSectionsExcept':
                case 'filter':
                    break;
                default:
                //alert('In process of implementation.');
            }
        }
    },

    _filterPagesBy: function (filter) {
        this._configureMasterView(true);
        var binder = this._masterView.get_currentItemsList().getBinder();
        binder.get_urlParams()['pageFilter'] = filter;
        binder.set_isFiltering(true);
        binder.DataBind();
    },

    _showHierarchical: function () {
        this._configureMasterView();
        var binder = this._masterView.get_currentItemsList().getBinder();
        binder.get_urlParams()['pageFilter'] = 'AllPages';
        binder.set_isFiltering(true);
        binder.DataBind();
    },

    _sortPages: function (sortExpression) {
        this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
        var binder = this._masterView.get_currentItemsList().getBinder();
        binder.set_sortExpression(sortExpression);
        binder.DataBind();

    },

    _tryMoveSelectedNodes: function (binder, direction) {
        //TODO localize this messages.
        if (binder.selectedNodesAreSiblings()) {
            if (binder.canMoveSelection(direction)) {
                binder.moveSelectedNodes(direction);
            }
            else {
                this._masterView.get_messageControl().showNegativeMessage("Can't move the selected pages " + direction + '.');
            }
        }
    },
    _itemsListBinderItemCommandHandler: function (sender, args) {
        if (this._deleteSingleItem) {
            this._deleteSingleItem = false;
            var dataItem = args.get_dataItem();
            var key = args.get_key();
            this._masterView.get_currentItemsList().deleteItems([key], false, [dataItem], true);
        }
    },
    // handles the commands fired by a single item
    _itemCommandHandler: function (sender, args) {
        var page = args.get_commandArgument();
        var pageId = args.get_commandArgument().Id;
        var currentList = this._masterView.get_currentItemsList();
        var binder = currentList.getBinder();

        switch (args.get_commandName()) {
            case 'editPage':
                var dataItem = args.get_commandArgument();
                if (!dataItem.IsGroup && !dataItem.IsExternal) {
                    args.set_cancel(true);
                    this._editPageContent(page.PageEditUrl);
                }
                break;
            case 'setAsHomepage':
                if (args) {
                    var commandArgument = args.get_commandArgument();
                }
                if (commandArgument) {
                    if (commandArgument.IsContentEditable == true) {
                        this._setHomePage(sender, pageId, pageId);
                    } else {
                        alert(this._masterView._noPermissionsToSetAsHomepage);
                    }
                }
                break;
            case 'publishDraft':
                this._publishDraft(sender, pageId, pageId, page);
                break;
            case 'unpublishPage':
                this._unpublishPage(sender, pageId, pageId, page);
                break;
            case 'editPageContent':
                this._editPageContent(page.PageEditUrl);
                break;
            case 'moveUp':
                this._movePage(pageId, binder, "up");
                break;
            case 'moveDown':
                this._movePage(pageId, binder, "down");
                break;
            case 'unlockPage':
                var dataItem = args.get_commandArgument();
                args.get_commandArgument().LockedByUsername = dataItem.PageLifecycleStatus.LockedByUsername;
                //this._unlockPage(sender, pageId);
                break;
            case 'changeTemplate':
                break;
            case 'history':
                break;
            case 'delete':
                this._processDeleteCommand(page, args);
                break;
            case 'personalize':
                // do nothing for now
                break;
            case 'createChildPage':
            case 'createSiblingPageBefore':
            case 'createSiblingPageAfter':
                alert('In process of implementation.');
                break;
        }
    },

    _processDeleteCommand: function (page, args) {
        if (args.get_cancel() == false) {
            var selectedItem = page;
            //It is allowed to delete translations of a parent page node as of 7.0
            //In monolingual there are no available languages and in multilingual there should be a minimal of 1
            this._deleteSingleItem = false;
            if (selectedItem.HasChildren) {
                if (selectedItem.AvailableLanguages.length > 1) {
                    this._deleteSingleItem = true;
                } else {
                    args.set_cancel(true);
                    if (selectedItem.IsHomePage) {
                        this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteHomepageDialog");
                    } else {
                        this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteParentPageDialog");
                    }
                }
            }
            else if (selectedItem.IsHomePage) {
                var binder = this._masterView.get_currentItemsList().getBinder();
                if (binder.get_displayedItemsCount() > 1) {
                    args.set_cancel(true);
                    this._masterView.get_currentItemsList().showPromptDialogByName("cannotDeleteHomepageDialog");
                }
            }
        }
    },

    // Fires when a dialog is closed
    _dialogClosedHandler: function (sender, args) {
        if (!Telerik.Sitefinity.DialogClosedEventArgs.isInstanceOfType(args)) {
            var itemsGrid = this._masterView.get_currentItemsList();
            var binder = itemsGrid.getBinder();
            var windowName = sender.get_name();

            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            var serviceUrl = this._getBaseUrl(binder);
            switch (windowName) {
                case "changeTemplate":

                    this._changeTemplate(serviceUrl, args, itemsGrid, clientManager);
                    break;
                case "batchChangeTemplate":
                    this._batchChangeTemplate(serviceUrl, binder, args, clientManager, itemsGrid);
                    break;
                case "batchChangeOwner":
                    this._batchAction("BatchChangeOwner", args)
                    break;
                case "personalize":
                    $('body').addClass('sfLoadingTransition');
                    var dataItem = args.PersonalizationDataItem;
                    this._editPageContent(dataItem.PageEditUrl, args.SegmentId);
                    break;
                case "unlockPage":
                    //If the window is an unlockPage window - check if the unlock button was clicked 
                    if (args.Action && args.Action == 'unlock') {
                        //If yes - refresh grid (ToDo: better refresh just unlocked item?)
                        binder.DataBind();
                    }
                    break;
                default:
                    break;
            }
        }
        else {
            var context = args.get_context(),
                _dataItem = args.get_dataItem();
            if (args && args.get_isCreated
                && context && context.get_widgetCommandName) {
                if (context.get_widgetCommandName() == 'create') {
                    if (window.chrome) {
                        var that = this;
                        setTimeout(function () {
                            that._navigateToEditPage(sender, args);
                        }, 0);
                    } else {
                        this._navigateToEditPage(sender, args);
                    }
                }
                else if (context.get_widgetCommandName() == 'save'
                    && _dataItem.TargetSiteId
                    && _dataItem.TargetSiteId != context._blankDataItem.TargetSiteId) {
                    var navigateUrl = window.location.pathname + "?sf_site=" + _dataItem.TargetSiteId;
                    if (_dataItem.Language)
                        navigateUrl += "&lang=" + _dataItem.Language;
                    window.location.href = navigateUrl;
                }
            }
        }
    },

    _editTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        var dataItem = args.get_dataItem();
        if (dataItem.IsGroup || dataItem.IsExternal) {
            var list = args.get_list();
            var key = args.get_key();
            list.executeItemCommandInLanguage(args.get_language(), "edit", dataItem, key, { language: args.get_language(), languageMode: args.get_commandName() });
        } else {
            if (dataItem.hasOwnProperty("PageEditLanguageUrls")) {
                var languageUrls = dataItem.PageEditLanguageUrls;
                var lang = args.get_language();
                for (var i = 0; i < languageUrls.length; ++i) {
                    var langUrlItem = languageUrls[i];
                    if (langUrlItem.hasOwnProperty("Key") && langUrlItem.Key == lang &&
                        langUrlItem.hasOwnProperty("Value")) {
                        this._editPageContent(langUrlItem.Value);
                        break;
                    }
                }
            }
            else {
                this._editPageContent(args.get_dataItem().PageEditUrl);
            }
        }
    },

    _createTranslationHandler: function (sender, args) {
        args.set_cancel(true);
        var list = args.get_list();
        var item = args.get_dataItem();
        list.executeItemCommand("create", item, args.get_key(), { language: args.get_language(), languageMode: args.get_commandName(), sourceObjectId: item.Id });
    },

    _onItemDomCreated: function (sender, args) {
        this._itemsTreeTable.getBinder().remove_onItemCommand(this._itemsListBinderItemCommandDelegate);
        this._itemsTreeTable.getBinder().add_onItemCommand(this._itemsListBinderItemCommandDelegate);
        this._masterView.setTranslations(args.get_dataItem(), this._masterView.get_currentItemsList(), args.get_key(), args.get_itemElement());
    },

    _batchAction: function (command, key) {
        var itemsGrid = this._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var serviceUrl = this._getBaseUrl(binder);
            serviceUrl += command + '/';

            var urlParams = [];
            var keys = [];
            if (key) {
                keys.push(key);
            }
            var data = [];
            var count = selectedItems.length;
            while (count--) {
                data.push(selectedItems[count].Id);
            }
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this._isMultilingual()) {
                clientManager.set_uiCulture(this._getCurrentLanguage());
            }
            clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccess, this._serviceFailure, itemsGrid);
        }
    },
    // Fires when the selection of the tree is changed
    _selectionChangedHandler: function (sender, args) {
        if (!args) {
            return;
        }

        var selectionType = this._getSelectionType(args);
        this._configureWidgets(selectionType);
    },

    // Fires when the deletion of items is completed
    _itemsDeletedHandler: function (sender, args) {
        //false is returned by the batch page deletion service if there are items that were not deleted.
        if (args == "false") {
            //TODO:Not the correct dialog.
            this._masterView._itemsTreeTable.showPromptDialogByName("somePagesWereNotDeletedDialog");
        }
    },

    // handles the clicks in the blank area around the items tree table
    _workAreaClickHandler: function (e) {
        if (($telerik.isIE && $(e.target).parents(".sfTreeTable").length > 0)) {
            return;
        }

        var currentList = this._masterView.get_currentItemsList();
        if (Object.getTypeName(currentList) == 'Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable') {
            var binder = currentList.getBinder();
            binder.clearSelection();
            this._configureWidgets(Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.None);
        }
    },

    _nodeDraggingHandler: function (sender, args) {
        var draggingClue = sender.get_draggingClueElement();
        if (draggingClue) {
            var sourceNodes = args.get_sourceNodes();
            var sourceNodesCount = sourceNodes.length;
            draggingClue.className = "sfDragHelper";
            if (sourceNodesCount == 1) {
                draggingClue.innerHTML = String.format('Move {0}', sourceNodes[0].get_text());
            }
            else {
                draggingClue.innerHTML = String.format('Move <strong>{0}</strong> pages', sourceNodesCount);
            }
        }

        jQuery('.sfDraggingAbove').removeClass('sfDraggingAbove');
        jQuery('.sfDraggingBelow').removeClass('sfDraggingBelow');

        // TODO: RadTreeView team should implement a public property for getting dropClue.
        var dropClue = sender._dropClue;
        if (dropClue && dropClue.style.visibility == 'visible') {
            var destinationNode = dropClue.treeNode;
            if (destinationNode) {
                var destinationNodeElement = destinationNode.get_element();
                if (jQuery(dropClue).hasClass('rtDropAbove')) {
                    jQuery(destinationNodeElement).addClass('sfDraggingAbove');
                }
                else if (jQuery(dropClue).hasClass('rtDropBelow')) {
                    jQuery(destinationNodeElement).addClass('sfDraggingBelow');
                }
            }
        }
    },

    _nodesPlacedHandler: function (sender, args) {
        if (args) {
            var destinationNode = args.get_destinationNode();
            var sourceNodes = args.get_sourceNodes();
            var placePosition = args.get_placePosition();
            var message = null;
            var dataItem = null;
            var title = null;

            if (sourceNodes.length == 1) {
                dataItem = sourceNodes[0].get_dataItem();
                title = dataItem.Title.hasOwnProperty('Value') ? dataItem.Title.Value : dataItem.Title;
                message = String.format('<em>{0}</em> has been ', title.htmlEncode());
            }
            else {
                message = String.format('<em>{0}</em> pages have been ', sourceNodes.length);
            }

            if (destinationNode) {
                destinationDataItem = destinationNode.get_dataItem();
                if (destinationDataItem) destinationDataItem.HasChildren = true;
            }

            //Refresh the action menu
            for (var i = 0; i < sourceNodes.length; i++) {
                var node = sourceNodes[i];
                this._configureActionMenusRecursive(node);
            }

            if (placePosition == 'over') {
                dataItem = destinationNode.get_dataItem();
                title = dataItem.Title.hasOwnProperty('Value') ? dataItem.Title.Value : dataItem.Title;
                message += String.format('moved to <em>{0}</em>', title.htmlEncode());
            }
            else {
                message += 'reordered';
            }

            this._masterView.get_messageControl().showPositiveMessage(message);
        }
    },

    _configureActionMenusRecursive: function (node) {
        var elm = node.get_element();
        var nodeDataItem = node.get_dataItem();
        this._configureMoreActionsMenu(nodeDataItem, elm);

        var children = node.get_nodes();
        for (var j = 0; j < children.get_count(); j++) {
            var childNode = children.getItem(j);
            // loading node is Telerik.Web.UI.RadTreeNode (not Telerik.Sitefinity.Web.UI.RadTreeNode), which means that it has no get_dataItem. Additionally, it is not visible
            // and it is not necessary to configure actions menu.
            if (childNode.get_dataItem) {
                this._configureActionMenusRecursive(childNode);
            }
        }
    },

    // Fired when item is bound in the tree mode
    _itemDataBoundHandler: function (sender, args) {
        var dataItem = args.get_dataItem();

        var itemElement = args.get_itemElement();
        var list = this._masterView.get_currentItemsList();

        if (dataItem.Renderer) {
            var anchorElement = jQuery(itemElement).find("a.sfItemTitle:not(.sfDisabled)");

            if (anchorElement) {
                anchorElement.attr("href", "javascript:void(0)");
                anchorElement.unbind("click").bind("click", function () {
                    list.showPromptDialogByName("switchToNewUiPageDialog", null, null, null);
                });
            }
        }

        if (dataItem.IsGroup || dataItem.IsExternal) {
            var anchorElement = jQuery(itemElement).find("a.sfItemTitle.sfgroup:first:not(.sfDisabled)");

            if (anchorElement != 'undefined') {
                anchorElement.attr("href", "javascript:void(0)");
                var key = args.get_key();
                if (list._isMultilingual) {
                    var hasTranslation = jQuery.inArray(list.get_uiCulture(), dataItem.AvailableLanguages) > -1;
                    var mode = hasTranslation ? "edit" : "create";
                    var data = { mode: mode, dataItem: dataItem, key: key, element: itemElement, lang: list.get_uiCulture(), handlers: this._translationHandlers };
                    anchorElement.unbind("click").bind("click", data, list.get_translationDelegate());
                }
                else {
                    anchorElement.unbind("click").bind("click", function () {
                        if (dataItem.HasTranslationSiblings) {
                            var commandArgs = { language: list.get_uiCulture(), languageMode: "edit" };
                            list.executeItemCommand("edit", dataItem, key, commandArgs);
                        }
                        else {
                            list.executeItemCommand("edit", dataItem, key);
                        }
                    });
                }
                anchorElement.on("unload",
                    function (e) {
                        jQuery.event.remove(this);
                        jQuery.removeData(this);
                    });
            }
            if (dataItem.IsGroup)
                jQuery(itemElement).find(".sfView a").hide();
        }

        this._configureMoreActionsMenu(dataItem, itemElement);

        var selectionType = this._getSelectionType(this._masterView.get_currentItemsList().get_selectedItems());
        this._configureWidgets(selectionType);
    },

    /* -------------------- private methods ----------- */

    _switchToHierarchical: function () {
        var toolbar = this._masterView.get_toolbar();
        this._masterView.set_currentItemsList(this._masterView.get_itemsTreeTable());
        var sortingWidget = toolbar.getWidgetByName("PageSortingWidget");
        var sortingDropdown = $("#" + sortingWidget._dropDownId)[0];
        sortingDropdown.selectedIndex = 0;
    },

    _configureMasterView: function (switchToFlatView) {
        /// <summary>Configures hierarchical or flat view depending on selection at sort widget.</summary>
        /// <param name="switchToFlatView">Optionally, will switch the view to flat, e.g. need to be switched when filter is appplied</param>
        var toolbar = this._masterView.get_toolbar();
        var sortingWidget = toolbar.getWidgetByName("PageSortingWidget");
        var sortingDropdown = $("#" + sortingWidget._dropDownId)[0];
        //current view is hierachical or need to be swithched to hierachical
        var currentViewIsHierarchical = sortingDropdown.value === this._masterView.get_showHierarchicalCommandName();
        if (switchToFlatView) {
            this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
            // change the value of dropdown (different than 'Hierarchical')
            sortingDropdown.selectedIndex = 1;
        }
        else if (currentViewIsHierarchical) {
            this._masterView.set_currentItemsList(this._masterView.get_itemsTreeTable());
        }
        else {
            this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
        }

        this._configureWidgets(null);
        this._masterView.get_messageControl().hide();
    },

    _setGridViewModeSelection: function (index) {
        //clear the filter of the current items in the master grid
        this._masterView.get_currentItemsList().applyFilter("");

        var toolbar = this._masterView.get_toolbar();
        var sortingWidget = toolbar.getWidgetByName("PageSortingWidget");
        var sortingDropdown = $("#" + sortingWidget._dropDownId)[0];

        sortingDropdown.selectedIndex = index;

        //if the selected index of the sorting widget is hierarchical then set the current mode to be tree view
        if (!index || index == 0) {
            this._masterView.set_currentItemsList(this._masterView.get_itemsTreeTable());
        }
        else {
            this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
        }

        this._configureWidgets(null);
        this._masterView.get_messageControl().hide();
    },

    _getCurrentGridViewModeSelection: function () {
        var toolbar = this._masterView.get_toolbar();
        var sortingWidget = toolbar.getWidgetByName("PageSortingWidget");
        var sortingDropdown = $("#" + sortingWidget._dropDownId)[0];
        return sortingDropdown.selectedIndex;
    },

    // set homepage
    _setHomePage: function (itemsListBase, pageId, context) {
        var serviceUrl = this._getBaseUrl(itemsListBase);
        serviceUrl += 'HomePage/Set/';
        var clientManager = this._getClientManager();
        clientManager.InvokePut(serviceUrl, null, null, pageId, this._serviceSuccess, this._serviceFailure, itemsListBase, null, null, context);
    },

    _publishDraftPromptHandler: function (sender, args, pageId) {
        var thisView = this;
        // If the function is NOT used as handler.
        if (sender) {
            if (args.get_commandName() == "cancel") {
                return;
            }
            pageId = sender._lastContext.pageId;
            thisView = sender._lastContext.thisView
        }

        var itemsGrid = thisView._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();

        var serviceUrl = thisView._getBaseUrl(binder);
        serviceUrl += 'batchPublishDraft/';

        var urlParams = [];
        var keys = [];
        var data = [];

        data.push(pageId);

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        if (this._isMultilingual()) {
            clientManager.set_uiCulture(this._getCurrentLanguage());
        }
        clientManager.InvokePut(serviceUrl, urlParams, keys, data, thisView._serviceSuccess, thisView._serviceFailure, itemsGrid);
    },

    // publish draft
    _publishDraft: function (itemsListBase, pageId, context, page) {
        var itemsGrid = this._masterView.get_currentItemsList();
        if (itemsGrid._isMultilingual && page.LocalizationStrategy != 1 && (page.AvailableLanguages && page.AvailableLanguages.length > 1)) {
            itemsGrid.showPromptDialogByName("publishWarningDialog", null, null, this._publishDraftPromptDelegate, { "thisView": this, "pageId": pageId });
        } else {
            this._publishDraftPromptHandler(null, null, pageId);
        }
    },

    _unpublishPagePromptHandler: function (sender, args, pageId) {
        var thisView = this;
        // If the function is NOT used as handler.
        if (sender) {
            if (args.get_commandName() == "cancel") {
                return;
            }
            pageId = sender._lastContext.pageId;
            thisView = sender._lastContext.thisView;
        }

        var itemsGrid = thisView._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();

        var serviceUrl = thisView._getBaseUrl(binder);
        serviceUrl += 'batchUnpublishPage/';

        var urlParams = [];
        var keys = [];
        var data = [];

        data.push(pageId);

        var clientManager = new Telerik.Sitefinity.Data.ClientManager();
        if (this._isMultilingual()) {
            clientManager.set_uiCulture(this._getCurrentLanguage());
        }
        clientManager.InvokePut(serviceUrl, urlParams, keys, data, thisView._serviceSuccess, thisView._serviceFailure, itemsGrid);
    },

    // unpublish draft
    _unpublishPage: function (itemsListBase, pageId, context, page) {
        var itemsGrid = this._masterView.get_currentItemsList();
        if (itemsGrid._isMultilingual && page.LocalizationStrategy != 1 && (page.AvailableLanguages && page.AvailableLanguages.length > 1)) {
            itemsGrid.showPromptDialogByName("unPublishWarningDialog", null, null, this._unpublishPagePromptDelegate, { "thisView": this, "pageId": pageId });
        } else {
            this._unpublishPagePromptHandler(null, null, pageId);
        }
    },

    // unlock page - not used currently - unlocks directly without showing the LockingDialog
    _unlockPage: function (itemsListBase, pageDataId) {
        var serviceUrl = this._getZoneEditorServiceUrl(itemsListBase);
        serviceUrl += 'Page/UnlockPage/';
        serviceUrl += pageDataId;
        var clientManager = this._getClientManager();
        clientManager.InvokeGet(serviceUrl, null, null, this._serviceSuccess, this._serviceFailure);
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

    _publishPromptHandler: function (sender, args) {

        if (sender) {
            if (args.get_commandName() == "cancel") {
                return;
            }
        }

        var itemsGrid = this._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var serviceUrl = this._getBaseUrl(binder);
            serviceUrl += 'batchPublishDraft/';

            var urlParams = [];
            var keys = [];
            var data = [];
            var count = selectedItems.length;
            while (count--) {
                data.push(selectedItems[count].Id);
            }
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this._isMultilingual()) {
                clientManager.set_uiCulture(this._getCurrentLanguage());
            }
            clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccess, this._serviceFailure, itemsGrid);
        }
    },

    _batchPublishPage: function () {
        var itemsGrid = this._masterView.get_currentItemsList();
        if (itemsGrid._isMultilingual) {
            itemsGrid.showPromptDialogByName("publishWarningDialog", null, null, this._publishPromptDelegate, this);
        } else {
            this._publishPromptHandler();
        }
    },

    _unpublishPromptHandler: function (sender, args) {

        if (sender) {
            if (args.get_commandName() == "cancel") {
                return;
            }
        }

        var itemsGrid = this._masterView.get_currentItemsList();
        var binder = itemsGrid.getBinder();
        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {
            var serviceUrl = this._getBaseUrl(binder);
            serviceUrl += 'batchUnpublishPage/';

            var urlParams = [];
            var keys = [];
            var data = [];
            var count = selectedItems.length;
            while (count--) {
                data.push(selectedItems[count].Id);
            }
            var clientManager = new Telerik.Sitefinity.Data.ClientManager();
            if (this._isMultilingual()) {
                clientManager.set_uiCulture(this._getCurrentLanguage());
            }
            clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._serviceSuccess, this._serviceFailure, itemsGrid);
        }
    },

    _batchUnpublishPage: function () {
        var itemsGrid = this._masterView.get_currentItemsList();
        if (itemsGrid._isMultilingual) {
            itemsGrid.showPromptDialogByName("unPublishWarningDialog", null, null, this._unpublishPromptDelegate, this);
        } else {
            this._unpublishPromptHandler();
        }
    },

    // move page up
    _movePageUp: function (binder, pageId) {
        if (binder.canMoveSelection(direction)) {
            binder.moveNode(pageId, "up");
        }
    },

    _movePage: function (pageId, binder, direction) {
        if (binder.canMoveNode(pageId, direction)) {
            binder.moveNode(pageId, direction);
        }
        else {
            this._showCantMoveMessage(direction);
        }
    },

    _tryMovingSelectedPages: function (binder, direction) {
        //TODO localize this messages.
        if (binder.selectedNodesAreSiblings()) {
            if (binder.canMoveSelection(direction)) {
                binder.moveSelectedNodes(direction);
            }
            else {
                this._showCantMoveMessage(direction);
            }
        }
        else {
            this._masterView.get_messageControl().showNegativeMessage("Can't move pages that are from different levels.");
        }
    },

    _showCantMoveMessage: function (direction) {
        this._masterView.get_messageControl().showNegativeMessage("Can't move the selected pages " + direction + '.');
    },

    // edit content
    _editPageContent: function (editUrl, segmentId) {
        var currentSiteId = this._masterView.get_currentSiteId();
        var appender = editUrl.indexOf("?") === -1 ? "?" : "&";
        if (currentSiteId && segmentId) {
            editUrl += appender + this._masterView.get_siteIdParamKey() + "=" + currentSiteId + ((segmentId) ? "&segment=" + segmentId : "");
        } else if (segmentId) {
            editUrl += appender + "segment=" + segmentId;
        }
        window.location = editUrl;
    },

    // utility methods
    _getBaseUrl: function (itemsListBase) {
        var baseUrl = itemsListBase.get_serviceBaseUrl();
        var serviceUrl = baseUrl.toString().substr(0, baseUrl.toString().indexOf('?'));
        return serviceUrl;
    },
    _getZoneEditorServiceUrl: function (itemsListBase) {
        var baseUrl = this._getBaseUrl(itemsListBase);
        var serviceUrl = baseUrl.replace('PagesService.svc', 'ZoneEditorService.svc');
        return serviceUrl;
    },

    _serviceSuccess: function (caller, data, request, context) {
        if (context && context.Command) {
            if (context.Command == "changeTemplate") {
                if (data && data == "true") {//Succeeded
                    //                    caller.showPromptDialogByName("templateChangedDialog");
                    this._masterView.get_messageControl().showPositiveMessage("The template was successfully changed.");
                } else {//Failed
                    //                    caller.showPromptDialogByName("templateChangeFailedDialog");
                    this._masterView.get_messageControl().showNegativeMessage("The template was not changed. The page is locked by someone else.");
                }
            } else if (context.Command == "batchChangeTemplate") {
                if (data && data.length && data.length > 0) {//Some failed
                    //                    var lockedPageTitles = '';
                    //                    for (var i = 0; i < data.length; i++) {
                    //                        if (lockedPageTitles.length > 0) lockedPageTitles += '<br/>';
                    //                        lockedPageTitles += data[i];
                    //                    }
                    var itemsCount = '<strong>' + (caller.getBinder().get_selectedItems().length - data.length) + '</strong>';

                    //                    var dialog = caller.getPromptDialogByName("templatesChangeFailedDialog");
                    var message = "The template was successfully applied to {0} pages. The template was not applied to {1} pages.";
                    message = String.format(message, itemsCount, data.length);
                    this._masterView.get_messageControl().showNeutralMessage(message);
                    //                    dialog.show_prompt(null, message);
                } else {//All succeeded
                    var itemsCount = caller.getBinder().get_selectedItems().length;

                    //                    var dialog = caller.getPromptDialogByName("templatesChangedDialog");
                    var message = "The template was successfully applied to {0} pages.";
                    message = String.format(message, '<strong>' + itemsCount + '</strong>');
                    this._masterView.get_messageControl().showPositiveMessage(message);
                    //                    dialog.show_prompt(null, message);
                }
            }

            context = null;
        }

        if (context) {
            caller.dataBind(null, context);
        }
        else {
            caller.dataBind();
        }
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
        alert(sender.Detail);

        if (args.dataBind) {
            args.dataBind();
        }
    },

    _isInTreeViewMode: function () {
        return Object.getTypeName(this._masterView.get_currentItemsList()) == 'Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable';
    },

    _configureWidgets: function (selectionType) {

        var isInTreeMode = this._isInTreeViewMode();
        if (selectionType === undefined || selectionType == null) {
            selectionType = this._getSelectionType(this._masterView.get_currentItemsList().getBinder().get_selectedItems());
        }

        var toolbar = this._masterView.get_toolbar();
        var createPageWidget = toolbar.getWidgetByName("CreatePageWidget");
        var deletePageWidget = toolbar.getWidgetByName("DeletePageWidget");
        var createChildWidget = toolbar.getWidgetByName("CreateChildWidget");
        var moreActionsWidget = toolbar.getWidgetByName("MoreActionsPagesWidget");

        var decisionScreenCreateWidget = null;

        //locate the "create" widget on the "no items" decision screen, to match its visibility with the toolbar's "create button"
        if ((this._masterView.get_decisionScreens()["NoItemsExist"]) && ($find(this._masterView.get_decisionScreens()["NoItemsExist"]))) {
            var actionItems = $find(this._masterView.get_decisionScreens()["NoItemsExist"])._actionItems;
            if (actionItems) {
                for (var i = 0; i < actionItems.length; i++) {
                    if (actionItems[i].CommandName == "create") {
                        decisionScreenCreateWidget = $($get(actionItems[i].LinkClientId));
                        break;
                    }
                }
            }
        }

        switch (selectionType) {
            case Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.None:
                deletePageWidget.set_enabled(false);
                moreActionsWidget.set_enabled(false);
                if (createPageWidget) {
                    if (this._userAllowedToCreatePagsOnTheCurrentSite) {
                        jQuery(createPageWidget.get_element()).show();
                        if (decisionScreenCreateWidget)
                            decisionScreenCreateWidget.show();
                    }
                    else {
                        jQuery(createPageWidget.get_element()).hide();
                        if (decisionScreenCreateWidget)
                            decisionScreenCreateWidget.hide();
                    }
                }
                if (createChildWidget) {
                    jQuery(createChildWidget.get_element()).hide();
                }
                moreActionsWidget.hideWidgetByName("batchChangeTemplate");
                moreActionsWidget.hideWidgetByName("changeTemplate");
                //moreActionsWidget.hideWidgetByName("moveUp");
                //moreActionsWidget.hideWidgetByName("moveDown");
                break;
            case Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.Single:
                deletePageWidget.set_enabled(true);
                moreActionsWidget.set_enabled(true);
                var isSubPageCreationAllowed = false;
                var selectedItem = this._masterView.get_currentItemsList().getBinder().get_selectedItems()[0];
                if (typeof (selectedItem) != "undefined")
                    isSubPageCreationAllowed = selectedItem.IsSubPageCreationAllowed;

                if (isInTreeMode) {
                    if (createPageWidget) {
                        jQuery(createPageWidget.get_element()).hide();
                    }
                    if (createChildWidget) {
                        if (isSubPageCreationAllowed)
                            jQuery(createChildWidget.get_element()).show();
                        else
                            jQuery(createChildWidget.get_element()).hide();
                    }
                    //moreActionsWidget.showWidgetByName("moveUp");
                    //moreActionsWidget.showWidgetByName("moveDown");
                }
                else {
                    if (createPageWidget) {
                        if (isSubPageCreationAllowed) {
                            if (this._userAllowedToCreatePagsOnTheCurrentSite) {
                                jQuery(createPageWidget.get_element()).show();
                            }
                            else {
                                jQuery(createPageWidget.get_element()).hide();
                            }
                        }
                        else
                            jQuery(createPageWidget.get_element()).hide();
                    }
                    if (createChildWidget) {
                        jQuery(createChildWidget.get_element()).hide();
                    }
                    //moreActionsWidget.hideWidgetByName("moveUp");
                    //moreActionsWidget.hideWidgetByName("moveDown");
                }
                moreActionsWidget.hideWidgetByName("batchChangeTemplate");
                moreActionsWidget.showWidgetByName("changeTemplate");
                break;
            case Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.Multiple:
                deletePageWidget.set_enabled(true);
                moreActionsWidget.set_enabled(true);
                if (createPageWidget) {
                    if (this._userAllowedToCreatePagsOnTheCurrentSite) {
                        jQuery(createPageWidget.get_element()).show();
                    }
                    else {
                        jQuery(createPageWidget.get_element()).hide();
                    }
                }
                if (createChildWidget) {
                    jQuery(createChildWidget.get_element()).hide();
                }
                moreActionsWidget.showWidgetByName("batchChangeTemplate");
                moreActionsWidget.hideWidgetByName("changeTemplate");
                if (isInTreeMode) {
                    //moreActionsWidget.showWidgetByName("moveUp");
                    //moreActionsWidget.showWidgetByName("moveDown");
                }
                else {
                    //moreActionsWidget.hideWidgetByName("moveUp");
                    //moreActionsWidget.hideWidgetByName("moveDown");
                }
                break;
        }
        moreActionsWidget.hideEmptySections();
    },

    _getSelectionType: function (selectedItems) {
        if (!selectedItems || selectedItems.length == 0) {
            return Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.None;
        }
        if (selectedItems.length == 1) {
            return Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.Single;
        }
        else {
            return Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.SelectionType.Multiple;

        }
    },

    _configureMoreActionsMenu: function (dataItem, containerElement) {

        var isInTreeMode = this._isInTreeViewMode();
        var itemsList = this._masterView.get_currentItemsList();

        if (dataItem.IsGroup) {
            if (isInTreeMode) {
                itemsList.keepActionsMenuItems(this._actionCommandsCssForGroupPages, containerElement);
            }
            else {
                itemsList.keepActionsMenuItems(this._actionCommandsCssForGroupPagesFlatMode, containerElement);
            }
        }
        if (dataItem.IsExternal)
            if (isInTreeMode)
                itemsList.keepActionsMenuItems(this._actionCommandsCssForRedirectingPages, containerElement);
            else
                itemsList.keepActionsMenuItems(this._actionCommandsCssForRedirectingPagesFlatMode, containerElement);

        if (!dataItem.IsContentEditable) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "editPageContent"], containerElement);
        }

        if (dataItem.PageLifecycleStatus && dataItem.PageLifecycleStatus.IsLocked == true && dataItem.IsUnlockable) {
        } else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unlockPage"], containerElement);
        }

        if (dataItem.PageLifecycleStatus && dataItem.PageLifecycleStatus.IsLocked == true) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "changeTemplate"], containerElement);
        }

        if (!dataItem.IsSubPageCreationAllowed) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "createChild"], containerElement);
        }

        if (!isInTreeMode) {
            itemsList.removeActionsMenuItems(this._actionCommandsToHideInFlatMode, containerElement);
        }

        if (dataItem.Status && dataItem.Status == "Published") {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "publishDraft"], containerElement);
        }
        else {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "unpublishPage"], containerElement);
        }

        if (!this._masterView._sharePageLinkEnabled) {
            itemsList.removeActionsMenuItems([this._actionCommandPrefix + "shareLink"], containerElement);
        }
    },

    _batchChangeTemplate: function (serviceUrl, binder, args, clientManager, itemsGrid) {
        serviceUrl += 'batchChangeTemplate/';

        var selectedItems = binder.get_selectedItems();
        if (selectedItems && selectedItems.length > 0) {

            var context = { Command: "batchChangeTemplate" };

            var failureFunction = this._serviceFailure;
            var successFunction = this._serviceSuccessDelegate;
            var that = this;
            var f = function () {
                var urlParams = [];
                var template = args;
                urlParams["newTemplateId"] = template.Id;

                var keys = [];
                var data = [];
                var count = selectedItems.length;
                while (count--) {
                    data.push(selectedItems[count].Id);
                }
                if (that._isMultilingual()) {
                    clientManager.set_uiCulture(that._getCurrentLanguage());
                }
                clientManager.InvokePut(serviceUrl, urlParams, keys, data, successFunction, failureFunction, itemsGrid, false, null, context);
            };
            var pdFunction = function (sender, args) {
                if (args.get_commandName() == "ok") {
                    f();
                }
            };

            var publishedItems = new Array();
            var count = selectedItems.length;
            while (count--) {
                var item = selectedItems[count];
                if (item.Status == "Published") {
                    publishedItems.push(item);
                }
            }

            if (publishedItems.length > 0) {
                itemsGrid.showPromptDialogByName("changeTemplatesWarningDialog", null, null, pdFunction);
            } else {
                f();
            }
        }
    },

    _isMultilingual: function () {
        var binder = this._masterView.get_currentItemsList().getBinder();
        return binder._isMultilingual;
    },

    _getCurrentLanguage: function () {
        var binder = this._masterView.get_currentItemsList().getBinder();
        return binder.get_uiCulture();
    },

    _changeTemplate: function (serviceUrl, args, itemsGrid, clientManager) {
        serviceUrl += 'changeTemplate/';

        var urlParams = [];

        var context = { Command: "changeTemplate" };

        var dataItem;
        if (args["Template"] !== undefined) {
            dataItem = args;
            urlParams["newTemplateId"] = dataItem.Template.Id;
        }
        else { //fired from the toolbar
            dataItem = itemsGrid.get_dataItem();
            urlParams["newTemplateId"] = args.Id;
        }

        var failureFunction = this._serviceFailure;
        var successFunction = this._serviceSuccessDelegate;
        var that = this;
        var f = function () {
            var keys = [];
            keys.push(dataItem.Id);
            if (that._isMultilingual()) {
                clientManager.set_uiCulture(that._getCurrentLanguage());
            }
            clientManager.InvokePut(serviceUrl, urlParams, keys, dataItem.Id, successFunction, failureFunction, itemsGrid, false, null, context);
        };
        var pdFunction = function (sender, args) {
            if (args.get_commandName() == "ok") {
                f();
            }
        };

        if (dataItem.Status == "Published") {
            itemsGrid.showPromptDialogByName("changeTemplateWarningDialog", null, null, pdFunction);
        } else {
            f();
        }

    },

    _navigateToEditPage: function (sender, args) {
        $('body').addClass('sfLoadingTransition');

        var dataItem = args.get_dataItem();
        var language = null;
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
        if (dataItem != null && dataItem.hasOwnProperty('NavigateUrl')) {
            var url = this._getLocalizedEditUrl(dataItem, language);
            this._editPageContent(url);
            args.set_cancel(true);
        }
    },

    _getLocalizedEditUrl: function (dataItem, language) {
        var url = dataItem.NavigateUrl;
        var query;
        var queryIdx = dataItem.NavigateUrl.lastIndexOf("?");
        if (queryIdx >= 0) {
            url = dataItem.NavigateUrl.substring(0, queryIdx);
            query = dataItem.NavigateUrl.substring(queryIdx);
        }

        if (language) {
            url += "/" + language;
        }
        else if (dataItem.Language) {
            url += "/" + dataItem.Language;
        }

        if (query)
            url += query;

        return url;
    },

    _setPermissionsForAllPagesDialogSecuredObjectIdToCurrentSite: function (sitemapRootId, currentItemsList) {
        if (currentItemsList) {
            var radWinMgr = currentItemsList._radWindowManager;
            if (radWinMgr) {
                var permissionsForAllPagesDialog = radWinMgr.GetWindowByName("permissions");
                if (permissionsForAllPagesDialog) {
                    var permissionsUrl = permissionsForAllPagesDialog.GetUrl();
                    permissionsUrl = permissionsUrl.replace(/securedObjectId=[^&]+/, "securedObjectId=" + sitemapRootId);
                    permissionsForAllPagesDialog.set_navigateUrl(permissionsUrl);
                }
            }
            var dialogParameters = currentItemsList.get_dialogParameters();
            if ((dialogParameters) && (dialogParameters.hasOwnProperty("permissions"))) {
                var permsDlgParams = dialogParameters["permissions"];
                permsDlgParams = permsDlgParams.replace(/securedObjectId=[^&]+/, "securedObjectId=" + sitemapRootId);
                dialogParameters["permissions"] = permsDlgParams;
            }
        }
    }


    /* -------------------- properties ---------------- */

}

Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.PagesMasterGridViewExtension', Sys.Component);

Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender = function (element) {
    Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender.initializeBase(this, [element]);
    this._element = element;
    this._actionsMenu = null;
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender.callBaseMethod(this, 'dispose');
    }
}

Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender.registerClass('Telerik.Sitefinity.Modules.Pages.Web.UI.ActionsMenuExtender', Sys.UI.Behavior);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
