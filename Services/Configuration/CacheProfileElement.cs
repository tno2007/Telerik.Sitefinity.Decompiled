// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.CacheProfileElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>
  /// Configures the output cache profile that can be used by the application pages and controls.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheProfileElementDescription", Title = "CacheProfileElementTitle")]
  public class CacheProfileElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CacheProfileElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// The time duration, in seconds, during which the item will be cached.
    /// </summary>
    [ConfigurationProperty("duration", DefaultValue = 1200)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheDurationDescription", Title = "CacheDurationTitle")]
    public int Duration
    {
      get => (int) this["duration"];
      set => this["duration"] = (object) value;
    }

    /// <summary>
    /// Indicates whether the expiration time should be reset on every request.
    /// </summary>
    [ConfigurationProperty("slidingExpiration")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CacheSlidingExpirationDescription", Title = "CacheSlidingExpirationTitle")]
    public bool SlidingExpiration
    {
      get => (bool) this["slidingExpiration"];
      set => this["slidingExpiration"] = (object) value;
    }
  }
}
