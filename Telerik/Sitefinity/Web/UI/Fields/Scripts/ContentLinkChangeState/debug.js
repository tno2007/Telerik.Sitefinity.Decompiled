/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState = function () {
    /// <summary>
    /// Represents the different client side states when editing related items
    /// </summary>
};
Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.prototype =
            {
                Added : 0,
                Removed: 1,
                Updated: 2
            }
Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState.registerEnum("Telerik.Sitefinity.Web.UI.Fields.ContentLinkChangeState");