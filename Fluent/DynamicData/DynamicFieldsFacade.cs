// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;

namespace Telerik.Sitefinity.Fluent.DynamicData
{
  /// <summary>
  /// Fluent API that provides most common functionality needed to work with a collection of dynamic fields.
  /// </summary>
  public class DynamicFieldsFacade : 
    ICollectionFacade<DynamicFieldsFacade, MetaField>,
    IFacade<DynamicFieldsFacade>
  {
    private IQueryable<MetaField> dynamicFields;
    private AppSettings appSettings;
    private MetadataManager metadataManager;
    private DynamicTypeFacade parentFacade;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    /// <param name="parentFacade">
    /// The parent facade that initialized this facade; pass null if accessing the facade directly.
    /// </param>
    public DynamicFieldsFacade(AppSettings appSettings, DynamicTypeFacade parentFacade)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
      this.parentFacade = parentFacade;
    }

    /// <summary>
    /// Gets the instance of <see cref="P:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade.MetadataManager" /> to be used by the facade.
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

    /// <summary>Gets all the dynamic fields in the system.</summary>
    protected IQueryable<MetaField> DynamicFields
    {
      get
      {
        if (this.dynamicFields == null)
          this.dynamicFields = this.MetadataManager.GetMetafields();
        return this.dynamicFields;
      }
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Count(out int result)
    {
      result = this.DynamicFields.Count<MetaField>();
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action for each item of collection at facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade ForEach(Action<MetaField> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (MetaField dynamicField in (IEnumerable<MetaField>) this.DynamicFields)
        action(dynamicField);
      return this;
    }

    /// <summary>
    /// Gets query with instances of type &lt;typeparam name="MetaField"&gt;.
    /// </summary>
    /// <returns>An instance of IQueryable[MetaField] object. </returns>
    public IQueryable<MetaField> Get() => this.DynamicFields;

    /// <summary>
    /// Returns the parent facade that initialized this child facade.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if method is called and parentFacade is null; meaning that facade is not a child facade in this context.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypeFacade" /> that initialized this facade as a child facade.</returns>
    public DynamicTypeFacade Done() => this.parentFacade != null ? this.parentFacade : throw new InvalidOperationException("Done method can be called only when the facade has been initialized as a child facade.");

    /// <summary>
    /// Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade OrderBy<TKey>(
      Expression<Func<MetaField, TKey>> keySelector)
    {
      this.dynamicFields = keySelector != null ? (IQueryable<MetaField>) this.DynamicFields.OrderBy<MetaField, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade OrderByDescending<TKey>(
      Expression<Func<MetaField, TKey>> keySelector)
    {
      this.dynamicFields = keySelector != null ? (IQueryable<MetaField>) this.DynamicFields.OrderByDescending<MetaField, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Sets the collection with items filtered with query parameter.
    /// </summary>
    /// <param name="query">The query to filter the items.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Set(IQueryable<MetaField> query)
    {
      this.dynamicFields = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>Skips the items of collection with specified count.</summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Skip(int count)
    {
      this.dynamicFields = count >= 0 ? this.DynamicFields.Skip<MetaField>(count) : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Take(int count)
    {
      this.dynamicFields = count >= 0 ? this.DynamicFields.Take<MetaField>(count) : throw new ArgumentNullException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade Where(Func<MetaField, bool> predicate)
    {
      this.dynamicFields = predicate != null ? this.DynamicFields.Where<MetaField>(predicate).AsQueryable<MetaField>() : throw new ArgumentNullException(nameof (predicate));
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations and optionally,
    /// restarts the application and upgrades the database.
    /// </summary>
    /// <param name="upgradeDatabase">Restarts the application and upgrades the database if true.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    [Obsolete("Use SaveChanges method without parameter instead, as the parameter 'upgradeDatabase' is not relevant - the database is always upgraded without restarting the application")]
    public DynamicFieldsFacade SaveChanges(bool upgradeDatabase) => this.SaveChanges();

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" />.</returns>
    public DynamicFieldsFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicFieldsFacade" /> object.</returns>
    public DynamicFieldsFacade Delete()
    {
      DynamicFieldFacade dynamicFieldFacade = new DynamicFieldFacade(this.appSettings, (DynamicTypeFacade) null);
      foreach (MetaField dynamicField in (IEnumerable<MetaField>) this.DynamicFields)
        dynamicFieldFacade.Set(dynamicField).Delete();
      return this;
    }
  }
}
