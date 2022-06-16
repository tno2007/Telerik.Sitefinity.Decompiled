// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.UpgradeVersionInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>Possible upgrade version for this license</summary>
  [DataContract(Name = "UpgradeVersionInfo", Namespace = "http://services.telerik.com")]
  public class UpgradeVersionInfo
  {
    /// <summary>The product version number.</summary>
    [DataMember]
    public string ProductVersion { get; set; }

    /// <summary>The version description. Fixes, new features etc.</summary>
    [DataMember]
    public string Description { get; set; }

    /// <summary>
    /// Indicates whether the upgrade version is free for this license Id.
    /// </summary>
    [DataMember]
    public bool IsFreeUpgrade { get; set; }

    /// <summary>Gets or sets the available download links.</summary>
    /// <value>The download link.</value>
    [DataMember]
    public string[] DownloadLinks { get; set; }
  }
}
