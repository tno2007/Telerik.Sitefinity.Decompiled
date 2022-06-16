// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.MasterTableDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>The master table view of download documents control.</summary>
  [ControlTemplateInfo("LibrariesResources", "DocumentsTableDetailViewFriendlyName", "DocumentsTitle")]
  public class MasterTableDetailView : DownloadMasterViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableDetailView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableDetailView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MasterTableDetailView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets or sets the query.</summary>
    /// <value>The query.</value>
    protected new IQueryable<Document> Query { get; set; }

    /// <summary>
    /// Gets reference to the RadGrid that will render documents items.
    /// </summary>
    /// <value>The grid.</value>
    protected internal virtual RadGrid DocumentsGrid => this.Container.GetControl<RadGrid>("documentsGrid", true);

    /// <summary>
    /// Handles the DataBound event of the DocumentsGrid control.
    /// </summary>
    public override void DataBindDocumentList()
    {
      if (this.TotalCount == 0)
      {
        this.DocumentsGrid.Visible = false;
      }
      else
      {
        if (this.ThumbnailType != ThumbnailType.NoIcons)
          this.DocumentsGrid.CssClass += " sfHasIcons";
        this.DocumentsGrid.AutoGenerateColumns = false;
        this.DocumentsGrid.ItemDataBound += new GridItemEventHandler(this.DocumentsGrid_ItemDataBound);
        this.DocumentsGrid.ItemCreated += new GridItemEventHandler(this.DocumentsGrid_ItemCreated);
        List<Document> list = base.Query.ToList<Document>();
        this.DocumentsGrid.DataSource = (object) list;
        ((IEnumerable<IDataItem>) list).SetRelatedDataSourceContext();
        string fullName = typeof (Document).FullName;
        if (!SystemManager.IsModuleEnabled("Comments") || !CommentsUtilities.GetThreadConfigByType(fullName).AllowComments)
          this.DocumentsGrid.MasterTableView.GetColumn("commentsColumn").Visible = false;
        else if (CommentsUtilities.GetThreadConfigByType(fullName).EnableRatings)
        {
          this.DocumentsGrid.MasterTableView.GetColumn("commentsColumn").HeaderText = Res.Get<CommentsResources>("ReviewsTitle");
          this.DocumentsGrid.MasterTableView.GetColumn("commentsColumn").ItemStyle.CssClass = "sfdownloadCommentsWithRating";
        }
        this.DocumentsGrid.DataBind();
      }
    }

    /// <summary>
    /// Handles the ItemCreated event of the DocumentsGrid control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
    private void DocumentsGrid_ItemCreated(object sender, GridItemEventArgs e)
    {
      object dataItem = e.Item.DataItem;
      if (!(e.Item.FindControl("detailsViewHyperLink") is DetailsViewHyperLink control))
        return;
      control.DataItem = dataItem;
    }

    /// <summary>
    /// Handles the ItemDataBound event of the DocumentsGrid control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.GridItemEventArgs" /> instance containing the event data.</param>
    private void DocumentsGrid_ItemDataBound(object sender, GridItemEventArgs e)
    {
      if (e.Item.ItemType != GridItemType.Item && e.Item.ItemType != GridItemType.AlternatingItem)
        return;
      object dataItem = e.Item.DataItem;
      GridDataItem gridDataItem1 = e.Item as GridDataItem;
      Document document = (Document) dataItem;
      if (document != null)
      {
        string str = document.Extension;
        if (str.Length > 0)
          str = str.Remove(0, 1);
        GridDataItem gridDataItem2 = gridDataItem1;
        gridDataItem2.CssClass = gridDataItem2.CssClass + " sf" + str.ToLower();
        TableCell tableCell1 = gridDataItem1["documentType"];
        if (tableCell1 != null)
          tableCell1.Text = str.ToUpperInvariant();
        TableCell tableCell2 = gridDataItem1["totalSize"];
        if (tableCell2 != null)
          tableCell2.Text = this.ConfigureBytes(document.TotalSize);
        if (e.Item.FindControl("documentLink") is HyperLink control)
          control.NavigateUrl = document.MediaUrl;
      }
      DetailsViewHyperLink control1 = e.Item.FindControl("detailsViewHyperLink") as DetailsViewHyperLink;
      string fullName = e.Item.DataItem.GetType().FullName;
      CommentsSettingsElement threadConfigByType = CommentsUtilities.GetThreadConfigByType(fullName);
      if (!threadConfigByType.AllowComments)
        return;
      if (threadConfigByType.EnableRatings)
      {
        gridDataItem1["commentsColumn"].Controls.Add((Control) new CommentsAverageRatingControl()
        {
          NavigateUrl = control1.NavigateUrl,
          ThreadKey = ControlUtilities.GetLocalizedKey((object) ((Telerik.Sitefinity.GenericContent.Model.Content) e.Item.DataItem).Id, (string) null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(fullName)),
          ThreadType = fullName,
          DisplayMode = "MediumText"
        });
      }
      else
      {
        CommentsCountControl child = new CommentsCountControl();
        child.NavigateUrl = control1.NavigateUrl;
        child.ThreadKey = ControlUtilities.GetLocalizedKey((object) ((Telerik.Sitefinity.GenericContent.Model.Content) e.Item.DataItem).Id, (string) null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(fullName));
        child.ThreadType = fullName;
        child.DisplayMode = "IconOnly";
        gridDataItem1["commentsColumn"].Controls.Add((Control) child);
      }
    }
  }
}
