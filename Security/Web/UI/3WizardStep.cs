// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.CreateUserWizardStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>The wizard step for the Create User layout.</summary>
  public class CreateUserWizardStep : TemplatedWizardStep
  {
    /// <summary>
    /// Gets or sets a value indicating whether user can return back to this step.
    /// </summary>
    /// <value><c>true</c> if user can return back to this step; otherwise, <c>false</c>.</value>
    public override bool AllowReturn
    {
      get => this.AllowReturnInternal;
      set => throw new InvalidOperationException("CreateUserWizardStep_AllowReturnCannotBeSet");
    }

    private bool AllowReturnInternal
    {
      get
      {
        object obj = this.ViewState[nameof (AllowReturnInternal)];
        return obj == null || (bool) obj;
      }
      set => this.ViewState[nameof (AllowReturnInternal)] = (object) value;
    }

    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    public override CreateUserWizardForm Owner
    {
      get => base.Owner;
      set => base.Owner = value;
    }

    /// <summary>Gets or sets the type of the step.</summary>
    /// <value>The type of the step.</value>
    public override WizardStepType StepType
    {
      get => base.StepType;
      set => throw new InvalidOperationException("CreateUserWizardStep_StepTypeCannotBeSet");
    }

    /// <summary>
    /// Gets or sets the title for the CreateUser wizard step.
    /// </summary>
    /// <value>The title.</value>
    public override string Title
    {
      get => this.TitleInternal ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultCreateUserTitleText;
      set => base.Title = value;
    }
  }
}
