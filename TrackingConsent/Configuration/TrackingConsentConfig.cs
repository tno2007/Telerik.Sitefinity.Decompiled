// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.Configuration.TrackingConsentConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Configuration;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.TrackingConsent.Configuration
{
  /// <summary>Tracking - test, test, test</summary>
  [ObjectInfo(typeof (ConfigDescriptions), Description = "TrackingConsentConfigDescriptions", Title = "TrackingConsentConfigDescriptions")]
  public class TrackingConsentConfig : ConfigElement, ITrackingConsentSettings
  {
    internal const string ConsentIsRequiredPropName = "consentIsRequired";
    internal const string ConsentDialogPropName = "consentDialog";
    internal const string DomainOverridesPropName = "domainOverrides";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.TrackingConsent.Configuration.TrackingConsentConfig" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public TrackingConsentConfig(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether user consent is needed.
    /// </summary>
    /// <value>True if consent is needed, otherwise false.</value>
    [ConfigurationProperty("consentIsRequired")]
    [DescriptionResource(typeof (ConfigDescriptions), "ConsentIsRequiredDescriptions")]
    [TypeConverter(typeof (BooleanConverter))]
    [DataMember]
    public virtual bool ConsentIsRequired
    {
      get => (bool) this["consentIsRequired"];
      set => this["consentIsRequired"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value relative path of consent dialog file.
    /// </summary>
    /// <value>Relative path of consent dialog file.</value>
    [ConfigurationProperty("consentDialog")]
    [DescriptionResource(typeof (ConfigDescriptions), "ConsentDialogDescriptions")]
    [TypeConverter(typeof (StringConverter))]
    [DataMember]
    public virtual string ConsentDialog
    {
      get => (string) this["consentDialog"];
      set => this["consentDialog"] = (object) value;
    }

    /// <summary>Gets or sets additional domains.</summary>
    [ConfigurationProperty("domainOverrides")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "DomainOverridesDescriptions", Title = "DomainOverridesDescriptions")]
    public virtual ConfigElementDictionary<string, TrackingConsentSettingsElement> DomainOverrides
    {
      get => (ConfigElementDictionary<string, TrackingConsentSettingsElement>) this["domainOverrides"];
      set => this["domainOverrides"] = (object) value;
    }
  }
}
