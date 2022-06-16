// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.Setters.ContentLinkSetter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services.InlineEditing;

namespace Telerik.Sitefinity.InlineEditing.Setters
{
  internal class ContentLinkSetter : PropertySetterBase
  {
    public override Type GetPropertyType() => typeof (ContentLink[]);

    public override void Set(object item, FieldValueModel field, PropertyDescriptor property)
    {
      ContentLink contentLink1 = (property.GetValue(item) as ContentLink[])[0];
      if (field.ComplexValue == null)
        return;
      Guid id = contentLink1.ChildItemId;
      string str1 = contentLink1.ChildItemProviderName;
      Guid guid = Guid.Parse(((IEnumerable<FieldValueModel>) field.ComplexValue).FirstOrDefault<FieldValueModel>((Func<FieldValueModel, bool>) (x => x.Name == "Id")).Value as string);
      string str2 = ((IEnumerable<FieldValueModel>) field.ComplexValue).First<FieldValueModel>((Func<FieldValueModel, bool>) (x => x.Name == "ProviderName")).Value as string;
      int num = guid != contentLink1.ChildItemId ? 1 : 0;
      if (num != 0)
      {
        id = guid;
        str1 = str2;
      }
      LibrariesManager manager = LibrariesManager.GetManager(str1);
      Image image = manager.GetImage(id);
      image.Title = (Lstring) (((IEnumerable<FieldValueModel>) field.ComplexValue).First<FieldValueModel>((Func<FieldValueModel, bool>) (x => x.Name == "Title")).Value as string);
      image.AlternativeText = (Lstring) (((IEnumerable<FieldValueModel>) field.ComplexValue).First<FieldValueModel>((Func<FieldValueModel, bool>) (x => x.Name == "AlternativeText")).Value as string);
      ILifecycleDataItem lifecycleDataItem1 = (ILifecycleDataItem) image;
      if (image.Status == ContentLifecycleStatus.Live)
      {
        lifecycleDataItem1 = manager.Lifecycle.Edit((ILifecycleDataItem) image);
        manager.RefreshItem((MediaContent) image);
      }
      ILifecycleDataItem lifecycleDataItem2 = manager.Lifecycle.Publish(lifecycleDataItem1);
      if (num != 0 && item is DynamicContent dynamicContent)
      {
        property.SetValue((object) dynamicContent, (object) null);
        dynamicContent.AddImage(property.Name, lifecycleDataItem2.Id, str1);
        ContentLink[] contentLinkArray = dynamicContent.GetValue(property.Name) as ContentLink[];
        if (dynamicContent.Status != ContentLifecycleStatus.Master)
        {
          foreach (ContentLink contentLink2 in contentLinkArray)
            contentLink2.ParentItemId = dynamicContent.OriginalContentId;
        }
      }
      manager.SaveChanges();
    }
  }
}
