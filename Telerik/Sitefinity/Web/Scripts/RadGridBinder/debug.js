/* RadGridBinder class */
Telerik.Sitefinity.Web.UI.RadGridBinder = function () {
    this._itemsBoundCount = null;
    this._context;
    this._virtualCount;
    this._batchSave;
    this._clearSelectionOnRebind = true;
    this._restoreSelectionAfterRebind = false;
    this._target = null;
    this._loadingText = null;
    this._postData = null;
    this._selectedItemsArray = {};
    this._dataSerializerType;
    //private
    var m_curPageIndex = -1;
    Telerik.Sitefinity.Web.UI.RadGridBinder.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.RadGridBinder.prototype = {
    // set up and tear down
    initialize: function () {
        var grid = this.get_target();
        this._binderItemDataBoundDelegate = Function.createDelegate(this, this.BinderItemDataBound);
        grid.add_rowDataBound(this._binderItemDataBoundDelegate);

        this._binderGridBoundDelegate = Function.createDelegate(this, this._handleGridDataBound);
        this._handleRowSelectedChangedDelegate = Function.createDelegate(this, this._handleRowSelectedChanged);

        grid.add_dataBound(this._binderGridBoundDelegate);
        grid.add_rowSelected(this._handleRowSelectedChangedDelegate);
        grid.add_rowDeselected(this._handleRowSelectedChangedDelegate);

        this._serviceCallback = this.DataBind;
        m_curPageIndex = -1;

        var loadingTemplate = '<div class="RadAjax"><div class="raDiv">{0}</div><div class="raColor"></div></div>';
        loadingTemplate = String.format(loadingTemplate, this._loadingText);
        $('#' + this.get_targetId()).after(loadingTemplate);
        $('.RadAjax').hide();

        Telerik.Sitefinity.Web.UI.RadGridBinder.callBaseMethod(this, "initialize");
    },
    dispose: function () {
        this._binderItemDataBoundDelegate = null;
        this._binderGridBoundDelegate = null;

        Telerik.Sitefinity.Web.UI.RadGridBinder.callBaseMethod(this, "dispose");
    },

    /* -------------------- event handlers -------------------- */
    _handleRowSelectedChanged: function (grid, args) {
        var dataItem = args.get_gridDataItem().get_dataItem();
        var itemElement = args.get_domEvent();
        var itemIndex = args.get_itemIndexHierarchical();
        var key = this.GetItemKey(itemIndex);

        this._itemSelectCommandHandler(key, dataItem, itemIndex, itemElement);
    },

    _handleGridDataBound: function (sender, args) {
        var tableView = this.GetTableView();
        var gridPageIndex = tableView.get_currentPageIndex();

        //page index has changed
        if (m_curPageIndex != gridPageIndex) {
            m_curPageIndex = gridPageIndex;
            //scroll to the top of the page
            window.scrollTo(0, 0);
        }
        $('.RadAjax').hide();
        var jTarget = $('#' + this._targetId);
        jTarget.show();

        // add css class to all the hidden rows
        var hiddenRowClass = 'sfHiddenGridRow';

        // clean up
        jTarget.find('.rgRow').each(function () {
            $(this).removeClass(hiddenRowClass);
        });

        // set hidden classes
        jTarget.find('.rgRow').each(function () {
            if (this.style.display == 'none') {
                $(this).addClass(hiddenRowClass);
            }
        });

        //do not display paging if there is one page only
        if (gridPageIndex == 0 && tableView.PageCount == 1)
            jTarget.find('.rgPagerCell').hide();

        else
            jTarget.find('.rgPagerCell').show();
    },

    // Overridden method from the ClientBinder. Resets paging.
    ClearPager: function () {
        var tableView = this.GetTableView();
        tableView.set_currentPageIndex(0);
    },

    // Overridden method from the ClientBinder. This method binds data to the target control.
    DataBind: function () {
        // hide the grid here and show once it's bound
        $('#' + this._targetId).hide();
        $('.RadAjax').show();

        this.MapEvents();
        var tableView = this.GetTableView();
        var currentPageIndex = tableView.get_currentPageIndex();
        var pageSize = tableView.get_pageSize();
        this._skip = (currentPageIndex * pageSize);

        if (this._skip == null) {
            this._skip = 0;
        }
        this._take = pageSize;
        if (tableView.get_sortExpressions().toString().length > 0) {
            this._sortExpression = tableView.get_sortExpressions().toString();
            var se = jQuery.trim(this._sortExpression);
            if (se == 'ASC' || se == 'DESC') {
                this._sortExpression = '';
            }
        }
        var clientManager = this.get_manager();
        clientManager.set_urlParams(this.get_urlParams());
        clientManager.set_dataSerializerType(this.get_dataSerializerType());
        clientManager.GetItemCollection(this, null, this.get_dataKeys(), null, null, this.get_postData());
    },
    SaveChanges: function () {
        if ($(document.forms[0]).validate().form() == false) {
            return false;
        }
        var grid = this.get_target();
        var masterTableview = grid.get_masterTableView();
        var dataItems = masterTableview.get_dataItems();
        var clientManager = this.get_manager();
        clientManager.set_urlParams(this.get_urlParams());

        var properties = [];
        var batchKeys;

        // for each row
        for (dataItemIndex = 0, dataItemsLen = masterTableview.get_virtualItemCount(); dataItemIndex < dataItemsLen; dataItemIndex++) {
            var dataItem = dataItems[dataItemIndex]._dataItem;
            if (dataItem != null) {
                if ($(document.forms[0]).validate().form() == false) {
                    return false;
                }
                var key = this.GetItemKey(dataItemIndex)
                // create a property bag
                var propertyBag = this.GetData(dataItems[dataItemIndex]._element, dataItemIndex);
                // fire saving event with populated property bag as an argument
                this._savingHandler(propertyBag);

                var args = this._itemSavingHandler(key, dataItem, dataItemIndex, dataItems[dataItemIndex].get_element(), propertyBag);
                // call client manager's save item
                if (args.get_cancel() == false) {
                    if (this._batchSave) {
                        if (dataItemIndex == 0) {
                            batchKeys = key;
                        }

                        var bag = args.get_propertyBag();
                        for (i = 0, len = bag.length; i < len; i++) {
                            properties.push(bag[i]);
                        }
                    }
                    else {
                        clientManager.SaveItem(this, key, args.get_propertyBag(), null);
                    }
                }
            }
        }

        if (this._batchSave) {
            clientManager.SaveItems(this, null, properties, null);
        }

        return true;
    },
    SaveItem: function (dataItemIndex) {
        if ($(document.forms[0]).validate().form() == false) {
            return false;
        }
        var grid = this.get_target();
        var masterTableview = grid.get_masterTableView();
        var dataItems = masterTableview.get_dataItems();
        var clientManager = this.get_manager();
        clientManager.set_urlParams(this.get_urlParams());

        var dataItem = dataItems[dataItemIndex]._dataItem;
        if (dataItem != null) {
            if ($(document.forms[0]).validate().form() == false) {
                return false;
            }
            var key = this.GetItemKey(dataItemIndex);
            // create a property bag
            var propertyBag = this.GetData(dataItems[dataItemIndex]._element, dataItemIndex);
            // fire saving event with populated property bag as an argument
            this._savingHandler(propertyBag);
            var args = this._itemSavingHandler(key, dataItem, dataItemIndex, dataItems[dataItemIndex].get_element(), propertyBag);
            // call client manager's save item
            if (args.get_cancel() == false) {
                clientManager.SaveItem(this, key, args.get_propertyBag(), null);
            }
        }
        return true;
    },

    // This method binds the form, with the specified sort expression. It is a shorthand to setting the 
    // sortExpression property and calling the bind.
    Sort: function (sortExpression) {
        this._sortExpression = sortExpression;
        this.DataBind();
    },

    // Clears the grid
    Clear: function () {
        var empty = { Context: null, IsGeneric: false, Items: [], TotalCount: 0 };
        this.BindCollection(empty);
    },

    // This method binds the form, with the specified filter expression. It is a shorthand to setting the 
    // filterExpression property and calling the bind.
    Filter: function (filterExpression) {
        this._filterExpression = filterExpression;
        this.DataBind();
    },
    // Overridden method from the ClientBinder. The ClientManager will call this method after it receives data
    // (collection of items) from the service and pass the CollectionContext (defined on the server) to it.
    BindCollection: function (data) {
        this._dataKeys = null;

        data = this.DeserializeData(data);
        this._dataBindingHandler(data);

        this._itemsBoundCount = 0;
        this._displayedItemsCount = data.Items.length;

        var tableView = this.GetTableView();

        tableView.set_virtualItemCount(data.TotalCount); //this results in a databind, see the TargetCommand method
        tableView.set_dataSource(data.Items);

        var selectedItemsArray = this.get_selectedItemsArray();
        if (this.get_restoreSelectionAfterRebind() == true) {
            selectedItemsArray = [];
            var selectedItems = this.GetTableView().get_selectedItems();
            for (var i = 0; i < selectedItems.length; i++) {
                var key = selectedItems[i].get_dataItem();
                selectedItemsArray.push(key);
            }
        }

        if (this._clearSelectionOnRebind) {
            this.clearSelection();
        }

        // process context

        if (data.Context) {
            var context = [];

            for (index = 0; index < data.Context.length; index++) {
                var item = data.Context[index];
                context[item.Key] = item.Value;
            }

            this._context = context;
        }

        this._virtualCount = data.TotalCount;

        // fix for the client binding of RadGrid, when no paging ought to be displayed
        if (data.TotalCount <= tableView.get_pageSize()) {
            $(this.get_target()).find('.rgPager').each(function () {
                $(this).hide();
            });
        }
        else {
            $(this.get_target()).find('.rgPager').each(function () {
                $(this).show();
            });
        }

        tableView.dataBind();

        if (this.get_restoreSelectionAfterRebind() == true) {
            if (this._clearSelectionOnRebind == false) this.clearSelection();

            var selectedItemsLength = selectedItemsArray.length;
            if (selectedItemsLength > 0) {
                var allItems = tableView.get_dataItems();
                var grid = this.get_target();
                grid.remove_rowDeselected(this._handleRowSelectedChangedDelegate);
                while (selectedItemsLength--) {
                    var key = selectedItemsArray[selectedItemsLength];
                    for (var i = 0; i < allItems.length; i++) {
                        var row = allItems[i];
                        var dataItem = row.get_dataItem();
                        if (dataItem != null) {
                            //Not very optimal - maybe implement some logic using this.GetItemKey
                            //var ckey = this.GetItemKey(dataItem);
                            //var sameKey = this.ObjectsAreSame(key, dataItem);
                            var sameKey = (dataItem.Id == key.Id);
                            if (sameKey == true) {
                                row.set_selected(true);
                                break;
                            }
                        }
                    }
                }
                grid.add_rowDeselected(this._handleRowSelectedChangedDelegate);
            }
        }

        this.set_selectedItemsArray(selectedItemsArray);

        this._dataBoundHandler(data);
        $('.RadAjax').hide();
        var jTarget = $('#' + this._targetId);
        jTarget.show();

    },

    // Overridden method from the ClientBinder. The ClientManager will call this method afer it receives data
    // (single item) from the service and pass the deserialized item to it.
    BindItem: function (data) {
        alert('BindItem function is not supported by RadGridBinder. Please use Bind instead.');
    },
    // This function is called when a grid binds an item.
    BinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();

        this._itemDataBindingHandler(dataItem);

        var item = args.get_item();
        var columns = item.get_owner().get_columns();
        var cells = args.get_item().get_element().cells;

        var templateIndex = 0;
        for (columnIndex = 0, columnsLen = columns.length; columnIndex < columnsLen; columnIndex++) {
            var column = columns[columnIndex];
            if (column.get_visible() == false) {
                continue;
            }

            var columnName = column.get_uniqueName();
            var binderNameConstant = 'BinderContainer';
            if (columnName.indexOf(binderNameConstant) != 0) {
                continue;
            }

            var binderIndex = columnName.substring(binderNameConstant.length);
            cells[columnIndex].innerHTML = '';
            var template = new Sys.UI.Template($get(this._clientTemplates[binderIndex]));

            this.LoadItem(cells[columnIndex], template, dataItem, false, item._itemIndexHierarchical);

            // ensure unique IDs
            $(cells[columnIndex]).find('[id]').each(function (i, element) {
                // new ajax framework always adds index to id parameters, so remove it
                var attr = $(element).attr('id');
                var attrUnique = attr.substring(0, attr.length - 1) + item._itemIndexHierarchical;
                $(element).attr('id', attrUnique);
            });

            // ensure unique IDs
            $(cells[columnIndex]).find('[for]').each(function (i, element) {
                var attrUnique = $(element).attr('for') + item._itemIndexHierarchical;
                $(element).attr('for', attrUnique);
            });

            // ensure unique IDs
            $(cells[columnIndex]).find('[menu]').each(function (i, element) {
                var attrUnique = $(element).attr('menu') + item._itemIndexHierarchical;
                $(element).attr('menu', attrUnique);
            });
        }

        this._itemsBoundCount++;

        var binder = this;
        var itemElement = item.get_element();
        this.LoadDataItemKey(dataItem);
        var key = this.GetItemKey(item._itemIndexHierarchical);
        this._itemDataBoundHandler(key, dataItem, item._itemIndexHierarchical, itemElement);
        this.BuildInputWrappers(itemElement);
        this.AssignCommands(itemElement, dataItem, key, item._itemIndexHierarchical);
        this.ApplyValidationRules(itemElement, item._itemIndexHierarchical);
        this.EnableActionMenus(itemElement);
    },
    // This function is called when a command has been invoked by the RadGrid.
    TargetCommand: function (sender, args) {
        var commandName = args.get_commandName().toLowerCase();
        var commandArgument = args.get_commandArgument();
        this._targetCommandHandler(commandName, commandArgument);

        var key;
        if (commandName == 'delete') {
            args.set_cancel(true);
            key = this.GetItemKey(commandArgument);
            var binderArgs = this._deletingHandler(key);
            if (binderArgs.get_cancel() == false) {
                this.DeleteItem(key);
                this._deletedHandler(key);
            }
        }
        else if (commandName == 'sort') {
            args.set_cancel(true);
            var sortExpression = args.get_commandArgument();
            if (sortExpression == null) {
                return;
            }
            if (sortExpression.length == 0) {
                return;
            }
        }
        else if (commandName == 'page') {
            args.set_cancel(true);
        }
        else if (commandName == 'pagesize') {
            args.set_cancel(true);
        }
        else if (commandName == 'edit') {
            args.set_cancel(true);
            key = this.GetItemKey(commandArgument);
            this._editHandler(key);
            return;
        }

        this.DataBind();

    },
    // This function gets a reference to the RadGrid client object.
    GetTableView: function () {
        var grid = this.get_target();
        var tableView = grid.get_masterTableView();
        return tableView;
    },

    MapEvents: function () {
        //var grid = this.get_target();
        //grid.add_dataBound(this.GridBound);
    },

    //selecting multiple rows by the dataItem key.
    selectByIds: function (ids, doNotClearPreviousSelection) {

        var masterTableView = this.GetTableView();
        if (!doNotClearPreviousSelection) {
            masterTableView.clearSelectedItems();
        }
        var dataKeysDictionary = null;
        var dataItems = masterTableView.get_dataItems();

        //iterating the ids to select
        var idsToSelectLength = ids.length;
        for (var idsIterator = 0; idsIterator < idsToSelectLength; idsIterator++) {
            var idToSelect = ids[idsIterator];
            //Since this operation is actually finding intersection of two arrays we can optimise by making the longer array
            // (presumably the one with bound rows) as a dictionary record.
            if (!dataKeysDictionary) {
                var dataKeysDictionary = {};
                for (var dataKeysIterator = 0, dataItemsLength = dataItems.length; dataKeysIterator < dataItemsLength; dataKeysIterator++) {
                    //TODO add support of complex dataKeys
                    var gridDataItem = dataItems[dataKeysIterator];
                    if (!gridDataItem || !gridDataItem.get_dataItem || !gridDataItem.get_dataItem()) {
                        break;
                    }
                    var dataItemId = gridDataItem.get_dataItem()[this.get_dataKeyNames()[0]];
                    //If there are more than one items to select
                    if (idsToSelectLength > 0) {
                        //store the index of the dataItem with its datakey as a key to speed up the next searches.

                        dataKeysDictionary[dataItemId] = dataKeysIterator;
                    }
                    if (dataItemId === idToSelect) {
                        var rowToSelect = dataItems[dataKeysIterator].get_element();
                        this._selectRow(rowToSelect);
                    }
                }
            }
            //use the dictionary with dataItemIds to use single operation search in the data items instead of iterating them.
            else {
                if (idToSelect in dataKeysDictionary) {
                    var rowToSelect = dataItems[dataKeysDictionary[idToSelect]].get_element();
                    this._selectRow(rowToSelect);
                }
            }
        }

        if (dataKeysDictionary) {
            delete dataKeysDictionary;
        }
    },

    _selectRow: function (rowToSelect) {
        var masterTableView = this.GetTableView();
        masterTableView.selectItem(rowToSelect);
    },

    // Clears all selected items, raising the selectionChangedEvent only once.
    clearSelection: function () {
        var selectedItems = this.GetTableView().get_selectedItems();
        var selectedItemsLength = selectedItems.length;
        if (selectedItemsLength > 0) {
            var grid = this.get_target();
            grid.remove_rowDeselected(this._handleRowSelectedChangedDelegate);
            while (selectedItemsLength--) {
                if (selectedItemsLength == 0) {
                    grid.add_rowDeselected(this._handleRowSelectedChangedDelegate);
                }
                selectedItems[selectedItemsLength].set_selected(false);
            }
        }
    },

    get_target: function () {
        if (!this._target)
            this._target = $find(this._targetId);
        return this._target;
    },

    // Additional context for holding data for rendering header/ footer.    
    get_context: function () {
        return this._context;
    },

    // Virutal item count of the grid.
    get_itemCount: function () {
        return this._virtualCount;
    },

    set_batchSave: function (value) {
        this._batchSave = value;
    },

    get_selectedItemsCount: function () {
        return this.GetTableView().get_selectedItems().length;
    },

    get_selectedItems: function () {
        var gridSelectedItems = this.GetTableView().get_selectedItems();
        var dataItems = new Array();
        for (var selectedGridItemIndex = 0, selectedGridItemsLen = gridSelectedItems.length; selectedGridItemIndex < selectedGridItemsLen; selectedGridItemIndex++) {
            dataItems.push(gridSelectedItems[selectedGridItemIndex].get_dataItem());
        }
        return dataItems;
    },
    get_restoreSelectionAfterRebind: function () {
        return this._restoreSelectionAfterRebind;
    },
    set_restoreSelectionAfterRebind: function (val) {
        if (val != this._restoreSelectionAfterRebind) {
            this._restoreSelectionAfterRebind = val;
            this.raisePropertyChanged("restoreSelectionAfterRebind");
        }
    },
    get_postData: function () {
        return this._postData;
    },
    set_postData: function (data) {
        this._postData = data;
    },
    get_selectedItemsArray: function () {
        return this._selectedItemsArray;
    },
    set_selectedItemsArray: function (array) {
        this._selectedItemsArray = array;
    },
    get_dataSerializerType: function () {
        return this._dataSerializerType;
    },
    set_dataSerializerType: function (serializerType) {
        this._dataSerializerType = serializerType;
    },
    get_clearSelectionOnRebind: function () {
        return this._clearSelectionOnRebind;
    },
    set_clearSelectionOnRebind: function (val) {
        if (val != this._clearSelectionOnRebind) {
            this._clearSelectionOnRebind = val;
            this.raisePropertyChanged("clearSelectionOnRebind");
        }
    }

}

Telerik.Sitefinity.Web.UI.RadGridBinder.registerClass('Telerik.Sitefinity.Web.UI.RadGridBinder', Telerik.Sitefinity.Web.UI.ClientBinder);
