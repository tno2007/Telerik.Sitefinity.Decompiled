// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts.IViewModeDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend.Master.Contracts
{
  /// <summary>
  /// An interface which provides all information needed to construct the view mode.
  /// </summary>
  public interface IViewModeDefinition : IDefinition
  {
    /// <summary>Gets or sets the name of the View Mode.</summary>
    /// <remarks>
    /// This name has to be unique inside of a collection of view modes.
    /// </remarks>
    string Name { get; set; }

    /// <summary>When set to true enables drag-and-drop functionality</summary>
    bool? EnableDragAndDrop { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to store the expansion of the tree per user.
    /// </summary>
    bool? EnableInitialExpanding { get; set; }

    /// <summary>
    /// Gets or sets the name of the cookie that will contain the information of the expanded nodes.
    /// </summary>
    string ExpandedNodesCookieName { get; set; }
  }
}
