// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Events.RoleEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Security.Events
{
  /// <inheritdoc />
  internal class RoleEvent : IRoleEvent, IDataEvent, IEvent
  {
    /// <inheritdoc />
    public string Action { get; set; }

    /// <inheritdoc />
    public Type ItemType { get; set; }

    /// <inheritdoc />
    public Guid ItemId { get; set; }

    /// <inheritdoc />
    public string ProviderName { get; set; }

    /// <inheritdoc />
    public string Origin { get; set; }

    /// <inheritdoc />
    public string RoleName { get; set; }
  }
}
