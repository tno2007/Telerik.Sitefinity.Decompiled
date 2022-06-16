// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets.SelectorItem
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Widgets
{
  /// <summary>Wraps a selector</summary>
  [ParseChildren(ChildrenAsProperties = true)]
  public class SelectorItem : Control
  {
    /// <summary>
    /// Name of the selector used by the client-side code as a reference
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Css class to apply to the header wrapper when selected
    /// </summary>
    public string SelectedCssClass { get; set; }

    /// <summary>
    /// Css class to apply to the header wrapper when not selected
    /// </summary>
    public string NotSelectedCssClass { get; set; }

    /// <summary>
    /// Names of the selector items that are invalidated when this item's selector changes its selection
    /// </summary>
    /// <value>Comma-separated list of names. Case-sensitive</value>
    public string InvalidatesNames { get; set; }

    /// <summary>Template for the header of the selector</summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate Header { get; set; }

    /// <summary>
    /// Free-form template that should contain at least one control that is assignable to ItemSelector
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public ITemplate Body { get; set; }
  }
}
