// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.WizardStepBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>The base class for wizard step.</summary>
  public abstract class WizardStepBase : System.Web.UI.WebControls.View
  {
    private CreateUserWizardForm owner;

    /// <summary>
    /// Restores view-state information from a previous page request that was saved by the <see cref="M:System.Web.UI.Control.SaveViewState" /> method.
    /// </summary>
    /// <param name="savedState">An <see cref="T:System.Object" /> that represents the control state to be restored.</param>
    protected override void LoadViewState(object savedState)
    {
      if (savedState == null)
        return;
      base.LoadViewState(savedState);
    }

    /// <summary>
    /// Gets or sets a value indicating whether user can return back to this wizard step.
    /// </summary>
    /// <value><c>true</c> if [allow return]; otherwise, <c>false</c>.</value>
    public virtual bool AllowReturn
    {
      get
      {
        object obj = this.ViewState[nameof (AllowReturn)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (AllowReturn)] = (object) value;
    }

    /// <summary>
    /// Gets or sets the programmatic identifier assigned to the server control.
    /// </summary>
    /// <value></value>
    /// <returns>The programmatic identifier assigned to the control.</returns>
    public override string ID
    {
      get => base.ID;
      set
      {
        if (this.Owner != null)
        {
          if (value != null && value.Equals(this.Owner.ID, StringComparison.OrdinalIgnoreCase))
            throw new ArgumentException("Id_already_used");
          foreach (WizardStepBase wizardStep in this.Owner.WizardSteps)
          {
            if (wizardStep != this && wizardStep.ID != null && wizardStep.ID.Equals(value, StringComparison.OrdinalIgnoreCase))
              throw new ArgumentException("Id_already_used");
          }
        }
        base.ID = value;
      }
    }

    /// <summary>The name of wizard step.</summary>
    /// <value>The name of wizard step.</value>
    public virtual string Name
    {
      get
      {
        if (!string.IsNullOrEmpty(this.Title))
          return this.Title;
        return !string.IsNullOrEmpty(this.ID) ? this.ID : (string) null;
      }
    }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    public virtual CreateUserWizardForm Owner
    {
      get => this.owner;
      set => this.owner = value;
    }

    /// <summary>The type of wizard step.</summary>
    /// <value>The type of wizard step.</value>
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

    /// <summary>Gets or sets the title of wizard step.</summary>
    /// <value>The title of wizard step.</value>
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

    /// <summary>Gets the title internal.</summary>
    /// <value>The title internal.</value>
    protected string TitleInternal => (string) this.ViewState["Title"];

    /// <summary>Gets the wizard.</summary>
    /// <value>The wizard.</value>
    public CreateUserWizardForm Wizard => this.Owner;
  }
}
