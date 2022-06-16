// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.RunWithUserRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>
  /// Responsible for running set of operations with specific user.
  /// Once disposed, the context user is set again to the principal used before.
  /// </summary>
  internal class RunWithUserRegion : IDisposable
  {
    private const string AuthenticationType = "UserRegion";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Claims.RunWithUserRegion" /> class.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    public RunWithUserRegion(Guid userId)
    {
      this.PreviousPrincipal = HttpContext.Current.User;
      if (!(userId != Guid.Empty))
        return;
      this.SetContextUser(new UserManager().GetUser(userId));
    }

    private IPrincipal PreviousPrincipal { get; set; }

    /// <summary>
    /// Performs application-defined tasks associated with freeing, releasing,
    /// or resetting unmanaged resources.
    /// Restores the CurrentHttpContext.User to be the principal used before.
    /// </summary>
    void IDisposable.Dispose()
    {
      IPrincipal previousPrincipal = this.PreviousPrincipal;
      SystemManager.CurrentHttpContext.User = previousPrincipal;
      Thread.CurrentPrincipal = previousPrincipal;
      HttpContext.Current.User = previousPrincipal;
    }

    private void SetContextUser(User user)
    {
      SitefinityPrincipal sitefinityPrincipal = user != null ? new SitefinityPrincipal((ClaimsIdentity) new SitefinityIdentity(user, authenticationType: "UserRegion")) : throw new ArgumentNullException(nameof (user));
      SystemManager.CurrentHttpContext.User = (IPrincipal) sitefinityPrincipal;
      Thread.CurrentPrincipal = (IPrincipal) sitefinityPrincipal;
      HttpContext.Current.User = (IPrincipal) sitefinityPrincipal;
    }
  }
}
