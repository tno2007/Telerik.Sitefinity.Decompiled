// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Extenders.IExpandableControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Extenders
{
  /// <summary>
  /// By implementing this interface, controls can extend their functinality in a way that a part of the
  /// control can be hidden and expanded after the user clicks on a ExpandControl.
  /// </summary>
  public interface IExpandableControl
  {
    /// <summary>
    /// Gets or sets the text that will be displayed on the control that can expand the hidden part.
    /// </summary>
    string ExpandText { get; set; }

    /// <summary>
    /// Gets or sets the value indicating whether the control is expanded by default. If control is to
    /// be expanded by default true; otherwise false.
    /// </summary>
    bool? Expanded { get; set; }

    /// <summary>
    /// Gets the reference to the control that when clicked expands the hidden part of the whole
    /// control.
    /// </summary>
    WebControl ExpandControl { get; }

    /// <summary>
    /// Gets the reference to the control that is hidden when Expanded is false and displayed
    /// upon clicking of the ExpandControl.
    /// </summary>
    WebControl ExpandTarget { get; }
  }
}
