// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.IsAccessibleArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Security.Claims;
using System.Web;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Event arguments for IsNodeAccessible event.</summary>
  public class IsAccessibleArgs : EventArgs
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.IsAccessibleArgs" /> class.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="principal">The principal.</param>
    /// <param name="isAccessible">if set to <c>true</c> [is accessible].</param>
    public IsAccessibleArgs(SiteMapNode node, ClaimsPrincipal principal, bool isAccessible)
    {
      this.Node = node;
      this.Principal = principal;
      this.IsAccessible = isAccessible;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="T:System.Web.SiteMapNode" /> instance is accessible to the current user.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this instance is accessible; otherwise, <c>false</c>.
    /// </value>
    public bool IsAccessible { get; set; }

    /// <summary>
    /// Gets the <see cref="T:System.Web.SiteMapNode" /> to be evaluated.
    /// </summary>
    /// <value>The node.</value>
    public SiteMapNode Node { get; private set; }

    /// <summary>
    /// Gets the <see cref="T:System.Security.Claims.ClaimsPrincipal" /> representing the current request.
    /// </summary>
    /// <value>The principal.</value>
    public ClaimsPrincipal Principal { get; private set; }
  }
}
