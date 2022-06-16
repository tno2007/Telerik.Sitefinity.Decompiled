// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.MetaData.Web.Services.MetaDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.Metadata.FieldTypes;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.MetaData.Web.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MetaDataService : IMetaDataService
  {
    public CollectionContext<FieldType> GetFields(string typeId)
    {
      List<FieldType> fields = FieldType.GetFields(MetadataManager.GetManager().GetMetaType(new Guid(typeId)));
      return new CollectionContext<FieldType>((IEnumerable<FieldType>) fields)
      {
        TotalCount = fields.Count
      };
    }

    /// <summary>
    /// Deletes the field and returns true if the field has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="fieldId">The field pageId.</param>
    /// <param name="typeId">The type pageId.</param>
    /// <returns></returns>
    public bool DeleteField(string fieldId, string typeId) => this.DeleteFieldInternal(fieldId, typeId);

    /// <summary>
    /// Deletes the field and returns true if the field has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="fieldId">The field pageId.</param>
    /// <param name="typeId">The type pageId.</param>
    /// <returns></returns>
    public bool DeleteFieldInXml(string fieldId, string typeId) => this.DeleteFieldInternal(fieldId, typeId);

    public FieldType GetField(string fieldId, string typeId) => FieldType.GetField(MetadataManager.GetManager().GetMetafield(new Guid(fieldId)));

    private bool DeleteFieldInternal(string fieldId, string typeId)
    {
      MetadataManager manager = MetadataManager.GetManager();
      Guid id = new Guid(typeId);
      Guid fguid = new Guid(fieldId);
      MetaType metaType = manager.GetMetaType(id);
      MetaField metafield = metaType.Fields.SingleOrDefault<MetaField>((Func<MetaField, bool>) (f => f.Id == fguid));
      metaType.Fields.Remove(metafield);
      manager.Delete(metafield);
      manager.SaveChanges();
      return true;
    }
  }
}
