// called by the MasterGridView when it is loaded
function OnMasterViewLoaded(sender, args) {
    var masterView = sender;
    var extender = new Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension(masterView);
    extender.initialize();
}

Type.registerNamespace("Telerik.Sitefinity.Modules.UserProfiles.Web.UI");

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension = function (masterView) {
    Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.initializeBase(this);

    // Main components
    this._masterView = masterView;
    this._binder = null;

    this._itemsGrid = {};
    this._itemsList = {};
    this._itemsTreeTable = {};

    this._actionCommandPrefix = "sf_binderCommand_";
    // commands for tree mode
    this._actionCommandsCssForGroupPages = [this._actionCommandPrefix + "delete", this._actionCommandPrefix + "permissions", this._actionCommandPrefix + "edit", this._actionCommandPrefix + "changePageOwner", this._actionCommandPrefix + "moveUp", this._actionCommandPrefix + "moveDown"];
    this._actionCommandsCssForRedirectingPages = this._actionCommandsCssForGroupPages;
    // commands for flat mode
    this._actionCommandsCssForGroupPagesFlatMode = [this._actionCommandPrefix + "delete", this._actionCommandPrefix + "permissions", this._actionCommandPrefix + "edit", this._actionCommandPrefix + "changePageOwner"];
    this._actionCommandsCssForRedirectingPagesFlatMode = this._actionCommandsCssForGroupPagesFlatMode;
    // commands to hide in flat mode
    this._actionCommandsToHideInFlatMode = [this._actionCommandPrefix + "moveUp", this._actionCommandPrefix + "moveDown"];

    // Event delegates
    this._dialogClosedDelegate = null;
    this._masterCommandDelegate = null;
    this._itemCommandDelegate = null;
    this._selectionChangedDelegate = null;
    this._workAreaClickDelegate = null;

    this._serviceSuccessDelegate = null;
    this._onItemDomCreatedDelegate = null;

    this._clientManager = null;
}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.SelectionType = { None: 0, Single: 1, Multiple: 2 };
Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.callBaseMethod(this, 'initialize');

        this._masterCommandDelegate = Function.createDelegate(this, this._masterCommandHandler);
        this._masterBeforeCommandDelegate = Function.createDelegate(this, this._masterBeforeCommandHandler);
        this._itemCommandDelegate = Function.createDelegate(this, this._itemCommandHandler);
        this._dialogClosedDelegate = Function.createDelegate(this, this._dialogClosedHandler);
        this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChangedHandler);
        this._itemDataBoundDelegate = Function.createDelegate(this, this._itemDataBoundHandler);
        this._serviceSuccessDelegate = Function.createDelegate(this, this._serviceSuccess);
        this._onItemDomCreatedDelegate = Function.createDelegate(this, this._onItemDomCreated);

        this._itemsGrid = this._masterView.get_itemsGrid();
        this._itemsList = this._masterView.get_itemsList();
        this._itemsTreeTable = this._masterView.get_itemsTreeTable();

        if (this._itemsGrid) {
            this._itemsGrid.add_command(this._masterCommandDelegate);
            this._itemsGrid.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsGrid.add_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsGrid.add_selectionChanged(this._selectionChangedDelegate);
            this._itemsGrid.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsGrid.getBinder().set_unescapeHtml(true);
        }
        if (this._itemsList) {
            this._itemsList.add_command(this._masterCommandDelegate);
            this._itemsList.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsList.add_itemCommand(this._itemCommandDelegate);
            this._itemsList.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsList.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsList.getBinder().set_unescapeHtml(true);
        }
        if (this._itemsTreeTable) {
            this._itemsTreeTable.add_command(this._masterCommandDelegate);
            this._itemsTreeTable.add_beforeCommand(this._masterBeforeCommandDelegate);
            this._itemsTreeTable.add_itemCommand(this._itemCommandDelegate);
            this._itemsTreeTable.add_dialogClosed(this._dialogClosedDelegate);
            this._itemsTreeTable.add_selectionChanged(this._selectionChangedDelegate);
            this._itemsTreeTable.getBinder().add_onItemDataBound(this._itemDataBoundDelegate);
            this._itemsTreeTable.getBinder().set_unescapeHtml(true);

            this._itemsTreeTable.getBinder().add_onItemDomCreated(this._onItemDomCreatedDelegate);
        }

        this._workAreaClickDelegate = Function.createDelegate(this, this._workAreaClickHandler);

        this._configureWidgets(null);

        jQuery(".sfWorkArea").click(this._workAreaClickDelegate);
        $(".sfWorkArea").on("unload", function(e) {
                jQuery.event.remove(this);
                jQuery.removeData(this);
            });

    },

    dispose: function () {
        Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.callBaseMethod(this, 'dispose');

        if (this._itemsGrid) {
            this._itemsGrid.remove_command(this._masterCommandDelegate);
            this._itemsGrid.remove_itemCommand(this._itemCommandDelegate);
            this._itemsGrid.remove_dialogClosed(this._dialogClosedDelegate);
        }
        if (this._itemsList) {
            this._itemsList.remove_command(this._masterCommandDelegate);
            this._itemsList.remove_itemCommand(this._itemCommandDelegate);
            this._itemsList.remove_dialogClosed(this._dialogClosedDelegate);
        }

        if (this._itemsTreeTable) {
            this._itemsTreeTable.remove_command(this._masterCommandDelegate);
            this._itemsTreeTable.remove_itemCommand(this._itemCommandDelegate);
            this._itemsTreeTable.remove_dialogClosed(this._dialogClosedDelegate);

            if (this._selectionChangedDelegate) {
                this._itemsTreeTable.remove_selectionChanged(this._selectionChangedDelegate);
                delete this._selectionChangedDelegate;
            }

            this._itemsTreeTable.getBinder().remove_onItemDomCreated(this._onItemDomCreatedDelegate);
        }

        delete this._masterCommandDelegate;
        delete this._itemCommandDelegate;
        delete this._dialogClosedDelegate;
        delete this._onItemDomCreatedDelegate;
    },

    /* --------------------  public methods ----------- */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _masterBeforeCommandHandler: function (sender, args) {

    },

    // handles commands fired the master view
    _masterCommandHandler: function (sender, args) {
        //        var commandName = args.get_commandName();
        //        if (args.get_cancel() === false) {
        //            switch (commandName) {
        //                case 'create':
        //                case "groupDelete":
        //                    // do nothing, let the master view handle that 
        //                    break;
        //                case 'searchGrid':
        //                    this._configureMasterView(true);
        //                    var binder = this._masterView.get_currentItemsList().getBinder();
        //                    delete binder.get_urlParams()['pageFilter'];
        //                    //HACK: the order of execution flow should be changed
        //                    this._masterView.get_binderSearch().search(args.get_commandArgument().get_query());
        //                    var sidebar = this._masterView.get_sidebar();
        //                    var allPagesWidget = sidebar.getWidgetByName("AllPages");
        //                    this._masterView._markItemSelected(this._masterView._previousFilterWidget, false);
        //                    this._masterView._markItemSelected(allPagesWidget, true);
        //                    this._masterView.set_previousFilterWidget(allPagesWidget);
        //                    break;
        //                case "sort":
        //                    {
        //                        var sortExpression = args.get_commandArgument();
        //                        this._masterView.set_currentItemsList(this._masterView.get_itemsGrid());
        //                        var binder = this._masterView.get_currentItemsList().getBinder();
        //                        //binder.get_urlParams()['pageFilter'] = 'MyPages';
        //                        //binder.set_isFiltering(true);
        //                        binder.set_sortExpression(sortExpression);
        //                        binder.DataBind();
        //                    }
        //                    break;
        //                case 'customFields':
        //                    alert('In process of implementation.');
        //                    break;
        //                case 'notImplemented':
        //                    alert('In process of implementation.');
        //                    break;
        //                default:
        //                    //alert('In process of implementation.');
        //                    break;
        //            }
        //        }
        //        else {
        //            switch (commandName) {
        //                case 'showSectionsExcept':
        //                    this._configureMasterView(true);
        //                    break;
        //                case "showCustomRange":
        //                case 'hideSectionsExcept':
        //                case 'filter':
        //                    break;
        //                default:
        //                    //alert('In process of implementation.');
        //            }
        //        }
    },

    // handles the commands fired by a single item
    _itemCommandHandler: function (sender, args) {
        //        var dataItem = args.get_commandArgument();
        //        var currentList = this._masterView.get_currentItemsList();
        //        var binder = currentList.getBinder();

        //        switch (args.get_commandName()) {
        //            case 'delete':
        //                this._processDeleteCommand(dataItem, args);
        //                break;
        //        }
    },

    // Fires when a dialog is closed
    _dialogClosedHandler: function (sender, args) {
        if (Telerik.Sitefinity.DialogClosedEventArgs.isInstanceOfType(args)) {
            var context = args.get_context();
            var dataItem = args.get_dataItem();

            if (args.get_isCreated() == true) {
                dataItem.RefreshParentOnCancel = "true";
                var commandName = 'moduleEditor';
                var list = this._masterView.get_currentItemsList();
                var key = dataItem.Id;
                args.set_cancel(true);
                list.executeItemCommand(commandName, dataItem, key, {});
            } else if (args.get_isUpdated() == true) {

            }
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

    // handles the clicks in the blank area around the items tree table
    _workAreaClickHandler: function (e) {
        var currentList = this._masterView.get_currentItemsList();
        if (Object.getTypeName(currentList) == 'Telerik.Sitefinity.Web.UI.ItemLists.ItemsTreeTable') {
            var binder = currentList.getBinder();
            binder.clearSelection();
            this._configureWidgets(Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.SelectionType.None);
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
        var currentViewIsHierarchical = sortingDropdown.value === "showHierarchical";
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

    _getClientManager: function () {
        if (!this._clientManager) {
            this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
        }

        return this._clientManager;
    },

    // utility methods
    _getBaseUrl: function (itemsListBase) {
        var baseUrl = itemsListBase.get_serviceBaseUrl();
        var serviceUrl = baseUrl.toString().substr(0, baseUrl.toString().indexOf('?'));
        return serviceUrl;
    },

    _serviceSuccess: function (caller, data, request, context) {
        if (context) {
            caller.dataBind(null, context);
        } else {
            caller.dataBind();
        }
    },

    _batchServiceSuccess: function (caller, sender, args) {

    },

    _serviceFailure: function (sender, args) {
        alert(sender.Detail);

        if (args.dataBind) {
            args.dataBind();
        }
    },

    _configureWidgets: function (selectionType) {


    },

    _getSelectionType: function (selectedItems) {
        if (!selectedItems || selectedItems.length == 0) {
            return Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.SelectionType.None;
        }
        if (selectedItems.length == 1) {
            return Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.SelectionType.Single;
        }
        else {
            return Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.SelectionType.Multiple;
        }
    },

    _configureMoreActionsMenu: function (dataItem, containerElement) {

    }
}

Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension.registerClass('Telerik.Sitefinity.Modules.UserProfiles.Web.UI.UserProfileTypesMasterGridViewExtension', Sys.Component);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
