// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.RadMediaPlayerMain
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents string resources for RadControls.</summary>
  [ObjectInfo(Description = "RadMediaPlayerMainDescription", Name = "RadMediaPlayer.Main", ResourceClassId = "RadMediaPlayer.Main", Title = "RadMediaPlayerMainTitle", TitlePlural = "RadMediaPlayerMainTitlePlural")]
  public sealed class RadMediaPlayerMain : Resource
  {
    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadMediaPlayerMain" /> class with the default <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    public RadMediaPlayerMain()
    {
    }

    /// <summary>
    /// Initializes new instance of <see cref="T:Telerik.Sitefinity.Localization.RadMediaPlayerMain" /> class with the provided <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" />.
    /// </summary>
    /// <param name="dataProvider"><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceDataProvider" /></param>
    public RadMediaPlayerMain(ResourceDataProvider dataProvider)
      : base(dataProvider)
    {
    }

    /// <summary>RadMediaPlayer Main</summary>
    [ResourceEntry("RadMediaPlayerMainTitle", Description = "The title of this class.", LastModified = "2016/08/19", Value = "RadMediaPlayer Main")]
    public string RadMediaPlayerMainTitle => this[nameof (RadMediaPlayerMainTitle)];

    /// <summary>RadMediaPlayer Main</summary>
    [ResourceEntry("RadMediaPlayerMainTitlePlural", Description = "The title plural of this class.", LastModified = "2016/08/19", Value = "RadMediaPlayer Main")]
    public string RadMediaPlayerMainTitlePlural => this[nameof (RadMediaPlayerMainTitlePlural)];

    /// <summary>Resource strings for RadMediaPlayer.</summary>
    [ResourceEntry("RadMediaPlayerMainDescription", Description = "The description of this class.", LastModified = "2016/08/19", Value = "Resource strings for RadMediaPlayer.")]
    public string RadMediaPlayerMainDescription => this[nameof (RadMediaPlayerMainDescription)];

    [ResourceEntry("BannerCloseButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Close")]
    public string BannerCloseButtonToolTip => this[nameof (BannerCloseButtonToolTip)];

    [ResourceEntry("FullScreenButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Full Screen")]
    public string FullScreenButtonToolTip => this[nameof (FullScreenButtonToolTip)];

    [ResourceEntry("HDButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "HD")]
    public string HDButtonToolTip => this[nameof (HDButtonToolTip)];

    [ResourceEntry("PauseButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Pause")]
    public string PauseButtonToolTip => this[nameof (PauseButtonToolTip)];

    [ResourceEntry("PlayButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Play")]
    public string PlayButtonToolTip => this[nameof (PlayButtonToolTip)];

    [ResourceEntry("ReservedResource", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Please do not remove this key.")]
    public string ReservedResource => this[nameof (ReservedResource)];

    [ResourceEntry("SubtitlesButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Subtitles")]
    public string SubtitlesButtonToolTip => this[nameof (SubtitlesButtonToolTip)];

    [ResourceEntry("Title", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "")]
    public string Title => this[nameof (Title)];

    [ResourceEntry("TitleBarShareToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Share")]
    public string TitleBarShareToolTip => this[nameof (TitleBarShareToolTip)];

    [ResourceEntry("VolumeButtonToolTip", Description = "RadMediaPlayerMain resource strings.", LastModified = "2016/06/28", Value = "Mute")]
    public string VolumeButtonToolTip => this[nameof (VolumeButtonToolTip)];
  }
}
