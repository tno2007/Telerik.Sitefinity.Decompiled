// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Data;

namespace Telerik.Sitefinity.Web.UI.Modules.Selectors
{
  public class ModuleSelector : SimpleView, IScriptControl
  {
    /// <summary>
    /// Associating modules with providers, and corresponding GUI elements
    /// </summary>
    protected List<ModuleProviderAssociation> moduleProviderAssociations = new List<ModuleProviderAssociation>();
    /// <summary>Name of the template to use</summary>
    public static readonly string layoutTemplateName = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Content.Selectors.ModuleSelector.ascx");
    private ModuleBuilderManager moduleBuilderManager;

    /// <summary>
    /// Full name of the manager class for managing the permissions
    /// </summary>
    public string ManagerClassName { get; set; }

    /// <summary>
    /// Name of the provider to use for managing the permissions
    /// </summary>
    public string DataProviderName { get; set; }

    /// <summary>
    /// ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).
    /// </summary>
    public Guid SecuredObjectID { get; set; }

    /// <summary>
    /// Gets or sets a client side function to be fired when a module is selected
    /// </summary>
    /// <value>The on module selected.</value>
    public string OnItemSelected { get; set; }

    /// <summary>
    /// Gets or sets a client side function to be fired when the client loaded.
    /// </summary>
    public string OnClientLoaded { get; set; }

    /// <summary>
    /// Gets the reference to the <see cref="P:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleSelector.ModuleBuilderManager" />.
    /// </summary>
    internal ModuleBuilderManager ModuleBuilderManager
    {
      get
      {
        if (this.moduleBuilderManager == null)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>Repeater for changing specific modules</summary>
    protected virtual Repeater ModuleSelectionRepeater => this.Container.GetControl<Repeater>("rptModuleSelection", true);

    IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
    {
      ScriptBehaviorDescriptor behaviorDescriptor = new ScriptBehaviorDescriptor(this.GetType().ToString(), this.ClientID);
      behaviorDescriptor.AddProperty("_moduleSelectionRepeaterID", (object) this.ModuleSelectionRepeater.ClientID);
      behaviorDescriptor.AddProperty("_moduleProviderAssociations", (object) new JavaScriptSerializer().Serialize((object) this.moduleProviderAssociations.ToArray()));
      behaviorDescriptor.AddProperty("managerClassName", (object) this.ManagerClassName);
      behaviorDescriptor.AddProperty("dataProviderName", (object) this.DataProviderName);
      behaviorDescriptor.AddProperty("securedObjectID", (object) this.SecuredObjectID);
      if (!string.IsNullOrEmpty(this.OnItemSelected))
        behaviorDescriptor.AddEvent("itemSelected", this.OnItemSelected);
      if (!string.IsNullOrEmpty(this.OnClientLoaded))
        behaviorDescriptor.AddEvent("clientLoaded", this.OnClientLoaded);
      return (IEnumerable<ScriptDescriptor>) new ScriptBehaviorDescriptor[1]
      {
        behaviorDescriptor
      };
    }

    IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
    {
      string str = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new ScriptReference[1]
      {
        new ScriptReference()
        {
          Assembly = str,
          Name = "Telerik.Sitefinity.Web.UI.Modules.Selectors.Scripts.ModuleSelector.js"
        }
      };
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
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
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ModuleSelector.layoutTemplateName : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      IManager manager1 = (IManager) null;
      if (!string.IsNullOrEmpty(this.ManagerClassName) && !string.IsNullOrEmpty(this.DataProviderName))
        manager1 = ManagerBase.GetManager(this.ManagerClassName, this.DataProviderName);
      IManager manager2 = ManagerBase.GetManager(AppPermission.Root.ManagerType, AppPermission.Root.DataProviderName);
      bool flag1 = AppPermission.Root.Id == this.SecuredObjectID || manager1 != null && manager1.GetType() == AppPermission.Root.ManagerType && AppPermission.Root.DataProviderName == this.DataProviderName;
      if (manager2 != null)
      {
        ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
        {
          LinkClientID = "",
          ModuleTitle = Res.Get<SecurityResources>("GlobalActionPermissionsListTitle"),
          ModuleName = "Backend",
          ModuleManagerTypeName = "",
          SecuredObjectId = "",
          SecuredObjectTypeName = "",
          IsSelectedModule = flag1
        };
        if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Count<ModuleProvider>() > 0)
          this.moduleProviderAssociations.Add(providerAssociation);
      }
      bool flag2 = false;
      if (this.isAccessibleToUser((IList<ISecuredObject>) TaxonomyManager.GetManager().StaticProviders.Select<TaxonomyDataProvider, ISecuredObject>((Func<TaxonomyDataProvider, ISecuredObject>) (prov => prov.SecurityRoot)).ToList<ISecuredObject>()))
      {
        ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
        {
          LinkClientID = "",
          ModuleTitle = Res.Get<TaxonomyResources>().ModuleTitle,
          ModuleName = "",
          ModuleManagerTypeName = typeof (TaxonomyManager).AssemblyQualifiedName,
          SecuredObjectId = "",
          SecuredObjectTypeName = "",
          IsSelectedModule = flag1
        };
        if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Count<ModuleProvider>() > 0)
          this.moduleProviderAssociations.Add(providerAssociation);
      }
      if (LicenseState.Current.LicenseInfo.WorkflowFeaturesLevel > 0 && this.isAccessibleToUser((IList<ISecuredObject>) WorkflowManager.GetManager().StaticProviders.Select<WorkflowDataProvider, ISecuredObject>((Func<WorkflowDataProvider, ISecuredObject>) (prov => prov.SecurityRoot)).ToList<ISecuredObject>()))
      {
        ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
        {
          LinkClientID = "",
          ModuleTitle = Res.Get<WorkflowResources>().Workflow,
          ModuleName = "",
          ModuleManagerTypeName = typeof (WorkflowManager).AssemblyQualifiedName,
          SecuredObjectId = "",
          SecuredObjectTypeName = "",
          IsSelectedModule = flag1
        };
        if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Count<ModuleProvider>() > 0)
          this.moduleProviderAssociations.Add(providerAssociation);
      }
      for (int index = 0; index < SystemManager.ApplicationModules.Values.Count; ++index)
      {
        IModule module = SystemManager.ApplicationModules.ElementAt<KeyValuePair<string, IModule>>(index).Value;
        if (module.Startup != StartupType.Disabled && this.isAccessibleToUser(module) && !(module.Name == "ResponsiveDesign"))
        {
          ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
          {
            LinkClientID = "",
            ModuleTitle = module.Title,
            ModuleName = module.Name,
            ModuleManagerTypeName = "",
            SecuredObjectId = "",
            SecuredObjectTypeName = "",
            IsSelectedModule = flag2
          };
          if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Count<ModuleProvider>() > 0)
            this.moduleProviderAssociations.Add(providerAssociation);
        }
      }
      if (SystemManager.IsModuleEnabled("ModuleBuilder"))
      {
        foreach (IDynamicModule dynamicModule in ModuleBuilderManager.GetModules().Active())
        {
          ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
          {
            LinkClientID = "",
            ModuleTitle = dynamicModule.Title,
            ModuleName = dynamicModule.Name,
            ModuleManagerTypeName = typeof (DynamicModuleManager).FullName,
            SecuredObjectId = dynamicModule.Id.ToString(),
            SecuredObjectTypeName = typeof (DynamicModule).FullName,
            IsSelectedModule = flag2,
            IsDynamicModule = true
          };
          if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Any<ModuleProvider>())
            this.moduleProviderAssociations.Add(providerAssociation);
        }
      }
      bool flag3 = false;
      foreach (string key in (IEnumerable<string>) SystemManager.ServiceModules.Keys)
      {
        IModule serviceModule = SystemManager.ServiceModules[key];
        if (this.isAccessibleToUser(serviceModule))
        {
          ModuleProviderAssociation providerAssociation = new ModuleProviderAssociation()
          {
            LinkClientID = "",
            ModuleTitle = serviceModule.Title,
            ModuleName = serviceModule.Name,
            ModuleManagerTypeName = "",
            SecuredObjectId = "",
            SecuredObjectTypeName = "",
            IsSelectedModule = flag3
          };
          if (((IEnumerable<ModuleProvider>) providerAssociation.ModuleProviders).Count<ModuleProvider>() > 0)
            this.moduleProviderAssociations.Add(providerAssociation);
        }
      }
      this.ModuleSelectionRepeater.ItemDataBound += new RepeaterItemEventHandler(this.ModuleSelectionRepeater_ItemDataBound);
      this.ModuleSelectionRepeater.DataSource = (object) this.moduleProviderAssociations;
      this.ModuleSelectionRepeater.DataBind();
      (ScriptManager.GetCurrent(this.Page) ?? throw new HttpException(Res.Get<ErrorMessages>().ScriptManagerIsNull)).RegisterScriptControl<ModuleSelector>(this);
    }

    /// <summary>
    /// Determines whether a set of Security roots are accessible to the current user.
    /// </summary>
    /// <param name="roots">The roots.</param>
    /// <returns>
    /// 	<c>true</c> if the set of Security roots are accessible to the current user; otherwise, <c>false</c>.
    /// </returns>
    private bool isAccessibleToUser(IList<ISecuredObject> roots)
    {
      bool user = false;
      if (ClaimsManager.IsUnrestricted())
        return true;
      Guid currentUserId = SecurityManager.GetCurrentUserId();
      foreach (ISecuredObject root in (IEnumerable<ISecuredObject>) roots)
      {
        if (root != null && currentUserId != Guid.Empty && root.SupportedPermissionSets.Length != 0)
        {
          string supportedPermissionSet = root.SupportedPermissionSets[0];
          ConfigElementDictionary<string, SecurityAction> actions = Config.Get<SecurityConfig>().Permissions[supportedPermissionSet].Actions;
          string key = actions.Keys.ElementAt<string>(0);
          if (root.IsGranted(supportedPermissionSet, (Guid[]) null, actions[key].Value))
            user = true;
        }
      }
      return user;
    }

    /// <summary>
    /// Determines whether a specific module is accessible to the current user.
    /// </summary>
    /// <param name="module">The module.</param>
    /// <returns>
    /// 	<c>true</c> if the module is accessible to the current user; otherwise, <c>false</c>.
    /// </returns>
    private bool isAccessibleToUser(IModule module) => !(module is ISecuredModule) || this.isAccessibleToUser((IList<ISecuredObject>) ((IEnumerable<ISecuredObject>) ((ISecuredModule) module).GetSecurityRoots()).ToList<ISecuredObject>());

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (!this.DesignMode && this.Page != null)
        ScriptManager.GetCurrent(this.Page)?.RegisterScriptDescriptors((IScriptControl) this);
      base.Render(writer);
    }

    /// <summary>
    /// Event handler for the ItemDataBound event of the repeater showing the list of actions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void ModuleSelectionRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      LinkButton control = e.Item.FindControl("lnkModule") as LinkButton;
      control.Text = ((ModuleProviderAssociation) e.Item.DataItem).ModuleTitle;
      ((ModuleProviderAssociation) e.Item.DataItem).LinkClientID = control.ClientID;
    }
  }
}
