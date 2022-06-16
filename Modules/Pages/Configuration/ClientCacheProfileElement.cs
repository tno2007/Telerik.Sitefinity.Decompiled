// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Configuration.ClientCacheProfileElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Pages.Configuration
{
  /// <summary>
  /// Configures the client cache profile that can be used by the application pages and controls.
  /// </summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientCacheProfileElementDescription", Title = "ClientCacheProfileElementTitle")]
  public class ClientCacheProfileElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ClientCacheProfileElement(ConfigElement parent)
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
    /// <c>true</c> (the default) - <c>Cache-Control: public</c> with a specified <c>max-age</c>;
    /// <c>false</c> - <c>Cache-Control: no-cache</c>;
    /// <c>null</c> - no 'Cache-Control' header is sent to the client.
    /// </summary>
    [ConfigurationProperty("enabled")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientCacheProfileEnabledDescription", Title = "ClientCacheProfileEnabledTitle")]
    public bool? Enabled
    {
      get => (bool?) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// The time duration, in seconds, during which the image or control is cached (<c>Cache-Control: public, max-age=</c><see cref="P:Telerik.Sitefinity.Modules.Pages.Configuration.ClientCacheProfileElement.Duration" />).
    /// </summary>
    [ConfigurationProperty("duration", DefaultValue = 172800)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ClientCacheDurationDescription", Title = "ClientCacheDurationTitle")]
    public int Duration
    {
      get => (int) this["duration"];
      set => this["duration"] = (object) value;
    }

    internal ClientCacheControl ToClientCacheControl(
      bool clientCacheIsGloballyDisabled = false)
    {
      if (!clientCacheIsGloballyDisabled)
      {
        bool? enabled = this.Enabled;
        bool flag = false;
        if (!(enabled.GetValueOrDefault() == flag & enabled.HasValue))
          return !this.Enabled.HasValue ? ClientCacheControl.Default : new ClientCacheControl(TimeSpan.FromSeconds((double) this.Duration));
      }
      return ClientCacheControl.NoCache;
    }
  }
}
