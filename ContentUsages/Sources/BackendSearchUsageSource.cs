// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentUsages.Sources.BackendSearchUsageSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.ContentUsages.Filters;
using Telerik.Sitefinity.ContentUsages.Model;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.NewImplementation;
using Telerik.Sitefinity.Search;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search.Data;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.ContentUsages.Sources
{
  internal class BackendSearchUsageSource : IContentUsageSource
  {
    private const int MaxExecutedSearchesCount = 10;
    private const int MaxLoadedResultsCount = 10000;

    public IEnumerable<IContentItemUsage> GetContentUsages(
      ContentUsageFilter filter)
    {
      List<IContentItemUsage> result = new List<IContentItemUsage>();
      IContentUsageSearch contentUsageSearch = ObjectFactory.Resolve<IContentUsageSearch>();
      IContentUsageSearchFilter filter1 = ObjectFactory.Resolve<IContentUsageSearchFilter>();
      if (contentUsageSearch != null && filter1 != null)
      {
        Guid? liveId = this.GetLiveId(filter);
        IEnumerable<string> terms = this.GetTerms(filter, liveId);
        IContentUsageSearchFilter usageSearchFilter1 = filter1;
        List<string> stringList1;
        if (filter.ItemTypeFilter != ItemTypeFilter.All && filter.ItemTypeFilter != ItemTypeFilter.Content)
          stringList1 = new List<string>()
          {
            typeof (PageNode).FullName
          };
        else
          stringList1 = (List<string>) null;
        usageSearchFilter1.IncludedContentTypes = (IEnumerable<string>) stringList1;
        IContentUsageSearchFilter usageSearchFilter2 = filter1;
        List<string> stringList2;
        if (filter.ItemTypeFilter != ItemTypeFilter.All && filter.ItemTypeFilter != ItemTypeFilter.Pages)
          stringList2 = new List<string>()
          {
            typeof (PageNode).FullName
          };
        else
          stringList2 = (List<string>) null;
        usageSearchFilter2.ExcludedContentTypes = (IEnumerable<string>) stringList2;
        filter1.Culture = (CultureInfo) null;
        int num1 = 0;
        int num2 = 0;
        while (true)
        {
          IResultSet searchResult = contentUsageSearch.Search(terms, new int?(num2), filter: filter1);
          this.FillResult(ref result, searchResult);
          ++num1;
          if (searchResult != null && result.Count < searchResult.HitCount && num1 < 10 && result.Count < 10000)
            num2 = result.Count;
          else
            break;
        }
      }
      return (IEnumerable<IContentItemUsage>) result;
    }

    private IEnumerable<string> GetTerms(ContentUsageFilter filter, Guid? liveId)
    {
      List<string> terms = new List<string>();
      Guid itemId;
      if (filter != null)
      {
        itemId = filter.ItemId;
        if (itemId.ToString() != null)
        {
          List<string> stringList = terms;
          itemId = filter.ItemId;
          string str = itemId.ToString("N");
          stringList.Add(str);
        }
      }
      if (liveId.HasValue)
      {
        List<string> stringList = terms;
        itemId = liveId.Value;
        string str = itemId.ToString("N");
        stringList.Add(str);
      }
      return (IEnumerable<string>) terms;
    }

    private void FillResult(ref List<IContentItemUsage> result, IResultSet searchResult)
    {
      if (searchResult == null)
        return;
      if (result == null)
        result = new List<IContentItemUsage>();
      foreach (IDocument document in (IEnumerable<IDocument>) searchResult)
      {
        ContentItemUsage contentItemUsage = new ContentItemUsage();
        Guid result1;
        if (Guid.TryParse(document.GetValue("OriginalItemId")?.ToString(), out result1))
        {
          contentItemUsage.ItemId = result1;
          contentItemUsage.ItemProvider = document.GetValue("Provider")?.ToString();
          contentItemUsage.ItemType = document.GetValue("ContentType")?.ToString();
          contentItemUsage.Culture = this.GetCulture(document);
          result.Add((IContentItemUsage) contentItemUsage);
        }
      }
    }

    private CultureInfo GetCulture(IDocument item)
    {
      string str = item.GetValue("Language")?.ToString();
      if (string.IsNullOrWhiteSpace(str))
        return (CultureInfo) null;
      foreach (CultureInfo systemCulture in SystemManager.CurrentContext.SystemCultures)
      {
        if (PublishingUtilities.TransformLanguageFieldValue(systemCulture.Name) == str)
          return systemCulture;
      }
      return (CultureInfo) null;
    }

    private Guid? GetLiveId(ContentUsageFilter filter)
    {
      if (filter == null || filter.ItemId == Guid.Empty || string.IsNullOrWhiteSpace(filter.ItemType))
        return new Guid?();
      Type itemType = TypeResolutionService.ResolveType(filter.ItemType, false);
      if (!(ManagerBase.GetMappedManager(itemType) is ILifecycleManager mappedManager))
        return new Guid?();
      if (!(mappedManager.GetItemOrDefault(itemType, filter.ItemId) is ILifecycleDataItem itemOrDefault))
        return new Guid?();
      return mappedManager.Lifecycle.GetLive(itemOrDefault)?.Id;
    }
  }
}
