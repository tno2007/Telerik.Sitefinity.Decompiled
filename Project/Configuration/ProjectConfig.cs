// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Project.Configuration.ProjectConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Project.Configuration
{
  /// <summary>Defines project configuration settings.</summary>
  [DescriptionResource(typeof (ConfigDescriptions), "ProjectConfig")]
  public sealed class ProjectConfig : ConfigSection
  {
    /// <summary>Defines the name of this project.</summary>
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProjectNameDescription", Title = "ProjectNameTitle")]
    [ConfigurationProperty("projectName", DefaultValue = "/")]
    [Browsable(false)]
    public string ProjectName
    {
      get => (string) this["projectName"];
      internal set => this["projectName"] = (object) value;
    }

    /// <summary>Provides summary of the project.</summary>
    [ConfigurationProperty("projectDescription", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProjectDescription", Title = "ProjectDescriptionTitle")]
    public string ProjectDescription
    {
      get => (string) this["projectDescription"];
      set => this["projectDescription"] = (object) value;
    }

    /// <summary>Defines the version number of this project.</summary>
    /// <value>The project version.</value>
    [ConfigurationProperty("projectVersion", DefaultValue = "1.0.0")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProjectVersionDescription", Title = "ProjectVersionTitle")]
    public string ProjectVersion
    {
      get => (string) this["projectVersion"];
      set => this["projectVersion"] = (object) value;
    }

    /// <summary>Specifies the date this project was created.</summary>
    [ConfigurationProperty("dateCreated", DefaultValue = "")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProjectDateCreatedDescription", Title = "ProjectDateCreatedTitle")]
    public DateTime DateCreated
    {
      get => (DateTime) this["dateCreated"];
      set => this["dateCreated"] = (object) value;
    }

    /// <summary>
    /// Specifies the date when this project was last modified.
    /// </summary>
    [DescriptionResource(typeof (ConfigDescriptions), "LastModified")]
    [ConfigurationProperty("lastModified", DefaultValue = "")]
    public DateTime LastModified
    {
      get => (DateTime) this["lastModified"];
      set => this["lastModified"] = (object) value;
    }

    /// <summary>Gets the default site.</summary>
    /// <value>The default site.</value>
    [ConfigurationProperty("defaultSite")]
    public SiteSettings DefaultSite => (SiteSettings) this["defaultSite"];

    public override void Upgrade(Version oldVersion, Version newVersion)
    {
      base.Upgrade(oldVersion, newVersion);
      if (oldVersion.Build >= 3595 || !(this.DefaultSite.SiteMapRootNodeId == Guid.Empty))
        return;
      this.DefaultSite.SiteMapRootNodeId = SiteInitializer.FrontendRootNodeId;
    }
  }
}
