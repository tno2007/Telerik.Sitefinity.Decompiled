// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.SitemapNodeFilterParameterElement
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
  /// Holds information about a custom parameter of a sitemap node filter
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SitemapNodeFilterParameterDescription", Title = "SitemapNodeFilterParameterTitle")]
  public class SitemapNodeFilterParameterElement : ConfigElement
  {
    /// <summary>Creates a new sitemap filter parameter element</summary>
    /// <param name="parent">Instance of the parent collection (e.g. SitemapNodeFilterElement.Parameters)</param>
    public SitemapNodeFilterParameterElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Name of the sitemap node filter parameter. Must be unique within the parent collection
    /// </summary>
    [ConfigurationProperty("paramName", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SitemapNodeFilterParameterNameDescription", Title = "SitemapNodeFilterParameterNameTitle")]
    public string ParameterName
    {
      get => (string) this["paramName"];
      set => this["paramName"] = (object) value;
    }

    /// <summary>Value of the sitemap node filter parameter</summary>
    [ConfigurationProperty("paramValue", IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SitempNodeFilterParameterValueDescription", Title = "SitempNodeFilterParameterValueTitle")]
    public string ParamterValue
    {
      get => (string) this["paramValue"];
      set => this["paramValue"] = (object) value;
    }

    /// <summary>
    /// Constants that map CLR property names to configuration file attribute/tag names
    /// </summary>
    public static class FieldNames
    {
      /// <summary>
      /// Name of the ParameterName property as referred to in the configuration file
      /// </summary>
      public const string ParameterName = "paramName";
      /// <summary>
      /// Name of the ParamterValue property as referred to in the configuration file
      /// </summary>
      public const string ParamterValue = "paramValue";
    }
  }
}
