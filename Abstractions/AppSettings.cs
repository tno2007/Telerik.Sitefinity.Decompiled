// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.AppSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Provides default application settings information.</summary>
  public class AppSettings : IAppSettings, IAppSettingsInternal
  {
    private static readonly ConcurrentProperty<AppSettings> settings = new ConcurrentProperty<AppSettings>((Func<AppSettings>) (() => new AppSettings()));
    private static string[] emptyArray = new string[0];
    private static readonly string cacheKey = typeof (AppSettings).FullName;
    private IRelatedDataResolver relatedItemsResolver;
    private IModuleBuilderProxy moduleBuilderProxy;
    private IDictionary<int, string> customCultures;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Abstractions.AppSettings" /> class.
    /// </summary>
    /// 
    ///             /// <remarks>
    /// Works as a singelton, constructor is internal just because of sitefinity.model
    /// </remarks>
    public AppSettings()
    {
      ResourcesConfig resourcesConfig = Config.Get<ResourcesConfig>();
      this.IsBackendMultilingual = resourcesConfig.IsBackendMultilingual;
      this.customCultures = resourcesConfig.CustomCultures;
      CultureElement defaultCulture = resourcesConfig.DefaultCulture;
      this.DefaultFrontendLanguage = defaultCulture == null ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(defaultCulture.UICulture);
      this.LegacyMultilingual = resourcesConfig.DefaultCultureKey == string.Empty;
      CultureElement defaultBackendCulture = resourcesConfig.DefaultBackendCulture;
      if (defaultBackendCulture != null)
        this.DefaultBackendLanguage = CultureInfo.GetCultureInfo(defaultBackendCulture.UICulture);
      this.AllLanguages = (IDictionary<int, CultureInfo>) ((IEnumerable<CultureInfo>) resourcesConfig.FrontendAndBackendCultures).ToDictionary<CultureInfo, int, CultureInfo>((Func<CultureInfo, int>) (key => this.GetCultureLcid(key)), (Func<CultureInfo, CultureInfo>) (value => value));
      this.DefinedFrontendLanguages = resourcesConfig.Cultures.Values.Select<CultureElement, CultureInfo>((Func<CultureElement, CultureInfo>) (v => CultureInfo.GetCultureInfo(v.UICulture))).ToArray<CultureInfo>();
      this.DefinedBackendLanguages = resourcesConfig.BackendCultures.Values.Select<CultureElement, CultureInfo>((Func<CultureElement, CultureInfo>) (v => CultureInfo.GetCultureInfo(v.UICulture))).ToArray<CultureInfo>();
      this.LanguageFallback = resourcesConfig.Fallback;
      this.BackendRootNodeId = SiteInitializer.BackendRootNodeId;
      this.BackendTemplatesCategoryId = SiteInitializer.BackendTemplatesCategoryId;
      this.EnableWidgetTranslations = true;
    }

    /// <summary>Returns the current application settings</summary>
    public static AppSettings CurrentSettings => AppSettings.settings.Value;

    internal static void Clear() => AppSettings.settings.Reset();

    /// <summary>
    /// Gets the default language for the system. This is the language that the system content was initially created in before other languages were added.
    /// </summary>
    /// <value>The default language.</value>
    [Obsolete("Use SystemManager.CurrentContext.DefaultSystemCulture")]
    public CultureInfo DefaultFrontendLanguage { get; private set; }

    /// <summary>Gets the defined languages for this application.</summary>
    /// <value>The defined languages.</value>
    [Obsolete("Use SystemManager.CurrentContext.SystemCultures")]
    public CultureInfo[] DefinedFrontendLanguages { get; private set; }

    /// <summary>Gets the default language.</summary>
    /// <value>The default language.</value>
    public CultureInfo DefaultBackendLanguage { get; private set; }

    /// <summary>
    /// Gets the defined backend UI cultures for this application.
    /// </summary>
    /// <value>The defined languages.</value>
    public CultureInfo[] DefinedBackendLanguages { get; private set; }

    /// <summary>
    /// Represents a dictionary with both Frontend and Backend UI cultures that are currently installed
    /// </summary>
    public IDictionary<int, CultureInfo> AllLanguages { get; set; }

    /// <summary>Gets the current culture.</summary>
    /// <value>The current culture.</value>
    [Obsolete("Use SystemManager.CurrentContext.Culture")]
    public CultureInfo CurrentCulture => SystemManager.CurrentContext.Culture;

    /// <summary>
    /// Gets a value indicating whether the requested language should fallback to neutral or invariant version.
    /// </summary>
    /// <value><c>true</c> if [language fallback]; otherwise, <c>false</c>.</value>
    public bool LanguageFallback { get; private set; }

    /// <summary>
    /// Specifies whether the system is running in multilingual mode
    /// </summary>
    /// <value></value>
    [Obsolete("There is no longer monoloingual mode. The flag always returns true.")]
    public bool Multilingual => true;

    /// <summary>
    /// Specifies whether there is more than one language installed for the backend
    /// </summary>
    public bool IsBackendMultilingual { get; private set; }

    /// <summary>Returns whether widget translation is enabled.</summary>
    /// <value>Whether widget translation is enabled.</value>
    public bool EnableWidgetTranslations { get; private set; }

    /// <summary>
    /// Specifies fallback mode valid for the current request only
    /// Highest priority has the Fallbackmode attribute, then the request one
    /// and the is the default behavior
    /// </summary>
    /// <value></value>
    public FallbackMode RequestLanguageFallbackMode => SystemManager.RequestLanguageFallbackMode;

    /// <summary>
    /// Used for Sitefinity.Model to avoid the circular reference
    /// Returns the current settings
    /// </summary>
    public IAppSettings Current => (IAppSettings) AppSettings.CurrentSettings;

    /// <inheritdoc />
    public IAppSettings ContextSettings => SystemManager.CurrentContext.AppSettings;

    /// <inheritdoc />
    public IRelatedDataResolver RelatedItemsResolver
    {
      get
      {
        if (this.relatedItemsResolver == null)
          this.relatedItemsResolver = ObjectFactory.Resolve<IRelatedDataResolver>();
        return this.relatedItemsResolver;
      }
    }

    /// <inheritdoc />
    public IModuleBuilderProxy ModuleBuilderProxy
    {
      get
      {
        if (this.moduleBuilderProxy == null)
          this.moduleBuilderProxy = ObjectFactory.Resolve<IModuleBuilderProxy>();
        return this.moduleBuilderProxy;
      }
    }

    /// <inheritdoc />
    public IPluralsResolver PluralsResolver => (IPluralsResolver) Telerik.Sitefinity.DynamicModules.Builder.Web.UI.PluralsResolver.Instance;

    /// <summary>
    /// Gets whether the project has been upgraded from legacy multilingual project
    /// </summary>
    internal bool LegacyMultilingual { get; private set; }

    /// <summary>
    /// Gets a type description for the specified object instance.
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <returns>
    /// A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.
    /// </returns>
    public MetaType[] GetMetaTypes(IDataItem instance) => this.GetMetaTypes(instance.GetType());

    /// <summary>Gets a type description for the specified type.</summary>
    /// <param name="type">The type.</param>
    /// <returns>A <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" /> object that stores type description information.</returns>
    public MetaType[] GetMetaTypes(Type type) => MetaDataProvider.GetMetaTypes(type, (Func<string, string, MetaType>) ((nameSpace, className) => this.GetMetaTypes().FirstOrDefault<MetaType>((Expression<Func<MetaType, bool>>) (x => x.Namespace == nameSpace && x.ClassName == className))));

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Metadata.Model.MetaType" />.
    /// </summary>
    public IQueryable<MetaType> GetMetaTypes() => MetadataSourceAggregator.GetMetaTypes().AsQueryable<MetaType>();

    /// <summary>Gets the type of the taxonomy.</summary>
    /// <param name="providerName">Name of the data provider.</param>
    /// <param name="taxonomyId">The taxonomy ID.</param>
    /// <returns></returns>
    public TaxonomyType GetTaxonomyType(string providerName, Guid taxonomyId)
    {
      Type type = TaxonomyManager.GetManager(providerName).GetTaxonomy(taxonomyId).GetType();
      if (type.IsAssignableFrom(typeof (FlatTaxonomy)))
        return TaxonomyType.Flat;
      if (type.IsAssignableFrom(typeof (HierarchicalTaxonomy)))
        return TaxonomyType.Hierarchical;
      throw new InvalidOperationException();
    }

    /// <summary>Gets the taxon ID.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="taxonId">The taxon ID.</param>
    /// <returns></returns>
    public ITaxon GetTaxon(string providerName, Guid taxonId) => TaxonomyManager.GetManager(providerName).GetTaxon(taxonId);

    /// <summary>
    /// Gets the place holders contained in a LayoutControl.
    /// If the provided control is not LayoutControl an empty array is returned.
    /// </summary>
    /// <param name="data">The data.</param>
    /// <returns></returns>
    public string[] GetPlaceHolders(ControlData data)
    {
      if (!data.IsLayoutControl)
        return AppSettings.emptyArray;
      LayoutControl layoutControl = (LayoutControl) PageManager.GetManager().LoadControl((ObjectData) data, (CultureInfo) null);
      layoutControl.PlaceHolder = data.PlaceHolder;
      return layoutControl.Placeholders.Convert<Control, string>((ConvertItem<Control, string>) (e => e.ID)).ToArray<string>();
    }

    /// <summary>Gets the instance of the organizer factory.</summary>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.Taxonomies.Model.IOrganizerFactory" />.
    /// </returns>
    public IOrganizerFactory GetOrganizerFactory() => (IOrganizerFactory) new OrganizerFactory();

    /// <summary>Gets the instance of the property resolver factory.</summary>
    /// <returns>
    /// Instance of the <see cref="T:Telerik.Sitefinity.Model.IPropertyResolverFactory" />.
    /// </returns>
    public IPropertyResolverFactory GetPropertyResolverFactory() => (IPropertyResolverFactory) new PropertyResolverFactory();

    /// <summary>
    /// Resolves an instance with the specified type name using Unity object container.
    /// </summary>
    /// <param name="typeName">The name of the type.</param>
    /// <returns>An instance of the specified type.</returns>
    public object ResolveInstance(string typeName) => this.ResolveInstance(TypeResolutionService.ResolveType(typeName, true));

    /// <summary>
    /// Resolves named instance with the specified type name and instance name using Unity object container.
    /// </summary>
    /// <param name="typeName">The name of the type.</param>
    /// <param name="instanceName">The name of the instance.</param>
    /// <returns>An instance of the specified type.</returns>
    public object ResolveInstance(string typeName, string instanceName) => this.ResolveInstance(TypeResolutionService.ResolveType(typeName, true), instanceName);

    /// <summary>
    /// Resolves an instance with the specified type name using Unity object container.
    /// </summary>
    /// <param name="type">The CLR type of the instance.</param>
    /// <returns>An instance of the specified type.</returns>
    public object ResolveInstance(Type type) => this.ResolveInstance(type, (string) null);

    /// <summary>
    /// Resolves named instance with the specified type name and instance name using Unity object container.
    /// </summary>
    /// <param name="type">The CLR type of the instance.</param>
    /// <param name="instanceName">The name of the instance.</param>
    /// <returns>An instance of the specified type.</returns>
    public object ResolveInstance(Type type, string instanceName) => !(type == (Type) null) ? ObjectFactory.Resolve(type, instanceName) : throw new ArgumentNullException(nameof (type));

    /// <summary>
    /// Gets the user with the specified provider and user ID.
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="userId">The user pageId.</param>
    /// <param name="transaction">The name of the transaction.</param>
    /// <returns>The user</returns>
    public User GetUser(string providerName, Guid userId, string transaction = null) => UserManager.GetManager(providerName, transaction).GetUser(userId);

    /// <summary>
    /// Gets the user profile with the specified provider and profile id.
    /// </summary>
    /// <param name="providerName">Name of the user profile provider.</param>
    /// <param name="profileId">Id of the user profile.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Security.Model.UserProfile" />.</returns>
    public UserProfile GetUserProfile(string providerName, Guid profileId) => UserProfileManager.GetManager(providerName).GetUserProfiles().Where<UserProfile>((Expression<Func<UserProfile, bool>>) (up => up.Id == profileId)).SingleOrDefault<UserProfile>();

    /// <summary>
    /// Gets the manager for the module with the given provider name
    /// </summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    public ICommentsManager GetContentManager(string itemType, string providerName) => (ICommentsManager) ManagerBase.GetMappedManager(itemType, providerName);

    /// <summary>Gets the item URL.</summary>
    /// <param name="locatable">The locatable.</param>
    /// <returns></returns>
    public string GetItemUrl(ILocatable locatable)
    {
      IContentManager contentManager = (IContentManager) null;
      IDataItem dataItem = (IDataItem) locatable;
      if (dataItem != null && dataItem.Provider != null)
      {
        PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem.Provider).Find("Name", false);
        if (propertyDescriptor != null && propertyDescriptor.PropertyType == typeof (string))
        {
          string providerName = (string) propertyDescriptor.GetValue(dataItem.Provider);
          contentManager = ManagerBase.GetMappedManager(locatable.GetType(), providerName) as IContentManager;
        }
      }
      if (contentManager == null)
        contentManager = ManagerBase.GetMappedManager(locatable.GetType()) as IContentManager;
      return contentManager != null ? RouteHelper.ResolveUrl(contentManager.GetItemUrl(locatable), UrlResolveOptions.Rooted) : string.Empty;
    }

    /// <summary>Gets the item URL.</summary>
    /// <param name="locatable">The locatable.</param>
    /// <returns></returns>
    public string GetMediaItemUrl(MediaContent media) => media.ResolveMediaUrl(true, (CultureInfo) null);

    /// <summary>Gets the embed url of the media content</summary>
    /// <param name="media">The media content</param>
    /// <returns>The embed url of the media content</returns>
    public string GetEmbedUrl(MediaContent media) => media.ResolveMediaUrl(true, (CultureInfo) null, true, true);

    /// <summary>Gets the item thumbnail URL.</summary>
    /// <param name="locatable">The locatable.</param>
    /// <returns></returns>
    public string GetThumbnailUrl(MediaContent media, string thumbnailName) => media.ResolveThumbnailUrl(thumbnailName, true);

    /// <inheritdoc />
    public string ResolveThumbnailFilePath(
      MediaContent media,
      string tmbName,
      string fromPath = null,
      string culture = null)
    {
      return media.ResolveThumbnailFilePath(tmbName, fromPath, culture);
    }

    /// <summary>Gets the dependency items for media content.</summary>
    /// <param name="media">The mediaContent.</param>
    /// <returns></returns>
    IEnumerable<IDependentItem> IAppSettingsInternal.GetDependencies(
      MediaContent media)
    {
      return MediaContentExtensions.GetDependencies(media);
    }

    /// <summary>Sets values from dependent item to media content</summary>
    /// <param name="media">The media content to be updated.</param>
    /// <param name="dependenItem">The dependent item for media content.</param>
    void IAppSettingsInternal.SetDependency(
      MediaContent media,
      IDependentItem dependenItem)
    {
      media.UpdateMediaContent(dependenItem);
    }

    /// <summary>Get media url for the specified version</summary>
    /// <param name="media">The media content.</param>
    /// <param name="version">The version number.</param>
    /// <returns>Media url with version</returns>
    string IAppSettingsInternal.GetVersionMediaUrl(
      MediaContent media,
      int version)
    {
      return media.ResolveVersionMediaUrl(version);
    }

    /// <summary>Deletes comments for the commented item.</summary>
    /// <param name="commentedItem">The commented item.</param>
    public void DeleteComments(Content commentedItem)
    {
      Type type = commentedItem.GetType();
      Type itemType = typeof (Comment);
      if (!(type != itemType) || !(ManagerBase.GetMappedManager(type) is IContentManager mappedManager))
        return;
      int? totalCount = new int?();
      Guid id = commentedItem.Id;
      foreach (object obj in mappedManager.GetItems(itemType, string.Format("CommentedItemID == ({0})", (object) id), string.Empty, 0, 0, ref totalCount))
        mappedManager.DeleteItem(obj);
    }

    /// <summary>Returns true the request is in the backend</summary>
    /// <value></value>
    public bool IsBackend => SystemManager.IsBackendRequest();

    public Guid BackendRootNodeId { get; private set; }

    public Guid BackendTemplatesCategoryId { get; private set; }

    /// <summary>Returns true if the application is starting</summary>
    public bool IsStarting => SystemManager.IsStarting;

    private static ICacheManager cache => SystemManager.GetCacheManager(CacheManagerInstance.Internal);

    /// <summary>Gets the resource value.</summary>
    /// <param name="resourceClassId">The resource class id.</param>
    /// <param name="resourceKey">The resource key.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public string GetResourceValue(string resourceClassId, string resourceKey, CultureInfo culture) => culture == null ? Res.ResolveLocalizedValue(resourceClassId.Trim(), resourceKey.Trim()) : Res.ResolveLocalizedValue(resourceClassId.Trim(), resourceKey.Trim(), culture);

    /// <summary>
    /// Gets a manager based on the specified item type
    /// <remarks>
    /// When a transaction is specified creates a manager in transaction
    /// When a provider name is specified the manager is created with the provider
    /// </remarks>
    /// </summary>
    /// <param name="itemTypeName"></param>
    /// <param name="providerName"></param>
    /// <param name="transactionName"></param>
    /// <returns>Manager instance</returns>
    public object GetMappedManager(
      string itemTypeName,
      string providerName,
      string transactionName)
    {
      if (string.IsNullOrEmpty(itemTypeName))
        throw new ArgumentNullException(nameof (itemTypeName));
      return !string.IsNullOrEmpty(transactionName) ? (string.IsNullOrEmpty(providerName) ? (object) ManagerBase.GetMappedManagerInTransaction(itemTypeName, providerName, transactionName) : (object) ManagerBase.GetMappedManagerInTransaction(itemTypeName, transactionName)) : (string.IsNullOrEmpty(providerName) ? (object) ManagerBase.GetMappedManager(itemTypeName) : (object) ManagerBase.GetMappedManager(itemTypeName, providerName));
    }

    /// <inheritdoc />
    public string GetCultureSuffix(CultureInfo culture) => ResourcesConfig.GetCultureSuffix(culture);

    /// <inheritdoc />
    public bool TryGetCultureFromSuffix(string cultureSufix, out CultureInfo culture) => ResourcesConfig.TryGetCultureFromSuffix(cultureSufix, out culture);

    /// <inheritdoc />
    public CultureInfo GetCultureByLcid(int lcid)
    {
      string name;
      CultureInfo cultureInfo = lcid < 100000 ? CultureInfo.GetCultureInfo(lcid) : (!this.customCultures.TryGetValue(lcid, out name) ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(name));
      return cultureInfo.Equals((object) CultureInfo.InvariantCulture) ? this.DefaultFrontendLanguage : cultureInfo;
    }

    /// <inheritdoc />
    public int GetCultureLcid(CultureInfo culture)
    {
      if (culture == null || this.DefaultFrontendLanguage.Equals((object) culture))
        culture = CultureInfo.InvariantCulture;
      else if (culture.LCID == 4096)
      {
        foreach (KeyValuePair<int, string> customCulture in (IEnumerable<KeyValuePair<int, string>>) this.customCultures)
        {
          if (culture.Name == customCulture.Value)
            return customCulture.Key;
        }
      }
      return culture.LCID;
    }

    /// <inheritdoc />
    public CultureInfo GetCultureByName(string name) => name.IsNullOrEmpty() ? this.DefaultFrontendLanguage : CultureInfo.GetCultureInfo(name);

    /// <inheritdoc />
    public string GetCultureName(CultureInfo culture)
    {
      if (this.DefaultFrontendLanguage.Equals((object) culture))
        return CultureInfo.InvariantCulture.Name;
      if (((IEnumerable<CultureInfo>) this.DefinedFrontendLanguages).Contains<CultureInfo>(culture) || ((IEnumerable<CultureInfo>) this.DefinedBackendLanguages).Contains<CultureInfo>(culture))
        return culture.Name;
      throw new Exception(string.Format("The given culture {0} was not found in Sitefinity`s cultures configuration.", (object) culture));
    }

    /// <summary>
    /// This method check if the Combine script settin is turned ON, depending on Public or Backend of Sitefinity is running.
    /// </summary>
    /// <returns>combineScripts - true/false</returns>
    internal bool CombineScripts() => (this.IsBackend || SystemManager.IsDesignMode ? 0 : (!SystemManager.IsPreviewMode ? 1 : 0)) == 0 ? Config.Get<PagesConfig>().CombineScriptsBackEnd.Value : Config.Get<PagesConfig>().CombineScriptsFrontEnd.Value;

    /// <summary>
    /// This method checks if the Compression for scripts is turned on for the frontend
    /// </summary>
    /// <returns></returns>
    internal OutputCompression CompressScripts() => Config.Get<PagesConfig>().CompressScriptsFrontEnd;
  }
}
