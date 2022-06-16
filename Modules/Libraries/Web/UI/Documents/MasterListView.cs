// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.MasterListView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>The master list view of download documents control.</summary>
  [ControlTemplateInfo("LibrariesResources", "DocumentsMasterListViewFriendlyName", "DocumentsTitle")]
  public class MasterListView : DownloadMasterViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MasterListView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>
    /// Gets reference to the repeater control that will render documents items.
    /// </summary>
    /// <value>The grid.</value>
    protected internal virtual Repeater DocumentsRepeater => this.Container.GetControl<Repeater>("documentsRepeater", true);

    /// <summary>Gets the items container.</summary>
    /// <value>The items container.</value>
    protected internal virtual HtmlGenericControl ItemsContainer => this.Container.GetControl<HtmlGenericControl>("itemsContainer", true);

    /// <summary>DataBinds the document list.</summary>
    public override void DataBindDocumentList()
    {
      if (this.Query == null && this.DataSource == null)
        return;
      if (this.TotalCount == 0)
      {
        this.ItemsContainer.Visible = false;
        this.DocumentsRepeater.Visible = false;
      }
      else
      {
        this.DocumentsRepeater.ItemDataBound += new RepeaterItemEventHandler(this.DocumentsList_ItemDataBound);
        if (this.DataSource == null)
        {
          List<Document> list = this.Query.ToList<Document>();
          this.DocumentsRepeater.DataSource = (object) list;
          ((IEnumerable<IDataItem>) list).SetRelatedDataSourceContext();
        }
        else
          this.DocumentsRepeater.DataSource = (object) this.DataSource;
        this.DocumentsRepeater.DataBind();
        if (this.ItemsContainer == null)
          return;
        if (this.ThumbnailType == ThumbnailType.SmallIcons)
        {
          this.ItemsContainer.Attributes["class"] += " sfSmallIcns";
        }
        else
        {
          if (this.ThumbnailType != ThumbnailType.BigIcons)
            return;
          this.ItemsContainer.Attributes["class"] += " sfLargeIcns";
        }
      }
    }

    /// <summary>
    /// Handles the ItemDataBound event of the DocumentsList control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.Web.UI.WebControls.RepeaterItemEventArgs" /> instance containing the event data.</param>
    private void DocumentsList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
      if (e.Item.ItemType != ListItemType.Item && e.Item.ItemType != ListItemType.AlternatingItem)
        return;
      Document dataItem = (Document) e.Item.DataItem;
      if (dataItem == null)
        return;
      string str = dataItem.Extension;
      if (str.Length > 0)
        str = str.Remove(0, 1);
      if (e.Item.FindControl("documentLink") is HyperLink control1)
      {
        control1.Text = (string) dataItem.Title;
        control1.ToolTip = (string) dataItem.Description;
        control1.NavigateUrl = dataItem.MediaUrl;
      }
      if (e.Item.FindControl("infoLabel") is SitefinityLabel control2)
        control2.Text = string.Format("{0}, {1}", (object) str.ToUpperInvariant(), (object) this.ConfigureBytes(dataItem.TotalSize));
      if (!(e.Item.FindControl("docItem") is HtmlGenericControl control3))
        return;
      AttributeCollection attributes = control3.Attributes;
      attributes["class"] = attributes["class"] + " sf" + str.ToLower();
    }
  }
}
