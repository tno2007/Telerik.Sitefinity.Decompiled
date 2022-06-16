// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.BlobItemWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle.Model;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class BlobItemWrapper
  {
    private readonly Thumbnail thumbnail;
    private readonly MediaContent mediaContent;

    public BlobItemWrapper(IBlobContent content)
    {
      if (content is Thumbnail)
      {
        this.thumbnail = (Thumbnail) content;
        this.mediaContent = ((Thumbnail) content).Parent;
      }
      else
      {
        this.thumbnail = (Thumbnail) null;
        this.mediaContent = (MediaContent) content;
      }
    }

    public IBlobContent Content => (IBlobContent) this.thumbnail ?? (IBlobContent) this.mediaContent;

    public Guid FileId
    {
      get => this.thumbnail != null ? this.thumbnail.FileId : this.mediaContent.FileId;
      set
      {
        if (this.thumbnail != null)
          this.thumbnail.FileId = value;
        else
          this.mediaContent.FileId = value;
      }
    }

    public string FilePath
    {
      get => this.thumbnail != null ? ((IBlobContentLocation) this.thumbnail).FilePath : this.mediaContent.FilePath;
      set
      {
        if (this.thumbnail != null)
          return;
        Guid fileId = this.FileId;
        foreach (MediaFileLink mediaFileLink in this.mediaContent.MediaFileLinks.Where<MediaFileLink>((Func<MediaFileLink, bool>) (l => l.FileId == fileId)))
          mediaFileLink.FilePath = value;
      }
    }

    public string BlobStorageProvider
    {
      get => this.mediaContent.BlobStorageProvider;
      set
      {
        if (this.thumbnail != null)
          return;
        this.mediaContent.BlobStorageProvider = value;
      }
    }

    public ContentLifecycleStatus Status => this.mediaContent.Status;

    public bool Visible => this.mediaContent.Visible;

    internal bool ShouldGenerateTempPath(LibrariesDataProvider provider, bool uploadAndReplace)
    {
      if (this.mediaContent.Status == ContentLifecycleStatus.Live)
        return false;
      string storageProvider = this.mediaContent.BlobStorageProvider;
      Guid originalContentId = this.mediaContent.OriginalContentId != Guid.Empty ? this.mediaContent.OriginalContentId : this.mediaContent.Id;
      MediaContent mediaContent = provider.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (c => c.OriginalContentId == originalContentId && (int) c.Status == 2 && c.BlobStorageProvider == storageProvider)).SingleOrDefault<MediaContent>();
      if (mediaContent == null)
        return false;
      return uploadAndReplace || mediaContent.IsPublishedInCulture();
    }

    internal void SetFilePath(
      LibrariesDataProvider provider,
      IMediaContentFilePathResolver filePathResolver,
      bool uploadAndReplace)
    {
      if (this.thumbnail != null && !string.IsNullOrEmpty(this.mediaContent.FilePath))
        return;
      this.mediaContent.FilePath = !(this.mediaContent.FileId != Guid.Empty) || !this.ShouldGenerateTempPath(provider, uploadAndReplace) ? filePathResolver.GenerateLivePath(this.mediaContent) : filePathResolver.GenerateTempPath(this.mediaContent);
    }

    internal string GenerateLivePath(IMediaContentFilePathResolver filePathResolver)
    {
      if (this.thumbnail == null)
        return filePathResolver.GenerateLivePath(this.mediaContent);
      string livePath;
      using (new CultureRegion(this.thumbnail.Culture))
        livePath = filePathResolver.GenerateLivePath(this.mediaContent);
      return this.mediaContent.ResolveThumbnailFilePath(this.thumbnail.Name, livePath, this.thumbnail.Culture);
    }

    internal string GenerateTempPath(IMediaContentFilePathResolver filePathResolver) => this.thumbnail != null ? this.mediaContent.ResolveThumbnailFilePath(this.thumbnail.Name) : filePathResolver.GenerateTempPath(this.mediaContent);

    internal bool IsFileUsedByOtherContent(LibrariesDataProvider provider) => this.thumbnail != null ? provider.IsFileUsedByOtherThumbnail(this.thumbnail) : provider.IsFileUsedByOtherMediaContent(this.mediaContent);

    internal IBlobProperties GetBlobProperties() => !(this.Content is MediaContent content) ? this.mediaContent.GetBlobProperties(this.Content) : content.GetBlobProperties();
  }
}
