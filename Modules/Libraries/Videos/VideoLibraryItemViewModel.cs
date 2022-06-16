// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules.Libraries.Videos
{
  /// <summary>
  /// Represents an item or folder that is in a Video library.
  /// </summary>
  [DataContract]
  public class VideoLibraryItemViewModel : VideoViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel" /> class.
    /// </summary>
    public VideoLibraryItemViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public VideoLibraryItemViewModel(Video contentItem, ContentDataProviderBase provider)
      : base(contentItem, provider)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live image related to the master image.</param>
    /// <param name="tempItem">The temp image related to the master image.</param>
    public VideoLibraryItemViewModel(
      Video contentItem,
      ContentDataProviderBase provider,
      Video live,
      Video temp)
      : base(contentItem, provider, live, temp)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public VideoLibraryItemViewModel(
      Video contentItem,
      ContentDataProviderBase provider,
      bool isEditable)
      : base(contentItem, provider, isEditable)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Videos.VideoLibraryItemViewModel" /> class.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="provider">The provider.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public VideoLibraryItemViewModel(IFolder folder, ContentDataProviderBase provider)
    {
      VideoLibraryItemViewModel libraryItemViewModel = this;
      this.ProviderName = provider.Name;
      this.IsFolder = true;
      this.Id = folder.Id;
      this.Title = (string) folder.Title;
      this.isDeletable = true;
      this.IsManageable = true;
      this.Url = folder.UrlName.ToString();
      if (provider is IFolderOAProvider provider1)
      {
        int num = provider1.GetFolders().Where<Folder>((Expression<Func<Folder, bool>>) (f => f.ParentId == (Guid?) folder.Id)).Count<Folder>();
        string str;
        if (num <= 0)
          str = "";
        else
          str = Res.Get<LibrariesResources>().LibrariesCountFormat.Arrange((object) num);
        this.LibrariesCount = str;
      }
      if (provider is LibrariesDataProvider librariesDataProvider)
      {
        int num = librariesDataProvider.GetVideos().Where<Video>((Expression<Func<Video, bool>>) (m => m.FolderId == (Guid?) folder.Id && (int) m.Status == 0)).Count<Video>();
        string str;
        if (num <= 0)
          str = "";
        else
          str = Res.Get<LibrariesResources>().VideosCountFormat.Arrange((object) num);
        this.VideosCount = str;
      }
      this.LastModified = new DateTime?(DateTime.UtcNow);
      this.DateCreated = DateTime.UtcNow;
      this.DateModified = DateTime.UtcNow;
      this.ExpirationDate = DateTime.UtcNow;
      this.PublicationDate = DateTime.UtcNow;
      Video video = ((LibrariesDataProvider) provider).GetVideos().Where<Video>((Expression<Func<Video, bool>>) (b => b.FolderId == (Guid?) this.Id)).OrderBy<Video, DateTime>((Expression<Func<Video, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Video>();
      if (video == null)
        return;
      this.SnapshotUrl = MediaContentViewModel.CreateThumbnailUrl((MediaContent) video);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is folder.
    /// </summary>
    [DataMember]
    public bool IsFolder { get; set; }

    /// <summary>Gets or sets the libraries count.</summary>
    [DataMember]
    public string LibrariesCount { get; set; }

    /// <summary>Gets or sets the videos count.</summary>
    [DataMember]
    public string VideosCount { get; set; }
  }
}
