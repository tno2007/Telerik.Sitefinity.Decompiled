// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Project.Configuration.SiteSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Project.Configuration
{
  public class SiteSettings : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public SiteSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the ID of the site.</summary>
    [ConfigurationProperty("id", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "FieldControlIDCaption")]
    public Guid Id
    {
      get => (Guid) this["id"];
      set => this["id"] = (object) value;
    }

    /// <summary>Gets or sets the name.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "default")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "NameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>Gets or sets the id of the root node of the site.</summary>
    /// <value>The site map root id.</value>
    [ConfigurationProperty("siteMapRootNodeId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    public Guid SiteMapRootNodeId
    {
      get => (Guid) this["siteMapRootNodeId"];
      set => this["siteMapRootNodeId"] = (object) value;
    }

    /// <summary>Gets or sets the home page id.</summary>
    /// <value>The home page id.</value>
    [ConfigurationProperty("homePageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "HomePageId")]
    public Guid HomePageId
    {
      get => (Guid) this["homePageId"];
      set => this["homePageId"] = (object) value;
    }

    /// <summary>Gets or sets the front end login page id.</summary>
    /// <value>The front end login page id.</value>
    [ConfigurationProperty("frontEndLoginPageId", DefaultValue = "00000000-0000-0000-0000-000000000000")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "FrontEndLoginPageId")]
    public Guid FrontEndLoginPageId
    {
      get => (Guid) this["frontEndLoginPageId"];
      set => this["frontEndLoginPageId"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the front end login page url.
    /// <para>If <see cref="P:Telerik.Sitefinity.Project.Configuration.SiteSettings.FrontEndLoginPageId" /> specified it will prevail and this parameter will be ignored.</para>
    /// </summary>
    /// <value>The front end login page url.</value>
    [ConfigurationProperty("frontEndLoginPageUrl", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "SiteSettingsObsoleteProperty", Title = "FrontEndLoginPageUrl")]
    public string FrontEndLoginPageUrl
    {
      get => (string) this["frontEndLoginPageUrl"];
      set => this["frontEndLoginPageUrl"] = (object) value;
    }
  }
}
