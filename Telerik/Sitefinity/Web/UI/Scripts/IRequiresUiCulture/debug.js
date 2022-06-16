Type.registerNamespace("Telerik.Sitefinity.Web.UI.Scripts");
// If this interface is implemented the controls binder will set the UI culture.
// This way the control will have access to items in a specific culture.
Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture = function () { }

Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.prototype = {
    // Passes the UI culture to the control
    set_uiCulture: function (value) { },

    // Gets the UI culture from the control
    get_uiCulture: function () { }
};

Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture.registerInterface("Telerik.Sitefinity.Web.UI.Scripts.IRequiresUiCulture");