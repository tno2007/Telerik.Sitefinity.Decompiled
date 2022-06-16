/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>
Type._registerScript("CustomListSettingsDesignerView.js", ["IDesignerViewControl.js"]);
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign.ContentView");

Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView = function (element) {
	this._sortableFields = null;

	Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.initializeBase(this, [element]);
}

Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {
    	Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.callBaseMethod(this, 'initialize');
	},

    dispose: function () {
        //Add custom dispose actions here
    	Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.callBaseMethod(this, 'dispose');
	},

    /* --------------------------------- public functions --------------------------------- */

    applyChanges: function () {
    	Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.callBaseMethod(this, 'applyChanges');
    },

    refreshUI: function () {
    	Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.callBaseMethod(this, 'refreshUI');
	},

	notifyViewSelected: function () {
		var currentView = this.get_currentView();
		var masterDefinitions = this.get_controlData().ControlDefinition.Views["UserProfilesFrontendMaster"];
		var sortExpressionsField = this._getFieldControl("CommonMasterDefinition.SortExpression")
		sortExpressionsField.clearListItems();
		var newOptions = this._sortableFields[masterDefinitions.ProfileTypeFullName];
		for (var option in newOptions) {
			sortExpressionsField.addListItem(option, newOptions[option]);
		}

		sortExpressionsField.set_value(currentView.SortExpression);
	},

	/* --------------------------------- event handlers --------------------------------- */

	/* --------------------------------- private functions --------------------------------- */

	/* --------------------------------- properties functions --------------------------------- */

    // gets the reference to the parent designer control
    get_parentDesigner: function () {
        return this._parentDesigner;
    },

    // sets the reference fo the parent designer control
    set_parentDesigner: function (value) {
        this._parentDesigner = value;
    },

	get_sortableFields: function () {
		return this._sortableFields;
	},

	set_sortableFields: function (value) {
		this._sortableFields = value;
	}
}
      
Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView.registerClass('Telerik.Sitefinity.Web.UI.ControlDesign.ContentView.CustomListSettingsDesignerView', Telerik.Sitefinity.Web.UI.ControlDesign.ListSettingsDesignerView, Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl);

if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();