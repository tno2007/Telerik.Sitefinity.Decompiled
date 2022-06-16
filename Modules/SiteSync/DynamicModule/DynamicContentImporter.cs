// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.DynamicContentImporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.DynamicModules.Web.Services;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSync.Serialization;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.SiteSync
{
  internal class DynamicContentImporter : ContentItemsImporterBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.DynamicContentImporter" /> class.
    /// </summary>
    public DynamicContentImporter()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.SiteSync.DynamicContentImporter" /> class.
    /// </summary>
    /// <param name="registrationPrefix">The registration prefix.</param>
    public DynamicContentImporter(string registrationPrefix) => this.RegistrationPrefix = registrationPrefix;

    internal override void ImportItemInternal(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction,
      Action<IDataItem, WrapperObject, IManager> postProcessingAction)
    {
      if (itemType == typeof (TaxonomyStatistic) || itemType == typeof (ScheduledTaskData))
      {
        base.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, postProcessingAction);
      }
      else
      {
        Guid siteId = Guid.Empty;
        object propertyOrNull = item.GetPropertyOrNull("SiteIds");
        if (propertyOrNull != null)
          siteId = !(propertyOrNull is string g) ? new Guid(((IEnumerable<string>) propertyOrNull).First<string>()) : new Guid(g);
        DynamicModuleManager manager = DynamicModuleManager.GetManager(provider, transactionName);
        this.ClearSchedulingRemains((IManager) manager, itemType, itemId, item);
        this.HideDuplicatedMovedItemsDraft(itemType, item, manager);
        IDataItem dynamicContent = this.GetOrCreateItem(itemType, itemId, siteId, manager);
        this.itemsToRemove.RemoveAll((Predicate<object>) (i => i is IDataItem dataItem && dataItem.Id == dynamicContent.Id && dataItem.Provider == dynamicContent.Provider && dataItem.ApplicationName == dynamicContent.ApplicationName));
        IDataItem parent = this.GetParent(item, provider, transactionName);
        if (parent != null)
          ((IHierarchicalItem) dynamicContent).Parent = (IHierarchicalItem) parent;
        object obj = (object) new
        {
          Manager = manager,
          Item = dynamicContent
        };
        Func<PropertyDescriptor, PropertyDescriptor, bool> func = (Func<PropertyDescriptor, PropertyDescriptor, bool>) ((source, dest) => dest.Name != "Provider" && dest.Name != "parent_id");
        // ISSUE: reference to a compiler-generated field
        if (DynamicContentImporter.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
        {
          // ISSUE: reference to a compiler-generated field
          DynamicContentImporter.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Action<CallSite, ISiteSyncSerializer, IDataItem, WrapperObject, Func<PropertyDescriptor, PropertyDescriptor, bool>, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SetProperties", (IEnumerable<Type>) null, typeof (DynamicContentImporter), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[5]
          {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
          }));
        }
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        DynamicContentImporter.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) DynamicContentImporter.\u003C\u003Eo__2.\u003C\u003Ep__0, this.Serializer, dynamicContent, item, func, obj);
        this.SetAdditionalValues(dynamicContent, provider, item, (FluentSitefinity) null, importTransaction);
        this.ValidateDataItemConstraints(dynamicContent, (IManager) manager, importTransaction);
        if (postProcessingAction == null)
          return;
        postProcessingAction(dynamicContent, item, (IManager) manager);
      }
    }

    /// <inheritdoc />
    protected override void ImportItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      WrapperObject item,
      string provider,
      ISiteSyncImportTransaction importTransaction)
    {
      this.ImportItemInternal(transactionName, itemType, itemId, item, provider, importTransaction, (Action<IDataItem, WrapperObject, IManager>) null);
    }

    protected override void ValidateDataItemConstraints(
      IDataItem dataItem,
      IManager manager,
      ISiteSyncImportTransaction importTransaction)
    {
      if (!(dataItem is DynamicContent dataItem1))
        return;
      string lower = string.Format("default-{0}", (object) dataItem1.GetType().Name).ToLower();
      if (!(dataItem1.UrlName != (Lstring) lower))
        return;
      Guid itemMasterId = dataItem1.OriginalContentId == Guid.Empty ? dataItem1.Id : dataItem1.OriginalContentId;
      new DataService().CheckIfUrlIsValid((DynamicModuleManager) manager, dataItem1, dataItem.GetType(), itemMasterId);
    }

    /// <summary>Gets or creates item with the specified id.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="dynamicModuleManager">The dynamic module manager.</param>
    /// <returns>The item.</returns>
    protected virtual IDataItem GetOrCreateItem(
      Type itemType,
      Guid itemId,
      Guid siteId,
      DynamicModuleManager dynamicModuleManager)
    {
      IDataItem dataItem;
      if (!this.TryGetItem(itemType, itemId, dynamicModuleManager, out dataItem))
        dataItem = this.CreateItem(itemType, itemId, siteId, dynamicModuleManager);
      return dataItem;
    }

    /// <summary>Creates an item with the specified id.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="siteId">The site id.</param>
    /// <param name="dynamicModuleManager">The dynamic module manager.</param>
    /// <returns>The created item.</returns>
    protected virtual IDataItem CreateItem(
      Type itemType,
      Guid itemId,
      Guid siteId,
      DynamicModuleManager dynamicModuleManager)
    {
      using (SiteRegion.FromSiteId(siteId))
        return (IDataItem) dynamicModuleManager.CreateDataItem(itemType, itemId, dynamicModuleManager.Provider.ApplicationName);
    }

    /// <summary>Tries to get an existing item with the specified id.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="dynamicModuleManager">The dynamic module manager.</param>
    /// <param name="item">The item.</param>
    /// <returns>True if item exists otherwise false.</returns>
    protected virtual bool TryGetItem(
      Type itemType,
      Guid itemId,
      DynamicModuleManager dynamicModuleManager,
      out IDataItem item)
    {
      item = (IDataItem) dynamicModuleManager.GetDataItems(itemType).FirstOrDefault<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Id == itemId));
      return item != null;
    }

    protected override void SetAdditionalValues(
      IDataItem dataItem,
      string provider,
      WrapperObject wrapperObject,
      FluentSitefinity fluent,
      ISiteSyncImportTransaction transaction)
    {
      Type type = dataItem.GetType();
      if (!(type == typeof (TaxonomyStatistic)) && !(type == typeof (ScheduledTaskData)))
        return;
      base.SetAdditionalValues(dataItem, provider, wrapperObject, fluent, transaction);
    }

    /// <inheritdoc />
    protected override void RemoveDataItem(
      string transactionName,
      Type itemType,
      Guid itemId,
      string provider,
      string language)
    {
      DynamicModuleManager manager = DynamicModuleManager.GetManager(provider, transactionName);
      DynamicContent dynamicContent = manager.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Id == itemId)).FirstOrDefault<DynamicContent>();
      if (dynamicContent == null)
        return;
      this.ClearSchedulingRemains((IManager) manager, itemType, itemId, (WrapperObject) null);
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      manager.DeleteDataItem(dynamicContent, culture);
    }

    /// <inheritdoc />
    private IDataItem GetParent(
      WrapperObject component,
      string provider,
      string transaction)
    {
      Type itemType = TypeResolutionService.ResolveType(component.GetPropertyOrDefault<string>("ParentType"), false);
      Guid parentId = component.GetPropertyOrDefault<Guid>("ParentId");
      if (itemType == (Type) null || parentId == Guid.Empty)
        return (IDataItem) null;
      return (IDataItem) (DynamicModuleManager.GetManager(provider, transaction).GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Id == parentId)).FirstOrDefault<DynamicContent>() ?? throw new ArgumentException("Unable to find parent of type {0}  with ID {1}".Arrange((object) itemType, (object) parentId)));
    }

    private void HideDuplicatedMovedItemsDraft(
      Type itemType,
      WrapperObject item,
      DynamicModuleManager dynamicModuleManager)
    {
      if (!item.HasProperty("SystemUrl") || !(item.GetProperty("SystemUrl").ToString() == "/" + string.Format("default-{0}", (object) itemType.Name).ToLower()))
        return;
      DynamicModuleType dynamicModuleType = ModuleBuilderManager.GetManager().GetDynamicModuleType(itemType);
      DynamicContent defaultDataItem = dynamicModuleManager.GetDefaultDataItem(dynamicModuleType);
      if (defaultDataItem == null || !(defaultDataItem.ApprovalWorkflowState == (Lstring) LifecycleExtensions.StatusDraft) || defaultDataItem.SystemHasChildItems)
        return;
      defaultDataItem.Status = ContentLifecycleStatus.Temp;
      defaultDataItem.SetWorkflowStatus(dynamicModuleManager.Provider.ApplicationName, LifecycleExtensions.StatusPublished);
    }
  }
}
