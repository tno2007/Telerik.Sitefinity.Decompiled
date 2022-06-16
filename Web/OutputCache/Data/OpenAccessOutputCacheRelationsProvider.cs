// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.Data.OpenAccessOutputCacheRelationsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.OutputCache;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Web.OutputCache.Data
{
  /// <summary>
  ///  Stores information for output cache dependencies using Data Access (database).
  /// </summary>
  /// <seealso cref="T:Telerik.Sitefinity.Web.OutputCache.Data.OutputCacheRelationsProvider" />
  /// <seealso cref="T:Telerik.Sitefinity.Data.IOpenAccessDataProvider" />
  public class OpenAccessOutputCacheRelationsProvider : 
    OutputCacheRelationsProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider
  {
    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context">The mapping context.</param>
    /// <returns>The metadata source for output cache relations.</returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new OutputCacheRelationsMetadataSource(context);

    /// <summary>Creates the output cache item.</summary>
    /// <returns>The OutputCacheItem.</returns>
    public override OutputCacheItem CreateOutputCacheItem()
    {
      OutputCacheItem entity = new OutputCacheItem();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the output cache items.</summary>
    /// <returns>OutputCacheItem queryable.</returns>
    public override IQueryable<OutputCacheItem> GetOutputCacheItems() => this.GetContext().GetAll<OutputCacheItem>();

    /// <summary>Deletes the output cache item.</summary>
    /// <param name="cacheItem">The output cache item to remove.</param>
    public override void DeleteOutputCacheItem(OutputCacheItem cacheItem) => this.GetContext().Remove((object) cacheItem);

    /// <summary>Deletes the output cache item.</summary>
    /// <param name="cacheItem">The output cache items to remove.</param>
    public override void DeleteAllOutputCacheItems(Expression<Func<OutputCacheItem, bool>> cacheItem) => this.GetContext().GetAll<OutputCacheItem>().Where<OutputCacheItem>(cacheItem).DeleteAll();

    /// <summary>Creates the output cache dependency.</summary>
    /// <returns>The OutputCacheDependency.</returns>
    public override OutputCacheDependency CreateOutputCacheDependency()
    {
      OutputCacheDependency entity = new OutputCacheDependency();
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the output cache dependencies.</summary>
    /// <returns>OutputCacheDependency queryable.</returns>
    public override IQueryable<OutputCacheDependency> GetOutputCacheDependencies() => this.GetContext().GetAll<OutputCacheDependency>();

    /// <summary>Deletes the output cache dependency.</summary>
    /// <param name="cacheDependency">The output cache dependency to remove.</param>
    public override void DeleteOutputCacheDependency(OutputCacheDependency cacheDependency) => this.GetContext().Remove((object) cacheDependency);

    /// <summary>Deletes the output cache dependency.</summary>
    /// <param name="cacheDependency">The output cache dependencies to remove.</param>
    public override void DeleteAllOutputCacheDependencies(
      Expression<Func<OutputCacheDependency, bool>> cacheDependency)
    {
      this.GetContext().GetAll<OutputCacheDependency>().Where<OutputCacheDependency>(cacheDependency).DeleteAll();
    }

    /// <summary>Gets the output cache dependencies.</summary>
    /// <returns>OutputCacheDependency queryable.</returns>
    internal override IQueryable<OutputCacheDependencyType> GetOutputCacheDependencyTypes() => this.GetContext().GetAll<OutputCacheDependencyType>();

    internal override OutputCacheDependencyType CreateOutputCacheDependencyType(
      string typeName)
    {
      OutputCacheDependencyType entity = new OutputCacheDependencyType(typeName);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || context == null || upgradingFromSchemaVersionNumber >= 7228)
        return;
      string[] strArray;
      if (upgradingFromSchemaVersionNumber < 7100)
        strArray = new string[2]
        {
          "sf_oc_items",
          "sf_oc_dependencies"
        };
      else
        strArray = new string[2]
        {
          "sf_oc_itms",
          "sf_oc_dpndncies"
        };
      foreach (string str1 in strArray)
      {
        string str2 = context.DatabaseContext.DatabaseType == DatabaseType.Oracle ? "truncate table \"{0}\"" : "truncate table {0}";
        OpenAccessConnection.Upgrade(context, string.Format((IFormatProvider) CultureInfo.InvariantCulture, "{0}: Upgrade to {1} - truncate table '{2}'", (object) this.GetType().Name, (object) 7126, (object) str1), str2.Arrange((object) str1));
      }
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
    }

    /// <inheritdoc />
    internal override Lock CreateLock(string lockId)
    {
      Lock entity = new Lock(lockId);
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <inheritdoc />
    internal override IQueryable<Lock> GetLocks() => this.GetContext().GetAll<Lock>();

    /// <inheritdoc />
    internal override void DeleteLock(Lock obj) => this.GetContext().Remove((object) obj);

    internal override void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus)
    {
      OpenAccessOutputCacheRelationsProvider.BatchSqlOperations.Get(this.GetContext()).UpdateStatusForItemsByCacheKeys(keys, oldStatus, newStatus);
    }

    internal override void UpdatePageVariationsStatus(
      IEnumerable<string> pageKeys,
      OutputCacheItemStatus oldStatus,
      OutputCacheItemStatus newStatus)
    {
      OpenAccessOutputCacheRelationsProvider.BatchSqlOperations.Get(this.GetContext()).UpdatePageVariationsStatus(pageKeys, oldStatus, newStatus);
    }

    internal override void UpdateOutputCacheItemsStatusByKeys(
      IEnumerable<string> keys,
      OutputCacheItemStatus newStatus)
    {
      OpenAccessOutputCacheRelationsProvider.BatchSqlOperations.Get(this.GetContext()).UpdateStatusForItemsByCacheKeys(keys, newStatus);
    }

    internal override void DeleteItemsByPageNodeKeys(IEnumerable<string> pageNodeKeys) => OpenAccessOutputCacheRelationsProvider.BatchSqlOperations.Get(this.GetContext()).DeleteItemsByPageKeys(pageNodeKeys);

    internal override void DeleteExpiredItemsOlderThan(DateTime olderThanDate) => OpenAccessOutputCacheRelationsProvider.BatchSqlOperations.Get(this.GetContext()).DeleteExpiredItemsOlderThan(olderThanDate);

    private class BatchSqlOperations
    {
      private SitefinityOAContext context;

      public static OpenAccessOutputCacheRelationsProvider.BatchSqlOperations Get(
        SitefinityOAContext context)
      {
        OpenAccessOutputCacheRelationsProvider.BatchSqlOperations batchSqlOperations;
        switch (context.OpenAccessConnection.DbType)
        {
          case DatabaseType.MsSql:
          case DatabaseType.SqlAzure:
            batchSqlOperations = (OpenAccessOutputCacheRelationsProvider.BatchSqlOperations) new OpenAccessOutputCacheRelationsProvider.MsSqlNativeBatchSqlOperations(context);
            break;
          case DatabaseType.Oracle:
            batchSqlOperations = new OpenAccessOutputCacheRelationsProvider.BatchSqlOperations(context);
            break;
          case DatabaseType.MySQL:
            batchSqlOperations = (OpenAccessOutputCacheRelationsProvider.BatchSqlOperations) new OpenAccessOutputCacheRelationsProvider.MySqlNativeBatchSqlOperations(context);
            break;
          default:
            batchSqlOperations = (OpenAccessOutputCacheRelationsProvider.BatchSqlOperations) new OpenAccessOutputCacheRelationsProvider.NativeBatchSqlOperations(context);
            break;
        }
        return batchSqlOperations;
      }

      protected BatchSqlOperations(SitefinityOAContext context) => this.context = context;

      public virtual void UpdateStatusForItemsByCacheKeys(
        IEnumerable<string> keys,
        OutputCacheItemStatus oldStatus,
        OutputCacheItemStatus newStatus)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        this.ExecuteOnBatches<string>(keys, (System.Action<IEnumerable<string>>) (batch => this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch.Contains<string>(i.Key) && (int) i.Status == (int) this.oldStatus)).UpdateAll<OutputCacheItem>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>>>) (i => i.Set<OutputCacheItemStatus>((Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => p.Status), (Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => this.newStatus)).Set<DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (p => p.DateModified), (Expression<Func<OutputCacheItem, DateTime>>) (p => DateTime.UtcNow))))));
      }

      public virtual void UpdatePageVariationsStatus(
        IEnumerable<string> pageKeys,
        OutputCacheItemStatus oldStatus,
        OutputCacheItemStatus newStatus)
      {
        // ISSUE: reference to a compiler-generated field
        this.ExecuteOnBatches<string>(pageKeys, (System.Action<IEnumerable<string>>) (batch => this.ExecuteOnBatches<string>((IEnumerable<string>) this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch.Contains<string>(i.SiteMapNodeKey) && (int) i.Status == (int) this.oldStatus && i.Priority > 50)).Select<OutputCacheItem, string>((Expression<Func<OutputCacheItem, string>>) (i => i.Key)).ToArray<string>(), (System.Action<IEnumerable<string>>) (batch2 => this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch2.Contains<string>(i.Key))).UpdateAll<OutputCacheItem>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>>>) (i => i.Set<OutputCacheItemStatus>((Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => p.Status), (Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => newStatus)).Set<DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (p => p.DateModified), (Expression<Func<OutputCacheItem, DateTime>>) (p => DateTime.UtcNow))))))));
      }

      public virtual void UpdateStatusForItemsByCacheKeys(
        IEnumerable<string> keys,
        OutputCacheItemStatus newStatus)
      {
        // ISSUE: reference to a compiler-generated field
        this.ExecuteOnBatches<string>(keys, (System.Action<IEnumerable<string>>) (batch => this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch.Contains<string>(i.Key))).UpdateAll<OutputCacheItem>((Expression<Func<Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>, Telerik.OpenAccess.ExtensionMethods.UpdateDescription<OutputCacheItem>>>) (i => i.Set<OutputCacheItemStatus>((Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => p.Status), (Expression<Func<OutputCacheItem, OutputCacheItemStatus>>) (p => this.newStatus)).Set<DateTime>((Expression<Func<OutputCacheItem, DateTime>>) (p => p.DateModified), (Expression<Func<OutputCacheItem, DateTime>>) (p => DateTime.UtcNow))))));
      }

      public virtual void DeleteItemsByPageKeys(IEnumerable<string> pageKeys) => this.ExecuteOnBatches<string>(pageKeys, (System.Action<IEnumerable<string>>) (batch => this.ExecuteOnBatches<string>((IEnumerable<string>) this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch.Contains<string>(i.SiteMapNodeKey))).Select<OutputCacheItem, string>((Expression<Func<OutputCacheItem, string>>) (i => i.Key)).ToArray<string>(), (System.Action<IEnumerable<string>>) (batch2 =>
      {
        this.Context.GetAll<OutputCacheDependency>().Where<OutputCacheDependency>((Expression<Func<OutputCacheDependency, bool>>) (i => batch2.Contains<string>(i.CacheItem.Key) && (int) i.CacheItem.Status == 5)).DeleteAll();
        this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch2.Contains<string>(i.Key) && (int) i.Status == 5)).DeleteAll();
      }))));

      public virtual void DeleteExpiredItemsOlderThan(DateTime olderThanDate) => this.ExecuteOnBatches<string>((IEnumerable<string>) this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => (int) i.Status != 2 && i.DateModified < olderThanDate)).Select<OutputCacheItem, string>((Expression<Func<OutputCacheItem, string>>) (i => i.Key)).ToArray<string>(), (System.Action<IEnumerable<string>>) (batch =>
      {
        this.Context.GetAll<OutputCacheDependency>().Where<OutputCacheDependency>((Expression<Func<OutputCacheDependency, bool>>) (d => batch.Contains<string>(d.CacheItem.Key) && (int) d.CacheItem.Status != 2)).DeleteAll();
        this.Context.GetAll<OutputCacheItem>().Where<OutputCacheItem>((Expression<Func<OutputCacheItem, bool>>) (i => batch.Contains<string>(i.Key) && (int) i.Status != 2)).DeleteAll();
      }));

      protected SitefinityOAContext Context => this.context;

      protected void ExecuteOnBatches<T>(
        IEnumerable<T> items,
        System.Action<IEnumerable<T>> batchOperation,
        int pageSize = 100)
      {
        if (items == null || !items.Any<T>())
          return;
        int num = 0;
        while (true)
        {
          T[] array = items.Skip<T>(num * pageSize).Take<T>(pageSize).ToArray<T>();
          if (((IEnumerable<T>) array).Any<T>())
          {
            batchOperation((IEnumerable<T>) array);
            ++num;
          }
          else
            break;
        }
      }
    }

    private class NativeBatchSqlOperations : 
      OpenAccessOutputCacheRelationsProvider.BatchSqlOperations
    {
      private const string ValueListSeparator = ", ";

      public NativeBatchSqlOperations(SitefinityOAContext context)
        : base(context)
      {
      }

      public override void DeleteItemsByPageKeys(IEnumerable<string> pageKeys) => this.ExecuteOnBatches<string>(pageKeys, (System.Action<IEnumerable<string>>) (batch =>
      {
        string[] array = batch.ToArray<string>();
        int index = 0;
        List<DbParameter> parameters = new List<DbParameter>();
        IList<string> stringList = this.Context.ExecuteQuery<string>(string.Format("select {0} from {1} where {2} in ({3}) and {4} = {5}", (object) this.GetColumnName("key"), (object) this.GetTableName("sf_ocd_itms"), (object) this.GetColumnName("site_map_node_key"), (object) this.AddKeysInClauseParameters(array, ref parameters, ref index), (object) this.GetColumnName("status"), (object) this.AddStatusParameter(OutputCacheItemStatus.Deleted, ref parameters, ref index)), parameters.ToArray());
        if (!stringList.Any<string>())
          return;
        this.DeleteCacheItems((IEnumerable<string>) stringList);
      }), 1000);

      public override void UpdatePageVariationsStatus(
        IEnumerable<string> pageKeys,
        OutputCacheItemStatus oldStatus,
        OutputCacheItemStatus newStatus)
      {
        this.ExecuteOnBatches<string>(pageKeys, (System.Action<IEnumerable<string>>) (batch =>
        {
          string[] array = batch.ToArray<string>();
          int index = 0;
          List<DbParameter> parameters = new List<DbParameter>();
          IList<string> stringList = this.Context.ExecuteQuery<string>(string.Format("select {0} from {1} where {2} in ({3}) and {4} = {5} and {6} > {7}", (object) this.GetColumnName("key"), (object) this.GetTableName("sf_ocd_itms"), (object) this.GetColumnName("site_map_node_key"), (object) this.AddKeysInClauseParameters(array, ref parameters, ref index), (object) this.GetColumnName("status"), (object) this.AddStatusParameter(oldStatus, ref parameters, ref index), (object) this.GetColumnName("priority"), (object) this.AddIntParameter(50, ref parameters, ref index)), parameters.ToArray());
          if (!stringList.Any<string>())
            return;
          this.UpdateStatusForItemsByCacheKeys((IEnumerable<string>) stringList, newStatus);
        }), 1000);
      }

      public override void UpdateStatusForItemsByCacheKeys(
        IEnumerable<string> keys,
        OutputCacheItemStatus oldStatus,
        OutputCacheItemStatus newStatus)
      {
        this.ExecuteOnBatches<string>(keys, (System.Action<IEnumerable<string>>) (batch =>
        {
          int index = 0;
          List<DbParameter> parameters = new List<DbParameter>();
          this.Context.ExecuteNonQuery(string.Format("update {0} set {1} = {4}, {2} = {5} where {1} = {6} and {3} in ({7})", (object) this.GetTableName("sf_ocd_itms"), (object) this.GetColumnName("status"), (object) this.GetColumnName("date_modified"), (object) this.GetColumnName("key"), (object) this.AddStatusParameter(newStatus, ref parameters, ref index), (object) this.AddDateTimeParameter(DateTime.UtcNow, ref parameters, ref index), (object) this.AddStatusParameter(oldStatus, ref parameters, ref index), (object) this.AddKeysInClauseParameters(batch.ToArray<string>(), ref parameters, ref index)), parameters.ToArray());
          this.Context.SaveChanges();
        }), 1000);
      }

      public override void UpdateStatusForItemsByCacheKeys(
        IEnumerable<string> keys,
        OutputCacheItemStatus newStatus)
      {
        this.ExecuteOnBatches<string>(keys, (System.Action<IEnumerable<string>>) (batch =>
        {
          int index = 0;
          List<DbParameter> parameters = new List<DbParameter>();
          this.Context.ExecuteNonQuery(string.Format("update {0} set {1} = {4}, {2} = {5} where {3} in ({6})", (object) this.GetTableName("sf_ocd_itms"), (object) this.GetColumnName("status"), (object) this.GetColumnName("date_modified"), (object) this.GetColumnName("key"), (object) this.AddStatusParameter(newStatus, ref parameters, ref index), (object) this.AddDateTimeParameter(DateTime.UtcNow, ref parameters, ref index), (object) this.AddKeysInClauseParameters(batch.ToArray<string>(), ref parameters, ref index)), parameters.ToArray());
          this.Context.SaveChanges();
        }), 1000);
      }

      public override void DeleteExpiredItemsOlderThan(DateTime olderThanDate)
      {
        int index = 0;
        List<DbParameter> parameters = new List<DbParameter>();
        this.DeleteKeysBySql(this.GenerateOlderExpiredItemsSelectSqlScript(this.GetColumnName("key"), this.GetTableName("sf_ocd_itms"), this.GetColumnName("status"), this.GetColumnName("date_modified"), this.AddStatusParameter(OutputCacheItemStatus.Live, ref parameters, ref index), this.AddDateTimeParameter(olderThanDate, ref parameters, ref index)), parameters.ToArray());
      }

      protected virtual void DeleteCacheItems(IEnumerable<string> keys) => this.ExecuteOnBatches<string>(keys, (System.Action<IEnumerable<string>>) (batch =>
      {
        string[] array = batch.ToArray<string>();
        this.DeleteItems(this.Context, "sf_ocd_itms", "key", array);
        this.DeleteItems(this.Context, "sf_ocd_dpndncies", "cache_key", array);
        this.Context.SaveChanges();
      }), 1000);

      protected virtual int DeleteItems(
        SitefinityOAContext context,
        string table,
        string column,
        string[] keys)
      {
        if (keys.Length == 0)
          return 0;
        int index = 0;
        List<DbParameter> parameters = new List<DbParameter>();
        string commandText = string.Format("delete from {0} where {1} in ({2})", (object) this.GetTableName(table), (object) this.GetColumnName(column), (object) this.AddKeysInClauseParameters(keys, ref parameters, ref index));
        return context.ExecuteNonQuery(commandText, parameters.ToArray());
      }

      private void DeleteKeysBySql(string sql, params DbParameter[] parameters)
      {
        int num1 = 10;
        int num2;
        do
        {
          --num1;
          IList<string> source = this.Context.ExecuteQuery<string>(sql, parameters);
          if (source.Count != 0)
          {
            num2 = this.DeleteItems(this.Context, "sf_ocd_itms", "key", source.ToArray<string>()) + this.DeleteItems(this.Context, "sf_ocd_dpndncies", "cache_key", source.ToArray<string>());
            this.Context.SaveChanges();
          }
          else
            goto label_3;
        }
        while (num2 != 0 && num1 != 0);
        goto label_4;
label_3:
        return;
label_4:;
      }

      protected virtual string GetTableName(string tableName) => tableName;

      protected virtual string GetColumnName(string columnName) => columnName;

      protected virtual string GenerateOlderExpiredItemsSelectSqlScript(
        string keyColumnName,
        string tableName,
        string statusColumnName,
        string dateColumnName,
        string stausParamName,
        string dateParamName)
      {
        return string.Format("select {0} from {1} where {2} <> {4} and {3} < {5}", (object) keyColumnName, (object) tableName, (object) statusColumnName, (object) dateColumnName, (object) stausParamName, (object) dateParamName);
      }

      protected virtual string AddStatusParameter(
        OutputCacheItemStatus value,
        ref List<DbParameter> parameters,
        ref int index)
      {
        string parameterName = "@p" + (object) index;
        OAParameter oaParameter = new OAParameter(parameterName, (object) value);
        oaParameter.DbType = DbType.Int32;
        parameters.Add((DbParameter) oaParameter);
        ++index;
        return parameterName;
      }

      protected virtual string AddIntParameter(
        int value,
        ref List<DbParameter> parameters,
        ref int index)
      {
        string parameterName = "@p" + (object) index;
        OAParameter oaParameter = new OAParameter(parameterName, (object) value);
        oaParameter.DbType = DbType.Int32;
        parameters.Add((DbParameter) oaParameter);
        ++index;
        return parameterName;
      }

      protected virtual string AddDateTimeParameter(
        DateTime value,
        ref List<DbParameter> parameters,
        ref int index)
      {
        string parameterName = "@p" + (object) index;
        OAParameter oaParameter = new OAParameter(parameterName, (object) value);
        oaParameter.DbType = DbType.DateTime;
        parameters.Add((DbParameter) oaParameter);
        ++index;
        return parameterName;
      }

      protected virtual string AddKeysInClauseParameters(
        string[] values,
        ref List<DbParameter> parameters,
        ref int index)
      {
        StringBuilder stringBuilder = new StringBuilder(5 * values.Length);
        for (int index1 = 0; index1 < values.Length; ++index1)
        {
          string parameterName = "@p" + (object) (index + index1);
          OAParameter oaParameter = new OAParameter(parameterName, (object) values[index1]);
          oaParameter.DbType = DbType.String;
          oaParameter.Size = (int) byte.MaxValue;
          parameters.Add((DbParameter) oaParameter);
          stringBuilder.Append(parameterName).Append(", ");
        }
        index += values.Length;
        stringBuilder.Remove(stringBuilder.Length - ", ".Length, ", ".Length);
        return stringBuilder.ToString();
      }
    }

    private class MySqlNativeBatchSqlOperations : 
      OpenAccessOutputCacheRelationsProvider.NativeBatchSqlOperations
    {
      public MySqlNativeBatchSqlOperations(SitefinityOAContext context)
        : base(context)
      {
      }

      protected override string GetColumnName(string columnName) => string.Format("`{0}`", (object) columnName);

      protected override string GetTableName(string tableName) => string.Format("`{0}`", (object) tableName);

      protected override string GenerateOlderExpiredItemsSelectSqlScript(
        string keyColumnName,
        string tableName,
        string statusColumnName,
        string dateColumnName,
        string stausParamName,
        string dateParamName)
      {
        return string.Format("select {0} from {1} where {2} <> {4} and {3} < {5} limit 100", (object) keyColumnName, (object) tableName, (object) statusColumnName, (object) dateColumnName, (object) stausParamName, (object) dateParamName);
      }
    }

    private class MsSqlNativeBatchSqlOperations : 
      OpenAccessOutputCacheRelationsProvider.NativeBatchSqlOperations
    {
      public MsSqlNativeBatchSqlOperations(SitefinityOAContext context)
        : base(context)
      {
      }

      protected override string GetColumnName(string columnName) => string.Format("[{0}]", (object) columnName);

      protected override string GetTableName(string tableName) => string.Format("[{0}]", (object) tableName);

      protected override string GenerateOlderExpiredItemsSelectSqlScript(
        string keyColumnName,
        string tableName,
        string statusColumnName,
        string dateColumnName,
        string stausParamName,
        string dateParamName)
      {
        return string.Format("select top 100 {0} from {1} where {2} <> {4} and {3} < {5}", (object) keyColumnName, (object) tableName, (object) statusColumnName, (object) dateColumnName, (object) stausParamName, (object) dateParamName);
      }
    }

    private class OracleNativeBatchSqlOperations : 
      OpenAccessOutputCacheRelationsProvider.NativeBatchSqlOperations
    {
      public OracleNativeBatchSqlOperations(SitefinityOAContext context)
        : base(context)
      {
      }

      protected override string GetColumnName(string columnName) => string.Format("\"{0}\"", (object) columnName);

      protected override string GetTableName(string tableName) => string.Format("\"{0}\"", (object) tableName);

      protected override string GenerateOlderExpiredItemsSelectSqlScript(
        string keyColumnName,
        string tableName,
        string statusColumnName,
        string dateColumnName,
        string stausParamName,
        string dateParamName)
      {
        return string.Format("select {0} from {1} where {2} <> {4} and {3} < {5} and ROWNUM <= 100", (object) keyColumnName, (object) tableName, (object) statusColumnName, (object) dateColumnName, (object) stausParamName, (object) dateParamName);
      }
    }
  }
}
