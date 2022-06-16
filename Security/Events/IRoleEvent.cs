// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.IRoleEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Events
{
  /// <summary>
  /// A contract for event notification containing information about modified roles.
  /// </summary>
  public interface IRoleEvent : IDataEvent, IEvent
  {
    /// <summary>The name of the role</summary>
    string RoleName { get; }
  }
}
