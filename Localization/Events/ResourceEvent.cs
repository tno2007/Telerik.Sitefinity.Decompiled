// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.ResourceEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  internal class ResourceEvent : IEvent, IMultilingualEvent
  {
    public string ProviderName { get; set; }

    public string ClassId { get; set; }

    public string Language { get; set; }

    public string Action { get; set; }

    public Type ItemType { get; set; }

    public string Key { get; set; }

    public string Origin { get; set; }
  }
}
