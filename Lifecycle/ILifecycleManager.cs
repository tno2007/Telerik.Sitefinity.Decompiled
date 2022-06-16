// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Lifecycle.ILifecycleManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.Lifecycle
{
  /// <summary>
  /// Represents interface for managers that support lifecycle
  /// </summary>
  public interface ILifecycleManager : IManager, IDisposable, IProviderResolver, ILanguageDataManager
  {
    /// <summary>
    /// Returns the decorator that encapsulates the lifecycle logic &amp; opearations - check in/out/publish/unpublish/etc
    /// </summary>
    ILifecycleDecorator Lifecycle { get; }
  }
}
