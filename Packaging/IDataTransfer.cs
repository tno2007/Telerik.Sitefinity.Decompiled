// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Packaging.IDataTransfer
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Packaging.Events;

namespace Telerik.Sitefinity.Packaging
{
  /// <summary>
  /// Interface providing methods to Export and Import data in Sitefinity
  /// </summary>
  internal interface IDataTransfer
  {
    /// <summary>Occurs when [item imported].</summary>
    event EventHandler<ItemImportedEventArgs> ItemImported;

    /// <summary>
    /// Determines whether data transfer is available for current site.
    /// </summary>
    /// <returns>Value indicating whether current data transfer is available for current site.</returns>
    bool IsAvailableForCurrentSite();
  }
}
