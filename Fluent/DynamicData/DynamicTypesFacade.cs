// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade
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
  /// Fluent API that provides most common functionality needed to work with a collection of dynamic types.
  /// </summary>
  public class DynamicTypesFacade : 
    ICollectionFacade<DynamicTypesFacade, MetaType>,
    IFacade<DynamicTypesFacade>
  {
    private IQueryable<MetaType> dynamicTypes;
    private AppSettings appSettings;
    private MetadataManager metadataManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public DynamicTypesFacade(AppSettings appSettings) => this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));

    /// <summary>
    /// Gets the instance of the <see cref="P:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade.MetadataManager" /> to be used by the facade.
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

    /// <summary>Gets the query of all dynamic types in the system.</summary>
    protected IQueryable<MetaType> DynamicTypes
    {
      get
      {
        if (this.dynamicTypes == null)
          this.dynamicTypes = this.MetadataManager.GetMetaTypes();
        return this.dynamicTypes;
      }
    }

    /// <summary>
    /// Gets the count of items in collection at current facade.
    /// </summary>
    /// <param name="result">The count of items.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade Count(out int result)
    {
      result = this.DynamicTypes.Count<MetaType>();
      return this;
    }

    /// <summary>
    /// Performs an arbitrary action for each item of collection at facade.
    /// </summary>
    /// <param name="action">An action to be performed for each item of collection.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade ForEach(Action<MetaType> action)
    {
      if (action == null)
        throw new ArgumentNullException(nameof (action));
      foreach (MetaType dynamicType in (IEnumerable<MetaType>) this.DynamicTypes)
        action(dynamicType);
      return this;
    }

    /// <summary>
    /// Gets query with instances of type &lt;typeparam name="MetaType"&gt;.
    /// </summary>
    /// <returns>An instance of IQueryable[MetaType] object. </returns>
    public IQueryable<MetaType> Get() => this.DynamicTypes;

    /// <summary>
    /// Orders the items of collection in ascending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade OrderBy<TKey>(
      Expression<Func<MetaType, TKey>> keySelector)
    {
      this.dynamicTypes = keySelector != null ? (IQueryable<MetaType>) this.DynamicTypes.OrderBy<MetaType, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Orders the items of collection in descending order with keys specified with keySelector parameter.
    /// </summary>
    /// <param name="keySelector">The key selector.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade OrderByDescending<TKey>(
      Expression<Func<MetaType, TKey>> keySelector)
    {
      this.dynamicTypes = keySelector != null ? (IQueryable<MetaType>) this.DynamicTypes.OrderByDescending<MetaType, TKey>(keySelector) : throw new ArgumentNullException(nameof (keySelector));
      return this;
    }

    /// <summary>
    /// Sets the collection with items filtered with query parameter.
    /// </summary>
    /// <param name="query">The query to filter the items.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade Set(IQueryable<MetaType> query)
    {
      this.dynamicTypes = query != null ? query : throw new ArgumentNullException(nameof (query));
      return this;
    }

    /// <summary>Skips the items of collection with specified count.</summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade Skip(int count)
    {
      this.dynamicTypes = count >= 0 ? this.DynamicTypes.Skip<MetaType>(count) : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Takes items from collection number of which is specified with the count parameter.
    /// </summary>
    /// <param name="count">The count.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade Take(int count)
    {
      this.dynamicTypes = count >= 0 ? this.DynamicTypes.Take<MetaType>(count) : throw new ArgumentException(nameof (count));
      return this;
    }

    /// <summary>
    /// Filters items of the collection by specified where clause at predicate parameter.
    /// </summary>
    /// <param name="predicate">The predicate to filter by.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade Where(Func<MetaType, bool> predicate)
    {
      this.dynamicTypes = predicate != null ? this.DynamicTypes.Where<MetaType>(predicate).AsQueryable<MetaType>() : throw new ArgumentNullException(nameof (predicate));
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
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade SaveChanges()
    {
      TransactionManager.CommitTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations and optionally,
    /// restarts the application and upgrades the database.
    /// </summary>
    /// <param name="upgradeDatabase">Restarts the application and upgrades the database if true.</param>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    [Obsolete("Use SaveChanges method without parameter instead, as the parameter 'upgradeDatabase' is not relevant - the database is always upgraded without restarting the application")]
    public DynamicTypesFacade SaveChanges(bool upgradeDatabase) => this.SaveChanges();

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <remarks>
    /// This method needs to be used if you have configured the fluent API not to auto-commit. By default
    /// fluent API will auto-commit all operations as they are called. Use this method when you wish to
    /// work in transactions.
    /// </remarks>
    /// <returns>An instance of the current <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" />.</returns>
    public DynamicTypesFacade CancelChanges()
    {
      TransactionManager.RollbackTransaction(this.appSettings.TransactionName);
      return this;
    }

    /// <summary>Deletes object.</summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object has not been initialized either through constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.DynamicData.DynamicTypesFacade" /> object.</returns>
    public DynamicTypesFacade Delete()
    {
      DynamicTypeFacade dynamicTypeFacade = new DynamicTypeFacade(this.appSettings);
      foreach (MetaType dynamicType in (IEnumerable<MetaType>) this.DynamicTypes)
        dynamicTypeFacade.Set(dynamicType).Delete();
      return this;
    }
  }
}
