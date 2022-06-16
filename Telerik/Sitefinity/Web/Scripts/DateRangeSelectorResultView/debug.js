Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView = function (element) {
    this._selector = null;
    this._chooseButtonTextControlId = null;
    this._selectLabel = null;
    this._changeLabel = null;

    Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView.callBaseMethod(this, "initialize");

    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView.callBaseMethod(this, "dispose");

    },
    /* --------------------------------- public methods ---------------------------------- */
    refreshUI: function () {
        var value = this._selector.get_value();
        if (value) {
            this._binder.BindCollection({ Items: [value] });
            jQuery("#" + this._chooseButtonTextControlId).html(this._changeLabel);
        } else {
            this._binder.ClearTarget();
            jQuery("#" + this._chooseButtonTextControlId).html(this._selectLabel);
        }
    },

    validate: function () {
        return this._selector.validate();
    },

    // Gets the selected items from the selector result view.
    get_selectedItems: function () {
        return this.get_selectedValues();
    },
    // Sets the selected items from the selector result view.
    set_selectedItems: function (value) {
        this.set_selectedValues(value);
    },
    // Gets the selected values from the selector result view. Used by the QuerySelectorItem to build a collection of QueryItem objects
    get_selectedValues: function () {
        var val = this._selector.get_value();
        if (val)
            return [val];
        else
            return [];
    },
    // Sets the selected values from the selector result view. Used by the QuerySelectorItem 
    set_selectedValues: function (value) {
        if (value && value.length > 0)
            this._selector.set_value(value[0]);
        else
            this._selector.set_value(null);
        this.refreshUI();
    },
    /* --------------------------------- event handlers ---------------------------------- */
    onLoad: function () {
    },
    doneClicked: function () {
        this.refreshUI();
    },
    cancelClicked: function () {
    }
}

Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView.registerClass('Telerik.Sitefinity.Web.UI.DateRangeSelectorResultView', Telerik.Sitefinity.Web.UI.SelectorResultView);