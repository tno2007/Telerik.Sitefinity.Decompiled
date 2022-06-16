// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ISiteSyncExportTransaction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  /// <summary>
  /// An group of exported items which are transferred and imported, as much as possible, in a single transaction.
  /// It usually consists of a high-level (main) item with all related lower-level items.
  /// </summary>
  /// <remarks>
  /// To obtain a new instance of the default implementation call <c>ObjectFactory.Resolve&lt;ISiteSyncExportTransaction&gt;()</c>.
  /// </remarks>
  public interface ISiteSyncExportTransaction : ISiteSyncTransaction
  {
    /// <summary>The pending items log entry.</summary>
    ISiteSyncLogEntry LogEntry { get; set; }

    /// <summary>
    /// An error occurred during processing (e.g. data loading error); <c>null</c> if none.
    /// </summary>
    Exception Exception { get; set; }

    /// <summary>The main object for the transaction.</summary>
    WrapperObject MainItem { get; set; }
  }
}
