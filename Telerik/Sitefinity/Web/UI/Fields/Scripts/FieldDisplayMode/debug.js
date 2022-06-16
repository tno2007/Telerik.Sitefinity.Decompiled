/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode = function() {
    /// <summary>
    /// Represents the different client side work modes
    /// </summary>
};
Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.prototype =
            {
                Read: 0,
                Write: 1
            }
Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode.registerEnum("Telerik.Sitefinity.Web.UI.Fields.FieldDisplayMode");