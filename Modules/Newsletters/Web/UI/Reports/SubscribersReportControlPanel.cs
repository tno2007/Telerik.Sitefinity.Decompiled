// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportControlPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>Control panel control for the subscribers report.</summary>
  public class SubscribersReportControlPanel : ControlPanel<Control>
  {
    private string backLinkText;
    private string backLinkNavigateUrl;
    private bool isBackLinkVisible;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportControlPanel" /> class.
    /// </summary>
    public SubscribersReportControlPanel()
      : base(false)
    {
    }

    /// <summary>Gets or sets the text of the back link.</summary>
    /// <value>The back link text.</value>
    public string BackLinkText
    {
      get => this.backLinkText;
      set
      {
        if (this.BackLink.Text != value)
          this.BackLink.Text = value;
        this.backLinkText = value;
      }
    }

    /// <summary>Gets or sets the navigate URL of the back link.</summary>
    /// <value>The back link navigate URL.</value>
    public string BackLinkNavigateUrl
    {
      get => this.backLinkNavigateUrl;
      set
      {
        if (this.BackLink.NavigateUrl != value)
          this.BackLink.NavigateUrl = value;
        this.backLinkNavigateUrl = value;
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether back link is visible.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if back link is visible; otherwise, <c>false</c>.
    /// </value>
    public bool IsBackLinkVisible
    {
      get => this.isBackLinkVisible;
      set
      {
        if (this.BackLink.Visible != value)
          this.BackLink.Visible = value;
        this.isBackLinkVisible = value;
      }
    }

    /// <summary>Gets the refernece to the back link control.</summary>
    protected virtual SitefinityHyperLink BackLink => this.Container.GetControl<SitefinityHyperLink>("backLink", true);

    /// <summary>Creates the views.</summary>
    protected override void CreateViews() => this.AddView<SubscribersReportView>();

    /// <summary>
    /// When overridden this method returns a list of custom Command Panels.
    /// </summary>
    /// <param name="viewMode">Specifies the current View Mode</param>
    /// <param name="list">A list of custom command panels.</param>
    protected override void CreateCustomCommandPanels(string viewMode, IList<ICommandPanel> list)
    {
      base.CreateCustomCommandPanels(viewMode, list);
      if (list == null)
        list = (IList<ICommandPanel>) new List<ICommandPanel>();
      SubscribersReportCommandPanel reportCommandPanel = new SubscribersReportCommandPanel();
      reportCommandPanel.ID = "subscribersRprtCmdPnl";
      reportCommandPanel.Host = this;
      reportCommandPanel.ControlPanel = (IControlPanel) this;
      list.Add((ICommandPanel) reportCommandPanel);
    }

    internal string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
