Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.TaxonField = function (element) {
    this._element = element;
    this._webServiceUrl = null;
    this._taxonomyId = null;
    this._taxonomyProvider = null;
    this._bindOnServer = false;
    this._allowCreating = null;
    this._allowMultipleSelection = null;
    this._initialTaxaCount = 0;
    this._selectedTaxa = [];
    this._selectedIds = [];
    this._selectedTaxaBinder = null;
    this._originalValue = [];
    this._clientManager = null;
    this._enabled = null;

    this._removeSelectedTaxonDelegate = null;
    this._selectedTaxaBinderItemCommandDelegate = null;

    Telerik.Sitefinity.Web.UI.Fields.TaxonField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.TaxonField.prototype =
 {
     initialize: function () {
         this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
         if (this._removeSelectedTaxonDelegate == null && typeof this._removeSelectedTaxonClicked === 'function') {
             this._removeSelectedTaxonDelegate = Function.createDelegate(this, this._removeSelectedTaxonClicked);
         }

         if (this._selectedTaxaBinderItemCommandDelegate == null) {
             this._selectedTaxaBinderItemCommandDelegate = Function.createDelegate(this, this._selectedTaxaBinderItemCommand);
         }
         this._selectedTaxaBinder.add_onItemCommand(this._selectedTaxaBinderItemCommandDelegate);

         Telerik.Sitefinity.Web.UI.Fields.TaxonField.callBaseMethod(this, "initialize");
     },

     dispose: function () {
         this._webServiceUrl = null;
         this._taxonomyId = null;
         this._taxonomyProvider = null;
         this._allowMultipleSelection = null;

         if (this._removeSelectedTaxonDelegate != null) {
             delete this._removeSelectedTaxonDelegate;
         }

         if (this._selectedTaxaBinderItemCommandDelegate != null) {
             this._selectedTaxaBinder.remove_onItemCommand(this._selectedTaxaBinderItemCommandDelegate);
             delete this._selectedTaxaBinderItemCommandDelegate;
         }

         Telerik.Sitefinity.Web.UI.Fields.TaxonField.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     // This function allows other objects to subscribe to the taxonAdded event of the taxon field.
     add_taxonAdded: function (delegate) {
         this.get_events().addHandler('taxonAdded', delegate);
     },

     // This function allows other objects to unsubscribe from the taxonAdded event of the taxon field.
     remove_taxonAdded: function (delegate) {
         this.get_events().removeHandler('taxonAdded', delegate);
     },

     // This function allows other objects to subscribe to the taxonRemoved event of the taxon field.
     add_taxonRemoved: function (delegate) {
         this.get_events().addHandler('taxonRemoved', delegate);
     },

     // This function allows other objects to unsubscribe from the taxonRemoved event of the taxon field.
     remove_taxonRemoved: function (delegate) {
         this.get_events().removeHandler('taxonRemoved', delegate);
     },

     // Adds the taxon to the list of selected taxons. If dontBind is true then the field is not bound with the new taxon.
     addTaxon: function (taxon, dontBind) {
         // TODO: write unit tests
         var alreadyAddedTaxon = this._getTaxon(taxon.Id);
         if (alreadyAddedTaxon) {
             alreadyAddedTaxon.Title = taxon.Title;
         }
         else {
             this._selectedTaxa.push(taxon);
         }

         this._selectedIds = null;

         if (!this._allowMultipleSelection) {
             this._selectedTaxa = [];
             this._selectedTaxa.push(taxon);
         }
         if (dontBind != true) {
             var data = { 'Items': this._selectedTaxa };
             this._selectedTaxaBinder.BindCollection(data);
         }
         this._taxonAddedHandler(taxon.Title, taxon.Id);
     },

     // Removes the taxon from the list of selected taxons.
     removeTaxon: function (taxonTitle, taxonId) {
         // TODO: write unit tests
         this._selectedIds = null;
         var selectedTaxaLength = this._selectedTaxa.length;
         for (var i = 0; i < selectedTaxaLength; i++) {
             if (this._selectedTaxa[i].Title == taxonTitle && this._selectedTaxa[i].Id == taxonId) {
                 this._selectedTaxa.splice(i, 1);
                 break;
             }
         }
         var data = { 'Items': this._selectedTaxa };
         this._selectedTaxaBinder.BindCollection(data);
         this._taxonRemovedHandler(taxonTitle, taxonId);
     },

     // Takes the array of taxon ids and populates the concrete implementation of taxon field with taxa being represented by those ids.
     loadTaxa: function (taxonIds) {
         alert('This method nees to be implemented on the concrete implementation of the taxon field!');
     },

     reset: function () {
         for (var i = 0; i < this._selectedTaxa.length; i++) {
            this.removeTaxon(this._selectedTaxa[i].Title, this._selectedTaxa[i].Id);
         }
         this._selectedIds = [];
         this._selectedTaxa = [];
         Telerik.Sitefinity.Web.UI.Fields.TaxonField.callBaseMethod(this, "reset");
     },

     /* -------------------- events -------------------- */

     // This function will rise taxonAdded event.
     _taxonAddedHandler: function (taxonTitle, taxonId) {
         // TODO: write unit tests
         var eventArgs = { 'Title': taxonTitle, 'Id': taxonId };
         var h = this.get_events().getHandler('taxonAdded');
         if (h) h(this, eventArgs);
         return eventArgs;
     },

     // This function will rise taxonRemoved event.
     _taxonRemovedHandler: function (taxonTitle, taxonId) {
         // TODO: write unit tests
         var eventArgs = { 'Title': taxonTitle, 'Id': taxonId };
         var h = this.get_events().getHandler('taxonRemoved');
         if (h) h(this, eventArgs);
         return eventArgs;
     },

     /* -------------------- event handlers ------------ */

     _selectedTaxaBinderItemCommand: function (sender, eventArgs) {
         if (this._enabled) {
             var dataItem = eventArgs.get_dataItem();
             if (eventArgs.get_commandName() == 'remove') {
                 this.removeTaxon(dataItem.Title, dataItem.Id);
             }
         }
     },

     /* -------------------- private methods ----------- */

     // converts the array of selected taxa (Title, Id) to an array of ids
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
             return [selectedIds[0]];
         }
         return null;
     },

     _getTaxon: function (taxonId) {
         var selectedTaxaCount = this._selectedTaxa.length;
         while (selectedTaxaCount--) {
             if (this._selectedTaxa[selectedTaxaCount].Id === taxonId) {
                 return this._selectedTaxa[selectedTaxaCount];
             }
         }
         return null;
     },

     /* -------------------- properties ---------------- */

     // Gets the url of the webservice that is used to manage taxonomies asynchronously.
     get_webServiceUrl: function () {
         return this._webServiceUrl;
     },
     // Sets the url of the webservice that is used to manage taxonomies asynchronously.
     set_webServiceUrl: function (value) {
         this._webServiceUrl = value;
     },
     // Gets the id of the taxonomy associated with this taxon field.
     get_taxonomyId: function () {
         return this._taxonomyId;
     },
     // Sets the id of the taxonomy associated with this taxon field.     
     set_taxonomyId: function (value) {
         this._taxonomyId = value;
     },
     // Gets the name of the taxonomy provider associated with this taxon field.
     get_taxonomyProvider: function () {
         return this._taxonomyProvider;
     },
     // Sets the name of the taxonomy provider associated with this taxon field.
     set_taxonomyProvider: function (value) {
         this._taxonomyProvider = value;
     },
     get_bindOnServer: function () {
         return this._bindOnServer;
     },
     set_bindOnServer: function (value) {
         this._bindOnServer = value;
     },
     // Gets the value indicating whether in write mode, more than one taxon can be selected. 
     // If multiple taxons can be selected true; otherwise false.
     get_allowMultipleSelection: function () {
         return this._allowMultipleSelection;
     },
     // Sets the value indicating whether in write mode, more than one taxon can be selected.
     // If multiple taxons can be selected true; otherwise false.
     set_allowMultipleSelection: function (value) {
         this._allowMultipleSelection = value;
     },
     get_allowCreating: function () {
         return this._allowCreating;
     },
     set_allowCreating: function (value) {
         this._allowCreating = value;
     },
     // Gets the array of taxon objects that have been selected by the user.
     get_selectedTaxa: function () {
         return this._selectedTaxa;
     },
     // Gets the reference to the client binder that displays the selected taxa.
     get_selectedTaxaBinder: function () {
         return this._selectedTaxaBinder;
     },
     // Sets the reference to the client binder that displays the selected taxa.
     set_selectedTaxaBinder: function (value) {
         this._selectedTaxaBinder = value;
     },

     // Returns true if the value of the field is changed
     isChanged: function () {
         if (this._originalValue == null) this._originalValue = [];

         var maxi = 0; //this._originalValue.length - 1;
         var changed = false;

         if (this.get_value() != null) {
             if (this._originalValue.length == this.get_value().length) {

                 while (this._originalValue[maxi]) {

                     var originalValue = this._originalValue[maxi];
                     var found = false;
                     var i = 0;
                     while (this.get_value()[i]) {
                         var currentValue = this.get_value()[i];
                         if (originalValue == currentValue) {
                             found = true;
                             break;
                         }
                         i++;
                     }

                     if (found == false) {
                         changed = true;
                         break;
                     }
                     maxi++;
                 }

             } else {
                 changed = true;
             }
         }

         return changed;
     },

     // Gets the value of the taxon field.
     get_value: function () {
         return this._getSelectedTaxaIds();
     },
     // Sets the value of the taxon field.
     set_value: function (value) {
         value = Telerik.Sitefinity.fixArray(value);
         this._originalValue = value;
         this._selectedIds = value;

         if (value != null && value.length > 0) {
             this._doExpandHandler();
             this.loadTaxa(value);
         }
     },
     get_defaultValue: function () {
         if (this._allowMultipleSelection) {
             return [];
         }
         return "";
     },

     // Gets the culture to use when visualizing a content.
     get_culture: function () {
         this._clientManager.get_culture();
     },
     // Sets the culture to use when visualizing a content.
     set_culture: function (culture) {
         this._clientManager.set_culture(culture);
     },

     // Gets the UI culture to use when visualizing a content.
     get_uiCulture: function () {
         this._clientManager.get_uiCulture();
     },
     // Sets the UI culture to use when visualizing a content.
     set_uiCulture: function (culture) {
         if (culture && this._clientManager.get_uiCulture() !== culture) {
             this._clientManager.set_uiCulture(culture);
             this._selectedTaxaBinder.set_uiCulture(culture);
             if (this._getSelectedTaxaIds() && this._getSelectedTaxaIds().length) {
                 this.loadTaxa(this._getSelectedTaxaIds());
             }
         }
     }
 };
 Telerik.Sitefinity.Web.UI.Fields.TaxonField.registerClass("Telerik.Sitefinity.Web.UI.Fields.TaxonField", Telerik.Sitefinity.Web.UI.Fields.FieldControl, Telerik.Sitefinity.Web.UI.Fields.ILocalizableFieldControl);
