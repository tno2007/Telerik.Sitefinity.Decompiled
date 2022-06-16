// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views
{
  public class FormChoiceFieldLabelView : FormMetaFieldLabelViewBase
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormChoiceFieldLabelView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormChoiceFieldLabelView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormChoiceFieldLabelView" /> class.
    /// </summary>
    public FormChoiceFieldLabelView() => this.LayoutTemplatePath = FormChoiceFieldLabelView.layoutTemplatePath;

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
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ConfigureView();
      base.InitializeControls(container);
    }

    /// <summary>Configures the view.</summary>
    protected void ConfigureView()
    {
      this.ErrorMessageTextField.Style.Add("dispay", "none");
      this.OtherTitleTextField.Style.Add("display", "none");
      ChoiceField control = (ChoiceField) this.ParentDesigner.PropertyEditor.Control;
      if (control.RenderChoicesAs == RenderChoicesAs.DropDown)
      {
        this.DefaultSelectedChoiceField.Visible = false;
        this.AddOtherChoiceField.Visible = false;
        this.OtherTitleTextField.Visible = false;
        this.RequiredChoiceField.Visible = false;
        this.SortChoicesAlphabeticallyChoiceField.Visible = true;
        this.ChoiceItemsBuilder.ShowDefaultItemSelector = true;
      }
      else if (control.RenderChoicesAs == RenderChoicesAs.RadioButtons)
      {
        this.DefaultSelectedChoiceField.Visible = true;
        this.AddOtherChoiceField.Visible = true;
        this.OtherTitleTextField.Visible = true;
        this.RequiredChoiceField.Visible = true;
        this.SortChoicesAlphabeticallyChoiceField.Visible = false;
      }
      else
      {
        if (control.RenderChoicesAs != RenderChoicesAs.CheckBoxes)
          return;
        this.DefaultSelectedChoiceField.Visible = false;
        this.AddOtherChoiceField.Visible = false;
        this.OtherTitleTextField.Visible = false;
        this.RequiredChoiceField.Visible = true;
        this.SortChoicesAlphabeticallyChoiceField.Visible = true;
        this.ChoiceItemsBuilder.MinimumItemsCount = 1;
      }
    }

    /// <summary>Represents the textfield for the label of the control</summary>
    protected TextField LabelTextField => this.Container.GetControl<TextField>("labelTextField", true);

    /// <summary>Represents the choice items builder</summary>
    public ChoiceItemsBuilder ChoiceItemsBuilder => this.Container.GetControl<ChoiceItemsBuilder>("choiceItemsBuilder", true);

    /// <summary>Represents the choicefield for addin another option</summary>
    public ChoiceField AddOtherChoiceField => this.Container.GetControl<ChoiceField>("addOtherChoiceField", true);

    /// <summary>
    /// Represents the choicefield for setting the default selected item
    /// </summary>
    public ChoiceField DefaultSelectedChoiceField => this.Container.GetControl<ChoiceField>("defaultSelectedChoiceField", true);

    /// <summary>
    /// Represents the textfield for the label of the additional item
    /// </summary>
    public TextField OtherTitleTextField => this.Container.GetControl<TextField>("otherTitleTextField", true);

    /// <summary>
    /// Represents the choicefield for setting the default selected item
    /// </summary>
    public ChoiceField SortChoicesAlphabeticallyChoiceField => this.Container.GetControl<ChoiceField>("sortChoicesAlphabeticallyChoiceField", true);

    /// <summary>
    /// Represents the choicefield for setting the default selected item
    /// </summary>
    public ChoiceField RequiredChoiceField => this.Container.GetControl<ChoiceField>("isRequiredChoiceField", true);

    /// <summary>Represents the textfield for the error message</summary>
    protected TextField ErrorMessageTextField => this.Container.GetControl<TextField>("errorMessageTextField", true);

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
      controlDescriptor.AddComponentProperty("labelTextField", this.LabelTextField.ClientID);
      controlDescriptor.AddComponentProperty("choiceItemsBuilder", this.ChoiceItemsBuilder.ClientID);
      if (this.RequiredChoiceField.Visible)
      {
        controlDescriptor.AddComponentProperty("requiredChoiceField", this.RequiredChoiceField.ClientID);
        controlDescriptor.AddComponentProperty("errorMessageTextField", this.ErrorMessageTextField.ClientID);
        controlDescriptor.AddProperty("_defaultRequiredMessage", (object) Res.Get<FormsResources>().ThisInformationIsRequired);
      }
      if (this.SortChoicesAlphabeticallyChoiceField.Visible)
        controlDescriptor.AddComponentProperty("sortChoicesAlphabeticallyChoiceField", this.SortChoicesAlphabeticallyChoiceField.ClientID);
      if (this.AddOtherChoiceField.Visible)
        controlDescriptor.AddComponentProperty("addOtherChoiceField", this.AddOtherChoiceField.ClientID);
      if (this.DefaultSelectedChoiceField.Visible)
        controlDescriptor.AddComponentProperty("defaultSelectedChoiceField", this.DefaultSelectedChoiceField.ClientID);
      if (!this.OtherTitleTextField.Visible)
        return scriptDescriptors;
      controlDescriptor.AddComponentProperty("otherTitleTextField", this.OtherTitleTextField.ClientID);
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
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormChoiceFieldLabelView.js", assembly)
      };
    }

    public enum FormChoiceViewMode
    {
      Dropdown,
      MultipleChoice,
      CheckBoxes,
    }
  }
}
