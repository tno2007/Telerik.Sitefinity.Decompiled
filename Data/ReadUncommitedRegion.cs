// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ReadUncommitedRegion
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data
{
  internal class ReadUncommitedRegion : IDisposable
  {
    private DataProviderBase provider;
    private SitefinityOAContext context;
    private IsolationLevel? prevMode;
    private bool isGlobalRegion;

    public ReadUncommitedRegion()
      : this((IManager) null)
    {
    }

    public ReadUncommitedRegion(IManager manager)
    {
      if (manager == null)
      {
        this.isGlobalRegion = true;
        if (SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Items == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) "IsolationLevel"] = (object) IsolationLevel.ReadUncommitted;
      }
      else
        ReadUncommitedRegion.Set(manager, this);
    }

    public void Dispose()
    {
      if (this.provider != null)
      {
        if (this.context == null || !this.prevMode.HasValue)
          return;
        this.context.ContextOptions.IsolationLevel = this.prevMode;
      }
      else
      {
        if (!this.isGlobalRegion || SystemManager.CurrentHttpContext == null || SystemManager.CurrentHttpContext.Items == null)
          return;
        SystemManager.CurrentHttpContext.Items[(object) "IsolationLevel"] = (object) null;
      }
    }

    internal static void Set(IManager manager, ReadUncommitedRegion container = null)
    {
      DataProviderBase dataProviderBase = manager != null ? manager.Provider : throw new ArgumentNullException(nameof (manager));
      if (dataProviderBase == null || !(dataProviderBase is IOpenAccessDataProvider provider))
        return;
      SitefinityOAContext context = provider.GetContext();
      if (context == null)
        return;
      IsolationLevel? isolationLevel1 = context.ContextOptions.IsolationLevel;
      IsolationLevel isolationLevel2 = IsolationLevel.ReadUncommitted;
      if (isolationLevel1.GetValueOrDefault() == isolationLevel2 & isolationLevel1.HasValue)
        return;
      if (container != null)
      {
        container.provider = dataProviderBase;
        container.context = context;
        container.prevMode = context.ContextOptions.IsolationLevel;
      }
      context.ContextOptions.IsolationLevel = new IsolationLevel?(IsolationLevel.ReadUncommitted);
    }
  }
}
