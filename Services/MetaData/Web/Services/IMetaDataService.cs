// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.MetaData.Web.Services.IMetaDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.Metadata.FieldTypes;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.MetaData.Web.Services
{
  [ServiceContract(Namespace = "MetaDataService")]
  [ServiceKnownType(typeof (FieldType))]
  [ServiceKnownType(typeof (CheckboxFieldType))]
  [ServiceKnownType(typeof (DateTimeFieldType))]
  [ServiceKnownType(typeof (HTMLContentType))]
  [ServiceKnownType(typeof (IntegerFieldType))]
  [ServiceKnownType(typeof (TextFieldType))]
  [ServiceKnownType(typeof (CategoryFieldType))]
  public interface IMetaDataService
  {
    [WebHelp(Comment = "Gets all users for given membership provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?typeId={typeId}")]
    [OperationContract]
    CollectionContext<FieldType> GetFields(string typeId);

    /// <summary>
    /// Deletes the field and returns true if the field has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="fieldId">The field pageId.</param>
    /// <param name="typeId">The type pageId.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Deletes content item. The results are returned in JSON format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{fieldId}/?typeId={typeId}")]
    [OperationContract]
    bool DeleteField(string fieldId, string typeId);

    /// <summary>
    /// Deletes the field and returns true if the field has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="fieldId">The field pageId.</param>
    /// <param name="typeId">The type pageId.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Deletes content item. The results are returned in XML format.")]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "/xml/{fieldId}/?typeId={typeId}")]
    [OperationContract]
    bool DeleteFieldInXml(string fieldId, string typeId);

    [WebHelp(Comment = "Gets all users for given membership provider. The results are returned in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{fieldID}/?typeId={typeId}")]
    [OperationContract]
    FieldType GetField(string fieldId, string typeId);
  }
}
