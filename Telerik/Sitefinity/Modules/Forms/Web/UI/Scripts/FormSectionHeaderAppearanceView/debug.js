Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView.initializeBase(this, [element]);
    this._titleFontSizeChoiceField = null;
    this._cssClassTextField = null;
    this._wrappingTagTextField = null;
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();
        this._cssClassTextField.set_value(controlData.CssClass);
        this._titleFontSizeChoiceField.set_value(controlData.TitleFontSize);
        this._wrappingTagTextField.set_value(controlData.WrapperTag);
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.CssClass = this._cssClassTextField.get_value();
        controlData.TitleFontSize = this._titleFontSizeChoiceField.get_value();
        controlData.WrapperTag = this._wrappingTagTextField.get_value();
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
    get_titleFontSizeChoiceField: function () { return this._titleFontSizeChoiceField; },
    // Sets the choicefield for setting the size of the texbox
    set_titleFontSizeChoiceField: function (value) { this._titleFontSizeChoiceField = value; },

    // Gets the textfield for the CSS class
    get_cssClassTextField: function () { return this._cssClassTextField; },
    // Sets the textfield for the CSS class
    set_cssClassTextField: function (value) { this._cssClassTextField = value; },

    get_wrappingTagTextField: function () { return this._wrappingTagTextField; },
    set_wrappingTagTextField: function (value) { this._wrappingTagTextField = value; }

}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderAppearanceView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
