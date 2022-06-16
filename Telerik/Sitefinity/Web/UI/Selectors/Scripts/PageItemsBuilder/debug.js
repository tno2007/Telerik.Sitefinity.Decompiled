﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

Telerik.Sitefinity.Web.UI.PageItemsBuilder = function (element) {
    Telerik.Sitefinity.Web.UI.PageItemsBuilder.initializeBase(this, [element]);
    this._itemsContainer = null;
    this._choiceItems = [];
    this._template = null;
    this._errorMessageLabel = null;
    this._minItemsCount = 0;
    this._enableReordering = null;

    this._choiceItemsDataView = null;

    // delegates
    this._dataViewOnCommandDelegate = null;
    this._sortStopDelegatge = null;
    this._sortStartDelegate = null;
    this._loadDelegate = null;
}

Telerik.Sitefinity.Web.UI.PageItemsBuilder.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PageItemsBuilder.callBaseMethod(this, 'initialize');
        this._dataViewOnCommandDelegate = Function.createDelegate(this, this._dataViewCommandHandler);

        if (this.get_enableReordering()) {
            this._sortStopDelegatge = Function.createDelegate(this, this._sortStopHandler);
            this._sortStartDelegate = Function.createDelegate(this, this._sortStartHandler);
            jQuery(this._itemsContainer).sortable(
                                                { update: this._sortStopDelegatge,
                                                    start: this._sortStartDelegate,
                                                    forcePlaceholderSize: true
                                                    //handle: '.sfDragAndDropTreeTableColumn'
                                                }
                                            );
        }

        this._loadDelegate = Function.createDelegate(this, this._loadHandler);
        Sys.Application.add_load(this._loadDelegate);

    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.PageItemsBuilder.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods ---------------------------------- */

    // Checks if all the items has correct values and shows an error message if needed
    validate: function () {
        var result = true;
        for (var i = 0, length = this._choiceItems.length; i < length; i++) {
            if (!this._choiceItems[i].Title || this._choiceItems[i].Title.trim() == "") {
                result = false;
                break;
            } else if (!this._choiceItems[i].Url || this._choiceItems[i].Url.trim() == "") {
                result = false;
                break;
            }
        }

        jQuery(this._errorMessageLabel).toggle(!result);
        return result;
    },

    updateItem: function (index, newItem) {
        this._choiceItems.beginUpdate();
        this._choiceItems[index] = newItem;
        this._choiceItems.endUpdate();
        this._choiceItemsDataView.refresh();
        dialogBase.resizeToContent();
        this._raiseItemsChanged("update", newItem, this._choiceItems.length);
    },

    getSelectedPages: function () {
        return this.get_choiceItems();
    },

    addNewPageDefault: function (index) {
        var page = { Title: "New Link", Url: "", Id: null };
        this.addNewPage(index, page);
    },
    addNewPageDefaultLast: function () {
        var index = this.get_choiceItems().length;
        this.addNewPageDefault(index);
    },
    addNewPageLast: function (page) {
        var index = this.get_choiceItems().length;
        this.addNewPage(index, page);
    },
    addNewPage: function (index, page) {
        this._choiceItems.beginUpdate();
        this._choiceItems.insert(index, page);
        this._choiceItems.endUpdate();
        dialogBase.resizeToContent();
        this._raiseItemsChanged("add", page, this._choiceItems.length);
    },

    /* -------------------- events -------------------- */

    // Happens when an item is clicked
    add_itemClicked: function (delegate) {
        this.get_events().addHandler("itemClicked", delegate);
    },

    // Happens when an item is clicked
    remove_itemClicked: function (delegate) {
        this.get_events().removeHandler("itemClicked", delegate);
    },

    _raiseItemClicked: function (itemIndex, item) {
        var eventArgs = { Index: itemIndex, Item: item };
        var handler = this.get_events().getHandler("itemClicked");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // Happens when an item is reordered
    add_itemReordered: function (delegate) {
        this.get_events().addHandler("itemReordered", delegate);
    },

    // Happens when an item is reordered
    remove_itemReordered: function (delegate) {
        this.get_events().removeHandler("itemReordered", delegate);
    },

    _raiseItemReordered: function (itemNewIndex, itemOldIndex, item) {
        var eventArgs = { Index: itemNewIndex, OldIndex: itemOldIndex, Item: item };
        var handler = this.get_events().getHandler("itemReordered");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    // Happens when an item is added, removed or updated
    add_itemsChanged: function (delegate) {
        this.get_events().addHandler("itemsChanged", delegate);
    },

    // Happens when an item is added, removed or updated
    remove_itemsChanged: function (delegate) {
        this.get_events().removeHandler("itemsChanged", delegate);
    },

    //Action - a constant identifying the action that caused the event to raise
    //item - the item associated with the event (can be null)
    //itemsCount - the count of all items
    _raiseItemsChanged: function (action, item, itemsCount) {
        var eventArgs = { Action: action, Item: item, ItemsCount: itemsCount };
        var handler = this.get_events().getHandler("itemsChanged");
        if (handler) handler(this, eventArgs);
        return eventArgs;
    },

    /* --------------------------------- event handlers ---------------------------------- */
    _dataViewCommandHandler: function (sender, args) {
        var commandName = args.get_commandName();
        var index = args.get_commandArgument();
        switch (commandName) {
            case "Add":
                this.addNewPageDefault(index + 1);
                break;
            case "Remove":
                if (this._choiceItems.length > this._minItemsCount) {
                    var item = this._choiceItemsDataView.findContext(args.get_commandSource());
                    this._choiceItems.beginUpdate();
                    this._choiceItems.remove(item.dataItem);
                    this._choiceItems.endUpdate();
                    this._raiseItemsChanged("remove", item, this._choiceItems.length);
                    this._updateSortingFunctionality();
                }
                dialogBase.resizeToContent();
                break;
            case "DefaultChange":
                this._choiceItems.beginUpdate();
                var item = this._choiceItemsDataView.findContext(args.get_commandSource());
                var count = this._choiceItems.length;
                while (count--) {
                    this._choiceItems[count].Selected = false;
                }
                item.dataItem.Selected = true;
                this._choiceItems.endUpdate();
                break;
            case "Edit":
                this._raiseItemClicked(index, this._choiceItems[index]);
                break;
        }
    },

    _updateSortingFunctionality: function () {
        jQuery(this._itemsContainer).sortable({ disabled: this._choiceItems.length <= 1 });
    },

    _loadHandler: function (e) {
        this._choiceItemsDataView = $create(Sys.UI.DataView, {}, {}, {}, this._itemsContainer);
        this._choiceItemsDataView.add_command(this._dataViewOnCommandDelegate);
        this._choiceItemsDataView.set_data(this._choiceItems);
    },


    _sortStopHandler: function (event, ui) {
        var el = ui.item;
        var newIndex = jQuery(this._itemsContainer).children().index(el);
        var oldIndex = jQuery(el).data("startIndex");

        var dataItem = this._choiceItemsDataView.findContext(el[0]).dataItem;

        this._choiceItems.beginUpdate();
        this._choiceItems.remove(dataItem);
        this._choiceItems.insert(newIndex, dataItem);
        this._choiceItems.endUpdate();

        this._raiseItemReordered(newIndex, dataItem, oldIndex);
    },

    _sortStartHandler: function (event, ui) {
        var el = ui.item;
        var startIndex = jQuery(this._itemsContainer).children().index(el);
        jQuery(el).data("startIndex", startIndex);
    },

    /* --------------------------------- private methods --------------------------------- */

    _findElement: function (id) {
        if (typeof (this._elementsCache[id]) !== 'undefined')
            return this._elementsCache[id];
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        this._elementsCache[id] = result;
        return result;
    },

    /* --------------------------------- properties -------------------------------------- */
    // Returns the current choice items
    get_choiceItems: function () {
        return this._choiceItems;
    },
    // Sets and binds the choice items
    set_choiceItems: function (value) {
        this._choiceItems = value;
        Sys.Observer.makeObservable(this._choiceItems);
        if (this._choiceItemsDataView) {
            this._choiceItemsDataView.set_data(this._choiceItems);
        }
        this._updateSortingFunctionality();
    },

    get_minItemsCount: function () { return this._minItemsCount; },
    set_minItemsCount: function (value) { this._minItemsCount = value; },
    get_enableReordering: function () { return this._enableReordering; },
    set_enableReordering: function (value) { this._enableReordering = value; },

    get_itemsContainer: function () { return this._itemsContainer; },
    set_itemsContainer: function (value) { this._itemsContainer = value; },

    get_errorMessageLabel: function () { return this._errorMessageLabel; },
    set_errorMessageLabel: function (value) { this._errorMessageLabel = value; }
}

Telerik.Sitefinity.Web.UI.PageItemsBuilder.registerClass('Telerik.Sitefinity.Web.UI.PageItemsBuilder', Sys.UI.Control);