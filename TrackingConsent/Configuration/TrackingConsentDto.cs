// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.TrackingConsent.Configuration.TrackingConsentDto
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.TrackingConsent.Configuration
{
  /// <summary>Data transfer object</summary>
  [DataContract]
  public class TrackingConsentDto
  {
    /// <summary>Gets or sets a value of the consent domain.</summary>
    [DataMember]
    public string Domain { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether user consent is required.
    /// </summary>
    [DataMember]
    public bool ConsentIsRequired { get; set; }

    /// <summary>Gets or sets a value indicating dialog html.</summary>
    [DataMember]
    public string ConsentDialog { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether element is master.
    /// </summary>
    [DataMember]
    public bool IsMaster { get; set; }
  }
}
