Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
// If this interface is implemented the field controls binder will set the data item context
// instead the value. Thi way the control will have access to all the properties of the item and context informationlike type, versioning info etc.
Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext = function() { }

Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext.prototype = {
    set_dataItemContext: function(value) { }
};

Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext.registerInterface("Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItemContext");
    