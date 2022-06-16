// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationsTracker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Tracks changes for the content location objects from the given dirty items and persist them.
  /// </summary>
  internal class ContentLocationsTracker : ITracker
  {
    private readonly Dictionary<Guid, ContentLocationsTracker.DeletedLocationTrackingInfo> deletedLocations = new Dictionary<Guid, ContentLocationsTracker.DeletedLocationTrackingInfo>();
    private readonly Dictionary<Guid, ContentLocationsTracker.UpdatedLocationsTrackingInfo> trackingInfo = new Dictionary<Guid, ContentLocationsTracker.UpdatedLocationsTrackingInfo>();
    private ContentLocationService clService;
    public const string LocatableItemTrackerStateKey = "locatable_item_tracker";

    private ContentLocationService ContentLocationService
    {
      get
      {
        if (this.clService == null)
          this.clService = SystemManager.GetContentLocationServiceInternal();
        return this.clService;
      }
    }

    internal bool IgnoreItemStatus { get; set; }

    /// <summary>
    /// Checks if there are any changes that need to be persisted
    /// </summary>
    /// <returns></returns>
    public bool HasChanges() => this.HasAnyChanges;

    /// <summary>
    /// Persist the tracked changes using the location service.
    /// </summary>
    public void SaveChanges()
    {
      foreach (ContentLocationsTracker.DeletedLocationTrackingInfo locationTrackingInfo in this.deletedLocations.Values)
      {
        ContentLocationsTracker.DeletedLocationTrackingInfo deletedTrackingInfo = locationTrackingInfo;
        if (deletedTrackingInfo.Cultures == null && deletedTrackingInfo.ControlsIds == null)
        {
          this.ContentLocationService.DeletePageLocations(deletedTrackingInfo.PageId);
        }
        else
        {
          IEnumerable<IContentLocation> source = this.ContentLocationService.GetPageLocations(deletedTrackingInfo.PageId);
          if (deletedTrackingInfo.Cultures != null)
            source = (IEnumerable<IContentLocation>) source.Where<IContentLocation>((Func<IContentLocation, bool>) (l => deletedTrackingInfo.Cultures.Contains(l.Culture))).ToList<IContentLocation>();
          if (deletedTrackingInfo.ControlsIds != null)
            source = (IEnumerable<IContentLocation>) source.Where<IContentLocation>((Func<IContentLocation, bool>) (l => deletedTrackingInfo.ControlsIds.Contains(new Guid?(l.ControlId)))).ToList<IContentLocation>();
          foreach (IContentLocation location in source)
            this.ContentLocationService.DeleteLocation(location);
        }
      }
      this.deletedLocations.Clear();
      foreach (KeyValuePair<Guid, ContentLocationsTracker.UpdatedLocationsTrackingInfo> keyValuePair in this.trackingInfo)
      {
        Guid key = keyValuePair.Key;
        ContentLocationsTracker.UpdatedLocationsTrackingInfo locationsTrackingInfo = keyValuePair.Value;
        foreach (ContentLocationsTracker.ContentLocationProxy contentLocationProxy in locationsTrackingInfo.LocationsForUpdate)
          this.ContentLocationService.UpdateLocation(contentLocationProxy.LocationInfo.ContentType, contentLocationProxy.LocationInfo.ProviderName, contentLocationProxy.PageId, contentLocationProxy.SiteId, contentLocationProxy.ControlId, contentLocationProxy.Culture, contentLocationProxy.LocationInfo);
        locationsTrackingInfo.LocationsForUpdate.Clear();
      }
      this.trackingInfo.Clear();
    }

    /// <summary>
    /// Stores the information that will be persisted when SaveChanges is called.
    /// </summary>
    /// <param name="item"></param>
    /// <param name="provider"></param>
    public void Track(object item, DataProviderBase provider)
    {
      if (SystemManager.DataTrackingDisabled)
        return;
      switch (item)
      {
        case PageControl pageControl:
          if (!this.IsContentLocatableControl(pageControl) || provider.GetDirtyItemStatus((object) pageControl) != SecurityConstants.TransactionActionType.Deleted)
            break;
          PageData originalValue1 = provider.GetOriginalValue<PageData>((object) pageControl, "Page");
          if (originalValue1 == null || !this.Validate(originalValue1))
            break;
          PageNode originalValue2 = provider.GetOriginalValue<PageNode>((object) originalValue1, "NavigationNode");
          if (originalValue2 == null)
            break;
          this.TrackDeletedLocation(originalValue1, originalValue2, new Guid?(pageControl.Id));
          break;
        case PageData pageData:
          if (!this.Validate(pageData))
            break;
          PageNode navigationNode = pageData.NavigationNode;
          if (navigationNode == null || navigationNode.IsBackend || navigationNode.RootNodeId == NewslettersModule.standardCampaignRootNodeId)
            break;
          SecurityConstants.TransactionActionType transactionActionType = provider.GetDirtyItemStatus((object) pageData);
          if (transactionActionType == SecurityConstants.TransactionActionType.None)
          {
            if (!this.IgnoreItemStatus)
              break;
            transactionActionType = SecurityConstants.TransactionActionType.New;
          }
          else
          {
            if (transactionActionType == SecurityConstants.TransactionActionType.Deleted || transactionActionType == SecurityConstants.TransactionActionType.Updated && pageData.IsDeleted)
            {
              this.TrackDeletedLocation(pageData, navigationNode);
              break;
            }
            if (transactionActionType == SecurityConstants.TransactionActionType.Updated && provider.IsFieldDirty((object) pageData, "LanguageData"))
            {
              foreach (LanguageData languageData in (IEnumerable<LanguageData>) provider.GetOriginalValue<IList<LanguageData>>((object) pageData, "LanguageData"))
              {
                if (!pageData.LanguageData.Contains(languageData))
                  this.TrackDeletedLocation(pageData, navigationNode, culture: new CultureInfo(languageData.Language));
              }
            }
          }
          ISite site = PageManager.GetSite(navigationNode);
          if (site == null || transactionActionType == SecurityConstants.TransactionActionType.New && pageData.Status != ContentLifecycleStatus.Live)
            break;
          SitefinityOAContext transaction = provider.GetTransaction() as SitefinityOAContext;
          bool flag = false;
          if (transaction != null)
            flag = transactionActionType != SecurityConstants.TransactionActionType.New && !transaction.IsFieldDirty((object) pageData, "Controls") && !transaction.IsFieldDirty((object) pageData, "LanguageData") && !transaction.IsFieldDirty((object) navigationNode, "IsDeleted");
          using (IEnumerator<PageControl> enumerator = pageData.Controls.GetEnumerator())
          {
            while (enumerator.MoveNext())
            {
              PageControl current = enumerator.Current;
              if (this.IsContentLocatableControl(current) && (!flag || provider.IsFieldDirty((object) current, "Properties") || this.HasAnyDirtyProperty((IEnumerable<ControlProperty>) current.Properties, provider)))
              {
                ContentLocationsTracker.PageControlTrackingContext controlTrackingContext = new ContentLocationsTracker.PageControlTrackingContext(provider as PageDataProvider, current, pageData, navigationNode, site);
                List<CultureInfo> cultureInfoList = new List<CultureInfo>();
                if (navigationNode.LocalizationStrategy == LocalizationStrategy.Split)
                {
                  CultureInfo cultureInfo = !string.IsNullOrEmpty(pageData.Culture) ? CultureInfo.GetCultureInfo(pageData.Culture) : site.DefaultCulture;
                  cultureInfoList.Add(cultureInfo);
                }
                else
                {
                  CultureInfo[] availableCultures = navigationNode.AvailableCultures;
                  if (((IEnumerable<CultureInfo>) availableCultures).Any<CultureInfo>())
                  {
                    foreach (CultureInfo cultureInfo in availableCultures)
                    {
                      if (!cultureInfoList.Contains(cultureInfo))
                        cultureInfoList.Add(cultureInfo);
                    }
                  }
                  else
                    cultureInfoList.Add(site.DefaultCulture);
                }
                ContentLocationsTracker.UpdatedLocationsTrackingInfo locationsTrackingInfo;
                if (!this.trackingInfo.TryGetValue(pageData.Id, out locationsTrackingInfo))
                {
                  locationsTrackingInfo = new ContentLocationsTracker.UpdatedLocationsTrackingInfo(navigationNode.Id);
                  this.trackingInfo.Add(pageData.Id, locationsTrackingInfo);
                }
                foreach (CultureInfo culture in cultureInfoList)
                {
                  IEnumerable<IContentLocationInfo> locations;
                  if (controlTrackingContext.TryGetLocations(culture, out locations))
                  {
                    if (locations == null)
                    {
                      this.TrackDeletedLocation(pageData, navigationNode, new Guid?(current.Id), culture);
                    }
                    else
                    {
                      foreach (IContentLocationInfo contentLocationInfo in locations)
                      {
                        Type contentType = contentLocationInfo.ContentType;
                        if (!(contentType == (Type) null) && this.ContentLocationService.IsTypeSupported(contentType))
                        {
                          ContentLocationsTracker.ContentLocationProxy contentLocationProxy = new ContentLocationsTracker.ContentLocationProxy()
                          {
                            LocationInfo = contentLocationInfo,
                            Culture = culture,
                            SiteId = site.Id,
                            PageId = navigationNode.Id,
                            ControlId = current.Id
                          };
                          locationsTrackingInfo.LocationsForUpdate.Add(contentLocationProxy);
                        }
                      }
                    }
                  }
                }
              }
            }
            break;
          }
      }
    }

    private bool Validate(PageData pageData) => pageData.PersonalizationMasterId == Guid.Empty;

    private void TrackDeletedLocation(
      PageData pageData,
      PageNode pageNode,
      Guid? controlId = null,
      CultureInfo culture = null)
    {
      Guid id = pageData.Id;
      if (pageNode.LocalizationStrategy == LocalizationStrategy.Split)
      {
        if (!pageData.Culture.IsNullOrEmpty())
        {
          culture = CultureInfo.GetCultureInfo(pageData.Culture);
        }
        else
        {
          ISite site = PageManager.GetSite(pageNode);
          if (site != null)
            culture = site.DefaultCulture;
        }
      }
      ContentLocationsTracker.DeletedLocationTrackingInfo locationTrackingInfo;
      if (!this.deletedLocations.TryGetValue(id, out locationTrackingInfo))
      {
        locationTrackingInfo = new ContentLocationsTracker.DeletedLocationTrackingInfo(pageNode.Id, culture, controlId);
        this.deletedLocations.Add(id, locationTrackingInfo);
      }
      else
      {
        locationTrackingInfo.SetControlId(controlId);
        locationTrackingInfo.SetCulture(culture);
      }
    }

    private bool HasAnyDirtyProperty(
      IEnumerable<ControlProperty> properties,
      DataProviderBase provider)
    {
      foreach (ControlProperty property in properties)
      {
        if (property.Value == null)
        {
          if (property.HasChildProps())
            return provider.IsFieldDirty((object) property, "ChildProperties") || this.HasAnyDirtyProperty((IEnumerable<ControlProperty>) property.ChildProperties, provider);
          if (property.HasListItems())
          {
            if (provider.IsFieldDirty((object) property, "ListItems"))
              return true;
            using (IEnumerator<ObjectData> enumerator = property.ListItems.GetEnumerator())
            {
              if (enumerator.MoveNext())
              {
                ObjectData current = enumerator.Current;
                return provider.IsFieldDirty((object) current, "Properties") || this.HasAnyDirtyProperty((IEnumerable<ControlProperty>) current.Properties, provider);
              }
            }
          }
        }
        else if (provider.IsFieldDirty((object) property, "Value"))
          return true;
      }
      return false;
    }

    protected bool IsContentLocatableControl(PageControl pageControl)
    {
      if (!pageControl.IsBackendObject)
      {
        if (!pageControl.ObjectType.StartsWith("~/"))
        {
          Type c;
          try
          {
            string behaviorObjectType = ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObjectType((ControlData) pageControl);
            if (behaviorObjectType.IsNullOrEmpty())
              return false;
            c = TypeResolutionService.ResolveType(behaviorObjectType, false);
            if (c == (Type) null)
              return false;
          }
          catch
          {
            return false;
          }
          return typeof (IContentLocatableView).IsAssignableFrom(c) && !typeof (BackendContentView).IsAssignableFrom(c);
        }
      }
      return false;
    }

    protected bool HasAnyChanges => this.deletedLocations.Any<KeyValuePair<Guid, ContentLocationsTracker.DeletedLocationTrackingInfo>>() || this.trackingInfo.Any<KeyValuePair<Guid, ContentLocationsTracker.UpdatedLocationsTrackingInfo>>();

    private class PageControlTrackingContext
    {
      private readonly ISite site;
      private PageManager pageManager;
      private readonly PageDataProvider provider;
      private readonly PageControl pageControl;
      private readonly PageData pageData;
      private readonly PageNode pageNode;

      public PageControlTrackingContext(
        PageDataProvider provider,
        PageControl pageControl,
        PageData pageData,
        PageNode pageNode,
        ISite site)
      {
        this.provider = provider;
        this.pageControl = pageControl;
        this.pageData = pageData;
        this.pageNode = pageNode;
        this.site = site;
      }

      public bool TryGetLocations(
        CultureInfo culture,
        out IEnumerable<IContentLocationInfo> locations)
      {
        locations = (IEnumerable<IContentLocationInfo>) null;
        try
        {
          using (new SiteRegion(this.site))
          {
            IContentLocatableView behaviorObject = (IContentLocatableView) ObjectFactory.Resolve<IControlBehaviorResolver>().GetBehaviorObject(this.Manager.LoadControl((ObjectData) this.pageControl, culture));
            MethodInfo method = behaviorObject.GetType().GetMethod("GetLocations", new Type[1]
            {
              typeof (ContentLocationTrackingContext)
            });
            if (method != (MethodInfo) null && method.ReturnType.Equals(typeof (IEnumerable<IContentLocationInfo>)))
              locations = (IEnumerable<IContentLocationInfo>) method.Invoke((object) behaviorObject, new object[1]
              {
                (object) new ContentLocationTrackingContext(this.pageNode, this.pageData, this.pageControl, culture)
              });
            else
              locations = behaviorObject.GetLocations();
          }
          return true;
        }
        catch (Exception ex)
        {
          Exception exceptionToHandle = new Exception("ContentLocationsTracker: Unable to get locations from '{0}'".Arrange((object) this.pageControl.ObjectType), ex);
          if (Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.IgnoreExceptions))
            throw exceptionToHandle;
        }
        return false;
      }

      private PageManager Manager
      {
        get
        {
          if (this.pageManager == null)
            this.pageManager = PageManager.GetManager(this.provider.Name);
          return this.pageManager;
        }
      }
    }

    private class ContentLocationProxy
    {
      public Guid PageId { get; set; }

      public Guid SiteId { get; set; }

      public Guid ControlId { get; set; }

      public CultureInfo Culture { get; set; }

      public IContentLocationInfo LocationInfo { get; set; }
    }

    private class TrackingInfoBase
    {
      private readonly Guid pageId;

      public TrackingInfoBase(Guid pageId) => this.pageId = pageId;

      public Guid PageId => this.pageId;
    }

    private class DeletedLocationTrackingInfo : ContentLocationsTracker.TrackingInfoBase
    {
      private HashSet<Guid?> controlsIds;
      private HashSet<CultureInfo> cultures;

      public DeletedLocationTrackingInfo(Guid pageId, CultureInfo culture, Guid? controlId)
        : base(pageId)
      {
        if (culture != null)
        {
          this.cultures = new HashSet<CultureInfo>();
          this.cultures.Add(culture);
        }
        if (!controlId.HasValue)
          return;
        this.controlsIds = new HashSet<Guid?>();
        this.controlsIds.Add(controlId);
      }

      public HashSet<Guid?> ControlsIds => this.controlsIds;

      public HashSet<CultureInfo> Cultures => this.cultures;

      internal void SetControlId(Guid? controlId)
      {
        if (controlId.HasValue)
        {
          if (this.controlsIds == null)
            return;
          this.ControlsIds.Add(controlId);
        }
        else
          this.controlsIds = (HashSet<Guid?>) null;
      }

      internal void SetCulture(CultureInfo culture)
      {
        if (culture != null)
        {
          if (this.cultures == null)
            return;
          this.cultures.Add(culture);
        }
        else
          this.cultures = (HashSet<CultureInfo>) null;
      }
    }

    private class UpdatedLocationsTrackingInfo : ContentLocationsTracker.TrackingInfoBase
    {
      private readonly List<ContentLocationsTracker.ContentLocationProxy> locationsForUpdate = new List<ContentLocationsTracker.ContentLocationProxy>();

      public UpdatedLocationsTrackingInfo(Guid pageId)
        : base(pageId)
      {
      }

      public List<ContentLocationsTracker.ContentLocationProxy> LocationsForUpdate => this.locationsForUpdate;
    }
  }
}
