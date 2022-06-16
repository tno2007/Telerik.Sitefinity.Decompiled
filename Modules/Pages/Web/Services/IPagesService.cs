// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.IPagesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Web services for page management.</summary>
  [ServiceContract]
  [AllowPageDynamicFields]
  public interface IPagesService
  {
    /// <summary>
    /// Gets the collection of top level pages and returns the result in JSON format.
    /// </summary>
    /// <param name="pageFilter">Filter expression to be applied.</param>
    /// <returns>A collection context that contains the selected pages.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?pageFilter={pageFilter}&hierarchyMode={hierarchyMode}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<PageViewModel> GetPages(
      string pageFilter,
      bool hierarchyMode,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    /// <summary>
    /// Gets the collection of top level pages and returns the result in XML format.
    /// </summary>
    /// <param name="pageFilter">Filter expression to be applied.</param>
    /// <returns>A collection context that contains the selected pages.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/?pageFilter={pageFilter}&hierarchyMode={hierarchyMode}&sortExpression={sortExpression}&skip={skip}&take={take}&filter={filter}&root={root}")]
    CollectionContext<PageViewModel> GetPagesInXml(
      string pageFilter,
      bool hierarchyMode,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root);

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "children/{parentId}/?provider={provider}&filter={filter}")]
    CollectionContext<PageViewModel> GetChildPages(
      string parentId,
      string provider,
      string filter);

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/children/{parentId}/?provider={provider}&filter={filter}")]
    CollectionContext<PageViewModel> GetChildPagesInXml(
      string parentId,
      string provider,
      string filter);

    /// <summary>
    /// Gets the path of pages up to the root page, starting with the designated page and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in JSON format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "predecessor/{pageId}/?provider={provider}&filter={filter}")]
    CollectionContext<PageViewModel> GetPredecessorPages(
      string pageId,
      string provider,
      string filter);

    /// <summary>
    /// Gets the path of pages up to the root page, starting with the designated page and returns a collection
    /// of <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> in XML format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageFilter">The page filter.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort expression.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/predecessor/{pageId}/?provider={provider}&filter={filter}")]
    CollectionContext<PageViewModel> GetPredecessorPagesInXml(
      string pageId,
      string provider,
      string filter);

    /// <summary>Gets pages as tree.</summary>
    /// <param name="LeafIds">The ids of the leaf pages.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "tree/?provider={provider}&nodesLimit={nodesLimit}&perLevelLimit={perLevelLimit}&perSubtreeLimit={perSubtreeLimit}&subtreesLimit={subtreesLimit}&root={root}")]
    CollectionContext<PageViewModel> GetPagesAsTree(
      string[] leafIds,
      string provider,
      int nodesLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root);

    /// <summary>Gets pages as tree in XML.</summary>
    /// <param name="LeafIds">The ids of the leaf pages.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/tree/?provider={provider}&nodesLimit={nodesLimit}&perLevelLimit={perLevelLimit}&perSubtreeLimit={perSubtreeLimit}&subtreesLimit={subtreesLimit}&root={root}")]
    CollectionContext<PageViewModel> GetPagesAsTreeInXml(
      string[] leafIds,
      string provider,
      int nodesLimit,
      int perLevelLimit,
      int perSubtreeLimit,
      int subtreesLimit,
      string root);

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteContent(
      string[] Ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes an array of pages.
    /// Result is returned in Xml format.
    /// </summary>
    /// <param name="Ids">An array containing the Ids of the pages to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    [OperationContract]
    [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/batch/?providerName={providerName}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool BatchDeleteContentInXml(
      string[] Ids,
      string providerName,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes the page and returns true if the page has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="pageId">Id of the page to be deleted.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{pageId}/?providerName={providerName}&duplicate={duplicate}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool DeletePage(
      string pageId,
      string providerName,
      bool duplicate,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>
    /// Deletes the page and returns true if the page has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="providerName">Name of the content provider to be used when deleting the page.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{pageId}/?providerName={providerName}&duplicate={duplicate}&language={deletedLanguage}&checkRelatingData={checkRelatingData}")]
    bool DeletePageInXml(
      string pageId,
      string providerName,
      bool duplicate,
      string deletedLanguage,
      bool checkRelatingData);

    /// <summary>Gets the page version info.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Pages/versions/?itemId={itemId}&versionId={versionId}")]
    PageTemplateDraftVersionInfo GetPageVersionInfo(
      string itemId,
      string versionId);

    /// <summary>Gets the template version info.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Templates/versions/?itemId={itemId}&versionId={versionId}")]
    PageTemplateDraftVersionInfo GetTemplateVersionInfo(
      string itemId,
      string versionId);

    /// <summary>Copies the draft template as new draft.</summary>
    /// <param name="itemId">The item id.</param>
    /// <param name="versionId">The version id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Pages/versions/copyversiontopage/?itemId={pageId}&versionId={versionId}")]
    void CopyDraftPageAsNewDraft(string pageId, string versionId);

    /// <summary>Gets the single page and returs it in JSON format.</summary>
    /// <param name="pageId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of CollectionContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "{pageId}/?providerName={providerName}&duplicate={duplicate}")]
    WcfPageContext GetPage(string pageId, string providerName, bool duplicate);

    /// <summary>Gets the single page and returns it in XML format.</summary>
    /// <param name="pageId">Id of the content item that ought to be retrieved.</param>
    /// <param name="providerName">Name of the content provider that is to be used to retrieve the content item.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns>
    /// An instance of ContentItemContext that contains the content item to be retrieved.
    /// </returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{pageId}/?providerName={providerName}&duplicate={duplicate}")]
    WcfPageContext GetPageInXml(string pageId, string providerName, bool duplicate);

    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Templates/versions/copyversiontopage/?itemId={itemId}&versionId={versionId}")]
    void CopyDraftTemplateAsNewDraft(string itemId, string versionId);

    /// <summary>Saves the page.</summary>
    /// <param name="pageContext">The page context.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "{pageId}/?providerName={providerName}&duplicate={duplicate}")]
    WcfPageContext SavePage(
      WcfPageContext pageContext,
      string pageId,
      string providerName,
      bool duplicate);

    /// <summary>Saves the page in XML.</summary>
    /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/{pageId}/?providerName={providerName}&duplicate={duplicate}")]
    WcfPageContext SavePageInXml(
      WcfPageContext pageContext,
      string pageId,
      string providerName,
      bool duplicate);

    /// <summary>Batch saving pages.</summary>
    /// <param name="pageContexts">The page contexts.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/?providerName={providerName}&root={root}")]
    CollectionContext<PageViewModel> BatchSavePage(
      WcfPage[] pageContexts,
      string providerName,
      string root);

    /// <summary>Batch saving pages in XML.</summary>
    /// <param name="pageContexts">The page contexts.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "batch/xml/?providerName={providerName}&root={root}")]
    CollectionContext<PageViewModel> BatchSavePageInXml(
      WcfPage[] pageContexts,
      string providerName,
      string root);

    /// <summary>Batch placing pages.</summary>
    /// <param name="placePosition">The position at which to place the pages.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/place/?providerName={providerName}&placePosition={placePosition}&destination={destination}&root={root}")]
    CollectionContext<PageViewModel> BatchPlacePage(
      string[] sourcePageIds,
      string providerName,
      string placePosition,
      string destination,
      string root);

    /// <summary>Batch placing pages in XML.</summary>
    /// <param name="placePosition">The position at which to place the pages.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "batch/place/xml/?providerName={providerName}&placePosition={placePosition}&destination={destination}&root={root}")]
    CollectionContext<PageViewModel> BatchPlacePageInXml(
      string[] sourcePageIds,
      string providerName,
      string placePosition,
      string destination,
      string root);

    /// <summary>Batch moving pages.</summary>
    /// <param name="direction">The moving direction.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batch/move/?providerName={providerName}&direction={direction}&root={root}")]
    void BatchMovePage(string[] sourcePageIds, string providerName, string direction, string root);

    /// <summary>Batch moving pages in XML.</summary>
    /// <param name="direction">The moving direction.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "batch/move/xml/?providerName={providerName}&direction={direction}&root={root}")]
    void BatchMovePageInXml(
      string[] sourcePageIds,
      string providerName,
      string direction,
      string root);

    /// <summary>Sets the home page.</summary>
    /// <param name="pageId">The page pageId.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "HomePage/Set/")]
    void SetHomePage(string pageId);

    /// <summary>
    /// Sets the default template. This template is selected by default when creating new page.
    /// </summary>
    /// <param name="pageId">The ID of the template.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Template/SetDefault/")]
    void SetDefaultTemplate(string templateId);

    /// <summary>Restores the template to default template.</summary>
    /// <param name="templateId">The template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Template/RestoreDefault/")]
    void RestoreTemplateToDefault(string templateId);

    /// <summary>Restores the template to default in XML.</summary>
    /// <param name="templateId">The template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "xml/Template/RestoreDefault/")]
    void RestoreTemplateToDefaultInXml(string templateId);

    /// <summary>Publishes the draft page.</summary>
    /// <param name="pageDraftId">Id of the page to be published.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PublishDraft/")]
    void PublishDraft(string pageDraftId);

    /// <summary>Publishes the specified pages.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchPublishDraft/")]
    [WebHelp(Comment = "Publishes the specified pages (IDs)")]
    void BatchPublishDraft(string[] pageIDs);

    /// <summary>Unpublishes a page.</summary>
    /// <param name="pageDraftId">Id of the page to be unpublished.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "UnpublishPage/")]
    void UnpublishPage(string pageId);

    /// <summary>Unpublishes the specified pages.</summary>
    /// <param name="pageIDs">The page Ids.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchUnpublishPage/")]
    [WebHelp(Comment = "Unpublishes the specified pages (IDs)")]
    void BatchUnpublishPage(string[] pageIDs);

    /// <summary>Duplicates the existing page.</summary>
    /// <param name="pageDraftId">Id of the page to be duplicated.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "Duplicate/")]
    PageViewModel DuplicatePage(string pageDraftId);

    /// <summary>Moves page one place up inside of its current level.</summary>
    /// <param name="pageDraftId">Id of the page to be moved up.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MoveUp/")]
    void MovePageUp(string pageDraftId);

    /// <summary>
    /// Moves page one place down inside of its current level.
    /// </summary>
    /// <param name="pageDraftId">Id of the page to be moved down.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "MoveDown/")]
    void MovePageDown(string pageDraftId);

    /// <summary>Moves the page before the supplied target page.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="targetPageId">The target page id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PlaceBefore/?targetPageId={targetPageId}")]
    void PlaceBefore(string pageId, string targetPageId);

    /// <summary>Moves the page after the supplied target page.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="targetPageId">The target page id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "PlaceAfter/?targetPageId={targetPageId}")]
    void PlaceAfter(string pageId, string targetPageId);

    /// <summary>Changes the page owner.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="userId">The user id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeOwner/{userId}/")]
    void ChangePageOwner(string pageId, string userId);

    /// <summary>Changes the page parent.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="newParentPageId">The new parent page id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "BatchChangeOwner/{userId}/")]
    void BatchChangePageOwner(string[] pageId, string userId);

    /// <summary>Changes the page parent.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="newParentPageId">The new parent page id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "ChangeParent/{newParentPageId}/")]
    void ChangePageParent(string pageId, string newParentPageId);

    /// <summary>Changes the template.</summary>
    /// <param name="pageId">The page id.</param>
    /// <param name="newTemplateId">The new template id.</param>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "changeTemplate/{pageId}/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the template of the specified page (ID)")]
    bool ChangeTemplate(string pageId, string newTemplateId);

    /// <summary>Gets the children.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "hierarchy/{parentId}/?provider={provider}&filter={filter}&siteId={siteId}")]
    CollectionContext<WcfPageNode> GetChildren(
      string parentId,
      string provider,
      string filter,
      string siteId);

    /// <summary>Gets the children in XML.</summary>
    /// <param name="parentId">The parent id.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/hierarchy/{parentId}/?provider={provider}&filter={filter}&siteId={siteId}")]
    CollectionContext<WcfPageNode> GetChildrenInXml(
      string parentId,
      string provider,
      string filter,
      string siteId);

    /// <summary>Changes templates of multiple pages at once</summary>
    /// <param name="pageIDs">The page Ids.</param>
    /// <param name="newTemplateId">The new template id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "batchChangeTemplate/?newTemplateId={newTemplateId}")]
    [WebHelp(Comment = "Changes the template of the the specified pages (IDs)")]
    string[] BatchChangeTemplate(string[] pageIDs, string newTemplateId);

    /// <summary>
    /// Returns the ID of the default template for frontend pages.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>The Id of the default template.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Template/GetDefault/")]
    WcfPageTemplate GetDefaultFrontendTemplateId();

    /// <summary>
    /// Returns the ID of the default template for frontend pages.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>The Id of the default template.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/Template/GetDefault/")]
    WcfPageTemplate GetDefaultFrontendTemplateIdInXml();

    /// <summary>
    /// Returns the ID of the default template for backend pages.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>The Id of the default template.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "Template/GetDefaultBackend/")]
    WcfPageTemplate GetDefaultBackendTemplateId();

    /// <summary>
    /// Returns the ID of the default template for backend pages in xml format.
    /// </summary>
    /// <param name="pageId"></param>
    /// <returns>The Id of the default template.</returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/Template/GetDefaultBackend/")]
    WcfPageTemplate GetDefaultBackendTemplateIdInXml();

    /// <summary>
    /// Makes a new template from the master file and add it as custom template.
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "template/createFromMaster/?rootTaxonType={rootTaxonType}")]
    WcfPageTemplate MakeTemplateFromMasterFile(
      string masterFilePath,
      string rootTaxonType);

    /// <summary>
    /// Makes a new template from the master file and add it as custom template.
    /// </summary>
    /// <returns></returns>
    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/template/createFromMaster/?rootTaxonType={rootTaxonType}")]
    WcfPageTemplate MakeTemplateFromMasterFileInXml(
      string masterFilePath,
      string rootTaxonType);

    /// <summary>Checks the page for updates.</summary>
    /// <param name="pageid">The page id.</param>
    /// <param name="pageStatus">The initial page status.</param>
    /// <param name="pageVersion">The initial  version.</param>
    /// <param name="provider">The provider.</param>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "CheckPageForChanges/{pageId}/?pageStatus={pageStatus}&pageVersion={pageVersion}&provider={provider}")]
    CurrentPageState CheckPageForChanges(
      string pageId,
      string pageStatus,
      int pageVersion,
      string provider);

    /// <summary>Gets the page template.</summary>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "template/{pageId}/")]
    WcfPageTemplate GetPageTemplate(string pageId);

    /// <summary>Gets the page template in XML.</summary>
    /// <param name="pageId">The page id.</param>
    /// <returns></returns>
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Xml, UriTemplate = "xml/template/{pageId}/")]
    WcfPageTemplate GetPageTemplateInXml(string pageId);
  }
}
