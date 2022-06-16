// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.OpenAccessUrlProviderDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// Represents content decorator for OpenAccess content data provider.
  /// </summary>
  public class OpenAccessUrlProviderDecorator : IUrlProviderDecorator, ICloneable
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    public virtual UrlDataProviderBase DataProvider { get; set; }

    internal virtual SitefinityOAContext Context => (SitefinityOAContext) this.DataProvider.GetTransaction();

    /// <summary>Creates new UrlData.</summary>
    /// <param name="urlType">Type of the URL.</param>
    /// <returns>The new UrlData object.</returns>
    public virtual UrlData CreateUrl(Type urlType) => this.CreateUrl(urlType, this.DataProvider.GetNewGuid());

    /// <summary>Creates new UrlData.</summary>
    /// <param name="urlType">Type of the URL.</param>
    /// <param name="pageId">The ID of the new UrlData.</param>
    /// <returns>The new UrlData object.</returns>
    public virtual UrlData CreateUrl(Type urlType, Guid id)
    {
      if (urlType == (Type) null)
        throw new ArgumentNullException(nameof (urlType));
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      UrlData instance = (UrlData) Activator.CreateInstance(urlType);
      instance.ApplicationName = this.DataProvider.ApplicationName;
      instance.Id = id;
      ((IDataItem) instance).Provider = (object) this.DataProvider;
      this.Context.Add((object) instance);
      return instance;
    }

    /// <summary>Creates new UrlData.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The new content item.</returns>
    public virtual T CreateUrl<T>() where T : UrlData, new() => this.CreateUrl<T>(this.DataProvider.GetNewGuid());

    /// <summary>Creates new UrlData with the specified ID.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageId">The ID of the new UrlData.</param>
    /// <returns>The new UrlData.</returns>
    public virtual T CreateUrl<T>(Guid id) where T : UrlData, new()
    {
      if (id == Guid.Empty)
        throw new ArgumentNullException(nameof (id));
      T obj = new T();
      obj.ApplicationName = this.DataProvider.ApplicationName;
      obj.Id = id;
      T entity = obj;
      entity.Provider = (object) this.DataProvider;
      this.Context.Add((object) entity);
      return entity;
    }

    /// <summary>Gets a UrlData with the specified ID.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A UrlData entry.</returns>
    public virtual T GetUrl<T>(Guid id) where T : UrlData
    {
      T url = !(id == Guid.Empty) ? this.Context.GetItemById<T>(id.ToString()) : throw new ArgumentNullException(nameof (id));
      url.Provider = (object) this.DataProvider;
      return url;
    }

    /// <summary>Gets a query for UrlData.</summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The query for UrlData.</returns>
    public virtual IQueryable<T> GetUrls<T>() where T : UrlData
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<T>((DataProviderBase) this.DataProvider).Where<T>((Expression<Func<T, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Gets a query for UrlData.</summary>
    /// <param name="urlType">The actual type of the UrlData.</param>
    /// <returns>The query for UrlData.</returns>
    public virtual IQueryable<UrlData> GetUrls(Type urlType)
    {
      string appName = this.DataProvider.ApplicationName;
      return SitefinityQuery.Get<UrlData>(urlType, (DataProviderBase) this.DataProvider).Where<UrlData>((Expression<Func<UrlData, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The url to delete.</param>
    public virtual void Delete(UrlData item) => this.Context.Remove((object) item);

    /// <summary>Parses the object identity.</summary>
    /// <param name="persistantType">The CLR type of the persistent object.</param>
    /// <param name="stringId">The string representation of the object identity.</param>
    /// <returns></returns>
    protected internal virtual IObjectId ParseObjectId(Type objectType, string stringId) => Database.OID.ParseObjectId(objectType, stringId);

    /// <summary>
    /// Creates a new object that is a copy of the current instance.
    /// </summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public object Clone() => this.MemberwiseClone();
  }
}
