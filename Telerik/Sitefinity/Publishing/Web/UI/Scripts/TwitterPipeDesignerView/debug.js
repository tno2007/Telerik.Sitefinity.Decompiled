/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("TwitterPipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._dataFieldNameControlIdMap = null;
    this._settingsData = {};
    this._uiNameLabel = null;
    this._urlNameNotSet = null;
    this._radioChoices = null;
    this._shortDescriptionBase = null;
    this._defaultSettings = {};
    this._openMappingSettingsButton = null;
    this._languageChoiceField = null;
    this._appsBinder = null;
    this._appsSelectorId = null;
    this._appsBinderItemDataBoundDelegate = null;
};

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView.callBaseMethod(this, 'initialize');

        this._appsBinderItemDataBoundDelegate = Function.createDelegate(this, this._appsBinderItemDataBound);
        this._appsBinder.add_onItemDataBound(this._appsBinderItemDataBoundDelegate);
    },
    dispose: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView.callBaseMethod(this, 'dispose');
            if (this._appsBinder) {
                this._appsBinder.remove_onItemDataBound(this._appsBinderItemDataBoundDelegate);
            }
            if (this._appsBinderItemDataBoundDelegate != null)
                delete this._appsBinderItemDataBoundDelegate;
    },
    set_controlData: function (value) {
        this._settingsData = value;
        this._setLanguage();
        this.refreshUI();
    },
    get_controlData: function (value) {
        this._getSelectedLanguage();
        return this._settingsData;
    },
    get_uiNameLabel: function () {
        return this._uiNameLabel;
    },
    set_uiNameLabel: function (value) {
        this._uiNameLabel = value;
    },
    validate: function () {
        var isValid = true;
        return isValid;
    },
    // Returns true if there are changes in the designer
    //    isChanged: function () {
    //        
    //    },
    refreshUI: function () {
    },
    get_uiDescription: function () {
        var urlName = this._settingsData.settings.UrlName;
        if (urlName) {
            if (this._feedsBaseUrl.charAt(this._feedsBaseUrl.length - 1) != "/")
                this._feedsBaseUrl = this._feedsBaseUrl + "/";
            var feedUrl = this._feedsBaseUrl + encodeURIComponent(urlName);
            return String.format("{0}<a href=\"{1}\" target=\"_blank\">{1}</a>", this._shortDescriptionBase, feedUrl);
        }
        else {
            return this._urlNameNotSet;
        }
    },
    applyChanges: function () {
        var selectedUser = $("#UsrSelect :selected").val(); //$("#UsrSelect option:selected").data("token");
        var selectedApp = $(".sfSocialAppsSelect .sfSel a:first").text();

        if (selectedApp == null || selectedUser == null) {
            return false;
        }

        this._settingsData.pipe.UIDescription = this.get_uiDescription();
        this._settingsData.settings.AppNameReference = selectedApp;
        this._settingsData.settings.UserNameReference = selectedUser;
        return true;
    },
    _getSelectedLanguage: function () {
        if (this._languageChoiceField) {
            var languageSelector = $find(this._languageChoiceField.id);
            if (languageSelector) {
                this._clearArray(this._settingsData.pipe.LanguageIds);
                this._settingsData.pipe.LanguageIds.push(languageSelector.get_value());
            }
        }
    },
    _setLanguage: function () {
        if (this._languageChoiceField) {
            if (this._settingsData.pipe.LanguageIds && this._settingsData.pipe.LanguageIds.length > 0) {
                var languageSelector = $find(this._languageChoiceField.id);
                if (languageSelector) {
                    languageSelector._selectItemByValue(this._settingsData.pipe.LanguageIds);
                }
            }
        }
    },
    _clearArray: function (arr) {
        arr.splice(0, arr.length);
    },
    _appsBinderItemDataBound: function (sender, args) {
        var dataItem = args.get_dataItem();
        var settings = Sys.Serialization.JavaScriptSerializer.deserialize(this._settingsData.pipe.Settings);
        if (dataItem.key.Name == settings['AppNameReference']) {
            SelectTwitterApp(sender, args);
            $("#UsrSelect option[value='" + settings['UserNameReference'] + "']").attr("selected", "selected");
        }
    },
    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    },
    get_languageChoiceField: function () {
        return this._languageChoiceField;
    },
    set_languageChoiceField: function (val) {
        this._languageChoiceField = val;
    },
    get_appsBinder: function () {
        return this._appsBinder;
    },
    set_appsBinder: function (val) {
        this._appsBinder = val;
    }
};

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterPipeDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();