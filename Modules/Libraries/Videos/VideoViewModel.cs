// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.VideoViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Libraries.Videos
{
  /// <summary>Represents a video view model</summary>
  [DataContract]
  public class VideoViewModel : MediaContentViewModel
  {
    private int width;
    private int height;
    private string snaphotUrl;
    private int snaphotWidth;
    private int snaphotHeight;
    private string categoryText;
    private string tagsText;
    private bool isManageable;
    private string lilbraryFullUrl;
    private static string defaultVideoThumbailUrl;
    /// <summary>
    /// Gets or sets the default video thumbail when there is no thumbnail.
    /// </summary>
    private const string DefaultVideoThumbail = "~/SFRes/images/Telerik.Sitefinity.Resources/Images.video-thumb.png";

    /// <summary>
    /// Initializes a new instance of the <see cref="!:VideoLibraryViewModel" /> class.
    /// </summary>
    public VideoViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="!:VideoLibraryViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public VideoViewModel(Telerik.Sitefinity.Libraries.Model.Video contentItem, ContentDataProviderBase provider)
      : base((MediaContent) contentItem, provider)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public VideoViewModel(Telerik.Sitefinity.Libraries.Model.Video contentItem, ContentDataProviderBase provider, bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live video related to the master video.</param>
    /// <param name="tempItem">The temp video related to the master video.</param>
    public VideoViewModel(
      Telerik.Sitefinity.Libraries.Model.Video contentItem,
      ContentDataProviderBase provider,
      Telerik.Sitefinity.Libraries.Model.Video live,
      Telerik.Sitefinity.Libraries.Model.Video temp)
      : base((MediaContent) contentItem, provider, (MediaContent) live, (MediaContent) temp)
    {
    }

    /// <summary>Gets or sets the width of the image.</summary>
    [DataMember]
    public int Width
    {
      get => this.ContentItem != null ? ((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem).Width : this.width;
      set
      {
        if (this.ContentItem != null)
          ((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem).Width = value;
        else
          this.width = value;
      }
    }

    /// <summary>Gets or sets the height of the image.</summary>
    [DataMember]
    public int Height
    {
      get => this.ContentItem != null ? ((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem).Height : this.height;
      set
      {
        if (this.ContentItem != null)
          ((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem).Height = value;
        else
          this.height = value;
      }
    }

    /// <summary>Gets or sets the snapshot URL.</summary>
    [DataMember]
    public string SnapshotUrl
    {
      get
      {
        if (this.ContentItem == null)
          return this.snaphotUrl;
        Telerik.Sitefinity.Libraries.Model.Video video = (Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem;
        if (video.GetThumbnails().Any<Thumbnail>())
          return MediaContentViewModel.CreateThumbnailUrl((MediaContent) this.ContentItem);
        Guid fileId = video.FileId;
        MediaFileLink mediaFileLink = video.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileId && video.Thumbnails.Any<Thumbnail>((Func<Thumbnail, bool>) (t => t.Culture == Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(l.Culture).Name)))).FirstOrDefault<MediaFileLink>();
        if (mediaFileLink != null)
        {
          string sharedLinkCulture = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureByLcid(mediaFileLink.Culture).Name;
          Thumbnail thumbnail = video.Thumbnails.Where<Thumbnail>((Func<Thumbnail, bool>) (t => t.Culture == sharedLinkCulture)).FirstOrDefault<Thumbnail>();
          if (thumbnail != null)
          {
            using (new CultureRegion(thumbnail.Culture))
              return MediaContentViewModel.CreateThumbnailUrl((MediaContent) video);
          }
        }
        return VideoViewModel.DefaultVideoThumbailUrl;
      }
      set => this.snaphotUrl = value;
    }

    /// <summary>Gets or sets the width of the snapshot.</summary>
    [DataMember]
    public int SnapshotWidth
    {
      get
      {
        if (this.ContentItem != null)
        {
          Thumbnail thumbnail = ((MediaContent) this.ContentItem).Thumbnail;
          if (thumbnail != null)
            return thumbnail.Width;
        }
        return this.snaphotWidth;
      }
      set => this.snaphotWidth = value;
    }

    /// <summary>Gets or sets the height of the snapshot.</summary>
    [DataMember]
    public int SnapshotHeight
    {
      get
      {
        if (this.ContentItem != null)
        {
          Thumbnail thumbnail = ((MediaContent) this.ContentItem).Thumbnail;
          if (thumbnail != null)
            return thumbnail.Height;
        }
        return this.snaphotHeight;
      }
      set => this.snaphotHeight = value;
    }

    /// <summary>Gets or sets the duration of a video.</summary>
    [DataMember]
    public string Duration
    {
      get => "12:54";
      set
      {
      }
    }

    /// <summary>
    /// Helper Property for presenting the list of the selected Categories as string
    /// </summary>
    [DataMember]
    public string CategoryText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Category", "categoryFilter", typeof (Telerik.Sitefinity.Libraries.Model.Video)) : this.categoryText;
      set => this.categoryText = value;
    }

    /// <summary>
    /// Helper Property for presenting the list of selected Tags as string
    /// </summary>
    [DataMember]
    public string TagsText
    {
      get => this.ContentItem != null ? MediaContentViewModel.GetTaxaText(this.ContentItem, "Tags", "tagFilter", typeof (Telerik.Sitefinity.Libraries.Model.Video)) : this.tagsText;
      set => this.tagsText = value;
    }

    /// <summary>
    /// Gets or sets the value indicating whether this item is  manageable (editable/deletable)
    /// </summary>
    [DataMember]
    public override bool IsManageable
    {
      get
      {
        if (this.ContentItem == null)
          return this.isManageable;
        return ((ISecuredObject) this.ContentItem).IsGranted("Video", "ManageVideo");
      }
      set => this.isManageable = value;
    }

    /// <summary>Gets or sets the library full URL.</summary>
    [DataMember]
    public string LibraryFullUrl
    {
      get => this.ContentItem != null ? this.ResolveLibraryFullUrl((MediaContent) this.ContentItem, LibrariesModule.LibraryVideosPageId) : this.lilbraryFullUrl;
      set => this.lilbraryFullUrl = value;
    }

    /// <summary>Gets the default image URL used as an icon.</summary>
    private static string DefaultVideoThumbailUrl
    {
      get
      {
        if (VideoViewModel.defaultVideoThumbailUrl != null)
          return VideoViewModel.defaultVideoThumbailUrl;
        VideoViewModel.defaultVideoThumbailUrl = RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.video-thumb.png", UrlResolveOptions.Rooted);
        return VideoViewModel.defaultVideoThumbailUrl;
      }
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => (Content) this.provider.GetLiveBase<Telerik.Sitefinity.Libraries.Model.Video>((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => (Content) this.provider.GetTempBase<Telerik.Sitefinity.Libraries.Model.Video>((Telerik.Sitefinity.Libraries.Model.Video) this.ContentItem);
  }
}
