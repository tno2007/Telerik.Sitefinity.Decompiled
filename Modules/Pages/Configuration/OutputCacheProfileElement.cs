// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.OutputCacheProfileElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Configures the output cache profile that can be used by the application pages and controls.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheProfileElementDescription", Title = "OutputCacheProfileElementTitle")]
  public class OutputCacheProfileElement : ConfigElement, IOutputCacheProfile, ICacheProfile
  {
    private const int MaxSizeDefaultValue = 500;
    private const bool VaryByHostDefaultValue = true;
    private const bool VaryByUserAgentDefaultValue = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.Configuration.OutputCacheProfileElement" /> class with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public OutputCacheProfileElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the programmatic name of the profile.</summary>
    /// <value>The name.</value>
    [ConfigurationProperty("name", DefaultValue = "", IsKey = true, IsRequired = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ItemName", Title = "ItemNameCaption")]
    public string Name
    {
      get => (string) this["name"];
      set => this["name"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a location where the content will be cached
    /// </summary>
    [ConfigurationProperty("location", DefaultValue = OutputCacheLocation.Server)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheProfileLocationDescription", Title = "OutputCacheProfileLocationTitle")]
    public OutputCacheLocation Location
    {
      get => (OutputCacheLocation) this["location"];
      set => this["location"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating the time duration, in seconds, during which the page or control is cached.
    /// </summary>
    [ConfigurationProperty("duration", DefaultValue = 60)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheDurationDescription", Title = "OutputCacheDurationTitle")]
    public int Duration
    {
      get => (int) this["duration"];
      set => this["duration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the expiration time should be reset on every request.
    /// </summary>
    [ConfigurationProperty("slidingExpiration", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheSlidingExpirationDescription", Title = "OutputCacheSlidingExpirationTitle")]
    public bool SlidingExpiration
    {
      get => (bool) this["slidingExpiration"];
      set => this["slidingExpiration"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value that specifies the maximum time in seconds that the fetched response is allowed to be reused from the time of the request. Corresponds to the 'max-age' directive of the Cache-Control header. If not specified and the a client caching is used, the Duration property will set the client cache max age.
    /// </summary>
    [ConfigurationProperty("maxAge")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "MaxAgeDesctiption", Title = "MaxAgeTitle")]
    public int? ClientMaxAge
    {
      get => (int?) this["maxAge"];
      set => this["maxAge"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value that specifies the maximum time in seconds that the fetched response is allowed to be cached on proxy servers and CDNs. It overrides the max-age directive and expires header field when present.
    /// </summary>
    [ConfigurationProperty("proxyMaxAge")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ProxyMaxAgeDesctiption", Title = "ProxyMaxAgeTitle")]
    public int? ProxyMaxAge
    {
      get => (int?) this["proxyMaxAge"];
      set => this["proxyMaxAge"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether caching is enabled.
    /// </summary>
    [ConfigurationProperty("enabled", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "OutputCacheProfileEnabledDescription", Title = "OutputCacheProfileEnabledTitle")]
    public bool Enabled
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a collection of user-defined parameters for the provider.
    /// </summary>
    [ConfigurationProperty("parameters")]
    public NameValueCollection Parameters
    {
      get => (NameValueCollection) this["parameters"];
      set => this["parameters"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cache should vary by user agent (browser) header.
    /// </summary>
    [Obsolete("Use .Parameters[OutputCacheElement.PageCacheKnownParams.VaryByUserAgent] instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool VaryByUserAgent
    {
      get
      {
        bool flag;
        return this.TryGetBoolParam("varyByUserAgent", out flag) && flag;
      }
      set
      {
        if (!value)
        {
          if (!this.Parameters.Keys.Contains("varyByUserAgent"))
            return;
          this.Parameters.Remove("varyByUserAgent");
        }
        else
          this.Parameters["varyByUserAgent"] = value.ToString();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the cache should vary by the host header.
    /// </summary>
    [Obsolete("Use .Parameters[OutputCacheElement.PageCacheKnownParams.VaryByHost] instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool VaryByHost
    {
      get
      {
        bool flag;
        return !this.TryGetBoolParam("varyByHost", out flag) || flag;
      }
      set
      {
        if (value)
        {
          if (!this.Parameters.Keys.Contains("varyByHost"))
            return;
          this.Parameters.Remove("varyByHost");
        }
        else
          this.Parameters["varyByHost"] = value.ToString();
      }
    }

    /// <summary>
    /// Gets or sets a custom string key that allows to specify varying the output cache by specific context information.
    /// The key can be used later in the $global.asax$ by overriding GetVaryByCustomString method and specifying the behavior of the output cache for the custom string.
    /// </summary>
    [Obsolete("Use .Parameters[OutputCacheElement.PageCacheKnownParams.VaryByHost] instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public string VaryByCustom
    {
      get
      {
        string str;
        return this.TryGetStringParam("varyByCustom", out str) ? str : string.Empty;
      }
      set
      {
        if (value.IsNullOrEmpty())
        {
          if (!this.Parameters.Keys.Contains("varyByCustom"))
            return;
          this.Parameters.Remove("varyByCustom");
        }
        else
          this.Parameters["varyByCustom"] = value;
      }
    }

    /// <summary>
    /// Gets or sets the max size in KB of item to be cached. The items that exceed that limit are not cached.
    /// </summary>
    [Obsolete("Use Parameters[OutputCacheElement.MediaCacheKnownParams.MaxSize] instead.")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public int MaxSize
    {
      get
      {
        int num;
        return this.TryGetIntParam("maxSize", out num) ? num : 500;
      }
      set
      {
        if (value == 500)
        {
          if (!this.Parameters.Keys.Contains("maxSize"))
            return;
          this.Parameters.Remove("maxSize");
        }
        else
          this.Parameters["maxSize"] = value.ToString();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether page should be served only once before its output cache is filled.
    /// If not - page can be served directly from database to many requests, causing compilation every time until it gets into the cache.
    /// </summary>
    [ConfigurationProperty("waitForPageOutputCacheToFill", DefaultValue = false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("The property is moved in OutputCacheElement.")]
    public bool WaitForPageOutputCacheToFill
    {
      get => (bool) this["waitForPageOutputCacheToFill"];
      set => this["waitForPageOutputCacheToFill"] = (object) value;
    }

    internal class PropNames
    {
      internal const string Name = "name";
      internal const string Location = "location";
      internal const string Enabled = "enabled";
      internal const string Duration = "duration";
      internal const string ProxyMaxAge = "proxyMaxAge";
      internal const string MaxAge = "maxAge";
      internal const string SlidingExpiration = "slidingExpiration";
    }
  }
}
