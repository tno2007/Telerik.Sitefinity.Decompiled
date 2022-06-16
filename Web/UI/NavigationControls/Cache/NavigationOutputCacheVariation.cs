// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.Cache.NavigationOutputCacheVariation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.Cache
{
  /// <summary>Navigation output cache variation</summary>
  [Serializable]
  public class NavigationOutputCacheVariation : CustomOutputCacheVariationBase
  {
    [NonSerialized]
    private bool noCache;

    /// <summary>Gets or sets the cache settings.</summary>
    public NavigationOutputCacheVariationSettings Settings { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.NavigationControls.Cache.NavigationOutputCacheVariation" /> class.
    /// </summary>
    public NavigationOutputCacheVariation()
      : this((NavigationOutputCacheVariationSettings) null)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.NavigationControls.Cache.NavigationOutputCacheVariation" /> class.
    /// </summary>
    /// <param name="settings">The output cache variation settings</param>
    public NavigationOutputCacheVariation(NavigationOutputCacheVariationSettings settings)
    {
      if (settings == null)
        settings = new NavigationOutputCacheVariationSettings();
      this.Settings = settings;
      this.Key = "sf-navigation-view";
    }

    /// <inheritdoc />
    public override bool NoCache => this.Settings.VaryByAuthenticationStatus ? ClaimsManager.GetCurrentIdentity().IsAuthenticated : this.noCache;

    /// <inheritdoc />
    public override string GetValue()
    {
      if (!this.Settings.VaryByUserRoles)
        return string.Empty;
      IEnumerable<Guid> values = ClaimsManager.GetCurrentIdentity().Roles.OrderBy<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id)).Select<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id));
      return string.Join<Guid>(string.Empty, values).GetHashCode().ToString();
    }
  }
}
