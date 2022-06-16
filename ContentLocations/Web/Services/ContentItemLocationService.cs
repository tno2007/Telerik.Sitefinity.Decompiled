// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Web.Services.ContentItemLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.ContentLocations.Web.Services
{
  /// <summary>Web service for working with content locations.</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  internal class ContentItemLocationService : IContentItemLocationService
  {
    /// <summary>
    ///     <para>The web service relative URL of the Content Locations Service.</para>
    ///     <para>'Sitefinity/Services/LocationService'</para>
    /// </summary>
    internal const string WebServiceUrl = "Sitefinity/Services/LocationService";

    /// <inheritdoc />
    public CollectionContext<WcfItemContentLocation> GetItemLocations(
      string itemType,
      string provider,
      string itemId)
    {
      List<WcfItemContentLocation> items = new List<WcfItemContentLocation>();
      if (!string.IsNullOrEmpty(itemType))
      {
        Guid contentId = Guid.Parse(itemId);
        Type contentType = TypeResolutionService.ResolveType(itemType);
        object obj;
        foreach (IContentLocation contentItemLocation in ContentItemLocationService.GetFilteredContentItemLocations(contentType, provider, contentId, out obj, ifAccessible: true))
        {
          if (obj != null)
          {
            string url = contentItemLocation.GetUrl(obj);
            if (!url.IsNullOrEmpty())
            {
              string str = ContentItemLocationService.ResolveTitle(contentItemLocation, obj as IDataItem);
              List<WcfItemContentLocation> itemContentLocationList = items;
              WcfItemContentLocation itemContentLocation = new WcfItemContentLocation();
              itemContentLocation.Url = ContentItemLocationService.ResolveUrl(contentId, contentItemLocation.PageId, contentType.FullName, contentItemLocation.ItemProvider, SystemManager.CurrentContext.Culture.Name);
              itemContentLocation.Title = str ?? url;
              itemContentLocation.ItemLiveUrl = url;
              itemContentLocationList.Add(itemContentLocation);
            }
          }
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfItemContentLocation>((IEnumerable<WcfItemContentLocation>) items)
      {
        TotalCount = items.Count
      };
    }

    /// <inheritdoc />
    [SuppressMessage("Telerik.Sitefinity", "SF1002:AvoidToListOnIEnumerable")]
    public CollectionContext<WcfContentLocation> GetLocations(
      string itemType,
      string provider)
    {
      List<WcfContentLocation> items = new List<WcfContentLocation>();
      int priorityIndex = 1;
      Type contentType = TypeResolutionService.ResolveType(itemType);
      PageManager pageManager = PageManager.GetManager();
      string providerName = provider;
      IEnumerable<IContentLocation> source = ContentItemLocationService.GetLocations(contentType, providerName).Distinct<IContentLocation>((IEqualityComparer<IContentLocation>) new ContentLocationEqualityComparer());
      IMultisiteContext context = SystemManager.CurrentContext.MultisiteContext;
      if (context != null)
        items = source.Select<IContentLocation, WcfContentLocation>((Func<IContentLocation, WcfContentLocation>) (location =>
        {
          return new WcfContentLocation()
          {
            Url = location.GetUrl(),
            Title = location.GetTitle(),
            Priority = priorityIndex++,
            Id = location.Id,
            Site = context.GetSiteById(location.SiteId).Name,
            PageStatus = (string) pageManager.GetPageNode(location.PageId).ApprovalWorkflowState
          };
        })).ToList<WcfContentLocation>();
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfContentLocation>((IEnumerable<WcfContentLocation>) items)
      {
        TotalCount = items.Count
      };
    }

    /// <inheritdoc />
    public bool ChangeContentLocationPriority(string id, MovingDirection direction)
    {
      int num = SystemManager.GetContentLocationServiceInternal().ChangeContentLocationPriority(new Guid(id), direction) ? 1 : 0;
      ServiceUtility.DisableCache();
      return num != 0;
    }

    private static object GetContentItem(
      Type contentType,
      IManager manager,
      Guid contentId,
      out ILifecycleDataItemGeneric masterItem)
    {
      masterItem = (ILifecycleDataItemGeneric) null;
      object contentItem = manager.GetItem(contentType, contentId);
      if (!(contentItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric))
        return contentItem;
      switch (lifecycleDataItemGeneric.Status)
      {
        case ContentLifecycleStatus.Temp:
        case ContentLifecycleStatus.PartialTemp:
          if (!(lifecycleDataItemGeneric.OriginalContentId != Guid.Empty))
            return contentItem;
          masterItem = manager.GetItem(contentType, lifecycleDataItemGeneric.OriginalContentId) as ILifecycleDataItemGeneric;
          return contentItem;
        default:
          return contentItem;
      }
    }

    internal static IEnumerable<IContentLocation> GetFilteredContentItemLocations(
      Type contentType,
      string providerName,
      Guid contentId,
      Guid pageId = default (Guid),
      bool returnFirst = false,
      Guid siteId = default (Guid),
      bool alwaysTryToResolveDefaultCulture = true)
    {
      return ContentItemLocationService.GetFilteredContentItemLocations(contentType, providerName, contentId, out object _, pageId, returnFirst, siteId: siteId, alwaysTryToResolveDefaultCulture: alwaysTryToResolveDefaultCulture);
    }

    internal static IEnumerable<IContentLocation> GetFilteredContentItemLocations(
      Type contentType,
      string providerName,
      Guid contentId,
      out object item,
      Guid pageId = default (Guid),
      bool returnFirst = false,
      bool ifAccessible = false,
      Guid siteId = default (Guid),
      bool alwaysTryToResolveDefaultCulture = true)
    {
      List<IContentLocation> contentItemLocations = new List<IContentLocation>();
      IManager manager;
      IEnumerable<IContentLocation> locations = ContentItemLocationService.GetLocations(contentType, providerName, out manager, out ContentTypeLocationsContainer _);
      using (new ReadUncommitedRegion(manager))
      {
        ILifecycleDataItemGeneric master;
        try
        {
          item = ContentItemLocationService.GetContentItem(contentType, manager, contentId, out master);
        }
        catch (UnauthorizedAccessException ex)
        {
          item = (object) null;
          return (IEnumerable<IContentLocation>) contentItemLocations;
        }
        Func<IEnumerable<IContentLocation>>[] funcArray = new Func<IEnumerable<IContentLocation>>[2]
        {
          (Func<IEnumerable<IContentLocation>>) (() => locations),
          (Func<IEnumerable<IContentLocation>>) (() => container.GetSingleItemLocations(master != null ? master.Id : contentId))
        };
        foreach (Func<IEnumerable<IContentLocation>> func in funcArray)
        {
          foreach (IContentLocation contentLocation in func())
          {
            IContentLocation location = contentLocation;
            if (ContentItemLocationService.ShouldAddContentLocation(ifAccessible, location, siteId, master, pageId, contentId))
            {
              if (SystemManager.CurrentContext.AppSettings.Multilingual && item is ILocalizable)
              {
                ILocalizable localizableItem = item as ILocalizable;
                if (!(!(item is DynamicContent) ? (IEnumerable<CultureInfo>) localizableItem.GetAvailableCultures((CultureInfo) null, alwaysTryToResolveDefaultCulture) : (IEnumerable<CultureInfo>) (item as DynamicContent).AvailableCultures).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.LCID == location.Culture.LCID)))
                  continue;
              }
              contentItemLocations.Add(location);
              if (returnFirst)
                return (IEnumerable<IContentLocation>) contentItemLocations;
            }
          }
        }
      }
      return (IEnumerable<IContentLocation>) contentItemLocations;
    }

    private static bool ShouldAddContentLocation(
      bool ifAccessible,
      IContentLocation location,
      Guid siteId,
      ILifecycleDataItemGeneric master,
      Guid pageId,
      Guid contentId)
    {
      if (ifAccessible && !location.IsAccessible() || siteId != new Guid() && location.SiteId != siteId)
        return false;
      if (location.IsSingleItem && master != null)
      {
        if (!(pageId == new Guid()) && !(pageId == location.PageId) || !location.IsMatch(master.Id))
          return false;
      }
      else if (!(pageId == new Guid()) && !(pageId == location.PageId) || !location.IsMatch(contentId))
        return false;
      return true;
    }

    internal static IEnumerable<IContentLocation> GetLocations(
      Type contentType,
      string providerName)
    {
      return ContentItemLocationService.GetLocations(contentType, providerName, out IManager _, out ContentTypeLocationsContainer _);
    }

    internal static IEnumerable<IContentLocation> GetLocations(
      Type contentType,
      string providerName,
      out IManager manager,
      out ContentTypeLocationsContainer container)
    {
      ContentLocationService locationServiceInternal = SystemManager.GetContentLocationServiceInternal();
      manager = ManagerBase.GetMappedManager(contentType, providerName);
      if (!locationServiceInternal.IsTypeSupported(contentType))
        throw new ArgumentException(string.Format("Content type is not supported : {0}", (object) contentType.FullName));
      providerName = manager.Provider.Name;
      return locationServiceInternal.GetContentLocations(contentType, providerName, out container);
    }

    internal static string ResolveUrl(
      Guid contentId,
      Guid pageId,
      string contentType,
      string contentProviderName,
      string cultureName)
    {
      return VirtualPathUtility.ToAbsolute(Url.AppendUrlParameters("~/" + ContentLocationRoute.path, new Dictionary<string, string>()
      {
        {
          "item_id",
          contentId.ToString()
        },
        {
          "item_type",
          contentType
        },
        {
          "item_provider",
          contentProviderName
        },
        {
          "item_culture",
          cultureName
        },
        {
          "page_id",
          pageId.ToString()
        }
      }));
    }

    internal static string ResolveTitle(IContentLocation location, IDataItem item)
    {
      string str = location.GetTitle();
      if (item is IHasTitle hasTitle)
      {
        string title = hasTitle.GetTitle();
        if (!title.IsNullOrEmpty())
          str = string.Format("{0} > {1}", (object) str, (object) title);
      }
      return str;
    }
  }
}
