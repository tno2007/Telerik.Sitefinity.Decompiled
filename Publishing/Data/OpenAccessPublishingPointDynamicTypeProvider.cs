// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Data.OpenAccessPublishingPointDynamicTypeProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Publishing.Data
{
  public class OpenAccessPublishingPointDynamicTypeProvider : 
    PublishingPointDynamicTypeProviderBase,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    protected internal override void EnsureDynamicTypesResolution()
    {
      base.EnsureDynamicTypesResolution();
      this.GetContext();
    }

    /// <summary>Gets the type of the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    protected virtual Type GetDataItemType(string itemType)
    {
      if (itemType == null)
        throw new ArgumentNullException(nameof (itemType), "Data item type is not defined");
      this.EnsureDynamicTypesResolution();
      Type c = TypeResolutionService.ResolveType(itemType);
      return typeof (DynamicTypeBase).IsAssignableFrom(c) ? c : throw new InvalidOperationException("The type {0} must be assignable from DynamicTypeBase ".Arrange((object) itemType));
    }

    /// <summary>Creates the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public override object CreateDataItem(string itemType) => this.CreateDataItem(itemType, this.GetNewGuid());

    /// <summary>Creates the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateDataItem(string itemType, Guid id)
    {
      Type dataItemType = this.GetDataItemType(itemType);
      IPersistentTypeDescriptor persistentTypeDescriptor = this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(dataItemType);
      if (persistentTypeDescriptor == null)
        throw new ArgumentException("publishingPoint.StorageTypeName is not a valid artifitial type.");
      if (!(persistentTypeDescriptor.CreateInstance((object) id) is DynamicTypeBase instance))
        throw new InvalidOperationException("Publishing point's storage type does not inherit from DynamicTypeBase.");
      this.SetupDataItemForCreation((IDataItem) instance);
      return (object) instance;
    }

    /// <summary>Deletes the data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="dataItemID">The data item ID.</param>
    public override void DeleteDataItem(string itemType, Guid dataItemID)
    {
      object dataItem = this.GetDataItem(itemType, dataItemID);
      this.GetContext().Remove(dataItem);
    }

    /// <summary>Deletes the data item.</summary>
    /// <param name="item">The item.</param>
    public override void DeleteDataItem(object item)
    {
      this.GetDataItemType(item.GetType().FullName);
      this.DeleteDataItem(item.GetType().FullName, ((DynamicTypeBase) item).Id);
    }

    /// <summary>Gets the data item.</summary>
    /// <param name="itemTpye">The item tpye.</param>
    /// <param name="dataItemID">The data item ID.</param>
    /// <returns></returns>
    public override object GetDataItem(string itemTpye, Guid dataItemID)
    {
      Type dataItemType = this.GetDataItemType(itemTpye);
      object objectById = this.GetContext().GetObjectById(Database.OID.ParseObjectId(dataItemType, dataItemID.ToString()));
      return typeof (DynamicTypeBase).IsAssignableFrom(objectById.GetType()) ? (object) (DynamicTypeBase) objectById : throw new InvalidOperationException("Publishing point's storage type does not inherit from DynamicTypeBase.");
    }

    /// <summary>Gets the data items internal.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    protected virtual IQueryable GetDataItemsInternal(string itemType)
    {
      Type dataItemType = this.GetDataItemType(itemType);
      IPersistentTypeDescriptor persistentTypeDescriptor = this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(dataItemType);
      if (persistentTypeDescriptor == null)
        return (IQueryable) ((IEnumerable<object>) new object[0]).AsQueryable<object>();
      return SitefinityQuery.Get(persistentTypeDescriptor.DescribedType, (DataProviderBase) this).Where("ApplicationName = @0", (object) this.ApplicationName);
    }

    /// <summary>Gets the data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <returns></returns>
    public override IQueryable GetDataItems(string itemType) => this.GetDataItemsInternal(itemType);

    /// <summary>Gets the data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="originalItemId">The original item id.</param>
    /// <returns></returns>
    public override IQueryable GetDataItems(string itemType, Guid originalItemId) => this.GetDataItemsInternal(itemType).Where("OriginalItemId = @0", (object) originalItemId);

    /// <summary>Gets the children data items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="parentId">The parent id.</param>
    /// <returns></returns>
    public override IQueryable GetChildrenDataItems(string itemType, Guid parentId) => this.GetDataItemsInternal(itemType).Where("OriginalParentId = @0", (object) parentId);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new DynamicBaseMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>
    /// Sets up IDataItem.{ApplicationName, Provider and Transaction} and adds it to the scope
    /// </summary>
    /// <param name="item">Data item to set up</param>
    protected virtual void SetupDataItemForCreation(IDataItem item)
    {
      item.ApplicationName = this.ApplicationName;
      item.Provider = (object) this;
      item.Transaction = (object) this.GetContext();
      this.GetContext().Add((object) item);
    }
  }
}
