// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.UrlLocalizationStrategies;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Workflow;
using Telerik.Sitefinity.Workflow.Activities;
using Telerik.Sitefinity.Workflow.Services.Data;
using Telerik.Sitefinity.Workflow.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services.Model
{
  /// <summary>ViewModel class for the page object.</summary>
  [DataContract]
  public class PageViewModel : WcfPageNode
  {
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:FieldsMustBePrivate", Justification = "Ignored so that the file can be included in StyleCop.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Ignored so that the file can be included in StyleCop.")]
    internal static string REDIRECTING_PAGE = "Redirect";
    private DateTime? dateCreated;
    private const string DataIntelligenceConnectorModuleName = "DataIntelligenceConnector";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> class.
    /// </summary>
    public PageViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> class.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="manager">The manager.</param>
    public PageViewModel(PageNode pageNode, PageManager manager = null)
      : this(pageNode, manager, false)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> class.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="isEditable">if set to <c>true</c> [is editable].</param>
    /// <param name="isContentEditable">Sets whether the content of the page is editable.</param>
    /// <param name="resolveToUserCreatedNode">The resolve to user created node.</param>
    /// <param name="excludeDecisions">Sets whether the workflow decisions should be calculated and included in the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> instance or not.</param>
    public PageViewModel(
      PageNode pageNode,
      PageManager manager,
      bool isEditable,
      bool isContentEditable,
      bool resolveToUserCreatedNode = false,
      bool excludeDecisions = false)
      : this(pageNode, manager, pageNode.GetSiteMapNode(), excludeDecisions)
    {
      this.IsEditable = isEditable;
      this.IsContentEditable = isContentEditable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> class.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="manager">The manager.</param>
    /// <param name="resolveToUserCreatedNode">The resolve to user created node.</param>
    /// <param name="excludeDecisions">Sets whether the workflow decisions should be calculated and included in the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.Services.Model.PageViewModel" /> instance or not.</param>
    public PageViewModel(
      PageNode pageNode,
      PageManager manager,
      bool resolveToUserCreatedNode,
      bool excludeDecisions = false)
      : this(pageNode, manager, pageNode.GetSiteMapNode(), excludeDecisions)
    {
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "wfOperations. Ignored so that the file can be included in StyleCop")]
    internal PageViewModel(PageSiteNode siteNode, bool excludeDecisions = false)
      : this((PageNode) null, (PageManager) null, siteNode, excludeDecisions)
    {
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "wfOperations. Ignored so that the file can be included in StyleCop")]
    internal PageViewModel(
      PageNode pageNode,
      PageManager manager,
      PageSiteNode siteNode,
      bool excludeDecisions = false)
      : base(siteNode)
    {
      this.SiteNode = siteNode;
      this.UrlName = siteNode.UrlName;
      string str1 = siteNode.GetLiveUrl(false);
      bool isMultilingual = siteNode.IsBackend ? ((IEnumerable<CultureInfo>) SystemManager.CurrentContext.AppSettings.DefinedBackendLanguages).Count<CultureInfo>() > 1 : SystemManager.CurrentContext.AppSettings.Multilingual;
      if (isMultilingual)
        str1 = ObjectFactory.Resolve<UrlLocalizationService>().ResolveUrl(str1, SystemManager.CurrentContext.Culture);
      this.CultureSpecificUrl = str1;
      this.PageViewUrl = !string.IsNullOrEmpty(str1) ? (RouteHelper.IsAbsoluteUrl(str1) ? str1 : VirtualPathUtility.ToAbsolute(str1)) : string.Empty;
      this.PageLiveUrl = siteNode.GetPageViewUrl();
      PageDataProxy data = (PageDataProxy) null;
      if (siteNode.NodeType == NodeType.Standard || siteNode.NodeType == NodeType.External)
      {
        this.SetEditBackendUrls(siteNode, this, isMultilingual);
        data = siteNode.CurrentPageDataItem;
        if (data != null && data.Id != Guid.Empty)
        {
          this.PageDataId = data.Id;
          this.VersioningId = data.Id;
          this.Visible = this.WasPublished;
          SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
          this.PageLifecycleStatus = new WcfContentLifecycleStatus()
          {
            IsAdmin = currentIdentity.IsUnrestricted,
            IsLocked = data.LockedBy != Guid.Empty,
            IsLockedByMe = data.LockedBy == currentIdentity.UserId,
            IsPublished = data.Status == ContentLifecycleStatus.Live,
            LockedByUsername = CommonMethods.GetUserName(data.LockedBy),
            Message = this.StatusText,
            SupportsContentLifecycle = true,
            LastModified = new DateTime?(data.LastModified),
            LastModifiedBy = CommonMethods.GetUserName(data.LastModifiedBy),
            PublicationDate = new DateTime?(data.PublicationDate)
          };
          if (!excludeDecisions && typeof (IApprovalWorkflowItem).IsAssignableFrom(typeof (PageNode)))
          {
            Guid itemId = Guid.Empty;
            if (!string.IsNullOrEmpty(siteNode.Key))
              itemId = new Guid(siteNode.Key);
            IDictionary<string, DecisionActivity> dictionary = pageNode == null || manager == null ? WorkflowManager.GetCurrentDecisions(typeof (PageNode), siteNode.PageProviderName, itemId, SystemManager.CurrentContext.Culture) : WorkflowManager.GetCurrentDecisions((IWorkflowItem) pageNode, (IManager) manager, SystemManager.CurrentContext.Culture);
            List<WorkflowVisualElement> workflowVisualElementList = new List<WorkflowVisualElement>();
            if (dictionary != null)
            {
              foreach (KeyValuePair<string, DecisionActivity> keyValuePair in (IEnumerable<KeyValuePair<string, DecisionActivity>>) dictionary)
                workflowVisualElementList.Add(WorkflowVisualElementFactory.ResolveVisualElement(keyValuePair.Key, keyValuePair.Value, true));
            }
            this.WorkflowOperations = (IList<WorkflowVisualElement>) workflowVisualElementList;
          }
          this.IsPersonalized = data.IsPersonalized;
          this.PageVariationViewModels = new Dictionary<string, PageVariationViewModel>();
          foreach (IPageVariationPlugin pageVariationPlugin in ObjectFactory.Container.ResolveAll<IPageVariationPlugin>())
          {
            bool isDecConnected = false;
            ObjectFactory.Container.ResolveAll<IModuleConnectionStatus>().FirstOrDefault<IModuleConnectionStatus>((Func<IModuleConnectionStatus, bool>) (m => m.ModuleName == "DataIntelligenceConnector"))?.ExecuteIfConfigured((Action) (() => isDecConnected = true));
            string str2 = isDecConnected ? pageVariationPlugin.GetReportsLink(siteNode) : string.Empty;
            bool flag = pageVariationPlugin.Key == "pers" && data.HasPersonalizedWidgets;
            if (siteNode.GetVariationTypes().Contains<string>(pageVariationPlugin.Key) | flag)
              this.PageVariationViewModels.Add(pageVariationPlugin.Key, new PageVariationViewModel()
              {
                Description = pageVariationPlugin.GetDescription(siteNode),
                Link = str2,
                LinkTitle = pageVariationPlugin.GetReportsLinkTitle(siteNode)
              });
          }
          this.HasPersonalizedWidgets = data.HasPersonalizedWidgets;
        }
      }
      else
      {
        this.PageEditUrl = "#";
        this.IsGroup = siteNode.NodeType == NodeType.Group;
        this.Visible = true;
      }
      this.SetPageStatusText(siteNode, data);
      if (string.IsNullOrEmpty(this.Status))
      {
        this.Status = ContentUIStatus.Draft.ToString();
        this.StatusText = Res.Get("PageResources", this.Status);
      }
      this.AdditionalStatus = StatusResolver.Resolve(typeof (PageNode), this.ProviderName, this.Id, this.RootId.ToString());
      this.SetLocation(siteNode);
      this.ParentTitlesPath = this.GetParentTitlesPath(siteNode);
      this.Owner = CommonMethods.GetUser(siteNode.Owner);
      this.DateCreated = siteNode.DateCreated;
      this.IsHomePage = siteNode.IsHomePage();
      this.LevelOrdinal = siteNode.Ordinal;
      this.IsSubPageCreationAllowed = siteNode.IsGranted("Create");
      this.IsContentEditable = siteNode.IsGranted("EditContent");
      this.IsEditable = siteNode.IsGranted("Modify");
      this.IsUnlockable = siteNode.IsGranted("Unlock");
    }

    private void SetEditBackendUrls(
      PageSiteNode siteNode,
      PageViewModel pageProxy,
      bool isMultilingual)
    {
      if (isMultilingual)
      {
        pageProxy.PageEditLanguageUrls = new Dictionary<string, string>();
        foreach (string availableLanguage in pageProxy.AvailableLanguages)
        {
          string pageEditBackendUrl = siteNode.GetPageEditBackendUrl(CultureInfo.GetCultureInfo(availableLanguage));
          pageProxy.PageEditLanguageUrls.Add(availableLanguage, pageEditBackendUrl);
          if (availableLanguage.Equals(SystemManager.CurrentContext.Culture.Name))
            pageProxy.PageEditUrl = pageEditBackendUrl;
        }
      }
      else
        pageProxy.PageEditUrl = siteNode.GetPageEditBackendUrl();
    }

    private void SetLocation(PageSiteNode siteNode)
    {
      if (siteNode.ParentKey.IsNullOrEmpty())
        return;
      string fullPath = (string) null;
      if (!PageHelper.TryGetFullUrlFromCache(Guid.Parse(siteNode.ParentKey), " > ", out fullPath))
      {
        SiteMapNode parentNode = siteNode.ParentNode;
        if (parentNode != null)
        {
          fullPath = (parentNode as PageSiteNode).GetTitlesPath();
        }
        else
        {
          this.Location = " <span class='sfSep'>|</span> <strong>The page's parent is not available in this context.</strong>";
          return;
        }
      }
      if (!string.IsNullOrEmpty(fullPath))
        this.Location = string.Format(" <span class='sfSep'>| under</span> <em>{0}</em>", (object) HttpUtility.HtmlEncode(fullPath));
      else
        this.Location = " <span class='sfSep'>|</span> on <strong>top level</strong>";
    }

    private void SetPageStatusText(PageSiteNode node, PageDataProxy data)
    {
      switch (node.NodeType)
      {
        case NodeType.Standard:
        case NodeType.External:
          string statusMessage;
          this.Status = !node.IsBackend ? data.GetOverallStatus(out statusMessage) : data.GetOverallStatus(out statusMessage, new bool?(node.IsBackend));
          this.StatusText = statusMessage;
          break;
        case NodeType.Group:
          this.Status = "Group";
          this.StatusText = Res.Get("PageResources", this.Status);
          break;
        case NodeType.InnerRedirect:
        case NodeType.OuterRedirect:
        case NodeType.Rewriting:
          this.Status = PageViewModel.REDIRECTING_PAGE;
          this.StatusText = string.Format(Res.Get<PageResources>().RedirectingPage);
          break;
      }
    }

    private string GetParentTitlesPath(PageSiteNode pageSiteNode)
    {
      string parentsOnlyTitlesPath = pageSiteNode.GetParentsOnlyTitlesPath();
      return !string.IsNullOrEmpty(parentsOnlyTitlesPath) ? "Under " + parentsOnlyTitlesPath : "On Top level";
    }

    /// <summary>Gets or sets the template.</summary>
    /// <value>The template.</value>
    [DataMember]
    [Obsolete("This property will be removed from future Versions.")]
    public WcfPageTemplate Template { get; set; }

    /// <summary>
    /// Gets or sets the Id of the current template. If the page has a draft this is the Id of the template of the draft,
    /// otherwise, it is the template of the PageData.
    /// </summary>
    /// <value>The template.</value>
    [DataMember]
    public Guid CurrentTemplateId { get; set; }

    /// <summary>Gets or sets the page data id.</summary>
    /// <value>The page data id.</value>
    [DataMember]
    public Guid PageDataId { get; set; }

    /// <summary>Gets or sets the name used in URLs.</summary>
    /// <value>The name used in URLs.</value>
    [DataMember]
    public string UrlName { get; set; }

    /// <summary>Gets or sets the page location.</summary>
    /// <value>The page location.</value>
    [DataMember]
    public string PageLocation { get; set; }

    /// <summary>
    /// Gets or sets the URL for viewing the page. Note that this URL does not contain
    /// language information e.g. the current URL localization strategy is not applied.
    /// For language-specific URL, use PageLiveUrl.
    /// </summary>
    /// <value>The URL for viewing the page.</value>
    [DataMember]
    public string PageViewUrl { get; set; }

    /// <summary>
    /// Gets or sets the page live URL (absolute). This is a language-specific URL, generated
    /// using the active URL localization strategy and the current thread UI culture (active
    /// when this object was created).
    /// </summary>
    /// <value>The page live URL.</value>
    [DataMember]
    public string PageLiveUrl { get; set; }

    /// <summary>Gets or sets the URL for editing the page.</summary>
    /// <value>The URL for editing the page.</value>
    [DataMember]
    public string PageEditUrl { get; set; }

    /// <summary>
    /// Gets or sets the default urls for page edit for each available language.
    /// </summary>
    /// <value>The page URLs for page edit for each of the available cultures.</value>
    [DataMember]
    public Dictionary<string, string> PageEditLanguageUrls { get; set; }

    /// <summary>Gets or sets the level ordinal.</summary>
    /// <value>The level ordinal.</value>
    [DataMember]
    public float LevelOrdinal { get; set; }

    /// <summary>
    /// Gets or sets the date on which the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> was created
    /// </summary>
    [DataMember]
    public DateTime DateCreated
    {
      get => this.dateCreated.Value;
      set => this.dateCreated = new DateTime?(value);
    }

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

    /// <summary>Gets or sets the additional status.</summary>
    [DataMember]
    public Telerik.Sitefinity.Services.Status AdditionalStatus { get; set; }

    /// <summary>
    /// Gets or sets the Owner of the <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> if the Page property is not null
    /// </summary>
    [DataMember]
    public string Owner { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is group.
    /// </summary>
    /// <value><c>true</c> if this instance is group; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsGroup { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is editable.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance is editable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can be unlocked.
    /// </summary>
    /// <value>
    ///     <c>true</c> if this instance can be unlocked, otherwise <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsUnlockable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the content of this page is editable (can open it in page editor)
    /// </summary>
    [DataMember]
    public bool IsContentEditable { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the user is allowed to create sub-pages for this page
    /// </summary>
    [DataMember]
    public bool IsSubPageCreationAllowed { get; set; }

    /// <summary>Gets or sets the location.</summary>
    /// <value>The location.</value>
    [DataMember]
    public string Location { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is home page.
    /// </summary>
    /// <value><c>true</c> if this instance is home page; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsHomePage { get; set; }

    /// <summary>Gets or sets information about the content lifecycle.</summary>
    [DataMember]
    public WcfContentLifecycleStatus PageLifecycleStatus { get; set; }

    /// <summary>
    /// Gets or sets information about the workflow operations available.
    /// </summary>
    [DataMember]
    public IList<WorkflowVisualElement> WorkflowOperations { get; set; }

    /// <summary>
    /// Gets or sets the Id of the PageData of this page node.
    /// </summary>
    [DataMember]
    public Guid VersioningId { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page data (if any) is visible.
    /// </summary>
    /// <value><c>true</c> if the page data visible; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool Visible { get; set; }

    /// <summary>
    /// Gets or sets the URL for the current page with the specific culture in the path.
    /// </summary>
    /// <value>The culture specific URL.</value>
    public string CultureSpecificUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the page data is personalized.
    /// </summary>
    /// <value>
    /// <c>true</c> if the page is personalized; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsPersonalized { get; set; }

    /// <summary>Gets or sets the variation view models.</summary>
    /// <value>The variation view models</value>
    [DataMember]
    public Dictionary<string, PageVariationViewModel> PageVariationViewModels { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance has personalized widgets.
    /// </summary>
    /// <value>
    /// <c>true</c> if this instance has personalized widgets; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool HasPersonalizedWidgets { get; set; }

    /// <summary>Gets or sets the parents titles path.</summary>
    /// <value>The parents titles path.</value>
    [DataMember]
    public string ParentTitlesPath { get; set; }

    internal PageSiteNode SiteNode { get; private set; }
  }
}
