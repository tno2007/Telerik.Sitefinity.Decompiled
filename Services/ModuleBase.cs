// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ModuleBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Abstractions.VirtualPath.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Web.UI.Backend;
using Telerik.Sitefinity.Web.UI.ContentUI.Config;

namespace Telerik.Sitefinity.Services
{
  /// <summary>The base class of Sitefinity modules.</summary>
  public abstract class ModuleBase : IModule
  {
    private bool initialized;
    private Guid moduleId;
    private string name;
    private string title;
    private string description;
    private string classId;
    private StartupType startupType;

    /// <summary>Gets the module globally unique identifier.</summary>
    /// <value>The module ID.</value>
    public virtual Guid ModuleId
    {
      get
      {
        this.VerifyInitialized();
        return this.moduleId;
      }
    }

    /// <summary>Gets the programmatic name of the service.</summary>
    /// <value>The name.</value>
    public virtual string Name
    {
      get
      {
        this.VerifyInitialized();
        return this.name;
      }
    }

    /// <summary>Gets the title displayed for this service.</summary>
    /// <value>The title.</value>
    public virtual string Title
    {
      get
      {
        this.VerifyInitialized();
        return !string.IsNullOrEmpty(this.ClassId) ? Res.Get(this.ClassId, this.title, SystemManager.CurrentContext.Culture, true, false) : this.title;
      }
    }

    /// <summary>Gets the description of the service.</summary>
    /// <value>The description.</value>
    public virtual string Description
    {
      get
      {
        this.VerifyInitialized();
        return !string.IsNullOrEmpty(this.ClassId) ? Res.Get(this.ClassId, this.description, SystemManager.CurrentContext.Culture, true, false) : this.description;
      }
    }

    /// <summary>Gets the current status of the service.</summary>
    /// <value>The status.</value>
    public virtual ServiceStatus Status { get; protected set; }

    /// <summary>Gets the startup type of the service.</summary>
    /// <value>The startup type.</value>
    public virtual StartupType Startup
    {
      get
      {
        this.VerifyInitialized();
        return this.startupType;
      }
      set
      {
        if (this.startupType == value)
          return;
        this.startupType = value;
        ConfigManager manager = Telerik.Sitefinity.Configuration.Config.GetManager();
        SystemConfig section = manager.GetSection<SystemConfig>();
        section.ApplicationModules[this.name].StartupType = value;
        manager.SaveSection((ConfigSection) section);
      }
    }

    /// <summary>
    /// Specifies the global resource class identifier to use for retrieving Title and Description values.
    /// If ClassId is specified the values of Title and Description properties are assumed to be resource keys.
    /// </summary>
    /// <value></value>
    public virtual string ClassId
    {
      get
      {
        this.VerifyInitialized();
        return this.classId;
      }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is application module.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is application module; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsApplicationModule
    {
      get
      {
        this.VerifyInitialized();
        return true;
      }
    }

    /// <summary>Gets the configuration element.</summary>
    /// <value>The settings.</value>
    protected internal ModuleSettings Settings => (ModuleSettings) Telerik.Sitefinity.Configuration.Config.Get<SystemConfig>().ApplicationModules[this.Name];

    /// <summary>
    /// Gets the landing page id for each module inherit from <see cref="T:Telerik.Sitefinity.Services.SecuredModuleBase" /> class.
    /// </summary>
    /// <value>The landing page id.</value>
    public abstract Guid LandingPageId { get; }

    /// <summary>Gets the module configuration.</summary>
    public ConfigSection ModuleConfig => this.GetModuleConfig();

    protected internal virtual ManagersInitializationMode ManagersInitializationMode => ManagersInitializationMode.OnDemand;

    private void VerifyInitialized()
    {
      if (!this.initialized)
        throw new InvalidOperationException("This instance is not initialized.");
    }

    /// <summary>Initializes the service with specified settings.</summary>
    /// <param name="settings">The settings.</param>
    public virtual void Initialize(ModuleSettings settings)
    {
      this.moduleId = settings != null ? settings.ModuleId : throw new ArgumentNullException(nameof (settings));
      this.name = settings.Name;
      this.title = settings.Title;
      this.description = settings.Description;
      this.classId = settings.ResourceClassId;
      this.startupType = settings.StartupType;
      this.initialized = true;
      LicenseLimitations.ValidateModuleLicensed((IModule) this);
    }

    /// <summary>Installs this module in Sitefinity system.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module is upgrading from. If it is null, the module is installing for the first time.</param>
    public void Install(SiteInitializer initializer, Version upgradeFrom)
    {
      this.InstallContentViews(initializer, upgradeFrom);
      this.InstallPermissions(initializer, upgradeFrom);
      this.InstallVirtualPaths(initializer);
      if (upgradeFrom == (Version) null)
      {
        this.Install(initializer);
      }
      else
      {
        this.Upgrade(initializer, upgradeFrom);
        initializer.Upgrade((object) this, upgradeFrom.Build);
      }
    }

    /// <summary>
    /// Installs this module in Sitefinity system for the first time.
    /// </summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    public abstract void Install(SiteInitializer initializer);

    /// <summary>Upgrades this module from the specified version.</summary>
    /// <param name="initializer">The Site Initializer. A helper class for installing Sitefinity modules.</param>
    /// <param name="upgradeFrom">The version this module us upgrading from.</param>
    public virtual void Upgrade(SiteInitializer initializer, Version upgradeFrom)
    {
    }

    /// <summary>Gets the module config.</summary>
    /// <returns></returns>
    protected abstract ConfigSection GetModuleConfig();

    protected internal virtual Type[] GetConfigTypes() => (Type[]) null;

    /// <summary>Installs module's content views configurations.</summary>
    protected virtual void InstallContentViews(SiteInitializer initializer, Version upgradeFrom)
    {
      Type[] configTypes = this.GetConfigTypes();
      if (configTypes != null)
      {
        foreach (Type type in configTypes)
        {
          if (typeof (IContentViewConfig).IsAssignableFrom(type))
            this.InstallContentViews((IContentViewConfig) Telerik.Sitefinity.Configuration.Config.Get(type), initializer, upgradeFrom);
        }
      }
      else
      {
        if (!(this.GetModuleConfig() is IContentViewConfig moduleConfig))
          return;
        this.InstallContentViews(moduleConfig, initializer, upgradeFrom);
      }
    }

    private void InstallContentViews(
      IContentViewConfig moduleConfig,
      SiteInitializer initializer,
      Version upgradeFrom)
    {
      ContentViewConfig config = initializer.Context.GetConfig<ContentViewConfig>();
      foreach (ContentViewControlElement element in (IEnumerable<ContentViewControlElement>) moduleConfig.ContentViewControls.Values)
      {
        IConfigElementItem configElementItem;
        if (config.ContentViewControls.TryGetItem(element.GetKey(), out configElementItem))
        {
          string str = !(configElementItem is IConfigElementLink configElementLink) ? configElementItem.Element.LinkModuleName : configElementLink.ModuleName;
          if (str.IsNullOrEmpty())
            throw new Exception(string.Format("ContentView control with key '{0}' cannot be added for module '{1}', because a key with the same name is already added", (object) element.GetKey(), (object) this.Name));
          if (!str.Equals(this.Name))
          {
            if (configElementLink != null)
            {
              string path = element.GetPath();
              if (configElementLink.Path.EndsWith(path))
              {
                configElementLink.ModuleName = this.Name;
                continue;
              }
            }
            throw new Exception(string.Format("ContentView control with key '{0}' cannot be added for module '{1}', because it is already used by module '{2}'", (object) element.GetKey(), (object) this.Name, (object) str));
          }
        }
        else
        {
          element.LinkModuleName = initializer.ModuleName;
          config.ContentViewControls.Add(element);
        }
      }
    }

    protected virtual void InstallPermissions(SiteInitializer initializer, Version upgradeFrom)
    {
      Type[] configTypes = this.GetConfigTypes();
      if (configTypes != null)
      {
        foreach (Type type in configTypes)
        {
          if (typeof (IPermissionsConfig).IsAssignableFrom(type))
            this.InstallPermissions((IPermissionsConfig) Telerik.Sitefinity.Configuration.Config.Get(type), initializer, upgradeFrom);
        }
      }
      else
      {
        if (!(this.GetModuleConfig() is IPermissionsConfig moduleConfig))
          return;
        this.InstallPermissions(moduleConfig, initializer, upgradeFrom);
      }
    }

    protected virtual void InstallPermissions(
      IPermissionsConfig moduleConfig,
      SiteInitializer initializer,
      Version upgradeFrom)
    {
      SecurityConfig config = initializer.Context.GetConfig<SecurityConfig>();
      foreach (Permission element in (IEnumerable<Permission>) moduleConfig.Permissions.Values)
      {
        IConfigElementItem configElementItem;
        if (config.Permissions.TryGetItem(element.GetKey(), out configElementItem))
        {
          string str = !(configElementItem is IConfigElementLink configElementLink) ? configElementItem.Element.LinkModuleName : configElementLink.ModuleName;
          if (str.IsNullOrEmpty())
            throw new Exception(string.Format("Permission with key '{0}' cannot be added for module '{1}', because a key with the same name is already added", (object) element.GetKey(), (object) this.Name));
          if (!str.Equals(this.Name))
          {
            if (configElementLink != null)
            {
              string path = element.GetPath();
              if (configElementLink.Path.EndsWith(path))
              {
                configElementLink.ModuleName = this.Name;
                continue;
              }
            }
            throw new Exception(string.Format("Permission with key '{0}' cannot be added for module '{1}', because it is already used by module '{2}'", (object) element.GetKey(), (object) this.Name, (object) str));
          }
        }
        else
        {
          element.LinkModuleName = this.Name;
          config.Permissions.Add(element);
        }
      }
      foreach (CustomPermissionsDisplaySettingsConfig displaySettingsConfig in (IEnumerable<CustomPermissionsDisplaySettingsConfig>) moduleConfig.CustomPermissionsDisplaySettings.Values)
      {
        if (displaySettingsConfig.SecuredObjectCustomPermissionSets.Count != 0)
        {
          bool flag = true;
          CustomPermissionsDisplaySettingsConfig element1;
          if (!config.CustomPermissionsDisplaySettings.TryGetValue(displaySettingsConfig.SetName, out element1))
          {
            element1 = new CustomPermissionsDisplaySettingsConfig((ConfigElement) config.CustomPermissionsDisplaySettings)
            {
              SetName = displaySettingsConfig.SetName
            };
            config.CustomPermissionsDisplaySettings.Add(element1);
            flag = false;
          }
          foreach (SecuredObjectCustomPermissionSet element2 in (IEnumerable<SecuredObjectCustomPermissionSet>) displaySettingsConfig.SecuredObjectCustomPermissionSets.Values)
          {
            IConfigElementItem configElementItem;
            if (flag && element1.SecuredObjectCustomPermissionSets.TryGetItem(element2.GetKey(), out configElementItem))
            {
              string str = !(configElementItem is IConfigElementLink configElementLink) ? configElementItem.Element.LinkModuleName : configElementLink.ModuleName;
              if (str.IsNullOrEmpty())
                throw new Exception(string.Format("CustomPermissionsDisplaySetting with key '{0}' cannot be added for module '{1}', because a key with the same name is already added", (object) displaySettingsConfig.GetKey(), (object) this.Name));
              if (!str.Equals(this.Name))
              {
                if (configElementLink != null)
                {
                  string path = displaySettingsConfig.GetPath();
                  if (configElementLink.Path.EndsWith(path))
                  {
                    configElementLink.ModuleName = this.Name;
                    continue;
                  }
                }
                throw new Exception(string.Format("CustomPermissionsDisplaySetting with key '{0}' cannot be added for module '{1}', because it is already used by module '{2}'", (object) displaySettingsConfig.GetKey(), (object) this.Name, (object) str));
              }
            }
            else
            {
              element2.LinkModuleName = this.Name;
              element1.SecuredObjectCustomPermissionSets.Add(element2);
            }
          }
        }
      }
    }

    /// <summary>
    /// Ensures the managers initialized. By default, forces initialization of the module managers.
    /// Called by the system when the module is installing or upgrading.
    /// </summary>
    protected internal virtual void EnsureManagersInitialized()
    {
      Type[] managers = this.Managers;
      if (managers != null && managers.Length != 0)
      {
        foreach (Type managerType in managers)
        {
          if (managerType.GetConstructor(new Type[0]) != (ConstructorInfo) null)
            ManagerBase.GetManager(managerType);
        }
      }
      Type[] configTypes = this.GetConfigTypes();
      if (configTypes != null)
      {
        foreach (Type sectionType in configTypes)
          Telerik.Sitefinity.Configuration.Config.Get(sectionType);
      }
      else
        this.GetModuleConfig();
    }

    /// <summary>
    /// Gets the virtual paths that should be installed/uninstalled in the VirtualPathSettingsConfig.
    /// If no action is provided EmbeddedResourceResolver and assembly name are used for registration.
    /// </summary>
    /// <returns></returns>
    protected virtual IDictionary<string, Action<VirtualPathElement>> GetVirtualPaths() => (IDictionary<string, Action<VirtualPathElement>>) null;

    internal IDictionary<string, Action<VirtualPathElement>> GetVirtualPathsInternal() => this.GetVirtualPaths();

    private void InstallVirtualPaths(SiteInitializer initializer)
    {
      IDictionary<string, Action<VirtualPathElement>> virtualPaths1 = this.GetVirtualPaths();
      if (virtualPaths1 == null || virtualPaths1.Count <= 0)
        return;
      ConfigElementDictionary<string, VirtualPathElement> virtualPaths2 = initializer.Context.GetConfig<VirtualPathSettingsConfig>().VirtualPaths;
      foreach (KeyValuePair<string, Action<VirtualPathElement>> keyValuePair in (IEnumerable<KeyValuePair<string, Action<VirtualPathElement>>>) virtualPaths1)
      {
        VirtualPathElement element;
        if (!virtualPaths2.TryGetValue(keyValuePair.Key, out element))
        {
          element = new VirtualPathElement((ConfigElement) virtualPaths2)
          {
            VirtualPath = keyValuePair.Key
          };
          virtualPaths2.Add(element);
        }
        if (keyValuePair.Value != null)
        {
          keyValuePair.Value(element);
        }
        else
        {
          element.ResolverName = "EmbeddedResourceResolver";
          element.ResourceLocation = this.GetType().Assembly.GetName().Name;
        }
      }
    }

    private void UninstallVirtualPaths(SiteInitializer initializer)
    {
      IDictionary<string, Action<VirtualPathElement>> virtualPaths1 = this.GetVirtualPaths();
      if (virtualPaths1 == null || virtualPaths1.Count <= 0)
        return;
      ConfigElementDictionary<string, VirtualPathElement> virtualPaths2 = initializer.Context.GetConfig<VirtualPathSettingsConfig>().VirtualPaths;
      foreach (string key in (IEnumerable<string>) virtualPaths1.Keys)
      {
        if (virtualPaths2.Contains(key))
          virtualPaths2.Remove(key);
      }
    }

    /// <summary>Gets the control panel.</summary>
    /// <returns></returns>
    public virtual IControlPanel GetControlPanel() => ObjectFactory.IsTypeRegistered<IControlPanel>(this.Name) ? ObjectFactory.Resolve<IControlPanel>(this.Name) : throw new InvalidOperationException("Control Panel \"{0}\" not registered.".Arrange((object) this.Name));

    /// <summary>
    /// Gets the CLR types of all data managers provided by this module.
    /// </summary>
    /// <value>An array of <see cref="T:System.Type" /> objects.</value>
    public abstract Type[] Managers { get; }

    /// <inheritdoc />
    public virtual void Load()
    {
      if (this.Managers == null)
        return;
      foreach (Type type in ((IEnumerable<Type>) this.Managers).Where<Type>((Func<Type, bool>) (m => m.ImplementsInterface(typeof (IMultisiteEnabledManager)))))
      {
        if (!SystemManager.MultisiteEnabledManagers.Contains(type))
          SystemManager.MultisiteEnabledManagers.Add(type);
      }
    }

    /// <inheritdoc />
    public virtual void Unload()
    {
    }

    /// <inheritdoc />
    public virtual void Uninstall(SiteInitializer initializer)
    {
      initializer.Uninstaller.Auto((IModule) this);
      if (this.GetModuleConfig() is IContentViewConfig moduleConfig1)
      {
        ContentViewConfig config = initializer.Context.GetConfig<ContentViewConfig>();
        foreach (ContentViewControlElement viewControlElement in (IEnumerable<ContentViewControlElement>) moduleConfig1.ContentViewControls.Values)
        {
          IConfigElementItem configElementItem;
          if (config.ContentViewControls.TryGetItem(viewControlElement.GetKey(), out configElementItem) && configElementItem is IConfigElementLink configElementLink && configElementLink.ModuleName == this.Name)
            config.ContentViewControls.Remove(configElementLink.Key);
        }
      }
      if (this.GetModuleConfig() is IPermissionsConfig moduleConfig2)
      {
        SecurityConfig config = initializer.Context.GetConfig<SecurityConfig>();
        foreach (Permission permission in (IEnumerable<Permission>) moduleConfig2.Permissions.Values)
        {
          IConfigElementItem configElementItem;
          if (config.Permissions.TryGetItem(permission.GetKey(), out configElementItem) && configElementItem is IConfigElementLink configElementLink && configElementLink.ModuleName == this.Name)
            config.Permissions.Remove(configElementLink.Key);
        }
        foreach (CustomPermissionsDisplaySettingsConfig displaySettingsConfig1 in (IEnumerable<CustomPermissionsDisplaySettingsConfig>) moduleConfig2.CustomPermissionsDisplaySettings.Values)
        {
          CustomPermissionsDisplaySettingsConfig displaySettingsConfig2;
          if (displaySettingsConfig1.SecuredObjectCustomPermissionSets.Count != 0 && config.CustomPermissionsDisplaySettings.TryGetValue(displaySettingsConfig1.SetName, out displaySettingsConfig2))
          {
            foreach (SecuredObjectCustomPermissionSet customPermissionSet in (IEnumerable<SecuredObjectCustomPermissionSet>) displaySettingsConfig1.SecuredObjectCustomPermissionSets.Values)
            {
              IConfigElementItem configElementItem;
              if (displaySettingsConfig2.SecuredObjectCustomPermissionSets.TryGetItem(customPermissionSet.GetKey(), out configElementItem) && configElementItem is IConfigElementLink configElementLink && configElementLink.ModuleName == this.Name)
                displaySettingsConfig2.SecuredObjectCustomPermissionSets.Remove(configElementLink.Key);
            }
          }
        }
      }
      this.UninstallVirtualPaths(initializer);
    }
  }
}
