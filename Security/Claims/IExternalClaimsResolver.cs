// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.IExternalClaimsResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>Represents an external claims resolver.</summary>
  internal interface IExternalClaimsResolver
  {
    /// <summary>
    /// Resolves the external mapped claims and adds them to the identity.
    /// </summary>
    /// <param name="identity">The identity.</param>
    /// <param name="user">The user.</param>
    void AddExternalClaims(ClaimsIdentity identity, User user);
  }
}
