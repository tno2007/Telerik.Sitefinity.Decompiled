// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.PageDefaultCanonicalUrlSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>Global settings for the page default canonical url.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "PageDefaultCanonicalUrlSettingsElementDescription", Title = "PageDefaultCanonicalUrlSettingsElementTitle")]
  public class PageDefaultCanonicalUrlSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public PageDefaultCanonicalUrlSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether the default canonical url for static pages is enabled.
    /// </summary>
    [ConfigurationProperty("enable", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnablePageDefaultCanonicalUrlsDescription", Title = "EnablePageDefaultCanonicalUrlsTitle")]
    public bool EnableDefaultPageCanonicalUrls
    {
      get => (bool) this["enable"];
      set => this["enable"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether caching is enabled.
    /// </summary>
    [ConfigurationProperty("allowedCanonicalUrlQueryStringParameters")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowedCanonicalUrlQueryStringParametersDescription", Title = "AllowedCanonicalUrlQueryStringParametersTitle")]
    [ConfigurationCollection(typeof (CanonicalUrlQueryStringParameterElement))]
    public virtual ConfigElementDictionary<string, CanonicalUrlQueryStringParameterElement> AllowedCanonicalUrlQueryStringParameters
    {
      get => (ConfigElementDictionary<string, CanonicalUrlQueryStringParameterElement>) this["allowedCanonicalUrlQueryStringParameters"];
      set => this["allowedCanonicalUrlQueryStringParameters"] = (object) value;
    }

    /// <inheritdoc />
    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeAllowedCanonicalUrlQueryStringParameters();
    }

    private void InitializeAllowedCanonicalUrlQueryStringParameters() => this.AllowedCanonicalUrlQueryStringParameters.Add(new CanonicalUrlQueryStringParameterElement((ConfigElement) this.AllowedCanonicalUrlQueryStringParameters)
    {
      ParameterName = "page"
    });

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string Enable = "enable";
      public const string AllowedCanonicalUrlQueryStringParameters = "allowedCanonicalUrlQueryStringParameters";
    }
  }
}
