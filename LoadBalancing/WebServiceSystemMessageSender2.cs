// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.WebServiceSystemMessageSender2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>A class for sending system messages.</summary>
  internal class WebServiceSystemMessageSender2 : ISystemMessageSender
  {
    private IList<string> serviceEndpoints;
    private static readonly bool isFormsAuthenticaiotn = Config.Get<SecurityConfig>().AuthenticationMode == AuthenticationMode.Forms;
    private static readonly Dictionary<string, SystemMessagesBackgroundService> systemMessagesServices = new Dictionary<string, SystemMessagesBackgroundService>();
    private static readonly Dictionary<string, List<SystemMessageBase>> messageGroups = new Dictionary<string, List<SystemMessageBase>>();
    private static readonly object smsLock = new object();
    private static readonly object messageGroupsLock = new object();

    public WebServiceSystemMessageSender2()
    {
      LicenseLimitations.CanUseLoadBalancing(true);
      ManagerBase<ConfigProvider>.Executed += new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
      SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
    }

    /// <inheritdoc />
    public virtual bool IsActive => !AzureRuntime.IsRunning;

    protected static int RequestTimeout => Config.Get<SystemConfig>().LoadBalancingConfig.RequestTimeout;

    /// <inheritdoc />
    public virtual void SendSystemMessage(SystemMessageBase realMessage)
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

    /// <summary>Sends a system message to all other instances.</summary>
    /// <param name="realMessage">The system message to be sent.</param>
    protected virtual void SendMessage(SystemMessageBase realMessage) => this.SendObject(new List<SystemMessageBase>()
    {
      new SystemMessageBase()
      {
        Key = realMessage.Key,
        MessageData = realMessage.MessageData,
        SenderId = SystemWebService.LocalId,
        AdditionalInfo = realMessage.AdditionalInfo,
        Timestamp = realMessage.Timestamp,
        Connection = realMessage.Connection
      }
    });

    protected virtual void SendMessages(SystemMessageBase[] realMessages)
    {
      int length = realMessages.Length;
      List<SystemMessageBase> messages = new List<SystemMessageBase>(length);
      for (int index = 0; index < length; ++index)
      {
        SystemMessageBase realMessage = realMessages[index];
        SystemMessageBase systemMessageBase = new SystemMessageBase()
        {
          Key = realMessage.Key,
          MessageData = realMessage.MessageData,
          SenderId = SystemWebService.LocalId,
          AdditionalInfo = realMessage.AdditionalInfo,
          Timestamp = realMessage.Timestamp,
          Connection = realMessage.Connection
        };
        messages.Add(systemMessageBase);
      }
      this.SendObject(messages);
    }

    private void SendObject(List<SystemMessageBase> messages)
    {
      foreach (string str in this.GetUrlsToReceiveMessage())
      {
        if (this.CanDetermineLocalAddress() && this.IsLocalIpAddress(str))
          this.TryStopLocalSystemBackgroundService(str);
        else
          this.InvokeWebMethod(str, messages);
      }
    }

    private void InvokeWebMethod(string url, List<SystemMessageBase> messages)
    {
      string hostHeader = this.GetRequestHostHeader();
      WebServiceSystemMessageSender2.WithExceptionHandling(url, hostHeader, (Action) (() =>
      {
        SystemMessagesBackgroundService systemMessagesService = WebServiceSystemMessageSender2.GetSystemMessagesService(url);
        if (!systemMessagesService.IsStopped)
        {
          if (systemMessagesService.TryRunTask((Action<IBackgroundTaskContext>) (executionContext => WebServiceSystemMessageSender2.SendMessageData(url, hostHeader, (IEnumerable<SystemMessageBase>) messages, this))))
            return;
          WebServiceSystemMessageSender2.StoreGroupedMessages(url, hostHeader, messages);
          systemMessagesService.EnqueueTask((Action) (() => WebServiceSystemMessageSender2.SendGroupedMessages(url, hostHeader, this)));
        }
        else
          System.Threading.ThreadPool.QueueUserWorkItem((WaitCallback) (obj => WebServiceSystemMessageSender2.SendMessageData(url, hostHeader, (IEnumerable<SystemMessageBase>) messages, this)));
      }));
    }

    private static void SendMessageData(
      string url,
      string hostHeader,
      IEnumerable<SystemMessageBase> messages,
      WebServiceSystemMessageSender2 wsMessageSender)
    {
      HttpWebRequest request = WebServiceSystemMessageSender2.CreateRequest(url, hostHeader, wsMessageSender);
      byte[] messageBytes = WebServiceSystemMessageSender2.GetMessageBytes(messages);
      if (messageBytes != null)
      {
        request.ContentLength = (long) messageBytes.Length;
        using (Stream requestStream = request.GetRequestStream())
          requestStream.Write(messageBytes, 0, messageBytes.Length);
      }
      WebServiceSystemMessageSender2.WithExceptionHandling(request.RequestUri.AbsoluteUri, request.Host, (Action) (() => request.BeginGetResponse(new AsyncCallback(WebServiceSystemMessageSender2.ResponseCallback), (object) request)));
      if (!SystemMessageDispatcher.TestLoggingEnabled)
        return;
      string messagesInformation = WebServiceSystemMessageSender2.GetDetailedMessagesInformation(messages.ToList<SystemMessageBase>());
      SystemMessageDispatcher.LogTestingMessage(string.Format("{0}, {1}, Messages sent: {2}, Messages info: {3}", (object) DateTime.UtcNow, (object) SystemManager.CurrentHttpContext.Request.RawUrl, (object) messages.Count<SystemMessageBase>(), (object) messagesInformation));
    }

    private static void ResponseCallback(IAsyncResult asyncResult)
    {
      HttpWebRequest request = (HttpWebRequest) asyncResult.AsyncState;
      WebServiceSystemMessageSender2.WithExceptionHandling(request.RequestUri.AbsoluteUri, request.Host, (Action) (() => request.EndGetResponse(asyncResult)));
    }

    private static void WithExceptionHandling(string url, string hostHeader, Action action)
    {
      try
      {
        action();
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(new Exception(string.Format("Error sending system message to URL: {0}; HTTP method: {1}; Host: ", (object) url, (object) "PUT") + (hostHeader == null ? "null" : string.Format("'{0}'", (object) hostHeader)), ex), ExceptionPolicyName.IgnoreExceptions);
      }
    }

    /// <summary>
    /// Returns the absolute URLs of an <see cref="T:Telerik.Sitefinity.LoadBalancing.Web.Services.ISystemWebService" /> endpoint for all Sitefinity instances.
    /// </summary>
    /// <returns>Absolute URLs to be invoked, including host, service endpoint and method name.</returns>
    protected virtual IEnumerable<string> GetUrlsToReceiveMessage() => this.GetServiceEndpoints().Select<string, string>((Func<string, string>) (url => url + "HandleMessages"));

    private IList<string> GetServiceEndpoints()
    {
      if (this.serviceEndpoints == null)
        this.serviceEndpoints = (IList<string>) Config.Get<SystemConfig>().LoadBalancingConfig.URLS.Elements.Select<InstanceUrlConfigElement, string>((Func<InstanceUrlConfigElement, string>) (url => WebServiceSystemMessageSender2.GetServiceEndpoint(url.Value))).ToList<string>();
      return this.serviceEndpoints;
    }

    internal static string GetServiceEndpoint(string instanceUrl)
    {
      string str = instanceUrl.Trim();
      int length = instanceUrl.IndexOf("://");
      if (length == -1)
      {
        str = "http://" + str;
      }
      else
      {
        string lowerInvariant = instanceUrl.Substring(0, length).ToLowerInvariant();
        if (lowerInvariant != "http" && lowerInvariant != "https")
          throw new ArgumentException(string.Format("The {0} URI scheme is not supported by the {1}.", (object) lowerInvariant, (object) typeof (WebServiceSystemMessageSender2).Name));
      }
      return str.TrimEnd('/') + "/Sitefinity/Services/LoadBalancing/SystemWebService.svc/";
    }

    /// <summary>
    /// Indicates whether <paramref name="ip" /> refers to the current instance and should be skipped when dispatching
    /// system messages.
    /// </summary>
    /// <param name="ip">The IP/host address that should be checked.</param>
    /// <returns><c>true</c> for the current instance; <c>false</c> otherwise.</returns>
    protected virtual bool IsLocalIpAddress(string ip) => !SystemWebService.LocalUrl.IsNullOrEmpty() && ip.StartsWith(SystemWebService.LocalUrl);

    /// <summary>
    /// Determines whether this instance can determine its local address.
    /// </summary>
    /// <returns></returns>
    protected virtual bool CanDetermineLocalAddress() => !string.IsNullOrWhiteSpace(SystemWebService.LocalUrl);

    /// <summary>Create and set authentication cookie, if needed.</summary>
    /// <param name="request">The web request that will be sent later.</param>
    protected virtual void Authenticate(HttpWebRequest request)
    {
      if (WebServiceSystemMessageSender2.isFormsAuthenticaiotn)
      {
        HttpCookie authCookie = SecurityManager.CreateAuthCookie("System", "system", SecurityManager.SystemAccountIDs[0], DateTime.UtcNow, false, (SecurityConfig) null);
        request.CookieContainer.Add(new Cookie()
        {
          Domain = request.RequestUri.Host,
          Expires = authCookie.Expires,
          Name = authCookie.Name,
          Path = authCookie.Path,
          Secure = authCookie.Secure,
          Value = authCookie.Value
        });
      }
      else
      {
        string str = SecurityManager.EncryptData(SecurityManager.SystemAccountIDs[0]);
        request.Headers.Add("SF-Sys-Message", str);
      }
    }

    /// <summary>
    /// The "Host" header value of the system message request.
    /// </summary>
    protected virtual string GetRequestHostHeader() => Config.Get<SystemConfig>().LoadBalancingConfig.DisableHostHeaders ? (string) null : SystemManager.Host;

    private void TryStopLocalSystemBackgroundService(string absoluteServiceUrl)
    {
      SystemMessagesBackgroundService systemMessagesService = WebServiceSystemMessageSender2.GetSystemMessagesService(absoluteServiceUrl);
      if (systemMessagesService.IsStopped)
        return;
      systemMessagesService.Stop(false);
    }

    private static SystemMessagesBackgroundService GetSystemMessagesService(
      string key)
    {
      SystemMessagesBackgroundService systemMessagesService = (SystemMessagesBackgroundService) null;
      if (!WebServiceSystemMessageSender2.systemMessagesServices.TryGetValue(key, out systemMessagesService))
      {
        lock (WebServiceSystemMessageSender2.smsLock)
        {
          if (!WebServiceSystemMessageSender2.systemMessagesServices.TryGetValue(key, out systemMessagesService))
          {
            ParameterOverrides parameterOverrides = new ParameterOverrides();
            parameterOverrides.Add("maxParallelTasks", (object) 2);
            parameterOverrides.Add("name", (object) key);
            systemMessagesService = ObjectFactory.Container.Resolve<SystemMessagesBackgroundService>((ResolverOverride) parameterOverrides);
            WebServiceSystemMessageSender2.systemMessagesServices.Add(key, systemMessagesService);
          }
        }
      }
      return systemMessagesService;
    }

    private void SystemManager_ShuttingDown(object sender, CancelEventArgs e)
    {
      if (e.Cancel)
        return;
      lock (WebServiceSystemMessageSender2.smsLock)
        WebServiceSystemMessageSender2.systemMessagesServices.Clear();
    }

    private void ConfigManager_Executed(object sender, ExecutedEventArgs args)
    {
      if (args == null || !(args.CommandName == "SaveSection") || !(args.CommandArguments.GetType() == typeof (SystemConfig)))
        return;
      this.serviceEndpoints = (IList<string>) null;
    }

    private static string GetMessageGroupKey(string url, string hostHeader) => string.IsNullOrWhiteSpace(hostHeader) ? url : url + hostHeader;

    private static HttpWebRequest CreateRequest(
      string url,
      string hostHeader,
      WebServiceSystemMessageSender2 wsMessageSender)
    {
      HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
      request.Method = "PUT";
      request.ContentType = "application/json";
      request.CookieContainer = new CookieContainer();
      if (WebServiceSystemMessageSender2.RequestTimeout > 0)
        request.Timeout = WebServiceSystemMessageSender2.RequestTimeout;
      wsMessageSender.Authenticate(request);
      if (hostHeader != null)
        request.Host = hostHeader;
      return request;
    }

    private static byte[] GetMessageBytes(IEnumerable<SystemMessageBase> obj)
    {
      DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(obj.GetType(), (IEnumerable<Type>) new Type[1]
      {
        obj.GetType()
      });
      byte[] numArray = new byte[0];
      using (MemoryStream memoryStream = new MemoryStream())
      {
        contractJsonSerializer.WriteObject((Stream) memoryStream, (object) obj);
        return memoryStream.ToArray();
      }
    }

    private static void SendGroupedMessages(
      string url,
      string hostHeader,
      WebServiceSystemMessageSender2 wsMessageSender)
    {
      string messageGroupKey = WebServiceSystemMessageSender2.GetMessageGroupKey(url, hostHeader);
      List<SystemMessageBase> systemMessageBaseList = (List<SystemMessageBase>) null;
      if (WebServiceSystemMessageSender2.messageGroups.ContainsKey(messageGroupKey))
      {
        List<SystemMessageBase> messageGroup = WebServiceSystemMessageSender2.messageGroups[messageGroupKey];
        if (messageGroup != null && messageGroup.Count > 0)
        {
          lock (messageGroup)
          {
            if (messageGroup.Count > 0)
            {
              systemMessageBaseList = new List<SystemMessageBase>((IEnumerable<SystemMessageBase>) messageGroup);
              messageGroup.Clear();
            }
          }
        }
      }
      if (systemMessageBaseList == null || systemMessageBaseList.Count<SystemMessageBase>() <= 0)
        return;
      WebServiceSystemMessageSender2.SendMessageData(url, hostHeader, (IEnumerable<SystemMessageBase>) systemMessageBaseList, wsMessageSender);
    }

    private static void StoreGroupedMessages(
      string url,
      string hostHeader,
      List<SystemMessageBase> messages)
    {
      string messageGroupKey = WebServiceSystemMessageSender2.GetMessageGroupKey(url, hostHeader);
      List<SystemMessageBase> collection = new List<SystemMessageBase>((IEnumerable<SystemMessageBase>) messages);
      if (WebServiceSystemMessageSender2.messageGroups.ContainsKey(messageGroupKey))
      {
        List<SystemMessageBase> group = WebServiceSystemMessageSender2.messageGroups[messageGroupKey];
        if (group == null)
          return;
        lock (group)
          collection.ForEach((Action<SystemMessageBase>) (m => group.Add(m)));
      }
      else
      {
        lock (WebServiceSystemMessageSender2.messageGroupsLock)
        {
          if (!WebServiceSystemMessageSender2.messageGroups.ContainsKey(messageGroupKey))
          {
            WebServiceSystemMessageSender2.messageGroups.Add(messageGroupKey, new List<SystemMessageBase>((IEnumerable<SystemMessageBase>) collection));
          }
          else
          {
            List<SystemMessageBase> group = WebServiceSystemMessageSender2.messageGroups[messageGroupKey];
            lock (group)
              collection.ForEach((Action<SystemMessageBase>) (m => group.Add(m)));
          }
        }
      }
    }

    private static string GetDetailedMessagesInformation(List<SystemMessageBase> currentMessages)
    {
      IEnumerable<\u003C\u003Ef__AnonymousType17<string, int>> datas = currentMessages.GroupBy((Func<SystemMessageBase, string>) (msg => msg.Key), (Func<SystemMessageBase, string>) (msg => msg.Key), (k, g) => new
      {
        MessageType = k,
        MessageCount = g.Count<string>()
      });
      StringBuilder stringBuilder = new StringBuilder();
      foreach (var data in datas)
        stringBuilder.AppendFormat("{0}:{1}; ", (object) data.MessageType, (object) data.MessageCount);
      return stringBuilder.ToString();
    }
  }
}
