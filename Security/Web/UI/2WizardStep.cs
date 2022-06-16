// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.UI.CompleteWizardStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Security.Web.UI
{
  /// <summary>
  /// The wizard step for the Complete layout of CreateUserWizard control.
  /// </summary>
  public class CompleteWizardStep : TemplatedWizardStep
  {
    /// <summary>Gets or sets the owner.</summary>
    /// <value>The owner.</value>
    public override CreateUserWizardForm Owner
    {
      get => base.Owner;
      set => base.Owner = value != null || value == null ? value : throw new HttpException("CompleteWizardStep_OnlyAllowedInCreateUserWizard");
    }

    /// <summary>Gets or sets the type of the step.</summary>
    /// <value>The type of the step.</value>
    public override WizardStepType StepType
    {
      get => WizardStepType.Complete;
      set => throw new InvalidOperationException(Res.Get<ErrorMessages>().CreateUserWizardStepStepTypeCannotBeSet);
    }

    /// <summary>Title text of Complete step in CreateUserWizard.</summary>
    /// <value>Title text of Complete step in CreateUserWizard.</value>
    public override string Title
    {
      get => this.TitleInternal ?? Res.Get<ErrorMessages>().CreateUserWizardDefaultCompleteTitleText;
      set => base.Title = value;
    }
  }
}
