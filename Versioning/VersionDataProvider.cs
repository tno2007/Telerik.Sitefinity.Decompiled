// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.VersionDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Serialization.Formatters;
using Telerik.Sitefinity.Versioning.Serialization.Interfaces;

namespace Telerik.Sitefinity.Versioning
{
  /// <summary>
  /// Represents base class for Version Control data providers.
  /// </summary>
  public abstract class VersionDataProvider : DataProviderBase
  {
    private string formatterTypeName;
    private ISitefinityFormatter formatter;
    public const string changeType_Add = "add";
    public const string changeType_Modify = "edit";
    public const string changeType_Publish = "publish";
    public const string changeType_Delete = "delete";

    /// <summary>
    /// Specifies whether the site is running in multilingual mode
    /// </summary>
    protected virtual bool IsMultilingual => true;

    /// <summary>Gets the default application settings information.</summary>
    protected virtual IAppSettings AppSettings => (IAppSettings) Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;

    /// <summary>
    /// Creates new version for the provided object using its identity.
    /// </summary>
    /// <param name="versionedItem">The versioned item.</param>
    public virtual Change CreateVersion(IDataItem versionedItem, bool isPublished)
    {
      if (versionedItem == null)
        throw new ArgumentNullException(nameof (versionedItem));
      return this.CreateVersion((object) versionedItem, versionedItem.Id, isPublished);
    }

    /// <summary>
    /// Creates new version for the provided object using the specified identity.
    /// The item is added to the specified trunk.
    /// If a trunk with the specified name doesn't exist an
    /// exception of type <see cref="T:System.InvalidOperationException" /> is thrown.
    /// </summary>
    /// <param name="versionedItem">The versioned item.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="isPublished">if set to <c>true</c> [is published].</param>
    /// <returns></returns>
    public virtual Change CreateVersion(object versionedItem, Guid itemId, bool isPublished)
    {
      if (versionedItem == null)
        throw new ArgumentNullException(nameof (versionedItem));
      if (itemId == Guid.Empty)
        throw new ArgumentNullException(nameof (itemId));
      Item obj1 = this.GetItems().Where<Item>((Expression<Func<Item, bool>>) (i => i.Id == itemId)).FirstOrDefault<Item>();
      if (obj1 == null)
      {
        try
        {
          obj1 = this.GetItem(itemId);
        }
        catch (ItemNotFoundException ex)
        {
        }
      }
      bool flag1 = false;
      DateTime utcNow;
      if (obj1 == null)
      {
        obj1 = this.CreateItem(itemId);
        obj1.TypeName = versionedItem.GetType().FullName;
        obj1.Trunk = this.GetDefaultTrunk();
        Item obj2 = obj1;
        utcNow = DateTime.UtcNow;
        DateTime universalTime = utcNow.ToUniversalTime();
        obj2.LastModified = universalTime;
        flag1 = true;
      }
      Change change1 = this.CreateChange();
      change1.IsPublishedVersion = isPublished;
      change1.Data = this.Compress(this.Serialize(versionedItem));
      change1.ChangeType = !change1.IsPublishedVersion ? (flag1 ? "add" : "edit") : "publish";
      int cultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture);
      ILocalizableSerialization localizableSerialization = versionedItem as ILocalizableSerialization;
      bool flag2 = true;
      if (localizableSerialization != null)
      {
        obj1.IsSyncedItem = localizableSerialization.GetStrategy() == LocalizationStrategy.Synced;
        flag2 = localizableSerialization.GetStrategy() == LocalizationStrategy.Split;
        CultureInfo[] array = ((IEnumerable<CultureInfo>) localizableSerialization.GetCultures()).ToArray<CultureInfo>();
        CultureInfo[] cultureInfoArray;
        if (!flag2 && array.Length != 0)
          cultureInfoArray = array;
        else
          cultureInfoArray = new CultureInfo[1]
          {
            SystemManager.CurrentContext.Culture
          };
        CultureInfo[] cultures = cultureInfoArray;
        change1.SetMetadataFromLanguages((IList<CultureInfo>) cultures);
      }
      int fullVersion = 0;
      IQueryable<Change> queryable = obj1.Changes.AsQueryable<Change>();
      if (flag2)
        queryable = this.ApplyCultureFilter(queryable);
      Change change2 = queryable.OrderByDescending<Change, int>((Expression<Func<Change, int>>) (c => c.Version)).FirstOrDefault<Change>();
      if (change2 != null)
        fullVersion = change2.Version;
      if (isPublished)
      {
        change1.Version = VersionDataProvider.IncrementMajorVersion(fullVersion);
        obj1.LastPublishedChange = change1;
      }
      else
        change1.Version = VersionDataProvider.IncrementMinorVersion(fullVersion);
      change1.Parent = obj1;
      obj1.LastVersionNumber = change1.Version;
      change1.Culture = cultureLcid;
      change1.SerializationInfo = this.GetActiveSerializationInfo();
      Change change3 = change1;
      utcNow = DateTime.UtcNow;
      DateTime universalTime1 = utcNow.ToUniversalTime();
      change3.LastModified = universalTime1;
      if (versionedItem is IHasVersionDependency hasVersionDependencyItem)
        this.CreateDependencies(change1, hasVersionDependencyItem);
      return change1;
    }

    /// <summary>Gets the active serialization info.</summary>
    /// <returns></returns>
    public virtual SerializationInfo GetActiveSerializationInfo()
    {
      string frmtrName = this.formatterTypeName;
      SerializationInfo serializationInfo = this.GetSerializationInfo().Where<SerializationInfo>((Expression<Func<SerializationInfo, bool>>) (i => i.Formatter == frmtrName)).FirstOrDefault<SerializationInfo>();
      if (serializationInfo == null)
      {
        serializationInfo = this.CreateSerializationInfo();
        serializationInfo.Formatter = this.formatterTypeName;
      }
      return serializationInfo;
    }

    /// <summary>Gets the latest version of the specified item.</summary>
    /// <param name="itemId">The item identity.</param>
    /// <returns></returns>
    public virtual void GetLatestVersion(object targetObject, Guid itemId)
    {
      Change change = this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.Id == itemId)).OrderByDescending<Change, int>((Expression<Func<Change, int>>) (c => c.Version)).Take<Change>(1).First<Change>();
      this.Deserialize(targetObject, this.Uncompress(change.Data), change.SerializationInfo);
      this.ApplyChange(targetObject, change);
    }

    /// <summary>Gets the specified version of the specified item.</summary>
    /// <param name="itemId">The item identity.</param>
    /// <param name="versionNumber">The version number.</param>
    /// <returns></returns>
    public virtual void GetSpecificVersion(object targetObject, Guid itemId, int versionNumber)
    {
      IQueryable<Change> queryable = this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Parent.Id == itemId && c.Version == versionNumber));
      if (this.ShouldApplyCultureFilter(itemId))
        queryable = this.ApplyCultureFilter(queryable);
      Change change = queryable.Single<Change>();
      this.Deserialize(targetObject, this.Uncompress(change.Data), change.SerializationInfo);
      this.ApplyChange(targetObject, change);
    }

    /// <summary>Gets specific version by change ID.</summary>
    /// <param name="targetObject">The target object.</param>
    /// <param name="changeId">The change id.</param>
    public virtual void GetSpecificVersionByChangeId(object targetObject, Guid changeId)
    {
      Change change = this.GetChange(changeId);
      this.Deserialize(targetObject, this.Uncompress(change.Data), change.SerializationInfo);
      this.ApplyChange(targetObject, change);
    }

    /// <summary>
    /// Deletes all changes with version number smaller or equal to the specified number.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    /// <param name="versionNumber">The version number.</param>
    public virtual void TruncateVersions(Guid itemId, int versionNumber)
    {
      IQueryable<Change> changes = this.GetChanges();
      Expression<Func<Change, bool>> predicate = (Expression<Func<Change, bool>>) (c => c.Parent.Id == itemId && c.Version <= versionNumber);
      foreach (Change change in (IEnumerable<Change>) changes.Where<Change>(predicate))
        this.Delete(change);
    }

    /// <summary>
    /// Deletes all changes with dates older or equal to the specified date.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    /// <param name="date">The date.</param>
    public virtual void TruncateVersions(Guid itemId, DateTime date)
    {
      IQueryable<Change> changes = this.GetChanges();
      Expression<Func<Change, bool>> predicate = (Expression<Func<Change, bool>>) (c => c.Parent.Id == itemId && c.LastModified <= date);
      foreach (Change change in (IEnumerable<Change>) changes.Where<Change>(predicate))
        this.Delete(change);
    }

    /// <summary>
    /// Permanently removes an item form its trunk and all associated changes.
    /// </summary>
    /// <param name="itemId">The item pageId.</param>
    public virtual void DeleteItem(Guid itemId) => this.Delete(this.GetItem(itemId));

    /// <summary>Serializes the specified versioned item.</summary>
    /// <param name="versionedItem">The versioned item.</param>
    /// <returns></returns>
    /// 
    ///             TODO: make protected when JustMock is able to mock non-public members
    public virtual byte[] Serialize(object versionedItem)
    {
      using (MemoryStream serializationStream = new MemoryStream())
      {
        this.ConfigureLocalizableFormatter(ref this.formatter);
        this.formatter.Serialize((Stream) serializationStream, versionedItem);
        return serializationStream.ToArray();
      }
    }

    /// <summary>Deserializes the specified data.</summary>
    /// <param name="targetObject">The target object to deserialize into.</param>
    /// <param name="data">The serialized data.</param>
    /// <param name="info">The info.</param>
    /// <returns></returns>
    /// 
    ///             TODO: make protected when JustMock is able to mock non-public members
    public virtual void Deserialize(object targetObject, byte[] data, SerializationInfo info)
    {
      ISitefinityFormatter frmtr = !(info.Formatter == this.formatterTypeName) ? VersionDataProvider.GetFormatterByName(info.Formatter) : this.formatter;
      this.ConfigureLocalizableFormatter(ref frmtr);
      using (MemoryStream serializationStream = new MemoryStream(data))
        frmtr.Deserialize((Stream) serializationStream, targetObject);
    }

    /// <summary>Compresses the specified data.</summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    /// 
    ///             TODO: make protected when JustMock is able to mock non-public members
    public virtual byte[] Compress(byte[] data) => data;

    /// <summary>Uncompress's the specified data.</summary>
    /// <param name="data">The data.</param>
    /// 
    ///             TODO: make protected when JustMock is able to mock non-public members
    public virtual byte[] Uncompress(byte[] data) => data;

    /// <summary>Applies the culture filter to the given query.</summary>
    /// <param name="query">The query to be filtered.</param>
    /// <param name="culture">The culture to be applied.</param>
    /// <returns></returns>
    public virtual IQueryable<Change> ApplyCultureFilter(
      IQueryable<Change> query,
      CultureInfo culture = null)
    {
      return query;
    }

    internal bool ShouldApplyCultureFilter(Guid itemId)
    {
      Item obj = this.GetItems().FirstOrDefault<Item>((Expression<Func<Item, bool>>) (x => x.Id == itemId));
      if (obj == null)
        return true;
      return obj != null && !obj.IsSyncedItem;
    }

    /// <summary>Creates new trunk for versioned items.</summary>
    /// <returns>The new trunk.</returns>
    public abstract Trunk CreateTrunk();

    /// <summary>Creates new trunk with the specified identity.</summary>
    /// <param name="pageId">The identity of the new trunk.</param>
    /// <returns>The new trunk.</returns>
    public abstract Trunk CreateTrunk(Guid id);

    /// <summary>Gets the trunk with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A content item.</returns>
    public abstract Trunk GetTrunk(Guid id);

    /// <summary>Gets a query for trunk items.</summary>
    /// <returns>The query for trunk items.</returns>
    public abstract IQueryable<Trunk> GetTrunks();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The trunk to delete.</param>
    public abstract void Delete(Trunk item);

    /// <summary>
    /// Creates new versioned item for storing version changes.
    /// </summary>
    /// <returns>The new versioned item.</returns>
    public abstract Item CreateItem();

    /// <summary>
    /// Creates new versioned item with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public abstract Item CreateItem(Guid id);

    /// <summary>Gets the versioned item with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A versioned item.</returns>
    public abstract Item GetItem(Guid id);

    /// <summary>Gets a query for versioned items.</summary>
    /// <returns>The query for items.</returns>
    public abstract IQueryable<Item> GetItems();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(Item item);

    /// <summary>Gets the default trunk.</summary>
    /// <returns></returns>
    /// 
    ///             TODO: make protected when JustMock is able to mock non-public members
    public virtual Trunk GetDefaultTrunk()
    {
      string mainTrunkName = "default";
      Trunk defaultTrunk = this.GetTrunks().Where<Trunk>((Expression<Func<Trunk, bool>>) (t => t.Name == mainTrunkName)).FirstOrDefault<Trunk>();
      if (defaultTrunk == null)
      {
        IList dirtyItems = this.GetDirtyItems();
        if (dirtyItems != null)
          defaultTrunk = dirtyItems.OfType<Trunk>().SingleOrDefault<Trunk>((Func<Trunk, bool>) (tr => tr.Name == mainTrunkName));
      }
      if (defaultTrunk == null)
      {
        defaultTrunk = this.CreateTrunk();
        defaultTrunk.Name = mainTrunkName;
        defaultTrunk.ApplicationName = this.ApplicationName;
      }
      return defaultTrunk;
    }

    /// <summary>Creates new change for a versioned item.</summary>
    /// <returns>The new versioned item.</returns>
    public abstract Change CreateChange();

    /// <summary>Creates new change with the specified identity.</summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public abstract Change CreateChange(Guid id);

    /// <summary>Gets the change item with the specified identity.</summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>A change item.</returns>
    public abstract Change GetChange(Guid id);

    /// <summary>Gets a query for change items.</summary>
    /// <returns>The query for items.</returns>
    public abstract IQueryable<Change> GetChanges();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(Change item);

    protected internal abstract Dependency GetDependency(string key, Type type);

    protected internal abstract Dependency CreateDependency(string key, Type type);

    protected internal abstract void DeleteDependency(Dependency dependency);

    protected internal abstract IQueryable<Dependency> GetDependencies();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class.
    /// </summary>
    /// <returns></returns>
    public abstract SerializationInfo CreateSerializationInfo();

    /// <summary>
    /// Creates new <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity of the new item.</param>
    /// <returns>The new item.</returns>
    public abstract SerializationInfo CreateSerializationInfo(Guid id);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class with the specified identity.
    /// </summary>
    /// <param name="pageId">The identity to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> class.</returns>
    public abstract SerializationInfo GetSerializationInfo(Guid id);

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Versioning.Model.SerializationInfo" /> classes.
    /// </summary>
    /// <returns>The query for items.</returns>
    public abstract IQueryable<SerializationInfo> GetSerializationInfo();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(SerializationInfo item);

    /// <summary>Initializes the specified provider name.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="config">The config.</param>
    /// <param name="managerType">Type of the manager.</param>
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType)
    {
      base.Initialize(providerName, config, managerType);
      this.formatterTypeName = config["formatter"];
      if (string.IsNullOrEmpty(this.formatterTypeName))
      {
        this.formatter = (ISitefinityFormatter) new SitefinityBinaryFormatter();
        this.formatterTypeName = typeof (SitefinityBinaryFormatter).AssemblyQualifiedName;
      }
      else
        this.formatter = VersionDataProvider.GetFormatterByName(this.formatterTypeName);
      this.ConfigureLocalizableFormatter(ref this.formatter);
      config.Remove("formatter");
    }

    /// <inheritdoc />
    protected override bool InitializeDefaultData()
    {
      bool flag = base.InitializeDefaultData();
      string mainTrunkName = "default";
      if (this.GetTrunks().Where<Trunk>((Expression<Func<Trunk, bool>>) (t => t.Name == mainTrunkName)).SingleOrDefault<Trunk>() == null)
      {
        Trunk trunk = this.CreateTrunk((this.ApplicationName + mainTrunkName).GenerateGuid());
        trunk.Name = mainTrunkName;
        trunk.ApplicationName = this.ApplicationName;
        flag = true;
      }
      string frmtrName = this.formatterTypeName;
      if (this.GetSerializationInfo().Where<SerializationInfo>((Expression<Func<SerializationInfo, bool>>) (i => i.Formatter == frmtrName)).FirstOrDefault<SerializationInfo>() == null)
      {
        this.CreateSerializationInfo().Formatter = this.formatterTypeName;
        flag = true;
      }
      return flag;
    }

    /// <summary>Gets a serialization formatter by the specified name.</summary>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    internal static ISitefinityFormatter GetFormatterByName(string name) => name.IndexOf(".") != -1 ? (ISitefinityFormatter) Activator.CreateInstance(TypeResolutionService.ResolveType(name)) : ObjectFactory.Resolve<ISitefinityFormatter>(name);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == typeof (Trunk))
        return (object) this.CreateTrunk(id);
      if (itemType == typeof (Item))
        return (object) this.CreateItem(id);
      if (itemType == typeof (Change))
        return (object) this.CreateChange(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == typeof (Trunk))
        return (object) this.GetTrunk(id);
      if (itemType == typeof (Item))
        return (object) this.GetItem(id);
      if (itemType == typeof (Change))
        return (object) this.GetChange(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == typeof (Trunk))
        return (object) this.GetTrunks().Where<Trunk>((Expression<Func<Trunk, bool>>) (t => t.Id == id)).FirstOrDefault<Trunk>();
      if (itemType == typeof (Item))
        return (object) this.GetItems().Where<Item>((Expression<Func<Item, bool>>) (i => i.Id == id)).FirstOrDefault<Item>();
      if (!(itemType == typeof (Change)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (c => c.Id == id)).FirstOrDefault<Change>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (Trunk))
        return (IEnumerable) DataProviderBase.SetExpressions<Trunk>(this.GetTrunks(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Item))
        return (IEnumerable) DataProviderBase.SetExpressions<Item>(this.GetItems(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Change))
        return (IEnumerable) DataProviderBase.SetExpressions<Change>(this.GetChanges(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      switch (item)
      {
        case null:
          throw new ArgumentNullException(nameof (item));
        case Trunk _:
          this.Delete((Trunk) item);
          break;
        case Item _:
          this.Delete((Item) item);
          break;
        case Change _:
          this.Delete((Change) item);
          break;
        default:
          throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      }
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (VersionDataProvider);

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[4]
    {
      typeof (Trunk),
      typeof (Item),
      typeof (Change),
      typeof (SerializationInfo)
    };

    /// <summary>Gets the major version number.</summary>
    /// <param name="fullVersion">The full version.</param>
    /// <returns></returns>
    public static int GetMajorVersionNumber(int fullVersion) => fullVersion / 10000;

    /// <summary>Gets the minor version number.</summary>
    /// <param name="fullVersion">The full version.</param>
    /// <returns></returns>
    public static int GetMinorVersionNumber(int fullVersion) => fullVersion % 10000;

    /// <summary>Builds the full version number.</summary>
    /// <param name="majorNumber">The major number.</param>
    /// <param name="minorNumber">The minor number.</param>
    /// <returns></returns>
    public static int BuildFullVersionNumber(int majorNumber, int minorNumber) => majorNumber * 10000 + minorNumber;

    /// <summary>Increments the major version.</summary>
    /// <param name="fullVersion">The full version.</param>
    /// <returns></returns>
    public static int IncrementMajorVersion(int fullVersion) => VersionDataProvider.BuildFullVersionNumber(VersionDataProvider.GetMajorVersionNumber(fullVersion) + 1, 0);

    /// <summary>Increments the minor version.</summary>
    /// <param name="fullVersion">The full version.</param>
    /// <returns></returns>
    public static int IncrementMinorVersion(int fullVersion) => VersionDataProvider.BuildFullVersionNumber(VersionDataProvider.GetMajorVersionNumber(fullVersion), VersionDataProvider.GetMinorVersionNumber(fullVersion) + 1);

    /// <summary>
    /// Builds the UI friendly version number,e.g. 100001 will become 1.1, 200120 will become 2.120
    /// </summary>
    /// <param name="fullVersion">The full version.</param>
    /// <returns></returns>
    public static string BuildUIVersionNumber(int fullVersion) => VersionDataProvider.GetMajorVersionNumber(fullVersion).ToString() + "." + (object) VersionDataProvider.GetMinorVersionNumber(fullVersion);

    /// <summary>Creates a user friendly change description.</summary>
    public static string GetChangeDescription(Change change) => VersionDataProvider.GetChangeDescription(change, false);

    internal static string GetChangeDescription(Change change, bool includeCulture)
    {
      VersionResources versionResources = Res.Get<VersionResources>();
      string changeDescription = change.ChangeType;
      if (change.IsLastPublishedVersion)
        changeDescription = change.Metadata.IsNullOrEmpty() ? versionResources.LastPublished : Res.Get<PageResources>().Published;
      else if (change.IsPublishedVersion)
        changeDescription = versionResources.PreviouslyPublished;
      else if (change.ChangeType == "add")
        changeDescription = versionResources.InitialDraft;
      if (change.ChangeType == "edit")
        changeDescription = versionResources.Draft;
      if (includeCulture && change.Culture != CultureInfo.InvariantCulture.LCID)
        changeDescription = changeDescription + " | from " + Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(change.Culture).Name.ToUpperInvariant();
      return changeDescription;
    }

    private void ConfigureLocalizableFormatter(ref ISitefinityFormatter frmtr)
    {
      if (!(frmtr is ILocalizableSitefinityFormatter))
        return;
      ILocalizableSitefinityFormatter sitefinityFormatter = frmtr as ILocalizableSitefinityFormatter;
      sitefinityFormatter.PersistSpecificCulture = true;
      sitefinityFormatter.CultureNameToPersist = SystemManager.CurrentContext.Culture.Name;
    }

    private void CreateDependencies(Change change, IHasVersionDependency hasVersionDependencyItem)
    {
      foreach (IDependentItem dependentItem in (IEnumerable<IDependentItem>) hasVersionDependencyItem.GetDependencies().Where<IDependentItem>((Func<IDependentItem, bool>) (di => di.Culture == change.Culture)).GroupBy<IDependentItem, string>((Func<IDependentItem, string>) (di => di.Key)).Select<IGrouping<string, IDependentItem>, IDependentItem>((Func<IGrouping<string, IDependentItem>, IDependentItem>) (g => g.First<IDependentItem>())).ToList<IDependentItem>())
      {
        if (dependentItem.CleanUpTaskType == (Type) null)
          throw new InvalidOperationException("Dependency CleanUpTaskType cannot be null.");
        Dependency dependency = this.GetDependency(dependentItem.Key, dependentItem.CleanUpTaskType);
        if (dependency == null)
        {
          dependency = this.CreateDependency(dependentItem.Key, dependentItem.CleanUpTaskType);
          dependency.Data = dependentItem.GetData();
        }
        change.Dependencies.Add(dependency);
      }
    }

    private void ApplyChange(object targetObject, Change change)
    {
      if (!(targetObject is IHasVersionDependency versionDependency))
        return;
      IEnumerable<DependentItem> dependencies = change.Dependencies.Select<Dependency, DependentItem>((Func<Dependency, DependentItem>) (d => new DependentItem(d)));
      int version = change.Version;
      versionDependency.SetDependencies((IEnumerable<IDependentItem>) dependencies, version);
    }
  }
}
