// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Strategies.IPushPipeOutput
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Publishing.Strategies
{
  /// <summary>
  /// Interface that allows additional operations to be done with pushed items in any <see cref="T:Telerik.Sitefinity.Publishing.Strategies.IDynamicPipe" /> that is <see cref="T:Telerik.Sitefinity.Publishing.IPushPipe" />
  /// </summary>
  public interface IPushPipeOutput
  {
    /// <summary>Applies additional operations to pushed items</summary>
    /// <param name="pipe">The current push pipe</param>
    /// <param name="itemsToRemove">Items marked as removed</param>
    /// <param name="iitemsToAdd">Items marked as added</param>
    void PushItems(
      object pipe,
      IEnumerable<IPublishingObject> itemsToRemove,
      IEnumerable<IPublishingObject> iitemsToAdd);
  }
}
