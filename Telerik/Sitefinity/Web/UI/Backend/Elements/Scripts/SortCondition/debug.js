Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

// ======================== SortCondition class ==================================

Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition = function (element, sortData, typeProperties, removeAllLink) {

    this._containerId = element;
    this._sortData = sortData;
    this._typeProperties = typeProperties;
    this._removeAllLink = removeAllLink;

    Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition.callBaseMethod(this, "initialize");

        this.buildUI();
    },

    dispose: function () {
        this._containerId = null;
        this._sortData = null;
        this._typeProperties = null;
        this._removeAllLink = null;

        Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition.callBaseMethod(this, "dispose");
    },

    /* -------------------- public methods -------------------- */

    createDefaultItems: function () {
        if (this._sortData == null || !this._sortData.haveDataItems()) {
            this._sortData = new Telerik.Sitefinity.Web.UI.Backend.Elements.SortData();
            var defaultItem = [
                    this._sortData.createSortDataItem(0, 'select', 'ASC')
            // , this._sortData.createSortDataItem(1, 'Description', 'DESC')
                ];

            this._sortData.set_typeProperties(this._typeProperties);
            this._sortData.set_dataItems(defaultItem);
        }
    },

    buildUI: function () {
        this.createDefaultItems();
        $('#' + this._containerId).empty();
        this._buildSortConditions(this._sortData, this._containerId);
    },

    onAddAnotherSortCondition: function (ordinal) {
        this._sortData.conditionAdded(ordinal);
        this.buildUI();
        this._addSortConditionHandler();
    },

    onRemoveSortCondition: function (ordinal) {
        this._sortData.conditionRemoved(ordinal);
        this.buildUI();
        this._removeSortConditionHandler();
    },

    onChangeSortByCondition: function (ordinal, newValue) {
        this._sortData.conditionUpdated(ordinal, newValue);
    },

    onChangeSortTypeCondition: function (ordinal, newValue) {
        this._sortData.conditionSortTypeUpdated(ordinal, newValue);
    },

    reset: function () {
        this._sortData.reset();
        this.buildUI();
    },

    getSortExpression: function () {
        return this._sortData.getSortExpression();
    },

    // This function allows other objects to subscribe to the addSortCondition event of the sort condition item via sort condition control
    add_addAnotherSortCondition: function (handler) {
        this.get_events().addHandler('addSortCondition', handler);
    },

    // This function allows other objects to unsubscribe to the addSortCondition event of the sort condition item via sort condition control
    remove_addAnotherSortCondition: function (handler) {
        this.get_events().removeHandler('addSortCondition', handler);
    },

    // This function allows other objects to subscribe to the removeSortCondition event of the sort condition item via sort condition control
    add_removeSortCondition: function (handler) {
        this.get_events().addHandler('removeSortCondition', handler);
    },

    // This function allows other objects to unsubscribe to the removeSortCondition event of the sort condition item via sort condition control
    remove_removeSortCondition: function (handler) {
        this.get_events().removeHandler('removeSortCondition', handler);
    },
    /* -------------------- events -------------------- */


    /* -------------------- event handlers ------------ */

    // This function will rise addSortCondition event.
    _addSortConditionHandler: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('addSortCondition');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    // This function will rise removeSortCondition event.
    _removeSortConditionHandler: function () {
        if (typeof this.get_events == 'function') {
            var h = this.get_events().getHandler('removeSortCondition');
            if (h) h(this, Sys.EventArgs.Empty);
            return Sys.EventArgs.Empty;
        }
    },

    /* -------------------- private methods ----------- */

    _buildSortConditions: function (sortData) {
        var sortConditionsCount = sortData._dataItems.length;
        this._showHideRemoveAllLink(sortConditionsCount >= 3);

        for (var i = 0; i < sortConditionsCount; i++) {
            //update ordinal
            sortData._dataItems[i].set_ordinal(i);
            this._createItem(sortData._dataItems[i], sortConditionsCount);
        }
    },

    _createItem: function (sortItem, itemsCount) {
        var element = document.createElement('li');
        $('#' + this._containerId).append(element);

        var sortItemControl = new Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem(element, sortItem, itemsCount, this._typeProperties.length);
        sortItemControl.initialize();

        if (sortItemControl.isLast()) {
            sortItemControl.remove_addAnotherSortCondition(Function.createDelegate(this, this.onAddAnotherSortCondition));
            sortItemControl.add_addAnotherSortCondition(Function.createDelegate(this, this.onAddAnotherSortCondition));
        }

        if (!sortItemControl.isFirst()) {
            sortItemControl.remove_removeSortCondition(Function.createDelegate(this, this.onRemoveSortCondition));
            sortItemControl.add_removeSortCondition(Function.createDelegate(this, this.onRemoveSortCondition));
        }

        sortItemControl.remove_changeSortByCondition(Function.createDelegate(this, this.onChangeSortByCondition));
        sortItemControl.add_changeSortByCondition(Function.createDelegate(this, this.onChangeSortByCondition));

        sortItemControl.remove_changeSortTypeCondition(Function.createDelegate(this, this.onChangeSortTypeCondition));
        sortItemControl.add_changeSortTypeCondition(Function.createDelegate(this, this.onChangeSortTypeCondition));

        this._populateProperties(sortItem, element);
        this._setSortItemType(sortItem, element);
    },

    //populates dropdown with properties
    _populateProperties: function (sortItem, element) {
        var properties = this._sortData.get_typeProperties();
        var propertiesCount = properties.length;
        var selectedIndex = 0;
        for (var i = 0; i < propertiesCount; i++) {
            this._appendOptionToSelect(properties[i].Value, properties[i].Key, $(element).find('.sortBySelect:first').get(0));
            if (properties[i].Key === sortItem.get_sortBy()) {
                selectedIndex = i;
            }
        }
        this._setSortBySelect(element, selectedIndex);
    },

    _setSortBySelect: function (element, selectedIndex) {
        $(element).find('.sortBySelect:first').get(0).selectedIndex = selectedIndex;
    },

    ///method sets ascending or descending sorting type for an item
    _setSortItemType: function (sortItem, element) {
        var items = $(element).find("input:radio[name=" + "sort_" + sortItem._ordinal + "]");
        if (items)
            $(items).filter("[value='" + sortItem._sortType + "']").attr('checked', true);
    },



    _appendOptionToSelect: function (text, value, element) {
        var optionToAppend = document.createElement('option');
        optionToAppend.setAttribute('text', text);
        optionToAppend.setAttribute('value', value);
        optionToAppend.innerHTML = text;

        element.appendChild(optionToAppend);
    },

    _showHideRemoveAllLink: function (toShow) {
        var link = $get(this._removeAllLink);
        if (toShow)
            link.style.display = '';
        else
            link.style.display = 'none';

    },
    /* -------------------- properties ---------------- */

    get_sortData: function () {
        return this._sortData;
    },

    set_sortData: function (value) {
        if (this._sortData !== value) {
            this._sortData = value;
        }
    },

    get_typeProperties: function () {
        return this._typeProperties;
    },

    set_typeProperties: function (value) {
        if (this._typeProperties !== value) {
            this._typeProperties = value;
        }
    },

    get_containerId: function () {
        return this._containerId;
    },
    set_containerId: function (value) {
        this._containerId = value;
    },

    get_removeAllLink: function () {
        return this._removeAllLink;
    },

    set_removeAllLink: function (value) {
        this._removeAllLink = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition.registerClass('Telerik.Sitefinity.Web.UI.Backend.Elements.SortCondition', Sys.UI.Control);
