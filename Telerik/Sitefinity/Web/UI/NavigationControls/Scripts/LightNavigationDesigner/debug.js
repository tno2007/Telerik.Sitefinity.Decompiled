Type.registerNamespace("Telerik.Sitefinity.Web.UI.NavigationControls");

Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner = function (element) {
    this._element = element;
    Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner.initializeBase(this, [element]);

    //delegates
    this._pageSelectedDelegate = null;
    this._pagesSelectedDelegate = null;
    this._pageReorderedDelegate = null;
    this._pageItemsChangedDelegate = null;
    this._showPageSelectorDelegate = null;
    this._showMultiPageSelectorDelegate = null;
    this._selectionModeDelegate = null;

    //buttons
    this._pageSelectButton = null;
    this._multiPageSelectButton = null;
    //labels
    this._selectedPageLabel = null
    this._multiPageSelectedPageLabel = null
    //others elements
    this._pageSelector = null;
    this._pagesSelector = null;
    this._customSelectedPagesControl = null;
    this._selectorTag = null;
    this._pagesSelectorTag = null;
    this._cssClassTextField = null;
    this._levelsToIncludeSelect = null;

    this._elementsCache = new Object();
    this._selectedPagesArrayChanged = false;

    //properties
    this._topLevelPageNames = "";    

    //flags
    this._refreshMode = false;

    //workflow
    this._currentNavigationMode = null;

    this._clientLabelManager = null;

    this._navigationTemplatesSelector = null;
    this._templatesLegendTooltipLink = null;
    this._templatesLegendTooltip = null;
    this._templatesLegendTooltipLinkClickDelegate = null;
}

Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner.callBaseMethod(this, 'initialize');

        //hook Hierarchy selection choices
        this._selectionModeDelegate = Function.createDelegate(this, this._clickedSelectionMode);
        this._setRadioClickHandler("SelectionMode", this._selectionModeDelegate);

        //hook Page selector
        this._showPageSelectorDelegate = Function.createDelegate(this, this._showPageSelector);
        $addHandler(this.get_pageSelectButton(), "click", this._showPageSelectorDelegate);

        //hook multi Page selector
        this._showMultiPageSelectorDelegate = Function.createDelegate(this, this._showMultiPageSelector);
        $addHandler(this.get_multiPageSelectButton(), "click", this._showMultiPageSelectorDelegate);

        //hook Page selector
        this._pageSelectedDelegate = Function.createDelegate(this, this._pageSelected);
        this.get_pageSelector().add_doneClientSelection(this._pageSelectedDelegate);

        //hook Pages selector
        this._pagesSelectedDelegate = Function.createDelegate(this, this._pagesSelected);
        this.get_pagesSelector().add_doneClientSelection(this._pagesSelectedDelegate);

        //hook custom page reordering event
        this._pageReorderedDelegate = Function.createDelegate(this, this._pageReorderedHandler);
        this.get_customSelectedPagesControl().add_itemReordered(this._pageReorderedDelegate);

        //hook custom page delete event
        this._pageItemsChangedDelegate = Function.createDelegate(this, this._pageItemsChangedHandler);
        this.get_customSelectedPagesControl().add_itemsChanged(this._pageItemsChangedDelegate);

        //telmpaltes tooltip click event
        this._templatesLegendTooltipLinkClickDelegate = Function.createDelegate(this, this._templatesLegendTooltipLinkClickHandler);
        $addHandler(this.get_templatesLegendTooltipLink(), "click", this._templatesLegendTooltipLinkClickDelegate);
        
        this._pageDialog = jQuery(this._selectorTag).dialog({
            autoOpen: false,
            modal: false,
            width: 355,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });

        this._pagesDialog = jQuery(this._pagesSelectorTag).dialog({
            autoOpen: false,
            modal: false,
            width: 355,
            closeOnEscape: true,
            resizable: false,
            draggable: false,
            classes: {
                "ui-dialog": "sfZIndexL"
            }
        });        
    },

    dispose: function () {
        if (this._pageSelectedDelegate) {
            if (this.get_pageSelector())
                this.get_pageSelector().remove_doneClientSelection(this._pageSelectedDelegate);

            delete this._pageSelectedDelegate;
        }

        if (this._pagesSelectedDelegate) {
            if (this.get_pagesSelector())
                this.get_pagesSelector().remove_doneClientSelection(this._pagesSelectedDelegate);

            delete this._pagesSelectedDelegate;
        }

        if (this._pageReorderedDelegate) {
            if (this.get_customSelectedPagesControl())
                this.get_customSelectedPagesControl().remove_itemReordered(this._pageReorderedDelegate);

            delete this._pageReorderedDelegate;
        }

        if (this._pageItemsChangedDelegate) {
            if (this.get_customSelectedPagesControl())
                this.get_customSelectedPagesControl().remove_itemsChanged(this._pageItemsChangedDelegate);

            delete this._pageItemsChangedDelegate;
        }

        if (this._showPageSelectorDelegate) {
            if (this.get_pageSelectButton())
                $removeHandler(this.get_pageSelectButton(), "click", this._showPageSelectorDelegate);

            delete this._showPageSelectorDelegate;
        }

        if (this._showMultiPageSelectorDelegate) {
            if (this.get_multiPageSelectButton())
                $removeHandler(this.get_multiPageSelectButton(), "click", this._showMultiPageSelectorDelegate);

            delete this._showMultiPageSelectorDelegate;
        }

        if (this._selectionModeDelegate)
            delete this._selectionModeDelegate;

        if (this._templatesLegendTooltipLinkClickDelegate) {
            if (this.get_templatesLegendTooltipLink()) {
                $removeHandler(this.get_templatesLegendTooltipLink(), "click", this._templatesLegendTooltipLinkClickDelegate);
            }
            delete this._templatesLegendTooltipLinkClickDelegate;
        }
   

        Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner.callBaseMethod(this, 'dispose');
    },

    /* ----------------------------- public functions ----------------------------- */

    // forces the designer to refresh the UI from the cotnrol Data
    refreshUI: function () {
        this._refreshMode = true;
        var data = this.get_controlData();

        this.get_cssClassTextField().set_value(data.CssClass);
        jQuery(this.get_levelsToIncludeSelect()).val(data.LevelsToInclude);
        var selectedPages = Sys.Serialization.JavaScriptSerializer.deserialize(data.CustomSelectedPages);
        if (!selectedPages) selectedPages = [];
        this.set_selectedPagesArray(selectedPages);
        this._selectedPagesArrayChanged = false;

        this.get_pagesSelector().get_extPagesSelector().set_selectedItems(selectedPages);

        //Set selected pages in selected pages list
        this.get_customSelectedPagesControl().set_choiceItems(selectedPages);
        if (selectedPages.length > 0) {
            jQuery(this.get_multiPageSelectedPageLabel()).hide();
        } else {
            jQuery(this.get_multiPageSelectedPageLabel()).show();
        }

        this._clickRadioChoice("SelectionMode", data.SelectionMode);
        this.get_topLevelPagesLabel().innerHTML = this._topLevelPageNames;

        if (this.get_controlData().SelectedPageTitle != null && this.get_controlData().SelectedPageTitle.length > 0) {
            this.get_selectedPageLabel().innerHTML = data.SelectedPageTitle;
            jQuery(this.get_selectedPageLabel()).show();
        }

        var dialog = dialogBase.get_radWindow();
        dialog.setSize(660, 670);

        this.get_navigationTemplatesSelector().refreshUI();
        this._refreshMode = false;
    },

    // forces the designer to apply the changes on UI to the control Data
    applyChanges: function () {
        this._validate();

        var controlData = this.get_controlData();

        controlData.CssClass = this.get_cssClassTextField().get_value();
        controlData.LevelsToInclude = jQuery(this.get_levelsToIncludeSelect()).val();
        if (this._selectedPagesArrayChanged == true) {
            var selectedPages = this.get_selectedPagesArray();
            for (var i = 0; i < selectedPages.length; i++) {
                var page = selectedPages[i];
                page.__type = controlData.SelectedPageQualifiedName;
            }
            controlData.CustomSelectedPages = Sys.Serialization.JavaScriptSerializer.serialize(selectedPages)
        }        

        this.get_navigationTemplatesSelector().applyChanges();
    },

    /* ----------------------------- private functions ----------------------------- */
    _validate: function () {
        if (!this.get_cssClassTextField().validate()) {
            throw "Validation error!";
        }
    },

    // this is an event handler for page selected event    
    _pageSelected: function (items) {

        //jQuery(this.get_selectorTag()).hide();
        this._pageDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (items == null)
            return;

        var selectedItem = this.get_pageSelector().getSelectedItems()[0];
        if (selectedItem) {
            this.get_selectedPageLabel().innerHTML = selectedItem.Title;
            jQuery(this.get_selectedPageLabel()).show();
            this.get_pageSelectButton().innerHTML = this.get_clientLabelManager().getLabel("Labels", "ChangePageButton");
            this.get_controlData().StartingNodeUrl = selectedItem.Id;
        }
    },

    // this is an event handler for pages selected event
    _pagesSelected: function (items, cancel) {
        //jQuery(this.get_pagesSelectorTag()).hide();
        this._pagesDialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();

        if (cancel == true) return;

        //Create new selected items array while preserving order of previously selected items.
        var newSelectedArray = [];
        var oldSelectedArray = this.get_selectedPagesArray();
        for (var i = 0; i < oldSelectedArray.length; i++) {
            var oldItem = oldSelectedArray[i];
            for (var j = 0; j < items.length; j++) {
                var newItem = items[j];
                if (newItem.Id && oldItem.Id && newItem.Id == oldItem.Id) {
                    newSelectedArray.push(newItem);
                    items.splice(j, 1);
                    break;
                } else if (newItem.Url && oldItem.Url && newItem.Url == oldItem.Url) {
                    newSelectedArray.push(newItem);
                    items.splice(j, 1);
                    break;
                }
            }
        }

        for (var i = 0; i < items.length; i++) {
            var newItem = items[i];
            newSelectedArray.push(newItem);
        }

        if (newSelectedArray.length > 0) {
            jQuery(this.get_multiPageSelectedPageLabel()).hide();
        } else {
            jQuery(this.get_multiPageSelectedPageLabel()).show();
        }

        this.set_selectedPagesArray(newSelectedArray);
        this.get_pageSelectButton().innerHTML = this.get_clientLabelManager().getLabel("Labels", "ChangeSelection");
    },

    _pageReorderedHandler: function (sender, args) {
        this._selectedPagesArrayChanged = true;
    },

    _pageItemsChangedHandler: function (sender, args) {
        this._selectedPagesArrayChanged = true;
    },

    //utility method to set radio group click handler
    _setRadioClickHandler: function (groupName, delegate) {
        jQuery(this.get_element()).find("input[name='" + groupName + "']").click(delegate)
    },

    //utility method to to click a radio group option
    _clickRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").click();
    },

    _clickedSelectionMode: function (sender) {
        this._showPageSelectors(sender.target.value);

        if (!this._refreshMode) {
            this.get_controlData().SelectionMode = sender.target.value;
        }
    },

    // shows the page selector
    _showPageSelector: function () {
        var selectedItem = this.get_controlData().StartingNodeUrl;
        if (selectedItem) {
            this.get_pageSelector().setSelectedItems([{ Id: selectedItem }]);
        }
        //jQuery(this.get_selectorTag()).show();
        this._pageDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    // shows the page selector
    _showMultiPageSelector: function () {
        var selectedItems = this.get_controlData().CustomSelectedPages;
        if (selectedItems) {
            this.get_pagesSelector().setSelectedItems(JSON.parse(selectedItems));
        }
        this._pagesDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _showPageSelectors: function (value) {
        jQuery([
            this.get_topLevelPagesLabel(),
            this.get_pageSelectZone(),
            this.get_multiPageSelectZone()
        ])
        .hide();

        switch (value) {
            case "TopLevelPages":
                //jQuery(this.get_topLevelPagesLabel()).show();
                break;
            case "SelectedPageChildren":
                jQuery(this.get_pageSelectZone()).show();
                break;
            case "SelectedPages":
                jQuery(this.get_multiPageSelectZone()).show();
                break;
            case "CurrentPageChildren":
                break;
            case "CurrentPageSiblings":
                break;
            case "CustomSelection":
                break;
            default:
                break;
        }

        dialogBase.resizeToContent();
    },

    _templatesLegendTooltipLinkClickHandler: function (sender, args) {
        jQuery(this.get_templatesLegendTooltip()).toggle();
        dialogBase.resizeToContent();
    },

    /* ----------------------------- properties ----------------------------- */

    //get the page selector open button
    get_pageSelectButton: function () {
        if (this._pageSelectButton == null) {
            this._pageSelectButton = this.findElement("pageSelectButton");
        }
        return this._pageSelectButton;
    },

    //gets the selected page label
    get_selectedPageLabel: function () {
        if (this._selectedPageLabel == null) {
            this._selectedPageLabel = this.findElement("selectedPageLabel");
        }
        return this._selectedPageLabel;
    },

    //gets the selected page label
    get_multiPageSelectedPageLabel: function () {
        if (this._multiPageSelectedPageLabel == null) {
            this._multiPageSelectedPageLabel = this.findElement("multiPageSelectedLabes");
        }
        return this._multiPageSelectedPageLabel;
    },

    get_multiPageSelectButton: function () {
        if (this._multiPageSelectButton == null) {
            this._multiPageSelectButton = this.findElement("multiplePageSelector");
        }
        return this._multiPageSelectButton;
    },

    //get the selected page info zone
    get_pageSelectZone: function () {
        if (this._pageSelectZone == null) {
            this._pageSelectZone = this.findElement("pageSelectZone");
        }
        return this._pageSelectZone;
    },

    //get the multi selected page info zone
    get_multiPageSelectZone: function () {
        if (this._multiPageSelectZone == null) {
            this._multiPageSelectZone = this.findElement("multiPageSelectionZone");
        }
        return this._multiPageSelectZone;
    },

    //get the page selector component
    get_pageSelector: function () {
        return this._pageSelector;
    },
    //sets the page selector component
    set_pageSelector: function (val) {
        this._pageSelector = val;
    },

    //get the pages selector component
    get_pagesSelector: function () {
        return this._pagesSelector;
    },
    //sets the pages selector component
    set_pagesSelector: function (val) {
        this._pagesSelector = val;
    },

    //gets the label to show the current top pages
    get_topLevelPagesLabel: function () {
        if (this._topLevelPagesLabel == null)
            this._topLevelPagesLabel = this.findElement("topLevelPagesLabel");
        return this._topLevelPagesLabel;
    },

    get_navigationTemplatesSelector: function () {
        return this._navigationTemplatesSelector;
    },

    set_navigationTemplatesSelector: function (value) {
        this._navigationTemplatesSelector = value;
    },

    findElement: function (id) {
        if (typeof (this._elementsCache[id]) !== 'undefined')
            return this._elementsCache[id];
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        this._elementsCache[id] = result;
        return result;
    },

    // Gets the control that shows and reorders selected pages
    get_customSelectedPagesControl: function () {
        return this._customSelectedPagesControl;
    },
    // Sets the control that shows and reorders selected pages
    set_customSelectedPagesControl: function (value) {
        this._customSelectedPagesControl = value;
    },

    get_selectedPagesArray: function () {
        return this.get_customSelectedPagesControl().get_choiceItems();
    },
    set_selectedPagesArray: function (value) {
        this.get_customSelectedPagesControl().set_choiceItems(value);
        this._selectedPagesArrayChanged = true;
    },

    //gets the page selector wrapping element(use to hide and show the selector)
    get_selectorTag: function () {
        return this._selectorTag;
    },
    set_selectorTag: function (value) {
        this._selectorTag = value;
    },

    //gets the page selector wrapping element(use to hide and show the selector)
    get_pagesSelectorTag: function () {
        return this._pagesSelectorTag;
    },
    set_pagesSelectorTag: function (value) {
        this._pagesSelectorTag = value;
    },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    },

    get_templatesLegendTooltipLink: function () {
        return this._templatesLegendTooltipLink;
    },
    set_templatesLegendTooltipLink: function (value) {
        this._templatesLegendTooltipLink = value;
    },

    get_templatesLegendTooltip: function () {
        return this._templatesLegendTooltip;
    },
    set_templatesLegendTooltip: function (value) {
        this._templatesLegendTooltip = value;
    },

    get_cssClassTextField: function () {
        return this._cssClassTextField;
    },
    set_cssClassTextField: function (value) {
        this._cssClassTextField = value;
    },

    get_levelsToIncludeSelect: function () {
        return this._levelsToIncludeSelect;
    },
    set_levelsToIncludeSelect: function (value) {
        this._levelsToIncludeSelect = value;
    }
}

Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner.registerClass('Telerik.Sitefinity.Web.UI.NavigationControls.LightNavigationDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);