// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.SystemStatsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Newsletters.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Service that provides methods for working with system stats of the newsletter module.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class SystemStatsService : ISystemStatsService
  {
    /// <summary>
    /// Gets the system stats object with the information on usages of various newsletters' module objects.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="provider">The name of the newsletters module provider.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SystemStats" /> object.</returns>
    public SystemStats GetStats(string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetStatsInternal(provider);
    }

    /// <summary>
    /// Gets the system stats object with the information on usages of various newsletters' module objects.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="provider">The name of the newsletters module provider.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel.SystemStats" /> object.</returns>
    public SystemStats GetStatsInXml(string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetStatsInternal(provider);
    }

    private SystemStats GetStatsInternal(string provider)
    {
      NewslettersManager manager = NewslettersManager.GetManager(provider);
      SystemStats statsInternal = new SystemStats();
      statsInternal.ABCampaignCount = manager.GetABCampaigns().Count<ABCampaign>();
      statsInternal.CampaignCount = manager.GetCampaigns().Count<Campaign>();
      statsInternal.MailingListsCount = manager.GetMailingLists().Count<MailingList>();
      statsInternal.MessageTemplatesCount = manager.GetMessageBodies().Where<MessageBody>((Expression<Func<MessageBody, bool>>) (mb => mb.IsTemplate == true)).Count<MessageBody>();
      statsInternal.SubscribersCount = manager.GetSubscribers().Count<Subscriber>();
      return statsInternal;
    }
  }
}
