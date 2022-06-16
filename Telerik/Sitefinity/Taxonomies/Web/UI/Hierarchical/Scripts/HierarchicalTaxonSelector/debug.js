Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical");

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector = function (element) {
    this._element = element;
    this._selectorTitle = null;
    this._labelNoCategories = null;
    this._createTaxonButton = null;
    this._doneButton = null;
    this._newTaxonDialog = null;
    this._rootRadio = null;
    this._taxaRadio = null;
    this._rootRadioLabel = null;
    this._taxaRadioLabel = null;
    this._treePanel = null;
    this._taxaTreeBinder = null;
    this._noTaxaCreatedLabel = null;

    this._selectorTitleText = null;
    this._selectRootText = null;
    this._selectFromTaxaText = null;
    this._noTaxaHaveBeenCreatedText = null;

    this._taxonomyId = null;
    this._allowRootSelection = false;
    this._allowMultipleSelection = false;
    this._showDoneSelectingButton = false;
    this._showCreateNewTaxonButton = false;
    this._showBinder = false;

    this._rootRadioClickDelegate = null;
    this._taxaRadioClickDelegate = null;
    this._createTaxonButtonClickDelegate = null;
    this._doneButtonClickDelegate = null;
    this._newTaxonDialogClosedDelegate = null;
    this._onLoadDelegate = null;
    this._dataBoundDelagete = null;

    this._isServiceUrlsSet = false;
    this._isControlsConfigured = false;

    this._rootRadioTextTemplate = null;
    this._createTaxonButtonTextTemplate = null;
    this._selectorTitleTextTemplate = null;

    this._controlLoaded = false;
    this._bindOnServer = false;
    this._isBound = false;
    this._hidden = false;

    Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector.initializeBase(this, [element]);
};

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        if (this._doneButtonClickedDelegate == null) {
            this._doneButtonClickedDelegate = Function.createDelegate(this, this._doneButtonClicked);
        }
        if (this._doneButton) {
            $addHandler(this._doneButton, 'click', this._doneButtonClickedDelegate);
        }
        if (this._createTaxonButtonClickDelegate == null) {
            this._createTaxonButtonClickDelegate = Function.createDelegate(this, this._createTaxonButtonClicked);
        }
        if (this._createTaxonButton) {
            $addHandler(this._createTaxonButton, 'click', this._createTaxonButtonClickDelegate);
        }
        if (this._rootRadioClickDelegate == null) {
            this._rootRadioClickDelegate = Function.createDelegate(this, this._rootRadioClicked);
        }
        if (this._rootRadio) {
            $addHandler(this._rootRadio, 'click', this._rootRadioClickDelegate);
        }

        if (this._taxaRadioClickDelegate == null) {
            this._taxaRadioClickDelegate = Function.createDelegate(this, this._taxaRadioClicked);
        }
        if (this._taxaRadio) {
            $addHandler(this._taxaRadio, 'click', this._taxaRadioClickDelegate);
        }

        if (this._newTaxonDialogClosedDelegate == null) {
            this._newTaxonDialogClosedDelegate = Function.createDelegate(this, this._newTaxonDialogClosed);
        }
        this._newTaxonDialog.add_close(this._newTaxonDialogClosedDelegate);
        this._showBinder = $(this._taxaRadio).is(':checked') || this._allowRootSelection == false;
        if (this._noTaxaCreatedLabel) {
            this._noTaxaCreatedLabel.innerHTML = String.format(this._noTaxaHaveBeenCreatedText);
        }
        if (this._dataBoundDelegate == null)
            this._dataBoundDelagete = Function.createDelegate(this, this._onDataBound);

        Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector.callBaseMethod(this, "initialize");

        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    dispose: function () {
        if (this._rootRadio && this._rootRadioClickDelegate) {
            $removeHandler(this._rootRadio, 'click', this._rootRadioClickDelegate);
        }

        if (this._taxaRadio && this._taxaRadioClickDelegate) {
            $removeHandler(this._taxaRadio, 'click', this._taxaRadioClickDelegate);
        }

        if (this._doneButtonClickDelegate != null) {
            delete this._doneButtonClickDelegate;
        }

        if (this._createTaxonButtonClickDelegate != null) {
            delete this._createTaxonButtonClickDelegate;
        }

        if (this._rootRadioClickDelegate != null) {
            delete this._rootRadioClickDelegate;
        }

        if (this._taxaRadioClickDelegate != null) {
            delete this._taxaRadioClickDelegate;
        }

        if (this._newTaxonDialog && this._newTaxonDialog.remove_closed && this._newTaxonDialogClosedDelegate != null) {
            this._newTaxonDialog.remove_closed(this._newTaxonDialogClosedDelegate);
        }

        if (this._newTaxonDialogClosedDelegate != null) {
            delete this._newTaxonDialogClosedDelegate;
        }

        if (this._onLoadDelegate != null) {
            delete this._onLoadDelegate;
        }

        if (this._dataBoundDelegate != null)
            delete this._dataBoundDelagete;

        Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    onload: function () {
        this._controlLoaded = true;
        if (!this._bindOnServer) {
            this._taxaTreeBinder.add_onDataBound(this._dataBoundDelagete);
            this._taxaTreeBinder.get_urlParams()["mode"] = "Simple";
        }
        this._configureControls();
    },

    _configureControls: function () {
        this._set_taxaRadioText();
        this._set_rootRadioText();
        this._set_createTaxonButtonText();
        this._set_selectorTitleText();
        this._configureSelectorMode();
    },

    _configureSelectorMode: function () {
        if (this._showBinder) {
            this.showTreePanel();
        }
        else {
            this.hideTreePanel();
        }
    },

    // Binds the hierarchical taxon selector to the data.
    dataBind: function () {
        if (this._hidden)
            return;
        if (!this._isServiceUrlsSet) {
            this._setServiceUrls();
        }
        if (this._isToBind()) {
            this._taxaTreeBinder.Sort("Ordinal"); //calls databind
            this._isBound = true;
        }
    },

    chnageVisibility: function (visible) {
        this._hidden = !visible;
        if (visible) {
            if (!this._isBound) {
                this.dataBind();
            }
        }
        else {
            this._isBound = false;
        }
    },

    // Clears all the selected items.
    clearSelection: function () {
        if (this._isBound)
            this._taxaTreeBinder.clearSelection();
    },

    showTreePanel: function () {
        $(this._treePanel).show();
        if (this._showDoneSelectingButton) {
            $(this._doneButton).show();
        }
        if (this._showCreateNewTaxonButton) {
            $(this._createTaxonButton).show();
        }
        if (!this._isBound) {
            this.dataBind();
        }
    },

    hideTreePanel: function () {
        $(this._treePanel).hide();
        if (this._showDoneSelectingButton) {
            $(this._doneButton).hide();
        }
        if (this._showCreateNewTaxonButton) {
            $(this._createTaxonButton).hide();
        }
        this._isBound = false;
    },

    toggleNoItemsMessage: function () {
        var treeView = jQuery(this.get_treePanel()).find(".RadTreeView"),
            noItemsMsg = jQuery(this.get_labelNoCategories());

        if (this._taxaTreeBinder.get_displayedItemsCount() === 0) {
            treeView.addClass("sfRadTreeViewEmpty");
            noItemsMsg.removeClass("sfDisplayNone");
        } else if (treeView.hasClass("sfRadTreeViewEmpty")) {
            treeView.removeClass("sfRadTreeViewEmpty");
            noItemsMsg.addClass("sfDisplayNone");
        }
    },

    /* -------------------- events -------------------- */

    // Subscribes a delegate to be called when event selection done is fired.
    add_selectionDone: function (delegate) {
        this.get_events().addHandler('selectionDone', delegate);
    },

    // Unsubscribes a delegate from the selection done event.
    remove_selectionDone: function (delegate) {
        this.get_events().removeHandler('selectionDone', delegate);
    },

    // Subscribes a delegate to be called when event radio clicked is fired.
    add_taxaRootChoiceChanged: function (delegate) {
        this.get_events().addHandler('taxaRootChoiceChanged', delegate);
    },

    // Unsubscribes a delegate from the radio clicked event.
    remove_radioClicked: function (delegate) {
        this.get_events().removeHandler('radioClicked', delegate);
    },

    /* -------------------- event handlers ------------ */

    // Fires the selection done event.
    _selectionDoneHandler: function (args) {
        var eventArgs = args;
        var h = this.get_events().getHandler('selectionDone');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    // Handles the click event of the create taxon button.
    _createTaxonButtonClicked: function () {
        this._newTaxonDialog._sfArgs = { "uiCulture": this.get_uiCulture() };
        this._newTaxonDialog.show();
        if (jQuery(".ui-dialog:visible")) {
            jQuery(this._newTaxonDialog._popupElement).css("z-index", 150001);
            jQuery(".TelerikModalOverlay:visible").css("z-index", 150000);
        }
        Telerik.Sitefinity.centerWindowHorizontally(this._newTaxonDialog);
    },

    // Handles the click event of the done button.
    _doneButtonClicked: function () {
        var args = { 'selectedTaxa': this.get_selectedTaxa() };
        this._selectionDoneHandler(args);
    },

    // Handles the click event of the root radio button. 
    _rootRadioClicked: function () {
        this._showBinder = false;
        this._configureSelectorMode();
        this._taxaRootChoiceChanged();
    },

    // Handles the click event of the taxa radio button.
    _taxaRadioClicked: function () {
        this._showBinder = true;
        this._configureSelectorMode();
        this._taxaRootChoiceChanged();
    },

    //Handles the click event of any of the radio buttons.
    _taxaRootChoiceChanged: function (sender, eventArgs) {
        var h = this.get_events().getHandler('taxaRootChoiceChanged');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    // Handles the closed event of the new taxon dialog.
    _newTaxonDialogClosed: function (sender, args) {
        if (args.get_argument() != null) {
            var newArgs = { 'selectedTaxa': [args.get_argument()], 'isToAdd': true };
            this._selectionDoneHandler(newArgs);
        }
        this.dataBind();
    },

    /* -------------------- private methods ----------- */
    _setServiceUrls: function () {
        if (!this._bindOnServer) {
            this._taxaTreeBinder.set_orginalServiceBaseUrl(String.format(this._taxaTreeBinder.get_orginalServiceBaseUrl(), this._taxonomyId));
            this._taxaTreeBinder.set_serviceChildItemsBaseUrl(String.format(this._taxaTreeBinder.get_serviceChildItemsBaseUrl(), this._taxonomyId));
            this._taxaTreeBinder.set_servicePredecessorBaseUrl(String.format(this._taxaTreeBinder.get_servicePredecessorBaseUrl(), this._taxonomyId));
        }
    },
    _set_taxaRadioText: function () {
        if (this._controlLoaded && this._taxaRadioLabel)
            this._taxaRadioLabel.innerHTML = this.get_taxaRadioText();
    },
    _set_rootRadioText: function () {
        if (this._controlLoaded && this._rootRadioLabel)
            this._rootRadioLabel.innerHTML = String.format(this._rootRadioTextTemplate, this.get_rootRadioText());
    },
    _set_createTaxonButtonText: function () {
        if (this._controlLoaded) {
            if (this._createTaxonButtonText) {
                this._createTaxonButton.innerHTML = String.format(this._createTaxonButtonTextTemplate, this.get_createTaxonButtonText().htmlEncode());
            } else {
                this._createTaxonButton.innerHTML = String.format(this._createTaxonButtonTextTemplate, '');
            }
        }
    },
    _set_selectorTitleText: function () {
        if (this._controlLoaded)
            this._selectorTitle.innerHTML = String.format(this._selectorTitleTextTemplate, this.get_selectorTitleText().htmlEncode());
    },
    _isToBind: function () {
        if (this._bindOnServer)
            return false;
        else
            return this._showBinder;
    },
    _onDataBound: function () {
        this.toggleNoItemsMessage();
        this._taxaRootChoiceChanged();
    },
    /* -------------------- properties ---------------- */

    // Gets the reference to the element displaying the title of the selector.
    get_selectorTitle: function () {
        return this._selectorTitle;
    },
    // Sets the reference to the element displaying the title of the selector.
    set_selectorTitle: function (value) {
        this._selectorTitle = value;
    },
    // Gets the reference to the element displaying no categories have been created yet message.
    get_labelNoCategories: function () {
        return this._labelNoCategories;
    },
    // Sets the reference to the element displaying no categories have been created yet message.
    set_labelNoCategories: function (value) {
        this._labelNoCategories = value;
    },
    // Gets the reference to the button that opens a create new taxon dialog.
    get_createTaxonButton: function () {
        return this._createTaxonButton;
    },
    // Sets the reference to the button that opens a create new taxon dialog.
    set_createTaxonButton: function (value) {
        this._createTaxonButton = value;
    },
    // Gets the reference to the button that means selection is done.
    get_doneButton: function () {
        return this._doneButton;
    },
    // Sets the reference to the button that means selection is done.
    set_doneButton: function (value) {
        this._doneButton = value;
    },
    // Gets the reference to the radio button that indicates that the root should be selected.
    get_rootRadio: function () {
        return this._rootRadio;
    },
    // Sets the reference to the radio button that indicates that the root should be selected.
    set_rootRadio: function (value) {
        this._rootRadio = value;
    },
    // Gets the reference to the radio button that indicates that the taxon from the taxa should be selected.
    get_taxaRadio: function () {
        return this._taxaRadio;
    },
    // Sets the reference to the radio button that indicates that the taxon from the taxa should be selected.
    set_taxaRadio: function (value) {
        this._taxaRadio = value;
    },
    // Gets the reference for the label of the root radio button.
    get_rootRadioLabel: function () {
        return this._rootRadioLabel;
    },
    // Sets the reference for the label of the root radio button.
    set_rootRadioLabel: function (value) {
        this._rootRadioLabel = value;
    },
    // Gets the reference for the label of the taxa radio radio button.
    get_taxaRadioLabel: function () {
        return this._taxaRadioLabel;
    },
    // Sets the reference for the label of the taxa radio radio button.
    set_taxaRadioLabel: function (value) {
        this._taxaRadioLabel = value;
    },
    // Gets the reference to the rad window component with the dialog for creating new taxon.
    get_newTaxonDialog: function () {
        return this._newTaxonDialog;
    },
    // Sets the reference to the rad window component with the dialog for creating new taxon.
    set_newTaxonDialog: function (value) {
        this._newTaxonDialog = value;
    },
    // Gets the reference to the panel that holds the tree selector.
    get_treePanel: function () {
        return this._treePanel;
    },
    // Sets the reference to the panel that holds the tree selector.
    set_treePanel: function (value) {
        this._treePanel = value;
    },
    // Gets the reference to the client binder component that binds existing taxa.
    get_taxaTreeBinder: function () {
        return this._taxaTreeBinder;
    },
    // Sets the reference to the client binder component that binds existing taxa.
    set_taxaTreeBinder: function (value) {
        this._taxaTreeBinder = value;
    },
    // Gets the id of the taxon that has been selected.
    get_selectedTaxa: function () {
        if ($(this._taxaRadio).is(':checked') || this._allowRootSelection == false) {
            var selectedTaxa = this._taxaTreeBinder.get_selectedItems();
            if (selectedTaxa == null) {
                return [this._taxaTreeBinder.GetEmptyGuid()];
            }
            var selectedTaxaIds = [];
            for (var i = 0; i < selectedTaxa.length; i++) {
                var key = this._taxaTreeBinder._getItemKey(selectedTaxa[i]);
                selectedTaxaIds.push(key);
            }
            return selectedTaxaIds;
        }
        return [this._taxaTreeBinder.GetEmptyGuid()];
    },
    // Gets the value that indicates whether multiple taxons can be selected.
    get_allowMultipleSelection: function () {
        return this._allowMultipleSelection;
    },
    // Sets the value that indicates whether multiple taxons can be selected.
    set_allowMultipleSelection: function (value) {
        this._allowMultipleSelection = value;
    },
    // Gets the reference to the label that displays that no taxa has been created yet.
    get_noTaxaCreatedLabel: function () {
        return this._noTaxaCreatedLabel;
    },
    // Sets the reference to the label that displayes that no taxa has been created yet.
    set_noTaxaCreatedLabel: function (value) {
        this._noTaxaCreatedLabel = value;
    },
    // Gets the taxonomy id.
    get_taxonomyId: function () {
        return this._taxonomyId;
    },
    // Sets the taxonomy id.
    set_taxonomyId: function (value) {
        if (value != this._taxonomyId) {
            this._taxonomyId = value;
            this._isServiceUrlsSet = false;
        }
    },
    // Gets the text of the selector title.
    get_selectorTitleText: function () {
        return this._selectorTitleText;
    },
    // Sets the text of the selector title.
    set_selectorTitleText: function (value) {
        if (value != this._selectorTitleText) {
            this._selectorTitleText = value;
            this._set_selectorTitleText();
        }
    },
    // Gets the text of the create taxon button.
    get_createTaxonButtonText: function () {
        return this._createTaxonButtonText;
    },
    // Sets the text of the create taxon button.
    set_createTaxonButtonText: function (value) {
        if (value != this._createTaxonButtonText) {
            this._createTaxonButtonText = value;
            this._set_createTaxonButtonText();
        }
    },
    // Gets the text of the root radio title.
    get_rootRadioText: function () {
        return this._rootRadioText;
    },
    // Sets the text of the root radio title.
    set_rootRadioText: function (value) {
        if (value != this._rootRadioText) {
            this._rootRadioText = value;
            this._set_rootRadioText();
        }
    },
    // Gets the text of the taxa radio title.
    get_taxaRadioText: function () {
        return this._taxaRadioText;
    },
    // Sets the text of the taxa radio title.
    set_taxaRadioText: function (value) {
        if (value != this._taxaRadioText) {
            this._taxaRadioText = value;
            this.set_taxaRadioText();
        }
    },

    set_selectorTitleTextTemplate: function (value) {
        this._selectorTitleTextTemplate = value;
        this._set_selectorTitleText();
    },
    get_selectorTitleTextTemplate: function () {
        return this._selectorTitleTextTemplate;
    },

    // Gets the culture to use when visualizing a content.
    get_culture: function () {
        return this._taxaTreeBinder.get_culture();
    },
    // Sets the culture to use when visualizing a content.
    set_culture: function (culture) {
        this._taxaTreeBinder.set_culture(culture);
    },

    // Gets the UI culture to use when visualizing a content.
    get_uiCulture: function () {
        return this._taxaTreeBinder.get_uiCulture();
    },
    // Sets the UI culture to use when visualizing a content.
    set_uiCulture: function (culture) {
        this._taxaTreeBinder.set_uiCulture(culture);
    }
};

Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector.registerClass("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical.HierarchicalTaxonSelector", Sys.UI.Control);
