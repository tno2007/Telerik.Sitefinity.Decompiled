// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncExportTransaction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  internal class SiteSyncExportTransaction : 
    SiteSyncTransaction,
    ISiteSyncExportTransaction,
    ISiteSyncTransaction
  {
    private WrapperObject mainItem;

    /// <inheritdoc />
    public ISiteSyncLogEntry LogEntry { get; set; }

    /// <inheritdoc />
    public Exception Exception { get; set; }

    /// <inheritdoc />
    public WrapperObject MainItem
    {
      get => this.mainItem == null ? this.Items.FirstOrDefault<WrapperObject>() : this.mainItem;
      set => this.mainItem = value;
    }
  }
}
