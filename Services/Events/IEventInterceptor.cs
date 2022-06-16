// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Events.IEventInterceptor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Events
{
  /// <summary>
  /// Defines the common interface for components responsible for interpreting <see cref="T:Telerik.Sitefinity.Services.Events.IEvent" />s.
  /// </summary>
  public interface IEventInterceptor
  {
    /// <summary>
    /// Subscribes this instance for <see cref="T:Telerik.Sitefinity.Services.Events.IEvent" />s.
    /// </summary>
    void Subscribe();
  }
}
