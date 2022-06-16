/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields.Designers.Views");

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView = function (element) {
    Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView.initializeBase(this, [element]);
    this._viewsSelector = null;
	this._cssClassTextField = null;
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView.prototype = {
    /* --------------------------------- set up and tear down --------------------------------- */
    initialize: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView.callBaseMethod(this, 'initialize');
    },

    dispose: function () {
        Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // implementation of IDesignerViewControl: Forces the control to refersh from the control Data
    refreshUI: function () {
    	var controlData = this.get_controlData();
        var selectedViews = this.get_controlData().VisibleViews;
        var hidden = controlData.Hidden;

        if (selectedViews && selectedViews.length > 0) {
            this.get_viewsSelector().setSelectedViews(selectedViews);
        }
        else if (hidden) {
            this.get_viewsSelector().setSelectedViews("nowhere");
        }
        else {
            this.get_viewsSelector().setSelectedViews("allViews");
        }

		this._cssClassTextField.set_value(controlData.CssClass);
    },

    // implementation of IDesignerViewControl: forces the designer view to apply the changes on UI to the control Data
    applyChanges: function () {
    	var controlData = this.get_controlData();
        var selectedViews = this.get_viewsSelector().getSelectedViews();
        controlData.VisibleViews = selectedViews;
        if (selectedViews === "allViews") {
        	controlData.Hidden = "false";
        }
        else if (selectedViews === "nowhere") {
        	controlData.Hidden = "true";
        }

        controlData.CssClass = this._cssClassTextField.get_value() ? this._cssClassTextField.get_value() : "";
    },

    get_controlData: function () {
		return this.get_parentDesigner().get_propertyEditor().get_control();
	},

    /* --------------------------------- event handlers --------------------------------- */

    /* --------------------------------- private methods --------------------------------- */

    /* --------------------------------- properties --------------------------------- */

    get_viewsSelector: function () {
        return this._viewsSelector;
    },
    set_viewsSelector: function (value) {
        this._viewsSelector = value;
    },
	// IDesignerViewControl: gets the reference fo the propertyEditor control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },
    // IDesignerViewControl: sets the reference fo the propertyEditor control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

	get_cssClassTextField: function() {
		return this._cssClassTextField;
	},

	set_cssClassTextField: function (value) {
		this._cssClassTextField = value;
	}
}

Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView.registerClass('Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldAppearanceView', Sys.UI.Control, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);
