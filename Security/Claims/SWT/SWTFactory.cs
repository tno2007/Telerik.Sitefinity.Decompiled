// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.SWTFactory
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Security.Claims;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>Factory for Simple Web Tokens</summary>
  public class SWTFactory
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.SWT.SimpleWebToken" /> class from the given properties.
    /// </summary>
    /// <param name="principal">The principal. (required)</param>
    /// <param name="realm">The realm the token is issued to. If null or empty, the realm will be resolved from the current request.</param>
    /// <returns>An instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.SWT.SimpleWebToken" /> class.</returns>
    public virtual SimpleWebToken Build(ClaimsPrincipal principal, string realm) => ClaimsManager.BuildSimpleWebToken(principal, realm);
  }
}
