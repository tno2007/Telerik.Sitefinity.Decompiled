// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SWTIssuerConfiguration
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IdentityModel.Configuration;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>A configuration for the SimpleWebToken sts issuer</summary>
  public class SWTIssuerConfiguration : SecurityTokenServiceConfiguration
  {
    public SWTIssuerConfiguration()
      : base(ClaimsManager.CurrentAuthenticationModule.GetIssuer())
    {
    }
  }
}
