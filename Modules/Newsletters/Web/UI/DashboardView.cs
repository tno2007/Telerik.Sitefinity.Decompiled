// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.DashboardView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>The dashboard view for the newsletters module.</summary>
  public class DashboardView : ViewModeControl<NewslettersControlPanel>
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.DashboardView.ascx");

    /// <summary>Gets the name of the layout template.</summary>
    /// <value>The name of the layout template.</value>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DashboardView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets the reference to the control that displays the total number of subscribers today.
    /// </summary>
    protected virtual ITextControl TotalNumberOfSubscribersToday => this.Container.GetControl<ITextControl>("totalNumberOfSubscribersToday", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of new subscribers today.
    /// </summary>
    protected virtual ITextControl NewSubscribersToday => this.Container.GetControl<ITextControl>("newSubscribersToday", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of new subscribers this week.
    /// </summary>
    protected virtual ITextControl NewSubscribersThisWeek => this.Container.GetControl<ITextControl>("newSubscribersThisWeek", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of new subscribers this month.
    /// </summary>
    protected virtual ITextControl NewSubscribersThisMonth => this.Container.GetControl<ITextControl>("newSubscribersThisMonth", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of unscubscribed people today.
    /// </summary>
    protected virtual ITextControl UnsubscribedToday => this.Container.GetControl<ITextControl>("unsubscribedToday", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of unsubscribed people this week.
    /// </summary>
    protected virtual ITextControl UnsubscribedThisWeek => this.Container.GetControl<ITextControl>("unsubscribedThisWeek", true);

    /// <summary>
    /// Gets the reference to the control that displays the number of unsubscribed people this month.
    /// </summary>
    protected virtual ITextControl UnsubscribedThisMonth => this.Container.GetControl<ITextControl>("unsubscribedThisMonth", true);

    /// <summary>
    /// Gets the reference to the control that holds the url to the campaigns view.
    /// </summary>
    protected virtual HiddenField CampaignsViewUrlHidden => this.Container.GetControl<HiddenField>("campaignsViewUrlHidden", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer) => SystemManager.CurrentHttpContext.Response.Redirect(this.GetPageUrl(NewslettersModule.campaignsPageId));

    private string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
