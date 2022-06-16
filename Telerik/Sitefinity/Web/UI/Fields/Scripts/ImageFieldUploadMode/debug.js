/// <reference name="MicrosoftAjax.js"/>
/// <reference name="Telerik.Sitefinity.Resources.Scripts.jquery-1.6.3-vsdoc.js" assembly="Telerik.Sitefinity.Resources"/>

Type.registerNamespace("Telerik.Sitefinity.Web.UI.Fields");

Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode = function () {
	/// <summary>
	/// Represents the different client side ImageField modes
	/// </summary>
};
Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode.prototype =
{
    NotSet: 0,
	Dialog: 1,
	InputField: 2
}

Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode.registerEnum("Telerik.Sitefinity.Web.UI.Fields.ImageFieldUploadMode");