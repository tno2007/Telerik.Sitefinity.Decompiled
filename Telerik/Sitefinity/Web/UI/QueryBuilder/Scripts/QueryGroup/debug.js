Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ======================== QueryGroup class ==================================

Telerik.Sitefinity.Web.UI.QueryGroup = function(element, queryDataItem) {
    this._queryDataItem = queryDataItem;
    this._ungroupDelegate = null;
    this._joinTypeChangeDelegate = null;
    this._joinTypeSelect = null;

    var dataItem = { 'GroupName': this._queryDataItem.get_Name() };
    var template = new Sys.UI.Template($('#sfGroupTemplate').get(0));
    template.instantiateIn(element, null, dataItem);

    Telerik.Sitefinity.Web.UI.QueryGroup.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.QueryGroup.prototype = {
    // set up
    initialize: function() {
        Telerik.Sitefinity.Web.UI.QueryGroup.callBaseMethod(this, "initialize");

        // handle ungrouping
        if (this._ungroupDelegate === null) {
            this._ungroupDelegate = Function.createDelegate(this, this._ungroupHandler)
        }
        if (this._joinTypeChangeDelegate === null) {
            this._joinTypeChangeDelegate = Function.createDelegate(this, this._joinTypeChangeHandler);
        }

        var ungroupButton = $(this.get_element()).find('li').find('.btnUngroup').get(0);
        $addHandler(ungroupButton, 'click', this._ungroupDelegate);

        this._joinTypeSelect = $(this.get_element()).find('li').find('.sfJoin').get(0);
        $addHandler(this._joinTypeSelect, 'change', this._joinTypeChangeDelegate);
    },

    // tear down
    dispose: function() {
        var element = this.get_element();
        $clearHandlers(element);
        if (this._ungroupDelegate) {
            delete this._ungroupHandler;
        }
        if (this._joinTypeChangeDelegate) {
            delete this._joinTypeChangeDelegate;
        }
        Telerik.Sitefinity.Web.UI.QueryGroup.callBaseMethod(this, "dispose");
    },

    // ======================= Events for QueryGroup ===========================

    add_ungroup: function(handler) {
        this.get_events().addHandler('ungroup', handler);
    },

    remove_ungroup: function(handler) {
        this.get_events().removeHanlder('ungroup', handler);
    },

    _ungroupHandler: function(event) {
        var h = this.get_events().getHandler('ungroup');
        if (h) h(this._queryDataItem.get_ItemPath());
    },

    _joinTypeChangeHandler: function(event) {
        var newValue = this._joinTypeSelect.options[this._joinTypeSelect.selectedIndex].text;
        this._queryDataItem.set_Join(newValue);
    },

    // ====================== Properties for QueryGroup =======================
    get_queryDataItem: function() {
        return this._queryDataItem;
    },

    set_queryDataItem: function(value) {
        if (this._queryDataItem !== value) {
            this._queryDataItem = value;
            this.raisePropertyChanged('queryDataItem');
        }
    }
}

Telerik.Sitefinity.Web.UI.QueryGroup.registerClass('Telerik.Sitefinity.Web.UI.QueryGroup', Sys.UI.Control);