/// <reference name="MicrosoftAjax.js"/>
/// <reference path="jquery-1.3.2-vsdoc2.js" />
Type._registerScript("IDesignerViewControl.js");
Type.registerNamespace("Telerik.Sitefinity.Web.UI.ControlDesign");

Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl = function() { };
Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl.prototype = {


    // gets the reference fo the propertyEditor control
    get_parentDesigner: function() {
    },
    // sets the reference fo the propertyEditor control
    set_parentDesigner: function(value) {
    },
   
    /// <summary>
    /// Base interface for all design controls
    /// </summary>

    set_controlData: function(value) {
    /// <summary>
    ///  Sets the control data to the component which is reponsible for interpration
    /// </summary>
    /// <param name="value">The controlData object</param>

    },
    refreshUI: function() {
    /// <summary>
    /// Forces the control to refersh from the control Data
    /// </summary>
    },
    
    applyChanges: function() {
    ///forces the designer view to apply the changes on UI to the control Data
    },

	notifyViewSelected: function(){
	/// <summary>
	/// Notifies the selected view that it is active
	/// </summary>
	}
};

Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl.registerInterface("Telerik.Sitefinity.Web.UI.ControlDesign.IDesignerViewControl");