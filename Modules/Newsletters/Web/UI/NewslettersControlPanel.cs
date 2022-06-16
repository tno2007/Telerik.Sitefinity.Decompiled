// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersControlPanel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Backend;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI
{
  /// <summary>Control panel control for the newsletters module.</summary>
  public class NewslettersControlPanel : ControlPanel<Control>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.NewslettersControlPanel" /> class.
    /// </summary>
    public NewslettersControlPanel()
      : base(false)
    {
    }

    /// <summary>Gets the refernece to the back link control.</summary>
    protected virtual SitefinityHyperLink BackLink => this.Container.GetControl<SitefinityHyperLink>("backLink", true);

    public override string Title
    {
      get
      {
        if (base.Title.IsNullOrEmpty())
          base.Title = Res.Get<NewslettersResources>().ABCampaignsHtmlTitle;
        return base.Title;
      }
      set => base.Title = value;
    }

    /// <summary>Gets or sets the view mode.</summary>
    /// <value>The view mode.</value>
    public override string ViewMode
    {
      get => base.ViewMode;
      set
      {
        base.ViewMode = value;
        if (base.ViewMode == typeof (DashboardView).Name)
          this.Title = Res.Get<NewslettersResources>().PageGroupNodeTitle;
        else if (base.ViewMode == typeof (CampaignsView).Name)
          this.Title = Res.Get<NewslettersResources>().Campaigns;
        else if (base.ViewMode == typeof (MailingListsView).Name)
          this.Title = Res.Get<NewslettersResources>().MailingLists;
        else if (base.ViewMode == typeof (TemplatesView).Name)
          this.Title = Res.Get<NewslettersResources>().Templates;
        else if (base.ViewMode == typeof (ReportsView).Name)
        {
          this.Title = Res.Get<NewslettersResources>().Reports;
        }
        else
        {
          int num = base.ViewMode == typeof (SubscribersView).Name ? 1 : 0;
        }
      }
    }

    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      if (!(base.ViewMode == typeof (SubscriberReportView).Name))
        return;
      this.BackLink.Visible = true;
      this.BackLink.Text = Res.Get<NewslettersResources>().BackToSubscribers;
      this.BackLink.NavigateUrl = this.GetPageUrl(NewslettersModule.subscribersPageId);
    }

    /// <summary>Creates the views.</summary>
    protected override void CreateViews()
    {
      this.AddView<DashboardView>();
      this.AddView<MailingListsView>();
      this.AddView<CampaignsView>();
      this.AddView<TemplatesView>();
      this.AddView<ReportsView>();
      this.AddView<SubscribersView>();
      this.AddView<SubscriberReportView>();
    }

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
      NewslettersCommandPanel newslettersCommandPanel = new NewslettersCommandPanel();
      newslettersCommandPanel.ID = "nwslettersCmdPnl";
      newslettersCommandPanel.Host = this;
      newslettersCommandPanel.ControlPanel = (IControlPanel) this;
      list.Add((ICommandPanel) newslettersCommandPanel);
    }

    private string GetPageUrl(Guid pageId)
    {
      SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(pageId, false);
      return siteMapNode == null ? string.Empty : RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash);
    }
  }
}
