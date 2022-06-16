// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.StateCommandItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Item to be inserted in States collection of StateToolboxItem
  /// </summary>
  public class StateCommandItem
  {
    /// <summary>Text of the item</summary>
    public string Text { get; set; }

    /// <summary>Name of the command to fire</summary>
    public string CommandName { get; set; }

    /// <summary>Url to navigate to</summary>
    public string Url { get; set; }

    /// <summary>Is the item checked or not</summary>
    public bool IsChecked { get; set; }

    /// <summary>Css class to apply to the item</summary>
    public string CssClass { get; set; }
  }
}
