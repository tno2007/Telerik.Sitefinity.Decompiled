Type._registerScript("PagePipeDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Publishing.Web.UI.Designers");

Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView = function (element) {
    Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView.initializeBase(this, [element]);

    this._refreshing = false;
    this._dataControlIdMap = null;
    this._shortDescription = null;
    this._settingsData = {};
    this._defaultSettings = {};
    this._openMappingSettingsButton = null;
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView.prototype = {
    initialize: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView.callBaseMethod(this, 'initialize');
        // here be initialization stuff
    },
    dispose: function () {
        Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView.callBaseMethod(this, 'dispose');
        // here be disposal stuff
    },

    //------------ #region IDesignerViewControl Overrides ------------

    set_controlData: function (value) {
        this._settingsData = value;
        this.refreshUI();
    },
    get_controlData: function () {
        return this._settingsData;
    },

    refreshUI: function () {
        this._refreshing = true;

        for (var dataFieldName in this._dataControlIdMap) {
            var fldCntrl = this._getFieldControl(dataFieldName);
            var fValue = this._settingsData.settings[dataFieldName];
            if (fValue)
                fldCntrl.set_value(fValue);
        }

        this._refreshing = false;
    },

    applyChanges: function () {
        for (var dataFieldName in this._dataControlIdMap) {
            var fldCntrl = this._getFieldControl(dataFieldName);
            if (fldCntrl.get_value() !== undefined && fldCntrl.get_value() !== null) {
                var value = fldCntrl.get_value();
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

    //------------ #endregion IDesignerViewControl Overrides ------------

    validate: function () {
        var isValid = true;
        for (var dataFieldName in this._dataControlIdMap) {
            var fldCntrl = this._getFieldControl(dataFieldName);
            isValid = isValid && fldCntrl.validate();
        }
        return isValid;
    },

    set_dataControlIdMap: function (value) {
        this._dataControlIdMap = value;
    },

    // sets the object which represents the map of field properties and respective controls
    // that are used to edit them
    get_dataControlIdMap: function () {
        return this._dataControlIdMap;
    },

    // gets the reference to the field control by the field name that it edits
    _getFieldControl: function (dataFieldName) {
        return $find(this._dataControlIdMap[dataFieldName]);
    },

    // generates the UIDescription of the pipe
    get_uiDescription: function () {
        var page = "";
        return String.format(this._shortDescription, page);
    },

    get_openMappingSettingsButton: function () {
        return this._openMappingSettingsButton;
    },

    set_openMappingSettingsButton: function (val) {
        this._openMappingSettingsButton = val;
    }
}

Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView.registerClass('Telerik.Sitefinity.Publishing.Web.UI.Designers.PagePipeDesignerView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();
