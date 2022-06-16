// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IStatusProviderRegistryExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Services
{
  internal static class IStatusProviderRegistryExtensions
  {
    internal const string StatusFilterFormat = "sf_status_filter_{0}_{1}";
    private static readonly Regex statusFilterRegex = new Regex(string.Format("sf_status_filter_{0}_{1}", (object) "(?<provider>.*)", (object) "(?<command>.*)"));

    internal static IEnumerable<StatusInfo> GetItemStatuses(
      this IStatusProviderRegistry registry,
      Guid id,
      Type itemType,
      string itemProvider,
      string rootKey,
      CultureInfo culture = null,
      StatusBehaviour statusBehaviour = StatusBehaviour.All)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      foreach (IStatusProvider provider in (IEnumerable<IStatusProvider>) registry.GetProviders().Where<IStatusProvider>((Func<IStatusProvider, bool>) (p => (p.Behaviour & statusBehaviour) > (StatusBehaviour) 0)).OrderBy<IStatusProvider, int>((Func<IStatusProvider, int>) (p => p.Priority)))
      {
        IItemStatusData statusData = provider.GetItem(itemType, itemProvider, culture, rootKey, id);
        if (statusData != null)
          yield return new StatusInfo(provider, statusData);
      }
    }

    internal static IEnumerable<StatusInfo> GetStatuses(
      this IStatusProviderRegistry registry,
      Guid[] ids,
      Type itemType,
      string itemProvider,
      string rootKey,
      CultureInfo culture = null,
      StatusBehaviour behaviorFilter = StatusBehaviour.All)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      foreach (IStatusProvider provider in (IEnumerable<IStatusProvider>) registry.GetProviders().Where<IStatusProvider>((Func<IStatusProvider, bool>) (p => (p.Behaviour & behaviorFilter) > (StatusBehaviour) 0)).OrderBy<IStatusProvider, int>((Func<IStatusProvider, int>) (p => p.Priority)))
      {
        foreach (IItemStatusData statusData in provider.GetItems(itemType, itemProvider, culture, rootKey, ids))
          yield return new StatusInfo(provider, statusData);
      }
    }

    internal static IEnumerable<WarningInfo> GetWarnings(
      this IStatusProviderRegistry registry,
      Guid id,
      Type itemType,
      string itemProvider,
      string rootKey = null,
      CultureInfo culture = null,
      StatusBehaviour statusBehaviour = StatusBehaviour.All)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      if (rootKey == null && typeof (PageNode).Equals(itemType))
        rootKey = SystemManager.CurrentContext.CurrentSite.SiteMapRootNodeId.ToString();
      foreach (IStatusProvider provider in (IEnumerable<IStatusProvider>) registry.GetProviders().Where<IStatusProvider>((Func<IStatusProvider, bool>) (p => (p.Behaviour & statusBehaviour) > (StatusBehaviour) 0)).OrderBy<IStatusProvider, int>((Func<IStatusProvider, int>) (p => p.Priority)))
      {
        IWarningData warning = provider.GetWarning(itemType, itemProvider, culture, rootKey, id);
        if (warning != null)
          yield return new WarningInfo(provider, warning);
      }
    }

    internal static bool TryGetMatchingFilterItemIds(
      this IStatusProviderRegistry registry,
      string filterString,
      Type itemType,
      string itemProvider,
      out IEnumerable<Guid> result,
      CultureInfo culture = null,
      string rootKey = null)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      Match match = IStatusProviderRegistryExtensions.statusFilterRegex.Match(filterString);
      if (match.Success)
      {
        string providerName = match.Groups["provider"].Value;
        string filterName = match.Groups["command"].Value;
        IStatusProvider provider = SystemManager.StatusProviderRegistry.GetProvider(providerName);
        if (provider != null)
        {
          result = (IEnumerable<Guid>) provider.GetItemsByFilter(itemType, itemProvider, culture, rootKey, filterName).Select<IItemStatusData, Guid>((Func<IItemStatusData, Guid>) (s => s.ItemId)).ToArray<Guid>();
          return true;
        }
      }
      result = (IEnumerable<Guid>) null;
      return false;
    }

    internal static IEnumerable<FilterInfo> GetFilters(
      this IStatusProviderRegistry registry,
      Type itemType = null)
    {
      foreach (IStatusProvider provider1 in registry.GetProviders())
      {
        IStatusProvider provider = provider1;
        if (!(itemType != (Type) null) || provider.IsTypeSupported(itemType))
        {
          IEnumerable<IStatusFilter> filters = provider.GetFilters();
          if (filters != null && filters.Any<IStatusFilter>())
          {
            foreach (IStatusFilter statusFilter in filters)
              yield return new FilterInfo(provider, statusFilter);
          }
          provider = (IStatusProvider) null;
        }
      }
    }
  }
}
