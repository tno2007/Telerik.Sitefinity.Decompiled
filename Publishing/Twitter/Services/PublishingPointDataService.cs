// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.PublishingPointDataService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Twitter.Services.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Publishing.Twitter.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class PublishingPointDataService : IPublishingPointDataService
  {
    public CollectionContext<SerializableDynamicObject> GetPublishingPointDataItems(
      string providerName,
      string pointId,
      int take)
    {
      Guid publishingPointID = Guid.Parse(pointId);
      PublishingManager manager = PublishingManager.GetManager(providerName);
      PublishingPoint publishingPoint = manager.GetPublishingPoint(publishingPointID);
      List<SerializableDynamicObject> items = new List<SerializableDynamicObject>();
      if (publishingPoint.IsActive)
      {
        IQueryable<DynamicTypeBase> queryable = Queryable.OfType<DynamicTypeBase>(manager.GetDynamicTypeManager((IPublishingPoint) publishingPoint).GetDataItems((IPublishingPoint) publishingPoint)).OrderBy<DynamicTypeBase>("PublicationDate").Reverse<DynamicTypeBase>();
        int num = 0;
        foreach (DynamicTypeBase component in (IEnumerable<DynamicTypeBase>) queryable)
        {
          if (take > 0)
          {
            if (num == take)
              break;
          }
          ++num;
          PropertyDescriptorCollection properties = TypeDescriptor.GetProperties((object) component);
          SerializableDynamicObject serializableDynamicObject = new SerializableDynamicObject();
          for (int index = 0; index < properties.Count; ++index)
          {
            PropertyDescriptor propertyDescriptor = properties[index];
            object obj = propertyDescriptor.GetValue((object) component);
            serializableDynamicObject.setValue(propertyDescriptor.Name, obj);
          }
          items.Add(serializableDynamicObject);
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<SerializableDynamicObject>((IEnumerable<SerializableDynamicObject>) items);
    }
  }
}
