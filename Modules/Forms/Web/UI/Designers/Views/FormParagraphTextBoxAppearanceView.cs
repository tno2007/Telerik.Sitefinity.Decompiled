// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views.FormParagraphTextBoxAppearanceView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Designers.Views
{
  public class FormParagraphTextBoxAppearanceView : ContentViewDesignerView
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Forms.FormParagraphTextBoxAppearanceView.ascx");
    internal const string designerViewJs = "Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormParagraphTextBoxAppearanceView.js";
    internal const string designerViewInterfaceControlJs = "Telerik.Sitefinity.Web.UI.ControlDesign.Scripts.IDesignerViewControl.js";

    /// <summary>Gets the name of the view.</summary>
    /// <value>The name of the view.</value>
    public override string ViewName => this.GetType().Name;

    /// <summary>Gets the view title.</summary>
    /// <value>The view title.</value>
    public override string ViewTitle => Res.Get<FormsResources>().Appearance;

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FormParagraphTextBoxAppearanceView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Gets the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> value that corresponds to this Web server control. This property is used primarily by control developers.
    /// </summary>
    /// <value></value>
    /// <returns>One of the <see cref="T:System.Web.UI.HtmlTextWriterTag" /> enumeration values.</returns>
    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>Represents the textfield for the CSS class</summary>
    protected TextField CssClassTextField => this.Container.GetControl<TextField>("cssClassTextField", true);

    /// <summary>
    /// Represents the choicefield for setting the size of the texbox
    /// </summary>
    protected ChoiceField TextBoxSizeChoiceField => this.Container.GetControl<ChoiceField>("textBoxSizeChoiceField", true);

    /// <summary>
    /// Gets a collection of script descriptors that represent ECMAScript (JavaScript) client components.
    /// </summary>
    /// <returns>
    /// An <see cref="T:System.Collections.IEnumerable" /> collection of <see cref="T:System.Web.UI.ScriptDescriptor" /> objects.
    /// </returns>
    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(this.GetType().FullName, this.ClientID);
      controlDescriptor.AddComponentProperty("cssClassTextField", this.CssClassTextField.ClientID);
      controlDescriptor.AddComponentProperty("textBoxSizeChoiceField", this.TextBoxSizeChoiceField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
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
        new ScriptReference("Telerik.Sitefinity.Modules.Forms.Web.UI.Scripts.FormParagraphTextBoxAppearanceView.js", assembly)
      };
    }
  }
}
