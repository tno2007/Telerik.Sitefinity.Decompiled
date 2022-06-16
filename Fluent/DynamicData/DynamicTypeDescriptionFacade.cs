// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Fluent.AnyContent.Implementation.Helpers;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Fluent.DynamicData
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single dynamic type.
  /// </summary>
  public class DynamicTypeDescriptionFacade : 
    IItemFacade<DynamicTypeDescriptionFacade, MetaTypeDescription>,
    IFacade<DynamicTypeDescriptionFacade>
  {
    private MetaTypeDescription dynamicTypeDescription;
    private AppSettings appSettings;
    private MetadataManager metadataManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public DynamicTypeDescriptionFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="dynamicTypeDescriptionId">The page id.</param>
    public DynamicTypeDescriptionFacade(AppSettings appSettings, Guid dynamicTypeDescriptionId)
      : this(appSettings)
    {
      if (dynamicTypeDescriptionId == Guid.Empty)
        throw new ArgumentNullException(nameof (dynamicTypeDescriptionId));
      this.LoadDynamicTypeDescription(dynamicTypeDescriptionId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="page">The dynamic type on which the fluent API functionality ought to be used.</param>
    public DynamicTypeDescriptionFacade(
      AppSettings appSettings,
      MetaTypeDescription dynamicTypeDescription)
      : this(appSettings)
    {
      this.dynamicTypeDescription = dynamicTypeDescription != null ? dynamicTypeDescription : throw new ArgumentNullException(nameof (dynamicTypeDescription));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> class
    /// and loads the dynamic type that was created from an existing type.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="nameSpace">The namespace of the MetaTypeDescription.</param>
    /// <param name="className">The class name for the MetaTypeDescription.</param>
    public DynamicTypeDescriptionFacade(AppSettings appSettings, Type dynamicType)
      : this(appSettings)
    {
      if (dynamicType == (Type) null)
        throw new ArgumentNullException(nameof (dynamicType));
      this.LoadDynamicTypeDescription(dynamicType);
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade.MetadataManager" /> to be used by the facade.
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

    DynamicTypeDescriptionFacade IItemFacade<DynamicTypeDescriptionFacade, MetaTypeDescription>.CreateNew() => throw new NotSupportedException("This method cannot be used with the DynamicTypeDescriptionFacade. Please use one of other overloads.");

    DynamicTypeDescriptionFacade IItemFacade<DynamicTypeDescriptionFacade, MetaTypeDescription>.CreateNew(
      Guid itemId)
    {
      throw new NotSupportedException("This method cannot be used with the DynamicTypeDescriptionFacade. Please use one of other overloads.");
    }

    /// <summary>
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> with specified CLR type of
    /// the dynamic type.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type this type description object describes.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.</returns>
    public DynamicTypeDescriptionFacade CreateNew(Guid metaTypeId)
    {
      this.dynamicTypeDescription = !(metaTypeId == Guid.Empty) ? this.MetadataManager.CreateMetaTypeDescription(metaTypeId) : throw new ArgumentNullException(nameof (metaTypeId));
      return this;
    }

    /// <summary>
    /// Creates a new instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> for the given meta type
    /// and id of the dynamic type description.
    /// </summary>
    /// <param name="metaTypeId">Id of the meta type this type description object describes.</param>
    /// <param name="id">The id of the new dynamic type to be created.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.</returns>
    public DynamicTypeDescriptionFacade CreateNew(
      Guid metaTypeId,
      Guid id)
    {
      if (metaTypeId == Guid.Empty)
        throw new ArgumentNullException(nameof (metaTypeId));
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      this.dynamicTypeDescription = this.MetadataManager.CreateMetaTypeDescription(metaTypeId, id);
      return this;
    }

    /// <summary>
    /// Returns instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object.</returns>
    public MetaTypeDescription Get() => this.dynamicTypeDescription;

    /// <summary>
    /// Sets an instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> on currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> object.</returns>
    public DynamicTypeDescriptionFacade Set(MetaTypeDescription item)
    {
      this.dynamicTypeDescription = item != null ? item : throw new ArgumentNullException(nameof (item));
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object.
    /// </summary>
    /// <param name="action">An action to be performed on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaTypeDescription" /> has not been initialized either through Facade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> object.</returns>
    public DynamicTypeDescriptionFacade Do(
      Action<MetaTypeDescription> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      this.EnsureState();
      action(this.dynamicTypeDescription);
      return this;
    }

    /// <summary>
    /// Provides the access to the child facade for working with a single dynamic field of this dynamic type.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicTypeFacade DynamicType()
    {
      this.EnsureState();
      return new DynamicTypeFacade(this.appSettings, this.dynamicTypeDescription.MetaTypeId);
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.</returns>
    public DynamicTypeDescriptionFacade SaveChanges()
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
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" />.</returns>
    public DynamicTypeDescriptionFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeDescriptionFacade" /> object.</returns>
    public DynamicTypeDescriptionFacade Delete()
    {
      this.EnsureState();
      this.MetadataManager.Delete(this.dynamicTypeDescription);
      return this;
    }

    /// <summary>
    /// Loads the dynamic type description from the metadata manager into the state of the facade.
    /// </summary>
    /// <param name="dynamicTypeDescriptionId">Id of the dynamic type description to be loaded.</param>
    private void LoadDynamicTypeDescription(Guid dynamicTypeDescriptionId) => this.dynamicTypeDescription = this.MetadataManager.GetMetaTypeDescription(dynamicTypeDescriptionId);

    /// <summary>
    /// Loads the dynamic type description from the metadata manager into the state of the facade.
    /// </summary>
    /// <param name="metaTypeId">The meta type id.</param>
    private void LoadDynamicTypeDescription(Type dynamicType)
    {
      this.dynamicTypeDescription = this.MetadataManager.GetMetaTypeDescription(dynamicType);
      if (this.dynamicTypeDescription == null)
        throw new InvalidOperationException(string.Format("Specified dynamic type '{0}' was not found.", (object) dynamicType));
    }

    /// <summary>
    /// Loads the dynamic type description from the metadata manager into the state of the facade.
    /// </summary>
    /// <param name="metaTypeId">The meta type id.</param>
    private void LoadDynamicTypeDescriptionForMetaType(Guid metaTypeId)
    {
      this.dynamicTypeDescription = !(metaTypeId == Guid.Empty) ? this.MetadataManager.GetMetaTypeDescriptionForMetaType(metaTypeId) : throw new ArgumentNullException(nameof (metaTypeId));
      if (this.dynamicTypeDescription == null)
        throw new InvalidOperationException(string.Format("Specified type ID '{0}' was not found.", (object) metaTypeId));
    }

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    private void EnsureState()
    {
      if (this.dynamicTypeDescription == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }
  }
}
