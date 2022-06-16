// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingPointDynamicTypeManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  public class PublishingPointDynamicTypeManager : 
    ManagerBase<PublishingPointDynamicTypeProviderBase>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    public PublishingPointDynamicTypeManager()
      : this((string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    public PublishingPointDynamicTypeManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Publishing.PublishingManager" /> class.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    public PublishingPointDynamicTypeManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    /// <summary>Saves the changes.</summary>
    public override void SaveChanges()
    {
      if (!string.IsNullOrEmpty(this.Provider.TransactionName))
        return;
      base.SaveChanges();
    }

    /// <summary>Gets the default provider delegate.</summary>
    /// <value>The default provider delegate.</value>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<PublishingDataItemsConfig>().DefaultProvider);

    /// <summary>Gets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public override string ModuleName => "Publishing";

    /// <summary>Gets the providers settings.</summary>
    /// <value>The providers settings.</value>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<PublishingDataItemsConfig>().ProviderSettings;

    /// <summary>
    /// Get an instance of the publishing manager using the default provider
    /// </summary>
    /// <returns>Instance of publishing manager</returns>
    public static PublishingPointDynamicTypeManager GetManager() => ManagerBase<PublishingPointDynamicTypeProviderBase>.GetManager<PublishingPointDynamicTypeManager>();

    /// <summary>
    /// Get an instance of the publishing manager by explicitly specifying the required provider to use
    /// </summary>
    /// <param name="providerName">Name of the provider to use, or null/empty string to use the default provider.</param>
    /// <returns>Instance of the publishing manager</returns>
    public static PublishingPointDynamicTypeManager GetManager(
      string providerName)
    {
      return ManagerBase<PublishingPointDynamicTypeProviderBase>.GetManager<PublishingPointDynamicTypeManager>(providerName);
    }

    /// <summary>Get publishing manager in named transaction</summary>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="transactionName">Name of the transaction</param>
    /// <returns>Manager in named transaction. Don't use SaveChanges with it, use TransactionManager.CommitTransaction, instead.</returns>
    public static PublishingPointDynamicTypeManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<PublishingPointDynamicTypeProviderBase>.GetManager<PublishingPointDynamicTypeManager>(providerName, transactionName);
    }

    /// <summary>Creates dynamic type for a publishing point.</summary>
    /// <param name="forPublishingPoint">Publishing pont whose dynamic type to create</param>
    public static MetaType CreateDynamicType(
      IPublishingPoint forPublishingPoint,
      MetadataManager metadataManager = null)
    {
      if (forPublishingPoint == null)
        throw new ArgumentNullException("publishingPoint");
      if (forPublishingPoint.StorageTypeName != null)
        return (MetaType) null;
      forPublishingPoint.StorageTypeName = "Telerik.Sitefinity.Publishing.Model" + (object) '.' + ("Dynamic_" + forPublishingPoint.Id.ToString("N"));
      PublishingExtensionMethods.FullTypeName dynamicTypeName = forPublishingPoint.GetDynamicTypeName();
      if (metadataManager == null)
        metadataManager = MetadataManager.GetManager();
      MetaType metaType = metadataManager.CreateMetaType(dynamicTypeName.Namespace, dynamicTypeName.ClassName);
      metaType.IsDynamic = true;
      metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
      metaType.BaseClassName = typeof (DynamicTypeBase).FullName;
      metaType.ModuleName = PublishingPointDynamicTypeManager.GetManager(forPublishingPoint.StorageItemsProvider).Provider.ModuleName;
      return metaType;
    }

    /// <summary>
    /// Creates dynamic type for a publishing point when a module is initialized.
    /// </summary>
    /// <param name="forPublishingPoint">Publishing pont whose dynamic type to create</param>
    /// <param name="initializer">The initializer</param>
    internal static MetaType CreateDynamicType(
      IPublishingPoint forPublishingPoint,
      SiteInitializer initializer)
    {
      if (forPublishingPoint == null)
        throw new ArgumentNullException("publishingPoint");
      if (forPublishingPoint.StorageTypeName != null)
        return (MetaType) null;
      forPublishingPoint.StorageTypeName = "Telerik.Sitefinity.Publishing.Model" + (object) '.' + ("Dynamic_" + forPublishingPoint.Id.ToString("N"));
      if (!(forPublishingPoint.Provider is PublishingDataProviderBase))
      {
        PublishingDataProviderBase provider = initializer.GetManagerInTransaction<PublishingManager>().Provider;
      }
      PublishingExtensionMethods.FullTypeName dynamicTypeName = forPublishingPoint.GetDynamicTypeName();
      MetaType metaType = initializer.GetManagerInTransaction<MetadataManager>().CreateMetaType(dynamicTypeName.Namespace, dynamicTypeName.ClassName);
      metaType.IsDynamic = true;
      metaType.DatabaseInheritance = DatabaseInheritanceType.vertical;
      metaType.BaseClassName = typeof (DynamicTypeBase).FullName;
      metaType.ModuleName = initializer.GetManagerInTransaction<PublishingPointDynamicTypeManager>(forPublishingPoint.StorageItemsProvider).Provider.ModuleName;
      return metaType;
    }

    /// <summary>Gets the data items.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <returns></returns>
    public IQueryable GetDataItems(IPublishingPoint publishingPoint)
    {
      this.ValidatePublishingPoint(publishingPoint, false);
      return publishingPoint.StorageTypeName == null ? (IQueryable) ((IEnumerable<DynamicTypeBase>) new DynamicTypeBase[0]).AsQueryable<DynamicTypeBase>() : this.Provider.GetDataItems(publishingPoint.StorageTypeName);
    }

    /// <summary>Gets the data items.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="originalItemId">The original item id.</param>
    /// <returns></returns>
    public IQueryable GetDataItems(IPublishingPoint publishingPoint, Guid originalItemId)
    {
      this.ValidatePublishingPoint(publishingPoint, false);
      return publishingPoint.StorageTypeName == null ? (IQueryable) ((IEnumerable<object>) new object[0]).AsQueryable<object>() : this.Provider.GetDataItems(publishingPoint.StorageTypeName, originalItemId);
    }

    /// <summary>Gets the children data items.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="parentId">The parent id.</param>
    /// <returns></returns>
    public IQueryable GetChildrenDataItems(
      IPublishingPoint publishingPoint,
      Guid parentId)
    {
      this.ValidatePublishingPoint(publishingPoint, false);
      return publishingPoint.StorageTypeName == null ? (IQueryable) ((IEnumerable<object>) new object[0]).AsQueryable<object>() : this.Provider.GetChildrenDataItems(publishingPoint.StorageTypeName, parentId);
    }

    /// <summary>Creates the data item.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <returns></returns>
    public object CreateDataItem(IPublishingPoint publishingPoint) => this.CreateDataItem(publishingPoint, this.Provider.GetNewGuid());

    /// <summary>Creates the data item.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="Id">The id.</param>
    /// <returns></returns>
    public object CreateDataItem(IPublishingPoint publishingPoint, Guid Id)
    {
      this.ValidatePublishingPoint(publishingPoint);
      return this.Provider.CreateDataItem(publishingPoint.StorageTypeName, Id);
    }

    /// <summary>Gets the data item.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="dataItemID">The data item ID.</param>
    /// <returns></returns>
    public object GetDataItem(IPublishingPoint publishingPoint, Guid dataItemID)
    {
      this.ValidatePublishingPoint(publishingPoint);
      return this.Provider.GetDataItem(publishingPoint.StorageTypeName, dataItemID);
    }

    /// <summary>Deletes the data item.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    /// <param name="Id">The id.</param>
    public void DeleteDataItem(IPublishingPoint publishingPoint, Guid Id)
    {
      this.ValidatePublishingPoint(publishingPoint);
      this.Provider.DeleteDataItem(publishingPoint.StorageTypeName, Id);
    }

    /// <summary>Deletes the data item.</summary>
    /// <param name="item">The item.</param>
    public void DeleteDataItem(object item) => this.Provider.DeleteDataItem(item);

    /// <summary>Validates the publishing point.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void ValidatePublishingPoint(IPublishingPoint publishingPoint) => this.ValidatePublishingPoint(publishingPoint, true);

    /// <summary>Validates the publishing point.</summary>
    /// <param name="publishingPoint">The publishing point.</param>
    protected virtual void ValidatePublishingPoint(
      IPublishingPoint publishingPoint,
      bool validateStorageType)
    {
      if (publishingPoint == null)
        throw new ArgumentNullException(nameof (publishingPoint));
      if (validateStorageType && string.IsNullOrEmpty(publishingPoint.StorageTypeName))
        throw new InvalidOperationException("Publishing point storage type is empty");
    }
  }
}
