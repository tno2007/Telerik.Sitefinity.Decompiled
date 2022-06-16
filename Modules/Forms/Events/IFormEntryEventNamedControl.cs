// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventNamedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// An <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl" /> which knows its field control name.
  /// </summary>
  public interface IFormEntryEventNamedControl : IFormEntryEventControl, IFormEntryEventControlBase
  {
    /// <summary>
    /// Gets the name of the field control (a.k.a. field name),
    /// not to be confused with the meta field name (<see cref="P:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControlBase.FieldName" />).
    /// </summary>
    string FieldControlName { get; }
  }
}
