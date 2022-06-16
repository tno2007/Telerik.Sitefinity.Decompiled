// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocatableViewExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>Extensions for IContentLocatableView.</summary>
  public static class ContentLocatableViewExtensions
  {
    /// <summary>Gets the item default location.</summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="item">The item.</param>
    /// <returns>The item default location.</returns>
    public static string GetItemDefaultCanonicalUrl(
      this IContentLocatableView locatableView,
      IDataItem item)
    {
      return SystemManager.GetContentLocationService().GetItemDefaultLocation(item)?.ItemAbsoluteUrl;
    }

    /// <summary>
    /// Adds the canonical tag in the page headers HTML tag, like <link rel="canonical" href="http://www.test.com/item1" />.
    /// </summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="page">The page.</param>
    /// <param name="item">The item.</param>
    public static void AddCanonicalUrlTagIfEnabled(
      this IContentLocatableView locatableView,
      Page page,
      IDataItem item)
    {
      if (!ContentLocatableViewExtensions.ShouldAddCanonicalUrlMetaTag(locatableView, page))
        return;
      string defaultCanonicalUrl = locatableView.GetItemDefaultCanonicalUrl(item);
      locatableView.AddCanonicalUrlTagIfEnabled(page, defaultCanonicalUrl);
    }

    /// <summary>
    /// Adds the canonical tag in the page headers HTML tag, like <link rel="canonical" href="http://www.test.com/item1" />.
    /// </summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="page">The page.</param>
    /// <param name="canonicalUrl">The canonical URL.</param>
    public static void AddCanonicalUrlTagIfEnabled(
      this IContentLocatableView locatableView,
      Page page,
      string canonicalUrl)
    {
      if (!ContentLocatableViewExtensions.ShouldAddCanonicalUrlMetaTag(locatableView, page) || canonicalUrl.IsNullOrEmpty())
        return;
      page.TryStoreCanonicalUrl(canonicalUrl);
    }

    /// <summary>
    /// Determines whether the content locatable view is a nested one (has a parent that is also IContentLocatableView).
    /// </summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <returns>True if there is a parent of the given locatable view.</returns>
    internal static bool IsNestedContentLocatableView(this IContentLocatableView locatableView)
    {
      bool flag = false;
      if (locatableView is Control control)
      {
        for (Control parent = control.Parent; parent != null; parent = parent.Parent)
        {
          if (parent is IContentLocatableView)
          {
            flag = true;
            break;
          }
        }
      }
      return flag;
    }

    /// <summary>Adds the detail view filter.</summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="location">The location.</param>
    /// <param name="detailView">The detail view.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="contentManager">The content manager.</param>
    internal static void AddDetailViewFilter(
      this IContentLocatableView locatableView,
      ContentLocationInfo location,
      IContentViewDetailDefinition detailView,
      Type contentType,
      IManager contentManager)
    {
      if (detailView == null)
        return;
      Guid dataItemId = detailView.DataItemId;
      if (!(dataItemId != Guid.Empty))
        return;
      List<string> itemIds = new List<string>();
      itemIds.Add(dataItemId.ToString());
      if (typeof (ILifecycleDataItemGeneric).IsAssignableFrom(contentType) && contentManager != null)
      {
        ILifecycleDataItemGeneric cnt = (ILifecycleDataItemGeneric) contentManager.GetItem(contentType, dataItemId);
        if (cnt.OriginalContentId != Guid.Empty)
          itemIds.Add(cnt.OriginalContentId.ToString());
        else if (cnt.Status == ContentLifecycleStatus.Master && contentManager is ILifecycleManager lifecycleManager)
        {
          ILifecycleDataItem live = lifecycleManager.Lifecycle.GetLive((ILifecycleDataItem) cnt);
          if (live != null)
            itemIds.Add(live.Id.ToString());
        }
      }
      ContentLocationSingleItemFilter singleItemFilter = new ContentLocationSingleItemFilter((IEnumerable<string>) itemIds);
      location.Filters.Add((IContentLocationFilter) singleItemFilter);
    }

    /// <summary>Adds the master view filters.</summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="location">The location.</param>
    /// <param name="masterView">The master view.</param>
    /// <param name="contentType">Type of the content.</param>
    /// <param name="contentManager">The content manager.</param>
    /// <param name="settings">The settings.</param>
    internal static void AddMasterViewFilters(
      this IContentLocatableView locatableView,
      ContentLocationInfo location,
      IContentViewMasterDefinition masterView,
      Type contentType,
      IManager contentManager,
      ContentLocationFilterExpressionSettings settings = null)
    {
      if (masterView == null)
        return;
      if (settings == null)
        settings = new ContentLocationFilterExpressionSettings();
      location.RedirectPageId = masterView.DetailsPageId;
      string str = DefinitionsHelper.GetFilterExpression(string.Empty, masterView.AdditionalFilter);
      if (!settings.SkipParentFilter)
      {
        ICollection<Guid> itemsParentsIds = masterView.ItemsParentsIds;
        if (itemsParentsIds != null && itemsParentsIds.Count > 0)
        {
          StringBuilder stringBuilder = new StringBuilder();
          foreach (Guid guid in (IEnumerable<Guid>) itemsParentsIds)
          {
            if (stringBuilder.Length > 0)
              stringBuilder.Append(" or ");
            stringBuilder.AppendFormat("{0} = {1}", (object) settings.ParentMember, (object) guid);
          }
          str = (string.IsNullOrEmpty(str) ? string.Empty : str + " and ") + string.Format("({0})", (object) stringBuilder.ToString());
        }
        else if (masterView.ItemsParentId != Guid.Empty)
          str = (string.IsNullOrEmpty(str) ? string.Empty : str + " and ") + string.Format("({0} = {1})", (object) settings.ParentMember, (object) masterView.ItemsParentId);
      }
      if (string.IsNullOrEmpty(str))
        return;
      location.Filters.Add((IContentLocationFilter) new BasicContentLocationFilter(str));
    }

    internal static string GetItemProvider(this IContentLocatableView locatableView, IDataItem item)
    {
      if (item.Provider is IDataProviderBase provider)
        return provider.Name;
      if (locatableView is ContentView contentView)
      {
        IManager manager = contentView.ResolveManager();
        if (manager != null)
          return manager.Provider.Name;
      }
      return string.Empty;
    }

    /// <summary>
    /// Tries to load the correct lifecycle data item if it is requested for preview or in inline editing mode.
    /// </summary>
    /// <param name="locatableView">The locatable view.</param>
    /// <param name="lifecycleItem">The lifecycle item.</param>
    /// <param name="lifecycleManager">The lifecycle manager.</param>
    /// <param name="resultItem">The result item.</param>
    /// <remarks>
    /// Checks there is a request for preview or inline editing mode of item with specific lifecycle status - gets the appropriate lifecycle data item.
    /// If the item that should be loaded is the live one and it is not visible - returns null.
    /// If the item does not support lifecycle - returns the item.
    /// </remarks>
    /// <returns>True if the item is different.</returns>
    internal static bool TryGetItemWithRequestedStatus(
      this IContentLocatableView locatableView,
      ILifecycleDataItem lifecycleItem,
      ILifecycleManager lifecycleManager,
      out object resultItem)
    {
      return ContentLocatableViewExtensions.TryGetItemWithRequestedStatus(lifecycleItem, lifecycleManager, out resultItem);
    }

    /// <summary>Tries to get item with requested status.</summary>
    /// <param name="lifecycleItem">The lifecycle item.</param>
    /// <param name="lifecycleManager">The lifecycle manager.</param>
    /// <param name="resultItem">The result item.</param>
    /// <returns>True if the item is different.</returns>
    public static bool TryGetItemWithRequestedStatus(
      ILifecycleDataItem lifecycleItem,
      ILifecycleManager lifecycleManager,
      out object resultItem)
    {
      bool withRequestedStatus = false;
      resultItem = (object) lifecycleItem;
      if (lifecycleItem != null && (ContentLocatableViewExtensions.IsPreviewRequested() || SystemManager.IsInlineEditingMode) && lifecycleManager != null)
      {
        ContentLocatableViewExtensions.CheckSecurity();
        ContentLifecycleStatus contentLifecycleStatus = ContentLocatableViewExtensions.GetRequestedItemStatus();
        if (contentLifecycleStatus == ContentLifecycleStatus.Temp && lifecycleItem.Status != ContentLifecycleStatus.Temp && lifecycleItem.Status != ContentLifecycleStatus.PartialTemp)
        {
          bool concurrentEditing = SystemManager.CurrentContext.AllowConcurrentEditing;
          SystemManager.CurrentContext.AllowConcurrentEditing = true;
          resultItem = (object) lifecycleManager.Lifecycle.GetTemp(lifecycleItem);
          SystemManager.CurrentContext.AllowConcurrentEditing = concurrentEditing;
          if (resultItem == null)
            contentLifecycleStatus = ContentLifecycleStatus.Master;
          else
            withRequestedStatus = true;
        }
        if (contentLifecycleStatus == ContentLifecycleStatus.Master && lifecycleItem.Status != ContentLifecycleStatus.Master)
        {
          resultItem = (object) lifecycleManager.Lifecycle.GetMaster(lifecycleItem);
          withRequestedStatus = true;
        }
        else if (contentLifecycleStatus == ContentLifecycleStatus.Live && lifecycleItem.Status != ContentLifecycleStatus.Live)
        {
          resultItem = (object) lifecycleManager.Lifecycle.GetLive(lifecycleItem);
          withRequestedStatus = true;
        }
      }
      return withRequestedStatus;
    }

    internal static ContentLifecycleStatus GetRequestedItemStatus(
      this IContentLocatableView locatableView)
    {
      return ContentLocatableViewExtensions.GetRequestedItemStatus();
    }

    /// <summary>
    /// Gets the lifecycle status of the requested <see cref="T:Telerik.Sitefinity.ContentLocations.IContentLocatableView" /> item
    /// </summary>
    /// <returns>The lifecycle status of the item.</returns>
    public static ContentLifecycleStatus GetRequestedItemStatus()
    {
      object obj = (object) SystemManager.CurrentHttpContext.Request.Params["sf-lc-status"] ?? SystemManager.CurrentHttpContext.Items[(object) "sf-lc-status"];
      ContentLifecycleStatus result = ContentLifecycleStatus.Live;
      if (obj != null && !Enum.TryParse<ContentLifecycleStatus>(obj as string, out result))
        result = ContentLifecycleStatus.Live;
      return result;
    }

    internal static bool IsPreviewRequested(this IContentLocatableView locatableView) => ContentLocatableViewExtensions.IsPreviewRequested();

    private static bool ShouldAddCanonicalUrlMetaTag(IContentLocatableView locatableView, Page page)
    {
      if (page == null || !Config.Get<SystemConfig>().ContentLocationsSettings.EnableSingleItemModeWidgetsBackwardCompatibilityMode && !SystemManager.IsDetailsView() || Config.Get<SystemConfig>().ContentLocationsSettings.DisableCanonicalUrls)
        return false;
      if (locatableView.DisableCanonicalUrlMetaTag.HasValue)
        return !locatableView.DisableCanonicalUrlMetaTag.Value;
      PageSiteNode actualCurrentNode = SiteMapBase.GetActualCurrentNode();
      return actualCurrentNode != null && actualCurrentNode.EnableDefaultCanonicalUrl.HasValue ? actualCurrentNode.EnableDefaultCanonicalUrl.Value : Config.Get<SystemConfig>().ContentLocationsSettings.PageDefaultCanonicalUrlSettings.EnableDefaultPageCanonicalUrls;
    }

    private static bool IsPreviewRequested()
    {
      string str = SystemManager.CurrentHttpContext.Request.Params["sf-content-action"];
      return str != null && str == "preview";
    }

    /// <summary>
    /// Checks if the current user is authenticated and a backend one and sends the appropriate response.
    /// </summary>
    private static void CheckForBackendUser()
    {
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if (currentIdentity == null || !currentIdentity.IsAuthenticated)
        ClaimsManager.CurrentAuthenticationModule.SendUnauthorizedResponse(SystemManager.CurrentHttpContext);
      if (!currentIdentity.IsBackendUser)
        throw new HttpException(403, Res.Get<PageResources>().YouAreNotAuthorizedToAccessThisPage);
    }

    private static void TryAuthenticate()
    {
      string encryptedValidationKey = SystemManager.CurrentHttpContext.Request.QueryStringGet("sf-auth");
      if (encryptedValidationKey.IsNullOrEmpty())
        return;
      SecurityManager.AuthenticateUser(encryptedValidationKey);
    }

    private static void CheckSecurity()
    {
      ContentLocatableViewExtensions.TryAuthenticate();
      ContentLocatableViewExtensions.CheckForBackendUser();
    }
  }
}
