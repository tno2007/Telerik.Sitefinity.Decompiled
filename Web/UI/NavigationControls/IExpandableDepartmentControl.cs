// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.NavigationControls.IExpandableDepartmentControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Web.UI.NavigationControls
{
  /// <summary>
  /// Interface for setting up levels shown by hierachical controls
  /// </summary>
  public interface IExpandableDepartmentControl
  {
    /// <summary>
    /// Gets or sets a value indicating whether the control will allow collapsing.
    /// </summary>
    bool AllowCollapsing { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the control shows expanded levels
    /// </summary>
    /// <value><c>true</c> if [show expanded]; otherwise, <c>false</c>.</value>
    bool ShowExpanded { get; set; }

    /// <summary>
    /// Gets or sets how many the levels to expand 0 - for all levels.
    /// </summary>
    /// <value>The levels to expand.</value>
    int LevelsToExpand { get; set; }

    /// <summary>Gets or sets the levels to bind.</summary>
    /// <value>The level to bind.</value>
    int MaxDataBindDepth { get; set; }

    /// <summary>Gets or sets the current page URL.</summary>
    /// <value>The current page URL.</value>
    string CurrentPageURL { get; set; }

    /// <summary>Gets or sets the navigation action.</summary>
    /// <value>The navigation action.</value>
    NavigationAction NavigationAction { get; set; }
  }
}
