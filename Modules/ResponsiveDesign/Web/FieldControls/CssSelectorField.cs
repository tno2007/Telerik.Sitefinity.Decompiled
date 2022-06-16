// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls.CssSelectorField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Web.FieldControls
{
  /// <summary>
  /// Field control for selecting the css files for the media queries.
  /// </summary>
  public class CssSelectorField : FieldControl
  {
    internal const string scriptReference = "Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.CssSelectorField.js";
    private const string kendoScriptRef = "Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.ResponsiveDesign.CssSelectorField.ascx");

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get
      {
        if (string.IsNullOrEmpty(base.LayoutTemplatePath))
          base.LayoutTemplatePath = CssSelectorField.layoutTemplatePath;
        return base.LayoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets the reference to the control that represents the title of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the title of the field.</value>
    protected internal override WebControl TitleControl => (WebControl) this.TitleLabel;

    /// <summary>
    /// Gets the reference to the control that represents the description of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the description of the field.</value>
    protected internal override WebControl DescriptionControl => (WebControl) this.DescriptionLabel;

    /// <summary>
    /// Gets the reference to the control that represents the example of the field control.
    /// Return null if no such control exists in the template.
    /// </summary>
    /// <value>The control displaying the sample usage of the field.</value>
    protected internal override WebControl ExampleControl => (WebControl) this.ExampleLabel;

    /// <summary>
    /// Gets the reference to the label which displays the title of the field.
    /// </summary>
    protected virtual Label TitleLabel => this.Container.GetControl<Label>("titleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label which displays the description of the field.
    /// </summary>
    protected virtual Label DescriptionLabel => this.Container.GetControl<Label>("descriptionLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>
    /// Gets the reference to the label which displays the example of the field.
    /// </summary>
    protected virtual Label ExampleLabel => this.Container.GetControl<Label>("exampleLabel", this.DisplayMode == FieldDisplayMode.Write);

    /// <summary>Gets the master page selector dialog.</summary>
    /// <value>The master page selector dialog.</value>
    protected virtual RadWindow CssFileSelectorDialog => this.Container.GetControl<RadWindow>("cssFileSelectorDialog", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.TitleLabel.Text = this.Title;
      this.DescriptionLabel.Text = this.Description;
      this.ExampleLabel.Text = this.Example;
    }

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      List<ScriptDescriptor> source = new List<ScriptDescriptor>(base.GetScriptDescriptors());
      ((ScriptComponentDescriptor) source.Last<ScriptDescriptor>()).AddComponentProperty("cssFileSelectorDialog", this.CssFileSelectorDialog.ClientID);
      return (IEnumerable<ScriptDescriptor>) source;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Resources.Scripts.Kendo.kendo.all.min.js", Config.Get<ControlsConfig>().ResourcesAssemblyInfo.Assembly.FullName),
      new ScriptReference("Telerik.Sitefinity.Modules.ResponsiveDesign.Web.Scripts.CssSelectorField.js", typeof (CssSelectorField).Assembly.FullName)
    };
  }
}
