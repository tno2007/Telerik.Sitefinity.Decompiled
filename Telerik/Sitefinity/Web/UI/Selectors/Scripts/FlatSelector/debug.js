Type.registerNamespace("Telerik.Sitefinity.Web.UI");

/* FlatSelector class */
Telerik.Sitefinity.Web.UI.FlatSelector = function(element) {
    Telerik.Sitefinity.Web.UI.FlatSelector.initializeBase(this, [element]);
    this._gridId = null;
    this._grid = null;
    this._masterTable = null;
    this._allowPaging = null;
    this._pageSize = null;
    this._providersListId = null;
    this._providersList = null;
    this._providerSelectionChangedDelegate = null;
    this._selectorSearchBox = null;
    this._selectorSearchBoxId = null;
    this._originalClickedRowState = null;
    this._clickedRow = null;
    this._allowMultipleSelection = null;
    this._lnkLoadMore = null;
    this._loadMoreDelegate = null;
    this._dataSource = null;
}

Telerik.Sitefinity.Web.UI.FlatSelector.prototype = {

    /* ****************************** set up / tear down methods ****************************** */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "initialize");

        // Register events
        if (this._providerSelectionChangedDelegate === null) {
            this._providerSelectionChangedDelegate = Function.createDelegate(this, this._providerSelectionChangedHandler);
        }

        if (this._loadMoreDelegate === null) {
            this._loadMoreDelegate = Function.createDelegate(this, this.loadMore);

            if(this._lnkLoadMore) {
                $addHandler(this._lnkLoadMore, "click", this._loadMoreDelegate);
            }
        }
    },

    // tear down
    dispose: function () {
        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "dispose");
        // Clean up events
        if (this._providerSelectionChangedDelegate) {
            delete this._providerSelectionChangedDelegate;
        }

        if (this._lnkLoadMore && this._loadMoreDelegate) {
            $removeHandler(this._lnkLoadMore, "click", this._loadMoreDelegate);
        }

        if (this._loadMoreDelegate) {
            delete this._loadMoreDelegate;
        }
    },

    /* ****************************** events binding and unbinding ****************************** */
    add_providerSelectionChanged: function (delegate) {
        this.get_events().addHandler('onProviderSelectionChanged', delegate);
    },
    remove_providerSelectionChanged: function (delegate) {
        this.get_events().removeHandler('onProviderSelectionChanged', delegate);
    },

    /* ****************************** public methods ****************************** */

    // this function is called by the base class, one the page has loaded. It grabs the reference to the
    // binder and control being bound to
    setUp: function () {
        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "setUp");
        if (this._providersListId != null) {
            this._providersList = $get(this._providersListId);
        }

        if (this._gridId != null) {
            this._grid = $find(this._gridId);
        }

        // set up the paging for the grid
        // TODO: fix - this is null when in a radwindow without windowmanager
        // EHH?!
        this._masterTable = this._grid.get_masterTableView();

        this._rowSelectedDelegate = Function.createDelegate(this, this._rowSelected);
        this._rowDeselectedDelegate = Function.createDelegate(this, this._rowDeselected);
        this._rowDataBoundDelegate = Function.createDelegate(this, this._rowDataBound);
        
        this._gridDataBoundDelegate = Function.createDelegate(this, this._gridDataBound);

        $addHandler(this._providersList, "change", Function.createDelegate(this, this._providersListChanged));
        $addHandler(this._providersList, "change", Function.createDelegate(this, this._providerSelectionChangedHandler));

        this._grid.add_rowSelected(this._rowSelectedDelegate);
        this._grid.add_rowDeselected(this._rowDeselectedDelegate);
        this._grid.add_rowDataBound(this._rowDataBoundDelegate);
        this._grid.add_dataBound(this._gridDataBoundDelegate);

        if (this._allowMultipleSelection) {
            this._rowDeselectingDelegate = Function.createDelegate(this, this._rowDeselecting);
            this._rowSelectingDelegate = Function.createDelegate(this, this._rowSelecting);
            this._rowClickDelegate = Function.createDelegate(this, this._rowClick);

            this._grid.add_rowDeselecting(this._rowDeselectingDelegate);
            this._grid.add_rowSelecting(this._rowSelectingDelegate);
            this._grid.add_rowClick(this._rowClickDelegate);
        }
        // bind the selector through the base class
        if (this.get_bindOnLoad())
            this.bindSelector();

        this._isInitialized = true;
        this._initializedHandler();
    },

    dataBind: function () {
        if (this._dataSource === null) {
            Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "dataBind");
        } else {
            this._bindItemSelector(this._dataSource);
        }
    },

    loadMore: function (ev) {
        var binder = this.get_binder();
    
        var pageSize = this.get_pageSize();
        var total = binder.get_itemCount();
        var skip = binder.get_skip();
        var take = binder.get_take();
    
    
        if (skip + take < total) {
            skip += take;
    
            var serviceUrl = binder.get_serviceBaseUrl();
            var urlParams = binder.get_urlParams() || {};
    
            serviceUrl += '?';
            for (var k in urlParams) {
                serviceUrl += k + '=';
    
                if (k === 'skip') {
                    serviceUrl += skip;
                }
                else {
                    serviceUrl += urlParams[k];
                }
    
                serviceUrl += '&';                
            }
    
            // Cut last &
            serviceUrl = serviceUrl.substring(0, serviceUrl.length - 1);
    
            binder.set_take(take);
            binder.set_skip(skip);
            binder.set_urlParams(urlParams);
            var that = this;
            var tableView = binder.GetTableView();
    
            jQuery.ajax({
                type: "GET",
                url: serviceUrl,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    var tableDataSource = tableView.get_dataSource();
                    data.Items = that._dateParser(data.Items);
                    var combinedDataSource = tableDataSource.concat(data.Items);
                    that._bindItemSelector(combinedDataSource);
                    that._toggleLoadMoreLink();
                },
                error: function (e) {
                    console.log(e);
                }
            });
        }
    },
    _toggleLoadMoreLink: function () {
        if (this._lnkLoadMore) {
            var binder = this.get_binder();
            if (binder) {
                var tableView = binder.GetTableView();
                if (tableView) {
                    var dataSource = tableView.get_dataSource();
                    if (dataSource && (dataSource.length === 0 || dataSource.length < tableView.PageSize || dataSource.length >= binder.get_itemCount())) {
                        jQuery(this._lnkLoadMore).hide();
                    }
                    else {
                        jQuery(tableView.get_element()).after(jQuery(this._lnkLoadMore).show());
                    }
                }
            }
        }
    },
    _bindItemSelector: function (dataSource) {
        var binder = this.get_binder();
        var tableView = binder.GetTableView();
        tableView.set_dataSource(dataSource);
        tableView.dataBind();
        this._dataSource = dataSource;
    },
    _dateParser: function (listItems) {
        for (var i = 0; i < listItems.length; i++) {
            var item = listItems[i];
            for (prop in item) {
                if (item[prop] && typeof item[prop] === 'string' && item[prop].indexOf('/Date(') >= 0){
                    item[prop] = new Date(parseInt(item[prop].replace(')/', '').replace('/Date(', '')));
                }
            }
        }
    
        return listItems;
    },

    cleanUp: function () {
        this._cleanInternal();

        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "cleanUp");
    },

    _cleanInternal: function()  {
        // always call the base method at the end
        if (this._masterTable) {
            this._masterTable.clearSelectedItems();
        }
    },

    _cleanUpWithoutRebind: function() {
        this._cleanInternal();

        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "_cleanUpWithoutRebind");
    },

    setFirstRowActive: function () {
        if ((this._grid.MasterTableView.get_dataItems().length > 0) && (this.get_dataKeyNames().length > 0)) {
            var firstItem = this._grid.MasterTableView.get_dataItems()[0];
            var key = this.get_dataKeyNames()[0];

            this.selectItem(firstItem.get_dataItem()[key], firstItem.get_dataItem());
            firstItem.set_selected(true)
        }
    },

    set_clearSelectionOnRebind: function (value) {
        this._binder.set_clearSelectionOnRebind(value);
    },

    selectItemsInternal: function (keys) {
        var gridItems = this._masterTable.get_dataItems();
        var gridItemsCount = gridItems.length;
        var dataKeyNames = this.get_dataKeyNames();
        for (var i = 0; i < gridItemsCount; i++) {
            var gridItem = gridItems[i];
            var dataItem = gridItem.get_dataItem();
            if (dataItem == null) {
                continue;
            }
            var keysCount = keys.length;
            for (var x = 0; x < keysCount; x++) {
                if (keys[x] == dataItem[dataKeyNames]) {
                    gridItem.set_selected(true);
                    break;
                }
            }
        }
    },

    set_template: function (template) {
        var clientTemplates = this.get_binder().get_clientTemplates();
        if (clientTemplates && clientTemplates.length > 0) {
            var clientTemplateSelector = "#" + clientTemplates[0];
            $(clientTemplateSelector).html(template);
            (new Sys.UI.Template(Sys.get(clientTemplateSelector))).recompile();
        }
    },

    _getSearchBox: function () {
        if ((this._selectorSearchBox == null) && (this._selectorSearchBoxId != null)) {
            this._selectorSearchBox = $find(this._selectorSearchBoxId);
        }
        return this._selectorSearchBox;
    },

    _providersListChanged: function (e) {
        if (this._isInitialized) {
            this._providerName = (typeof (e) != "undefined" ? e.target : event.srcElement).value;
            this.bindSelector();
        }
    },

    // method that handles the event of a row in the grid before the selected event is fired
    _rowSelecting: function (sender, args) {
        if (this._clickedRow == args.get_gridDataItem() && this._originalClickedRowState) {
            args.set_cancel(true);
            this._originalClickedRowStateu = null;
            this._clickedRow = null;
        }
    },

    // method that handles the event of a row in the grid being selected
    _rowSelected: function (sender, args) {
        if (this._allowMultipleSelection) {
            this._originalClickedRowState = null;
            this._clickedRow = null;
        }
        var dataKeyNames = this.get_dataKeyNames();
        if (dataKeyNames.length > 1) {
            alert('composite data keys not yet supported.');
        }
        var dataKeyName = dataKeyNames[0];
        var dataItem = args.get_gridDataItem().get_dataItem();
        var dataKeyValue = dataItem[dataKeyName];
        this.selectItem(dataKeyValue, dataItem);
    },

    // method that handles the event of a row in the grid before the deselected event is fired
    _rowDeselecting: function (sender, args) {
        if (this._clickedRow != null && this._clickedRow != args.get_gridDataItem()) {
            args.set_cancel(true);
        }
    },

    // method that handles the event of a row in the grid being deselected
    _rowDeselected: function (sender, args) {
        var dataKeyNames = this.get_dataKeyNames();
        if (dataKeyNames.length > 1) {
            alert('composite data keys not yet supported.');
        }
        var dataKeyName = dataKeyNames[0];
        var dataItem = args.get_gridDataItem().get_dataItem();
        var dataKeyValue = dataItem[dataKeyName];
        this.deselectItem(dataKeyValue, dataItem);
    },

    // method that handles the row click event of the grid
    _rowClick: function (sender, args) {
        this._clickedRow = args.get_gridDataItem();
        this._originalClickedRowState = args.get_gridDataItem().get_selected();
    },

    _rowDataBound: function (sender, args) {
        var gridItem = args.get_item();
        var dataItem = gridItem.get_dataItem();
        var key = dataItem[this._dataKeyNames];
        var selectedKeys = this.getSelectedKeys();
        if (selectedKeys.indexOf(key) > -1) {
            gridItem.set_selected(true);
        } else {
            gridItem.set_selected(false);
        }
    },

    _gridDataBound: function (sender, args) {
        this._selectorReadyHandler();
        this._toggleLoadMoreLink();
    },

    /* EVENT HANDLERS  */
    _providerSelectionChangedHandler: function () {
        var h = this.get_events().getHandler('onProviderSelectionChanged');
        if (h) h(this, Sys.EventArgs.Empty);
    },

    /* ****************************** properties ****************************** */
    get_allowPaging: function () {
        return this._allowPaging;
    },
    set_allowPaging: function (value) {
        if (this._allowPaging != value) {
            this._allowPaging = value;
            this.raisePropertyChanged('allowPaging');
        }
    },
    get_pageSize: function () {
        return this._pageSize;
    },
    set_pageSize: function (value) {
        if (this._pageSize != value) {
            this._pageSize = value;
            this.raisePropertyChanged('pageSize');
        }
    },
    get_lnkLoadMore: function () {
        return this._lnkLoadMore;
    },
    
    set_lnkLoadMore: function (value) {
        if (this._lnkLoadMore != value) {
            this._lnkLoadMore = value;
            this.raisePropertyChanged('lnkLoadMore');
        }
    },

    set_itemsFilter: function (value) {
        if (this._itemsFilter != value) {
            this._itemsFilter = value;
            this.raisePropertyChanged('_itemsFilter');
            if (this._getSearchBox() != null)
                this._getSearchBox().set_additionalFilterExpression(value);
        }
        Telerik.Sitefinity.Web.UI.FlatSelector.callBaseMethod(this, "set_itemsFilter", [value]);
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;
    }
};

Telerik.Sitefinity.Web.UI.FlatSelector.registerClass('Telerik.Sitefinity.Web.UI.FlatSelector', Telerik.Sitefinity.Web.UI.ItemSelector);