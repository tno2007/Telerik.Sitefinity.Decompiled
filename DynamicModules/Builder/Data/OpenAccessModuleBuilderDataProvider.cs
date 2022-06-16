// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.OpenAccessModuleBuilderDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Metadata.Relational;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// Implementation of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.ModuleBuilderDataProvider" /> with Telerik OpenAccess ORM.
  /// </summary>
  public class OpenAccessModuleBuilderDataProvider : 
    ModuleBuilderDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public override DynamicModule CreateDynamicModule() => this.CreateDynamicModule(this.GetNewGuid(), (string) null);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public override DynamicModule CreateDynamicModule(Guid id, string applicationName)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      DynamicModule dynamicModule = new DynamicModule(id, applicationName1);
      dynamicModule.Owner = SecurityManager.GetCurrentUserId();
      this.providerDecorator.CreatePermissionInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) dynamicModule);
      this.GetContext().Add((object) dynamicModule);
      return dynamicModule;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.</returns>
    public override DynamicModule GetDynamicModule(Guid id)
    {
      DynamicModule dynamicModule = !(id == Guid.Empty) ? this.GetContext().GetItemById<DynamicModule>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      dynamicModule.Provider = (object) this;
      return dynamicModule;
    }

    /// <summary>Gets the query of all dynamic modules.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> type.</returns>
    public override IQueryable<DynamicModule> GetDynamicModules() => SitefinityQuery.Get<DynamicModule>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (p => p.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" />.
    /// </summary>
    /// <param name="dynamicModule">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModule" /> to be deleted.</param>
    public override void Delete(DynamicModule dynamicModule)
    {
      if (dynamicModule == null)
        throw new ArgumentNullException(nameof (dynamicModule));
      this.providerDecorator.DeletePermissions((object) dynamicModule);
      this.providerDecorator.DeletePermissionsInheritanceAssociation(this.GetSecurityRoot(), (ISecuredObject) dynamicModule);
      this.GetContext().Remove((object) dynamicModule);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public override DynamicModuleType CreateDynamicModuleType() => this.CreateDynamicModuleType(this.GetNewGuid(), (string) null);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module type ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public override DynamicModuleType CreateDynamicModuleType(
      Guid id,
      string applicationName)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      DynamicModuleType entity = new DynamicModuleType(id, applicationName1);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module type to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.</returns>
    public override DynamicModuleType GetDynamicModuleType(Guid id)
    {
      DynamicModuleType dynamicModuleType = !(id == Guid.Empty) ? this.GetContext().GetItemById<DynamicModuleType>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      dynamicModuleType.Provider = (object) this;
      return dynamicModuleType;
    }

    /// <summary>Gets the query of all dynamic module types.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> type.</returns>
    public override IQueryable<DynamicModuleType> GetDynamicModuleTypes() => SitefinityQuery.Get<DynamicModuleType>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (p => p.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" />.
    /// </summary>
    /// <param name="dynamicModuleType">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> to be deleted.</param>
    public override void Delete(DynamicModuleType dynamicModuleType)
    {
      if (dynamicModuleType == null)
        throw new ArgumentNullException(nameof (dynamicModuleType));
      this.providerDecorator.DeletePermissions((object) dynamicModuleType);
      ((OpenAccessDecorator) this.providerDecorator).DeletePermissionsInheritanceAssociation(dynamicModuleType.ParentModuleId, dynamicModuleType.Id);
      this.GetContext().Remove((object) dynamicModuleType);
    }

    /// <summary>
    /// Sets the parent module of the specified dynamic module type.
    /// </summary>
    /// <param name="dynamicModuleType">Type of the dynamic module.</param>
    /// <param name="parentModule">The parent module.</param>
    internal override void SetParentModule(
      DynamicModuleType dynamicModuleType,
      DynamicModule parentModule)
    {
      dynamicModuleType.ParentModuleId = parentModule.Id;
      dynamicModuleType.ModuleName = parentModule.Name;
      dynamicModuleType.CanInheritPermissions = true;
      dynamicModuleType.InheritsPermissions = true;
      this.providerDecorator.CreatePermissionInheritanceAssociation((ISecuredObject) parentModule, (ISecuredObject) dynamicModuleType);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public override DynamicModuleField CreateDynamicModuleField() => this.CreateDynamicModuleField(this.GetNewGuid(), (string) null);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module field ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public override DynamicModuleField CreateDynamicModuleField(
      Guid id,
      string applicationName)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      DynamicModuleField entity = new DynamicModuleField(id, applicationName1);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module field to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.</returns>
    public override DynamicModuleField GetDynamicModuleField(Guid id)
    {
      DynamicModuleField dynamicModuleField = !(id == Guid.Empty) ? this.GetContext().GetItemById<DynamicModuleField>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      dynamicModuleField.Provider = (object) this;
      return dynamicModuleField;
    }

    /// <summary>Gets the query of all dynamic module fields.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> type.</returns>
    public override IQueryable<DynamicModuleField> GetDynamicModuleFields() => SitefinityQuery.Get<DynamicModuleField>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<DynamicModuleField>((Expression<Func<DynamicModuleField, bool>>) (p => p.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" />.
    /// </summary>
    /// <param name="dynamicModuleField">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleField" /> to be deleted.</param>
    public override void Delete(DynamicModuleField dynamicModuleField)
    {
      if (dynamicModuleField == null)
        throw new ArgumentNullException(nameof (dynamicModuleField));
      this.providerDecorator.DeletePermissions((object) dynamicModuleField);
      this.GetContext().Remove((object) dynamicModuleField);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public override FieldsBackendSection CreateFieldsBackendSection() => this.CreateFieldsBackendSection(this.GetNewGuid(), (string) null);

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the fields backend section to be created.</param>
    /// <param name="applicationName">Application name under which the fields backend section ought to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public override FieldsBackendSection CreateFieldsBackendSection(
      Guid id,
      string applicationName)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      FieldsBackendSection entity = new FieldsBackendSection(id, applicationName1);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the fields backend section to be retrieved.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.</returns>
    public override FieldsBackendSection GetFieldsBackendSection(Guid id)
    {
      FieldsBackendSection fieldsBackendSection = !(id == Guid.Empty) ? this.GetContext().GetItemById<FieldsBackendSection>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      fieldsBackendSection.Provider = (object) this;
      return fieldsBackendSection;
    }

    /// <summary>Gets the query of all field backend sections.</summary>
    /// <returns>An query of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> type.</returns>
    public override IQueryable<FieldsBackendSection> GetFieldsBackendSections() => SitefinityQuery.Get<FieldsBackendSection>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<FieldsBackendSection>((Expression<Func<FieldsBackendSection, bool>>) (p => p.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" />.
    /// </summary>
    /// <param name="fieldsBackendSection">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.FieldsBackendSection" /> to be deleted.</param>
    public override void Delete(FieldsBackendSection fieldsBackendSection)
    {
      if (fieldsBackendSection == null)
        throw new ArgumentNullException(nameof (fieldsBackendSection));
      this.GetContext().Remove((object) fieldsBackendSection);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> with specified id and application name.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be created.</param>
    /// <param name="applicationName">Application name under which the dynamic module ought to be created.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </returns>
    public override DynamicContentProvider CreateDynamicContentProvider(
      Guid id,
      string applicationName,
      ISecuredObject parentSecuredObject)
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      string applicationName1 = applicationName;
      if (string.IsNullOrEmpty(applicationName1))
        applicationName1 = this.ApplicationName;
      DynamicContentProvider dynamicContentProvider = new DynamicContentProvider(id, applicationName1);
      this.SetParentSecuredObject(dynamicContentProvider, parentSecuredObject);
      this.GetContext().Add((object) dynamicContentProvider);
      return dynamicContentProvider;
    }

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="dynamicContentProvider">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> to be deleted.</param>
    public override void DeleteDynamicContentProvider(DynamicContentProvider dynamicContentProvider)
    {
      if (dynamicContentProvider == null)
        throw new ArgumentNullException(nameof (dynamicContentProvider));
      this.GetContext().Remove((object) dynamicContentProvider);
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> by it's id.
    /// </summary>
    /// <param name="id">Id of the dynamic module to be retrieved.</param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </returns>
    public override DynamicContentProvider GetDynamicContentProvider(Guid id)
    {
      DynamicContentProvider dynamicContentProvider = !(id == Guid.Empty) ? this.GetContext().GetItemById<DynamicContentProvider>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      dynamicContentProvider.Provider = (object) this;
      return dynamicContentProvider;
    }

    /// <summary>Gets the list of all dynamic modules.</summary>
    /// <returns>
    /// A list of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> type.
    /// </returns>
    public override IQueryable<DynamicContentProvider> GetDynamicContentProviders() => SitefinityQuery.Get<DynamicContentProvider>((DataProviderBase) this, MethodBase.GetCurrentMethod()).Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.ApplicationName == this.ApplicationName));

    /// <summary>
    /// Sets the parent secured object to the specified dynamicContentProvider.
    /// </summary>
    /// <param name="dynamicContentProvider">The dynamic content provider.</param>
    /// <param name="parentSecuredObject">The parent secured object.</param>
    public void SetParentSecuredObject(
      DynamicContentProvider dynamicContentProvider,
      ISecuredObject parentSecuredObject)
    {
      dynamicContentProvider.ParentSecuredObjectId = parentSecuredObject.Id;
      dynamicContentProvider.ParentSecuredObjectType = parentSecuredObject.GetType().FullName;
      if (!(parentSecuredObject is IDynamicSecuredObject))
        return;
      dynamicContentProvider.ParentSecuredObjectTitle = ((IDynamicSecuredObject) parentSecuredObject).GetTitle();
      dynamicContentProvider.ModuleName = ((IDynamicSecuredObject) parentSecuredObject).GetModuleName();
    }

    /// <summary>
    /// Deletes the passed instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" />.
    /// </summary>
    /// <param name="dynamicContentProvider">The instance of <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> to be deleted.</param>
    public override void Delete(DynamicContentProvider dynamicContentProvider)
    {
      if (dynamicContentProvider == null)
        throw new ArgumentNullException(nameof (dynamicContentProvider));
      this.GetContext().Remove((object) dynamicContentProvider);
    }

    /// <inheritdoc />
    public override bool HasDropTablePermissions(DynamicModuleType dynamicModuleType)
    {
      SitefinityOAContext context = this.GetContext();
      if (context.OpenAccessConnection.DbType == DatabaseType.MsSql)
      {
        MetaTable mappedTableName = this.GetMappedTableName(dynamicModuleType.GetFullTypeName());
        if (mappedTableName != null)
          return new OpenAccessTablePermissionValidator(mappedTableName.SchemaName, (OpenAccessContext) context).CanDropTableInCurrentSchema(mappedTableName.Name);
      }
      return base.HasDropTablePermissions(dynamicModuleType);
    }

    /// <inheritdoc />
    public override bool HasAlterTablePermissions(DynamicModuleType dynamicModuleType)
    {
      SitefinityOAContext context = this.GetContext();
      if (context.OpenAccessConnection.DbType == DatabaseType.MsSql)
      {
        MetaTable mappedTableName = this.GetMappedTableName(dynamicModuleType.GetFullTypeName());
        if (mappedTableName != null)
          return new OpenAccessTablePermissionValidator(mappedTableName.SchemaName, (OpenAccessContext) context).CanAlterTable(mappedTableName.Name);
      }
      return base.HasAlterTablePermissions(dynamicModuleType);
    }

    private MetaTable GetMappedTableName(string persistedTypeName) => this.GetContext().Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => pt.FullName == persistedTypeName))?.Table;

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>
    /// Gets the meta data source for the module builder data provider.
    /// </summary>
    /// <returns>An instance of the <see cref="!:MetadataSouce" />.</returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new ModuleBuilderMetadataSource(context);
  }
}
