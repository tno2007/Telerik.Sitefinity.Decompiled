// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Fluent.DynamicData
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single dynamic field.
  /// </summary>
  public class DynamicFieldFacade : 
    IItemFacade<DynamicFieldFacade, MetaField>,
    IFacade<DynamicFieldFacade>
  {
    private MetaField dynamicField;
    private AppSettings appSettings;
    private MetadataManager metadataManager;
    private DynamicTypeFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    /// <param name="parentFacade">
    /// The parent facade that initialized this facade; pass null if accessing the facade directly.
    /// </param>
    public DynamicFieldFacade(AppSettings appSettings, DynamicTypeFacade parentFacade)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    /// <param name="parentFacade">
    /// The parent facade that initialized this facade; pass null if accessing the facade directly.
    /// </param>
    /// <param name="dynamicFieldId">The id of the dynamic field.</param>
    public DynamicFieldFacade(
      AppSettings appSettings,
      DynamicTypeFacade parentFacade,
      Guid dynamicFieldId)
      : this(appSettings, parentFacade)
    {
      if (dynamicFieldId == Guid.Empty)
        throw new ArgumentNullException(nameof (dynamicFieldId));
      this.LoadDynamicField(dynamicFieldId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    /// <param name="parentFacade">
    /// The parent facade that initialized this facade; pass null if accessing the facade directly.
    /// </param>
    /// <param name="dynamicField">The dynamic field on which the fluent API functionality ought to be used.</param>
    public DynamicFieldFacade(
      AppSettings appSettings,
      DynamicTypeFacade parentFacade,
      MetaField dynamicField)
      : this(appSettings, parentFacade)
    {
      this.dynamicField = dynamicField != null ? dynamicField : throw new ArgumentNullException(nameof (dynamicField));
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade.MetadataManager" /> to be used by the facade.
    /// </summary>
    protected MetadataManager MetadataManager
    {
      get
      {
        if (this.metadataManager == null)
          this.metadataManager = MetadataManager.GetManager((string) null, this.appSettings.TransactionName);
        return this.metadataManager;
      }
    }

    DynamicFieldFacade IItemFacade<DynamicFieldFacade, MetaField>.CreateNew() => throw new NotSupportedException("This method cannot be used with the DynamicFieldFacade. Please use one of other overloads.");

    DynamicFieldFacade IItemFacade<DynamicFieldFacade, MetaField>.CreateNew(
      Guid itemId)
    {
      throw new NotSupportedException("This method cannot be used with the DynamicFieldFacade. Please use one of other overloads.");
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name and type.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade CreateNew(string fieldName, Type fieldType)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      if (fieldType == (Type) null)
        throw new ArgumentNullException(nameof (fieldType));
      this.dynamicField = this.MetadataManager.CreateMetafield(fieldName);
      this.dynamicField.ClrType = fieldType.FullName;
      if (this.parentFacade != null)
        this.parentFacade.Get().Fields.Add(this.dynamicField);
      return this;
    }

    /// <summary>Creates a linked content field.</summary>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="contentLinksProviderName">Name of the content links provider.</param>
    /// <param name="relationshipType">Type of the relationship - 1 to 1 or 1 to many.</param>
    public DynamicFieldFacade CreateContentLinkField(
      string fieldName,
      string contentLinksProviderName,
      RelationshipType relationshipType = RelationshipType.OneToOne)
    {
      this.dynamicField = ContentLinksExtensions.CreateContentLinkField(fieldName, contentLinksProviderName, this.MetadataManager, relationshipType);
      if (this.parentFacade != null)
        this.parentFacade.Get().Fields.Add(this.dynamicField);
      return this;
    }

    /// <summary>
    /// Creates a content links field with the default conetnt links provider ("OpenAccessDataProvider")
    /// </summary>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="relationshipType">Type of the relationship - 1 to 1 or 1 to many.</param>
    public DynamicFieldFacade CreateContentLinkField(
      string fieldName,
      RelationshipType relationshipType = RelationshipType.OneToOne)
    {
      this.CreateContentLinkField(fieldName, "OpenAccessDataProvider", relationshipType);
      return this;
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name, type and id.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <param name="fieldId">The id of the new dynamic field.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade CreateNew(
      string fieldName,
      Type fieldType,
      Guid fieldId)
    {
      if (string.IsNullOrEmpty(fieldName))
        throw new ArgumentNullException(nameof (fieldName));
      if (fieldType == (Type) null)
        throw new ArgumentNullException(nameof (fieldType));
      if (fieldId == Guid.Empty)
        throw new ArgumentNullException(nameof (fieldId));
      this.dynamicField = this.MetadataManager.CreateMetafield(fieldName, fieldId);
      this.dynamicField.ClrType = fieldType.FullName;
      if (this.parentFacade != null)
        this.parentFacade.Get().Fields.Add(this.dynamicField);
      return this;
    }

    /// <summary>
    /// Tries to create a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name and type. Field will
    /// be created only if the field with such name does not exist.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade TryCreateNew(string fieldName, Type fieldType)
    {
      if (!this.FieldExist(fieldName))
        return this.CreateNew(fieldName, fieldType);
      this.dynamicField = this.parentFacade.Get().Fields.Single<MetaField>((Func<MetaField, bool>) (f => f.FieldName == fieldName));
      return this;
    }

    /// <summary>
    /// Tries to create a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name and type. Field will
    /// be created only if the field with such name does not exist.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <param name="result">True if the field has been created; otherwise it will keep the original value</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade TryCreateNew(
      string fieldName,
      Type fieldType,
      ref bool result)
    {
      if (this.FieldExist(fieldName))
      {
        this.dynamicField = this.parentFacade.Get().Fields.Single<MetaField>((Func<MetaField, bool>) (f => f.FieldName == fieldName));
        return this;
      }
      result = true;
      return this.CreateNew(fieldName, fieldType);
    }

    /// <summary>
    /// Tries to create a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name, type and id. Field
    /// will be created only if the field with such name does not exist.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <param name="fieldId">The id of the new dynamic field.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade TryCreateNew(
      string fieldName,
      Type fieldType,
      Guid fieldId)
    {
      if (!this.FieldExist(fieldName))
        return this.CreateNew(fieldName, fieldType, fieldId);
      this.dynamicField = this.parentFacade.Get().Fields.Single<MetaField>((Func<MetaField, bool>) (f => f.FieldName == fieldName));
      return this;
    }

    /// <summary>
    /// Tries to create a new instance of the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> with the specified field name, type and id. Field
    /// will be created only if the field with such name does not exist.
    /// </summary>
    /// <param name="fieldName">The name of the new dynamic field.</param>
    /// <param name="fieldType">The type of the new dynamic field.</param>
    /// <param name="fieldId">The id of the new dynamic field.</param>
    /// <param name="result">True if the field has been created; otherwise false</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade TryCreateNew(
      string fieldName,
      Type fieldType,
      Guid fieldId,
      out bool result)
    {
      result = false;
      if (this.FieldExist(fieldName))
      {
        this.dynamicField = this.parentFacade.Get().Fields.Single<MetaField>((Func<MetaField, bool>) (f => f.FieldName == fieldName));
        return this;
      }
      result = true;
      return this.CreateNew(fieldName, fieldType, fieldId);
    }

    /// <summary>
    /// Returns instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> currently loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object.</returns>
    public MetaField Get() => this.dynamicField;

    /// <summary>
    /// Sets an instance of type <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> on currently loaded fluent API.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> object.</returns>
    public DynamicFieldFacade Set(MetaField item)
    {
      this.dynamicField = item != null ? item : throw new ArgumentNullException(nameof (item));
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object.
    /// </summary>
    /// <param name="action">An action to be performed on the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> object.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaField" /> has not been initialized either through Facade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> object.</returns>
    public DynamicFieldFacade Do(Action<MetaField> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      this.EnsureState();
      action(this.dynamicField);
      return this;
    }

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> that initialized this facade as a child facade.</returns>
    public DynamicTypeFacade Done() => this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("Done method can be called only when the facade has been initialized as a child facade.");

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations and optionally,
    /// restarts the application and upgrades the database.
    /// </summary>
    /// <param name="upgradeDatabase">Upgrades the database if true.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    [Obsolete("Use SaveChanges method without parameter instead, as the parameter 'upgradeDatabase' is not relevant - the database is always upgraded without restarting the application")]
    public DynamicFieldFacade SaveChanges(bool upgradeDatabase) => this.SaveChanges();

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" />.</returns>
    public DynamicFieldFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldFacade" /> object.</returns>
    public DynamicFieldFacade Delete()
    {
      this.EnsureState();
      this.MetadataManager.Delete(this.dynamicField);
      return this;
    }

    /// <summary>
    /// Sets the specified data item as a default value for the content link
    /// If the item is null will clear the current if available
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <returns></returns>
    public DynamicFieldFacade SetDefaultContentLinkValue(IDataItem dataItem)
    {
      this.EnsureState();
      if (dataItem != null)
        this.dynamicField.SetDefaultContentLinkValueAttributes(dataItem);
      else
        this.dynamicField.RemoveDefaultContentLinkValueAttributes();
      return this;
    }

    private bool FieldExist(string fieldName)
    {
      if (this.parentFacade == null)
        return false;
      foreach (MetaField field in (IEnumerable<MetaField>) this.parentFacade.Get().Fields)
      {
        if (field.FieldName == fieldName)
          return true;
      }
      return false;
    }

    /// <summary>Loads the dynamic field from the metadata manager.</summary>
    /// <param name="dynamicFieldId">
    /// Id of the dynamic field to be loaded in the state of the facade.
    /// </param>
    private void LoadDynamicField(Guid dynamicFieldId) => this.dynamicField = !(dynamicFieldId == Guid.Empty) ? this.MetadataManager.GetMetafield(dynamicFieldId) : throw new ArgumentNullException(nameof (dynamicFieldId));

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    private void EnsureState()
    {
      if (this.dynamicField == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }
  }
}
