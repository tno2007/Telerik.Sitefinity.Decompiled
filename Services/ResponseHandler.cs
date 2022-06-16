// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.ResponseHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Abstractions;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Provides API for configuring and sending a Http response.
  /// </summary>
  internal class ResponseHandler : IResponseHandler
  {
    private const int CacheLifetimeInMinutes = 60;
    private HttpRequest request;
    private HttpResponse response;
    private string content;
    private string etag;

    /// <summary>Configures the process of sending the response.</summary>
    /// <param name="request">The current request object.</param>
    /// <param name="response">The current response object.</param>
    /// <param name="content">The content of the response.</param>
    /// <returns>Current instance of the handler.</returns>
    public IResponseHandler Configure(
      HttpRequest request,
      HttpResponse response,
      string content)
    {
      this.content = content;
      this.request = request;
      this.response = response;
      this.etag = ObjectFactory.Resolve<IEtagProvider>().GetEtag(this.request.Path.ToString(), Encoding.UTF8.GetBytes(this.content));
      return (IResponseHandler) this;
    }

    /// <summary>Configures the cache headers.</summary>
    /// <returns>Current instance of the handler.</returns>
    public IResponseHandler AddCaching()
    {
      this.response.Headers.Add("ETag", this.etag);
      this.response.Cache.SetCacheability(HttpCacheability.Public);
      this.response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
      this.response.Cache.SetMaxAge(TimeSpan.FromMinutes(60.0));
      return (IResponseHandler) this;
    }

    /// <summary>
    /// Configures the compression of the response content.
    /// If the browser doesn't support GZip, no compression
    /// will be applied.
    /// </summary>
    /// <returns>Current instance of the handler.</returns>
    public IResponseHandler AddGZip()
    {
      string str = this.request.Headers.Get("Accept-Encoding");
      if (str != null && str.Contains("gzip"))
      {
        this.response.Filter = (Stream) new GZipStream(this.response.Filter, CompressionMode.Compress);
        this.response.Headers.Add("Content-Encoding", "gzip");
      }
      return (IResponseHandler) this;
    }

    /// <summary>Sends the response to the clients.</summary>
    public void Send()
    {
      if (this.request.Headers.Keys.Contains("If-None-Match") && this.request.Headers["If-None-Match"].ToString() == this.etag)
      {
        this.response.StatusCode = 304;
      }
      else
      {
        this.response.Write(this.content);
        this.response.ContentType = "application/x-javascript";
      }
    }
  }
}
