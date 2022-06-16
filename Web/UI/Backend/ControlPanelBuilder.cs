// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.ControlPanelBuilder
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Builds a control panel</summary>
  public class ControlPanelBuilder : Control
  {
    /// <summary>
    /// Gets or sets a value indicating whether this control represents application module or service module.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this control represents application module; otherwise, <c>false</c>.
    /// </value>
    public bool IsApplicationModule
    {
      get
      {
        object obj = this.ViewState[nameof (IsApplicationModule)];
        return obj != null && (bool) obj;
      }
      set => this.ViewState[nameof (IsApplicationModule)] = (object) value;
    }

    /// <summary>Gets or sets the name of the module.</summary>
    /// <value>The name of the module.</value>
    public string ModuleName
    {
      get => (string) this.ViewState[nameof (ModuleName)] ?? string.Empty;
      set => this.ViewState[nameof (ModuleName)] = (object) value;
    }

    /// <summary>Gets the control panel.</summary>
    /// <value>The control panel.</value>
    [Browsable(false)]
    public Control ControlPanel
    {
      get
      {
        this.EnsureChildControls();
        return this.Controls[0];
      }
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.Load" /> event.
    /// </summary>
    /// <param name="e">The <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      this.EnsureChildControls();
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      IModule applicationModule = SystemManager.GetApplicationModule(this.ModuleName);
      if (applicationModule == null)
        throw new InvalidOperationException("Invalid module name: \"{0}\".".Arrange((object) this.ModuleName));
      if (applicationModule is InactiveModule)
        throw new InvalidOperationException("The module \"{0}\" is either disabled or not started.".Arrange((object) this.ModuleName));
      this.Controls.Add((Control) applicationModule.GetControlPanel());
    }
  }
}
