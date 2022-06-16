Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView.initializeBase(this, [element]);
    this._columnsModeChoiceField = null;
    this._cssClassTextField = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._cssClassTextField.set_value(controlData.CssClass);
        if (this._columnsModeChoiceField) {
            this._columnsModeChoiceField.set_value(controlData.FormControlColumnsMode);
        }
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.CssClass = this._cssClassTextField.get_value();
        if (this._columnsModeChoiceField) {
            controlData.FormControlColumnsMode = this._columnsModeChoiceField.get_value();
        }
    },

    // gets the javascript control object that is being designed
    get_controlData: function () {
        return this.get_parentDesigner().get_propertyEditor().get_control();
    },

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    // IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () { return this._parentDesigner; },

    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) { this._parentDesigner = value; },

    // Returns the property editor of the current view
    get_propertyEditor: function () {
        if (this.get_parentDesigner()) {
            return this.get_parentDesigner().get_propertyEditor();
        }
        return null;
    },

    // Gets the choicefield for setting the size of the texbox
    get_columnsModeChoiceField: function () { return this._columnsModeChoiceField; },
    // Sets the choicefield for setting the size of the texbox
    set_columnsModeChoiceField: function (value) { this._columnsModeChoiceField = value; },

    // Gets the textfield for the CSS class
    get_cssClassTextField: function () { return this._cssClassTextField; },
    // Sets the textfield for the CSS class
    set_cssClassTextField: function (value) { this._cssClassTextField = value; }

}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldColumnsAppearanceView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
