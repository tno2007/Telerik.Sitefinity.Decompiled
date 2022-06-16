﻿/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("RssAtomPipeImportDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._dataFieldNameControlIdMap = null;
    this._settingsData = {};
    this._uiNameLabel = null;
    this._radioChoices = null;
    this._shortDescriptionBase = null;
    this._urlNameNotSet = null;
    this._defaultSettings = {};
    this._openMappingSettingsButton = null;

    this._scheduleTime = null;
    this._scheduleTypeElement = null;
    this._scheduleDaysElement = null;
    this._selectLanguage = null;
    this._errorMessageHolderId = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView.callBaseMethod(this, 'initialize');

        this._radioClickDelegate = Function.createDelegate(this, this._setRssSettings);
        this.get_radioChoices().click(this._radioClickDelegate);
        this.get_scheduleTime().set_datePickerFormat("", "hh:mm:ss");  

        if (this._scheduleTypeElement) {
            if (this._selectScheduleTypeDelegate == null)
                this._selectScheduleTypeDelegate = Function.createDelegate(this, this._selectScheduleTypeChanged);
            $addHandler(this._scheduleTypeElement, "change", this._selectScheduleTypeDelegate);
        }
       $("#" + this._errorMessageHolderId).hide();
    },
    dispose: function () {
       if (this._scheduleTypeElement) {
            $removeHandler(this._scheduleTypeElement, "change", this._selectScheduleTypeDelegate);
            if (this._selectScheduleTypeDelegate != null)
                delete this._selectScheduleTypeDelegate;
        }

        Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView.callBaseMethod(this, 'dispose');
    },

    validate: function () {
        var isValid = false;
        var urlName;
        var re = /^(http[s]?:\/\/)/;
        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            if (this.isEnabledElement(cnt.get_element())) {
                if (dataFieldName == "UrlName") {
                    urlName = cnt._element.control._textBoxElement.value;
                }
                isValid = isValid && cnt.validate();
            }
        }
  
        if (re.test(urlName)) {
            isValid = true;
        }
        else {
            var errorHolder = $("#" + this._errorMessageHolderId);
            var errorMessage = this._labelManager.getLabel("PublishingMessages", "IncorrectUrlFormat");

            errorHolder.html(errorMessage);
            errorHolder.show();
            dialogBase.resizeToContent();
            isValid = false;
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

        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            var fValue = this._settingsData.settings[dataFieldName];
            if (fValue != null && fValue != undefined)
                cnt.set_value(fValue);
        }
        if (this._settingsData.settings.MaxItems == 0) {
            this._checkRadioButton("listRadio_includeAll");
            this.disableElement(this._getFieldControl("MaxItems").get_element());
        }
        else {
            this._checkRadioButton("listRadio_includeNewest");
            this.enableElement(this._getFieldControl("MaxItems").get_element());
        }

        if (this._settingsData.settings.UIName != 'undefined')
            this._uiNameLabel.innerHTML = this._settingsData.settings.UIName;
        if (this._settingsData.pipe.UIName != 'undefined')
            this._uiNameLabel.innerHTML = this._settingsData.pipe.UIName;

        this._refreshScheduleData();

        this._refreshing = false;
    },
    applyChanges: function () {
        for (var dataFieldName in this._dataFieldNameControlIdMap) {
            var cnt = this._getFieldControl(dataFieldName);
            if (cnt.get_value() != '' && this.isEnabledElement(cnt.get_element()))
                this._settingsData.settings[dataFieldName] = cnt.get_value();
        }
        this._settingsData.pipe.UIDescription = this.get_uiDescription();
        this._settingsData.settings.ScheduleType = this.get_scheduleType().val();
        this._settingsData.settings.ScheduleDay = this.get_scheduleDays().val();
        this._settingsData.pipe.ScheduleTime = new Date(this.get_scheduleTime().get_value());
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
    _getSelectedLanguage: function () {
        if (this._selectLanguage) {
            var languageSelector = $find(this._selectLanguage.id);
            if (languageSelector) {
                this._clearArray(this._settingsData.pipe.LanguageIds);
                this._settingsData.pipe.LanguageIds.push(languageSelector.get_value());
            }
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
    _clearArray: function (arr) {
        arr.splice(0, arr.length);
    },
    _setRssSettings: function (sender, args) {
        if (!this._refreshing) {
            var radioID = sender.target.value;
            if (radioID == "listRadio_includeNewest")
                this.enableElement(this._getFieldControl("MaxItems").get_element());
            if (radioID == "listRadio_includeAll") {
                this.disableElement(this._getFieldControl("MaxItems").get_element());
                //this._getFieldControl("MaxItems").set_value(0);
                this._settingsData.settings.MaxItems = 0;
            }
        }
    },
    _checkRadioButton: function (value) {
        var radios = this.get_radioChoices();
        radios.filter(function (i) { return this.value == value; }).attr('checked', true);
    },
    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataFieldNameControlIdMap[dataFieldName]);
    },
    // gets all the radio buttons in the container of this control
    get_radioChoices: function () {
        if (!this._radioChoices) {
            this._radioChoices = jQuery(this.get_element()).find('input[type|=radio]');
        }
        return this._radioChoices;
    },
    // gets the object which represents the map of field properties and respective controls
    // that are used to edit them
    set_dataFieldNameControlIdMap: function (value) {
        this._dataFieldNameControlIdMap = value;
    },
    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataFieldNameControlIdMap: function () {
        return this._dataFieldNameControlIdMap;
    },
    enableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).removeAttr('disabled');
        });
    },
    isEnabledElement: function (domElement) {
        var enabled = false;
        $(domElement).find('input').each(function () {
            enabled = !($(this).attr('disabled'));
        });
        return enabled;
    },
    disableElement: function (domElement) {
        $(domElement).find('input').each(function () {
            $(this).attr('disabled', 'disabled');
        });
    },
    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var urlName = this._settingsData.settings.UrlName;
        if (urlName) {
            return String.format("{0}<a href=\"{1}\" target=\"_blank\">{1}</a>", this._shortDescriptionBase, urlName);
        }
        else {

           return this._urlNameNotSet;
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
    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    },
    set_uiNameLabel: function (value) {
        this._uiNameLabel = value;
    },
    get_selectLanguage: function () {
        return this._selectLanguage;
    },
    set_selectLanguage: function (value) {
        this._selectLanguage = value;
    },
    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.RssAtomPipeImportDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
