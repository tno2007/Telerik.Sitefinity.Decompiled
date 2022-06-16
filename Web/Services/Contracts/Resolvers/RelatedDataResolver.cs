// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.RelatedDataPropertyResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Web.Services.Contracts
{
  internal class RelatedDataPropertyResolver : PropertyRelationResolverBase
  {
    public override IQueryable GetRelatedItems(object item)
    {
      IQueryable<IDataItem> source = item.GetRelatedItems(this.Descriptor.Name);
      if (typeof (ILifecycleDataItemGeneric).IsAssignableFrom(this.RelatedType) && RelatedDataExtensions.GetStatus(item) == ContentLifecycleStatus.Live)
        source = (IQueryable<IDataItem>) Queryable.Cast<ILifecycleDataItemGeneric>(source).EnhanceQueryToFilterPublished<ILifecycleDataItemGeneric>(SystemManager.CurrentContext.Culture);
      return (IQueryable) source;
    }

    public override object GetRelatedItem(object item, Guid relatedItemKey)
    {
      DataProviderBase provider;
      IDataItem asDataItem = this.GetAsDataItem(item, out provider);
      return (object) this.GetRelatedItem(provider, asDataItem, relatedItemKey, (string) null);
    }

    public override void CreateRelation(
      object item,
      Guid relatedItemKey,
      string relatedItemProvider,
      object persistentItem)
    {
      DataProviderBase provider;
      IDataItem asDataItem = this.GetAsDataItem(item, out provider);
      asDataItem.CreateRelation(this.GetItemToRelate(provider, asDataItem, relatedItemKey, relatedItemProvider, persistentItem) ?? throw new ItemNotFoundException("The item with key '{0}' was not found".Arrange((object) relatedItemKey)), this.Descriptor.Name);
    }

    public override void DeleteRelation(object item, Guid relatedItemKey)
    {
      DataProviderBase provider;
      IDataItem asDataItem = this.GetAsDataItem(item, out provider);
      asDataItem.DeleteRelation(this.GetRelatedItem(provider, asDataItem, relatedItemKey, (string) null) ?? throw new ItemNotFoundException("The item with key '{0}' was not found".Arrange((object) relatedItemKey)), this.Descriptor.Name);
    }

    public override void DeleteAllRelations(object item) => this.GetAsDataItem(item, out DataProviderBase _).DeleteRelations(this.Descriptor.Name);

    private IDataItem GetAsDataItem(object item, out DataProviderBase provider)
    {
      provider = item is IDataItem dataItem ? dataItem.GetProvider() as DataProviderBase : throw new NotSupportedException("An object of type IDataItem is expected.");
      if (provider == null)
        throw new NotSupportedException("A provider of type DataProviderBase is expected.");
      return dataItem;
    }

    private IDataItem GetItemToRelate(
      DataProviderBase provider,
      IDataItem item,
      Guid relatedItemKey,
      string relatedItemProvider,
      object persistentItem)
    {
      Type relatedType = this.RelatedType;
      if (string.IsNullOrEmpty(relatedItemProvider) && this.Descriptor.Attributes[typeof (MetaFieldAttributeAttribute)] is MetaFieldAttributeAttribute attribute)
        attribute.Attributes.TryGetValue(DynamicAttributeNames.RelatedProviders, out relatedItemProvider);
      if (relatedItemProvider == "sf-site-default-provider")
        relatedItemProvider = RelatedDataHelper.ResolveProvider(relatedType.FullName);
      if (relatedItemProvider == "sf-any-site-provider")
        throw new ArgumentException("When related data field is configured to work with all sources for current site, relatedItemProvider must be passed to CreateRelation");
      Type mappedManagerType = ManagerBase.GetMappedManagerType(relatedType.FullName);
      IManager relatedManager = provider.GetRelatedManager(mappedManagerType, relatedItemProvider);
      ContentLifecycleStatus status = ContentLifecycleStatus.Master;
      if (persistentItem is ILifecycleDataItemGeneric lifecycleDataItemGeneric)
        status = lifecycleDataItemGeneric.Status;
      Type itemType = relatedType;
      return this.FilterItemsByStatusAndKey((IQueryable) relatedManager.GetItems(itemType, (string) null, (string) null, 0, 0), status, relatedItemKey);
    }

    private IDataItem GetRelatedItem(
      DataProviderBase provider,
      IDataItem item,
      Guid relatedItemKey,
      string relatedItemProvider)
    {
      ILifecycleDataItemGeneric lifecycleDataItemGeneric = item as ILifecycleDataItemGeneric;
      ContentLifecycleStatus status = ContentLifecycleStatus.Master;
      if (lifecycleDataItemGeneric != null)
        status = lifecycleDataItemGeneric.Status;
      return this.FilterItemsByStatusAndKey(this.GetRelatedItems((object) item), status, relatedItemKey);
    }

    private IDataItem FilterItemsByStatusAndKey(
      IQueryable items,
      ContentLifecycleStatus status,
      Guid relatedItemKey)
    {
      IQueryable<ILifecycleDataItemGeneric> query = Queryable.Cast<ILifecycleDataItemGeneric>(items);
      if (query != null && status == ContentLifecycleStatus.Live)
        return (IDataItem) Queryable.Cast<ILifecycleDataItemGeneric>(query.EnhanceQueryToFilterPublished<ILifecycleDataItemGeneric>(SystemManager.CurrentContext.Culture)).Where<ILifecycleDataItemGeneric>((Expression<Func<ILifecycleDataItemGeneric, bool>>) (x => x.OriginalContentId == relatedItemKey && (int) x.Status == 2)).FirstOrDefault<ILifecycleDataItemGeneric>();
      return Queryable.Cast<IDataItem>(items).Where<IDataItem>((Expression<Func<IDataItem, bool>>) (x => x.Id == relatedItemKey)).FirstOrDefault<IDataItem>();
    }

    public override Type RelatedType => (this.Descriptor as RelatedDataPropertyDescriptor).RelatedType;

    public override string RelatedProviders => (this.Descriptor as RelatedDataPropertyDescriptor).RelatedProviders;

    public override bool IsMultipleRelation => (this.Descriptor as RelatedDataPropertyDescriptor).MetaField.AllowMultipleRelations;
  }
}
