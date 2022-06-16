/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("ContentPipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._settingsDatas = {};
    this._multiPage = null;
    this._selectContent = null;
    this._selectLanguage = null;
    this._selectContentChangedDelegate = null;
    this._selectLanguageChangedDelegate = null;
    this._resources = null;
    this._openMappingSettingsButton = null;
    this._backLinksPagePicker = null;
    this._pageSelectorOpenedDelegate = null;
    this._pageSelectorClosedDelegate = null;
    this._isInitialized = false;
    this._tempContentType = null; //used to store a temp content type between function calls
    this._designerViewIds = null;
    this._designerViews = [];
    this._defaultDesignerView = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView.callBaseMethod(this, 'initialize');

        if (this._selectContent) {
            if (this._selectContentChangedDelegate == null)
                this._selectContentChangedDelegate = Function.createDelegate(this, this._selectContentChanged);
            $addHandler(this._selectContent, "change", this._selectContentChangedDelegate);
        }

        if (this._selectLanguage) {
            if (this._selectLanguageChangedDelegate == null)
                this._selectLanguageChangedDelegate = Function.createDelegate(this, this._selectLanguageChanged);
            $addHandler(this._selectLanguage, "change", this._selectLanguageChangedDelegate);
        }

        this._pageSelectorOpenedDelegate = Function.createDelegate(this, this._pageSelectorOpened);
        this._pageSelectorClosedDelegate = Function.createDelegate(this, this._pageSelectorClosed);
        this.get_backLinksPagePicker().add_selectorOpened(this._pageSelectorOpenedDelegate);
        this.get_backLinksPagePicker().add_selectorClosed(this._pageSelectorClosedDelegate);
        this._selectContentChanged();
        var designerViews = this._designerViews;
        designerViews.length = 0;
        jQuery.each(this._designerViewIds, function (i, e) {
            designerViews.push($find(e));
        });
        this._isInitialized = true;
    },
    dispose: function () {
        if (this._selectContent) {
            $removeHandler(this._selectContent, "change", this._selectContentChangedDelegate);
        }
        if (this._selectLanguage) {
            $removeHandler(this._selectLanguage, "change", this._selectLanguageChangedDelegate);
            if (this._selectLanguageChangedDelegate != null)
                delete this._selectLanguageChangedDelegate;
        }
        if (this._selectContentChangedDelegate != null)
            delete this._selectContentChangedDelegate;
        if (this._pageSelectorOpenedDelegate != null)
            delete this._pageSelectorOpenedDelegate;
        if (this._pageSelectorClosedDelegate != null)
            delete this._pageSelectorClosedDelegate;

        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView.callBaseMethod(this, 'dispose');
    },
    set_controlData: function (value) {
        // first-time
        if (typeof value.settings.ContentTypeName == "undefined") {
            value.settings.ContentTypeName = this._getDefaultContentType();
            if (typeof value.settings.ContentTypeName == "undefined") {
                return;
            }
        }
        this._tempContentType = value.settings.ContentTypeName;
        this._settingsDatas[this._tempContentType] = value;

        var idx = this._getContentTypeIndex(this._tempContentType);
        this._selectContent.selectedIndex = idx;

        //set language
        this._setLanguage();

        var component = this._getComponentForIndex(this._tempContentType);
        component.set_controlData(value);
        this.refreshUI();
    },
    get_controlData: function () {
        var cmp = this._getActiveComponent();
        var cmpData = cmp.get_controlData();

        this._pushSelectedLanguage();

        return cmpData;
    },
    //TODO
    validate: function () {
        var isValid = true;
        return isValid;
    },
    // Returns true if there are changes in the designer
    //    isChanged: function () {
    //        
    //    },
    refreshUI: function () {
        this._refreshing = true;
        this._set_culture();
        this._setContentType();
        this.get_backLinksPagePicker().set_value(this._getActiveSettingsData().pipe.ContentLocationPageID);
        this._refreshing = false;
    },
    applyChanges: function () {
        var comp = this._getActiveComponent();
        if (comp != null)
            comp.applyChanges();

        var activeSettings = this._getActiveSettingsData();
        activeSettings.settings.ContentTypeName = this._getContentType();
        activeSettings.pipe.ContentLocationPageID = this.get_backLinksPagePicker().get_value();
        activeSettings.pipe.UIDescription = this.get_uiDescription();
    },
    _pushSelectedLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = $find(this._selectLanguage.id);
            var settings = this._getActiveSettingsData();
            if (languageSelector) {
                this._clearArray(settings.pipe.LanguageIds);
                settings.pipe.LanguageIds.push(languageSelector.get_value());
            }
        }
    },
    _setLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = this.get_languageSelector();
            var activeSettings = this._getActiveSettingsData();
            if (languageSelector && activeSettings.pipe.LanguageIds && activeSettings.pipe.LanguageIds.length > 0) {
                languageSelector._selectItemByValue(activeSettings.pipe.LanguageIds);
            }
        }
    },
    _clearArray: function (arr) {
        arr.splice(0, arr.length);
    },
    _selectContentChanged: function () {
        var contentType = this._getContentType();

        var isFind = false;

        for (i = 0; i < this._multiPage.get_pageViews().get_count(); i++) {
            if (this._multiPage.get_pageViews().getPageView(i).get_id().replace(/_/g, ".").lastIndexOf(contentType) != -1) {
                this._multiPage.get_pageViews().getPageView(i).set_selected(true);
                isFind = true;
            }
        }
        if (!isFind) {
            for (i = 0; i < this._multiPage.get_pageViews().get_count(); i++) {
                if (this._multiPage.get_pageViews().getPageView(i).get_id().lastIndexOf("default") != -1) {
                    this._multiPage.get_pageViews().getPageView(i).set_selected(true);
                    isFind = true;
                }
            }
        }
        var comp = this._getActiveComponent();
        if (comp != null) {
            var activeData = this._settingsDatas[contentType];
            comp.set_controlData(activeData);

            // Crear filter when content is changed. (Check if component is loaded, to clear only when user is responsible for the change)
            if (this._isInitialized == true && this._refreshing == false && comp.get_isUpdating() == false) {
                comp.clearAdditionalFilter();
            }
            comp.refreshUI();
        }
    },

    _selectLanguageChanged: function () {
        // clears selections, that depend on language - page, categories, tags, etc.
        this._backLinksPagePicker.set_value(null);
        jQuery.each(this._designerViews, function (i, e) {
            if (e.clearAdditionalFilter) {
                e.clearAdditionalFilter();
                e.refreshUI();
            }
        });
        this._set_culture();
    },

    _pageSelectorOpened: function () {
        dialogBase.resizeToContent();
    },

    _pageSelectorClosed: function () {
        dialogBase.resizeToContent();
    },

    _set_culture: function () {
        var culture = this._getSelectedLanguage();

        if (culture) {
            var pageField = this.get_backLinksPagePicker();
            if (pageField) {
                //optimization check if culture is changed
                var cultureIsChanged = pageField.get_culture() != culture;
                if (cultureIsChanged)
                    pageField.set_culture(culture);
            }
            var designerViews = this._designerViews;
            var designerView;
            for (var i = 0; i < designerViews.length; i++) {
                designerView = designerViews[i];
                if (designerView.get_filterSelector) {
                    var filterSelector = designerView.get_filterSelector();
                    filterSelector.set_culture(culture);

                    // Set the culture for inner selectors, because it is not passed to them.
                    // When there is more time, this can be implemented in the base classes
                    for (var j = 0, l = filterSelector._items.length; j < l; j++) {
                        var item = $find(filterSelector._items[j]);
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
            }
        }
    },

    _getSelectedLanguage: function () {
        var languageSelector = this.get_languageSelector();
        if (languageSelector) {
            return languageSelector.get_value();
        }
    },

    get_languageSelector: function () {
        if (this._selectLanguage)
            return $find(this._selectLanguage.id);
    },

    get_multiPage: function () {
        return this._multiPage;
    },
    set_multiPage: function (value) {
        this._multiPage = value;
    },
    get_selectContent: function () {
        return this._selectContent;
    },
    set_selectContent: function (value) {
        this._selectContent = value;
    },
    get_selectLanguage: function () {
        return this._selectLanguage;
    },
    set_selectLanguage: function (value) {
        this._selectLanguage = value;
    },
    get_resources: function () {
        return this._resources;
    },
    set_resources: function (value) {
        this._resources = value;
    },

    get_backLinksPagePicker: function () {
        return this._backLinksPagePicker;
    },
    set_backLinksPagePicker: function (value) {
        this._backLinksPagePicker = value;
    },
    get_designerViewIds: function () {
        return this._designerViewIds;
    },
    set_designerViewIds: function (value) {
        this._designerViewIds = value;
    },
    get_designerViews: function () {
        return this._designerViews;
    },
    set_designerViews: function (value) {
        this._designerViews = value;
    },
    get_defaultDesignerView: function () {
        return this._defaultDesignerView;
    },
    set_defaultDesignerView: function (value) {
        this._defaultDesignerView = value;
    },
    _getActiveComponent: function () {
        var contentTypeName = this._getContentType();
        return this._getComponentForIndex(contentTypeName);
    },

    _getComponentForIndex: function (typeName) {
        var name = typeName.replace(/\./g, "_");
        var designerViews = this._designerViews;
        var designerView;
        for (var i = 0, l = designerViews.length; i < l; i++) {
            designerView = designerViews[i];
            if (designerView.get_id().indexOf(name) > -1) {
                return designerView;
            }
        }
        return this._defaultDesignerView;
    },

    _getDefaultContentType: function () {
        var undef;
        if (this._selectContent.options.length > 0) {
            return this._selectContent.options[0].value;
        }
        else {
            return undef;
        }
    },

    // returns the value of the list with types
    _getContentType: function () {
        return this._selectContent.options[this._selectContent.selectedIndex].value;
    },
    // returns the pipe settings for the currently selected content type
    _getActiveSettingsData: function () {
        return this._settingsDatas[this._getContentType()];
    },
    // selects the item with the given value
    _setContentType: function () {
        var value = this._tempContentType;
        for (var i = 0; i < this._selectContent.length; i++) {
            if (this._selectContent.options[i].value == value) {
                this._selectContent.selectedIndex = i;
                break;
            }
        }
        this._selectContentChanged();
    },
    // gets the index of the option with the given value
    _getContentTypeIndex: function (value) {
        for (var i = 0; i < this._selectContent.length; i++) {
            if (this._selectContent.options[i].value == value) {
                return i;
            }
        }
        return -1;
    },

    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var comp = this._getActiveComponent();
        if (comp && comp.get_uiDescription) {
            return comp.get_uiDescription();
        }
        return "";
    },

    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
