// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.ClientCacheControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using System.Web;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Represents the browser cache control settings of a web resource.
  /// </summary>
  public class ClientCacheControl
  {
    /// <summary>
    /// When <c>true</c> no <c>Cache-Control</c> header is specified.
    /// </summary>
    public readonly bool IsDefault;
    /// <summary>Type</summary>
    /// <remarks>
    /// Only client cacheablity values are supported: <see cref="F:System.Web.HttpCacheability.Public" />,
    /// <see cref="F:System.Web.HttpCacheability.Private" /> and <see cref="F:System.Web.HttpCacheability.NoCache" />.
    /// </remarks>
    public readonly HttpCacheability Cacheablity;
    /// <summary>
    /// Duration of the browser cache.
    /// The response is stale if its current age is greater than the age value given (in seconds)
    /// at the time of a new request for that resource.
    /// </summary>
    public readonly TimeSpan MaxAge;
    /// <summary>Duration of the proxy/cdn cache</summary>
    internal readonly TimeSpan ProxyMaxAge;
    /// <summary>
    /// A singleton instance representing the default settings.
    /// </summary>
    public static readonly ClientCacheControl Default = new ClientCacheControl();
    /// <summary>
    /// A singleton instance representing "no cache" settings.
    /// </summary>
    public static readonly ClientCacheControl NoCache = new ClientCacheControl(HttpCacheability.NoCache);

    /// <summary>Creates a new instance with default settings.</summary>
    public ClientCacheControl() => this.IsDefault = true;

    /// <summary>
    /// Creates a new instance with the specified cacheablity.
    /// </summary>
    public ClientCacheControl(HttpCacheability cacheablity)
      : this(cacheablity, new TimeSpan())
    {
    }

    /// <summary>
    /// Creates a new instance representing the specified expiration.
    /// </summary>
    public ClientCacheControl(TimeSpan maxAge)
      : this(HttpCacheability.Public, maxAge)
    {
    }

    /// <summary>
    /// Creates a new instance representing the specified cacheablity and expiration.
    /// </summary>
    public ClientCacheControl(HttpCacheability cacheablity, TimeSpan maxAge)
      : this(cacheablity, maxAge, new TimeSpan())
    {
    }

    /// <summary>
    /// Creates a new instance representing the specified cacheablity and expiration.
    /// </summary>
    public ClientCacheControl(HttpCacheability cacheablity, TimeSpan maxAge, TimeSpan proxyMaxAge)
    {
      ClientCacheControl.Validate(cacheablity);
      this.IsDefault = false;
      this.Cacheablity = cacheablity;
      this.MaxAge = maxAge;
      this.ProxyMaxAge = proxyMaxAge;
    }

    /// <summary>
    /// Returns the value of the HTTP 1.1 Cache-Control header or <c>null</c>, if none has to be set.
    /// </summary>
    /// <returns></returns>
    public string ToHttpCacheControlHeaderValue()
    {
      if (this.IsDefault)
        return (string) null;
      bool flag = false;
      StringBuilder stringBuilder = new StringBuilder();
      switch (this.Cacheablity)
      {
        case HttpCacheability.NoCache:
        case HttpCacheability.Server:
          stringBuilder.Append("no-cache");
          flag = true;
          break;
        case HttpCacheability.Private:
        case HttpCacheability.ServerAndPrivate:
          stringBuilder.Append("private");
          break;
        case HttpCacheability.Public:
          stringBuilder.Append("public");
          break;
        default:
          throw new InvalidOperationException("Unsupported client cacheablity: " + (object) this.Cacheablity);
      }
      if (!flag)
      {
        if (this.MaxAge != new TimeSpan())
          stringBuilder.AppendFormat(", max-age={0}", (object) this.MaxAge.TotalSeconds);
        if (this.ProxyMaxAge != new TimeSpan())
          stringBuilder.AppendFormat(", s-maxage={0}", (object) this.ProxyMaxAge.TotalSeconds);
      }
      return stringBuilder.ToString();
    }

    public override string ToString() => this.ToHttpCacheControlHeaderValue();

    private static void Validate(HttpCacheability cacheablity)
    {
    }
  }
}
