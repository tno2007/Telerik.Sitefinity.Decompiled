// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.Reports.ABCampaignReport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Data.Reports
{
  /// <summary>The class that the A/B campaign reports are bound to.</summary>
  [Obsolete("Use IssueStatistics instead.")]
  public class ABCampaignReport : NewslettersReportBase
  {
    private string name;
    private Guid abCampaignId;
    private ABCampaign abCampaign;
    private MailingList mailingList;
    private int mailingListSubscriberCount;
    private double testSample;
    private int testSampleCount;
    private CampaignWinningCondition winningCondition;
    private DateTime endTestingOn;
    private ABTestingStatus testingStatus;
    private int soFarSentMessages;
    private double soFarSentMessagesPercentage;
    private Campaign campaignA;
    private Campaign campaignB;
    private CampaignReport campaignAReport;
    private CampaignReport campaignBReport;

    public ABCampaignReport(string providerName, Guid abCampaignId)
      : base(providerName)
    {
      this.abCampaignId = abCampaignId;
    }

    /// <summary>Gets the name of the AB campaign.</summary>
    public string Name
    {
      get
      {
        if (this.name == null)
          this.name = this.ABCampaign.Name;
        return this.name;
      }
    }

    /// <summary>
    /// Gets or sets the mailing list to which both of the campaigns belong to.
    /// </summary>
    public MailingList MailingList
    {
      get
      {
        if (this.mailingList == null)
          this.mailingList = this.ABCampaign.CampaignA.List;
        return this.mailingList;
      }
    }

    /// <summary>
    /// Gets the total number of subscribers in the mailing list.
    /// </summary>
    public int MailingListSubscriberCount
    {
      get
      {
        if (this.mailingListSubscriberCount == 0 && this.MailingList != null)
        {
          Guid listId = this.MailingList.Id;
          this.mailingListSubscriberCount = this.Manager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (s => s.Lists.Any<MailingList>((Func<MailingList, bool>) (l => l.Id == listId)))).Count<Subscriber>();
        }
        return this.mailingListSubscriberCount;
      }
    }

    /// <summary>Gets or sets the test sample percentage.</summary>
    public double TestSample => this.testSample == 0.0 ? Convert.ToDouble(this.ABCampaign.TestingSamplePercentage) / 100.0 : this.testSample;

    /// <summary>Gets or sets the test sample count.</summary>
    public int TestSampleCount
    {
      get
      {
        int testSampleCount = this.testSampleCount;
        this.testSampleCount = this.MailingListSubscriberCount * this.ABCampaign.TestingSamplePercentage / 100;
        return this.testSampleCount;
      }
    }

    /// <summary>Gets or sets the winning condition.</summary>
    public CampaignWinningCondition WinningCondition => this.ABCampaign.WinningCondition;

    /// <summary>
    /// Gets or sets the date and time when testing should end.
    /// </summary>
    public DateTime EndTestingOn
    {
      get
      {
        if (this.endTestingOn == new DateTime())
          this.endTestingOn = this.ABCampaign.TestingEnds;
        return this.endTestingOn;
      }
    }

    /// <summary>Gets or sets the testing status of the campaign.</summary>
    public ABTestingStatus TestingStatus => this.abCampaign.ABTestingStatus;

    /// <summary>
    /// Gets or sets the number of messages that have been sent so far.
    /// </summary>
    public int SoFarSentMessages
    {
      get
      {
        if (this.soFarSentMessages == 0)
          this.soFarSentMessages = this.CampaignAReport.SuccessfulDeliveries + this.CampaignBReport.SuccessfulDeliveries;
        return this.soFarSentMessages;
      }
    }

    /// <summary>
    /// Gets or sets the percentage of the messages that have been sent so far.
    /// </summary>
    public double SoFarSentMessagesPercentage
    {
      get
      {
        if (this.soFarSentMessagesPercentage == 0.0)
          this.soFarSentMessagesPercentage = this.testSampleCount != 0 ? Convert.ToDouble(this.SoFarSentMessages) / Convert.ToDouble(this.testSampleCount) : 0.0;
        return this.soFarSentMessagesPercentage;
      }
    }

    /// <summary>Gets or sets the campaign A of the A/B campaign.</summary>
    public Campaign CampaignA => this.ABCampaign.CampaignA;

    /// <summary>Gets or sets the campaign B of the A/B campaign.</summary>
    public Campaign CampaignB => this.ABCampaign.CampaignB;

    /// <summary>Gets or sets the report for the Campaign A.</summary>
    public CampaignReport CampaignAReport
    {
      get
      {
        if (this.campaignAReport == null)
          this.campaignAReport = new CampaignReport(string.Empty, this.CampaignA.Id);
        return this.campaignAReport;
      }
    }

    /// <summary>Gets or sets the report for the campaign B.</summary>
    public CampaignReport CampaignBReport
    {
      get
      {
        if (this.campaignBReport == null)
          this.campaignBReport = new CampaignReport(string.Empty, this.CampaignB.Id);
        return this.campaignBReport;
      }
    }

    protected virtual ABCampaign ABCampaign
    {
      get
      {
        if (this.abCampaign == null)
          this.abCampaign = this.Manager.GetABCampaign(this.abCampaignId);
        return this.abCampaign;
      }
    }
  }
}
