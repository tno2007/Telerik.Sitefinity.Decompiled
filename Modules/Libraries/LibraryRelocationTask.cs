// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryRelocationTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Libraries.BlobStorage;
using Telerik.Sitefinity.Scheduling;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>Scheduled task for Library storage relocation</summary>
  /// <seealso cref="T:Telerik.Sitefinity.Scheduling.ScheduledTask" />
  [SuppressMessage("Microsoft.StyleCop.CSharp.NamingRules", "*", Justification = "Public constant with lowercase")]
  public class LibraryRelocationTask : ScheduledTask
  {
    /// <summary>The task name</summary>
    public const string taskName = "LibraryRelocationTask";
    /// <summary>The relocate mode title</summary>
    public const string relocateModeTitle = "RelocateLibrary";
    /// <summary>The transfer mode title</summary>
    public const string transferModeTitle = "TransferLibrary";
    private int itemsCount;
    private int currentIndex;
    private List<string> failedItems = new List<string>();

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Libraries.LibraryRelocationTask" /> class.
    /// </summary>
    public LibraryRelocationTask()
    {
      this.ExecuteTime = DateTime.UtcNow;
      this.Description = Res.Get<LibrariesResources>().TransferLibrary;
    }

    /// <summary>Identifier used for Task Factory</summary>
    public override string TaskName => nameof (LibraryRelocationTask);

    /// <summary>Gets or sets the library identifier.</summary>
    /// <value>The library identifier.</value>
    public Guid LibraryId { get; set; }

    /// <summary>Gets or sets the library provider.</summary>
    /// <value>The library provider.</value>
    public string LibraryProvider { get; set; }

    /// <summary>Gets or sets the library title.</summary>
    /// <value>The library title.</value>
    public string LibraryTitle { get; set; }

    /// <summary>Gets or sets the type of the library.</summary>
    /// <value>The type of the library.</value>
    public string LibraryType { get; set; }

    /// <summary>
    /// Sets the custom data. This is used when reviving a task from a persistent storage to deserialize any custom stored data
    /// </summary>
    /// <param name="customData">The custom task data.</param>
    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.LibraryId = Guid.Parse(strArray[0]);
      this.LibraryProvider = strArray[1];
      if (strArray.Length >= 3)
        this.LibraryTitle = strArray[2];
      if (strArray.Length < 4)
        return;
      this.LibraryType = strArray[3];
    }

    /// <summary>
    /// Builds a unique key for the task based on the parameters.
    /// </summary>
    /// <returns>A unique key for the task.</returns>
    public override string BuildUniqueKey() => this.GetCustomData();

    /// <summary>
    /// Gets any custom data that the task needs persisted. The data should be serialized as a string.
    /// The <seealso cref="M:Telerik.Sitefinity.Modules.Libraries.LibraryRelocationTask.SetCustomData(System.String)" />should have implementation for deserialized the data
    /// </summary>
    /// <returns>string serialized custom task data</returns>
    public override string GetCustomData() => this.LibraryId.ToString() + ";" + this.LibraryProvider + ";" + this.LibraryTitle + ";" + this.LibraryType;

    /// <summary>Executes the task.</summary>
    public override void ExecuteTask()
    {
      string transactionName = this.LibraryId.ToString();
      LibrariesManager manager = LibrariesManager.GetManager(this.LibraryProvider, transactionName);
      try
      {
        Library library = manager.GetLibrary(this.LibraryId);
        library.RunningTask = this.Id;
        manager.Provider.CommitTransaction();
        this.itemsCount = 0;
        this.currentIndex = 0;
        BlobStorageProvider provider = BlobStorageManager.GetManager(manager.Provider.GetMappedBlobStorageProviderName(library.BlobStorageProvider)).Provider;
        string blobStorageProviderName = provider.Name;
        List<Guid> list = library.Items().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => (int) i.Status == 0 && i.BlobStorageProvider != blobStorageProviderName)).OrderBy<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (i => i.LastModified)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (i => i.Id)).ToList<Guid>();
        IEnumerable<Guid> guids = (IEnumerable<Guid>) null;
        if (provider is IExternalBlobStorageProvider externalBlobStorage)
          guids = (IEnumerable<Guid>) library.Items().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => (int) i.Status == 2 && i.BlobStorageProvider == blobStorageProviderName && i.Visible == true)).OrderBy<MediaContent, DateTime>((Expression<Func<MediaContent, DateTime>>) (i => i.LastModified)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (i => i.Id)).ToList<Guid>();
        this.itemsCount = list.Count<Guid>();
        if (guids != null)
          this.itemsCount += guids.Count<Guid>();
        this.UpdateLibraryItemsStorage(manager, provider, (IEnumerable<Guid>) list);
        if (guids != null)
          this.UpdateExternalLibraryItemsLocation(manager, externalBlobStorage, guids);
        if (this.failedItems.Count == 0)
        {
          library.RunningTask = Guid.Empty;
          TransactionManager.CommitTransaction(transactionName);
          Log.Trace("Sucessfully moved all {0} items to storage {1}", (object) this.itemsCount, (object) blobStorageProviderName);
        }
        else
          LibraryTasksUtilities.TaskFailed(this.failedItems, this.itemsCount);
      }
      catch (Exception ex)
      {
        throw ex;
      }
      finally
      {
        TransactionManager.DisposeTransaction(transactionName);
      }
    }

    private void UpdateExternalLibraryItemsLocation(
      LibrariesManager manager,
      IExternalBlobStorageProvider externalBlobStorage,
      IEnumerable<Guid> items)
    {
      BlobStorageProvider blobStorage = externalBlobStorage as BlobStorageProvider;
      foreach (Guid id in items)
      {
        MediaContent mediaItem = manager.GetMediaItem(id);
        try
        {
          this.RecompileItemUrl(manager, mediaItem);
          LibraryTasksUtilities.UpdateExternalLibraryItemLocation(manager, externalBlobStorage, blobStorage, mediaItem);
          this.UpdateProgress();
        }
        catch (TaskStoppedException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          this.ProcessUpdateItemStorageFailed(manager, blobStorage, mediaItem, ex);
        }
      }
    }

    private void UpdateLibraryItemsStorage(
      LibrariesManager manager,
      BlobStorageProvider blobStorage,
      IEnumerable<Guid> items)
    {
      foreach (Guid id in items)
      {
        MediaContent mediaItem = manager.GetMediaItem(id);
        try
        {
          this.RecompileItemUrl(manager, mediaItem);
          LibraryTasksUtilities.UpdateLibraryItemStorage(manager, blobStorage, mediaItem);
          this.UpdateProgress();
        }
        catch (TaskStoppedException ex)
        {
          throw ex;
        }
        catch (Exception ex)
        {
          this.ProcessUpdateItemStorageFailed(manager, blobStorage, mediaItem, ex);
        }
      }
    }

    private void RecompileItemUrl(LibrariesManager manager, MediaContent item)
    {
      manager.RecompileItemUrls<MediaContent>(item);
      CommonMethods.ValidateUrlConstraints<MediaContent>((IManager) manager, item, false);
    }

    private void ProcessUpdateItemStorageFailed(
      LibrariesManager manager,
      BlobStorageProvider blobStorage,
      MediaContent item,
      Exception e)
    {
      string mediaContentFullPath = LibraryTasksUtilities.GetMediaContentFullPath(manager, item);
      Log.Error("Failed to transfer item: {0} from storage {1} to storage {2}.{3}Error: {4}", (object) mediaContentFullPath, (object) item.BlobStorageProvider, (object) blobStorage.Name, (object) Environment.NewLine, (object) e);
      this.failedItems.Add(mediaContentFullPath);
    }

    private void UpdateProgress()
    {
      ++this.currentIndex;
      TaskProgressEventArgs eventArgs = new TaskProgressEventArgs()
      {
        Progress = this.currentIndex * 100 / this.itemsCount,
        StatusMessage = string.Empty
      };
      this.OnProgressChanged(eventArgs);
      if (eventArgs.Stopped)
        throw new TaskStoppedException();
    }
  }
}
