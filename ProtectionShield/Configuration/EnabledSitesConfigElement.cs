// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.Configuration.EnabledSitesConfigElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.ProtectionShield.Configuration
{
  /// <summary>
  /// Represents a type that indicates for which site the e protection shield is enabled.
  /// </summary>
  internal class EnabledSitesConfigElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.Configuration.EnabledSitesConfigElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public EnabledSitesConfigElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets the site name.</summary>
    [ConfigurationProperty("siteName", IsKey = true)]
    [ObjectInfo(typeof (ProtectionShieldResources), Description = "SiteNameDescription", Title = "SiteNameLabel")]
    public string SiteName
    {
      get => (string) this["siteName"];
      set => this["siteName"] = (object) value;
    }

    private class PropertyNames
    {
      public const string SiteName = "siteName";
    }
  }
}
