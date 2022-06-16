// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Scheduling.Web.Services;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// View model class for the <see cref="T:Telerik.Sitefinity.Libraries.Model.Library" /> model.
  /// </summary>
  public class LibraryViewModel : HierarchicalContentViewModelBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryViewModel" /> class.
    /// </summary>
    public LibraryViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="liveItem">The live library related to the master library.</param>
    /// <param name="tempItem">The temp library related to the master library.</param>
    public LibraryViewModel(
      Library contentItem,
      ContentDataProviderBase provider,
      Library live,
      Library temp)
      : base((Content) contentItem, provider, (Content) live, (Content) temp)
    {
      this.Init(contentItem, provider);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public LibraryViewModel(Library contentItem, ContentDataProviderBase provider)
      : base((Content) contentItem, provider)
    {
      this.Init(contentItem, provider);
    }

    private void Init(Library contentItem, ContentDataProviderBase provider)
    {
      this.NeedThumbnailsRegeneration = contentItem.NeedThumbnailsRegeneration;
      this.BlobStorageProvider = this.ResolveBlobStorageProviderName(contentItem.BlobStorageProvider);
      this.LibraryType = contentItem.GetType().FullName;
      this.LibrariesCount = "";
      this.ThumbnailProfiles = (IEnumerable<string>) contentItem.ThumbnailProfiles;
      this.CoverId = contentItem.CoverId;
      if (provider is LibrariesDataProvider librariesDataProvider)
      {
        if (contentItem.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.Album))
        {
          IQueryable<Telerik.Sitefinity.Libraries.Model.Image> source = librariesDataProvider.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (m => m.FolderId == new Guid?() && (int) m.Status == 0 && m.Parent == contentItem));
          this.ItemsCount = source.Count<Telerik.Sitefinity.Libraries.Model.Image>();
          Telerik.Sitefinity.Libraries.Model.Image image1 = (Telerik.Sitefinity.Libraries.Model.Image) null;
          if (this.CoverId.HasValue)
            image1 = librariesDataProvider.GetImages().Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (img => img.Id == this.CoverId.Value && (int) img.Status != 4)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
          if (image1 == null && this.ItemsCount > 0)
            image1 = source.Where<Telerik.Sitefinity.Libraries.Model.Image>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, bool>>) (m => m.Uploaded)).OrderBy<Telerik.Sitefinity.Libraries.Model.Image, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Image, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Image>();
          if (image1 != null)
          {
            try
            {
              this.ThumbnailUrl = MediaContentViewModel.CreateThumbnailUrl((MediaContent) image1);
            }
            catch (BlobStorageException ex)
            {
              this.ThumbnailUrl = string.Empty;
            }
            Telerik.Sitefinity.Libraries.Model.Image image2 = image1;
            if (image2 != null)
            {
              this.Width = image2.Width;
              this.Height = image2.Height;
            }
          }
          string str;
          if (this.ItemsCount <= 0)
            str = string.Empty;
          else
            str = Res.Get<LibrariesResources>().ImagesCountFormat.Arrange((object) this.ItemsCount);
          this.MediaItemsCount = str;
        }
        else if (contentItem.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
        {
          IQueryable<Telerik.Sitefinity.Libraries.Model.Video> source = librariesDataProvider.GetVideos().Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (m => m.FolderId == new Guid?() && (int) m.Status == 0 && m.Parent == contentItem));
          this.ItemsCount = source.Count<Telerik.Sitefinity.Libraries.Model.Video>();
          if (this.ItemsCount > 0)
            source.Where<Telerik.Sitefinity.Libraries.Model.Video>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, bool>>) (m => m.Uploaded)).OrderBy<Telerik.Sitefinity.Libraries.Model.Video, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Video, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Video>();
          string str;
          if (this.ItemsCount <= 0)
            str = string.Empty;
          else
            str = Res.Get<LibrariesResources>().VideosCountFormat.Arrange((object) this.ItemsCount);
          this.MediaItemsCount = str;
        }
        else if (contentItem.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
        {
          IQueryable<Telerik.Sitefinity.Libraries.Model.Document> source = librariesDataProvider.GetDocuments().Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (m => m.FolderId == new Guid?() && (int) m.Status == 0 && m.Parent == contentItem));
          this.ItemsCount = source.Count<Telerik.Sitefinity.Libraries.Model.Document>();
          if (this.ItemsCount > 0)
            source.Where<Telerik.Sitefinity.Libraries.Model.Document>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, bool>>) (m => m.Uploaded)).OrderBy<Telerik.Sitefinity.Libraries.Model.Document, DateTime>((Expression<Func<Telerik.Sitefinity.Libraries.Model.Document, DateTime>>) (b => b.DateCreated)).FirstOrDefault<Telerik.Sitefinity.Libraries.Model.Document>();
          string str;
          if (this.ItemsCount <= 0)
            str = string.Empty;
          else
            str = Res.Get<LibrariesResources>().DocumentsCountFormat.Arrange((object) this.ItemsCount);
          this.MediaItemsCount = str;
        }
      }
      if (contentItem.RunningTask != Guid.Empty)
      {
        SchedulingManager manager = SchedulingManager.GetManager();
        try
        {
          ScheduledTaskData taskData = manager.GetTaskData(contentItem.RunningTask);
          this.TaskInfo = new ProcessingTaskInfo(taskData);
          this.ScheduledTaskInfo = new WcfScheduledTask(taskData);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions);
        }
      }
      if (contentItem.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.VideoLibrary))
      {
        this.IsManageable = contentItem.IsGranted("Video", "ManageVideo");
        this.IsViewable = contentItem.IsGranted("Video", "ViewVideo");
      }
      else if (contentItem.GetType() == typeof (Telerik.Sitefinity.Libraries.Model.DocumentLibrary))
      {
        this.IsManageable = contentItem.IsGranted("Document", "ManageDocument");
        this.IsViewable = contentItem.IsGranted("Document", "ViewDocument");
      }
      else
      {
        this.IsViewable = true;
        this.IsManageable = true;
      }
    }

    /// <summary>Gets or sets the last uploaded date.</summary>
    /// <value>The last uploaded date.</value>
    [DataMember]
    public DateTime? LastUploadedDate { get; set; }

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

    /// <summary>
    /// Gets the BLOB storage provider set for this library,e.g. where do we keep the media files (database/file system etc.)
    /// </summary>
    /// <value>The BLOB storage provider.</value>
    [DataMember]
    public string BlobStorageProvider { get; set; }

    /// <summary>Gets or sets the type of the library.</summary>
    /// <value>The type of the library.</value>
    [DataMember]
    public string LibraryType { get; set; }

    /// <summary>
    /// Keeps information for long running tasks(move to another storage, change url) on a library -gives progress report, failure, available commands.
    /// </summary>
    [DataMember]
    public ProcessingTaskInfo TaskInfo { get; set; }

    /// <summary>
    /// Gets or sets information regarding the current task that the library has.
    /// </summary>
    [DataMember]
    public WcfScheduledTask ScheduledTaskInfo { get; set; }

    /// <summary>
    /// Gets or sets the value indicating if this library is manageable (its properties are editable and included items can be added/removed/modified)
    /// </summary>
    [DataMember]
    public bool IsManageable { get; set; }

    /// <summary>
    /// Gets or sets the value indicating if the current user has permissions to view this library.
    /// </summary>
    [DataMember]
    public bool IsViewable { get; set; }

    /// <summary>
    /// Gets or sets the parent id. Used when representing a folder.
    /// </summary>
    [DataMember]
    public Guid? ParentId { get; set; }

    [DataMember]
    public string LibrariesCount { get; set; }

    [DataMember]
    public string MediaItemsCount { get; set; }

    [DataMember]
    public bool NeedThumbnailsRegeneration { get; set; }

    [DataMember]
    public IEnumerable<string> ThumbnailProfiles { get; set; }

    /// <summary>Gets or sets the id of the cover.</summary>
    [DataMember]
    public Guid? CoverId { get; set; }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Content GetLive() => (Content) this.provider.GetLiveBase<Library>((Library) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Content GetTemp() => (Content) this.provider.GetTempBase<Library>((Library) this.ContentItem);

    /// <summary>Resolves the name of the BLOB storage provider.</summary>
    /// <param name="storedProviderName">Name of the stored provider.</param>
    /// <returns>returns the default provider name if the stored provider name was null</returns>
    private string ResolveBlobStorageProviderName(string storedProviderName) => storedProviderName == null ? Config.Get<LibrariesConfig>().BlobStorage.DefaultProvider : storedProviderName;

    internal static void ValidateMediaContentBeforeCheckout(MediaContent content)
    {
      Guid taskId = content.Parent.RunningTask;
      if (!(taskId != Guid.Empty))
        return;
      ScheduledTaskData scheduledTaskData = SchedulingManager.GetManager().GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == taskId)).SingleOrDefault<ScheduledTaskData>();
      if (scheduledTaskData != null && scheduledTaskData.Status == TaskStatus.Started && scheduledTaskData.TaskName == "LibraryRelocationTask")
      {
        string title = scheduledTaskData.Title;
        throw new ApplicationException(title == "RelocateLibrary" ? Res.Get<LibrariesResources>().UnableToEditItemBecauseOfLibraryUrlChangeTask : (title == "TransferLibrary" ? Res.Get<LibrariesResources>().UnableToEditItemBecauseOfLibraryStorageChangeTask : "You are not allowed to edit this item, because the library it belongs to is in process of relocation. Try again in a while."));
      }
    }

    internal static void SetLibrariesCount(LibraryViewModel viewModel, int librariesCount)
    {
      LibraryViewModel libraryViewModel = viewModel;
      string str;
      if (librariesCount <= 0)
        str = string.Empty;
      else
        str = Res.Get<LibrariesResources>().LibrariesCountFormat.Arrange((object) librariesCount);
      libraryViewModel.LibrariesCount = str;
    }
  }
}
