
Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

/* SortData class */

Telerik.Sitefinity.Web.UI.Backend.Elements.SortData = function() {

    this._dataItems = [];
    this._typeProperties = [];

    Telerik.Sitefinity.Web.UI.Backend.Elements.SortData.initializeBase(this);
}
Telerik.Sitefinity.Web.UI.Backend.Elements.SortData.prototype = {

    // set up
    initialize: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortData.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function() {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortData.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */
    createDefauldSortDataItem: function(ordinal) {
        return this.createSortDataItem(ordinal, 'select', 'ASC');
    },

    createSortDataItem: function(ordinal, sortBy, sortType) {
        var item = new Telerik.Sitefinity.Web.UI.Backend.Elements.SortDataItem();

        item.set_ordinal(ordinal);
        item.set_sortBy(sortBy);
        item.set_sortType(sortType);

        return item;
    },

    /* -------------------- public methods ----------- */

    conditionAdded: function(ordinal) {
        var newItem = this.createDefauldSortDataItem(++ordinal);
        this._dataItems.push(newItem);
    },

    conditionRemoved: function(ordinal) {
        this._dataItems.splice(ordinal, 1);
    },

    conditionUpdated: function(ordinal, newValue) {
        if (this._dataItems[ordinal] != null) {
            this._dataItems[ordinal].set_sortBy(newValue);
        }
    },

    conditionSortTypeUpdated: function(ordinal, newValue) {
        if (this._dataItems[ordinal] != null) {
            this._dataItems[ordinal].set_sortType(newValue);
        }
    },

    reset: function() {
        this._dataItems = [];
    },

    haveDataItems: function() {
        return this._dataItems && this._dataItems.length;
    },

    //retrieves 1-dimentional array with all sorting expressions
    getSortBy: function() {
        var i = 0, len = this._dataItems.length;
        var items = [];
        while (i < len) {
            items.push(this._dataItems[i].get_sortBy);
            i++;
        }
        return items;
    },

    //parse sort expression and creates a data item for each sort condition
    setSortExpression: function(sortExpression) {
        var items = sortExpression.split(",")
        for (i = 0, itemsLength = items.length; i < itemsLength; i++) {
            items[i] = $.trim(items[i]);
            var conditonItem = items[i].split(" ");
            var sortBy = conditonItem[0];
            var sortType = conditonItem[1] != null ? conditonItem[1] : "ASC";

            //if array already contains certain sort condition, it is ignored.
            if ($.inArray(sortBy, this.getSortBy()) < 0)
                this._sortData.createSortDataItem(i, sortBy, sortType)
        }
    },

    //generates sort expression from an array of data item objects 
    getSortExpression: function() {
        var sortBuilder = '';
        var sortByClauses = [];
        
        this.fixDataItems();

        for (var i = 0; i < this._dataItems.length; i++) {
            var item = this._dataItems[i];
            sortBuilder += item.get_sortBy() + " " + item.get_sortType() + ",";
        }

        //remove last comma    
        if (sortBuilder.length > 0)
            return sortBuilder.substring(0, sortBuilder.length - 1);
    },

    //removes repetitions and empty(properties dropdown that are not selected, but added) data items 
    fixDataItems: function() {
        var sortByClauses = [];
        var itemsCount = this._dataItems.length;
        for (var i = itemsCount - 1; i >= 0; i--) {
            var item = this._dataItems[i];
            var sortBy = item.get_sortBy();

            //fix array if empty(property is not selected) data items are added
            if (sortBy == 'select') {
                this._dataItems.splice(i, 1);
                continue;
            }
            //add existing sort condition to an array
            if ($.inArray(sortBy, sortByClauses) < 0)
                sortByClauses.push(sortBy);
            else { //remove repetitions
                this._dataItems.splice(i, 1);
            }
        }
    },


    /* -------------------- events -------------------- */


    /* -------------------- event handlers ------------ */


    /* -------------------- private methods ----------- */


    /* -------------------- properties ---------------- */


    get_typeProperties: function() {
        return this._typeProperties;
    },
    set_typeProperties: function(value) {
        if (this._typeProperties != value) {
            this._typeProperties = value;
            this.raisePropertyChanged('typeProperties');
        }
    },

    get_dataItems: function() {
        return this._dataItems;
    },
    set_dataItems: function(value) {
        if (this._dataItems != value) {
            this._dataItems = value;
            this.raisePropertyChanged('dataItems');
        }
    }
};


Telerik.Sitefinity.Web.UI.Backend.Elements.SortData.registerClass(' Telerik.Sitefinity.Web.UI.Backend.Elements.SortData', Sys.Component);
