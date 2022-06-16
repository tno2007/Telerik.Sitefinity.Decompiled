// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Update.LicenseUpdateResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Licensing.Update
{
  internal class LicenseUpdateResult
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Update.LicenseUpdateResult" /> class.
    /// </summary>
    /// <param name="status">The status.</param>
    public LicenseUpdateResult(LicenseUpdateStatus status)
      : this(status, (string) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Licensing.Update.LicenseUpdateResult" /> class.
    /// </summary>
    /// <param name="status">The update status.</param>
    /// <param name="licenseData">The encrypted license data.</param>
    public LicenseUpdateResult(LicenseUpdateStatus status, string licenseData)
    {
      this.Status = status;
      this.LicenseData = licenseData;
    }

    /// <summary>Gets or the status of the update.</summary>
    /// <value>The status.</value>
    public LicenseUpdateStatus Status { get; private set; }

    /// <summary>Gets the encrypted license data.</summary>
    /// <value>The license data.</value>
    public string LicenseData { get; private set; }
  }
}
