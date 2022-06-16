// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IResponseHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;

namespace Telerik.Sitefinity.Services
{
  /// <summary>
  /// Provides API for configuring and sending a Http response.
  /// </summary>
  internal interface IResponseHandler
  {
    /// <summary>Configures the process of sending the response.</summary>
    /// <param name="request">The current request object.</param>
    /// <param name="response">The current response object.</param>
    /// <param name="content">The content of the response.</param>
    /// <returns>Current instance of the handler.</returns>
    IResponseHandler Configure(
      HttpRequest request,
      HttpResponse response,
      string content);

    /// <summary>Configures the cache headers.</summary>
    /// <returns>Current instance of the handler.</returns>
    IResponseHandler AddCaching();

    /// <summary>
    /// Configures the compression of the response content.
    /// If the browser doesn't support GZip, no compression
    /// will be applied.
    /// </summary>
    /// <returns>Current instance of the handler.</returns>
    IResponseHandler AddGZip();

    /// <summary>Sends the response to the clients.</summary>
    void Send();
  }
}
