// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventClientControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Events
{
  /// <summary>
  /// An <see cref="T:Telerik.Sitefinity.Modules.Forms.Events.IFormEntryEventControl" /> which knows its field control name.
  /// </summary>
  internal interface IFormEntryEventClientControl : 
    IFormEntryEventControl,
    IFormEntryEventControlBase
  {
    /// <summary>Gets the client control id</summary>
    string ClientID { get; }

    /// <summary>Gets the control type</summary>
    string ControlType { get; }
  }
}
