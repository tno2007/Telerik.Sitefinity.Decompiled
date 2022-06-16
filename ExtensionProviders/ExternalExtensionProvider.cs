// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ExtensionProviders.ExternalExtensionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace Telerik.Sitefinity.ExtensionProviders
{
  /// <summary>
  /// Provides API for retrieving extension bundles from
  /// external URLs.
  /// </summary>
  internal class ExternalExtensionProvider : IExtensionsProvider
  {
    private readonly HttpClient http;
    private readonly IEnumerable<string> cdnUrls;

    public ExternalExtensionProvider(IEnumerable<string> urls)
    {
      this.http = new HttpClient();
      this.cdnUrls = urls;
    }

    /// <summary>Gets the bundles from the configured URLs.</summary>
    /// <returns>Retrieved bundles.</returns>
    public virtual IEnumerable<string> GetBundles() => (IEnumerable<string>) this.cdnUrls.AsParallel<string>().Select<string, string>((Func<string, string>) (url =>
    {
      try
      {
        HttpResponseMessage result = this.http.GetAsync(url).Result;
        return result.StatusCode != HttpStatusCode.OK ? string.Empty : result.Content.ReadAsStringAsync().Result;
      }
      catch
      {
        return string.Empty;
      }
    })).ToList<string>();
  }
}
