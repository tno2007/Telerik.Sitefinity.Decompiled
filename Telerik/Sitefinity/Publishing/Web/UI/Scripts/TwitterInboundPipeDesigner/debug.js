/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.3.2.min-vsdoc2.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("TwitterInboundPipeDesigner.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner.initializeBase(this, [element]);
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
    this._SearchPattern = null;
    this._MaxItems = null;


    this._scheduleTime = null;
    this._scheduleTypeElement = null;
    this._scheduleDaysElement = null;
};

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner.callBaseMethod(this, 'initialize');

        this._appsBinderItemDataBoundDelegate = Function.createDelegate(this, this._appsBinderItemDataBound);
        this._appsBinder.add_onItemDataBound(this._appsBinderItemDataBoundDelegate);

        this.get_scheduleTime().set_datePickerFormat("", "hh:mm:ss");


        if (this._scheduleTypeElement) {
            if (this._selectScheduleTypeDelegate == null)
                this._selectScheduleTypeDelegate = Function.createDelegate(this, this._selectScheduleTypeChanged);
            $addHandler(this._scheduleTypeElement, "change", this._selectScheduleTypeDelegate);
        }
    },
    dispose: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner.callBaseMethod(this, 'dispose');
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
    _selectScheduleTypeChanged: function () {
        this._showHideScheduledDaysAndTime();
    },
    _showHideScheduledDaysAndTime: function () {
        // Todo : remove schedule type daily and weekly hardcoded values
        //daily
        this.get_scheduleTime().get_datePicker().hide();
        this.get_scheduleDays().hide();
        if (this.get_scheduleTypeElement().value == "4") {
            this.get_scheduleTime().get_datePicker().show();
        }
        //weekly
        if (this.get_scheduleTypeElement().value == "5") {
            this.get_scheduleDays().show();
            this.get_scheduleTime().get_datePicker().show();
        }
    },


    _refreshScheduleData: function () {
        var settings = this._settingsData.settings;
        if (settings) {
            var scheduleType = settings.ScheduleType;
            if (scheduleType && scheduleType > 0)
                this.get_scheduleType().val(settings.ScheduleType);

            this._showHideScheduledDaysAndTime();
            this.get_scheduleDays().val(settings.ScheduleDay);
        }
        if (this._settingsData.pipe && this._settingsData.pipe.ScheduleTime) {
            this.get_scheduleTime().set_value(new Date(this._settingsData.pipe.ScheduleTime));
        }

        if (this._settingsData.settings.SearchPattern != 'undefined')
            this.get_SearchPattern().set_value(this._settingsData.settings.SearchPattern);

        if (this._settingsData.settings.MaxItems != 'undefined')
            this.get_MaxItems().set_value(this._settingsData.settings.MaxItems);
    },

    refreshUI: function () {
        this._refreshScheduleData();
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
        var selectedUser = $("#InboundUsrSelect :selected").val(); //$("#UsrSelect option:selected").data("token");
        var selectedApp = $(".sfSocialAppsSelect .sfSel a:first").text();

        if (selectedApp == null || selectedUser == null) {
            return false;
        }

        this._settingsData.pipe.UIDescription = this.get_uiDescription();
        this._settingsData.settings.AppNameReference = selectedApp;
        this._settingsData.settings.UserNameReference = selectedUser;

        this._settingsData.pipe.ScheduleTime = new Date(this.get_scheduleTime().get_value());
        this._settingsData.settings.ScheduleType = this.get_scheduleType().val();
        this._settingsData.settings.ScheduleDay = this.get_scheduleDays().val();
        this._settingsData.settings.SearchPattern = this.get_SearchPattern().get_value();
        this._settingsData.settings.MaxItems = this.get_MaxItems().get_value();
        

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
            $("#InboundUsrSelect option[value='" + settings['UserNameReference'] + "']").attr("selected", "selected");
        }
    },

    get_scheduleTime: function () {
        return this._scheduleTime;
    },
    set_scheduleTime: function (val) {
        this._scheduleTime = val;
    },
    get_scheduleType: function () {
        return $(this._scheduleTypeElement);
    },
    get_scheduleTypeElement: function () {
        return this._scheduleTypeElement;
    },
    set_scheduleTypeElement: function (val) {
        this._scheduleTypeElement = val;
    },
    get_scheduleDays: function () {
        return $(this._scheduleDaysElement);
    },
    get_scheduleDaysElement: function () {
        return this._scheduleDaysElement;
    },
    set_scheduleDaysElement: function (val) {
        this._scheduleDaysElement = val;
    },

    get_SearchPattern: function () {
        return this._SearchPattern;
    },
    set_SearchPattern: function (value) {
        this._SearchPattern = value;
    },
    get_MaxItems: function () {
        return this._MaxItems;
    },
    set_MaxItems: function (value) {
        this._MaxItems = value;
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

Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.TwitterInboundPipeDesigner',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();