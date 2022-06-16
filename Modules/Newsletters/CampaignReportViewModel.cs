// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.CampaignReportViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Newsletters.Model;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// ViewModel class that represents the report of a given campaign.
  /// </summary>
  public class CampaignReportViewModel
  {
    private Campaign campaign;
    private string providerName;
    private NewslettersManager newslettersManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.CampaignReportViewModel" /> class.
    /// </summary>
    /// <param name="campaign">The campaign for which the report is to be generated.</param>
    /// <param name="providerName">Name of the provider.</param>
    public CampaignReportViewModel(Campaign campaign, string providerName)
    {
      this.campaign = campaign;
      this.providerName = providerName;
    }

    public int TotalRecipients => this.NewslettersManager.GetSubscribers().Where<Subscriber>((Expression<Func<Subscriber, bool>>) (sub => sub.Lists.Contains(this.campaign.List))).Count<Subscriber>();

    protected NewslettersManager NewslettersManager
    {
      get
      {
        if (this.newslettersManager == null)
          this.newslettersManager = NewslettersManager.GetManager(this.providerName);
        return this.newslettersManager;
      }
    }
  }
}
