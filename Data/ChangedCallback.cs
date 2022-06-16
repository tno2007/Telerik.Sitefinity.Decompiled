// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ChangedCallback
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Represents a call back delegate that is invoked when the tracked item has changed.
  /// </summary>
  /// <param name="caller">
  /// A reference to the cache dependency making the callback.
  /// </param>
  /// <param name="trackedItem">The tracked item.</param>
  public delegate void ChangedCallback(
    ICacheDependencyHandler caller,
    Type itemType,
    string itemKey);
}
