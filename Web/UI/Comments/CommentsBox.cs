// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Comments.CommentsBox
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.Fields.Enums;

namespace Telerik.Sitefinity.Web.UI.Comments
{
  /// <summary>This control is used to show comments info.</summary>
  public class CommentsBox : SimpleView
  {
    private string hyperLinkNoCommentsText = Res.Get<Labels>().NoCommentsLabel;
    private string hyperLinkStringFormat = "{0} {1}";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Comments.CommentsBox.ascx");

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.Comments.CommentsBox" /> class.
    /// </summary>
    public CommentsBox() => this.LayoutTemplatePath = CommentsBox.layoutTemplatePath;

    /// <summary>Gets or sets the current mode.</summary>
    /// <value>Mode to be used.</value>
    public CommentsBoxDisplayMode DisplayMode { get; set; }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value>Comments count</value>
    public int CommentsCount { get; set; }

    /// <summary>
    /// Gets or sets the CSS class name of the comments hyper link, if there are no comments.
    /// When <c>null</c>, the <see cref="!:CssClass" /> value is used.
    /// </summary>
    public virtual string NoCommentsCssClass { get; set; }

    /// <summary>
    /// Gets or sets the text of the comments hyper link, if there are no comments.
    /// </summary>
    public virtual string HyperLinkNoCommentsText
    {
      get => this.hyperLinkNoCommentsText;
      set => this.hyperLinkNoCommentsText = value;
    }

    /// <summary>
    /// Gets or sets the format of the comments hyper link, if there are comments.
    /// This format will be used with <see cref="M:System.String.Format(System.String,System.Object)" />,
    /// passing the <see cref="P:Telerik.Sitefinity.Web.UI.Comments.CommentsBox.CommentsCount" /> as <c>{0}</c>
    /// and the ContentResources.Comment/Comments resource value as <c>{1}</c>.
    /// </summary>
    public virtual string HyperLinkStringFormat
    {
      get => this.hyperLinkStringFormat;
      set => this.hyperLinkStringFormat = value;
    }

    /// <summary>Gets the hyper link used to show number of comments.</summary>
    protected DetailsViewHyperLink HyperLink => this.Container.GetControl<DetailsViewHyperLink>("commentsLink", true);

    /// <inheritdoc />
    protected override string LayoutTemplateName => (string) null;

    /// <inheritdoc />
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
    }

    /// <inheritdoc />
    public override void RenderEndTag(HtmlTextWriter writer)
    {
    }

    /// <inheritdoc />
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <inheritdoc />
    protected virtual void InitializeControls()
    {
      this.DisplayMode = this.CommentsCount <= 0 ? CommentsBoxDisplayMode.NoneComments : CommentsBoxDisplayMode.ExistingComments;
      switch (this.DisplayMode)
      {
        case CommentsBoxDisplayMode.ExistingComments:
          ContentResources contentResources = Res.Get<ContentResources>();
          this.HyperLink.Text = string.Format(this.HyperLinkStringFormat, (object) this.CommentsCount, this.CommentsCount == 1 ? (object) contentResources.Comment : (object) contentResources.Comments);
          this.HyperLink.CssClass = this.CssClass;
          this.HyperLink.NavigateUrl = string.Format("{0}#comments", (object) this.HyperLink.NavigateUrl);
          break;
        case CommentsBoxDisplayMode.NoneComments:
          this.HyperLink.Text = this.HyperLinkNoCommentsText;
          this.HyperLink.CssClass = this.NoCommentsCssClass ?? this.CssClass;
          break;
      }
    }

    /// <inheritdoc />
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      this.InitializeControls();
    }
  }
}
