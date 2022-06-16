// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.ContentBlockBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.UI;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages.PropertyPersisters;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI
{
  /// <summary>
  /// Control for adding arbitrary blocks of text to the templates
  /// </summary>
  [ControlDesigner(typeof (ContentBlockDesignerBase))]
  public class ContentBlockBase : SimpleView, ICustomWidgetVisualization, IBrowseAndEditable
  {
    private bool hasWrappingTag = true;
    private string wrapperTag = "div";
    private BrowseAndEditToolbar browseAndEditToolbar;
    private List<BrowseAndEditCommand> commands = new List<BrowseAndEditCommand>();
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.GenericContent.ContentBlock.ascx");

    /// <summary>
    /// Gets or sets the HTML content to be displayed by the ContentBlock control.
    /// </summary>
    [DynamicLinksContainer]
    [MultilingualProperty]
    public virtual string Html { get; set; }

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? ContentBlockBase.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
    /// </summary>
    /// <value></value>
    /// <returns>The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.</returns>
    public override string CssClass
    {
      get => string.IsNullOrEmpty(base.CssClass) ? "sfContentBlock" : base.CssClass;
      set => base.CssClass = value;
    }

    /// <summary>Gets or sets the wrapper tag.</summary>
    public virtual string WrapperTag
    {
      get => this.wrapperTag;
      set => this.wrapperTag = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the control will have a wrapping tag.
    /// </summary>
    public virtual bool HasWrappingTag
    {
      get => this.hasWrappingTag;
      set => this.hasWrappingTag = value;
    }

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets the text control containing the content HTML.</summary>
    /// <value>The instance of the text control.</value>
    protected virtual ITextControl ContentHtml => this.Container.GetControl<ITextControl>("contentHtml", true);

    /// <summary>
    /// Represents the browse and edit toolbar for the control
    /// </summary>
    protected virtual BrowseAndEditToolbar BrowseAndEditToolbar
    {
      get
      {
        if (this.browseAndEditToolbar == null)
          this.browseAndEditToolbar = this.Container.GetControl<BrowseAndEditToolbar>("browseAndEditToolbar", true);
        return this.browseAndEditToolbar;
      }
    }

    BrowseAndEditToolbar IBrowseAndEditable.BrowseAndEditToolbar => this.BrowseAndEditToolbar;

    /// <summary>
    /// Gets the information needed to configure this instance.
    /// </summary>
    [PersistenceMode(PersistenceMode.InnerProperty)]
    public BrowseAndEditableInfo BrowseAndEditableInfo { get; set; }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (!this.HasWrappingTag)
        writer.Write("");
      else if (!this.WrapperTag.IsNullOrWhitespace())
      {
        if (!string.IsNullOrEmpty(this.CssClass))
          writer.Write("<" + this.WrapperTag + " class='" + this.CssClass + "'>");
        else
          writer.Write("<" + this.WrapperTag + ">");
      }
      else
        writer.Write("");
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (!this.HasWrappingTag)
        writer.Write("");
      else if (!this.WrapperTag.IsNullOrWhitespace())
        writer.Write("</" + this.WrapperTag + ">");
      else
        writer.Write("");
    }

    /// <summary>
    /// Adds browse and edit commands to be executed by the toolbar
    /// </summary>
    /// <param name="commands">The commands.</param>
    public void AddCommands(IList<BrowseAndEditCommand> commands) => this.commands.AddRange((IEnumerable<BrowseAndEditCommand>) commands);

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
      this.ContentHtml.Text = ControlUtilities.Sanitize(LinkParser.ResolveLinks(this.Html, new GetItemUrl(DynamicLinksParser.GetContentUrl), (ResolveUrl) null, this.IsInlineEditingMode()));
      this.IsEmpty = string.IsNullOrEmpty(this.ContentHtml.Text);
      if (!SystemManager.IsBrowseAndEditMode)
        return;
      this.SetDefaultBrowseAndEditCommands();
      this.BrowseAndEditToolbar.Commands.AddRange((IEnumerable<BrowseAndEditCommand>) this.commands);
      BrowseAndEditManager.GetCurrent(this.Page)?.Add((IBrowseAndEditToolbar) this.BrowseAndEditToolbar);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is empty.
    /// </summary>
    /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
    public bool IsEmpty { get; protected set; }

    /// <summary>Gets the empty link text.</summary>
    /// <value>The empty link text.</value>
    public string EmptyLinkText => Res.Get<ContentResources>().CreateContent;
  }
}
