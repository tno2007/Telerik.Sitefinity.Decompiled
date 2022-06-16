// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.SitefinityPrincipal
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Claims;
using System.Security.Principal;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Sitefinity principal</summary>
  public sealed class SitefinityPrincipal : ClaimsPrincipal
  {
    private SitefinityIdentity primaryIdentity;

    /// <summary>Gets the original principal.</summary>
    /// <value>The original principal.</value>
    public ClaimsPrincipal OriginalPrincipal { get; private set; }

    /// <summary>Gets the primary identity.</summary>
    public override IIdentity Identity => (IIdentity) this.primaryIdentity;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityPrincipal" /> class with the given identity.
    /// </summary>
    /// <param name="identity">The primary identity for the principal.</param>
    /// <exception cref="T:System.ArgumentNullException">If the <paramref name="identity" /> is null.</exception>
    public SitefinityPrincipal(ClaimsIdentity identity)
    {
      if (identity == null)
        throw new ArgumentNullException(nameof (identity));
      this.AddIdentity(identity);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.SitefinityPrincipal" /> class from the given principal.
    /// </summary>
    /// <param name="principal">The principal.</param>
    /// <param name="primaryIdentityLabel">The label to be used for the primary identity.</param>
    /// <exception cref="T:System.ArgumentNullException">If the provided <paramref name="principal" /> is null.</exception>
    public SitefinityPrincipal(ClaimsPrincipal principal, string primaryIdentityLabel = null)
    {
      this.OriginalPrincipal = principal != null ? principal : throw new ArgumentNullException(nameof (principal));
      bool flag = !string.IsNullOrEmpty(primaryIdentityLabel);
      foreach (ClaimsIdentity identity in principal.Identities)
      {
        SitefinityIdentity sitefinityIdentity = this.GetSitefinityIdentity(identity);
        if (flag && identity.Label == primaryIdentityLabel)
          this.primaryIdentity = sitefinityIdentity;
        this.AddIdentity((ClaimsIdentity) sitefinityIdentity);
      }
    }

    /// <summary>Adds a new identity to the principal.</summary>
    /// <param name="identity">The identity to add.</param>
    public override void AddIdentity(ClaimsIdentity identity)
    {
      SitefinityIdentity sitefinityIdentity = this.GetSitefinityIdentity(identity);
      base.AddIdentity((ClaimsIdentity) sitefinityIdentity);
      if (this.primaryIdentity != null)
        return;
      this.primaryIdentity = sitefinityIdentity;
    }

    private SitefinityIdentity GetSitefinityIdentity(ClaimsIdentity identity) => identity is SitefinityIdentity sitefinityIdentity ? sitefinityIdentity : new SitefinityIdentity(identity);
  }
}
