// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Data.OpenAccessXmlConfigStorageProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Exceptions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Configuration.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Configuration.Data
{
  /// <summary>
  /// Represents an addition storage provider for persisting configuration in the database.
  /// It uses OpenAccessConnection.
  /// </summary>
  public class OpenAccessXmlConfigStorageProvider : 
    IXmlConfigStorageProvider,
    IOpenAccessMetadataProvider,
    IOpenAccessSelfUpgradableProvider
  {
    private string appName = "/";
    private string name;
    private string connectionName;
    private const string SchemaVersionKey = "OpenAccessXmlConfigStorageProvider-SchemaVersion";
    private const string ConfigCacheKey = "sf_xml_config_cache";
    private readonly object configCacheSync = new object();

    /// <inheritdoc />
    public IXmlConfigItem GetElement(string path)
    {
      TempXmlConfigItem element = (TempXmlConfigItem) null;
      if (this.TryGetConfigItem(path, out element))
        return (IXmlConfigItem) element;
      using (SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) this, this.connectionName))
      {
        context.ProviderName = this.ModuleName;
        XmlConfigItem xmlConfigItem = context.GetAll<XmlConfigItem>().Include<XmlConfigItem>((Expression<Func<XmlConfigItem, object>>) (i => i.Data)).SingleOrDefault<XmlConfigItem>((Expression<Func<XmlConfigItem, bool>>) (c => c.ApplicationName == this.appName && c.Path == path));
        if (xmlConfigItem == null)
          return (IXmlConfigItem) null;
        return (IXmlConfigItem) new TempXmlConfigItem()
        {
          Path = xmlConfigItem.Path,
          Data = xmlConfigItem.Data,
          LastModified = xmlConfigItem.LastModified
        };
      }
    }

    /// <inheritdoc />
    public void SaveElement(IXmlConfigItem configElement)
    {
      using (SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) this, this.connectionName))
      {
        context.ProviderName = this.ModuleName;
        XmlConfigItem entity = context.GetAll<XmlConfigItem>().SingleOrDefault<XmlConfigItem>((Expression<Func<XmlConfigItem, bool>>) (c => c.ApplicationName == this.appName && c.Path == configElement.Path));
        if (entity == null)
        {
          entity = new XmlConfigItem(this.appName, configElement.Path);
          context.Add((object) entity);
        }
        entity.Data = configElement.Data;
        context.SaveChanges();
        CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
        {
          new CacheDependencyKey()
          {
            Key = configElement.Path,
            Type = typeof (XmlConfigItem)
          }
        });
      }
    }

    /// <inheritdoc />
    public void Initialize(string providerName, NameValueCollection config, Type managerType)
    {
      this.name = typeof (OpenAccessXmlConfigStorageProvider).FullName;
      this.connectionName = config["connectionName"];
      if (string.IsNullOrEmpty(this.connectionName))
        this.connectionName = "Sitefinity";
      config.Remove("connectionName");
      string str = config["applicationName"];
      if (!string.IsNullOrEmpty(str))
        this.appName = str;
      config.Remove("applicationName");
    }

    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context)
    {
      OpenAccessConnection accessConnection = OpenAccessConnection.GetConnections().FirstOrDefault<OpenAccessConnection>();
      return accessConnection == null || accessConnection.Name == this.connectionName ? (MetadataSource) null : (MetadataSource) new XmlConfigStorageMetadataSource(context);
    }

    /// <summary>
    /// Gets the name of the module which uniquely identifies MetadataSource provided to the connection.
    /// If the module name is null or empty, the current type will be used as a key.
    /// </summary>
    /// <value>The name of the module.</value>
    public string ModuleName => this.name;

    internal string ConnectionName => this.connectionName;

    private bool TryGetConfigItem(string path, out TempXmlConfigItem item)
    {
      if (path.EndsWith("Config.config"))
      {
        ICacheManager cacheManager;
        try
        {
          cacheManager = SystemManager.GetCacheManager(CacheManagerInstance.Internal);
        }
        catch (Exception ex)
        {
          item = (TempXmlConfigItem) null;
          return false;
        }
        if (!(cacheManager["sf_xml_config_cache"] is IDictionary<string, TempXmlConfigItem> dictionary1))
        {
          lock (this.configCacheSync)
          {
            if (!(cacheManager["sf_xml_config_cache"] is IDictionary<string, TempXmlConfigItem> dictionary1))
            {
              using (SitefinityOAContext context = OpenAccessConnection.GetContext((IOpenAccessMetadataProvider) this, this.connectionName))
              {
                context.ProviderName = this.ModuleName;
                dictionary1 = (IDictionary<string, TempXmlConfigItem>) context.GetAll<XmlConfigItem>().Include<XmlConfigItem>((Expression<Func<XmlConfigItem, object>>) (i => i.Data)).Where<XmlConfigItem>((Expression<Func<XmlConfigItem, bool>>) (c => c.ApplicationName == this.appName && c.Path.EndsWith("Config.config"))).ToDictionary<XmlConfigItem, string, TempXmlConfigItem>((Func<XmlConfigItem, string>) (x => x.Path), (Func<XmlConfigItem, TempXmlConfigItem>) (x => new TempXmlConfigItem()
                {
                  Path = x.Path,
                  Data = x.Data,
                  LastModified = x.LastModified
                }));
              }
              cacheManager.Add("sf_xml_config_cache", (object) dictionary1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(2.0)));
              cacheManager.Add("sf_xml_config_cache", (object) dictionary1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new DataItemCacheDependency(typeof (XmlConfigItem), (string) null), (ICacheItemExpiration) new SlidingTime(TimeSpan.FromMinutes(2.0)));
            }
          }
        }
        dictionary1.TryGetValue(path, out item);
        return true;
      }
      item = (TempXmlConfigItem) null;
      return false;
    }

    void IOpenAccessSelfUpgradableProvider.Update(
      OpenAccessConnection oaConnection,
      Database database,
      string newConnectionString,
      MetadataContainer newMetadataContainer,
      UpgradeDatabaseScheme defaultUpgradeSchemeMethod)
    {
      using (new MethodPerformanceRegion("OpenAccessXmlConfigStorageProvider data model update"))
      {
        bool flag = false;
        int build = typeof (ConfigVarialble).Assembly.GetName().Version.Build;
        try
        {
          using (UpgradingContext upgradingContext = new UpgradingContext(newConnectionString, oaConnection, newMetadataContainer))
          {
            ConfigVarialble configVarialble = upgradingContext.GetAll<ConfigVarialble>().FirstOrDefault<ConfigVarialble>((Expression<Func<ConfigVarialble, bool>>) (v => v.Key == "OpenAccessXmlConfigStorageProvider-SchemaVersion" && v.ApplicationName == this.appName));
            int result;
            flag = configVarialble == null || int.TryParse(configVarialble.Value, out result) && result < build;
          }
        }
        catch (MetadataException ex)
        {
          flag = true;
        }
        catch (TypeLoadException ex)
        {
          flag = true;
        }
        catch (DatabaseNotFoundException ex)
        {
          flag = true;
        }
        catch (DataStoreException ex)
        {
          flag = true;
        }
        catch (System.InvalidOperationException ex)
        {
          switch (ex.InnerException)
          {
            case DataStoreException _:
            case MetadataException _:
            case TypeLoadException _:
            case DatabaseNotFoundException _:
              flag = true;
              break;
          }
        }
        if (!flag)
          return;
        defaultUpgradeSchemeMethod(database, oaConnection);
        this.UpdateSchemaVersion(database, build);
      }
    }

    private void UpdateSchemaVersion(Database database, int currentAssemblyBuildNumber)
    {
      using (IObjectScope objectScope = database.GetObjectScope())
      {
        ConfigVarialble configVarialble = objectScope.Extent<ConfigVarialble>().FirstOrDefault<ConfigVarialble>((Expression<Func<ConfigVarialble, bool>>) (v => v.Key == "OpenAccessXmlConfigStorageProvider-SchemaVersion" && v.ApplicationName == this.appName));
        if (configVarialble == null)
        {
          objectScope.Transaction.Begin();
          objectScope.Add((object) new ConfigVarialble(this.appName, "OpenAccessXmlConfigStorageProvider-SchemaVersion", currentAssemblyBuildNumber.ToString()));
          objectScope.Transaction.Commit();
        }
        else
        {
          int result;
          if (!int.TryParse(configVarialble.Value, out result) || result >= currentAssemblyBuildNumber)
            return;
          objectScope.Transaction.Begin();
          configVarialble.Value = currentAssemblyBuildNumber.ToString();
          objectScope.Transaction.Commit();
        }
      }
    }
  }
}
