// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.FormSubmitButtonDesigner
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers
{
  public class FormSubmitButtonDesigner : ControlDesignerBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormSubmitButtonDesigner.ascx");
    private const string designerScriptName = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormSubmitButtonDesigner.js";

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormSubmitButtonDesigner.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Represents the textfield for the label of the button</summary>
    protected RadMultiPage MultiPage => this.Container.GetControl<RadMultiPage>("multiPage", true);

    /// <summary>Represents the textfield for the label of the button</summary>
    protected TextField LabelTextField => this.Container.GetControl<TextField>("labelTextField", true);

    /// <summary>
    /// Represents the choicefield for setting the required option
    /// </summary>
    protected ChoiceField ButtonSizeChoiceField => this.Container.GetControl<ChoiceField>("buttonSizeChoiceField", true);

    /// <summary>
    /// Represents the textfield for the CSS style of the button
    /// </summary>
    protected TextField CssClassTextField => this.Container.GetControl<TextField>("cssClassTextField", true);

    /// <summary>
    /// Represents the textfield for the CSS style of the button
    /// </summary>
    protected TextField ImageCssClassTextField => this.Container.GetControl<TextField>("imageCssClassTextField", true);

    /// <summary>Represents the button that swithes to image selection</summary>
    protected LinkButton UseImageButton => this.Container.GetControl<LinkButton>("useImageButton", true);

    /// <summary>Represents the image for the button</summary>
    protected Image ButtonImage => this.Container.GetControl<Image>("buttonImage", true);

    /// <summary>Represents the button that invokes the image selector</summary>
    protected LinkButton ChangeImageButton => this.Container.GetControl<LinkButton>("changeImageButton", true);

    /// <summary>
    /// Represents the textfield for the tooltip of the submit button
    /// </summary>
    protected TextField TooltipTextField => this.Container.GetControl<TextField>("tooltipTextField", true);

    /// <summary>
    /// Represents the button that shwitches the designer to button mode
    /// </summary>
    protected LinkButton UseButtonButton => this.Container.GetControl<LinkButton>("useButtonButton", true);

    /// <summary>Represents the image selector</summary>
    protected ImageSettingsDesigner ImageSettingsDesigner => this.Container.GetControl<ImageSettingsDesigner>("imageSettingsDesigner", true);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container) => this.UseImageButton.Attributes.CssStyle.Add("display", "none");

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = (ScriptControlDescriptor) scriptDescriptors.Last<ScriptDescriptor>();
      controlDescriptor.AddElementProperty("useImageButton", this.UseImageButton.ClientID);
      controlDescriptor.AddComponentProperty("labelTextField", this.LabelTextField.ClientID);
      controlDescriptor.AddComponentProperty("buttonSizeChoiceField", this.ButtonSizeChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("cssClassTextField", this.CssClassTextField.ClientID);
      controlDescriptor.AddComponentProperty("multiPage", this.MultiPage.ClientID);
      return (IEnumerable<ScriptDescriptor>) scriptDescriptors.ToArray<ScriptDescriptor>();
    }

    /// <summary>Gets the script references.</summary>
    /// <returns></returns>
    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>(base.GetScriptReferences())
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormSubmitButtonDesigner.js", typeof (FormSubmitButtonDesigner).Assembly.FullName)
    }.ToArray();
  }
}
