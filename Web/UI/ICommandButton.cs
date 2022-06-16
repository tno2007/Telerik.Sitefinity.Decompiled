// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ICommandButton
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Interface to be implemented by all the toolbox items that act as buttons
  /// </summary>
  public interface ICommandButton
  {
    /// <summary>Gets or sets the name of the command.</summary>
    string CommandName { get; set; }

    /// <summary>Gets the client pageId of the button control</summary>
    string ButtonClientId { get; }

    /// <summary>
    /// Generates the command item and returns instantiated control.
    /// </summary>
    /// <returns>Instance of the command button</returns>
    Control GenerateCommandItem();
  }
}
