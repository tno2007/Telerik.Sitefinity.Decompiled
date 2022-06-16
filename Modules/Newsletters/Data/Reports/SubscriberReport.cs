// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Data.Reports.SubscriberReport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters.Data.Reports
{
  /// <summary>The class that the subscriber reports are bound to.</summary>
  public class SubscriberReport : NewslettersReportBase
  {
    private IList<ClickedLinkSubscriberRecord> linksClicked;
    private Guid subscriberId;

    public SubscriberReport(string providerName, Guid subscriberId)
      : base(providerName)
    {
      this.subscriberId = subscriberId;
    }

    /// <summary>Gets a query of links that subscriber has clicked on.</summary>
    public IList<ClickedLinkSubscriberRecord> LinksClicked
    {
      get
      {
        if (this.linksClicked == null)
          this.linksClicked = this.GetClickedLinks();
        return this.linksClicked;
      }
    }

    private IList<ClickedLinkSubscriberRecord> GetClickedLinks()
    {
      List<LinkClickStat> list = this.Manager.GetLinkClickStats().Where<LinkClickStat>((Expression<Func<LinkClickStat, bool>>) (cs => cs.SubscriberId == this.subscriberId)).ToList<LinkClickStat>();
      List<ClickedLinkSubscriberRecord> clickedLinks = new List<ClickedLinkSubscriberRecord>();
      for (int index = 0; index < list.Count; ++index)
      {
        Guid issueId = list[index].CampaignId;
        string str = this.Manager.GetIssues().Where<Campaign>((Expression<Func<Campaign, bool>>) (issue => issue.Id == issueId)).Select<Campaign, string>((Expression<Func<Campaign, string>>) (issue => issue.Name)).SingleOrDefault<string>() ?? "- issue does not exist anymore -";
        ClickedLinkSubscriberRecord subscriberRecord = new ClickedLinkSubscriberRecord()
        {
          CampaignName = str,
          DateClicked = list[index].DateTimeClicked.ToSitefinityUITime(),
          Url = list[index].Url
        };
        clickedLinks.Add(subscriberRecord);
      }
      return (IList<ClickedLinkSubscriberRecord>) clickedLinks;
    }
  }
}
