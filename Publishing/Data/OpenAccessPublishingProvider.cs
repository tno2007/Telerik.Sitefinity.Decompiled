// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Data.OpenAccessPublishingProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent.Data;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Publishing.Data
{
  /// <summary>Implements publishing provider via OpenAccess</summary>
  [ContentProviderDecorator(typeof (OpenAccessContentDecorator))]
  public class OpenAccessPublishingProvider : 
    PublishingDataProviderBase,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOpenAccessUpgradableProvider,
    IMultisiteEnabledOAProvider,
    IDataEventProvider
  {
    private static Assembly[] persistentAssemblies;

    /// <summary>
    /// Create a new publishing point and associated it with a random ID
    /// </summary>
    /// <returns>Publishing point in transaction.</returns>
    public override PublishingPoint CreatePublishingPoint() => this.CreatePublishingPoint(this.GetNewGuid());

    /// <summary>
    /// Create a new publishing point by explicitly specifying its ID
    /// </summary>
    /// <param name="id">ID of the publishing point that is to be created.</param>
    /// <returns>Publishing point in transaction.</returns>
    public override PublishingPoint CreatePublishingPoint(Guid id)
    {
      PublishingPoint publishingPoint = new PublishingPoint();
      publishingPoint.Id = id;
      publishingPoint.Owner = SecurityManager.GetCurrentUserId();
      publishingPoint.Settings = this.CreatePublishingPointSettings();
      this.SetupDataItemForCreation((IDataItem) publishingPoint);
      if (SystemManager.CurrentContext.CurrentSite.Id != Guid.Empty)
        this.AddItemLink(SystemManager.CurrentContext.CurrentSite.Id, (IDataItem) publishingPoint);
      return publishingPoint;
    }

    /// <summary>Mark a publishing point for deleteion</summary>
    /// <param name="publishingPoint">Publishing point to be deleted when the transaction is commited.</param>
    public override void DeletePublishingPoint(PublishingPoint publishingPoint)
    {
      if (publishingPoint == null)
        throw new ArgumentNullException(nameof (publishingPoint));
      this.DeleteLinksForItem((IDataItem) publishingPoint);
      this.DeletePublishingPointSettings(publishingPoint.Settings);
      this.GetContext().Remove((object) publishingPoint);
    }

    /// <summary>Get a specific publishing point by its ID</summary>
    /// <param name="publishingPointID">ID of the publishing point to retrieve</param>
    /// <returns>
    /// Instance of the publishing point with the specified ID
    /// </returns>
    /// <exception cref="!:InvalidDataException">
    /// When a publishing point with the specified ID cannot be found.
    /// </exception>
    public override PublishingPoint GetPublishingPoint(Guid publishingPointID) => this.GetObjectById<PublishingPoint>(publishingPointID);

    /// <summary>Retrieve all publishing points as a query</summary>
    /// <returns>Query of all publishing points.</returns>
    public override IQueryable<PublishingPoint> GetPublishingPoints()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PublishingPoint>((DataProviderBase) this).Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (pp => pp.ApplicationName == appName));
    }

    /// <summary>Creates the pipe settings.</summary>
    /// <param name="settingsType">Type of the settings.</param>
    /// <returns></returns>
    public override PipeSettings CreatePipeSettings(Type settingsType) => this.CreatePipeSettings(settingsType, this.GetNewGuid());

    /// <summary>Creates the pipe settings.</summary>
    /// <typeparam name="TPipeSettings">The type of the pipe settings.</typeparam>
    /// <returns></returns>
    public override TPipeSettings CreatePipeSettings<TPipeSettings>() => this.CreatePipeSettings<TPipeSettings>(this.GetNewGuid());

    /// <summary>Creates the pipe settings.</summary>
    /// <param name="settingsType">Type of the settings.</param>
    /// <param name="settingsID">The settings ID.</param>
    /// <returns>New instance</returns>
    public override PipeSettings CreatePipeSettings(Type settingsType, Guid settingsID)
    {
      if (settingsType == (Type) null)
        throw new ArgumentNullException(nameof (settingsType));
      PipeSettings pipeSettings = typeof (PipeSettings).IsAssignableFrom(settingsType) ? (PipeSettings) Activator.CreateInstance(settingsType) : throw new ArgumentException("settingsType must inherit from PipeSettings");
      pipeSettings.Id = settingsID;
      pipeSettings.ApplicationName = this.ApplicationName;
      pipeSettings.Mappings = this.CreateMappingSettings();
      this.SetupDataItemForCreation((IDataItem) pipeSettings);
      return pipeSettings;
    }

    /// <summary>Creates the pipe settings.</summary>
    /// <typeparam name="TPipeSettings">The type of the pipe settings.</typeparam>
    /// <param name="settingsId">The settings id.</param>
    /// <returns>New instance</returns>
    public override TPipeSettings CreatePipeSettings<TPipeSettings>(Guid settingsId)
    {
      TPipeSettings instance = Activator.CreateInstance<TPipeSettings>();
      instance.ApplicationName = this.ApplicationName;
      instance.Id = settingsId;
      this.SetupDataItemForCreation((IDataItem) instance);
      return instance;
    }

    /// <summary>Gets the pipe settings.</summary>
    /// <param name="settingsType">Type of the settings.</param>
    /// <param name="settingsID">The settings ID.</param>
    /// <returns>Pipe Settings</returns>
    public override PipeSettings GetPipeSettings(Type settingsType, Guid settingsID)
    {
      if (settingsType == (Type) null)
        throw new ArgumentNullException(nameof (settingsType));
      return typeof (PipeSettings).IsAssignableFrom(settingsType) ? this.GetObjectById<PipeSettings>(settingsType, settingsID) : throw new ArgumentException("settingsType must inherit from PipeSettings");
    }

    /// <summary>Gets the pipe settings.</summary>
    /// <typeparam name="TPipeSettings">The type of the pipe settings.</typeparam>
    /// <param name="settingsID">The settings ID.</param>
    /// <returns>Pipe Settings</returns>
    public override TPipeSettings GetPipeSettings<TPipeSettings>(Guid settingsID) => this.GetObjectById<TPipeSettings>(settingsID);

    /// <summary>Get pipe settings by name</summary>
    /// <param name="pipeName">Name of the pipe whose settings to retrieve</param>
    /// <returns>Settings of a specific pipe</returns>
    public override PipeSettings GetPipeSettings(string pipeName)
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PipeSettings>((DataProviderBase) this).Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.ApplicationName == appName && p.PipeName == pipeName)).SingleOrDefault<PipeSettings>();
    }

    /// <summary>Get pipe settings of all types</summary>
    /// <returns>All pipe settings, regardless of type</returns>
    public override IQueryable<PipeSettings> GetPipeSettings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PipeSettings>((DataProviderBase) this).Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (p => p.ApplicationName == appName));
    }

    /// <summary>Gets the pipe settings.</summary>
    /// <param name="settingsType">Type of the settings.</param>
    /// <returns>Query of pipe settings</returns>
    public override IQueryable<PipeSettings> GetPipeSettings(Type settingsType)
    {
      if (settingsType == (Type) null)
        throw new ArgumentNullException(nameof (settingsType));
      IQueryable<PipeSettings> source = typeof (PipeSettings).IsAssignableFrom(settingsType) ? SitefinityQuery.Get<PipeSettings>(settingsType, (DataProviderBase) this) : throw new ArgumentException("settingsType must inherit from PipeSettings");
      string appName = this.ApplicationName;
      Expression<Func<PipeSettings, bool>> predicate = (Expression<Func<PipeSettings, bool>>) (setting => setting.ApplicationName == appName);
      return source.Where<PipeSettings>(predicate);
    }

    /// <summary>Get pipe settings of specific type</summary>
    /// <typeparam name="TPipeSettings">Get all pipe settings with this type</typeparam>
    /// <returns>Get all pipe settings with a specific type</returns>
    public override IQueryable<TPipeSettings> GetPipeSettings<TPipeSettings>()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<TPipeSettings>((DataProviderBase) this).Where<TPipeSettings>((Expression<Func<TPipeSettings, bool>>) (ps => ps.ApplicationName == appName));
    }

    /// <summary>
    /// Mark pipe settings for deletion in the current transaction
    /// </summary>
    /// <param name="settings">Pipe settings to mark for deletion.</param>
    public override void DeletePipeSettings(PipeSettings settings) => this.GetContext().Remove((object) settings);

    /// <summary>Creates the mapping settings.</summary>
    public override MappingSettings CreateMappingSettings() => this.CreateMappingSettings(this.GetNewGuid());

    /// <summary>Creates the mapping settings.</summary>
    /// <param name="id">The id.</param>
    public override MappingSettings CreateMappingSettings(Guid id)
    {
      MappingSettings mappingSettings = new MappingSettings();
      mappingSettings.Id = id;
      this.SetupDataItemForCreation((IDataItem) mappingSettings);
      return mappingSettings;
    }

    /// <summary>Gets the mapping settings.</summary>
    /// <param name="id">The id.</param>
    public override MappingSettings GetMappingSettings(Guid id) => this.GetObjectById<MappingSettings>(id);

    /// <summary>Gets the mapping settings.</summary>
    public override IQueryable<MappingSettings> GetMappingSettings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<MappingSettings>((DataProviderBase) this).Where<MappingSettings>((Expression<Func<MappingSettings, bool>>) (mappingSetting => mappingSetting.ApplicationName == appName));
    }

    /// <summary>Deletes the mapping settings.</summary>
    /// <param name="mappingSettings">The mapping settings.</param>
    public override void DeleteMappingSettings(MappingSettings mappingSettings) => this.GetContext().Remove((object) mappingSettings);

    /// <summary>Creates a new mapping</summary>
    /// <returns>Mapping in transaction</returns>
    public override Mapping CreateMapping() => this.CreateMapping(this.GetNewGuid());

    /// <summary>Creates a new mapping with a specific ID</summary>
    /// <param name="id">ID of the item to create</param>
    /// <returns>Mapping in transaction</returns>
    public override Mapping CreateMapping(Guid id)
    {
      Mapping mapping = new Mapping();
      mapping.Id = id;
      this.SetupDataItemForCreation((IDataItem) mapping);
      return mapping;
    }

    /// <summary>Query for a mapping with a specific ID</summary>
    /// <param name="id">ID of the mapping to look for</param>
    /// <returns>
    /// Item with the specified ID or exception if there is no mapping with that ID
    /// </returns>
    public override Mapping GetMapping(Guid id) => this.GetObjectById<Mapping>(id);

    /// <summary>Retrieve all mappings</summary>
    /// <returns>Query object for all mappings</returns>
    public override IQueryable<Mapping> GetMappings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<Mapping>((DataProviderBase) this).Where<Mapping>((Expression<Func<Mapping, bool>>) (mapping => mapping.ApplicationName == appName));
    }

    /// <summary>Mark a mapping for deletion</summary>
    /// <param name="mapping">Mapping to be marked for deletion</param>
    public override void DeleteMapping(Mapping mapping) => this.GetContext().Remove((object) mapping);

    /// <summary>Create a mapping translator with random ID</summary>
    /// <returns>Newly created item which is added to scope</returns>
    public override PipeMappingTranslation CreatePipeMappingTranslation() => this.CreatePipeMappingTranslation(Guid.NewGuid());

    /// <summary>Create a mapping translator settings with specific ID</summary>
    /// <param name="id">ID of the new item</param>
    /// <returns>Newly created item which is added to scope</returns>
    public override PipeMappingTranslation CreatePipeMappingTranslation(
      Guid id)
    {
      PipeMappingTranslation mappingTranslation = new PipeMappingTranslation();
      mappingTranslation.Id = id;
      this.SetupDataItemForCreation((IDataItem) mappingTranslation);
      return mappingTranslation;
    }

    /// <summary>Get a mapping translator settings by ID</summary>
    /// <param name="id">ID of the mapping translator settings to retrieve</param>
    /// <returns>Instance from db or exception</returns>
    public override PipeMappingTranslation GetPipeMappingTranslation(Guid id) => this.GetObjectById<PipeMappingTranslation>(id);

    /// <summary>
    /// Retrieves all mapping translator settings for this provider
    /// </summary>
    /// <returns>Query of all items</returns>
    public override IQueryable<PipeMappingTranslation> GetPipeMappingTranslations()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PipeMappingTranslation>((DataProviderBase) this).Where<PipeMappingTranslation>((Expression<Func<PipeMappingTranslation, bool>>) (t => t.ApplicationName == appName));
    }

    /// <summary>
    /// Removes <paramref name="toDelete" /> from scope
    /// </summary>
    /// <param name="toDelete">Item to delete</param>
    public override void DeletePipeMappingTranslation(PipeMappingTranslation toDelete) => this.GetContext().Remove((object) toDelete);

    /// <summary>Create new publishing point settings</summary>
    /// <returns>Publishing point settings in transaction</returns>
    public override PublishingPointSettings CreatePublishingPointSettings() => this.CreatePublishingPointSettings(this.GetNewGuid());

    /// <summary>Create new publishing point settings with specific ID</summary>
    /// <param name="id">ID to set the new instance</param>
    /// <returns>Publishing point settings with a specific ID</returns>
    public override PublishingPointSettings CreatePublishingPointSettings(
      Guid id)
    {
      PublishingPointSettings publishingPointSettings = new PublishingPointSettings();
      publishingPointSettings.Id = id;
      this.SetupDataItemForCreation((IDataItem) publishingPointSettings);
      return publishingPointSettings;
    }

    /// <summary>Query for publishing point settings with specific ID</summary>
    /// <param name="id">ID of the item to look for</param>
    /// <returns>Instance or exception if not found</returns>
    public override PublishingPointSettings GetPublishingPointSettings(
      Guid id)
    {
      return this.GetObjectById<PublishingPointSettings>(id);
    }

    /// <summary>Retrieve all publishing point settings</summary>
    /// <returns>Query for all publishing point settings</returns>
    public override IQueryable<PublishingPointSettings> GetPublishingPointSettings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<PublishingPointSettings>((DataProviderBase) this).Where<PublishingPointSettings>((Expression<Func<PublishingPointSettings, bool>>) (settings => settings.ApplicationName == appName));
    }

    /// <summary>Mark publishing point settings for deletion</summary>
    /// <param name="settings">Settings to mark for deletion</param>
    public override void DeletePublishingPointSettings(PublishingPointSettings settings)
    {
      if (settings == null)
        throw new ArgumentNullException(nameof (settings));
      if (settings.Throttle != null)
        this.DeleteThrottleSettings(settings.Throttle);
      this.GetContext().Remove((object) settings);
    }

    /// <summary>Create new throttle settings</summary>
    /// <returns>Throttle settings in transaction</returns>
    public override ThrottleSettings CreateThrottleSettings() => this.CreateThrottleSettings(this.GetNewGuid());

    /// <summary>Create new throttle settings with specific id</summary>
    /// <param name="id">ID to set the the newly created throttle settings</param>
    /// <returns>Throttle settings in transaciton</returns>
    public override ThrottleSettings CreateThrottleSettings(Guid id)
    {
      ThrottleSettings throttleSettings = new ThrottleSettings();
      throttleSettings.Id = id;
      this.SetupDataItemForCreation((IDataItem) throttleSettings);
      return throttleSettings;
    }

    /// <summary>Query for throttle settings with a specific ID</summary>
    /// <param name="id">ID of the throttle settings to look for</param>
    /// <returns>
    /// Throttle settings with specific ID or exception if none is found
    /// </returns>
    public override ThrottleSettings GetThrottleSettings(Guid id) => this.GetObjectById<ThrottleSettings>(id);

    /// <summary>Retieve all throttle settings</summary>
    /// <returns>Query for all throttle settings</returns>
    public override IQueryable<ThrottleSettings> GetThrottleSettings()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<ThrottleSettings>((DataProviderBase) this).Where<ThrottleSettings>((Expression<Func<ThrottleSettings, bool>>) (throttle => throttle.ApplicationName == appName));
    }

    /// <summary>Mark throttle settings for deletion</summary>
    /// <param name="throttle">Throttle settings to mark for deletion</param>
    public override void DeleteThrottleSettings(ThrottleSettings throttle) => this.GetContext().Remove((object) throttle);

    /// <summary>Gets the meta data source.</summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new PublishingMetadataSource(context);

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <inheritdoc />
    public virtual int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public virtual void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || upgradingFromSchemaVersionNumber > 1574)
        return;
      OpenAccessConnection.Upgrade(new DatabaseType?(DatabaseType.Oracle), context, "OpenAccessPublishingProvider: Upgrade to 1574. sf_publishing_pipe_settings filter_expression to NCLOB", context.DatabaseContext.GetColumnTypeMigrationScript("sf_publishing_pipe_settings", "filter_expression", "LONGVARCHAR"));
      OpenAccessConnection.Upgrade(new DatabaseType?(DatabaseType.Oracle), context, "OpenAccessPublishingProvider: Upgrade to 1574. sf_publishing_pipe_settings title to NCLOB", context.DatabaseContext.GetColumnTypeMigrationScript("sf_publishing_pipe_settings", "title", "LONGVARCHAR"));
    }

    /// <inheritdoc />
    public virtual void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
    }

    /// <summary>
    /// Shortcut for the open access get-object-by-id-with-parsing. Sets up IDataItem.{Provider and Transaction}
    /// </summary>
    /// <typeparam name="T">Type of item to create</typeparam>
    /// <param name="id">Id of the item</param>
    /// <returns>Item</returns>
    protected virtual T GetObjectById<T>(Guid id) => this.GetObjectById<T>(typeof (T), id);

    /// <summary>
    /// Shortcut for the open access get-object-by-id-with-parsing. Sets up IDataItem.{Provider and Transaction}
    /// </summary>
    /// <typeparam name="T">Result will be cast to this type</typeparam>
    /// <param name="actualType">Type of the data item</param>
    /// <param name="id">Id of the item</param>
    /// <returns>Item</returns>
    protected virtual T GetObjectById<T>(Type actualType, Guid id)
    {
      object objectById;
      ((IDataItem) (objectById = this.GetContext().GetObjectById(Database.OID.ParseObjectId(actualType, id.ToString())))).Provider = (object) this;
      ((IDataItem) objectById).Transaction = (object) this.GetContext();
      return (T) objectById;
    }

    /// <summary>
    /// Sets up IDataItem.{ApplicationName, Provider and Transaction} and adds it to the scope
    /// </summary>
    /// <param name="item">Data item to set up</param>
    protected virtual void SetupDataItemForCreation(IDataItem item)
    {
      item.ApplicationName = this.ApplicationName;
      item.Provider = (object) this;
      item.Transaction = (object) this.GetContext();
      this.GetContext().Add((object) item);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    public override SiteItemLink CreateSiteItemLink() => MultisiteExtensions.CreateSiteItemLink(this);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> for removal.
    /// </summary>
    /// <param name="link">The item link to delete.</param>
    public override void Delete(SiteItemLink link) => MultisiteExtensions.Delete(this, link);

    /// <summary>Deletes the links for item.</summary>
    /// <param name="item">The item.</param>
    public override void DeleteLinksForItem(IDataItem item) => MultisiteExtensions.DeleteLinksForItem(this, item);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<SiteItemLink> GetSiteItemLinks() => MultisiteExtensions.GetSiteItemLinks(this);

    /// <summary>
    /// Adds the item link that associates the item with the site.
    /// </summary>
    /// <param name="siteId">The site id.</param>
    /// <param name="item">The item.</param>
    /// <returns>The created SiteItemLink.</returns>
    public override SiteItemLink AddItemLink(Guid siteId, IDataItem item) => MultisiteExtensions.AddItemLink(this, siteId, item);

    /// <summary>Gets the items linked to the specified site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>
    ///  Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    public override IQueryable<T> GetSiteItems<T>(Guid siteId) => MultisiteExtensions.GetSiteItems<T>(this, siteId);

    public bool DataEventsEnabled => true;

    public bool ApplyDataEventItemFilter(IDataItem item) => item is PublishingPoint;
  }
}
