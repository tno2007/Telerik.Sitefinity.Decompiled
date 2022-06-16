// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ItemLists.ClientCommand
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.ItemLists
{
  /// <summary>Client Command</summary>
  public struct ClientCommand
  {
    /// <summary>Id of the button that will fire a command</summary>
    public string ButtonId;
    /// <summary>Name of the command to be fired by the button</summary>
    public string CommandName;
    /// <summary>
    /// Determines whether the command item is a rad menu item
    /// </summary>
    public bool IsRadMenu;
    /// <summary>Name of the button</summary>
    public string ButtonName;
  }
}
