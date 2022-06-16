// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentItemLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides information about a specific content item location.
  /// </summary>
  public interface IContentItemLocation : IContentLocationBase
  {
    /// <summary>Gets the absolute URL of the item location.</summary>
    /// <value>The item absolute URL.</value>
    string ItemAbsoluteUrl { get; }

    /// <summary>
    /// Gets whether the content item location is default(canonical) for the item.
    /// </summary>
    /// <value>The is canonical.</value>
    bool IsDefault { get; }
  }
}
