/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type._registerScript("SearchIndexExportPipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView.initializeBase(this, [element]);
    this._refreshing = false;
    this._dataControlIdMap = null;
    this._uiNameLabel = null;

    // For workaround purposes. Provide specific parsing for ChoiceFields on RenderAs SingleCheckBox
    this._isUseSslDataFieldName = null;
    // For workaround purposes. Do not have client api member validate()
    this._passwordRadTextBoxDataFieldName = null;

    this._settingsData = {};
    this._shortDescription = null;
    this._defaultSettings = {};
    this._openMappingSettingsButton = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView.callBaseMethod(this, 'dispose');
    },
    set_controlData: function (value) {
        this._settingsData = value;
        this.refreshUI();
    },
    get_controlData: function (value) {
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
            }
        }
        this._settingsData.pipe.UIDescription = this.get_uiDescription();
    },
    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataControlIdMap[dataFieldName]);
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

    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },
    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.SearchIndexExportPipeDesignerView',
    Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
