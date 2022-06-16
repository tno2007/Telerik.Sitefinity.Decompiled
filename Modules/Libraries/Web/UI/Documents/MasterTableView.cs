// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.MasterTableView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>The master table view of download documents control.</summary>
  [ControlTemplateInfo("LibrariesResources", "DocumentsMasterTableViewFriendlyName", "DocumentsTitle")]
  public class MasterTableView : DownloadMasterViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterTableView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MasterTableView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

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
      object obj;
      if (this.Host.DataSource != null)
        obj = (object) this.Host.DataSource;
      else
        ((IEnumerable<IDataItem>) (obj = (object) this.Query.ToList<Document>())).SetRelatedDataSourceContext();
      if (this.DataSource != null)
        obj = (object) this.DataSource;
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
        this.DocumentsGrid.DataSource = obj;
        this.DocumentsGrid.DataBind();
      }
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
      if (document == null)
        return;
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
      if (!(e.Item.FindControl("documentLink") is HyperLink control))
        return;
      control.NavigateUrl = document.MediaUrl;
    }
  }
}
