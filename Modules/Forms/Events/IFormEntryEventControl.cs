// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// Represents a form control and its value in an <see cref="!:IFormEntryCreatedEvent.Controls" /> collection.
  /// </summary>
  public interface IFormEntryEventControl : IFormEntryEventControlBase
  {
    /// <summary>Gets the ID of the control.</summary>
    Guid Id { get; }

    /// <summary>Gets the ID of the sibling control.</summary>
    Guid SiblingId { get; }

    /// <summary>
    /// Gets the text representation of the <see cref="!:Value" />, if any and <c>null</c> otherwise.
    /// </summary>
    string Text { get; }

    /// <summary>Gets the type of the control.</summary>
    FormEntryEventControlType Type { get; }

    /// <summary>Gets the title of the control.</summary>
    string Title { get; }
  }
}
