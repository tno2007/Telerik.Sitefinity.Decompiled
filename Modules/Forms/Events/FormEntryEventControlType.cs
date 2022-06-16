// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.FormEntryEventControlType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// Enumerates the different types of <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl" />s in an <see cref="!:IFormEntryCreatedEvent.Controls" /> collection.
  /// </summary>
  public enum FormEntryEventControlType
  {
    /// <summary>
    /// An <see cref="!:IFieldControl" /> instance with a value.
    /// </summary>
    FieldControl,
    /// <summary>The file field control</summary>
    FileFieldControl,
    /// <summary>Form section header control.</summary>
    SectionHeader,
    /// <summary>Additional instructional text control.</summary>
    InstructionalText,
  }
}
