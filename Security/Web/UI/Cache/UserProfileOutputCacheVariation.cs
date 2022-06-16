// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.Cache.UserProfileOutputCacheVariation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Security.Web.UI.Cache
{
  [Serializable]
  internal class UserProfileOutputCacheVariation : CustomOutputCacheVariationBase
  {
    [NonSerialized]
    private bool? noCache;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.UI.Cache.UserProfileOutputCacheVariation" /> class.
    /// </summary>
    public UserProfileOutputCacheVariation() => this.Key = "sf-user-profile-view";

    /// <inheritdoc />
    public override bool NoCache => this.noCache.HasValue ? this.noCache.Value : ClaimsManager.GetCurrentIdentity().IsAuthenticated;

    /// <inheritdoc />
    public override string GetValue() => string.Empty;

    internal void ForceNoCache(bool noCache = true) => this.noCache = new bool?(noCache);
  }
}
