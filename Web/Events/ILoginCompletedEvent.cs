// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.ILoginCompletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <summary>
  /// An event containing information about the login attempt and it's result.
  /// </summary>
  public interface ILoginCompletedEvent : ILoginEventBase, IEvent
  {
    /// <summary>Gets the login result.</summary>
    /// <value>The login result.</value>
    UserLoggingReason LoginResult { get; }
  }
}
