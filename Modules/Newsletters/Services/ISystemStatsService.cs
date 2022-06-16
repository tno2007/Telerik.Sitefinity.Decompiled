// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ISystemStatsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Interface that defines the members of the system stats service.
  /// </summary>
  [ServiceContract]
  public interface ISystemStatsService
  {
    /// <summary>
    /// Gets the system stats object with the information on usages of various newsletters' module objects.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="provider">The name of the newsletters module provider.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SystemStats" /> object.</returns>
    [WebHelp(Comment = "Gets the system stats object with the information on usages of various newsletters' module objects.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?provider={provider}")]
    [OperationContract]
    SystemStats GetStats(string provider);

    /// <summary>
    /// Gets the system stats object with the information on usages of various newsletters' module objects.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="provider">The name of the newsletters module provider.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SystemStats" /> object.</returns>
    [WebHelp(Comment = "Gets the system stats object with the information on usages of various newsletters' module objects.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?provider={provider}")]
    [OperationContract]
    SystemStats GetStatsInXml(string provider);
  }
}
