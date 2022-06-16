// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.CampaignViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>
  /// ViewModel class for the campaign type of the newsletter module.
  /// </summary>
  [DataContract]
  public class CampaignViewModel
  {
    private IList<MergeTagViewModel> mergeTags;
    private MessageBodyViewModel messageBody;
    private string campaignReportUrl;

    /// <summary>Gets or sets the id of the campaign.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the id of the list.</summary>
    [DataMember]
    public Guid ListId { get; set; }

    /// <summary>Gets or sets the title of the list.</summary>
    [DataMember]
    public string ListTitle { get; set; }

    /// <summary>Gets or sets the subscriber count of the list.</summary>
    [DataMember]
    public int ListSubscriberCount { get; set; }

    /// <summary>Gets or sets the name of the campaign.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the subject of the message.</summary>
    [DataMember]
    public string MessageSubject { get; set; }

    /// <summary>
    /// Gets or sets the name or the person or organization sending the message.
    /// </summary>
    /// <remarks>This name will appear in the message being sent.</remarks>
    [DataMember]
    public string FromName { get; set; }

    /// <summary>
    /// Gets or sets the email address to which the recipients may reply to.
    /// </summary>
    /// <remarks>
    /// This email address will appear in the message being sent.
    /// </remarks>
    [DataMember]
    public string ReplyToEmail { get; set; }

    /// <summary>Gets the user friendly name for the campaign type.</summary>
    [DataMember]
    public string CampaignTypeUX { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the campaign is completed.
    /// </summary>
    [DataMember]
    public bool IsReadyForSending { get; set; }

    /// <summary>Gets or sets the message body of the campaign.</summary>
    [DataMember]
    public MessageBodyViewModel MessageBody
    {
      get
      {
        if (this.messageBody == null)
          this.messageBody = new MessageBodyViewModel();
        return this.messageBody;
      }
      set => this.messageBody = value;
    }

    /// <summary>Gets or sets the campaign template id.</summary>
    [DataMember]
    public Guid CampaignTemplateId { get; set; }

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

    /// <summary>
    /// Gets the collection of merge tags available for this campaign.
    /// </summary>
    [DataMember]
    public IList<MergeTagViewModel> MergeTags
    {
      get
      {
        if (this.mergeTags == null)
          this.mergeTags = (IList<MergeTagViewModel>) new List<MergeTagViewModel>();
        return this.mergeTags;
      }
    }

    [DataMember]
    public DateTime DeliveryDate { get; set; }

    [DataMember]
    public bool UseGoogleTracking { get; set; }
  }
}
