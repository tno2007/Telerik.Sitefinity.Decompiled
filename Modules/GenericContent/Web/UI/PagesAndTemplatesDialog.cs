// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.PagesAndTemplatesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Web.Services;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>An abstract dialog for listing pages and templates.</summary>
  public abstract class PagesAndTemplatesDialog : AjaxDialogBase
  {
    private string uiCulture;
    private RepeaterItemEventHandler repeaterItemCreatedHandler;
    private ContentPagesDisplayMode displayMode;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.UI.PagesAndTemplatesDialog" /> class.
    /// </summary>
    public PagesAndTemplatesDialog() => this.LayoutTemplatePath = this.GetTemplatePath();

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// <c>UICulture</c> returned as CultureInfo
    /// </summary>
    public CultureInfo UiCultureInfo
    {
      get
      {
        try
        {
          return CultureInfo.GetCultureInfo(this.UICulture);
        }
        catch
        {
          return SystemManager.CurrentContext.Culture;
        }
      }
    }

    /// <summary>Gets or sets the UI culture.</summary>
    /// <value>The UI culture.</value>
    public string UICulture
    {
      get
      {
        if (this.uiCulture == null)
          this.uiCulture = SystemManager.CurrentHttpContext.Request.QueryString["uiCulture"];
        return this.uiCulture;
      }
      set => this.uiCulture = value;
    }

    /// <summary>Gets or sets the close link text.</summary>
    /// <value>The close link text.</value>
    public string CloseLinkText { get; set; }

    /// <summary>
    /// Gets or sets the close link client-side script that executes when a <see cref="T:System.Web.UI.WebControls.LinkButton" />
    /// control's <see cref="E:System.Web.UI.WebControls.LinkButton.Click" /> event is
    /// raised
    /// </summary>
    /// <value>The close link client-side script.</value>
    public string CloseLinkOnClientClick { get; set; }

    /// <summary>Gets or sets the pages count.</summary>
    /// <value>The pages count.</value>
    protected int PagesCount { get; set; }

    /// <summary>Gets or sets the templates count.</summary>
    /// <value>The templates count.</value>
    protected int TemplatesCount { get; set; }

    /// <summary>
    /// Gets a value indicating whether the dialog should display a "successfully updated" message.
    /// </summary>
    protected bool IsSuccessfullyUpdatedDialog
    {
      get
      {
        string str = SystemManager.CurrentHttpContext.Request.QueryString["isSuccessfullyUpdatedDialog"];
        bool result;
        return str != null && bool.TryParse(str, out result) && result;
      }
    }

    /// <summary>Gets or sets the display mode of the dialog.</summary>
    /// <value>The display mode.</value>
    [Obsolete("There is no need to use this property, it will be removed in one of the next releases.")]
    public ContentPagesDisplayMode DisplayMode
    {
      get
      {
        if (this.displayMode == ContentPagesDisplayMode.None)
        {
          string str = SystemManager.CurrentHttpContext.Request.QueryString["displayMode"];
          this.displayMode = str.IsNullOrEmpty() ? ContentPagesDisplayMode.AllPages : (ContentPagesDisplayMode) Enum.Parse(typeof (ContentPagesDisplayMode), str);
        }
        return this.displayMode;
      }
      set => this.displayMode = value;
    }

    /// <summary>
    /// Returns true if there is a querystring parameter isInZoneEditor=true
    /// </summary>
    [Obsolete("There is no need to use this property, it will be removed in one of the next releases.")]
    protected bool IsInZoneEditor
    {
      get
      {
        string str = SystemManager.CurrentHttpContext.Request.QueryString["isInZoneEditor"];
        bool result;
        return str != null && bool.TryParse(str, out result) && result;
      }
    }

    /// <summary>Gets the pagesCount control.</summary>
    /// <value>The pagesCount control.</value>
    protected virtual Literal PagesCountLiteral => this.Container.GetControl<Literal>("pagesCount", true);

    /// <summary>Gets the description label.</summary>
    protected virtual SitefinityLabel DescriptionLabel => this.Container.GetControl<SitefinityLabel>("description", true);

    /// <summary>Gets the pagesRepeater control.</summary>
    /// <value>The pagesRepeater control.</value>
    protected virtual Repeater PagesRepeater => this.Container.GetControl<Repeater>("pagesRepeater", true);

    /// <summary>Gets the templatesRepeater control.</summary>
    /// <value>The templatesRepeater control.</value>
    protected virtual Repeater TemplatesRepeater => this.Container.GetControl<Repeater>("templatesRepeater", true);

    /// <summary>Gets the closeLink button.</summary>
    /// <value>The closeLink button.</value>
    protected virtual LinkButton CloseLink => this.Container.GetControl<LinkButton>("closeLink", true);

    /// <summary>Gets the close literal.</summary>
    /// <value>The close literal.</value>
    protected virtual Literal CloseLiteral => this.Container.GetControl<Literal>("closeLiteral", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      if (this.IsSuccessfullyUpdatedDialog)
      {
        this.CloseLinkText = Res.Get<Labels>().Ok;
        this.CloseLinkOnClientClick = "dialogBase.close('saveEditorChanges');return false;";
      }
      else
      {
        this.CloseLinkText = Res.Get<Labels>().Close;
        this.CloseLinkOnClientClick = "dialogBase.close();return false;";
      }
      this.CloseLiteral.Text = this.CloseLinkText;
      this.CloseLink.OnClientClick = this.CloseLinkOnClientClick;
      IList<NavigationNode> navNodes = this.GetNavNodes();
      this.PagesCount = navNodes.Count;
      if (this.PagesCount > 0)
      {
        this.repeaterItemCreatedHandler = new RepeaterItemEventHandler(this.PagesRepeater_ItemCreated);
        this.PagesRepeater.ItemCreated += this.repeaterItemCreatedHandler;
        this.PagesRepeater.DataSource = (object) navNodes;
        this.PagesRepeater.DataBind();
      }
      else
        this.PagesRepeater.Visible = false;
      IEnumerable<PageTemplate> templates = this.GetTemplates();
      this.TemplatesCount = templates.Count<PageTemplate>();
      if (this.TemplatesCount > 0)
      {
        this.repeaterItemCreatedHandler = new RepeaterItemEventHandler(this.TemplatesRepeater_ItemCreated);
        this.TemplatesRepeater.ItemCreated += this.repeaterItemCreatedHandler;
        this.TemplatesRepeater.DataSource = (object) templates;
        this.TemplatesRepeater.DataBind();
      }
      else
        this.TemplatesRepeater.Visible = false;
      this.SetPagesCountLiteral();
    }

    internal virtual IList<NavigationNode> GetNavNodes()
    {
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      CultureInfo culture = multilingual ? this.UiCultureInfo : (CultureInfo) null;
      IEnumerable<PageData> pageDataList = this.GetPageDataList();
      List<NavigationNode> navNodes = new List<NavigationNode>();
      foreach (PageData pageData in pageDataList)
      {
        if (multilingual && !pageData.Culture.IsNullOrEmpty())
          navNodes.Add(PageTemplateViewModel.GetNavigationNode(pageData.NavigationNode, pageData.NavigationNode.IsPublished(), CultureInfo.GetCultureInfo(pageData.Culture)));
        else
          navNodes.Add(PageTemplateViewModel.GetNavigationNode(pageData.NavigationNode, pageData.NavigationNode.IsPublished(), culture));
      }
      return (IList<NavigationNode>) navNodes;
    }

    private static CultureInfo GetCultureForNode(
      PageNode node,
      CultureInfo dialogCulture)
    {
      return ((IEnumerable<CultureInfo>) node.AvailableCultures).Contains<CultureInfo>(dialogCulture) ? dialogCulture : ((IEnumerable<CultureInfo>) node.AvailableCultures).FirstOrDefault<CultureInfo>();
    }

    protected override void OnUnload(EventArgs e)
    {
      base.OnUnload(e);
      if (this.PagesRepeater != null)
      {
        this.PagesRepeater.ItemCreated -= this.repeaterItemCreatedHandler;
        this.repeaterItemCreatedHandler = (RepeaterItemEventHandler) null;
      }
      if (this.TemplatesRepeater == null)
        return;
      this.TemplatesRepeater.ItemCreated -= this.repeaterItemCreatedHandler;
      this.repeaterItemCreatedHandler = (RepeaterItemEventHandler) null;
    }

    protected abstract string GetTemplatePath();

    [Obsolete("Use the GetPageDataList method instead")]
    protected abstract IEnumerable<PageNode> GetPages();

    protected abstract IEnumerable<PageTemplate> GetTemplates();

    protected abstract IEnumerable<PageData> GetPageDataList();

    private void PagesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is NavigationNode) || !(e.Item.DataItem is NavigationNode dataItem))
        return;
      if (e.Item.FindControl("pageLink") is HyperLink control1)
      {
        control1.Text = dataItem.Text;
        control1.ToolTip = dataItem.ToolTip;
        control1.NavigateUrl = dataItem.NaviagtionUrl;
      }
      if (e.Item.FindControl("statusTextLiteral") is HtmlGenericControl control2)
        control2.InnerText = dataItem.StatusText;
      if (!(e.Item.FindControl("itemContainer") is HtmlGenericControl control3))
        return;
      control3.Attributes.Add("class", "sfItemTitle sf" + dataItem.Status.ToLower());
    }

    private void TemplatesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is PageTemplate))
        return;
      PageTemplate dataItem = (PageTemplate) e.Item.DataItem;
      if (dataItem == null)
        return;
      HtmlControl control1 = (HtmlControl) e.Item.FindControl("templItem");
      System.Web.UI.WebControls.Image control2 = (System.Web.UI.WebControls.Image) e.Item.FindControl("screenshot");
      if (control2 != null)
        control2.ImageUrl = dataItem.GetSmallThumbnailUrl((Telerik.Sitefinity.Libraries.Model.Image) null, this.Page);
      if (e.Item.FindControl("templatesStatus") is SitefinityLabel control3)
      {
        string statusText = (string) null;
        string statusKey = (string) null;
        LifecycleExtensions.GetOverallStatus((ILifecycleDataItemLive) dataItem, ((CultureInfo) null).GetSitefinityCulture(), ref statusKey, ref statusText);
        control3.Text = statusText;
        control3.CssClass = "sf" + statusKey.ToLower();
      }
      if (!(e.Item.FindControl("templatePreviewLink") is HyperLink control4))
        return;
      CultureInfo culture = !SystemManager.CurrentContext.AppSettings.Multilingual ? (CultureInfo) null : (((IEnumerable<CultureInfo>) dataItem.AvailableCultures).Contains<CultureInfo>(this.UiCultureInfo) ? this.UiCultureInfo : ((IEnumerable<CultureInfo>) dataItem.AvailableCultures).FirstOrDefault<CultureInfo>());
      if (culture != null)
      {
        control4.Text = dataItem.Title[culture];
        control4.NavigateUrl = RouteHelper.ResolveUrl("/Sitefinity/Template/{0}/Preview/{1}".Arrange((object) dataItem.Id, (object) culture.Name), UrlResolveOptions.Rooted);
      }
      else
      {
        control4.Text = (string) dataItem.Title;
        control4.NavigateUrl = RouteHelper.ResolveUrl("/Sitefinity/Template/{0}/Preview".Arrange((object) dataItem.Id), UrlResolveOptions.Rooted);
      }
    }

    protected virtual void SetPagesCountLiteral()
    {
      string statisticsText = ContentViewModel.GetStatisticsText(this.PagesCount, this.TemplatesCount, " " + Res.Get<Labels>().And + " ");
      if (!this.IsSuccessfullyUpdatedDialog)
      {
        this.PagesCountLiteral.Text = this.PagesCount + this.TemplatesCount != 1 ? statisticsText + Res.Get<ContentResources>().UseThisGroupMultiple : statisticsText + Res.Get<ContentResources>().UseThisGroupSingle;
      }
      else
      {
        this.PagesCountLiteral.Text = Res.Get<ContentResources>().SuccessfullyUpdatedContent.Arrange((object) statisticsText);
        this.DescriptionLabel.Visible = false;
      }
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference()
      {
        Assembly = "Telerik.Web.UI",
        Name = "Telerik.Web.UI.Common.Core.js"
      }
    };
  }
}
