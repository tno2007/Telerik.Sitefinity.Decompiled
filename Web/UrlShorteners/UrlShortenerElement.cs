// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlShorteners.UrlShortenerElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Web.UrlShorteners
{
  /// <summary>
  /// Serves like a configuration element for <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortener" />
  /// </summary>
  public class UrlShortenerElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortenerElement" /> class.
    /// </summary>
    public UrlShortenerElement()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UrlShorteners.UrlShortenerElement" /> class.
    /// </summary>
    /// <param name="shortenerServiceUrl">The URL for the shortener's service.</param>
    public UrlShortenerElement(ConfigElement parent)
      : base(parent)
    {
    }

    internal UrlShortenerElement(bool check)
      : base(check)
    {
    }

    /// <summary>
    /// Gets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"] ?? new NameValueCollection();
      set => this["parameters"] = (object) value;
    }

    /// <summary>The URL for the shortener's service.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ShortenerServiceUrlDescription", Title = "ShortenerServiceUrlCaption")]
    [ConfigurationProperty("shortenerServiceUrl", DefaultValue = "", IsKey = true)]
    public string ShortenerServiceUrl
    {
      get => (string) this["shortenerServiceUrl"];
      set => this["shortenerServiceUrl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the provider is enabled.
    /// </summary>
    /// <value><c>true</c> if enabled; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnabledUrlShortenerElementDescription", Title = "EnabledCaption")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>The type of the data provider.</summary>
    [TypeConverter(typeof (StringTypeConverter))]
    [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
    public Type ProviderType
    {
      get
      {
        Type providerType = (Type) this["type"];
        if (providerType == (Type) null && !string.IsNullOrEmpty(this.ProviderTypeName))
        {
          providerType = TypeResolutionService.ResolveType(this.ProviderTypeName);
          this["type"] = (object) providerType;
        }
        return providerType;
      }
      set => this["type"] = (object) value;
    }

    internal string ProviderTypeName { get; set; }

    /// <summary>
    /// Constants for the names of the configuration properties
    /// </summary>
    internal static class Names
    {
      internal const string ShortenerServiceUrl = "shortenerServiceUrl";
      internal const string Enabled = "enabled";
    }
  }
}
