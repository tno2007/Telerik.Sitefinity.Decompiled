// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.DatabaseMappingOptionsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  public class DatabaseMappingOptionsElement : ConfigElement
  {
    public DatabaseMappingOptionsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets the use value indicating wether to use split tables for multilingual fields.
    /// </summary>
    /// <value>The use split tables for multilingual.</value>
    [ConfigurationProperty("useMultilingualSplitTables", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseMultilingualSplitTablesDescription", Title = "UseMultilingualSplitTablesTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For backward compatability.")]
    [Browsable(false)]
    public bool UseMultilingualSplitTables
    {
      get => (bool) this["useMultilingualSplitTables"];
      set => this["useMultilingualSplitTables"] = (object) value;
    }

    /// <summary>Gets or sets the use multilingual fetch strategy.</summary>
    /// <value>The use multilingual fetch strategy.</value>
    [ConfigurationProperty("useMultilingualFetchStrategy", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "UseMultilingualFetchStrategyDescription", Title = "UseMultilingualFetchStrategyTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("For backward compatability. The recommended value is true.")]
    public bool UseMultilingualFetchStrategy
    {
      get => (bool) this["useMultilingualFetchStrategy"];
      set => this["useMultilingualFetchStrategy"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating for which cultures the main required fileds should be in split tables.
    /// </summary>
    /// <value>Comma separated list of cultures.</value>
    [ConfigurationProperty("mainFieldsIgnoredCultures", DefaultValue = "")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    internal string MainFieldsIgnoredCultures
    {
      get => (string) this["mainFieldsIgnoredCultures"];
      set => this["mainFieldsIgnoredCultures"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the cultures do not split when using split tables for multilingual.
    /// </summary>
    /// <value>The cultures do not split.</value>
    /// 
    ///             TODOML property was empty before - try to remove and check tests for new project
    [ConfigurationProperty("splitTablesIgnoredCultures", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SplitTablesIgnoredCulturesDescription", Title = "SplitTablesIgnoredCulturesTitle")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("For backward compatability.")]
    public string SplitTablesIgnoredCultures
    {
      get => (string) this["splitTablesIgnoredCultures"];
      set => this["splitTablesIgnoredCultures"] = (object) value;
    }

    /// <summary>Gets the specific azure database mapping options.</summary>
    [ConfigurationProperty("azureOptions")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AzureOptionsDescription", Title = "AzureOptionsTitle")]
    public AzureOptionsElement AzureOptions => (AzureOptionsElement) this["azureOptions"];

    /// <summary>
    /// Gets the L2 Cache configurations for persistent types.
    /// </summary>
    [ConfigurationProperty("L2CacheTypeMappings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "L2CacheTypeMappingsDescription", Title = "L2CacheTypeMappingsTitle")]
    public ConfigElementDictionary<string, L2CacheMappingElement> L2CacheTypeMappings => (ConfigElementDictionary<string, L2CacheMappingElement>) this[nameof (L2CacheTypeMappings)];

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Props
    {
      public const string L2CacheTypeMappings = "L2CacheTypeMappings";
      public const string AzureOptions = "azureOptions";
    }

    internal class PropNames
    {
      internal const string SplitTablesIgnoredCultures = "splitTablesIgnoredCultures";
      internal const string UseMultilingualSplitTables = "useMultilingualSplitTables";
    }
  }
}
