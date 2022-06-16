﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.GenericPageSelector = function (element) {
    this._element = element;
    this._itemsGrid = null;
    this._itemsTree = null;
    this._searchBox = null;
    this._rootNodeId = null;
    this._baseServiceUrl = null;
    this._provider = null;
    this._clearFilterWrpClientID = null;
    this._clearFilterWrp = null;
    this._clearFilterButtonClientID = null;
    this._clearFilterButton = null;    
    this._serviceChildItemsBaseUrl = null;
    this._servicePredecessorBaseUrl = null;
    this._serviceTreeUrl = null;
    this._orginalServiceBaseUrl = null;

    this._bindOnLoad = true;

    this._isTreeMode = false;
    this._selectedItemId = null;
    this._selectedItemIds = null;
    //Whether the grid is bound
    this._gridIsBound = false
    //Whether the grid must be updated to match the current selected pages
    this._gridMustBeUpdated = false;
    //Whether the tree is bound
    this._treeIsBound = false
    //Whether the tree must be updated to match the current selected pages
    this._treeMustBeUpdated = false;

    this._onloadDelegate = null;
    this._selectionChangedDelegate = null;
    this._searchDelegate = null;
    this._clearFilterDelegate = null;
    this._gridBinderDataBoundDelegate = null;
    this._treeBinderDataBoundDelegate = null;
    this._treeItemDataBoundDelegate = null;
    this._treeSelectionChangingDelegate = null;
    this._showOnlySelectedAsGridOnLoad = null;
    this._operationFailureDelegate = null;

    this._culture = null;
    this._uiCulture = null;
    this._markItemsWithoutTranslation = false;
    this._privateUnselectableItems = [];
    this._unselectableItems = [];
    this._unselectableClass;
    this._siteSelector = null;
    this._siteSelectorClickedDelegate = null;
    this._languageSelectorSelectedCulture = null;
    this._languageSelector = null;
    this._languageSelectorClickedDelegate = null;
    this._allSitesAvailableCultures = null;
    this._rootNodeIdOnEdit = null;
    this._targetLibraryId = null;

    Telerik.Sitefinity.Web.UI.GenericPageSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.GenericPageSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.GenericPageSelector.callBaseMethod(this, "initialize");
        if (this._onloadDelegate == null) {
            this._onloadDelegate = Function.createDelegate(this, this._onload);
        }
        Sys.Application.add_load(this._onloadDelegate);

        this._showOnlySelectedAsGridOnLoad = Sys.Serialization.JavaScriptSerializer.deserialize(this._showOnlySelectedAsGridOnLoad);

        if (this._selectionChangedDelegate == null) {
            this._selectionChangedDelegate = Function.createDelegate(this, this._selectionChanged);
        }
        this.get_itemsGrid().add_selectionChanged(this._selectionChangedDelegate);
        this.get_itemsTree().add_selectionChanged(this._selectionChangedDelegate);

        this.get_itemsGrid().getBinder().set_restoreSelectionAfterRebind(true);

        if (this.get_searchBox()) {
            if (this._searchDelegate == null) {
                this._searchDelegate = Function.createDelegate(this, this._search);
            }
            this.get_searchBox().add_manualSearch(this._searchDelegate);
            this.get_searchBox().set_enableManualSearch(true);
        }

        this._clearFilterWrpClientID = "clearFilterWrp";
        this._clearFilterWrp = $get(this._clearFilterWrpClientID, this.get_element());
        this._clearFilterButtonClientID = "clearFilter";
        this._clearFilterButton =
        $get(this._clearFilterButtonClientID, this.get_element())
        if (this._clearFilterDelegate == null) {
            this._clearFilterDelegate = Function.createDelegate(this, this._clearFilter);
        }
        $addHandler(this._clearFilterButton, 'click', this._clearFilterDelegate);

        //Initialize tree view
        var serviceChildItemsBaseUrl = this._serviceChildItemsBaseUrl;
        if (!serviceChildItemsBaseUrl)
            serviceChildItemsBaseUrl = this.get_baseServiceUrl() + "children/";
        var serviceTreeBaseUrl = this._serviceTreeUrl;
        if (!serviceTreeBaseUrl)
            serviceTreeBaseUrl = this.get_baseServiceUrl() + "tree/";
        var servicePredecessorBaseUrl = this._servicePredecessorBaseUrl;
        if (!servicePredecessorBaseUrl)
            servicePredecessorBaseUrl = this.get_baseServiceUrl() + "predecessor/";
        var treeBinder = this.get_treeBinder();
        this._setOriginalServiceUrl();
        treeBinder.set_serviceChildItemsBaseUrl(serviceChildItemsBaseUrl);
        treeBinder.set_servicePredecessorBaseUrl(servicePredecessorBaseUrl);
        treeBinder.set_serviceTreeUrl(serviceTreeBaseUrl);
        treeBinder.set_enableInitialExpanding(true);

        if (!this._treeBinderDataBoundDelegate) {
            this._treeBinderDataBoundDelegate = Function.createDelegate(this, this._treeDataBound);
            treeBinder.add_onDataBound(this._treeBinderDataBoundDelegate);
        }

        this._treeItemDataBoundDelegate = Function.createDelegate(this, this._treeItemDataBound);
        this.get_treeBinder().add_onItemDataBound(this._treeItemDataBoundDelegate);

        this._treeSelectionChangingDelegate = Function.createDelegate(this, this._treeSelectionChanging);
        this._itemsTree.add_selectionChanging(this._treeSelectionChangingDelegate);

        this._operationFailureDelegate = Function.createDelegate(this, this._operationFailureHandler);
        this.get_itemsTree().add_operationFailure(this._operationFailureDelegate);

        //hook sites selector
        if (this._siteSelector) {
            this._siteSelectorClickedDelegate = Function.createDelegate(this, this._handleSiteSelected);
            this._siteSelector.add_onSiteSelected(this._siteSelectorClickedDelegate);
        }
        if (this._languageSelector) {
            this._languageSelectorClickedDelegate = Function.createDelegate(this, this._handleLanguageSelected);
            this._languageSelector.add_languageChanged(this._languageSelectorClickedDelegate);
        }

        if (this.get_languageSelector()) {
            var dropDown = this.get_languageSelector()._dropDownList;

            if (dropDown.options.length < 2) {
                //If we have only one language for the current site we need to hide the language selector.
                jQuery(this.get_languageSelector().get_element()).closest(".sfFormCtrl").hide();
            } else {
                dropDown.value = this.get_uiCulture();
            }
        }
    },

    dispose: function () {
        if (this._onloadDelegate) {
            Sys.Application.remove_load(this._onloadDelegate);
            delete this._onloadDelegate;
        }

        if (this._selectionChangedDelegate) {
            if (this.get_itemsGrid()) {
                this.get_itemsGrid().remove_selectionChanged(this._selectionChangedDelegate);
            }
            if (this.get_itemsTree()) {
                this.get_itemsTree().remove_selectionChanged(this._selectionChangedDelegate);
            }
            delete this._selectionChangedDelegate;
        }

        if (this._searchDelegate) {
            if (this.get_searchBox()) {
                this.get_searchBox().remove_manualSearch(this._searchDelegate);
            }
            delete this._searchDelegate;
        }

        if (this._clearFilterDelegate) {
            if (this._clearFilterButton) {
                $removeHandler(this._clearFilterButton, 'click', this._clearFilterDelegate);
            }
            delete this._clearFilterDelegate;
        }
        if (this._treeItemDataBoundDelegate) {
            this.get_treeBinder().remove_onItemDataBound(this._treeItemDataBoundDelegate);
            delete this._treeItemDataBoundDelegate;
        }
        this._itemsTree.remove_selectionChanging(this._treeSelectionChangingDelegate);
        delete this._treeSelectionChangingDelegate;

        if (this._siteSelector) {
            this._siteSelector.remove_onSiteSelected(this._siteSelectorClickedDelegate);
            delete this._siteSelectorClickedDelegate;
        }

        if (this._operationFailureDelegate) {
            if (this.get_itemsTree()) {
                this.get_itemsTree().remove_operationFailure(this._operationFailureDelegate);
            }
            delete this._operationFailureDelegate;
        }

        Telerik.Sitefinity.Web.UI.GenericPageSelector.callBaseMethod(this, "dispose");
    },

    _operationFailureHandler: function (sender, args) {
        if (this.get_selectedItemId() || (this.get_selectedItemIds() && this.get_selectedItemIds().length > 0)) {
            args.set_cancel(true);
            this.set_selectedItemId(null);
            this.set_selectedItemIds([]);
            this.dataBind();
        }
    },

    /* --------------------  public methods ----------- */

    set_selectedItems: function (items) {
        this._updateSelectedItemIds(items);
        this._updateGridSelection();
        this._updateTreeSelection();
    },

    set_selectedItemIds: function (ids) {
        this._selectedItemIds = ids;
        this._updateGridSelection();
        this._updateTreeSelection();
    },

    // Binds the page selector to the data.
    dataBind: function (query) {
        if (this.get_treeBinder() && this.get_treeBinder().get_urlParams() && this.get_rootNodeId() && !this.get_treeBinder().get_urlParams().root)
            this.get_treeBinder().get_urlParams().root = this.get_rootNodeId();

        if (query && query.length > 0) {
            this._configureGridMode(query);
        }
        else {
            this._configureTreeMode();
        }
    },

    changeSite: function (siteId, rootNodeId) {
        if (!siteId || !rootNodeId) {
            return;
        }

        if (rootNodeId != this._rootNodeId) {
            this.set_rootNodeId(rootNodeId);
        }
        
        var treeBinder = this.get_itemsTree().getBinder();
        
        var urlParams = treeBinder.get_urlParams() || {};
        urlParams["sf_site"] = siteId;
        treeBinder.set_urlParams(urlParams);

        this.clearSelection();
    },

    changeCulture: function (culture) {
        if (!culture || this._uiCulture === culture) return;

        this._uiCulture = culture;

        if (this.get_itemsGrid() != null) {
            this.get_itemsGrid().getBinder().set_uiCulture(culture);
        }

        if (this.get_itemsTree() != null) {
            this.get_itemsTree().getBinder().set_uiCulture(culture);
        }
    },

    // Clears all selected items.
    clearSelection: function () {
        this.set_selectedItemIds(null);
        this.set_selectedItemId(null);

        if (this._isTreeMode) {
            return this.get_treeBinder().clearSelection();
        }
        else {
            return this.get_gridBinder().clearSelection();
        }
    },

    /* -------------------- events -------------------- */

    // Happens when selection of the pages is changed (node is checked or unchecked)
    add_selectionChanged: function (delegate) {
        this.get_events().addHandler("selectionChanged", delegate);
    },

    // Happens when selection of the pages is changed (node is checked or unchecked)
    remove_selectionChanged: function (delegate) {
        this.get_events().removeHandler("selectionChanged", delegate);
    },

    add_selectionApplied: function (handler) {
        /// <summary>
        /// Raised when the pre-set selection was actually applied after data is bound.
        /// </summary>
        this.get_events().addHandler("selectionApplied", handler);
    },

    remove_selectionApplied: function (handler) {
        this.get_events().removeHandler("selectionApplied", handler);
    },

    // Happens on item data bound of the tree binder
    add_onTreeBinderItemDataBound: function (delegate) {
        this.get_events().addHandler("treeBinderItemDataBound", delegate);
    },

    // Happens on item data bound of the tree binder
    remove_onTreeBinderItemDataBound: function (delegate) {
        this.get_events().removeHandler("treeBinderItemDataBound", delegate);
    },

    // Happens on data bound of the tree binder
    add_onTreeBinderDataBound: function (delegate) {
        this.get_events().addHandler("treeBinderDataBound", delegate);
    },

    // Happens on data bound of the tree binder
    remove_onTreeBinderDataBound: function (delegate) {
        this.get_events().removeHandler("treeBinderDataBound", delegate);
    },

    _raiseSelectionApplied: function (args) {
        var handler = this.get_events().getHandler("selectionApplied");
        if (handler) handler(this, args);
    },

    _raiseTreeBinderItemDataBound: function (args) {
        var handler = this.get_events().getHandler("treeBinderItemDataBound");
        if (handler) handler(this, args);
    },

    _raiseTreeBinderDataBound: function (args) {
        var handler = this.get_events().getHandler("treeBinderDataBound");
        if (handler) handler(this, args);
    },

    /* -------------------- event handlers ------------ */

    _onload: function () {
        var siteRootNodeId;
        if (this._rootNodeIdOnEdit) {
            siteRootNodeId = this._rootNodeIdOnEdit;
        }

        if (this.get_siteSelector() && this.get_siteSelector().get_sitesDropDown()) {
            var siteSelectorDropDown = this.get_siteSelector().get_sitesDropDown();
            if (this._rootNodeIdOnEdit) {
                // set currently selected site to be the one from the edited link
                $(siteSelectorDropDown).val(this._rootNodeIdOnEdit);
            }
            else {
                // set the rootNodeId to the one of the currently selected site
                siteRootNodeId = $(siteSelectorDropDown).val();
            }
        }

        if (siteRootNodeId) {
            this.set_rootNodeId(siteRootNodeId);
        }

        if (this.get_bindOnLoad()) {
            this.dataBind();
        }
    },

    _handleSiteSelected: function (sender, args) {
        var siteRootNodeId = args.RootNodeId;

        if (this._allSitesAvailableCultures && this.get_languageSelector()) {
            var currentSiteLanguages = Sys.Serialization.JavaScriptSerializer.deserialize(this._allSitesAvailableCultures)[args.SiteId],
                languagesDataSource = Sys.Serialization.JavaScriptSerializer.deserialize(currentSiteLanguages);
            this._clearLanguageSelector();
            this._bindLanguageSelector(languagesDataSource);
        }

        if (siteRootNodeId) {
            this.set_rootNodeId(siteRootNodeId);
        }
        this.clearSelection();

        this.get_treeBinder()._clearExpandingNodesCookie();
        if (this.get_treeBinder().get_enableInitialExpanding()) {
            this.get_treeBinder().set_enableInitialExpanding(false);
        }
        this._configureTreeMode();
    },

    _handleLanguageSelected: function (sender, args) {
        this._clearFilter();
        this.set_languageSelectorSelectedCulture(args._value);
        this.set_uiCulture(args._value);
        this.get_itemsTree().set_uiCulture(args._value);
        this.clearSelection();
        this.get_treeBinder()._clearExpandingNodesCookie();
        if (this.get_treeBinder().get_enableInitialExpanding()) {
            this.get_treeBinder().set_enableInitialExpanding(false);
        }
        this._configureTreeMode();
    },

    _bindLanguageSelector: function (dataSource) {
        var count = 0;
        var dropDown = this.get_languageSelector()._dropDownList;
        for (var propertyName in dataSource) {
            this._addOption(dataSource[propertyName], propertyName);
            count++;
        }
        if (count < 2) {
            //If we have only one language for the current site we need to hide the language selector.
            jQuery(this.get_languageSelector().get_element()).closest(".sfFormCtrl").hide();
        }

        this.set_uiCulture(dropDown.options[0].value);
        this.get_itemsTree().set_uiCulture(dropDown.options[0].value);
        this.get_treeBinder().set_isMultilingual(count > 1);
        this._markItemsWithoutTranslation = true;
    },

    _clearLanguageSelector: function (dataSource) {
        var dropDown = this.get_languageSelector()._dropDownList;
        dropDown.options.length = 0;
        jQuery(this.get_languageSelector().get_element()).closest(".sfFormCtrl").show();
    },

    _addOption: function (text, value) {
        var dropDown = this.get_languageSelector()._dropDownList;

        dropDown.options[dropDown.options.length] = new Option(text, value);
    },

    _selectionChanged: function (sender, args) {
        if (this._gridMustBeUpdated) {
            this._selectedItemIds = null;
        }

        this._raiseSelectionChanged(args);
    },

    _search: function (sender, args) {
        if (this._isTreeMode) {
            this._switchToGridMode();
        }

        var query = args.get_query();
        this.dataBind(query);
    },

    _clearFilter: function (sender, args) {
        if (this.get_searchBox()) {
            this.get_searchBox().get_textBox().value = "";
        }
        this.clearSelection();
        this._switchToTreeMode();
        return false;
    },

    _switchToTreeMode: function () {
        var selectedItems = this.get_selectedItems();
        this._updateSelectedItemIds(selectedItems);
        this._configureTreeMode();
    },
    
    _switchToGridMode: function () {
        var selectedItems = this.get_selectedItems();
        this._updateSelectedItemIds(selectedItems);
        //this._configureGridMode();
    },

    _raiseSelectionChanged: function (selectedItems) {
        var eventArgs = selectedItems;
        var handler = this.get_events().getHandler("selectionChanged");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    _gridDataBound: function (sender, args) {
        this._gridIsBound = true;
        var selectedItems = this.get_selectedItems();
        this._updateSelectedItemIds(selectedItems);
        this._updateGridSelection();
    },

    _treeDataBound: function (sender, args) {
        this._treeIsBound = true;
        this._updateTreeSelection();
        this.get_itemsTree().show();
        this._markUnselectableItems();

        this._raiseTreeBinderDataBound(args);
    },

    _treeItemDataBound: function (sender, args) {
        if (sender.get_isMultilingual() == true) {
            if (this._markItemsWithoutTranslation) {
                var dataItem = args.get_dataItem();
                var culture = this._uiCulture;
                var notTranslated = jQuery.inArray(culture, dataItem.AvailableLanguages) == -1;
                var itemIndex = jQuery.inArray(dataItem.Id, this._privateUnselectableItems);
                if (notTranslated) {
                    if (itemIndex == -1) {
                        this._privateUnselectableItems.push(dataItem.Id);
                    }
                }
                else {
                    if (itemIndex > -1)
                        this._privateUnselectableItems.splice(itemIndex, 1);
                }
            }
        }
        this._raiseTreeBinderItemDataBound(args);
    },

    _treeSelectionChanging: function (sender, args) {
        if (this._checkUnselectedItem(args.get_node().get_value()))
            args.set_cancel(true);
    },

    /* -------------------- private methods ----------- */

    _configureGridMode: function (query) {
        this._isTreeMode = false;
        var binder = this.get_gridBinder();

        if (!this._gridBinderDataBoundDelegate) {
            this._gridBinderDataBoundDelegate = Function.createDelegate(this, this._gridDataBound);
            binder.add_onDataBound(this._gridBinderDataBoundDelegate);
        }

        this.get_itemsGrid().show();
        this.get_itemsTree().hide();
        jQuery(this._clearFilterButton).show();
        jQuery(this._clearFilterWrp).show();
        if (this._provider) {
            binder.set_provider(this._provider);
        }
        this.get_itemsGrid().applyFilter(query);
    },

    _configureTreeMode: function () {
        this._isTreeMode = true;
        var tree = this.get_itemsTree();

        if (this.get_searchBox()) {
            this.get_searchBox().get_textBox().value = "";
        } else {
            jQuery(this._clearFilterWrp).hide();
        }

        this.get_itemsGrid().hide();
        jQuery(this._clearFilterButton).hide();

        var originalBindOnSuccess = tree.get_bindOnSuccess();
        tree.set_bindOnSuccess(false);
        tree.applyFilter("");
        tree.set_bindOnSuccess(originalBindOnSuccess);
        var bindingContext = null;
        if (this.get_selectedItemIds() && this.get_selectedItemIds().length > 0) {
            bindingContext = { nodeIdsToExpand: this.get_selectedItemIds() };
        }
        else if (this.get_selectedItemId()) {
            bindingContext = this.get_selectedItemId();
        }
        if (this._provider) {
            this.get_treeBinder().set_provider(this._provider);
        }

        var targetLibraryId = this.get_targetLibraryId();
        if (targetLibraryId && targetLibraryId != Telerik.Sitefinity.getEmptyGuid()) {
            this.get_treeBinder().set_filterExpression("Id==" + this.get_targetLibraryId());
            this.get_treeBinder().set_isTargetLibrary(true);
        } else {
            this.get_treeBinder().set_isTargetLibrary(false);
        }

        this.get_treeBinder().DataBind(null, bindingContext);
    },

    _updateSelectedItemIds: function (items) {
        this._selectedItemIds = [];
        if (!items) return;
        for (var i = 0; i < items.length; i++) {
            var item = items[i];
            this._selectedItemIds.push(item.Id);
        }
    },

    _updateGridSelection: function () {
        //If grid is not bound yet, just schedule an update
        if (this._gridIsBound == false) {
            this._gridMustBeUpdated = true;
            return;
        }

        //Update grid
        this._gridMustBeUpdated = false;

        var selectedItemIds = this._selectedItemIds;
        var grid = this.get_itemsGrid();
        grid.getBinder().clearSelection();
        var dataItems = grid.getBinder().GetTableView().get_dataItems();
        var isCurrentSiteMultilingual = this._isCurrentSiteMultilingual();
        for (var i = 0; i < dataItems.length; i++) {
            var item = dataItems[i];
            var dataItem = item.get_dataItem();
            if (dataItem && selectedItemIds) {
                if (jQuery.inArray(dataItem.Id, selectedItemIds) > -1) {
                    item.set_selected(true);
                }
            }
            if (isCurrentSiteMultilingual) {
                if (jQuery.inArray(this.get_languageSelector()._dropDownList.value, item._dataItem.AvailableLanguages) === -1) {
                    item.set_visible(false);
                }
            }
        }

        this._raiseSelectionApplied({});
    },

    _isCurrentSiteMultilingual: function() {
        if (this.get_languageSelector() != null) {
            var dropDown = this.get_languageSelector()._dropDownList;
            var languagesCount = dropDown.options.length;
            return languagesCount > 1;
        }

        return false;
    },

    _updateTreeSelection: function () {
        //If grid is not bound yet, just schedule an update
        if (this._treeIsBound == false) {
            this._treeMustBeUpdated = true;
            return;
        }

        //        if (this._treeMustBeUpdated == true) {
        //Update grid
        this._treeMustBeUpdated = false;

        var tree = this.get_itemsTree();
        var binder = tree.getBinder();
        if (this._selectedItemId) {
            binder.setSelectedValues([this._selectedItemId], true, true);
        }
        else {
            var selectedItems = tree.get_selectedItems();
            if (selectedItems && selectedItems.length > 0) {
                binder.clearSelection();
                binder.setSelectedItems(selectedItems, true, true);
            } else if (this._selectedItemIds) {
                binder.setSelectedValues(this._selectedItemIds, true, true);
            }
        }

        this._raiseSelectionApplied(this, {});
        //        }
    },

    _markUnselectableItems: function (items) {
        var item, node, i, l;
        var tree = this.get_itemsTree().get_treeTable();
        var cssclass = this._unselectableClass;
        if (items === undefined) {
            var allNodes = tree.get_allNodes();
            for (i = 0, l = allNodes.length; i < l; i++) {
                node = allNodes[i]
                jQuery(node.get_element()).find(".rtIn").first().removeClass(cssclass);
            }
            this._markUnselectableItems(this._privateUnselectableItems);
            this._markUnselectableItems(this._unselectableItems);
            return;
        }
        for (i = 0, l = items.length; i < l; i++) {
            item = items[i];
            node = tree.findNodeByValue(item);
            if (node)
                jQuery(node.get_element()).find(".rtIn").first().addClass(cssclass);
        }
    },

    _checkUnselectedItem: function (item) {
        return jQuery.inArray(item, this._privateUnselectableItems) > -1 || jQuery.inArray(item, this._unselectableItems) > -1;
    },

    _setOriginalServiceUrl: function () {
        var orginalServiceBaseUrl = this._orginalServiceBaseUrl;
        if (!orginalServiceBaseUrl)
            orginalServiceBaseUrl = String.format(this.get_baseServiceUrl() + "children/{0}/", this._rootNodeId);
        this.get_treeBinder().set_orginalServiceBaseUrl(orginalServiceBaseUrl);
    },

    _setGridServiceBaseUrl: function () {
        var gridServiceBaseUrl = String.format(this.get_baseServiceUrl() + "?root={0}", this._rootNodeId);
        this.get_gridBinder().set_serviceBaseUrl(gridServiceBaseUrl);
    },

    /* -------------------- properties ---------------- */

    get_itemsGrid: function () {
        return this._itemsGrid;
    },
    set_itemsGrid: function (value) {
        this._itemsGrid = value;
    },

    get_gridBinder: function () {
        return this._itemsGrid.getBinder();
    },

    get_itemsTree: function () {
        return this._itemsTree;
    },
    set_itemsTree: function (value) {
        this._itemsTree = value;
    },

    get_treeBinder: function () {
        return this._itemsTree.getBinder();
    },

    get_searchBox: function () {
        return this._searchBox;
    },
    set_searchBox: function (value) {
        this._searchBox = value;
    },

    get_rootNodeId: function () {
        return this._rootNodeId;
    },
    set_rootNodeId: function (value) {
        if (value.indexOf("|lng") !== -1) {
            var selectedCulture = value.substring(value.indexOf("|lng"), value.length).replace("|lng%3A", ""),
                dropDown = this.get_languageSelector()._dropDownList;
            dropDown.value = selectedCulture;
            this.set_uiCulture(selectedCulture);
            value = value.substring(0, value.indexOf("|lng"));
        }
        this._rootNodeId = value;

        this._setOriginalServiceUrl();
        // set root node id (otherwise it will be taken from the context of the current site)
        this._setGridServiceBaseUrl();
    },

    get_baseServiceUrl: function () {
        return this._baseServiceUrl;
    },
    set_baseServiceUrl: function (value) {
        this._baseServiceUrl = value;
    },

    get_serviceChildItemsBaseUrl: function () {
        return this._serviceChildItemsBaseUrl;
    },
    set_serviceChildItemsBaseUrl: function (value) {
        this._serviceChildItemsBaseUrl = value;
    },

    get_servicePredecessorBaseUrl: function () {
        return this._servicePredecessorBaseUrl;
    },
    set_servicePredecessorBaseUrl: function (value) {
        this._servicePredecessorBaseUrl = value;
    },

    get_serviceTreeUrl: function () {
        return this._serviceTreeUrl;
    },
    set_serviceTreeUrl: function (value) {
        this._serviceTreeUrl = value;
    },

    get_orginalServiceBaseUrl: function () {
        return this._orginalServiceBaseUrl;
    },
    set_orginalServiceBaseUrl: function (value) {
        this._orginalServiceBaseUrl = value;
    },

    get_provider: function () {
        return this._provider;
    },
    set_provider: function (value) {
        this._provider = value;
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },
    set_bindOnLoad: function (value) {
        this._bindOnLoad = value;
    },

    get_targetLibraryId: function () {
        return this._targetLibraryId;
    },
    set_targetLibraryId: function (value) {
        this._targetLibraryId = value;
    },

    get_selectedItem: function () {
        var selectedItems = this.get_selectedItems();
        if (selectedItems) {
            return selectedItems[0];
        }

        return null;
    },

    get_selectedItems: function () {
        var selectedItems = null;
        if (this._isTreeMode) {
            selectedItems = this.get_itemsTree().get_selectedItems();
        }
        else {
            selectedItems = this.get_itemsGrid().get_selectedItems();
        }

        return selectedItems;
    },

    get_selectedItemIds: function () {
        if (!this._selectedItemIds) {
            this._selectedItemIds = [];
        }

        return this._selectedItemIds;
    },

    get_selectedItemObjects: function () {
        var result = [];
        var selectedItemIds = this.get_selectedItemIds();
        for (var i = 0; i < selectedItemIds.length; i++) {
            result.push({ Id: selectedItemIds[i] });
        }
        return result;
    },

    set_selectedItemId: function (value) {
        this._selectedItemId = value;
    },
    get_selectedItemId: function () {
        var selectedItem = this.get_selectedItem();
        if (selectedItem) {
            return selectedItem.Id;
        }
        return this._selectedItemId;
    },
    _setCultures: function (culture) {
        if (culture) {
            this._culture = culture;
            this._uiCulture = this._culture;
            if (this.get_itemsGrid() != null) {
                this.get_gridBinder().set_culture(this._culture);
                this.get_gridBinder().set_uiCulture(this._culture);
            }

            if (this.get_itemsTree() != null) {
                this.get_treeBinder().set_culture(this._culture);
                this.get_treeBinder().set_uiCulture(this._culture);
            }
            this.dataBind();
        }
    },
    // Specifies the ui culture to be used by the control
    set_uiCulture: function (culture) {
        if (this._uiCulture !== culture) {
            this._uiCulture = culture;

            if (this.get_itemsGrid() != null) {
                this.get_itemsGrid().getBinder().set_uiCulture(culture);
                this._configureGridMode();
            }

            if (this.get_itemsTree() != null) {
                this.get_itemsTree().getBinder().set_uiCulture(culture);
                this._configureTreeMode();
            }
        }
    },

    // Gets the ui culture used by the control
    get_uiCulture: function () {
        return this._uiCulture;
    },

    // Sets the culture culture to be used by the control
    set_culture: function (culture) {
        this._culture = culture;

        if (this.get_itemsGrid() != null) {
            this.get_itemsGrid().getBinder().set_culture(culture);
        }
        if (this.get_itemsTree() != null) {
            this.get_itemsTree().getBinder().set_culture(culture);
        }
    },
    // Gets the culture used by the control
    get_culture: function () {
        return this._culture;
    },

    get_markItemsWithoutTranslation: function () {
        return this._markItemsWithoutTranslation;
    },
    set_markItemsWithoutTranslation: function (value) {
        this._markItemsWithoutTranslation = value;
    },

    get_unselectableItems: function () {
        return this._unselectableItems;
    },
    set_unselectableItems: function (value) {
        this._unselectableItems = value;
        this._markUnselectableItems();
    },

    get_unselectableClass: function () {
        return this._unselectableClass;
    },
    set_unselectableClass: function (value) {
        this._unselectableClass = value;
    },

    get_siteSelector: function () {
        return this._siteSelector;
    },
    set_siteSelector: function (value) {
        this._siteSelector = value;
    },

    get_languageSelectorSelectedCulture: function () {
        return this._languageSelectorSelectedCulture;
    },

    set_languageSelectorSelectedCulture: function (value) {
        this._languageSelectorSelectedCulture = value;
    },

    get_languageSelector: function () {
        return this._languageSelector;
    },
    set_languageSelector: function (value) {
        this._languageSelector = value;
    },

    get_allSitesAvailableCultures: function () {
        return this._allSitesAvailableCultures;
    },

    set_allSitesAvailableCultures: function (value) {
        this._allSitesAvailableCultures = value;
    },

    get_rootNodeIdOnEdit: function () {
        return this._rootNodeIdOnEdit;
    },
    set_rootNodeIdOnEdit: function (value) {
        this._rootNodeIdOnEdit = value;
    }
};

Telerik.Sitefinity.Web.UI.GenericPageSelector.registerClass("Telerik.Sitefinity.Web.UI.GenericPageSelector", Sys.UI.Control);
