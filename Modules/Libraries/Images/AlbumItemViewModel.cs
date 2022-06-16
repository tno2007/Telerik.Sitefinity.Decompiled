// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel
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

namespace Telerik.Sitefinity.Modules.Libraries.Images
{
  /// <summary>Represents an item or folder that is in an Album.</summary>
  public class AlbumItemViewModel : ImageViewModel
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel" /> class.
    /// </summary>
    public AlbumItemViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public AlbumItemViewModel(Image contentItem, ContentDataProviderBase provider)
      : base(contentItem, provider)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live image related to the master image.</param>
    /// <param name="tempItem">The temp image related to the master image.</param>
    public AlbumItemViewModel(
      Image contentItem,
      ContentDataProviderBase provider,
      Image live,
      Image temp)
      : base(contentItem, provider, live, temp)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public AlbumItemViewModel(Image contentItem, ContentDataProviderBase provider, bool isEditable)
      : base(contentItem, provider, isEditable)
    {
      this.IsFolder = false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Images.AlbumItemViewModel" /> class.
    /// </summary>
    /// <param name="folder">The folder.</param>
    /// <param name="provider">The provider.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public AlbumItemViewModel(IFolder folder, ContentDataProviderBase provider)
    {
      AlbumItemViewModel albumItemViewModel = this;
      this.ProviderName = provider.Name;
      this.IsFolder = true;
      this.Id = folder.Id;
      this.Title = (string) folder.Title;
      this.isDeletable = true;
      this.IsManageable = true;
      this.Url = folder.UrlName.ToString();
      this.CoverId = folder.CoverId;
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
      if (provider is LibrariesDataProvider librariesDataProvider1)
      {
        int num = librariesDataProvider1.GetImages().Where<Image>((Expression<Func<Image, bool>>) (m => m.FolderId == (Guid?) folder.Id && (int) m.Status == 0)).Count<Image>();
        string str;
        if (num <= 0)
          str = "";
        else
          str = Res.Get<LibrariesResources>().ImagesCountFormat.Arrange((object) num);
        this.ImagesCount = str;
      }
      this.LastModified = new DateTime?(DateTime.UtcNow);
      this.DateCreated = DateTime.UtcNow;
      this.DateModified = DateTime.UtcNow;
      this.ExpirationDate = DateTime.UtcNow;
      this.PublicationDate = DateTime.UtcNow;
      Image image = (Image) null;
      Guid? coverId = this.CoverId;
      if (coverId.HasValue)
      {
        LibrariesDataProvider librariesDataProvider2 = librariesDataProvider1;
        coverId = this.CoverId;
        Guid id = coverId.Value;
        image = librariesDataProvider2.GetImage(id);
      }
      if (image == null)
        image = ((LibrariesDataProvider) provider).GetImages().Where<Image>((Expression<Func<Image, bool>>) (b => b.FolderId == (Guid?) this.Id)).OrderBy<Image, DateTime>((Expression<Func<Image, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Image>();
      if (image == null)
        return;
      this.ThumbnailUrl = MediaContentViewModel.CreateThumbnailUrl((MediaContent) image);
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is folder.
    /// </summary>
    [DataMember]
    public bool IsFolder { get; set; }

    /// <summary>Gets or sets the libraries count.</summary>
    [DataMember]
    public string LibrariesCount { get; set; }

    /// <summary>Gets or sets the images count.</summary>
    [DataMember]
    public string ImagesCount { get; set; }

    /// <summary>Gets or sets the id of the cover.</summary>
    [DataMember]
    public Guid? CoverId { get; set; }
  }
}
