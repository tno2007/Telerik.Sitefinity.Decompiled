// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.SystemInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Project.Configuration;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Provides information about Sitefinity system.</summary>
  public class SystemInfo
  {
    private Version version;
    private ProjectConfig config = Config.Get<ProjectConfig>();

    /// <summary>Gets the current Sitefinity version.</summary>
    /// <value>The version.</value>
    public Version Version
    {
      get
      {
        if (this.version == (Version) null)
          this.version = typeof (SystemInfo).Assembly.GetName().Version;
        return this.version;
      }
    }

    /// <summary>Gets the name of this project.</summary>
    public string ProjectName => this.config.ProjectName;

    /// <summary>Provides summary of the project.</summary>
    /// <value>The project description.</value>
    public string ProjectDescription => this.config.ProjectDescription;

    /// <summary>Defines the version number of this project.</summary>
    /// <value>The project version.</value>
    public string ProjectVersion => this.config.ProjectVersion;

    /// <summary>Gets the date this project was created.</summary>
    public DateTime DateCreated => this.config.DateCreated;

    /// <summary>
    /// Gets the date and time since the system started running.
    /// </summary>
    /// <value>The running since.</value>
    public DateTime RunningSince => SystemManager.StartedOn;
  }
}
