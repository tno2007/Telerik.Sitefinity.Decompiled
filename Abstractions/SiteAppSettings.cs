// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.SiteAppSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Taxonomies.Model;

namespace Telerik.Sitefinity.Abstractions
{
  /// <summary>Provides default application settings information.</summary>
  public class SiteAppSettings : IAppSettings
  {
    private Dictionary<int, CultureInfo> allCultures;
    private IRelatedDataResolver relatedItemsResolver;
    private IModuleBuilderProxy moduleBuilderProxy;

    /// <inheritdoc />
    public SiteAppSettings(ISite site) => this.CurrentSite = site;

    /// <inheritdoc />
    public ISite CurrentSite { get; private set; }

    /// <inheritdoc />
    [Obsolete("Use SystemManager.CurrentContext.CurrentSite.DefaultCulture.")]
    public CultureInfo DefaultFrontendLanguage => this.CurrentSite.DefaultCulture;

    /// <inheritdoc />
    [Obsolete("Use SystemManager.CurrentContext.CurrentSite.Cultures.")]
    public CultureInfo[] DefinedFrontendLanguages => this.CurrentSite.PublicContentCultures;

    /// <inheritdoc />
    public CultureInfo DefaultBackendLanguage => AppSettings.CurrentSettings.DefaultBackendLanguage;

    /// <inheritdoc />
    public CultureInfo[] DefinedBackendLanguages => AppSettings.CurrentSettings.DefinedBackendLanguages;

    /// <inheritdoc />
    public string GetResourceValue(string resourceClassId, string resourceKey, CultureInfo culture) => AppSettings.CurrentSettings.GetResourceValue(resourceClassId, resourceKey, culture);

    /// <inheritdoc />
    public Guid BackendRootNodeId => AppSettings.CurrentSettings.BackendRootNodeId;

    /// <inheritdoc />
    public Guid BackendTemplatesCategoryId => AppSettings.CurrentSettings.BackendTemplatesCategoryId;

    /// <inheritdoc />
    public MetaType[] GetMetaTypes(IDataItem instance) => AppSettings.CurrentSettings.GetMetaTypes(instance);

    /// <inheritdoc />
    MetaType[] IAppSettings.GetMetaTypes(Type type) => AppSettings.CurrentSettings.GetMetaTypes(type);

    /// <inheritdoc />
    IQueryable<MetaType> IAppSettings.GetMetaTypes() => AppSettings.CurrentSettings.GetMetaTypes();

    /// <inheritdoc />
    public TaxonomyType GetTaxonomyType(string providerName, Guid taxonomyId) => AppSettings.CurrentSettings.GetTaxonomyType(providerName, taxonomyId);

    /// <inheritdoc />
    public ITaxon GetTaxon(string providerName, Guid taxonId) => AppSettings.CurrentSettings.GetTaxon(providerName, taxonId);

    /// <inheritdoc />
    public string[] GetPlaceHolders(ControlData data) => AppSettings.CurrentSettings.GetPlaceHolders(data);

    /// <inheritdoc />
    public User GetUser(string providerName, Guid userId, string transaction = null) => AppSettings.CurrentSettings.GetUser(providerName, userId, transaction);

    /// <inheritdoc />
    public UserProfile GetUserProfile(string providerName, Guid profileId) => AppSettings.CurrentSettings.GetUserProfile(providerName, profileId);

    /// <inheritdoc />
    public bool LanguageFallback => AppSettings.CurrentSettings.LanguageFallback;

    /// <inheritdoc />
    public FallbackMode RequestLanguageFallbackMode => AppSettings.CurrentSettings.RequestLanguageFallbackMode;

    /// <inheritdoc />
    public IAppSettings Current => (IAppSettings) this;

    /// <inheritdoc />
    public IAppSettings ContextSettings => (IAppSettings) this;

    /// <inheritdoc />
    public IDictionary<int, CultureInfo> AllLanguages
    {
      get
      {
        if (this.allCultures == null)
        {
          this.allCultures = new Dictionary<int, CultureInfo>();
          foreach (KeyValuePair<int, CultureInfo> allLanguage in (IEnumerable<KeyValuePair<int, CultureInfo>>) AppSettings.CurrentSettings.AllLanguages)
          {
            KeyValuePair<int, CultureInfo> item = allLanguage;
            if (((IEnumerable<CultureInfo>) this.DefinedFrontendLanguages).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.Name == item.Value.Name)) || ((IEnumerable<CultureInfo>) this.DefinedBackendLanguages).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.Name == item.Value.Name)))
              this.allCultures.Add(item.Key, item.Value);
          }
        }
        return (IDictionary<int, CultureInfo>) this.allCultures;
      }
    }

    [Obsolete("Use SystemManager.CurrentContext.Culture")]
    public CultureInfo CurrentCulture => AppSettings.CurrentSettings.CurrentCulture;

    /// <inheritdoc />
    public IOrganizerFactory GetOrganizerFactory() => AppSettings.CurrentSettings.GetOrganizerFactory();

    /// <inheritdoc />
    public IPropertyResolverFactory GetPropertyResolverFactory() => AppSettings.CurrentSettings.GetPropertyResolverFactory();

    /// <inheritdoc />
    public object ResolveInstance(string typeName) => AppSettings.CurrentSettings.ResolveInstance(typeName);

    /// <inheritdoc />
    public object ResolveInstance(string typeName, string instanceName) => AppSettings.CurrentSettings.ResolveInstance(typeName, instanceName);

    /// <inheritdoc />
    object IAppSettings.ResolveInstance(Type type) => AppSettings.CurrentSettings.ResolveInstance(type);

    /// <inheritdoc />
    object IAppSettings.ResolveInstance(Type type, string instanceName) => AppSettings.CurrentSettings.ResolveInstance(type, instanceName);

    /// <inheritdoc />
    public ICommentsManager GetContentManager(string itemType, string providerName) => AppSettings.CurrentSettings.GetContentManager(itemType, providerName);

    /// <inheritdoc />
    public string GetItemUrl(ILocatable locatable) => AppSettings.CurrentSettings.GetItemUrl(locatable);

    /// <inheritdoc />
    public string GetMediaItemUrl(MediaContent media) => AppSettings.CurrentSettings.GetMediaItemUrl(media);

    /// <inheritdoc />
    public string GetEmbedUrl(MediaContent media) => AppSettings.CurrentSettings.GetEmbedUrl(media);

    /// <inheritdoc />
    public string GetThumbnailUrl(MediaContent media, string thumbnailName = null) => AppSettings.CurrentSettings.GetThumbnailUrl(media, thumbnailName);

    /// <inheritdoc />
    public string ResolveThumbnailFilePath(
      MediaContent media,
      string tmbName,
      string fromPath = null,
      string culture = null)
    {
      return AppSettings.CurrentSettings.ResolveThumbnailFilePath(media, tmbName, fromPath, culture);
    }

    /// <inheritdoc />
    public void DeleteComments(Content commentedItem) => AppSettings.CurrentSettings.DeleteComments(commentedItem);

    /// <inheritdoc />
    [Obsolete("Please use SystemManager.CurrentContext.CurrentSite.Cultures.Length > 1")]
    public bool Multilingual => this.DefinedFrontendLanguages.Length > 1;

    /// <inheritdoc />
    public bool IsBackend => AppSettings.CurrentSettings.IsBackend;

    /// <inheritdoc />
    public bool IsStarting => AppSettings.CurrentSettings.IsStarting;

    /// <inheritdoc />
    public bool EnableWidgetTranslations => AppSettings.CurrentSettings.EnableWidgetTranslations;

    /// <inheritdoc />
    public object GetMappedManager(
      string itemTypeName,
      string providerName,
      string transactionName)
    {
      return AppSettings.CurrentSettings.GetMappedManager(itemTypeName, providerName, transactionName);
    }

    /// <inheritdoc />
    public bool IsBackendMultilingual => AppSettings.CurrentSettings.IsBackendMultilingual;

    /// <inheritdoc />
    public string GetCultureSuffix(CultureInfo culture) => AppSettings.CurrentSettings.GetCultureSuffix(culture);

    /// <inheritdoc />
    public bool TryGetCultureFromSuffix(string cultureSufix, out CultureInfo culture) => AppSettings.CurrentSettings.TryGetCultureFromSuffix(cultureSufix, out culture);

    /// <inheritdoc />
    public CultureInfo GetCultureByLcid(int lcid) => AppSettings.CurrentSettings.GetCultureByLcid(lcid);

    /// <inheritdoc />
    public int GetCultureLcid(CultureInfo culture) => AppSettings.CurrentSettings.GetCultureLcid(culture);

    /// <inheritdoc />
    public CultureInfo GetCultureByName(string name) => AppSettings.CurrentSettings.GetCultureByName(name);

    /// <inheritdoc />
    public string GetCultureName(CultureInfo culture) => AppSettings.CurrentSettings.GetCultureName(culture);

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
  }
}
