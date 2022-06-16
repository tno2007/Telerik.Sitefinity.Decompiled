// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.ControlNodeType
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Enumeration that presents the type of the node on a page
  /// </summary>
  public enum ControlNodeType
  {
    /// <summary>All nodes that are not Root and Orphaned.</summary>
    Normal,
    /// <summary>The first node on a page.</summary>
    Root,
    /// <summary>
    /// The node which placeholder is removed.
    /// At backend this node is rendered after all controls on a page and not rendered on a frontend.
    /// </summary>
    Orphaned,
  }
}
