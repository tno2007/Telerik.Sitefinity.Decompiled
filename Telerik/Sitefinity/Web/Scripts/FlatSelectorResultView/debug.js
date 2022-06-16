﻿Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.FlatSelectorResultView = function (element) {
    this._selector = null;
    this._chooseButtonTextControlId = null;
    this._selectLabel = null;
    this._changeLabel = null;
    this._selection = [];
    this._selectedIds = [];
    this._allowMultipleSelection = null;
    this._providerName = null;

    this._binderDataBoundDelegate = null;
    this._onBinderDataBoundDelegate = null;
    Telerik.Sitefinity.Web.UI.FlatSelectorResultView.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.FlatSelectorResultView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FlatSelectorResultView.callBaseMethod(this, "initialize");

        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        this._selector.add_binderDataBound(this._binderDataBoundDelegate);

        this._onBinderDataBoundDelegate = Function.createDelegate(this, this._onBinderDataBound);
        this._binder.add_onDataBound(this._onBinderDataBoundDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.FlatSelectorResultView.callBaseMethod(this, "dispose");

        this._selector.remove_binderDataBound(this._binderDataBoundDelegate);
        delete this._binderDataBoundDelegate;

        if (this._onBinderDataBoundDelegate) {
            this._binder.remove_onDataBound(this._onBinderDataBoundDelegate);
            delete this._onBinderDataBoundDelegate;
        }
    },
    /* --------------------------------- public methods ---------------------------------- */
    refreshUI: function () {

        if (this._culture) {
            this._binder.set_culture(this._culture);
            this._binder.set_uiCulture(this._culture);
        }
        if (this._selection.length == this._selectedIds.length) {
            this._bindCurrentSelection();
        }
        else { //ids are set, but items are not
            var filter = this._buildFilter(this._selectedIds);
            var urlParams = this._binder.get_urlParams();
            urlParams["filter"] = filter;
            urlParams["provider"] = this._providerName;
            this._binder.set_urlParams(urlParams);
            this._binder.DataBind();
        }
    },
    /* --------------------------------- event handlers ---------------------------------- */
    onLoad: function () {
        this._selector.add_binderDataBound(this._binderDataBoundDelegate);
        var selectedItems = this._selector.get_selectedItems();
        if (!selectedItems || selectedItems.length == 0) {
            this._setSelectLabelHtml();
        } else {
            this._setChangeLabelHtml();
        }
    },
    doneClicked: function () {
        this._handleSelectionDone();
        this.refreshUI();
    },

    _onBinderDataBound: function () {
        dialogBase.resizeToContent();
    },

    _handleSelectionDone: function (sender, args) {
        var selectedItems = this._selector.get_selectedItems();
        this._selection = [];
        this._selectedIds = [];
        if (selectedItems != null && selectedItems.length > 0) {
            this._setChangeLabelHtml();

            for (i = 0; i < selectedItems.length; i++) {
                this._selection.push(selectedItems[i]);
                this._selectedIds.push(selectedItems[i].Id);
            }
        } else {
            this._setSelectLabelHtml();
        }
    },

    get_selectedItemsTitles: function () {
        var titles = [];
        var items = this.get_selectedItems();
        if (items && items.length > 0) {
            for (var i = 0, l = items.length; i < l; i++) {
                if (items[i].Title)
                    titles.push(items[i].Title);
            }
        }
        return titles;
    },
    clearSelection: function () {
        this._selection = [];
        this._selectedIds = [];
    },
    get_selectedItems: function () {
        if (this._selection.length == 0 || this._allowMultipleSelection)
            return this._selection;
        else
            return [this._selection[0]];
    },
    set_selectedItems: function (value) {
        this._selection = [];
        this._selectedIds = [];
        if (value) {
            if (this._allowMultipleSelection) {
                this._selection = this._selection.concat(value);
                for (var i = 0, l = this._selection.length; i < l; i++) {
                    this._selectedIds.push(this._selection[i].Id);
                }
            }
            else {
                this._selection.push(value[0]);
                this._selectedIds.push(value[0].Id);
            }
        }
        this.refreshUI();
    },
    get_selectedValues: function () {
        if (this._selectedIds.length == 0 || this._allowMultipleSelection)
            return this._selectedIds;
        else
            return [this._selectedIds[0]];
    },
    set_selectedValues: function (value) {
        if (!value) {
            this._selectedIds = [];
        }
        else {
            if (this._allowMultipleSelection)
                this._selectedIds = value;
            else
                this._selectedIds = [value[0]];
        }
        this.refreshUI();
    },

    // clears the selected items and rebinds with empty collection when provider's changed
    clearSelectedItems: function () {
        this.clearSelection();
        this._setSelectorSelectedIds();
        this._bindCurrentSelection();
        this._setSelectLabelHtml();
    },

    /* --------------------------------- private methods --------------------------------- */

    _buildFilter: function (ids) {
        if (!ids)
            return "";
        var filters = [];
        for (var i = 0; i < ids.length; i++) {
            filters.push(String.format("Id==({0})", ids[i]));
        }
        return filters.join(" OR ");
    },

    _binderDataBound: function (sender, args) {
        this._setSelectorSelectedIds();
        this._handleSelectionDone();
    },

    _bindCurrentSelection: function () {
        var data = { 'Items': this._selection };
        this._binder.BindCollection(data);
    },

    _setSelectorSelectedIds: function () {
        this._selector.set_selectedKeys(this._selectedIds);
        this._selector.selectItemsInternal(this._selectedIds);
    },

    _setSelectLabelHtml: function () {
        jQuery("#" + this._chooseButtonTextControlId).html(this._selectLabel);
    },

    _setChangeLabelHtml: function () {
        jQuery("#" + this._chooseButtonTextControlId).html(this._changeLabel);
    },

    /* --------------------------------- properties -------------------------------------- */
    // Gets or sets the reference to selector component.
    get_selector: function () {
        return this._selector;
    },
    set_selector: function (value) {
        this._selector = value;
    },
    get_providerName: function () {
        return this._providerName;
    },
    set_providerName: function (value) {
        this._providerName = value;

        //pass the provider to the selector
        this._selector.set_providerName(value);
        this._binder.set_provider(value);
    }
}

Telerik.Sitefinity.Web.UI.FlatSelectorResultView.registerClass('Telerik.Sitefinity.Web.UI.FlatSelectorResultView', Telerik.Sitefinity.Web.UI.SelectorResultView);