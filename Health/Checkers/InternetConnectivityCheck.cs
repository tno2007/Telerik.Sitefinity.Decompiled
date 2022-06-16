// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Health.InternetConnectivityCheck
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Telerik.Sitefinity.Health
{
  /// <summary>
  /// Defines a functionality that checks if internet connectivity is working.
  /// </summary>
  internal class InternetConnectivityCheck : HealthCheckBase
  {
    private readonly string failTaskMessage = " failed!";
    private readonly string healthyMessage = HttpStatusCode.OK.ToString();

    /// <summary>Url Request</summary>
    /// <returns>Task HttpStatusCode</returns>
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    internal async Task<HttpStatusCode> UrlRequest()
    {
      string parameter = this.Parameters["url"];
      if (!parameter.IsNullOrEmpty())
        return (await new HttpClient().GetAsync(parameter)).StatusCode;
      throw new ArgumentException("Parameters collection require an item with key: URL");
    }

    /// <inheritdoc />
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "Not needed")]
    [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", Justification = "Not needed")]
    protected override async Task<HealthCheckResult> RunCore()
    {
      InternetConnectivityCheck connectivityCheck = this;
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      // ISSUE: explicit non-virtual call
      return await Task.FromResult<HealthCheckResult>(await connectivityCheck.UrlRequest() == HttpStatusCode.OK ? HealthCheckResult.Healthy(__nonvirtual (connectivityCheck.Name), connectivityCheck.healthyMessage) : HealthCheckResult.Unhealthy(__nonvirtual (connectivityCheck.Name), __nonvirtual (connectivityCheck.Name) + connectivityCheck.failTaskMessage));
    }
  }
}
