// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.SeoAndOpenGraphElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>SEO and OpenGraph settings for Sitefinity</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SeoAndOpenGraphPropetiesDescription", Title = "SeoAndOpenGraphPropetiesCaption")]
  public class SeoAndOpenGraphElement : ConfigElement
  {
    private const string EnabledSEOPropName = "enabledSEO";
    private const string EnabledOpenGraphPropName = "enabledOpenGraph";
    private const string AppendNoFollowLinksForUntrustedContentPropName = "appendNoFollowLinksForUntrustedContent";
    private const string TrustedDomainsPropName = "trustedDomainsPropName";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.SeoAndOpenGraphElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SeoAndOpenGraphElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Configuration.SeoAndOpenGraphElement" /> class.
    /// </summary>
    /// 
    ///                     /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SeoAndOpenGraphElement()
      : base(false)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether to turn the SEO metadata properties on or off.
    /// </summary>
    /// <value>The SEO metadata properties are enabled or disabled.</value>
    [ConfigurationProperty("enabledSEO", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableSeoMetadataPropertiesDescription", Title = "EnableSeoMetadataPropertiesCaption")]
    public virtual bool EnabledSEO
    {
      get => (bool) this["enabledSEO"];
      set => this["enabledSEO"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to turn the OpenGraph metadata properties on or off.
    /// </summary>
    /// <value>The OpenGraph metadata properties are enabled or disabled.</value>
    [ConfigurationProperty("enabledOpenGraph", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableOpenGraphMetadataPropertiesDescription", Title = "EnableOpenGraphMetadataPropertiesCaption")]
    public virtual bool EnabledOpenGraph
    {
      get => (bool) this["enabledOpenGraph"];
      set => this["enabledOpenGraph"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to append nofollow links for untrusted content.
    /// </summary>
    /// <value>The nofollow links are either appended or not.</value>
    [ConfigurationProperty("appendNoFollowLinksForUntrustedContent", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AppendNoFollowLinksForUntrustedContentDescription", Title = "AppendNoFollowLinksForUntrustedContentCaption")]
    public virtual bool AppendNoFollowLinksForUntrustedContent
    {
      get => (bool) this["appendNoFollowLinksForUntrustedContent"];
      set => this["appendNoFollowLinksForUntrustedContent"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to append nofollow links for untrusted content.
    /// </summary>
    /// <value>The nofollow links are either appended or not.</value>
    [ConfigurationProperty("trustedDomainsPropName", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrustedDomainsDescription", Title = "TrustedDomainsCaption")]
    public virtual string TrustedDomains
    {
      get => (string) this["trustedDomainsPropName"];
      set => this["trustedDomainsPropName"] = (object) value;
    }
  }
}
