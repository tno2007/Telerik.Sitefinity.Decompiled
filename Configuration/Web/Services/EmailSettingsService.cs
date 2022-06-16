// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.Services.EmailSettingsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration.Web.ViewModels;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Notifications;
using Telerik.Sitefinity.Services.Notifications.Configuration;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.OpenPOP.POP3;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web.Services
{
  /// <summary>
  /// Service implementation for the email settings in the basic settings.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class EmailSettingsService : IEmailSettingsService
  {
    internal const string WebServiceUrl = "Sitefinity/Services/Configuration/EmailSettings.svc";
    private readonly string testMessageSubject = Res.Get<ConfigDescriptions>().TestMessageSubject;
    private readonly string testMessageBody = Res.Get<ConfigDescriptions>().TestMessageBody;

    /// <inheritdoc />
    public string SendTestMail(TestEmailViewModel testEmailViewModel)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (testEmailViewModel == null || string.IsNullOrWhiteSpace(testEmailViewModel.ReceiverEmailAddress))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "A receiver email address is required", (Exception) null);
      INotificationService notificationService1 = SystemManager.GetNotificationService();
      ISenderProfile defaultSenderProfile = notificationService1.GetDefaultSenderProfile((ServiceContext) null, "smtp");
      string errorMessage;
      if (notificationService1 is ISenderProfileVerifiable profileVerifiable && defaultSenderProfile != null && !profileVerifiable.VerifySenderProfile(defaultSenderProfile.ProfileName, out errorMessage))
        throw new WebProtocolException(HttpStatusCode.InternalServerError, errorMessage, (Exception) null);
      ServiceContext serviceContext = !testEmailViewModel.ModuleName.IsNullOrEmpty() ? new ServiceContext("ThisApplicationKey", testEmailViewModel.ModuleName) : new ServiceContext((string) null, (string) null);
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      List<SystemEmailsPlaceholderViewModel> placeholderViewModelList = new List<SystemEmailsPlaceholderViewModel>();
      if (testEmailViewModel.PlaceholderFields != null)
        placeholderViewModelList.AddRange(testEmailViewModel.PlaceholderFields);
      if (testEmailViewModel.DynamicPlaceholderFields != null)
        placeholderViewModelList.AddRange(testEmailViewModel.DynamicPlaceholderFields);
      foreach (SystemEmailsPlaceholderViewModel placeholderViewModel in placeholderViewModelList)
      {
        if (!dictionary.ContainsKey(placeholderViewModel.FieldName))
          dictionary.Add(placeholderViewModel.FieldName, string.Format("[{0}]", (object) placeholderViewModel.DisplayName));
      }
      try
      {
        List<SubscriberRequestProxy> subscriberRequestProxyList = new List<SubscriberRequestProxy>();
        string[] strArray;
        if (testEmailViewModel == null)
        {
          strArray = (string[]) null;
        }
        else
        {
          string receiverEmailAddress = testEmailViewModel.ReceiverEmailAddress;
          if (receiverEmailAddress == null)
            strArray = (string[]) null;
          else
            strArray = receiverEmailAddress.Split(new string[3]
            {
              ";",
              ",",
              " "
            }, StringSplitOptions.RemoveEmptyEntries);
        }
        foreach (string str in strArray)
          subscriberRequestProxyList.Add(new SubscriberRequestProxy()
          {
            Email = str,
            ResolveKey = Guid.NewGuid().ToString()
          });
        INotificationService notificationService2 = notificationService1;
        ServiceContext context = serviceContext;
        MessageJobRequestProxy messageJob = new MessageJobRequestProxy();
        messageJob.Description = string.Format("Sitefinity email settings test message");
        messageJob.MessageTemplate = (IMessageTemplateRequest) new MessageTemplateRequestProxy()
        {
          BodyHtml = (!testEmailViewModel.BodyHtml.IsNullOrEmpty() ? testEmailViewModel.BodyHtml : this.testMessageBody),
          PlainTextVersion = (!testEmailViewModel.BodyHtml.IsNullOrEmpty() ? testEmailViewModel.BodyHtml : this.testMessageBody),
          Subject = (!testEmailViewModel.Subject.IsNullOrEmpty() ? testEmailViewModel.Subject : this.testMessageSubject)
        };
        messageJob.Subscribers = (IEnumerable<ISubscriberRequest>) subscriberRequestProxyList;
        messageJob.SenderEmailAddress = !testEmailViewModel.SenderEmailAddress.IsNullOrEmpty() ? testEmailViewModel.SenderEmailAddress : defaultSenderProfile.CustomProperties["defaultSenderEmailAddress"];
        messageJob.SenderProfileName = defaultSenderProfile.ProfileName;
        messageJob.SenderName = testEmailViewModel.SenderName;
        messageJob.ClearSubscriptionData = true;
        Dictionary<string, string> contextItems = dictionary;
        notificationService2.SendMessage(context, (IMessageJobRequest) messageJob, (IDictionary<string, string>) contextItems);
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex);
      }
      return "Your message has been successfully sent. Check the inbox for the test message.";
    }

    /// <inheritdoc />
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
