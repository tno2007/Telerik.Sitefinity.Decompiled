// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.Services.IBackendFormService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web.Services
{
  /// <summary>
  /// This interface provides functionality for modifying the backend fields and
  /// sections of the dynamic module type.
  /// </summary>
  [ServiceContract]
  internal interface IBackendFormService
  {
    /// <summary>
    /// Modifies the sections and fields of the backend forms for dynamic modules.
    /// </summary>
    [WebHelp(Comment = "Modifies the sections and fields of the backend forms for dynamic module.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?parentTypeId={parentTypeId}")]
    [OperationContract]
    void ModifyBackendForms(SectionFieldWrapper[] sectionsAndFields, string parentTypeId);

    /// <summary>
    /// Gets the sections and fields of the backend forms for dynamic modules. The data is returned in JSON format.
    /// </summary>
    /// <param name="parentTypeId">Id of the desired module type.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.GridColumnWrapper" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the sections and fields of the backend forms for dynamic modules. The data is returned in JSON format.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?parentTypeId={parentTypeId}")]
    [OperationContract]
    CollectionContext<SectionFieldWrapper> GetBackendForms(
      string parentTypeId);

    /// <summary>
    /// Gets the sections and fields of the backend forms for dynamic modules. The data is returned in XML format.
    /// </summary>
    /// <param name="parentTypeId">Id of the desired module type.</param>
    /// <returns>
    /// Collection context object of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Web.Services.Data.GridColumnWrapper" /> objects.
    /// </returns>
    [WebHelp(Comment = "Gets the sections and fields of the backend forms for dynamic modules. The data is returned in XML format.")]
    [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/?parentTypeId={parentTypeId}")]
    [OperationContract]
    CollectionContext<SectionFieldWrapper> GetBackendFormsInXml(
      string parentTypeId);
  }
}
