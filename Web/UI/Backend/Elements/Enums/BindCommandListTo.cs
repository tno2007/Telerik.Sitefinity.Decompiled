// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.Elements.Enums.BindCommandListTo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.Backend.Elements.Enums
{
  /// <summary>
  /// The different render modes of the dynamic command widget. Possible values are RadComboBox and Client
  /// </summary>
  public enum BindCommandListTo
  {
    /// <summary>
    /// Renders the Dynamic Command Widget as a RadComboBox. Each item in the RadComboBox fires a command
    /// </summary>
    ComboBox,
    /// <summary>
    /// Redners the Dynamic Command Widget as a simple list. Each item has its own template and fires a command
    /// </summary>
    Client,
    /// <summary>
    /// Renders the Dynamic Command Widget as a simple hierarchical list.
    /// </summary>
    HierarchicalData,
  }
}
