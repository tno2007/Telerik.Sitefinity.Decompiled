// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryBuilder.OpenAccessQueryDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Data.QueryBuilder
{
  public class OpenAccessQueryDataProvider : 
    QueryDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates a new query</summary>
    /// <returns></returns>
    public override Query CreateQuery() => this.CreateQuery(this.GetNewGuid());

    /// <summary>Creates a new query with the specified ID</summary>
    /// <param name="pageId">ID for the query</param>
    /// <returns></returns>
    public override Query CreateQuery(Guid id)
    {
      Query entity = !(id == Guid.Empty) ? new Query(id, this.ApplicationName) : throw new ArgumentException("The id of the query cannot be empty");
      entity.LastModified = DateTime.UtcNow;
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Get a query by ID</summary>
    /// <param name="pageId">The ID of the query to get</param>
    /// <returns></returns>
    public override Query GetQuery(Guid id)
    {
      Query itemById = this.GetContext().GetItemById<Query>(id.ToString());
      ((IDataItem) itemById).Provider = (object) this;
      return itemById;
    }

    /// <summary>Get all queries</summary>
    /// <returns></returns>
    public override IQueryable<Query> GetQueries()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Query>((DataProviderBase) this).Where<Query>((Expression<Func<Query, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Gets queries for a specified persistent type</summary>
    /// <param name="persistentTypeName">The name of the persistent type</param>
    /// <returns></returns>
    public override IQueryable<Query> GetQueries(string persistentTypeName)
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Query>((DataProviderBase) this).Where<Query>((Expression<Func<Query, bool>>) (c => c.ApplicationName == appName && c.QueryData.PersistentTypeName == persistentTypeName));
    }

    /// <summary>Delete a query</summary>
    /// <param name="query">The query to delete</param>
    public override void DeleteQuery(Query query) => this.GetContext().Remove((object) query);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new QueryDataMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }
  }
}
