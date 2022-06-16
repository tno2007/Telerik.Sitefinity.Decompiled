// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.Configuration.TrackingConsentSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings;

namespace Telerik.Sitefinity.TrackingConsent.Configuration
{
  /// <summary>
  /// Represents the contract for tracking consent settings.
  /// </summary>
  [DataContract]
  public class TrackingConsentSettingsContract : ISettingsDataContract
  {
    /// <inheritdoc />
    public TrackingConsentSettingsContract() => this.TrackingConsents = (IEnumerable<TrackingConsentDto>) new List<TrackingConsentDto>();

    /// <inheritdoc />
    [DataMember]
    public IEnumerable<TrackingConsentDto> TrackingConsents { get; set; }

    /// <inheritdoc />
    public void LoadDefaults(bool forEdit = false) => this.TrackingConsents = Config.Get<SystemConfig>().TrackingConsentConfig.ToTrackingConsentDto();

    /// <inheritdoc />
    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      SystemConfig section = manager.GetSection<SystemConfig>();
      section.TrackingConsentConfig.Update(this.TrackingConsents);
      manager.SaveSection((ConfigSection) section);
    }
  }
}
