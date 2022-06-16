// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.Enums.CanonicalUrlResolverMode
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations.Enums
{
  /// <summary>Defines canonical url resolver mode</summary>
  public enum CanonicalUrlResolverMode
  {
    /// <summary>
    /// Auto mode - resolve domain based on cache profile parameters (varyByHost) and domain aliases
    /// </summary>
    Auto,
    /// <summary>Dynamic - resolve domain based on current request</summary>
    Dynamic,
    /// <summary>
    /// Static - resolve by site domain, ignoring on current request
    /// </summary>
    Static,
  }
}
