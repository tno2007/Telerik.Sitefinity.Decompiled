// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.LoadBalancing.WebServiceSystemMessageSender
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
using Telerik.Sitefinity.Cloud.WindowsAzure;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Licensing;
using Telerik.Sitefinity.LoadBalancing.Configuration;
using Telerik.Sitefinity.LoadBalancing.Web.Services;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.LoadBalancing
{
  /// <summary>A class for sending system messages.</summary>
  public class WebServiceSystemMessageSender : ISystemMessageSender
  {
    private IList<string> serviceEndpoints;
    private static readonly bool isFormsAuthenticaiotn = Config.Get<SecurityConfig>().AuthenticationMode == AuthenticationMode.Forms;
    private static readonly Dictionary<string, SystemMessagesBackgroundService> systemMessagesServices = new Dictionary<string, SystemMessagesBackgroundService>();
    private const string HandleMessageMethodName = "HandleMessage";
    private const string HandleMessagesMethodName = "HandleMessages";
    private const string RequestNlbMessagesKey = "sf_req_nlb_msgs";
    private const string ParentOperationKey = "sf_nlb_parent";

    public WebServiceSystemMessageSender()
    {
      LicenseLimitations.CanUseLoadBalancing(true);
      ManagerBase<ConfigProvider>.Executed += new EventHandler<ExecutedEventArgs>(this.ConfigManager_Executed);
      EventHub.Subscribe<IContextOperationStartEvent>(new SitefinityEventHandler<IContextOperationStartEvent>(this.OnOperationStart));
      EventHub.Subscribe<IContextOperationEndEvent>(new SitefinityEventHandler<IContextOperationEndEvent>(this.OnOperationEnd));
      SystemManager.ShuttingDown += new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
    }

    /// <inheritdoc />
    public virtual bool IsActive => !AzureRuntime.IsRunning;

    protected virtual int RequestTimeout => Config.Get<SystemConfig>().LoadBalancingConfig.RequestTimeout;

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
    protected virtual void SendMessage(SystemMessageBase realMessage)
    {
      List<SystemMessageBase> msgs = new List<SystemMessageBase>()
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
      };
      if (this.TryStoreMessages((IEnumerable<SystemMessageBase>) msgs))
        return;
      this.SendObject((object) msgs);
      if (!SystemMessageDispatcher.TestLoggingEnabled)
        return;
      SystemMessageDispatcher.LogTestingMessage(string.Format("{0}, Messages sent: {1}. Failed to group messages - sending directly.", (object) DateTime.UtcNow, (object) 1));
    }

    protected virtual void SendMessages(SystemMessageBase[] realMessages)
    {
      int length = realMessages.Length;
      SystemMessageBase[] msgs = new SystemMessageBase[length];
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
        msgs[index] = systemMessageBase;
      }
      if (this.TryStoreMessages((IEnumerable<SystemMessageBase>) msgs))
        return;
      this.SendObject((object) msgs);
      if (!SystemMessageDispatcher.TestLoggingEnabled)
        return;
      SystemMessageDispatcher.LogTestingMessage(string.Format("{0}, Messages sent: {1}. Failed to group messages - sending directly.", (object) DateTime.UtcNow, (object) length));
    }

    private void SendObject(object obj)
    {
      DataContractJsonSerializer contractJsonSerializer = new DataContractJsonSerializer(obj.GetType(), (IEnumerable<Type>) new Type[1]
      {
        obj.GetType()
      });
      byte[] data = new byte[0];
      using (MemoryStream memoryStream = new MemoryStream())
      {
        contractJsonSerializer.WriteObject((Stream) memoryStream, obj);
        data = memoryStream.ToArray();
      }
      foreach (string str in this.GetUrlsToReceiveMessage())
      {
        if (this.CanDetermineLocalAddress() && this.IsLocalIpAddress(str))
          this.TryStopLocalSystemBackgroundService(str);
        else
          this.InvokeWebMethod(str, "PUT", data);
      }
    }

    private void InvokeWebMethod(string url, string httpMethod, byte[] data)
    {
      string hostHeader = this.GetRequestHostHeader();
      WebServiceSystemMessageSender.WithExceptionHandling(url, httpMethod, hostHeader, (Action) (() =>
      {
        HttpWebRequest request = (HttpWebRequest) WebRequest.Create(url);
        request.Method = httpMethod;
        request.ContentType = "application/json";
        request.CookieContainer = new CookieContainer();
        if (this.RequestTimeout > 0)
          request.Timeout = this.RequestTimeout;
        this.Authenticate(request);
        if (hostHeader != null)
          request.Host = hostHeader;
        SystemMessagesBackgroundService systemMessagesService = WebServiceSystemMessageSender.GetSystemMessagesService(url);
        if (!systemMessagesService.IsStopped)
          systemMessagesService.EnqueueTask((Action) (() => WebServiceSystemMessageSender.SendMessageData(url, httpMethod, data, hostHeader, request)));
        else
          ThreadPool.QueueUserWorkItem((WaitCallback) (obj => WebServiceSystemMessageSender.SendMessageData(url, httpMethod, data, hostHeader, request)));
      }));
    }

    private static void SendMessageData(
      string url,
      string httpMethod,
      byte[] data,
      string hostHeader,
      HttpWebRequest request)
    {
      if (data != null)
      {
        request.ContentLength = (long) data.Length;
        using (Stream requestStream = request.GetRequestStream())
          requestStream.Write(data, 0, data.Length);
      }
      WebServiceSystemMessageSender.WithExceptionHandling(url, httpMethod, hostHeader, (Action) (() => request.BeginGetResponse(new AsyncCallback(WebServiceSystemMessageSender.ResponseCallback), (object) request)));
    }

    private static void ResponseCallback(IAsyncResult asyncResult)
    {
      HttpWebRequest request = (HttpWebRequest) asyncResult.AsyncState;
      WebServiceSystemMessageSender.WithExceptionHandling(request.RequestUri.AbsoluteUri, request.Method, request.Host, (Action) (() => request.EndGetResponse(asyncResult)));
    }

    private static void WithExceptionHandling(
      string url,
      string httpMethod,
      string hostHeader,
      Action action)
    {
      try
      {
        action();
      }
      catch (Exception ex)
      {
        Exceptions.HandleException(new Exception(string.Format("Error sending system message to URL: {0}; HTTP method: {1}; Host: ", (object) url, (object) httpMethod) + (hostHeader == null ? "null" : string.Format("'{0}'", (object) hostHeader)), ex), ExceptionPolicyName.IgnoreExceptions);
      }
    }

    /// <summary>
    /// Returns the absolute URLs of an <see cref="T:Telerik.Sitefinity.LoadBalancing.Web.Services.ISystemWebService" /> endpoint for all Sitefinity instances.
    /// </summary>
    /// <returns>Absolute URLs to be invoked, including host, service endpoint and method name.</returns>
    protected internal virtual IEnumerable<string> GetUrlsToReceiveMessage() => this.GetServiceEndpoints().Select<string, string>((Func<string, string>) (url => url + "HandleMessages"));

    private IList<string> GetServiceEndpoints()
    {
      if (this.serviceEndpoints == null)
        this.serviceEndpoints = (IList<string>) Config.Get<SystemConfig>().LoadBalancingConfig.URLS.Elements.Select<InstanceUrlConfigElement, string>((Func<InstanceUrlConfigElement, string>) (url => WebServiceSystemMessageSender.GetServiceEndpoint(url.Value))).ToList<string>();
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
          throw new ArgumentException(string.Format("The {0} URI scheme is not supported by the {1}.", (object) lowerInvariant, (object) typeof (WebServiceSystemMessageSender).Name));
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
      if (WebServiceSystemMessageSender.isFormsAuthenticaiotn)
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
      SystemMessagesBackgroundService systemMessagesService = WebServiceSystemMessageSender.GetSystemMessagesService(absoluteServiceUrl);
      if (systemMessagesService.IsStopped)
        return;
      systemMessagesService.Stop(false);
    }

    private static SystemMessagesBackgroundService GetSystemMessagesService(
      string key)
    {
      SystemMessagesBackgroundService systemMessagesService;
      if (!WebServiceSystemMessageSender.systemMessagesServices.TryGetValue(key, out systemMessagesService))
      {
        lock (WebServiceSystemMessageSender.systemMessagesServices)
        {
          if (!WebServiceSystemMessageSender.systemMessagesServices.TryGetValue(key, out systemMessagesService))
          {
            ParameterOverrides parameterOverrides = new ParameterOverrides();
            parameterOverrides.Add("maxParallelTasks", (object) 2);
            parameterOverrides.Add("name", (object) key);
            systemMessagesService = ObjectFactory.Container.Resolve<SystemMessagesBackgroundService>((ResolverOverride) parameterOverrides);
            WebServiceSystemMessageSender.systemMessagesServices.Add(key, systemMessagesService);
          }
        }
      }
      return systemMessagesService;
    }

    private void SystemManager_ShuttingDown(object sender, CancelEventArgs e)
    {
      SystemManager.ShuttingDown -= new EventHandler<CancelEventArgs>(this.SystemManager_ShuttingDown);
      this.Flush();
      if (e.Cancel)
        return;
      lock (WebServiceSystemMessageSender.systemMessagesServices)
        WebServiceSystemMessageSender.systemMessagesServices.Clear();
    }

    private void ConfigManager_Executed(object sender, ExecutedEventArgs args)
    {
      if (args == null || !(args.CommandName == "SaveSection") || !(args.CommandArguments.GetType() == typeof (SystemConfig)))
        return;
      this.serviceEndpoints = (IList<string>) null;
    }

    private bool TryStoreMessages(IEnumerable<SystemMessageBase> msgs)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !currentHttpContext.Items.Contains((object) "sf_req_nlb_msgs") && Bootstrapper.IsNLBComunicationSetup)
        return false;
      if (!(currentHttpContext.Items[(object) "sf_req_nlb_msgs"] is List<SystemMessageBase> systemMessageBaseList))
        currentHttpContext.Items[(object) "sf_req_nlb_msgs"] = (object) msgs.ToList<SystemMessageBase>();
      else
        systemMessageBaseList.AddRange(msgs);
      return true;
    }

    /// <summary>
    /// Registers the current operation for buffering nlb messages
    /// </summary>
    /// <param name="event">The event.</param>
    private void OnOperationStart(IContextOperationStartEvent @event)
    {
      if (!this.IsActive)
        return;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || currentHttpContext.Items.Contains((object) "sf_req_nlb_msgs"))
        return;
      currentHttpContext.Items[(object) "sf_nlb_parent"] = (object) @event.OperationKey;
      currentHttpContext.Items[(object) "sf_req_nlb_msgs"] = (object) null;
    }

    /// <summary>
    /// Send grouped messages for a given parent operation (e.g. request). Nested operations won't cause sending.
    /// </summary>
    /// <param name="event">The event.</param>
    private void OnOperationEnd(IContextOperationEndEvent @event)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      string operationKey = @event.OperationKey;
      if (currentHttpContext != null && (operationKey == null || !operationKey.Equals(currentHttpContext.Items[(object) "sf_nlb_parent"])))
        return;
      this.Flush();
    }

    private void Flush()
    {
      if (!this.IsActive)
        return;
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !(currentHttpContext.Items[(object) "sf_req_nlb_msgs"] is List<SystemMessageBase> currentMessages) || currentMessages.Count <= 0)
        return;
      this.SendObject((object) currentMessages);
      if (SystemMessageDispatcher.TestLoggingEnabled)
      {
        string messagesInformation = WebServiceSystemMessageSender.GetDetailedMessagesInformation(currentMessages);
        SystemMessageDispatcher.LogTestingMessage(string.Format("{0}, {1}, Messages sent: {2}, Messages info: {3}", (object) DateTime.UtcNow, (object) currentHttpContext.Request.RawUrl, (object) currentMessages.Count, (object) messagesInformation));
      }
      currentHttpContext.Items[(object) "sf_req_nlb_msgs"] = (object) null;
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
