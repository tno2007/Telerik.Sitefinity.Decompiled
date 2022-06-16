// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.Web.Services.LicenseRequestResult
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Licensing.Web.Services
{
  /// <summary>Possible outcomes for a license request</summary>
  public enum LicenseRequestResult
  {
    /// <summary>
    /// 
    /// </summary>
    Ok,
    /// <summary>License Id not found in orders database</summary>
    InvalidLicenseId,
    /// <summary>no such version exists</summary>
    InvalidProductVersion,
    /// <summary>
    /// this product version is not licensed for the license holder
    /// </summary>
    UnlicensedProductVersion,
  }
}
