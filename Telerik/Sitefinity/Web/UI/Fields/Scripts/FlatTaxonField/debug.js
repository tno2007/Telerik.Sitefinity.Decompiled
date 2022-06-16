Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField = function (element) {
    this._element = element;
    this._taxaInput = null;
    this._taxaInputHint = null;
    this._addTaxaButton = null;
    this._existingTaxaPanel = null;
    this._existingTaxaBinder = null;
    this._selectedTaxaBinder = null;
    this._selectFromExistingPanel = null;
    this._selectFromExistingButton = null;
    this._showAllTaxaButton = null;
    this._showOnlyMostPopularTaxaButton = null;
    this._closeExistingButton = null;
    this._existingTaxaTitle = null;
    this._openingExistingLoader = null;
    this._openingAllLoader = null;
    this._taxaAutoComplete = null;
    this._enabled = null;
    this._defaultTaxonsCount;

    this._currentPrefix = null;
    this._mostPopularTaxaTitle = null;
    this._allTaxaTitle = null;
    this._inputHintVisibleClass = null;
    this._inputHintHiddenClass = null;
    this._lastEnterWasAutocompletion = false;

    this._addTaxaButtonClickDelegate = null;
    this._selectFromExistingButtonClickDelegate = null;
    this._showAllTaxaButtonClickDelegate = null;
    this._showOnlyMostPopularTaxaButtonClickDelegate = null;
    this._closeExistingButtonClickDelegate = null;
    this._binderItemCommandDelegate = null;
    this._binderDataBoundDelegate = null;
    this._taxonAddedDelegate = null;
    this._taxonRemovedDelegate = null;
    this._taxaAutoCompletePopulatingDelegate = null;
    this._taxaAutoCompleteItemSelectedDelegate = null;
    this._taxaInputKeyPressDelegate = null;
    this._taxaInputFocusDelegate = null;
    this._taxaInputBlurDelegate = null;

    this._mostPopularTaxonsBound = false;
    this._allTaxonsBound = false;
    this._bindOnServer = false;

    Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.prototype =
{
    /* --------------------  set up and tear down ----------- */

    initialize: function () {

        if (this._addTaxaButtonClickDelegate == null) {
            this._addTaxaButtonClickDelegate = Function.createDelegate(this, this._addTaxaButtonClicked);
        }
        if (this._addTaxaButton) {
            if (!this.get_allowCreating()) {
                jQuery(this.get_addTaxaButton()).hide();
            }

            $addHandler(this._addTaxaButton, 'click', this._addTaxaButtonClickDelegate);
        }

        if (this._selectFromExistingButtonClickDelegate == null) {
            this._selectFromExistingButtonClickDelegate = Function.createDelegate(this, this._selectFromExistingButtonClicked);
        }
        if (this._selectFromExistingButton) {
            $addHandler(this._selectFromExistingButton, 'click', this._selectFromExistingButtonClickDelegate);
        }

        if (this._showAllTaxaButtonClickDelegate == null) {
            this._showAllTaxaButtonClickDelegate = Function.createDelegate(this, this._showAllTaxaButtonClicked);
        }
        if (this._showAllTaxaButton) {
            $addHandler(this._showAllTaxaButton, 'click', this._showAllTaxaButtonClickDelegate);
        }

        if (this._showOnlyMostPopularTaxaButtonClickDelegate == null) {
            this._showOnlyMostPopularTaxaButtonClickDelegate = Function.createDelegate(this, this._showOnlyMostPopularTaxaButtonClicked);
        }
        if (this._showOnlyMostPopularTaxaButton) {
            $addHandler(this._showOnlyMostPopularTaxaButton, 'click', this._showOnlyMostPopularTaxaButtonClickDelegate);
        }

        if (this._closeExistingButtonClickDelegate == null) {
            this._closeExistingButtonClickDelegate = Function.createDelegate(this, this._closeExistingButtonClicked);
        }
        if (this._closeExistingButton) {
            $addHandler(this._closeExistingButton, 'click', this._closeExistingButtonClickDelegate);
        }

        if (this._binderItemCommandDelegate == null) {
            this._binderItemCommandDelegate = Function.createDelegate(this, this._binderItemCommand);
        }
        if (this._existingTaxaBinder) {
            this._existingTaxaBinder.add_onItemCommand(this._binderItemCommandDelegate);
        }

        if (this._binderDataBoundDelegate == null) {
            this._binderDataBoundDelegate = Function.createDelegate(this, this._binderDataBound);
        }
        if (this._existingTaxaBinder) {
            this._existingTaxaBinder.add_onDataBound(this._binderDataBoundDelegate);
        }

        if (this._taxonAddedDelegate == null) {
            this._taxonAddedDelegate = Function.createDelegate(this, this._taxonAdded);
        }
        this.add_taxonAdded(this._taxonAddedDelegate);

        if (this._taxonRemovedDelegate == null) {
            this._taxonRemovedDelegate = Function.createDelegate(this, this._taxonRemoved);
        }
        this.add_taxonRemoved(this._taxonRemovedDelegate);

        if (this._taxaAutoCompletePopulatingDelegate == null) {
            this._taxaAutoCompletePopulatingDelegate = Function.createDelegate(this, this._taxaAutoCompletePopulating);
        }

        if (!this._bindOnServer) {
            this._taxaAutoComplete.add_populating(this._taxaAutoCompletePopulatingDelegate);
        }

        if (this._taxaAutoCompleteItemSelectedDelegate == null) {
            this._taxaAutoCompleteItemSelectedDelegate = Function.createDelegate(this, this._taxaAutoCompleteItemSelected);
        }

        if (!this._bindOnServer) {
            this._taxaAutoComplete.add_itemSelected(this._taxaAutoCompleteItemSelectedDelegate);
        }

        if (this._taxaInputKeyPressDelegate == null) {
            this._taxaInputKeyPressDelegate = Function.createDelegate(this, this._taxaInputKeyPress)
        }
        $addHandler(this._taxaInput, 'keypress', this._taxaInputKeyPressDelegate);

        if (this._taxaInputFocusDelegate == null) {
            this._taxaInputFocusDelegate = Function.createDelegate(this, this._taxaInputFocus);
        }
        $addHandler(this._taxaInput, 'focus', this._taxaInputFocusDelegate);

        if (this._taxaInputBlurDelegate == null) {
            this._taxaInputBlurDelegate = Function.createDelegate(this, this._taxaInputBlur);
        }
        $addHandler(this._taxaInput, 'blur', this._taxaInputBlurDelegate);

        Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.callBaseMethod(this, "initialize");
    },

    dispose: function () {

        if (this._addTaxaButtonClickDelegate != null) {
            delete this._addTaxaButtonClickDelegate;
        }

        if (this._selectFromExistingButtonClickDelegate != null) {
            delete this._selectFromExistingButtonClickDelegate;
        }

        if (this._closeExistingButtonClickDelegate != null) {
            delete this._closeExistingButtonClickDelegate;
        }

        if (this._binderItemCommandDelegate != null) {
            this._existingTaxaBinder.remove_onItemCommand(this._binderItemCommandDelegate);
            delete this._binderItemCommandDelegate;
        }

        if (this._binderDataBoundDelegate != null) {
            this._existingTaxaBinder.remove_onDataBound(this._binderDataBoundDelegate);
            delete this._binderDataBoundDelegate;
        }

        if (this._taxonAddedDelegate != null) {
            this.remove_taxonAdded(this._taxonAddedDelegate);
            delete this._taxonAddedDelegate;
        }

        if (this._taxonRemovedDelegate != null) {
            this.remove_taxonRemoved(this._taxonRemovedDelegate);
            delete this._taxonRemovedDelegate;
        }

        if (this._taxaAutoCompletePopulatingDelegate != null) {
            this._taxaAutoComplete.remove_populating(this._taxaAutoCompletePopulatingDelegate);
            delete this._taxaAutoCompletePopulatingDelegate;
        }

        if (this._taxaAutoCompleteItemSelectedDelegate != null) {
            this._taxaAutoComplete.remove_itemSelected(this._taxaAutoCompleteItemSelectedDelegate);
            delete this._taxaAutoCompleteItemSelectedDelegate;
        }

        if (this._taxaInputKeyPressDelegate != null) {
            delete this._taxaInputKeyPressDelegate;
        }

        if (this._taxaInputFocusDelegate != null) {
            delete this._taxaInputFocusDelegate;
        }

        if (this._taxaInputBlurDelegate != null) {
            delete this._taxaInputBlurDelegate;
        }

        Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    // Takes the array of taxon ids and returns client side objects with the desired taxon title (could be a full path, not necesarrily just
    // the title of the taxon) and id.
    loadTaxa: function (taxonIds) {
        if (taxonIds && taxonIds.length && !this._bindOnServer) {
            var serviceUrl = this.get_webServiceUrl();

            var data = {};
            data.ProviderName = this.get_taxonomyProvider();
            data.Skip = 0;
            data.Take = 0;
            data.SortExpression = 'Title ASC';
            data.Mode = 'Simple';
            data.SiteContextMode = 'allSitesContext';

            var filterExpression = '';
            var taxonIdCount = taxonIds.length;
            while (taxonIdCount--) {
                filterExpression += 'Id == ' + taxonIds[taxonIdCount] + ' OR ';
            }
            filterExpression = filterExpression.substr(0, filterExpression.length - 4);

            data.FilterExpression = filterExpression;

            var keys = [];
            var urlParams = [];

            if (this._clientManager)
                this._clientManager.InvokePost(serviceUrl, urlParams, keys, data, this._loadTaxaSuccess, this._loadTaxaFailure, this);
        }
    },

    _loadTaxaSuccess: function (caller, result) {
        var taxa = result.Items;
        var taxaCount = taxa.length;

        for (var i = 0; i < taxaCount; i++) {
            caller.addTaxon(taxa[i], true);
        }
        var data = { 'Items': caller._selectedTaxa };
        caller._selectedTaxaBinder.BindCollection(data);
    },

    //TODO: this code needs review
    reset: function () {
        Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.callBaseMethod(this, "reset");
        this._clientManager.set_uiCulture(null);
        this._existingTaxaBinder.set_uiCulture(null);
        this.clearSelection();
    },

    clearSelection: function () {
        $(this._existingTaxaPanel).hide();
        $(this._selectFromExistingPanel).show();
        this._selectedTaxaBinder.ClearTarget();
        if (this._taxaInput != null)
            this._taxaInput.value = "";
    },

    _loadTaxaFailure: function (result) {
        alert(result.Detail);
    },

    // Focuses the input element.
    // Overriden from field control
    focus: function () {
        var input = this.get_taxaInput();
        if (jQuery(input).is(":visible")) {

            input.focus();
        }
    },

    // Blures the input element.
    // Overriden from field control
    blur: function () {
        var behavior = this._get_ExpandableExtenderBehavior();
        if (behavior != null && !behavior.get_originalExpandedState()) {
            var val = this.get_value();
            if (val == "") {
                behavior.collapse();
            }
        }
    },

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    // Handles the click event of the add taxa button.
    _addTaxaButtonClicked: function () {
        // TODO: write unit tests
        this._addTaxa();
    },

    // Handles the click event of the select from existing button.
    _selectFromExistingButtonClicked: function () {
        // TODO: write unit tests
        $(this._existingTaxaPanel).show();
        $(this._selectFromExistingPanel).hide();
        if (this._initialTaxaCount > 0) {
            this._bindMostPopularExistingTaxa();
        }
    },

    // Handles the click event of the show all taxa button.
    _showAllTaxaButtonClicked: function () {
        // TODO: write unit tests
        this._bindAllExistingTaxa();
        $(this._showAllTaxaButton).hide();
        $(this._showOnlyMostPopularTaxaButton).show();
        $(this._existingTaxaTitle).text(this._allTaxaTitle);
    },

    // Handles the click event of the show only most popular taxa button.
    _showOnlyMostPopularTaxaButtonClicked: function () {
        this._bindMostPopularExistingTaxa();
        $(this._showAllTaxaButton).show();
        $(this._showOnlyMostPopularTaxaButton).hide();
        $(this._existingTaxaTitle).text(this._mostPopularTaxaTitle);
    },

    // Handles the click event of the close existing button.
    _closeExistingButtonClicked: function () {
        $(this._existingTaxaPanel).hide();
        $(this._selectFromExistingPanel).show();
    },

    // Handles the populating event of the taxa auto complete extender.
    _taxaAutoCompletePopulating: function (sender, eventArgs) {
        // TODO: write unit tests
        // cancel the default behavior
        eventArgs.set_cancel(true);

        // get the data
        var serviceUrl = this.get_webServiceUrl();
        this._currentPrefix = jQuery.trim(this._taxaAutoComplete._currentPrefix);
        this._currentPrefix = this._currentPrefix.replace(/\"/g, "''");
        var urlParams = [];
        urlParams['providerName'] = this.get_taxonomyProvider();
        urlParams['skip'] = 0;
        urlParams['take'] = sender.get_completionSetCount();
        urlParams['sortExpression'] = 'Title ASC';
        urlParams['filter'] = this._currentPrefix;
        urlParams['mode'] = 'AutoComplete';

        var keys = [];
        this._clientManager.InvokeGet(serviceUrl, urlParams, keys, this._taxaAutoCompletePopulatingSuccess, this._taxaAutoCompletePopulatingFailure, this);
    },

    // Callback method called upon successful call made on populating event of auto complete extender.
    _taxaAutoCompletePopulatingSuccess: function (caller, result) {
        completionItems = [];
        for (i = 0; i < result.Items.length; i++) {
            var currentItem = result.Items[i].Title;
            if (currentItem) {
                var autoCompleteEntry = Object.create({});
                autoCompleteEntry["First"] = currentItem;
                autoCompleteEntry["Second"] = currentItem;
                completionItems.push(JSON.stringify(autoCompleteEntry));
            }
        }
        caller._taxaAutoComplete._update(caller._taxaAutoComplete._currentPrefix, completionItems, false);
        caller._taxaAutoComplete.raisePopulated();
        caller._taxaAutoComplete.showPopup();
    },

    // Callback method called upon successful call made on populating event of auto complete extender.
    _taxaAutoCompletePopulatingFailure: function (result) {
        alert(result.Detail);
    },

    // Handles the item selected event of the taxa auto complete extender.
    _taxaAutoCompleteItemSelected: function (sender, args) {
        this._taxaInput.value += ', ';
        this._lastEnterWasAutocompletion = true;
    },

    // Handles the key press event on the taxa input field.
    _taxaInputKeyPress: function (e) {
        var characterCode; //literal character code will be stored in this variable
        if (e && e.keyCode) {
            characterCode = e.keyCode;
        }
        else {
            characterCode = e.charCode;
        }

        if (characterCode == 13 && this.get_allowCreating()) { //if generated character code is equal to ascii 13 (if enter key)
            if (!this._lastEnterWasAutocompletion || this._getInternetExplorerVersion() > -1) {
                this._addTaxa();
            }
            this._lastEnterWasAutocompletion = false;
            // prevents beeping in IE
            return false;
        }
    },

    // Handles the focus event of the taxa input element. 
    _taxaInputFocus: function () {
        $(this._taxaInputHint).removeClass(this._inputHintVisibleClass);
        $(this._taxaInputHint).addClass(this._inputHintHiddenClass);
    },

    // Handles the blur event of the taxa input element. 
    _taxaInputBlur: function () {
        if (this._taxaInput.value.length == 0) {
            $(this._taxaInputHint).addClass(this._inputHintVisibleClass);
            $(this._taxaInputHint).removeClass(this._inputHintHiddenClass);
        }
    },

    // Handles the item command of the existing taxa binder.
    _binderItemCommand: function (sender, eventArgs) {
        if (this._enabled) {
            var dataItem = eventArgs.get_dataItem();
            this.addTaxon(dataItem);
        }
    },

    // Handles the data bound event of the existing taxa binder.
    _binderDataBound: function (sender, eventArgs) {
        $(this._openingExistingLoader).hide();
        $(this._openingAllLoader).hide();
        this._disableSelectedTaxa();
    },

    // Handles the taxon added event of this class.
    _taxonAdded: function (sender, eventArgs) {
        // TODO: write unit tests for this
        this._disableSelectedTaxa();
    },

    // Handles the taxon removed event of this class.
    _taxonRemoved: function (sender, eventArgs) {
        // TODO: write unit tests for this
        this._disableSelectedTaxa();
    },

    _getInternetExplorerVersion: function () {
        // Returns the version of Windows Internet Explorer or a -1
        // (indicating the use of another browser).
        var rv = -1; // Return value assumes failure.
        if (navigator.appName == 'Microsoft Internet Explorer') {
            var ua = navigator.userAgent;
            var re = new RegExp("MSIE ([0-9]{1,}[\.0-9]{0,})");
            if (re.exec(ua) != null)
                rv = parseFloat(RegExp.$1);
        }
        return rv;
    },

    /* -------------------- private methods ----------- */

    _addTaxa: function (isSynchronous) {
        if (!this.get_allowCreating())
            return;
        var taxaTitles = this._getEnteredTaxa();
        this._ensureTaxa(taxaTitles, isSynchronous);
        this._taxaInput.value = '';
    },

    // Gets an array of taxon titles entered by the user in the taxa input element.
    _getEnteredTaxa: function () {
        var taxaTitles = [];
        if (this._taxaInput == null) {
            throw ('TaxaInput element is not initialized.');
        }
        var taxa = this._taxaInput.value.split(',');
        var taxaCount = taxa.length;
        while (taxaCount--) {
            var taxonTitle = jQuery.trim(taxa[taxaCount]);
            taxonTitle = taxonTitle.replace(/\"/g, "''");
            if (taxonTitle.length > 0) {
                taxaTitles.push(taxonTitle);
            }
        }
        return taxaTitles;
    },

    // Calls the enusre taxa method through a web service, which will create new taxon objects for each
    // title that does not exist.
    _ensureTaxa: function (taxaTitles, isSynchronous) {
        // TODO: write unit tests
        if (taxaTitles && taxaTitles.length) {
            var serviceUrl = this.get_webServiceUrl() + 'ensure/';
            var data = taxaTitles;
            var urlParams = [];
            urlParams['providerName'] = this.get_taxonomyProvider();
            var keys = [];
            //this._clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._ensureTaxaSuccess, this._ensureTaxaFailure, this);
            this._clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._ensureTaxaSuccess, this._ensureTaxaFailure, this, null, null, null, isSynchronous);
        }
    },

    // Callback function called upon successfull execution of the async. request to ensure taxa.
    _ensureTaxaSuccess: function (caller, result) {
        // TODO: write unit tests
        var taxaCount = result.length;
        while (taxaCount--) {
            caller.addTaxon(result[taxaCount]);
        }
    },

    // Callback function called upon failed execution of the async. request to ensure taxa.
    // Never call this method on your own as it will ensure the end of the flat taxonomy species!
    _ensureTaxaFailure: function (result) {
        // TODO: write unit tests
        alert(result.Detail);
    },

    // Binds the 30 most popular taxons.
    _bindMostPopularExistingTaxa: function () {
        this._mostPopularTaxonsBound = true;
        this._allTaxonsBound = false;
        $(this._openingExistingLoader).show();
        this._existingTaxaBinder.set_take(this._defaultTaxonsCount);
        this._existingTaxaBinder.set_sortExpression('Title ASC');
        this._existingTaxaBinder.DataBind();
    },

    // Binds all the existing taxons.
    _bindAllExistingTaxa: function () {
        // TODO: write unit tests
        // max is 500
        this._mostPopularTaxonsBound = false;
        this._allTaxonsBound = true;
        $(this._openingAllLoader).show();
        this._existingTaxaBinder.set_take(500);
        this._existingTaxaBinder.set_sortExpression('Title ASC');
        this._existingTaxaBinder.DataBind();
    },

    // This function goes through all the existing taxa links and disables all that have been selected.
    _disableSelectedTaxa: function () {
        // TODO: write unit tests for this
        var _self = this;
        $(this._existingTaxaBinder.get_target()).find('.sf_binderCommand_select').each(function () {
            $(this).removeClass('sfDisabled');
            if (_self._isTaxonSelected(this.innerHTML)) {
                $(this).addClass('sfDisabled');
            }
        });
    },

    // Determines whether taxon has been selected by its title.
    _isTaxonSelected: function (title) {
        var selectedTaxa = this.get_selectedTaxa();
        var selectedTaxaCount = selectedTaxa.length;
        while (selectedTaxaCount--) {
            if (title == selectedTaxa[selectedTaxaCount].Title) {
                return true;
            }
        }
        return false;
    },

    // Gets the attached expandable behaviour
    _get_ExpandableExtenderBehavior: function () {
        if (this._expandableExtenderBehavior) {
            return this._expandableExtenderBehavior;
        }
        this._expandableExtenderBehavior = Sys.UI.Behavior.getBehaviorByName(this._element, "ExpandableExtender");
        return this._expandableExtenderBehavior;
    },

    _rebindExistingTaxa: function () {
        if (this._mostPopularTaxonsBound) {
            this._bindMostPopularExistingTaxa();
        }

        if (this._allTaxonsBound) {
            this._bindAllExistingTaxa();
        }
    },

    /* -------------------- properties ---------------- */

    // Gets the reference to the text input element used to enter taxa.
    get_taxaInput: function () {
        return this._taxaInput;
    },
    // Sets the reference to the text input element used to enter taxa.
    set_taxaInput: function (value) {
        this._taxaInput = value;
    },
    // Gets the reference to the label that represents the hint for the taxa input.
    get_taxaInputHint: function () {
        return this._taxaInputHint;
    },
    // Sets the reference to the label that represents the hint for the taxa input.
    set_taxaInputHint: function (value) {
        this._taxaInputHint = value;
    },
    // Gets the reference to the button that adds taxa to the selected taxa collection.
    get_addTaxaButton: function () {
        return this._addTaxaButton;
    },
    // Sets the reference to the button that adds taxa to the selected taxa collection.
    set_addTaxaButton: function (value) {
        this._addTaxaButton = value;
    },
    // Gets the reference to the element which represents the panel that holds existing taxa.
    get_existingTaxaPanel: function () {
        return this._existingTaxaPanel;
    },
    // Sets the reference to the element which represents the panel that holds existing taxa.
    set_existingTaxaPanel: function (value) {
        this._existingTaxaPanel = value;
    },
    // Gets the reference to the client binder that binds the existing taxa.
    get_existingTaxaBinder: function () {
        return this._existingTaxaBinder;
    },
    // Sets the reference to the client binder that binds the existing taxa.
    set_existingTaxaBinder: function (value) {
        this._existingTaxaBinder = value;
    },

    // Gets the reference to the client binder that binds the selected taxa.
    get_selectedTaxaBinder: function () {
        return this._selectedTaxaBinder;
    },
    // Sets the reference to the client binder that binds the selected taxa.
    set_selectedTaxaBinder: function (value) {
        this._selectedTaxaBinder = value;
    },

    // Gets the reference to the panel which holds command for selecting taxon from existing taxa.
    get_selectFromExistingPanel: function () {
        return this._selectFromExistingPanel;
    },
    // Sets the reference to the panel which holds command for selecting taxon from existing taxa.
    set_selectFromExistingPanel: function (value) {
        this._selectFromExistingPanel = value;
    },
    // Gets the reference to the button which shows 30 most popular existing taxa.
    get_selectFromExistingButton: function () {
        return this._selectFromExistingButton;
    },
    // Sets the reference to the button which shows 30 most popular existing taxa.
    set_selectFromExistingButton: function (value) {
        this._selectFromExistingButton = value;
    },
    // Gets the reference to the button which closes the panel with existing taxa.
    get_closeExistingButton: function () {
        return this._closeExistingButton;
    },
    // Sets the reference to the button which closes the panel with existing taxa.
    set_closeExistingButton: function (value) {
        this._closeExistingButton = value;
    },
    // Gets the reference to the taxa auto complete extender.
    get_taxaAutoComplete: function () {
        return this._taxaAutoComplete;
    },
    // Sets the reference to the taxa auto complete extender.
    set_taxaAutoComplete: function (value) {
        this._taxaAutoComplete = value;
    },
    // Gets the reference to the button that shows all existing taxa when clicked.
    get_showAllTaxaButton: function () {
        return this._showAllExistingTaxaButton;
    },
    // Sets the reference to the button that shows all existing taxa when clicked.
    set_showAllTaxaButton: function (value) {
        this._showAllTaxaButton = value;
    },
    // Gets the reference to the button that shows only the most popular taxa when clicked.
    get_showOnlyMostPopularTaxaButton: function () {
        return this._showOnlyMostPopularTaxaButton;
    },
    // Sets the reference to the button that shows only the most popular taxa when clicked.
    set_showOnlyMostPopularTaxaButton: function (value) {
        this._showOnlyMostPopularTaxaButton = value;
    },
    // Gets the reference to the control that displays the title of the existing taxa panel.
    get_existingTaxaTitle: function () {
        return this._existingTaxaTitle;
    },
    // Sets the reference to the control that displays the title of the existing taxa panel.
    set_existingTaxaTitle: function (value) {
        this._existingTaxaTitle = value;
    },
    // Gets the reference to the control that represents the loader for displaying existing taxa.
    get_openingExistingLoader: function () {
        return this._openingExistingLoader;
    },
    // Sets the reference to the control that represents the loader for displaying existing taxa.
    set_openingExistingLoader: function (value) {
        this._openingExistingLoader = value;
    },
    // Gets the reference to the control that represents the loader for displaying all taxa.
    get_openingAllLoader: function () {
        return this._openingAllLoader;
    },
    // Sets the reference to the control that represents the loader for displaying all taxa.
    set_openingAllLoader: function (value) {
        this._openingAllLoader = value;
    },

    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
        if (culture && this._clientManager.get_uiCulture() !== culture) {
            Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.callBaseMethod(this, "set_uiCulture", [culture]);
            this._existingTaxaBinder.set_uiCulture(culture);
            this._rebindExistingTaxa(culture);
        }
    },

    get_value: function () {
        this._addTaxa(true); //true for synchronous request
        //        //this._addTaxa:
        //        var taxaTitles = this._getEnteredTaxa();
        //        if (taxaTitles && taxaTitles.length) {
        //            //this._ensureTaxa:
        //            var serviceUrl = this.get_webServiceUrl() + 'ensure/';
        //            var data = taxaTitles;
        //            var urlParams = [];
        //            urlParams['providerName'] = this.get_taxonomyProvider();
        //            var keys = [];
        //            this._clientManager.InvokePut(serviceUrl, urlParams, keys, data, this._ensureTaxaSuccess, this._ensureTaxaFailure, this, null, null, null, true); //true for synchronous request
        //            this._taxaInput.value = '';
        //        }
        return Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.callBaseMethod(this, "get_value");
    }
};

Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField.registerClass("Telerik.Sitefinity.Web.UI.Fields.FlatTaxonField", Telerik.Sitefinity.Web.UI.Fields.TaxonField);