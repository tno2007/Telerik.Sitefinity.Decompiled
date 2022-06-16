// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormEmailTextBoxLabelView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views
{
  /// <inheritdoc />
  public class FormEmailTextBoxLabelView : FormMetaFieldLabelViewBase
  {
    /// <summary>The layout template path</summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1307:AccessibleFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1311:StaticReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormEmailTextBoxLabelView.ascx");
    internal const string DesignerViewJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEmailTextBoxLabelView.js";
    internal const string DesignerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this.GetType().Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().LabelAndTexts;

    /// <summary>Gets the name of the embedded layout template.</summary>
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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormEmailTextBoxLabelView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether is localizable checkbox will be shown.
    /// </summary>
    /// <value>
    /// <c>true</c> if is localizable checkbox is visible; otherwise, <c>false</c>.
    /// </value>
    internal bool ShowIsLocalizableOption { get; set; }

    /// <summary>
    /// Gets a value that represents the text field for the label of the control
    /// </summary>
    protected TextField LabelTextField => this.Container.GetControl<TextField>("labelTextField", true);

    /// <summary>
    /// Gets a value that represents the choice field for setting the required option
    /// </summary>
    protected ChoiceField RequiredChoiceField => this.Container.GetControl<ChoiceField>("requiredChoiceField", true);

    /// <summary>
    /// Gets a value that represents the choice field for setting the is localizable option
    /// </summary>
    protected ChoiceField IsLocalizableChoiceField => this.Container.GetControl<ChoiceField>("isLocalizableChoiceField", false);

    /// <summary>
    /// Gets a value that represents the text field for the instructions text
    /// </summary>
    protected TextField InstructionsTextField => this.Container.GetControl<TextField>("instructionsTextField", true);

    /// <summary>
    /// Gets a value that represents the text field for the default value text
    /// </summary>
    protected TextField PredefinedValueTextField => this.Container.GetControl<TextField>("predefinedValueTextField", true);

    /// <summary>
    /// Gets a value that represents the text field for the error message
    /// </summary>
    protected TextField ErrorMessageTextField => this.Container.GetControl<TextField>("errorMessageTextField", true);

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    /// <param name="container">The container</param>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ErrorMessageTextField.Style.Add("dispay", "none");
      base.InitializeControls(container);
    }

    /// <inheritdoc />
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      if (this.IsLocalizableChoiceField != null)
        this.IsLocalizableChoiceField.Visible = this.ShowIsLocalizableOption;
      base.OnPreRender(e);
    }

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
      controlDescriptor.AddComponentProperty("labelTextField", this.LabelTextField.ClientID);
      controlDescriptor.AddComponentProperty("requiredChoiceField", this.RequiredChoiceField.ClientID);
      controlDescriptor.AddComponentProperty("instructionsTextField", this.InstructionsTextField.ClientID);
      controlDescriptor.AddComponentProperty("predefinedValueTextField", this.PredefinedValueTextField.ClientID);
      controlDescriptor.AddComponentProperty("errorMessageTextField", this.ErrorMessageTextField.ClientID);
      controlDescriptor.AddProperty("_defaultValueValidationMessage", (object) Res.Get<FormsResources>().DefaultValueExceedsMaxRange);
      if (this.IsLocalizableChoiceField == null || !this.IsLocalizableChoiceField.Visible)
        return scriptDescriptors;
      controlDescriptor.AddComponentProperty("isLocalizableChoiceField", this.IsLocalizableChoiceField.ClientID);
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
      string assembly = this.GetType().Assembly.GetName().ToString();
      return (IEnumerable<ScriptReference>) new List<ScriptReference>()
      {
        new ScriptReference("Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js", assembly),
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormEmailTextBoxLabelView.js", assembly)
      };
    }
  }
}
