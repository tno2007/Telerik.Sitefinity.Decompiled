// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ICacheProfile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Configures the output cache profile that can be used by the application pages and controls
  /// </summary>
  public interface ICacheProfile
  {
    /// <summary>Gets the programmatic name of the profile.</summary>
    string Name { get; }

    /// <summary>Gets a location where the content will be cached</summary>
    OutputCacheLocation Location { get; }

    /// <summary>
    /// Gets the time duration, in seconds, during which the page or control is cached.
    /// </summary>
    int Duration { get; }

    /// <summary>
    /// Gets the time duration, in seconds, during which the page is cached on client browser or proxy servers.
    /// </summary>
    int? ClientMaxAge { get; }

    /// <summary>
    /// Gets the time duration, in seconds, during which the page is cached on proxy servers.
    /// </summary>
    int? ProxyMaxAge { get; }

    /// <summary>
    /// Gets a value indicating whether the expiration time should be reset on every request.
    /// </summary>
    bool SlidingExpiration { get; }

    /// <summary>
    /// Gets a collection of dynamic parameters for the cache profile.
    /// </summary>
    NameValueCollection Parameters { get; }
  }
}
