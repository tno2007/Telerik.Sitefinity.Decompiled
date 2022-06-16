// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Update.LicenseUpdateStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Licensing.Web.Services;

namespace Telerik.Sitefinity.Licensing.Update
{
  /// <summary>
  /// Default implementation of the <see cref="T:Telerik.Sitefinity.Licensing.Update.ILicenseUpdateStrategy" /> interface.
  /// This class is used for updating an expired recurrent billing license.
  /// </summary>
  internal class LicenseUpdateStrategy : ILicenseUpdateStrategy
  {
    /// <summary>
    /// Updates the updated license.
    /// Returns the encrypted updated license or null if there isn't a newer one.
    /// </summary>
    /// <param name="currentLicense">The current license.</param>
    /// <returns>The encrypted data of the updated license or null if one isn't available.</returns>
    public LicenseUpdateResult UpdateLicense(LicenseState currentLicense)
    {
      if (this.ReloadLicenseFromFile().LicenseInfo.IssueDate > currentLicense.LicenseInfo.IssueDate)
        return new LicenseUpdateResult(LicenseUpdateStatus.AlreadyUpdated);
      string licenseFromService = this.GetLicenseFromService(currentLicense);
      return string.IsNullOrEmpty(licenseFromService) ? new LicenseUpdateResult(LicenseUpdateStatus.Failure) : new LicenseUpdateResult(LicenseUpdateStatus.Success, licenseFromService);
    }

    /// <summary>Reloads the license from the file system.</summary>
    /// <returns>The reloaded license.</returns>
    private LicenseState ReloadLicenseFromFile() => LicenseState.ParseLicense(LicenseState.LoadLicenseData());

    /// <summary>Gets the license from service.</summary>
    /// <param name="currentLicense">The current license.</param>
    /// <returns>The license from the license service or null if there isn't a newer one.</returns>
    private string GetLicenseFromService(LicenseState currentLicense)
    {
      try
      {
        string sitefinityLicense = LicenseUpdater.GetSitefinityLicense(currentLicense.LicenseInfo.LicenseId);
        if (LicenseState.ParseLicense(sitefinityLicense).LicenseInfo.IssueDate > currentLicense.LicenseInfo.IssueDate)
          return sitefinityLicense;
      }
      catch (Exception ex)
      {
        Log.Write((object) ex, ConfigurationPolicy.ErrorLog);
      }
      return (string) null;
    }
  }
}
