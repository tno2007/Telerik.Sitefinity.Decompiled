// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.MainMenuPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UrlEvaluation;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>
  /// Represents the panel containing the main menu in the backend area.
  /// </summary>
  public class MainMenuPanel : SimpleView
  {
    private AnimationType expandAnimationType;
    private AnimationType collapseAnimationType;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.MainMenuPanel.ascx");

    /// <summary>
    /// Gets the name of the embedded layout template. If the control uses layout template this
    /// property must be overridden to provide the path (key) to an embedded resource file.
    /// </summary>
    /// <value></value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MainMenuPanel.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the MainMenu expand animation type.</summary>
    /// <value>The MainMenu expand animation type.</value>
    public AnimationType ExpandAnimationType
    {
      get => this.expandAnimationType;
      set => this.expandAnimationType = value;
    }

    /// <summary>Gets or sets the MainMenu collapse animation type.</summary>
    /// <value>The MainMenu collapse animation type.</value>
    public AnimationType CollapseAnimationType
    {
      get => this.collapseAnimationType;
      set => this.collapseAnimationType = value;
    }

    /// <summary>Gets the main menu control.</summary>
    /// <value>The main menu control.</value>
    public virtual MainMenu MainMenu => this.Container.GetControl<MainMenu>(nameof (MainMenu), true);

    /// <summary>Gets the site selector control.</summary>
    /// <value>The site selector control.</value>
    public virtual SiteSelector SiteSelector => this.Container.GetControl<SiteSelector>("siteSelector", true);

    /// <summary>Gets the back to site link.</summary>
    /// <value>The back to site link.</value>
    public virtual HyperLink BackToLink => this.Container.GetControl<HyperLink>("backToLink", true);

    /// <summary>
    /// Overridden. Cancels the rendering of a beginning HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Overridden. Cancels the rendering of an ending HTML tag for the control.
    /// </summary>
    /// <param name="writer">The HtmlTextWriter object used to render the markup.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="dialogContainer"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ShouldShowBacklink())
      {
        this.MainMenu.Visible = false;
        this.SiteSelector.Visible = false;
        this.BackToLink.Visible = true;
        QueryStringBuilder collection = new QueryStringBuilder();
        SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(Config.Get<PagesConfig>().BackendHomePageId, false);
        string str = RouteHelper.ResolveUrl(siteMapNode != null ? siteMapNode.Url : "~/Sitefinity", UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
        ISite currentSite = SystemManager.CurrentContext.CurrentSite;
        collection.Remove("sf_site");
        collection.Add("sf_site", currentSite.Id.ToString());
        this.BackToLink.NavigateUrl = str + collection.ToQueryString();
        this.BackToLink.Text = string.Format(Res.Get<Labels>().BackToSite, (object) HttpUtility.HtmlEncode(currentSite.Name));
      }
      else
      {
        this.MainMenu.ExpandAnimation.Type = this.ExpandAnimationType;
        this.MainMenu.CollapseAnimation.Type = this.CollapseAnimationType;
        this.SiteSelector.Visible = SystemManager.CurrentContext.MultisiteContext != null;
        if (SystemManager.CurrentContext.IsOneSiteMode)
          return;
        this.SiteSelector.SiteMenuHeading = Res.Get<MultisiteResources>().Sites;
      }
    }

    /// <summary>
    /// Gets a value indicating whether main menu should be hidden.
    /// </summary>
    private bool ShouldShowBacklink()
    {
      Guid result;
      Guid.TryParse(SiteMapBase.GetCurrentNode()?.Key, out result);
      return (SystemManager.CurrentContext.GetSites().Count<ISite>() != 1 || !(result != MultisiteModule.HomePageId) || !(result != MultisiteModule.SiteSettingsPageId)) && SystemManager.CurrentContext.IsGlobalContext;
    }
  }
}
