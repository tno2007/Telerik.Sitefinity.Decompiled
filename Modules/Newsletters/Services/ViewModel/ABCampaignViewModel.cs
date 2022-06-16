// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.ABCampaignViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.Web;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel
{
  /// <summary>View model class for the A/B campaigns.</summary>
  [DataContract]
  public class ABCampaignViewModel
  {
    private string abCampaignReportUrl;

    /// <summary>Gets or sets the id of the A/B campaign.</summary>
    [DataMember]
    public Guid Id { get; set; }

    /// <summary>Gets or sets the name of the test.</summary>
    [DataMember]
    public string Name { get; set; }

    /// <summary>Gets or sets the id of the campaign A.</summary>
    [DataMember]
    public Guid CampaignAId { get; set; }

    /// <summary>Gets or sets the name of the campaign A.</summary>
    [DataMember]
    public string CampaignAName { get; set; }

    /// <summary>Gets or sets the campaign A message subject.</summary>
    [DataMember]
    public string CampaignAMessageSubject { get; set; }

    /// <summary>Gets or sets the campaign A from name.</summary>
    [DataMember]
    public string CampaignAFromName { get; set; }

    /// <summary>Gets or sets the campaign A reply to email.</summary>
    /// <value>The campaign A reply to email.</value>
    [DataMember]
    public string CampaignAReplyToEmail { get; set; }

    /// <summary>Gets or sets the campaign A list.</summary>
    [DataMember]
    public string CampaignAList { get; set; }

    /// <summary>Gets or sets the campaign A message body type.</summary>
    [DataMember]
    public MessageBodyType CampaignAMessageBodyType { get; set; }

    /// <summary>Gets or sets the id of the campaign B.</summary>
    [DataMember]
    public Guid CampaignBId { get; set; }

    /// <summary>Gets or sets the name of the campaign B.</summary>
    [DataMember]
    public string CampaignBName { get; set; }

    /// <summary>Gets or sets the campaign B message subject.</summary>
    [DataMember]
    public string CampaignBMessageSubject { get; set; }

    /// <summary>Gets or sets the campaign B from name.</summary>
    [DataMember]
    public string CampaignBFromName { get; set; }

    /// <summary>Gets or sets the campaign B reply to email.</summary>
    [DataMember]
    public string CampaignBReplyToEmail { get; set; }

    /// <summary>Gets or sets the campaign B list.</summary>
    [DataMember]
    public string CampaignBList { get; set; }

    /// <summary>Gets or sets the campaign B message body type.</summary>
    [DataMember]
    public MessageBodyType CampaignBMessageBodyType { get; set; }

    /// <summary>Gets or sets the winning condition.</summary>
    [DataMember]
    public int WinningCondition { get; set; }

    [DataMember]
    public string WinningConditionUX
    {
      get
      {
        switch (this.WinningCondition)
        {
          case 0:
            return Res.Get<NewslettersResources>().MoreOpenedEmails;
          case 1:
            return Res.Get<NewslettersResources>().MoreLinkClicks;
          case 2:
            return Res.Get<NewslettersResources>().LessBounces;
          case 3:
            return Res.Get<NewslettersResources>().ManualWinningDecision;
          default:
            throw new NotSupportedException();
        }
      }
      set
      {
      }
    }

    /// <summary>Gets or sets the testing sample percentage.</summary>
    [DataMember]
    public Decimal TestingSamplePercentage { get; set; }

    /// <summary>Gets or sets when the testing ends.</summary>
    [DataMember]
    public DateTime TestingEnds { get; set; }

    [DataMember]
    public string TestingEndsUX
    {
      get => this.TestingEnds.ToSitefinityUITime().ToString("MM/dd/yyyy hh:mm tt");
      set
      {
      }
    }

    /// <summary>Gets or sets the AB campaign report URL.</summary>
    [DataMember]
    public string ABCampaignReportUrl
    {
      get
      {
        if (string.IsNullOrEmpty(this.abCampaignReportUrl))
        {
          SiteMapNode siteMapNode = BackendSiteMap.FindSiteMapNode(NewslettersModule.abTestReportPageId, false);
          if (siteMapNode == null)
            return "#";
          this.abCampaignReportUrl = VirtualPathUtility.ToAbsolute(RouteHelper.ResolveUrl(siteMapNode.Url, UrlResolveOptions.Rooted | UrlResolveOptions.RemoveTrailingSlash) + "/" + (object) this.Id);
        }
        return this.abCampaignReportUrl;
      }
      set => this.abCampaignReportUrl = value;
    }

    /// <summary>Gets or sets the root campaign id.</summary>
    [DataMember]
    public Guid RootCampaignId { get; set; }

    /// <summary>Gets or sets the testing note.</summary>
    [DataMember]
    public string TestingNote { get; set; }

    /// <summary>Gets or sets the schedule date.</summary>
    [DataMember]
    public DateTime? ScheduleDate { get; set; }

    /// <summary>
    /// Gets or sets the number of subscribers belonging to campaign A mailing list.
    /// </summary>
    [DataMember]
    public string SubscribersCount { get; set; }
  }
}
