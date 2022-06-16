// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityLabel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Control which extends the standard <see cref="T:System.Web.UI.WebControls.Label" /> control
  /// </summary>
  public class SitefinityLabel : Label, IHasTextMode
  {
    private HideIfNoTextMode hideIfNoTextMode;
    private bool renderWrapperTag = true;
    private HtmlTextWriterTag wrapperTagName = HtmlTextWriterTag.Span;

    /// <summary>Gets or sets the name of the wrapper tag.</summary>
    /// <value>The name of the wrapper tag.</value>
    [DefaultValue(HtmlTextWriterTag.Span)]
    public HtmlTextWriterTag WrapperTagName
    {
      get => this.AssociatedControlID.Length != 0 ? HtmlTextWriterTag.Label : this.wrapperTagName;
      set => this.wrapperTagName = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the wrapper tag will be rendered.
    /// </summary>
    /// <value><c>true</c> if the wrapper tag will be rendered; otherwise, <c>false</c>.</value>
    [DefaultValue(true)]
    public virtual bool RenderWrapperTag
    {
      get => this.renderWrapperTag;
      set => this.renderWrapperTag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control will be rendered if the text is null or empty.
    /// </summary>
    /// <value><c>true</c> if the control will not be rendered; otherwise, <c>false</c>.</value>
    [DefaultValue(false)]
    public virtual bool HideIfNoText { get; set; }

    public virtual HideIfNoTextMode HideIfNoTextMode
    {
      get => this.hideIfNoTextMode;
      set => this.hideIfNoTextMode = value;
    }

    /// <inheritdoc />
    public TextMode TextMode { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (!this.CssClass.IsNullOrEmpty() && !this.RenderWrapperTag)
        throw new ArgumentException("RenderWrapperTag property cannot be false because CssClass property is set.");
      if (!this.RenderWrapperTag)
        return;
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(this.WrapperTagName);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (!this.RenderWrapperTag)
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that use composition-based implementation to create any child controls they contain in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      if (this.HideIfNoText && this.Text.IsNullOrEmpty() && this.HideIfNoTextMode == HideIfNoTextMode.Server)
        this.Visible = false;
      if (this.HideIfNoText && this.Text.IsNullOrEmpty() && this.HideIfNoTextMode == HideIfNoTextMode.Client)
        this.Style.Add("display", "none");
      this.Text = this.ProcessText(this.Text);
    }
  }
}
