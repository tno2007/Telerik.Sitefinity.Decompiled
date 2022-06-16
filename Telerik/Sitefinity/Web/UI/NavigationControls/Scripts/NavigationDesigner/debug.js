Type.registerNamespace("Telerik.Sitefinity.Web.UI.NavigationControls");

Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner = function (element) {

    //delegates

    this._pageSelectedDelegate = null;
    this._pagesSelectedDelegate = null;
    this._pageReorderedDelegate = null;
    this._pageItemsChangedDelegate = null;
    this._multiPageSelectedDelegate = null;
    this._showPageSelectorDelegate = null;
    this._showMultiPageSelectorDelegate = null;
    this._navigationModeDelegate = null;
    this._selectionModeDelegate = null;
    this._navigationActionDelegate = null;
    this._maxDataBindDepthModeDelegate = null
    this._levelsToExpandModeDelegate = null;
    this._headingsModeDelegate = null;
    this._toogleDesignSettingsDelegate = null;
    this._expandAllCheckBoxDelegate = null;
    //buttons
    this._pageSelectButton = null;
    this._multiPageSelectButton = null;
    //textboxes
    this._expandLevelsTextBox = null;
    this._bindingLevelsTextBox = null;
    this._customTemplateTextBox = null;
    this._skinTextBox = null;
    this._allowCollapsingCheckBox = null;
    this._expandAllCheckBox = null;
    //labels
    this._selectedPageLabel = null
    this._multiPageSelectedPageLabel = null
    //others elements
    this._pageSelectZone = null;
    this._multiPageSelectZone = null;
    this._currentPageSelectZone = null;
    this._pageSelector = null;
    this._pagesSelector = null;
    this._customSelectedPagesControl = null;

    //    this._multiPageSelector = null;
    this._element = null;
    this._controlData = null;
    this._headingsCheckBox = null;

    this._pageDialog = null;
    this._pagesDialog = null;
    this._selectorTag = null;
    this._pagesSelectorTag = null;
    //    this._multiSelectorTag = null;
    this._customPagesSelector = null;

    this._elementsCache = new Object();

    //    //Contains the selected pages as an array (SelectedPage objects)
    //    this._selectedPagesArray = null;
    this._selectedPagesArrayChanged = false;

    //messages
    this._topLevelPageNames = "";

    //flags
    this._refreshMode = false;

    //workflow
    this._currentNavigationMode = null;
    this._selectionHeadingLabels;

    this._clientLabelManager = null;

    Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner.initializeBase(this, [element]);

}

Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner.prototype = {

    initialize: function () {
        Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner.callBaseMethod(this, 'initialize');

        //hook Navigation modes
        this._navigationModeDelegate = Function.createDelegate(this, this._clickedNavigationMode);
        this._setRadioClickHandler("NavigationMode", this._navigationModeDelegate);

        //hook Hierarchy selection choices
        this._selectionModeDelegate = Function.createDelegate(this, this._clickedSelectionMode);
        this._setRadioClickHandler("SelectionMode", this._selectionModeDelegate);

        //hook Page selector
        this._showPageSelectorDelegate = Function.createDelegate(this, this._showPageSelector);
        $addHandler(this.get_pageSelectButton(), "click", this._showPageSelectorDelegate);

        //hook multi Page selector
        this._showMultiPageSelectorDelegate = Function.createDelegate(this, this._showMultiPageSelector);
        $addHandler(this.get_multiPageSelectButton(), "click", this._showMultiPageSelectorDelegate);


        //hook Open drop down click choices
        this._navigationActionDelegate = Function.createDelegate(this, this._clickedNavigationAction);
        this._setRadioClickHandler("NavigationAction", this._navigationActionDelegate);

        //hook expand levels choices
        this._levelsToExpandModeDelegate = Function.createDelegate(this, this._clickedLevelsToExpandMode);
        this._setRadioClickHandler("LevelsToExpandMode", this._levelsToExpandModeDelegate);


        //hook include levels choices
        this._maxDataBindDepthModeDelegate = Function.createDelegate(this, this._clickedMaxDataBindDepthMode);
        this._setRadioClickHandler("MaxDataBindDepthMode", this._maxDataBindDepthModeDelegate);

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

        this._headingsModeDelegate = Function.createDelegate(this, this._clickedHeadingsMode);
        $addHandler(this.get_headingsCheckBox(), "click", this._headingsModeDelegate);

        this._expandAllCheckBoxDelegate = Function.createDelegate(this, this._clickedExpandAllCheckBox);
        $addHandler(this.get_expandAllCheckBox(), "click", this._expandAllCheckBoxDelegate);


        this._toogleDesignSettingsDelegate = Function.createDelegate(this, function () {
            $("#groupDesignSettings").toggleClass("sfExpandedSection");
            dialogBase.resizeToContent();
        });

        jQuery("#expanderDesignSettings").click(this._toogleDesignSettingsDelegate);

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
        Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner.callBaseMethod(this, 'dispose');
        this._pageSelectedDelegate = null;
        this._pagesSelectedDelegate = null;
        this._pageReorderedDelegate = null;
        this._showPageSelectorDelegate = null;
        this._navigationModeDelegate = null;
        this._selectionModeDelegate = null;
        this._navigationActionDelegate = null;
        this._maxDataBindDepthModeDelegate = null
        this._levelsToExpandModeDelegate = null;
        this._headingsModeDelegate = null;
        this._toogleDesignSettingsDelegate = null;

        if (this.get_customSelectedPagesControl()) {
            this.get_customSelectedPagesControl().remove_itemsChanged(this._pageReorderedDelegate);
            delete this._pageReorderedDelegate;
        }
    },

    /* ----------------------------- properties ----------------------------- */

    get_parentPageCheckBox: function () {
        var mode = this.get_controlData().SelectionMode;
        var parentPageCheckBox;

        switch (mode) {
            case "SelectedPageChildren":
            case "CurrentPageChildren":
            case "CurrentPageSiblings":
                {
                    parentPageCheckBox = jQuery(".sfShowParentPage", this.get_element())
                                           .filter("[id*='" + mode + "']")
                                           .get(0);
                }
                break;
            default:
                parentPageCheckBox = null;
                break;

        }

        return parentPageCheckBox;
    },

    get_groupPageNavigationCheckBox: function () {
        return jQuery(this.get_element()).find("#cbGroupPageNavigation").get(0);
    },

    //get the page selector open button
    get_pageSelectButton: function () {
        if (this._pageSelectButton == null) {
            this._pageSelectButton = this.findElement("pageSelectButton");
        }
        return this._pageSelectButton;
    },
    //get the multi page selector open button
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

    //get the current page selected info zone
    get_currentPageSelectZone: function () {
        if (this._currentPageSelectZone == null) {
            this._currentPageSelectZone = this.findElement("currentPageSelectionZone");
        }
        return this._currentPageSelectZone;
    },

    get_currentPageSiblingsZone: function () {
        if (this._currentPageSiblingsZone == null) {
            this._currentPageSiblingsZone = this.findElement("currentPageSiblingsZone");
        }
        return this._currentPageSiblingsZone;
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

    //gets the page selector wrapping element(use to hide and show the selector)
    get_multiSelectorTag: function () {
        if (this._multiSelectorTag == null)
            this._multiSelectorTag = this.findElement("multiPageSelectorTag");
        return this._multiSelectorTag;
    },

    get_customPagesSelector: function () {
        if (this._customPagesSelector == null) {
            this._customPagesSelector = this.findElement("liCustomSelection");
        }
        return this._customPagesSelector;
    },

    //gets the textbox to enter Levels to expand
    get_expandLevelsTextBox: function () {
        if (this._expandLevelsTextBox == null) {
            this._expandLevelsTextBox = this.findElement("txtExpandLevels");
        }
        return this._expandLevelsTextBox;
    },
    //gets the textbox to enter Levels to include
    get_bindingLevelsTextBox: function () {
        if (this._bindingLevelsTextBox == null) {
            this._bindingLevelsTextBox = this.findElement("txtBindingLevels");
        }
        return this._bindingLevelsTextBox;
    },

    //gets the textbox to enter Column/Row headings as links
    get_headingsCheckBox: function () {
        if (this._headingsCheckBox == null)
            this._headingsCheckBox = this.findElement("cbHeadings");
        return this._headingsCheckBox;
    },

    //gets the label to show the current top pages
    get_topLevelPagesLabel: function () {
        if (this._topLevelPagesLabel == null)
            this._topLevelPagesLabel = this.findElement("topLevelPagesLabel");
        return this._topLevelPagesLabel;
    },

    get_customTemplateTextBox: function () {
        if (this._customTemplateTextBox == null) {
            this._customTemplateTextBox = this.findElement("txtCustomTemplatePath");
        }
        return this._customTemplateTextBox;
    },
    get_skinTextBox: function () {
        if (this._skinTextBox == null) {
            this._skinTextBox = this.findElement("txtSkin");
        }
        return this._skinTextBox;
    },
    get_allowCollapsingCheckBox: function () {
        if (this._allowCollapsingCheckBox == null) {
            this._allowCollapsingCheckBox = this.findElement("cbAllowCollapsing");
        }
        return this._allowCollapsingCheckBox;
    },

    get_expandAllCheckBox: function () {
        if (this._expandAllCheckBox == null) {
            this._expandAllCheckBox = this.findElement("cbExpandAllLevels");
        }
        return this._expandAllCheckBox;

    },

    get_selectionHeadingLabels: function () {
        return this._selectionHeadingLabels;
    },
    set_selectionHeadingLabels: function (value) {
        this._selectionHeadingLabels = value;
    },

    get_selectedPagesArray: function () {
        return this.get_customSelectedPagesControl().get_choiceItems();
    },
    set_selectedPagesArray: function (value) {
        this.get_customSelectedPagesControl().set_choiceItems(value);
        this._selectedPagesArrayChanged = true;
    },

    /* ----------------------------- public methods ----------------------------- */

    //refreshes the UI overrides the base interface
    refreshUI: function () {
        this._refreshMode = true;
        var data = this.get_controlData();

        //Deserialize the SelectedPages property
        var selectedPages = Sys.Serialization.JavaScriptSerializer.deserialize(data.CustomSelectedPages);
        if (!selectedPages) selectedPages = [];
        this.set_selectedPagesArray(selectedPages);
        this._selectedPagesArrayChanged = false;

        if (this.get_parentPageCheckBox())
            this.get_parentPageCheckBox().checked = data.ShowParentPage;

        this.get_pagesSelector().get_extPagesSelector().set_selectedItems(selectedPages);

        //Set selected pages in selected pages list
        this.get_customSelectedPagesControl().set_choiceItems(selectedPages);
        if (selectedPages.length > 0) {
            jQuery(this.get_multiPageSelectedPageLabel()).hide();
        } else {
            jQuery(this.get_multiPageSelectedPageLabel()).show();
        }

        this._clickRadioChoice("NavigationMode", data.NavigationMode);
        this._clickRadioChoice("SelectionMode", data.SelectionMode);
        this._clickRadioChoice("NavigationAction", data.NavigationAction);

        if (data.LevelsToExpand > 0) {
            this._clickRadioChoice("LevelsToExpandMode", "SpecifyLevels");
            jQuery(this.get_expandLevelsTextBox()).val(data.LevelsToExpand);
        }
        else {
            this._clickRadioChoice("LevelsToExpandMode", "AllLevels");
        }
        if (data.MaxDataBindDepth > 0) {
            this._clickRadioChoice("MaxDataBindDepthMode", "SpecifyBindingLevels");
            jQuery(this.get_bindingLevelsTextBox()).val(data.MaxDataBindDepth);
        }
        else {
            this._clickRadioChoice("MaxDataBindDepthMode", "AllBindingLevels");
        }


        this.get_headingsCheckBox().checked = data.HeadingsShouldBeLinks;
        this.get_topLevelPagesLabel().innerHTML = this._topLevelPageNames;
        if (this.get_controlData().SelectedPageTitle != null && this.get_controlData().SelectedPageTitle.length > 0) {
            this.get_selectedPageLabel().innerHTML = data.SelectedPageTitle;
            jQuery(this.get_selectedPageLabel()).show();
        }
        //        if (this.get_controlData().SelectedPagesDelimited != null && this.get_controlData().SelectedPagesDelimited.length > 0) {
        //            var pageIds = this.get_controlData().SelectedPagesDelimited;
        //            if (pageIds.length != 0) {
        //                var selectedItems = this.get_multiPageSelector().get_nodesByIds(pageIds);
        //                //                this.get_multiPageSelectedPageLabel().innerHTML = this._setMultiPageTitles(selectedItems);
        //            }
        //            jQuery(this.get_multiPageSelectedPageLabel()).show();
        //        }

        //        if (this.get_controlData().SelectedPagesTitlesDelimited != null && this.get_controlData().SelectedPagesTitlesDelimited.length > 0) {
        //            var titles = this.get_controlData().SelectedPagesTitlesDelimited.split(",");
        //            this.get_multiPageSelectedPageLabel().innerHTML = "";
        //            for (var titleIndex = 0; titleIndex < titles.length; titleIndex++) {
        //                if (titles[titleIndex] != '')
        //                    this.get_multiPageSelectedPageLabel().innerHTML += titles[titleIndex] + ',';
        //            }
        //            if (titles.length > 0) {
        //                this.get_multiPageSelectedPageLabel().innerHTML = this.get_multiPageSelectedPageLabel().innerHTML.substring(0, this.get_multiPageSelectedPageLabel().innerHTML.length - 1);
        //            }
        //        }

        if (this.get_controlData().Skin != null)
            this.get_skinTextBox().value = this.get_controlData().Skin;
        this.get_allowCollapsingCheckBox().checked = this.get_controlData().AllowCollapsing;
        this._showTreeViewExpandSettings();
        var dialog = dialogBase.get_radWindow();
        dialog.setSize(660, 670);

        this._refreshMode = false;
    },

    // forces the designer to apply the changes on UI to the control Data
    applyChanges: function () {
        var txtVal = this.get_expandLevelsTextBox().value;
        if (txtVal == "") txtVal = "0";
        this.get_controlData().LevelsToExpand = parseInt(txtVal, 0);
        txtVal = this.get_bindingLevelsTextBox().value;
        if (txtVal == "") txtVal = "0";
        this.get_controlData().MaxDataBindDepth = parseInt(txtVal, 0);
        this._updateCustomTemplate()
        var controlData = this.get_controlData();
        controlData.Skin = this.get_skinTextBox().value;
        controlData.AllowCollapsing = this.get_allowCollapsingCheckBox().checked;

        if (this.get_parentPageCheckBox())
            controlData.ShowParentPage = this.get_parentPageCheckBox().checked;

        var groupPageNavigationCheckBox = this.get_groupPageNavigationCheckBox();
        if (groupPageNavigationCheckBox)
            controlData.GroupPageIsLink = groupPageNavigationCheckBox.checked;

        if (this._selectedPagesArrayChanged == true) {
            var selectedPages = this.get_selectedPagesArray();
            for (var i = 0; i < selectedPages.length; i++) {
                var page = selectedPages[i];
                page.__type = controlData.SelectedPageQualifiedName;
            }
            controlData.CustomSelectedPages = Sys.Serialization.JavaScriptSerializer.serialize(selectedPages)
        }
    },

    /* ----------------------------- private methods ----------------------------- */


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

        var controlData = this.get_controlData();

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

    //utility method to get a radio button option
    _getRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").get(0);
    },
    //utility method to to click a radio group option
    _clickRadioChoice: function (groupName, value) {
        return jQuery(this.get_element()).find("input[name='" + groupName + "'][value='" + value + "']").click();
    },
    //hides all navigation settings
    _hideAllSettings: function () {
        jQuery(this.get_element()).find("#NavigationSettings div[id^='groupSetting']").hide();
    },

    //shows a specific setting group
    _showSettingGroup: function (settingGroupName) {
        jQuery(this.get_element()).find("#NavigationSettings #" + settingGroupName).show();
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
        this._pagesDialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _updateCustomTemplate: function () {
        if (this._currentNavigationMode == null) return;
        var templateValue = this.get_customTemplateTextBox().value
        var data = this.get_controlData();
        data["CustomTemplate" + this._currentNavigationMode] = templateValue;
    },

    //remove click handlers from radio buttons (mouse-over/click) and checkbox (show all levels expanded initially)
    _removeClickHandlersManipulatingGroupCheckBox: function () {
        jQuery(this.get_element()).undelegate("#rbMouseOver", "click");
        jQuery(this.get_element()).undelegate("#rbClick", "click");
        jQuery(this.get_element()).undelegate("#cbExpandAllLevels", "click");
    },

    //initialize the groupPage navigation checkbox. Set value from the server side.
    _setupGroupCheckBox: function () {
        var groupPageNavigationCheckBox = this.get_groupPageNavigationCheckBox();
        if (groupPageNavigationCheckBox) {
            groupPageNavigationCheckBox.checked = this.get_controlData().GroupPageIsLink;
            groupPageNavigationCheckBox.disabled = false;
            var groupPageNavigationWrapper = jQuery(this.get_element()).find(".sfGroupPageNavigationWrapper");
            groupPageNavigationWrapper.removeClass("sfGroupPageNavigationDisabled");
        }
    },

    //add click handlers to radio buttons (mouse-over / click) and checkbox (show all levels expanded initially)
    //depends on the navigation option
    _addClickHandlersToManipulateGroupCheckBox: function (navigationOption) {
        var that = this;
        var groupPageNavigationCheckBox = this.get_groupPageNavigationCheckBox();

        switch (navigationOption) {
            case "HorizontalDropDownMenu":
            case "HorizontalTabs":
                var mouseClickOpenOption = jQuery(this.get_element()).find("#rbClick").get(0);
                if (mouseClickOpenOption) {
                    if (mouseClickOpenOption.checked)
                        that._disableGroupNavigationOnClick(groupPageNavigationCheckBox);
                }

                jQuery(this.get_element()).delegate("#rbMouseOver", "click", function (e) {
                    that._enableGroupPageNavigationOnMouseOver(groupPageNavigationCheckBox);
                });
                jQuery(this.get_element()).delegate("#rbClick", "click", function (e) {
                    that._disableGroupNavigationOnClick(groupPageNavigationCheckBox);
                });
                break;
            case "VerticalTree":
                var showExpandedNodesElement = jQuery(this.get_element()).find("#cbExpandAllLevels").get(0);
                if (showExpandedNodesElement)
                    that._changeGroupPageNavigationOnShowExpandedNodes(showExpandedNodesElement, groupPageNavigationCheckBox)

                jQuery(this.get_element()).delegate("#cbExpandAllLevels", "click", function (e) {
                    that._changeGroupPageNavigationOnShowExpandedNodes(this, groupPageNavigationCheckBox);
                });
                break;
        }
    },

    //handles the NavigationMode
    _clickedNavigationMode: function (sender) {

        var data = this.get_controlData();
        this._showRelevantSettings(sender.target.value);

        this._removeClickHandlersManipulatingGroupCheckBox();
        this._setupGroupCheckBox();

        var navigationOption = sender.target.value;
        this._addClickHandlersToManipulateGroupCheckBox(navigationOption);

        if (!this._refreshMode) {
            data.NavigationMode = sender.target.value;
            this._updateCustomTemplate();
        }
        if (data.NavigationMode != "VerticalSimple")
            if (data.SelectionMode == "CurrentPageSiblings") {
                this._clickRadioChoice("SelectionMode", "TopLevelPages");
            }
        if (data.SelectionMode == "SelectedPages" && data.NavigationMode != "VerticalSimple" && data.NavigationMode != "HorizontalSimple")
            this._clickRadioChoice("SelectionMode", "TopLevelPages");

        this._currentNavigationMode = sender.target.value;
        if (data["CustomTemplate" + this._currentNavigationMode] != null) {
            this.get_customTemplateTextBox().value = data["CustomTemplate" + this._currentNavigationMode];
        }
        else {
            this.get_customTemplateTextBox().value = "";
        }
    },

    _enableGroupPageNavigationOnMouseOver: function (groupPageNavigationCheckBox) {
        var disabledGroupPageNavigation = jQuery(this.get_element()).find(".sfGroupPageNavigationDisabled");
        if (disabledGroupPageNavigation)
            disabledGroupPageNavigation.removeClass("sfGroupPageNavigationDisabled");

        groupPageNavigationCheckBox.disabled = false;
    },

    _disableGroupNavigationOnClick: function (groupPageNavigationCheckBox) {
        var groupPageNavigationWrapper = jQuery(this.get_element()).find(".sfGroupPageNavigationWrapper");
        if (groupPageNavigationWrapper)
            groupPageNavigationWrapper.addClass("sfGroupPageNavigationDisabled");

        groupPageNavigationCheckBox.checked = false;
        groupPageNavigationCheckBox.disabled = true;
    },

    _changeGroupPageNavigationOnShowExpandedNodes: function (showExpandedNodesElement, groupPageNavigationCheckBox) {
        var groupPageNavigationWrapper = jQuery(this.get_element()).find(".sfGroupPageNavigationWrapper");
        if (groupPageNavigationWrapper && showExpandedNodesElement.checked) {
            groupPageNavigationWrapper.addClass("sfGroupPageNavigationDisabled");
            groupPageNavigationCheckBox.checked = true;
            groupPageNavigationCheckBox.disabled = true;
        }
        else {
            groupPageNavigationWrapper.removeClass("sfGroupPageNavigationDisabled");
            groupPageNavigationCheckBox.disabled = false;
        }

    },

    _clickedSelectionMode: function (sender) {
        this._showPageSelectors(sender.target.value);

        if (!this._refreshMode) {
            this.get_controlData().SelectionMode = sender.target.value;
        }
    },

    _clickedNavigationAction: function (sender) {
        if (!this._refreshMode) {
            this.get_controlData().NavigationAction = sender.target.value;
        }
    },

    _clickedLevelsToExpandMode: function (sender) {
        if (sender.target.value == "AllLevels") {
            jQuery(this.get_expandLevelsTextBox()).attr("disabled", "true");
            if (!this._refreshMode) {
                jQuery(this.get_expandLevelsTextBox()).val("");
                this.get_controlData().ExpandLevel = 0;
            }
        }
        else {
            jQuery(this.get_expandLevelsTextBox()).val("1");
            jQuery(this.get_expandLevelsTextBox()).attr("disabled", null);
        }
    },

    _clickedMaxDataBindDepthMode: function (sender) {
        if (sender.target.value == "AllBindingLevels") {
            jQuery(this.get_bindingLevelsTextBox()).attr("disabled", "true");
            if (!this._refreshMode) {
                this.get_controlData().MaxDataBindDepth = 0;
                jQuery(this.get_bindingLevelsTextBox()).val("");
            }
        }
        else {
            jQuery(this.get_bindingLevelsTextBox()).val("2");
            jQuery(this.get_bindingLevelsTextBox()).attr("disabled", null);
        }
    },

    _clickedHeadingsMode: function (sender) {
        if (!this._refreshMode) {
            this.get_controlData().HeadingsShouldBeLinks = sender.target.checked;
        }
    },

    _clickedExpandAllCheckBox: function (sender) {
        if (!this._refreshMode) {
            if (sender.target.checked == true) {
                this.get_controlData().LevelsToExpand = 0;
                this.get_allowCollapsingCheckBox().disabled = null;
                this.get_expandLevelsTextBox().value = "";
            }
            else {
                this.get_controlData().LevelsToExpand = 1;
                this.get_expandLevelsTextBox().value = "1";
                this.get_allowCollapsingCheckBox().checked = true;
                this.get_allowCollapsingCheckBox().disabled = true;
            }
        }

    },

    _showTreeViewExpandSettings: function () {
        var levels = this.get_controlData().LevelsToExpand;
        if (levels == 0) {
            this.get_expandAllCheckBox().checked = true;
            this.get_allowCollapsingCheckBox().disabled = null;
        }
        if (levels > 0) {
            this.get_expandAllCheckBox().checked = false;
            this.get_allowCollapsingCheckBox().disabled = true;
        }
    },


    _showPageSelectors: function (value) {

        jQuery([
            this.get_pageSelectZone(),
            this.get_multiPageSelectZone(),
            this.get_currentPageSiblingsZone(),
            this.get_currentPageSelectZone()
        ])
        .hide();

        switch (value) {
            case "TopLevelPages":
                break;
            case "SelectedPageChildren":
                jQuery(this.get_pageSelectZone()).show();
                break;
            case "SelectedPages":
                jQuery(this.get_multiPageSelectZone()).show();
                break;
            case "CurrentPageChildren":
                jQuery(this.get_currentPageSelectZone()).show();
                break;
            case "CurrentPageSiblings":
                jQuery(this.get_currentPageSiblingsZone()).show();
                break;
            case "CustomSelection":
                break;
        }

        dialogBase.resizeToContent();

    },

    _showRelevantSettings: function (navOption) {
        this._hideAllSettings();
        jQuery(this.get_customPagesSelector()).hide();
        var siblingsSelector = jQuery(this.get_element()).find("#NavigationSettings #liCurrentPageSiblings");
        var multiplePages = jQuery(this.get_element()).find("#NavigationSettings #liMultiplePages");
        var showParentPagesCheckBoxes = jQuery(this.get_element()).find(".sfShowParentPageWrapper");
        showParentPagesCheckBoxes.hide();

        siblingsSelector.hide();
        multiplePages.hide();
        if (!this._refreshMode) {
            this.applyChanges();
        }
        switch (navOption) {
            case "HorizontalSimple":
                this._showSettingGroup("groupSettingPageSelect");
                multiplePages.show();
                //jQuery(this.get_customPagesSelector()).show();
                break;
            case "HorizontalDropDownMenu":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingBinding");
                this._showSettingGroup("groupSettingDropDown");
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
            case "HorizontalTabs":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingBinding")
                this._showSettingGroup("groupSettingDropDown");
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
            case "VerticalSimple":
                this._showSettingGroup("groupSettingPageSelect");
                multiplePages.show();
                siblingsSelector.show();
                showParentPagesCheckBoxes.show();
                // jQuery(this.get_customPagesSelector()).show();
                break;
            case "VerticalTree":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingBinding");
                this._showSettingGroup("groupSettingExpandAll");
                this._showSettingGroup("groupSettingCollapsing");
                this._showTreeViewExpandSettings();
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
            case "SiteMapInColumns":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
            case "SiteMapInRows":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
            case "CustomNavigation":
                this._showSettingGroup("groupSettingPageSelect");
                this._showSettingGroup("groupSettingGroupPageNavigation");
                break;
        }

        jQuery(this.findElement("groupDesignSettings")).removeClass("sfExpandedSection");

        var label = this.get_selectionHeadingLabels()[navOption];
        this.findElement("SelectionHeading").innerHTML = label.split("|")[0];
        this.findElement("SelectionNotes").innerHTML = label.split("|")[1];
        dialogBase.resizeToContent();

    },

    findElement: function (id) {

        if (typeof (this._elementsCache[id]) !== 'undefined')
            return this._elementsCache[id];
        var result = jQuery(this.get_element()).find("#" + id).get(0);
        this._elementsCache[id] = result;
        return result;
    },

    // Gets the control that shows and reorders selected pages
    get_customSelectedPagesControl: function () { return this._customSelectedPagesControl; },
    // Sets the control that shows and reorders selected pages
    set_customSelectedPagesControl: function (value) { this._customSelectedPagesControl = value; },

    get_clientLabelManager: function () {
        return this._clientLabelManager;
    },
    set_clientLabelManager: function (value) {
        this._clientLabelManager = value;
    }

}

Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner.registerClass('Telerik.Sitefinity.Web.UI.NavigationControls.NavigationDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
