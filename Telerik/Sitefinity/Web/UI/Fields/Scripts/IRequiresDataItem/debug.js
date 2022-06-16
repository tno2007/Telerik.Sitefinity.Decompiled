Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");
// If this interface is implemented the field controls binder will set the data item
// instead the value. Thi way the control will have access to all the properties of the item
Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem = function() { }

Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem.prototype = {
    // Passes the data item to the field control and a boolean parameter specifying if the data item is the default one
    set_dataItem: function(value, isDefault) { }
};

Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem.registerInterface("Telerik.Sitefinity.Web.UI.Fields.IRequiresDataItem");
