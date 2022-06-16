// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.CommonItemLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  internal class CommonItemLoader : ICommonItemLoader
  {
    private ParentAccessor parentAccessor = new ParentAccessor();

    /// <summary>Loads data item</summary>
    /// <param name="itemType">The type</param>
    /// <param name="id">The id</param>
    /// <param name="provider">The provider</param>
    /// <returns>The item, or null if it can't be found</returns>
    public virtual object LoadDataItem(Type itemType, Guid id, string provider) => ManagerBase.GetMappedManager(itemType, provider).GetItemOrDefault(itemType, id);

    /// <inheritdoc />
    public virtual IList<object> LoadLifecycleDataItem(object item)
    {
      List<object> objectList = new List<object>();
      if (item is ILifecycleDataItem)
      {
        ILifecycleDataItem cnt = item as ILifecycleDataItem;
        IManager mappedManager = ManagerBase.GetMappedManager(item.GetType(), cnt.Provider.ToString());
        if (mappedManager is ILifecycleManager)
        {
          ILifecycleDataItem live = (mappedManager as ILifecycleManager).Lifecycle.GetLive(cnt);
          if (live != null)
            objectList.Add((object) live);
        }
      }
      return (IList<object>) objectList;
    }

    /// <inheritdoc />
    public virtual void SetCommonProperties(
      WrapperObject item,
      string typeName,
      string providerName,
      string action,
      string language)
    {
      item.Language = language;
      item.AddProperty("ItemAction", (object) action);
      item.AddProperty("Provider", (object) providerName);
      item.AddProperty("objectTypeId", (object) typeName);
      item.AddProperty("LangId", (object) language);
    }

    /// <inheritdoc />
    public virtual void SetParentProperties(WrapperObject item, object dataItem)
    {
      IDataItem parent = this.parentAccessor.GetParent(dataItem);
      if (parent == null)
        return;
      item.AddProperty("ParentId", (object) parent.Id);
      item.AddProperty("ParentType", (object) parent.GetType().FullName);
    }
  }
}
