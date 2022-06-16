// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.Cache.NavigationOutputCacheVariationSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.NavigationControls.Cache
{
  /// <summary>Type of navigation control output cache</summary>
  [Serializable]
  public class NavigationOutputCacheVariationSettings
  {
    /// <summary>
    /// Gets or sets a value indicating whether to vary the output cache by authentication status
    /// </summary>
    public bool VaryByAuthenticationStatus { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to vary the output cache by user's roles
    /// </summary>
    public bool VaryByUserRoles { get; set; }
  }
}
