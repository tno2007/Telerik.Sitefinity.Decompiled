// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.Events.IContentLinkEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.ContentLinks.Events
{
  /// <summary>
  /// An interface for events notifying for changes at instances of type <see cref="T:Telerik.Sitefinity.Model.ContentLinks.ContentLink" />.
  /// </summary>
  public interface IContentLinkEvent : IEvent
  {
    /// <summary>Gets the content link item between two data items.</summary>
    ContentLink Item { get; }

    /// <summary>Gets the user id.</summary>
    Guid UserId { get; }
  }
}
