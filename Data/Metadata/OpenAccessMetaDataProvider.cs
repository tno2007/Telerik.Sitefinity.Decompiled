// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.OpenAccessMetaDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Represents a data provider for storing object descriptions in database using OpenAccess.
  /// </summary>
  public class OpenAccessMetaDataProvider : 
    MetaDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates the module version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns></returns>
    public override ModuleVersion CreateModuleVersion(string moduleName)
    {
      ModuleVersion entity = new ModuleVersion(moduleName);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Creates the module version.</summary>
    /// <param name="settings">The settings.</param>
    /// <returns></returns>
    public override ModuleVersion CreateModuleVersion(ModuleSettings settings)
    {
      ModuleVersion entity = new ModuleVersion(settings.Name);
      if (settings.Version != (Version) null)
        entity.Version = settings.Version.CloneVersion();
      entity.ErrorMessage = settings.ErrorMessage;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the module version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <returns></returns>
    public override ModuleVersion GetModuleVersion(string moduleName) => SitefinityQuery.Get<ModuleVersion>((DataProviderBase) this).SingleOrDefault<ModuleVersion>((Expression<Func<ModuleVersion, bool>>) (m => m.ModuleName == moduleName));

    /// <summary>Gets the module versions.</summary>
    /// <returns>Module versions.</returns>
    public override IQueryable<ModuleVersion> GetModuleVersions() => SitefinityQuery.Get<ModuleVersion>((DataProviderBase) this);

    /// <summary>Deletes the module version.</summary>
    /// <param name="moduleVersion">The module version.</param>
    public override void DeleteModuleVersion(ModuleVersion moduleVersion) => this.GetContext().Delete((object) moduleVersion);

    /// <summary>Creates the schema version.</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="connectionId">The connection id.</param>
    /// <returns></returns>
    public override SchemaVersion CreateSchemaVersion(
      string moduleName,
      string connectionId)
    {
      SchemaVersion entity = new SchemaVersion(this.ApplicationName, moduleName, connectionId);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the schema versions.</summary>
    /// <returns></returns>
    public override IQueryable<SchemaVersion> GetSchemaVersions()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<SchemaVersion>((DataProviderBase) this).Where<SchemaVersion>((Expression<Func<SchemaVersion, bool>>) (s => s.ApplicationName == appName));
    }

    /// <inheritdoc />
    public override void Delete(SchemaVersion schemaVersion) => this.GetContext().Delete((object) schemaVersion);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(Type type) => this.CreateMetaType(type, this.GetNewGuid());

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(
      string nameSpace,
      string className)
    {
      return this.CreateMetaType(nameSpace, className, this.GetNewGuid());
    }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(
      Type type,
      Guid id)
    {
      return this.CreateMetaType(type.Assembly.GetName().Name, type.Namespace, type.Name, id);
    }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type and sets the pageId of the type.
    /// </summary>
    /// <param name="nameSpace">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(
      string nameSpace,
      string className,
      Guid id)
    {
      Telerik.Sitefinity.Metadata.Model.MetaType entity = new Telerik.Sitefinity.Metadata.Model.MetaType(this.ApplicationName, id);
      entity.Namespace = nameSpace;
      entity.ClassName = className;
      this.GetContext().Add((object) entity);
      return entity;
    }

    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className)
    {
      Telerik.Sitefinity.Metadata.Model.MetaType metaType = this.CreateMetaType(nameSpace, className);
      metaType.AssemblyName = assemblyName;
      return metaType;
    }

    public override Telerik.Sitefinity.Metadata.Model.MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className,
      Guid id)
    {
      Telerik.Sitefinity.Metadata.Model.MetaType metaType = this.CreateMetaType(nameSpace, className, id);
      metaType.AssemblyName = assemblyName;
      return metaType;
    }

    /// <summary>Gets a type description by ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType GetMetaType(Guid id) => !(id == Guid.Empty) ? this.GetContext().GetItemById<Telerik.Sitefinity.Metadata.Model.MetaType>(id.ToString()) : throw new ArgumentNullException("Id cannot be an Empty Guid");

    /// <summary>
    /// Gets an array of type descriptions for the specified type and its base types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>
    /// An array of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> objects that store type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType[] GetMetaTypes(Type type) => !(type == (Type) null) ? MetaDataProvider.GetMetaTypes(type, new Func<string, string, Telerik.Sitefinity.Metadata.Model.MetaType>(((MetaDataProvider) this).GetMetaType)) : throw new ArgumentNullException(nameof (type));

    /// <summary>
    /// Gets an array of type descriptions for the specified object instance and its inherited types.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType[] GetMetaTypes(
      object instance)
    {
      return instance != null ? this.GetMetaTypes(instance.GetType()) : throw new ArgumentNullException(nameof (instance));
    }

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="type">The CLR type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType GetMetaType(Type type)
    {
      if (type == (Type) null)
        throw new ArgumentException("type cannot be null");
      return this.GetMetaType(type.Namespace, type.Name);
    }

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="nameSpace">The name space.</param>
    /// <param name="className">The class name.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public override Telerik.Sitefinity.Metadata.Model.MetaType GetMetaType(
      string nameSpace,
      string className)
    {
      if (string.IsNullOrEmpty(nameSpace))
        throw new ArgumentException("nameSpace cannot be null or empty string");
      if (string.IsNullOrEmpty(className))
        throw new ArgumentException("className cannot be null or empty string");
      SitefinityOAContext context = this.GetContext();
      Telerik.Sitefinity.Metadata.Model.MetaType metaType1 = context.GetChanges().GetInserts<Telerik.Sitefinity.Metadata.Model.MetaType>().FirstOrDefault<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (x => x.Namespace == nameSpace && x.ClassName == className));
      if (metaType1 != null)
        return metaType1;
      Telerik.Sitefinity.Metadata.Model.MetaType metaType2 = context.GetChanges().GetUpdates<Telerik.Sitefinity.Metadata.Model.MetaType>().FirstOrDefault<Telerik.Sitefinity.Metadata.Model.MetaType>((Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>) (x => x.Namespace == nameSpace && x.ClassName == className));
      if (metaType2 != null)
        return metaType2;
      string appName = this.ApplicationName;
      return context.GetAll<Telerik.Sitefinity.Metadata.Model.MetaType>().FirstOrDefault<Telerik.Sitefinity.Metadata.Model.MetaType>((Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (x => x.Namespace == nameSpace && x.ClassName == className && x.ApplicationName == appName));
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    /// <returns></returns>
    public override IQueryable<Telerik.Sitefinity.Metadata.Model.MetaType> GetMetaTypes()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Telerik.Sitefinity.Metadata.Model.MetaType>((DataProviderBase) this).Where<Telerik.Sitefinity.Metadata.Model.MetaType>((Expression<Func<Telerik.Sitefinity.Metadata.Model.MetaType, bool>>) (m => m.ApplicationName == appName));
    }

    /// <summary>
    /// Gets the CLR type from the given namespace and className.
    /// </summary>
    /// <param name="nameSpace">The namespace of the type.</param>
    /// <param name="className">Name of the class.</param>
    /// <returns></returns>
    public override Type GetClrTypeFor(string nameSpace, string className) => (this.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(string.Format("{0}.{1}", (object) nameSpace, (object) className)) ?? throw new ArgumentException(string.Format("There is no persitent type with the following name: {0}.{1}", (object) nameSpace, (object) className))).DescribedType;

    /// <summary>Deletes the specified meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    public override void Delete(Telerik.Sitefinity.Metadata.Model.MetaType metaType) => this.GetContext().Remove((object) metaType);

    /// <summary>Gets a query for metafield.</summary>
    /// <returns>
    /// A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> objects.
    /// </returns>
    public override IQueryable<MetaField> GetMetafields()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MetaField>((DataProviderBase) this).Where<MetaField>((Expression<Func<MetaField, bool>>) (m => m.ApplicationName == appName));
    }

    /// <summary>
    /// Creates new metadata field for the specified type (class).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns></returns>
    public override MetaField CreateMetafield(string fieldName) => this.CreateMetafield(fieldName, this.GetNewGuid());

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="pageId">The pageId for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.
    /// </returns>
    public override MetaField CreateMetafield(string fieldName, Guid id)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      MetaField entity = new MetaField(this.ApplicationName, id);
      entity.FieldName = fieldName;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Creates new index for the meta field.</summary>
    /// <param name="indexName">Name of the index.</param>
    /// <returns></returns>
    public override MetaIndex CreateMetaIndex(string indexName, Guid id) => new MetaIndex(id)
    {
      Name = indexName
    };

    /// <summary>Gets the metafield with the specified ID.</summary>
    /// <param name="pageId">The pageId.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.
    /// </returns>
    public override MetaField GetMetafield(Guid id) => !(id == Guid.Empty) ? this.GetContext().GetItemById<MetaField>(id.ToString()) : throw new ArgumentNullException("Id cannot be an Empty Guid");

    /// <summary>Deletes the specified metafield.</summary>
    /// <param name="metafield">The metafield.</param>
    public override void Delete(MetaField metafield) => this.GetContext().Remove((object) metafield);

    /// <summary>
    /// Creates a new metatype description for the specified type (class).
    /// </summary>
    /// <param name="metaTypeId">Id of the metatype.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object for storing data.</returns>
    public override MetaTypeDescription CreateMetaTypeDescription(Guid metaTypeId) => this.CreateMetaTypeDescription(metaTypeId, this.GetNewGuid());

    /// <summary>
    /// Creates new description for the specified type (class) and set the ID for the new object.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object for storing data field description.</returns>
    public override MetaTypeDescription CreateMetaTypeDescription(
      Guid metaTypeId,
      Guid id)
    {
      if (metaTypeId == Guid.Empty)
        throw new ArgumentNullException("metaTypeName");
      MetaTypeDescription entity = new MetaTypeDescription(this.ApplicationName, id);
      entity.MetaTypeId = metaTypeId;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the a meta type description by ID.</summary>
    /// <param name="id">The id of the type description.</param>
    /// <returns></returns>
    public override MetaTypeDescription GetMetaTypeDescription(Guid id) => !(id == Guid.Empty) ? this.GetContext().GetItemById<MetaTypeDescription>(id.ToString()) : throw new ArgumentNullException("Id cannot be an Empty Guid");

    /// <summary>Gets a query for meta type descriptions.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> objects.</returns>
    public override IQueryable<MetaTypeDescription> GetMetaTypeDescriptions()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MetaTypeDescription>((DataProviderBase) this).Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (m => m.ApplicationName == appName));
    }

    /// <summary>Deletes the specified meta type description.</summary>
    /// <param name="descr">The type description to delete.</param>
    public override void Delete(MetaTypeDescription descr) => this.GetContext().Remove((object) descr);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) null;

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Commits the provided transaction.</summary>
    /// <param name="updateSchema">if set to <c>true</c> [update schema].</param>
    public override void CommitTransaction(bool updateSchema)
    {
      SitefinityOAContext context = this.GetContext();
      if (!context.IsActive)
        return;
      this.DoWithRelatedManagers((Action<IManager>) (m => m.Provider.CommitTransaction()));
      if (this.providerDecorator is OpenAccessDecorator providerDecorator)
      {
        providerDecorator.UpdateInternalDirtyItemsCache(context);
        providerDecorator.UpdateInvalidatedModules(context);
      }
      context.Commit();
      if (providerDecorator != null && providerDecorator.InvalidatedItems.Count > 0)
      {
        if (!this.SuppressNotifications)
          CacheDependency.Notify((IList<CacheDependencyKey>) providerDecorator.InvalidatedItems);
        providerDecorator.InvalidatedItems.Clear();
      }
      if (!updateSchema || providerDecorator == null)
        return;
      providerDecorator.EnsureModelReset();
    }

    internal override bool ShouldValidateOnCommit() => false;

    internal bool EnsureSchemaChanges(
      SitefinityOAContext oaContext,
      out ResetModelReason resetModelReason)
    {
      resetModelReason = new ResetModelReason();
      if (this.TransactionName != null && this.TransactionName.Equals("OAC_Upgrading_0123"))
        return false;
      List<string> pendingTypes = new List<string>();
      List<string> pendingModules = new List<string>();
      foreach (object dirtyItem in (IEnumerable) oaContext.GetDirtyItems())
      {
        if (dirtyItem is Telerik.Sitefinity.Metadata.Model.MetaType metaType)
        {
          this.AddMetaTypeToPendingTypes(metaType, pendingModules, pendingTypes);
          if (!resetModelReason.HasDeletedTypes && metaType.IsDeleted && oaContext.IsFieldDirty((object) metaType, "IsDeleted"))
            resetModelReason.HasDeletedTypes = true;
        }
        if (dirtyItem is MetaField persistentObject && persistentObject.Parent != null)
        {
          this.AddMetaTypeToPendingTypes(persistentObject.Parent, pendingModules, pendingTypes);
          if (persistentObject.IsDeleted && !resetModelReason.HasDeletedFields && oaContext.IsFieldDirty((object) persistentObject, "IsDeleted"))
            resetModelReason.HasDeletedFields = true;
        }
      }
      if (pendingModules.Count > 0)
      {
        foreach (string str in pendingModules)
        {
          string moduleName = str;
          IQueryable<SchemaVersion> schemaVersions = this.GetSchemaVersions();
          Expression<Func<SchemaVersion, bool>> predicate = (Expression<Func<SchemaVersion, bool>>) (s => s.ModuleName == moduleName);
          foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) schemaVersions.Where<SchemaVersion>(predicate))
          {
            schemaVersion.MetaDataChanged = true;
            resetModelReason.AddModule(schemaVersion.ModuleName);
          }
        }
      }
      if (pendingTypes.Count > 0)
      {
        foreach (string str in pendingTypes)
        {
          string typ = str;
          IQueryable<SchemaVersion> schemaVersions = this.GetSchemaVersions();
          Expression<Func<SchemaVersion, bool>> predicate = (Expression<Func<SchemaVersion, bool>>) (s => s.MetaTypes.Contains(typ));
          foreach (SchemaVersion schemaVersion in (IEnumerable<SchemaVersion>) schemaVersions.Where<SchemaVersion>(predicate))
          {
            schemaVersion.MetaDataChanged = true;
            resetModelReason.AddModule(schemaVersion.ModuleName);
          }
        }
      }
      return resetModelReason.HasChanges;
    }

    private void AddMetaTypeToPendingTypes(
      Telerik.Sitefinity.Metadata.Model.MetaType type,
      List<string> pendingModules,
      List<string> pendingTypes)
    {
      if (!string.IsNullOrEmpty(type.ModuleName))
      {
        if (pendingModules.Contains(type.ModuleName))
          return;
        pendingModules.Add(type.ModuleName);
      }
      else
      {
        string str = type.IsDynamic ? type.BaseClassName : type.Namespace + "." + type.ClassName;
        if (pendingTypes.Contains(str))
          return;
        pendingTypes.Add(str);
      }
    }

    /// <inheritdoc />
    public override MetadataMapping CreateMetadataMapping()
    {
      MetadataMapping entity = new MetadataMapping();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    public override void Delete(MetadataMapping metadataMapping) => this.GetContext().Remove((object) metadataMapping);

    /// <inheritdoc />
    public override IQueryable<MetadataMapping> GetMetadataMappings() => SitefinityQuery.Get<MetadataMapping>((DataProviderBase) this);

    /// <inheritdoc />
    public override MetadataMapping GetMetadataMapping(
      string moduleName,
      string typeName,
      string fieldName)
    {
      return this.GetContext().GetAll<MetadataMapping>().FirstOrDefault<MetadataMapping>((Expression<Func<MetadataMapping, bool>>) (m => m.ModuleName == moduleName && m.TypeName == typeName && m.FieldName == fieldName));
    }
  }
}
