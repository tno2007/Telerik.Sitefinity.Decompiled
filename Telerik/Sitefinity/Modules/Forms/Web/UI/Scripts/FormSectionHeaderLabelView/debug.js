/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views");

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView = function (element) {
    Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.initializeBase(this, [element]);
    this._titleTextField = null;
}


Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
        var controlData = this.get_controlData();

        this._titleTextField.set_value(controlData.Title);
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
        var controlData = this.get_controlData();
        controlData.Title = this._titleTextField.get_value();
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

    // Gets the textfield for the title of the section
    get_titleTextField: function () { return this._titleTextField; },
    // Sets the textfield for the title of the section
    set_titleTextField: function (value) { this._titleTextField = value; }
}

Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView.registerClass('Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormSectionHeaderLabelView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
