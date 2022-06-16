// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.ProvidersComponent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>
  /// A component class for pipes that contains logic related to data item providers.
  /// </summary>
  internal class ProvidersComponent
  {
    /// <summary>
    /// Gets the providers for the given publishing point and data item type.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="point">The point.</param>
    /// <returns></returns>
    public virtual IEnumerable<string> GetProviders(
      IManager manager,
      Type itemType,
      PublishingPoint point)
    {
      if (point.IsSharedWithAllSites)
        return manager.GetProviderNames(ProviderBindingOptions.SkipSystem);
      IList<Guid> byPointFromCache = PublishingManager.GetSitesByPointFromCache(point);
      return manager.GetProviderNames(ProviderBindingOptions.SkipSystem, byPointFromCache.ToArray<Guid>());
    }

    /// <summary>
    /// Checks if the given publishing point supports the provider of the given data item.
    /// </summary>
    /// <param name="point">The point.</param>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public virtual bool CheckProvider(PublishingPoint point, IDataItem item)
    {
      if (point.IsSharedWithAllSites)
        return true;
      IList<Guid> siteIds = PublishingManager.GetSitesByPointFromCache(point);
      if (siteIds.Count == 0)
        return false;
      string itemProvider = (item.GetProvider() ?? throw new ArgumentException("Cannot process a data item without a provider.")).ToString();
      Type type = item.GetType();
      if (!(SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext))
        return true;
      Type mappedManagerType = ManagerBase.GetMappedManagerType(type);
      return multisiteContext.GetDataSourcesByManager(mappedManagerType.FullName).Any<ISiteDataSource>((Func<ISiteDataSource, bool>) (ds => ds.Provider == itemProvider && ds.Sites.Any<Guid>((Func<Guid, bool>) (s => siteIds.Contains(s)))));
    }
  }
}
