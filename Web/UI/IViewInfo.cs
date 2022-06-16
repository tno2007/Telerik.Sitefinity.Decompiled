// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.IViewInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Provides information for creating Views for ViewModeControl based controls.
  /// </summary>
  public interface IViewInfo
  {
    /// <summary>The type of the view.</summary>
    Type ViewType { get; }

    /// <summary>The name of the view.</summary>
    string ViewName { get; }

    /// <summary>The title of the view.</summary>
    string Title { get; }

    /// <summary>The Description of the view.</summary>
    string Description { get; }

    /// <summary>
    /// Gets the view command CSS class which is used for automatically generated command panels.
    /// </summary>
    /// <value>The view command CSS class.</value>
    string ViewCommandCssClass { get; }

    /// <summary>Loads the specified view control.</summary>
    /// <returns>Control</returns>
    Control LoadView();
  }
}
