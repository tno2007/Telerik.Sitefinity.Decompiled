// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SfImage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Works just as a normal <see cref="T:System.Web.UI.Image" /> control, but removes the automatically added inline style.
  /// </summary>
  public class SfImage : Image
  {
    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      Unit unit;
      if (this.Width.Value != 0.0)
      {
        AttributeCollection attributes = this.Attributes;
        unit = this.Width;
        string str = unit.ToString();
        attributes.Add("width", str);
      }
      unit = this.Height;
      if (unit.Value != 0.0)
      {
        AttributeCollection attributes = this.Attributes;
        unit = this.Height;
        string str = unit.ToString();
        attributes.Add("height", str);
      }
      using (StringWriter writer1 = new StringWriter())
      {
        using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
          base.RenderBeginTag(writer2);
        string input = writer1.ToString();
        string replacement;
        if (!this.Style.Value.IsNullOrWhitespace())
          replacement = " style=\"{0}\"".Arrange((object) this.Style.Value);
        else
          replacement = "";
        string str = Regex.Replace(input, "\\s*style=[\"'][^\"']*[\"']", replacement, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        writer.Write(str);
      }
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// We do nothing here.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <summary>
    /// Renders the image control contents to the specified writer.
    /// We do nothing here.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
    }
  }
}
