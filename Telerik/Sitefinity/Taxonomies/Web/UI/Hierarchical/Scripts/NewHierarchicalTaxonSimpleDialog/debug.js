Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical");

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog = function (element) {
    this._element = element;
    this._dialogTitleElement = null;
    this._taxonTitleField = null;
    this._createButton = null;
    this._cancelButton = null;
    this._parentSelector = null;

    this._taxonomyId = null;
    this._taxonomyProvider = null;
    this._taxonomyTitle = null;
    this._taxonName = null;

    this._createALabel = null;
    this._createThisLabel = null;
    this._selectAParentLabel = null;
    this._noParentLabel = null;
    this._selectorTitleLabel = null;

    this._loadDelegate = null;
    this._createButtonClickDelegate = null;
    this._cancelButtonClickDelegate = null;

    this._createTaxonSuccessDelegate = null
    this._createTaxonFailureDelegate = null
    this._dialogShowDelegate = null;

    this._clientManager = null;
    this._culture = null;
    this._uiCulture = null;
    this._fallbackMode = null;
    this._webServiceUrl = null;
    this._urlRegexFilter = null;
    this._regularExpression = null;

    Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog.initializeBase(this, [element]);
}

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog.prototype =
 {
     /* -------------------- set up and tear down ----------- */

     initialize: function () {
         this._clientManager = new Telerik.Sitefinity.Data.ClientManager();
         if (this._createButtonClickDelegate == null) {
             this._createButtonClickDelegate = Function.createDelegate(this, this._createButtonClicked);
         }
         if (this._createButton) {
             $addHandler(this._createButton, 'click', this._createButtonClickDelegate);
         }
         if (this._cancelButtonClickDelegate == null) {
             this._cancelButtonClickDelegate = Function.createDelegate(this, this._cancelButtonClicked);
         }
         if (this._createTaxonSuccessDelegate == null) {
             this._createTaxonSuccessDelegate = Function.createDelegate(this, this._createTaxonSuccess);
         }
         if (this._createTaxonFailureDelegate == null) {
             this._createTaxonFailureDelegate = Function.createDelegate(this, this._createTaxonFailure);
         }
         if (this._dialogShowDelegate == null) {
             this._dialogShowDelegate = Function.createDelegate(this, this._dialogShowHandler);
         }
         if (this._cancelButton) {
             $addHandler(this._cancelButton, 'click', this._cancelButtonClickDelegate);
         }

         if (this._loadDelegate == null) {
             this._loadDelegate = Function.createDelegate(this, this.loadDialog);
         }
         Sys.Application.add_load(this._loadDelegate);

         Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog.callBaseMethod(this, "initialize");
     },

     dispose: function () {
         if (this._createButtonClickDelegate != null) {
             delete this._createButtonClickDelegate;
         }
         if (this._cancelButtonClickDelegate != null) {
             delete this._cancelButtonClickDelegate;
         }
         if (this._loadDelegate != null) {
             delete this._loadDelegate;
         }
         if (this._createTaxonSuccessDelegate != null) {
             delete this._createTaxonSuccessDelegate;
         }
         if (this._createTaxonFailureDelegate != null) {
             delete this._createTaxonFailureDelegate;
         }
         if (this._dialogShowDelegate !== null) {
             if (dialogBase && dialogBase.get_radWindow && dialogBase.get_radWindow()) {
                 dialogBase.get_radWindow().remove_show(this._dialogShowDelegate);
             }
             delete this._dialogShowDelegate;
         }
         Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog.callBaseMethod(this, "dispose");
     },

     /* --------------------  public methods ----------- */

     // Loads the selector
     loadDialog: function () {
         if (this._taxonomyId == null) {
             this._taxonomyId = dialogBase.getQueryValue('taxonomyId', true);
         }
         if (this._taxonomyId == null) {
             throw ('The dialog needs to have taxonomyId property initialized. You can initialize it by setting it directly on the component, or having the query string value with the key "taxonomyId"');
         }

         if (this._taxonomyProvider == null) {
             this._taxonomyProvider = dialogBase.getQueryValue('taxonomyProvider', true);
         }
         if (this._taxonomyTitle == null) {
             this._taxonomyTitle = dialogBase.getQueryValue('taxonomyTitle', true);
         }
         if (this._taxonName == null) {
             this._taxonName = dialogBase.getQueryValue('taxonName', true);
         }

         this._dialogTitleElement.innerHTML = this._createALabel + ' ' + this._taxonName.toLowerCase();
         this._taxonTitleField.set_title(this._taxonName);
         this._createButton.innerHTML = this._createThisLabel + ' ' + this._taxonName.toLowerCase();

         var serviceUrl = this._parentSelector.get_taxaTreeBinder().get_serviceBaseUrl();
         serviceUrl = String.format(serviceUrl, this._taxonomyId);

         this._parentSelector.add_taxaRootChoiceChanged(this._resizeDialog);

         this._parentSelector.get_taxaTreeBinder().set_provider(this._taxonomyProvider);
         this._parentSelector.get_taxaTreeBinder().set_orginalServiceBaseUrl(serviceUrl);
         this._parentSelector.set_selectorTitleText(this._taxonName.toLowerCase());
         this._parentSelector.set_selectorTitleTextTemplate(this._selectorTitleLabel);
         this._parentSelector.set_rootRadioText(this._taxonName.toLowerCase());
         this._parentSelector.set_taxaRadioText(this._selectAParentLabel);

         dialogBase.get_radWindow().add_show(this._dialogShowDelegate);
         dialogBase.resizeToContent();
     },

     /* -------------------- events -------------------- */

     /* -------------------- event handlers ------------ */

     // Handles the click event of the create button.
     _createButtonClicked: function () {
         if (this._taxonTitleField.validate()) {
             var parentTaxonId = this._parentSelector.get_selectedTaxa()[0];
             var urlName = this._taxonTitleField.get_value();
             if (urlName) {
                 urlName = urlName.toLowerCase().replace(this._get_regularExpression(), "-");
             }
             var taxonName = this._taxonTitleField.get_value();
             if (taxonName) {
                 taxonName = taxonName.toLowerCase().replace(this._get_regularExpression(), "-");
             }
             var data = {
                 'Id': this._clientManager.GetEmptyGuid(),
                 'TaxonomyId': this._taxonomyId,
                 'ParentTaxonId': parentTaxonId,
                 'Title': this._taxonTitleField.get_value(),
                 'Name': taxonName,
                 'UrlName': urlName
             };

             var serviceUrl = this._webServiceUrl;
             var urlParams = [];
             urlParams['providerName'] = this._taxonomyProvider;
             var keys = [this._taxonomyId, this._clientManager.GetEmptyGuid()];
             var culture = this._getConfiguredCulture();
             this._clientManager.set_uiCulture(culture);
             this._clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._createTaxonSuccessDelegate, this._createTaxonFailureDelegate, this);
         }
         else {
             dialogBase.get_radWindow().autoSize();
         }
     },

     // Called upon successful creation of the new taxon.
     _createTaxonSuccess: function (caller, result) {
         this._reset();
         dialogBase.close(result.Id);
     },

     // Called if the creation of the new taxon has failed.
     _createTaxonFailure: function (result) {
         alert(result.Detail);
     },

     // Resets the form
     _reset: function () {
         this._taxonTitleField.reset();
     },

     // Handles the click event of the cancel button.
     _cancelButtonClicked: function () {
         this._reset();
         dialogBase.close();
     },

     _dialogShowHandler: function () {
         this._parentSelector.get_taxaTreeBinder().set_uiCulture(this._getConfiguredCulture());
     },

     /* -------------------- private methods ----------- */
     _resizeDialog: function () {
         dialogBase.resizeToContent();
     },

     _get_regularExpression: function () {
         if (!this._regularExpression)
             this._regularExpression = new XRegExp(this._urlRegexFilter, "g");
         return this._regularExpression;
     },

     _getConfiguredCulture: function () {
         var culture = null;
         var args = dialogBase.get_radWindow()._sfArgs;
         if (args && args.uiCulture)
             return args.uiCulture;
         return culture;
     },

     /* -------------------- properties ---------------- */

     // Gets the reference to the DOM element that represents the dialog title.
     get_dialogTitleElement: function () {
         return this._dialogTitleElement;
     },
     // Sets the reference to the DOM element that represents the dialog title.
     set_dialogTitleElement: function (value) {
         this._dialogTitleElement = value;
     },
     // Gets the reference to the text field component that represents the title for the taxon title textbox.
     get_taxonTitleField: function () {
         return this._taxonTitleField;
     },
     // Sets the reference to the text field component that represents the title for the taxon title textbox.
     set_taxonTitleField: function (value) {
         this._taxonTitleField = value;
     },
     // Gets the reference to the button that when clicked creates a new taxon.
     get_createButton: function () {
         return this._createButton;
     },
     // Sets the reference to the button that when clicked creates a new taxon.
     set_createButton: function (value) {
         this._createButton = value;
     },
     // Gets the reference to the button that when clicked closes the dialog.
     get_cancelButton: function () {
         return this._cancelButton;
     },
     // Sets the reference to the button that when clicked closes the dialog.
     set_cancelButton: function (value) {
         this._cancelButton = value;
     },
     // Gets the reference to the hierarchical taxon selector component.
     get_parentSelector: function () {
         return this._parentSelector;
     },
     // Sets the reference to the hierarchical taxon selector component.
     set_parentSelector: function (value) {
         this._parentSelector = value;
     },
     // Gets the id of the taxonomy for which the dialog creates new taxon.
     get_taxonomyId: function () {
         return this._taxonomyId;
     },
     // Sets the id of the taxonomy for which the dialog creates new taxon.
     set_taxonomyId: function (value) {
         this._taxonomyId = value;
     },
     // Gets the provider of the taxonomy for which the dialog creates new taxon.
     get_taxonomyProvider: function () {
         return this._taxonomyProvider;
     },
     // Sets the provider of the taxonomy for which the dialog creates new taxon.
     set_taxonomyProvider: function (value) {
         this._taxonomyProvider = value;
     },
     // Gets the title of the taxonomy.
     get_taxonomyTitle: function () {
         return this._taxonomyTitle;
     },
     // Sets the title of the taxonomy.
     set_taxonomyTitle: function (value) {
         this._taxonomyTitle = value;
     },
     // Gets the name of the single taxon.
     get_taxonName: function () {
         return this._taxonName;
     },
     // Sets the name of the single taxon.
     set_taxonName: function (value) {
         this._taxonName = value;
     },

     // Specifies the culture that will be used on the server as CurrentThread when processing the request
     set_culture: function (culture) {
         this._culture = culture;
     },
     // Gets the culture that will be used on the server when processing the request
     get_culture: function () {
         return this._culture;
     },

     // Specifies the culture that will be used on the server as UICulture when processing the request
     set_uiCulture: function (culture) {
         this._uiCulture = culture;
     },
     // Gets the culture that will be used on the server as UICulture when processing the request
     get_uiCulture: function () {
         return this._uiCulture;
     },

     // Sets the culture fallback mode for the requests.
     set_fallbackMode: function (culture) {
         this._fallbackMode = culture;
     },
     // Gets the culture fallback mode for the requests.
     get_fallbackMode: function () {
         return this._fallbackMode;
     }
 };

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog.registerClass("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.NewHierarchicalTaxonSimpleDialog", Sys.UI.Control);
