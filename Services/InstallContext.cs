// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.InstallContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Defines the context class to share some common objects between modules while installing them
  /// </summary>
  public class InstallContext : IDisposable
  {
    private string transactionName;
    private ConfigManager configManager;
    private MetadataManager metadataManager;
    private Dictionary<Type, ConfigSection> configs;
    private Dictionary<Type, IDictionary<string, object>> sharedObjects;
    public const string Key = "sf_InstallContext";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.InstallContext" /> class.
    /// </summary>
    public InstallContext()
      : this(string.Empty)
    {
    }

    internal InstallContext(SiteInitializer siteInitializer)
      : this(siteInitializer.TransactionName)
    {
      this.SiteInitializer = siteInitializer;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.InstallContext" /> class.
    /// </summary>
    public InstallContext(string transactionName)
    {
      if (!string.IsNullOrEmpty(transactionName))
        this.transactionName = transactionName;
      this.configManager = ConfigManager.GetManager();
    }

    internal SiteInitializer SiteInitializer { get; set; }

    /// <summary>Gets or sets the upgrade info.</summary>
    /// <value>The upgrade info.</value>
    internal UpgradeContext UpgradeInfo { get; set; }

    /// <summary>Gets the manager from the current install context</summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <returns></returns>
    internal TManager GetManager<TManager>() where TManager : IManager => this.SiteInitializer != null ? this.SiteInitializer.GetManagerInTransaction<TManager>() : this.GetManager<TManager>((string) null);

    /// <summary>
    /// Gets the manager from the current install context for the specified provider.
    /// </summary>
    /// <typeparam name="TManager">The type of the manager.</typeparam>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    internal TManager GetManager<TManager>(string providerName) where TManager : IManager => this.SiteInitializer != null ? this.SiteInitializer.GetManagerInTransaction<TManager>() : (TManager) ManagerBase.GetManagerInTransaction(typeof (TManager), providerName, this.transactionName);

    /// <summary>Gets and cached the config section.</summary>
    /// <typeparam name="TSection">The type of the section.</typeparam>
    /// <returns></returns>
    public TSection GetConfig<TSection>() where TSection : ConfigSection, new() => this.GetConfig<TSection>(false);

    internal bool TryGetConfig(Type sectionType, out ConfigSection section)
    {
      if (this.configs != null)
        return this.configs.TryGetValue(sectionType, out section);
      section = (ConfigSection) null;
      return false;
    }

    internal void SaveConfig<TSection>() where TSection : ConfigSection, new()
    {
      ConfigSection section;
      if (!this.TryGetConfig(typeof (TSection), out section))
        return;
      this.configManager.Provider.SuppressSecurityChecks = true;
      this.configManager.SaveSection(section);
      this.configManager.Provider.SuppressSecurityChecks = false;
    }

    private TSection GetConfig<TSection>(bool readOnly) where TSection : ConfigSection, new()
    {
      if (this.configs == null)
      {
        this.configs = new Dictionary<Type, ConfigSection>();
        SystemManager.HttpContextItems[(object) "sf_InstallContext"] = (object) this;
      }
      ConfigSection config;
      if (this.configs.TryGetValue(typeof (TSection), out config))
        return (TSection) config;
      if (readOnly)
        return default (TSection);
      ConfigSection section = (ConfigSection) this.configManager.GetSection<TSection>();
      section.InstallMode = true;
      this.configs.Add(typeof (TSection), section);
      return (TSection) section;
    }

    /// <summary>Gets and cached the config section.</summary>
    /// <param name="configType">Type of the section.</param>
    /// <returns></returns>
    internal ConfigSection GetConfig(Type configType) => this.GetConfig(configType, false);

    private ConfigSection GetConfig(Type configType, bool readOnly)
    {
      if (this.configs == null)
      {
        this.configs = new Dictionary<Type, ConfigSection>();
        SystemManager.HttpContextItems[(object) "sf_InstallContext"] = (object) this;
      }
      ConfigSection config;
      if (this.configs.TryGetValue(configType, out config))
        return config;
      if (readOnly)
        return (ConfigSection) null;
      ConfigSection section = this.configManager.GetSection(configType.Name);
      section.InstallMode = true;
      this.configs.Add(configType, section);
      return section;
    }

    [Obsolete("Save Meta Data through SiteInitializer")]
    public void SaveMetaData(bool reset)
    {
      if (this.SiteInitializer == null)
        return;
      this.SiteInitializer.SaveChanges(false);
    }

    /// <summary>Saves the changes and clean the cached resources.</summary>
    public void SaveChanges() => this.SaveChanges(false);

    /// <summary>Saves the changes.</summary>
    /// <param name="clean">if set to <c>true</c> [clean].</param>
    public void SaveChanges(bool clean)
    {
      this.configManager.Provider.SuppressSecurityChecks = true;
      if (this.configs != null)
      {
        foreach (ConfigSection section in this.configs.Values)
          this.configManager.SaveSection(section);
      }
      this.configManager.Provider.SuppressSecurityChecks = false;
      if (!clean)
        return;
      this.configs = (Dictionary<Type, ConfigSection>) null;
      this.sharedObjects = (Dictionary<Type, IDictionary<string, object>>) null;
      SystemManager.HttpContextItems.Remove((object) "sf_InstallContext");
    }

    public void UndoChanges()
    {
      this.configs = (Dictionary<Type, ConfigSection>) null;
      this.sharedObjects = (Dictionary<Type, IDictionary<string, object>>) null;
      SystemManager.HttpContextItems?.Remove((object) "sf_InstallContext");
    }

    public void Dispose() => this.DisposeInternal();

    private void DisposeInternal() => this.UndoChanges();

    public bool Success { get; internal set; }

    public T GetSharedObject<T>(string key) where T : class
    {
      IDictionary<string, object> dictionary;
      object obj;
      return this.sharedObjects != null && this.sharedObjects.TryGetValue(typeof (T), out dictionary) && dictionary.TryGetValue(key, out obj) ? (T) obj : default (T);
    }

    public void SetSharedObject(string key, object obj)
    {
      if (this.sharedObjects == null)
        this.sharedObjects = new Dictionary<Type, IDictionary<string, object>>();
      Type type = obj.GetType();
      IDictionary<string, object> dictionary;
      if (!this.sharedObjects.TryGetValue(type, out dictionary))
      {
        dictionary = (IDictionary<string, object>) new Dictionary<string, object>();
        this.sharedObjects.Add(type, dictionary);
      }
      dictionary[key] = obj;
    }
  }
}
