// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Configuration.ContentLocationsSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations.Enums;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Services.Configuration
{
  /// <summary>Global settings for content locations service.</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "ContentLocationsSettingsDescription", Title = "ContentLocationsSettingsTitle")]
  public class ContentLocationsSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes new instance of configuration element with the provided parent element.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public ContentLocationsSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether canonical urls should be enabled.
    /// </summary>
    [ConfigurationProperty("disabled", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DisableCanonicalUrlsDescription", Title = "DisableCanonicalUrlsTitle")]
    public bool DisableCanonicalUrls
    {
      get => (bool) this["disabled"];
      set => this["disabled"] = (object) value;
    }

    [ConfigurationProperty("enabled", DefaultValue = false)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use DisableCanonicalUrls")]
    internal bool DisableCanonicalUrlsObsolete
    {
      get => (bool) this["enabled"];
      set => this["enabled"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether widget in single item mode should change page properties (e.g. page title, canonical url etc.)
    /// </summary>
    [ConfigurationProperty("enableSingleItemModeWidgetsBackwardCompatibilityMode", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableSingleItemModeWidgetsBackwardCompatibilityModeDescription", Title = "EnableSingleItemModeWidgetsBackwardCompatibilityModeTitle")]
    public bool EnableSingleItemModeWidgetsBackwardCompatibilityMode
    {
      get => (bool) this["enableSingleItemModeWidgetsBackwardCompatibilityMode"];
      set => this["enableSingleItemModeWidgetsBackwardCompatibilityMode"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the settings for the default canonical url on static pages.
    /// </summary>
    /// <value>Settings for the default canonical url on static pages.</value>
    [ConfigurationProperty("pageDefaultCanonicalUrlSettings")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PageDefaultCanonicalUrlSettingsDescription", Title = "PageDefaultCanonicalUrlSettingsTitle")]
    public virtual PageDefaultCanonicalUrlSettingsElement PageDefaultCanonicalUrlSettings
    {
      get => (PageDefaultCanonicalUrlSettingsElement) this["pageDefaultCanonicalUrlSettings"];
      set => this["pageDefaultCanonicalUrlSettings"] = (object) value;
    }

    /// <summary>
    /// Gets or sets the settings for the canonical url resolver mode
    /// </summary>
    [ConfigurationProperty("canonicalUrlResolverMode", DefaultValue = CanonicalUrlResolverMode.Auto)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CanonicalUrlResolverModeDescription", Title = "CanonicalUrlResolverModeTitle")]
    public CanonicalUrlResolverMode CanonicalUrlResolverMode
    {
      get => (CanonicalUrlResolverMode) this["canonicalUrlResolverMode"];
      set => this["canonicalUrlResolverMode"] = (object) value;
    }

    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct PropNames
    {
      public const string EnableSingleItemModeWidgetsBackwardCompatibilityMode = "enableSingleItemModeWidgetsBackwardCompatibilityMode";
      public const string CanonicalUrlResolverMode = "canonicalUrlResolverMode";
    }
  }
}
