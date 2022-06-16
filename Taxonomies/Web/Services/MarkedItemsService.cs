// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Web.Services.MarkedItemsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.OpenAccess.FetchOptimization;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Taxonomies.Web.Services
{
  /// <summary>
  /// Implementation of WCF service marked items management.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MarkedItemsService : IMarkedItemsService
  {
    /// <summary>
    /// Gets the items that are marked with a specified taxon and returns the collection context of <see cref="!:ContentSummary" />
    /// objects in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that marks the items that are to be returned.</param>
    /// <param name="itemType">Type of the item that ought to be returned.</param>
    /// <param name="provider">Name of the provider that ought to be used to return the collection of content summary objects.</param>
    /// <param name="sortExpression">Sort expression used to order the marked items.</param>
    /// <param name="skip">Number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that will be taken in the result set.</param>
    /// <returns>
    /// Collection of ContentSummary objects that are marked with the specified taxon.
    /// </returns>
    public CollectionContext<WcfMarketContentItem> GetItems(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetItemsInternal(taxonId, itemType, provider, itemProvider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the items that are marked with a specified taxon and returns the collection context of <see cref="!:ContentSummary" />
    /// objects in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that marks the items that are to be returned.</param>
    /// <param name="itemType">Type of the item that ought to be returned.</param>
    /// <param name="provider">Name of the provider that ought to be used to return the collection of content summary objects.</param>
    /// <param name="sortExpression">Sort expression used to order the marked items.</param>
    /// <param name="skip">Number of items to skip before starting to take the items in the result set.</param>
    /// <param name="take">Number of items to take in the result set.</param>
    /// <param name="filter">Filter expression used to filter the items that will be taken in the result set.</param>
    /// <returns>
    /// Collection of ContentSummary objects that are marked with the specified taxon.
    /// </returns>
    public CollectionContext<WcfMarketContentItem> GetItemsInXml(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return this.GetItemsInternal(taxonId, itemType, provider, itemProvider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the statistical information for a specified taxon and returns the collection of taxon statistic objects in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the statistical information ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider from which the statistical information ought to be retrieved.</param>
    /// <returns>
    /// CollectionContext object with the collection of wcf taxon statistic objects.
    /// </returns>
    public CollectionContext<WcfTaxonStatistic> GetTaxonStatistics(
      string taxonId,
      string provider)
    {
      return this.GetTaxonStatisticsInternal(taxonId, provider);
    }

    /// <summary>
    /// Gets the statistical information for a specified taxon and returns the collection of taxon statistic objects in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon for which the statistical information ought to be retrieved.</param>
    /// <param name="provider">Name of the taxonomy provider from which the statistical information ought to be retrieved.</param>
    /// <returns>
    /// CollectionContext object with the collection of wcf taxon statistic objects.
    /// </returns>
    public CollectionContext<WcfTaxonStatistic> GetTaxonStatisticsInXml(
      string taxonId,
      string provider)
    {
      return this.GetTaxonStatisticsInternal(taxonId, provider);
    }

    /// <summary>
    /// Removes (unmarks) the item from the taxon and returns true if the item has been removed; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that should not mark the item anymore.</param>
    /// <param name="itemType">Type of the item that should be unmarked.</param>
    /// <param name="itemIDs">Id of the item that should be unmarked.</param>
    /// <param name="provider">Name of the taxonomy provider that should be used when unmarking the item.</param>
    /// <returns></returns>
    public bool RemoveItemFromTaxon(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string itemIDs)
    {
      return this.RemoveItemFromTaxonInternal(taxonId, itemType, itemIDs, provider, itemProvider);
    }

    /// <summary>
    /// Removes (unmarks) the item from the taxon and returns true if the item has been removed; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="taxonId">Id of the taxon that should not mark the item anymore.</param>
    /// <param name="itemType">Type of the item that should be unmarked.</param>
    /// <param name="itemIDs">Id of the item that should be unmarked.</param>
    /// <param name="provider">Name of the taxonomy provider that should be used when unmarking the item.</param>
    /// <returns></returns>
    public bool RemoveItemFromTaxonInXml(
      string taxonId,
      string itemType,
      string provider,
      string itemProvider,
      string itemIDs)
    {
      return this.RemoveItemFromTaxonInternal(taxonId, itemType, itemIDs, provider, itemProvider);
    }

    private CollectionContext<WcfMarketContentItem> GetItemsInternal(
      string taxonId,
      string itemTypeName,
      string provider,
      string itemProvider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      Guid id = new Guid(taxonId);
      Type type = TypeResolutionService.ResolveType(itemTypeName);
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      List<WcfMarketContentItem> items = new List<WcfMarketContentItem>();
      int? totalCount = new int?(0);
      IManager mappedManager = ManagerBase.GetMappedManager(type, itemProvider);
      if (mappedManager.Provider is IOrganizableProvider provider1)
      {
        ITaxon taxon = manager.GetTaxon(id);
        int num = typeof (ILifecycleDataItem).IsAssignableFrom(type) ? 1 : 0;
        bool flag1 = typeof (PageNode).IsAssignableFrom(type);
        bool flag2 = typeof (IRecyclableDataItem).IsAssignableFrom(type);
        if (num != 0)
          filter = filter.IsNullOrWhitespace() ? "Status = Master" : filter + " AND (Status = Master)";
        else if (flag1)
          filter = filter.IsNullOrWhitespace() ? "IsDeleted = False" : filter + " AND (IsDeleted = False)";
        IEnumerable enumerable = this.GetItems(taxon, provider1, type, sortExpression, skip, take, filter, ref totalCount);
        if (((num != 0 ? 0 : (!flag1 ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
          enumerable = (IEnumerable) enumerable.Cast<IRecyclableDataItem>().Where<IRecyclableDataItem>((Func<IRecyclableDataItem, bool>) (i => !i.IsDeleted));
        if (typeof (IDataItem).IsAssignableFrom(type))
        {
          FetchStrategy fetchStrategyForItem = mappedManager.GetFetchStrategyForItem(type, (string[]) null, true);
          enumerable = (IEnumerable) (enumerable as IQueryable<IDataItem>).LoadWith<IDataItem>(fetchStrategyForItem).ToList<IDataItem>();
        }
        foreach (object obj in enumerable)
        {
          if (obj is IDataItem dataItem)
            items.Add(new WcfMarketContentItem(dataItem, provider1 as UrlDataProviderBase));
        }
      }
      else
        totalCount = new int?(0);
      CollectionContext<WcfMarketContentItem> itemsInternal = new CollectionContext<WcfMarketContentItem>((IEnumerable<WcfMarketContentItem>) items);
      itemsInternal.TotalCount = totalCount.GetValueOrDefault();
      ServiceUtility.DisableCache();
      return itemsInternal;
    }

    private CollectionContext<WcfTaxonStatistic> GetTaxonStatisticsInternal(
      string taxonId,
      string provider)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(provider);
      Guid taxonGuidId = new Guid(taxonId);
      IQueryable<TaxonomyStatistic> queryable = manager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (st => st.TaxonId == taxonGuidId && (int) st.StatisticType == 0));
      List<WcfTaxonStatistic> items = new List<WcfTaxonStatistic>();
      foreach (TaxonomyStatistic stat in (IEnumerable<TaxonomyStatistic>) queryable)
      {
        if (stat.MarkedItemsCount > 0U)
          items.Add(new WcfTaxonStatistic()
          {
            Name = this.GetTypeName(stat),
            ItemType = stat.DataItemType,
            ItemProvider = stat.ItemProviderName,
            Count = stat.MarkedItemsCount
          });
      }
      CollectionContext<WcfTaxonStatistic> statisticsInternal = new CollectionContext<WcfTaxonStatistic>((IEnumerable<WcfTaxonStatistic>) items);
      statisticsInternal.TotalCount = items.Count;
      ServiceUtility.DisableCache();
      return statisticsInternal;
    }

    private bool RemoveItemFromTaxonInternal(
      string taxonId,
      string itemTypeName,
      string itemIDs,
      string provider,
      string itemProvider)
    {
      string[] strArray = itemIDs.Split(new char[1]{ ',' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length != 0)
      {
        WorkflowBatchExceptionHandler exceptionHandler = (WorkflowBatchExceptionHandler) null;
        Guid id = new Guid(taxonId);
        TaxonomyManager manager = TaxonomyManager.GetManager(provider);
        ITaxon taxon = manager.GetTaxon(id);
        if (!(taxon.Taxonomy is ISecuredObject securedObject))
          securedObject = manager.GetSecurityRoot();
        try
        {
          securedObject.Demand("Taxonomies", "DeleteTaxonomy");
        }
        catch (SecurityDemandFailException ex)
        {
          throw new WebProtocolException(HttpStatusCode.Forbidden, ex.Message, (Exception) null);
        }
        Type type = TypeResolutionService.ResolveType(itemTypeName);
        TaxonomyPropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(type, taxon);
        if (propertyDescriptor == null)
          throw new InvalidOperationException("Type " + itemTypeName + " does not contain field for " + (taxon != null ? taxon.Taxonomy.Name : "this taxon"));
        IManager mappedManager = ManagerBase.GetMappedManager(type, itemProvider);
        using (new ElevatedModeRegion(mappedManager))
        {
          foreach (string g in strArray)
          {
            Guid itemGuidId = new Guid(g);
            IEnumerable<IDataItem> dataItems = !typeof (ILifecycleDataItemGeneric).IsAssignableFrom(type) ? (IEnumerable<IDataItem>) mappedManager.GetItems(type, (string) null, (string) null, 0, 0).Cast<IDataItem>().Where<IDataItem>((Func<IDataItem, bool>) (i => i.Id == itemGuidId)).ToList<IDataItem>() : (IEnumerable<IDataItem>) mappedManager.GetItems(type, (string) null, (string) null, 0, 0).Cast<ILifecycleDataItemGeneric>().Where<ILifecycleDataItemGeneric>((Func<ILifecycleDataItemGeneric, bool>) (i => i.Id == itemGuidId || i.OriginalContentId == itemGuidId)).ToList<ILifecycleDataItemGeneric>();
            IDataItem component = (IDataItem) null;
            try
            {
              foreach (IDataItem dataItem in dataItems)
              {
                if (!((component = dataItem) is IOrganizable organizable))
                  throw new NotSupportedException("This method can work only with items that implement IOrganizable interface.");
                if (organizable.Organizer.TaxonExists(propertyDescriptor.Name, taxon.Id))
                {
                  if (propertyDescriptor.MetaField.IsSingleTaxon)
                    organizable.Organizer.RemoveTaxon(propertyDescriptor.Name, taxon.Id);
                  else
                    organizable.Organizer.RemoveTaxa(propertyDescriptor.Name, taxon.Id);
                }
              }
              mappedManager.SaveChanges();
            }
            catch (Exception ex)
            {
              string empty = string.Empty;
              if (component is Content)
                empty = ((Content) component).Title.ToString();
              else if (component is DynamicContent)
                empty = ModuleBuilderManager.GetTypeMainProperty(component.GetType()).GetValue((object) component).ToString();
              if (exceptionHandler == null)
                exceptionHandler = new WorkflowBatchExceptionHandler();
              else
                empty = component.Id.ToString();
              exceptionHandler.RegisterException(ex, empty);
            }
            if (exceptionHandler != null)
            {
              string pluralName;
              string typeName = this.GetTypeName(type, out pluralName);
              exceptionHandler.ThrowAccumulatedError(strArray.Length, "Unmark", typeName, pluralName);
            }
          }
        }
      }
      return true;
    }

    private IEnumerable GetItems(
      ITaxon taxon,
      IOrganizableProvider contentProvider,
      Type itemType,
      string sortExpression,
      int skip,
      int take,
      string filter,
      ref int? totalCount)
    {
      TaxonomyPropertyDescriptor propertyDescriptor = this.GetPropertyDescriptor(itemType, taxon);
      return contentProvider.GetItemsByTaxon(taxon.Id, propertyDescriptor.MetaField.IsSingleTaxon, propertyDescriptor.Name, itemType, filter, sortExpression, skip, take, ref totalCount);
    }

    private string GetTypeName(TaxonomyStatistic stat)
    {
      Type type = TypeResolutionService.ResolveType(stat.DataItemType);
      string pluralName;
      string typeName = this.GetTypeName(type, out pluralName);
      if (!pluralName.IsNullOrEmpty())
        typeName = pluralName;
      IManager mappedManager = ManagerBase.GetMappedManager(type, stat.ItemProviderName);
      if (mappedManager.GetProviderNames(ProviderBindingOptions.NoFilter).Count<string>() > 1)
        typeName = typeName + " (" + mappedManager.Provider.Title + ")";
      return typeName;
    }

    private string GetTypeName(Type type, out string pluralName)
    {
      string typeName;
      if (TypeDescriptor.GetAttributes(type)[typeof (ObjectInfoAttribute)] is ObjectInfoAttribute attribute)
      {
        pluralName = attribute.TitlePlural;
        typeName = attribute.Title;
      }
      else
      {
        typeName = type.Name;
        List<char> charList = new List<char>();
        for (int index = 0; index < typeName.Length; ++index)
        {
          if (char.IsUpper(typeName[index]) && index > 0)
          {
            charList.Add(' ');
            charList.Add(char.ToLower(typeName[index]));
          }
          else
            charList.Add(typeName[index]);
        }
        pluralName = new string(charList.ToArray());
        if (!pluralName.EndsWith("s"))
          pluralName += "s";
      }
      return typeName;
    }

    private TaxonomyPropertyDescriptor GetPropertyDescriptor(
      Type itemType,
      ITaxon taxon)
    {
      return TaxonomyManager.GetPropertyDescriptor(itemType, taxon);
    }
  }
}
