/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.Designers.Views");

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView.initializeBase(this, [element]);
    this._widthTextField = null;
    this._heightTextField = null;
    this._rangeViolationMessageField = null;
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView.callBaseMethod(this, 'initialize');
    },
    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        if (controlData && controlData.MaxWidth) {
            this._widthTextField.set_value(controlData.MaxWidth);
        }
        else {
            this._widthTextField.reset();
        }
        if (controlData && controlData.MaxHeight) {
            this._heightTextField.set_value(controlData.MaxHeight);
        }
        else {
            this._heightTextField.reset();
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.MaxWidth = this._widthTextField.get_value() ? this._widthTextField.get_value() : 0;
        controlData.MaxHeight = this._heightTextField.get_value() ? this._heightTextField.get_value() : 0;
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    //Gets the widthTextField component.
    get_widthTextField: function () {
        return this._widthTextField;
    },
    //Sets the widthTextField component.
    set_widthTextField: function (value) {
        this._widthTextField = value;
    },

    //Gets the heightTextField component.
    get_heightTextField: function () {
        return this._heightTextField;
    },
    //Sets the heightTextField component.
    set_heightTextField: function (value) {
        this._heightTextField = value;
    }
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView.registerClass('Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLimitationsView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);