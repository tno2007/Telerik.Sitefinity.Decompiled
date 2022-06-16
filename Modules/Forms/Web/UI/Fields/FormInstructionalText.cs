// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormInstructionalText
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.GenericContent.Web.UI;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [ControlDesigner(typeof (FormInstructionalTextDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "InstructionalText")]
  [FormControlDisplayMode(FormControlDisplayMode.Read)]
  public class FormInstructionalText : ContentBlockBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormInstructionalText" /> class.
    /// </summary>
    public FormInstructionalText() => this.Html = Res.Get<FormsResources>().InstructionalTextGoesHere;

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      this.AddCssClass("sfFormInstructions");
    }
  }
}
