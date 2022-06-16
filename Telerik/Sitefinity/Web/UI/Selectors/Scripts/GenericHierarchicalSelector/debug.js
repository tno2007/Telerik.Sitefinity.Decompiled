Type.registerNamespace("Telerik.Sitefinity.Taxonomies.Web.UI.Hierarchical");

Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector = function (element) {
    this._element = element;
    this._selectorTitle = null;
    this._doneButton = null;
    this._treeView = null;
    this._rootRadio = null;
    this._nodesRadio = null;
    this._rootRadioLabel = null;
    this._nodesRadioLabel = null;
    this._treePanel = null;
    this._treeBinder = null;
    this._noNodesCreatedLabel = null;
    this._noNodesPanel = null;

    this._selectorTitleText = null;
    this._selectRootText = null;
    this._noNodesHaveBeenCreatedText = null;

    this._rootNodeId = null;
    this._allowRootSelection = false;
    this._showDoneSelectingButton = false;
    this._showBinder = false;

    this._rootRadioClickDelegate = null;
    this._nodesRadioClickDelegate = null;
    this._doneButtonClickDelegate = null;
    this._onLoadDelegate = null;
    this._dataBoundDelagete = null;

    this._isServiceUrlsSet = false;
    this._isControlsConfigured = false;

    this._bindOnLoad = true;

    this._rootRadioTextTemplate = null;
    this._selectorTitleTextTemplate = null;

    this._controlLoaded = false;
    Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector.prototype =
{
    /* -------------------- set up and tear down ----------- */

    initialize: function () {
        if (this._doneButtonClickedDelegate == null) {
            this._doneButtonClickedDelegate = Function.createDelegate(this, this._doneButtonClicked);
        }
        if (this._doneButton) {
            $addHandler(this._doneButton, 'click', this._doneButtonClickedDelegate);
        }
        if (this._treeViewNodeClickedDelegate == null) {
            this._treeViewNodeClickedDelegate = Function.createDelegate(this, this._treeViewNodeClicked);
        }
        if (this._treeView) {
            this._treeView.control.add_nodeClicked(this._treeViewNodeClickedDelegate);
        }
        if (this._rootRadioClickDelegate == null) {
            this._rootRadioClickDelegate = Function.createDelegate(this, this._rootRadioClicked);
        }
        if (this._rootRadio) {
            $addHandler(this._rootRadio, 'click', this._rootRadioClickDelegate);
        }

        if (this._nodesRadioClickDelegate == null) {
            this._nodesRadioClickDelegate = Function.createDelegate(this, this._nodesRadioClicked);
        }
        if (this._nodesRadio) {
            $addHandler(this._nodesRadio, 'click', this._nodesRadioClickDelegate);
        }

        this._showBinder = $(this._nodesRadio).is(':checked') || this._allowRootSelection == false;

        if (this._dataBoundDelegate == null)
            this._dataBoundDelagete = Function.createDelegate(this, this._onDataBound);

        Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector.callBaseMethod(this, "initialize");

        Sys.Application.add_load(Function.createDelegate(this, this.onload));
    },

    dispose: function () {
        if (this._rootRadio && this._rootRadioClickDelegate) {
            $removeHandler(this._rootRadio, 'click', this._rootRadioClickDelegate);
        }

        if (this._nodesRadio && this._nodesRadioClickDelegate) {
            $removeHandler(this._nodesRadio, 'click', this._nodesRadioClickDelegate);
        }

        if (this._doneButtonClickDelegate != null) {
            delete this._doneButtonClickDelegate;
        }

        if (this._rootRadioClickDelegate != null) {
            delete this._rootRadioClickDelegate;
        }

        if (this._nodesRadioClickDelegate != null) {
            delete this._nodesRadioClickDelegate;
        }

        if (this._onLoadDelegate != null) {
            delete this._onLoadDelegate;
        }

        if (this._dataBoundDelegate != null)
            delete this._dataBoundDelagete;

        Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector.callBaseMethod(this, "dispose");
    },

    /* --------------------  public methods ----------- */

    onload: function () {
        this._controlLoaded = true;
        this._treeBinder.add_onDataBound(this._dataBoundDelagete);
        this._configureControls();
    },

    // Binds the hierarchical taxon selector to the data.
    dataBind: function (dataKey, context) {
        if (!this._isServiceUrlsSet) {
            this._setServiceUrls();
        }
        if (this._showBinder) {
            this._hideLoadingScreen(true);
            this._treeBinder.DataBind(dataKey, context);
        }
    },

    // Clears all the selected items.
    clearSelection: function () {
        this._treeBinder.clearSelection();
    },

    showTreePanel: function () {
        if (this._treeBinder.get_hasNoData()) {
            $(this._noNodesPanel).show();
        }
        else {
            $(this._treePanel).show();
            if (this._showDoneSelectingButton) {
                $(this._doneButton).show();
            }
            if (this._showCreateNewTaxonButton) {
                $(this._createTaxonButton).show();
            }
        }
    },

    hideTreePanel: function () {
        $(this._noNodesPanel).hide();
        $(this._treePanel).hide();
        if (this._showDoneSelectingButton) {
            $(this._doneButton).hide();
        }
        if (this._showCreateNewTaxonButton) {
            $(this._createTaxonButton).hide();
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

    // Subscribes a delegate to be called when radio clicked event is fired.
    add_nodesRootChoiceChanged: function (delegate) {
        this.get_events().addHandler('nodesRootChoiceChanged', delegate);
    },

    // Unsubscribes a delegate from the radio clicked event.
    remove_nodesRootChoiceChanged: function (delegate) {
        this.get_events().removeHandler('nodesRootChoiceChanged', delegate);
    },

    /* -------------------- event handlers ------------ */

    // Fires the selection done event.
    _selectionDoneHandler: function (args) {
        var eventArgs = args;
        var h = this.get_events().getHandler('selectionDone');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    // Handles the click event of the done button.
    _doneButtonClicked: function () {
        var args = { 'selectedNode': this.get_selectedNode() };
        if (args.selectedNode != null) {
            this._selectionDoneHandler(args);
        }
    },

    // Handles the OnClientNodeClicked event of the treeView.
    _treeViewNodeClicked: function () {
        this._doneButton.classList.remove("sfDisabledLinkBtn");
    },

    // Handles the click event of the root radio button. 
    _rootRadioClicked: function (sender, args) {
        this._showBinder = false;
        this._configureSelectorMode();
        args = { 'selectedNode': this.get_selectedNode(), 'commandName': 'create' };
        this._nodesRootChoiceChanged(sender, args);
    },

    // Handles the click event of the nodes radio button.
    _nodesRadioClicked: function (sender, args) {
        this._doneButton.classList.add("sfDisabledLinkBtn");
        this._showBinder = true;
        this._configureSelectorMode();
        this._nodesRootChoiceChanged();
    },

    //Handles the click event of any of the radio buttons.
    _nodesRootChoiceChanged: function (sender, eventArgs) {
        var h = this.get_events().getHandler('nodesRootChoiceChanged');
        if (h) h(this, eventArgs);
        return eventArgs;
    },

    /* -------------------- private methods ----------- */

    _setServiceUrls: function () {
        this._treeBinder.set_orginalServiceBaseUrl(String.format(this._treeBinder.get_orginalServiceBaseUrl(), this._rootNodeId));
        this._treeBinder.set_serviceChildItemsBaseUrl(String.format(this._treeBinder.get_serviceChildItemsBaseUrl(), this._rootNodeId));
        this._treeBinder.set_servicePredecessorBaseUrl(String.format(this._treeBinder.get_servicePredecessorBaseUrl(), this._rootNodeId));
    },

    //This method hides all the loading screens for specific purposes
    //When you use it make sure to have _hideLoadingScreen(false) on data bound
    _hideLoadingScreen: function (hide) {

        if (jQuery('.RadAjax').length == 0) {
            return;
        }

        jQuery('.RadAjax').each(function (i, el) {
            if (hide) {
                this.style.visibility = 'hidden';
            } else {
                this.style.visibility = 'visible';
            }
        });
    },

    _configureControls: function () {
        this._set_nodesRadioText();
        this._set_rootRadioText();
        this._set_selectorTitleText();

        if (this._showBinder) {
            if (this._bindOnLoad) {
                this.dataBind();
            }
        }
        else {
            this.hideTreePanel();
        }
    },

    _set_nodesRadioText: function () {
        if (this._controlLoaded && this._nodesRadioLabel)
            this._nodesRadioLabel.innerHTML = this.get_nodesRadioText();
    },

    _set_rootRadioText: function () {
        if (this._controlLoaded && this._rootRadioLabel)
            this._rootRadioLabel.innerHTML = String.format(this._rootRadioTextTemplate, this.get_rootRadioText());
    },

    _set_selectorTitleText: function () {
        if (this._controlLoaded)
            this._selectorTitle.innerHTML = String.format(this._selectorTitleTextTemplate, this.get_selectorTitleText());
    },

    _onDataBound: function () {
        this.showTreePanel();
        this._nodesRootChoiceChanged();
        this._hideLoadingScreen(false);
    },

    _configureSelectorMode: function () {
        if (this._showBinder) {
            this.dataBind();
        }
        else {
            this.hideTreePanel();
        }
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

    // Gets the reference to the button that means selection is done.
    get_doneButton: function () {
        return this._doneButton;
    },

    // Sets the reference to the button that means selection is done.
    set_doneButton: function (value) {
        this._doneButton = value;
    },

    // Gets the reference to the treeView.
    get_treeView: function () {
        return this._treeView;
    },

    // Sets the reference to the treeView.
    set_treeView: function (value) {
        this._treeView = value;
    },

    // Gets the reference to the radio button that indicates that the root should be selected.
    get_rootRadio: function () {
        return this._rootRadio;
    },

    // Sets the reference to the radio button that indicates that the root should be selected.
    set_rootRadio: function (value) {
        this._rootRadio = value;
    },

    // Gets the reference to the radio button that indicates that the taxon from the nodes should be selected.
    get_nodesRadio: function () {
        return this._nodesRadio;
    },

    // Sets the reference to the radio button that indicates that the taxon from the nodes should be selected.
    set_nodesRadio: function (value) {
        this._nodesRadio = value;
    },

    // Gets the reference for the label of the root radio button.
    get_rootRadioLabel: function () {
        return this._rootRadioLabel;
    },

    // Sets the reference for the label of the root radio button.
    set_rootRadioLabel: function (value) {
        this._rootRadioLabel = value;
    },

    // Gets the reference for the label of the nodes radio radio button.
    get_nodesRadioLabel: function () {
        return this._nodesRadioLabel;
    },

    // Sets the reference for the label of the nodes radio radio button.
    set_nodesRadioLabel: function (value) {
        this._nodesRadioLabel = value;
    },

    // Gets the reference to the panel that holds the tree selector.
    get_treePanel: function () {
        return this._treePanel;
    },

    // Sets the reference to the panel that holds the tree selector.
    set_treePanel: function (value) {
        this._treePanel = value;
    },

    // Gets the reference to the client binder component that binds existing nodes.
    get_treeBinder: function () {
        return this._treeBinder;
    },

    // Sets the reference to the client binder component that binds existing nodes.
    set_treeBinder: function (value) {
        this._treeBinder = value;
    },

    // Gets the taxon that has been selected.
    get_selectedNode: function () {
        if ($(this._nodesRadio).is(':checked') || this._allowRootSelection == false) {
            var selectedNodes = this._treeBinder.get_selectedItems();
            if (selectedNodes == null || selectedNodes.length == 0) {
                return null;
            }
            return selectedNodes[0];
        }
        return null;
    },

    // Gets the reference to the label that displays that no nodes has been created yet.
    get_noNodesCreatedLabel: function () {
        return this._noNodesCreatedLabel;
    },

    // Sets the reference to the label that displayes that no nodes has been created yet.
    set_noNodesCreatedLabel: function (value) {
        this._noNodesCreatedLabel = value;
    },

    // Gets the taxonomy id.
    get_rootNodeId: function () {
        return this._rootNodeId;
    },

    // Sets the taxonomy id.
    set_rootNodeId: function (value) {
        if (value != this._rootNodeId) {
            this._rootNodeId = value;
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

    // Gets the text of the nodes radio title.
    get_nodesRadioText: function () {
        return this._nodesRadioText;
    },

    // Sets the text of the nodes radio title.
    set_nodesRadioText: function (value) {
        if (value != this._nodesRadioText) {
            this._nodesRadioText = value;
            this._set_nodesRadioText();
        }
    },

    get_showBinder: function () {
        return this._showBinder;
    },

    set_showBinder: function (value) {
        this._showBinder = value;
    },

    get_bindOnLoad: function () {
        return this._bindOnLoad;
    },

    set_bindOnLoad: function (value) {
        this._bindOnLoad = value;
    },

    get_noNodesPanel: function () {
        return this._noNodesPanel;
    },
    set_noNodesPanel: function (value) {
        this._noNodesPanel = value;
    }
};

Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector.registerClass("Telerik.Sitefinity.Web.UI.GenericHierarchicalSelector", Sys.UI.Control);