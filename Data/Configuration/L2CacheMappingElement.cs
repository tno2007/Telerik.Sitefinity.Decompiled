// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Configuration.L2CacheMappingElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Data.Configuration
{
  /// <summary>
  /// Represents a configuration element that will allow configuring the L2 cache for a specified persistent type.
  /// </summary>
  public class L2CacheMappingElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Configuration.L2CacheMappingElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public L2CacheMappingElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the CLR type name to be configured.</summary>
    [ConfigurationProperty("typeName", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "L2CacheMappingTypeNameDescription", Title = "L2CacheMappingTypeNameTitle")]
    public string TypeName
    {
      get => (string) this["typeName"];
      set => this["typeName"] = (object) value;
    }

    /// <summary>Gets or sets the cache strategy.</summary>
    [ConfigurationProperty("cacheStrategy", DefaultValue = TypeCacheStrategy.Default)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "L2CacheMappingCacheStrategyDescription", Title = "L2CacheMappingCacheStrategyTitle")]
    public TypeCacheStrategy CacheStrategy
    {
      get => (TypeCacheStrategy) this["cacheStrategy"];
      set => this["cacheStrategy"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct Props
    {
      public const string TypeName = "typeName";
      public const string CacheStrategy = "cacheStrategy";
    }
  }
}
