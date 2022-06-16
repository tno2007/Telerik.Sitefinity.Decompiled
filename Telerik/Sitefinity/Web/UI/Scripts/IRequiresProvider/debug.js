Type.registerNamespace("Telerik.Sitefinity.Web.UI.Scripts");
// If this interface is implemented the controls binder will set the provider
// instead the value. Thi way the control will have access to items from specific provider
Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider = function () { }

Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.prototype = {
    // Passes the provider to the control
    set_providerName: function (value) { },

    // Gets the provider from the control
    get_providerName: function () { }
};

Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider.registerInterface("Telerik.Sitefinity.Web.UI.Scripts.IRequiresProvider");