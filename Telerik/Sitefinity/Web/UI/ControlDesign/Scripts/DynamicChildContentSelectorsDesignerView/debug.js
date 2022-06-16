/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("DynamicChildContentSelectorsDesignerView.js", ["DynamicContentSelectorsDesignerView.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.initializeBase(this, [element]);
    this._parentSelector = null;
    this._parentSelectorId = null;
    this._filterByParentUrl = null;
    this._enableFilterByParent = null;
    this._parentFilterRadioChoices = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.callBaseMethod(this, 'initialize');

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        this._parentFilterRadioClickDelegate = Function.createDelegate(this, this._setParentFilter);
        this.get_parentFilterRadioChoices().click(this._parentFilterRadioClickDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }

        if (this._parentFilterRadioClickDelegate) {
            delete this._parentFilterRadioClickDelegate;
        }
    },

    /* --------------------------------- public functions --------------------------------- */

    // Refreshes the user interface. Call this method in case underlying control object has been changed somewhere else then through this designer.
    refreshUI: function () {
        this._refreshing = true;
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }

        var additionalFilter = this.get_currentMasterView().AdditionalFilter;
        if (additionalFilter)
            additionalFilter = Sys.Serialization.JavaScriptSerializer.deserialize(additionalFilter);
        this._filterSelector.set_queryData(additionalFilter);
        this._filterByParentUrl = this.get_controlData().FilterByParentUrl;
        var itemParentIds = this.get_currentMasterView().ItemsParentsIds;
        if (itemParentIds)
            itemParentIds = Sys.Serialization.JavaScriptSerializer.deserialize(itemParentIds);
        var disabledFilter = true;

        if (controlData.ContentViewDisplayMode !== "Detail") {
            
            // Set Parent filter selection choice
            if (this._filterByParentUrl) {
                this.get_radioChoiceByValue("contentSelect_CurrentlyOpenParentFilter").click();
            } else if (itemParentIds && itemParentIds.length > 0) {
                this.get_radioChoiceByValue("contentSelect_SelectedParentsFilter").click();
            }
            else {
                this.get_radioChoiceByValue("contentSelect_AllParents").click();
            }

            // Set Narrow selection choice
            if (additionalFilter) {
                this.get_radioChoiceByValue("contentSelect_SimpleFilter").click();
                disabledFilter = false;
            } else {
                this.get_radioChoiceByValue("contentSelect_AllItems").click();
            }
        }
        else {
            this.get_radioChoiceByValue("contentSelect_OneItem").click();
        }
        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();
        this._refreshing = false;
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        if (this.get_parentSelector()) {
            if (this._enableFilterByParent) {
                var itemsParentIds = this.get_parentSelector().get_value();
                if (itemsParentIds)
                    this.get_currentMasterView().ItemsParentsIds = Sys.Serialization.JavaScriptSerializer.serialize(itemsParentIds);
            } else {
                this._clearItemsParentIds();
            }
        }
        this.get_controlData().FilterByParentUrl = this._filterByParentUrl;
        this.get_controlData().ContentViewDisplayMode = this._contentViewDisplayMode;
        var displayMode = this.get_controlData().ContentViewDisplayMode;
        if (displayMode === "Automatic") {
            this.get_currentDetailView().DataItemId = '00000000-0000-0000-0000-000000000000';
        } else {
            this.get_currentDetailView().DataItemId = this._dataItemId;
        }

        this.get_currentMasterView().AdditionalFilter = null;
        if (displayMode === "Automatic" || displayMode === "Master") {
            if (this.get_radioChoiceByValue("contentSelect_SimpleFilter").attr("checked")) {
                this._filterSelector.applyChanges();
                var queryData = this._filterSelector.get_queryData();
                if (queryData.QueryItems && queryData.QueryItems.length > 0)
                    queryData = Telerik.Sitefinity.JSON.stringify(queryData);
                else
                    queryData = null;
                this.get_currentMasterView().AdditionalFilter = queryData;
            }
        }

        ////Doesn't call base method because template is incompatible with base logic
    },

    /* --------------------------------- event handlers --------------------------------- */

    // Called when page has loaded with all of its components. At this moment property
    // editor already has the control data.
    _onLoad: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.callBaseMethod(this, '_onLoad');
        this._onParentSelectorDialogOpenedDelegate = Function.createDelegate(this, this._onParentSelectorOpened);
        this._onParentSelectorDialogClosedDelegate = Function.createDelegate(this, this._onParentSelectorClosed);
        if (this.get_parentSelector()) {
            if (this.get_currentMasterView().ItemsParentsIds.length > 0) {
                var itemsParentIds = Sys.Serialization.JavaScriptSerializer.deserialize(this.get_currentMasterView().ItemsParentsIds);
                this.get_parentSelector().set_value(itemsParentIds);
            }

            this.get_parentSelector()._dialog.on("dialogopen", this._onParentSelectorDialogOpenedDelegate);
            this.get_parentSelector()._dialog.on("dialogclose", this._onParentSelectorDialogClosedDelegate);
        }
    },

    _onParentSelectorOpened: function () {
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },

    _onParentSelectorClosed: function () {
        jQuery("body > form").show();
        dialogBase.resizeToContent();
    },

    _handleProvidersChanged: function (sender, args) {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.callBaseMethod(this, '_handleProvidersChanged', arguments);

        if (this.get_parentSelector() !== null) {
            this.get_parentSelector().set_providerName(args.ProviderName);
        }
    },

    // sets the parent filter setting based on the radio button that is selected
    _setParentFilter: function (sender) {
        var radioID = sender.target.value;
        this._filterByParentUrl = false;
        this._enableFilterByParent = false;
        var disableContentFilters = false;
        switch (radioID) {
            case "contentSelect_AllParents":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                jQuery(this.get_element()).find('#parentSelectorPanel').hide();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_CurrentlyOpenParentFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                jQuery(this.get_element()).find('#parentSelectorPanel').hide();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                this._filterByParentUrl = true;
                break;
            case "contentSelect_SelectedParentsFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                jQuery(this.get_element()).find('#parentSelectorPanel').show();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                this._enableFilterByParent = true;
                break;
            case "contentSelect_OneItem":
                jQuery(this.get_element()).find('#selectorPanel').show();
                jQuery(this.get_element()).find('#parentSelectorPanel').hide();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Detail";
                }
                disableContentFilters = true;
                break;
        }
        this._disableContentFilters(disableContentFilters);
        dialogBase.resizeToContent();
    },

    // sets the content filter setting based on the radio button that is selected
    _setContentFilter: function (sender) {
        var radioID = sender.target.value;
        var disabledFilter = true;
        switch (radioID) {
            case "contentSelect_AllItems":
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_SimpleFilter":
                disabledFilter = false;
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_AdvancedFilter": break;
        }

        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();
    },

    _resetViewOnProvidersChanged: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.callBaseMethod(this, '_resetViewOnProvidersChanged');
        this.get_parentSelector().set_value([]);
        this.get_parentSelector().resetSelectedContentTitle();
    },

    /* --------------------------------- private functions --------------------------------- */

    // Clears selected parents in the Master view definition
    _clearItemsParentIds: function () {
        this.get_currentMasterView().ItemsParentsIds = Sys.Serialization.JavaScriptSerializer.serialize([]);
    },

    _disableContentFilters: function (disable) {
        if (disable) {
            this.get_radioChoiceByValue("contentSelect_AllItems").click();
        }

        var radioChoices = this.get_radioChoices();
        if (radioChoices) {
            for (var i = 0; i < radioChoices.length; i++) {
                var radioChoice = $(radioChoices[i]);
                var radioChoiceLabel = jQuery(this.element).find("label[for=" + radioChoice.attr("id") + "]");
                if (disable) {
                    radioChoice.attr("disabled", true);
                    radioChoiceLabel.addClass(this._disabledTextClass);
                }
                else {
                    radioChoice.removeAttr("disabled");
                    radioChoiceLabel.removeClass(this._disabledTextClass);
                }
            }
        }
    },

    /* --------------------------------- getters and setters --------------------------------- */

    // Gets the radio buttons from "ParentFilter" group
    get_parentFilterRadioChoices: function () {
        if (!this._parentFilterRadioChoices) {
            this._parentFilterRadioChoices = jQuery(this.get_element()).find(':radio[name$=ParentFilterSelection]'); // finds radio buttons with names ending with 'ParentFilterSelection'
        }
        return this._parentFilterRadioChoices;
    },

    get_radioChoiceByValue: function (value) {
        var selector = String.format(':radio[value={0}]', value);
        return jQuery(this.get_element()).find(selector);
    },

    get_parentSelector: function () {
        if (!this._parentSelector) {
            this._parentSelector = $find(this._parentSelectorId);
        }

        return this._parentSelector;
    },
}

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView.registerClass("Telerik.Sitefinity.Web.UI.ControlDesign.DynamicChildContentSelectorsDesignerView", Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();