// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEntryEventControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// Contains information related to the form field and the form control.
  /// </summary>
  internal class FormEntryEventControl : 
    IFormEntryEventControl,
    IFormEntryEventControlBase,
    IFormEntryEventNamedControl,
    IFormEntryOldValueControl,
    IFormEntryEventClientControl
  {
    /// <inheritdoc />
    public Guid Id { get; set; }

    /// <inheritdoc />
    public string FieldName { get; set; }

    /// <inheritdoc />
    public string FieldControlName { get; set; }

    /// <inheritdoc />
    public FormEntryEventControlType Type { get; set; }

    /// <inheritdoc />
    public string Title { get; set; }

    /// <inheritdoc />
    public object Value { get; set; }

    /// <inheritdoc />
    public string Text { get; set; }

    /// <inheritdoc />
    public Guid SiblingId { get; set; }

    /// <inheritdoc />
    public object OldValue { get; set; }

    /// <inheritdoc />
    public string ClientID { get; set; }

    /// <inheritdoc />
    public string ControlType { get; set; }
  }
}
