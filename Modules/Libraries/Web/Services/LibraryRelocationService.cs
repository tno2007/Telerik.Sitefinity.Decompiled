// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.Web.Services.LibraryRelocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Folders;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Security;

namespace Telerik.Sitefinity.Modules.Libraries.Web.Services
{
  /// <summary>
  /// Service implementation for REST-full operations related to relocating the public Url or transferring the storage of a media library
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class LibraryRelocationService : ILibraryRelocationService
  {
    /// <summary>Relocates the library.</summary>
    /// <param name="libraryProvider"></param>
    /// <param name="libraryId">The library id.</param>
    /// <param name="libraryName">The new name of the library.</param>
    /// <param name="libraryUrl">The new library URL.</param>
    /// <returns></returns>
    public Guid RelocateLibrary(
      string libraryType,
      string libraryProvider,
      string libraryId,
      string libraryUrl = null,
      string blobProvider = null)
    {
      LibrariesManager manager = LibrariesManager.GetManager(libraryProvider);
      return this.RelocateLibrary(libraryType, manager, libraryId, libraryUrl, blobProvider);
    }

    internal Guid RelocateLibrary(
      string libraryType,
      LibrariesManager manager,
      string libraryId,
      string libraryUrl = null,
      string blobProvider = null)
    {
      string str = ((IEnumerable<string>) libraryType.Split('.')).Last<string>();
      Guid id = Guid.Parse(libraryId);
      Library library = (Library) null;
      if (!(str == "Album"))
      {
        if (!(str == "VideoLibrary"))
        {
          if (str == "DocumentLibrary")
          {
            library = (Library) manager.GetDocumentLibrary(id);
            library.Demand("Document", "ManageDocument");
          }
        }
        else
        {
          library = (Library) manager.GetVideoLibrary(id);
          library.Demand("Video", "ManageVideo");
        }
      }
      else
      {
        library = (Library) manager.GetAlbum(id);
        library.Demand("Image", "ManageImage");
      }
      if (library.RunningTask == new Guid())
        return this.StartLibraryTask(manager, library, libraryUrl, blobProvider);
      if (SchedulingManager.GetManager().GetTaskData().FirstOrDefault<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (x => x.Id == library.RunningTask)) == null)
        return this.StartLibraryTask(manager, library, libraryUrl, blobProvider);
      throw new Exception(Res.Get<LibrariesResources>().AnotherTaskRunning);
    }

    private Guid StartLibraryTask(
      LibrariesManager manager,
      Library library,
      string libraryUrl,
      string blobProvider)
    {
      library.RunningTask = new Guid();
      if (libraryUrl != null)
        library.UrlName = (Lstring) libraryUrl;
      if (blobProvider != null)
        library.BlobStorageProvider = blobProvider;
      manager.Provider.RecompileLibraryUrl(library);
      manager.ValidateUrlConstraints<Library>(library);
      if (string.IsNullOrEmpty(manager.TransactionName))
        manager.SaveChanges();
      else
        TransactionManager.CommitTransaction(manager.TransactionName);
      return LibrariesManager.StartRelocateLibraryItemsTaskInteral(library.Id, (library.Provider as LibrariesDataProvider).Name, library.Title.ToString(), library.Title.ToString(), library.GetType().FullName);
    }

    /// <summary>Relocates the folder.</summary>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="folderId">The folder id.</param>
    /// <param name="newFolderUrl">The new folder URL.</param>
    public void RelocateFolder(string providerName, string folderId, string newFolderUrl) => this.RelocateFolder(LibrariesManager.GetManager(providerName), folderId, newFolderUrl);

    internal void RelocateFolder(LibrariesManager manager, string folderId, string newFolderUrl)
    {
      Folder folder = manager.GetFolder(Guid.Parse(folderId)) as Folder;
      string str = CommonMethods.ValidateUrl(newFolderUrl);
      folder.UrlName = (Lstring) str;
      manager.ValidateFolderUrl(folder);
      foreach (MediaContent content in manager.GetDescendants((IFolder) folder).ToArray<MediaContent>())
        manager.RecompileAndValidateUrls<MediaContent>(content);
      if (string.IsNullOrEmpty(manager.TransactionName))
        manager.SaveChanges();
      else
        TransactionManager.CommitTransaction(manager.TransactionName);
    }

    /// <summary>Executes the task command.</summary>
    /// <param name="taskId">The task id.</param>
    /// <param name="command">The command.</param>
    /// <returns></returns>
    public bool ExecuteTaskCommand(string taskId, string command)
    {
      Guid taskId1 = Guid.Parse(taskId);
      if (!(command == "restart-task"))
      {
        if (command == "stop-task")
          Scheduler.Instance.StopTask(taskId1);
      }
      else
        Scheduler.Instance.RestartTask(taskId1);
      return true;
    }
  }
}
