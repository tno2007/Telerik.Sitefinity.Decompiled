Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements");

Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem = function(element, sortDataItem, conditionsCount, propertiesCount) {

    //constructor parameters
    this._sortDataItem = sortDataItem;
    this._conditionsCount = conditionsCount;
    this._propertiesCount = propertiesCount;

    //delegates
    this._addAnotherSortConditionDelegate = null;
    this._removeSortConditionDelegate = null;
    this._changeSortByConditionDelegate = null;
    this._changeSortTypeConditionDelegate = null;

    //method calls
    this.instantiateConditionTemplate(element);

    //variables
    this._sortBySelect = null;
    this._radioButtons = null;

    Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem.callBaseMethod(this, "initialize");

        this.initAddAnotherLink();
        this.ensureUniqueAttributeName();
        this.initRemoveLink();
        this.initSortBySelect();
        this.initSortTypeSelect();
        this.initSortByLabel();

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },

    dispose: function () {
        var element = this.get_element();
        $clearHandlers(element);

        if (this._addAnotherSortConditionDelegate)
            delete this._addAnotherSortConditionDelegate;
        if (this._removeSortConditionDelegate)
            delete this._removeSortConditionDelegate;
        if (this._changeSortByConditionDelegate)
            delete this._changeSortByConditionDelegate;
        if (this._changeSortTypeConditionDelegate)
            delete this._changeSortTypeConditionDelegate;

        this._sortDataItem = null;
        this._conditionsCount = null;
        this._propertiesCount = null;

        this._sortBySelect = null;
        this._radioButtons = null;

        Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem.callBaseMethod(this, "dispose");
    },


    /* --------------------  public methods ----------- */

    add_addAnotherSortCondition: function (handler) {
        this.get_events().addHandler('addSortCondition', handler);
    },

    remove_addAnotherSortCondition: function (handler) {
        this.get_events().removeHandler('addSortCondition', handler);
    },

    add_removeSortCondition: function (handler) {
        this.get_events().addHandler('removeSortCondition', handler);
    },

    remove_removeSortCondition: function (handler) {
        this.get_events().removeHandler('removeSortCondition', handler);
    },

    add_changeSortByCondition: function (handler) {
        this.get_events().addHandler('changeSortByCondition', handler);
    },

    remove_changeSortByCondition: function (handler) {
        this.get_events().addHandler('changeSortByCondition', handler);
    },

    add_changeSortTypeCondition: function (handler) {
        this.get_events().addHandler('changeSortTypeCondition', handler);
    },

    remove_changeSortTypeCondition: function (handler) {
        this.get_events().addHandler('changeSortTypeCondition', handler);
    },

    ensureUniqueAttributeName: function () {

        //the 'name' attribute at the radio button in a sort condition item group should be the same
        var ordinal = this.get_ordinal();
        var elem = this.get_element();
        $(elem).find('input').attr('name', 'sort_' + ordinal);

        //fix id of select
        $(elem).find('.sortBySelect').attr('id', 'sortBySelect' + ordinal);
        
        //fix id of input radio
        $(elem).find("input[id*=ascRadio]").attr('id', 'ascRadio' + ordinal);
        $(elem).find("input[id*=descRadio]").attr('id', 'descRadio' + ordinal);

        //fix attributes 'for'
        $(elem).find('[for]').each(function (i1, element) {
            var attrUnique = $(element).attr('for') + ordinal;
            $(element).attr('for', attrUnique);
        });

    },

    //Determines if the current sort condtion item is the last one
    isLast: function () {
        return this._conditionsCount == this.get_ordinal() + 1;
    },

    //Determines if the current sort condtion item is the first one
    isFirst: function () {
        return this.get_ordinal() == 0;
    },

    //max available sorting conditions will be equal to number of properties
    isMaxItemsLimitReached: function () {
        return this._propertiesCount == this._conditionsCount;
    },

    instantiateConditionTemplate: function (element) {
        var template = new Sys.UI.Template($get('sfBasicItemTemplate'));
        template.instantiateIn(element);
    },
    /* -------------------- events -------------------- */

    //attaches handler to dropdown with properties
    initSortBySelect: function () {
        this._sortBySelect = $(this.get_element()).find('.sortBySelect').get(0);
        if (this._sortBySelect != null) {
            if (this._changeSortByConditionDelegate === null)
                this._changeSortByConditionDelegate = Function.createDelegate(this, this._changeSortByConditionHandler);
            $addHandler(this._sortBySelect, 'blur', this._changeSortByConditionDelegate);
        }
    },


    //attaches handler to radio buttons with sorting type - ASC, DESC
    initSortTypeSelect: function () {
        if (this._changeSortTypeConditionDelegate === null)
            this._changeSortTypeConditionDelegate = Function.createDelegate(this, this._changeSortTypeConditionHandler);
        this._get_radioChoices().click(this._changeSortTypeConditionDelegate);
    },

    //ensures that only last condition will contain link for adding another condition
    initAddAnotherLink: function () {
        var addButton = $(this.get_element()).find('.addAnotherLink').get(0);
        if (addButton != null) {
            if (this.isLast() && !this.isMaxItemsLimitReached()) {
                if (this._addAnotherSortConditionDelegate === null)
                    this._addAnotherSortConditionDelegate = Function.createDelegate(this, this._addSortConditionHandler);
                $addHandler(addButton, 'click', this._addAnotherSortConditionDelegate);
                $(addButton).show();
            }
            else {
                $(addButton).hide();
            }
        }
    },

    //ensures that first condition will not contain link for removing condition
    initRemoveLink: function () {
        var removeLink = $(this.get_element()).find('.removeLink').get(0);
        if (removeLink != null) {
            if (this.isFirst()) {
                $(removeLink).hide();
            }
            else {
                $(removeLink).show();
                if (this._removeSortConditionDelegate === null)
                    this._removeSortConditionDelegate = Function.createDelegate(this, this._removeSortConditionHandler);
                $addHandler(removeLink, 'click', this._removeSortConditionDelegate);
            }
        }
    },
    //if the sort condition is first item the label has caption 'sortBy' otherwise 'thenBy'
    initSortByLabel: function () {
        var sortByLabel = $(this.get_element()).find('.sortByLabel').get(0);
        var thenByLabel = $(this.get_element()).find('.thenByLabel').get(0);
        if (this.isFirst()) {
            $(sortByLabel).show();
            $(thenByLabel).hide();
        }
        else {
            $(sortByLabel).hide();
            $(thenByLabel).show();
        }
    },
    /* -------------------- event handlers ------------ */

    // This function will rise addSortCondition event.
    _addSortConditionHandler: function (event) {
        event.preventDefault();
        var h = this.get_events().getHandler('addSortCondition');
        if (h) h(this._sortDataItem.get_ordinal());
    },

    // This function will rise removeSortCondition event.
    _removeSortConditionHandler: function (event) {
        event.preventDefault();
        var h = this.get_events().getHandler('removeSortCondition');
        if (h) h(this._sortDataItem.get_ordinal());
    },

    // This function will rise changeSortByCondition event.
    _changeSortByConditionHandler: function (event) {
        var newValue = this._sortBySelect.value;
        var h = this.get_events().getHandler('changeSortByCondition');
        this._sortDataItem.set_sortBy(newValue);
        if (h) h(this._sortDataItem._ordinal, newValue);
    },

    _changeSortTypeConditionHandler: function (event) {
        var newSortTypeValue = event.target.value;
        var h = this.get_events().getHandler('changeSortByCondition');
        this._sortDataItem.set_sortType(newSortTypeValue);
        if (h) h(this._sortDataItem.get_sortType(), newSortTypeValue);
    },

    /* -------------------- private methods ----------- */

    // gets all the radio buttons in the container of this control
    _get_radioChoices: function () {
        if (!this._radioButtons) {
            this._radioButtons = $(this.get_element()).find('input[type|=radio]');
        }
        return this._radioButtons;
    },

    /* -------------------- properties ---------------- */

    get_sortDataItem: function () {
        return this._sortDataItem;
    },

    set_sortDataItem: function (value) {
        if (this._sortDataItem !== value) {
            this._sortDataItem = value;
            this.raisePropertyChanged('sortDataItem');
        }
    },

    get_sortBy: function () {
        return this.get_sortDataItem().get_sortBy();
    },

    set_sortBy: function (value) {
        this.get_sortDataItem().set_sortBy() = value;
    },

    get_ordinal: function () {
        return this.get_sortDataItem().get_ordinal();
    },

    set_ordinal: function (value) {
        this.get_sortDataItem().set_ordinal() = value;
    },

    get_conditionsCount: function () {
        return this._conditionsCount;
    },

    set_conditionsCount: function (value) {
        this._conditionsCount = value;
    },

    get_propertiesCount: function () {
        return this._propertiesCount;
    },

    set_propertiesCount: function (value) {
        this._propertiesCount = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem.registerClass('Telerik.Sitefinity.Web.UI.Backend.Elements.SortConditionItem', Sys.UI.Control);