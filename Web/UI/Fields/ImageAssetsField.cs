// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Fields.ImageAssetsField
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Web.UI.PublicControls.Enums;

namespace Telerik.Sitefinity.Web.UI.Fields
{
  /// <summary>
  /// This class extends the AssetsField class with a functionality specific for images.
  /// </summary>
  public class ImageAssetsField : AssetsField
  {
    /// <summary>
    /// Gets or sets whether the single image should be shown as thumbnail.
    /// </summary>
    public bool IsThumbnail { get; set; }

    /// <summary>
    /// Gets or sets whether the single image should be shown as thumbnail with the specified name.
    /// </summary>
    public string ThumbnailName { get; set; }

    /// <summary>
    /// If true when click the control will open the original image
    /// </summary>
    public bool OpenOriginalImageOnClick { get; set; }

    internal override void DisplaySingleImageReadMode()
    {
      base.DisplaySingleImageReadMode();
      if (!this.ImageControlReadMode.Visible)
        return;
      this.ImageControlReadMode.ThumbnailName = this.ThumbnailName;
      this.ImageControlReadMode.OpenOriginalImageOnClick = this.OpenOriginalImageOnClick;
      if (!this.IsThumbnail)
        return;
      this.ImageControlReadMode.DisplayMode = ImageDisplayMode.Thumbnail;
    }
  }
}
