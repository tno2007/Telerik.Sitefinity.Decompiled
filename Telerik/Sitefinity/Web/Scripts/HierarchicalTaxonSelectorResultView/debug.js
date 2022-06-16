Type.registerNamespace("Telerik.Sitefinity.Web.UI");
Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView = function (element) {
    this._clientManager = null;
    this._taxaSelector = null;
    this._predecessorWebServiceUrl = null;
    this._chooseButtonTextControlId = null;
    this._selectLabel = null;
    this._changeLabel = null;
    this._binderDataBoundDelegate = null;
    this._selectedTaxaBinderItemCommandDelegate = null;

    Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView.initializeBase(this, [element]);
}
Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView.callBaseMethod(this, "initialize");

        this._clientManager = new Telerik.Sitefinity.Data.ClientManager();

        if (this._binderItemDataBoundDelegate == null)
            this._binderItemDataBoundDelegate = Function.createDelegate(this, this._binderItemDataBound);

        if (this._selectedTaxaBinderItemCommandDelegate == null) {
            this._selectedTaxaBinderItemCommandDelegate = Function.createDelegate(this, this._selectedTaxaBinderItemCommand);
        }

        this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        this._binder.add_onDataBound(this._binderDataBoundDelegate);
        this._binder.add_onItemCommand(this._selectedTaxaBinderItemCommandDelegate);

        this._selector = this._taxaSelector;
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView.callBaseMethod(this, "dispose");

        if (this._binderItemDataBoundDelegate != null) {
            this._taxaSelector.get_treeBinder().remove_onItemDataBound(this._binderItemDataBoundDelegate);
            delete this._binderItemDataBoundDelegate;
        }

        if (this._binderDataBoundDelegate) {
            this._binder.remove_onDataBound(this._binderDataBoundDelegate);
            delete this._binderDataBoundDelegate;
        }
    },
    /* --------------------------------- public methods ---------------------------------- */
    refreshUI: function () {
        this.loadTaxa(this._selectedIds);
    },
    // Takes the array of taxon ids and returns client side objects with the desired taxon title (could be a full path, not necesarrily just
    // the title of the taxon) and id.
    loadTaxa: function (taxonIds) {
        if (!taxonIds || taxonIds.length == 0) {
            jQuery("#" + this._chooseButtonTextControlId).html(this._selectLabel);
            return;
        } else {
            jQuery("#" + this._chooseButtonTextControlId).html(this._changeLabel);
        }

        var serviceUrl = this.get_webServiceUrl();
        var urlParams = [];
        urlParams['provider'] = this.get_taxonomyProvider();
        var keys = [];

        if (this._culture) {
            this._clientManager.set_uiCulture(this._culture);
        }
        this._clientManager.InvokePut(serviceUrl + "batchpath/", urlParams, keys, taxonIds, this._loadTaxaSuccess, this._loadTaxaFailure, this);
    },

    // Adds the taxon to the list of selected taxons.
    addTaxon: function (taxon) {
        if (this._taxonAlreadyAdded(taxon.Id)) {
            return;
        }

        if (!this._allowMultipleSelection) {
            this._selectedTaxa = [];
            this._selectedIds = [];
        }
        this._selectedTaxa.push(taxon);
        if (jQuery.inArray(taxon.Id, this._selectedIds) == -1)
            this._selectedIds.push(taxon.Id);
        var data = { 'Items': this._selectedTaxa };
        this._binder.BindCollection(data);
    },

    // Removes the taxon from the list of selected taxons.
    removeTaxon: function (taxonTitle, taxonId) {
        // Removes the selection from the tree
        this._node_set_checked(taxonId, false);

        var selectedTaxaLength = this._selectedTaxa.length;
        for (var i = 0; i < selectedTaxaLength; i++) {
            if (this._selectedTaxa[i].Title == taxonTitle && this._selectedTaxa[i].Id == taxonId) {
                this._selectedTaxa.splice(i, 1);
                break;
            }
        }
        var k = jQuery.inArray(taxonId, this._selectedIds);
        if (k > -1)
            this._selectedIds.splice(k, 1);
        var data = { 'Items': this._selectedTaxa };
        this._binder.BindCollection(data);
    },
    /* --------------------------------- event handlers ---------------------------------- */
    onLoad: function () {
        this._taxaSelector.get_treeBinder().add_onItemDataBound(this._binderItemDataBoundDelegate);
        this._taxaSelector.get_treeBinder().set_enableInitialExpanding(false);
    },
    doneClicked: function () {
        this._handleSelectionDone();
    },
    _handleSelectionDone: function (sender, args) {
        var selectedItems = this._taxaSelector.get_selectedItems();
        //this._selectedTaxa = [];
        //this._selectedIds = [];
        if (selectedItems != null && selectedItems.length > 0) {
            jQuery("#" + this._chooseButtonTextControlId).html(this._changeLabel);

            for (var i = 0, l = selectedItems.length; i < l; i++) {
                var selectedTaxonId = selectedItems[i].Id;
                this._getTaxonPath(selectedTaxonId);
            }
        } else {
            jQuery("#" + this._chooseButtonTextControlId).html(this._selectLabel);
        }
        var data = { 'Items': this._selectedTaxa };
        this._binder.BindCollection(data);
    },
    _binderItemDataBound: function (sender, args) {
        var id = args.get_dataItem().Id;
        if (jQuery.inArray(id, this._selectedIds) > -1)
            this._taxaSelector.get_treeBinder().selectByIds([id]);
    },
    _selectedTaxaBinderItemCommand: function (sender, eventArgs) {
        var dataItem = eventArgs.get_dataItem();
        if (eventArgs.get_commandName() == 'remove') {
            this.removeTaxon(dataItem.Title, dataItem.Id);
        }
    },
    _binderDataBound: function (sender, args) {
        if (jQuery("body").hasClass("sfSelectorDialog"))
            dialogBase.resizeToContent();
    },

    add_selectionDone: function (delegate) {
        this.get_events().addHandler("selectionDone", delegate);
    },

    remove_selectionDone: function (delegate) {
        this.get_events().removeHandler("selectionDone", delegate);
    },

    _raiseSelectionDone: function () {
        var handler = this.get_events().getHandler("selectionDone");
        if (handler) handler(this);
    },

    /* --------------------------------- private methods --------------------------------- */
    _loadTaxaSuccess: function (caller, result) {
        var taxaPaths = result.Items;
        var taxaPathsLength = taxaPaths.length;
        while (taxaPathsLength--) {
            var taxa = taxaPaths[taxaPathsLength];
            var taxaLength = taxa.length;
            var taxonToAdd = null;
            var delimiter = ' > ';
            var taxonPathTitle = '';
            for (var i = 0; i < taxaLength; i++) {
                if (i == taxaLength - 1) {
                    delimiter = '';
                    taxonToAdd = taxa[i];
                }
                taxonPathTitle += taxa[i].Title + delimiter;
            }
            taxonToAdd.Title = taxonPathTitle;
            caller.addTaxon(taxonToAdd);
        }
    },

    _loadTaxaFailure: function (result) {
        alert(result.Detail);
    },
    // Gets the full path of the taxon by it's id and adds the taxon to the selected taxa.
    _getTaxonPath: function (taxonId) {
        var serviceUrl = this._webServiceUrl + this._predecessorWebServiceUrl;
        var urlParams = [];
        urlParams['providerName'] = this._taxonomyProvider;
        urlParams['onlyPath'] = 'true';
        var keys = [taxonId];
        if (this._culture) {
            this._clientManager.set_uiCulture(this._culture);
        }
        this._clientManager.InvokeGet(serviceUrl, urlParams, keys, this._getPathSuccess, this._getPathFailure, this);
    },
    _getSelectedTaxaIds: function () {
        if (this._selectedIds) {
            return this._getSelectedIds(this._selectedIds);
        }

        this._selectedIds = [];
        var selectedTaxaCount = this._selectedTaxa.length;
        while (selectedTaxaCount--) {
            var id = this._selectedTaxa[selectedTaxaCount].Id;
            this._selectedIds.push(id);
        }

        return this._getSelectedIds(this._selectedIds);
    },

    _getSelectedIds: function (selectedIds) {
        if (this._allowMultipleSelection) {
            return selectedIds;
        } else if (selectedIds.length > 0) {
            return selectedIds[0];
        }
        return null;
    },

    _taxonAlreadyAdded: function (taxonId) {
        var selectedTaxaCount = this._selectedTaxa.length;
        while (selectedTaxaCount--) {
            if (this._selectedTaxa[selectedTaxaCount].Id == taxonId) {
                return true;
            }
        }
        return false;
    },

    // Called when the request to get the path from the taxon to the root fails.
    _getPathSuccess: function (caller, result) {
        var taxa = result.Items;
        var taxonToAdd = null;
        var taxonPathTitle = '';
        var taxaLength = taxa.length;
        if (taxaLength > 0) {
            var taxonId = taxa[taxaLength - 1].Id;
            var delimiter = ' > ';
            for (var i = 0, l = taxa.length; i < l; i++) {
                if (i == taxa.length - 1) {
                    delimiter = '';
                    taxonToAdd = taxa[i];
                }
                taxonPathTitle += taxa[i].Title + delimiter;
            }
            if (taxonId !== taxonToAdd.Id) {
                throw 'unexpected end of the taxon path.';
            }
            taxonToAdd.Title = taxonPathTitle;
            caller.addTaxon(taxonToAdd);
        }
        else {
            throw "Getting the taxon path returned an empty collection.";
        }

        caller._raiseSelectionDone();
    },

    _node_set_checked: function (nodeId, checked) {
        var treeView = this.get_taxaSelector().get_treeBinder().get_target().control;
        var nodeToUncheck = treeView.findNodeByValue(nodeId);
        if (nodeToUncheck)
            nodeToUncheck.set_checked(checked);
    },

    // Called when the request to get the path from the taxon to the root fails.
    _getPathFailure: function (result) {
        alert(result.Detail);
    },
    /* --------------------------------- properties -------------------------------------- */
    // Gets or sets the reference to hierarchical selector component used to select hierarchical taxa.
    get_taxaSelector: function () {
        return this._taxaSelector;
    },
    set_taxaSelector: function (value) {
        this._taxaSelector = value;
    },
    get_predecessorWebServiceUrl: function () {
        return this._predecessorWebServiceUrl;
    },
    set_predecessorWebServiceUrl: function (value) {
        this._predecessorWebServiceUrl = value;
    }
}

Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView.registerClass('Telerik.Sitefinity.Web.UI.HierarchicalTaxonSelectorResultView', Telerik.Sitefinity.Web.UI.TaxonSelectorResultView);