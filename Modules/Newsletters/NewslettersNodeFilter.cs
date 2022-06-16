// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.NewslettersNodeFilter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  internal class NewslettersNodeFilter : ISitemapNodeFilter
  {
    private static readonly Guid[] NewsletterPageIds = new Guid[13]
    {
      NewslettersModule.newslettersNodeId,
      NewslettersModule.landingPageId,
      NewslettersModule.campaignsPageId,
      NewslettersModule.mailingListsPageId,
      NewslettersModule.subscribersPageId,
      NewslettersModule.templatesPageId,
      NewslettersModule.reportsPageId,
      NewslettersModule.campaignOverviewPageId,
      NewslettersModule.issueReportsPageId,
      NewslettersModule.subscriberReportPageId,
      NewslettersModule.standardCampaignRootNodeId,
      NewslettersModule.subscribersReportPageId,
      NewslettersModule.abTestReportPageId
    };

    public bool IsNodeAccessPrevented(PageSiteNode pageNode)
    {
      if (!pageNode.IsBackend)
        return false;
      bool flag = false;
      if (((IEnumerable<Guid>) NewslettersNodeFilter.NewsletterPageIds).Contains<Guid>(pageNode.Id))
        flag = !AppPermission.IsGranted(AppAction.ManageNewsletters);
      return flag;
    }
  }
}
