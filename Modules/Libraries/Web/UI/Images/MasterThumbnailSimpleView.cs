// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.MasterThumbnailSimpleView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  [ControlTemplateInfo("LibrariesResources", "ImagesMasterThumbnailSimpleViewFriendlyName", "ImagesTitle")]
  public class MasterThumbnailSimpleView : MasterThumbnailView
  {
    internal new const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailSimpleView.ascx";
    public new static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.MasterThumbnailSimpleView.ascx");

    /// <summary>
    /// Gets the default name of the layout template.
    /// It will be used if the TemplateName of the <see cref="!:IContentViewDefinition" />
    /// Definition property is not set.
    /// </summary>
    /// <value>The default name of the layout template.</value>
    protected override string DefaultLayoutTemplateName => MasterThumbnailSimpleView.layoutTemplatePath;

    /// <summary>Configures the detail link.</summary>
    /// <param name="singleItemLink">The single item link.</param>
    /// <param name="dataItem">The data item.</param>
    /// <param name="item">The item.</param>
    protected override void ConfigureDetailLink(
      HyperLink singleItemLink,
      Telerik.Sitefinity.Libraries.Model.Image dataItem,
      RadListViewItem item)
    {
      if (this.MasterViewDefinition is MediaContentMasterDefinition masterViewDefinition)
      {
        if (string.IsNullOrWhiteSpace(masterViewDefinition.SingleItemThumbnailsName))
        {
          singleItemLink.ImageUrl = dataItem.ResolveMediaUrl(false, (CultureInfo) null);
        }
        else
        {
          singleItemLink.ImageUrl = dataItem.ResolveThumbnailUrl(masterViewDefinition.SingleItemThumbnailsName);
          if (dataItem.IsVectorGraphics())
            dataItem.ApplyThumbnailProfileToControl((WebControl) singleItemLink, masterViewDefinition.SingleItemThumbnailsName);
        }
      }
      singleItemLink.Text = (string) dataItem.AlternativeText;
      singleItemLink.ToolTip = (string) dataItem.Description;
    }
  }
}
