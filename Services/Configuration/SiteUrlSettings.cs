// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SiteUrlSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Defines site URL configuration settings.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteUrlSettingsDescription", Title = "SiteUrlSettingsCaption")]
  public class SiteUrlSettings : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.SiteUrlSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public SiteUrlSettings(ConfigElement parent)
      : base(parent)
    {
    }

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.InitializeTextTransformations();
    }

    /// <summary>
    /// Gets or sets a value indicating whether to enable non-default  site URL settings : domain, http and https ports.
    /// </summary>
    /// <value><c>true</c> if replace the site URL is to be replaced; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("replaceSiteUrl", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableNonDefaultSiteUrlSettingsDescription", Title = "EnableNonDefaultSiteUrlSettingsTitle")]
    public bool EnableNonDefaultSiteUrlSettings
    {
      get => (bool) this["replaceSiteUrl"];
      set => this["replaceSiteUrl"] = (object) value;
    }

    /// <summary>
    /// Defines the site host when constructing absolute URLs for links when it has to be replaced.
    /// </summary>
    /// <value>The site URL.</value>
    [ConfigurationProperty("siteUrl", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "HostTitle")]
    public string Host
    {
      get => (string) this["siteUrl"];
      set => this["siteUrl"] = (object) value;
    }

    /// <summary>
    /// Defines the port of site URL when constructing absolute URLs over HTTP protocol and non-default port is used.
    /// </summary>
    /// <value>The site URL.</value>
    [ConfigurationProperty("nonDefaultHttpPort", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "NonDefaultHttpPortTitle")]
    public string NonDefaultHttpPort
    {
      get => (string) this["nonDefaultHttpPort"];
      set => this["nonDefaultHttpPort"] = (object) value;
    }

    /// <summary>
    /// Defines the port of site URL when constructing absolute URLs over HTTPS protocol and non-default port is used.
    /// </summary>
    /// <value>The site URL.</value>
    [ConfigurationProperty("nonDefaultHttpsPort", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "NonDefaultHttpsPortTitle")]
    public string NonDefaultHttpsPort
    {
      get => (string) this["nonDefaultHttpsPort"];
      set => this["nonDefaultHttpsPort"] = (object) value;
    }

    /// <summary>Remove ssl when the page does not require it..</summary>
    /// <value><c>true</c> if the pages will redirect to http when they does not require ssl; otherwise, <c>false</c>.</value>
    [ConfigurationProperty("removeNonRequiredSsl")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "RemoveNonRequiredSslSettingsDescription", Title = "RemoveNonRequiredSslSettingsTitle")]
    public bool RemoveNonRequiredSsl
    {
      get => (bool) this["removeNonRequiredSsl"];
      set => this["removeNonRequiredSsl"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to lower the
    /// the value of the control.
    /// </summary>
    [ConfigurationProperty("generateAbsoluteUrls", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "GenerateAbsoluteUrlsDescription", Title = "GenerateAbsoluteUrlsTitle")]
    public bool GenerateAbsoluteUrls
    {
      get => (bool) this["generateAbsoluteUrls"];
      set => this["generateAbsoluteUrls"] = (object) value;
    }

    /// <summary>
    /// Gets the client URL transformations used for mirroring the url field from the title field for the content in sitefinity on the client.
    /// </summary>
    [ConfigurationProperty("serverUrlTransformations")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientUrlTransformationsDescription", Title = "ClientUrlTransformationsTitle")]
    public TextTransformationSettings ClientUrlTransformations => (TextTransformationSettings) this["serverUrlTransformations"];

    /// <summary>
    /// Gets the client URL transformations used for mirroring the url field from the title field for the content in sitefinity on the server.
    /// </summary>
    [ConfigurationProperty("clientUrlTransformations")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ServerUrlTransformationsDescription", Title = "ServerUrlTransformationsTitle")]
    public TextTransformationSettings ServerUrlTransformations => (TextTransformationSettings) this["clientUrlTransformations"];

    private void InitializeTextTransformations()
    {
      this.ClientUrlTransformations.RegularExpressionFilter = DefinitionsHelper.UrlRegularExpressionFilter;
      this.ClientUrlTransformations.ReplaceWith = "-";
      this.ClientUrlTransformations.Trim = true;
      this.ClientUrlTransformations.ToLower = true;
      this.ServerUrlTransformations.RegularExpressionFilter = "[^\\w\\-\\!\\$\\'\\(\\)\\=\\@\\d_]+";
      this.ServerUrlTransformations.ReplaceWith = "-";
      this.ServerUrlTransformations.Trim = false;
      this.ServerUrlTransformations.ToLower = true;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    private struct PropertyNames
    {
      public const string ServerUrlTransformations = "clientUrlTransformations";
      public const string ClientUrlTransformations = "serverUrlTransformations";
      public const string ReplaceSiteUrlPropertyName = "replaceSiteUrl";
      public const string NonDefaultHttpPort = "nonDefaultHttpPort";
      public const string NonDefaultHttpsPort = "nonDefaultHttpsPort";
      public const string SiteUrlPropertyName = "siteUrl";
      public const string RemoveNonRequiredSsl = "removeNonRequiredSsl";
      public const string GenerateAbsoluteUrls = "generateAbsoluteUrls";
    }
  }
}
