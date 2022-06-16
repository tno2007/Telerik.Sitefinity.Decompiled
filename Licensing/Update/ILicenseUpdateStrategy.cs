// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Update.ILicenseUpdateStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Licensing.Update
{
  /// <summary>
  /// An interface for the strategy used for updating an expired recurrent billing license.
  /// </summary>
  internal interface ILicenseUpdateStrategy
  {
    /// <summary>
    /// Updates the updated license.
    /// Returns the encrypted updated license or null if there isn't a newer one.
    /// </summary>
    /// <param name="currentLicense">The current license.</param>
    /// <returns>The encrypted data of the updated license or null if one isn't available.</returns>
    LicenseUpdateResult UpdateLicense(LicenseState currentLicense);
  }
}
