// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SymmetricSigningCredentials
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.IdentityModel.Tokens;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>A singning credentials that use symmetric algorithm</summary>
  public class SymmetricSigningCredentials : SigningCredentials
  {
    public SymmetricSigningCredentials(byte[] key)
      : base((SecurityKey) new InMemorySymmetricSecurityKey(key), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256", "http://www.w3.org/2001/04/xmlenc#sha256")
    {
    }
  }
}
