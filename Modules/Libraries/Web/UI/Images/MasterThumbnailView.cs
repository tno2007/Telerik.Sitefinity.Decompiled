// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  [ControlTemplateInfo("LibrariesResources", "ImagesMasterThumbnailViewFriendlyName", "ImagesTitle")]
  public class MasterThumbnailView : BaseMasterThumbnailView<Telerik.Sitefinity.Libraries.Model.Image>
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailView.ascx");

    /// <summary>Gets the query.</summary>
    /// <value>The query.</value>
    public override IQueryable<Telerik.Sitefinity.Libraries.Model.Image> Query => this.Manager.GetImages();

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="!:IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected override string DefaultLayoutTemplateName => MasterThumbnailView.layoutTemplatePath;

    /// <summary>Configures the detail link.</summary>
    /// <param name="singleItemLink">The single item link.</param>
    /// <param name="dataItem">The data item.</param>
    /// <param name="item">The item.</param>
    protected override void ConfigureDetailLink(
      HyperLink singleItemLink,
      Telerik.Sitefinity.Libraries.Model.Image dataItem,
      RadListViewItem item)
    {
      string str = string.IsNullOrWhiteSpace(this.ThumbnailName) ? "0" : this.ThumbnailName;
      singleItemLink.ImageUrl = dataItem.ResolveThumbnailUrl(str);
      if (dataItem.IsVectorGraphics())
        dataItem.ApplyThumbnailProfileToControl((WebControl) singleItemLink, str);
      singleItemLink.Text = (string) dataItem.AlternativeText;
      Lstring lstring = dataItem.Description;
      if (string.IsNullOrEmpty((string) lstring))
        lstring = dataItem.Title;
      singleItemLink.ToolTip = (string) lstring;
    }
  }
}
