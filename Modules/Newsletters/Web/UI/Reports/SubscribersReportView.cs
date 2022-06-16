// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports
{
  /// <summary>
  /// The view for exploring the subscribers report (per issue).
  /// </summary>
  internal class SubscribersReportView : ViewModeControl<SubscribersReportControlPanel>
  {
    private Guid issueId;
    private Guid campaignId;
    private static readonly string layoutTemplatePath = "Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Reports.SubscribersReportView.ascx";
    public static readonly string webServiceUrl = "~/Sitefinity/Services/Newsletters/IssueReport.svc/{0}/{1}/";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Web.UI.Reports.SubscribersReportView" /> class.
    /// </summary>
    public SubscribersReportView() => this.LayoutTemplatePath = ControlUtilities.ToVppPath(SubscribersReportView.layoutTemplatePath);

    /// <summary>Gets the campaign id.</summary>
    protected Guid CampaignId
    {
      get
      {
        if (this.campaignId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length == 0)
            throw new InvalidOperationException("It is unclear for which campaign report ought to be generated. The id of the campaign is missing from the url.");
          this.campaignId = urlParameters[0].IsGuid() ? new Guid(urlParameters[0]) : throw new ArgumentException("The url parameter carrying information about the campaign id is not a valid GUID.");
        }
        return this.campaignId;
      }
    }

    /// <summary>Gets the issue id.</summary>
    protected Guid IssueId
    {
      get
      {
        if (this.issueId == Guid.Empty)
        {
          string[] urlParameters = this.GetUrlParameters();
          if (urlParameters.Length < 2)
            throw new InvalidOperationException("It is unclear for which issue report ought to be generated. The id of the issue is missing from the url.");
          this.issueId = urlParameters[1].IsGuid() ? new Guid(urlParameters[1]) : throw new ArgumentException("The url parameter carrying information about the issue id is not a valid GUID.");
        }
        return this.issueId;
      }
    }

    /// <summary>Gets or sets the name of the provider.</summary>
    public string ProviderName { get; private set; }

    /// <summary>
    /// Gets the reference to the hidden field holding the url of the web service for getting subscribers.
    /// </summary>
    protected virtual HiddenField SubscribersServiceUrlHidden => this.Container.GetControl<HiddenField>("subscribersServiceUrlHidden", true);

    /// <summary>
    /// Gets the reference to the hidden field holding the url of the web service for getting subscriber clicks.
    /// </summary>
    protected virtual HiddenField SubscriberClicksServiceUrlHidden => this.Container.GetControl<HiddenField>("subscriberClicksServiceUrlHidden", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="viewContainer">The view container.</param>
    protected override void InitializeControls(Control viewContainer)
    {
      base.InitializeControls(viewContainer);
      string str1 = SystemManager.CurrentHttpContext.Request.Params["providerName"];
      if (str1 != null)
        this.ProviderName = str1;
      Campaign issue = NewslettersManager.GetManager(this.ProviderName).GetIssue(this.IssueId);
      if (issue != null)
      {
        string str2 = HttpUtility.HtmlEncode(issue.Name.Length < 29 ? issue.Name : issue.Name.Substring(0, 26) + "...");
        this.Host.Title = string.Format(Res.Get<NewslettersResources>().SubscribersActivityFor, (object) str2);
        this.Host.IsBackLinkVisible = true;
        this.Host.BackLinkText = string.Format(Res.Get<Labels>().BackTo, (object) str2);
        this.Host.BackLinkNavigateUrl = this.Host.GetPageUrl(NewslettersModule.issueReportsPageId) + "/" + (object) this.CampaignId + "/" + (object) this.IssueId;
      }
      string format = this.Page.ResolveUrl(SubscribersReportView.webServiceUrl);
      this.SubscribersServiceUrlHidden.Value = string.Format(format, (object) "subscribers", (object) this.IssueId);
      this.SubscriberClicksServiceUrlHidden.Value = string.Format(format, (object) "subscriberclicks", (object) this.IssueId);
    }
  }
}
