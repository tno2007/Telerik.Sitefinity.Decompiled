// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationPriority
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>Defines the content location relative priority.</summary>
  public enum ContentLocationPriority
  {
    /// <summary>
    /// Items with this level will have the lowest priority no matter of their type.
    /// </summary>
    Lowest = -100, // 0xFFFFFF9C
    /// <summary>Items from the same type will be treated equally.</summary>
    Default = 0,
    /// <summary>
    /// Items with this level will have the highest priority no matter of their type.
    /// </summary>
    Highest = 100, // 0x00000064
  }
}
