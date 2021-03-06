Type.registerNamespace("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets");

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget = function (element) {
    Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget.initializeBase(this, [element]);

    this._selectAllButton = null;
    this._selectAllButtonClickDelegate = null;

    this._selectNoneButton = null;
    this._selectNoneButtonClickDelegate = null;

    this._name = null;
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget.callBaseMethod(this, 'initialize');

        this._selectAllButtonClickDelegate = Function.createDelegate(this, this._selectAllButtonClickHandler);
        $addHandler(this.get_selectAllButton(), "click", this._selectAllButtonClickDelegate);

        this._selectNoneButtonClickDelegate = Function.createDelegate(this, this._selectNoneButtonClickHandler);
        $addHandler(this.get_selectNoneButton(), "click", this._selectNoneButtonClickDelegate);
    },

    dispose: function () {
        if (this._selectAllButtonClickDelegate) {
            if (this.get_selectAllButton()) {
                $removeHandler(this.get_selectAllButton(), "click", this._selectAllButtonClickDelegate);
            }
            delete this._selectAllButtonClickDelegate;
        }

        if (this._selectNoneButtonClickDelegate) {
            if (this.get_selectNoneButton()) {
                $removeHandler(this.get_selectNoneButton(), "click", this._selectNoneButtonClickDelegate);
            }
            delete this._selectNoneButtonClickDelegate;
        }

        Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget.callBaseMethod(this, 'dispose');
    },

    _selectAllButtonClickHandler: function (sender, args) {
        jQuery(".selectCommand:not(:checked)").each(function () {
            this.click();
        });
    },

    _selectNoneButtonClickHandler: function (sender, args) {
        jQuery(".selectCommand:checked").each(function () {
            this.click();
        });
    },

    get_selectAllButton: function () {
        return this._selectAllButton;
    },
    set_selectAllButton: function (value) {
        this._selectAllButton = value;
    },

    get_selectNoneButton: function () {
        return this._selectNoneButton;
    },
    set_selectNoneButton: function (value) {
        this._selectNoneButton = value;
    },

    get_name: function () {
        return this._name;
    },
    set_name: function (value) {
        this._name = value;
    }
}

Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget.registerClass("Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectionWidget", Sys.UI.Control);