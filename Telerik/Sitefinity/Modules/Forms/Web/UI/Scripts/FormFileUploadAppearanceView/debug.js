Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView.initializeBase(this, [element]);

    this._cssClassTextField = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refresh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._cssClassTextField.set_value(controlData.CssClass);
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.CssClass = this._cssClassTextField.get_value();
    },

    // gets the JavaScript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference for the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },

    // IDesignerViewControl: sets the reference for the propertyEditor control
    set_parentDesigner: function (value) { this._parentDesigner = value; },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the TextField for the CSS class
    get_cssClassTextField: function () { return this._cssClassTextField; },
    // Sets the TextField for the CSS class
    set_cssClassTextField: function (value) { this._cssClassTextField = value; }

}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormFileUploadAppearanceView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
