// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.IFormEntryEditModeProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Modules.Forms.Events;

namespace Telerik.Sitefinity.Modules.Forms
{
  /// <summary>
  /// Defines method for getting the current forms mode.
  /// Forms control will use this mode to determine the visual (hidden/read only) state of each control field.
  /// The rendered forms field will be toggled in a read only and/or hidden state if there is a match between the value return from this method implementation and
  /// those defined in the advanced settings of the form field control.
  /// </summary>
  public interface IFormEntryEditModeProvider
  {
    /// <summary>
    /// Gets the current mode that forms control will used to render its fields.
    /// </summary>
    /// <param name="formEntryEditContext">The form entry edit context.</param>
    /// <returns>Get the current mode.</returns>
    string GetCurrentMode(IFormEntryEditContext formEntryEditContext);
  }
}
