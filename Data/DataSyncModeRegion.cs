// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.DataSyncModeRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.OA;

namespace Telerik.Sitefinity.Data
{
  internal class DataSyncModeRegion : IDisposable
  {
    private readonly DataProviderBase provider;
    private readonly SitefinityOAContext context;
    private readonly bool prevMode;
    private readonly bool prevSuppressEvents;
    private readonly bool suppressEventsOnly;

    public DataSyncModeRegion(IManager manager)
      : this(manager, false)
    {
    }

    public DataSyncModeRegion(IManager manager, bool suppressEventsOnly)
    {
      this.provider = manager != null ? manager.Provider : throw new ArgumentNullException(nameof (manager));
      if (this.provider == null)
        return;
      if (!suppressEventsOnly && this.provider is IOpenAccessDataProvider provider)
      {
        this.context = provider.GetContext();
        if (this.context != null)
        {
          this.prevMode = this.context.ContextOptions.EnableDataSynchronization;
          this.context.ContextOptions.EnableDataSynchronization = true;
        }
      }
      this.prevSuppressEvents = this.provider.SuppressEvents;
      this.provider.SuppressEvents = true;
    }

    public void Dispose()
    {
      if (this.provider == null)
        return;
      this.provider.SuppressEvents = this.prevSuppressEvents;
      if (this.context == null)
        return;
      this.context.ContextOptions.EnableDataSynchronization = this.prevMode;
    }
  }
}
