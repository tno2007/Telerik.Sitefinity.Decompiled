// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views.DetailsView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Sitefinity.Web.UI.ContentUI.Views.Backend;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.UI.Views
{
  /// <summary>Displays a single content item on the frontend</summary>
  public class DetailsView : ViewBase
  {
    private string browseAndEditToolbarId = "BrowseAndEditToolbar";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.GenericContent.DetailsView.ascx");

    /// <summary>A radlist for displaying a single item</summary>
    protected virtual RadListView SingleItemContainer => this.Container.GetControl<RadListView>(nameof (DetailsView), true);

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DetailsView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Renders the HTML opening tag of the control to the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      this.AddAttributesToRender(writer);
      writer.RenderBeginTag(HtmlTextWriterTag.Div);
    }

    /// <summary>
    /// Renders the HTML closing tag of the control into the specified writer. This method is used primarily by control developers.
    /// </summary>
    /// <param name="writer">A <see cref="T:System.Web.UI.HtmlTextWriter" /> that represents the output stream to render HTML content on the client.</param>
    public override void RenderEndTag(HtmlTextWriter writer)
    {
      if (string.IsNullOrEmpty(this.CssClass))
        return;
      base.RenderEndTag(writer);
    }

    /// <summary>Initializes the controls.</summary>
    /// <param name="container">The controls container.</param>
    /// <param name="definition">The content view definition.</param>
    /// <remarks>
    /// Initialize your controls in this method. Do not override CreateChildControls method.
    /// </remarks>
    protected override void InitializeControls(
      GenericContainer container,
      IContentViewDefinition definition)
    {
      if (!(definition is IContentViewDetailDefinition))
        return;
      GenericContentView host = (GenericContentView) this.Host;
      if (host.Item == null)
      {
        if (!this.IsDesignMode())
          return;
        this.Controls.Clear();
        this.Controls.Add((Control) new LiteralControl("A generic content item was not selected or has been deleted. Please select another one."));
      }
      else
      {
        this.SingleItemContainer.ItemCreated += new EventHandler<RadListViewItemEventArgs>(this.SingleItemContainer_ItemCreated);
        this.SingleItemContainer.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.SingleItemContainer_ItemDataBound);
        this.SingleItemContainer.DataSource = (object) new ContentItem[1]
        {
          host.Item
        };
      }
    }

    /// <summary>
    /// Handles the ItemDataBound event of the SingleItemContainer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void SingleItemContainer_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType == RadListViewItemType.DataItem || e.Item.ItemType == RadListViewItemType.AlternatingItem)
        this.InitCommentsView(e.Item.FindControl("commentsDetailsView") as ContentView);
      if (!SystemManager.IsBrowseAndEditMode)
        return;
      BrowseAndEditManager.GetCurrent(this.Page).AddBrowseAndEditToolBar((Control) e.Item, this.Host, this.browseAndEditToolbarId);
    }

    /// <summary>
    /// Handles the ItemCreated event of the SingleItemContainer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    private void SingleItemContainer_ItemCreated(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
        return;
      this.InitCommentsView(e.Item.FindControl("commentsListView") as ContentView);
    }

    private void InitCommentsView(ContentView view)
    {
      if (view == null)
        return;
      view.Visible = true;
      view.ControlDefinition.ProviderName = this.Host.ControlDefinition.ProviderName;
      if (!(this.Host.DetailItem is Content detailItem))
        return;
      view.DetailItem = (IDataItem) detailItem;
    }
  }
}
