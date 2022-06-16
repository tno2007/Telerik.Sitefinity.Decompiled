// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControlBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// A base interface for the events that represent a FormEntryControl.
  /// </summary>
  public interface IFormEntryEventControlBase
  {
    /// <summary>
    /// Gets the name of the meta field that stores the value of the field control.
    /// </summary>
    string FieldName { get; }

    /// <summary>Gets the value of the (meta) field.</summary>
    object Value { get; }
  }
}
