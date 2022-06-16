// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.DetailsListView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>The details list view of download documents control.</summary>
  [ControlTemplateInfo("LibrariesResources", "DocumentsDetailViewFriendlyName", "DocumentsTitle")]
  public class DetailsListView : DownloadListViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.DetailsListView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.DetailsListView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? DetailsListView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Displays a download link above the description</summary>
    protected bool? ShowDownloadLinkAboveDescription { get; set; }

    /// <summary>Displays a download link below the description</summary>
    protected bool? ShowDownloadLinkBelowDescription { get; set; }

    /// <summary>Gets the repeater for documents list.</summary>
    /// <value>The repeater.</value>
    protected internal virtual RadListView DetailsView => this.Container.GetControl<RadListView>(nameof (DetailsView), true);

    /// <summary>Configures the documents query.</summary>
    /// <param name="definition">The content view definition for DowloadListView control.</param>
    protected override void ConfigureDocumentsQuery(IContentViewDefinition definition)
    {
      IDownloadListViewDetailDefinition detailDefinition = definition as IDownloadListViewDetailDefinition;
      IDownloadListViewMasterDefinition masterViewDefinition = this.MasterViewDefinition as IDownloadListViewMasterDefinition;
      if (detailDefinition != null && detailDefinition.ThumbnailType != ThumbnailType.NoIcons)
        this.DetailsView.CssClass += " sfHasIcons";
      if (masterViewDefinition == null)
        return;
      this.ShowDownloadLinkBelowDescription = masterViewDefinition.ShowDownloadLinkBelowDescription;
      this.ShowDownloadLinkAboveDescription = masterViewDefinition.ShowDownloadLinkAboveDescription;
    }

    /// <summary>Configures the documents query.</summary>
    /// <param name="definition"></param>
    public override void DataBindDocumentList()
    {
      if (!(this.Host.DetailItem is Document detailItem))
      {
        if (!this.IsDesignMode())
          return;
        this.Controls.Clear();
        this.Controls.Add((Control) new LiteralControl("A news item was not selected or has been deleted. Please select another one."));
      }
      else
      {
        this.DetailsView.DataSource = (object) new Document[1]
        {
          detailItem
        };
        this.DetailsView.ItemDataBound += new EventHandler<RadListViewItemEventArgs>(this.DocumentsList_ItemDataBound);
      }
    }

    private void ConfigureDocumentSection(
      HyperLink documentLink,
      SitefinityLabel documentInfoLabel,
      HtmlGenericControl rowItem,
      Document document,
      bool showSection)
    {
      string str = document.Extension;
      if (str.Length > 0)
        str = str.Remove(0, 1);
      if (rowItem != null)
      {
        if (!showSection)
        {
          rowItem.Visible = false;
          return;
        }
        rowItem.Visible = true;
        AttributeCollection attributes = rowItem.Attributes;
        attributes["class"] = attributes["class"] + " sf" + str.ToLower();
      }
      if (documentLink != null)
      {
        documentLink.Text = Res.Get<Labels>().Download;
        documentLink.ToolTip = (string) document.Description;
        documentLink.NavigateUrl = document.MediaUrl;
      }
      if (documentInfoLabel == null)
        return;
      documentInfoLabel.Text = string.Format("{0}, {1}", (object) str.ToUpperInvariant(), (object) this.ConfigureBytes(document.TotalSize));
    }

    /// <summary>
    /// Handles the ItemDataBound event of the DetailsView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:Telerik.Web.UI.RadListViewItemEventArgs" /> instance containing the event data.</param>
    protected void DocumentsList_ItemDataBound(object sender, RadListViewItemEventArgs e)
    {
      if (e.Item.ItemType != RadListViewItemType.DataItem && e.Item.ItemType != RadListViewItemType.AlternatingItem)
        return;
      RadListViewDataItem listViewDataItem = e.Item as RadListViewDataItem;
      Document dataItem = (Document) listViewDataItem.DataItem;
      if (dataItem == null)
        return;
      HyperLink control1 = listViewDataItem.FindControl("documentLink") as HyperLink;
      SitefinityLabel control2 = listViewDataItem.FindControl("infoLabel") as SitefinityLabel;
      HtmlGenericControl control3 = listViewDataItem.FindControl("downloadFile") as HtmlGenericControl;
      HyperLink documentLink1 = control1;
      SitefinityLabel documentInfoLabel1 = control2;
      HtmlGenericControl rowItem1 = control3;
      Document document1 = dataItem;
      bool? aboveDescription = this.ShowDownloadLinkAboveDescription;
      bool flag1 = true;
      int num1 = aboveDescription.GetValueOrDefault() == flag1 & aboveDescription.HasValue ? 1 : 0;
      this.ConfigureDocumentSection(documentLink1, documentInfoLabel1, rowItem1, document1, num1 != 0);
      HyperLink control4 = listViewDataItem.FindControl("documentLinkBottom") as HyperLink;
      SitefinityLabel control5 = listViewDataItem.FindControl("infoLabelBottom") as SitefinityLabel;
      HtmlGenericControl control6 = listViewDataItem.FindControl("downloadFileBottom") as HtmlGenericControl;
      HyperLink documentLink2 = control4;
      SitefinityLabel documentInfoLabel2 = control5;
      HtmlGenericControl rowItem2 = control6;
      Document document2 = dataItem;
      bool? belowDescription = this.ShowDownloadLinkBelowDescription;
      bool flag2 = true;
      int num2 = belowDescription.GetValueOrDefault() == flag2 & belowDescription.HasValue ? 1 : 0;
      this.ConfigureDocumentSection(documentLink2, documentInfoLabel2, rowItem2, document2, num2 != 0);
    }
  }
}
