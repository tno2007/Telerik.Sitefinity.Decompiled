// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Project.Web.Services.SitefinityProject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Project.Configuration;

namespace Telerik.Sitefinity.Project.Web.Services
{
  /// <summary>WCF Rest service for the Sitefinity project.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class SitefinityProject : ISitefinityProject
  {
    private ProjectInfo info;

    /// <summary>Gets the Sitefinity project version.</summary>
    /// <returns></returns>
    public string GetVersion() => this.Info.Version;

    /// <summary>Gets the Sitefinity version.</summary>
    /// <returns></returns>
    public string GetSfVersion() => this.Info.SfVersion;

    /// <summary>Gets the name of the Sitefinity project.</summary>
    /// <returns></returns>
    public string GetName() => this.Info.Name;

    /// <summary>Gets all Sitefinity project information.</summary>
    /// <returns></returns>
    public ProjectInfo GetAll() => this.Info;

    protected ProjectInfo Info
    {
      get
      {
        if (this.info == null)
          this.info = new ProjectInfo(Config.Get<ProjectConfig>());
        return this.info;
      }
    }
  }
}
