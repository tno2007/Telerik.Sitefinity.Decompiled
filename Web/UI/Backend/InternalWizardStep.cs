// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.InternalWizardStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Wizard step</summary>
  [Bindable(false)]
  [ControlBuilder(typeof (ControlBuilder))]
  internal class InternalWizardStep : View
  {
    private InternalWizard _owner;

    /// <param name="savedState">An <see cref="T:System.Object"></see> that represents the <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see> control to restore.</param>
    protected override void LoadViewState(object savedState)
    {
      if (savedState == null)
        return;
      base.LoadViewState(savedState);
    }

    /// <summary>Raises the <see cref="M:System.Web.UI.Control.OnLoad(System.EventArgs)"></see> event.</summary>
    /// <param name="e">The <see cref="T:System.EventArgs"></see> object that contains the event data.</param>
    protected override void OnLoad(EventArgs e) => base.OnLoad(e);

    /// <summary>Gets or sets a value indicating whether the user is allowed to return to the current step from a subsequent step in a <see cref="T:Telerik.Cms.Web.UI.InternalWizardStepCollection"></see> collection. </summary>
    /// <returns>true if the user is allowed to return to the current step; otherwise, false. The default value is true.</returns>
    [Themeable(false)]
    [Filterable(false)]
    [DefaultValue(true)]
    [Category("Behavior")]
    public virtual bool AllowReturn
    {
      get
      {
        object obj = this.ViewState[nameof (AllowReturn)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (AllowReturn)] = (object) value;
    }

    /// <summary>Gets or sets a value indicating whether themes apply to this control.</summary>
    /// <returns>true to use themes; otherwise, false. The default is false.</returns>
    [Browsable(true)]
    public override bool EnableTheming
    {
      get => base.EnableTheming;
      set => base.EnableTheming = value;
    }

    /// <summary>Gets the name associated with a step in a control that acts as a wizard.</summary>
    /// <returns>The name associated with a step in a control that acts as a wizard.</returns>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance")]
    public virtual string Name
    {
      get
      {
        if (!string.IsNullOrEmpty(this.Title))
          return this.Title;
        return !string.IsNullOrEmpty(this.ID) ? this.ID : (string) null;
      }
    }

    internal virtual InternalWizard Owner
    {
      get => this._owner;
      set => this._owner = value;
    }

    /// <summary>Gets or sets the type of navigation user interface (UI) to display for a step in a <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control.</summary>
    /// <returns>One of the <see cref="T:Telerik.Cms.Web.UI.WizardStepType"></see> enumeration values. The default value is WizardStepType.Auto.</returns>
    [Category("Behavior")]
    [DefaultValue(0)]
    public virtual WizardStepType StepType
    {
      get
      {
        object obj = this.ViewState[nameof (StepType)];
        return obj != null ? (WizardStepType) obj : WizardStepType.Auto;
      }
      set
      {
        if (value < WizardStepType.Auto || value > WizardStepType.Step)
          throw new ArgumentOutOfRangeException(nameof (value));
        if (this.StepType == value)
          return;
        this.ViewState[nameof (StepType)] = (object) value;
      }
    }

    /// <summary>Gets or sets the title to use for a step in a <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control when the sidebar feature is enabled.</summary>
    /// <returns>The title to use for a step in a <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control when the sidebar feature is enabled. The default value is an empty string ("").</returns>
    [DefaultValue("")]
    [Localizable(true)]
    [Category("Appearance")]
    public virtual string Title
    {
      get => (string) this.ViewState[nameof (Title)] ?? string.Empty;
      set
      {
        if (!(this.Title != value))
          return;
        this.ViewState[nameof (Title)] = (object) value;
      }
    }

    internal string TitleInternal => (string) this.ViewState["Title"];

    /// <summary>Gets the <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control that is the parent of the object derived from <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>.</summary>
    /// <returns>The <see cref="T:Telerik.Cms.Web.UI.InternalWizard"></see> control that is the parent of the object derived from <see cref="T:Telerik.Cms.Web.UI.InternalWizardStep"></see>.</returns>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [Category("Appearance")]
    public InternalWizard InternalWizard => this.Owner;
  }
}
