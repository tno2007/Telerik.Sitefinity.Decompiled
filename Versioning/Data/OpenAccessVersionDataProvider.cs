// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Data.OpenAccessVersionDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Data
{
  /// <summary>
  /// Represents version control data provider that uses OpenAccess to store and retrieve version data.
  /// </summary>
  public class OpenAccessVersionDataProvider : 
    VersionDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider
  {
    /// <summary>Creates new trunk for versioned items.</summary>
    /// <returns>The new trunk.</returns>
    public override Trunk CreateTrunk() => this.CreateTrunk(this.GetNewGuid());

    /// <summary>Creates new trunk with the specified identity.</summary>
    /// <param name="pageId">The identity of the new trunk.</param>
    /// <returns>The new trunk.</returns>
    public override Trunk CreateTrunk(Guid id)
    {
      Trunk entity = !(id == Guid.Empty) ? new Trunk()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      } : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the trunk with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A content item.</returns>
    public override Trunk GetTrunk(Guid id)
    {
      Trunk trunk = !(id == Guid.Empty) ? this.GetContext().GetItemById<Trunk>(id.ToString()) : throw new ArgumentException("id cannot be empty GUID.");
      ((IDataItem) trunk).Provider = (object) this;
      return trunk;
    }

    /// <summary>Gets a query for trunk items.</summary>
    /// <returns>The query for trunk items.</returns>
    public override IQueryable<Trunk> GetTrunks()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Trunk>((DataProviderBase) this).Where<Trunk>((Expression<Func<Trunk, bool>>) (e => e.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The trunk to delete.</param>
    public override void Delete(Trunk item) => this.GetContext().Remove((object) item);

    /// <summary>
    /// Creates new versioned item for storing version changes.
    /// </summary>
    /// <returns>The new versioned item.</returns>
    public override Item CreateItem() => this.CreateItem(this.GetNewGuid());

    /// <summary>
    /// Creates new versioned item with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public override Item CreateItem(Guid id)
    {
      Item entity = !(id == Guid.Empty) ? new Item()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      } : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the versioned item with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A versioned item.</returns>
    public override Item GetItem(Guid id)
    {
      Item obj = !(id == Guid.Empty) ? this.GetContext().GetItemById<Item>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      ((IDataItem) obj).Provider = (object) this;
      return obj;
    }

    /// <summary>Gets a query for versioned items.</summary>
    /// <returns>The query for items.</returns>
    public override IQueryable<Item> GetItems()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Item>((DataProviderBase) this).Where<Item>((Expression<Func<Item, bool>>) (e => e.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(Item item) => this.GetContext().Remove((object) item);

    public override void CommitTransaction()
    {
      foreach (Item dirtyObject in (IEnumerable<Item>) this.GetContext().GetDirtyObjects<Item>())
        dirtyObject.LastModified = DateTime.UtcNow;
      base.CommitTransaction();
    }

    /// <summary>Applies the culture filter to the given query.</summary>
    /// <param name="query">The query to be filtered.</param>
    /// <param name="culture">The culture to be applied.</param>
    /// <returns></returns>
    public override IQueryable<Change> ApplyCultureFilter(
      IQueryable<Change> query,
      CultureInfo culture = null)
    {
      IQueryable<Change> source = query;
      if (this.IsMultilingual)
      {
        if (culture == null)
          culture = SystemManager.CurrentContext.Culture;
        int currentCultureId = AppSettings.CurrentSettings.GetCultureLcid(culture);
        int invariantCultureId = CultureInfo.InvariantCulture.LCID;
        if (SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Equals((object) culture))
          source = source.Where<Change>((Expression<Func<Change, bool>>) (c => c.Culture == currentCultureId || c.Culture == 0 || c.Culture == invariantCultureId));
        else
          source = source.Where<Change>((Expression<Func<Change, bool>>) (c => c.Culture == currentCultureId || c.Culture == 0));
      }
      return source;
    }

    /// <summary>Creates new change for a versioned item.</summary>
    /// <returns>The new versioned item.</returns>
    public override Change CreateChange() => this.CreateChange(this.GetNewGuid());

    /// <summary>Creates new change with the specified identity.</summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public override Change CreateChange(Guid id)
    {
      if (id == Guid.Empty)
        throw new ArgumentException("Id cannot be an Empty Guid");
      Change entity = new Change()
      {
        ApplicationName = this.ApplicationName,
        Id = id,
        Culture = SystemManager.CurrentContext.Culture.LCID
      };
      entity.Owner = SecurityManager.GetCurrentUserId();
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets the change item with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A change item.</returns>
    public override Change GetChange(Guid id)
    {
      Change change = !(id == Guid.Empty) ? this.GetContext().GetItemById<Change>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      ((IDataItem) change).Provider = (object) this;
      return change;
    }

    /// <summary>Gets a query for change items.</summary>
    /// <returns>The query for items.</returns>
    public override IQueryable<Change> GetChanges()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Change>((DataProviderBase) this).Where<Change>((Expression<Func<Change, bool>>) (e => e.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(Change item) => this.GetContext().Remove((object) item);

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class.
    /// </summary>
    /// <returns></returns>
    public override SerializationInfo CreateSerializationInfo() => this.CreateSerializationInfo(this.GetNewGuid());

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public override SerializationInfo CreateSerializationInfo(Guid id)
    {
      SerializationInfo entity = !(id == Guid.Empty) ? new SerializationInfo()
      {
        ApplicationName = this.ApplicationName,
        Id = id
      } : throw new ArgumentException("Id cannot be an Empty Guid");
      ((IDataItem) entity).Provider = (object) this;
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class.</returns>
    public override SerializationInfo GetSerializationInfo(Guid id)
    {
      SerializationInfo serializationInfo = !(id == Guid.Empty) ? this.GetContext().GetItemById<SerializationInfo>(id.ToString()) : throw new ArgumentException("Id cannot be Empty Guid");
      ((IDataItem) serializationInfo).Provider = (object) this;
      return serializationInfo;
    }

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> classes.
    /// </summary>
    /// <returns>The query for items.</returns>
    public override IQueryable<SerializationInfo> GetSerializationInfo()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<SerializationInfo>((DataProviderBase) this).Where<SerializationInfo>((Expression<Func<SerializationInfo, bool>>) (e => e.ApplicationName == appName));
    }

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public override void Delete(SerializationInfo item) => this.GetContext().Remove((object) item);

    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new VersioningMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the dependency with the specified key.</summary>
    /// <param name="key">The identity to search for.</param>
    /// <param name="type">The type.</param>
    /// <returns>A dependency item.</returns>
    /// <exception cref="T:System.ArgumentException">Key cannot be empty.</exception>
    protected internal override Dependency GetDependency(string key, Type type)
    {
      if (string.IsNullOrEmpty(key))
        throw new ArgumentException("Key cannot be empty.");
      Dependency dependency = this.GetContext().GetDirtyObjects<Dependency>().FirstOrDefault<Dependency>((Func<Dependency, bool>) (d => d.Key == key && d.CleanUpTaskType == type.FullName));
      if (dependency == null)
        dependency = this.GetContext().GetAll<Dependency>().FirstOrDefault<Dependency>((Expression<Func<Dependency, bool>>) (d => d.Key == key && d.CleanUpTaskType == type.FullName));
      return dependency;
    }

    /// <summary>Creates new dependency with the specified key.de</summary>
    /// <param name="key">The key of the new dependency.</param>
    /// <param name="type">The type.</param>
    /// <returns>The new dependency.</returns>
    /// <exception cref="T:System.ArgumentException">Key cannot be empty.</exception>
    /// <exception cref="T:System.ArgumentNullException">type</exception>
    protected internal override Dependency CreateDependency(string key, Type type)
    {
      if (string.IsNullOrEmpty(key))
        throw new ArgumentException("Key cannot be empty.");
      if (type == (Type) null)
        throw new ArgumentNullException(nameof (type));
      Dependency entity = new Dependency()
      {
        Id = this.GetNewGuid(),
        CleanUpTaskType = type.FullName,
        Key = key
      };
      this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Deletes the dependency.</summary>
    /// <param name="dependency">The dependency.</param>
    /// <exception cref="T:System.ArgumentNullException">dependency</exception>
    protected internal override void DeleteDependency(Dependency dependency)
    {
      if (dependency == null)
        throw new ArgumentNullException(nameof (dependency));
      this.GetContext().Remove((object) dependency);
    }

    /// <summary>Gets the dependencies.</summary>
    /// <returns>Collection of dependencies.</returns>
    protected internal override IQueryable<Dependency> GetDependencies() => this.GetContext().GetAll<Dependency>();
  }
}
