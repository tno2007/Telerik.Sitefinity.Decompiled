Type.registerNamespace("Telerik.Sitefinity.Workflow.UI");

Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView = function (element) {
    Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView.initializeBase(this, [element]);
    this._parentPageSelector = null;
    this._pagesSelector = null;
    this._selectedPagesBuilder = null;

    this._allPagesRadio = null;
    this._parentPageRadio = null;
    this._customPagesRadio = null;
    this._parentPageSelectionContainer = null;
    this._pagesSelectionContainer = null;
    this._selectParentPageLink = null;
    this._selectPagesLink = null;
    this._selectedParentPageLabel = null;
    this._selectedPagesLabel = null;
    this._parentPageSelectorWrapper = null;
    this._pagesSelectorWrapper = null;
    this._parentPageRadioLabel = null;

    this._changeResource = null;
    this._selectParentResource = null;
    this._addOtherPagesResource = null;
    this._allPagesUnderResource = null;
    this._allPagesUnderParticularParentPageResource = null;

    this._radioClickedDelegate = null;
    this._showParentPageSelectorDelegate = null;
    this._showPagesSelectorDelegate = null;
    this._parentPageSelectedDelegate = null;
    this._pagesSelectedDelegate = null;

    this._dataItem = null;
}

Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView.prototype = {

    // set up 
    initialize: function () {
        Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView.callBaseMethod(this, "initialize");

        this._radioClickedDelegate = Function.createDelegate(this, this._radioClicked);
        $addHandler(this._allPagesRadio, 'click', this._radioClickedDelegate);
        $addHandler(this._parentPageRadio, 'click', this._radioClickedDelegate);
        $addHandler(this._customPagesRadio, 'click', this._radioClickedDelegate);

        this._showParentPageSelectorDelegate = Function.createDelegate(this, this._showParentPageSelector);
        $addHandler(this._selectParentPageLink, "click", this._showParentPageSelectorDelegate);

        this._showPagesSelectorDelegate = Function.createDelegate(this, this._showPagesSelector);
        $addHandler(this._selectPagesLink, "click", this._showPagesSelectorDelegate);

        this._parentPageSelectedDelegate = Function.createDelegate(this, this._parentPageSelected);
        this._parentPageSelector.add_doneClientSelection(this._parentPageSelectedDelegate);

        this._pagesSelectedDelegate = Function.createDelegate(this, this._pagesSelected);
        this._pagesSelector.add_doneClientSelection(this._pagesSelectedDelegate);

        jQuery(this._parentPageSelectionContainer).hide();
        jQuery(this._pagesSelectionContainer).hide();
    },

    // tear down
    dispose: function () {
        if (this._radioClickedDelegate) {
            if (this._allPagesRadio) {
                $removeHandler(this._allPagesRadio, 'click', this._radioClickedDelegate);
            }
            if (this._parentPageRadio) {
                $removeHandler(this._parentPageRadio, 'click', this._radioClickedDelegate);
            }
            if (this._customPagesRadio) {
                $removeHandler(this._customPagesRadio, 'click', this._radioClickedDelegate);
            }
            delete this._radioClickedDelegate;
        }
        if (this._showParentPageSelectorDelegate) {
            if (this._selectParentPageLink) {
                $removeHandler(this._selectParentPageLink, "click", this._showParentPageSelectorDelegate);
            }
            delete this._showParentPageSelectorDelegate;
        }
        if (this._showPagesSelectorDelegate) {
            if (this._selectPagesLink) {
                $removeHandler(this._selectPagesLink, "click", this._showPagesSelectorDelegate);
            }
            delete this._showPagesSelectorDelegate;
        }
        Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView.callBaseMethod(this, "dispose");
    },

    /* ************** public methods **************** */

    getDataItem: function () {
        return this._dataItem;
    },

    reset: function () {
        this._allPagesRadio.click();
        this._configureCustomPagesSelection([]);
        this._configureParentPageSelection(null);
    },

    /* ************** private methods **************** */

    _radioClicked: function (sender, args) {
        switch (sender.target) {
            case this._allPagesRadio:
                jQuery(this._parentPageSelectionContainer).hide();
                this.get_parentPageRadioLabel().html(this._allPagesUnderParticularParentPageResource);
                jQuery(this._pagesSelectionContainer).hide();
                break;
            case this._parentPageRadio:
                jQuery(this._parentPageSelectionContainer).show();
                if (jQuery(this._parentPageSelectionContainer).is(':visible')) {
                    this.get_parentPageRadioLabel().html(this._allPagesUnderResource);
                }
                jQuery(this._pagesSelectionContainer).hide();
                break;
            case this._customPagesRadio:
                jQuery(this._parentPageSelectionContainer).hide();
                this.get_parentPageRadioLabel().html(this._allPagesUnderParticularParentPageResource);
                jQuery(this._pagesSelectionContainer).show();
                break;
        }
        dialogBase.resizeToContent();
    },

    // shows the parent page selector
    _showParentPageSelector: function () {
        jQuery(this._parentPageSelectorWrapper).show();
        dialogBase.resizeToContent();
    },

    // shows the pages selector
    _showPagesSelector: function () {
        jQuery(this._pagesSelectorWrapper).show();
        dialogBase.resizeToContent();
    },

    // event handler for parent page selected event    
    _parentPageSelected: function (items) {
        jQuery(this._parentPageSelectorWrapper).hide();

        if (items == null)
            return;

        var selectedPage = this.get_parentPageSelector().get_selectedPage();
        if (selectedPage != null) {
            this._configureParentPageSelection(selectedPage);
            this._dataItem = { Parent: selectedPage };
        }
        dialogBase.resizeToContent();
    },


    // event handler for pages selected event
    _pagesSelected: function (items, cancel) {
        jQuery(this._pagesSelectorWrapper).hide();

        if (cancel == true) return;

        //Create an array with the newly selected items while preserving order of previously selected items.
        var newSelections = [];
        var oldSelections = this.get_selectedCustomPages();
        for (var i = 0; i < oldSelections.length; i++) {
            var oldItem = oldSelections[i];
            for (var j = 0; j < items.length; j++) {
                var newItem = items[j];
                if (newItem.Id && oldItem.Id && newItem.Id == oldItem.Id) {
                    newSelections.push(newItem);
                    items.splice(j, 1);
                    break;
                }
                else if (newItem.Url && oldItem.Url && newItem.Url == oldItem.Url) {
                    newSelections.push(newItem);
                    items.splice(j, 1);
                    break;
                }
            }
        }
        for (var i = 0; i < items.length; i++) {
            var newItem = items[i];
            newSelections.push(newItem);
        }
        this._configureCustomPagesSelection(newSelections);
        this._dataItem = { Pages: newSelections };
        dialogBase.resizeToContent();
    },

    _configureParentPageSelection: function (parent) {
        if (parent) {
            this.get_parentPageRadioLabel().html(this._allPagesUnderResource);
            this._selectedParentPageLabel.innerHTML = parent.Title;
            jQuery(this._selectedParentPageLabel).show();
            this._selectParentPageLink.innerHTML = this._changeResource;
        }
        else {
            this.get_parentPageRadioLabel().html(this._allPagesUnderParticularParentPageResource);
            this._selectedParentPageLabel.innerHTML = "";
            jQuery(this._selectedParentPageLabel).hide();
            this._selectParentPageLink.innerHTML = this._selectParentResource;
        }
    },

    _configureCustomPagesSelection: function (pages) {
        if (pages.length > 0) {
            jQuery(this._selectedPagesLabel).hide();
        } else {
            jQuery(this._selectedPagesLabel).show();
        }

        this.set_selectedCustomPages(pages);
        this._selectPagesLink.innerHTML = this._addOtherPagesResource;
    },

    /* ************** properties ********************* */

    get_parentPageSelector: function () {
        return this._parentPageSelector;
    },
    set_parentPageSelector: function (value) {
        this._parentPageSelector = value;
    },

    get_pagesSelector: function () {
        return this._pagesSelector;
    },
    set_pagesSelector: function (value) {
        this._pagesSelector = value;
    },

    get_selectedPagesBuilder: function () {
        return this._selectedPagesBuilder;
    },
    set_selectedPagesBuilder: function (value) {
        this._selectedPagesBuilder = value;
    },

    get_allPagesRadio: function () {
        return this._allPagesRadio;
    },
    set_allPagesRadio: function (value) {
        this._allPagesRadio = value;
    },

    get_parentPageRadio: function () {
        return this._parentPageRadio;
    },
    set_parentPageRadio: function (value) {
        this._parentPageRadio = value;
    },

    get_customPagesRadio: function () {
        return this._customPagesRadio;
    },
    set_customPagesRadio: function (value) {
        this._customPagesRadio = value;
    },

    get_parentPageSelectionContainer: function () {
        return this._parentPageSelectionContainer;
    },
    set_parentPageSelectionContainer: function (value) {
        this._parentPageSelectionContainer = value;
    },

    get_pagesSelectionContainer: function () {
        return this._pagesSelectionContainer;
    },
    set_pagesSelectionContainer: function (value) {
        this._pagesSelectionContainer = value;
    },

    get_selectParentPageLink: function () {
        return this._selectParentPageLink;
    },
    set_selectParentPageLink: function (value) {
        this._selectParentPageLink = value;
    },

    get_selectPagesLink: function () {
        return this._selectPagesLink;
    },
    set_selectPagesLink: function (value) {
        this._selectPagesLink = value;
    },

    get_selectedParentPageLabel: function () {
        return this._selectedParentPageLabel;
    },
    set_selectedParentPageLabel: function (value) {
        this._selectedParentPageLabel = value;
    },

    get_selectedPagesLabel: function () {
        return this._selectedPagesLabel;
    },
    set_selectedPagesLabel: function (value) {
        this._selectedPagesLabel = value;
    },

    get_parentPageSelectorWrapper: function () {
        return this._parentPageSelectorWrapper;
    },
    set_parentPageSelectorWrapper: function (value) {
        this._parentPageSelectorWrapper = value;
    },

    get_pagesSelectorWrapper: function () {
        return this._pagesSelectorWrapper;
    },
    set_pagesSelectorWrapper: function (value) {
        this._pagesSelectorWrapper = value;
    },

    get_selectedCustomPages: function () {
        return this.get_selectedPagesBuilder().get_choiceItems();
    },
    set_selectedCustomPages: function (value) {
        this.get_selectedPagesBuilder().set_choiceItems(value);
        this._selectedCustomPagesChanged = true;
    },

    get_parentPageRadioLabel: function () {
        if (this._parentPageRadioLabel == null) {
            this._parentPageRadioLabel = jQuery('[for=' + this._parentPageRadio.id + ']');
        }
        return this._parentPageRadioLabel;
    }
};

Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView.registerClass('Telerik.Sitefinity.Workflow.UI.PagesWorkflowScopeView', Sys.UI.Control);