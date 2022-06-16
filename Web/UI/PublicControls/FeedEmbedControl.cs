// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.PublicControls.FeedEmbedControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.Publishing.Configuration;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.PublicControls.Designers;

namespace Telerik.Sitefinity.Web.UI.PublicControls
{
  [ControlDesigner(typeof (FeedEmbedControlDesigner))]
  [PropertyEditorTitle(typeof (Labels), "Feed")]
  public class FeedEmbedControl : SimpleView, ICustomWidgetVisualization
  {
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.PublicControls.FeedEmbedControl.ascx");
    private string toolTip;

    /// <summary>Gets or sets the title of the feed.</summary>
    /// <value>The title of the feed.</value>
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the programmatic identifier assigned to the feed.
    /// </summary>
    /// <value></value>
    /// <returns>The programmatic identifier assigned to the feed.</returns>
    public Guid FeedID { get; set; }

    /// <summary>Gets or sets the size of the feed icon.</summary>
    /// <value>The size of the feed icon.</value>
    public FeedEmbedControl.IconSize FeedIconSize { get; set; }

    /// <summary>Gets or sets the feed link insertion method.</summary>
    /// <value>The feed link insertion method.</value>
    public FeedEmbedControl.LinkInsertionMethod FeedInsertionMethod { get; set; }

    /// <summary>Gets or sets the tooltip.</summary>
    /// <value>The tooltip.</value>
    public override string ToolTip
    {
      get => this.toolTip;
      set => this.toolTip = value;
    }

    public string TextToDisplay { get; set; }

    /// <summary>
    /// Gets or sets the Cascading Style Sheet (CSS) class rendered by the Web server control on the client.
    /// </summary>
    /// <value></value>
    /// <returns>
    /// The CSS class rendered by the Web server control on the client. The default is <see cref="F:System.String.Empty" />.
    /// </returns>
    public new string CssClass { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether [open in new window].
    /// </summary>
    /// <value><c>true</c> if [open in new window]; otherwise, <c>false</c>.</value>
    public bool OpenInNewWindow { get; set; }

    /// <summary>Indicates if the control is empty.</summary>
    /// <value></value>
    public bool IsEmpty => this.FeedID == Guid.Empty;

    /// <summary>
    /// Gets the text to be shown when the box in the designer is empty
    /// </summary>
    /// <value></value>
    public string EmptyLinkText => Res.Get<PublicControlsResources>().SelectFeed;

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>
    /// Gets or sets the path of the external template to be used by the control.
    /// </summary>
    /// <value></value>
    public override string LayoutTemplatePath
    {
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? FeedEmbedControl.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container"></param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(GenericContainer container)
    {
    }

    /// <summary>
    /// Raises the <see cref="E:System.Web.UI.Control.PreRender" /> event.
    /// </summary>
    /// <param name="e">An <see cref="T:System.EventArgs" /> object that contains the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {
      base.OnPreRender(e);
      if (this.Page == null)
        return;
      Guid feedId = this.FeedID;
      if (!(this.FeedID != Guid.Empty))
        return;
      Guid pipeID = this.FeedID;
      RssPipeSettings rssPipeSettings = PublishingManager.GetManager().GetPipeSettings<RssPipeSettings>().Where<RssPipeSettings>((Expression<Func<RssPipeSettings, bool>>) (p => p.Id == pipeID)).FirstOrDefault<RssPipeSettings>();
      if (rssPipeSettings == null)
        return;
      string str1 = RouteHelper.ResolveUrl("~/" + Config.Get<PublishingConfig>().FeedsBaseURl + "/" + rssPipeSettings.UrlName, UrlResolveOptions.Absolute);
      string str2 = !string.IsNullOrEmpty(this.TextToDisplay) ? HttpUtility.HtmlEncode(this.TextToDisplay) : string.Empty;
      if (this.Page.Header != null && (this.FeedInsertionMethod == FeedEmbedControl.LinkInsertionMethod.AddressBarOnly || this.FeedInsertionMethod == FeedEmbedControl.LinkInsertionMethod.PageAndAddressBar))
        this.Page.Header.Controls.Add((Control) new Literal()
        {
          Text = string.Format("<link rel=\"alternate\" type=\"application/rss+xml\" title=\"{0}\" href=\"{1}\"/>", (object) str2, (object) str1)
        });
      if (this.FeedInsertionMethod != FeedEmbedControl.LinkInsertionMethod.PageOnly && this.FeedInsertionMethod != FeedEmbedControl.LinkInsertionMethod.PageAndAddressBar)
        return;
      string cssClass = this.CssClass;
      switch (this.FeedIconSize)
      {
        case FeedEmbedControl.IconSize.BigIcon:
          cssClass += " sfFeedBigIcn";
          break;
        case FeedEmbedControl.IconSize.SmallIcon:
          cssClass += " sfFeedSmallIcn";
          break;
      }
      string str3 = this.OpenInNewWindow ? "target=\"_blank\"" : "";
      this.Controls.Add((Control) new Literal()
      {
        Text = string.Format("<a href=\"{0}\" class=\"{1}\" title=\"{2}\" {3}>{4}</a>", (object) str1, (object) cssClass, (object) this.ToolTip, (object) str3, (object) str2)
      });
    }

    protected override HtmlTextWriterTag TagKey => HtmlTextWriterTag.Div;

    /// <summary>
    /// Writes the <see cref="T:System.Web.UI.WebControls.CompositeControl" /> content to the specified <see cref="T:System.Web.UI.HtmlTextWriter" /> object, for display on the client.
    /// </summary>
    /// <param name="writer">An <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.GetIndexRenderMode() != IndexRenderModes.Normal)
        return;
      base.Render(writer);
    }

    /// <summary>Enumeration for the link insertion method</summary>
    public enum LinkInsertionMethod
    {
      /// <summary>
      /// Link is inserted both on the page and the address bar feeds
      /// </summary>
      PageAndAddressBar,
      /// <summary>Link is inserted on the address bar feeds only</summary>
      AddressBarOnly,
      /// <summary>Link is inserted on the page only</summary>
      PageOnly,
    }

    /// <summary>Enumeration for the link icon size</summary>
    public enum IconSize
    {
      /// <summary>A huge very very large extremely gigantic icon</summary>
      BigIcon,
      /// <summary>
      /// A tiny super small bearly visible kinda microscopic icon
      /// </summary>
      SmallIcon,
      /// <summary>
      /// No icon. At all. At all at all. Whatsoever. There isn't, I say. None. It's not invisible, it's simply not there.
      /// </summary>
      NoIcon,
    }
  }
}
