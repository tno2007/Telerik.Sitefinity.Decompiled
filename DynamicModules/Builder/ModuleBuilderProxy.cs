// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder
{
  /// <summary>
  /// Contains basic functionality for working with dynamic modules and dynamic content items.
  /// </summary>
  internal class ModuleBuilderProxy : IModuleBuilderProxy
  {
    /// <inheritdoc />
    public IQueryable<DynamicContent> GetChildItems(
      DynamicContent parentItem,
      Type childType)
    {
      return parentItem.GetChildItems(childType);
    }

    /// <inheritdoc />
    public DynamicContent GetLiveItem(DynamicContent item)
    {
      DataProviderBase provider = item.Provider as DataProviderBase;
      return DynamicModuleManager.GetManager(provider.Name, provider.TransactionName).Lifecycle.GetLive((ILifecycleDataItem) item) as DynamicContent;
    }
  }
}
