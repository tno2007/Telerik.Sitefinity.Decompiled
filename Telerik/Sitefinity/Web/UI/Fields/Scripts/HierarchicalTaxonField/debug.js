Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField = function (element) {
    this._element = element;
    this._changeSelectedTaxaButton = null;
    this._createTaxonButton = null;
    this._selectedTaxaPanel = null;
    this._taxaSelector = null;

    this._loadDelegate = null;
    this._selectionDoneDelegate = null;
    this._changeSelectedTaxaClickDelegate = null;
    this._predecessorWebServiceUrl = null;

    this._taxonRemovedDelegate = null;
    this._binderBoundDelegate = null;
    this._selectedFrontEndItemIds = null;

    this._isInEditMode = false;

    Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.prototype =
 {
     initialize: function () {
         if (this._loadDelegate == null) {
             this._loadDelegate = Function.createDelegate(this, this._load)
         }
         Sys.Application.add_load(this._loadDelegate);

         if (this._changeSelectedTaxaClickDelegate == null) {
             this._changeSelectedTaxaClickDelegate = Function.createDelegate(this, this._changeSelectedTaxaClicked);
         }
         if (this._changeSelectedTaxaButton) {
             $addHandler(this._changeSelectedTaxaButton, 'click', this._changeSelectedTaxaClickDelegate);
         }

         if (this._selectionDoneDelegate == null) {
             this._selectionDoneDelegate = Function.createDelegate(this, this._handleSelectionDone);
         }

         if (this._taxaSelector) {
             this._taxaSelector.add_selectionDone(this._selectionDoneDelegate);
             this._taxaSelector.set_allowMultipleSelection(this.get_allowMultipleSelection());
         }

         if (this._taxonRemovedDelegate == null) {
             this._taxonRemovedDelegate = Function.createDelegate(this, this._taxonRemoved);
         }
         this.add_taxonRemoved(this._taxonRemovedDelegate);

         Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.callBaseMethod(this, "initialize");

         if (this._binderBoundDelegate == null) {
             this._binderBoundDelegate = Function.createDelegate(this, this._binderBound);
         }
     },

     dispose: function () {
         if (this._loadDelegate != null) {
             delete this._loadDelegate;
         }

         if (this._changeSelectedTaxaClickDelegate != null) {
             delete this._changeSelectedTaxaClickDelegate;
         }

         if (this._taxaSelector && this._selectionDoneDelegate != null) {
             this._taxaSelector.remove_selectionDone(this._selectionDoneDelegate);
             delete this._selectionDoneDelegate;
         }

         if (this._taxonRemovedDelegate) {
             this.remove_taxonRemoved(this._taxonRemovedDelegate);
             delete this._taxonRemovedDelegate;
         }

         Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     // Takes the array of taxon ids and returns client side objects with the desired taxon title (could be a full path, not necesarrily just
     // the title of the taxon) and id.
     loadTaxa: function (taxonIds) {
         if (taxonIds && taxonIds.length && this._taxaSelector) {

             this._hideSelector();

             var serviceUrl = this.get_webServiceUrl();
             var urlParams = [];
             urlParams['provider'] = this.get_taxonomyProvider();
             var keys = [];

             this._clientManager.InvokePut(serviceUrl + "batchpath/", urlParams, keys, taxonIds, this._loadTaxaSuccess, this._loadTaxaFailure, this);
         }
     },

     //TODO: this code needs review
     reset: function () {
         Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.callBaseMethod(this, "reset");
         this._taxaSelector.set_uiCulture(null);
         this._clientManager.set_uiCulture(null);
         this.clearSelection();
     },

     clearSelection: function () {
        this._taxaSelector.clearSelection();
        if (this._displayMode == Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.Read) {
            $(this._selectedTaxaPanel).show();
            $(this._selectedTaxaPanel).parent().show();
            $(this._taxaSelector.get_element()).hide();
        } else {
            $(this._selectedTaxaPanel).hide();
            $(this._taxaSelector.get_element()).show();
        }
     },

     _loadTaxaSuccess: function (caller, result) {
         var taxaPaths = result.Items;
         taxaPaths.sort(function (a, b) {
                 var itemA = a[a.length - 1].Name;
                 var itemB = b[b.length - 1].Name;

                 if (itemA < itemB)
                     return -1;
                 if (itemA > itemB)
                     return 1;
                 return 0;
         });

         var taxaPathsLength = taxaPaths.length;
         for(var t= 0; t<taxaPathsLength; t++)
        {
             var taxa = taxaPaths[t];
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

     // Removes the taxon from the list of selected taxons and unchecks it from the tree.
     removeTaxon: function (taxonTitle, taxonId) {
         if (this.get_allowMultipleSelection()) {
             this._node_set_checked(taxonId, false);
         }
         Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.callBaseMethod(this, "removeTaxon", [taxonTitle, taxonId]);
     },

     focus: function () {
         var button = this._taxaSelector.get_createTaxonButton();
         if (jQuery(button).is(":visible")) {
             button.focus();
         }
         var value = this.get_value();
         if (value && value.length == 0) {
             this._showSelector();
         }
         else {
             this._hideSelector();
         }
     },

     blur: function () {
         var behavior = this._get_ExpandableExtenderBehavior();
         var value = this.get_value();
         if (value && value.length == 0) {
             this._showSelector();
             behavior.reset();
         }
         else {
             this._hideSelector();
         }
     },

     /* -------------------- events --------------------- */

     /* -------------------- event handlers ------------- */

     // Handles the application load event.
     _load: function () {
         if (!this._bindOnServer) {
             this.get_taxaSelector().get_taxaTreeBinder().add_onDataBound(this._binderBoundDelegate);
         }
     },

     // Handles the click event of the change selected taxa button.
     _changeSelectedTaxaClicked: function () {
         this._showSelector();
     },

     _showSelector: function () {
         this._isInEditMode = true;

         $(this._selectedTaxaPanel).hide();
         this._taxaSelector.chnageVisibility(true);         
         $(this._taxaSelector.get_element()).show();
     },

     _hideSelector: function () {
         this._isInEditMode = false;

         $(this._taxaSelector.get_element()).hide();
         this._taxaSelector.chnageVisibility(false);
         $(this._selectedTaxaPanel).show();
     },


     _binderBound: function (sender, args) {
         if (this._selectedFrontEndItemIds) {
             this.get_taxaSelector().get_taxaTreeBinder().setSelectedValues(this._selectedFrontEndItemIds, true, false);
         }
         else if (this._getSelectedTaxaIds()) {
             this.get_taxaSelector().get_taxaTreeBinder().setSelectedValues(this._getSelectedTaxaIds(), true, false);
         }
     },

     // Handles the selection done event of the taxa selector.
     _handleSelectionDone: function (sender, args) {
         var emptyGuid = this._clientManager.GetEmptyGuid();
         var selectedItems = args.selectedTaxa;
         //TODO clear this mess
         if (this.get_allowMultipleSelection()) {
             if (args.isToAdd) {
                 for (var i = 0; i < selectedItems.length; i++) {
                     var selectedTaxonId = selectedItems[i];
                     this._node_set_checked(selectedTaxonId, true);
                 }
             }
         }
         if (selectedItems != null && selectedItems != emptyGuid && selectedItems.length > 0) {
             for (i = 0; i < selectedItems.length; i++) {
                 var selectedTaxonId = selectedItems[i];
                 this._getTaxonPath(selectedTaxonId);
             }
         }

         this._hideSelector();
     },

     // Gets the full path of the taxon by it's id and adds the taxon to the selected taxa.
     _getTaxonPath: function (taxonId) {
         var serviceUrl = this._predecessorWebServiceUrl;
         var urlParams = [];
         urlParams['providerName'] = this._taxonomyProvider;
         urlParams['onlyPath'] = 'true';
         var keys = [taxonId];
         this._clientManager.InvokeGet(serviceUrl, urlParams, keys, this._getPathSuccess, this._getPathFailure, this);
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
     },

     // Called when the request to get the path from the taxon to the root fails.
     _getPathFailure: function (result) {
         alert(result.Detail);
     },

     _taxonRemoved: function (sender, args) {
         var value = this.get_value();
         if (value && value.length == 0) {
             this._isInEditMode = true;
             $(this._taxaSelector.get_element()).show();
             $(this._selectedTaxaPanel).hide();
         }
     },


     /* -------------------- private methods ----------- */
     _get_ExpandableExtenderBehavior: function () {
         if (this._expandableExtenderBehavior) {
             return this._expandableExtenderBehavior;
         }
         this._expandableExtenderBehavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
         return this._expandableExtenderBehavior;
     },

     _node_set_checked: function (nodeId, checked) {
         var treeView = this._taxaSelector.get_taxaTreeBinder().get_target().control;
         var nodeToUncheck = treeView.findNodeByValue(nodeId);
         if (nodeToUncheck)
             nodeToUncheck.set_checked(checked);
     },

     _rebindTaxaSelectorInSpecificCulture: function (culture) {
         this._taxaSelector.set_uiCulture(culture);
         this._taxaSelector.dataBind();
     },

     // Gets the value of the taxon field.
     get_value: function () {
         if (this._isInEditMode) {
             return this._taxaSelector.get_selectedTaxa();
         } else {
             return this._getSelectedTaxaIds();
         }
     },

     /* -------------------- properties ---------------- */

     // Gets the reference to the button/element which is used to change the selected taxa.
     get_changeSelectedTaxaButton: function () {
         return this._changeSelectedTaxaButton;
     },
     // Sets the reference to the button/element which is used to change the selected taxa.
     set_changeSelectedTaxaButton: function (value) {
         this._changeSelectedTaxaButton = value;
     },
     // Gets the reference to the button/element which is used to open the dialog for creating a new taxon.
     get_createTaxonButton: function () {
         return this._createTaxonButton;
     },
     // Sets the reference to the button/element which is used to open the dialog for creating a new taxon.
     set_createTaxonButton: function (value) {
         this._createTaxonButton = value;
     },
     // Gets the reference to the element (container) which is displayed when user has selected a taxa.
     get_selectedTaxaPanel: function () {
         return this._selectedTaxaPanel;
     },
     // Sets the reference to the element (container) which is displayed when user has selected a taxa.
     set_selectedTaxaPanel: function (value) {
         this._selectedTaxaPanel = value;
     },
     // Gets the reference to hierarchical selector component used to select hierarchical taxa.
     get_taxaSelector: function () {
         return this._taxaSelector;
     },
     // Sets the reference to hierarchical selector component used to select hierarchical taxa.
     set_taxaSelector: function (value) {
         this._taxaSelector = value;
     },

     get_selectedFrontEndItemIds: function () {
         return this._selectedFrontEndItemIds;
     },

     set_selectedFrontEndItemIds: function (value) {
         this._selectedFrontEndItemIds = value;
     },

     // Sets the UI culture to use when visualizing a content.
     set_uiCulture: function (culture) {
         if (culture && this._clientManager.get_uiCulture() !== culture) {
             Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.callBaseMethod(this, "set_uiCulture", [culture]);
             this._rebindTaxaSelectorInSpecificCulture(culture);
         }
     }
 };

Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField.registerClass("Telerik.Sitefinity.Web.UI.Fields.HierarchicalTaxonField", Telerik.Sitefinity.Web.UI.Fields.TaxonField);