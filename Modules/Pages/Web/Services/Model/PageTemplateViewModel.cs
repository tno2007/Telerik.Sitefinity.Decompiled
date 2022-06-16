// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageTemplateViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>ViewModel class for the page template object.</summary>
  [DataContract]
  public class PageTemplateViewModel : WcfPageTemplate
  {
    public PageTemplateViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.WcfPageTemplate" /> class.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    public PageTemplateViewModel(PageTemplate pageTemplate)
      : base(pageTemplate)
    {
      this.TemplateId = pageTemplate.Id;
      this.DateCreated = new DateTime?(pageTemplate.LastModified);
      this.Owner = PageTemplateViewModel.GetUser(pageTemplate.Owner);
      List<Guid> list1 = pageTemplate.GetPageDataBasedOnTemplate().Select<PageData, Guid>((Expression<Func<PageData, Guid>>) (x => x.NavigationNode.RootNodeId)).ToList<Guid>();
      List<Guid> list2 = pageTemplate.GetDraftPagesBasedOnTemplate().Select<PageDraft, Guid>((Expression<Func<PageDraft, Guid>>) (d => d.ParentPage.NavigationNode.RootNodeId)).ToList<Guid>();
      List<Guid> source = list1;
      source.AddRange((IEnumerable<Guid>) list2);
      this.PagesCount = source.Count;
      this.PagesCountString = PageTemplateViewModel.GetStatisticsText(this.PagesCount, source.Distinct<Guid>());
      PageTemplate parentTemplate = this.GetParentTemplate(pageTemplate);
      if (parentTemplate != null)
      {
        this.ParentTemplateTitle = (string) parentTemplate.Title;
        this.ParentTemplateUrl = RouteHelper.ResolveUrl("/Sitefinity/Template/" + (object) parentTemplate.Id, UrlResolveOptions.Rooted);
        this.ParentTemplateId = parentTemplate.Id;
        this.Template = new WcfPageTemplate(parentTemplate);
      }
      this.Category = pageTemplate.Category;
      this.ChildTemplatesCount = pageTemplate.ChildTemplates.Count;
      this.ShowInNavigation = pageTemplate.ShowInNavigation;
      this.EditUrl = PageTemplateViewModel.GetEditUrl(pageTemplate.Id);
      this.TemplatePreviewUrl = PageTemplateViewModel.GetPreviewUrl(pageTemplate.Id);
      string statusKey = (string) null;
      string statusText = (string) null;
      CultureInfo culture = (CultureInfo) null;
      LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) pageTemplate, culture.GetSitefinityCulture(), ref statusKey, ref statusText);
      this.Status = statusKey;
      this.StatusText = statusText;
      this.AdditionalStatus = StatusResolver.Resolve(typeof (PageTemplate), pageTemplate.GetProviderName(), this.Id);
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      this.PageLifecycleStatus = new WcfContentLifecycleStatus()
      {
        IsAdmin = currentIdentity.IsUnrestricted,
        IsLocked = pageTemplate.LockedBy != Guid.Empty,
        IsLockedByMe = pageTemplate.LockedBy == currentIdentity.UserId,
        IsPublished = pageTemplate.Status == ContentLifecycleStatus.Live,
        LockedByUsername = CommonMethods.GetUserName(pageTemplate.LockedBy),
        Message = this.StatusText,
        SupportsContentLifecycle = pageTemplate.SupportsContentLifecycle
      };
      this.HasPersonalizedWidgets = pageTemplate.Controls.Any<TemplateControl>((Func<TemplateControl, bool>) (c => c.IsPersonalized));
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageTemplateViewModel" /> class.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="isEditable">if set to <c>true</c> [is editable].</param>
    public PageTemplateViewModel(PageTemplate pageTemplate, bool isEditable)
      : this(pageTemplate, isEditable, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageTemplateViewModel" /> class.
    /// </summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="isEditable">if set to <c>true</c> [is editable].</param>
    /// <param name="isUnlockable">if set to <c>true</c> [can be unlocked].</param>
    public PageTemplateViewModel(PageTemplate pageTemplate, bool isEditable, bool isUnlockable)
      : this(pageTemplate)
    {
      this.IsEditable = isEditable;
      this.IsUnlockable = isUnlockable;
    }

    /// <summary>Gets or sets the template.</summary>
    /// <value>The template.</value>
    [DataMember]
    public WcfPageTemplate Template { get; set; }

    /// <summary>
    /// Gets or sets the date on which an instance of type <see cref="T:Telerik.Sitefinity.Pages.Model.PageTemplate" /> was created.
    /// </summary>
    [DataMember]
    public DateTime? DateCreated { get; set; }

    /// <summary>Gets or sets the owner.</summary>
    [DataMember]
    public string Owner { get; set; }

    /// <summary>Gets or sets the pages count.</summary>
    /// <value>The pages count.</value>
    [DataMember]
    public string PagesCountString { get; set; }

    /// <summary>Gets or sets the pages count.</summary>
    /// <value>The pages count.</value>
    [DataMember]
    public int PagesCount { get; set; }

    /// <summary>Gets or sets the sites count.</summary>
    [DataMember]
    public int SitesCount { get; set; }

    /// <summary>Gets or sets the child templates count.</summary>
    /// <value>The pages count.</value>
    [DataMember]
    public int ChildTemplatesCount { get; set; }

    /// <summary>Gets or sets the parent template Id.</summary>
    /// <value>The parent template Id.</value>
    [DataMember]
    public Guid ParentTemplateId { get; set; }

    /// <summary>Gets or sets the parent template URL.</summary>
    /// <value>The parent template URL.</value>
    [DataMember]
    public string ParentTemplateUrl { get; set; }

    /// <summary>Gets or sets the parent template title.</summary>
    /// <value>The parent template title.</value>
    [DataMember]
    public string ParentTemplateTitle { get; set; }

    /// <summary>Gets or sets the category.</summary>
    [DataMember]
    public Guid Category { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [show in navigation].
    /// </summary>
    /// <value><c>true</c> if [show in navigation]; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool ShowInNavigation { get; set; }

    /// <summary>Gets or sets the template URL.</summary>
    /// <value>The template URL.</value>
    public string TemplateUrl { get; set; }

    /// <summary>Gets or sets the URL for viewing the template.</summary>
    /// <value>The URL for viewing the template.</value>
    [DataMember]
    public string TemplatePreviewUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is editable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can be unlocked.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can be unlocked, otherwise <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsUnlockable { get; set; }

    /// <summary>Gets or sets information about the content lifecycle.</summary>
    [DataMember]
    public WcfContentLifecycleStatus PageLifecycleStatus { get; set; }

    /// <summary>
    /// Gets or sets the Status of the page. If the Page property of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />
    /// is null (in case of group page), the status is discarded
    /// </summary>
    [DataMember]
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the Status of the page. If the Page property of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" />
    /// is null (in case of group page), the status is discarded
    /// </summary>
    [DataMember]
    public string StatusText { get; set; }

    /// <summary>Gets or sets additional status.</summary>
    [DataMember]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Telerik.Sitefinity.Services.Status AdditionalStatus { get; set; }

    /// <summary>Gets or sets template id.</summary>
    [DataMember]
    public Guid TemplateId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has personalized widgets.
    /// </summary>
    [DataMember]
    public bool HasPersonalizedWidgets { get; set; }

    /// <summary>Gets or sets the available page template frameworks.</summary>
    [DataMember]
    public PageTemplatesAvailability AvailableFrameworks { get; set; }

    /// <summary>
    /// Gets the pages based on template - draft and published.
    /// </summary>
    /// <param name="template">The template.</param>
    /// <returns>The pages based on template.</returns>
    public static IEnumerable<NavigationNode> GetPagesBasedOnTempalate(
      PageTemplate template)
    {
      IQueryable<PageData> dataBasedOnTemplate = template.GetPageDataBasedOnTemplate();
      IQueryable<PageData> queryable = template.GetDraftPagesBasedOnTemplate().Select<PageDraft, PageData>((Expression<Func<PageDraft, PageData>>) (x => x.ParentPage));
      List<NavigationNode> basedOnTempalate = new List<NavigationNode>();
      foreach (PageData pageData in (IEnumerable<PageData>) dataBasedOnTemplate)
        basedOnTempalate.Add(PageTemplateViewModel.GetNavNode(pageData));
      foreach (PageData pageData in (IEnumerable<PageData>) queryable)
        basedOnTempalate.Add(PageTemplateViewModel.GetNavNode(pageData));
      return (IEnumerable<NavigationNode>) basedOnTempalate;
    }

    internal static NavigationNode GetNavNode(
      PageData pageData,
      bool isPublished = false,
      bool resolve = false)
    {
      PageNode navigationNode = pageData.NavigationNode;
      CultureInfo culture = pageData.Culture.IsNullOrEmpty() ? ((IEnumerable<CultureInfo>) navigationNode.AvailableCultures).FirstOrDefault<CultureInfo>() : CultureInfo.GetCultureInfo(pageData.Culture);
      string url = PageTemplateViewModel.GetUrl(navigationNode, isPublished, culture, resolve);
      string statusKey = (string) null;
      string statusText = (string) null;
      if (!isPublished)
      {
        statusKey = navigationNode.ApprovalWorkflowState[culture];
        statusText = navigationNode.ApprovalWorkflowState[culture];
      }
      if (statusKey == null || statusText == null)
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) pageData, culture, ref statusKey, ref statusText);
      return culture == null ? new NavigationNode(navigationNode.Id, (string) navigationNode.Title, (string) navigationNode.Title, url, statusText, statusKey, navigationNode.RootNodeId, isPublished) : new NavigationNode(navigationNode.Id, navigationNode.Title[culture], navigationNode.Title[culture], url, statusText, statusKey, navigationNode.RootNodeId, isPublished);
    }

    public static List<PageNode> AllPageNodesBasedOnTemplate(PageTemplate template)
    {
      List<PageNode> source = new List<PageNode>();
      IQueryable<PageNode> collection = template.GetPageDataBasedOnTemplate().Select<PageData, PageNode>((Expression<Func<PageData, PageNode>>) (x => x.NavigationNode));
      IQueryable<PageNode> queryable = template.GetDraftPagesBasedOnTemplate().Select<PageDraft, PageNode>((Expression<Func<PageDraft, PageNode>>) (d => d.ParentPage.NavigationNode));
      source.AddRange((IEnumerable<PageNode>) collection);
      foreach (PageNode pageNode1 in (IEnumerable<PageNode>) queryable)
      {
        PageNode draftPage = pageNode1;
        if (!source.Any<PageNode>((Func<PageNode, bool>) (d => d.Id.Equals(draftPage.Id))))
          source.Add(draftPage);
        if (PageTemplateViewModel.IsDraftNewerThanPublishedWithSameTemplate(draftPage.GetPageData()))
        {
          PageNode pageNode2 = source.FirstOrDefault<PageNode>((Func<PageNode, bool>) (n => n.Id.Equals(draftPage.Id)));
          if (pageNode2 != null)
          {
            source.Remove(pageNode2);
            source.Add(draftPage);
          }
        }
      }
      return source;
    }

    /// <summary>
    /// The function builds the navigation node collection from collection of page nodes,
    /// which can be 2 different types which is set by isPublished flag parameter.
    /// Navigation nodes are in the default culture, or according to their page data.
    /// </summary>
    /// <param name="nodes">The collection of page nodes on which we build navigation nodes.</param>
    /// <param name="isPublished">The flag determines the type of collection to be build: if set to <c>true</c> published navigation nodes, otherwise drafts.</param>
    /// <returns>A collection of <c>NavigationNode</c> instances. </returns>
    public static List<NavigationNode> GetNavigationNodes(
      IEnumerable<PageNode> nodes,
      bool isPublished)
    {
      return PageTemplateViewModel.GetNavigationNodes(nodes, isPublished, (CultureInfo) null);
    }

    /// <summary>
    /// The function builds the navigation node collection from collection of page nodes,
    /// which can be 2 different types which is set by isPublished flag parameter.
    /// </summary>
    /// <param name="nodes">The collection of page nodes on which we build navigation nodes.</param>
    /// <param name="isPublished">The flag determines the type of collection to be build: if set to <c>true</c> published navigation nodes, otherwise drafts.</param>
    /// <param name="culture">The culture to get the nodes in. If set to null, gets them in the default culture, or according to their page data.</param>
    /// <returns>A collection of <c>NavigationNode</c> instances. </returns>
    public static List<NavigationNode> GetNavigationNodes(
      IEnumerable<PageNode> nodes,
      bool isPublished,
      CultureInfo culture)
    {
      List<NavigationNode> navigationNodes = new List<NavigationNode>();
      foreach (PageNode node in nodes)
      {
        NavigationNode navigationNode = PageTemplateViewModel.GetNavigationNode(node, isPublished, culture);
        if (navigationNode != null)
          navigationNodes.Add(navigationNode);
      }
      return navigationNodes;
    }

    /// <summary>
    /// The function builds the navigation node collection from collection of page nodes,
    /// which can be 2 different types which is set by isPublished flag parameter.
    /// </summary>
    /// <param name="node">The page node on which we build navigation nodes.</param>
    /// <param name="isPublished">The flag determines the type of collection to be build: if set to <c>true</c> published navigation nodes, otherwise drafts.</param>
    /// <param name="culture">The culture to get the nodes in. If set to null, gets them in the default culture, or according to their page data.</param>
    /// <returns>A <c>NavigationNode</c> instance. </returns>
    internal static NavigationNode GetNavigationNode(
      PageNode node,
      bool isPublished,
      CultureInfo culture)
    {
      bool isPublished1 = isPublished;
      PageData pageData = node.GetPageData(culture);
      if (pageData == null)
        return (NavigationNode) null;
      bool flag = pageData.LockedBy != Guid.Empty;
      if (isPublished1)
      {
        isPublished1 = pageData.Status == ContentLifecycleStatus.Live && pageData.Visible;
        if (PageTemplateViewModel.IsUnpublished<PageDraft>((IContentWithDrafts<PageDraft>) pageData) && !flag)
          isPublished1 = false;
      }
      string status = string.Empty;
      string standardPageStatusText = node.GetStandardPageStatusText(pageData, out status);
      string url = PageTemplateViewModel.GetUrl(node, isPublished1, culture);
      return culture == null ? new NavigationNode(node.Id, (string) node.Title, (string) node.Title, url, standardPageStatusText, status, node.RootNodeId) : new NavigationNode(node.Id, node.Title[culture], node.Title[culture], url, standardPageStatusText, status, node.RootNodeId);
    }

    internal static List<KeyValuePair<PageData, bool>> GetOverridenWidgetChangedPages(
      ControlData controlData)
    {
      List<KeyValuePair<PageData, bool>> widgetChangedPages = new List<KeyValuePair<PageData, bool>>();
      PageManager manager = PageManager.GetManager();
      TemplateDraftControl originalControl = manager.GetControls<TemplateDraftControl>().Where<TemplateDraftControl>((Expression<Func<TemplateDraftControl, bool>>) (e => e.Id == controlData.OriginalControlId)).SingleOrDefault<TemplateDraftControl>();
      if (originalControl != null && originalControl.OriginalControlId != Guid.Empty)
      {
        IQueryable<ControlData> controls = manager.GetControls<ControlData>();
        Expression<Func<ControlData, bool>> predicate = (Expression<Func<ControlData, bool>>) (a => a.BaseControlId == originalControl.OriginalControlId && a.OriginalControlId == Guid.Empty);
        foreach (ControlData controlData1 in controls.Where<ControlData>(predicate).ToList<ControlData>())
        {
          if (controlData1 is PageControl)
          {
            PageControl pageControl = (PageControl) controlData1;
            if (pageControl.Page != null && !pageControl.Page.IsDeleted)
            {
              PageData page = pageControl.Page;
              PageNode navigationNode = page.NavigationNode;
              CultureInfo culture = page.Culture.IsNullOrEmpty() ? ((IEnumerable<CultureInfo>) navigationNode.AvailableCultures).FirstOrDefault<CultureInfo>() : CultureInfo.GetCultureInfo(page.Culture);
              widgetChangedPages.Add(new KeyValuePair<PageData, bool>(page, LifecycleExtensions.IsPublished(page, culture)));
            }
          }
          else if (controlData1 is PageDraftControl)
          {
            PageDraftControl pageDraftControl = (PageDraftControl) controlData1;
            if (pageDraftControl.Page != null && !pageDraftControl.Page.ParentPage.IsDeleted)
              widgetChangedPages.Add(new KeyValuePair<PageData, bool>(pageDraftControl.Page.ParentPage, false));
          }
        }
      }
      return widgetChangedPages;
    }

    /// <summary>Gets the parent template.</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <returns>The parent template.</returns>
    public PageTemplate GetParentTemplate(PageTemplate pageTemplate)
    {
      PageTemplate parentTemplate = (PageTemplate) null;
      if (pageTemplate.ParentTemplate != null)
        parentTemplate = pageTemplate.ParentTemplate;
      return parentTemplate;
    }

    internal static string GetUrl(
      PageNode node,
      bool isPublished,
      CultureInfo culture,
      bool resolve = false)
    {
      if (isPublished)
        return node.GetLiveUrl(culture, resolve);
      string url = node.GetBackendUrl("Preview", culture);
      if (culture == null)
      {
        PageData pageData = node.GetPageData();
        if (pageData != null && !string.IsNullOrEmpty(pageData.Culture))
          url = url + "/" + pageData.Culture;
      }
      else
        url = url + "/" + culture.Name;
      return url;
    }

    internal static List<NavigationNode> GetNodesNewerThanPublishedWithSameTemplate(
      IList<PageNode> draftNodes)
    {
      List<NavigationNode> withSameTemplate = new List<NavigationNode>();
      List<PageNode> list = draftNodes.Where<PageNode>((Func<PageNode, bool>) (p => p != null && PageTemplateViewModel.IsDraftNewerThanPublishedWithSameTemplate(p.GetPageData()))).ToList<PageNode>();
      if (list.Count<PageNode>() > 0)
        withSameTemplate.AddRange((IEnumerable<NavigationNode>) PageTemplateViewModel.GetNavigationNodes((IEnumerable<PageNode>) list, false));
      return withSameTemplate;
    }

    internal static bool IsDraftNewerThanPublishedWithSameTemplate(PageData item)
    {
      if (item == null || item.Template == null)
        return false;
      IEnumerable<PageDraft> source = item.Drafts.Where<PageDraft>((Func<PageDraft, bool>) (d => !d.IsTempDraft));
      int num = source.Count<PageDraft>();
      return item.Version > 0 && num > 0 && source.FirstOrDefault<PageDraft>().Version > item.Version && item.Template.Id == source.FirstOrDefault<PageDraft>().TemplateId;
    }

    private static string GetUser(Guid id) => UserProfilesHelper.GetUserDisplayName(id);

    /// <summary>Gets the edit URL.</summary>
    /// <param name="templateId">The id of page template.</param>
    /// <returns>The url for editing a template.</returns>
    internal static string GetEditUrl(Guid templateId) => PageTemplateViewModel.GetEditUrl(templateId, (CultureInfo) null);

    internal static string GetEditUrl(Guid templateId, CultureInfo culture)
    {
      string url = "/Sitefinity/Template/" + (object) templateId;
      if (culture != null)
        url = url + "/" + culture.Name;
      return RouteHelper.ResolveUrl(url, UrlResolveOptions.Rooted);
    }

    internal static string GetPreviewUrl(Guid templateId) => PageTemplateViewModel.GetEditUrl(templateId) + string.Format("/Preview/{0}", (object) SystemManager.CurrentContext.Culture);

    internal static string GetStatisticsText(int pageCount, IEnumerable<Guid> rootNodeIds)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      StringBuilder stringBuilder2 = stringBuilder1;
      string str;
      if (pageCount != 1)
        str = Res.Get<PageResources>().PagesCount.Arrange((object) pageCount);
      else
        str = Res.Get<PageResources>().PageCount.Arrange((object) pageCount);
      stringBuilder2.Append(str);
      int num = rootNodeIds.Count<Guid>();
      if (num > 0 && !SystemManager.CurrentContext.IsOneSiteMode)
      {
        stringBuilder1.Append(" ");
        if (num == 1)
        {
          IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
          if (multisiteContext != null && multisiteContext.CurrentSite.SiteMapRootNodeId == rootNodeIds.First<Guid>())
            stringBuilder1.Append(Res.Get<MultisiteResources>().InThisSite);
          else
            stringBuilder1.Append(Res.Get<MultisiteResources>().InOneSite);
        }
        else
          stringBuilder1.Append(Res.Get<MultisiteResources>().SiteCountFormat.Arrange((object) num));
      }
      return stringBuilder1.ToString();
    }

    private static bool IsUnpublished<T>(IContentWithDrafts<T> item) where T : DraftData => item.Version > 0 && !item.IsDraftNewerThanPublished<T>();
  }
}
