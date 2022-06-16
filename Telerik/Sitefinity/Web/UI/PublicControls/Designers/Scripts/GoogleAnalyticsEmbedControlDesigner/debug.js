/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type.registerNamespace("Telerik.Sitefinity.Web.UI.PublicControls.Designers.Views");

Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner = function (element) {
    Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner.initializeBase(this, [element]);

    this._parentDesigner = null;
    this._googleAnalyticsCodeTextField = null;
    this._scriptEmbedPositionChoiceField = null;
}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */
    // implementation of IControlDesigner: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData.GoogleAnalyticsCode) {
            this.get_googleAnalyticsCodeTextField().set_value(controlData.GoogleAnalyticsCode);
        }
    },

    // implementation of IControlDesigner: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {

        var controlData = this.get_controlData();
        controlData.GoogleAnalyticsCode = this.get_googleAnalyticsCodeTextField().get_value();


    },
    // gets the GoogleAnalytics control object that is being designed
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    get_googleAnalyticsCodeTextField: function () { return this._googleAnalyticsCodeTextField; },
    set_googleAnalyticsCodeTextField: function (value) { this._googleAnalyticsCodeTextField = value; }
}

Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner.registerClass('Telerik.Sitefinity.Web.UI.PublicControls.Designers.GoogleAnalyticsEmbedControlDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();