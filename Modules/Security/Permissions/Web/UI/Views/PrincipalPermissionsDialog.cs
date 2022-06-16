// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.PrincipalPermissionsDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Permissions;

namespace Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views
{
  public class PrincipalPermissionsDialog : AjaxDialogBase
  {
    public static readonly string UiPath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.PrincipalPermissionsDialog.ascx");

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (SystemManager.CurrentHttpContext == null)
        return;
      NameValueCollection queryString = SystemManager.CurrentHttpContext.Request.QueryString;
      this.PermissionsList.PrincipalID = new Guid(queryString["principalID"]);
      this.PermissionsList.PrincipalName = queryString["principalName"];
      this.PermissionsList.ShowPrincipalName = !string.IsNullOrEmpty(queryString["principalName"]);
      this.PermissionsList.BindOnLoad = true;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? PrincipalPermissionsDialog.UiPath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    protected virtual UserPermissionsList PermissionsList => this.Container.GetControl<UserPermissionsList>("permissionsList", true);
  }
}
