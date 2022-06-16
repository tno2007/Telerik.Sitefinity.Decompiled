// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControlWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI
{
  /// <summary>Provides UI for editing WebForms Form entries.</summary>
  public class FormEntryEditControlWrapper : WebControl, IFormEntryEditControl
  {
    private IFormEntryEditControl formEntryControl;

    /// <summary>Gets or sets the name of the form.</summary>
    /// <value>The name of the form.</value>
    public string FormName
    {
      get => this.formEntryControl.FormName;
      set => this.formEntryControl.FormName = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.FormEntryEditControlWrapper" /> class.
    /// </summary>
    public FormEntryEditControlWrapper() => this.formEntryControl = (IFormEntryEditControl) new FormEntryEditControl();

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer) => writer.Write(string.Empty);

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer) => writer.Write(string.Empty);

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      if (!(this.formEntryControl is Control formEntryControl))
        return;
      this.Controls.Add(formEntryControl);
    }
  }
}
