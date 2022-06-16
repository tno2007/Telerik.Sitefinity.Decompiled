﻿Telerik.Sitefinity.Web.UI.RadComboBinder = function() {
    Telerik.Sitefinity.Web.UI.RadComboBinder.initializeBase(this);
    this._template = null;
    this._comboBox = null;
    this._staticItemText = null;
    this._staticItemValue = null;
    this._levelField = null;
}

Telerik.Sitefinity.Web.UI.RadComboBinder.prototype = {
    // set up and tear down
    initialize: function () {
        Telerik.Sitefinity.Web.UI.RadComboBinder.callBaseMethod(this, "initialize");
        this._template = new Sys.UI.Template($get(this._clientTemplates[0]));
        this._selectedIndexChangingDelegate = Function.createDelegate(this, this._selectedIndexChanging);
    },
    dispose: function () {
        this._template = null;
        this._staticItemText = null;
        this._staticItemValue = null;
        this._levelField = null;

        if (this._comboBox != null) {
            this._comboBox.remove_selectedIndexChanging(this._selectedIndexChangingDelegate);
            delete this._selectedIndexChangingDelegate;
        }
        this._selectedIndexChangingDelegate = null;
        this._comboBox = null;
        Telerik.Sitefinity.Web.UI.RadComboBinder.callBaseMethod(this, "dispose");
    },

    // Overridden method from the ClientBinder. This method binds data to the target control.
    DataBind: function () {
        Telerik.Sitefinity.Web.UI.RadComboBinder.callBaseMethod(this, "DataBind");
        // get all items.
        var clientManager = this.get_manager();
        clientManager.GetItemCollection(this);
    },

    // This method binds the form, with the specified sort expression. It is a shorthand to setting the 
    // sortExpression property and calling the bind.
    Sort: function (sortExpression) {
        this._sortExpression = sortExpression;
        this.DataBind();
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
        this._dataBindingHandler(data);

        var dataItems = data.Items;
        var dataItemCount = data.Items.length;

        this._comboBox = $find(this._targetId);
        this._comboBox.remove_selectedIndexChanging(this._selectedIndexChangingDelegate);
        this._comboBox.add_selectedIndexChanging(this._selectedIndexChangingDelegate);

        // clear all items in the combo box
        var comboBoxItems = this._comboBox.get_items();
        comboBoxItems.clear();

        // create static item
        var staticItem = this._createStaticItem();
        if (staticItem) {
            comboBoxItems.add(staticItem);
        }

        // create data bound items
        for (var i = 0; i < dataItemCount; i++) {
            var dataItem = dataItems[i];
            this._itemDataBindingHandler(dataItem);

            var comboItem = this._createComboItem(dataItem, i, false);
            comboBoxItems.add(comboItem);
            this._itemDataBoundHandler(dataItem[this._dataKeyNames[0]], dataItem, i, comboItem);
        }

        comboBoxItems.getItem(0).select();
        this._dataBoundHandler(data);
    },

    // Overridden method from the ClientBinder. The ClientManager will call this method afer it receives data
    // (single item) from the service and pass the deserialized item to it.
    BindItem: function (data) {
        alert('BindItem function is not supported by RadComboBinder. Please use Bind instead.');
    },

    /* ********************* private methods ********************* */
    _createStaticItem: function () {
        if (this._staticItemText == null && this._staticItemValue == null) {
            return;
        }

        var staticItem = new Telerik.Web.UI.RadComboBoxItem();
        staticItem.set_text(this._staticItemText);
        staticItem.set_value(this._staticItemValue);
        return staticItem;
    },

    _createComboItem: function (dataItem, dataItemIndex, isGenericItem) {
        // create a new instance of RadComboItem
        var comboItem = new Telerik.Web.UI.RadComboBoxItem();
        // create the container; wrapper element
        // TODO: make, the container element tag configurable
        var container = document.createElement('div');
        container.innerHTML = '';

        this.LoadItem(container, this._template, dataItem, isGenericItem, dataItemIndex);
        var itemText = '';
        if (this._levelField) {
            itemText = this._getIndentation(dataItem);
        }
        itemText += $(container).text().trim();
        comboItem.set_text(itemText);

        var key = this._getItemKey(dataItem);
        comboItem.set_value(key);

        return comboItem;
    },

    _getIndentation: function (dataItem) {
        var indentation = '';
        var level = Number(dataItem[this._levelField]);
        for (var x = 0; x < level; x++) {
            indentation += '&nbsp;&nbsp;';
        }
        return indentation;
    },

    _getItemKey: function (dataItem) {
        var key = [];
        var dataKeyNames = this.get_dataKeyNames();
        var dataKeyNamesLength = dataKeyNames.length;
        for (var i = 0; i < dataKeyNamesLength; i++) {
            key[dataKeyNames[i]] = dataItem[dataKeyNames[i]];
        }
        return key;
    },

    _selectedIndexChanging: function (sender, args) {
        args.set_cancel(true);
        var item = args.get_item();
        var tempElem = document.createElement('span');
        $(tempElem).html(item.get_text());
        this._comboBox.set_text($(tempElem).text().trim());
        this._comboBox.set_value(item.get_value());
        tempElem = null;
    },

    /* ********************* properties ********************* */
    get_selectedItem: function () {
        return this._comboBox.get_value();
    },

    set_selectedItem: function (keyName, value) {
        var items = this._comboBox.get_items().toArray();
        for (var i = 0, len = items.length; i < len; i++) {
            var item = items[i].get_value();
            if (item && item[keyName] == value) {
                items[i].select();
                break;
            }
        }
    },

    get_staticItemText: function () {
        return this._staticItemText;
    },
    set_staticItemText: function (value) {
        this._staticItemText = value;
    },
    get_staticItemValue: function () {
        return this._staticItemValue;
    },
    set_staticItemValue: function (value) {
        this._staticItemValue = value;
    }
}

Telerik.Sitefinity.Web.UI.RadComboBinder.registerClass('Telerik.Sitefinity.Web.UI.RadComboBinder', Telerik.Sitefinity.Web.UI.ClientBinder);
