// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ZoneEditorEventArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>ZoneEditorEventArgs</summary>
  public class ZoneEditorEventArgs : EventArgs
  {
    private Control _dockContentContainer;
    private ControlData _controlData;
    private bool _editable = true;
    /// <summary>
    /// This is where the real control is set when it is instantiated. It needs to be set so it is available
    /// in ZoneEditor. It is really strange that the control is instantiated in an event handler, but now is
    /// not a good time to change that - RC coming in a few days.
    /// </summary>
    private Control realControl;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditorEventArgs" /> class.
    /// </summary>
    public ZoneEditorEventArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ZoneEditorEventArgs" /> class.
    /// </summary>
    /// <param name="dockContentContainer">The dock content container.</param>
    /// <param name="controlData">The control data.</param>
    public ZoneEditorEventArgs(Control dockContentContainer, ControlData controlData)
    {
      this._dockContentContainer = dockContentContainer;
      this._controlData = controlData;
    }

    /// <summary>Gets the control container.</summary>
    /// <value>The control container.</value>
    public Control ControlContainer => this._dockContentContainer;

    /// <summary>Gets the control data.</summary>
    /// <value>The control data.</value>
    public ControlData ControlData => this._controlData;

    /// <summary>
    /// Gets or sets whether the current control should be displayed on the client-side as editable
    /// </summary>
    /// <value>The value.</value>
    public bool Editable
    {
      get => this._editable;
      set => this._editable = value;
    }

    public Control RealControl
    {
      get => this.realControl;
      set => this.realControl = value;
    }
  }
}
