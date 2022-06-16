// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.HttpClients.TrackingClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Microsoft.CSharp.RuntimeBinder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Licensing;

namespace Telerik.Sitefinity.UsageTracking.HttpClients
{
  internal class TrackingClient : ITrackingClient, IDisposable
  {
    private readonly HttpClient httpClient;
    private readonly Random randomGenerator;
    private const string AuthorizationHeader = "Authorization";
    private const string ApplicationJsonContentType = "application/json";
    private const string NotAuthenticatedErrorResponse = "Not authenticated.";
    private const string SfUsageTrackingReportingUrl = "https://metrics.sitefinity.com";
    private const string SfUsageTrackingConfigUrl = "https://metrics-config.sitefinity.com";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.UsageTracking.HttpClients.TrackingClient" /> class.
    /// </summary>
    [InjectionConstructor]
    public TrackingClient()
      : this(new HttpClient())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.UsageTracking.HttpClients.TrackingClient" /> class.
    /// </summary>
    /// <param name="httpClient">The http client used</param>
    internal TrackingClient(HttpClient httpClient)
    {
      if (httpClient == null)
        throw new ArgumentNullException(nameof (httpClient));
      string authorizationHeaderValue = this.GetAuthorizationHeaderValue();
      this.httpClient = httpClient;
      this.DecorateHeaders(this.httpClient, "application/json", authorizationHeaderValue);
    }

    /// <inheritdoc />
    public string GetTrackingConfiguration()
    {
      HttpResponseMessage result = this.httpClient.GetAsync(this.GetLicenseInfo().IsTrial ? "https://metrics-config.sitefinity.com?isTrial=true" : "https://metrics-config.sitefinity.com").Result;
      result.EnsureSuccessStatusCode();
      object obj1 = JsonConvert.DeserializeObject<object>(result.Content.ReadAsStringAsync().Result);
      // ISSUE: reference to a compiler-generated field
      if (TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__1 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__1 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof (string), typeof (TrackingClient)));
      }
      // ISSUE: reference to a compiler-generated field
      Func<CallSite, object, string> target = TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__1.Target;
      // ISSUE: reference to a compiler-generated field
      CallSite<Func<CallSite, object, string>> p1 = TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__1;
      // ISSUE: reference to a compiler-generated field
      if (TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__0 == null)
      {
        // ISSUE: reference to a compiler-generated field
        TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, object>>.Create(Binder.GetMember(CSharpBinderFlags.None, "SyncCronSpec", typeof (TrackingClient), (IEnumerable<CSharpArgumentInfo>) new CSharpArgumentInfo[1]
        {
          CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null)
        }));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      object obj2 = TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__0.Target((CallSite) TrackingClient.\u003C\u003Eo__2.\u003C\u003Ep__0, obj1);
      return target((CallSite) p1, obj2);
    }

    /// <inheritdoc />
    public void SendReportData(string report)
    {
      StringContent content = new StringContent("{\"event\": " + report + ", \"index\": \"sf-test-metrics\" }", Encoding.UTF8, "application/json");
      this.httpClient.PostAsync(string.Format("{0}/services/collector/event", (object) "https://metrics.sitefinity.com"), (HttpContent) content).Result.EnsureSuccessStatusCode();
    }

    private LicenseInfo GetLicenseInfo() => LicenseState.Current.LicenseInfo;

    /// <inheritdoc />
    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize((object) this);
    }

    /// <summary>Disposes the managed resources</summary>
    /// <param name="disposing">Defines whether a disposing is executing now.</param>
    protected virtual void Dispose(bool disposing)
    {
      if (!disposing || this.httpClient == null)
        return;
      this.httpClient.Dispose();
    }

    private string GetAuthorizationHeaderValue() => "Splunk " + "a00df4f0-8b4b-4cd6-83d5-32d824b6ef40";

    private void DecorateHeaders(
      HttpClient client,
      string acceptContentTypeHeaderValue,
      string authorizationHeaderValue)
    {
      client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptContentTypeHeaderValue));
      client.DefaultRequestHeaders.Add("Authorization", authorizationHeaderValue);
    }
  }
}
