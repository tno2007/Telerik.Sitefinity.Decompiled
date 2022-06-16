// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizardStepInfo
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework
{
  /// <summary>Represents a single step of a wizard.</summary>
  public class SitefinityWizardStepInfo
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="!:WizardStep" /> class.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <param name="stepType">Type of the step.</param>
    public SitefinityWizardStepInfo(string name, Control stepControl)
    {
      this.Name = name;
      this.StepControl = stepControl;
    }

    /// <summary>
    /// Gets or sets the name of the wizard step. Inside of one wizard, each step must
    /// have unique name.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>Gets or sets the title of the wizard step.</summary>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the control to be loaded for wizard step.
    /// </summary>
    public Control StepControl { get; private set; }
  }
}
