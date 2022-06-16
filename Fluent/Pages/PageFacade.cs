// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.Pages.PageFacade
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent.Pages.Contracts;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Fluent.Pages
{
  /// <summary>
  /// Fluent API that provides most common functionality related to a single Sitefinity page.
  /// </summary>
  public class PageFacade : BaseFacadeWithManager, IPageFacade
  {
    private PageNode currentState;
    private AppSettings appSettings;
    private PageManager pageManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">
    /// The app settings that configure the way fluent API will function.
    /// </param>
    public PageFacade(AppSettings appSettings)
      : base(appSettings)
    {
      this.appSettings = appSettings != null ? appSettings : throw new ArgumentNullException(nameof (appSettings));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="pageId">The page id.</param>
    public PageFacade(AppSettings appSettings, Guid pageId)
      : this(appSettings)
    {
      if (pageId == Guid.Empty)
        throw new ArgumentNullException(nameof (pageId));
      this.LoadPage(pageId);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> class.
    /// </summary>
    /// <param name="appSettings">The app settings.</param>
    /// <param name="page">The page on which the fluent API functionality ought to be used.</param>
    public PageFacade(AppSettings appSettings, PageNode page)
      : this(appSettings)
    {
      this.currentState = page != null ? page : throw new ArgumentNullException(nameof (page));
    }

    /// <summary>Needed for mocking.</summary>
    internal PageFacade()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageFacade.PageManager" /> to be used by this facade.
    /// </summary>
    /// <value>An initialized instance of the <see cref="P:Telerik.Sitefinity.Fluent.Pages.PageFacade.PageManager" /> class.</value>
    public virtual PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.appSettings.PagesProviderName, this.appSettings.TransactionName);
        return this.pageManager;
      }
    }

    internal AppSettings Settings => this.appSettings;

    public IControlsContainer CurrentState => this.currentState != null ? (IControlsContainer) this.currentState.Page : (IControlsContainer) null;

    /// <summary>Creates new standard page.</summary>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage()
    {
      this.CreateNew(this.GetLocationRoot(PageLocation.Frontend), Guid.Empty, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="pageId">The ID of the page.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(Guid pageId)
    {
      this.CreateNew(this.GetLocationRoot(PageLocation.Frontend), pageId, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="location">The location.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(PageLocation location)
    {
      this.CreateNew(this.GetLocationRoot(location), Guid.Empty, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="location">The location.</param>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageLocation location,
      Guid pageId)
    {
      this.CreateNew(this.GetLocationRoot(location), pageId, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(PageNode parentNode)
    {
      this.CreateNew(parentNode, Guid.Empty, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageNode parentNode,
      Guid pageId)
    {
      this.CreateNew(parentNode, pageId, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      Guid parentId,
      Guid pageId)
    {
      if (parentId == Guid.Empty)
        throw new ArgumentNullException(nameof (parentId));
      this.CreateNew(this.PageManager.GetPageNode(parentId), pageId, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageDataId">Id of the page data (title and properties) to create</param>
    /// <returns>Fluent chain with new state</returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      Guid parentId,
      Guid pageId,
      Guid pageDataId)
    {
      if (parentId == Guid.Empty)
        throw new ArgumentNullException(nameof (parentId));
      this.CreateNew(this.PageManager.GetPageNode(parentId), pageId, (PageData) null, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageNode parentNode,
      Guid pageId,
      PageData pageData)
    {
      this.CreateNew(parentNode, pageId, pageData, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Create a new standard page by sepcifically setting location and IDs
    /// </summary>
    /// <param name="location">Create the p</param>
    /// <param name="pageId">Id of the page to be created</param>
    /// <param name="pageDataId">Id of the page data (title and properties) to create</param>
    /// <returns>Fluent chain with new state</returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageLocation location,
      Guid pageId,
      Guid pageDataId)
    {
      PageData pageData = this.PageManager.CreatePageData(pageDataId);
      this.CreateNew(this.GetLocationRoot(PageLocation.Frontend), pageId, pageData, NodeType.Standard);
      pageData.NavigationNode = this.currentState;
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageData">Id of the page data (title and properties) to create</param>
    /// <returns>Fluent chain with new state</returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageNode parentNode,
      Guid pageId,
      Guid pageDataId)
    {
      PageData pageData = this.PageManager.CreatePageData(pageDataId);
      this.CreateNew(parentNode, pageId, pageData, NodeType.Standard);
      pageData.NavigationNode = this.currentState;
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new standard page.</summary>
    /// <param name="location">The location.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageData">The page data.</param>
    /// <returns></returns>
    public virtual StandardPageFacade CreateNewStandardPage(
      PageLocation location,
      Guid pageId,
      PageData pageData)
    {
      this.CreateNew(this.GetLocationRoot(location), pageId, pageData, NodeType.Standard);
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <returns></returns>
    public virtual PageGroupFacade CreateNewPageGroup()
    {
      this.CreateNew(this.GetLocationRoot(PageLocation.Frontend), Guid.Empty, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual PageGroupFacade CreateNewPageGroup(Guid pageId)
    {
      this.CreateNew(this.GetLocationRoot(PageLocation.Frontend), pageId, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <param name="location">The location.</param>
    /// <returns></returns>
    public virtual PageGroupFacade CreateNewPageGroup(PageLocation location)
    {
      this.CreateNew(this.GetLocationRoot(location), Guid.Empty, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <param name="location">The location.</param>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual PageGroupFacade CreateNewPageGroup(
      PageLocation location,
      Guid pageId)
    {
      this.CreateNew(this.GetLocationRoot(location), pageId, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <param name="parentNode">The parent node.</param>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    public virtual PageGroupFacade CreateNewPageGroup(
      PageNode parentNode,
      Guid pageId)
    {
      this.CreateNew(parentNode, pageId, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>Creates new page group.</summary>
    /// <param name="parentNodeId">Id of the parent page or page group.</param>
    /// <param name="pageId">Id of the new page group that is to be created</param>
    /// <returns>Fluent chain with new state</returns>
    public virtual PageGroupFacade CreateNewPageGroup(Guid parentNodeId, Guid pageId)
    {
      this.CreateNew(this.PageManager.GetPageNode(parentNodeId), pageId, (PageData) null, NodeType.Group);
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> object and a new instance
    /// of <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" /> object. Assigns the instance of <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" /> object
    /// to the Page property of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> object.
    /// </summary>
    /// <param name="parentPage">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> to which this page should be added as a child. If the page should
    /// be located in the root pass null.
    /// </param>
    /// <param name="pageId">The id of page</param>
    /// <param name="pageData">
    /// An instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageData" />.
    /// </param>
    /// <param name="pageType">The type of page to create.</param>
    /// <returns>
    /// An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.
    /// </returns>
    protected virtual void CreateNew(
      PageNode parentPage,
      Guid pageId,
      PageData pageData,
      NodeType pageType)
    {
      if (parentPage == null)
        throw new ArgumentNullException(nameof (parentPage));
      this.currentState = this.PageManager.CreatePage(parentPage, pageId, pageType);
      this.appSettings.TrackModifiedItem((IDataItem) this.currentState);
    }

    /// <summary>
    /// Provides fluent API for standard page. All methods will be ignored if the current node does not represent standard page.
    /// </summary>
    /// <returns></returns>
    public virtual StandardPageFacade IfStandardPage()
    {
      this.EnsureState();
      return new StandardPageFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Provides fluent API for standard page. This method will throw an exception if the current page node does not represent standard page.
    /// </summary>
    /// <returns></returns>
    public virtual StandardPageFacade AsStandardPage()
    {
      this.EnsureState();
      return this.currentState.NodeType == NodeType.Standard ? new StandardPageFacade(this, this.appSettings, this.currentState) : throw new InvalidOperationException("The current page is not standard page.");
    }

    /// <summary>
    /// Provides fluent API for page groups. All methods will be ignored if the current node does not represent a page group.
    /// </summary>
    /// <returns></returns>
    public virtual PageGroupFacade IfPageGroup()
    {
      this.EnsureState();
      return new PageGroupFacade(this, this.appSettings, this.currentState);
    }

    /// <summary>
    /// Provides fluent API for page groups. This method will throw an exception if the current page node does not represent a page group.
    /// </summary>
    /// <returns></returns>
    public virtual PageGroupFacade AsPageGroup()
    {
      this.EnsureState();
      return this.currentState.NodeType == NodeType.Group ? new PageGroupFacade(this, this.appSettings, this.currentState) : throw new InvalidOperationException("The current page node does not represent a page group.");
    }

    /// <summary>
    /// Provides fluent API for external pages. This method will throw an exception if the current page node does not represent an external page.
    ///  </summary>
    /// <returns></returns>
    internal virtual PageGroupFacade AsPageExternal()
    {
      this.EnsureState();
      return this.currentState.NodeType == NodeType.OuterRedirect ? new PageGroupFacade(this, this.appSettings, this.currentState) : throw new InvalidOperationException("The current page node does not represent an external page.");
    }

    /// <summary>
    /// Performs an arbitrary action on the page of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> currently loaded at Fluent API.
    /// </summary>
    /// <param name="setAction">An action to be performed on the current page loaded at Fluent API.</param>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> has not been initialized either through PageFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Do(Action<PageNode> setAction)
    {
      if (setAction == null)
        throw new ArgumentNullException(nameof (setAction));
      this.EnsureState();
      setAction(this.currentState);
      return this;
    }

    /// <summary>
    /// Performs deletion of the current page loaded by the fluent API.
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">
    /// thrown if the PageNode object has not been initialized either through PageFacade(Guid pageId) constructor or CreateNew() method.
    /// </exception>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Delete()
    {
      this.EnsureState();
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        foreach (CultureInfo availableCulture in this.currentState.AvailableCultures)
          this.appSettings.TrackDeletedItem((IDataItem) this.currentState, availableCulture.Name);
      }
      else
        this.appSettings.TrackDeletedItem((IDataItem) this.currentState);
      this.PageManager.Delete(this.currentState);
      return this;
    }

    /// <summary>
    /// Performs the deletion of the personalized page loaded by fluent API for the given segment.
    /// </summary>
    /// <param name="segmentId">
    /// Id of the segment for which the personalized page ought to be deleted.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.
    /// </returns>
    public virtual PageFacade Delete(object segmentId)
    {
      if (segmentId == null)
        return this.Delete();
      this.EnsureState();
      Guid segmentIdGuid = new Guid(segmentId.ToString());
      Guid pageDataId = this.currentState.GetPageData().Id;
      PageData pageData = this.PageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageDataId && p.PersonalizationSegmentId == segmentIdGuid)).Single<PageData>();
      foreach (CultureInfo availableCulture in this.currentState.AvailableCultures)
        this.appSettings.TrackDeletedItem((IDataItem) pageData, availableCulture.Name);
      this.PageManager.Delete(pageData);
      return this;
    }

    /// <summary>
    /// Deletes the specified language version of the currently loaded page. If null is given for language,
    /// all language versions are deleted.
    /// </summary>
    /// <param name="language">The language to delete.</param>
    /// <returns></returns>
    public virtual PageFacade Delete(CultureInfo language)
    {
      if (language == null)
        return this.Delete();
      this.appSettings.TrackDeletedItem((IDataItem) this.currentState);
      this.PageManager.Delete(this.currentState, language);
      return this;
    }

    /// <summary>
    /// Deletes the specified language version of the currently loaded personalized page. If null is given
    /// for language, all language version are deleted.
    /// </summary>
    /// <param name="language"></param>
    /// <param name="segmentId"></param>
    /// <returns></returns>
    public virtual PageFacade Delete(CultureInfo language, object segmentId)
    {
      if (segmentId == null)
        return this.Delete(language);
      Guid segmentIdGuid = new Guid(segmentId.ToString());
      if (language == null)
        return this.Delete((object) segmentIdGuid);
      Guid pageDataId = this.currentState.GetPageData(language).Id;
      PageData pageData = this.PageManager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageDataId && p.PersonalizationSegmentId == segmentIdGuid)).Single<PageData>();
      foreach (CultureInfo availableCulture in this.currentState.AvailableCultures)
        this.appSettings.TrackDeletedItem((IDataItem) pageData, availableCulture.Name);
      if (pageData.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == language.Name)))
      {
        if (pageData.LanguageData.Count == 1 || pageData.LanguageData.Count == 2 && pageData.LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == null)))
        {
          this.PageManager.Delete(pageData, false);
        }
        else
        {
          ++pageData.BuildStamp;
          this.PageManager.Delete(this.currentState, pageData, language);
        }
      }
      return this;
    }

    /// <summary>
    /// Duplicates the current page and uses the newly created duplicate as the current page loaded by the fluent API.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Duplicate() => this.Duplicate((CultureInfo) null, (CultureInfo) null);

    /// <summary>
    /// Duplicates the current page and uses the newly created duplicate as the current page loaded by the fluent API.
    /// </summary>
    /// <param name="parentId">The parent of the newly created page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public PageFacade Duplicate(Guid parentId) => this.Duplicate((CultureInfo) null, (CultureInfo) null, parentId);

    internal PageFacade Duplicate(CultureInfo sourceCulture, CultureInfo targetCulture) => this.Duplicate(sourceCulture, targetCulture, Guid.Empty, false, false);

    internal PageFacade Duplicate(
      CultureInfo sourceCulture,
      CultureInfo targetCulture,
      Guid parentId)
    {
      return this.Duplicate(sourceCulture, targetCulture, parentId, false, false);
    }

    internal PageFacade Duplicate(
      CultureInfo sourceCulture,
      CultureInfo targetCulture,
      Guid parentId,
      bool duplicateHierarchy,
      bool fullCopy)
    {
      this.EnsureState();
      ISite siteIdByPageNodeId = this.GetSiteIdByPageNodeId(parentId);
      using (SiteRegion.FromSiteId(siteIdByPageNodeId == null || !(siteIdByPageNodeId.Id != SystemManager.CurrentContext.CurrentSite.Id) ? Guid.Empty : siteIdByPageNodeId.Id))
      {
        if (!SystemManager.CurrentContext.AppSettings.Multilingual)
          targetCulture = CultureInfo.InvariantCulture;
        using (new CultureRegion(targetCulture))
          this.currentState = this.pageManager.DuplicatePage(this.currentState, this.pageManager.CreatePageNode(), sourceCulture, targetCulture, parentId, duplicateHierarchy, fullCopy);
      }
      return this;
    }

    internal PageFacade Duplicate(CultureInfo[] sourceCultures) => this.Duplicate(sourceCultures, Guid.Empty, false, false);

    internal PageFacade Duplicate(CultureInfo[] sourceCultures, Guid parentId) => this.Duplicate(sourceCultures, parentId, false, false);

    internal PageFacade Duplicate(
      CultureInfo[] sourceCultures,
      Guid parentId,
      bool duplicateHierarchy,
      bool fullCopy)
    {
      this.EnsureState();
      ISite siteIdByPageNodeId = this.GetSiteIdByPageNodeId(parentId);
      using (SiteRegion.FromSiteId(siteIdByPageNodeId.Id != SystemManager.CurrentContext.CurrentSite.Id ? siteIdByPageNodeId.Id : Guid.Empty))
      {
        PageNode targetPageNode = this.pageManager.CreatePageNode();
        foreach (CultureInfo sourceCulture in sourceCultures)
        {
          CultureInfo culture = sourceCulture;
          if (siteIdByPageNodeId != null && !((IEnumerable<CultureInfo>) siteIdByPageNodeId.PublicContentCultures).Any<CultureInfo>((Func<CultureInfo, bool>) (c => c.Name == culture.Name)))
            throw new InvalidOperationException(string.Format("Culture {0} is not available on site {1}.", (object) culture.Name, (object) siteIdByPageNodeId.Name));
          targetPageNode = this.pageManager.DuplicatePage(this.currentState, targetPageNode, culture, culture, parentId, duplicateHierarchy, fullCopy);
        }
        this.currentState = targetPageNode;
        if (parentId != Guid.Empty)
          this.MakeChildOf(parentId);
      }
      return this;
    }

    /// <summary>
    /// Makes the current page loaded at Fluent API a child of the page specified through the pageId parameter.
    /// </summary>
    /// <param name="pageId">An id of the page that is to become the parent of the current page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade MakeChildOf(Guid pageId) => !(pageId == Guid.Empty) ? this.MakeChildOf(this.PageManager.GetPageNode(pageId)) : throw new ArgumentException(nameof (pageId));

    /// <summary>
    /// Makes the current page loaded at Fluent API a child of the page specified through the pageId parameter.
    /// </summary>
    /// <param name="pageId">An id of the page that is to become the parent of the current page.</param>
    /// <param name="storeAsAdditionalUrl">if set to <c>true</c> [store as additional URL].</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade MakeChildOf(Guid pageId, bool storeAsAdditionalUrl)
    {
      if (pageId == Guid.Empty)
        throw new ArgumentException(nameof (pageId));
      return this.MakeChildOf(this.PageManager.GetPageNode(pageId), storeAsAdditionalUrl);
    }

    /// <summary>
    /// Makes the current page loaded at Fluent API a child of the page specified through the page parameter.
    /// </summary>
    /// <param name="page">A page that is to become the parent of the current page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade MakeChildOf(PageNode page)
    {
      this.EnsureState();
      this.PageManager.ChangeParent(this.currentState, page);
      return this;
    }

    /// <summary>
    /// Makes the current page loaded at Fluent API a child of the page specified through the page parameter.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="storeAsAdditionalUrl">if set to <c>true</c> [store as additional URL].</param>
    internal virtual PageFacade MakeChildOf(PageNode page, bool storeAsAdditionalUrl)
    {
      this.EnsureState();
      this.PageManager.ChangeParent(this.currentState, page, storeAsAdditionalUrl);
      return this;
    }

    /// <summary>
    /// Makes the current page loaded at Fluent API a homepage.
    /// </summary>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade SetAsHomePage()
    {
      this.EnsureState();
      using (SiteRegion.FromSiteMapRoot(this.currentState.RootNodeId))
      {
        this.PageManager.SetHomePage(this.currentState.Id);
        return this;
      }
    }

    /// <summary>
    /// Makes the current page loaded at Fluent API a parent of the page specified through the pageId parameter.
    /// </summary>
    /// <param name="pageId">An id of the page that is to become the child of the current page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade MakeParentOf(Guid pageId) => !(pageId == Guid.Empty) ? this.MakeParentOf(this.PageManager.GetPageNode(pageId)) : throw new ArgumentException(nameof (pageId));

    /// <summary>
    /// Makes the current page loaded at Fluent API a parent of the page specified through the page parameter.
    /// </summary>
    /// <param name="page">A page that is to become a child of the current page.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade MakeParentOf(PageNode page)
    {
      this.EnsureState();
      this.PageManager.ChangeParent(page, this.currentState);
      return this;
    }

    /// <summary>
    /// Moves the current page loaded at Fluent API to one of the relative positions predefined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.MoveTo" /> enumeration.
    /// </summary>
    /// <param name="moveTo">
    /// A value representing the new position to which the page ought to be moved.
    /// </param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Move(MoveTo moveTo)
    {
      this.EnsureState();
      this.PageManager.MovePageNode(this.currentState, moveTo);
      return this;
    }

    /// <summary>
    /// Moves the page to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration, relative to the
    /// supplied target page id.
    /// </summary>
    /// <param name="place">
    /// A value representing the place to which the page ought to be moved.
    /// </param>
    /// <param name="targetPageId">
    /// Id of the page that serves as a reference point to the new placing of the page.
    /// </param>
    /// <returns>
    /// An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.
    /// </returns>
    public virtual PageFacade Move(Place place, Guid targetPageId)
    {
      this.PageManager.MovePageNode(this.currentState, targetPageId, place);
      return this;
    }

    /// <summary>
    /// Moves the page to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration, relative to the
    /// supplied target page.
    /// </summary>
    /// <param name="place">
    /// A value representing the place to which the page ought to be moved.
    /// </param>
    /// <param name="targetPage">
    /// An instance of the page that serves as a reference point to the new placing of the page.
    /// </param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Move(Place place, PageNode targetPage)
    {
      this.EnsureState();
      this.PageManager.MovePageNode(this.currentState, targetPage, place);
      return this;
    }

    /// <summary>
    /// Moves the current page loaded at Fluent API by specified number of places in the direction specified by the <see cref="!:Telerik.Sitefinity.Fluent.Move" />
    /// enumeration.
    /// </summary>
    /// <param name="move">
    /// A value representing the direction in which the page ought to be moved.
    /// </param>
    /// <param name="numberOfPlaces">
    /// Number of places by which the page ought to be moved.
    /// </param>
    /// <remarks>
    /// If the number of places is larger than the number of pages in the given direction, the page will be moved to
    /// the first or last place the level - depending on the <see cref="!:Telerik.Sitefinity.Fluent.Move" /> direction.
    /// </remarks>
    /// <exception cref="T:System.ArgumentException">thrown if parameter numberOfPlaces is not larger than 0.</exception>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade Move(Telerik.Sitefinity.Modules.Pages.Move move, int numberOfPlaces)
    {
      this.EnsureState();
      this.PageManager.MovePageNode(this.currentState, move, numberOfPlaces);
      return this;
    }

    /// <summary>Returns the page currently loaded by the fluent API.</summary>
    /// <returns>An instance of <see cref="!:Node" /> object.</returns>
    public PageNode Get()
    {
      this.EnsureState();
      return this.currentState;
    }

    /// <summary>
    /// Performs setting the page currently loaded by the fluent API to specified parameter object of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />.
    /// </summary>
    /// <param name="pageNode">The node to be set.</param>
    /// <returns></returns>
    public virtual PageFacade Set(PageNode pageNode)
    {
      this.currentState = pageNode;
      this.EnsureState();
      return this;
    }

    /// <summary>
    /// Saves all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public new virtual bool SaveChanges()
    {
      this.settings.TransactionItems[ItemTrackingStatus.Modified].ForEach((Action<IDataItem>) (item =>
      {
        if (!(item is PageNode pageNode2))
          return;
        if (string.IsNullOrEmpty((string) pageNode2.Title))
          pageNode2.Title = pageNode2.Name.IsNullOrEmpty() ? (Lstring) pageNode2.Id.ToString() : (Lstring) pageNode2.Name;
        if (!string.IsNullOrEmpty((string) pageNode2.UrlName))
          return;
        pageNode2.UrlName = (Lstring) CommonMethods.TitleToUrl((string) pageNode2.Title);
      }));
      base.SaveChanges();
      return true;
    }

    /// <summary>
    /// Commit the changes and return the current facade for additional fluent calls
    /// </summary>
    public virtual PageFacade SaveAndContinue()
    {
      this.SaveChanges();
      return this;
    }

    /// <summary>
    /// Cancels all the changes that were performed through the fluent API operations.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageFacade" /> object.</returns>
    public virtual PageFacade CancelChanges()
    {
      base.CancelChanges();
      return this;
    }

    /// <summary>Gets the template of the page</summary>
    /// <returns></returns>
    public virtual PageTemplate GetTemplate()
    {
      this.EnsureState();
      return this.PageManager.GetPageTemplate(this.currentState.Id);
    }

    /// <summary>
    /// An instance of child facade collection object of type <see cref="!:ControlsFacade" /> that allow managing control on multiple pages.
    /// </summary>
    /// <returns>
    /// An instance of child facade of type <see cref="!:ControlsFacade[PageFacade]" />.
    /// </returns>
    public virtual ControlsFacade<PageFacade> Controls() => new ControlsFacade<PageFacade>(this, this.appSettings);

    /// <summary>
    /// Loads the page into the API state. This method should be called only if the class have been
    /// constructed with the page id parameter.
    /// </summary>
    /// <param name="pageId">Id of the page that ought to be loaded in the API state.</param>
    protected virtual void LoadPage(Guid pageId) => this.currentState = !(pageId == Guid.Empty) ? this.PageManager.GetPageNode(pageId) : throw new ArgumentNullException(nameof (pageId));

    /// <summary>
    /// Ensures that the state of the facade has been initialized and throws an exception if not.
    /// </summary>
    protected virtual void EnsureState()
    {
      if (this.currentState == null)
        throw new InvalidOperationException("This method cannot be executed when the state of the facade is not initialized.");
    }

    /// <summary>
    /// Determines the identifier of root page depending on predefined <see cref="T:Telerik.Sitefinity.Fluent.Pages.PageLocation" /> enumeration and sets it as current page at fluent API.
    /// </summary>
    /// <param name="pageLocation">The page location.</param>
    /// <returns></returns>
    protected virtual PageNode GetLocationRoot(PageLocation pageLocation) => this.PageManager.GetLocationRoot(pageLocation);

    /// <summary>Gets site by PageNode Id or SiteMapRoot Id</summary>
    /// <param name="parentId">Id of the page or the SiteMapRoot</param>
    /// <returns>The site <see cref="T:Telerik.Sitefinity.Multisite.ISite" /></returns>
    private ISite GetSiteIdByPageNodeId(Guid parentId) => parentId != Guid.Empty ? SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(this.PageManager.GetPageNode(parentId).RootNodeId) ?? SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(parentId) : (ISite) null;

    /// <summary>
    /// Create a new instance of the manager in a named transaction using <see cref="!:settings" />
    /// </summary>
    /// <returns>Instance of this facade's manager</returns>
    /// <remarks>This is called internally by <see cref="!:GetManager" />. Do not call this manually unless you override GetManager as well.</remarks>
    protected override IManager InitializeManager() => (IManager) this.PageManager;
  }
}
