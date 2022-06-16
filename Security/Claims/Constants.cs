// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.Constants
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>Constatns related to Claims processing.</summary>
  public static class Constants
  {
    public const string IssuerName = "IssuerName";
    public const string SigningCertificateName = "SigningCertificateName";
    public const string EncryptingCertificateName = "EncryptingCertificateName";
    public const string LocalService = "Sitefinity/Authenticate";
    public const string AuthenticationErrorHeader = "X-Authentication-Error";
    /// <summary>
    /// Contains the URL parameter used to the location
    /// where the client will be redirected automatically after the authentication is completed successfully.
    /// </summary>
    public const string ReturnUriSts = "redirect_uri";
  }
}
