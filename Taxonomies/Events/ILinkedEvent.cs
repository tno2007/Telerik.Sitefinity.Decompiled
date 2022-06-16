// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Taxonomies.Events.ILinkedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Taxonomies.Events
{
  /// <summary>
  /// A contract for events notifying for created link operations between an item and a <see cref="!:Site" />.
  /// </summary>
  public interface ILinkedEvent : ILinkEvent, IDataEvent, IEvent
  {
  }
}
