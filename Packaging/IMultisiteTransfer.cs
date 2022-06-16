// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.IMultisiteTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>
  /// Marks package transfer for objects shared across sites
  /// </summary>
  internal interface IMultisiteTransfer
  {
    /// <summary>
    /// Activates the specified source name for the given site.
    /// </summary>
    /// <param name="sourceName">Name of the source.</param>
    /// <param name="siteId">The site identifier.</param>
    void Activate(string sourceName, Guid siteId);

    /// <summary>Activates the specified items for the given site.</summary>
    /// <param name="itemLinks">The collection of item links.</param>
    /// <param name="siteId">The site identifier.</param>
    void Activate(ICollection<ItemLink> itemLinks, Guid siteId);

    /// <summary>
    /// Deactivates the specified source name for the given site.
    /// </summary>
    /// <param name="sourceName">Name of the source.</param>
    /// <param name="siteId">The site identifier.</param>
    void Deactivate(string sourceName, Guid siteId);
  }
}
