// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.PagesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Renderer.Editor.Specifics;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.RelatedData.Messages;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Taxonomies.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;
using Telerik.Sitefinity.Versioning.Web.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Sitefinity.Workflow;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>The WCF web service that is used for page management</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class PagesService : IPagesService
  {
    private static object syncLock = new object();

    /// <summary>
    /// Gets the collection of top level pages and returns the result in JSON format.
    /// </summary>
    /// <param name="pageFilter">Filter expression to be applied.</param>
    /// <param name="hierarchyMode">The hierarchy mode.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">Skip count.</param>
    /// <param name="take">Take count.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="root">The root.</param>
    /// <returns>
    /// A collection context that contains the selected pages.
    /// </returns>
    public CollectionContext<PageViewModel> GetPages(
      string pageFilter,
      bool hierarchyMode,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetPagesInternal(hierarchyMode, root, pageFilter, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the collection of top level pages and returns the result in XML format.
    /// </summary>
    /// <param name="pageFilter">Filter expression to be applied.</param>
    /// <param name="hierarchyMode">The hierarchy mode.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <param name="skip">Skip count.</param>
    /// <param name="take">Take count.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="root">The root.</param>
    /// <returns>
    /// A collection context that contains the selected pages.
    /// </returns>
    public CollectionContext<PageViewModel> GetPagesInXml(
      string pageFilter,
      bool hierarchyMode,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetPagesInternal(hierarchyMode, root, pageFilter, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the child pages.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>The child pages.</returns>
    public CollectionContext<PageViewModel> GetChildPages(
      string parentId,
      string provider,
      string filter)
    {
      return this.GetChildPagesFromSitemap<PageViewModel>(new Guid(parentId), filter, (string) null, (Func<PageSiteNode, PageViewModel>) (siteNode => new PageViewModel(siteNode, true)));
    }

    /// <summary>Gets the child pages in XML.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <returns>The child pages in xml.</returns>
    public CollectionContext<PageViewModel> GetChildPagesInXml(
      string parentId,
      string provider,
      string filter)
    {
      this.GetChildPagesFromSitemap<PageViewModel>(new Guid(parentId), filter, (string) null, (Func<PageSiteNode, PageViewModel>) (siteNode => new PageViewModel(siteNode, true)));
      return (CollectionContext<PageViewModel>) null;
    }

    /// <summary>
    /// Gets the path of pages up to the root page, starting with the designated page and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in JSON format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The page filter.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in JSON format.</returns>
    public CollectionContext<PageViewModel> GetPredecessorPages(
      string pageId,
      string provider,
      string filter)
    {
      return this.GetPredecessorPagesInternal(pageId, filter);
    }

    /// <summary>
    /// Gets the path of pages up to the root page, starting with the designated page and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in XML format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The page filter.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in XML format.</returns>
    public CollectionContext<PageViewModel> GetPredecessorPagesInXml(
      string pageId,
      string provider,
      string filter)
    {
      return this.GetPredecessorPagesInternal(pageId, filter);
    }

    /// <summary>Gets pages as tree.</summary>
    /// <param name="leafIds">The ids of the leaf pages.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="nodesLimit">The nodes limit.</param>
    /// <param name="perLevelLimit">The limit per level.</param>
    /// <param name="perSubtreeLimit">The limit per subtree.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    public CollectionContext<PageViewModel> GetPagesAsTree(
      string[] leafIds,
      string provider,
      int nodesLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root)
    {
      return this.GetPagesAsTreeFromSitemap(leafIds, provider, nodesLimit, perLevelLimit, perSubtreeLimit, subtreesLimit, root);
    }

    /// <summary>Gets pages as tree in XML.</summary>
    /// <param name="leafIds">The ids of the leaf pages.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="nodesLimit">The nodes limit.</param>
    /// <param name="perLevelLimit">The limit per level.</param>
    /// <param name="perSubtreeLimit">The limit per subtree.</param>
    /// <param name="subtreesLimit">The subtrees limit.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /></returns>
    public CollectionContext<PageViewModel> GetPagesAsTreeInXml(
      string[] leafIds,
      string provider,
      int nodesLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root)
    {
      return this.GetPagesAsTreeFromSitemap(leafIds, provider, nodesLimit, perLevelLimit, perSubtreeLimit, subtreesLimit, root);
    }

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="deletedLanguage">The deleted language</param>
    /// <param name="checkRelatingData">The check relating data.</param>
    /// <returns>True if the deletion is successful; otherwise false.</returns>
    public bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData)
    {
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="deletedLanguage">The deleted language</param>
    /// <param name="checkRelatingData">The check relating data.</param>
    /// <returns>True if the deletion is successful; otherwise false.</returns>
    public bool BatchDeleteContentInXml(
      string[] Ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData)
    {
      return this.BatchDeleteContentInternal(Ids, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes the page and returns true if the page has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the page to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <param name="deletedLanguage">The deleted language</param>
    /// <param name="checkRelatingData">The check relating data.</param>
    /// <returns>True if the page has been deleted; otherwise false.</returns>
    public bool DeletePage(
      string pageId,
      string providerName,
      bool duplicate,
      string deletedLanguage,
      bool checkRelatingData)
    {
      return this.DeletePageInternal(pageId, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>
    /// Deletes the page and returns true if the page has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <param name="deletedLanguage">The deleted language</param>
    /// <param name="checkRelatingData">The check relating data.</param>
    /// <returns>True if the page has been deleted; otherwise false.</returns>
    public bool DeletePageInXml(
      string pageId,
      string providerName,
      bool duplicate,
      string deletedLanguage,
      bool checkRelatingData)
    {
      return this.DeletePageInternal(pageId, providerName, deletedLanguage, checkRelatingData);
    }

    /// <summary>Gets information about the page version.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.PageTemplateDraftVersionInfo" />.</returns>
    public PageTemplateDraftVersionInfo GetPageVersionInfo(
      string itemId,
      string versionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id = new Guid(itemId);
      Guid version = new Guid(versionId);
      PageData pageData1 = PageManager.GetManager().GetPageData(id);
      Change change = VersionManager.GetManager().GetChanges().Where<Change>((Expression<Func<Change, bool>>) (u => u.Id == version)).First<Change>();
      Change previousChange = VersionManager.GetManager().GetPreviousChange(change);
      Change nextChange = VersionManager.GetManager().GetNextChange(change);
      bool flag = false;
      IEnumerable<CultureInfo> changeCultures = change.GetLanguagesFromMetadata().Select<string, CultureInfo>((Func<string, CultureInfo>) (x => CultureInfo.GetCultureInfo(x)));
      if (pageData1.NavigationNode.LocalizationStrategy == LocalizationStrategy.Synced)
        flag = ((IEnumerable<CultureInfo>) pageData1.NavigationNode.AvailableCultures).Any<CultureInfo>((Func<CultureInfo, bool>) (x => !changeCultures.Contains<CultureInfo>(x)));
      if (string.IsNullOrEmpty(change.Metadata))
      {
        PageData pageData2 = PageManager.GetManager().GetPageData(new Guid(itemId));
        change.Metadata = pageData2.Culture;
      }
      PageTemplateDraftVersionInfo pageVersionInfo = new PageTemplateDraftVersionInfo()
      {
        VersionInfo = new WcfChange(change),
        IsLockedByCurrentUser = pageData1.LockedBy == SecurityManager.GetCurrentUserId(),
        IsLocked = pageData1.LockedBy != Guid.Empty,
        IsEditable = this.IsPageEditable(pageData1.NavigationNode),
        IsContentEditable = this.IsContentEditable(pageData1.NavigationNode),
        HasConflict = flag
      };
      if (pageVersionInfo.IsLocked)
        pageVersionInfo.LockedByUser = UserProfilesHelper.GetUserDisplayName(pageData1.LockedBy);
      pageVersionInfo.PageTitle = !changeCultures.Contains<CultureInfo>(SystemManager.CurrentContext.Culture) ? ((IHasTitle) pageData1.NavigationNode).GetTitle(pageVersionInfo.VersionInfo.CultureInfo) : (string) pageData1.NavigationNode.Title;
      pageVersionInfo.VersionInfo.PreviousId = previousChange != null ? previousChange.Id.ToString() : string.Empty;
      pageVersionInfo.VersionInfo.NextId = nextChange != null ? nextChange.Id.ToString() : string.Empty;
      pageVersionInfo.VersionInfo.NextVersionNumber = nextChange != null ? nextChange.Version : -1;
      pageVersionInfo.VersionInfo.PrevVersionNumber = previousChange != null ? previousChange.Version : -1;
      ServiceUtility.DisableCache();
      return pageVersionInfo;
    }

    /// <summary>Copies the draft page as a new draft.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    public void CopyDraftPageAsNewDraft(string itemId, string versionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid pageDataId = new Guid(itemId);
      PageManager manager = PageManager.GetManager();
      PageDraft draft = manager.EditPage(pageDataId, true);
      PagesService.CopyVersionToDraft(versionId, manager, (IControlsContainer) draft);
      manager.SaveChanges();
    }

    /// <summary>Gets information about the template version.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.PageTemplateDraftVersionInfo" />.</returns>
    public PageTemplateDraftVersionInfo GetTemplateVersionInfo(
      string itemId,
      string versionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid id = new Guid(itemId);
      Guid version = new Guid(versionId);
      PageTemplate template = PageManager.GetManager().GetTemplate(id);
      Change change = VersionManager.GetManager().GetChanges().Where<Change>((Expression<Func<Change, bool>>) (u => u.Id == version)).First<Change>();
      Change previousChange = VersionManager.GetManager().GetPreviousChange(change);
      Change nextChange = VersionManager.GetManager().GetNextChange(change);
      PageTemplateDraftVersionInfo templateVersionInfo = new PageTemplateDraftVersionInfo()
      {
        VersionInfo = new WcfChange(change),
        IsLockedByCurrentUser = template.LockedBy == SecurityManager.GetCurrentUserId(),
        IsLocked = template.LockedBy != Guid.Empty
      };
      templateVersionInfo.IsEditable = this.IsTemplateEditable(template);
      templateVersionInfo.IsContentEditable = templateVersionInfo.IsEditable;
      if (templateVersionInfo.IsLocked)
        templateVersionInfo.LockedByUser = UserProfilesHelper.GetUserDisplayName(template.LockedBy);
      IList<string> languagesFromMetadata = change.GetLanguagesFromMetadata();
      templateVersionInfo.PageTitle = !languagesFromMetadata.Select<string, CultureInfo>((Func<string, CultureInfo>) (x => CultureInfo.GetCultureInfo(x))).Contains<CultureInfo>(SystemManager.CurrentContext.Culture) ? ((IHasTitle) template).GetTitle(templateVersionInfo.VersionInfo.CultureInfo) : (string) template.Title;
      templateVersionInfo.VersionInfo.PreviousId = previousChange != null ? previousChange.Id.ToString() : string.Empty;
      templateVersionInfo.VersionInfo.NextId = nextChange != null ? nextChange.Id.ToString() : string.Empty;
      templateVersionInfo.VersionInfo.NextVersionNumber = nextChange != null ? nextChange.Version : -1;
      templateVersionInfo.VersionInfo.PrevVersionNumber = previousChange != null ? previousChange.Version : -1;
      ServiceUtility.DisableCache();
      return templateVersionInfo;
    }

    /// <summary>Copies the draft template as new draft.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    public void CopyDraftTemplateAsNewDraft(string itemId, string versionId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid pageTemplateId = new Guid(itemId);
      PageManager manager = PageManager.GetManager();
      TemplateDraft draft = manager.EditTemplate(pageTemplateId, true);
      PagesService.CopyVersionToDraft(versionId, manager, (IControlsContainer) draft);
      manager.SaveChanges();
    }

    /// <summary>Gets the single page and returns it in JSON format.</summary>
    /// <param name="pageId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    public WcfPageContext GetPage(string pageId, string providerName, bool duplicate) => this.GetPageInternal(pageId, providerName, duplicate);

    /// <summary>Gets the single page and returns it in XML format.</summary>
    /// <param name="pageId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    public WcfPageContext GetPageInXml(
      string pageId,
      string providerName,
      bool duplicate)
    {
      return this.GetPageInternal(pageId, providerName, duplicate);
    }

    /// <summary>Saves the page.</summary>
    /// <param name="pageContext">The page context.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageContext" />.</returns>
    public WcfPageContext SavePage(
      WcfPageContext pageContext,
      string pageId,
      string providerName,
      bool duplicate)
    {
      return this.SavePageInternal(pageContext, duplicate);
    }

    /// <summary>Saves the page in XML.</summary>
    /// <param name="pageContext">The page context,</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageContext" />.</returns>
    public WcfPageContext SavePageInXml(
      WcfPageContext pageContext,
      string pageId,
      string providerName,
      bool duplicate)
    {
      return this.SavePageInternal(pageContext, duplicate);
    }

    /// <summary>Batch saving pages.</summary>
    /// <param name="pageContexts">The page contexts.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    public CollectionContext<PageViewModel> BatchSavePage(
      WcfPage[] pageContexts,
      string providerName,
      string root)
    {
      return this.BatchSavePageInternal(pageContexts, providerName, root);
    }

    /// <summary>Batch saving pages in XML.</summary>
    /// <param name="pageContexts">The page contexts.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    public CollectionContext<PageViewModel> BatchSavePageInXml(
      WcfPage[] pageContexts,
      string providerName,
      string root)
    {
      return this.BatchSavePageInternal(pageContexts, providerName, root);
    }

    /// <summary>Batch placing pages.</summary>
    /// <param name="sourcePageIds">The source page ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="placePosition">The position at which to place the pages.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    public CollectionContext<PageViewModel> BatchPlacePage(
      string[] sourcePageIds,
      string providerName,
      string placePosition,
      string destination,
      string root)
    {
      return this.BatchPlacePageInternal(sourcePageIds, providerName, placePosition, destination, root);
    }

    /// <summary>Batch placing pages in XML.</summary>
    /// <param name="sourcePageIds">The source page ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="placePosition">The position at which to place the pages.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    public CollectionContext<PageViewModel> BatchPlacePageInXml(
      string[] sourcePageIds,
      string providerName,
      string placePosition,
      string destination,
      string root)
    {
      return this.BatchPlacePageInternal(sourcePageIds, providerName, placePosition, destination, root);
    }

    /// <summary>Batch moving pages.</summary>
    /// <param name="sourcePageIds">The source page ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="direction">The moving direction.</param>
    /// <param name="root">The root</param>
    public void BatchMovePage(
      string[] sourcePageIds,
      string providerName,
      string direction,
      string root)
    {
      this.BatchMovePageInternal(sourcePageIds, providerName, direction, root);
    }

    /// <summary>Batch moving pages in XML.</summary>
    /// <param name="sourcePageIds">The source page ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="direction">The moving direction.</param>
    /// <param name="root">The root.</param>
    public void BatchMovePageInXml(
      string[] sourcePageIds,
      string providerName,
      string direction,
      string root)
    {
      this.BatchMovePageInternal(sourcePageIds, providerName, direction, root);
    }

    /// <summary>Changes the template of the page</summary>
    /// <param name="pageNodeId">The page node id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns>A value indicating whether the template was changed successfully.</returns>
    public bool ChangeTemplate(string pageNodeId, string newTemplateId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      bool flag = false;
      Guid pageId = new Guid(pageNodeId);
      Guid templateId = new Guid(newTemplateId);
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        flag = this.TryChangeTemplate(fluentSitefinity.Page(pageId).AsStandardPage(), templateId, (string) null);
        fluentSitefinity.SaveChanges();
      }
      return flag;
    }

    /// <summary>Changes templates of multiple pages at once.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns>The pages that were skipped(template not changed).</returns>
    public string[] BatchChangeTemplate(string[] pageIDs, string newTemplateId) => this.BatchChangeTemplateInternal(pageIDs, newTemplateId).Values.ToArray<string>();

    internal IDictionary<Guid, string> BatchChangeTemplateInternal(
      string[] pageIDs,
      string newTemplateId,
      string templateName = null,
      bool handleExceptions = false)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<Guid> list = ((IEnumerable<string>) pageIDs).Select<string, Guid>((Func<string, Guid>) (id => new Guid(id))).ToList<Guid>();
      Guid templateId = new Guid(newTemplateId);
      if (PageTemplateHelper.IsOnDemandTempalteId(templateId))
        PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(templateId, true);
      Dictionary<Guid, string> lockedPages = new Dictionary<Guid, string>();
      using (FluentSitefinity fluent = App.WorkWith())
      {
        list.ForEach((Action<Guid>) (id =>
        {
          PageFacade pageFacade = fluent.Page(id);
          try
          {
            StandardPageFacade spf = pageFacade.AsStandardPage();
            if (this.TryChangeTemplate(spf, templateId, templateName))
              return;
            lockedPages.Add(spf.Get().Id, (string) spf.Get().Title);
          }
          catch (InvalidOperationException ex)
          {
            if (handleExceptions)
              lockedPages.Add(pageFacade.Get().Id, (string) pageFacade.Get().Title);
            else
              throw;
          }
        }));
        fluent.SaveChanges();
      }
      return (IDictionary<Guid, string>) lockedPages;
    }

    /// <summary>
    /// Makes a new template from the master file and add it as custom template.
    /// </summary>
    /// <param name="masterFilePath">The master file path.</param>
    /// <param name="rootTaxonType">The root taxon type.</param>
    /// <returns>The new template.</returns>
    public WcfPageTemplate MakeTemplateFromMasterFile(
      string masterFilePath,
      string rootTaxonType)
    {
      return this.MakeTemplateFromMasterFileInternal(masterFilePath, rootTaxonType);
    }

    /// <summary>
    /// Makes a new template from the master file and add it as custom template.
    /// </summary>
    /// <param name="masterFilePath">The master file path.</param>
    /// <param name="rootTaxonType">The root taxon type.</param>
    /// <returns>The new template.</returns>
    public WcfPageTemplate MakeTemplateFromMasterFileInXml(
      string masterFilePath,
      string rootTaxonType)
    {
      return this.MakeTemplateFromMasterFileInternal(masterFilePath, rootTaxonType);
    }

    /// <summary>Sets the home page.</summary>
    /// <param name="pageId">The page pageId.</param>
    public void SetHomePage(string pageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      try
      {
        Guid pageId1 = new Guid(pageId);
        PageFacade pageFacade = App.WorkWith().Page(pageId1);
        if (pageFacade.Get().RootNodeId == SiteInitializer.BackendRootNodeId)
        {
          ConfigManager manager = ConfigManager.GetManager();
          PagesConfig section = manager.GetSection<PagesConfig>();
          section.BackendHomePageId = pageId1;
          manager.SaveSection((ConfigSection) section);
        }
        else
          pageFacade.SetAsHomePage().SaveChanges();
      }
      catch (SecurityDemandFailException ex)
      {
        throw new SecurityDemandFailException(Res.Get<PageResources>().HomepageSettingFailedMessage);
      }
    }

    /// <summary>Sets the default template for frontend pages.</summary>
    /// <param name="templateId">The ID of the template to be used as default.</param>
    public void SetDefaultTemplate(string templateId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      MultisiteManager manager = MultisiteManager.GetManager();
      Site site = manager.GetSites().FirstOrDefault<Site>((Expression<Func<Site, bool>>) (s => s.Id == SystemManager.CurrentContext.CurrentSite.Id));
      if (site == null)
        return;
      site.DefaultFrontendTemplateId = new Guid(templateId);
      manager.SaveChanges();
    }

    /// <summary>Returns the default template for frontend pages.</summary>
    /// <returns>The default template.</returns>
    public WcfPageTemplate GetDefaultFrontendTemplateId() => this.GetDefaultTemplateIdInternal(false);

    /// <summary>Returns the default template for frontend pages.</summary>
    /// <returns>The default template.</returns>
    public WcfPageTemplate GetDefaultFrontendTemplateIdInXml() => this.GetDefaultTemplateIdInternal(false);

    /// <summary>Returns the default template for frontend pages.</summary>
    /// <returns>The default template.</returns>
    public WcfPageTemplate GetDefaultBackendTemplateId() => this.GetDefaultTemplateIdInternal(true);

    /// <summary>Returns the default template for frontend pages.</summary>
    /// <returns>The default template.</returns>
    public WcfPageTemplate GetDefaultBackendTemplateIdInXml() => this.GetDefaultTemplateIdInternal(true);

    /// <summary>Publishes the draft.</summary>
    /// <param name="pageNodeId">The page draft pageId.</param>
    public void PublishDraft(string pageNodeId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager();
      this.PublishPageInternal(manager, new Guid(pageNodeId));
      manager.SaveChanges();
    }

    /// <summary>Publishes multiple pages at once.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    public void BatchPublishDraft(string[] pageIDs)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      this.CallWorkflowBatch(pageIDs, "Publish");
    }

    private bool CallWorkflowBatch(
      string[] pageIDs,
      string operationName,
      string providerName = null,
      string deletedLanguage = null,
      bool checkRelatingData = false)
    {
      string empty = string.Empty;
      PageManager manager = PageManager.GetManager();
      IEnumerable<Guid> guids = ((IEnumerable<string>) pageIDs).Select<string, Guid>((Func<string, Guid>) (i => Guid.Parse(i)));
      HashSet<Guid> source = this.ConstructHomePagePath();
      if (!providerName.IsNullOrEmpty())
        manager = PageManager.GetManager(providerName);
      CultureInfo language = (CultureInfo) null;
      if (!deletedLanguage.IsNullOrEmpty())
        language = CultureInfo.GetCultureInfo(deletedLanguage);
      source.IntersectWith(guids);
      bool flag = source.Any<Guid>() && !this.AllPagesAreForDeletion(guids);
      int num = 0;
      WorkflowBatchExceptionHandler exceptionHandler = new WorkflowBatchExceptionHandler();
      foreach (Guid guid in guids)
      {
        try
        {
          PageNode pageNode = manager.GetPageNode(guid);
          empty = pageNode.Title.ToString();
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          if (operationName == "Delete")
          {
            if (flag && source.Contains(guid))
            {
              ++num;
              continue;
            }
            WorkflowManager.AddLanguageToWorkflowContext(dictionary, language);
            dictionary.Add("CheckRelatingData", checkRelatingData.ToString());
          }
          else if (pageNode.NodeType == NodeType.Standard)
            manager.SaveChanges();
          else
            continue;
          WorkflowManager.MessageWorkflow(guid, pageNode.GetType(), string.Empty, operationName, false, dictionary);
          if (language == null)
          {
            if (operationName == "Delete")
              CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
              {
                new CacheDependencyKey()
                {
                  Type = typeof (CacheDependencyObjectForAllSites),
                  Key = guid.ToString().ToUpper()
                }
              });
          }
        }
        catch (Exception ex)
        {
          exceptionHandler.RegisterException(ex, empty);
        }
      }
      exceptionHandler.ThrowAccumulatedErrorForPages(guids.Count<Guid>(), operationName);
      return num == 0;
    }

    /// <summary>Unpublishes a page.</summary>
    /// <param name="pageNodeId">The page node id.</param>
    public void UnpublishPage(string pageNodeId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = new PageManager();
      this.UnpublishPageInternal(manager, new Guid(pageNodeId));
      manager.SaveChanges();
    }

    /// <summary>Unpublishes multiple pages at once.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    public void BatchUnpublishPage(string[] pageIDs)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager();
      foreach (string pageId in pageIDs)
      {
        try
        {
          PageNode pageNode = manager.GetPageNode(new Guid(pageId));
          if (pageNode.NodeType == NodeType.Standard)
          {
            manager.EditPage(pageNode.GetPageData().Id, false);
            manager.SaveChanges();
            Dictionary<string, string> contextBag = new Dictionary<string, string>();
            WorkflowManager.MessageWorkflow(pageNode.Id, pageNode.GetType(), string.Empty, "Unpublish", false, contextBag);
          }
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.Global);
        }
      }
    }

    /// <summary>Duplicates the page.</summary>
    /// <param name="pageId">The page pageId.</param>
    /// <returns>The page view model for the duplicated page</returns>
    public PageViewModel DuplicatePage(string pageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageFacade pageFacade = App.WorkWith().Page(new Guid(pageId)).Duplicate();
      pageFacade.SaveChanges();
      return new PageViewModel(pageFacade.Get(), pageFacade.PageManager);
    }

    /// <summary>Moves the page up.</summary>
    /// <param name="pageId">The page id.</param>
    public void MovePageUp(string pageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      App.WorkWith().Page(new Guid(pageId)).Move(Move.Up, 1).SaveChanges();
    }

    /// <summary>Moves the page down.</summary>
    /// <param name="pageId">The page id.</param>
    public void MovePageDown(string pageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      App.WorkWith().Page(new Guid(pageId)).Move(Move.Down, 1).SaveChanges();
    }

    /// <summary>Moves the page before the supplied target page.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="targetPageId">The target page id.</param>
    public void PlaceBefore(string pageId, string targetPageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      App.WorkWith().Page(new Guid(pageId)).Move(Place.Before, new Guid(targetPageId)).SaveChanges();
    }

    /// <summary>Moves the page after the supplied target page.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="targetPageId">The target page id.</param>
    public void PlaceAfter(string pageId, string targetPageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      App.WorkWith().Page(new Guid(pageId)).Move(Place.After, new Guid(targetPageId)).SaveChanges();
    }

    /// <summary>Changes the page owner.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="userId">The user id.</param>
    public void ChangePageOwner(string pageId, string userId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager();
      PageNode pageNode = manager.GetPageNode(new Guid(pageId));
      if (pageNode == null)
        return;
      manager.ChangeOwner(pageNode, new Guid(userId));
      manager.SaveChanges();
    }

    /// <summary>Changes the page owner.</summary>
    /// <param name="pageIDs">The page id.</param>
    /// <param name="userId">The user id.</param>
    public void BatchChangePageOwner(string[] pageIDs, string userId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid userGuid = new Guid(userId);
      List<Guid> list = ((IEnumerable<string>) pageIDs).Select<string, Guid>((Func<string, Guid>) (id => new Guid(id))).ToList<Guid>();
      PageManager pageManager = PageManager.GetManager();
      Action<Guid> action = (Action<Guid>) (id => pageManager.ChangeOwner(pageManager.GetPageNode(id), userGuid));
      list.ForEach(action);
      pageManager.SaveChanges();
    }

    /// <summary>Changes the page parent.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="newParentPageId">The new parent page id.</param>
    public void ChangePageParent(string pageId, string newParentPageId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (PagesService.syncLock)
      {
        PageManager manager = PageManager.GetManager();
        PageNode pageNode1 = manager.GetPageNode(new Guid(pageId));
        if (pageNode1 == null)
          throw new ArgumentNullException("page");
        PageNode pageNode2 = manager.GetPageNode(new Guid(newParentPageId));
        if (pageNode1.Id == pageNode2.Id)
          throw new ArgumentException("The page cannot become the child of itself.", "page");
        if (pageNode2.Parent != null && pageNode1.Id == pageNode2.Parent.Id)
          throw new ArgumentException("The child page cannot become parent of it's own parent.", "page");
        manager.ChangeParent(pageNode1, pageNode2);
        manager.SaveChanges();
      }
    }

    /// <summary>Gets the children.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The children.</returns>
    public CollectionContext<WcfPageNode> GetChildren(
      string parentId,
      string provider,
      string filter,
      string siteId)
    {
      return this.GetChildPagesFromSitemap<WcfPageNode>(new Guid(parentId), filter, siteId, (Func<PageSiteNode, WcfPageNode>) (siteNode => new WcfPageNode(siteNode)));
    }

    /// <summary>Gets the children in XML.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="filter">The filter.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The children in XML.</returns>
    public CollectionContext<WcfPageNode> GetChildrenInXml(
      string parentId,
      string provider,
      string filter,
      string siteId)
    {
      return this.GetChildPagesFromSitemap<WcfPageNode>(new Guid(parentId), filter, siteId, (Func<PageSiteNode, WcfPageNode>) (siteNode => new WcfPageNode(siteNode)));
    }

    /// <summary>Checks the page for changes.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageStatus">The page status.</param>
    /// <param name="pageVersion">The page version.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The page state.</returns>
    public CurrentPageState CheckPageForChanges(
      string pageId,
      string pageStatus,
      int pageVersion,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!pageId.IsGuid())
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Invalid control ID format.", (Exception) null);
      CurrentPageState currentPageState = new CurrentPageState()
      {
        ItemState = ItemState.Ready
      };
      Guid guid = Guid.Parse(pageId);
      Guid lockedById;
      currentPageState.ItemState = guid.IsLiveLocked(DesignMediaType.Page, out lockedById);
      if (currentPageState.ItemState == ItemState.Locked)
      {
        User user = UserManager.FindUser(lockedById);
        currentPageState.Message = Res.Get<PageResources>().PageIsLockedBy.Arrange((object) user.UserName);
      }
      if (currentPageState.ItemState == ItemState.Ready)
      {
        PageData pageData = PageManager.GetManager().GetPageData(guid);
        if (pageData.Version != pageVersion || pageData.Status.ToString() != pageStatus || pageData.Drafts.Any<PageDraft>((Func<PageDraft, bool>) (d => d.Version > pageVersion)))
          currentPageState.ItemState = ItemState.Updated;
      }
      switch (currentPageState.ItemState)
      {
        case ItemState.Deleted:
          currentPageState.Message = Res.Get<PageResources>().PageHasBeenDeletedInBackend;
          break;
        case ItemState.Updated:
          currentPageState.Message = Res.Get<PageResources>().PageHasBeenChangedInBackend;
          break;
      }
      ServiceUtility.DisableCache();
      return currentPageState;
    }

    /// <inheritdoc />
    public void RestoreTemplateToDefault(string templateId) => this.RestoreTemplateToDefaultInternal(templateId);

    /// <inheritdoc />
    public void RestoreTemplateToDefaultInXml(string templateId) => this.RestoreTemplateToDefaultInternal(templateId);

    /// <summary>Gets the page template.</summary>
    /// <param name="pageId">The page data id.</param>
    /// <returns>The page template.</returns>
    public WcfPageTemplate GetPageTemplate(string pageId) => this.GetPageTemplateInternal(pageId);

    /// <summary>Gets the page template in XML.</summary>
    /// <param name="pageId">The page data id.</param>
    /// <returns>The page template in XML.</returns>
    public WcfPageTemplate GetPageTemplateInXml(string pageId) => this.GetPageTemplateInternal(pageId);

    internal WcfPage GetPageProxy(WcfPageContext pageContext)
    {
      WcfPage pageProxy = pageContext.Item;
      if (pageProxy.Parent != null && pageProxy.Parent.Id != Guid.Empty)
        pageProxy.ParentId = pageProxy.Parent.Id;
      if (pageProxy.RootId == Guid.Empty)
        pageProxy.RootId = SiteInitializer.CurrentFrontendRootNodeId;
      return pageProxy;
    }

    internal PageNode UpdateToPageNode(
      WcfPageContext pageContext,
      WcfPage proxy,
      FluentSitefinity fluent,
      IManager pageManager,
      PageFacade pageFacade,
      bool isNew,
      PageNode sourceNode)
    {
      Guid parentId = proxy.ParentId;
      int num = proxy.ParentId == Guid.Empty ? 1 : (proxy.ParentId == proxy.RootId ? 1 : 0);
      bool additionalUrlsChanged = false;
      bool flag = false;
      bool isLanguageVersion = proxy.SourceLanguagePageId != Guid.Empty;
      pageManager = fluent.Page().GetManager();
      if (num == 0)
      {
        flag = pageManager.Provider.SuppressSecurityChecks;
        pageManager.Provider.SuppressSecurityChecks = true;
      }
      PageNode pageNode1 = pageFacade.PageManager.GetPageNode(proxy.ParentId);
      if (num == 0)
        pageManager.Provider.SuppressSecurityChecks = flag;
      if (!isNew)
      {
        if (proxy.IsGroup)
          pageFacade = pageFacade.IfStandardPage().ConvertToPageGroup().Done();
        else if (!proxy.IsExternal)
          pageFacade = pageFacade.IfPageGroup().ConvertToStandardPage().Done();
      }
      else if (sourceNode != null)
      {
        if (sourceNode.NodeType != NodeType.Group && proxy.IsGroup)
          pageFacade = pageFacade.IfStandardPage().ConvertToPageGroup().Done();
        if (sourceNode.NodeType == NodeType.Group && !proxy.IsGroup && !proxy.IsExternal)
          pageFacade = pageFacade.IfPageGroup().ConvertToStandardPage().Done();
      }
      bool isLanguageVersionOfSplitPage = sourceNode != null && sourceNode.LocalizationStrategy == LocalizationStrategy.Split;
      if (!proxy.IsGroup && !proxy.IsExternal && isNew && !isLanguageVersion)
      {
        bool suppressSecurityChecks = pageManager.Provider.SuppressSecurityChecks;
        pageManager.Provider.SuppressSecurityChecks = true;
        Guid guid = proxy.Template == null ? Guid.Empty : proxy.Template.Id;
        if (PageTemplateHelper.IsOnDemandTempalteId(guid))
          PageTemplateHelper.CheckCreateOrMockAndReturnBasicEmptyTemplate(guid, true);
        pageFacade = pageFacade.AsStandardPage().CheckOut().SetTemplateTo(guid).Do((Action<PageDraft>) (x =>
        {
          ((IRendererCommonData) x).Renderer = proxy.Renderer;
          ((IRendererCommonData) x).TemplateName = proxy.TemplateName;
        })).CheckIn().Done();
        pageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
        pageNode1 = pageFacade.PageManager.GetPageNode(proxy.ParentId);
      }
      bool suppressSecurityChecks1 = pageManager.Provider.SuppressSecurityChecks;
      pageManager.Provider.SuppressSecurityChecks = true;
      int languageFallbackMode = (int) SystemManager.RequestLanguageFallbackMode;
      SystemManager.RequestLanguageFallbackMode = FallbackMode.NoFallback;
      pageFacade.Do((Action<PageNode>) (n => proxy.TransferToNode(n, pageFacade, sourceNode, isNew, isLanguageVersion, isLanguageVersionOfSplitPage, out additionalUrlsChanged)));
      SystemManager.RequestLanguageFallbackMode = (FallbackMode) languageFallbackMode;
      if (!isNew || !isLanguageVersion)
      {
        PageNode pageNode2 = pageFacade.Get();
        if (proxy.ParentId != Guid.Empty && pageNode1 != null && pageNode2.ParentId != proxy.ParentId)
        {
          if (!pageNode1.IsGranted("Pages", "Create"))
            throw new SecurityDemandFailException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Res.Get<PageResources>().CannotCreateChildPages, (object) pageNode1.Title));
          pageFacade.MakeChildOf(proxy.ParentId, false);
        }
      }
      pageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks1;
      return pageFacade.Get();
    }

    private WcfPageTemplate MakeTemplateFromMasterFileInternal(
      string masterFilePath,
      string rootTaxonType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      RootTaxonType rootTaxonType1 = RootTaxonType.Frontend;
      Guid category = SiteInitializer.CustomTemplatesCategoryId;
      if (!rootTaxonType.IsNullOrEmpty())
        rootTaxonType1 = (RootTaxonType) System.Enum.Parse(typeof (RootTaxonType), rootTaxonType);
      if (rootTaxonType1 == RootTaxonType.Backend)
        category = SiteInitializer.BackendTemplatesCategoryId;
      masterFilePath = VirtualPathUtility.ToAppRelative(masterFilePath);
      PageManager manager = PageManager.GetManager();
      PageTemplate pageTemplate;
      if (!manager.GetTemplates().Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.MasterPage == masterFilePath && t.Category == category)))
      {
        string str = VirtualPathUtility.GetFileName(masterFilePath);
        int num = str.LastIndexOf(".");
        if (num > -1)
          str = str.Sub(0, num - 1);
        pageTemplate = manager.CreateTemplate();
        pageTemplate.Name = str;
        pageTemplate.Title = (Lstring) str;
        pageTemplate.MasterPage = masterFilePath;
        pageTemplate.Category = category;
        if (manager.TransactionName.IsNullOrEmpty())
          manager.SaveChanges();
        else
          TransactionManager.CommitTransaction(manager.TransactionName);
      }
      else
        pageTemplate = manager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.MasterPage == masterFilePath && t.Category == category));
      return new WcfPageTemplate(pageTemplate);
    }

    private WcfPageContext GetPageInternal(
      string pageId,
      string providerName,
      bool duplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
      {
        "DraftLinksParser"
      });
      PageFacade pageFacade = App.WorkWith().Page(new Guid(pageId));
      PageNode pageNode = pageFacade.Get();
      PageManager pageManager = pageFacade.PageManager;
      WcfPage wcfPage = new WcfPage(pageNode, pageManager);
      this.ValidateViewModel((IEnumerable<WcfPageNode>) new WcfPage[1]
      {
        wcfPage
      }, wcfPage.RootId);
      if (RelatedDataHelper.CopyMasterItemRelations((IManager) pageManager, (IDataItem) pageNode))
        pageFacade.SaveChanges();
      WcfPageContext pageInternal = new WcfPageContext();
      pageInternal.Item = wcfPage;
      pageInternal.Warnings = duplicate ? (IEnumerable<ItemWarning>) new ItemWarning[0] : SystemManager.StatusProviderRegistry.GetWarnings(pageNode.Id, typeof (PageNode), providerName).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
      ServiceUtility.DisableCache();
      return pageInternal;
    }

    private CollectionContext<PageViewModel> GetPagesInternal(
      bool hierarchyMode,
      string root,
      string pageFilter,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid siteMapRootId = this.GetSiteMapRootId(root);
      using (SiteRegion.FromSiteMapRoot(siteMapRootId))
        return hierarchyMode ? this.GetChildPagesFromSitemap<PageViewModel>(siteMapRootId, filter, SystemManager.CurrentContext.CurrentSite.Id.ToString(), (Func<PageSiteNode, PageViewModel>) (siteNode => new PageViewModel(siteNode, true))) : this.GetPagesInternal(siteMapRootId, pageFilter, sortExpression, skip, take, filter);
    }

    private Guid GetSiteMapRootId(string root)
    {
      if (string.IsNullOrEmpty(root))
        return SiteInitializer.CurrentFrontendRootNodeId;
      Guid result;
      if (!Guid.TryParse(root, out result))
        result = !(root == "backend") ? SiteInitializer.CurrentFrontendRootNodeId : SiteInitializer.BackendRootNodeId;
      return result;
    }

    private SiteMapBase GetSitemapProviderFromRoot(string root = null)
    {
      if (root == null)
        root = HttpContext.Current.Request.QueryString[nameof (root)];
      if (root == "backend")
        return BackendSiteMap.GetCurrentProvider();
      return !(SiteMapBase.GetCurrentProvider() is SiteMapBase currentProvider) ? SitefinitySiteMap.GetCurrentProvider() as SiteMapBase : currentProvider;
    }

    private CultureInfo GetCultureForNode(bool isBackend)
    {
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      return isBackend ? appSettings.DefaultBackendLanguage : SystemManager.CurrentContext.Culture;
    }

    private CollectionContext<PageViewModel> GetPagesInternal(
      Guid location,
      string pageFilterString,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      using (SiteRegion.FromSiteMapRoot(location))
      {
        PagesFacade facade = App.WorkWith().Pages().FetchAllLanguages().LocatedIn(location);
        IQueryable<PageNode> query = this.FilterPages(facade, pageFilterString, filter, location).Get();
        int? nullable = new int?(0);
        sortExpression = !string.IsNullOrEmpty(sortExpression) ? sortExpression : "DateCreated";
        string orderExpression = sortExpression;
        int? skip1 = new int?(skip);
        int? take1 = new int?(take);
        ref int? local = ref nullable;
        IQueryable<PageNode> queryable = DataProviderBase.SetExpressions<PageNode>(query, (string) null, orderExpression, skip1, take1, ref local);
        List<PageViewModel> pageViewModelList = new List<PageViewModel>();
        foreach (PageNode pageNode in (IEnumerable<PageNode>) queryable)
        {
          if (this.IsPageVisible(pageNode))
          {
            PageViewModel pageViewModel = new PageViewModel(pageNode, facade.PageManager, true, true);
            bool flag1 = this.IsPageEditable(pageNode);
            bool flag2 = this.IsContentEditable(pageNode);
            pageViewModel.IsEditable = flag1;
            pageViewModel.IsContentEditable = flag2;
            pageViewModel.IsSubPageCreationAllowed = this.IsSubPageCreationAllowed(pageNode);
            pageViewModelList.Add(pageViewModel);
          }
        }
        this.ValidateViewModel((IEnumerable<WcfPageNode>) pageViewModelList, location);
        ServiceUtility.DisableCache();
        return new CollectionContext<PageViewModel>((IEnumerable<PageViewModel>) pageViewModelList)
        {
          TotalCount = nullable.Value
        };
      }
    }

    internal PagesFacade FilterPages(
      PagesFacade facade,
      string pageFilterString,
      string additionalFilter,
      Guid rootId)
    {
      if (!pageFilterString.IsNullOrEmpty())
      {
        switch (pageFilterString)
        {
          case "AllPages":
            break;
          case "AwaitingApproval":
            facade = facade.ThatAreWaitingForApproval();
            break;
          case "AwaitingMyAction":
            facade = facade.FilterByCulture().UseWorkflowFiltering().FilterByWorkflowStatus("AwaitingReview", "AwaitingApproval", "AwaitingPublishing").ThatCurrentUserCanApprove().Done();
            break;
          case "AwaitingPublishing":
            facade = facade.FilterByCulture().UseWorkflowFiltering().FilterByWorkflowStatus("AwaitingPublishing").Done();
            break;
          case "AwaitingReview":
            facade = facade.FilterByCulture().UseWorkflowFiltering().FilterByWorkflowStatus("AwaitingReview").Done();
            break;
          case "Draft":
            facade = facade.ThatAreDrafts();
            break;
          case "MyPages":
            facade = facade.ThatAreOwnedBy(ClaimsManager.GetCurrentUserId());
            break;
          case "Published":
            facade = facade.ThatArePublished();
            break;
          case "Rejected":
            facade = facade.FilterByCulture().UseWorkflowFiltering().FilterByWorkflowStatus("RejectedForPublishing", "RejectedForApproval", "RejectedForReview", "Rejected").Done();
            break;
          case "Scheduled":
            facade = facade.ThatAreScheduled();
            break;
          case "Unpublished":
            facade = facade.FilterByCulture().UseWorkflowFiltering().FilterByWorkflowStatus("Unpublished").Done();
            break;
          case "WithNoDescriptions":
            facade = facade.ThatHaveNoDescription();
            break;
          case "WithNoKeywords":
            facade = facade.ThatHaveNoKeywords();
            break;
          case "WithNoTitle":
            facade = facade.ThatHaveNoTitles();
            break;
          default:
            string name = PageManager.GetManager().Provider.Name;
            IEnumerable<Guid> result;
            if (SystemManager.StatusProviderRegistry.TryGetMatchingFilterItemIds(pageFilterString, typeof (PageNode), name, out result, SystemManager.CurrentContext.Culture, rootId.ToString()))
            {
              facade = facade.InPages(result);
              break;
            }
            break;
        }
      }
      if (!string.IsNullOrEmpty(additionalFilter))
        facade.Where(additionalFilter);
      return facade;
    }

    private CollectionContext<PageViewModel> GetPredecessorPagesInternal(
      string pageId,
      string filter)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SiteMapBase providerFromRoot = this.GetSitemapProviderFromRoot();
      SiteRegion siteRegion = (SiteRegion) null;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      Guid result;
      if (currentHttpContext != null && Guid.TryParse(new QueryStringBuilder(currentHttpContext.Request.Url.Query)["root"], out result))
        siteRegion = SiteRegion.FromSiteMapRoot(result);
      List<PageSiteNode> pageSiteNodeList = this.ConstructPageSubTree(this.ConstructPath(providerFromRoot.FindSiteMapNodeFromKey(pageId, false) as PageSiteNode, (SiteMapProvider) providerFromRoot, (HashSet<string>) null), providerFromRoot, filter);
      List<PageViewModel> pageViewModelList = new List<PageViewModel>();
      foreach (PageSiteNode siteNode in pageSiteNodeList)
      {
        if (this.IsPageVisible(siteNode))
        {
          PageViewModel pageViewModel = new PageViewModel(siteNode, true);
          pageViewModelList.Add(pageViewModel);
        }
      }
      if (pageViewModelList.Count > 0)
      {
        Guid rootId = pageViewModelList[0].RootId;
        this.ValidateViewModel((IEnumerable<WcfPageNode>) pageViewModelList, rootId);
      }
      ServiceUtility.DisableCache();
      siteRegion?.Dispose();
      return new CollectionContext<PageViewModel>((IEnumerable<PageViewModel>) pageViewModelList);
    }

    internal void ValidateViewModel(IEnumerable<WcfPageNode> list, Guid rootId)
    {
      ISite siteByDomain = SystemManager.CurrentContext.MultisiteContext.GetSiteByDomain(SystemManager.CurrentHttpContext.Request.Url.Authority);
      if (siteByDomain == null || !(rootId != siteByDomain.SiteMapRootNodeId))
        return;
      ISite siteBySiteMapRoot = SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(rootId);
      if (siteBySiteMapRoot == null)
        return;
      ISiteUrlResolver siteUrlResolver = (ISiteUrlResolver) null;
      if (siteBySiteMapRoot is MultisiteContext.SiteProxy siteProxy1 && siteByDomain is MultisiteContext.SiteProxy siteProxy2 && siteProxy2.Contains(siteProxy1) && SystemManager.CurrentContext.MultisiteContext is MultisiteContext multisiteContext)
        siteUrlResolver = (ISiteUrlResolver) new SubSiteUrlResolver(multisiteContext, siteProxy1, siteBySiteMapRoot.GetUri());
      if (siteUrlResolver == null)
        siteUrlResolver = (ISiteUrlResolver) new SiteUrlResolver(siteBySiteMapRoot.GetUri());
      foreach (WcfPageNode wcfPageNode in list)
      {
        if (wcfPageNode is PageViewModel pageViewModel && pageViewModel.Visible)
          pageViewModel.PageLiveUrl = siteUrlResolver.ResolveUrl(pageViewModel.CultureSpecificUrl);
        wcfPageNode.FullUrl = siteUrlResolver.ResolveUrl(wcfPageNode.FullUrl);
        if (!string.IsNullOrEmpty(wcfPageNode.LinkedNodeFullUrl))
          wcfPageNode.LinkedNodeFullUrl = siteUrlResolver.ResolveUrl(wcfPageNode.LinkedNodeFullUrl);
      }
    }

    private bool TryGetPage(Guid pageId, out PageNode page)
    {
      try
      {
        page = App.WorkWith().Page(pageId).Get();
        return true;
      }
      catch (ItemNotFoundException ex)
      {
        page = (PageNode) null;
        return false;
      }
    }

    private bool TryChangeTemplate(
      StandardPageFacade spf,
      Guid templateId,
      string compositeTemplateName)
    {
      bool flag = false;
      if (!spf.IsLocked(out Guid _))
      {
        PageTemplate startContainer = spf.PageManager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (x => x.Id == templateId));
        IRendererCommonData lastContainer1 = spf.Get().GetPageData().GetLastContainer<IRendererCommonData>();
        if (!string.IsNullOrEmpty(compositeTemplateName))
        {
          string[] strArray = compositeTemplateName.Split(new char[1]
          {
            '.'
          }, StringSplitOptions.RemoveEmptyEntries);
          if (strArray.Length <= 1)
            return false;
          string renderer = strArray[0];
          string templateName = strArray[1];
          if (lastContainer1 != null)
          {
            if (lastContainer1.Renderer != renderer || startContainer != null || !(templateId == Guid.Empty))
              return false;
            spf.CheckOut().Do((Action<PageDraft>) (x =>
            {
              ((IRendererCommonData) x).TemplateName = templateName;
              ((IRendererCommonData) x).Renderer = renderer;
              x.TemplateId = Guid.Empty;
            })).CheckIn().Done().Do((Action<PageNode>) (p => p.ApprovalWorkflowState = (Lstring) "Draft"));
            return true;
          }
        }
        else
        {
          IRendererCommonData lastContainer2 = startContainer.GetLastContainer<IRendererCommonData>();
          if (lastContainer2 != null && lastContainer1.Renderer != null && !string.Equals(lastContainer2.Renderer, lastContainer1.Renderer))
            return false;
        }
        spf.CheckOut().SetTemplateTo(templateId).CheckIn();
        spf.Done().Do((Action<PageNode>) (p => p.ApprovalWorkflowState = (Lstring) "Draft"));
        flag = true;
      }
      return flag;
    }

    private int GetNodesPerSubtreeLimit(int perSubtreeLimit) => perSubtreeLimit == 0 ? 50 : perSubtreeLimit;

    private bool BatchDeleteContentInternal(
      string[] ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData = false)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.CallWorkflowBatch(ids, "Delete", providerName, deletedLanguage, checkRelatingData);
    }

    private bool DeletePageInternal(
      string pageId,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      using (FluentSitefinity fluentSitefinity = App.WorkWith())
      {
        PageNode pageNode = fluentSitefinity.Page(new Guid(pageId)).Get();
        RelatedDataHelper.CheckRelatingData(checkRelatingData, new Guid(pageId), pageNode.GetType().FullName);
      }
      lock (PagesService.syncLock)
      {
        CultureInfo language = (CultureInfo) null;
        if (!string.IsNullOrEmpty(deletedLanguage) && SystemManager.CurrentContext.AppSettings.Multilingual)
          language = new CultureInfo(deletedLanguage);
        using (FluentSitefinity fluentSitefinity = App.WorkWith())
        {
          fluentSitefinity.Page(new Guid(pageId)).Delete(language);
          fluentSitefinity.SaveChanges();
        }
      }
      return true;
    }

    private WcfPageContext SavePageInternal(
      WcfPageContext pageContext,
      bool duplicate)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (PagesService.syncLock)
      {
        if (duplicate)
          return this.DuplicatePage(pageContext);
        WcfPage pageProxy = this.GetPageProxy(pageContext);
        Guid parentId = pageProxy.ParentId;
        int num = pageProxy.ParentId == Guid.Empty ? 1 : (pageProxy.ParentId == pageProxy.RootId ? 1 : 0);
        bool flag = false;
        bool isLanguageVersion = pageProxy.SourceLanguagePageId != Guid.Empty;
        FluentSitefinity fluent = App.WorkWith();
        IManager manager = fluent.Page().GetManager();
        PageNode sourceNode = (PageNode) null;
        if (num == 0)
        {
          flag = manager.Provider.SuppressSecurityChecks;
          manager.Provider.SuppressSecurityChecks = true;
        }
        bool isNew;
        PageFacade facade = pageProxy.GetFacade(fluent, isLanguageVersion, out isNew, out sourceNode);
        manager.Provider.SuppressSecurityChecks = flag;
        PageNode pageNode = this.UpdateToPageNode(pageContext, pageProxy, fluent, manager, facade, isNew, sourceNode);
        pageContext.Item.Attributes.CopyTo(pageNode.Attributes);
        RelatedDataHelper.SaveRelatedDataChanges(manager, (IDataItem) pageNode, pageContext.ChangedRelatedData, true);
        if (pageContext.ChangedRelatedData != null && pageContext.ChangedRelatedData.Length != 0)
          pageNode.LastModified = DateTime.UtcNow;
        pageContext.ChangedRelatedData = (ContentLinkChange[]) null;
        fluent.SaveChanges();
        if (isNew && pageNode.NodeType != NodeType.Standard)
        {
          List<CacheDependencyKey> items1 = new List<CacheDependencyKey>();
          CacheDependencyKey cacheDependencyKey = new CacheDependencyKey();
          cacheDependencyKey.Key = pageNode.ParentId.ToString();
          cacheDependencyKey.Type = typeof (CacheDependencyPageNodeStateChange);
          items1.Add(cacheDependencyKey);
          CacheDependency.Notify((IList<CacheDependencyKey>) items1);
          List<CacheDependencyKey> items2 = new List<CacheDependencyKey>();
          cacheDependencyKey = new CacheDependencyKey();
          cacheDependencyKey.Key = pageNode.ParentId.ToString() + (object) SystemManager.CurrentContext.Culture;
          cacheDependencyKey.Type = typeof (CacheDependencyPageNodeStateChange);
          items2.Add(cacheDependencyKey);
          CacheDependency.Notify((IList<CacheDependencyKey>) items2);
        }
        WcfPageContext wcfPageContext = new WcfPageContext();
        wcfPageContext.Item = new WcfPage(pageNode, facade.PageManager);
        wcfPageContext.Warnings = SystemManager.StatusProviderRegistry.GetWarnings(pageProxy.Id, typeof (PageNode), manager.Provider.Name).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
        return wcfPageContext;
      }
    }

    private void GuardForGroupExternalPageUnderSplitPage(WcfPage proxy, PageNode parentNode)
    {
    }

    private WcfPageContext DuplicatePage(WcfPageContext pageContext)
    {
      WcfPage proxy = pageContext.Item;
      if (proxy.Id == Guid.Empty)
        throw new ArgumentException("No page to duplicate");
      CultureInfo sourceCulture = SystemManager.CurrentContext.AppSettings.Multilingual ? SystemManager.CurrentContext.Culture : CultureInfo.InvariantCulture;
      CultureInfo targetCulture = !string.IsNullOrEmpty(proxy.Language) ? new CultureInfo(proxy.Language) : sourceCulture;
      ISite site = (ISite) null;
      if (proxy.RootId != Guid.Empty)
      {
        PageNode pageNode = PageManager.GetManager().GetPageNode(proxy.RootId);
        IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
        if (multisiteContext != null)
          site = multisiteContext.GetSiteBySiteMapRoot(pageNode.RootNodeId);
        if (site == null)
          site = SystemManager.CurrentContext.MultisiteContext.GetSiteBySiteMapRoot(proxy.RootId);
      }
      Guid siteId = site == null || !(site.Id != SystemManager.CurrentContext.CurrentSite.Id) ? Guid.Empty : site.Id;
      PageFacade pageFacade = App.WorkWith().Page(proxy.Id);
      WcfPage wcfPage;
      PageNode result;
      using (SiteRegion.FromSiteId(siteId))
      {
        targetCulture = targetCulture = SystemManager.CurrentContext.AppSettings.Multilingual ? targetCulture : SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        pageFacade = pageFacade.Duplicate(sourceCulture, targetCulture, proxy.Parent.Id, proxy.DuplicateChildren, false);
        result = pageFacade.Do((Action<PageNode>) (n =>
        {
          using (new CultureRegion(targetCulture))
          {
            if (n.NodeType == NodeType.Standard)
            {
              n.GetPageData(targetCulture).Status = ContentLifecycleStatus.Master;
              n.GetPageData(targetCulture).Version = 0;
              proxy.CopyPagePropertiesTo(pageFacade.PageManager, n.GetPageData(targetCulture));
              n.GetPageData(targetCulture).Visible = false;
            }
            proxy.ApplyCanonicalUrlSettingsTo(n);
            n.Title.ClearAllValues(true);
            n.Title.SetString(targetCulture, proxy.Title.PersistedValue);
            n.UrlName.ClearAllValues(true);
            n.Extension.ClearAllValues(true);
            if (!string.IsNullOrEmpty(proxy.UrlName) && proxy.UrlName.IndexOf(".") > 0)
            {
              int num = proxy.UrlName.IndexOf(".");
              string str1 = proxy.UrlName.Substring(num);
              n.Extension.SetString(targetCulture, str1);
              string str2 = proxy.UrlName.Substring(0, num);
              n.UrlName.SetString(targetCulture, str2);
            }
            else
            {
              n.Extension.SetString(targetCulture, string.Empty);
              n.UrlName.SetString(targetCulture, proxy.UrlName);
            }
            n.ShowInNavigation = proxy.ShowInNavigation;
            n.Urls.ClearUrls<PageUrlData>(true);
            pageFacade.PageManager.SetPageNodeAdditionalUrls(n, targetCulture, new string[0]);
            n.LocalizationStrategy = LocalizationStrategy.NotSelected;
            proxy.UpdateCustomFields(n);
            n.Attributes.Clear();
            proxy.Attributes.CopyTo(n.Attributes);
            n.IncludeInSearchIndex = proxy.IncludeInSearchIndex;
            n.Priority = proxy.Priority;
            n.RedirectUrl = (Lstring) proxy.RedirectUrl;
          }
        })).SaveAndContinue().Get();
        RelatedDataHelper.SaveRelatedDataChanges((IManager) pageFacade.PageManager, (IDataItem) result, pageContext.ChangedRelatedData, true);
        pageContext.ChangedRelatedData = (ContentLinkChange[]) null;
        if (result.NodeType == NodeType.Standard)
        {
          Guid templateId = proxy.Template != null ? proxy.Template.Id : Guid.Empty;
          this.TryChangeTemplate(pageFacade.AsStandardPage(), templateId, (string) null);
        }
        PermissionsInheritanceMap[] array = pageFacade.PageManager.GetInheritanceMaps().Where<PermissionsInheritanceMap>((Expression<Func<PermissionsInheritanceMap, bool>>) (p => p.ChildObjectId == result.Id && p.ObjectId != proxy.ParentId)).ToArray<PermissionsInheritanceMap>();
        for (int index = array.Length - 1; index >= 0; --index)
          pageFacade.PageManager.DeletePermissionsInheritanceMap(array[index]);
        pageFacade.SaveChanges();
        wcfPage = new WcfPage(result, pageFacade.PageManager);
      }
      WcfPageContext wcfPageContext = new WcfPageContext();
      wcfPageContext.Item = wcfPage;
      wcfPageContext.Warnings = SystemManager.StatusProviderRegistry.GetWarnings(result.Id, typeof (PageNode), pageFacade.PageManager.Provider.Name).Select<WarningInfo, ItemWarning>((Func<WarningInfo, ItemWarning>) (w => new ItemWarning(w)));
      return wcfPageContext;
    }

    private CollectionContext<PageViewModel> BatchSavePageInternal(
      WcfPage[] pagesContexts,
      string providerName,
      string root)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      lock (PagesService.syncLock)
      {
        PageManager pageManager = new PageManager();
        foreach (WcfPage pagesContext in pagesContexts)
        {
          PageNode pageNode1 = pageManager.GetPageNode(pagesContext.Id);
          if (pageNode1 == null)
            throw new ArgumentNullException("page");
          Guid parentId = pagesContext.ParentId;
          PageNode pageNode2 = pageManager.GetPageNode(pagesContext.ParentId);
          if (pageNode1.Id == pageNode2.Id)
            throw new ArgumentException("The page cannot become the child of itself.", "page");
          if (pageNode2.Parent != null && pageNode1.Id == pageNode2.Parent.Id)
            throw new ArgumentException("The child page cannot become parent of it's own parent.", "page");
          this.GuardForGroupExternalPageUnderSplitPage(pagesContext, pageNode2);
          if (pagesContext.ParentId != Guid.Empty && pageNode2 != null)
          {
            if (pageNode2.IsGranted("Pages", "Create"))
            {
              if (pageNode1.IsGranted("Pages", "Modify"))
              {
                bool suppressSecurityChecks = pageManager.Provider.SuppressSecurityChecks;
                pageManager.Provider.SuppressSecurityChecks = true;
                pageManager.ChangeParent(pageNode1, pageNode2);
                pageManager.Provider.SuppressSecurityChecks = suppressSecurityChecks;
                continue;
              }
            }
            if (!pageNode2.IsGranted("Pages", "Create"))
              throw new SecurityDemandFailException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Res.Get<PageResources>().CannotCreateChildPages, (object) pageNode2.Title));
            throw new SecurityDemandFailException(string.Format((IFormatProvider) CultureInfo.InvariantCulture, Res.Get<PageResources>().CannotModifyPage, (object) pageNode1.Title));
          }
        }
        pageManager.SaveChanges();
      }
      string filter = string.Join(" OR ", ((IEnumerable<WcfPage>) pagesContexts).Select<WcfPage, string>((Func<WcfPage, string>) (pc => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ID = {0}", (object) pc.Id))));
      return this.GetPagesInternal(this.GetSiteMapRootId(root), (string) null, (string) null, 0, 0, filter);
    }

    /// <summary>Batch placing pages.</summary>
    /// <param name="sourcePageIds">The source page ids.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="placePosition">The position at which to place the pages.</param>
    /// <param name="destination">The destination.</param>
    /// <param name="root">The root.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" />.</returns>
    private CollectionContext<PageViewModel> BatchPlacePageInternal(
      string[] sourcePageIds,
      string providerName,
      string placePosition,
      string destination,
      string root)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      Guid targetPageId = new Guid(destination);
      lock (PagesService.syncLock)
      {
        using (FluentSitefinity fluentSitefinity = App.WorkWith())
        {
          if (placePosition == "before")
          {
            for (int index = sourcePageIds.Length - 1; index > -1; --index)
              fluentSitefinity.Page(new Guid(sourcePageIds[index])).Move(Place.Before, targetPageId).SaveChanges();
          }
          else
          {
            foreach (string sourcePageId in sourcePageIds)
              fluentSitefinity.Page(new Guid(sourcePageId)).Move(Place.After, targetPageId).SaveChanges();
          }
          fluentSitefinity.SaveChanges();
        }
      }
      string filter = string.Join(" OR ", ((IEnumerable<string>) sourcePageIds).Select<string, string>((Func<string, string>) (sp => string.Format((IFormatProvider) CultureInfo.InvariantCulture, "ID = {0}", (object) sp))));
      return this.GetPagesInternal(this.GetSiteMapRootId(root), (string) null, (string) null, 0, 0, filter);
    }

    private void BatchMovePageInternal(
      string[] sourcePageIds,
      string providerName,
      string direction,
      string root)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      List<Guid> guidList = new List<Guid>(sourcePageIds.Length);
      foreach (string sourcePageId in sourcePageIds)
        guidList.Add(new Guid(sourcePageId));
      if (guidList.Count > 1)
        throw new NotSupportedException("The batch move operation is not supported");
      lock (PagesService.syncLock)
      {
        PageFacade pageFacade = App.WorkWith().Page(guidList[0]);
        if (direction == "up")
          pageFacade.Move(Move.Up, 1);
        else
          pageFacade.Move(Move.Down, 1);
        pageFacade.SaveChanges();
      }
    }

    /// <summary>Returns the default template for frontend pages.</summary>
    /// <param name="isBackend">Whether is backend.</param>
    /// <returns>The default template.</returns>
    private WcfPageTemplate GetDefaultTemplateIdInternal(bool isBackend)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      Guid id = Guid.Empty;
      id = !isBackend ? SystemManager.CurrentContext.CurrentSite.DefaultFrontendTemplateId : pagesConfig.DefaultBackendTemplateId;
      PageManager manager = PageManager.GetManager();
      if (!manager.GetTemplates(SystemManager.MultisiteContext.CurrentSite.Id).Any<PageTemplate>((Expression<Func<PageTemplate, bool>>) (p => p.Id == id)))
        id = Guid.Empty;
      PageTemplate pageTemplate1 = (PageTemplate) null;
      if (id != Guid.Empty)
      {
        pageTemplate1 = manager.GetTemplate(id);
      }
      else
      {
        IQueryable<PageTemplate> templates = manager.GetTemplates(SystemManager.CurrentContext.CurrentSite.Id);
        Expression<Func<PageTemplate, bool>> predicate = (Expression<Func<PageTemplate, bool>>) (t => t.Name.Contains("Bootstrap4.default"));
        foreach (PageTemplate pageTemplate2 in templates.Where<PageTemplate>(predicate).ToList<PageTemplate>())
        {
          if (pageTemplate2.Title.GetString(CultureInfo.InvariantCulture, false) == "default")
          {
            pageTemplate1 = pageTemplate2;
            break;
          }
        }
        if (pageTemplate1 == null)
        {
          pageTemplate1 = new PageTemplate();
          pageTemplate1.Id = Guid.Empty;
        }
      }
      return new WcfPageTemplate(pageTemplate1);
    }

    private void RestoreTemplateToDefaultInternal(string templateId)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      PageManager manager = PageManager.GetManager();
      TemplateInitializer templateInitializer = new TemplateInitializer(manager);
      Guid guid = Guid.Parse(templateId);
      PageTemplate template = manager.GetTemplate(guid);
      HierarchicalTaxon taxon = TaxonomyManager.GetManager().GetTaxon<HierarchicalTaxon>(template.Category);
      templateInitializer.InvokeMethod(guid, taxon, template.Ordinal, true);
      manager.SaveChanges();
    }

    private WcfPageTemplate GetPageTemplateInternal(string pageId)
    {
      PageTemplate pageTemplate = new PageManager().GetPageTemplate(new Guid(pageId));
      return pageTemplate != null ? new WcfPageTemplate(pageTemplate) : (WcfPageTemplate) null;
    }

    private Guid GetRootPageId(Guid pageId) => PageManager.GetManager().GetPageNode(pageId).RootNodeId;

    private static void CopyVersionToDraft(
      string versionId,
      PageManager pageManager,
      IControlsContainer draft)
    {
      Guid changeId = new Guid(versionId);
      VersionManager manager = VersionManager.GetManager();
      Change change = manager.GetChanges().Where<Change>((Expression<Func<Change, bool>>) (u => u.Id == changeId)).FirstOrDefault<Change>();
      if (change == null)
        return;
      Dictionary<Guid, int> dictionary1 = draft.Controls.ToDictionary<Telerik.Sitefinity.Pages.Model.ControlData, Guid, int>((Func<Telerik.Sitefinity.Pages.Model.ControlData, Guid>) (x => x.OriginalControlId), (Func<Telerik.Sitefinity.Pages.Model.ControlData, int>) (y => y.Version));
      int num1 = 0;
      if (dictionary1.Count > 0)
        num1 = dictionary1.Values.Max();
      int num2 = 0;
      Dictionary<Guid, int> dictionary2 = draft.Controls.SelectMany<Telerik.Sitefinity.Pages.Model.ControlData, Telerik.Sitefinity.Pages.Model.ControlData>((Func<Telerik.Sitefinity.Pages.Model.ControlData, IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>>) (c => (IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) c.PersonalizedControls)).ToDictionary<Telerik.Sitefinity.Pages.Model.ControlData, Guid, int>((Func<Telerik.Sitefinity.Pages.Model.ControlData, Guid>) (x => x.OriginalControlId), (Func<Telerik.Sitefinity.Pages.Model.ControlData, int>) (y => y.Version));
      if (dictionary2.Count > 0)
        num2 = dictionary2.Values.Max();
      PageDraft pageDraft = draft as PageDraft;
      TemplateDraft templateDraft = draft as TemplateDraft;
      Guid itemId = pageDraft != null ? pageDraft.ParentId : templateDraft.ParentId;
      manager.GetSpecificVersion((object) draft, itemId, change.Version);
      foreach (Telerik.Sitefinity.Pages.Model.ControlData control in draft.Controls)
      {
        control.SupportedPermissionSets = control.IsLayoutControl ? Telerik.Sitefinity.Pages.Model.ControlData.LayoutPermissionSets : Telerik.Sitefinity.Pages.Model.ControlData.ControlPermissionSets;
        control.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId((IManager) pageManager);
        if (control.PersonalizedControls.Count > 0)
        {
          foreach (Telerik.Sitefinity.Pages.Model.ControlData personalizedControl in (IEnumerable<Telerik.Sitefinity.Pages.Model.ControlData>) control.PersonalizedControls)
          {
            personalizedControl.SupportedPermissionSets = personalizedControl.IsLayoutControl ? Telerik.Sitefinity.Pages.Model.ControlData.LayoutPermissionSets : Telerik.Sitefinity.Pages.Model.ControlData.ControlPermissionSets;
            personalizedControl.ConvertPermissionsWithExternalIdToPermissionsWithCurrentId((IManager) pageManager);
            int num3;
            if (dictionary2.TryGetValue(personalizedControl.OriginalControlId, out num3))
              personalizedControl.Version = ++num3;
            else
              personalizedControl.Version = ++num2;
          }
        }
        int num4;
        if (dictionary1.TryGetValue(control.OriginalControlId, out num4))
          control.Version = ++num4;
        else
          control.Version = ++num1;
      }
      if (pageDraft != null)
        ++pageDraft.Version;
      else
        ++templateDraft.Version;
    }

    private HashSet<Guid> ConstructHomePagePath()
    {
      HashSet<Guid> guidSet = new HashSet<Guid>();
      Guid homePageId = SystemManager.CurrentContext.CurrentSite.HomePageId;
      if (homePageId != Guid.Empty)
      {
        SiteMapNode siteMapNodeFromKey = (SitefinitySiteMap.GetCurrentProvider() as SiteMapBase).FindSiteMapNodeFromKey(homePageId.ToString(), false);
        if (siteMapNodeFromKey != null)
        {
          SiteMapNode siteMapNode = siteMapNodeFromKey;
          Guid guid1 = Guid.Parse(siteMapNodeFromKey.Key);
          guidSet.Add(guid1);
          while (siteMapNode.ParentNode != null)
          {
            siteMapNode = siteMapNode.ParentNode;
            Guid guid2 = Guid.Parse(siteMapNode.Key);
            guidSet.Add(guid2);
          }
        }
      }
      return guidSet;
    }

    private bool AllPagesAreForDeletion(IEnumerable<Guid> pageForDeletionsIds)
    {
      SiteMapNodeCollection childNodes = (SitefinitySiteMap.GetCurrentProvider() as SiteMapBase).FindSiteMapNodeFromKey(SiteInitializer.CurrentFrontendRootNodeId.ToString(), false).ChildNodes;
      HashSet<Guid> source = new HashSet<Guid>();
      int num = 0;
      foreach (SiteMapNode siteMapNode in childNodes)
      {
        source.Add(Guid.Parse(siteMapNode.Key));
        ++num;
      }
      if (pageForDeletionsIds.Count<Guid>() == 1)
      {
        Guid guid = pageForDeletionsIds.First<Guid>();
        if (source.Contains(guid))
          return true;
      }
      source.IntersectWith(pageForDeletionsIds);
      return source.Count<Guid>() == num;
    }

    private void TreeMalformed() => throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<TaxonomyResources>().TaxonomyTreeMalformed, (Exception) null);

    private bool IsPageVisible(PageNode page)
    {
      bool flag = true;
      if (page.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null)
      {
        if (!page.IsGranted("Pages", "View"))
          flag = false;
      }
      return flag;
    }

    private bool IsPageEditable(PageNode page)
    {
      bool flag = true;
      if (page.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null && !page.IsEditable("Pages", "Modify"))
        flag = false;
      return flag;
    }

    private bool IsTemplateEditable(PageTemplate template)
    {
      bool flag = true;
      if (template.GetType().GetInterface(typeof (ISecuredObject).FullName) != (Type) null && !template.IsEditable("PageTemplates", "Modify"))
        flag = false;
      return flag;
    }

    private bool IsContentEditable(PageNode page) => page.IsGranted("Pages", "EditContent");

    private bool IsSubPageCreationAllowed(PageNode page) => page.IsGranted("Pages", "Create");

    private void PublishPageInternal(PageManager manager, Guid pageNodeId)
    {
      PageNode pageNode = manager.GetPageNode(pageNodeId);
      DateTime utcNow = DateTime.UtcNow;
      PageData pageData = pageNode.GetPageData();
      if (pageData == null)
        return;
      pageData.PublicationDate = utcNow;
      if (pageData.Status == ContentLifecycleStatus.Live)
      {
        pageData.Visible = true;
      }
      else
      {
        PageDraft pageDraft = manager.EditPage(pageData.Id, false);
        manager.PublishPageDraft(pageDraft.Id, true);
      }
    }

    private void UnpublishPageInternal(PageManager manager, Guid pageNodeId)
    {
      PageData pageData = manager.GetPageNode(pageNodeId).GetPageData();
      if (pageData == null)
        return;
      manager.UnpublishPage(pageData);
    }

    private bool IsPageVisible(PageSiteNode siteNode) => siteNode.IsGranted("View");

    private List<PageSiteNode> ConstructPath(
      PageSiteNode siteNode,
      SiteMapProvider provider,
      HashSet<string> previouslyVisitedNodes)
    {
      if (siteNode == null)
        throw new ArgumentNullException("pageNode");
      List<PageSiteNode> pagePath = new List<PageSiteNode>();
      pagePath.Add(siteNode);
      if (!siteNode.ParentKey.IsNullOrEmpty() && (previouslyVisitedNodes == null || !previouslyVisitedNodes.Contains(siteNode.Key)))
        this.VisitPageTaxon(siteNode, provider, pagePath, new HashSet<string>()
        {
          siteNode.Key
        }, previouslyVisitedNodes);
      return pagePath;
    }

    private void VisitPageTaxon(
      PageSiteNode siteNode,
      SiteMapProvider provider,
      List<PageSiteNode> pagePath,
      HashSet<string> visitedNodesIds,
      HashSet<string> previouslyVisitedNodes)
    {
      if (siteNode.ParentKey.IsNullOrEmpty() || string.Equals(siteNode.ParentKey, SiteInitializer.CurrentFrontendRootNodeId.ToString(), StringComparison.OrdinalIgnoreCase))
        return;
      if (visitedNodesIds.Contains(siteNode.ParentKey))
        this.TreeMalformed();
      if (previouslyVisitedNodes != null && previouslyVisitedNodes.Contains(siteNode.ParentKey))
        return;
      visitedNodesIds.Add(siteNode.ParentKey);
      PageSiteNode parentNode = provider.GetParentNode((SiteMapNode) siteNode) as PageSiteNode;
      pagePath.Insert(0, parentNode);
      this.VisitPageTaxon(parentNode, provider, pagePath, visitedNodesIds, previouslyVisitedNodes);
    }

    private List<PageSiteNode> ConstructPageSubTree(
      List<PageSiteNode> pagePath,
      SiteMapBase provider,
      string filter = null)
    {
      List<PageSiteNode> pageSiteNodeList = new List<PageSiteNode>(pagePath.Count);
      bool flag = !string.IsNullOrEmpty(filter);
      foreach (PageSiteNode childNode in pagePath)
      {
        if (!childNode.ParentKey.IsNullOrEmpty())
        {
          SiteMapNode parentNode = provider.GetParentNode((SiteMapNode) childNode, false);
          IEnumerable<PageSiteNode> pageSiteNodes = (IEnumerable<PageSiteNode>) provider.GetChildNodes(parentNode, false).Cast<PageSiteNode>().OrderBy<PageSiteNode, float>((Func<PageSiteNode, float>) (t => t.Ordinal));
          if (flag)
            pageSiteNodes = (IEnumerable<PageSiteNode>) pageSiteNodes.AsQueryable<PageSiteNode>().Where<PageSiteNode>(filter);
          pageSiteNodeList.AddRange(pageSiteNodes);
        }
      }
      return pageSiteNodeList;
    }

    private CollectionContext<PageViewModel> GetPagesAsTreeFromSitemap(
      string[] leafIds,
      string provider,
      int nodesLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SiteMapBase providerFromRoot = this.GetSitemapProviderFromRoot(root);
      using (new CultureRegion(this.GetCultureForNode(providerFromRoot is BackendSiteMap)))
      {
        List<PageViewModel> pageViewModelList = new List<PageViewModel>();
        bool flag1 = false;
        bool flag2 = false;
        if (leafIds.Length != 0)
        {
          HashSet<string> previouslyVisitedNodes = new HashSet<string>();
          foreach (string leafId in leafIds)
          {
            if (providerFromRoot.FindSiteMapNodeFromKey(leafId, false) is PageSiteNode siteMapNodeFromKey && this.IsPageVisible(siteMapNodeFromKey))
            {
              foreach (PageSiteNode siteNode in this.ConstructPageSubTree(this.ConstructPath(siteMapNodeFromKey, (SiteMapProvider) providerFromRoot, previouslyVisitedNodes), providerFromRoot))
              {
                if (!previouslyVisitedNodes.Contains(siteNode.Key) && this.IsPageVisible(siteNode))
                {
                  PageViewModel pageViewModel = new PageViewModel(siteNode, true);
                  pageViewModelList.Add(pageViewModel);
                  previouslyVisitedNodes.Add(siteNode.Key);
                }
              }
              foreach (PageSiteNode siteNode in providerFromRoot.GetChildNodes((SiteMapNode) siteMapNodeFromKey, false).Cast<PageSiteNode>().OrderBy<PageSiteNode, float>((Func<PageSiteNode, float>) (p => p.Ordinal)).ToList<PageSiteNode>())
              {
                if (!previouslyVisitedNodes.Contains(siteNode.Key) && this.IsPageVisible(siteNode))
                {
                  PageViewModel pageViewModel = new PageViewModel(siteNode, true);
                  pageViewModelList.Add(pageViewModel);
                  previouslyVisitedNodes.Add(siteNode.Key);
                }
              }
              flag2 = true;
            }
          }
          if (!flag1 & flag2)
          {
            ServiceUtility.DisableCache();
            this.ValidateViewModel((IEnumerable<WcfPageNode>) pageViewModelList, this.GetSiteMapRootId(root));
            return new CollectionContext<PageViewModel>((IEnumerable<PageViewModel>) pageViewModelList);
          }
        }
        return this.GetPagesInternal(true, root, (string) null, (string) null, 0, 0, (string) null);
      }
    }

    private CollectionContext<T> GetChildPagesFromSitemap<T>(
      Guid parentId,
      string filter,
      string siteId,
      Func<PageSiteNode, T> createViewModel)
      where T : WcfPageNode
    {
      ServiceUtility.RequestBackendUserAuthentication();
      SiteRegion siteRegion = (SiteRegion) null;
      Guid result;
      if (!siteId.IsNullOrEmpty() && Guid.TryParse(siteId, out result))
      {
        if (result != SystemManager.CurrentContext.CurrentSite.Id)
          siteRegion = SiteRegion.FromSiteId(result);
      }
      else if (parentId != Guid.Empty)
      {
        PageNode pageNode = App.WorkWith().Page(parentId).Get();
        siteRegion = !(pageNode.RootNodeId != Guid.Empty) ? SiteRegion.FromSiteMapRoot(parentId) : SiteRegion.FromSiteMapRoot(pageNode.RootNodeId);
      }
      if (parentId == Guid.Empty)
        parentId = SiteInitializer.CurrentFrontendRootNodeId;
      List<T> objList = new List<T>();
      try
      {
        SiteMapBase providerFromRoot = this.GetSitemapProviderFromRoot();
        using (new CultureRegion(this.GetCultureForNode(providerFromRoot is BackendSiteMap)))
        {
          SiteMapNode siteMapNodeFromKey = providerFromRoot.FindSiteMapNodeFromKey(parentId.ToString(), false);
          IEnumerable<PageSiteNode> source = providerFromRoot.GetChildNodes(siteMapNodeFromKey, false).OfType<PageSiteNode>();
          if (!string.IsNullOrEmpty(filter))
            source = (IEnumerable<PageSiteNode>) source.AsQueryable<PageSiteNode>().Where<PageSiteNode>(filter);
          foreach (PageSiteNode siteNode in source.OrderBy<PageSiteNode, float>((Func<PageSiteNode, float>) (p => p.Ordinal)).ToList<PageSiteNode>())
          {
            if ((siteNode.ModuleName.IsNullOrEmpty() || SystemManager.IsModuleEnabled(siteNode.ModuleName)) && this.IsPageVisible(siteNode))
            {
              T obj = createViewModel(siteNode);
              objList.Add(obj);
            }
          }
        }
      }
      finally
      {
        siteRegion?.Dispose();
      }
      if (objList.Count > 0)
      {
        Guid rootId = objList[0].RootId;
        this.ValidateViewModel((IEnumerable<WcfPageNode>) objList, rootId);
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<T>((IEnumerable<T>) objList);
    }
  }
}
