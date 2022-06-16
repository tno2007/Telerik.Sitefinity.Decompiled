// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents.MasterListDetailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Documents
{
  /// <summary>
  /// The master list detail view of download documents control.
  /// </summary>
  [ControlTemplateInfo("LibrariesResources", "DocumentsListDetailViewFriendlyName", "DocumentsTitle")]
  public class MasterListDetailView : DownloadMasterViewBase
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListDetailView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Documents.MasterListDetailView.ascx");

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
      get => string.IsNullOrEmpty(base.LayoutTemplatePath) ? MasterListDetailView.layoutTemplatePath : base.LayoutTemplatePath;
      set => base.LayoutTemplatePath = value;
    }

    /// <summary>Gets the repeater for documents list.</summary>
    /// <value>The repeater.</value>
    protected internal virtual RadListView DocumentsList => this.Container.GetControl<RadListView>(nameof (DocumentsList), true);

    /// <summary>DataBinds the document list.</summary>
    public override void DataBindDocumentList()
    {
      if (this.TotalCount == 0)
      {
        this.DocumentsList.Visible = false;
      }
      else
      {
        List<Document> list = this.Query.ToList<Document>();
        this.DocumentsList.DataSource = (object) list;
        ((IEnumerable<IDataItem>) list).SetRelatedDataSourceContext();
        this.DocumentsList.DataBind();
      }
    }
  }
}
