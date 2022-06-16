﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");

// ======================== QueryItem class ==================================

Telerik.Sitefinity.Web.UI.QueryItem = function(element, queryDataItem) {
    this._queryDataItem = queryDataItem;
    this._selectDelegate = null;
    this._conditionAddedDelegate = null;
    this._conditionRemovedDelegate = null;
    this._joinTypeChangeDelegate = null;
    this._joinTypeSelect = null;
    this._propertyChangeDelegate = null;
    this._propertiesSelect = null;
    this._operatorChangeDelegate = null;
    this._operatorsSelect = null;
    this._valueSelector = null;
    this._valueChangedDelegate = null;
    var template = new Sys.UI.Template($('#sfBasicItemTemplate').get(0));
    template.instantiateIn(element);

    Telerik.Sitefinity.Web.UI.QueryItem.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.QueryItem.prototype = {
    // set up
    initialize: function() {
        if (this._selectDelegate === null) {
            this._selectDelegate = Function.createDelegate(this, this._selectHandler);
        }
        if (this._conditionAddedDelegate === null) {
            this._conditionAddedDelegate = Function.createDelegate(this, this._conditionAddedHandler);
        }
        if (this._conditionRemovedDelegate === null) {
            this._conditionRemovedDelegate = Function.createDelegate(this, this._conditionRemovedHandler);
        }
        if (this._joinTypeChangeDelegate === null) {
            this._joinTypeChangeDelegate = Function.createDelegate(this, this._joinTypeChangeHandler);
        }
        if (this._propertyChangeDelegate === null) {
            this._propertyChangeDelegate = Function.createDelegate(this, this._propertyChangeHandler);
        }
        if (this._operatorChangeDelegate === null) {
            this._operatorChangeDelegate = Function.createDelegate(this, this._operatorChangeHandler);
        }
        if (this._valueChangedDelegate === null) {
            this._valueChangedDelegate = Function.createDelegate(this, this._valueChangedHandler);
        }

        var itemCheckBox = $(this.get_element()).find('.sfItemGrouping').get(0);
        $addHandler(itemCheckBox, 'click', this._selectDelegate);

        var addButton = $(this.get_element()).find('.sfAddCondition').get(0);
        $addHandler(addButton, 'click', this._conditionAddedDelegate);

        var removeButton = $(this.get_element()).find('.sfRemoveCondition').get(0);
        $addHandler(removeButton, 'click', this._conditionRemovedDelegate);

        this._joinTypeSelect = $(this.get_element()).find('.sfJoin').get(0);
        $addHandler(this._joinTypeSelect, 'change', this._joinTypeChangeDelegate);

        this._propertiesSelect = $(this.get_element()).find('.sfFields').get(0);
        $addHandler(this._propertiesSelect, 'change', this._propertyChangeDelegate);

        this._operatorsSelect = $(this.get_element()).find('.sfOperators').get(0);
        $addHandler(this._operatorsSelect, 'change', this._operatorChangeDelegate);

        this._valueSelector = $(this.get_element()).find('.sfValue').get(0);
        $addHandler(this._valueSelector, 'blur', this._valueChangedDelegate);

        Telerik.Sitefinity.Web.UI.QueryItem.callBaseMethod(this, "initialize");
    },

    // tear down
    dispose: function() {
        var element = this.get_element();
        $clearHandlers(element);
        if (this._selectDelegate) {
            delete this._selectDelegate;
        }
        if (this._conditionAddedDelegate) {
            delete this._conditionAddedDelegate;
        }
        if (this._conditionRemovedDelegate) {
            delete this._conditionRemovedDelegate;
        }
        if (this._joinTypeChangeDelegate) {
            delete this._joinTypeChangeDelegate;
        }
        if (this._propertyChangeDelegate) {
            delete this._propertyChangeDelegate;
        }
        if (this._operatorChangeDelegate) {
            delete this._operatorChangeDelegate;
        }
        Telerik.Sitefinity.Web.UI.QueryItem.callBaseMethod(this, "dispose");
    },

    // ====================== Events for QueryItem ===========================

    add_select: function(handler) {
        this.get_events().addHandler('select', handler);
    },

    remove_select: function(handler) {
        this.get_events().removeHandler('select', handler);
    },

    add_conditionadd: function(handler) {
        this.get_events().addHandler('conditionadd', handler);
    },

    remove_conditionadd: function(handler) {
        this.get_events().removeHandler('conditionadd', handler);
    },

    add_conditionremove: function(handler) {
        this.get_events().addHandler('conditionremove', handler);
    },

    remove_conditionremove: function(handler) {
        this.get_events().removeHandler('conditionremove', handler);
    },

    add_valuechanged: function(handler) {
        this.get_events().addHandler('valuechanged', handler);
    },

    remove_valuechanged: function(handler) {
        this.get_events().removeHandler('valuechanged', handler);
    },

    _selectHandler: function(event) {
        var h = this.get_events().getHandler('select');
        var itemCheckbox = $(this.get_element()).find('.sfItemGrouping').get(0);
        if (h) h(this._queryDataItem.get_ItemPath(), itemCheckbox.checked);
    },

    _conditionAddedHandler: function(event) {
        var h = this.get_events().getHandler('conditionadd');
        if (h) h(this._queryDataItem.get_ItemPath());
    },

    _conditionRemovedHandler: function() {
        var h = this.get_events().getHandler('conditionremove');
        if (h) h(this._queryDataItem.get_ItemPath());
    },

    _joinTypeChangeHandler: function(event) {
        var newValue = this._joinTypeSelect.options[this._joinTypeSelect.selectedIndex].text;
        this._queryDataItem.set_Join(newValue);
    },

    _propertyChangeHandler: function(event) {
        var newValue = this._propertiesSelect.options[this._propertiesSelect.selectedIndex].text;
        this._queryDataItem.get_Condition().set_FieldName(newValue);
    },

    _operatorChangeHandler: function(event) {
        var newValue = this._operatorsSelect.options[this._operatorsSelect.selectedIndex].text;
        this._queryDataItem.get_Condition().set_Operator(newValue);
    },

    _valueChangedHandler: function(event) {
        var newValue = this._valueSelector.value;
        var h = this.get_events().getHandler('valuechanged');
        this._queryDataItem.set_Value(newValue);
        if (h) h(this._queryDataItem.get_ItemPath(), newValue);
    },

    // ====================== Properties for QueryItem =======================
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

Telerik.Sitefinity.Web.UI.QueryItem.registerClass('Telerik.Sitefinity.Web.UI.QueryItem', Sys.UI.Control);