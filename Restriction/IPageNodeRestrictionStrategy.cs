// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Restriction.IPageNodeRestrictionStrategy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Restriction
{
  /// <summary>
  /// Defines a strategy, which verifies whether given page node is restricted.
  /// </summary>
  internal interface IPageNodeRestrictionStrategy : IRestrictionStrategy
  {
    /// <summary>
    /// Determines whether the specified page node is restricted.
    /// </summary>
    /// <param name="pageNodeId">The page node id.</param>
    /// <returns>Whether item is restricted.</returns>
    bool IsRestricted(Guid pageNodeId);
  }
}
