// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings.IResponsiveDesignSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.BasicSettings
{
  [ServiceContract]
  public interface IResponsiveDesignSettingsService
  {
    /// <summary>
    /// Gets the navigation transformations and returns it in JSON format.
    /// </summary>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/nav_transformations/")]
    IEnumerable<NavTransformationElementViewModel> GetNavigationTransformationElements();

    /// <summary>Gets the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/nav_transformations/{name}/")]
    NavTransformationElementViewModel GetNavigationTransformationElement(
      string name);

    /// <summary>Saves the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    /// <param name="transformation">The transformation.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/nav_transformations/{name}/?isNew={isNew}")]
    NavTransformationElementViewModel SaveNavigationTransformationElement(
      string name,
      NavTransformationElementViewModel transformation,
      bool isNew);

    /// <summary>Deletes the navigation transformation element.</summary>
    /// <param name="name">The name.</param>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/nav_transformations/{name}/")]
    bool DeleteNavigationTransformationElement(string name);

    /// <summary>Change navigation transformation status.</summary>
    /// <param name="name">The name.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/nav_transformations/changeStatus/{name}/")]
    bool ChangeStatus(string name);
  }
}
