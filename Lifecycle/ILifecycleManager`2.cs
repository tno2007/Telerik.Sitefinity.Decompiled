// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.ILifecycleManager`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// Represents interface for managers that support lifecycle with the live-based pattern.
  /// </summary>
  public interface ILifecycleManager<TItem, TDraft> : 
    IManager,
    IDisposable,
    IProviderResolver,
    ILanguageDataManager
    where TItem : class, ILifecycleDataItemLive
    where TDraft : class, ILifecycleDataItemDraft
  {
    /// <summary>Deletes the specified draft item.</summary>
    /// <param name="item"></param>
    void Delete(TDraft item);

    /// <summary>Creates a new draft object of the specified type.</summary>
    /// <returns></returns>
    TDraft CreateDraft();

    LifecycleDecorator<TItem, TDraft> Lifecycle { get; }

    /// <summary>Gets the keep temp draft after publish.</summary>
    /// <value>The keep temp after publish.</value>
    bool DeleteTempAfterPublish { get; }
  }
}
