// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.IssueGridViewModel
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
  /// ViewModel class for the campaign issues grid of the newsletter module.
  /// </summary>
  [DataContract]
  public class IssueGridViewModel
  {
    private string issueReportUrl;

    /// <summary>Gets or sets the id.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the root campaign id.</summary>
    [DataMember]
    public Guid RootCampaignId { get; set; }

    /// <summary>Gets or sets the name.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the sent issues count.</summary>
    [DataMember]
    public int Sent { get; set; }

    /// <summary>Gets or sets the delivered issues count.</summary>
    [DataMember]
    public int Delivered { get; set; }

    /// <summary>Gets or sets the opened issues count.</summary>
    [DataMember]
    public int Opened { get; set; }

    /// <summary>Gets or sets the clicked issues count.</summary>
    [DataMember]
    public int Clicked { get; set; }

    /// <summary>Gets or sets the date when the issue was sent.</summary>
    [DataMember]
    public DateTime? DateSent { get; set; }

    /// <summary>
    /// Gets or sets the url of the page with the campaign report.
    /// </summary>
    [DataMember]
    public string IssueReportUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.issueReportUrl))
        {
          SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.issueReportsPageId, false);
          if (siteMapNode == null)
            return "#";
          this.issueReportUrl = VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.RootCampaignId + "/" + (object) this.Id);
        }
        return this.issueReportUrl;
      }
      set => this.issueReportUrl = value;
    }

    /// <summary>
    /// Gets or sets the user friendly string representing the issue state.
    /// </summary>
    [DataMember]
    public string IssueStateUX { get; set; }

    /// <summary>
    /// Gets or sets the CSS class that represents the issue state.
    /// </summary>
    [DataMember]
    public string IssueStateClass { get; set; }

    /// <summary>Gets or sets the last modified date of the issue.</summary>
    [DataMember]
    public DateTime LastModified { get; set; }

    /// <summary>Gets or sets the type of the message body.</summary>
    [DataMember]
    public MessageBodyType MessageBodyType { get; set; }

    /// <summary>Gets or sets the message body id.</summary>
    [DataMember]
    public Guid MessageBodyId { get; set; }
  }
}
