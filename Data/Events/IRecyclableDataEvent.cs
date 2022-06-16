// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Events.IRecyclableDataEvent
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Services.Events;

namespace Telerik.Sitefinity.Data.Events
{
  /// <summary>
  /// An interface used to distinguish various actions with
  /// data items that support Recycle Bin.
  /// </summary>
  public interface IRecyclableDataEvent : IDataEvent, IEvent
  {
    /// <summary>
    /// Gets or sets whether a delete event means permanent deletion
    /// or the item was marked as deleted (a.k.a. sent to the Recycle Bin).
    /// </summary>
    /// <value>The recycle bin action.</value>
    RecycleBinAction RecycleBinAction { get; set; }

    /// <summary>
    /// Gets or sets the affected languages for the executed <see cref="P:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent.RecycleBinAction" />.
    /// </summary>
    /// <value>The affected languages for the executed <see cref="P:Telerik.Sitefinity.Data.Events.IRecyclableDataEvent.RecycleBinAction" />.</value>
    string[] AffectedLanguages { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the action performed on the item is the result of a parent related action.
    /// For example the parent was moved to the Recycle bin and for that reason the child is moved to the Recycle bin as well.
    /// </summary>
    /// <value><c>true</c>if the item was affected by a parent related action, otherwise <c>false</c></value>
    bool WithParent { get; set; }
  }
}
