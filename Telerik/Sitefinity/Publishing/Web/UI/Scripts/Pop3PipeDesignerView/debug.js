/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("Pop3PipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._dataControlIdMap = null; 
    this._uiNameLabel = null;
    this._selectLanguage = null;

    // For workaround purposes. Provide specific parsing for ChoiceFields on RenderAs SingleCheckBox
    this._isUseSslDataFieldName = null;
    // For workaround purposes. Do not have client api member validate()
    this._passwordRadTextBoxDataFieldName = null;

    this._settingsData = {};
    this._shortDescription = null;
    this._defaultSettings = {};
    this._openMappingSettingsButton = null;

    this._scheduleTime = null;
    this._scheduleTypeElement = null;
    this._scheduleDaysElement = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView.callBaseMethod(this, 'initialize');

        this.get_scheduleTime().set_datePickerFormat("", "hh:mm:ss");
        if (this._scheduleTypeElement) {
            if (this._selectScheduleTypeDelegate == null)
                this._selectScheduleTypeDelegate = Function.createDelegate(this, this._selectScheduleTypeChanged);
            $addHandler(this._scheduleTypeElement, "change", this._selectScheduleTypeDelegate);
        }
    },
    dispose: function () {
        if (this._scheduleTypeElement) {
            $removeHandler(this._scheduleTypeElement, "change", this._selectScheduleTypeDelegate);
            if (this._selectScheduleTypeDelegate != null)
                delete this._selectScheduleTypeDelegate;
        }
        Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView.callBaseMethod(this, 'dispose');
    },
    _selectLanguageChanged: function () {
        this._setLanguage();
    },

    _setLanguage: function () {
        if (this._selectLanguage) {
            if (this._settingsData.pipe.LanguageIds && this._settingsData.pipe.LanguageIds.length > 0) {
                var languageSelector = $find(this._selectLanguage.id);
                if (languageSelector) {
                    languageSelector._selectItemByValue(this._settingsData.pipe.LanguageIds);
                }
            }
        }
    },
    _getSelectedLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = $find(this._selectLanguage.id);
            if (languageSelector) {
                this._clearArray(this._settingsData.pipe.LanguageIds);
                this._settingsData.pipe.LanguageIds.push(languageSelector.get_value());
            }
        }
    },
    _clearArray: function (arr) {
        arr.splice(0, arr.length);
    },
    validate: function () {
        var isValid = true;
        for (var dataFieldName in this._dataControlIdMap) {
            // workaround as RadTextBox has no client api member validate():
            if (this.get_passwordRadTextBoxDataFieldName() !== dataFieldName) {
                var cnt = this._getFieldControl(dataFieldName);
                isValid = isValid && cnt.validate();
            }
        }
        return isValid;
    },
    // Returns true if there are changes in the designer
    //    isChanged: function () {
    //        
    //    },
    refreshUI: function () {
        this._refreshing = true;
        this._setDefaultValueForScheduleTypeSelector();

        for (var dataFieldName in this._dataControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            var fValue = this._settingsData.settings[dataFieldName];
            if (fValue)
                cnt.set_value(fValue);
        }

        if (this._settingsData.settings.UIName != 'undefined')
            this._uiNameLabel.innerHTML = this._settingsData.settings.UIName;
        if (this._settingsData.pipe.UIName != 'undefined')
            this._uiNameLabel.innerHTML = this._settingsData.pipe.UIName;


        this._refreshScheduleData();
        this._refreshing = false;
    },
    applyChanges: function () {
        for (var dataFieldName in this._dataControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            if (cnt.get_value() !== undefined && cnt.get_value() !== null) {
                var value = cnt.get_value();
                // provides specific parsing for ChoiceFields on RenderAs SingleCheckBox:
                if ((value === "false" || value === "true") && dataFieldName === this.get_isUseSslDataFieldName()) {
                    if (value === "false") {
                        value = false;
                    }
                    else {
                        value = true;
                    }
                }
                this._settingsData.settings[dataFieldName] = value;

                this._settingsData.settings.ScheduleType = this.get_scheduleType().val();
                this._settingsData.settings.ScheduleDay = this.get_scheduleDays().val();
                this._settingsData.pipe.ScheduleTime = new Date(this.get_scheduleTime().get_value());
            }
        }
        this._settingsData.pipe.UIDescription = this.get_uiDescription();
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
    },
    _setDefaultValueForScheduleTypeSelector: function () {
        //default value for schedule type should be 5 min
        //this.get_scheduleType().val(1);
        this.get_scheduleDays().hide();
        this.get_scheduleTime().get_datePicker().hide();
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
    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataControlIdMap[dataFieldName]);
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
    get_isUseSslDataFieldName: function () {
        return this._isUseSslDataFieldName;
    },
    set_isUseSslDataFieldName: function (value) {
        this._isUseSslDataFieldName = value;
    },
    get_passwordRadTextBoxDataFieldName: function () {
        return this._passwordRadTextBoxDataFieldName;
    },
    set_passwordRadTextBoxDataFieldName: function (value) {
        this._passwordRadTextBoxDataFieldName = value;
    },
    // gets the object which represents the map of field properties and respective controls
    // that are used to edit them
    set_dataControlIdMap: function (value) {
        this._dataControlIdMap = value;
    },
    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataControlIdMap: function () {
        return this._dataControlIdMap;
    },
    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var serverName = this._settingsData.settings.Pop3Server;
        var accountName = this._settingsData.settings.Pop3UserName;
        if (serverName) {
            return String.format(this._shortDescription, serverName, accountName);
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
    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    },
    get_selectLanguage: function () {
        return this._selectLanguage;
    },
    set_selectLanguage: function (value) {
        this._selectLanguage = value;
    }
}
Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.Pop3PipeDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
