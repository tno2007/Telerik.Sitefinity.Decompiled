// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.RelatedData.RelatedDataViewExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.ModuleEditor;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.RelatedData
{
  /// <summary>
  /// Extension methods which extend <see cref="T:Telerik.Sitefinity.RelatedData.Web.UI.IRelatedDataView" /> with helper methods for displaying related items.
  /// </summary>
  public static class RelatedDataViewExtensions
  {
    private const string widgetNameRegularExpression = "/!(?<urlPrefix>\\w+)/.*";
    private const string ModuleErrorTemplate = "<p class=\"sfFailure\">{0}</p>";

    /// <summary>
    /// Determining whether the control is configured to display related items.
    /// </summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <param name="force">if set to <c>true</c> the return value is calculated again.</param>
    /// <returns>
    /// Boolean values determines whether related items should be displayed
    /// </returns>
    public static bool DisplayRelatedData(this IRelatedDataView relatedDataView, bool force = false)
    {
      if (!relatedDataView.DisplayRelatedData.HasValue | force)
      {
        if (relatedDataView != null && relatedDataView.RelatedDataDefinition != null && !string.IsNullOrEmpty(relatedDataView.RelatedDataDefinition.RelatedItemType) && (relatedDataView.ConfigureRelatedItem() || relatedDataView.RelatedDataDefinition.RelatedItemSource != RelatedItemSource.Url))
        {
          relatedDataView.DisplayRelatedData = new bool?(true);
          relatedDataView.ValidateCyclicReferences();
        }
        else
          relatedDataView.DisplayRelatedData = new bool?(false);
      }
      return relatedDataView.DisplayRelatedData.Value;
    }

    /// <summary>
    /// Configures the related item depending on <see cref="T:Telerik.Sitefinity.RelatedData.Web.UI.RelatedItemSource" /> specified in <see cref="T:Telerik.Sitefinity.RelatedData.Web.UI.RelatedDataDefinition" />.
    /// </summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// RelatedDataView is not a control
    /// or
    /// This control is not within a data bound item template.
    /// </exception>
    public static bool ConfigureRelatedItem(this IRelatedDataView relatedDataView)
    {
      if (relatedDataView.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.NoAutomaticBinding)
        return true;
      if (!(relatedDataView is Control))
        throw new InvalidOperationException("RelatedDataView is not a control");
      Type type = TypeResolutionService.ResolveType(relatedDataView.RelatedDataDefinition.RelatedItemType, false);
      object obj = (object) null;
      if (type != (Type) null)
        obj = relatedDataView.ResolveRelatedItem(type);
      switch (obj)
      {
        case IDataItem dataItem:
          if (type == (Type) null || type.IsAssignableFrom(dataItem.GetType()) || type.FullName.Equals(dataItem.GetType().FullName))
          {
            Guid guid = dataItem.Id;
            if (dataItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric && lifecycleDataItemGeneric.OriginalContentId != Guid.Empty)
              guid = lifecycleDataItemGeneric.OriginalContentId;
            relatedDataView.RelatedDataDefinition.RelatedItemId = guid;
            string str = string.Empty;
            if (dataItem.Provider is DataProviderBase provider)
              str = provider.Name;
            relatedDataView.RelatedDataDefinition.RelatedItemProviderName = str;
            return true;
          }
          break;
        case PageSiteNode _:
          if (type == (Type) null || typeof (PageNode).IsAssignableFrom(type))
          {
            PageSiteNode pageSiteNode = obj as PageSiteNode;
            relatedDataView.RelatedDataDefinition.RelatedItemId = pageSiteNode.Id;
            relatedDataView.RelatedDataDefinition.RelatedItemProviderName = ManagerBase<PageDataProvider>.GetDefaultProviderName();
            return true;
          }
          break;
      }
      return false;
    }

    /// <summary>Gets the related item.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <returns></returns>
    public static IDataItem GetRelatedItem(this IRelatedDataView relatedDataView)
    {
      IDataItem relatedItem = (IDataItem) null;
      int? totalCount = new int?();
      IQueryable relatedItems = relatedDataView.GetRelatedItems(string.Empty, string.Empty, new int?(0), ref totalCount);
      if (relatedItems != null)
        relatedItem = Queryable.OfType<IDataItem>(relatedItems).FirstOrDefault<IDataItem>();
      return relatedItem;
    }

    /// <summary>Gets the related items.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="itemsPerPage">The number of items displayed per page.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public static IQueryable GetRelatedItems(
      this IRelatedDataView relatedDataView,
      string filterExpression,
      string sortExpression,
      int? itemsPerPage,
      ref int? totalCount)
    {
      filterExpression = ContentHelper.AdaptMultilingualFilterExpression(filterExpression);
      int num = 0;
      if (relatedDataView is Control control)
        num = control.GetItemsToSkipCount(itemsPerPage, UrlEvaluationMode.QueryString, relatedDataView.UrlKeyPrefix);
      IQueryable relatedItems;
      if (relatedDataView.RelatedDataDefinition.RelationTypeToDisplay == RelationDirection.Child)
      {
        RelatedItemsApplicationState applicationState = RelatedItemsApplicationState.GetCurrentContextApplicationState();
        relatedItems = applicationState == null ? RelatedDataHelper.GetRelatedItems(relatedDataView.RelatedDataDefinition.RelatedItemType, relatedDataView.RelatedDataDefinition.RelatedItemProviderName, relatedDataView.RelatedDataDefinition.RelatedItemId, relatedDataView.RelatedDataDefinition.RelatedFieldName, new ContentLifecycleStatus?(ContentLifecycleStatus.Live), filterExpression, sortExpression, new int?(num), itemsPerPage, ref totalCount) : (IQueryable) applicationState.GetRelatedItems(relatedDataView.RelatedDataDefinition.RelatedItemType, relatedDataView.RelatedDataDefinition.RelatedItemProviderName, relatedDataView.RelatedDataDefinition.RelatedFieldName, relatedDataView.RelatedDataDefinition.RelatedItemId, ContentLifecycleStatus.Live, ref totalCount);
      }
      else
        relatedItems = RelatedDataHelper.GetRelatedParentItems(relatedDataView.RelatedDataDefinition.RelatedItemType, relatedDataView.RelatedDataDefinition.RelatedItemProviderName, relatedDataView.RelatedDataDefinition.RelatedItemId, relatedDataView.GetContentType(), relatedDataView.GetProviderName(), relatedDataView.RelatedDataDefinition.RelatedFieldName, ContentLifecycleStatus.Live, filterExpression, sortExpression, new int?(num), itemsPerPage, ref totalCount).AsQueryable();
      if (relatedItems == null)
        relatedItems = (IQueryable) Enumerable.Empty<IDataItem>().AsQueryable<IDataItem>();
      return relatedItems;
    }

    /// <summary>Gets the pager base URL.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <returns></returns>
    public static string GetPagerBaseUrl(this IRelatedDataView relatedDataView)
    {
      string pagerBaseUrl = string.Empty;
      if (relatedDataView is Control control)
        pagerBaseUrl = RouteHelper.ResolveUrl(SiteMapBase.GetActualCurrentNode().Url + control.GetUrlParameterString(false), UrlResolveOptions.Absolute);
      return pagerBaseUrl;
    }

    /// <summary>Resolves the related item.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <param name="relatedItemType">Type of the related item.</param>
    /// <returns></returns>
    /// <exception cref="T:System.Exception">RelatedItemType  is not valid</exception>
    /// <exception cref="T:System.ArgumentException">The specified manager does not support GetItemFromUrl method.</exception>
    internal static object ResolveRelatedItem(
      this IRelatedDataView relatedDataView,
      Type relatedItemType)
    {
      object obj = (object) null;
      if (relatedDataView.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.DataItemContainer)
        obj = relatedDataView.GetRelatedDataItem();
      else if (relatedDataView.RelatedDataDefinition.RelatedItemSource == RelatedItemSource.Url)
        obj = relatedDataView.ResolveRelatedItemFromUrl(relatedItemType);
      return obj;
    }

    internal static object ResolveRelatedItemFromUrl(
      this IRelatedDataView relatedDataView,
      Type relatedItemType)
    {
      object lifecycleItem = (object) null;
      Control control = relatedDataView as Control;
      if (relatedItemType == (Type) null)
        throw new Exception("RelatedItemType is not valid");
      string urlParameterString1 = control.GetUrlParameterString(true);
      if (urlParameterString1 != null)
      {
        if (urlParameterString1.StartsWith("/Action/Edit") || urlParameterString1.StartsWith("/Action/Preview"))
          return (object) false;
        string providerName = relatedDataView.RelatedDataDefinition.RelatedItemProviderName;
        if (string.IsNullOrEmpty(providerName) && typeof (DynamicContent).IsAssignableFrom(relatedItemType))
          providerName = SystemManager.CurrentContext.CurrentSite.GetDefaultProvider(ModuleBuilderManager.GetManager().GetDynamicModuleType(relatedItemType).ModuleName).ProviderName;
        IManager mappedManager = ManagerBase.GetMappedManager(relatedItemType, providerName);
        if (!(mappedManager.Provider is UrlDataProviderBase))
          throw new ArgumentException("The specified manager does not support GetItemFromUrl method.");
        object obj = (object) ((UrlDataProviderBase) mappedManager.Provider).GetItemFromUrl(relatedItemType, urlParameterString1, true, out string _);
        object resultItem;
        if (control is IContentLocatableView && ((IContentLocatableView) control).TryGetItemWithRequestedStatus(lifecycleItem as ILifecycleDataItem, mappedManager as ILifecycleManager, out resultItem))
          obj = resultItem;
        if (obj != null)
        {
          string urlParameterString2 = control.GetUrlParameterString(false);
          if (!string.IsNullOrEmpty(urlParameterString2))
          {
            MatchCollection matchCollection = Regex.Matches(urlParameterString2, "/!(?<urlPrefix>\\w+)/.*");
            if (matchCollection.Count == 1 && matchCollection[0].Groups["urlPrefix"].Value == relatedDataView.UrlKeyPrefix || matchCollection.Count == 0 && string.IsNullOrEmpty(relatedDataView.UrlKeyPrefix))
            {
              lifecycleItem = obj;
              RouteHelper.SetUrlParametersResolved();
            }
          }
        }
      }
      return lifecycleItem;
    }

    /// <summary>
    /// Checks if current site has corresponding content type module enabled with current provider
    /// </summary>
    internal static bool IsModuleEnabledForCurrentSite(this IRelatedDataView relatedDataView)
    {
      Type contentType = TypeResolutionService.ResolveType(relatedDataView.GetContentType(), false);
      return contentType != (Type) null && RelatedDataHelper.IsModuleEnabledForCurrentSite(contentType, relatedDataView.GetProviderName());
    }

    /// <summary>Gets a literal control displaying error message.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    internal static Control GetModuleErrorMessageControl(
      this IRelatedDataView relatedDataView)
    {
      return (Control) new LiteralControl()
      {
        Text = string.Format("<p class=\"sfFailure\">{0}</p>", (object) Res.Get<ModuleEditorResources>().DeletedModuleWarning)
      };
    }

    /// <summary>Validates cyclic references</summary>
    internal static void ValidateCyclicReferences(this IRelatedDataView relatedDataView)
    {
      Control control = relatedDataView as Control;
      for (Control parent = control.Parent; parent != null; parent = parent.Parent)
      {
        if (parent is IRelatedDataView relatedDataView1 && relatedDataView1.RelatedDataDefinition != null && relatedDataView1.RelatedDataDefinition.RelatedItemType == relatedDataView.RelatedDataDefinition.RelatedItemType && relatedDataView1.RelatedDataDefinition.RelatedItemId == relatedDataView.RelatedDataDefinition.RelatedItemId)
        {
          control.Visible = false;
          break;
        }
      }
    }

    /// <summary>Gets the related live content links.</summary>
    /// <param name="relatedDataView">The related data view.</param>
    /// <returns></returns>
    internal static IEnumerable GetRelatedLiveContentLinks(
      this IRelatedDataView relatedDataView)
    {
      return RelatedDataHelper.GetRelatedContentLinks(relatedDataView.RelatedDataDefinition.RelatedItemType, relatedDataView.RelatedDataDefinition.RelatedItemId, relatedDataView.RelatedDataDefinition.RelatedItemProviderName, relatedDataView.RelatedDataDefinition.RelatedFieldName, ContentLifecycleStatus.Live) ?? (IEnumerable) Enumerable.Empty<ContentLink>();
    }
  }
}
