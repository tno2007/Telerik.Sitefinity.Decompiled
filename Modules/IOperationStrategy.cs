// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.IOperationStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules
{
  /// <summary>
  /// Interface for strategies that work with <see cref="T:Telerik.Sitefinity.Modules.ItemOperation" />.
  /// </summary>
  internal interface IOperationStrategy
  {
    /// <summary>Gets the item operations.</summary>
    /// <param name="item">The item.</param>
    /// <param name="itemId">The item ID.</param>
    /// <param name="itemType">The item type.</param>
    /// <param name="culture">The culture.</param>
    /// <param name="provider">The provider.</param>
    /// <returns>The item operations.</returns>
    IEnumerable<ItemOperation> GetOperations(
      object item,
      Guid itemId,
      Type itemType,
      string culture,
      string provider);
  }
}
