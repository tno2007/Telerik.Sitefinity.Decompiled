// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IHasEditCommands
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// ZoneEditor use this interface to define whether to show custom commands in the RadDock for a widget.
  /// If a widget doesn't implement the interface it will have the default set of commands.
  /// </summary>
  public interface IHasEditCommands
  {
    /// <summary>Gets the commands.</summary>
    /// <value>The commands.</value>
    IList<WidgetMenuItem> Commands { get; }
  }
}
