// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenInvitationsEmailSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ProtectionShield.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access tokens invitation email sender</summary>
  /// <remarks>
  /// Multithreaded Singleton:
  /// <a href="https://msdn.microsoft.com/en-us/library/ff650316.aspx">See more</a>
  /// </remarks>
  internal sealed class AccessTokenInvitationsEmailSender
  {
    private static volatile AccessTokenInvitationsEmailSender instance;
    private static object syncRoot = new object();

    private AccessTokenInvitationsEmailSender()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenInvitationsEmailSender" /> class.
    /// </summary>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenInvitationsEmailSender" /> class.</returns>
    public static AccessTokenInvitationsEmailSender Get()
    {
      if (AccessTokenInvitationsEmailSender.instance == null)
      {
        lock (AccessTokenInvitationsEmailSender.syncRoot)
        {
          if (AccessTokenInvitationsEmailSender.instance == null)
            AccessTokenInvitationsEmailSender.instance = new AccessTokenInvitationsEmailSender();
        }
      }
      return AccessTokenInvitationsEmailSender.instance;
    }

    /// <summary>Send an access token email invitation</summary>
    /// <param name="recipients">The recipients.</param>
    /// <param name="subject">The subject.</param>
    /// <param name="body">The body.</param>
    public void SendInvitation(string recipients, string subject, string body)
    {
      INotificationService notificationService = SystemManager.GetNotificationService();
      string senderProfile = Config.Get<ProtectionShieldConfig>().Notifications.SenderProfile;
      string errorMessage;
      if (notificationService is ISenderProfileVerifiable profileVerifiable && !profileVerifiable.VerifySenderProfile(senderProfile, out errorMessage))
        throw new InvalidOperationException(errorMessage);
      ServiceContext context = new ServiceContext((string) null, (string) null);
      MessageTemplateRequestProxy templateRequestProxy = new MessageTemplateRequestProxy()
      {
        Subject = subject,
        BodyHtml = body
      };
      IEnumerable<SubscriberRequestProxy> subscriberRequestProxies = (IEnumerable<SubscriberRequestProxy>) null;
      if (!recipients.IsNullOrEmpty())
        subscriberRequestProxies = ((IEnumerable<string>) recipients.Split(new char[1]
        {
          ';'
        }, StringSplitOptions.RemoveEmptyEntries)).Select<string, SubscriberRequestProxy>((Func<string, SubscriberRequestProxy>) (x => new SubscriberRequestProxy()
        {
          Email = x
        }));
      MessageJobRequestProxy messageJob = new MessageJobRequestProxy()
      {
        Description = string.Format("Access token email invitation"),
        MessageTemplate = (IMessageTemplateRequest) templateRequestProxy,
        Subscribers = (IEnumerable<ISubscriberRequest>) subscriberRequestProxies,
        ClearSubscriptionData = true,
        SenderProfileName = senderProfile
      };
      notificationService.SendMessage(context, (IMessageJobRequest) messageJob, (IDictionary<string, string>) null);
    }
  }
}
