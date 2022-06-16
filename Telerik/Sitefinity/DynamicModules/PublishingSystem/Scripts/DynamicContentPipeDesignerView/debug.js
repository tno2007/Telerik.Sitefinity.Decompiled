﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
//Type._registerScript("DynamicContentPipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.DynamicModules.PublishingSystem");

Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView = function (element) {
    this._openMappingSettingsButton = null;
    this._pipeName = null;
    this._contentName = null;
    this._allItemsLabel = null;
    this._backLinksPagePicker = null;
    this._pageSelectorOpenedDelegate = null;
    this._pageSelectorClosedDelegate = null;
    this._settingsData = {};
    this._selectLanguage = null;
    this._selectLanguageChangedDelegate = null;
    this._defaultFrontendLanguage = null;
    this._filterSelector = null;

    Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.initializeBase(this, [element]);
}

Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.prototype = {
    initialize: function () {
        
        this._pageSelectorOpenedDelegate = Function.createDelegate(this, this._pageSelectorOpened);
        this._pageSelectorClosedDelegate = Function.createDelegate(this, this._pageSelectorClosed);
        this.get_backLinksPagePicker().add_selectorOpened(this._pageSelectorOpenedDelegate);
        this.get_backLinksPagePicker().add_selectorClosed(this._pageSelectorClosedDelegate);

        this._radioClickDelegate = Function.createDelegate(this, this._setContentFilter);
        this.get_radioChoices().click(this._radioClickDelegate);

        if (this._selectLanguage) {
            if (this._selectLanguageChangedDelegate == null)
                this._selectLanguageChangedDelegate = Function.createDelegate(this, this._selectLanguageChanged);
            $addHandler(this._selectLanguage, "change", this._selectLanguageChangedDelegate);
        }

        Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {

        if (this._pageSelectorOpenedDelegate != null)
            delete this._pageSelectorOpenedDelegate;
        if (this._pageSelectorClosedDelegate != null)
            delete this._pageSelectorClosedDelegate;

        if (this._selectLanguage) {
            $removeHandler(this._selectLanguage, "change", this._selectLanguageChangedDelegate);
            if (this._selectLanguageChangedDelegate != null)
                delete this._selectLanguageChangedDelegate;
        }

        if (this._radioClickDelegate) {
            this.get_radioChoices().unbind("click", this._radioClickDelegate);
            delete this._radioClickDelegate;
        }

        Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.callBaseMethod(this, 'dispose');
    },

    set_controlData: function (value) {
        this._settingsData = value;
        this._setLanguage();
        this._additionalFilter = value.pipe.AdditionalFilter;
        this.refreshUI();
    },

    get_controlData: function () {
        this._settingsData.pipe.PipeName = this._pipeName;
        this._settingsData.pipe.ContentName = this._contentName;
        this._settingsData.pipe.UIDescription = "<strong>" + this._contentName + "</strong> : " + this._allItemsLabel;
        this._settingsData.settings.PipeName = this._pipeName;
        this._pushSelectedLanguage();

        return this._settingsData;
    },

    validate: function () {
        return true;
    },

    refreshUI: function () {
        if (this._filterSelector) {
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

            this._filterSelector.set_queryData(additionalFilter);
            this._filterSelector.set_disabled(disabledFilter);
        }

        if (this._selectLanguage) {
            this._setCulture();
        } else {
            this._resetLanguageDependantSections();
        }
        this.get_backLinksPagePicker().set_value(this._settingsData.pipe.ContentLocationPageID);
    },

    applyChanges: function () {
        this._settingsData.pipe.AdditionalFilter = null;
        if (this.get_radioChoices().eq(1).attr("checked")) {
            this._filterSelector.applyChanges();
            var queryData = this._filterSelector.get_queryData();
            if (queryData.QueryItems && queryData.QueryItems.length > 0)
                queryData = Telerik.Sitefinity.JSON.stringify(queryData);
            else
                queryData = null;

            this._settingsData.pipe.AdditionalFilter = queryData;
        }
        this._settingsData.pipe.ContentLocationPageID = this.get_backLinksPagePicker().get_value();
    },

    _pageSelectorOpened: function () {
        dialogBase.resizeToContent();
    },
    _pageSelectorClosed: function () {
        dialogBase.resizeToContent();
    },
    _pushSelectedLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = $find(this._selectLanguage.id);
            if (languageSelector) {
                this._clearArray(this._settingsData.pipe.LanguageIds);
                this._settingsData.pipe.LanguageIds.push(languageSelector.get_value());
            }
        } 
    },
    _setLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = this._getLanguageSelector();
            if (languageSelector && this._settingsData.pipe.LanguageIds && this._settingsData.pipe.LanguageIds.length > 0) {
                languageSelector._selectItemByValue(this._settingsData.pipe.LanguageIds);
            }
        }
    },
    _clearArray: function (arr) {
        arr.splice(0, arr.length);
    },
    _selectLanguageChanged: function () {
        this._resetLanguageDependantSections();
        this._setCulture();
        this.refreshUI();
    },
    _resetLanguageDependantSections: function () {
        // clears selections, that depend on language - page
        if (this._backLinksPagePicker) {
            this._backLinksPagePicker.set_value(null);
            if (this._defaultFrontendLanguage) {
                this._backLinksPagePicker.set_culture(this._defaultFrontendLanguage);
            } else {
                this._backLinksPagePicker.set_culture(null);
            }
        }
    },
    _setCulture: function () {
        var culture = this._getSelectedLanguage();
        if (culture) {
            var pageField = this.get_backLinksPagePicker();
            if (pageField) {
                //optimization check if culture is changed
                var cultureIsChanged = pageField.get_culture() != culture;
                if (cultureIsChanged)
                    pageField.set_culture(culture);
            }

            this._filterSelector.set_culture(culture);

            // Set the culture for inner selectors, because it is not passed to them.
            // When there is more time, this can be implemented in the base classes
            for (var j = 0, l = this._filterSelector._items.length; j < l; j++) {
                var item = $find(this._filterSelector._items[j]);
                if (item && item._selectorResultView) {
                    item._selectorResultView.set_culture(culture);
                    var selector = item._selectorResultView._selector;
                    if (selector && selector.set_culture) {
                        selector.set_culture(culture);
                        selector.set_uiCulture(culture);
                    }
                }
            }
        }
    },

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
            case "contentSelect_SimpleFilter":
                jQuery(this.get_element()).find('#selectorPanel').hide();
                disabledFilter = false;
                break;
        }
        this._filterSelector.set_disabled(disabledFilter);
        dialogBase.resizeToContent();
    },

    clearAdditionalFilter: function () {
        this._additionalFilter = null;
        if (this._settingsData && this._settingsData.pipe) {
            this._settingsData.pipe.AdditionalFilter = null;
        }
    },


    // gets all the radio buttons in the container of this control with group name 'ContentSelection'
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find(':radio[name$=ContentSelection]'); // finds radio buttons with names ending with 'ContentSelection'
        }
        return this._radioChoices;
    },

    _getSelectedLanguage: function () {
        var languageSelector = this._getLanguageSelector();
        if (languageSelector) {
            return languageSelector.get_value();
        }
    },
    _getLanguageSelector: function () {
        if (this._selectLanguage)
            return $find(this._selectLanguage.id);
    },
    /* properties */
    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    },
    get_backLinksPagePicker: function () {
        return this._backLinksPagePicker;
    },
    set_backLinksPagePicker: function (value) {
        this._backLinksPagePicker = value;
    },
    get_selectLanguage: function () {
        return this._selectLanguage;
    },
    set_selectLanguage: function (value) {
        this._selectLanguage = value;
    },

    //gets reference to the filter selector
    get_filterSelector: function () {
        return this._filterSelector;
    },

    //sets reference to the filter selector
    set_filterSelector: function (value) {
        this._filterSelector = value;
    }
}

Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView.registerClass('Telerik.Sitefinity.DynamicModules.PublishingSystem.DynamicContentPipeDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') {
    Sys.Application.notifyScriptLoaded();
}
