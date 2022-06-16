// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Webhooks.WebhookDataSender
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.Utilities.Newtonsoft.Json;
using Telerik.Sitefinity.Webhooks.Configuration;

namespace Telerik.Sitefinity.Webhooks
{
  internal class WebhookDataSender : IDisposable
  {
    private readonly HttpClient httpClient;
    private const string ApplicationJsonContentType = "application/json";
    private const string SignatureHeaderName = "Sf-Signature";
    private const string SignatureTimestampHeaderName = "Sf-Signature-Timestamp";

    public WebhookDataSender()
      : this(new HttpClient())
    {
    }

    internal WebhookDataSender(HttpClient httpClient) => this.httpClient = httpClient != null ? httpClient : throw new ArgumentNullException(nameof (httpClient));

    public async Task SendEventDataAsync(
      IEvent webhookEvent,
      HashSet<Type> webhookEvents,
      ConfigElementDictionary<string, WebhookEventConfigElement> events)
    {
      Type webhookEventType = webhookEvent.GetType();
      foreach (Type eventInterface in webhookEvents.Where<Type>((Func<Type, bool>) (e => e.IsAssignableFrom(webhookEventType))))
      {
        WebhookEventConfigElement eventConfigElement = events[eventInterface.FullName];
        InterfaceContractResolver contractResolver = new InterfaceContractResolver();
        string content = JsonConvert.SerializeObject((object) webhookEvent, new JsonSerializerSettings()
        {
          ContractResolver = (IContractResolver) contractResolver
        });
        foreach (WebhookUrlConfigElement urlConfig in (IEnumerable<WebhookUrlConfigElement>) eventConfigElement.Urls.Values)
        {
          try
          {
            StringContent stringContent = new StringContent(content, Encoding.UTF8, "application/json");
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, urlConfig.Url);
            request.Content = (HttpContent) stringContent;
            if (!string.IsNullOrWhiteSpace(urlConfig.Secret))
            {
              string str1 = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString();
              string str2 = (string) null;
              using (HMACSHA256 hmacshA256 = new HMACSHA256())
              {
                hmacshA256.Key = Encoding.UTF8.GetBytes(urlConfig.Secret);
                str2 = Convert.ToBase64String(hmacshA256.ComputeHash(Encoding.UTF8.GetBytes(content + "_" + str1)));
              }
              request.Headers.Add("Sf-Signature", str2);
              request.Headers.Add("Sf-Signature-Timestamp", str1);
            }
            (await this.httpClient.SendAsync(request)).EnsureSuccessStatusCode();
          }
          catch (Exception ex)
          {
            Log.Error("Could not send request to {0} for webhook event {1}. Error: {2}", (object) urlConfig.Url, (object) eventInterface.FullName, (object) ex.Message);
          }
        }
        content = (string) null;
      }
    }

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
  }
}
