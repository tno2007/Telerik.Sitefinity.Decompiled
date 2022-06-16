// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignGridViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// ViewModel class for the campaign type of the newsletter module. It's used in the campaigns grid.
  /// </summary>
  [DataContract]
  public class CampaignGridViewModel
  {
    private string campaignReportUrl;

    /// <summary>Gets or sets the id of the campaign.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the campaign.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets the user friendly name for the campaign type.</summary>
    [DataMember]
    public string CampaignTypeUX { get; set; }

    /// <summary>Gets or sets the state of the campaign.</summary>
    [DataMember]
    public CampaignState CampaignState { get; set; }

    /// <summary>
    /// Gets or sets the user friendly string representing the campaign state.
    /// </summary>
    [DataMember]
    public string CampaignStateUX { get; set; }

    /// <summary>
    /// Gets or sets the CSS class that represents the campaign state.
    /// </summary>
    [DataMember]
    public string CampaignStateClass { get; set; }

    /// <summary>
    /// Gets or sets the url of the page with the campaign report.
    /// </summary>
    [DataMember]
    public string CampaignReportUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.campaignReportUrl))
        {
          SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.campaignOverviewPageId, false);
          if (siteMapNode == null)
            return "#";
          this.campaignReportUrl = VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Id);
        }
        return this.campaignReportUrl;
      }
      set => this.campaignReportUrl = value;
    }

    /// <summary>Gets the unique identity of the message body.</summary>
    [DataMember]
    public Guid MessageBodyId { get; set; }

    /// <summary>Gets or sets the type of the message body.</summary>
    [DataMember]
    public MessageBodyType MessageBodyType { get; set; }

    /// <summary>Gets or sets the subscriber count of the list.</summary>
    [DataMember]
    public int ListSubscriberCount { get; set; }

    /// <summary>
    /// Gets or sets a value indicating the top action of the grid action menu.
    /// </summary>
    /// <value>"createIssue", "editIssue", "editAbTest".</value>
    [DataMember]
    [Obsolete("You can only create issues from the grid.")]
    public string GridActionMode { get; set; }

    /// <summary>Gets or sets the latest issue id.</summary>
    /// <value>The latest issue id.</value>
    [DataMember]
    [Obsolete("The grid does not care about the latest issue.")]
    public Guid LatestIssueId { get; set; }

    /// <summary>Gets or sets the latest issue message body id.</summary>
    /// <value>The latest issue message body id.</value>
    [DataMember]
    [Obsolete("The grid does not care about the latest issue.")]
    public Guid LatestIssueMessageBodyId { get; set; }

    /// <summary>Gets or sets the latest ab test id.</summary>
    [DataMember]
    [Obsolete("The grid does not care about the latest A/B test.")]
    public Guid LatestAbTestId { get; set; }
  }
}
