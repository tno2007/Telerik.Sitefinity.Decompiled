// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.SettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [Obsolete("Use the EmailSettingsService instead")]
  public class SettingsService : ISettingsService
  {
    private readonly string testMessageSubject = Res.Get<NewslettersResources>().TestMessageSubject;
    private readonly string testMessageBody = Res.Get<NewslettersResources>().TestMessageBody;

    public string TestSmtpServer(SmtpSettingsViewModel settings)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      string notificationsProfileName = settings.NotificationsSmtpProfile;
      INotificationService notificationService = SystemManager.GetNotificationService();
      ISenderProfile senderProfile = notificationService.GetSenderProfiles((QueryParameters) null).First<ISenderProfile>((Func<ISenderProfile, bool>) (prof => prof.ProfileName == notificationsProfileName));
      if (string.IsNullOrWhiteSpace(settings.TestEmailAddress))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "A test email address is required", (Exception) null);
      if (senderProfile == null)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("A sender profile with the name \"{0}\" must be defined in Notifications settings", (object) notificationsProfileName), (Exception) null);
      if (senderProfile.ProfileType == "smtp")
      {
        if (string.IsNullOrWhiteSpace(senderProfile.CustomProperties["host"]))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("Smtp host property must be set in the \"{0}\" Notifications sender profile.", (object) notificationsProfileName), (Exception) null);
        int num = !string.IsNullOrWhiteSpace(senderProfile.CustomProperties["port"]) ? int.Parse(senderProfile.CustomProperties["port"]) : throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("Smtp port property must be set in the \"{0}\" Notifications sender profile.", (object) notificationsProfileName), (Exception) null);
        if (num < 0 || num > (int) ushort.MaxValue)
          throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("Smtp port property in the \"{0}\" Notifications sender profile must be a value between 0 and 65535 (generally, it is 25).", (object) notificationsProfileName), (Exception) null);
        if (bool.Parse(senderProfile.CustomProperties["useAuthentication"]))
        {
          if (string.IsNullOrWhiteSpace(senderProfile.CustomProperties["username"]))
            throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("When using SMTP authentication in the \"{0}\" Notifications sender profile it is required to specify the smtp username property.", (object) notificationsProfileName), (Exception) null);
          if (string.IsNullOrWhiteSpace(senderProfile.CustomProperties["password"]))
            throw new WebProtocolException(HttpStatusCode.InternalServerError, string.Format("When using SMTP authentication in the \"{0}\" Notifications sender profile it is required to specify the smtp password property.", (object) notificationsProfileName), (Exception) null);
        }
      }
      try
      {
        notificationService.SendMessage(NewslettersModule.GetServiceContext(), (IMessageJobRequest) new MessageJobRequestProxy()
        {
          Description = string.Format("Sending test message from Newsletters basic settings"),
          MessageTemplate = (IMessageTemplateRequest) new MessageTemplateRequestProxy()
          {
            BodyHtml = this.testMessageBody,
            PlainTextVersion = this.testMessageBody,
            Subject = this.testMessageSubject
          },
          Subscribers = (IEnumerable<ISubscriberRequest>) new List<SubscriberRequestProxy>()
          {
            new SubscriberRequestProxy()
            {
              Email = settings.TestEmailAddress,
              ResolveKey = Guid.NewGuid().ToString()
            }
          },
          SenderEmailAddress = (string.IsNullOrWhiteSpace(senderProfile.CustomProperties["defaultSenderEmailAddress"]) ? settings.TestEmailAddress : senderProfile.CustomProperties["defaultSenderEmailAddress"]),
          SenderProfileName = notificationsProfileName,
          ClearSubscriptionData = true
        }, (IDictionary<string, string>) null);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return "Your message has been successfully sent. Check the inbox for the test message.";
    }

    public string TestPop3Server(Pop3SettingsViewModel settings)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (string.IsNullOrEmpty(settings.Host))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "POP3 host property must be set.", (Exception) null);
      if (settings.Port < 0)
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "POP3 protocol property must be a positive number.", (Exception) null);
      if (string.IsNullOrEmpty(settings.Username))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Username for the POP3 server must be set.", (Exception) null);
      if (string.IsNullOrEmpty(settings.Password))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "Password for the POP3 server must be set.", (Exception) null);
      try
      {
        using (TcpClient clientSocket = new TcpClient())
        {
          POPClient popClient = new POPClient();
          if (popClient.Connected)
            popClient.Disconnect();
          popClient.Connect(clientSocket, settings.Host, settings.Port, settings.UseSSL);
          popClient.Authenticate(settings.Username, settings.Password);
        }
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return "The connection to POP3 server was successful. Your POP3 settings are correct.";
    }
  }
}
