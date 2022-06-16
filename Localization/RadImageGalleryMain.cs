// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadImageGalleryMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadImageGalleryMainDescription", Name = "RadImageGallery.Main", ResourceClassId = "RadImageGallery.Main", Title = "RadImageGalleryMainTitle", TitlePlural = "RadImageGalleryMainPlural")]
  public sealed class RadImageGalleryMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadImageGalleryMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadImageGalleryMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadImageGalleryMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadImageGalleryMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    [ResourceEntry("RadImageGalleryMainTitle", Description = "Title of this class.", LastModified = "2014/05/08", Value = "RadImageGalleryMain")]
    public string RadImageGalleryMainTitle => this[nameof (RadImageGalleryMainTitle)];

    [ResourceEntry("RadImageGalleryMainDescription", Description = "The description of this class.", LastModified = "2014/05/08", Value = "Resource strings for RadImageGalleryMain.")]
    public string RadImageGalleryMainDescription => this[nameof (RadImageGalleryMainDescription)];

    [ResourceEntry("RadImageGalleryMainPlural", Description = "The title plural of this class.", LastModified = "2014/05/08", Value = "RadImageGalleryMain")]
    public string RadImageGalleryMainPlural => this[nameof (RadImageGalleryMainPlural)];

    [ResourceEntry("CloseButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Close")]
    public string CloseButtonText => this[nameof (CloseButtonText)];

    [ResourceEntry("EnterFullScreenButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Enter FullScreen")]
    public string EnterFullScreenButtonText => this[nameof (EnterFullScreenButtonText)];

    [ResourceEntry("ExitFullScreenButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Exit FullScreen")]
    public string ExitFullScreenButtonText => this[nameof (ExitFullScreenButtonText)];

    [ResourceEntry("HideThumbnailsButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Hide Thumbnails")]
    public string HideThumbnailsButtonText => this[nameof (HideThumbnailsButtonText)];

    [ResourceEntry("ItemsCounterFormat", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Item {0} of {1}")]
    public string ItemsCounterFormat => this[nameof (ItemsCounterFormat)];

    [ResourceEntry("NextImageButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Next Image")]
    public string NextImageButtonText => this[nameof (NextImageButtonText)];

    [ResourceEntry("PagerTextFormat", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Page {0} of {1}")]
    public string PagerTextFormat => this[nameof (PagerTextFormat)];

    [ResourceEntry("PauseButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Pause Slideshow")]
    public string PauseButtonText => this[nameof (PauseButtonText)];

    [ResourceEntry("PlayButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Play Slideshow")]
    public string PlayButtonText => this[nameof (PlayButtonText)];

    [ResourceEntry("PrevImageButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Previous Image")]
    public string PrevImageButtonText => this[nameof (PrevImageButtonText)];

    [ResourceEntry("ReservedResource", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("ScrollNextButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Scroll Next")]
    public string ScrollNextButtonText => this[nameof (ScrollNextButtonText)];

    [ResourceEntry("ScrollPrevButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Scroll Prev")]
    public string ScrollPrevButtonText => this[nameof (ScrollPrevButtonText)];

    [ResourceEntry("ShowThumbnailsButtonText", Description = "RadImageGalleryMain resource strings.", LastModified = "2014/06/19", Value = "Show Thumbnails")]
    public string ShowThumbnailsButtonText => this[nameof (ShowThumbnailsButtonText)];

    [ResourceEntry("MobileItemsCounterFormat", Description = "RadImageGalleryMain resource strings.", LastModified = "2015-06-25", Value = "{0} / {1}")]
    public string MobileItemsCounterFormat => this[nameof (MobileItemsCounterFormat)];
  }
}
