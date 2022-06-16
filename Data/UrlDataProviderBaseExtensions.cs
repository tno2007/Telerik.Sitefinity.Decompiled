// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.UrlDataProviderBaseExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Extension methods for <see cref="T:Telerik.Sitefinity.Modules.GenericContent.UrlDataProviderBase" /> providers.
  /// </summary>
  internal static class UrlDataProviderBaseExtensions
  {
    /// <summary>
    /// Accepts an item of type IDataItem and returns the corresponding item in master (draft) state.
    /// </summary>
    /// <param name="provider">The data provider.</param>
    /// <param name="dataItem">The data item.</param>
    /// <returns>The item in master (draft) state.</returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="dataItem" /> is <c>null</c>.</exception>
    /// <exception cref="T:System.NotImplementedException">When the lifecycle status case cannot be handled.</exception>
    /// <exception cref="T:System.InvalidOperationException">When no master can be found for <paramref name="dataItem" />.</exception>
    public static IDataItem GetContentMasterBase(
      this UrlDataProviderBase provider,
      IDataItem dataItem)
    {
      if (!(dataItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric))
        throw new ArgumentNullException("contentItem");
      IDataItem dataItem1;
      switch (lifecycleDataItemGeneric.Status)
      {
        case ContentLifecycleStatus.Master:
          dataItem1 = (IDataItem) lifecycleDataItemGeneric;
          break;
        case ContentLifecycleStatus.Temp:
        case ContentLifecycleStatus.Live:
        case ContentLifecycleStatus.PartialTemp:
          dataItem1 = provider.GetItem(lifecycleDataItemGeneric.GetType(), lifecycleDataItemGeneric.OriginalContentId) as IDataItem;
          break;
        default:
          throw new NotImplementedException();
      }
      return dataItem1 != null ? dataItem1 : throw new InvalidOperationException(Res.Get<ContentLifecycleMessages>().MasterNotFound);
    }
  }
}
