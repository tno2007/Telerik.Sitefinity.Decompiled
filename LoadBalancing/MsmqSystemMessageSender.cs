// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.MsmqSystemMessageSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.MsmqIntegration;
using System.Transactions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>A class for sending system messages.</summary>
  public class MsmqSystemMessageSender : ISystemMessageSender
  {
    public MsmqSystemMessageSender() => LicenseLimitations.CanUseLoadBalancing(true);

    /// <summary>Sends the system message.</summary>
    /// <param name="msg">The message.</param>
    public void SendSystemMessage(SystemMessageBase realMessage)
    {
      try
      {
        this.SendMessage(realMessage);
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
    }

    /// <summary>Sends the system messages.</summary>
    /// <param name="realMessages">The system messages to be sent.</param>
    public void SendSystemMessages(SystemMessageBase[] realMessages)
    {
      try
      {
        this.SendMessages(realMessages);
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
      }
    }

    /// <summary>
    /// Starts the web service, that will be called when new MSMQ message arrives.
    /// If the service is already started, no exception is thrown.
    /// </summary>
    public static void StartMsmqReceiver()
    {
      if (!MsmqSystemMessageSender.IsActive)
        return;
      try
      {
        MsmqIntegrationBinding integrationBinding = new MsmqIntegrationBinding(MsmqIntegrationSecurityMode.None);
        ServiceHost serviceHost = new ServiceHost(typeof (MsmqWebService), new Uri[1]
        {
          new Uri("http://localhost/msmq")
        });
        Uri address = new Uri(MsmqSystemMessageSender.MsmqEndpointAddress);
        serviceHost.AddServiceEndpoint(typeof (IMsmqWebService), (Binding) integrationBinding, address);
        serviceHost.Open();
      }
      catch (Exception ex)
      {
      }
    }

    private void SendMessage(SystemMessageBase realMessage)
    {
      SystemMessageBase body = new SystemMessageBase();
      body.Key = realMessage.Key;
      body.MessageData = realMessage.MessageData;
      body.SenderId = Environment.MachineName;
      using (TransactionScope transactionScope = new TransactionScope(TransactionScopeOption.Required))
      {
        new ChannelFactory<IMsmqWebService>((Binding) new MsmqIntegrationBinding(), new EndpointAddress(MsmqSystemMessageSender.MsmqEndpointAddress)).CreateChannel().HandleSystemMessage(new MsmqMessage<SystemMessageBase>(body));
        transactionScope.Complete();
      }
    }

    private void SendMessages(SystemMessageBase[] realMessages)
    {
      int length = realMessages.Length;
      SystemMessageBase[] systemMessageBaseArray = new SystemMessageBase[length];
      for (int index = 0; index < length; ++index)
      {
        SystemMessageBase realMessage1 = realMessages[index];
        SystemMessageBase realMessage2 = new SystemMessageBase()
        {
          Key = realMessage1.Key,
          MessageData = realMessage1.MessageData
        };
        systemMessageBaseArray[index] = realMessage2;
        this.SendMessage(realMessage2);
      }
    }

    /// <inheritdoc />
    bool ISystemMessageSender.IsActive => MsmqSystemMessageSender.IsActive;

    internal static bool IsActive => !AzureRuntime.IsRunning && !MsmqSystemMessageSender.MsmqEndpointAddress.IsNullOrEmpty();

    private static string MsmqEndpointAddress => Config.Get<SystemConfig>().LoadBalancingConfig.MsmqSettings.EndpointAddress;
  }
}
