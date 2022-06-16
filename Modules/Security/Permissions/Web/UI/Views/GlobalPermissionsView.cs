// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.GlobalPermissionsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend.Security.Permissions;
using Telerik.Sitefinity.Web.UI.Backend.Security.Principals;
using Telerik.Sitefinity.Web.UI.Modules.Selectors;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views
{
  /// <summary>Represents a view that edits global permissions</summary>
  /// <typeparam name="THost">Hosting control</typeparam>
  public class GlobalPermissionsView : SimpleView, IScriptControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Security.Permissions.GlobalPermissionsView.ascx");

    /// <summary>Initialize child controls</summary>
    /// <param name="viewContainer">Container of the view</param>
    protected override void InitializeControls(GenericContainer viewContainer)
    {
      RadTab tabByValue1 = this.PermissionsControlSelector.FindTabByValue("bySection");
      RadTab tabByValue2 = this.PermissionsControlSelector.FindTabByValue("byUser");
      RadTab tabByValue3 = this.PermissionsControlSelector.FindTabByValue("byRole");
      if (tabByValue1 != null)
        tabByValue1.Text = Res.Get<SecurityResources>().PermissionsBySection;
      if (tabByValue2 != null)
        tabByValue2.Text = Res.Get<SecurityResources>().PermissionsByUser;
      if (tabByValue3 == null)
        return;
      tabByValue3.Text = Res.Get<SecurityResources>().PermissionsByRole;
    }

    /// <summary>Event handler for the PreRender lifecycle event</summary>
    /// <param name="e">Not used.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<GlobalPermissionsView>(this);
    }

    /// <summary>Event handler for the Render lifecycle event</summary>
    /// <param name="writer">Not used.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Retrieves the embedded path of the template used for this view
    /// </summary>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? GlobalPermissionsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Instance of the ItemActionPermissionsList - which displays permissions by a list of users per action.
    /// </summary>
    protected virtual ItemActionPermissionsList GlobalItemsActionPermissionsList => this.Container.GetControl<ItemActionPermissionsList>("globalItemsActionPermissionsList", true);

    /// <summary>
    /// Instance of the UserPermissionsList - which displays permissions by a list of actions per item.
    /// </summary>
    protected virtual UserPermissionsList GlobalUserPermissionsList => this.Container.GetControl<UserPermissionsList>("globalUserPermissionsList", true);

    /// <summary>
    /// Instance of the UserPermissionsList - which displays permissions by a list of actions per item.
    /// </summary>
    protected virtual UserPermissionsList GlobalUserPermissionsListRoles => this.Container.GetControl<UserPermissionsList>("globalUserPermissionsListRoles", true);

    /// <summary>
    /// A Rad Tab Strip to switch between permission editing controls and methods
    /// </summary>
    protected virtual RadTabStrip PermissionsControlSelector => this.Container.GetControl<RadTabStrip>("rtsPermissionsControlSelector", true);

    protected virtual ModuleSelector ModuleSelector => this.Container.GetControl<ModuleSelector>("moduleSelector", true);

    protected virtual UserSelector UserSelector => this.Container.GetControl<UserSelector>("userSelector", true);

    protected virtual RoleSelector RoleSelector => this.Container.GetControl<RoleSelector>("roleSelector", true);

    IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(typeof (GlobalPermissionsView).ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_permissionsControlSelectorID", (object) this.PermissionsControlSelector.ClientID);
      behaviorDescriptor.AddProperty("_globalItemsActionPermissionsListID", (object) this.GlobalItemsActionPermissionsList.ClientID);
      behaviorDescriptor.AddProperty("_globalUserPermissionsListID", (object) this.GlobalUserPermissionsList.ClientID);
      behaviorDescriptor.AddProperty("_globalUserPermissionsListRolesID", (object) this.GlobalUserPermissionsListRoles.ClientID);
      behaviorDescriptor.AddProperty("_moduleSelectorID", (object) this.ModuleSelector.ClientID);
      behaviorDescriptor.AddProperty("_userSelectorID", (object) this.UserSelector.ClientID);
      behaviorDescriptor.AddProperty("_roleSelectorID", (object) this.RoleSelector.ClientID);
      behaviorDescriptor.AddProperty("_dynamicModulePermissionSet", (object) "General");
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
    {
      string str = typeof (GlobalPermissionsView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Modules.Security.Permissions.Web.UI.Views.Scripts.GlobalPermissionsView.js"
        }
      };
    }
  }
}
