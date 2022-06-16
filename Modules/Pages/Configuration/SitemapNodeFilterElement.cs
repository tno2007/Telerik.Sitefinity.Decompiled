// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.SitemapNodeFilterElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Holds information about the availability of a sitemap node filter and optional parameters
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SitemapNodeFilterElementDescription", Title = "SitemapNodeFilterElementTitle")]
  [RestartAppOnChange]
  public class SitemapNodeFilterElement : ConfigElement
  {
    /// <summary>Creates a new sitemap node filter element</summary>
    /// <param name="parent">Instance of the parent collection (e.g. PagesConfig.SitemapNodeFilters)</param>
    public SitemapNodeFilterElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Name of the sitemap node filter, as registed in <c>ObjectFactory</c>. Must be unique within the parent collection
    /// </summary>
    [ConfigurationProperty("filterName", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SitempNodeFilterNameDescription", Title = "SitempNodeFilterNameTitle")]
    public string FilterName
    {
      get => (string) this["filterName"];
      set => this["filterName"] = (object) value;
    }

    /// <summary>True if the filter is active, false to turn it off</summary>
    [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SitempNodeFilterEnabledDescription", Title = "SitempNodeFilterEnabledTitle")]
    public bool IsEnabled
    {
      get => this["enabled"] as bool? ?? true;
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Collection of any custom parameters a filter needs to operate properly
    /// </summary>
    [ConfigurationProperty("paramters")]
    [ConfigurationCollection(typeof (Toolbox), AddItemName = "param")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SitemapNodeFilterParametersDescription", Title = "SitemapNodeFilterParametersTitle")]
    public ConfigElementDictionary<string, SitemapNodeFilterParameterElement> Parameters => (ConfigElementDictionary<string, SitemapNodeFilterParameterElement>) this["paramters"];

    /// <summary>
    /// Constants that map CLR property names to configuration file attribute/tag names
    /// </summary>
    public static class FieldNames
    {
      /// <summary>
      /// Name of the FilterName property as referred to in the configuration file
      /// </summary>
      public const string FilterName = "filterName";
      /// <summary>
      /// Name of the IsEnabled property as referred to in the configuration file
      /// </summary>
      public const string IsEnabled = "enabled";
      /// <summary>
      /// Name of the Parameters property as referred to in the configuration file
      /// </summary>
      public const string Parameters = "paramters";
    }
  }
}
