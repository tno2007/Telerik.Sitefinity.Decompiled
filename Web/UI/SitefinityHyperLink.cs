// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityHyperLink
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Works just as a normal <see cref="T:System.Web.UI.HyperLink" /> control, but does not render inline style when ImageUrl is specified.
  /// </summary>
  public class SitefinityHyperLink : HyperLink, IHasTextMode
  {
    private bool resolveNavigateUrl = true;

    /// <inheritdoc />
    public TextMode TextMode { get; set; }

    /// <summary>
    /// Gets or sets the text caption for the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control.
    /// </summary>
    public override string Text
    {
      get => base.Text;
      set => base.Text = this.ProcessText(value);
    }

    /// <summary>
    /// Gets or sets the text displayed when the mouse pointer hovers over the Web server control.
    /// </summary>
    public override string ToolTip
    {
      get => base.ToolTip;
      set => base.ToolTip = this.ProcessText(value);
    }

    /// <summary>
    /// Indicates when the NavigateUrl should be resolved.
    /// If false - the NavigateUrl will not be resolved and rendered as it is.
    /// </summary>
    public bool ResolveNavigateUrl
    {
      get => this.resolveNavigateUrl;
      set => this.resolveNavigateUrl = value;
    }

    /// <summary>
    /// Displays the <see cref="T:System.Web.UI.WebControls.HyperLink" /> control on a page.
    /// Overrides the default implementation in order to use SfImage instead of Image (if ImageUrl is specified).
    /// </summary>
    /// <param name="writer">The output stream to render on the client.</param>
    protected override void RenderContents(HtmlTextWriter writer)
    {
      if (this.ImageUrl.Length > 0)
      {
        SfImage sfImage = new SfImage();
        sfImage.ImageUrl = this.ResolveClientUrl(this.ImageUrl);
        string toolTip = this.ToolTip;
        if (toolTip.Length != 0)
          sfImage.ToolTip = toolTip;
        string text = this.Text;
        if (text.Length != 0)
          sfImage.AlternateText = text;
        sfImage.GenerateEmptyAlternateText = true;
        sfImage.Width = this.Width;
        sfImage.Height = this.Height;
        sfImage.RenderControl(writer);
      }
      else
        base.RenderContents(writer);
    }

    protected override void AddAttributesToRender(HtmlTextWriter writer)
    {
      string navigateUrl = this.NavigateUrl;
      if (!this.ResolveNavigateUrl)
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Href, navigateUrl);
        this.NavigateUrl = string.Empty;
      }
      base.AddAttributesToRender(writer);
    }
  }
}
