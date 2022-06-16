// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryMoveTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Scheduling;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// Asynchronous task for moving libraries and folders to different parents. Transfers data between storage providers and recompiles urls when needed.
  /// </summary>
  [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "*", Justification = "Public constant with lowercase")]
  [Serializable]
  internal class LibraryMoveTask : ScheduledTask
  {
    private int itemsCount;
    private int currentIndex;
    private List<string> failedItems = new List<string>();
    private JavaScriptSerializer serializer;
    public const string taskName = "LibraryMoveTask";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibraryMoveTask" /> class.
    /// </summary>
    public LibraryMoveTask()
    {
      this.ExecuteTime = DateTime.UtcNow;
      this.Description = Res.Get<LibrariesResources>().MoveLibrary;
      this.serializer = new JavaScriptSerializer();
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => nameof (LibraryMoveTask);

    /// <summary>Gets or sets the id of the item that is to be moved.</summary>
    public Guid ItemId { get; set; }

    /// <summary>
    /// Gets or sets the ids of the items that is to be moved.
    /// </summary>
    public Guid[] ItemIDs { get; set; }

    /// <summary>Gets or sets the target parent id.</summary>
    public Guid ParentId { get; set; }

    /// <summary>Gets or sets the library provider.</summary>
    public string LibraryProvider { get; set; }

    /// <summary>Gets or sets the id of the item that is created.</summary>
    public Guid NewItemId { get; set; }

    /// <summary>
    /// Gets or sets the library title if we are moving a single library only.
    /// </summary>
    public string LibraryTitle { get; set; }

    /// <summary>Gets or sets the type of the library.</summary>
    /// <value>The type of the library.</value>
    public string LibraryType { get; set; }

    /// <summary>
    /// Sets the custom data. This is used when reviving a task from a persistent storage to deserialize any custom stored data
    /// </summary>
    /// <param name="customData">The custom data for the task.</param>
    public override void SetCustomData(string customData)
    {
      LibraryMoveTaskState libraryMoveTaskState = this.serializer.Deserialize<LibraryMoveTaskState>(customData);
      this.ItemId = libraryMoveTaskState.ItemId;
      this.ItemIDs = libraryMoveTaskState.ItemIDs;
      if (this.ItemIDs == null)
        this.ItemIDs = new Guid[1]{ this.ItemId };
      this.ParentId = libraryMoveTaskState.ParentId;
      this.LibraryProvider = libraryMoveTaskState.LibraryProvider;
      this.NewItemId = libraryMoveTaskState.NewItemId;
      this.LibraryTitle = libraryMoveTaskState.LibraryTitle;
      this.LibraryType = libraryMoveTaskState.LibraryType;
    }

    /// <summary>
    /// Builds a unique key for the task based on the parameters.
    /// </summary>
    /// <returns>A unique key for the task based on the parameters.</returns>
    public override string BuildUniqueKey() => this.GetCustomData().GetHashCode().ToString();

    /// <summary>
    /// Gets any custom data that the task needs persisted. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.Modules.Libraries.LibraryMoveTask.SetCustomData(System.String)" />should have implementation for deserialized the data
    /// </summary>
    /// <returns>string serialized custom task data</returns>
    public override string GetCustomData() => this.serializer.Serialize((object) new LibraryMoveTaskState(this));

    /// <summary>Executes the task.</summary>
    public override void ExecuteTask()
    {
      this.itemsCount = 0;
      this.currentIndex = 0;
      switch (this.DetermineOperation())
      {
        case LibraryMoveTask.LibraryMoveOperation.MigrateLibraryToFolder:
          this.MigrateLibraryToFolder(this.ItemId, this.ParentId);
          break;
        case LibraryMoveTask.LibraryMoveOperation.MigrateFolderToLibrary:
          this.MigrateFolderToLibrary(this.ItemId);
          break;
        case LibraryMoveTask.LibraryMoveOperation.MoveFolder:
          Guid[] guidArray;
          if (this.ItemIDs == null)
            guidArray = new Guid[1]{ this.ItemId };
          else
            guidArray = this.ItemIDs;
          this.ItemIDs = guidArray;
          this.MoveFolders(this.ItemIDs, this.ParentId);
          break;
      }
    }

    /// <summary>
    /// Updates the folder structure by parent. All child folders of the oldParent should become children of the new parent.
    /// Makes sure the RootId of all the folders is consistent with the new parent.
    /// </summary>
    /// <param name="manager">The manager.</param>
    /// <param name="oldParent">The old parent.</param>
    /// <param name="newParent">The new parent.</param>
    private static void UpdateFolderStructureByParent(
      LibrariesManager manager,
      IFolder oldParent,
      IFolder newParent)
    {
      Guid guid;
      Guid? nullable;
      if (newParent is Library)
      {
        guid = newParent.Id;
        nullable = new Guid?();
      }
      else
      {
        guid = ((Folder) newParent).RootId;
        nullable = new Guid?(newParent.Id);
      }
      IQueryable<IFolder> childFolders = manager.GetChildFolders(oldParent);
      Expression<Func<IFolder, Guid>> selector = (Expression<Func<IFolder, Guid>>) (f => f.Id);
      foreach (Guid id in childFolders.Select<IFolder, Guid>(selector).ToArray<Guid>())
      {
        Folder folder = (Folder) manager.GetFolder(id);
        if (!nullable.HasValue)
          folder.RootId = guid;
        folder.ParentId = nullable;
      }
    }

    /// <summary>
    /// Gets a new GUID from the provider. This can return a sequential GUID if needed which is different from Guid.NewGuid().
    /// </summary>
    /// <returns>A new GUID.</returns>
    private static Guid GetNewGuid() => LibrariesManager.GetManager().Provider.GetNewGuid();

    /// <summary>
    /// Determines the operation that is to be executed based on ItemId and ParentId.
    /// </summary>
    /// <returns>The library move operation type</returns>
    private LibraryMoveTask.LibraryMoveOperation DetermineOperation()
    {
      if (this.ItemId == Guid.Empty)
        return LibraryMoveTask.LibraryMoveOperation.None;
      switch (LibrariesManager.GetManager(this.LibraryProvider).GetFolder(this.ItemId))
      {
        case Folder _ when this.ParentId == Guid.Empty:
          return LibraryMoveTask.LibraryMoveOperation.MigrateFolderToLibrary;
        case Folder _ when this.ParentId != Guid.Empty:
          return LibraryMoveTask.LibraryMoveOperation.MoveFolder;
        case Library _ when this.ParentId != Guid.Empty:
          return LibraryMoveTask.LibraryMoveOperation.MigrateLibraryToFolder;
        default:
          return LibraryMoveTask.LibraryMoveOperation.None;
      }
    }

    /// <summary>Migrates the library to folder.</summary>
    /// <param name="libraryId">The library id.</param>
    /// <param name="parentId">The parent id.</param>
    private void MigrateLibraryToFolder(Guid libraryId, Guid parentId)
    {
      Guid newFolderId = this.NewItemId == Guid.Empty ? LibraryMoveTask.GetNewGuid() : this.NewItemId;
      string transactionName = newFolderId.ToString();
      LibrariesManager manager = LibrariesManager.GetManager(this.LibraryProvider, transactionName);
      Library library1 = manager.GetLibrary(libraryId);
      IFolder folder = manager.GetFolder(parentId);
      if (!(manager.GetAllFolders(folder).FirstOrDefault<IFolder>((Expression<Func<IFolder, bool>>) (f => f.Id == newFolderId)) is Folder newParent))
      {
        newParent = (Folder) manager.CreateFolder(newFolderId, folder);
        Lstring.CopyValues(library1.Title, newParent.Title);
        Lstring.CopyValues(library1.UrlName, newParent.UrlName);
        Lstring.CopyValues(library1.Description, newParent.Description);
        TransactionManager.CommitTransaction(transactionName);
        this.NewItemId = newFolderId;
        this.PersistState();
      }
      Library library2 = manager.GetLibrary(newParent.RootId);
      library2.RunningTask = this.Id;
      Guid[] array = manager.GetDescendants((IFolder) library1).Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (int) x.Status == 0)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (mc => mc.Id)).ToArray<Guid>();
      this.itemsCount = array.Length;
      for (int index = 0; index < array.Length; ++index)
      {
        this.currentIndex = index;
        Guid guid = array[index];
        try
        {
          this.MoveMediaContent(manager, library2, guid, new Guid?(newParent.Id));
          this.UpdateProgress();
        }
        catch (TaskStoppedException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          this.ProcessMoveMediaContentFailed(manager, guid, ex);
        }
      }
      LibraryMoveTask.UpdateFolderStructureByParent(manager, (IFolder) library1, (IFolder) newParent);
      TransactionManager.CommitTransaction(transactionName);
      if (this.failedItems.Count == 0)
      {
        library1.RunningTask = Guid.Empty;
        manager.DeleteLibrary(library1);
        library2.RunningTask = Guid.Empty;
        TransactionManager.CommitTransaction(transactionName);
      }
      else
        LibraryTasksUtilities.TaskFailed(this.failedItems, this.itemsCount);
    }

    /// <summary>Migrates the folder to library.</summary>
    /// <param name="folderId">The folder id.</param>
    private void MigrateFolderToLibrary(Guid folderId)
    {
      Guid newLibId = this.NewItemId == Guid.Empty ? LibraryMoveTask.GetNewGuid() : this.NewItemId;
      LibrariesManager manager1 = LibrariesManager.GetManager(this.LibraryProvider, newLibId.ToString());
      Folder folder = (Folder) manager1.GetFolder(folderId);
      Library library1 = manager1.GetLibrary(folder.RootId);
      Library library2 = manager1.GetLibraries().FirstOrDefault<Library>((Expression<Func<Library, bool>>) (l => l.Id == newLibId));
      if (library2 == null)
      {
        if (typeof (Album).IsInstanceOfType((object) library1))
          library2 = (Library) manager1.CreateAlbum();
        else if (typeof (DocumentLibrary).IsInstanceOfType((object) library1))
          library2 = (Library) manager1.CreateDocumentLibrary();
        else if (typeof (VideoLibrary).IsInstanceOfType((object) library1))
          library2 = (Library) manager1.CreateVideoLibrary();
        manager1.Copy((Content) library1, (Content) library2);
        Lstring.CopyValues(folder.UrlName, library2.UrlName);
        Lstring.CopyValues(folder.Title, library2.Title);
        Lstring.CopyValues(folder.Description, library2.Description);
        manager1.RecompileItemUrls<Library>(library2);
        library2.RunningTask = this.Id;
        library2.ThumbnailProfiles = library1.ThumbnailProfiles;
        manager1.Provider.CommitTransaction();
        this.NewItemId = newLibId;
        this.PersistState();
      }
      Guid[] array = manager1.GetDescendants((IFolder) folder).Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (int) x.Status == 0)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (mc => mc.Id)).ToArray<Guid>();
      this.itemsCount = array.Length;
      for (int index = 0; index < array.Length; ++index)
      {
        this.currentIndex = index;
        Guid id1 = array[index];
        Guid? folderId1 = manager1.GetMediaItem(id1).FolderId;
        bool flag = false;
        Guid? nullable = folderId1;
        Guid id2 = folder.Id;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == id2 ? 1 : 0) : 1) : 0) != 0)
          flag = true;
        try
        {
          LibrariesManager manager2 = manager1;
          Library library3 = library2;
          Guid mediaContentId = id1;
          nullable = new Guid?();
          Guid? folderId2 = nullable;
          int num = flag ? 1 : 0;
          this.MoveMediaContent(manager2, library3, mediaContentId, folderId2, num != 0);
          this.UpdateProgress();
        }
        catch (TaskStoppedException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          this.ProcessMoveMediaContentFailed(manager1, id1, ex);
        }
      }
      LibraryMoveTask.UpdateFolderStructureByParent(manager1, (IFolder) folder, (IFolder) library2);
      manager1.Provider.CommitTransaction();
      manager1.Delete((IFolder) folder);
      if (this.failedItems.Count == 0)
      {
        library2.RunningTask = Guid.Empty;
        library1.RunningTask = Guid.Empty;
        manager1.Provider.CommitTransaction();
      }
      else
        LibraryTasksUtilities.TaskFailed(this.failedItems, this.itemsCount);
    }

    /// <summary>Moves a folder under another parent.</summary>
    /// <param name="folderIDs">The folders that we are moving</param>
    /// <param name="parentId">The parent id.</param>
    private void MoveFolders(Guid[] folderIDs, Guid parentId)
    {
      LibrariesManager manager = LibrariesManager.GetManager(this.LibraryProvider);
      for (int index1 = 0; index1 < folderIDs.Length; ++index1)
      {
        Guid folderId = folderIDs[index1];
        int chunk = folderIDs.Length - index1;
        Folder folder1 = (Folder) manager.GetFolder(folderId);
        IFolder folder2 = manager.GetFolder(parentId);
        Library library1 = manager.GetLibrary(folder1.RootId);
        Guid id;
        Guid? nullable;
        if (folder2 is Folder)
        {
          id = ((Folder) folder2).RootId;
          nullable = new Guid?(folder2.Id);
        }
        else
        {
          id = folder2.Id;
          nullable = new Guid?();
        }
        bool flag = id != folder1.RootId;
        Library library2 = manager.GetLibrary(id);
        library2.RunningTask = this.Id;
        Guid[] array = manager.GetDescendants((IFolder) folder1).Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => (int) x.Status == 0)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (m => m.Id)).ToArray<Guid>();
        this.itemsCount = array.Length;
        for (int index2 = 0; index2 < array.Length; ++index2)
        {
          this.currentIndex = index2;
          Guid guid = array[index2];
          try
          {
            this.MoveMediaContent(manager, library2, guid, new Guid?(folder1.Id));
            manager.Provider.CommitTransaction();
            this.UpdateProgress(chunk);
          }
          catch (TaskStoppedException ex)
          {
            throw ex;
          }
          catch (Exception ex)
          {
            this.ProcessMoveMediaContentFailed(manager, guid, ex);
          }
        }
        folder1.RootId = id;
        folder1.ParentId = nullable;
        if (flag)
          LibraryMoveTask.UpdateFolderStructureByParent(manager, (IFolder) folder1, (IFolder) folder1);
        manager.RecompileItemUrls<Library>(library2);
        manager.Provider.CommitTransaction();
        if (this.failedItems.Count == 0)
        {
          library1.RunningTask = Guid.Empty;
          library2.RunningTask = Guid.Empty;
        }
        else
          LibraryTasksUtilities.TaskFailed(this.failedItems, this.itemsCount);
      }
    }

    private void MoveMediaContent(
      LibrariesManager manager,
      Library library,
      Guid mediaContentId,
      Guid? folderId,
      bool shouldChangeParentFolder = false)
    {
      MediaContent mediaContent;
      switch (library)
      {
        case Album _:
          mediaContent = (MediaContent) manager.GetImage(mediaContentId);
          break;
        case VideoLibrary _:
          mediaContent = (MediaContent) manager.GetVideo(mediaContentId);
          break;
        case DocumentLibrary _:
          mediaContent = (MediaContent) manager.GetDocument(mediaContentId);
          break;
        default:
          throw new ArgumentException("Library is not of any recognized type. Album, VideoLibrary and DocumentLibrary is allowed.", nameof (library));
      }
      bool flag = library.BlobStorageProvider == mediaContent.BlobStorageProvider;
      Guid? folderId1 = mediaContent.FolderId;
      int num;
      if (folderId1.HasValue)
      {
        Guid? nullable = folderId1;
        Guid empty = Guid.Empty;
        num = nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() == empty ? 1 : 0) : 1) : 0;
      }
      else
        num = 1;
      mediaContent.AutoGenerateUniqueUrl = true;
      if (num != 0)
        manager.ChangeItemParent(mediaContent, (Content) library, folderId, true);
      else
        manager.ChangeItemParent((Content) mediaContent, (Content) library, true);
      if (folderId1.HasValue)
      {
        Guid? nullable = folderId1;
        Guid empty = Guid.Empty;
        if ((nullable.HasValue ? (nullable.HasValue ? (nullable.GetValueOrDefault() != empty ? 1 : 0) : 0) : 1) != 0 && !shouldChangeParentFolder)
        {
          mediaContent.FolderId = folderId1;
          this.ChangeLyfecycleItemFolders(manager, folderId1, mediaContent);
          goto label_16;
        }
      }
      if (!mediaContent.FolderId.HasValue | shouldChangeParentFolder)
      {
        mediaContent.FolderId = folderId;
        this.ChangeLyfecycleItemFolders(manager, folderId, mediaContent);
      }
label_16:
      manager.Provider.CommitTransaction();
      BlobStorageManager blobStorageManager = manager.Provider.GetBlobStorageManager(mediaContent);
      IExternalBlobStorageProvider provider = blobStorageManager.Provider as IExternalBlobStorageProvider;
      if (!(provider != null & flag))
        return;
      LibraryTasksUtilities.UpdateExternalLibraryItemLocation(manager, provider, blobStorageManager.Provider, mediaContent);
    }

    private void ChangeLyfecycleItemFolders(
      LibrariesManager manager,
      Guid? folderId,
      MediaContent item)
    {
      ILifecycleDecorator lifecycle = manager.Lifecycle;
      ILifecycleDataItem live = lifecycle.GetLive((ILifecycleDataItem) item);
      if (live != null)
        (live as MediaContent).FolderId = folderId;
      ILifecycleDataItem temp = lifecycle.GetTemp((ILifecycleDataItem) item);
      if (temp == null)
        return;
      (temp as MediaContent).FolderId = folderId;
    }

    private void UpdateProgress(int chunk = 1)
    {
      ++this.currentIndex;
      TaskProgressEventArgs eventArgs = new TaskProgressEventArgs()
      {
        Progress = this.currentIndex * 100 / this.itemsCount / chunk,
        StatusMessage = string.Empty
      };
      this.OnProgressChanged(eventArgs);
      if (eventArgs.Stopped)
        throw new TaskStoppedException();
    }

    private void ProcessMoveMediaContentFailed(LibrariesManager manager, Guid id, Exception e)
    {
      MediaContent mediaItem = manager.GetMediaItem(id);
      string mediaContentFullPath = LibraryTasksUtilities.GetMediaContentFullPath(manager, mediaItem);
      Log.Error("Failed to move item: {0}.{1}Error: {2}", (object) mediaContentFullPath, (object) Environment.NewLine, (object) e);
      this.failedItems.Add(mediaContentFullPath);
    }

    /// <summary>
    /// Used in LibraryMoveTask. Enumerates possible cases of the task.
    /// </summary>
    private enum LibraryMoveOperation
    {
      /// <summary>No action is to be performed.</summary>
      None,
      /// <summary>
      /// Migrate library to folder. Create a new folder and deletes the library.
      /// </summary>
      MigrateLibraryToFolder,
      /// <summary>
      /// Migrate folder to library. Create a new library and delete the folder.
      /// </summary>
      MigrateFolderToLibrary,
      /// <summary>Move folder under a different parent.</summary>
      MoveFolder,
    }
  }
}
