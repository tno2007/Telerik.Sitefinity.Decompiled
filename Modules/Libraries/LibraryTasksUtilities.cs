// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryTasksUtilities
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BlobStorage;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Modules.Libraries.Configuration;
using Telerik.Sitefinity.Modules.Libraries.Versioning;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Modules.Libraries
{
  internal class LibraryTasksUtilities
  {
    internal static void UpdateExternalLibraryItemLocation(
      LibrariesManager manager,
      IExternalBlobStorageProvider externalBlobStorage,
      BlobStorageProvider blobStorage,
      MediaContent mediaContent)
    {
      if (mediaContent.BlobStorageProvider != blobStorage.Name)
        return;
      List<MediaFileLink> transferredMediaFileLinks = new List<MediaFileLink>();
      ILifecycleDecorator lifecycle = manager.Lifecycle;
      MediaContent mediaContent1 = mediaContent.Status == ContentLifecycleStatus.Live ? mediaContent : lifecycle.GetLive((ILifecycleDataItem) mediaContent) as MediaContent;
      if (mediaContent1 != null)
        LibraryTasksUtilities.MoveItem(manager, mediaContent1, externalBlobStorage, blobStorage, transferredMediaFileLinks);
      MediaContent mediaContent2 = mediaContent.Status == ContentLifecycleStatus.Master ? mediaContent : lifecycle.GetMaster((ILifecycleDataItem) mediaContent) as MediaContent;
      LibraryTasksUtilities.MoveItem(manager, mediaContent2, externalBlobStorage, blobStorage, transferredMediaFileLinks);
      if (lifecycle.GetTemp((ILifecycleDataItem) mediaContent2) is MediaContent temp && temp.FileId == mediaContent2.FileId)
        temp.FilePath = mediaContent2.FilePath;
      if (mediaContent1 != null)
        manager.Copy((Content) mediaContent1, (Content) mediaContent2);
      manager.Provider.CommitTransaction();
    }

    internal static void UpdateLibraryItemStorage(
      LibrariesManager librariresManager,
      BlobStorageProvider destinationStorageProvider,
      MediaContent item)
    {
      if (item.BlobStorageProvider == destinationStorageProvider.Name)
        return;
      ILifecycleDecorator lifecycle = librariresManager.Lifecycle;
      if (lifecycle.GetTemp((ILifecycleDataItem) item) is MediaContent temp)
        librariresManager.DeleteItem((object) temp);
      MediaContent live = lifecycle.GetLive((ILifecycleDataItem) item) as MediaContent;
      BlobStorageManager blobStorageManager = librariresManager.Provider.GetBlobStorageManager(item);
      List<IBlobContentLocation> blobsToDelete1 = new List<IBlobContentLocation>();
      Dictionary<Guid, MediaFileLink> dictionary = new Dictionary<Guid, MediaFileLink>();
      try
      {
        foreach (int cultureId in item.MediaFileLinks.Select<MediaFileLink, int>((Func<MediaFileLink, int>) (l => l.Culture)))
        {
          using (new CultureRegion(cultureId))
          {
            bool flag = false;
            if (live != null)
            {
              MediaFileLink fileLink = live.GetFileLink(cultureId: new int?(cultureId), fallBack: false);
              if (fileLink != null)
              {
                Guid fileId = fileLink.FileId;
                flag = item.FileId == fileId;
                if (!dictionary.ContainsKey(fileId))
                {
                  List<IBlobContentLocation> blobsToDelete2;
                  LibraryTasksUtilities.TransferBlobItemToStorage(librariresManager, live, destinationStorageProvider, out blobsToDelete2);
                  if (blobsToDelete2 != null)
                    blobsToDelete1.AddRange((IEnumerable<IBlobContentLocation>) blobsToDelete2);
                  dictionary.Add(fileId, live.GetFileLink());
                }
                else
                {
                  MediaFileLink mediaFileLink = dictionary[fileId];
                  live.NumberOfChunks = mediaFileLink.NumberOfChunks;
                  live.FilePath = mediaFileLink.FilePath;
                  live.TotalSize = mediaFileLink.TotalSize;
                  live.ChunkSize = mediaFileLink.ChunkSize;
                }
              }
            }
            if (flag)
            {
              item.NumberOfChunks = live.NumberOfChunks;
              item.FilePath = live.FilePath;
              item.TotalSize = live.TotalSize;
              item.ChunkSize = live.ChunkSize;
            }
            else if (!dictionary.ContainsKey(item.FileId))
            {
              List<IBlobContentLocation> blobsToDelete3;
              LibraryTasksUtilities.TransferBlobItemToStorage(librariresManager, item, destinationStorageProvider, out blobsToDelete3);
              if (blobsToDelete3 != null)
                blobsToDelete1.AddRange((IEnumerable<IBlobContentLocation>) blobsToDelete3);
              dictionary.Add(item.FileId, item.GetFileLink());
            }
          }
        }
      }
      catch (Exception ex)
      {
        foreach (Guid key in dictionary.Keys)
        {
          Guid fileId = key;
          MediaFileLink mediaFileLink = item.MediaFileLinks.FirstOrDefault<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.FileId == fileId));
          if (mediaFileLink != null)
          {
            using (new CultureRegion(mediaFileLink.Culture))
              destinationStorageProvider.Delete((IBlobContentLocation) BlobContentProxy.CreateFrom((IBlobContent) item));
          }
        }
        throw ex;
      }
      try
      {
        List<Guid> transferedThumbnails = new List<Guid>();
        List<IBlobContentLocation> blobsToDelete4;
        LibraryTasksUtilities.TransferThumbnails(librariresManager, item, destinationStorageProvider, transferedThumbnails, out blobsToDelete4);
        if (blobsToDelete4 != null)
          blobsToDelete1.AddRange((IEnumerable<IBlobContentLocation>) blobsToDelete4);
        if (live != null)
        {
          LibraryTasksUtilities.TransferThumbnails(librariresManager, live, destinationStorageProvider, transferedThumbnails, out blobsToDelete4);
          if (blobsToDelete4 != null)
            blobsToDelete1.AddRange((IEnumerable<IBlobContentLocation>) blobsToDelete4);
          foreach (Thumbnail thumbnail1 in (IEnumerable<Thumbnail>) live.Thumbnails)
          {
            Thumbnail thumbnail = thumbnail1;
            Thumbnail source = item.Thumbnails.FirstOrDefault<Thumbnail>((Func<Thumbnail, bool>) (x => x.FileId == thumbnail.FileId));
            if (source != null)
              librariresManager.CopyBlobContent((IChunksBlobContent) source, (IChunksBlobContent) thumbnail);
          }
        }
      }
      catch (Exception ex)
      {
        librariresManager.RegenerateThumbnails(item);
        Log.Trace("Thumbnail(s) for: {0} were regenerated successfully.", (object) LibraryTasksUtilities.GetMediaContentFullPath(librariresManager, item));
      }
      LibraryTasksUtilities.TransferRevisionBlobs(item, librariresManager.Provider, destinationStorageProvider, blobStorageManager, blobsToDelete1, (IEnumerable<Guid>) dictionary.Keys);
      foreach (IBlobContentLocation blobContentLocation in blobsToDelete1)
        librariresManager.Provider.DeleteBlob(blobContentLocation, blobStorageManager);
      item.BlobStorageProvider = destinationStorageProvider.Name;
      if (live != null)
        live.BlobStorageProvider = destinationStorageProvider.Name;
      if (string.IsNullOrEmpty(librariresManager.TransactionName))
        librariresManager.SaveChanges();
      else
        TransactionManager.CommitTransaction(librariresManager.TransactionName);
    }

    internal static void TransferRevisionBlobs(
      MediaContent item,
      LibrariesDataProvider librariesDataProvider,
      BlobStorageProvider destinationStorageProvider,
      BlobStorageManager fromStorage,
      List<IBlobContentLocation> blobsToDelete,
      IEnumerable<Guid> transferedFiles)
    {
      List<Dependency> list = librariesDataProvider.GetRelatedManager<VersionManager>((string) null).GetChanges().Where<Change>((Expression<Func<Change, bool>>) (x => x.Parent.Id == item.Id)).SelectMany<Change, Dependency>((Expression<Func<Change, IEnumerable<Dependency>>>) (r => r.Dependencies)).Distinct<Dependency>().ToList<Dependency>();
      HashSet<Guid> guidSet = new HashSet<Guid>(transferedFiles);
      foreach (Dependency dependency in list)
      {
        Guid fileId = new Guid(dependency.Key);
        IDictionary<string, object> data = BlobContentCleanerTask.GetData(dependency.Data);
        if (!guidSet.Contains(fileId))
        {
          BlobContentProxy fromDependencyData = LibraryTasksUtilities.CreateBlobProxyFromDependencyData(fileId, data);
          if (fromStorage.Provider.Name != destinationStorageProvider.Name)
            LibraryTasksUtilities.TransferRevisionBlob(librariesDataProvider, destinationStorageProvider, fromStorage, blobsToDelete, fromDependencyData);
          guidSet.Add(fromDependencyData.FileId);
        }
        data["blobStorageProvider"] = (object) destinationStorageProvider.Name;
        LibraryTasksUtilities.SetNumberOfChunks(destinationStorageProvider, data);
        dependency.Data = BlobContentCleanerTask.ConvertData(data);
      }
    }

    private static void SetNumberOfChunks(
      BlobStorageProvider destinationStorageProvider,
      IDictionary<string, object> dependencyData)
    {
      if (!(destinationStorageProvider.Name == "Database"))
        return;
      int.Parse(dependencyData["numberOfChunks"].ToString());
      int num = (int) Math.Ceiling(double.Parse(dependencyData["totalSize"].ToString()) / (double) Config.Get<LibrariesConfig>().SizeOfChunk);
      dependencyData["numberOfChunks"] = (object) num;
    }

    private static BlobContentProxy CreateBlobProxyFromDependencyData(
      Guid fileId,
      IDictionary<string, object> data)
    {
      int sizeOfChunk = Config.Get<LibrariesConfig>().SizeOfChunk;
      int num1 = int.Parse(data["numberOfChunks"].ToString());
      long num2 = long.Parse(data["totalSize"].ToString());
      return new BlobContentProxy()
      {
        FileId = fileId,
        FilePath = data["filePath"].ToString(),
        MimeType = data["mimeType"].ToString(),
        Extension = data["extension"].ToString(),
        NumberOfChunks = num1,
        TotalSize = num2,
        ChunkSize = sizeOfChunk
      };
    }

    private static void TransferRevisionBlob(
      LibrariesDataProvider librariresProvider,
      BlobStorageProvider destinationStorageBlobProvider,
      BlobStorageManager sourceStorageManager,
      List<IBlobContentLocation> blobsToDelete,
      BlobContentProxy blobFromRevisionHistory)
    {
      bool flag = sourceStorageManager.Provider.HasSameLocation(destinationStorageBlobProvider);
      if (flag)
        return;
      string filePath = blobFromRevisionHistory.FilePath;
      Guid fileId;
      if (sourceStorageManager.Provider is IExternalBlobStorageProvider)
      {
        BlobContentProxy blobContentProxy = blobFromRevisionHistory;
        fileId = blobFromRevisionHistory.FileId;
        string str = fileId.ToString();
        blobContentProxy.FilePath = str;
      }
      if (!flag)
      {
        BlobContentProxy blobContentProxy = new BlobContentProxy((IBlobContent) blobFromRevisionHistory);
        blobsToDelete.Add((IBlobContentLocation) blobContentProxy);
      }
      if (destinationStorageBlobProvider.BlobExists((IBlobContentLocation) blobFromRevisionHistory))
        destinationStorageBlobProvider.Delete((IBlobContentLocation) blobFromRevisionHistory);
      using (Stream downloadStream = sourceStorageManager.GetDownloadStream((IBlobContent) blobFromRevisionHistory))
      {
        if (destinationStorageBlobProvider is IExternalBlobStorageProvider)
        {
          BlobContentProxy blobContentProxy = blobFromRevisionHistory;
          fileId = blobFromRevisionHistory.FileId;
          string str = fileId.ToString();
          blobContentProxy.FilePath = str;
        }
        else
          blobFromRevisionHistory.FilePath = filePath;
        librariresProvider.UploadBlobToStorage((IBlobContent) blobFromRevisionHistory, downloadStream, destinationStorageBlobProvider, sourceStorageManager.Provider.GetStreamingBufferSize());
      }
    }

    internal static string GetMediaContentFullPath(LibrariesManager manager, MediaContent item)
    {
      try
      {
        List<string> source = new List<string>();
        for (IFolder folder = manager.GetFolder(item.FolderId.HasValue ? item.FolderId.Value : item.ParentId); folder.ParentId != Guid.Empty; folder = manager.GetFolder(folder.ParentId))
          source.Insert(0, (string) folder.Title);
        source.Insert(0, (string) item.Parent.Title);
        return source.Aggregate<string>((Func<string, string, string>) ((a, b) => a + "\\" + b)) + "\\" + (string) item.Title;
      }
      catch
      {
        return (string) item.Title;
      }
    }

    internal static void TaskFailed(List<string> failedItems, int itemsCount)
    {
      Log.Write((object) LibraryTasksUtilities.GenerateExceptionMessageForFailedTask(failedItems, itemsCount, false), ConfigurationPolicy.ErrorLog);
      throw new Exception(LibraryTasksUtilities.GenerateExceptionMessageForFailedTask(failedItems, itemsCount, true));
    }

    /// <summary>
    /// Generates a full or partial exception message for failed media content scheduled tasks
    /// </summary>
    /// <param name="failedItems">The failed items' paths</param>
    /// <param name="itemsCount">How many items have failed</param>
    /// <param name="shouldGeneratePartialException">If true, the method will return a message which has each item limited to no more than 40 symbols in length and shows maximum of 5 items</param>
    /// <returns>An error message for failed media content scheduled tasks</returns>
    private static string GenerateExceptionMessageForFailedTask(
      List<string> failedItems,
      int itemsCount,
      bool shouldGeneratePartialException)
    {
      int num = 5;
      string messageForFailedTask = string.Format("Failed to move {0} of {1} items:", (object) failedItems.Count, (object) itemsCount) + Environment.NewLine + Environment.NewLine;
      foreach (string str1 in failedItems.Take<string>(shouldGeneratePartialException ? num : itemsCount))
      {
        string str2 = str1.Length > 40 & shouldGeneratePartialException ? str1.Substring(0, 40) + "..." : str1;
        messageForFailedTask = messageForFailedTask + " • " + str2 + Environment.NewLine;
      }
      if (failedItems.Count > num)
        messageForFailedTask = messageForFailedTask + string.Format("And {0} more...", (object) (failedItems.Count - num)) + Environment.NewLine;
      if (shouldGeneratePartialException)
        messageForFailedTask = messageForFailedTask + Environment.NewLine + "Check the error log for more details or contact your administrator for assistance.";
      return messageForFailedTask;
    }

    private static void TransferThumbnails(
      LibrariesManager manager,
      MediaContent item,
      BlobStorageProvider blobStorage,
      List<Guid> transferedThumbnails,
      out List<IBlobContentLocation> blobsToDelete)
    {
      blobsToDelete = new List<IBlobContentLocation>();
      BlobContentProxy[] array = item.Thumbnails.Select<Thumbnail, BlobContentProxy>((Func<Thumbnail, BlobContentProxy>) (t => BlobContentProxy.CreateFrom((IBlobContent) t))).ToArray<BlobContentProxy>();
      BlobStorageManager blobStorageManager = manager.Provider.GetBlobStorageManager(item);
      foreach (Thumbnail thumbnail in (IEnumerable<Thumbnail>) item.Thumbnails)
      {
        Thumbnail thmbItem = thumbnail;
        if (!transferedThumbnails.Contains(thmbItem.FileId))
        {
          transferedThumbnails.Add(thmbItem.FileId);
          Stream source;
          if (thmbItem.FileId != Guid.Empty)
          {
            BlobContentProxy content = ((IEnumerable<BlobContentProxy>) array).Single<BlobContentProxy>((Func<BlobContentProxy, bool>) (t => t.Id == thmbItem.Id));
            blobsToDelete.Add((IBlobContentLocation) content);
            source = blobStorageManager.GetDownloadStream((IBlobContent) content);
          }
          else
          {
            source = (Stream) new MemoryStream(thmbItem.Data);
            thmbItem.FileId = Guid.NewGuid();
            thmbItem.Data = (byte[]) null;
          }
          if (blobStorage.BlobExists((IBlobContentLocation) thmbItem))
            blobStorage.Delete((IBlobContentLocation) thmbItem);
          using (source)
            manager.Provider.UploadToStorage(new BlobItemWrapper((IBlobContent) thmbItem), source, blobStorage, blobStorageManager.Provider.GetStreamingBufferSize());
        }
      }
    }

    private static bool TransferBlobItemToStorage(
      LibrariesManager manager,
      MediaContent item,
      BlobStorageProvider blobStorage,
      out List<IBlobContentLocation> blobsToDelete)
    {
      blobsToDelete = (List<IBlobContentLocation>) null;
      if (!(item.BlobStorageProvider != blobStorage.Name))
        return false;
      BlobStorageManager blobStorageManager = manager.Provider.GetBlobStorageManager(item);
      BlobContentProxy from = BlobContentProxy.CreateFrom((IBlobContent) item);
      int num = blobStorageManager.Provider.HasSameLocation(blobStorage) ? 1 : 0;
      if (num == 0)
      {
        blobsToDelete = new List<IBlobContentLocation>();
        blobsToDelete.Add((IBlobContentLocation) from);
      }
      if (num != 0)
        return false;
      if (blobStorage.BlobExists((IBlobContentLocation) item))
        blobStorage.Delete((IBlobContentLocation) item);
      using (Stream downloadStream = blobStorageManager.GetDownloadStream((IBlobContent) from))
      {
        string blobStorageProvider = item.BlobStorageProvider;
        manager.Provider.UploadToStorage(new BlobItemWrapper((IBlobContent) item), downloadStream, blobStorage, blobStorageManager.Provider.GetStreamingBufferSize());
        item.BlobStorageProvider = blobStorageProvider;
      }
      return true;
    }

    private static void MoveItem(
      LibrariesManager manager,
      MediaContent mediaContent,
      IExternalBlobStorageProvider externalBlobStorage,
      BlobStorageProvider blobStorage,
      List<MediaFileLink> transferredMediaFileLinks)
    {
      foreach (MediaFileLink mediaFileLink1 in (IEnumerable<MediaFileLink>) mediaContent.MediaFileLinks)
      {
        using (new CultureRegion(mediaFileLink1.Culture))
        {
          MediaContent mediaContent1 = mediaContent.Status == ContentLifecycleStatus.Live ? mediaContent : manager.Lifecycle.GetLive((ILifecycleDataItem) mediaContent) as MediaContent;
          string empty = string.Empty;
          string str;
          if (mediaContent1 != null && mediaContent.FileId != mediaContent1.FileId)
            str = (mediaFileLink1.DefaultUrl != null ? mediaFileLink1.DefaultUrl : mediaContent.ItemDefaultUrl.ToLowerInvariant().ToString()).TrimStart('/') + "_" + (object) mediaContent1.FileId + mediaFileLink1.Extension;
          else
            str = manager.Provider.GenerateLivePath(mediaContent);
          if (mediaContent1 != null && str.Equals(mediaContent1.FilePath))
            break;
          if (!transferredMediaFileLinks.Any<MediaFileLink>((Func<MediaFileLink, bool>) (x => x.FileId == mediaContent.FileId)))
          {
            IBlobContentLocation source1 = blobStorage.ResolveBlobContentLocation((IBlobContent) mediaContent);
            IBlobContentLocation[] array = mediaContent.GetThumbnails().Select<Thumbnail, IBlobContentLocation>((Func<Thumbnail, IBlobContentLocation>) (t => blobStorage.ResolveBlobContentLocation((IBlobContent) t))).ToArray<IBlobContentLocation>();
            mediaContent.FilePath = str;
            externalBlobStorage.Move(source1, (IBlobContentLocation) mediaContent);
            foreach (Thumbnail thumbnail in mediaContent.GetThumbnails())
            {
              Thumbnail thmbItem = thumbnail;
              if (thmbItem.FileId != Guid.Empty)
              {
                IBlobContentLocation source2 = ((IEnumerable<IBlobContentLocation>) array).Single<IBlobContentLocation>((Func<IBlobContentLocation, bool>) (t => t.FileId == thmbItem.FileId));
                externalBlobStorage.Move(source2, (IBlobContentLocation) thmbItem);
              }
            }
            transferredMediaFileLinks.Add(mediaFileLink1);
          }
          else
          {
            MediaFileLink mediaFileLink2 = transferredMediaFileLinks.First<MediaFileLink>();
            mediaFileLink1.NumberOfChunks = mediaFileLink2.NumberOfChunks;
            mediaFileLink1.FilePath = mediaFileLink2.FilePath;
          }
        }
      }
    }
  }
}
