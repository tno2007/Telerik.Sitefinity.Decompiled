// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.UI.Images.DetailSimpleView
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.DataResolving;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ContentUI.Contracts;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Modules.Libraries.Web.UI.Images
{
  [ControlTemplateInfo("LibrariesResources", "ImagesDetailSimpleViewFriendlyName", "ImagesTitle")]
  public class DetailSimpleView : BaseDetailSimpleView<Telerik.Sitefinity.Libraries.Model.Image>
  {
    internal const string layoutTemplateName = "Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.DetailSimpleView.ascx";
    public static readonly string layoutTemplatePath = ControlUtilities.ToVppPath("Telerik.Sitefinity.Resources.Templates.Frontend.Libraries.Images.DetailSimpleView.ascx");

    /// <summary>Gets the name of the embedded layout template.</summary>
    /// <value></value>
    /// <remarks>
    /// Override this property to change the embedded template to be used with the dialog
    /// </remarks>
    protected override string LayoutTemplateName => (string) null;

    /// <summary>Gets or sets the layout template path.</summary>
    /// <value>The layout template path.</value>
    public override string LayoutTemplatePath
    {
      get
      {
        string layoutTemplatePath = base.LayoutTemplatePath;
        return string.IsNullOrEmpty(layoutTemplatePath) ? DetailSimpleView.layoutTemplatePath : layoutTemplatePath;
      }
      set => base.LayoutTemplatePath = value;
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
      base.InitializeControls(container, definition);
      this.DetailsView.LayoutCreated += new EventHandler<EventArgs>(this.DetailsView_LayoutCreated);
    }

    /// <summary>Configures the detail control.</summary>
    /// <param name="listViewItem">The list view item.</param>
    protected override void ConfigureDetailControl(RadListViewItem listViewItem)
    {
      System.Web.UI.WebControls.Image control = (System.Web.UI.WebControls.Image) listViewItem.FindControl("imageControl");
      if (control == null || !(listViewItem is RadListViewDataItem listViewDataItem) || !(listViewDataItem.DataItem is Telerik.Sitefinity.Libraries.Model.Image dataItem))
        return;
      if (this.MasterViewDefinition is MediaContentMasterDefinition masterViewDefinition)
      {
        if (string.IsNullOrWhiteSpace(masterViewDefinition.SingleItemThumbnailsName))
        {
          control.ImageUrl = dataItem.ResolveMediaUrl(false, (CultureInfo) null);
        }
        else
        {
          control.ImageUrl = dataItem.ResolveThumbnailUrl(masterViewDefinition.SingleItemThumbnailsName);
          if (dataItem.IsVectorGraphics())
            dataItem.ApplyThumbnailProfileToControl((WebControl) control, masterViewDefinition.SingleItemThumbnailsName);
        }
      }
      control.AlternateText = (string) dataItem.AlternativeText;
      control.GenerateEmptyAlternateText = true;
    }

    /// <summary>
    /// Handles the LayoutCreated event of the DetailsView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="T:System.EventArgs" /> instance containing the event data.</param>
    private void DetailsView_LayoutCreated(object sender, EventArgs e)
    {
      if (!(this.Host.DetailViewDefinition as IImagesViewDetailDefinition).EnablePrevNextLinks.Value)
        return;
      IContentViewMasterDefinition masterViewDefinition = this.Host.MasterViewDefinition;
      Telerik.Sitefinity.Libraries.Model.Image detailItem = this.Host.DetailItem as Telerik.Sitefinity.Libraries.Model.Image;
      PrevAndNextSupportableControl<Telerik.Sitefinity.Libraries.Model.Image> supportableControl = new PrevAndNextSupportableControl<Telerik.Sitefinity.Libraries.Model.Image>(masterViewDefinition.ItemsParentId, masterViewDefinition.SortExpression, masterViewDefinition.FilterExpression, this.Host.ControlDefinition.ProviderName, detailItem);
      Telerik.Sitefinity.Libraries.Model.Image prevItem = supportableControl.PrevItem;
      Telerik.Sitefinity.Libraries.Model.Image nextItem = supportableControl.NextItem;
      if (this.DetailsView.FindControl("previousImageLink") is HyperLink control1)
        this.ConfigurePrevNextLink(control1, prevItem);
      if (!(this.DetailsView.FindControl("nextImageLink") is HyperLink control2))
        return;
      this.ConfigurePrevNextLink(control2, nextItem);
    }

    private string GetNavigateUrl(object dataItem)
    {
      Guid detailsPageId = this.Host.MasterViewDefinition.DetailsPageId;
      return detailsPageId != Guid.Empty ? DataResolver.Resolve(dataItem, "URL", (string) null, detailsPageId.ToString()) : DataResolver.Resolve(dataItem, "URL");
    }

    /// <summary>Configures the link.</summary>
    /// <param name="imageLink">The image link.</param>
    /// <param name="image">The image.</param>
    private void ConfigurePrevNextLink(HyperLink imageLink, Telerik.Sitefinity.Libraries.Model.Image image)
    {
      if (image == null)
        return;
      imageLink.NavigateUrl = this.GetNavigateUrl((object) image);
      imageLink.Visible = true;
      if ((this.Host.DetailViewDefinition as IImagesViewDetailDefinition).PrevNextLinksDisplayMode != PrevNextLinksDisplayMode.Thumbnails)
        return;
      string name = (string) null;
      MediaContentMasterDefinition masterViewDefinition = this.MasterViewDefinition as MediaContentMasterDefinition;
      if (!masterViewDefinition.ThumbnailsName.IsNullOrEmpty())
        name = masterViewDefinition.ThumbnailsName;
      imageLink.ImageUrl = image.ResolveThumbnailUrl(name);
      imageLink.Text = (string) image.AlternativeText;
      imageLink.ToolTip = (string) image.Description;
    }
  }
}
