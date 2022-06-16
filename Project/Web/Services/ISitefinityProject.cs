// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Project.Web.Services.ISitefinityProject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Project.Web.Services
{
  /// <summary>
  /// Mandates the members to be implemented by every WCF service which implements Sitefinity project service.
  /// </summary>
  [ServiceContract]
  public interface ISitefinityProject
  {
    /// <summary>Gets the project version.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "The method returns project version.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Version")]
    [OperationContract]
    string GetVersion();

    /// <summary>Gets the Sitefinity version.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "The method returns Sitefinity version.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/SfVersion")]
    [OperationContract]
    string GetSfVersion();

    /// <summary>Gets the name of the Sitefinity project.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "The method returns the name of the Sitefinity project.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/Name")]
    [OperationContract]
    string GetName();

    /// <summary>Gets the Sitefinity project Info.</summary>
    /// <returns></returns>
    [WebHelp(Comment = "The method returns All Sitefinity project info in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/All")]
    [OperationContract]
    ProjectInfo GetAll();
  }
}
