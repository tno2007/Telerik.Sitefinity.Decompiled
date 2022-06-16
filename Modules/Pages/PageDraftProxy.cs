// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageDraftProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Represents page draft runtime data.</summary>
  public sealed class PageDraftProxy : DraftProxyBase
  {
    private PageNode pageNode;
    private PageData page;
    private Guid templateId;

    /// <summary>Gets the type of the proxy class.</summary>
    /// <value>The type of the proxy.</value>
    protected internal override DesignMediaType MediaType => DesignMediaType.Page;

    /// <summary>Gets the controls toolbox name.</summary>
    /// <value>The controls toolbox.</value>
    internal override string ControlsToolbox => "PageControls";

    /// <summary>Gets the layout toolbox name.</summary>
    /// <value>The layout toolbox.</value>
    internal override string LayoutToolbox => "PageLayouts";

    public bool CanBeSplit { get; set; }

    public PageData Page => this.page;

    public PageNode PageNode => this.pageNode;

    /// <summary>Gets the template id for this page.</summary>
    public Guid TemplateId => this.templateId;

    internal PageDraftProxy(
      PageDraft pageData,
      PageNode pageNode,
      PageDataProvider provider,
      bool isPreview,
      CultureInfo objectCulture,
      bool optimized,
      bool isInlineEditing = false)
    {
      this.page = pageData.ParentPage;
      if (pageNode == null)
        pageNode = this.page.NavigationNode;
      this.pageNode = pageNode;
      this.templateId = pageData.TemplateId;
      this.CurrentObjectCulture = objectCulture;
      this.PageDraftId = pageData.Id;
      if (pageNode == null)
        this.PageNodeId = Guid.Empty;
      else
        this.PageNodeId = pageNode.Id;
      this.PageProvider = provider;
      this.IsPreview = isPreview;
      this.IsInlineEditing = isInlineEditing;
      this.ParentItemId = this.page.Id;
      this.PageTitle = (string) this.pageNode.Title;
      this.LocalizationStrategy = new LocalizationStrategy?(pageNode.LocalizationStrategy);
      bool includeScriptManager = false;
      this.UrlEvaluationMode = pageData.UrlEvaluationMode;
      if (this.Settings.Multilingual && !isPreview)
      {
        this.CanBeSplit = pageNode.CanBeSplit;
        List<CultureInfo> cultureInfoList = new List<CultureInfo>();
        CultureInfo[] availableCultures = pageNode.AvailableCultures;
        cultureInfoList.AddRange((IEnumerable<CultureInfo>) availableCultures);
        this.UsedLanguages = cultureInfoList;
        this.IsSplitByLanguage = pageNode.LocalizationStrategy == LocalizationStrategy.Split || pageNode.LocalizationStrategy == LocalizationStrategy.NotSelected;
        this.InitializeIsBackend((System.Web.UI.Page) null);
        if (this.page.IsAutoCreated && this.pageNode.IsSplitPage)
          this.ShowLocalizationStrategySelector = true;
        else
          this.ShowLocalizationStrategySelector = !this.IsBackend && this.pageNode.LocalizationStrategy == LocalizationStrategy.NotSelected && cultureInfoList.Count > 1 && this.CurrentObjectCulture.Name != this.page.Culture;
      }
      else
        this.ShowLocalizationStrategySelector = false;
      PageHelper.ProcessPresentationData(((IPresentable) pageData).Presentation, (IList<Dictionary<string, ITemplate>>) this.Layouts, this.Templates);
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      if (optimized)
        controlsContainerList.Add((IControlsContainer) new PageDraftControlsContainerWrapper((IControlsContainer) pageData, this.PageProvider));
      else
        controlsContainerList.Add((IControlsContainer) pageData);
      PageTemplate pageTemplate = (PageTemplate) null;
      try
      {
        if (pageData.TemplateId != Guid.Empty)
        {
          bool suppressSecurityChecks = this.PageProvider.SuppressSecurityChecks;
          this.PageProvider.SuppressSecurityChecks = true;
          pageTemplate = this.PageProvider.GetTemplate(pageData.TemplateId);
          this.Framework = pageTemplate.Framework;
          this.PageProvider.SuppressSecurityChecks = suppressSecurityChecks;
        }
      }
      catch (ItemNotFoundException ex)
      {
        throw new TemplateNotFoundException(ex.Message);
      }
      if (string.IsNullOrEmpty(pageData.MasterPage))
      {
        string masterPage;
        PageHelper.ProcessTemplates((IPageTemplate) pageTemplate, this.Layouts, this.Templates, controlsContainerList, optimized, out masterPage, out includeScriptManager);
        this.MasterPage = masterPage;
      }
      this.ControlContainersOrdered.AddRange((IEnumerable<IControlsContainer>) controlsContainerList);
      if (this.IsPreview || this.IsInlineEditing)
        PageHelper.ProcessControls((IList<ControlBuilder>) this.Controls, (IList<IControlsContainer>) controlsContainerList, pageData.Id);
      else
        PageHelper.ProcessControls((IList<ControlData>) this.DockedControls, (IList<ControlData>) this.PlaceHolders, (IList<IControlsContainer>) controlsContainerList);
      this.Layouts.Reverse();
      this.Themes = (IDictionary<CultureInfo, string>) PageHelper.GetTheme(pageData, pageTemplate);
      this.ExternalPage = pageData.ExternalPage;
      if (string.IsNullOrEmpty(this.MasterPage))
        this.MasterPage = pageData.MasterPage;
      this.IncludeScriptManager = pageData.IncludeScriptManager | includeScriptManager;
      this.LastControlId = pageData.LastControlId;
      this.HtmlTitle = (string) pageData.ParentPage.HtmlTitle;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.PageDraftProxy" /> class.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isPreview">if set to <c>true</c> zones won't be rendered.</param>
    public PageDraftProxy(
      PageDraft pageData,
      PageNode pageNode,
      PageDataProvider provider,
      bool isPreview,
      CultureInfo objectCulture,
      bool isInlineEditing = false)
      : this(pageData, pageNode, provider, isPreview, objectCulture, false, isInlineEditing)
    {
    }

    public override void CreateChildControls(System.Web.UI.Page page)
    {
      this.CanUnlock = this.PageNode.IsGranted("Pages", "Unlock");
      base.CreateChildControls(page);
      if (this.Toolbar == null)
        return;
      string statusKey = (string) null;
      string statusText = (string) null;
      PageData pageData = this.pageNode.GetPageData();
      LifecycleExtensions.GetLifecycleStatus((ILifecycleDataItemLive) pageData, this.CurrentObjectCulture, ref statusKey, ref statusText);
      this.Toolbar.PageStatusText = statusText;
      this.Toolbar.Status = statusKey;
      this.Toolbar.PreviewPublishedUrl = VirtualPathUtility.ToAbsolute(this.pageNode.GetFullUrl());
      this.Toolbar.ItemVisible = pageData.Visible;
      this.Toolbar.ItemVersion = pageData.Version;
      this.Toolbar.WorkflowItemState = (string) this.PageNode.ApprovalWorkflowState;
    }

    /// <summary>Gets the page template.</summary>
    /// <returns></returns>
    /// <value>The page template.</value>
    public override ITemplate GetPageTemplate() => base.GetPageTemplate() ?? ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);

    /// <summary>
    /// Gets or sets a URL for loading external page from the file system.
    /// </summary>
    /// <value>The URL of the external page.</value>
    public new string ExternalPage { get; private set; }
  }
}
