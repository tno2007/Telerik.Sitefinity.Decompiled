// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.ModulePermissionsPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Permissions;

namespace Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views
{
  public class ModulePermissionsPanel : AjaxDialogBase
  {
    private string templateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Content.ModulePermissionsPanel.ascx");

    protected override void InitializeControls(GenericContainer viewContainer)
    {
      if (!string.IsNullOrEmpty(this.PermissionSetName))
        this.GlobalItemsActionPermissionsList.PermissionsSetName = this.PermissionSetName;
      this.GlobalItemsActionPermissionsList.SecuredObjectID = this.SecuredObjectId;
      this.BackToModuleLink.Text = this.BackButtonText;
    }

    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? this.templateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    public Guid SecuredObjectId { get; set; }

    public string PermissionSetName { get; set; }

    public string BackButtonText { get; set; }

    /// <summary>
    /// Instance of the ItemActionPermissionsList - which displays permissions by a list of users per action.
    /// </summary>
    protected virtual ItemActionPermissionsList GlobalItemsActionPermissionsList => this.Container.GetControl<ItemActionPermissionsList>("globalItemsActionPermissionsList", true);

    protected virtual LinkButton BackToModuleLink => this.Container.GetControl<LinkButton>("backToModule", true);
  }
}
