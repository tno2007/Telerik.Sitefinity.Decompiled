// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.UI.TemplatePagesDialog
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages.Web.UI
{
  /// <summary>A dialog listing all pages using a given template.</summary>
  public class TemplatePagesDialog : AjaxDialogBase
  {
    private PageManager pageManager;
    private string rootTaxon;
    protected internal Guid selectedSiteId;
    protected internal IEnumerable<NavigationNode> navigationNodes;
    private LinkButton selectedSiteLink;
    private PageTemplate template;
    /// <summary>
    /// Gets the name of resource file representing the dialog.
    /// </summary>
    public static readonly string DialogTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Pages.TemplatePagesDialog.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Web.UI.TemplatePagesDialog" /> class.
    /// </summary>
    public TemplatePagesDialog() => this.LayoutTemplatePath = TemplatePagesDialog.DialogTemplatePath;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the page provider.</summary>
    /// <value>The page provider.</value>
    public virtual string PageProvider
    {
      get => (string) this.ViewState[nameof (PageProvider)] ?? Config.Get<PagesConfig>().DefaultProvider;
      set => this.ViewState[nameof (PageProvider)] = (object) value;
    }

    /// <summary>Gets the page manager.</summary>
    /// <value>The page manager.</value>
    protected PageManager PageManager
    {
      get
      {
        if (this.pageManager == null)
          this.pageManager = PageManager.GetManager(this.PageProvider);
        return this.pageManager;
      }
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Gets the pagesRepeater control.</summary>
    /// <value>The pagesRepeater control.</value>
    protected virtual Repeater PagesRepeater => this.Container.GetControl<Repeater>("pagesRepeater", true);

    /// <summary>Gets the page count literal.</summary>
    protected virtual ITextControl PageCountLit => this.Container.GetControl<ITextControl>("pagesCount", false);

    /// <summary>Gets the sites repeater.</summary>
    protected virtual Repeater MoreSitesRepeater => this.Container.GetControl<Repeater>("moreSitesRepeater", false);

    /// <summary>Gets the sites menu link.</summary>
    protected virtual HyperLink SitesMenuLink => this.Container.GetControl<HyperLink>("sitesMenuLink", false);

    /// <summary>Gets the sites menu.</summary>
    protected virtual HtmlGenericControl SitesMenu => this.Container.GetControl<HtmlGenericControl>("sitesMenu", false);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer dialogContainer)
    {
      string g = this.Page.Request.QueryString["id"];
      if (!string.IsNullOrEmpty(g) && Utility.IsGuid(g))
      {
        this.template = this.PageManager.GetTemplate(new Guid(g));
        if (this.template != null)
          this.navigationNodes = PageTemplateViewModel.GetPagesBasedOnTempalate(this.template);
      }
      IMultisiteContext multisiteContext = SystemManager.CurrentContext.MultisiteContext;
      if (multisiteContext != null && this.MoreSitesRepeater != null)
      {
        this.selectedSiteId = multisiteContext.CurrentSite.Id;
        this.MoreSitesRepeater.ItemCreated += new RepeaterItemEventHandler(this.MoreSitesRepeater_ItemCreated);
        this.MoreSitesRepeater.DataSource = (object) multisiteContext.GetSites().OrderBy<ISite, string>((Func<ISite, string>) (s => s.Name));
        this.MoreSitesRepeater.DataBind();
      }
      else
      {
        this.selectedSiteId = Guid.Empty;
        if (this.SitesMenu == null)
          return;
        this.SitesMenu.Visible = false;
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      string str = this.Page.Request.QueryString["rootTaxon"];
      this.rootTaxon = string.IsNullOrEmpty(str) || !str.Equals("Backend", StringComparison.OrdinalIgnoreCase) ? Config.Get<PagesConfig>().FrontendRootNode : Config.Get<PagesConfig>().BackendRootNode;
      if (this.template == null)
        return;
      int num = this.navigationNodes.Count<NavigationNode>();
      if (this.PageCountLit != null)
        this.PageCountLit.Text = num != 1 ? string.Format(Res.Get<Labels>().ManyPagesUseTemplate, (object) num.ToString()) : string.Format(Res.Get<Labels>().OnePageUsesTemplate, (object) num.ToString());
      this.PagesRepeater.ItemCreated += new RepeaterItemEventHandler(this.PagesRepeater_ItemCreated);
      if (this.selectedSiteId != Guid.Empty)
      {
        Guid selectedSiteRootNodeId = SystemManager.MultisiteContext.GetSiteById(this.selectedSiteId).SiteMapRootNodeId;
        this.PagesRepeater.DataSource = (object) this.navigationNodes.Where<NavigationNode>((Func<NavigationNode, bool>) (n => n.RootNodeId == selectedSiteRootNodeId || n.RootNodeId == SiteInitializer.BackendRootNodeId));
      }
      else
        this.PagesRepeater.DataSource = (object) this.navigationNodes;
      this.PagesRepeater.DataBind();
    }

    protected internal void PagesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem || !(e.Item.DataItem is NavigationNode dataItem))
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

    private void MoreSitesRepeater_ItemCreated(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      LinkButton siteLink = e.Item.FindControl("siteLink") as LinkButton;
      ISite site = (ISite) e.Item.DataItem;
      int num = this.navigationNodes.Count<NavigationNode>((Func<NavigationNode, bool>) (n => n.RootNodeId == site.SiteMapRootNodeId || n.RootNodeId == SiteInitializer.BackendRootNodeId));
      if (num == 0 && site.Id != this.selectedSiteId)
      {
        e.Item.Visible = false;
      }
      else
      {
        siteLink.Text = "<span>" + site.Name + "</span> (" + num.ToString() + ")";
        if (this.selectedSiteId == site.Id)
        {
          siteLink.CssClass = "sfSel";
          this.selectedSiteLink = siteLink;
          this.SitesMenuLink.Text = siteLink.Text;
        }
        siteLink.Click += (EventHandler) ((s, args) =>
        {
          if (this.selectedSiteLink != null)
            this.selectedSiteLink.CssClass = "";
          this.selectedSiteId = site.Id;
          this.SitesMenuLink.Text = siteLink.Text;
          siteLink.CssClass = "sfSel";
          this.selectedSiteLink = siteLink;
        });
      }
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      List<ScriptReference> scriptReferences = new List<ScriptReference>(base.GetScriptReferences());
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = "Telerik.Web.UI",
        Name = "Telerik.Web.UI.Common.Core.js"
      });
      string name = Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.GetName().Name;
      scriptReferences.Add(new ScriptReference()
      {
        Assembly = name,
        Name = "Telerik.Sitefinity.Resources.Scripts.jquery.clickmenu.pack.js"
      });
      return (IEnumerable<ScriptReference>) scriptReferences;
    }
  }
}
