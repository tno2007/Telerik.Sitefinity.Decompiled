// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Project.Web.Services.ProjectInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Reflection;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Project.Configuration;

namespace Telerik.Sitefinity.Project.Web.Services
{
  /// <summary>
  /// This class is used to serialize the Sitefinity Project data through WCF service
  /// </summary>
  [DataContract]
  public class ProjectInfo
  {
    /// <summary>Constructor</summary>
    public ProjectInfo(ProjectConfig projectConfig)
    {
      this.SfVersion = Assembly.GetAssembly(this.GetType()).GetName().Version.ToString();
      this.Version = projectConfig.ProjectVersion;
      this.Name = projectConfig.ProjectName;
      this.Description = projectConfig.ProjectDescription;
    }

    /// <summary>Provides the project version.</summary>
    [DataMember]
    public string Version { get; set; }

    /// <summary>Provides the Sitefinity version number.</summary>
    [DataMember]
    public string SfVersion { get; set; }

    /// <summary>Gets or sets the name of the project.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Provides summary of the project.</summary>
    [DataMember]
    public string Description { get; set; }
  }
}
