Type._registerScript("ContentSelectorsPipeDesignerView.js", ["ContentSelectorsDesignerView.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView.initializeBase(this, [element]);
    this._additionalFilter = null;

}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView.callBaseMethod(this, 'initialize');

        this._radioClickDelegate = Function.createDelegate(this, this._setContentFilter);
        this.get_radioChoices().click(this._radioClickDelegate);

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });

    },
    dispose: function () {

        if (this._radioClickDelegate) {
            this.get_radioChoices().unbind("click", this._radioClickDelegate);
            delete this._radioClickDelegate;
        }

        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // refresh the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this designer.
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (!controlData) {
            return;
        }
        var disabledFilter = true;
        var additionalFilter = this._additionalFilter;
        if (additionalFilter) {
            additionalFilter = Sys.Serialization.JavaScriptSerializer.deserialize(additionalFilter);
            disabledFilter = false;
            this.get_radioChoices()[1].click()

        }
        else {
            this.get_radioChoices()[0].click();
        }

        var filterSelector = this._filterSelector;
        if (filterSelector) {
            filterSelector.set_queryData(additionalFilter);
            filterSelector.set_disabled(disabledFilter);
        }

        if (this._providersSelector != null) {
            this._providersSelectorDataBoundDelegate = Function.createDelegate(this, this._providersSelectorDataBound);
            this._providersSelector.get_providersSelector().add_binderDataBound(this._providersSelectorDataBoundDelegate);
        }

        dialogBase.resizeToContent();
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        if (this.get_radioChoices().eq(1).attr("checked")) {
            this._filterSelector.applyChanges();
            var queryData = this._filterSelector.get_queryData();
            if (queryData.QueryItems && queryData.QueryItems.length > 0)
                queryData = Telerik.Sitefinity.JSON.stringify(queryData);
            else
                queryData = null;

            this._controlData.pipe.AdditionalFilter = queryData;
        }

        if (this._providersSelector != null) {
            this._controlData.settings.ProviderName = this._providersSelector.get_selectedProvider();
        }
    },

    get_uiDescription: function () {
        var res = this._resources;
        var typeName;
        var contentType = this._controlData.settings.ContentTypeName;
        var contentRes = res[contentType];
        var defaultRes = res["default"];
        if (contentRes) {
            typeName = contentRes.ItemsName;
        }
        else {
            var namespaces = contentType.split('.');
            typeName = namespaces[namespaces.length - 1];
        }
        var typePrefix = String.format("<strong>{0}</strong>: ", typeName);
        return typePrefix + defaultRes.AllItems;
    },

    _setLabels: function (contentType) {

        var res = this._resources;

        //set title
        var contentRes = res[contentType];
        if (!contentRes)
            contentRes = res["default"];
        if (contentRes) {
            if (contentRes.TitleText)
                jQuery(this.get_element()).find('.choicesTitleSelector').html(contentRes.TitleText);
            if (contentRes.ChooseAllText)
                this.get_radioChoices().eq(0).next().html(contentRes.ChooseAllText) //'next' gets the label rendered after input of radio button
            if (contentRes.SelectionOfItem)
                this.get_radioChoices().eq(1).next().html(contentRes.SelectionOfItem)

            if (contentRes.SelectorDateRangesTitle && this._filterSelector && this._filterSelector._items.length > 0) {
                var item = $find(this._filterSelector._items[2]);
                if (item && item._selectorResultView && item._selectorResultView._selector && item._selectorResultView._selector._dateRangesChoiceField)
                    item._selectorResultView._selector._dateRangesChoiceField.set_title(contentRes.SelectorDateRangesTitle);
            }
        }
    },

    /* --------------------------------- event handlers ---------------------------------- */

    // sets the content filter setting based on the radio button that selected the
    // filter type
    _setContentFilter: function (sender) {

        var radioID = sender.target.value;

        var disabledFilter = true;
        switch (radioID) {
            case "contentSelect_AllItems":
                this.clearAdditionalFilter();
                jQuery(this.get_element()).find('#selectorPanel').hide();
                break;
            case "contentSelect_OneItem":
                jQuery(this.get_element()).find('#selectorPanel').show();
                break;
            case "contentSelect_SimpleFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                disabledFilter = false;
            case "contentSelect_AdvancedFilter": break;
        }
        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();

    },

    clearAdditionalFilter: function () {
        this._additionalFilter = null;
        if (this._controlData && this._controlData.settings) {
            this._controlData.pipe.AdditionalFilter = null;
        }
    },

    /* --------------------------------- private methods --------------------------------- */

    _providersSelectorDataBound: function () {
        var providerName = this.get_controlData().settings.ProviderName;

        if (providerName != null) {
            this._providersSelector.selectProvider(providerName);
        }

        this._providersSelector.get_providersSelector().remove_binderDataBound(this._providersSelectorDataBoundDelegate);
    },

    get_controlData: function () {
        return this._controlData;
    },
    set_controlData: function (value) {
        this._controlData = value;
        this._additionalFilter = value.pipe.AdditionalFilter;
        this._setLabels(this._controlData.settings.ContentTypeName);
    },

    get_resources: function () {
        return this._resources;
    },
    set_resources: function (value) {
        this._resources = value;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentSelectorsPipeDesignerView',
    Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();

