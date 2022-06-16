﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("ContentPipeExportDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._settingsDatas = {};
    this._multiPage = null;
    this._selectContent = null;
    this._selectLanguage = null;
    this._selectContentChangedDelegate = null;
    this._openMappingSettingsButton = null;

    //delegates
    this._exportAsPublished = null;
    this._designerViews = [];
    this._designerViewIds = null;
    this._defaultDesignerView = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView.callBaseMethod(this, 'initialize');

        if (this._selectContentChangedDelegate == null)
            this._selectContentChangedDelegate = Function.createDelegate(this, this._selectContentChanged);
        $addHandler(this._selectContent, "change", this._selectContentChangedDelegate);

        var designerViews = this._designerViews;
        designerViews.length = 0;
        jQuery.each(this._designerViewIds, function (i, e) {
            designerViews.push($find(e));
        });
    },
    dispose: function () {
        $removeHandler(this._selectContent, "change", this._selectContentChangedDelegate);

        $removeHandler(this._selectContent, "change", this._selectContentChangedDelegate);
        if (this._selectContentChangedDelegate != null)
            delete this._selectContentChangedDelegate;

        Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView.callBaseMethod(this, 'dispose');
    },

    set_controlData: function (value) {
        var contentType = value.settings.ContentTypeName;
        this._settingsDatas[contentType] = value;

        var idx = this._getContentTypeIndex(contentType);
        this._selectContent.selectedIndex = idx;

        this._setLanguage();

        this.refreshUI();
    },
    get_controlData: function (value) {
        this._getSelectedLanguage();
        return this._getActiveSettingsData();
    },

    validate: function () {
        var isValid = true;
        var comp = this._getActiveComponent();
        if (comp != null)
            isValid = comp.validate();
        return isValid;
    },

    refreshUI: function () {
        this._refreshing = true;
        var settingsData = this._getActiveSettingsData();
        this.get_exportAsPublished().checked = settingsData.settings.ImportItemAsPublished;
        this._selectContentChanged();
        this._refreshing = false;
    },
    applyChanges: function () {
        var settingsData = this._getActiveSettingsData();
        var comp = this._getActiveComponent();
        if (comp != null)
            comp.applyChanges();
        settingsData.settings.ImportItemAsPublished = this.get_exportAsPublished().checked;
        settingsData.pipe.UIDescription = this.get_uiDescription();
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
            comp.refreshUI();
        }

        dialogBase.resizeToContent();
    },

    _getSelectedLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = $find(this._selectLanguage.id);
            if (languageSelector) {
                var settingsData = this._getActiveSettingsData();
                this._clearArray(settingsData.pipe.LanguageIds);
                settingsData.pipe.LanguageIds.push(languageSelector.get_value());
            }
        }
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
    _setLanguage: function () {
        if (this._selectLanguage) {
            var settingsData = this._getActiveSettingsData();
            var languageIds = settingsData.pipe.LanguageIds;
            if (languageIds && languageIds.length > 0) {
                var languageSelector = $find(this._selectLanguage.id);
                if (languageSelector) {
                    languageSelector._selectItemByValue(languageIds);
                }
            }
        }
    },
    _clearArray: function (arr) {
        arr.length = 0;
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

    // returns the value of the list with types
    _getContentType: function () {
        return this._selectContent.options[this._selectContent.selectedIndex].value;
    },
    // returns the pipe settings for the currently selected content type
    _getActiveSettingsData: function () {
        return this._settingsDatas[this._getContentType()];
    },

    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var comp = this._getActiveComponent();
        if (comp && comp.get_uiDescription) {
            return comp.get_uiDescription();
        }
        return "";
    },

    get_exportAsPublished: function () { return this._exportAsPublished; },
    set_exportAsPublished: function (value) { this._exportAsPublished = value; },

    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    },
    get_designerViews: function () {
        return this._designerViews;
    },
    set_designerViews: function (value) {
        this._designerViews = value;
    },
    get_designerViewIds: function () {
        return this._designerViewIds;
    },
    set_designerViewIds: function (value) {
        this._designerViewIds = value;
    },
    get_defaultDesignerView: function () {
        return this._defaultDesignerView;
    },
    set_defaultDesignerView: function (value) {
        this._defaultDesignerView = value;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.ContentPipeExportDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
