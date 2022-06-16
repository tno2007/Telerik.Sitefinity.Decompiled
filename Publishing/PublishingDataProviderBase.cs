// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.PublishingDataProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicTypes.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Publishing.Model;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>
  /// Base class for data layer of Sitefinity's publishing system
  /// </summary>
  public abstract class PublishingDataProviderBase : DataProviderBase, IMultisiteEnabledProvider
  {
    private static Type[] knownTypes;

    /// <summary>
    /// Create a new publishing point and associated it with a random ID
    /// </summary>
    /// <returns>Publishing point in transaction.</returns>
    public abstract PublishingPoint CreatePublishingPoint();

    /// <summary>
    /// Create a new publishing point by explicitly specifying its ID
    /// </summary>
    /// <param name="id">ID of the publishing point that is to be created.</param>
    /// <returns>Publishing point in transaction.</returns>
    public abstract PublishingPoint CreatePublishingPoint(Guid id);

    /// <summary>Mark a publishing point for deleteion.</summary>
    /// <param name="publishingPoint">Publishing point to be deleted when the transaction is commited.</param>
    public abstract void DeletePublishingPoint(PublishingPoint publishingPoint);

    /// <summary>Get a specific publishing point by its ID</summary>
    /// <param name="publishingPointID">ID of the publishing point to retrieve</param>
    /// <returns>Instance of the publishing point with the specified ID</returns>
    /// <exception cref="!:InvalidDataException">
    /// When a publishing point with the specified ID cannot be found.
    /// </exception>
    public abstract PublishingPoint GetPublishingPoint(Guid publishingPointID);

    /// <summary>Retrieve all publishing points as a query</summary>
    /// <returns>Query of all publishing points.</returns>
    public abstract IQueryable<PublishingPoint> GetPublishingPoints();

    /// <summary>Create pipe settings with specific type</summary>
    /// <param name="settingsType">Type of pipe settings to created</param>
    /// <returns>Created pipe settings in transaction</returns>
    public abstract PipeSettings CreatePipeSettings(Type settingsType);

    /// <summary>Creates pipe settings of specific type</summary>
    /// <typeparam name="TPipeSettings">Type of the pipe settings to create.</typeparam>
    /// <returns>Created pipe settings in transaction</returns>
    public abstract TPipeSettings CreatePipeSettings<TPipeSettings>() where TPipeSettings : PipeSettings;

    /// <summary>Create pipe settings of speicific type and ID</summary>
    /// <param name="settingsType">Type of the pipe settings to create.</param>
    /// <param name="settingsID">Id to set to the newly created settings.</param>
    /// <returns>Pipe, created with a specific ID and of given type, which is added to a transaction.</returns>
    public abstract PipeSettings CreatePipeSettings(Type settingsType, Guid settingsID);

    /// <summary>Create pipe settings of speicific type and ID</summary>
    /// <typeparam name="TPipeSettings">Type of the pipe settings to create.</typeparam>
    /// <param name="settingsId">Id to set to the newly created settings.</param>
    /// <returns>Pipe, created with a specific ID and of given type, which is added to a transaction.</returns>
    public abstract TPipeSettings CreatePipeSettings<TPipeSettings>(Guid settingsId) where TPipeSettings : PipeSettings;

    public abstract PipeSettings GetPipeSettings(Type settingsType, Guid settingsID);

    /// <summary>Get pipe settings by name</summary>
    /// <param name="pipeName">Name of the pipe whose settings to retrieve</param>
    /// <returns>Settings of a specific pipe</returns>
    public abstract PipeSettings GetPipeSettings(string pipeName);

    /// <summary>Get pipe settings of a specific type and ID</summary>
    /// <typeparam name="TPipeSettings">Type of the pipe settings to retrieve.</typeparam>
    /// <param name="settingsID">Search settings with this ID</param>
    /// <returns>Pipe settings with specific type and ID</returns>
    public abstract TPipeSettings GetPipeSettings<TPipeSettings>(Guid settingsID) where TPipeSettings : PipeSettings;

    /// <summary>Get pipe settings of all types</summary>
    /// <returns>All pipe settings, regardless of type</returns>
    public abstract IQueryable<PipeSettings> GetPipeSettings();

    /// <summary>Get pipe settings of specific type</summary>
    /// <param name="settingsType">Get all pipe settings with this type</param>
    /// <returns>Get all pipe settings with a specific type</returns>
    public abstract IQueryable<PipeSettings> GetPipeSettings(Type settingsType);

    /// <summary>Get pipe settings of specific type</summary>
    /// <typeparam name="TPipeSettings">Get all pipe settings with this type</typeparam>
    /// <returns>Get all pipe settings with a specific type</returns>
    public abstract IQueryable<TPipeSettings> GetPipeSettings<TPipeSettings>() where TPipeSettings : PipeSettings;

    /// <summary>
    /// Mark pipe settings for deletion in the current transaction
    /// </summary>
    /// <param name="settings">Pipe settings to mark for deletion.</param>
    public abstract void DeletePipeSettings(PipeSettings settings);

    /// <summary>Create new mapping settings with random ID.</summary>
    /// <returns>Mapping settings in transaction.</returns>
    public abstract MappingSettings CreateMappingSettings();

    /// <summary>Create new mapping settings with a specified ID</summary>
    /// <param name="id">ID of the new mapping settings</param>
    /// <returns>Mapping settings in transaction</returns>
    public abstract MappingSettings CreateMappingSettings(Guid id);

    /// <summary>Retrieve mapping settings by querying for their ID</summary>
    /// <param name="id">ID of the mapping settings to retrieve</param>
    /// <returns>Unique instance of mapping settings with specified ID or exception if there is no mapping settings with this ID</returns>
    public abstract MappingSettings GetMappingSettings(Guid id);

    /// <summary>Retrieve all mapping settings</summary>
    /// <returns>Query for all mapping settings</returns>
    public abstract IQueryable<MappingSettings> GetMappingSettings();

    /// <summary>Mark mapping settings for deletion</summary>
    /// <param name="mappingSettings">Mapping settings to mark for deletion</param>
    public abstract void DeleteMappingSettings(MappingSettings mappingSettings);

    /// <summary>Creates a new mapping</summary>
    /// <returns>Mapping in transaction</returns>
    public abstract Mapping CreateMapping();

    /// <summary>Creates a new mapping with a specific ID</summary>
    /// <param name="id">ID of the item to create</param>
    /// <returns>Mapping in transaction</returns>
    public abstract Mapping CreateMapping(Guid id);

    /// <summary>Query for a mapping with a specific ID</summary>
    /// <param name="id">ID of the mapping to look for</param>
    /// <returns>Item with the specified ID or exception if there is no mapping with that ID</returns>
    public abstract Mapping GetMapping(Guid id);

    /// <summary>Retrieve all mappings</summary>
    /// <returns>Query object for all mappings</returns>
    public abstract IQueryable<Mapping> GetMappings();

    /// <summary>Mark a mapping for deletion</summary>
    /// <param name="mapping">Mapping to be marked for deletion</param>
    public abstract void DeleteMapping(Mapping mapping);

    /// <summary>Create a mapping translator with random ID</summary>
    /// <returns>Newly created item which is added to the transaction</returns>
    public abstract PipeMappingTranslation CreatePipeMappingTranslation();

    /// <summary>Create a mapping translator settings with specific ID</summary>
    /// <param name="id">ID of the new item</param>
    /// <returns>Newly created item which is added to the transaction</returns>
    public abstract PipeMappingTranslation CreatePipeMappingTranslation(
      Guid id);

    /// <summary>Get a mapping translator settings by ID</summary>
    /// <param name="id">ID of the mapping translator settings to retrieve</param>
    /// <returns>Instance from data source or exception</returns>
    public abstract PipeMappingTranslation GetPipeMappingTranslation(Guid id);

    /// <summary>
    /// Retrieves all mapping translator settings for this provider
    /// </summary>
    /// <returns>Query of all items in the current provider</returns>
    public abstract IQueryable<PipeMappingTranslation> GetPipeMappingTranslations();

    /// <summary>
    /// Removes <paramref name="toDelete" /> from transaction
    /// </summary>
    /// <param name="toDelete">Item to delete</param>
    public abstract void DeletePipeMappingTranslation(PipeMappingTranslation toDelete);

    /// <summary>Create new publishing point settings</summary>
    /// <returns>Publishing point settings in transaction</returns>
    public abstract PublishingPointSettings CreatePublishingPointSettings();

    /// <summary>Create new publishing point settings with specific ID</summary>
    /// <param name="id">ID to set the new instance</param>
    /// <returns>Publishing point settings with a specific ID</returns>
    public abstract PublishingPointSettings CreatePublishingPointSettings(
      Guid id);

    /// <summary>Query for publishing point settings with specific ID</summary>
    /// <param name="id">ID of the item to look for</param>
    /// <returns>Instance or exception if not found</returns>
    public abstract PublishingPointSettings GetPublishingPointSettings(
      Guid id);

    /// <summary>Retrieve all publishing point settings</summary>
    /// <returns>Query for all publishing point settings</returns>
    public abstract IQueryable<PublishingPointSettings> GetPublishingPointSettings();

    /// <summary>Mark publishing point settings for deletion</summary>
    /// <param name="settings">Settings to mark for deletion</param>
    public abstract void DeletePublishingPointSettings(PublishingPointSettings settings);

    /// <summary>Create new throttle settings</summary>
    /// <returns>Throttle settings in transaction</returns>
    public abstract ThrottleSettings CreateThrottleSettings();

    /// <summary>Create new throttle settings with specific id</summary>
    /// <param name="id">ID to set the the newly created throttle settings</param>
    /// <returns>Throttle settings in transaciton</returns>
    public abstract ThrottleSettings CreateThrottleSettings(Guid id);

    /// <summary>Query for throttle settings with a specific ID</summary>
    /// <param name="id">ID of the throttle settings to look for</param>
    /// <returns>Throttle settings with specific ID or exception if none is found</returns>
    public abstract ThrottleSettings GetThrottleSettings(Guid id);

    /// <summary>Retieve all throttle settings</summary>
    /// <returns>Query for all throttle settings</returns>
    public abstract IQueryable<ThrottleSettings> GetThrottleSettings();

    /// <summary>Mark throttle settings for deletion</summary>
    /// <param name="throttle">Throttle settings to mark for deletion</param>
    public abstract void DeleteThrottleSettings(ThrottleSettings throttle);

    /// <summary>Create an item of a specific type, if it is supported</summary>
    /// <param name="itemType">Type of the item</param>
    /// <param name="id">ID of the item to create</param>
    /// <returns>Created item</returns>
    /// <exception cref="T:System.ArgumentException">If <paramref name="itemType" /> is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == typeof (PublishingPoint) || itemType == typeof (IPublishingPoint))
        return (object) this.CreatePublishingPoint(id);
      if (typeof (PipeSettings).IsAssignableFrom(itemType))
        return (object) this.CreatePipeSettings(itemType, id);
      if (itemType == typeof (MappingSettings))
        return (object) this.CreateMappingSettings(id);
      if (itemType == typeof (Mapping))
        return (object) this.CreateMapping(id);
      if (itemType == typeof (PublishingPointSettings))
        return (object) this.CreatePublishingPointSettings(id);
      if (itemType == typeof (ThrottleSettings))
        return (object) this.CreateThrottleSettings(id);
      if (itemType == typeof (DynamicTypeBase))
        throw new NotSupportedException(Res.Get<PublishingMessages>().NoPublishingPoint);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>Get a queryable of items of the specified type</summary>
    /// <param name="itemType">Type to get queryables for.</param>
    /// <param name="filterExpression">Filter expression in Dynamic Linq.</param>
    /// <param name="orderExpression">Sorting expression in Dynamic Linq.</param>
    /// <param name="skip">Used for paging: how many items to skip from the start.</param>
    /// <param name="take">Used for paging: the maximum number of items to take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns>Queryable of items</returns>
    /// <remarks>
    /// Whether the result is queryable or not depends on the provider. However,
    /// Sitefinity's providers return IQueryable-s.
    /// </remarks>
    /// 
    ///             /// <exception cref="T:System.ArgumentException">If <paramref name="itemType" /> is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (PublishingPoint) || itemType == typeof (IPublishingPoint))
        return (IEnumerable) DataProviderBase.SetExpressions<PublishingPoint>(this.GetPublishingPoints(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PipeSettings).IsAssignableFrom(itemType))
        return (IEnumerable) this.GetPipeSettings(itemType);
      if (itemType == typeof (MappingSettings))
        return (IEnumerable) this.GetMappingSettings();
      if (itemType == typeof (Mapping))
        return (IEnumerable) this.GetMappings();
      if (itemType == typeof (PublishingPointSettings))
        return (IEnumerable) this.GetPublishingPointSettings();
      if (itemType == typeof (ThrottleSettings))
        return (IEnumerable) this.GetThrottleSettings();
      if (itemType == typeof (DynamicTypeBase))
        throw new NotSupportedException(Res.Get<PublishingMessages>().NoPublishingPoint);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <inheritcod />
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == typeof (PublishingPoint) || itemType == typeof (IPublishingPoint))
        return (object) this.GetPublishingPoint(id);
      return itemType == typeof (Mapping) ? (object) this.GetMapping(id) : base.GetItem(itemType, id);
    }

    /// <summary>Delete an item</summary>
    /// <param name="item">Item to delete</param>
    /// <exception cref="T:System.ArgumentException">If <paramref name="item" />'s type is not supported by this manager.</exception>
    /// <exception cref="T:System.NotSupportedException">If you try to create a dynamic type without specifying a publishing point.</exception>
    public override void DeleteItem(object item)
    {
      Type type = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      if (type == typeof (PublishingPoint) || type == typeof (IPublishingPoint))
        this.DeletePublishingPoint((PublishingPoint) item);
      else if (typeof (PipeSettings).IsAssignableFrom(type))
        this.DeletePipeSettings((PipeSettings) item);
      else if (type == typeof (MappingSettings))
        this.DeleteMappingSettings((MappingSettings) item);
      else if (type == typeof (Mapping))
        this.DeleteMapping((Mapping) item);
      else if (type == typeof (PublishingPointSettings))
        this.DeletePublishingPointSettings((PublishingPointSettings) item);
      else if (type == typeof (ThrottleSettings))
      {
        this.DeleteThrottleSettings((ThrottleSettings) item);
      }
      else
      {
        if (type == typeof (DynamicTypeBase))
          throw new NotSupportedException(Res.Get<PublishingMessages>().NoPublishingPoint);
        throw DataProviderBase.GetInvalidItemTypeException(type, this.GetKnownTypes());
      }
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>Array of Type-s that can be used in the generic IManager methods as arguments.</returns>
    public override Type[] GetKnownTypes()
    {
      if (PublishingDataProviderBase.knownTypes == null)
        PublishingDataProviderBase.knownTypes = new Type[9]
        {
          typeof (IPublishingPoint),
          typeof (PublishingPoint),
          typeof (DynamicTypeBase),
          typeof (PipeSettings),
          typeof (SitefinityContentPipeSettings),
          typeof (MappingSettings),
          typeof (Mapping),
          typeof (PublishingPointSettings),
          typeof (ThrottleSettings)
        };
      return PublishingDataProviderBase.knownTypes;
    }

    /// <inheritdoc />
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (PublishingPoint)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (p => p.Id == id)).FirstOrDefault<PublishingPoint>();
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (PublishingDataProviderBase);

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public abstract SiteItemLink CreateSiteItemLink();

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public abstract void Delete(SiteItemLink link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public abstract void DeleteLinksForItem(IDataItem item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<SiteItemLink> GetSiteItemLinks();

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public abstract SiteItemLink AddItemLink(Guid siteId, IDataItem item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public abstract IQueryable<T> GetSiteItems<T>(Guid siteId) where T : IDataItem;
  }
}
