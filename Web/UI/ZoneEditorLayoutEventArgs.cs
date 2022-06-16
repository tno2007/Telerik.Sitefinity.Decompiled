// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ZoneEditorLayoutEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>ZoneEditorLayoutEventArgs</summary>
  public class ZoneEditorLayoutEventArgs : ZoneEditorEventArgs
  {
    private LayoutControl _layoutControl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditorLayoutEventArgs" /> class.
    /// </summary>
    public ZoneEditorLayoutEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditorLayoutEventArgs" /> class.
    /// </summary>
    /// <param name="dockContentContainer">The dock content container.</param>
    /// <param name="controlData">The control data.</param>
    public ZoneEditorLayoutEventArgs(Control dockContentContainer, ControlData controlData)
      : base(dockContentContainer, controlData)
    {
    }

    /// <summary>Gets or sets a LayoutControl instance</summary>
    /// <value>The LayoutControl instance.</value>
    public LayoutControl LayoutControl
    {
      get => this._layoutControl;
      set => this._layoutControl = value;
    }
  }
}
