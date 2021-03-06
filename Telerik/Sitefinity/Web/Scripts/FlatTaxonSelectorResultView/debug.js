Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView = function (element) {
    this._taxaSelector = null;
    this._chooseButtonTextControlId = null;
    this._selectLabel = null;
    this._changeLabel = null;
    this._oldTaxa = null; // the taxa that are set with set_selectedValues and retrieved from the result view (the binder)
    this._selectorOpened = false; // true if the flat taxa selector has been opened and then closed with "Done" button

    this._binderDataBoundDelegate = null;
    this._binderOnDataBoundDelegate = null;

    this._rowSelectedDelegate = null;
    this._rowDeselectedDelegate = null;
    Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.callBaseMethod(this, "initialize");

        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        this._taxaSelector.add_binderDataBound(this._binderDataBoundDelegate);

        this._binderOnDataBoundDelegate = Function.createDelegate(this, this._binderOnDataBound);
        this._binder.add_onDataBound(this._binderOnDataBoundDelegate);

        this._selector = this._taxaSelector;

        this._rowSelectedDelegate = Function.createDelegate(this, this._rowSelected);
        this._rowDeselectedDelegate = Function.createDelegate(this, this._rowDeselected);
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.callBaseMethod(this, "dispose");

        if (this._binderDataBoundDelegate) {
            if (this._taxaSelector) {
                this._taxaSelector.remove_binderDataBound(this._binderDataBoundDelegate);
            }
            delete this._binderDataBoundDelegate;
        }

        if (this._binderOnDataBoundDelegate) {
            if (this._binder) {
                this._binder.remove_onDataBound(this._binderOnDataBoundDelegate);
            }
            delete this._binderOnDataBoundDelegate;
        }

        if (this._rowSelectedDelegate) {
            if (this._taxaSelector && this._taxaSelector._grid) {
                this._taxaSelector._grid.remove_rowSelected(this._rowSelectedDelegate);
            }
            delete this._rowSelectedDelegate;
        }
        if (this._rowDeselectedDelegate) {
            if (this._taxaSelector && this._taxaSelector._grid) {
                this._taxaSelector._grid.remove_rowDeselected(this._rowDeselectedDelegate);
            }
            delete this._rowDeselectedDelegate;
        }
    },
    /* --------------------------------- public methods ---------------------------------- */
    refreshUI: function () {

        if (this._culture) {
            this._binder.set_culture(this._culture);
            this._binder.set_uiCulture(this._culture);
        }
        if (this._selectedTaxa.length == this._selectedIds.length) {
            var data = { 'Items': this._selectedTaxa };
            this._binder.BindCollection(data);
        }
        else { // taxa ids are set, but taxa items are not
            var filter = this._buildFilter(this._selectedIds);
            var urlParams = this._binder.get_urlParams();
            urlParams["filter"] = filter;
            this._binder.set_urlParams(urlParams);
            this._binder.DataBind();
        }
    },
    // Gets or sets the selected taxa
    get_selectedItems: function () {
        // if the taxa selector hasn't been opened then return the old selected taxa
        if (!this._selectorOpened) {
            return this._oldTaxa;
        }
        return Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.callBaseMethod(this, "get_selectedItems");
    },
    /* --------------------------------- event handlers ---------------------------------- */
    onLoad: function () {
        this._handleSelectionDone();

        this._taxaSelector._grid.add_rowSelected(this._rowSelectedDelegate);
        this._taxaSelector._grid.add_rowDeselected(this._rowDeselectedDelegate);
    },
    doneClicked: function () {
        this._selectorOpened = true;
        this._handleSelectionDone();
        this.refreshUI();
    },
    openSelectorCommand: function () {
        Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.callBaseMethod(this, "openSelectorCommand");
        this._selectorOpened = false;
    },
    _handleSelectionDone: function (sender, args) {
        if (this._selectedIds.length > 0) {
            jQuery("#" + this._chooseButtonTextControlId).html(this._changeLabel);
        } else {
            jQuery("#" + this._chooseButtonTextControlId).html(this._selectLabel);
        }
    },

    _rowSelected: function (sender, args) {
        var e = args.get_domEvent();
        var target = e.target ? e.target : e.srcElement;
        if (target) {
            var dataItem = args.get_gridDataItem().get_dataItem();
            if (this._selectedIds.indexOf(dataItem.Id) == -1) {
                this._selectedIds.push(dataItem.Id);
                this._selectedTaxa.push(dataItem);
            }
        }
    },

    _rowDeselected: function (sender, args) {
        var e = args.get_domEvent();
        var target = e.target ? e.target : e.srcElement;
        if (target) {
            var dataItem = args.get_gridDataItem().get_dataItem();
            var idx = this._selectedIds.indexOf(dataItem.Id);
            if (idx > -1) {
                this._selectedIds.splice(idx, 1);
                this._selectedTaxa.splice(idx, 1);
            }
        }
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
        this._taxaSelector.set_selectedKeys(this._selectedIds);
        this._taxaSelector.selectItemsInternal(this._selectedIds);
    },
    _binderOnDataBound: function (sender, args) {
        if (jQuery("body").hasClass("sfSelectorDialog"))
            dialogBase.resizeToContent();
        this._oldTaxa = args.get_dataItem().Items;

        //added .slice(0) to make a copy of the array. Otherwise _oldTaxa and _selectedTaxa reference the same array.
        this._selectedTaxa = args.get_dataItem().Items.slice(0);
    },

    /* --------------------------------- properties -------------------------------------- */
    // Gets or sets the reference to hierarchical selector component used to select hierarchical taxa.
    get_taxaSelector: function () {
        return this._taxaSelector;
    },
    set_taxaSelector: function (value) {
        this._taxaSelector = value;
    }
}

Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView.registerClass('Telerik.Sitefinity.Web.UI.FlatTaxonSelectorResultView', Telerik.Sitefinity.Web.UI.TaxonSelectorResultView);