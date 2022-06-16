// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfilesSerializer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.Images;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  public class UserProfilesSerializer : JavaScriptSerializer
  {
    public UserProfilesSerializer()
      : base((JavaScriptTypeResolver) new MetaTypeJavaScriptResolver())
    {
      MetaTypeJavaScriptConverter javaScriptConverter = new MetaTypeJavaScriptConverter(typeof (UserProfile));
      javaScriptConverter.EnhancementDelegate = new SerializationEnhancementDelegate(this.AddContextPropertiesForContentLinks);
      javaScriptConverter.MustSerializeDelegate = new MustSerializePropertyDelegate(this.MustSerialize);
      javaScriptConverter.DeserializeValueEnhancementDelegate = new DeserializeValueEnhancementDelegate(this.RecompileContentLinks);
      this.MetaTypeConverter = javaScriptConverter;
      this.RegisterConverters((IEnumerable<JavaScriptConverter>) new JavaScriptConverter[1]
      {
        (JavaScriptConverter) javaScriptConverter
      });
    }

    public MetaTypeJavaScriptConverter MetaTypeConverter { get; protected set; }

    /// <summary>
    /// For all the content links we add custom field with the image in order to avoid service calls
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="result"></param>
    /// <param name="serializer"></param>
    protected virtual void AddContextPropertiesForContentLinks(
      object obj,
      Dictionary<string, object> result,
      JavaScriptSerializer serializer)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(obj))
      {
        if (property is ContentLinksPropertyDescriptor propertyDescriptor && propertyDescriptor.IsSingleLink)
        {
          string name = propertyDescriptor.Name;
          object obj1;
          if (result.TryGetValue(name, out obj1))
          {
            ContentLink contentLink = obj1 as ContentLink;
            if (obj1 != null && contentLink.ChildItemType == typeof (Image).FullName && contentLink.GetLinkedItem() is Image linkedItem)
              result.Add("__$Context$imageLink${0}".Arrange((object) name), (object) WcfHelper.SerializeToJson((object) new ImageViewModel(linkedItem, (ContentDataProviderBase) null, false), typeof (ImageViewModel)));
          }
        }
      }
    }

    /// <summary>
    /// For newely created imtes we ensure that the parent id and parent provder name of the content link are set property
    /// </summary>
    protected virtual object RecompileContentLinks(
      PropertyDescriptor propertyDescriptor,
      Type type,
      object deserializedValue,
      IDynamicFieldsContainer objectInstance)
    {
      if (!(propertyDescriptor is ContentLinksPropertyDescriptor) || !(deserializedValue is ContentLink contentLink))
        return deserializedValue;
      IDataItem dataItem = objectInstance as IDataItem;
      contentLink.ParentItemId = dataItem.Id;
      contentLink.ParentItemType = dataItem.GetType().FullName;
      if (dataItem.Provider is DataProviderBase provider)
        contentLink.ParentItemProviderName = provider.Name;
      return (object) contentLink;
    }

    protected virtual bool MustSerialize(PropertyDescriptor propertyDescriptor, bool serialize) => propertyDescriptor is TaxonomyPropertyDescriptor || MetaTypeJavaScriptConverter.MustSerializeProperty(propertyDescriptor, checkReadOnly: (!serialize));
  }
}
