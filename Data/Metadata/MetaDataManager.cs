// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Metadata.MetadataManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Metadata
{
  /// <summary>
  /// Represents an intermediary between metadata objects and persisted data.
  /// </summary>
  public class MetadataManager : 
    ManagerBase<MetaDataProvider>,
    IMetadataManager,
    IManager,
    IDisposable,
    IProviderResolver
  {
    internal const string useAutoGeneratedTableNameAttributeName = "useAutoGeneratedTableName";
    internal const string mainPropertyNameAttributeName = "mainPropertyName";
    internal const string metaTypeFlagsAttributeName = "metaTypeFlags";
    internal const string moduleNameAttributeName = "moduleName";
    internal const string emptyMappingName = "#NA";

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Data.Metadata.MetadataManager" /> class with the default provider.
    /// </summary>
    public MetadataManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of <see cref="T:Telerik.Sitefinity.Data.Metadata.MetadataManager" /> class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set.
    /// </param>
    public MetadataManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Metadata.MetadataManager" /> class.
    /// </summary>
    /// <param name="providerName">
    /// The name of the provider. If empty string or null the default provider is set
    /// </param>
    /// <param name="transactionName">
    /// The name of a distributed transaction. If empty string or null this manager will use separate transaction.
    /// </param>
    public MetadataManager(string providerName, string transactionName)
      : base(providerName, transactionName)
    {
    }

    public ModuleVersion CreateModuleVersion(string moduleName) => this.Provider.CreateModuleVersion(moduleName);

    public ModuleVersion CreateModuleVersion(ModuleSettings settings) => this.Provider.CreateModuleVersion(settings);

    public virtual ModuleVersion GetModuleVersion(string moduleName) => this.Provider.GetModuleVersion(moduleName);

    public virtual IQueryable<ModuleVersion> GetModuleVersions() => this.Provider.GetModuleVersions();

    public virtual void DeleteModuleVersion(ModuleVersion moduleVersion) => this.Provider.DeleteModuleVersion(moduleVersion);

    public SchemaVersion CreateSchemaVersion(string moduleName, string connectionId) => this.Provider.CreateSchemaVersion(moduleName, connectionId);

    public virtual IQueryable<SchemaVersion> GetSchemaVersions() => this.Provider.GetSchemaVersions();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType CreateMetaType(Type type) => this.Provider.CreateMetaType(type);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type and sets the id of the type.
    /// </summary>
    /// <param name="namespaceParam">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public virtual MetaType CreateMetaType(
      string namespaceParam,
      string className,
      Guid id)
    {
      return this.Provider.CreateMetaType(namespaceParam, className, id);
    }

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing description of the specified type and sets the id of the type.
    /// </summary>
    /// <param name="type">The type that will be described.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> description.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public virtual MetaType CreateMetaType(Type type, Guid id) => this.Provider.CreateMetaType(type, id);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object for storing descriptions for specified type.
    /// </summary>
    /// <param name="namespaceParam">The name space for the described type.</param>
    /// <param name="className">The name of the described type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType CreateMetaType(string namespaceParam, string className) => this.Provider.CreateMetaType(namespaceParam, className);

    public virtual MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className)
    {
      return this.Provider.CreateMetaType(assemblyName, nameSpace, className);
    }

    public virtual MetaType CreateMetaType(
      string assemblyName,
      string nameSpace,
      string className,
      Guid id)
    {
      return this.Provider.CreateMetaType(assemblyName, nameSpace, className, id);
    }

    /// <summary>Gets a type description by ID.</summary>
    /// <param name="id">The id.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType GetMetaType(Guid id) => this.Provider.GetMetaType(id);

    /// <summary>
    /// Gets an array of type descriptions for the specified type and its inherited types.
    /// </summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType[] GetMetaTypes(Type type) => this.Provider.GetMetaTypes(type);

    /// <summary>
    /// Gets an array of type descriptions for the specified object instance and its inherited types.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType[] GetMetaTypes(object instance) => this.Provider.GetMetaTypes(instance);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="namespaceParm">The name space.</param>
    /// <param name="className">The class name.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public virtual MetaType GetMetaType(string namespaceParm, string className) => this.Provider.GetMetaType(namespaceParm, className);

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="type">The CLR type.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public virtual MetaType GetMetaType(Type type) => this.Provider.GetMetaType(type);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    /// <returns></returns>
    public virtual IQueryable<MetaType> GetMetaTypes() => this.Provider.GetMetaTypes();

    /// <summary>Deletes the specified meta type.</summary>
    /// <param name="metaType">Type of the meta.</param>
    public virtual void Delete(MetaType metaType) => this.Provider.Delete(metaType);

    /// <summary>Gets a query for metafield.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> objects.</returns>
    public virtual IQueryable<MetaField> GetMetafields() => this.Provider.GetMetafields();

    /// <summary>
    /// Creates new metadata field for the specified type (class).
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public virtual MetaField CreateMetafield(string fieldName) => MetaDataExtensions.ValidateMetaFieldName(fieldName) ? this.Provider.CreateMetafield(fieldName) : throw new ArgumentException("Invalid field name: " + fieldName);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public virtual MetaField CreateMetafield(string fieldName, Guid id)
    {
      if (!MetaDataExtensions.ValidateMetaFieldName(fieldName))
        throw new ArgumentException("Invalid field name: " + fieldName);
      return this.Provider.CreateMetafield(fieldName, id);
    }

    /// <summary>Gets the metafield with the specified ID.</summary>
    /// <param name="id">The id.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public virtual MetaField GetMetafield(Guid id) => this.Provider.GetMetafield(id);

    /// <summary>Deletes the specified metafield.</summary>
    /// <param name="metafield">The metafield.</param>
    public virtual void Delete(MetaField metafield) => this.Provider.Delete(metafield);

    /// <summary>Creates new index for the meta field.</summary>
    /// <param name="indexName">Name of the index.</param>
    /// <returns></returns>
    public virtual MetaIndex CreateMetaIndex(string indexName) => this.Provider.CreateMetaIndex(indexName);

    /// <summary>
    /// Gets the CLR type from the given namespace and className.
    /// </summary>
    /// <param name="nameSpace">The namespace of the type.</param>
    /// <param name="className">Name of the class.</param>
    /// <returns></returns>
    public Type GetClrTypeFor(string nameSpace, string className) => this.Provider.GetClrTypeFor(nameSpace, className);

    /// <summary>Copies the metatype.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    internal void CopyMetaType(MetaType source, MetaType target)
    {
      target.AssemblyName = source.AssemblyName;
      target.BaseClassName = source.BaseClassName;
      target.ClassName = source.ClassName;
      target.DatabaseInheritance = source.DatabaseInheritance;
      target.IsDeleted = source.IsDeleted;
      target.ParentTypeId = source.ParentTypeId;
      target.IsDynamic = source.IsDynamic;
      target.ModuleName = source.ModuleName;
      target.Namespace = source.Namespace;
      target.SectionCaptionsResourceType = source.SectionCaptionsResourceType;
      target.SectionNames = source.SectionNames;
    }

    /// <summary>Copies the meta type attributes.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    internal void CopyMetaTypeAttributes(MetaType source, MetaType target)
    {
      List<MetaTypeAttribute> list1 = source.MetaAttributes.Where<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => !target.MetaAttributes.Any<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (ta => ta.Name == a.Name)))).ToList<MetaTypeAttribute>();
      List<MetaTypeAttribute> list2 = source.MetaAttributes.Where<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => target.MetaAttributes.Any<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (ta => ta.Name == a.Name)))).ToList<MetaTypeAttribute>();
      List<MetaTypeAttribute> list3 = target.MetaAttributes.Where<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (ta => !source.MetaAttributes.Any<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == ta.Name)))).ToList<MetaTypeAttribute>();
      foreach (MetaTypeAttribute metaTypeAttribute in list1)
        target.MetaAttributes.Add(metaTypeAttribute);
      foreach (MetaTypeAttribute metaTypeAttribute in list2)
      {
        MetaTypeAttribute attribute = metaTypeAttribute;
        target.MetaAttributes.Single<MetaTypeAttribute>((Func<MetaTypeAttribute, bool>) (a => a.Name == attribute.Name)).Value = attribute.Value;
      }
      foreach (MetaTypeAttribute metaTypeAttribute in list3)
        target.MetaAttributes.Remove(metaTypeAttribute);
    }

    /// <summary>Copies the metafield.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    public static void CopyMetafield(IMetaField source, MetaField target)
    {
      target.ApplicationName = source.ApplicationName;
      target.ClrType = source.ClrType;
      target.ColumnName = source.ColumnName;
      target.DBLength = source.DBLength;
      target.DBScale = source.DBScale;
      target.DBSqlType = source.DBSqlType;
      target.DBType = source.DBType;
      target.DefaultFetchGroup = source.DefaultFetchGroup;
      target.DefaultValue = source.DefaultValue;
      target.DefaultValueExpression = source.DefaultValueExpression;
      target.Description = source.Description;
      target.DisplayFormat = source.DisplayFormat;
      target.FieldName = source.FieldName;
      target.Hidden = source.Hidden;
      target.Index = source.Index;
      target.IsInternal = source.IsInternal;
      target.IsSingleTaxon = source.IsSingleTaxon;
      target.IsTwoWayContentLink = source.IsTwoWayContentLink;
      target.LinkedContentProvider = source.LinkedContentProvider;
      target.MaxLength = source.MaxLength;
      target.MaxValue = source.MaxValue;
      target.MinValue = source.MinValue;
      target.PositionInSection = source.PositionInSection;
      target.RegularExpression = source.RegularExpression;
      target.Required = source.Required;
      target.SectionName = source.SectionName;
      target.SitefinityType = source.SitefinityType;
      target.StorageType = source.StorageType;
      target.TaxonomyId = source.TaxonomyId;
      target.TaxonomyProvider = source.TaxonomyProvider;
      target.UIHint = source.UIHint;
      target.Validation = source.Validation;
      target.AllowMultipleRelations = source.AllowMultipleRelations;
      target.IsProtectedRelation = source.IsProtectedRelation;
      target.ChoiceFieldDefinition = source.ChoiceFieldDefinition;
      target.IsLocalizable = source.IsLocalizable;
    }

    /// <summary>Copies the meta type attributes.</summary>
    /// <param name="source">The source.</param>
    /// <param name="target">The target.</param>
    internal void CopyMetaFieldAttributes(MetaField source, MetaField target)
    {
      List<MetaFieldAttribute> list1 = source.MetaAttributes.Where<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => !target.MetaAttributes.Any<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (ta => ta.Name == a.Name)))).ToList<MetaFieldAttribute>();
      List<MetaFieldAttribute> list2 = source.MetaAttributes.Where<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => target.MetaAttributes.Any<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (ta => ta.Name == a.Name)))).ToList<MetaFieldAttribute>();
      List<MetaFieldAttribute> list3 = target.MetaAttributes.Where<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (ta => !source.MetaAttributes.Any<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == ta.Name)))).ToList<MetaFieldAttribute>();
      foreach (MetaFieldAttribute metaFieldAttribute in list1)
        target.MetaAttributes.Add(metaFieldAttribute);
      foreach (MetaFieldAttribute metaFieldAttribute in list2)
      {
        MetaFieldAttribute attribute = metaFieldAttribute;
        target.MetaAttributes.Single<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name == attribute.Name)).Value = attribute.Value;
      }
      foreach (MetaFieldAttribute metaFieldAttribute in list3)
        target.MetaAttributes.Remove(metaFieldAttribute);
    }

    /// <summary>
    /// Creates a new metatype description for the specified type (class).
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object for storing data field description.</returns>
    public MetaTypeDescription CreateMetaTypeDescription(Guid metaTypeId) => this.Provider.CreateMetaTypeDescription(metaTypeId);

    /// <summary>
    /// Creates new metadata field for the specified type (class) and set the ID for the field.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type.</param>
    /// <param name="id">The id for the new <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> description.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object for storing data field description.</returns>
    public MetaTypeDescription CreateMetaTypeDescription(
      Guid metaTypeId,
      Guid id)
    {
      return this.Provider.CreateMetaTypeDescription(metaTypeId, id);
    }

    /// <summary>Gets the a meta type description by ID.</summary>
    /// <param name="id">The id of the type description.</param>
    /// <returns></returns>
    public MetaTypeDescription GetMetaTypeDescription(Guid id) => this.Provider.GetMetaTypeDescription(id);

    /// <summary>Gets the a meta type description by dynamic type.</summary>
    /// <param name="dynamicType">The dynamic type to get description for.</param>
    /// <returns></returns>
    public MetaTypeDescription GetMetaTypeDescription(Type dynamicType) => this.GetMetaTypeDescriptionForMetaType(this.GetMetaType(dynamicType).Id);

    /// <summary>Gets the a meta type description by ID.</summary>
    /// <param name="id">The id of the type description.</param>
    /// <returns></returns>
    public MetaTypeDescription GetMetaTypeDescriptionForMetaType(Guid metaTypeId) => this.Provider.GetMetaTypeDescriptions().Where<MetaTypeDescription>((Expression<Func<MetaTypeDescription, bool>>) (mtd => mtd.MetaTypeId == metaTypeId)).SingleOrDefault<MetaTypeDescription>();

    /// <summary>Gets a query for meta type descriptions.</summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> objects.</returns>
    public IQueryable<MetaTypeDescription> GetMetaTypeDescriptions() => this.Provider.GetMetaTypeDescriptions();

    /// <summary>Deletes the specified meta type description.</summary>
    /// <param name="descr">The type description to delete.</param>
    public void Delete(MetaTypeDescription descr) => this.Provider.Delete(descr);

    /// <summary>
    /// Gets the name of the default provider for this manager.
    /// </summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Config.Get<MetadataConfig>(true).DefaultProvider);

    /// <summary>
    /// Gets the name of the module to which this manager belongs.
    /// </summary>
    public override string ModuleName => string.Empty;

    /// <summary>Gets all provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<MetadataConfig>().Providers;

    /// <summary>
    /// Saves any changes made to objects retrieved with this manager.
    /// </summary>
    /// <param name="updateSchema">if set to <c>true</c> [update schema].</param>
    [Obsolete("Use SaveChanges method without parameter instead, as the parameter 'updateSchema' is not relevant - the database  schema is always upgraded")]
    public virtual void SaveChanges(bool updateSchema) => this.SaveChanges();

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static MetadataManager GetManager() => ManagerBase<MetaDataProvider>.GetManager<MetadataManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static MetadataManager GetManager(string providerName) => ManagerBase<MetaDataProvider>.GetManager<MetadataManager>(providerName);

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <typeparam name="T">The type of the manager.</typeparam>
    /// <param name="providerName">The name of the data provider.</param>
    /// <param name="transactionName">Name of a named global transaction.</param>
    /// <returns>The manager instance.</returns>
    public static MetadataManager GetManager(
      string providerName,
      string transactionName)
    {
      return ManagerBase<MetaDataProvider>.GetManager<MetadataManager>(providerName, transactionName);
    }

    /// <summary>Creates new metadata mapping.</summary>
    internal MetadataMapping CreateMetadataMapping() => this.Provider.CreateMetadataMapping();

    /// <summary>Deletes the specified metadata mapping.</summary>
    /// <param name="metadataMapping">The metadata mapping.</param>
    internal virtual void Delete(MetadataMapping metadataMapping) => this.Provider.Delete(metadataMapping);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetadataMapping" />.
    /// </summary>
    /// <returns></returns>
    internal virtual IQueryable<MetadataMapping> GetMetadataMappings() => this.Provider.GetMetadataMappings();

    /// <summary>Gets a metadata mapping</summary>
    /// <param name="moduleName">Name of the module.</param>
    /// <param name="typeName">Name of the type.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetadataMapping" /> object that stores mapping information.</returns>
    internal virtual MetadataMapping GetMetadataMapping(
      string moduleName,
      string typeName,
      string fieldName)
    {
      return this.Provider.GetMetadataMapping(moduleName, typeName, fieldName);
    }

    internal static bool IsOpenAccessProvider
    {
      get
      {
        MetadataConfig metadataConfig = Config.Get<MetadataConfig>();
        return typeof (OpenAccessMetaDataProvider).IsAssignableFrom(metadataConfig.Providers[metadataConfig.DefaultProvider].ProviderType);
      }
    }

    internal static void SetMetaFieldFlags(MetaField metaField, MetaFieldFlags flags)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name.Equals("metaTypeFlags")));
      if (metaFieldAttribute1 == null)
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = "metaTypeFlags";
        metaFieldAttribute1 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute1);
      }
      MetaFieldFlags result = MetaFieldFlags.None;
      if (!metaFieldAttribute1.Value.IsNullOrEmpty())
        Enum.TryParse<MetaFieldFlags>(metaFieldAttribute1.Value, out result);
      metaFieldAttribute1.Value = ((int) (result | flags)).ToString();
    }

    internal static MetaFieldFlags GetMetaFieldFlags(MetaField metaField)
    {
      MetaFieldAttribute metaFieldAttribute1 = metaField.MetaAttributes.FirstOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (a => a.Name.Equals("metaTypeFlags")));
      if (metaFieldAttribute1 == null)
      {
        MetaFieldAttribute metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = "metaTypeFlags";
        metaFieldAttribute1 = metaFieldAttribute2;
        metaField.MetaAttributes.Add(metaFieldAttribute1);
      }
      MetaFieldFlags result = MetaFieldFlags.None;
      if (!metaFieldAttribute1.Value.IsNullOrEmpty())
        Enum.TryParse<MetaFieldFlags>(metaFieldAttribute1.Value, out result);
      return result;
    }
  }
}
