// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.IOutputCacheProfile
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Configures the output cache profile that can be used by the application pages and controls
  /// </summary>
  public interface IOutputCacheProfile : ICacheProfile
  {
    /// <summary>
    /// Gets a value indicating whether the cache should vary by user agent (browser) header.
    /// </summary>
    bool VaryByUserAgent { get; }

    /// <summary>
    /// Gets a value indicating whether the cache should vary by the host header.
    /// </summary>
    bool VaryByHost { get; }

    /// <summary>
    /// Gets a custom string key that allows to specify varying the output cache by specific context information.
    /// The key can be used later in the global application class by overriding GetVaryByCustomString method and specifying the behavior of the output cache for the custom string.
    /// </summary>
    string VaryByCustom { get; }
  }
}
