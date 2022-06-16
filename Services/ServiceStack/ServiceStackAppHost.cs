// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ServiceStack.ServiceStackAppHost
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Funq;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Host;
using ServiceStack.Text;
using ServiceStack.Web;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.HealthMonitoring;
using Telerik.Sitefinity.Security.Sanitizers;

namespace Telerik.Sitefinity.Services.ServiceStack
{
  /// <summary>App host for service stack</summary>
  public class ServiceStackAppHost : AppHostHttpListenerBase
  {
    private const string ApiKey = "8778-e1JlZjo4Nzc4LE5hbWU6UHJvZ3Jlc3MgU29mdHdhcmUgRUFELFR5cGU6QnVzaW5lc3MsTWV0YTowLEhhc2g6RTFBWGdEM1lkUUhpQ2tScHN2MWo3Vk9ieUx3SGt2RnVLRXA5S3M0dE8vUTBFdnRLUXREWkZPMU0zUklQbUFSS1dJYWh6SFpGWGpMQlZha3FqOTZBdGdTQUltVzI4K25zNHI5R1d0YjlUR0t4MkNKdW1FN2kxdHBlRy9vRWgzaEpnbW9ZbE9mRjRGQTVjNU4xZ1Vsd0dnRWZVL2ErTXllN1RobnBUZXN6NlpJPSxFeHBpcnk6MjAyMi0wMi0xOX0=";

    public ServiceStackAppHost()
      : base(string.Empty, (Assembly) Thread.GetDomain().DefineDynamicAssembly(new AssemblyName("Empty assembly"), AssemblyBuilderAccess.Run))
    {
    }

    /// <summary>Configures the specified container.</summary>
    /// <param name="container">The container.</param>
    public override void Configure(Container container)
    {
      Feature feature = Feature.None;
      string str1 = ConfigurationManager.AppSettings.Get("sf:serviceStackEnableFeatures");
      if (!string.IsNullOrEmpty(str1))
      {
        string str2 = str1;
        char[] separator = new char[1]{ ',' };
        foreach (string str3 in str2.Split(separator, StringSplitOptions.RemoveEmptyEntries))
        {
          Feature result;
          if (Enum.TryParse<Feature>(str3, out result))
            feature |= result;
        }
      }
      else
        feature = Feature.All;
      this.SetConfig(new HostConfig()
      {
        HandlerFactoryPath = "RestApi",
        EnableFeatures = feature,
        MetadataVisibility = RequestAttributes.None,
        StrictMode = new bool?(false),
        UseJsObject = false,
        LogUnobservedTaskExceptions = false
      });
      UnityContainerAdapter containerAdapter = new UnityContainerAdapter();
      container.Adapter = (IContainerAdapter) containerAdapter;
      SystemManager.PendingServiceStackPlugins.ForEach((Action<IPlugin>) (p => this.Plugins.Add(p)));
      this.ServiceExceptionHandlers.Add((HandleServiceExceptionDelegate) ((httpReq, request, exception) =>
      {
        Exceptions.HandleException(exception, ExceptionPolicyName.UnhandledExceptions);
        return DtoUtils.CreateErrorResponse(request, exception);
      }));
      this.UncaughtExceptionHandlers.Add((HandleUncaughtExceptionDelegate) ((req, res, operationName, ex) =>
      {
        Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
        string errorMessage = !(ex is IHttpError httpError2) ? (string) null : httpError2.Message;
        if (res.IsClosed)
          return;
        res.WriteErrorToResponse(req, req.ResponseContentType, operationName, errorMessage, ex, ex.ToStatusCode());
      }));
      HostConfig config = this.Config;
      if (config == null)
        return;
      Dictionary<string, string> globalResponseHeaders = config.GlobalResponseHeaders;
      if (globalResponseHeaders == null)
        return;
      globalResponseHeaders.TryRemove<string, string>("X-Powered-By", out string _);
    }

    /// <summary>Initializes the Service Stack app host</summary>
    public static void Initialize()
    {
      if (ServiceStackHost.Instance != null)
        return;
      using (new MethodPerformanceRegion("Initialize ServiceStack app host."))
      {
        ServiceStackAppHost.ConfigureGuidSerialization();
        Licensing.RegisterLicense("8778-e1JlZjo4Nzc4LE5hbWU6UHJvZ3Jlc3MgU29mdHdhcmUgRUFELFR5cGU6QnVzaW5lc3MsTWV0YTowLEhhc2g6RTFBWGdEM1lkUUhpQ2tScHN2MWo3Vk9ieUx3SGt2RnVLRXA5S3M0dE8vUTBFdnRLUXREWkZPMU0zUklQbUFSS1dJYWh6SFpGWGpMQlZha3FqOTZBdGdTQUltVzI4K25zNHI5R1d0YjlUR0t4MkNKdW1FN2kxdHBlRy9vRWgzaEpnbW9ZbE9mRjRGQTVjNU4xZ1Vsd0dnRWZVL2ErTXllN1RobnBUZXN6NlpJPSxFeHBpcnk6MjAyMi0wMi0xOX0=");
        new ServiceStackAppHost().Init();
      }
    }

    /// <summary>
    /// Configure GUID serialization to return GUID with dashes, so that consistency with WCF services is maintained.
    /// </summary>
    public static void ConfigureGuidSerialization()
    {
      JsConfig<Guid>.SerializeFn = (Func<Guid, string>) (guid => guid.ToString());
      JsConfig<Guid?>.SerializeFn = (Func<Guid?, string>) (guid => guid.ToString());
    }

    /// <inheritdoc />
    public override IServiceRunner<TRequest> CreateServiceRunner<TRequest>(
      ActionContext actionContext)
    {
      return (IServiceRunner<TRequest>) new ServiceStackServiceRunner<TRequest>((IAppHost) this, actionContext);
    }

    public override string ResolveAbsoluteUrl(string virtualPath, IRequest httpReq)
    {
      string url = base.ResolveAbsoluteUrl(virtualPath, httpReq);
      return ObjectFactory.Resolve<IHtmlSanitizer>().SanitizeUrl(url);
    }
  }
}
