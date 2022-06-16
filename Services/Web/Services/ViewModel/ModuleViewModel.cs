// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Web.Services.ViewModel.ModuleViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.ControlTemplates;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.Services.Web.Services.ViewModel
{
  /// <summary>
  /// Contains the minimum required information for showing Modules in the grid.
  /// </summary>
  [DataContract]
  internal class ModuleViewModel
  {
    /// <summary>Gets or sets the key of the module or system service.</summary>
    [DataMember]
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the module or system service.
    /// </summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name identifier (with no whitespaces) that can be used in html for id.
    /// </summary>
    [DataMember]
    public string ClientId { get; set; }

    /// <summary>Gets or sets the name of the module provider.</summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the type of the module.</summary>
    [DataMember]
    public ModuleType ModuleType { get; set; }

    /// <summary>Gets or sets the status of the module.</summary>
    /// <value>The status.</value>
    [DataMember]
    public ModuleStatus Status { get; set; }

    /// <summary>
    /// Gets or sets the type of the of the module or system service.
    /// </summary>
    /// <value>The type.</value>
    [DataMember]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the name that will be displayed for the item on the user interface.
    /// </summary>
    [DataMember]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets short description of the module or system service.
    /// </summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>Gets or sets the module id.</summary>
    /// <value>The module id.</value>
    [DataMember]
    public Guid ModuleId { get; set; }

    /// <summary>
    /// Defines the startup type of the service. The default value is OnFirstCall.
    /// </summary>
    [DataMember]
    public StartupType StartupType { get; set; }

    /// <summary>Gets or sets a version this module is installed to.</summary>
    [DataMember]
    public Version Version { get; set; }

    /// <summary>
    /// Gets or sets the installation/activation error message of the module.
    /// </summary>
    [DataMember]
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a whether this module can be used according the applied license.
    /// </summary>
    [DataMember]
    public bool IsModuleLicensed { get; set; }

    /// <summary>
    /// Gets or sets a whether this module is a system one and there are other ones that depend on it.
    /// </summary>
    [DataMember]
    public bool IsSystemModule { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Web.Services.ViewModel.ModuleViewModel" /> class.
    /// </summary>
    public ModuleViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Web.Services.ViewModel.ModuleViewModel" /> class.
    /// </summary>
    /// <param name="module">The module.</param>
    public ModuleViewModel(AppModuleSettings module)
    {
      this.Key = module.Name;
      this.Name = module.Name;
      this.SetClientId(this.Name);
      this.SetTitleAndDescription(module);
      this.ProviderName = string.Empty;
      this.ModuleType = ModuleType.Static;
      this.Status = ModuleViewModel.GetModuleStatus(module);
      this.Type = module.Type;
      this.ModuleId = module.ModuleId;
      this.StartupType = module.StartupType;
      this.Version = module.Version;
      this.ErrorMessage = module.ErrorMessage;
      this.IsModuleLicensed = !(this.ModuleId != Guid.Empty) || LicenseState.CheckIsModuleLicensedInAnyDomain(this.ModuleId);
      this.IsSystemModule = this.CheckIsModuleSystem(module);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Web.Services.ViewModel.ModuleViewModel" /> class.
    /// </summary>
    /// <param name="module">The module.</param>
    public ModuleViewModel(DynamicModule module)
    {
      this.Key = module.Id.ToString();
      this.Name = module.Name;
      this.SetClientId(module.Id.ToString());
      this.ProviderName = ModulesService.GetProviderName(module);
      this.ModuleType = ModuleType.Dynamic;
      this.Status = this.GetModuleStatus(module);
      this.Type = this.GetModuleTypes(module);
      this.Title = module.Title;
      this.Description = module.Description;
      this.ModuleId = module.Id;
      this.StartupType = this.GetStartupType(this.Status);
      this.Version = new Version();
      this.ErrorMessage = string.Empty;
      this.IsModuleLicensed = true;
      this.IsSystemModule = false;
    }

    private bool CheckIsModuleSystem(AppModuleSettings module) => module.Type == typeof (LibrariesModule).FullName || module.Type == typeof (PublishingModule).FullName || module.Type == typeof (ControlTemplatesModule).FullName || module.Type == typeof (MultisiteModule).FullName || module.Type == "Telerik.Sitefinity.Authentication.AuthenticationModule, Telerik.Sitefinity.Authentication" || module.Type == "Telerik.Sitefinity.Frontend.FrontendModule, Telerik.Sitefinity.Frontend";

    private void SetClientId(string name) => this.ClientId = name.Replace(' ', '_');

    private StartupType GetStartupType(ModuleStatus moduleStatus)
    {
      switch (moduleStatus)
      {
        case ModuleStatus.NotInstalled:
        case ModuleStatus.Inactive:
          return StartupType.Disabled;
        case ModuleStatus.Active:
          return StartupType.OnApplicationStart;
        default:
          throw new NotSupportedException(string.Format("The module status {0} has no equivalent startup type", (object) Enum.GetName(typeof (ModuleStatus), (object) moduleStatus)));
      }
    }

    private string GetModuleTypes(DynamicModule module)
    {
      string empty = string.Empty;
      if (module.Types != null && module.Types.Length != 0)
      {
        StringBuilder stringBuilder = new StringBuilder();
        int length = module.Types.Length;
        foreach (DynamicModuleType type in module.Types)
        {
          stringBuilder.AppendFormat("{0}.{1}", (object) type.TypeNamespace, (object) type.TypeName);
          if (--length > 0)
            stringBuilder.AppendFormat(", ");
        }
        if (stringBuilder.Length > 0)
          empty = stringBuilder.ToString();
      }
      return empty;
    }

    private ModuleStatus GetModuleStatus(DynamicModule module)
    {
      switch (module.Status)
      {
        case DynamicModuleStatus.NotInstalled:
          return ModuleStatus.NotInstalled;
        case DynamicModuleStatus.Active:
          return ModuleStatus.Active;
        case DynamicModuleStatus.Inactive:
          return ModuleStatus.Inactive;
        default:
          throw new NotSupportedException(string.Format("The dynamic module status {0} has no equivalent ModuleStatus", (object) Enum.GetName(typeof (DynamicModuleStatus), (object) module.Status)));
      }
    }

    private static ModuleStatus GetModuleStatus(AppModuleSettings module)
    {
      if (!string.IsNullOrWhiteSpace(module.ErrorMessage))
        return ModuleStatus.Failed;
      if (module.Version == (Version) null)
        return ModuleStatus.NotInstalled;
      switch (module.StartupType)
      {
        case StartupType.OnApplicationStart:
        case StartupType.OnFirstCall:
        case StartupType.Manual:
          return ModuleStatus.Active;
        case StartupType.Disabled:
          return ModuleStatus.Inactive;
        default:
          throw new NotSupportedException(string.Format("The start up type {0} is not supported", (object) Enum.GetName(typeof (StartupType), (object) module.StartupType)));
      }
    }

    private void SetTitleAndDescription(AppModuleSettings module)
    {
      string resourceClassId = module.ResourceClassId;
      if (!string.IsNullOrEmpty(resourceClassId))
      {
        string str;
        this.Title = !ModuleViewModel.TryGetResource(resourceClassId, module.Title, out str) ? module.Name : str;
        if (!ModuleViewModel.TryGetResource(resourceClassId, module.Description, out str))
          return;
        module.Description = str;
      }
      else
      {
        this.Title = module.Title;
        this.Description = module.Description;
      }
    }

    private static bool TryGetResource(string resourceClassId, string key, out string value)
    {
      try
      {
        value = Res.Get(resourceClassId, key);
        return !value.StartsWith("#ResourceNotFound#");
      }
      catch (ArgumentException ex)
      {
        value = (string) null;
        return false;
      }
    }
  }
}
