// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.Configuration.TrackingConsentConfigExtensionMethods
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;

namespace Telerik.Sitefinity.TrackingConsent.Configuration
{
  internal static class TrackingConsentConfigExtensionMethods
  {
    public static void Update(
      this TrackingConsentConfig config,
      IEnumerable<TrackingConsentDto> allDto)
    {
      config.DomainOverrides.Clear();
      foreach (TrackingConsentDto trackingConsentDto in allDto)
      {
        if (trackingConsentDto.IsMaster)
        {
          config.ConsentIsRequired = trackingConsentDto.ConsentIsRequired;
          config.ConsentDialog = trackingConsentDto.ConsentDialog;
        }
        else
          config.DomainOverrides.Add(new TrackingConsentSettingsElement((ConfigElement) config.DomainOverrides)
          {
            Domain = trackingConsentDto.Domain,
            ConsentIsRequired = trackingConsentDto.ConsentIsRequired,
            ConsentDialog = trackingConsentDto.ConsentDialog
          });
      }
    }

    public static IEnumerable<TrackingConsentDto> ToTrackingConsentDto(
      this TrackingConsentConfig config)
    {
      List<TrackingConsentDto> trackingConsentDto = new List<TrackingConsentDto>();
      trackingConsentDto.Add(new TrackingConsentDto()
      {
        Domain = string.Empty,
        ConsentIsRequired = config.ConsentIsRequired,
        ConsentDialog = string.IsNullOrEmpty(config.ConsentDialog) ? "~/App_Data/Sitefinity/TrackingConsent/consentDialog.html" : config.ConsentDialog,
        IsMaster = true
      });
      foreach (TrackingConsentSettingsElement domainOverride in (ConfigElementCollection) config.DomainOverrides)
        trackingConsentDto.Add(new TrackingConsentDto()
        {
          Domain = domainOverride.Domain,
          ConsentIsRequired = domainOverride.ConsentIsRequired,
          ConsentDialog = domainOverride.ConsentDialog,
          IsMaster = false
        });
      return (IEnumerable<TrackingConsentDto>) trackingConsentDto;
    }
  }
}
