// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.DatabaseMappingOptions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.OA
{
  internal class DatabaseMappingOptions : IDatabaseMappingOptions
  {
    private ISet<string> splitTablesIgnoredCultures = (ISet<string>) new HashSet<string>();
    private ISet<string> mainFiledsIgnoredCultures = (ISet<string>) new HashSet<string>();
    private IDictionary<string, TypeCacheStrategy> l2CacheTypeMappings = (IDictionary<string, TypeCacheStrategy>) new Dictionary<string, TypeCacheStrategy>();
    public const string AllCulturesKey = "ALLCULTURES";

    public bool UseMultilingualSplitTables { get; set; }

    public bool UseMultilingualFetchStrategy { get; set; }

    public ISet<string> SplitTablesIgnoredCultures => this.splitTablesIgnoredCultures;

    public IDictionary<string, TypeCacheStrategy> L2CacheTypeMappings => this.l2CacheTypeMappings;

    public ISet<string> MainFieldsIgnoredCultures => this.mainFiledsIgnoredCultures;

    public void LoadDefaults()
    {
      DatabaseMappingOptionsElement databaseMappingOptions = Config.Get<DataConfig>().DatabaseMappingOptions;
      this.UseMultilingualSplitTables = databaseMappingOptions.UseMultilingualSplitTables;
      this.UseMultilingualFetchStrategy = databaseMappingOptions.UseMultilingualFetchStrategy;
      if (this.UseMultilingualSplitTables)
      {
        if (!databaseMappingOptions.SplitTablesIgnoredCultures.IsNullOrEmpty())
          this.splitTablesIgnoredCultures.UnionWith((IEnumerable<string>) databaseMappingOptions.SplitTablesIgnoredCultures.Replace(" ", "").Split(new char[1]
          {
            ','
          }, StringSplitOptions.RemoveEmptyEntries));
        if (!databaseMappingOptions.MainFieldsIgnoredCultures.IsNullOrEmpty())
        {
          if (databaseMappingOptions.MainFieldsIgnoredCultures == "ALLCULTURES")
            this.mainFiledsIgnoredCultures.UnionWith(((IEnumerable<CultureInfo>) Config.Get<ResourcesConfig>().FrontendAndBackendCultures).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)));
          else
            this.mainFiledsIgnoredCultures.UnionWith((IEnumerable<string>) databaseMappingOptions.MainFieldsIgnoredCultures.Replace(" ", "").Split(new char[1]
            {
              ','
            }, StringSplitOptions.RemoveEmptyEntries));
        }
      }
      foreach (L2CacheMappingElement cacheMappingElement in (IEnumerable<L2CacheMappingElement>) databaseMappingOptions.L2CacheTypeMappings.Values)
        this.L2CacheTypeMappings.Add(cacheMappingElement.TypeName, cacheMappingElement.CacheStrategy);
      if (!SystemManager.IsInLoadBalancingMode)
        return;
      if (!this.L2CacheTypeMappings.ContainsKey(typeof (DraftData).FullName))
        this.L2CacheTypeMappings.Add(typeof (DraftData).FullName, TypeCacheStrategy.No);
      if (this.L2CacheTypeMappings.ContainsKey(typeof (User).FullName))
        return;
      this.L2CacheTypeMappings.Add(typeof (User).FullName, TypeCacheStrategy.No);
    }
  }
}
