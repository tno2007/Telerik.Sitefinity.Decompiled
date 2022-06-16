// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Backend.FieldLabel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI.Backend
{
  /// <summary>Field label control</summary>
  [ParseChildren(false)]
  [PersistChildren(true)]
  [DefaultProperty("Text")]
  [ToolboxData("<{0}:FieldLabel runat=server Width=\"125px\" Height=\"50px\"></{0}:FieldLabel>")]
  public class FieldLabel : WebControl, ITextControl
  {
    /// <summary>Gets or sets the text content of a control.</summary>
    /// <value></value>
    /// <returns>The text content of a control.</returns>
    [Bindable(true)]
    [Category("Appearance")]
    [DefaultValue("")]
    [Localizable(true)]
    public string Text
    {
      get => (string) this.ViewState[nameof (Text)] ?? string.Empty;
      set => this.ViewState[nameof (Text)] = (object) value;
    }

    /// <summary>Gets or sets the target ID.</summary>
    /// <value>The target ID.</value>
    [Bindable(true)]
    [Category("Misc")]
    [DefaultValue("")]
    [Localizable(true)]
    public string TargetID
    {
      get => (string) this.ViewState[nameof (TargetID)] ?? string.Empty;
      set => this.ViewState[nameof (TargetID)] = (object) value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>Renders the contents.</summary>
    /// <param name="output">The output.</param>
    protected override void RenderContents(HtmlTextWriter output)
    {
      if (!string.IsNullOrEmpty(this.CssClass))
        output.AddAttribute(HtmlTextWriterAttribute.Class, this.CssClass);
      if (this.Width != Unit.Empty)
        output.AddStyleAttribute(HtmlTextWriterStyle.Width, this.Width.ToString());
      if (!string.IsNullOrEmpty(this.TargetID))
      {
        string str = string.Empty;
        string[] strArray = this.TargetID.Split(new char[1]
        {
          ':'
        }, 2);
        Control control = this.FindControl(strArray[0]);
        if (control != null)
        {
          str = control.ClientID;
          if (strArray.Length > 1)
            str += strArray[1];
        }
        output.AddAttribute(HtmlTextWriterAttribute.For, str);
      }
      output.AddAttribute(HtmlTextWriterAttribute.Onclick, "needToConfirm = false;");
      output.RenderBeginTag(HtmlTextWriterTag.Label);
      output.Write(this.Text);
      foreach (Control control in this.Controls)
        control.RenderControl(output);
      output.RenderEndTag();
    }
  }
}
