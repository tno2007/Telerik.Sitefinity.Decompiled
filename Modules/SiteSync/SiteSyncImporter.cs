// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync
{
  internal class SiteSyncImporter
  {
    protected bool shouldRescheduleNextRun;
    protected IEnumerable<object> removedTaxaToUpdate = (IEnumerable<object>) new List<object>();
    protected List<object> itemsToRemove = new List<object>();
    protected const string UnableToLoadItem = "Item cannot be imported because item type or item id are missing.";
    private ISiteSyncLogger siteSyncLogger;
    private string registrationPrefix;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.SiteSyncImporter" /> class.
    /// </summary>
    public SiteSyncImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.SiteSyncImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public SiteSyncImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    public ISiteSyncSerializer Serializer { get; set; }

    internal List<object> ItemsToRemove => this.itemsToRemove;

    public virtual void Import(ISiteSyncImportTransaction transaction)
    {
      string name;
      if (!transaction.Headers.TryGetValue("MultisiteMigrationTarget", out name))
      {
        this.ImportInternal(transaction);
      }
      else
      {
        using (new SiteRegion(SystemManager.CurrentContext.MultisiteContext.GetSiteByName(name)))
          this.ImportInternal(transaction);
      }
    }

    protected virtual void ImportScheduledItem(WrapperObject item, string transactionName)
    {
      if (!(item.GetProperty("objectTypeId").ToString() == typeof (ScheduledTaskData).FullName))
        return;
      Guid property = item.GetProperty<Guid>("Id");
      SchedulingManager manager = SchedulingManager.GetManager(string.Empty, transactionName);
      ScheduledTaskData taskData = manager.GetItemOrDefault(typeof (ScheduledTaskData), property) as ScheduledTaskData;
      if (taskData == null)
        taskData = manager.CreateTaskData(property);
      var args = new{ Manager = manager, Item = taskData };
      this.Serializer.SetProperties((object) taskData, (object) item, (object) args);
      this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is ScheduledTaskData && ((ScheduledTaskData) i).Id == taskData.Id));
      this.shouldRescheduleNextRun = true;
    }

    private void ImportInternal(ISiteSyncImportTransaction transaction)
    {
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      Guid guid = Guid.NewGuid();
      string transactionName = "Sync_" + guid.ToString();
      try
      {
        IEnumerable<WrapperObject> items = transaction.Items;
        if (items.Count<WrapperObject>() == 0)
          return;
        string propertyOrDefault = items.First<WrapperObject>().GetPropertyOrDefault<string>("LangId");
        this.itemsToRemove = new List<object>();
        this.removedTaxaToUpdate = (IEnumerable<object>) new List<object>();
        foreach (WrapperObject wrapperObject in items)
        {
          WrapperObject item = wrapperObject;
          string itemTypeValue = item.GetPropertyOrDefault<string>("objectTypeId");
          Type itemType = TypeResolutionService.ResolveType(itemTypeValue, false);
          Guid itemId = item.GetPropertyOrDefault<Guid>("objectId");
          string action = item.GetPropertyOrDefault<string>("ItemAction");
          if (!(itemType == (Type) null))
          {
            guid = new Guid();
            if (!guid.Equals(itemId))
            {
              string lang = item.GetPropertyOrDefault<string>("LangId");
              string provider = item.GetPropertyOrDefault<string>("Provider");
              Func<string, Action<string, Exception>> func = (Func<string, Action<string, Exception>>) (messagePrefix => (Action<string, Exception>) ((language, ex) => this.SiteSyncLogger.Write((object) this.PrepareMessage((object) item, string.IsNullOrEmpty(language) ? messagePrefix + " an item." : messagePrefix + string.Format(" an item in language '{0}'.", (object) language), itemId, itemTypeValue, provider, action, ex))));
              if (action == DataEventAction.Created || action == DataEventAction.Updated)
              {
                this.RunInSpecificCulture(lang, transactionName, (Action<FluentSitefinity>) (app => this.ImportItem(transactionName, itemType, itemId, item, provider, transaction)), func("Imported"));
                continue;
              }
              this.RunInSpecificCulture(lang, transactionName, (Action<FluentSitefinity>) (app => this.RemoveDataItem(transactionName, itemType, itemId, provider, lang)), func("Removed"));
              continue;
            }
          }
          this.SiteSyncLogger.Write((object) this.PrepareMessage((object) item, "Item cannot be imported because item type or item id are missing.", Guid.Empty, (string) null, (string) null, (string) null, (Exception) null));
        }
        this.UpdateRemovedTaxaSatistics(transactionName);
        this.RemoveUnnecessaryItems(transactionName);
        if (!string.IsNullOrEmpty(propertyOrDefault))
          SystemManager.CurrentContext.Culture = CultureInfo.GetCultureInfo(propertyOrDefault);
        TransactionManager.CommitTransaction(transactionName);
        this.OnTransactionComitted();
      }
      catch (Exception ex)
      {
        try
        {
          TransactionManager.RollbackTransaction(transactionName);
        }
        catch
        {
        }
        this.SiteSyncLogger.Write((object) ("Failed to finish transaction." + Environment.NewLine + "Error details:" + Environment.NewLine + ex.ToString()));
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        throw;
      }
      finally
      {
        SystemManager.CurrentContext.Culture = culture;
      }
    }

    protected virtual void OnTransactionComitted()
    {
      if (!this.shouldRescheduleNextRun)
        return;
      Scheduler.Instance.RescheduleNextRun();
    }

    protected virtual void RunInSpecificCulture(
      string culture,
      string transaction,
      Action<FluentSitefinity> func,
      Action<string, Exception> logAction)
    {
      CultureInfo culture1 = CultureInfo.InvariantCulture;
      if (!string.IsNullOrEmpty(culture))
        culture1 = CultureInfo.GetCultureInfo(culture);
      using (new CultureRegion(culture1))
      {
        try
        {
          FluentSitefinity fluentSitefinity = App.Prepare().SetTransactionName(transaction).WorkWith();
          func(fluentSitefinity);
          logAction(culture, (Exception) null);
        }
        catch (Exception ex)
        {
          logAction(culture, ex);
          throw;
        }
      }
    }

    /// <summary>Imports the content item.</summary>
    /// <param name="transactionName">The name of the transaction to work with.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="item">The object that is imported.</param>
    /// <param name="provider">The provider of the object being imported.</param>
    /// <param name="importTransaction">The transaction used to import the current item.</param>
    protected virtual void ImportItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction)
    {
      this.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, (Action<IDataItem, WrapperObject, IManager>) null);
    }

    /// <summary>Imports the content item.</summary>
    /// <param name="transactionName">The name of the transaction to work with.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="item">The object that is imported.</param>
    /// <param name="provider">The provider of the object being imported.</param>
    /// <param name="importTransaction">The transaction used to import the current item.</param>
    /// <param name="postProcessingAction">The post processing action.</param>
    internal virtual void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      if (itemType == typeof (ScheduledTaskData))
      {
        this.ImportScheduledItem(item, transactionName);
      }
      else
      {
        FluentSitefinity fluent = App.Prepare().SetContentProvider(provider).SetTransactionName(transactionName).WorkWith();
        Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(itemType, fluent, provider);
        bool flag1 = typeof (IHasParent).IsAssignableFrom(itemType) || typeof (IHasIDataItemParent).IsAssignableFrom(itemType);
        IDataItem parent = (IDataItem) null;
        bool flag2 = false;
        if (flag1)
        {
          parent = this.GetParent(item, provider, transactionName, fluent);
          flag2 = this.ShouldProcessCustomParent(itemType);
        }
        Guid siteId = Guid.Empty;
        List<Guid> propertyOrDefault = item.GetPropertyOrDefault<List<Guid>>("SiteIds");
        if (propertyOrDefault != null)
          siteId = propertyOrDefault.First<Guid>();
        this.ClearSchedulingRemains(facade.Manager, itemType, itemId, item);
        Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade dataItemFacade = (Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade) null;
        int num = facade.Exists(itemId) ? 1 : 0;
        bool flag3 = importTransaction.Headers.ContainsKey("MultisiteMigrationTarget");
        bool flag4 = item.HasProperty("ForceCreateNewItem");
        if (num == 0 | flag4)
        {
          using (SiteRegion.FromSiteId(siteId))
          {
            Guid itemId1 = itemId;
            if (flag3 & flag4)
              itemId1 = facade.Manager.Provider.GetNewGuid();
            if (this.ShouldCreateMappingForType(itemType))
              importTransaction.Response.Mappings.AddMapping(itemType.FullName, itemId.ToString(), itemId1.ToString());
            dataItemFacade = facade.CreateNew(itemId1);
          }
        }
        else
          dataItemFacade = facade.Load(itemId);
        IDataItem dataItem = dataItemFacade.Get();
        this.PrepareToUpdateRemovedTaxaStatistics(transactionName, provider, itemType, item, dataItem);
        this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is IDataItem dataItem1 && dataItem1.Id == dataItem.Id && dataItem1.GetType().FullName == dataItem.GetType().FullName && dataItem1.ApplicationName == dataItem.ApplicationName));
        if (flag1 && parent != null)
        {
          if (!flag2)
          {
            if (typeof (IHasParent).IsAssignableFrom(itemType))
              ((IHasParent) dataItem).Parent = (Content) parent;
            else
              ((IHasIDataItemParent) dataItem).ItemParent = parent;
          }
          else
            this.ProcessCustomParent(dataItem, parent, facade.Manager);
        }
        object obj1 = (object) new ExpandoObject();
        // ISSUE: reference to a compiler-generated field
        if (SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, IManager, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Manager", typeof (SiteSyncImporter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj2 = SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__0.Target((CallSite) SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__0, obj1, facade.Manager);
        // ISSUE: reference to a compiler-generated field
        if (SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__1 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, IDataItem, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Item", typeof (SiteSyncImporter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj3 = SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__1.Target((CallSite) SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__1, obj1, dataItem);
        // ISSUE: reference to a compiler-generated field
        if (SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__2 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__2 = CallSite<Func<CallSite, object, ISiteSyncImportTransaction, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "ImportTransaction", typeof (SiteSyncImporter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[2]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        object obj4 = SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__2.Target((CallSite) SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__2, obj1, importTransaction);
        // ISSUE: reference to a compiler-generated field
        if (SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__3 == null)
        {
          // ISSUE: reference to a compiler-generated field
          SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__3 = CallSite<Action<CallSite, ISiteSyncSerializer, IDataItem, WrapperObject, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetProperties", (IEnumerable<Type>) null, typeof (SiteSyncImporter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[4]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__3.Target((CallSite) SiteSyncImporter.\u003C\u003Eo__14.\u003C\u003Ep__3, this.Serializer, dataItem, item, obj1);
        dataItem.Provider = (object) facade.Manager.Provider;
        this.RecompileItemUrls(facade, dataItem);
        if (postProcessingAction != null)
          postProcessingAction(dataItem, item, facade.Manager);
        this.SetAdditionalValues(dataItem, provider, item, fluent, importTransaction);
        this.ValidateDataItemConstraints(dataItem, facade.Manager, importTransaction);
      }
    }

    protected virtual void RecompileItemUrls(Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade fluent, IDataItem dataItem)
    {
      if (!(fluent.Manager.Provider is UrlDataProviderBase provider))
        return;
      ILocatable locatable = dataItem as ILocatable;
      PageNode pageNode = dataItem as PageNode;
      if (locatable == null || pageNode != null)
        return;
      provider.RecompileItemUrls<ILocatable>(locatable);
    }

    protected virtual void ValidateDataItemConstraints(
      IDataItem dataItem,
      IManager manager,
      ISiteSyncImportTransaction importTransaction)
    {
      if (!(dataItem is ILocatable) || !(manager is IContentManager))
        return;
      ((IContentManager) manager).ValidateUrlConstraints<ILocatable>((ILocatable) dataItem);
    }

    protected virtual void ClearSchedulingRemains(
      IManager manager,
      Type itemType,
      Guid itemId,
      WrapperObject item)
    {
      if (!typeof (IApprovalWorkflowItem).IsAssignableFrom(itemType) || !typeof (ILifecycleDataItem).IsAssignableFrom(itemType))
        return;
      Guid guid = itemId;
      if (item != null && (ContentLifecycleStatus) Enum.Parse(typeof (ContentLifecycleStatus), item.GetProperty<long>("Status").ToString()) == ContentLifecycleStatus.Live)
      {
        guid = item.GetProperty<Guid>("OriginalContentId");
        object obj = manager.GetItems(itemType, "OriginalContentId = " + (object) guid + " AND Id <> " + (object) itemId, (string) null, 0, 0).Cast<object>().FirstOrDefault<object>();
        if (obj != null)
          manager.DeleteItem(obj);
      }
      this.RemoveWorkflowTask(guid.ToString(), manager.TransactionName);
    }

    protected void RemoveWorkflowTask(string id, string transactionName)
    {
      string name = SystemManager.CurrentContext.Culture.Name;
      this.itemsToRemove.AddRange((IEnumerable<object>) SiteSyncImporter.GetWorkflowTasks(id, name, transactionName));
    }

    /// <summary>Links a data item to given sites, using site links</summary>
    /// <param name="item">The data item</param>
    /// <param name="siteIds">The site ids</param>
    /// <param name="fluent">The fluent facade</param>
    /// <param name="siteLinkItemType">
    /// The site link item type, if the type is different from data item's type.
    /// For example taxonomies use "Taxonomy" as a type instead of "HierarchicalTaxonomy", "FlatTaxonomy", etc.
    /// </param>
    protected void LinkItemToSites(
      IDataItem item,
      IList<Guid> siteIds,
      FluentSitefinity fluent,
      string siteLinkItemType = null)
    {
      Type type = item.GetType();
      if (string.IsNullOrEmpty(siteLinkItemType))
        siteLinkItemType = type.FullName;
      IManager manager = this.GetFacade(type, fluent, string.Empty).Manager;
      if (!(manager.Provider is IMultisiteEnabledOAProvider provider1))
        return;
      IQueryable<SiteItemLink> source = provider1.GetSiteItemLinks().Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemId == item.Id));
      if (siteIds != null)
        source = source.Where<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => !siteIds.Contains(l.SiteId)));
      foreach (SiteItemLink link in (IEnumerable<SiteItemLink>) source)
        provider1.Delete(link);
      if (siteIds == null)
        return;
      foreach (Guid siteId1 in (IEnumerable<Guid>) siteIds)
      {
        Guid siteId = siteId1;
        if (!provider1.GetDirtyItems().Cast<object>().Any<object>((Func<object, bool>) (d => d is SiteItemLink && ((SiteItemLink) d).ItemId == item.Id && ((SiteItemLink) d).SiteId == siteId && ((SiteItemLink) d).ItemType == siteLinkItemType)))
        {
          if (!provider1.GetSiteItemLinks().Any<SiteItemLink>((Expression<Func<SiteItemLink, bool>>) (l => l.ItemType == siteLinkItemType && l.ItemId == item.Id && l.SiteId == siteId)))
          {
            if (manager.Provider is IMultisiteEnabledProvider provider2)
              provider2.AddItemLink(siteId, item);
            else
              provider1.AddItemLink(siteId, item);
          }
        }
      }
    }

    private bool ShouldCreateMappingForType(Type itemType) => typeof (PageTemplate).IsAssignableFrom(itemType) || typeof (ControlPresentation).IsAssignableFrom(itemType);

    internal void ImportTaxonomyStatistic(
      string transactionName,
      Guid itemId,
      object item,
      string provider)
    {
      TaxonomyManager manager = TaxonomyManager.GetManager(provider, transactionName);
      TaxonomyStatistic stat = manager.GetStatistics().FirstOrDefault<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.Id == itemId));
      if (stat == null)
        stat = this.CreateStatistic((DataProviderBase) manager.Provider, itemId);
      this.Serializer.SetProperties((object) stat, item);
      this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is TaxonomyStatistic taxonomyStatistic && taxonomyStatistic.Id == stat.Id && taxonomyStatistic.ApplicationName == stat.ApplicationName && taxonomyStatistic.ItemProviderName == stat.ItemProviderName && taxonomyStatistic.DataItemType == stat.DataItemType));
    }

    internal void PrepareStatisticsToRemove(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider)
    {
      foreach (DataProviderBase staticProvider in ((IManager) TaxonomyManager.GetManager()).StaticProviders)
        this.itemsToRemove.AddRange((IEnumerable<object>) TaxonomyManager.GetManager(staticProvider.Name, transactionName).GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.Id == itemId && s.DataItemType == itemType.FullName && s.ItemProviderName == provider)));
    }

    /// <summary>
    /// Returns whether the given type should be handled separately when an item of this type has its parent set.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns>A value indicating whether the given type should be handled separately when an item of this type has its parent set.</returns>
    protected virtual bool ShouldProcessCustomParent(Type itemType) => false;

    /// <summary>Handles the custom assignment of a parent.</summary>
    /// <param name="item">The item.</param>
    /// <param name="parent">The parent of the item.</param>
    /// <param name="manager">The manager.</param>
    protected virtual void ProcessCustomParent(IDataItem item, IDataItem parent, IManager manager)
    {
    }

    /// <summary>Loads or creates the parent of the given item.</summary>
    /// <param name="component">The component.</param>
    /// <param name="provider">The provider name.</param>
    /// <param name="transaction">The transaction name.</param>
    /// <param name="fluent">The fluent API facade.</param>
    /// <returns>The parent of the given item.</returns>
    protected virtual IDataItem GetParent(
      WrapperObject component,
      string provider,
      string transaction,
      FluentSitefinity fluent)
    {
      Type itemType = TypeResolutionService.ResolveType(component.GetPropertyOrDefault<string>("ParentType"), false);
      Guid propertyOrDefault = component.GetPropertyOrDefault<Guid>("ParentId");
      if (itemType == (Type) null || propertyOrDefault == Guid.Empty)
        return (IDataItem) null;
      Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(itemType, fluent, provider);
      return facade.Exists(propertyOrDefault) ? facade.Load(propertyOrDefault).Get() : throw new ArgumentException("Unable to find parent of type {0}  with ID {1}".Arrange((object) itemType, (object) propertyOrDefault));
    }

    protected string PrepareMessage(
      object item,
      string message,
      Guid itemId,
      string itemType,
      string itemProvider,
      string action,
      Exception ex)
    {
      WrapperObject wrapperObject = !(item is WrapperObject) ? new WrapperObject(item) : (WrapperObject) item;
      return this.SiteSyncLogger.PrepareMessage(message, "Error importing item - {0}", wrapperObject, (object) itemId, itemType, itemProvider, (string) null, action, ex);
    }

    protected virtual void RemoveDataItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider,
      string language)
    {
      FluentSitefinity fluent = App.Prepare().SetContentProvider(provider).SetTransactionName(transactionName).WorkWith();
      Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(itemType, fluent, provider);
      this.ClearSchedulingRemains(facade.Manager, itemType, itemId, (WrapperObject) null);
      if (!facade.Exists(itemId))
        return;
      CultureInfo language1 = string.IsNullOrEmpty(language) ? (CultureInfo) null : CultureInfo.GetCultureInfo(language);
      facade.Load(itemId).Delete(language1);
    }

    protected virtual void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
      this.ProcessTranslations(dataItem);
      dataItem.Transaction = (object) fluent.AppSettings.TransactionName;
    }

    /// <summary>
    /// Removes the cultures from the item that are not synced.
    /// If they are not removed, they fallback to the default language,
    /// causing the item to be visible on the frontend even when it is unpublished on source site.
    /// </summary>
    /// <param name="item">The item that will be processed</param>
    protected virtual void ProcessTranslations(IDataItem item)
    {
      ILifecycleDataItem lifecycleDataItem = item as ILifecycleDataItem;
      ILocalizable localizable = item as ILocalizable;
      if (lifecycleDataItem == null || localizable == null)
        return;
      IList<string> publishedTranslations = lifecycleDataItem.PublishedTranslations;
      IList<LanguageData> languageData1 = lifecycleDataItem.LanguageData;
      publishedTranslations.Clear();
      foreach (LanguageData languageData2 in (IEnumerable<LanguageData>) languageData1)
      {
        CultureInfo cultureByName = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByName(languageData2.Language);
        publishedTranslations.Add(cultureByName.Name);
      }
    }

    protected virtual Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade GetFacade(
      Type itemType,
      FluentSitefinity fluent,
      string providerName)
    {
      Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = fluent.DataItemFacade();
      this.SetFacadeManager(itemType, facade);
      facade.SetProviderName(providerName);
      facade.SetItemType(itemType);
      return facade;
    }

    protected virtual void SetFacadeManager(Type itemType, Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade)
    {
    }

    private void UpdateRemovedTaxaSatistics(string transactionName)
    {
      foreach (object obj in this.removedTaxaToUpdate)
        this.UpdateRemovedTaxonSatistics(obj, transactionName);
    }

    private void RemoveUnnecessaryItems(string transactionName)
    {
      foreach (object obj in this.itemsToRemove)
        this.RemoveUnnecessaryItem(obj, transactionName);
    }

    internal virtual void UpdateRemovedTaxonSatistics(object item, string transactionName)
    {
      if (!(item is TaxonomyStatistic taxonomyStatistic))
        return;
      --taxonomyStatistic.MarkedItemsCount;
    }

    internal virtual void RemoveUnnecessaryItem(
      object item,
      string transactionName,
      bool retrieveItemBeforeDelete = false)
    {
      TaxonomyStatistic stat = item as TaxonomyStatistic;
      if (stat != null)
      {
        TaxonomyManager manager = TaxonomyManager.GetManager(string.Empty, transactionName);
        if (retrieveItemBeforeDelete)
          stat = manager.GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.Id == stat.Id)).FirstOrDefault<TaxonomyStatistic>();
        manager.DeleteStatistic(stat);
      }
      else
      {
        if (!(item is IDataItem itemToSet))
          return;
        string providerName = itemToSet.Provider == null ? string.Empty : itemToSet.Provider.ToString();
        FluentSitefinity fluent = App.Prepare().SetTransactionName(transactionName).WorkWith();
        Telerik.Sitefinity.Fluent.IDataItemFacade.IDataItemFacade facade = this.GetFacade(item.GetType(), fluent, providerName);
        if (retrieveItemBeforeDelete)
          facade.Load(itemToSet.Id).Delete();
        else
          facade.Set(itemToSet).Delete();
      }
    }

    internal TaxonomyStatistic CreateStatistic(DataProviderBase provider, Guid id)
    {
      TaxonomyStatistic entity = new TaxonomyStatistic()
      {
        Id = id,
        ApplicationName = provider.ApplicationName
      };
      ((IDataItem) entity).Provider = (object) provider;
      if (provider is IOpenAccessDataProvider provider1)
        provider1.GetContext().Add((object) entity);
      else
        this.SiteSyncLogger.Write((object) string.Format("Current Taxonomy provider is not an IOpenAccessDataProvider and the statistic with id: {0} failed to sync.", (object) id));
      return entity;
    }

    private void PrepareToUpdateRemovedTaxaStatistics(
      string transactionName,
      string provider,
      Type itemType,
      WrapperObject item,
      IDataItem dataItem)
    {
      IEnumerable<Guid> removedTaxaIds = this.GetRemovedTaxaIds(itemType, item, dataItem);
      IEnumerable<DataProviderBase> staticProviders = ((IManager) TaxonomyManager.GetManager()).StaticProviders;
      foreach (Guid guid in removedTaxaIds)
      {
        Guid taxonId = guid;
        foreach (DataProviderBase dataProviderBase in staticProviders)
          this.removedTaxaToUpdate = this.removedTaxaToUpdate.Union<object>((IEnumerable<object>) TaxonomyManager.GetManager(dataProviderBase.Name, transactionName).GetStatistics().Where<TaxonomyStatistic>((Expression<Func<TaxonomyStatistic, bool>>) (s => s.TaxonId == taxonId && s.DataItemType == itemType.FullName && s.ItemProviderName == provider)));
      }
    }

    private IEnumerable<Guid> GetRemovedTaxaIds(
      Type itemType,
      WrapperObject item,
      IDataItem dataItem)
    {
      TaxonomyPropertyDescriptor[] propertiesForType = OrganizerBase.GetPropertiesForType(itemType);
      List<Guid> guidList = new List<Guid>();
      List<Guid> newStateTaxaIds = new List<Guid>();
      foreach (TaxonomyPropertyDescriptor propertyDescriptor in propertiesForType)
      {
        if (propertyDescriptor.GetValue((object) dataItem) is IEnumerable<Guid> collection)
          guidList.AddRange(collection);
        List<Guid> propertyOrDefault = item.GetPropertyOrDefault<List<Guid>>(propertyDescriptor.Name);
        if (propertyOrDefault != null)
          newStateTaxaIds.AddRange((IEnumerable<Guid>) propertyOrDefault);
      }
      List<Guid> removedTaxaIds = guidList;
      removedTaxaIds.RemoveAll((Predicate<Guid>) (p => newStateTaxaIds.Contains(p)));
      return (IEnumerable<Guid>) removedTaxaIds;
    }

    internal static IEnumerable<ScheduledTaskData> GetWorkflowTasks(
      string itemId,
      string language,
      string transactionName = null)
    {
      return SchedulingManager.GetManager(string.Empty, transactionName).GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.TaskName == "WorkflowCallTask")).AsEnumerable<ScheduledTaskData>().Where<ScheduledTaskData>((Func<ScheduledTaskData, bool>) (t => XDocument.Parse(t.TaskData).Element((XName) "TaskData").Attribute(XName.Get("ContentItemMasterId")).Value == itemId)).Where<ScheduledTaskData>((Func<ScheduledTaskData, bool>) (t => t.Language == language || t.Language == null));
    }

    protected ISiteSyncLogger SiteSyncLogger
    {
      get
      {
        if (this.siteSyncLogger == null)
          this.siteSyncLogger = !string.IsNullOrEmpty(this.registrationPrefix) ? ObjectFactory.Resolve<ISiteSyncLogger>(this.RegistrationPrefix) : ObjectFactory.Resolve<ISiteSyncLogger>();
        return this.siteSyncLogger;
      }
    }

    protected string RegistrationPrefix
    {
      get => this.registrationPrefix;
      set => this.registrationPrefix = value;
    }
  }
}
