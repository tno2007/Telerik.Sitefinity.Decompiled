/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("DynamicContentSelectorsDesignerView.js", ["ContentSelectorsDesignerView.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView = function (element) {
    Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.initializeBase(this, [element]);
    this._mainFieldName = null;
    this._contentViewDisplayMode = null;
    this._dataItemId = null;
    this._onLoadDelegate = null;
    this._contentSelectorCultureFilter = null;
    this._buttonSelectText = null;
    this._buttonChangeText = null;
}

Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.callBaseMethod(this, 'initialize');

        this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        Sys.Application.add_load(this._onLoadDelegate);

        // prevent memory leaks
        $(this).on("unload", function (e) {
            jQuery.event.remove(this);
            jQuery.removeData(this);
        });
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.callBaseMethod(this, 'dispose');

        if (this._onLoadDelegate) {
            Sys.Application.remove_load(this._onLoadDelegate);
            delete this._onLoadDelegate;
        }
    },

    /* --------------------------------- public methods --------------------------------- */
    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        this.get_controlData().ContentViewDisplayMode = this._contentViewDisplayMode;
        this.get_currentDetailView().DataItemId = this._dataItemId;
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.callBaseMethod(this, 'applyChanges');
    },

    /* --------------------------------- event handlers --------------------------------- */

    // Called when page has loaded with all of its components. At this moment property
    // editor already has the control data.
    _onLoad: function () {
        this._contentViewDisplayMode = this.get_controlData().ContentViewDisplayMode;
        this._dataItemId = this.get_currentDetailView().DataItemId;

        if (this._dataItemId === Telerik.Sitefinity.getEmptyGuid()) {
            this._setSelectContentButtonText(this._buttonSelectText);
        } else {
            this._setSelectContentButtonText(this._buttonChangeText);
        }
    },
    // handles the click event of the select content button
    _showContentSelector: function () {
        var contentSelector = this.get_contentSelector();
        if (contentSelector) {
            // dynamic items don't support fallback, the binder search should work as in monolingual
            contentSelector.get_itemSelector()._selectorSearchBox.get_binderSearch()._multilingual = false;
            if (this._contentSelectorCultureFilter) {
                this.get_contentSelector().set_itemsFilter(this._contentSelectorCultureFilter);
            }
            contentSelector.dataBind();
            var dataItemId = this._dataItemId;
            if (dataItemId) {
                contentSelector.set_selectedKeys([dataItemId]);
            }
        }

        //jQuery(this.get_element()).find('#selectorTag').show();
        this._dialog.dialog("open");
        jQuery("body > form").hide();
        dialogBase.resizeToContent();
    },
    // handles the content selected event of the content selector
    _selectContent: function (items) {
        this._dialog.dialog("close");
        jQuery("body > form").show();
        dialogBase.resizeToContent();
        if (items == null) return;
        var selectedItems = this.get_contentSelector().getSelectedItems();
        if (selectedItems != null) {
            if (selectedItems.length > 0) {
                if (selectedItems[0].hasOwnProperty(this._mainFieldName)) {
                    if (selectedItems[0][this._mainFieldName].hasOwnProperty('Value')) {
                        this.get_selectedContentTitle().innerText = selectedItems[0][this._mainFieldName].Value;
                    } else {
                        this.get_selectedContentTitle().innerText = selectedItems[0][this._mainFieldName];
                    }
                }
                this._dataItemId = selectedItems[0].Id;
                this._contentViewDisplayMode = "Detail";
                jQuery(this.get_selectedContentTitle()).show();

                this._setSelectContentButtonText(this._buttonChangeText);
            }
            else {
                this._setSelectContentButtonText(this._buttonSelectText);
            }
        }
    },

    // reset the dataItemId if provider is changed
    _resetViewOnProvidersChanged: function () {
        Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.callBaseMethod(this, '_resetViewOnProvidersChanged');
        this._dataItemId = Telerik.Sitefinity.getEmptyGuid();
    },

    /* --------------------------------- private methods --------------------------------- */

    // sets the content filter setting based on the radio button that selected the
    // filter type
    _setContentFilter: function (sender) {
        var radioID = sender.target.value;
        var disabledFilter = true;
        switch (radioID) {
            case "contentSelect_AllItems":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Automatic";
                }
                break;
            case "contentSelect_OneItem":
                jQuery(this.get_element()).find('#selectorPanel').show();
                if (!this._refreshing) {
                    this._contentViewDisplayMode = "Detail";
                }
                break;
            case "contentSelect_SimpleFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
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

    _setSelectContentButtonText: function (value) {
        $(this._element).find("#" + this._selectContentButton).text(value);
    },
}
Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.DynamicContentSelectorsDesignerView', Telerik.Sitefinity.Web.UI.ControlDesign.ContentSelectorsDesignerView);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();