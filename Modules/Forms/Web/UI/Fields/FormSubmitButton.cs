// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSubmitButton
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Forms.Web.UI.Designers;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;

namespace Telerik.Sitefinity.Modules.Forms.Web.UI.Fields
{
  [FormControlDisplayMode(FormControlDisplayMode.Write)]
  [ControlDesigner(typeof (FormSubmitButtonDesigner))]
  [PropertyEditorTitle(typeof (FormsResources), "SubmitButton")]
  public class FormSubmitButton : Button
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Forms.Web.UI.Fields.FormSubmitButton" /> class.
    /// </summary>
    public FormSubmitButton()
    {
      this.ButtonSize = FormControlSize.Small;
      this.Text = Res.Get<Labels>().Submit;
      this.WrapperTag = HtmlTextWriterTag.Div;
    }

    /// <inheritdoc />
    public override string PostBackUrl
    {
      get => ControlUtilities.SanitizeUrl(base.PostBackUrl);
      set => base.PostBackUrl = value;
    }

    /// <summary>Represents the differnet sizes of the button</summary>
    public FormControlSize ButtonSize { get; set; }

    /// <summary>Gets or sets the wrapper tag.</summary>
    /// <value>The wrapper tag.</value>
    public HtmlTextWriterTag WrapperTag { get; set; }

    /// <summary>
    /// Gets or sets the text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control.
    /// </summary>
    /// <value></value>
    /// <returns>The text caption displayed in the <see cref="T:System.Web.UI.WebControls.Button" /> control. The default value is <see cref="F:System.String.Empty" />.</returns>
    [MultilingualProperty]
    public new virtual string Text
    {
      get => base.Text;
      set => base.Text = value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      StringBuilder stringBuilder = new StringBuilder("sfFormSubmit");
      if (!string.IsNullOrEmpty(this.CssClass))
        stringBuilder.AppendFormat(" {0}", (object) this.CssClass);
      if (this.ButtonSize != FormControlSize.None)
        stringBuilder.AppendFormat(" sfSubmitBtn{0}", (object) this.ButtonSize);
      writer.AddAttribute("class", stringBuilder.ToString());
      writer.AddAttribute("data-sf-btn-role", "submit");
      writer.RenderBeginTag(this.WrapperTag);
      base.RenderBeginTag(writer);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      base.RenderEndTag(writer);
      writer.RenderEndTag();
    }

    /// <summary>
    /// Determines whether the button has been clicked prior to rendering on the client.
    /// </summary>
    /// <param name="e">The event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      if (this.IsDesignMode())
        this.OnClientClick = "return false;";
      base.OnPreRender(e);
    }
  }
}
