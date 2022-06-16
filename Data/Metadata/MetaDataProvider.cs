// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.MetaDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Represents a data provider for storing object descriptions.
  /// </summary>
  public abstract class MetaDataProvider : DataProviderBase
  {
    private Type[] acceptedTypes = new Type[2]
    {
      typeof (MetaType),
      typeof (MetaField)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (MetaDataProvider);

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>Creates the module version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns></returns>
    public abstract ModuleVersion CreateModuleVersion(string moduleName);

    /// <summary>Creates the module version.</summary>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    public abstract ModuleVersion CreateModuleVersion(ModuleSettings settings);

    /// <summary>Gets the module version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns>Module version object</returns>
    public abstract ModuleVersion GetModuleVersion(string moduleName);

    /// <summary>Gets the module versions.</summary>
    /// <returns>Module versions.</returns>
    public abstract IQueryable<ModuleVersion> GetModuleVersions();

    /// <summary>Deletes the module version.</summary>
    /// <param name="moduleVersion">The module version.</param>
    public abstract void DeleteModuleVersion(ModuleVersion moduleVersion);

    /// <summary>Creates the schema version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="connectionId">The connection id.</param>
    /// <returns></returns>
    public abstract SchemaVersion CreateSchemaVersion(
      string moduleName,
      string connectionId);

    /// <summary>Gets the schema versions.</summary>
    /// <returns></returns>
    public abstract IQueryable<SchemaVersion> GetSchemaVersions();

    /// <summary>Deletes the specified schema version.</summary>
    /// <param name="schemaVersion">The schema version.</param>
    public abstract void Delete(SchemaVersion schemaVersion);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType CreateMetaType(Type type);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public abstract MetaType CreateMetaType(string nameSpace, string className, Guid id);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public abstract MetaType CreateMetaType(Type type, Guid id);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType CreateMetaType(string nameSpace, string className);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="assemblyName">Name of the assembly the class resides.</param>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public abstract MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="assemblyName">Name of the assembly the class resides.</param>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <param name="id">The desired unique id of the metatype.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public abstract MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className,
      Guid id);

    /// <summary>Gets a type description by ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType GetMetaType(Guid id);

    /// <summary>
    /// Gets an array of type descriptions for the specified type and its inherited types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType[] GetMetaTypes(Type type);

    /// <summary>
    /// Gets an array of type descriptions for the specified object instance and its inherited types.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType[] GetMetaTypes(object instance);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="nameSpace">The name space.</param>
    /// <param name="className">The class name.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public abstract MetaType GetMetaType(string nameSpace, string className);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="type">The CLR type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public abstract MetaType GetMetaType(Type type);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    /// <returns></returns>
    public abstract IQueryable<MetaType> GetMetaTypes();

    /// <summary>Deletes the specified meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    public abstract void Delete(MetaType metaType);

    /// <summary>Gets a query for metafield.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> objects.</returns>
    public abstract IQueryable<MetaField> GetMetafields();

    /// <summary>
    /// Creates new metadata field for the specified type (class).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public abstract MetaField CreateMetafield(string fieldName);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public abstract MetaField CreateMetafield(string fieldName, Guid id);

    /// <summary>Creates new index for the meta field.</summary>
    /// <param name="indexName">Name of the index.</param>
    /// <returns></returns>
    public virtual MetaIndex CreateMetaIndex(string indexName) => this.CreateMetaIndex(indexName, this.GetNewGuid());

    /// <summary>Creates the index of the meta.</summary>
    /// <param name="indexName">Name of the index.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public abstract MetaIndex CreateMetaIndex(string indexName, Guid id);

    /// <summary>Gets the metafield with the specified ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public abstract MetaField GetMetafield(Guid id);

    /// <summary>Deletes the specified metafield.</summary>
    /// <param name="metafield">The metafield.</param>
    public abstract void Delete(MetaField metafield);

    /// <summary>Commits the provided transaction.</summary>
    /// <param name="updateSchema">if set to <c>true</c> [update schema].</param>
    public abstract void CommitTransaction(bool updateSchema);

    /// <summary>Commits the provided transaction.</summary>
    public override void CommitTransaction() => this.CommitTransaction(true);

    /// <summary>
    /// Creates a new metatype description for the specified type (class).
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public abstract MetaTypeDescription CreateMetaTypeDescription(Guid metaTypeId);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object for storing data field description.</returns>
    public abstract MetaTypeDescription CreateMetaTypeDescription(
      Guid metaTypeId,
      Guid id);

    /// <summary>Gets the a meta type description by ID.</summary>
    /// <param name="id">The id of the type description.</param>
    /// <returns></returns>
    public abstract MetaTypeDescription GetMetaTypeDescription(Guid id);

    /// <summary>Gets a query for meta type descriptions.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> objects.</returns>
    public abstract IQueryable<MetaTypeDescription> GetMetaTypeDescriptions();

    /// <summary>Deletes the specified meta type description.</summary>
    /// <param name="descr">The type description to delete.</param>
    public abstract void Delete(MetaTypeDescription descr);

    /// <summary>
    /// Gets the CLR type from the given namespace and className.
    /// </summary>
    /// <param name="nameSpace">The namespace of the type.</param>
    /// <param name="className">Name of the class.</param>
    /// <returns></returns>
    public abstract Type GetClrTypeFor(string nameSpace, string className);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (MetaType))
        return (object) this.GetMetaType(id);
      if (itemType == typeof (MetaField))
        return (object) this.GetMetafield(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.acceptedTypes);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == typeof (MetaType))
        return (object) this.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (t => t.Id == id)).FirstOrDefault<MetaType>();
      if (!(itemType == typeof (MetaField)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetMetafields().Where<MetaField>((Expression<Func<MetaField, bool>>) (f => f.Id == id)).FirstOrDefault<MetaField>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (MetaType))
        return (IEnumerable) DataProviderBase.SetExpressions<MetaType>(this.GetMetaTypes(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (MetaField))
        return (IEnumerable) DataProviderBase.SetExpressions<MetaField>(this.GetMetafields(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.acceptedTypes);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      switch (item)
      {
        case null:
          throw new ArgumentNullException(nameof (item));
        case MetaType _:
          this.Delete((MetaType) item);
          break;
        case MetaField _:
          this.Delete((MetaField) item);
          break;
      }
      throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.acceptedTypes);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => this.acceptedTypes;

    /// <summary>Creates new metadata mapping.</summary>
    public abstract MetadataMapping CreateMetadataMapping();

    /// <summary>Deletes the specified metadata mapping.</summary>
    /// <param name="metafield">The metadata mapping.</param>
    public abstract void Delete(MetadataMapping metadataMapping);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetadataMapping" />.
    /// </summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetadataMapping" /> objects.</returns>
    public abstract IQueryable<MetadataMapping> GetMetadataMappings();

    /// <summary>Gets a metadata mapping</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetadataMapping" /> object that stores mapping information.</returns>
    public abstract MetadataMapping GetMetadataMapping(
      string moduleName,
      string typeName,
      string fieldName);

    internal static MetaType[] GetMetaTypes(
      Type type,
      Func<string, string, MetaType> getMetaTypeFunc)
    {
      List<MetaType> metaTypeList = new List<MetaType>();
      for (Type type1 = type; type1 != typeof (object); type1 = type1.BaseType)
      {
        MetaType metaType = getMetaTypeFunc(type1.Namespace, type1.Name);
        if (metaType != null)
          metaTypeList.Add(metaType);
      }
      return metaTypeList.ToArray();
    }
  }
}
