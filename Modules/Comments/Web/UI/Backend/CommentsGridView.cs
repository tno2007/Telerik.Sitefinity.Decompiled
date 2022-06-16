// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Backend.CommentsGridView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Backend
{
  /// <summary>
  /// This is class that represent comments list view in backend
  /// </summary>
  internal class CommentsGridView : CommentsMasterView
  {
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Backend.Comments.CommentsGridView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? CommentsGridView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// The script control type name passed to the <see cref="!:ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected new virtual string ScriptDescriptorTypeName => this.GetType().FullName;

    public new IViewDefinition Definition { get; set; }

    /// <summary>Gets the literal that shows the view title</summary>
    protected virtual Literal ViewTitleLiteral => this.Container.GetControl<Literal>(nameof (ViewTitleLiteral), true);

    /// <summary>Gets the literal that shows the thread title</summary>
    protected virtual Literal ViewTitleThreadLiteral => this.Container.GetControl<Literal>(nameof (ViewTitleThreadLiteral), true);

    protected override void InitializeControls(GenericContainer container)
    {
      base.InitializeControls(container);
      string str1 = this.Page.Request.QueryString["threadType"];
      if (string.IsNullOrEmpty(str1))
        return;
      this.ViewTitleLiteral.Text = Res.Get<CommentsResources>().CommentsFor;
      string str2 = str1;
      switch (str1)
      {
        case "Telerik.Sitefinity.Blogs.Model.BlogPost":
          str2 = Res.Get<CommentsResources>().BlogPostsThreadTitle;
          break;
        case "Telerik.Sitefinity.Ecommerce.Catalog.Model.Product":
          str2 = Res.Get<CommentsResources>().ProductsThreadTitle;
          break;
        case "Telerik.Sitefinity.Events.Model.Event":
          str2 = Res.Get<CommentsResources>().EventsThreadTitle;
          break;
        case "Telerik.Sitefinity.Forms.Model.Form":
          str2 = Res.Get<CommentsResources>().FormsThreadTitle;
          break;
        case "Telerik.Sitefinity.Libraries.Model.Document":
          str2 = Res.Get<CommentsResources>().DocumentsThreadTitle;
          break;
        case "Telerik.Sitefinity.Libraries.Model.Image":
          str2 = Res.Get<CommentsResources>().ImagesThreadTitle;
          break;
        case "Telerik.Sitefinity.Libraries.Model.Video":
          str2 = Res.Get<CommentsResources>().VideosThreadTitle;
          break;
        case "Telerik.Sitefinity.Lists.Model.ListItem":
          str2 = Res.Get<CommentsResources>().ListItemsThreadTitle;
          break;
        case "Telerik.Sitefinity.News.Model.NewsItem":
          str2 = Res.Get<CommentsResources>().NewsItemsThreadTitle;
          break;
      }
      this.ViewTitleThreadLiteral.Text = str2;
    }
  }
}
