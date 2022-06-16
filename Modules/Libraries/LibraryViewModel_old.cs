// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryViewModel_old
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Represents library view model.</summary>
  [DataContract]
  public class LibraryViewModel_old : ContentViewModelBase, IDynamicFieldsContainer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibraryViewModel_old" /> class.
    /// </summary>
    public LibraryViewModel_old()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibraryViewModel_old" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public LibraryViewModel_old(
      Library contentItem,
      ContentDataProviderBase provider,
      bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibraryViewModel_old" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public LibraryViewModel_old(Library contentItem, ContentDataProviderBase provider)
      : base((Content) contentItem, provider)
    {
      IQueryable<MediaContent> source = ((Library) this.ContentItem).Items().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => (int) i.Status == 0));
      MediaContent mediaContent = source.OrderBy<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (b => b.DateCreated)).FirstOrDefault<MediaContent>();
      if (mediaContent == null)
        return;
      MediaContentViewModel.CreateThumbnailUrl(mediaContent);
      if (mediaContent is Image image)
      {
        this.Width = image.Width;
        this.Height = image.Height;
      }
      this.LastUploadedDate = new DateTime?(source.OrderByDescending<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (b => b.DateCreated)).FirstOrDefault<MediaContent>().DateCreated);
    }

    /// <summary>Gets or sets the thumbnail URL.</summary>
    /// <value>The thumbnail URL.</value>
    [DataMember]
    public string ThumbnailUrl { get; set; }

    /// <summary>
    /// Gets or sets the width of the first image in the album.
    /// </summary>
    [DataMember]
    public int Width { get; set; }

    /// <summary>
    /// Gets or sets the height of the first image in the album.
    /// </summary>
    [DataMember]
    public int Height { get; set; }

    /// <summary>Gets or sets the last uploaded date.</summary>
    /// <value>The last uploaded date.</value>
    [DataMember]
    public DateTime? LastUploadedDate { get; set; }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => throw new NotSupportedException();

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => throw new NotSupportedException();
  }
}
