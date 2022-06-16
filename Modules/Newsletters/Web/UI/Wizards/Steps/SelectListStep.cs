// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.SelectListStep
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Newsletters.Web.UI.WizardFramework;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps
{
  /// <summary>
  /// Campaign wizard step for selecting a list to which the campaign ought to be sent.
  /// </summary>
  public class SelectListStep : SitefinityWizardStepControl
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Newsletters.Wizards.Steps.SelectListStep.ascx");
    internal new const string scriptReference = "Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.SelectListStep.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? SelectListStep.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the reference to the list selector control.</summary>
    public virtual FlatSelector ListSelector => this.Container.GetControl<FlatSelector>("listSelector", true);

    /// <summary>Gets the reference to the message control.</summary>
    protected virtual Message MessageControl => this.Container.GetControl<Message>("messageControl", true);

    /// <summary>Gets the reference to the selector panel control</summary>
    protected virtual HtmlGenericControl SelectorPanel => this.Container.GetControl<HtmlGenericControl>("selectorPanel", true);

    /// <summary>Gets the reference to the no lists panel</summary>
    protected virtual HtmlGenericControl NoListsPanel => this.Container.GetControl<HtmlGenericControl>("noListsPanel", true);

    /// <summary>
    /// Gets the reference to the link button for creating a first list, when no lists exist.
    /// </summary>
    protected virtual LinkButton CreateYourFirstListButton => this.Container.GetControl<LinkButton>("createYourFirstListButton", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = base.GetScriptDescriptors().Count<ScriptDescriptor>() != 0 ? (ScriptControlDescriptor) base.GetScriptDescriptors().Last<ScriptDescriptor>() : new ScriptControlDescriptor(typeof (SelectListStep).FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("listSelector", this.ListSelector.ClientID);
      controlDescriptor.AddComponentProperty("messageControl", this.MessageControl.ClientID);
      controlDescriptor.AddElementProperty("selectorPanel", this.SelectorPanel.ClientID);
      controlDescriptor.AddElementProperty("noListsPanel", this.NoListsPanel.ClientID);
      controlDescriptor.AddElementProperty("createYourFirstListButton", this.CreateYourFirstListButton.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptDescriptor[1]
      {
        (ScriptDescriptor) controlDescriptor
      };
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Newsletters.Web.UI.Wizards.Steps.Scripts.SelectListStep.js", typeof (SelectListStep).Assembly.FullName)
    };

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
