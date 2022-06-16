// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryOldValueControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// An <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl" /> which knows its old field control value.
  /// </summary>
  public interface IFormEntryOldValueControl : IFormEntryEventControl, IFormEntryEventControlBase
  {
    /// <summary>Gets the old value.</summary>
    /// <value>The old value.</value>
    object OldValue { get; }
  }
}
