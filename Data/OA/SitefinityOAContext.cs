// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.OA.SitefinityOAContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.OA
{
  /// <summary>Sitefinity OpenAccessContext class</summary>
  public class SitefinityOAContext : OpenAccessContext, IProviderContext
  {
    private IOpenAccessMetadataProvider provider;
    private Dictionary<string, object> executionStateBag;
    private Dictionary<string, SitefinityOAContext> subContexts;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.SitefinityOAContext" /> class.
    /// </summary>
    /// <param name="fromContext">From context.</param>
    protected SitefinityOAContext(SitefinityOAContext fromContext)
      : base((OpenAccessContextBase) fromContext)
    {
      this.OpenAccessConnection = fromContext.OpenAccessConnection;
      this.Init();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.OA.SitefinityOAContext" /> class.
    /// </summary>
    /// <param name="connectionString">The connection string.</param>
    /// <param name="backendConfiguration">The backend configuration.</param>
    /// <param name="metadataContainer">The metadata container.</param>
    public SitefinityOAContext(
      string connectionString,
      BackendConfiguration backendConfiguration,
      MetadataContainer metadataContainer)
      : base(connectionString, backendConfiguration, metadataContainer)
    {
      this.GetScope();
      this.Init();
    }

    private void Init()
    {
      this.ContextOptions.MaintainOriginalValues = true;
      this.Events.ObjectConstructed += new ObjectConstructedEventHandler(this.Events_ObjectConstructed);
    }

    private void Events_ObjectConstructed(object sender, ObjectConstructedEventArgs e)
    {
      if (!(e.PersistentObject is IDataItem persistentObject) || persistentObject.Provider != null)
        return;
      persistentObject.Provider = (object) this.Provider;
    }

    internal void FetchAllLanguagesData()
    {
      if (!this.OpenAccessConnection.AllowFetchStrategy || this.Provider == null)
        return;
      this.FetchStrategy = this.OpenAccessConnection.FetchStrategyCache.GetLocalizedFetchStrategy(this.Provider.GetModuleName(), new Func<IEnumerable<CultureInfo>>(this.GetFetchCultures));
    }

    internal void FetchLanguadeSpecificData(CultureInfo culture)
    {
      if (!this.OpenAccessConnection.AllowFetchStrategy || this.Provider == null || this.Provider is OpenAccessMultisiteProvider || this.OpenAccessConnection.FetchStrategyCache == null)
        return;
      Func<IEnumerable<CultureInfo>> culturesFunc = (Func<IEnumerable<CultureInfo>>) (() =>
      {
        List<CultureInfo> first = new List<CultureInfo>();
        first.Add(culture);
        if (!culture.IsNeutralCulture)
        {
          do
          {
            culture = culture.Parent;
            first.Add(culture);
          }
          while (!culture.IsNeutralCulture && !culture.Equals((object) CultureInfo.InvariantCulture));
          first = first.Intersect<CultureInfo>((IEnumerable<CultureInfo>) AppSettings.CurrentSettings.DefinedFrontendLanguages).ToList<CultureInfo>();
        }
        return (IEnumerable<CultureInfo>) first;
      });
      this.FetchStrategy = this.OpenAccessConnection.FetchStrategyCache.GetLocalizedFetchStrategy(this.Provider.GetModuleName(), culturesFunc, new Func<IEnumerable<CultureInfo>>(this.GetFetchCultures));
    }

    internal void ExecutePagedOperation<TItem>(
      IEnumerable<TItem> items,
      System.Action<IEnumerable<TItem>> action)
    {
      int count = 100;
      int num1 = items.Count<TItem>();
      int num2 = 0;
      if (num1 > 0)
        num2 = (int) Math.Ceiling((double) num1 / (double) count);
      for (int index = 0; index < num2; ++index)
      {
        TItem[] array = items.Skip<TItem>(index * count).Take<TItem>(count).ToArray<TItem>();
        action((IEnumerable<TItem>) array);
      }
    }

    /// <summary>
    /// Releases unmanaged and - optionally - managed resources
    /// </summary>
    /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && this.subContexts != null)
      {
        foreach (KeyValuePair<string, SitefinityOAContext> subContext in this.subContexts)
          subContext.Value.Dispose();
        this.subContexts.Clear();
      }
      base.Dispose(disposing);
    }

    /// <summary>Saves the changes.</summary>
    public virtual void Commit()
    {
      if (this.subContexts != null)
      {
        foreach (KeyValuePair<string, SitefinityOAContext> subContext in this.subContexts)
          subContext.Value.Commit();
      }
      this.SaveChanges();
    }

    /// <summary>Undo the changes.</summary>
    public virtual void Rollback()
    {
      if (this.subContexts != null)
      {
        foreach (KeyValuePair<string, SitefinityOAContext> subContext in this.subContexts)
          subContext.Value.Rollback();
      }
      this.ClearChanges();
    }

    /// <summary>Flushes the transaction of the context scope.</summary>
    public virtual void Flush()
    {
      if (this.subContexts != null)
      {
        foreach (KeyValuePair<string, SitefinityOAContext> subContext in this.subContexts)
          subContext.Value.Flush();
      }
      this.Scope.Transaction.Flush();
    }

    /// <summary>Gets the object by id.</summary>
    /// <param name="objId">The obj id.</param>
    /// <returns></returns>
    public object GetObjectById(IObjectId objId) => this.Scope.GetObjectById(objId);

    protected override IQueryable<T> GetAllCore<T>() => base.GetAllCore<T>();

    /// <summary>Gets the object by id.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="objId">The obj id.</param>
    /// <returns></returns>
    public T GetObjectById<T>(IObjectId objId) => this.Scope.GetObjectById<T>(objId);

    /// <summary>Removes the specified entity.</summary>
    /// <param name="entity">The entity.</param>
    public void Remove(object entity) => this.Delete(entity);

    /// <summary>Locks the specified target object.</summary>
    /// <param name="targetObject">The target object.</param>
    /// <param name="mode">The mode.</param>
    public void Lock(object targetObject, LockMode mode) => this.Scope.Transaction.Lock(targetObject, mode);

    /// <summary>
    /// Gets a value indicating whether this current transaction is active.
    /// </summary>
    /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
    public bool IsActive => this.Scope.Transaction.IsActive;

    /// <summary>Gets the dirty items.</summary>
    /// <returns></returns>
    public IList GetDirtyItems() => this.Scope.Transaction.DirtyObjects;

    /// <summary>Gets the dirty objects.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public IList<T> GetDirtyObjects<T>() => this.Scope.Transaction.GetDirtyObjects<T>();

    /// <summary>
    /// Determines whether the specified persistent object is new.
    /// </summary>
    /// <param name="persistentObject">The persistent object.</param>
    /// <returns>
    /// 	<c>true</c> if the specified persistent object is new; otherwise, <c>false</c>.
    /// </returns>
    public bool IsNew(object persistentObject) => this.Scope.IsNew(persistentObject);

    /// <summary>
    /// Determines whether the specified persistent object is removed.
    /// </summary>
    /// <param name="persistentObject">The persistent object.</param>
    /// <returns>
    /// 	<c>true</c> if the specified persistent object is removed; otherwise, <c>false</c>.
    /// </returns>
    public bool IsRemoved(object persistentObject) => this.Scope.IsRemoved(persistentObject);

    /// <summary>
    /// Determines whether the specified persistent object is dirty.
    /// </summary>
    /// <param name="persistentObject">The persistent object.</param>
    /// <returns>
    /// 	<c>true</c> if the specified persistent object is dirty; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDirty(object persistentObject) => this.Scope.IsDirty(persistentObject);

    /// <summary>
    /// Determines whether a persistent object field has been modified
    /// </summary>
    /// <param name="persistentObject">The persistent object.</param>
    /// <param name="fieldName">Name of the field.</param>
    public bool IsFieldDirty(object persistentObject, string fieldName) => this.Scope.IsDirty(persistentObject, fieldName);

    /// <summary>Gets the object id.</summary>
    /// <param name="persistentObject">The persistent object.</param>
    /// <returns></returns>
    public IObjectId GetObjectId(object persistentObject) => this.Scope.GetObjectId(persistentObject);

    /// <summary>
    /// Gets the tables mapped to a persistent type  and its dynamic associations.
    /// </summary>
    /// <param name="persistedTypeName">Name of the persisted type.</param>
    internal List<string> GetPersistentTypeTablesList(string persistedTypeName)
    {
      List<string> list = this.GetAssociatedTablesToDrop(persistedTypeName).ToList<string>();
      string mappedTableName = this.GetMappedTableName(persistedTypeName);
      if (!string.IsNullOrEmpty(mappedTableName))
        list.Add(mappedTableName);
      return list;
    }

    /// <summary>
    /// Gets the type associated tables for the specified persistent.
    /// </summary>
    /// <param name="persistedTypeName">Name of the persisted type.</param>
    internal List<string> GetPersistentTypeAssociatedTables(string persistedTypeName) => this.GetAssociatedTablesToDrop(persistedTypeName).ToList<string>();

    /// <summary>Drops the specified tables.</summary>
    /// <param name="tables">The tables.</param>
    internal bool DropTables(string[] tables)
    {
      try
      {
        if (tables.Length != 0)
        {
          SitefinityOAContext subContext = this.GetSubContext("Drop Tables");
          foreach (string table in tables)
            subContext.ExecuteNonQuery(this.GetDropTableCommandScript(table, this));
          subContext.SaveChanges();
        }
        return true;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders);
        return false;
      }
    }

    /// <summary>
    /// Drops the persistent type table and all its dynamic associated tables
    /// </summary>
    /// <param name="persistedTypeName">Name of the persisted type.</param>
    internal bool DropPersistentTypeTables(string persistedTypeName)
    {
      try
      {
        this.DropDynamicAssociatedTables(persistedTypeName, this);
        this.DropPersistentTypeTable(persistedTypeName, this);
        this.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders);
        return false;
      }
    }

    /// <summary>Drops the persistent type table.</summary>
    /// <param name="persistedTypeName">Name of the persisted type.</param>
    /// <returns></returns>
    internal bool DropPersistentTypeTable(string persistedTypeName)
    {
      try
      {
        this.DropPersistentTypeTable(persistedTypeName, this);
        this.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders);
        return false;
      }
    }

    /// <summary>
    /// Drops the dynamic associated tables which are associated to the specified persisted
    /// type and have their names start with the name of the mapped table for the specified dynamic type
    /// </summary>
    /// <param name="persistedTypeName">Name of the persisted type.</param>
    /// <returns></returns>
    internal bool DropDynamicAssociatedTables(string persistedTypeName)
    {
      try
      {
        this.DropDynamicAssociatedTables(persistedTypeName, this);
        this.SaveChanges();
        return true;
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.DataProviders);
        return false;
      }
    }

    private void DropDynamicAssociatedTables(string persistedTypeName, SitefinityOAContext context)
    {
      foreach (string tableName in this.GetAssociatedTablesToDrop(persistedTypeName))
        this.DropTable(tableName, context);
    }

    private void DropPersistentTypeTable(string persistedTypeName, SitefinityOAContext context)
    {
      string mappedTableName = this.GetMappedTableName(persistedTypeName);
      if (mappedTableName.IsNullOrEmpty())
        return;
      this.DropTable(mappedTableName, context);
    }

    private void DropTable(string tableName, SitefinityOAContext context)
    {
      if (context.OpenAccessConnection == null)
        return;
      string tableCommandScript = this.GetDropTableCommandScript(tableName, context);
      context.ExecuteNonQuery(tableCommandScript);
    }

    private IEnumerable<string> GetAssociatedTablesToDrop(string persistedTypeName)
    {
      MetaPersistentType metaPersistentType = this.Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => pt.FullName == persistedTypeName));
      return metaPersistentType != null && metaPersistentType.MetaDataContainer != null && persistedTypeName != null ? metaPersistentType.MetaDataContainer.Associations.Where<MetaAssociation>((Func<MetaAssociation, bool>) (a => a is MetaJoinTableAssociation tableAssociation && tableAssociation.Source.FullName == persistedTypeName)).Select<MetaAssociation, string>((Func<MetaAssociation, string>) (a => ((MetaJoinTableAssociation) a).JoinTable.Name)) : (IEnumerable<string>) new List<string>();
    }

    private string GetMappedTableName(string persistedTypeName)
    {
      MetaPersistentType metaPersistentType = this.Metadata.PersistentTypes.FirstOrDefault<MetaPersistentType>((Func<MetaPersistentType, bool>) (pt => pt.FullName == persistedTypeName));
      return metaPersistentType != null ? metaPersistentType.Table.Name : string.Empty;
    }

    private string GetDropTableCommandScript(string tableName, SitefinityOAContext context) => string.Format(context.OpenAccessConnection.GetFluentMappingContext().DatabaseType == DatabaseType.Oracle ? "DROP TABLE \"{0}\"" : "DROP TABLE {0}", (object) tableName);

    /// <summary>Gets the persistent meta data.</summary>
    /// <value>The persistent meta data.</value>
    public PersistentMetaData PersistentMetaData => this.Scope.PersistentMetaData;

    /// <summary>Gets the scope.</summary>
    /// <value>The scope.</value>
    [Obsolete("Do not use this property outside of this class. If you need something from the scope create wrapping method.")]
    internal IObjectScope Scope => this.GetScope();

    /// <summary>Gets the connection used by the OpenAccess context.</summary>
    /// <value>Connection that can be used to perform ADO operations</value>
    internal OpenAccessConnection OpenAccessConnection { get; set; }

    internal IOpenAccessMetadataProvider Provider
    {
      get => this.provider;
      set
      {
        if (value == null)
          return;
        this.provider = value;
        this.Name = value.ModuleName;
      }
    }

    /// <summary>Gets the sub context.</summary>
    /// <param name="contextName">Name of the context.</param>
    /// <returns></returns>
    internal SitefinityOAContext GetSubContext(string contextName)
    {
      if (this.subContexts == null)
        this.subContexts = new Dictionary<string, SitefinityOAContext>();
      SitefinityOAContext subContext;
      if (!this.subContexts.TryGetValue(contextName, out subContext))
      {
        subContext = new SitefinityOAContext(this);
        this.subContexts.Add(contextName, subContext);
      }
      return subContext;
    }

    /// <summary>Clears the state bag.</summary>
    public void ClearExecutionStateBag() => this.executionStateBag = (Dictionary<string, object>) null;

    private IEnumerable<CultureInfo> GetFetchCultures() => !(SystemManager.CurrentContext.CurrentSite is MultisiteContext.SiteProxy currentSite) ? (IEnumerable<CultureInfo>) MetadataSourceAggregator.GetConfiguredCultures() : currentSite.FetchingCultures;

    /// <summary>
    /// Gets a value indicating whether this instance has temporary transaction state objects for processing after context commit.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance has transcation state; otherwise, <c>false</c>.
    /// </value>
    internal bool HasExecutionState => this.executionStateBag != null && this.executionStateBag.Count > 0;

    /// <summary>
    /// Gets the transaction state bag. The state bag can be used to store temporary named objects that can be processed after the context is committed
    /// </summary>
    internal IDictionary<string, object> ExecutionStateBag
    {
      get
      {
        if (this.executionStateBag == null)
          this.executionStateBag = new Dictionary<string, object>();
        return (IDictionary<string, object>) this.executionStateBag;
      }
    }

    internal string ProviderName { get; set; }

    internal string TransactionName { get; set; }

    object IProviderContext.Provider => (object) this.Provider;
  }
}
