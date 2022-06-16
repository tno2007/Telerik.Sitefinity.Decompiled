// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Events.LoginEventBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Web.Events
{
  /// <inheritdoc />
  internal class LoginEventBase : ILoginEventBase, IEvent
  {
    /// <inheritdoc />
    public string UserId { get; set; }

    /// <inheritdoc />
    public string Username { get; set; }

    /// <inheritdoc />
    public string ProviderName { get; set; }

    /// <inheritdoc />
    public string IpAddress { get; set; }

    /// <inheritdoc />
    public bool IsBackendUser { get; set; }

    /// <inheritdoc />
    public string Email { get; set; }

    /// <inheritdoc />
    public string Origin { get; set; }
  }
}
