// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.DraftProxyBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.DesignerToolbox;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Localization.Web.UI;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Newsletters;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Data;
using Telerik.Sitefinity.Modules.Pages.Web.UI;
using Telerik.Sitefinity.Mvc.Proxy;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Components;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Represents base proxy class for page and template drafts.
  /// </summary>
  public abstract class DraftProxyBase : IPageData, IPresentable
  {
    private Dictionary<string, ITemplate> templates;
    private List<Dictionary<string, ITemplate>> layouts;
    private List<IControlsContainer> controlContainersOrdered;
    private List<ControlData> dockControls;
    private List<ControlData> placeHolders;
    private List<ControlBuilder> controls;
    private PlaceHoldersCollection baseZones;
    private ZoneEditor editor;
    private ZoneEditorWrapper wrapper;
    private Control dataSourcesContainer;
    private Control lockingControl;
    private string cancelUrl;
    private bool isBackend;
    private string pageUrl;
    private Page page;
    private string title;
    private bool isSplitByLanguage;
    private List<CultureInfo> usedLanguages;
    private CultureInfo culture;
    private List<CultureInfo> unusedLanguages;
    private bool showLocalizationStrategySelector;
    private bool isPersonalized;
    private Guid personalizationMasterId;
    private Guid personalizationSegmentId;

    /// <summary>Gets the page data provider.</summary>
    /// <value>The page provider.</value>
    public virtual PageDataProvider PageProvider { get; protected set; }

    /// <summary>Gets the metadata data provider.</summary>
    /// <value>The provider.</value>
    public virtual FormsDataProvider FormsProvider { get; protected set; }

    /// <summary>Gets the newsletters data provider.</summary>
    /// <value>The provider.</value>
    public virtual NewslettersDataProvider NewslettersDataProvider { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is preview.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is preview; otherwise, <c>false</c>.
    /// </value>
    public virtual bool IsPreview { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is in inline editing mode.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is in inline editing mode; otherwise, <c>false</c>.
    /// </value>
    internal virtual bool IsInlineEditing { get; set; }

    public CultureInfo CurrentObjectCulture
    {
      get
      {
        if (this.culture != null)
          return this.culture;
        if (!this.Settings.Multilingual)
          return SystemManager.CurrentContext.Culture;
        return this.IsBackend ? this.Settings.DefaultBackendLanguage : this.Settings.DefaultFrontendLanguage;
      }
      set => this.culture = value;
    }

    /// <summary>Gets or sets the page pageId.</summary>
    /// <value>The page pageId.</value>
    public virtual Guid PageDraftId { get; protected set; }

    public virtual PageTemplateFramework Framework { get; protected set; }

    public virtual Guid LockedBy { get; set; }

    /// <summary>Gets or sets the PageNode id.</summary>
    public virtual Guid PageNodeId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the parent(base, public) item of the currently edited item. For example,
    /// for a page this contains the ID of the PageData object. For a form, this holds the ID of the FormDescription.
    /// </summary>
    /// <value>The ID of the parent item.</value>
    public virtual Guid ParentItemId { get; protected set; }

    /// <summary>Gets or sets the page title.</summary>
    /// <value>The page title.</value>
    public virtual string PageTitle { get; protected set; }

    internal virtual Telerik.Sitefinity.Localization.LocalizationStrategy? LocalizationStrategy { get; set; }

    /// <summary>Gets the templates.</summary>
    /// <value>The templates.</value>
    protected virtual Dictionary<string, ITemplate> Templates
    {
      get
      {
        if (this.templates == null)
          this.templates = new Dictionary<string, ITemplate>();
        return this.templates;
      }
    }

    /// <summary>Gets the layouts.</summary>
    /// <value>The layouts.</value>
    protected virtual List<Dictionary<string, ITemplate>> Layouts
    {
      get
      {
        if (this.layouts == null)
          this.layouts = new List<Dictionary<string, ITemplate>>();
        return this.layouts;
      }
    }

    /// <summary>Gets the docked controls.</summary>
    /// <value>The docked controls.</value>
    protected virtual List<ControlData> DockedControls
    {
      get
      {
        if (this.dockControls == null)
          this.dockControls = new List<ControlData>();
        return this.dockControls;
      }
    }

    protected virtual List<IControlsContainer> ControlContainersOrdered
    {
      get
      {
        if (this.controlContainersOrdered == null)
          this.controlContainersOrdered = new List<IControlsContainer>();
        return this.controlContainersOrdered;
      }
    }

    /// <summary>Gets the place holders.</summary>
    /// <value>The place holders.</value>
    protected virtual List<ControlData> PlaceHolders
    {
      get
      {
        if (this.placeHolders == null)
          this.placeHolders = new List<ControlData>();
        return this.placeHolders;
      }
    }

    /// <summary>Gets the controls.</summary>
    /// <value>The controls.</value>
    protected virtual List<ControlBuilder> Controls
    {
      get
      {
        if (this.controls == null)
          this.controls = new List<ControlBuilder>();
        return this.controls;
      }
    }

    /// <summary>Gets the type of the proxy class.</summary>
    /// <value>The type of the proxy.</value>
    protected internal virtual DesignMediaType MediaType => DesignMediaType.Page;

    /// <summary>Gets the controls toolbox name.</summary>
    /// <value>The controls toolbox.</value>
    internal abstract string ControlsToolbox { get; }

    /// <summary>Gets the layout toolbox name.</summary>
    /// <value>The layout toolbox.</value>
    internal abstract string LayoutToolbox { get; }

    public EditorToolBar Toolbar { get; protected set; }

    public bool ShowLocalizationStrategySelector
    {
      get => this.showLocalizationStrategySelector;
      set => this.showLocalizationStrategySelector = value;
    }

    public bool HideDraftButton { get; set; }

    public Control LockingControl
    {
      get => this.lockingControl;
      set => this.lockingControl = value;
    }

    /// <summary>Gets or sets the URL evaluation mode.</summary>
    /// <value>The URL evaluation mode.</value>
    public UrlEvaluationMode UrlEvaluationMode { get; set; }

    /// <summary>
    /// Gets or sets the value which indicates weather the page is personalized or not. If true page is
    /// personalized; otherwise it is not personalized.
    /// </summary>
    public bool IsPersonalized
    {
      get => this.isPersonalized;
      set => this.isPersonalized = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance can unlock an item.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance can unlock item; otherwise, <c>false</c>.
    /// </value>
    public bool CanUnlock { get; set; }

    /// <summary>
    /// Gets or sets the id of the page that is the master in case this page is a personalized variation.
    /// If this value is an empty GUID, it means that this page acts as a personalization master.
    /// </summary>
    public Guid PersonalizationMasterId
    {
      get => this.personalizationMasterId;
      set => this.personalizationMasterId = value;
    }

    /// <summary>
    /// Gets or sets the id of the segment for which the page is personalized. If page is a master
    /// (not a personalized version), this value is an empty GUID.
    /// </summary>
    public Guid PersonalizationSegmentId
    {
      get => this.personalizationSegmentId;
      set => this.personalizationSegmentId = value;
    }

    /// <summary>
    /// Gets or sets a URL for loading external page from the file system.
    /// </summary>
    /// <value>The URL of the external page.</value>
    public string ExternalPage { get; set; }

    /// <summary>Gets the master page.</summary>
    /// <value>The master page.</value>
    public string MasterPage { get; set; }

    /// <summary>
    /// Gets the theme in the current language. This method implements the fallback logic.
    /// </summary>
    /// <value>The theme.</value>
    public string Theme => this.Themes != null ? PageHelper.GetThemeName(this.Themes) : string.Empty;

    /// <summary>
    /// Contains a dictionary with theme values for each language of the template.
    /// </summary>
    public IDictionary<CultureInfo, string> Themes { get; set; }

    /// <summary>Gets or sets the HTML title.</summary>
    /// <value>The HTML title.</value>
    public string HtmlTitle { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically include script manager on the page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [include script manager]; otherwise, <c>false</c>.
    /// </value>
    public bool IncludeScriptManager { get; set; }

    /// <summary>Gets or sets the last ID used for new control.</summary>
    /// <value>The last control pageId.</value>
    public int LastControlId { get; set; }

    /// <summary>
    /// Gets a collection of data defining how this page should be presented on the user interface.
    /// </summary>
    /// <value>The presentation data.</value>
    IEnumerable<PresentationData> IPresentable.Presentation => throw new NotSupportedException();

    private string UnlockServiceUrl
    {
      get
      {
        string str = this.page.ResolveUrl("~/Sitefinity/Services/Pages/ZoneEditorService.svc/");
        switch (this.MediaType)
        {
          case DesignMediaType.Page:
          case DesignMediaType.NewsletterCampaign:
          case DesignMediaType.NewsletterTemplate:
            str += "Page/UnlockPage/";
            break;
          case DesignMediaType.Template:
            str += "Template/UnlockTemplate/";
            break;
          case DesignMediaType.Form:
            str += "Form/UnlockForm/";
            break;
        }
        return str + this.ParentItemId.ToString();
      }
    }

    private string CancelUrl
    {
      get
      {
        if (this.cancelUrl == null)
        {
          Guid guid;
          string nodeId;
          switch (this.MediaType)
          {
            case DesignMediaType.Page:
              if (SiteMapBase.GetSiteMapProviderForUrl(this.PageUrl) is SiteMapBase mapProviderForUrl && mapProviderForUrl.BackendLandingPageId != Guid.Empty)
              {
                nodeId = mapProviderForUrl.BackendLandingPageId.ToString();
                break;
              }
              if (this.IsBackend)
              {
                guid = SiteInitializer.BackendPagesActualNodeId;
                nodeId = guid.ToString();
                break;
              }
              guid = SiteInitializer.PagesNodeId;
              nodeId = guid.ToString();
              break;
            case DesignMediaType.Template:
              if (this.IsBackend)
              {
                guid = SiteInitializer.BackendPageTemplatesNodeId;
                nodeId = guid.ToString();
                break;
              }
              guid = SiteInitializer.PageTemplatesNodeId;
              nodeId = guid.ToString();
              break;
            case DesignMediaType.Form:
              guid = FormsModule.HomePageId;
              nodeId = guid.ToString();
              break;
            case DesignMediaType.NewsletterCampaign:
            case DesignMediaType.NewsletterTemplate:
              return SystemManager.CurrentHttpContext.Request.Params["ReturnUrl"];
            default:
              throw new ArgumentException();
          }
          string url = DraftProxyBase.GetBackendPageUrlByNodeId(nodeId);
          if (this.MediaType == DesignMediaType.Page || this.MediaType == DesignMediaType.Template || this.MediaType == DesignMediaType.Form)
          {
            NameValueCollection queryString = new NameValueCollection(1);
            if (this.Settings.Multilingual)
              queryString.Add("lang", this.CurrentObjectCulture.Name);
            NameValueCollection nameValueCollection = queryString;
            guid = SystemManager.CurrentContext.CurrentSite.Id;
            string str = guid.ToString();
            nameValueCollection.Add("sf_site", str);
            if (queryString.Count > 0)
              url = UrlPath.AppendQueryString(url, queryString);
          }
          this.cancelUrl = url;
        }
        return this.cancelUrl;
      }
    }

    internal static string GetBackendPageUrlByNodeId(string nodeId) => RouteHelper.ResolveUrl(((PageSiteNode) BackendSiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(nodeId)).GetUrl(SystemManager.CurrentContext.Culture, true), UrlResolveOptions.Rooted);

    internal string PageUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.pageUrl))
        {
          CultureInfo cultureInfo = this.Settings.Multilingual ? this.CurrentObjectCulture : (CultureInfo) null;
          using (new CultureRegion(cultureInfo))
          {
            SiteMapNode dataToken = (SiteMapNode) this.page.GetRequestContext().RouteData.DataTokens["SiteMapNode"];
            string url;
            if (dataToken is PageSiteNode node1)
            {
              if (cultureInfo == null || !(((SiteMapBase) dataToken.Provider).FindSiteMapNodeForSpecificLanguage((SiteMapNode) node1, cultureInfo) is PageSiteNode node1))
                ;
              url = node1.GetUrl(true, false);
            }
            else
              url = dataToken.Url;
            this.pageUrl = RouteHelper.ResolveUrl(url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
          }
        }
        return this.pageUrl;
      }
    }

    private string ViewUrl => this.GetItemPreviewUrl();

    protected void InitializeIsBackend(Page page)
    {
      switch (this.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          if (BackendSiteMap.GetCurrentProvider().CurrentNode == null)
            break;
          this.IsBackend = true;
          break;
        case DesignMediaType.Template:
          int num = page.Request.RawUrl.LastIndexOf("/");
          if (num <= -1)
            break;
          string g = page.Request.RawUrl.Substring(num + 1);
          int length = g.IndexOf("?");
          if (length != -1)
            g = g.Substring(0, length);
          if (!Utility.IsGuid(g))
            break;
          PageTemplate template = this.PageProvider.GetTemplate(new Guid(g));
          if (template == null)
            break;
          Guid templatesCategoryId = SiteInitializer.BackendTemplatesCategoryId;
          if (!template.Category.Equals(templatesCategoryId))
            break;
          this.IsBackend = true;
          break;
        case DesignMediaType.Form:
          this.IsBackend = false;
          break;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this object is being displayed in the backend or in the frontend.
    /// This is true for backend pages/templates and false for frontend pages/templates and forms.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is backend; otherwise, <c>false</c>.
    /// </value>
    internal bool IsBackend
    {
      get => this.isBackend;
      set => this.isBackend = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this object(page or form) has versions for each language or all language versions use a single object.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is split by language; otherwise, <c>false</c>.
    /// </value>
    public bool IsSplitByLanguage
    {
      get => this.isSplitByLanguage;
      set => this.isSplitByLanguage = value;
    }

    /// <summary>
    /// Gets or sets the languages for which this object(page or form) has translation. This is only used when split
    /// mode is used for the objet, e.g. there is a different object for each language e.g. IsSplitByLanguage = true.
    /// </summary>
    /// <value>The languages for which this object(page or form) has translation.</value>
    public List<CultureInfo> UsedLanguages
    {
      get => this.usedLanguages;
      set => this.usedLanguages = value;
    }

    /// <summary>
    /// Gets the languages for which this object does not have translation.
    /// </summary>
    /// <value>The unused languages.</value>
    public List<CultureInfo> UnusedLanguages
    {
      get
      {
        if (this.unusedLanguages == null)
        {
          this.unusedLanguages = new List<CultureInfo>();
          if (this.IsBackend)
            ((IEnumerable<CultureInfo>) this.Settings.DefinedBackendLanguages).ToList<CultureInfo>().ForEach((Action<CultureInfo>) (l => this.unusedLanguages.Add(l)));
          else
            ((IEnumerable<CultureInfo>) this.Settings.DefinedFrontendLanguages).ToList<CultureInfo>().ForEach((Action<CultureInfo>) (l => this.unusedLanguages.Add(l)));
          this.unusedLanguages.RemoveAll((Predicate<CultureInfo>) (ci => this.UsedLanguages.Contains(ci)));
        }
        return this.unusedLanguages;
      }
    }

    internal IAppSettings Settings => SystemManager.CurrentContext.AppSettings;

    /// <summary>
    /// Gets or sets the label used to represent the plural of the current object. Example: for a page this equals Pages. Used in Locking Dialog.
    /// </summary>
    /// <value>The label used to represent the plural of the current object.</value>
    public string Title
    {
      get
      {
        if (this.title == null)
        {
          switch (this.MediaType)
          {
            case DesignMediaType.Page:
              this.title = "Pages";
              break;
            case DesignMediaType.Template:
              this.title = "Templates";
              break;
            case DesignMediaType.Form:
              this.title = "Forms";
              break;
            case DesignMediaType.NewsletterCampaign:
            case DesignMediaType.NewsletterTemplate:
              this.title = "Newsletters";
              break;
          }
        }
        return this.title;
      }
      set => this.title = value;
    }

    /// <summary>Sets the page properties.</summary>
    /// <param name="page">The page.</param>
    public virtual void SetPageDirectives(Page page)
    {
      ThemeController.SetPageTheme(this.Theme, page);
      page.MasterPageFile = this.MasterPage;
      page.Title = this.HtmlTitle;
    }

    /// <summary>Applies the layouts.</summary>
    /// <param name="page">The page.</param>
    public void ApplyLayouts(Page page) => PageHelper.ApplyLayouts((IList<Dictionary<string, ITemplate>>) this.Layouts, page, this.Theme);

    /// <summary>Gets the page template.</summary>
    /// <value>The page template.</value>
    public virtual ITemplate GetPageTemplate() => PageHelper.GetPageTemplate((IDictionary<string, ITemplate>) this.Templates, this.Theme);

    /// <summary>Creates the child controls.</summary>
    /// <param name="page">The page.</param>
    public virtual void CreateChildControls(Page page)
    {
      this.page = page;
      this.AddResponsiveDesignLayoutTransformationCss(page);
      if (this.IsPreview || this.IsInlineEditing)
      {
        if (this.IncludeScriptManager)
          RouteHandler.EnsureScriptManager(page);
        PageHelper.CreateChildControls((IList<ControlBuilder>) this.Controls, page, false, this.IsInlineEditing);
      }
      else
      {
        this.InitializeIsBackend(page);
        Guid userId = ClaimsManager.GetCurrentIdentity().UserId;
        int num = !(this.LockedBy != Guid.Empty) ? 0 : (this.LockedBy != userId ? 1 : 0);
        ILockingControl child1;
        if (num != 0)
          child1 = (ILockingControl) new LockingDialog()
          {
            ProcessRequest = false
          };
        else
          child1 = (ILockingControl) new LockingHandler();
        this.lockingControl = (Control) child1;
        this.lockingControl.ID = "lockingHandler";
        child1.ShowCloseButton = true;
        child1.ShowViewButton = true;
        child1.ShowUnlockButton = this.CanUnlock;
        child1.Title = this.Title;
        child1.ItemName = this.PageTitle;
        child1.ViewUrl = this.ViewUrl;
        child1.CloseUrl = this.CancelUrl;
        if (num != 0)
        {
          RouteHandler.EnsureScriptManager(page);
          child1.LockedBy = this.LockedBy.ToString();
          child1.UnlockUrl = this.CancelUrl;
          if (this.CanUnlock)
            child1.UnlockServiceUrl = this.UnlockServiceUrl;
          page.Form.Controls.Add((Control) child1);
        }
        else
        {
          bool isBackend = this.IsBackend;
          Dictionary<string, string> dictionary = new Dictionary<string, string>();
          switch (this.MediaType)
          {
            case DesignMediaType.Page:
              dictionary.Add("Telerik.Sitefinity.Modules.Pages.Web.UI.Scripts.PageEditorToolBarExtension.js, Telerik.Sitefinity", "OnEditorToolBarLoaded");
              break;
          }
          if (ScriptManager.GetCurrent(page) == null)
            RouteHandler.EnsureScriptManager(page);
          page.Form.Controls.Add((Control) child1);
          this.dataSourcesContainer = new Control();
          this.wrapper = new ZoneEditorWrapper();
          this.wrapper.ContentContainer.Controls.Add(this.dataSourcesContainer);
          foreach (Control child2 in new ArrayList((ICollection) page.Form.Controls))
          {
            this.wrapper.ContentContainer.Controls.Add(child2);
            child2.Page = page;
          }
          LanguageToolBar languageToolBar = (LanguageToolBar) null;
          if (this.Settings.Multilingual)
          {
            languageToolBar = new LanguageToolBar();
            languageToolBar.ID = "languageToolbar";
            languageToolBar.IsBackend = this.IsBackend;
            languageToolBar.CurrentObjectId = this.MediaType == DesignMediaType.Page ? this.PageNodeId : this.ParentItemId;
            languageToolBar.BaseEditUrl = this.GetItemEditUrl(this.PageUrl);
            languageToolBar.Proxy = this;
            languageToolBar.CanStopSync = this is PageDraftProxy && !this.IsBackend && ((PageDraftProxy) this).CanBeSplit;
            languageToolBar.MediaType = this.MediaType;
          }
          this.Toolbar = new EditorToolBar()
          {
            DraftId = this.PageDraftId,
            PageNodeId = this.PageNodeId,
            LocalizationStrategy = this.LocalizationStrategy,
            ProxyType = this.MediaType,
            IsBackend = isBackend,
            ParentItemId = this.ParentItemId,
            PageTitle = this.PageTitle,
            ExternalClientScripts = (IDictionary<string, string>) dictionary,
            MediaType = this.MediaType,
            CancelUrl = this.CancelUrl,
            ViewUrl = this.ViewUrl,
            LockingHandler = (LockingHandler) this.LockingControl,
            LanguageToolbar = languageToolBar,
            Proxy = this
          };
          page.Form.Controls.Add((Control) this.Toolbar);
          page.Form.Controls.Add((Control) this.wrapper);
          this.baseZones = page.GetPlaceHolders();
          List<ControlData> controlDataList = new List<ControlData>(this.baseZones.Count + this.placeHolders.Count);
          foreach (Control baseZone in (Collection<Control>) this.baseZones)
          {
            StaticControlData staticControlData1 = new StaticControlData();
            staticControlData1.Caption = (string) new Lstring(baseZone.ID);
            staticControlData1.DraftId = this.PageDraftId;
            staticControlData1.ObjectType = typeof (LayoutControl).AssemblyQualifiedName;
            staticControlData1.Container = baseZone.ID;
            StaticControlData staticControlData2 = staticControlData1;
            StaticControlData staticControlData3 = staticControlData2;
            StaticControlProperty property = new StaticControlProperty();
            property.Name = "PlaceHolderId";
            property.Value = baseZone.ID;
            staticControlData3.AddProperty(property);
            controlDataList.Add((ControlData) staticControlData2);
          }
          controlDataList.AddRange((IEnumerable<ControlData>) this.placeHolders);
          IToolboxFactory toolboxFactory = ObjectFactory.Resolve<IToolboxFactory>();
          ZoneEditor zoneEditor = new ZoneEditor();
          zoneEditor.ID = "ZoneEditor";
          zoneEditor.DraftId = this.PageDraftId;
          zoneEditor.PageNodeId = this.PageNodeId;
          zoneEditor.Placeholders = (IList<ControlData>) controlDataList;
          zoneEditor.PageControls = (IList<ControlData>) this.dockControls;
          zoneEditor.ContainerIdsOrdered = this.ControlContainersOrdered.Select<IControlsContainer, Guid>((Func<IControlsContainer, Guid>) (cc => cc.Id)).ToList<Guid>();
          zoneEditor.ControlToolbox = toolboxFactory.ResolveToolbox(this.ControlsToolbox);
          zoneEditor.LayoutToolbox = toolboxFactory.ResolveToolbox(this.LayoutToolbox);
          zoneEditor.WebServiceUrl = "~/Sitefinity/Services/Pages/ZoneEditorService.svc/";
          zoneEditor.ControlWebMethodName = "Control";
          zoneEditor.LayoutWebMethodName = "Layout";
          zoneEditor.PropertyEditorUrl = "~/Sitefinity/Dialog/PropertyEditor";
          zoneEditor.SegmentSelectorUrl = "~/Sitefinity/Dialog/SegmentSelector";
          zoneEditor.Skin = "Default";
          zoneEditor.ShowTabStrip = false;
          zoneEditor.MediaType = this.MediaType;
          zoneEditor.Framework = this.Framework;
          zoneEditor.PageProvider = this.PageProvider;
          zoneEditor.FormsProvider = this.FormsProvider;
          zoneEditor.LockingHandlerId = this.lockingControl.ClientID;
          zoneEditor.LockingHandler = (LockingHandler) this.LockingControl;
          zoneEditor.ZoneEditorWrapper = this.wrapper;
          zoneEditor.ClientIDMode = ClientIDMode.Static;
          zoneEditor.Proxy = this;
          this.editor = zoneEditor;
          foreach (ControlData dockControl in this.dockControls)
          {
            if (dockControl.IsDataSource)
              this.dataSourcesContainer.Controls.Add(PageManager.GetManager().LoadControl((ObjectData) dockControl, (CultureInfo) null));
          }
          this.editor.ControlAdd += new EventHandler<ZoneEditorEventArgs>(this.editor_ControlAdd);
          this.editor.LayoutControlAdd += new EventHandler<ZoneEditorLayoutEventArgs>(this.editor_LayoutControlAdd);
          HtmlGenericControl child3 = new HtmlGenericControl();
          child3.TagName = "script";
          child3.Attributes.Add("type", "text/javascript");
          child3.InnerHtml = "resizePageEditor();";
          page.Form.Controls.Add((Control) child3);
          page.Form.Controls.Add((Control) this.editor);
          this.Toolbar.ZoneEditorID = this.editor.ClientID;
          if (!this.Settings.Multilingual || this.MediaType != DesignMediaType.Page || !this.ShowLocalizationStrategySelector)
            return;
          LanguageInstanceStrategySelector localizationSelectorControl = this.GetLocalizationSelectorControl();
          page.Form.Controls.Add((Control) localizationSelectorControl);
          this.wrapper.Attributes.CssStyle.Add(HtmlTextWriterStyle.Display, "none");
        }
      }
    }

    private void AddResponsiveDesignLayoutTransformationCss(Page page)
    {
      if (!SystemManager.IsModuleAccessible("ResponsiveDesign"))
        return;
      page.Controls.AddAt(1, (Control) new ResourceLinks()
      {
        UseEmbeddedThemes = false,
        Links = {
          new ResourceFile()
          {
            Name = "~/Sitefinity/Public/ResponsiveDesign/layout_transformations.css?pageDataId=" + (object) this.ParentItemId + "&unique_key=" + (object) Guid.NewGuid(),
            Static = true
          }
        }
      });
    }

    private LanguageInstanceStrategySelector GetLocalizationSelectorControl() => new LanguageInstanceStrategySelector()
    {
      Proxy = this
    };

    private void editor_LayoutControlAdd(object sender, ZoneEditorLayoutEventArgs e)
    {
      e.Editable = e.ControlData.Editable;
      LayoutControl child = (LayoutControl) PageManager.GetManager().LoadControl((ObjectData) e.ControlData, (CultureInfo) null);
      if (e.ControlContainer == null)
      {
        this.baseZones[((StaticControlData) e.ControlData).Container].Controls.Add((Control) child);
      }
      else
      {
        child.PlaceHolder = e.ControlData.PlaceHolder;
        e.ControlContainer.Controls.Add((Control) child);
      }
      e.LayoutControl = child;
    }

    private void editor_ControlAdd(object sender, ZoneEditorEventArgs e)
    {
      DesignTimeControl child = (DesignTimeControl) null;
      CultureInfo culture1 = SystemManager.CurrentContext.Culture;
      if (this.Settings.Multilingual)
        SystemManager.CurrentContext.Culture = this.CurrentObjectCulture;
      try
      {
        SystemManager.IsFrontEndControlRender = true;
        e.Editable = e.ControlData.Editable;
        string moduleName;
        Control control;
        if (ToolboxesConfig.Current.ValidateWidget(e.ControlData, out moduleName))
        {
          if (this.Settings.Multilingual)
          {
            CultureInfo culture2 = SystemManager.CurrentContext.Culture;
            int languageFallbackMode = (int) SystemManager.RequestLanguageFallbackMode;
            SystemManager.RequestLanguageFallbackMode = FallbackMode.NoFallback;
            SystemManager.CurrentContext.Culture = this.CurrentObjectCulture;
            control = PageManager.GetManager().LoadControl((ObjectData) e.ControlData, this.CurrentObjectCulture);
            SystemManager.CurrentContext.Culture = culture2;
            SystemManager.RequestLanguageFallbackMode = (FallbackMode) languageFallbackMode;
          }
          else
            control = PageManager.GetManager().LoadControl((ObjectData) e.ControlData, (CultureInfo) null);
          switch (control)
          {
            case IDataSource _:
              goto label_12;
            case MvcProxyBase originalControl:
              originalControl.ControlDataId = e.ControlData.Id.ToString();
              child = new DesignTimeControl((Control) originalControl);
              break;
            default:
              child = new DesignTimeControl(control);
              break;
          }
          e.ControlContainer.Controls.Add((Control) child);
        }
        else
        {
          control = (Control) new InactiveModuleControl(moduleName);
          e.ControlContainer.Controls.Add(control);
        }
label_12:
        e.RealControl = control;
      }
      catch (Exception ex)
      {
        if (child != null)
          e.ControlContainer.Controls.Remove((Control) child);
        e.ControlContainer.Controls.Add((Control) new DesignTimeControl(ex));
      }
      finally
      {
        if (this.Settings.Multilingual)
          SystemManager.CurrentContext.Culture = culture1;
        SystemManager.IsFrontEndControlRender = false;
      }
    }

    internal static string GetPageEditUrl(string pageUrl, CultureInfo language = null)
    {
      if (!pageUrl.EndsWith("/"))
        pageUrl += "/";
      string pageEditUrl = pageUrl + "Action" + "/" + "Edit";
      if (language != null)
        pageEditUrl = pageEditUrl + "/" + language.Name;
      return pageEditUrl;
    }

    private string GetItemEditUrl(string baseUrl)
    {
      if (this.MediaType != DesignMediaType.Page)
        return baseUrl;
      return baseUrl + "/" + "Action" + "/" + "Edit";
    }

    private string GetItemPreviewUrl()
    {
      string pageUrl = this.PageUrl;
      string itemPreviewUrl;
      switch (this.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          itemPreviewUrl = pageUrl + "/" + "Action" + "/" + "Preview";
          break;
        case DesignMediaType.Template:
        case DesignMediaType.Form:
          itemPreviewUrl = pageUrl + "/" + "Preview";
          break;
        default:
          throw new ArgumentException();
      }
      bool flag = this.Settings.Multilingual && this.CurrentObjectCulture != null;
      if (((this.MediaType == DesignMediaType.NewsletterCampaign ? 0 : (this.MediaType != DesignMediaType.NewsletterTemplate ? 1 : 0)) & (flag ? 1 : 0)) != 0)
        itemPreviewUrl = itemPreviewUrl + "/" + this.CurrentObjectCulture.Name;
      string str = SystemManager.CurrentHttpContext.Request.QueryString["sf_site"];
      if (!string.IsNullOrEmpty(str))
        itemPreviewUrl = itemPreviewUrl + "?" + "sf_site" + "=" + str;
      return itemPreviewUrl;
    }
  }
}
