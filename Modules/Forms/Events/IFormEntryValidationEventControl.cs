// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryValidationEventControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>Represents a form control and its value.</summary>
  public interface IFormEntryValidationEventControl : IFormEntryEventControlBase
  {
    /// <summary>Gets the name of the field.</summary>
    /// <value>The name of the field.</value>
    new string FieldName { get; }

    /// <summary>Gets the value.</summary>
    /// <value>The value.</value>
    new object Value { get; }

    /// <summary>Gets the old value.</summary>
    /// <value>The old value.</value>
    object OldValue { get; }

    /// <summary>Gets the type.</summary>
    /// <value>The type.</value>
    FormEntryEventControlType Type { get; }

    /// <summary>Gets or sets the title.</summary>
    /// <value>The title.</value>
    string Title { get; set; }
  }
}
