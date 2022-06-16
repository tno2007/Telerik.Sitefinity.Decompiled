// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views;

namespace Telerik.Sitefinity.Web.UI.Fields.Designers.Views
{
  /// <summary>
  /// The "Label and Tests" view of <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.TextFieldDesigner" />.
  /// </summary>
  public class ImageFieldLabelView : FormMetaFieldLabelViewBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Fields.ImageFieldLabelView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ImageFieldLabelView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Fields.Designers.Views.ImageFieldLabelView" /> class.
    /// </summary>
    public ImageFieldLabelView() => this.LayoutTemplatePath = ImageFieldLabelView.layoutTemplatePath;

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => typeof (ImageFieldLabelView).Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().General;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Represents the name of the type that is going to be used as default for the field - Image for image field, etc
    /// </summary>
    public virtual string DefaultItemTypeName => typeof (Image).FullName;

    /// <summary>Represents the textfield for the label of the control</summary>
    protected TextField LabelTextField => this.Container.GetControl<TextField>("labelTextField", true);

    /// <summary>
    /// Represents the choicefield for setting the required option
    /// </summary>
    protected ChoiceField RequiredChoiceField => this.Container.GetControl<ChoiceField>("requiredChoiceField", true);

    /// <summary>Represents the textfield for the instructions text</summary>
    protected TextField InstructionsTextField => this.Container.GetControl<TextField>("instructionsTextField", true);

    /// <summary>Represents the textfield for the default value text</summary>
    protected ImageField PredefinedImageField => this.Container.GetControl<ImageField>("predefinedImageField", true);

    /// <summary>Represents the textfield for the error message</summary>
    protected TextField ErrorMessageTextField => this.Container.GetControl<TextField>("errorMessageTextField", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ErrorMessageTextField.Style.Add("dispay", "none");
      this.MetaFieldNameTextBox.Visible = false;
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      IEnumerable<ScriptDescriptor> scriptDescriptors = base.GetScriptDescriptors();
      ScriptControlDescriptor controlDescriptor = scriptDescriptors.Last<ScriptDescriptor>() as ScriptControlDescriptor;
      controlDescriptor.AddProperty("_defaultRequiredMessage", (object) Res.Get<FormsResources>().ThisInformationIsRequired);
      controlDescriptor.AddProperty("_defaultItemTypeName", (object) this.DefaultItemTypeName);
      controlDescriptor.AddProperty("_webServiceUrl", (object) VirtualPathUtility.ToAbsolute("~/Sitefinity/Services/Content/ImageService.svc/"));
      controlDescriptor.AddComponentProperty("labelTextField", this.LabelTextField.ClientID);
      controlDescriptor.AddComponentProperty("requiredChoiceField", this.RequiredChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("instructionsTextField", this.InstructionsTextField.ClientID);
      controlDescriptor.AddComponentProperty("predefinedImageField", this.PredefinedImageField.ClientID);
      controlDescriptor.AddComponentProperty("errorMessageTextField", this.ErrorMessageTextField.ClientID);
      return scriptDescriptors;
    }

    /// <summary>
    /// Gets a collection of <see cref="T:System.Web.UI.ScriptReference" /> objects that define script resources that the control requires.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptReference" /> objects.
    /// </returns>
    public override IEnumerable<ScriptReference> GetScriptReferences()
    {
      string assembly = typeof (ImageFieldLabelView).Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Web.UI.Fields.Designers.Scripts.ImageFieldLabelView.js", assembly)
      };
    }
  }
}
