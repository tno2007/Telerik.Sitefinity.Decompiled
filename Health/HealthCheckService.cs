// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.HealthCheckService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Web;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Health.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Health
{
  internal static class HealthCheckService
  {
    private static object locker = new object();
    private const string Type = "type";
    private const string HealthCheckCacheKey = "sf_HealthCheckCache_";
    internal const string AuthKey = "authKey";

    internal static string Endpoint => HealthCheckService.Nested.HealthCheckApiEndpoint;

    internal static bool CanHandle(HttpRequest request)
    {
      if (request == null)
        throw new ArgumentNullException(nameof (request));
      return !string.IsNullOrEmpty(HealthCheckService.Endpoint) && !(request.Url == (Uri) null) && !string.IsNullOrWhiteSpace(request.Url.AbsolutePath) && request.Url.AbsolutePath.EndsWith(HealthCheckService.Endpoint, StringComparison.OrdinalIgnoreCase);
    }

    internal static void ProcessRequest(HttpContext context)
    {
      if (context == null)
        throw new ArgumentNullException(nameof (context));
      if (!HealthCheckService.ValidateRequestHttpMethod(context))
        return;
      IHealthCheck[] healthCheckers = new IHealthCheck[2];
      SystemIsBootstrapped systemIsBootstrapped = new SystemIsBootstrapped();
      systemIsBootstrapped.Name = "System is Bootstrapped";
      systemIsBootstrapped.Critical = true;
      healthCheckers[0] = (IHealthCheck) systemIsBootstrapped;
      StartUpFinished startUpFinished = new StartUpFinished();
      startUpFinished.Name = "StartUp Finished";
      startUpFinished.Critical = true;
      healthCheckers[1] = (IHealthCheck) startUpFinished;
      HealthCheckStatus healthCheckStatus = new HealthCheckRunner((IEnumerable<IHealthCheck>) healthCheckers).Run().Result;
      HealthCheckConfig config = (HealthCheckConfig) null;
      try
      {
        config = Config.Get<SystemConfig>().HealthCheckConfig;
      }
      catch
      {
      }
      if (healthCheckStatus.Healthy)
      {
        HttpStatusCode authenticationKey = HealthCheckService.GetHttpStatusCodeByAuthenticationKey(context, config.AuthenticationKey);
        if (authenticationKey != HttpStatusCode.OK)
        {
          context.Response.StatusCode = (int) authenticationKey;
          return;
        }
        if (config.Enabled)
        {
          IEnumerable<HealthCheckResult> cachedResults = HealthCheckService.GetCachedResults(context);
          healthCheckStatus = new HealthCheckStatus(healthCheckStatus.Checks.Union<HealthCheckResult>(cachedResults));
        }
      }
      context.Response.StatusCode = HealthCheckService.GetStatusCode(healthCheckStatus.Healthy, config);
      context.Response.CacheControl = "no-cache";
      context.Response.AddHeader("Content-Type", "application/json; charset=" + context.Response.Charset);
      context.Response.Write(JsonConvert.SerializeObject((object) healthCheckStatus));
      context.ApplicationInstance.CompleteRequest();
    }

    internal static bool ValidateRequestHttpMethod(HttpContext context) => context.Request.HttpMethod == "GET";

    internal static HttpStatusCode GetHttpStatusCodeByAuthenticationKey(
      HttpContext context,
      string authKey)
    {
      if (string.IsNullOrEmpty(authKey))
        return HttpStatusCode.OK;
      if (!context.Request.Headers.Keys.Contains(nameof (authKey), StringComparison.OrdinalIgnoreCase))
        return HttpStatusCode.NotFound;
      return context.Request.Headers[nameof (authKey)] == authKey ? HttpStatusCode.OK : HttpStatusCode.Forbidden;
    }

    internal static IEnumerable<HealthCheckResult> GetCachedResults(
      HttpContext context)
    {
      IEnumerable<string> queryStringGroups = HealthCheckService.GetQueryStringGroups(context);
      string key = "sf_HealthCheckCache_" + string.Join("_", queryStringGroups.Select<string, string>((Func<string, string>) (p => p.ToUpperInvariant())));
      if (!(SystemManager.Cache.GetData(key) is IEnumerable<HealthCheckResult> cachedResults1))
      {
        lock (HealthCheckService.locker)
        {
          if (!(SystemManager.Cache.GetData(key) is IEnumerable<HealthCheckResult> cachedResults1))
          {
            IEnumerable<IHealthCheck> healthChecks = HealthCheckFactory.FilterByGroups(queryStringGroups);
            if (healthChecks != null)
            {
              if (healthChecks.Any<IHealthCheck>())
              {
                cachedResults1 = (IEnumerable<HealthCheckResult>) new HealthCheckRunner(healthChecks).Run().Result.Checks.OrderBy<HealthCheckResult, string>((Func<HealthCheckResult, string>) (check => check.Operation));
                SystemManager.Cache.Add(key, (object) cachedResults1, CacheItemPriority.Normal, (ICacheItemRefreshAction) null, (ICacheItemExpiration) new AbsoluteTime(TimeSpan.FromSeconds(10.0)));
              }
            }
          }
        }
      }
      if (cachedResults1 == null)
        cachedResults1 = Enumerable.Empty<HealthCheckResult>();
      return cachedResults1;
    }

    private static IEnumerable<string> GetQueryStringGroups(HttpContext context)
    {
      string str = context.Request.QueryString["type"];
      IEnumerable<string> queryStringGroups = Enumerable.Empty<string>();
      if (!str.IsNullOrWhitespace())
        queryStringGroups = (IEnumerable<string>) ((IEnumerable<string>) str.Split(new string[1]
        {
          ","
        }, StringSplitOptions.RemoveEmptyEntries)).OrderBy<string, string>((Func<string, string>) (p => p));
      return queryStringGroups;
    }

    private static int GetStatusCode(bool healthy, HealthCheckConfig config = null)
    {
      if (!healthy)
      {
        if (HealthCheckService.Nested.HealthCheckUnhealthyStatusCode > 0)
          return HealthCheckService.Nested.HealthCheckUnhealthyStatusCode;
        if (config != null && config.ReturnHttpErrorStatusCode)
          return 500;
      }
      return 200;
    }

    [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1812:AvoidUninstantiatedInternalClasses", Justification = "Not needed")]
    private class Nested
    {
      internal static readonly string HealthCheckApiEndpoint = ConfigurationManager.AppSettings.Get("sf:HealthCheckApiEndpoint");
      internal static readonly int HealthCheckUnhealthyStatusCode;

      [SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Justification = "Not needed")]
      [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1409:RemoveUnnecessaryCode", Justification = "Reviewed.")]
      static Nested()
      {
        string s = ConfigurationManager.AppSettings.Get("sf:HealthCheckUnhealthyStatusCode");
        if (!s.IsNullOrEmpty() && int.TryParse(s, out HealthCheckService.Nested.HealthCheckUnhealthyStatusCode))
          return;
        HealthCheckService.Nested.HealthCheckUnhealthyStatusCode = 0;
      }
    }
  }
}
