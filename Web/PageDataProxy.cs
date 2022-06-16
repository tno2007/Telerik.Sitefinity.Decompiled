// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageDataProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Lifecycle.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Workflow.Model.Tracking;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Contains the metadata information for PageData object.
  /// It will be cached in PageSiteNode.
  /// </summary>
  internal class PageDataProxy : 
    IEquatable<PageDataProxy>,
    ILifecycleStatusItemLive,
    ILifecycleStatusItem,
    IBackendItem,
    IWorkflowItem,
    IHasRoot,
    IDataItemProxy,
    IWorkflowChildItem
  {
    private readonly object attributesLock = new object();
    private readonly IList<ILanguageData> languageDataList;
    private readonly PageDataProxy.PageDraftProxy master;
    private readonly ILstring description;
    private readonly ILstring htmlTitle;
    private readonly string brokenMessage;
    private readonly IDictionary<string, object> dependentObjects = (IDictionary<string, object>) new Dictionary<string, object>();
    private NameValueCollection attributes;
    private Guid pageNodeId;
    private DateTime lastModified;
    private Guid lastModifiedBy;
    private ILookup<Guid, Guid> widgetSegmentFromTemplateIds;
    private ILookup<Guid, Guid> widgetSegmentIds;
    private bool widgetSegmentIdsLoaded;
    private bool? hasPersonalizedWidgets;
    private bool? hasSecuredWidgets;
    private WeakReference<OutputCacheProfileElement> outputCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.PageDataProxy" /> class.
    /// </summary>
    internal PageDataProxy()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.PageDataProxy" /> class.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="manager">The page manager.</param>
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Ignored so that the file can be included in StyleCop")]
    public PageDataProxy(PageData pageData, PageManager manager)
    {
      if (pageData != null)
      {
        this.Id = pageData.Id;
        this.Version = pageData.Version;
        this.BuildStamp = pageData.BuildStamp;
        this.Status = pageData.Status;
        this.Visible = pageData.Visible;
        this.IsPersonalized = pageData.IsPersonalized;
        this.VariationTypeKey = pageData.VariationTypeKey;
        this.PersonalizationSegmentId = pageData.PersonalizationSegmentId;
        if (pageData.Template != null)
          this.TemplateFramework = pageData.Template.Framework;
        this.OutputCacheProfile = pageData.OutputCacheProfile;
        this.IncludeScriptManager = pageData.IncludeScriptManager;
        if (!string.IsNullOrEmpty(pageData.HeadTagContent))
          this.HeadTagContent = pageData.HeadTagContent;
        this.Themes = (IDictionary<CultureInfo, string>) PageHelper.GetTheme(pageData);
        this.UrlEvaluationMode = pageData.UrlEvaluationMode;
        PageNode navigationNode = pageData.NavigationNode;
        this.Culture = navigationNode.LocalizationStrategy == LocalizationStrategy.Split ? pageData.Culture : (string) null;
        this.RootKey = ((IHasRoot) navigationNode).RootKey;
        this.pageNodeId = navigationNode.Id;
        this.PublishedTranslations = (IList<string>) new List<string>((IEnumerable<string>) pageData.PublishedTranslations);
        this.description = (ILstring) new LstringProxy(pageData.Description);
        this.htmlTitle = (ILstring) new LstringProxy(pageData.HtmlTitle);
        List<PageTemplate> templates = PageHelper.GetTemplates(pageData);
        this.TemplatesIds = (IList<Guid>) templates.Select<PageTemplate, Guid>((Func<PageTemplate, Guid>) (t => t.Id)).ToArray<Guid>();
        IRendererCommonData rendererCommonData = (IRendererCommonData) pageData;
        if (templates.Count > 0)
          rendererCommonData = (IRendererCommonData) templates.Last<PageTemplate>();
        this.Renderer = rendererCommonData.Renderer;
        this.TemplateName = rendererCommonData.TemplateName;
        this.VersionKey = PageSiteNode.GenerateVersionKey(pageData, (IList<PageTemplate>) templates);
        this.LockedBy = pageData.LockedBy;
        this.lastModified = pageData.LastModified;
        this.lastModifiedBy = pageData.LastModifiedBy;
        this.PublicationDate = pageData.PublicationDate;
        this.ExpirationDate = pageData.ExpirationDate;
        this.CodeBehindType = pageData.CodeBehindType;
        this.EnableViewState = pageData.EnableViewState;
        this.languageDataList = (IList<ILanguageData>) new List<ILanguageData>((IEnumerable<ILanguageData>) pageData.LanguageData.Select<LanguageData, LanguageDataProxy>((Func<LanguageData, LanguageDataProxy>) (x => new LanguageDataProxy((ILanguageData) x))));
        PageDraft draft = pageData.Drafts.FirstOrDefault<PageDraft>((Func<PageDraft, bool>) (x => !x.IsTempDraft));
        if (draft != null)
        {
          this.master = new PageDataProxy.PageDraftProxy(draft, pageData);
          if (draft.Version > pageData.Version)
          {
            this.lastModified = draft.LastModified;
            this.lastModifiedBy = draft.Owner;
          }
        }
        this.ApprovalWorlflowState = (ILstring) new LstringProxy(pageData.NavigationNode.ApprovalWorkflowState);
      }
      else
      {
        this.brokenMessage = "The Page Node is marked as standard or external, however its Page Data is missing";
        this.lastModified = DateTime.UtcNow;
        this.PublicationDate = DateTime.UtcNow;
        this.ExpirationDate = new DateTime?(DateTime.UtcNow);
      }
    }

    public NameValueCollection GetAttributes(NameValueCollection baseAttributes)
    {
      NameValueCollection attributes = this.attributes;
      if (attributes == null)
      {
        lock (this.attributesLock)
        {
          attributes = this.attributes;
          if (attributes == null)
          {
            attributes = new NameValueCollection(baseAttributes);
            attributes["IncludeScriptManager"] = this.IncludeScriptManager.ToString();
            if (!string.IsNullOrEmpty(this.HeadTagContent))
              attributes["HeadTagContent"] = this.HeadTagContent;
            this.attributes = attributes;
          }
        }
      }
      return attributes;
    }

    public override bool Equals(object obj) => this.Equals(obj as PageDataProxy);

    public override int GetHashCode() => this.Id.GetHashCode();

    public bool Equals(PageDataProxy other) => other != null && this.Id.Equals(other.Id);

    public bool IsBackend { get; private set; }

    public int LanguageDataCount { get; private set; }

    public ILanguageData GetLanguageData(CultureInfo culture = null)
    {
      string languageKey = culture.GetLanguageKeyRaw();
      return this.IsBackend ? this.languageDataList.GetLanguageDataRawBackendPage(culture) : this.languageDataList.FirstOrDefault<ILanguageData>((Func<ILanguageData, bool>) (ld => ld.Language == languageKey));
    }

    public ILifecycleStatusItem GetMaster() => (ILifecycleStatusItem) this.master;

    private void ResolveWorkflowStatus(out string statusKey)
    {
      string str;
      this.ApprovalWorlflowState.TryGetValue(out str);
      statusKey = (string) null;
      if (str == null)
        return;
      statusKey = string.Copy(str);
    }

    /// <summary>Gets the page data id.</summary>
    /// <value>The page data id.</value>
    public Guid Id { get; private set; }

    /// <summary>Gets the version.</summary>
    /// <value>The version.</value>
    public int Version { get; private set; }

    /// <summary>Gets the build stamp.</summary>
    /// <value>The build stamp.</value>
    public int BuildStamp { get; private set; }

    /// <summary>Gets the current status of the page.</summary>
    /// <value>The status.</value>
    public ContentLifecycleStatus Status { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:Telerik.Sitefinity.Web.PageSiteNode" /> is visible.
    /// </summary>
    /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
    public bool Visible { get; private set; }

    /// <summary>Gets or sets the framework.</summary>
    /// <value>The framework.</value>
    public PageTemplateFramework TemplateFramework { get; internal set; }

    /// <summary>Gets the name of the output cache profile.</summary>
    /// <value>The output cache profile name.</value>
    public string OutputCacheProfile { get; private set; }

    /// <summary>
    /// Gets a value indicating whether include script manager.
    /// </summary>
    /// <value>The include script manager.</value>
    public bool IncludeScriptManager { get; private set; }

    /// <summary>Gets a value indicating whether to enable view state.</summary>
    /// <value>A value indicating whether to enable view state.</value>
    public bool EnableViewState { get; private set; }

    /// <summary>Gets the content of the head tag.</summary>
    /// <value>The content of the head tag.</value>
    public string HeadTagContent { get; private set; }

    /// <summary>Gets the code behind type for ASP.NET pages.</summary>
    /// <value>The code behind type for ASP.NET pages.</value>
    public string CodeBehindType { get; private set; }

    /// <summary>Gets the themes.</summary>
    /// <value>The themes.</value>
    public IDictionary<CultureInfo, string> Themes { get; private set; }

    /// <summary>
    ///  Gets the URL evaluation mode. Used by content view controls for paging and URLs for master/detail mode.
    /// </summary>
    /// <value>The URL evaluation mode.</value>
    public UrlEvaluationMode UrlEvaluationMode { get; private set; }

    /// <summary>Gets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string Culture { get; private set; }

    /// <summary>
    /// Gets a value indicating whether this node is a group page - does not have page date associated with it.
    /// Contains the translations in which the page is published. In monolingual is null.
    /// </summary>
    /// <value>The published translations of the item.</value>
    public IList<string> PublishedTranslations { get; private set; }

    /// <summary>Gets the description.</summary>
    /// <value>The description.</value>
    public string Description
    {
      get
      {
        string empty = string.Empty;
        if (this.description != null)
          this.description.TryGetValue(out empty, this.ResolveStatusCulture());
        return empty;
      }
    }

    /// <summary>Gets the html title.</summary>
    /// <value>The html title.</value>
    public string HtmlTitle
    {
      get
      {
        string empty = string.Empty;
        if (this.htmlTitle != null)
          this.htmlTitle.TryGetValue(out empty, this.ResolveStatusCulture());
        return empty;
      }
    }

    /// <summary>Gets a value indicating whether is personalized.</summary>
    /// <value>The is personalized.</value>
    public bool IsPersonalized { get; private set; }

    public Guid PersonalizationSegmentId { get; set; }

    public string VariationTypeKey { get; private set; }

    public string VersionKey { get; internal set; }

    /// <summary>Gets the ID of the user who has locked the page.</summary>
    /// <value>The ID of the user.</value>
    public virtual Guid LockedBy { get; private set; }

    /// <summary>Gets the time this item was last modified.</summary>
    /// <value>The last modified time.</value>
    public DateTime LastModified => this.lastModified;

    /// <summary>
    /// Gets the ID of the user who has last modified the page.
    /// </summary>
    /// <value>The ID of the user.</value>
    public virtual Guid LastModifiedBy => this.lastModifiedBy;

    /// <summary>
    /// Gets date and time when the item is going to be published (publicly available).
    /// </summary>
    public DateTime PublicationDate { get; private set; }

    public DateTime? ExpirationDate { get; private set; }

    internal IList<Guid> TemplatesIds { get; private set; }

    public Guid TemplateId => this.TemplatesIds == null ? Guid.Empty : this.TemplatesIds.FirstOrDefault<Guid>();

    public string Renderer { get; set; }

    public string TemplateName { get; set; }

    Guid IWorkflowItem.Id => this.pageNodeId;

    /// <inheritdoc />
    public Type WorkflowType => typeof (PageNode);

    string IDataItemProxy.GetProviderName() => ManagerBase<PageDataProvider>.GetDefaultProviderName();

    Type IDataItemProxy.GetActualType() => typeof (PageNode);

    public ILstring ApprovalWorlflowState { get; private set; }

    public string GetOverallStatus(out string statusMessage, bool? isBackend = null)
    {
      if (!this.brokenMessage.IsNullOrEmpty())
      {
        statusMessage = this.brokenMessage;
        return "PageBrokenMessage";
      }
      if (this.ApprovalWorlflowState != null)
      {
        bool isBackend1 = this.IsBackend;
        if (isBackend.HasValue)
          this.IsBackend = isBackend.Value;
        string statusKey;
        statusMessage = this.GetLocalizedStatus(out statusKey, out IStatusInfo _, this.ResolveStatusCulture());
        LifecycleExtensions.GetOverallStatus((ILifecycleStatusItemLive) this, ((CultureInfo) null).GetSitefinityCulture(), ref statusKey, ref statusMessage);
        this.IsBackend = isBackend1;
        return statusKey;
      }
      statusMessage = string.Empty;
      return string.Empty;
    }

    internal object GetOrAddDependentObject(string key, Func<object> dependentObjectFactory)
    {
      object addDependentObject;
      if (!this.dependentObjects.TryGetValue(key, out addDependentObject))
      {
        lock (this.dependentObjects)
        {
          if (!this.dependentObjects.TryGetValue(key, out addDependentObject))
          {
            addDependentObject = dependentObjectFactory();
            this.dependentObjects.Add(key, addDependentObject);
          }
        }
      }
      return addDependentObject;
    }

    internal OutputCacheProfileElement OutputCache
    {
      get
      {
        OutputCacheProfileElement target;
        return this.outputCache != null && this.outputCache.TryGetTarget(out target) ? target : (OutputCacheProfileElement) null;
      }
      set => this.outputCache = new WeakReference<OutputCacheProfileElement>(value);
    }

    private CultureInfo ResolveStatusCulture() => this.Culture.IsNullOrEmpty() ? SystemManager.CurrentContext.Culture : CultureInfo.GetCultureInfo(this.Culture);

    /// <summary>
    /// Gets a value indicating whether this page has personalized widgets.
    /// </summary>
    internal bool HasPersonalizedWidgets
    {
      get
      {
        if (!this.hasPersonalizedWidgets.HasValue)
        {
          lock (this)
          {
            if (!this.hasPersonalizedWidgets.HasValue)
            {
              this.hasPersonalizedWidgets = new bool?(false);
              if (SystemManager.IsModuleAccessible("Personalization"))
              {
                PageManager manager = PageManager.GetManager();
                using (new ReadUncommitedRegion((IManager) manager))
                {
                  using (new ElevatedModeRegion((IManager) manager))
                  {
                    int num;
                    if (this.WidgetSegmentIds != null && this.WidgetSegmentIds.Count > 0)
                      num = manager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == this.Id && c.IsPersonalized)).Any<PageControl>() ? 1 : 0;
                    else
                      num = 0;
                    this.hasPersonalizedWidgets = new bool?(num != 0);
                  }
                }
              }
            }
          }
        }
        return this.hasPersonalizedWidgets.Value;
      }
    }

    internal bool HasSecuredWidgets
    {
      get
      {
        if (!this.hasSecuredWidgets.HasValue)
        {
          lock (this)
          {
            if (!this.hasSecuredWidgets.HasValue)
            {
              this.hasSecuredWidgets = new bool?(false);
              PageManager manager = PageManager.GetManager();
              using (new ReadUncommitedRegion((IManager) manager))
              {
                using (new ElevatedModeRegion((IManager) manager))
                  this.hasSecuredWidgets = new bool?(manager.GetControls<PageControl>().Where<PageControl>((Expression<Func<PageControl, bool>>) (c => c.Page.Id == this.Id && c.Permissions.Any<Permission>((Func<Permission, bool>) (p => p.PrincipalId == SecurityManager.EveryoneRole.Id && p.Grant == 0 || p.PrincipalId == SecurityManager.EveryoneRole.Id && (p.Deny & 1) > 0)))).Any<PageControl>());
              }
            }
          }
        }
        return this.hasSecuredWidgets.Value;
      }
    }

    /// <summary>
    /// Gets or sets the ids of the segments used for personalization of any widget on the page.
    /// </summary>
    internal ILookup<Guid, Guid> WidgetSegmentIds
    {
      get
      {
        if (!this.widgetSegmentIdsLoaded)
        {
          lock (this)
          {
            if (!this.widgetSegmentIdsLoaded)
            {
              this.widgetSegmentIdsLoaded = true;
              if (SystemManager.IsModuleAccessible("Personalization"))
                this.widgetSegmentIds = PageHelper.GetWidgetSegmentIds(this.Id, this.TemplatesIds);
            }
          }
        }
        return this.widgetSegmentIds;
      }
      set => this.widgetSegmentIds = value;
    }

    /// <summary>
    /// Gets or sets the ids of the segments used for personalization of any widget on the page that comes from a template.
    /// </summary>
    internal ILookup<Guid, Guid> WidgetSegmentFromTemplateIds
    {
      get
      {
        if (this.widgetSegmentFromTemplateIds == null && SystemManager.IsModuleAccessible("Personalization"))
          this.widgetSegmentFromTemplateIds = PageManager.GetManager().GetControlsFromTemplatesSegmentIds(this.TemplatesIds).ToLookup<KeyValuePair<Guid, Guid>, Guid, Guid>((Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Key), (Func<KeyValuePair<Guid, Guid>, Guid>) (kv => kv.Value));
        return this.widgetSegmentFromTemplateIds;
      }
      set => this.widgetSegmentFromTemplateIds = value;
    }

    public string RootKey { get; private set; }

    internal static PageDataProxy Empty => new PageDataProxy()
    {
      Visible = true
    };

    ApprovalTrackingRecordMap IWorkflowItem.ApprovalTrackingRecordMap { get; set; }

    private class PageDraftProxy : ILifecycleStatusItem, IBackendItem
    {
      private IList<ILanguageData> languageDataList;

      public PageDraftProxy(PageDraft draft, PageData pd)
      {
        this.IsBackend = pd.IsBackend;
        this.LanguageDataCount = draft.LanguageData.Count;
        this.Version = draft.Version;
        this.languageDataList = (IList<ILanguageData>) new List<ILanguageData>((IEnumerable<ILanguageData>) draft.LanguageData.Select<LanguageData, LanguageDataProxy>((Func<LanguageData, LanguageDataProxy>) (x => new LanguageDataProxy((ILanguageData) x))));
      }

      public bool IsBackend { get; private set; }

      public int LanguageDataCount { get; private set; }

      public ILanguageData GetLanguageData(CultureInfo culture = null) => this.languageDataList.GetLanguageDataRaw(culture);

      public int Version { get; private set; }
    }
  }
}
