// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Base class for page data providers.</summary>
  public abstract class PageDataProvider : 
    ContentDataProviderBase,
    IControlPropertyProvider,
    ILanguageDataProvider,
    IMultisiteEnabledProvider,
    IPageDataProvider,
    IPageControlProvider
  {
    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    private string[] supportedPermissionSets = new string[3]
    {
      "Pages",
      "PageTemplates",
      "Controls"
    };
    internal new const string ValidateOnCommitKey = "sf-pdp-validate-commit";
    private LocalDataStoreSlot dataStoreSlot;
    private string suppressValidationOnCommitKey;

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public abstract PageNode CreatePageNode();

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public abstract PageNode CreatePageNode(Guid id);

    /// <summary>Gets the page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    [ValuePermission("Pages", new string[] {"View"})]
    public abstract PageNode GetPageNode(Guid id);

    /// <summary>Gets a query for page nodes.</summary>
    /// <returns>The query for pages.</returns>
    [EnumeratorPermission("Pages", new string[] {"View"})]
    public abstract IQueryable<PageNode> GetPageNodes();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    [ParameterPermission("item", "Pages", new string[] {"Delete"})]
    public abstract void Delete(PageNode item);

    /// <summary>
    /// Moves the page node passed as first argument to one of the positions predefined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.MoveTo" /> enumeration
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="moveTo">The position to move to.</param>
    [ParameterPermission("nodeToMove", "Pages", new string[] {"Modify"})]
    public abstract void MovePageNode(PageNode nodeToMove, MoveTo moveTo);

    /// <summary>
    /// Moves the page node passed as first argument by the specified number of places, in the direction given by the
    /// <see cref="T:Telerik.Sitefinity.Modules.Pages.Move" /> enumeration.
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="move">A value representing the direction in which the node will be moved.</param>
    /// <param name="numberOfPlaces">The number of places to move.</param>
    [ParameterPermission("nodeToMove", "Pages", new string[] {"Modify"})]
    public abstract void MovePageNode(PageNode nodeToMove, Move move, int numberOfPlaces);

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNode">An instance of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the place to which the page ought to be moved.</param>
    [ParameterPermission("nodeToMove", "Pages", new string[] {"Modify"})]
    public abstract void MovePageNode(PageNode nodeToMove, PageNode targetNode, Place place);

    /// <summary>
    /// Moves the page node passed as first argument to the place defined by the <see cref="T:Telerik.Sitefinity.Modules.Pages.Place" /> enumeration,
    /// relative to the supplied target page node
    /// </summary>
    /// <param name="nodeToMove">The node to move.</param>
    /// <param name="targetNodeId">The ID of the page that serves as a reference point to the new placing of the page.</param>
    /// <param name="place">A value representing the direction in which the node will be moved. </param>
    [ParameterPermission("nodeToMove", "Pages", new string[] {"Modify"})]
    public abstract void MovePageNode(PageNode nodeToMove, Guid targetNodeId, Place place);

    /// <summary>
    /// Make the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passes as first argument the child of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> passed as second argument
    /// </summary>
    /// <param name="childNode">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> whose parent should be changed</param>
    /// <param name="newParent">The <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> representing the new parent</param>
    [ParameterPermission("newParent", "Pages", new string[] {"Create"})]
    public abstract void ChangeParent(PageNode childNode, PageNode newParent);

    /// <summary>Creates new page.</summary>
    /// <returns>The new page.</returns>
    public abstract PageData CreatePageData();

    /// <summary>Creates new page with the specified ID.</summary>
    /// <param name="id">The pageId of the new page.</param>
    /// <returns>The new page.</returns>
    public abstract PageData CreatePageData(Guid id);

    /// <summary>Gets the page with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page data item.</returns>
    public abstract PageData GetPageData(Guid id);

    /// <summary>Gets a query for pages.</summary>
    /// <returns>The query for pages.</returns>
    public abstract IQueryable<PageData> GetPageDataList();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page to delete.</param>
    public abstract void Delete(PageData item);

    /// <summary>Changes the owner of a page.</summary>
    /// <param name="node">The page node.</param>
    /// <param name="newOwnerID">The new owner ID.</param>
    [ParameterPermission("node", "Pages", new string[] {"ChangeOwner"})]
    public abstract void ChangeOwner(PageNode node, Guid newOwnerID);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public abstract PageTemplate CreateTemplate();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public abstract PageTemplate CreateTemplate(Guid id);

    /// <summary>Links the template to site.</summary>
    /// <param name="template">The template.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal abstract SiteItemLink LinkTemplateToSite(
      PageTemplate template,
      Guid siteId);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    [ValuePermission("PageTemplates", new string[] {"View"})]
    public abstract PageTemplate GetTemplate(Guid id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    [EnumeratorPermission("PageTemplates", new string[] {"View"})]
    public abstract IQueryable<PageTemplate> GetTemplates();

    /// <summary>Gets a query for page templates in a specific site.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for page templates.</returns>
    [EnumeratorPermission("PageTemplates", new string[] {"View"})]
    internal abstract IQueryable<PageTemplate> GetTemplates(Guid siteId);

    /// <summary>Gets the links for all templates.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    [EnumeratorPermission("PageTemplates", new string[] {"View"})]
    internal abstract IQueryable<SiteItemLink> GetSiteTemplateLinks();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The page template to delete.</param>
    [ParameterPermission("item", "PageTemplates", new string[] {"Delete"})]
    public abstract void Delete(PageTemplate item);

    /// <summary>Creates new draft page or template.</summary>
    /// <returns>The new control.</returns>
    [TypedMethodPermission(typeof (TemplateDraft), "PageTemplates", new string[] {"Create"})]
    [TypedMethodPermission(typeof (PageDraft), "Pages", new string[] {"Create"})]
    public abstract T CreateDraft<T>() where T : DraftData;

    /// <summary>Creates new draft or page with the specified ID.</summary>
    /// <param name="id">The pageId of the new control.</param>
    /// <returns>The new control.</returns>
    [TypedMethodPermission(typeof (TemplateDraft), "PageTemplates", new string[] {"Create"})]
    [TypedMethodPermission(typeof (PageDraft), "Pages", new string[] {"Create"})]
    public abstract T CreateDraft<T>(Guid id) where T : DraftData;

    /// <summary>
    /// Gets the draft page or template with the specified ID.
    /// </summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    [TypedValuePermission(typeof (TemplateDraft), "PageTemplates", new string[] {"View"})]
    [TypedValuePermission(typeof (PageDraft), "Pages", new string[] {"View"})]
    public abstract T GetDraft<T>(Guid id) where T : DraftData;

    /// <summary>Gets a query for draft pages or templates.</summary>
    /// <returns>The query for controls.</returns>
    [TypedEnumeratorPermission(typeof (TemplateDraft), "PageTemplates", new string[] {"View"})]
    [TypedEnumeratorPermission(typeof (PageDraft), "Pages", new string[] {"View"})]
    public abstract IQueryable<T> GetDrafts<T>() where T : DraftData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    [TypedParameterPermission(typeof (TemplateDraft), "item", "PageTemplates", new string[] {"Delete"})]
    [TypedParameterPermission(typeof (PageDraft), "item", "Pages", new string[] {"Delete"})]
    public abstract void Delete(DraftData item);

    /// <summary>Creates new control.</summary>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(bool isBackendObject = false) where T : ObjectData;

    /// <summary>Creates new control with the specified ID.</summary>
    /// <param name="id">The pageId of the new control.</param>
    /// <param name="isBackendObject">Is backend object.</param>
    /// <returns>The new control.</returns>
    public abstract T CreateControl<T>(Guid id, bool isBackendObject = false) where T : ObjectData;

    /// <summary>Gets the control with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>Control data persistent object.</returns>
    public abstract T GetControl<T>(Guid id) where T : ObjectData;

    /// <summary>Gets a query for controls.</summary>
    /// <returns>The query for controls.</returns>
    public abstract IQueryable<T> GetControls<T>() where T : ObjectData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The control to delete.</param>
    public abstract void Delete(ObjectData item);

    /// <summary>Creates new page template.</summary>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty();

    /// <summary>Creates new page template with the specified ID.</summary>
    /// <param name="id">The pageId of the new page template.</param>
    /// <returns>The new page template.</returns>
    public abstract ControlProperty CreateProperty(Guid id);

    /// <summary>Gets the page template with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A page template.</returns>
    [ValuePermission("Controls", new string[] {"ViewControl"})]
    public abstract ControlProperty GetProperty(Guid id);

    /// <summary>Gets a query for page templates.</summary>
    /// <returns>The query for page templates.</returns>
    [EnumeratorPermission("Controls", new string[] {"ViewControl"})]
    public abstract IQueryable<ControlProperty> GetProperties();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    [ParameterPermission("item", "Controls", new string[] {"EditControlProperties"})]
    public abstract void Delete(ControlProperty item);

    void IControlPropertyProvider.DeleteProperty(ControlProperty item) => this.Delete(item);

    void IControlPropertyProvider.DeleteObject(ObjectData item) => this.Delete(item);

    void IControlPropertyProvider.DeletePresentation(PresentationData item) => this.Delete(item);

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>
    /// Creates new object for storing presentation information.
    /// </summary>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>() where T : PresentationData;

    /// <summary>
    /// Creates new object for storing presentation information with the specified ID.
    /// </summary>
    /// <param name="id">The id of the new item.</param>
    /// <returns>The new <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T CreatePresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>Links the presentation item to site.</summary>
    /// <param name="presentationData">The presentation item.</param>
    /// <param name="siteId">The site id.</param>
    /// <returns>The created link.</returns>
    internal abstract SiteItemLink LinkPresentationItemToSite(
      PresentationData presentationData,
      Guid siteId);

    /// <summary>Gets the item with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>The <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> object.</returns>
    public abstract T GetPresentationItem<T>(Guid id) where T : PresentationData;

    /// <summary>
    /// Gets a query for <see cref="T:Telerik.Sitefinity.Pages.Model.PresentationData" /> items.
    /// </summary>
    /// <returns>The query for page templates.</returns>
    public abstract IQueryable<T> GetPresentationItems<T>() where T : PresentationData;

    /// <summary>Gets the presentation items for a specified site id.</summary>
    /// <param name="siteId">The site id.</param>
    /// <returns>The query for presentation items.</returns>
    internal abstract IQueryable<T> GetPresentationItems<T>(Guid siteId) where T : PresentationData;

    /// <summary>Gets the links for all presentation items.</summary>
    /// <returns>The query for SiteItemLink.</returns>
    internal abstract IQueryable<SiteItemLink> GetSitePresentationItemLinks<T>() where T : PresentationData;

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The item to delete.</param>
    public abstract void Delete(PresentationData item);

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (PageDataProvider);

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <returns>The type.</returns>
    public override Type GetUrlTypeFor(Type itemType)
    {
      if (itemType == typeof (PageNode))
        return typeof (PageUrlData);
      throw new ArgumentException("Unknown type specified.");
    }

    public override string GetUrlFormat(ILocatable item) => typeof (PageNode).IsAssignableFrom(item.GetType()) ? "/[Page.UrlName]" : base.GetUrlFormat(item);

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      base.SetRootPermissions(root);
      Permission permission1 = this.CreatePermission("Pages", root.Id, SecurityManager.OwnerRole.Id);
      permission1.GrantActions(true, "Delete", "Modify");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("PageTemplates", root.Id, SecurityManager.EveryoneRole.Id);
      permission2.GrantActions(true, "View");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("PageTemplates", root.Id, SecurityManager.OwnerRole.Id);
      permission3.GrantActions(true, "Delete", "Modify");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("PageTemplates", root.Id, SecurityManager.DesignersRole.Id);
      permission4.GrantActions(true, "Create", "Delete", "Modify");
      root.Permissions.Add(permission4);
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The pageId.</param>
    /// <returns>The item.</returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (PageData))
        return (object) this.CreatePageData(id);
      if (itemType == typeof (PageNode))
        return (object) this.CreatePageNode(id);
      if (itemType == typeof (PageTemplate))
        return (object) this.CreateTemplate(id);
      if (itemType == typeof (PageDraft))
        return (object) this.CreateDraft<PageDraft>(id);
      if (itemType == typeof (LanguageData))
        return (object) this.CreateLanguageData(id);
      if (itemType == typeof (PageControl))
        return (object) this.CreateControl<PageControl>(id, false);
      if (itemType == typeof (PageDraftControl))
        return (object) this.CreateControl<PageDraftControl>(id, false);
      if (itemType == typeof (ControlProperty))
        return (object) this.CreateProperty(id);
      if (itemType == typeof (ObjectData))
        return (object) this.CreateControl<ObjectData>(id, false);
      if (itemType == typeof (TemplateControl))
        return (object) this.CreateControl<TemplateControl>(id, false);
      if (itemType == typeof (TemplatePresentation))
        return (object) this.CreatePresentationItem<TemplatePresentation>(id);
      if (itemType == typeof (ControlPresentation))
        return (object) this.CreatePresentationItem<ControlPresentation>(id);
      if (itemType == typeof (PresentationData))
        return (object) this.CreatePresentationItem<ControlPresentation>(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns>The item.</returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (typeof (PageData).IsAssignableFrom(itemType))
        return (object) this.GetPageData(id);
      if (typeof (PageNode).IsAssignableFrom(itemType))
        return (object) this.GetPageNode(id);
      if (typeof (PageTemplate).IsAssignableFrom(itemType))
        return (object) this.GetTemplate(id);
      if (typeof (PageDraft).IsAssignableFrom(itemType))
        return (object) this.GetDraft<PageDraft>(id);
      if (typeof (TemplateDraft).IsAssignableFrom(itemType))
        return (object) this.GetDraft<TemplateDraft>(id);
      if (typeof (PageDraftControl).IsAssignableFrom(itemType))
        return (object) this.GetControl<PageDraftControl>(id);
      if (typeof (TemplateDraftControl).IsAssignableFrom(itemType))
        return (object) this.GetControl<TemplateDraftControl>(id);
      if (typeof (ControlData).IsAssignableFrom(itemType))
        return (object) this.GetControl<ControlData>(id);
      if (typeof (ControlProperty).IsAssignableFrom(itemType))
        return (object) this.GetProperty(id);
      if (typeof (ObjectData).IsAssignableFrom(itemType))
        return (object) this.GetControl<ObjectData>(id);
      if (typeof (TemplatePresentation).IsAssignableFrom(itemType))
        return (object) this.GetPresentationItem<TemplatePresentation>(id);
      if (typeof (LanguageData).IsAssignableFrom(itemType))
        return (object) this.GetLanguageData(id);
      if (typeof (ControlPresentation).IsAssignableFrom(itemType))
        return (object) this.GetPresentationItem<ControlPresentation>(id);
      return base.GetItem(itemType, id) ?? throw new NotSupportedException();
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (typeof (PageData).IsAssignableFrom(itemType))
        return (object) this.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Id == id)).FirstOrDefault<PageData>();
      if (typeof (PageDraft).IsAssignableFrom(itemType))
        return (object) this.GetDraft<PageDraft>(id);
      if (typeof (TemplateDraft).IsAssignableFrom(itemType))
        return (object) this.GetDraft<TemplateDraft>(id);
      if (typeof (PageNode).IsAssignableFrom(itemType))
        return (object) this.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id == id)).FirstOrDefault<PageNode>();
      if (typeof (PageTemplate).IsAssignableFrom(itemType))
        return (object) this.GetTemplates().Where<PageTemplate>((Expression<Func<PageTemplate, bool>>) (p => p.Id == id)).FirstOrDefault<PageTemplate>();
      if (typeof (PageDraftControl).IsAssignableFrom(itemType))
        return (object) this.GetControls<PageDraftControl>().Where<PageDraftControl>((Expression<Func<PageDraftControl, bool>>) (p => p.Id == id)).FirstOrDefault<PageDraftControl>();
      if (typeof (TemplateDraftControl).IsAssignableFrom(itemType))
        return (object) this.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (p => p.Id == id)).FirstOrDefault<TemplateDraftControl>();
      if (typeof (ControlData).IsAssignableFrom(itemType))
        return (object) this.GetControls<ControlData>().Where<ControlData>((Expression<Func<ControlData, bool>>) (p => p.Id == id)).FirstOrDefault<ControlData>();
      if (typeof (ControlPresentation).IsAssignableFrom(itemType))
        return (object) this.GetPresentationItems<ControlPresentation>().Where<ControlPresentation>((Expression<Func<ControlPresentation, bool>>) (p => p.Id == id)).FirstOrDefault<ControlPresentation>();
      if (typeof (TemplateDraftPresentation).IsAssignableFrom(itemType))
        return (object) this.GetPresentationItems<TemplateDraftPresentation>().Where<TemplateDraftPresentation>((Expression<Func<TemplateDraftPresentation, bool>>) (p => p.Id == id)).FirstOrDefault<TemplateDraftPresentation>();
      if (typeof (TemplatePresentation).IsAssignableFrom(itemType))
        return (object) this.GetPresentationItems<TemplatePresentation>().Where<TemplatePresentation>((Expression<Func<TemplatePresentation, bool>>) (p => p.Id == id)).FirstOrDefault<TemplatePresentation>();
      if (!typeof (PageDraftPresentation).IsAssignableFrom(itemType))
        return base.GetItem(itemType, id);
      return (object) this.GetPresentationItems<PageDraftPresentation>().Where<PageDraftPresentation>((Expression<Func<PageDraftPresentation, bool>>) (p => p.Id == id)).FirstOrDefault<PageDraftPresentation>();
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
      if (typeof (PageData).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageData>(this.GetPageDataList(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (Comment).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<Comment>(this.GetComments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageTemplate).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageTemplate>(this.GetTemplates(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageNode).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageNode>(this.GetPageNodes(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageControl).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageControl>(this.GetControls<PageControl>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageDraftControl).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageDraftControl>(this.GetControls<PageDraftControl>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (TemplateControl).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<TemplateControl>(this.GetControls<TemplateControl>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (TemplateDraftControl).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<TemplateDraftControl>(this.GetControls<TemplateDraftControl>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (ControlProperty).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<ControlProperty>(this.GetProperties(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageDraft).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageDraft>(this.GetDrafts<PageDraft>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (TemplateDraft).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<TemplateDraft>(this.GetDrafts<TemplateDraft>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PagePresentation).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PagePresentation>(this.GetPresentationItems<PagePresentation>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (PageDraftPresentation).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<PageDraftPresentation>(this.GetPresentationItems<PageDraftPresentation>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (TemplatePresentation).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<TemplatePresentation>(this.GetPresentationItems<TemplatePresentation>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (TemplateDraftPresentation).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<TemplateDraftPresentation>(this.GetPresentationItems<TemplateDraftPresentation>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (ControlPresentation).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<ControlPresentation>(this.GetPresentationItems<ControlPresentation>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (Comment).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<Comment>(this.GetComments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (typeof (ObjectData).IsAssignableFrom(itemType))
        return (IEnumerable) DataProviderBase.SetExpressions<ObjectData>(this.GetControls<ObjectData>(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon pageId.</param>
    /// <param name="isSingleTaxon">Is single taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>The items by taxon.</returns>
    public override IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (PageNode))
      {
        this.CurrentTaxonomyProperty = propertyName;
        int? totalCount1 = new int?();
        IQueryable<PageNode> items = (IQueryable<PageNode>) this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref totalCount1);
        IQueryable<PageNode> source;
        if (isSingleTaxon)
          source = items.Where<PageNode>((Expression<Func<PageNode, bool>>) (i => i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId));
        else
          source = items.Where<PageNode>((Expression<Func<PageNode, bool>>) (i => i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty).Any<Guid>((Func<Guid, bool>) (t => t == taxonId))));
        if (totalCount.HasValue)
          totalCount = new int?(source.Count<PageNode>());
        if (skip > 0)
          source = source.Skip<PageNode>(skip);
        if (take > 0)
          source = source.Take<PageNode>(take);
        return (IEnumerable) source;
      }
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (PageNode));
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      Type c = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      if (typeof (PageData).IsAssignableFrom(c))
        this.Delete((PageData) item);
      else if (typeof (PageNode).IsAssignableFrom(c))
        this.Delete((PageNode) item);
      else if (typeof (PageTemplate).IsAssignableFrom(c))
        this.Delete((PageTemplate) item);
      else if (typeof (PageDraftControl).IsAssignableFrom(c))
        this.Delete((ObjectData) item);
      else if (typeof (TemplateDraftControl).IsAssignableFrom(c))
        this.Delete((ObjectData) item);
      else if (typeof (ControlData).IsAssignableFrom(c))
        this.Delete((ObjectData) item);
      else if (typeof (PageDraft).IsAssignableFrom(c))
        this.Delete((DraftData) item);
      else if (typeof (ObjectData).IsAssignableFrom(c))
        this.Delete((ObjectData) item);
      else if (typeof (TemplateDraft).IsAssignableFrom(c))
        this.Delete((DraftData) item);
      else if (typeof (TemplatePresentation).IsAssignableFrom(c))
        this.Delete((PresentationData) item);
      else if (typeof (TemplateDraftPresentation).IsAssignableFrom(c))
        this.Delete((PresentationData) item);
      else if (typeof (ControlProperty).IsAssignableFrom(c))
        this.Delete((ControlProperty) item);
      else if (typeof (ControlPresentation).IsAssignableFrom(c))
        this.Delete((PresentationData) item);
      else
        base.DeleteItem(item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns>The list of types.</returns>
    public override Type[] GetKnownTypes() => new Type[19]
    {
      typeof (PageData),
      typeof (PageControl),
      typeof (PageDraftControl),
      typeof (TemplateControl),
      typeof (TemplateDraftControl),
      typeof (ControlProperty),
      typeof (PageDraft),
      typeof (TemplateDraft),
      typeof (PageNode),
      typeof (PageTemplate),
      typeof (PagePresentation),
      typeof (PageDraftPresentation),
      typeof (TemplatePresentation),
      typeof (TemplateDraftPresentation),
      typeof (ControlPresentation),
      typeof (Comment),
      typeof (ObjectData),
      typeof (TemplatePresentation),
      typeof (PresentationData)
    };

    /// <summary>Creates a language data item</summary>
    /// <returns>The language data item.</returns>
    public abstract LanguageData CreateLanguageData();

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns>The language data item.</returns>
    public abstract LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns>The language data item.</returns>
    public abstract LanguageData GetLanguageData(Guid id);

    /// <summary>Gets a query of all language data items</summary>
    /// <returns>The query.</returns>
    public abstract IQueryable<LanguageData> GetLanguageData();

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns>The item.</returns>
    [TypedValuePermission(typeof (PageNode), "Pages", new string[] {"View"})]
    [TypedValuePermission(typeof (PageTemplate), "PageTemplates", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, out redirectUrl);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns>The item.</returns>
    [TypedValuePermission(typeof (PageNode), "Pages", new string[] {"View"})]
    [TypedValuePermission(typeof (PageTemplate), "PageTemplates", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    protected override void ValidateOnCommitting(IList dirtyItems)
    {
      base.ValidateOnCommitting(dirtyItems);
      PageManager.GuardDuplicateUrls(this, dirtyItems);
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> instance.
    /// </summary>
    /// <returns>The site item link.</returns>
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
    /// <typeparam name="T">The type of the data item.</typeparam>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.Multisite.Model.SiteItemLink" /> objects.
    /// </returns>
    [TypedEnumeratorPermission(typeof (PageTemplate), "PageTemplates", new string[] {"View"})]
    public abstract IQueryable<T> GetSiteItems<T>(Guid siteId) where T : IDataItem;

    /// <summary>Gets the page data.</summary>
    /// <param name="node">The node.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The page data.</returns>
    PageData IPageDataProvider.GetPageData(
      PageNode node,
      CultureInfo culture = null)
    {
      string cultureName = culture == null || node.LocalizationStrategy != LocalizationStrategy.Split ? (string) null : culture.Name;
      IQueryable<PageData> source1 = this.GetDirtyItems().OfType<PageData>().AsQueryable<PageData>().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.NavigationNode != default (object) && pd.NavigationNode.Id == node.Id));
      if (cultureName != null)
        source1 = source1.Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Culture == cultureName));
      PageData pageData = source1.FirstOrDefault<PageData>();
      if (pageData == null)
      {
        IQueryable<PageData> source2 = this.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.NavigationNode != default (object) && pd.NavigationNode.Id == node.Id));
        if (cultureName != null)
          source2 = source2.Where<PageData>((Expression<Func<PageData, bool>>) (pd => pd.Culture == cultureName));
        pageData = source2.FirstOrDefault<PageData>();
      }
      return pageData;
    }

    public PageNode GetPageNode(PageData pageData) => pageData.PersonalizationMasterId != Guid.Empty ? this.GetPageData(pageData.PersonalizationMasterId).NavigationNode : pageData.NavigationNode;
  }
}
