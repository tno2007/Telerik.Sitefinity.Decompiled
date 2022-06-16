Type.registerNamespace("Telerik.Sitefinity.Versioning.Web.UI.Basic");

Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView = function (element) { 
    this._loadDelegate = null;   
    this._settingsPanel = null;
    this._scheduledTaskInfoPanel = null;
    this._settingsStatusLabel = null;
    this._labelManager = null;
    this._versionsNumber = null;
    this._nextTaskExecuteTimeFormatted = null;
    this._currentTaskExecuteTimeFormatted = null; 
    this._isTaskScheduled = null;
    this._isTaskRunning = null;
    this._versionsNumberFocusLostDelegate = null;
    this._limitStatusChoiceField = null;
    this._limitStatusChangeDelegate = null;
    this._saveButton = null;
    this._saveDelegate = null;

    Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView.initializeBase(this, [element]);
}

Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView.callBaseMethod(this, "initialize");
        
        this.get_versionsNumber().get_textBoxElement().type = "number";

        if (this.get_limitStatusChoiceField() != null) {
            this._limitStatusChangeDelegate = Function.createDelegate(this, this.statusChangedHandler);
            this.get_limitStatusChoiceField().add_valueChanged(this._limitStatusChangeDelegate);            
        }

        if (this.get_versionsNumber() != null) {            
            this._versionsNumberFocusLostDelegate = Function.createDelegate(this, this.versionsNumberFocusLostHandler);
            $addHandler(this.get_versionsNumber().get_textBoxElement(), "blur", this._versionsNumberFocusLostDelegate);          
        }

        this._saveButton = jQuery(".sfSave")[0];
        if (this._saveButton)
        {
            this._saveDelegate = Function.createDelegate(this, this.saveHandler);
            $addHandler(this._saveButton, "click", this._saveDelegate);
        }      
    },

    dispose: function () {
        Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView.callBaseMethod(this, "dispose");

        if(this._limitStatusChangeDelegate) {
            delete this._limitStatusChangeDelegate;
        }

        if(this._versionsNumberFocusLostDelegate) {
            delete this._versionsNumberFocusLostDelegate;
        }

        if (this._saveDelegate) {
            delete this._saveDelegate;            
        }
    }, 

    versionsNumberFocusLostHandler: function (e) {
      if(this.get_versionsNumber().get_value() === '') {
           this.get_versionsNumber().set_value(1);
      }
      else if (this.get_versionsNumber().isChanged() && this.get_versionsNumber().validate()) {
            this.get_versionsNumber()._value = this.get_versionsNumber().get_value();
      }
    },  

    saveHandler: function (e, args) { 
        if(this.get_limitStatusChoiceField().get_value() == "true" && 
           (this.get_versionsNumber().validate() || this.get_isTaskScheduled())) {
                var statusInfoText = this.get_nextTaskExecuteTimeFormatted();           
                this.setTaskInfo(statusInfoText);
                this.set_isTaskScheduled(true);
        }
        else
        {
            this.clearTaskInfo();
            this.set_isTaskScheduled(false);
        }

    },   

    statusChangedHandler: function (sender) {
        var statusChoice = sender.get_value();        
        switch (statusChoice) {
            case "true":
                this._hideAllConfigurationFields();
                jQuery('#' + this.get_settingsPanel()).show();

                if(this.get_isTaskScheduled()) {
                    var statusInfoText = this.get_currentTaskExecuteTimeFormatted();           
                    this.setTaskInfo(statusInfoText);
                }

                var previousVersionsNumberValue = this.get_versionsNumber()._value;
                if (previousVersionsNumberValue != "") {
                    this.get_versionsNumber().set_value(previousVersionsNumberValue);
                    this.get_versionsNumber().validate()
                }

                break;           
            default:
                this._hideAllConfigurationFields();
                this.clearTaskInfo();                
                break;
        }
    },

    _hideAllConfigurationFields: function () {
        jQuery('#' + this.get_settingsPanel()).hide();     
    },

    setTaskInfo: function(text) {
        var statusInfoText = this.get_labelManager().getLabel('Labels', 'VersionCleanerScheduledLabel').replace("{0}", text);           

        $(this.get_settingsStatusLabel()).text(statusInfoText);       
        jQuery('#' + this.get_scheduledTaskInfoPanel()).show();
    },

    clearTaskInfo: function() {
        jQuery('#' + this.get_scheduledTaskInfoPanel()).hide();
    }, 

    get_nextTaskExecuteTimeFormatted: function() {
        return this._nextTaskExecuteTimeFormatted;
    },
    set_nextTaskExecuteTimeFormatted: function(value) {
        this._nextTaskExecuteTimeFormatted = value;
    },
    get_currentTaskExecuteTimeFormatted: function() {
        return this._currentTaskExecuteTimeFormatted;
    },
    set_currentTaskExecuteTimeFormatted: function(value) {
        this._currentTaskExecuteTimeFormatted = value;
    },
    set_isTaskRunning: function(value) {
        this._isTaskRunning = value;
    },
    get_isTaskRunning: function() {
        return this._isTaskRunning;
    },
    set_isTaskScheduled: function(value) {
        this._isTaskScheduled = value;
    },
    get_isTaskScheduled: function() {
        return this._isTaskScheduled;
    },  
    get_settingsPanel: function() {
        return this._settingsPanel;
    },
    set_settingsPanel: function(value) {
        this._settingsPanel = value;
    },
    get_scheduledTaskInfoPanel: function() {
        return this._scheduledTaskInfoPanel;
    },
    set_scheduledTaskInfoPanel: function(value) {
        this._scheduledTaskInfoPanel = value;
    },
    get_settingsStatusLabel: function () {
        return this._settingsStatusLabel;
    },
    set_settingsStatusLabel: function (value) {
        this._settingsStatusLabel = value;
    },
    get_versionsNumber: function() {
        return this._versionsNumber;
    },
    set_versionsNumber: function(value) {
        this._versionsNumber = value;
    },
    get_limitStatusChoiceField: function() {
        return this._limitStatusChoiceField;
    },
    set_limitStatusChoiceField: function(value) {
        this._limitStatusChoiceField = value;
    },   
    get_labelManager: function () {
        return this._labelManager;
    },
    set_labelManager: function (value) {
        this._labelManager = value;
    }
};

Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView.registerClass("Telerik.Sitefinity.Versioning.Web.UI.Basic.RevisionHistoryBasicSettingsView", Telerik.Sitefinity.Web.UI.Fields.FieldControl);