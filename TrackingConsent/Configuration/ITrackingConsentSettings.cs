// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.Configuration.ITrackingConsentSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.TrackingConsent.Configuration
{
  /// <summary>Holds settings for tracking consent.</summary>
  public interface ITrackingConsentSettings
  {
    /// <summary>
    /// Gets or sets a value indicating whether user consent is needed.
    /// </summary>
    /// <value>True if consent is needed, otherwise false.</value>
    bool ConsentIsRequired { get; set; }

    /// <summary>
    /// Gets or sets a value relative path of consent dialog file.
    /// </summary>
    /// <value>Relative path of consent dialog file.</value>
    string ConsentDialog { get; set; }
  }
}
