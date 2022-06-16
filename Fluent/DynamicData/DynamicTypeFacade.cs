// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Fluent.DynamicData
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single dynamic type.
  /// </summary>
  public class DynamicTypeFacade : 
    IItemFacade<DynamicTypeFacade, MetaType>,
    IFacade<DynamicTypeFacade>
  {
    private MetaType dynamicType;
    private AppSettings appSettings;
    private MetadataManager metadataManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public DynamicTypeFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="dynamicTypeId">The page id.</param>
    public DynamicTypeFacade(AppSettings appSettings, Guid dynamicTypeId)
      : this(appSettings)
    {
      if (dynamicTypeId == Guid.Empty)
        throw new ArgumentNullException(nameof (dynamicTypeId));
      this.LoadDynamicType(dynamicTypeId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="page">The dynamic type on which the fluent API functionality ought to be used.</param>
    public DynamicTypeFacade(AppSettings appSettings, MetaType dynamicType)
      : this(appSettings)
    {
      this.dynamicType = dynamicType != null ? dynamicType : throw new ArgumentNullException(nameof (dynamicType));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> class
    /// and loads the dynamic type that was created from an existing type.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="nameSpace">The namespace of the MetaType.</param>
    /// <param name="className">The class name for the MetaType.</param>
    public DynamicTypeFacade(AppSettings appSettings, string nameSpace, string className)
      : this(appSettings)
    {
      if (string.IsNullOrEmpty(nameSpace))
        throw new ArgumentNullException(nameof (nameSpace));
      if (string.IsNullOrEmpty(className))
        throw new ArgumentNullException(nameof (className));
      this.LoadDynamicType(nameSpace, className);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> class
    /// and loads the dynamic type from the specified class name and namespace.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="existingType">
    /// The existing type that contains the dynamic fields and hence has became a
    /// dynamic type.
    /// </param>
    public DynamicTypeFacade(AppSettings appSettings, Type existingType)
      : this(appSettings)
    {
      if (existingType == (Type) null)
        throw new ArgumentNullException(nameof (existingType));
      this.LoadDynamicType(existingType);
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade.MetadataManager" /> to be used by the facade.
    /// </summary>
    public MetadataManager MetadataManager
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = MetadataManager.GetManager((string) null, this.appSettings.TransactionName);
        return this.metadataManager;
      }
    }

    DynamicTypeFacade IItemFacade<DynamicTypeFacade, MetaType>.CreateNew() => throw new NotSupportedException("This method cannot be used with the DynamicTypeFacade. Please use one of other overloads.");

    DynamicTypeFacade IItemFacade<DynamicTypeFacade, MetaType>.CreateNew(
      Guid itemId)
    {
      throw new NotSupportedException("This method cannot be used with the DynamicTypeFacade. Please use one of other overloads.");
    }

    /// <summary>
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> with specified CLR type of
    /// the dynamic type.
    /// </summary>
    /// <param name="type">CLR type of the new dynamic type.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade CreateNew(Type type)
    {
      this.dynamicType = !(type == (Type) null) ? this.MetadataManager.CreateMetaType(type) : throw new ArgumentNullException(nameof (type));
      return this;
    }

    /// <summary>
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> with specified CRL type of the
    /// dynamic type and id of the dynamic type.
    /// </summary>
    /// <param name="type">CLR type of the new dynamic type.</param>
    /// <param name="id">The id of the new dynamic type to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade CreateNew(Type type, Guid id)
    {
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      this.dynamicType = this.MetadataManager.CreateMetaType(type, id);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> with specified class name and
    /// namespace name.
    /// </summary>
    /// <param name="className">Name of the class of the new dynamic type.</param>
    /// <param name="namespaceName">Name of the namespace of the new dynamic type.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade CreateNew(string className, string namespaceName)
    {
      if (string.IsNullOrEmpty(className))
        throw new ArgumentNullException(nameof (className));
      if (string.IsNullOrEmpty(namespaceName))
        throw new ArgumentNullException(nameof (namespaceName));
      this.dynamicType = this.MetadataManager.CreateMetaType(namespaceName, className);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> with specified class name, namespace name
    /// and the id of the newly created dynamic type.
    /// </summary>
    /// <param name="className">Name of the class of the new dynamic type.</param>
    /// <param name="namespaceName">Name of the namespace of the new dynamic type.</param>
    /// <param name="id">The id of the new dynamic type to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade CreateNew(
      string className,
      string namespaceName,
      Guid id)
    {
      if (string.IsNullOrEmpty(className))
        throw new ArgumentNullException(nameof (className));
      if (string.IsNullOrEmpty(namespaceName))
        throw new ArgumentNullException(nameof (namespaceName));
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      this.dynamicType = this.MetadataManager.CreateMetaType(namespaceName, className, id);
      return this;
    }

    /// <summary>
    /// Returns instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object.</returns>
    public MetaType Get() => this.dynamicType;

    /// <summary>
    /// Sets an instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> on currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> object.</returns>
    public DynamicTypeFacade Set(MetaType item)
    {
      this.dynamicType = item != null ? item : throw new ArgumentNullException(nameof (item));
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object.
    /// </summary>
    /// <param name="action">An action to be performed on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> has not been initialized either through Facade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> object.</returns>
    public DynamicTypeFacade Do(Action<MetaType> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      this.EnsureState();
      action(this.dynamicType);
      return this;
    }

    /// <summary>
    /// Provides the access to the child facade for working with a single dynamic field of this dynamic type.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade Field() => new DynamicFieldFacade(this.appSettings, this);

    /// <summary>
    /// Provides the access to the child facade for working with a single dynamic field of this dynamic type
    /// and provides the id of the field to be preloaded.
    /// </summary>
    /// <param name="dynamicFieldId">
    /// Id of the dynamic field that ought to be loaded in the child facade's state.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.
    /// </returns>
    public DynamicFieldFacade Field(Guid dynamicFieldId) => new DynamicFieldFacade(this.appSettings, this, dynamicFieldId);

    /// <summary>
    /// Provides the access to the child facade for working with a single dynamic field of this dynamic type
    /// and provides the instance of the dynamic field to be preloaded.
    /// </summary>
    /// <param name="dynamicField">
    /// An instance of the dynamic field to be loaded in the child facade's state.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.
    /// </returns>
    public DynamicFieldFacade Field(MetaField dynamicField) => new DynamicFieldFacade(this.appSettings, this, dynamicField);

    /// <summary>
    /// Provides the access to the child facade for working with all the dynamic fields of this dynamic type.
    /// </summary>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.
    /// </returns>
    public DynamicFieldsFacade Fields() => new DynamicFieldsFacade(this.appSettings, this);

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade SaveChanges() => this.SaveChanges(true);

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations and optionally,
    /// restarts the application and upgrades the database.
    /// </summary>
    /// <param name="resetDatabase">Restarts the application and upgrades the database if true.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade SaveChanges(bool resetDatabase)
    {
      AllFacadesHelper.SaveChanges(this.appSettings);
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" />.</returns>
    public DynamicTypeFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> object.</returns>
    public DynamicTypeFacade Delete()
    {
      this.EnsureState();
      this.MetadataManager.Delete(this.dynamicType);
      return this;
    }

    /// <summary>
    /// Loads the dynamic type from the metadata manager into the state of the facade.
    /// </summary>
    /// <param name="dynamicTypeId">Id of the dynamic type to be loaded.</param>
    private void LoadDynamicType(Guid dynamicTypeId) => this.dynamicType = !(dynamicTypeId == Guid.Empty) ? this.MetadataManager.GetMetaType(dynamicTypeId) : throw new ArgumentNullException(nameof (dynamicTypeId));

    private void LoadDynamicType(Type existingType)
    {
      string existingClassName = !(existingType == (Type) null) ? existingType.Name : throw new ArgumentNullException(nameof (existingType));
      string existingNamespace = existingType.Namespace;
      this.dynamicType = this.MetadataManager.GetMetaTypes().Where<MetaType>((Expression<Func<MetaType, bool>>) (dt => dt.ClassName == existingClassName && dt.Namespace == existingNamespace)).SingleOrDefault<MetaType>();
      if (this.dynamicType == null)
        throw new InvalidOperationException(string.Format("Specified type '{0}' is not a dynamic type.", (object) existingType.FullName));
    }

    private void LoadDynamicType(string nameSpace, string className)
    {
      if (string.IsNullOrEmpty(nameSpace))
        throw new ArgumentNullException(nameof (nameSpace));
      if (string.IsNullOrEmpty(className))
        throw new ArgumentNullException(nameof (className));
      this.dynamicType = this.MetadataManager.GetMetaType(nameSpace, className);
      if (this.dynamicType == null)
        throw new InvalidOperationException(string.Format("Specified type '{0}' is not a dynamic type.", (object) string.Format("{0}.{1}", (object) nameSpace, (object) className)));
    }

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    private void EnsureState()
    {
      if (this.dynamicType == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }
  }
}
