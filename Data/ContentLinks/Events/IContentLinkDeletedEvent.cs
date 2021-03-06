// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.Events.IContentLinkDeletedEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.ContentLinks.Events
{
  /// <summary>
  /// An interface for events notifying for delete operations performed on instances of type <see cref="T:Telerik.Sitefinity.Model.ContentLinks.ContentLink" />.
  /// </summary>
  public interface IContentLinkDeletedEvent : IContentLinkEvent, IEvent
  {
    /// <summary>Gets the deletion date.</summary>
    DateTime DeletionDate { get; }
  }
}
