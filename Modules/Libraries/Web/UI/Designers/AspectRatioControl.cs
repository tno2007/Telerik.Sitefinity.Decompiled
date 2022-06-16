// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers.AspectRatioControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Fields;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Designers
{
  public class AspectRatioControl : SimpleScriptView
  {
    private const string ControlScript = "Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.AspectRatioControl.js";
    private static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Designers.Libraries.AspectRatioControl.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? AspectRatioControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Represents the DIV containing the Width and Height textboxes.
    /// </summary>
    protected virtual HtmlGenericControl DivWidthHeight => this.Container.GetControl<HtmlGenericControl>("divWH", true);

    /// <summary>
    /// Represents the textbox containing the custom Width value.
    /// </summary>
    protected virtual TextBox TextBoxWidth => this.Container.GetControl<TextBox>("txtWidth", true);

    /// <summary>
    /// Represents the textbox containing the custom Height value.
    /// </summary>
    protected virtual TextBox TextBoxHeight => this.Container.GetControl<TextBox>("txtHeight", true);

    /// <summary>Gets the aspect ratio choice field.</summary>
    /// <value>The aspect ratio choice field.</value>
    protected virtual ChoiceField AspectRatioChoiceField => this.Container.GetControl<ChoiceField>("aspectRatioChoiceField", true);

    public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
    {
      ScriptControlDescriptor controlDescriptor = new ScriptControlDescriptor(typeof (AspectRatioControl).FullName, this.ClientID);
      controlDescriptor.AddElementProperty("textBoxHeight", this.TextBoxHeight.ClientID);
      controlDescriptor.AddElementProperty("textBoxWidth", this.TextBoxWidth.ClientID);
      controlDescriptor.AddElementProperty("divWH", this.DivWidthHeight.ClientID);
      controlDescriptor.AddComponentProperty("aspectRatioChoiceField", this.AspectRatioChoiceField.ClientID);
      return (IEnumerable<ScriptDescriptor>) new ScriptControlDescriptor[1]
      {
        controlDescriptor
      };
    }

    public override IEnumerable<ScriptReference> GetScriptReferences() => (IEnumerable<ScriptReference>) new List<ScriptReference>()
    {
      new ScriptReference("Telerik.Sitefinity.Modules.Libraries.Web.UI.Scripts.AspectRatioControl.js", typeof (AspectRatioControl).Assembly.GetName().ToString())
    };

    protected override void InitializeControls(GenericContainer container)
    {
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;
  }
}
