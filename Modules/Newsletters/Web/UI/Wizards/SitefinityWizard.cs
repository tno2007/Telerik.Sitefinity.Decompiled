// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.SitefinityWizard
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework
{
  /// <summary>Wizard control.</summary>
  public class SitefinityWizard : SimpleScriptView
  {
    private IList<SitefinityWizardStepInfo> steps;
    private SitefinityWizardNavigationDisplayMode navigationDisplayMode;
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.WizardFramework.Wizard.ascx");
    internal const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.Scripts.SitefinityWizard.js";

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SitefinityWizard.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the collection of wizard steps for this wizard.</summary>
    protected IList<SitefinityWizardStepInfo> Steps
    {
      get
      {
        if (this.steps == null)
          this.steps = (IList<SitefinityWizardStepInfo>) new List<SitefinityWizardStepInfo>();
        return this.steps;
      }
    }

    /// <summary>
    /// Gets or sets the navigation mode of the Sitefinity wizard control.
    /// </summary>
    public SitefinityWizardNavigationDisplayMode NavigationDisplayMode
    {
      get => this.navigationDisplayMode;
      set
      {
        this.navigationDisplayMode = value;
        this.ChildControlsCreated = false;
      }
    }

    /// <summary>
    /// Gets the reference to the repeater control representing the wizard progress.
    /// </summary>
    protected virtual Repeater WizardProgress => this.Container.GetControl<Repeater>("wizardProgress", true);

    /// <summary>Gets the reference to the steps placeholder control.</summary>
    protected virtual Control StepsPlaceholder => this.Container.GetControl<Control>("stepsPlaceholder", true);

    /// <summary>
    /// Gets the reference to the control that holds all navigation controls on the top.
    /// </summary>
    protected virtual Panel TopNavigation => this.Container.GetControl<Panel>("topNavigation", true);

    /// <summary>
    /// Gets the reference to the control that holds all navigation controls on the bottom.
    /// </summary>
    protected virtual Panel BottomNavigation => this.Container.GetControl<Panel>("bottomNavigation", true);

    /// <summary>
    /// Gets the reference to the top button for moving the wizard to the previous step.
    /// </summary>
    protected virtual LinkButton TopPreviousButton => this.Container.GetControl<LinkButton>("topPreviousButton", true);

    /// <summary>
    /// Gets the reference to the top button for moving the wizard to the next step.
    /// </summary>
    protected virtual LinkButton TopNextButton => this.Container.GetControl<LinkButton>("topNextButton", true);

    /// <summary>
    /// Gets the reference to the bottom button for moving the wizard to the previous step.
    /// </summary>
    protected virtual LinkButton BottomPreviousButton => this.Container.GetControl<LinkButton>("bottomPreviousButton", true);

    /// <summary>
    /// Gets the reference to the bottom button for moving the wizard to the next step.
    /// </summary>
    protected virtual LinkButton BottomNextButton => this.Container.GetControl<LinkButton>("bottomNextButton", true);

    /// <summary>Adds the wizard step to the wizard.</summary>
    /// <param name="step"></param>
    public void AddStep(SitefinityWizardStepInfo step)
    {
      this.Steps.Add(step);
      this.ChildControlsCreated = false;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.WizardProgress.DataSource = (object) this.Steps;
      this.TopNavigation.Visible = this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Both || this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Top;
      this.BottomNavigation.Visible = this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Both || this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Bottom;
      foreach (SitefinityWizardStepInfo step in (IEnumerable<SitefinityWizardStepInfo>) this.Steps)
        this.StepsPlaceholder.Controls.Add(step.StepControl);
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.WizardProgress.DataBind();
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> scriptDescriptors = new List<ScriptDescriptor>();
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (SitefinityWizard).FullName, this.ClientID);
      controlDescriptor.AddProperty("steps", (object) this.GetStepControlIds());
      if (this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Both || this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Top)
      {
        controlDescriptor.AddElementProperty("topPreviousButton", this.TopPreviousButton.ClientID);
        controlDescriptor.AddElementProperty("topNextButton", this.TopNextButton.ClientID);
      }
      if (this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Both || this.NavigationDisplayMode == SitefinityWizardNavigationDisplayMode.Bottom)
      {
        controlDescriptor.AddElementProperty("bottomPreviousButton", this.BottomPreviousButton.ClientID);
        controlDescriptor.AddElementProperty("bottomNextButton", this.BottomNextButton.ClientID);
      }
      scriptDescriptors.Add((ScriptDescriptor) controlDescriptor);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework.Scripts.SitefinityWizard.js", typeof (SitefinityWizard).Assembly.FullName)
    };

    private string[] GetStepControlIds()
    {
      this.EnsureChildControls();
      List<string> stringList = new List<string>();
      foreach (Control control in this.StepsPlaceholder.Controls)
      {
        if (control is WebControl)
          stringList.Add(control.ClientID);
      }
      return stringList.ToArray();
    }
  }
}
