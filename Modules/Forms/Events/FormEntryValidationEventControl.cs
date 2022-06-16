// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEntryValidationEventControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>Represents a form control and its value.</summary>
  public class FormEntryValidationEventControl : 
    IFormEntryValidationEventControl,
    IFormEntryEventControlBase
  {
    /// <summary>Gets the name of the field.</summary>
    /// <value>The name of the field.</value>
    public string FieldName { get; internal set; }

    /// <summary>Gets the value.</summary>
    /// <value>The value.</value>
    public object Value { get; internal set; }

    /// <summary>Gets the old value.</summary>
    /// <value>The old value.</value>
    public object OldValue { get; internal set; }

    /// <summary>Gets the form entry control type.</summary>
    /// <value>The type.</value>
    public FormEntryEventControlType Type { get; internal set; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    public string Title { get; set; }
  }
}
