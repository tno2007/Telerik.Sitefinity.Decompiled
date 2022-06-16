// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Libraries.LibraryThumbnailsTaskBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Libraries
{
  /// <summary>
  /// 
  /// </summary>
  public abstract class LibraryThumbnailsTaskBase : ScheduledTask
  {
    private int itemsCount;
    private int currentIndex;
    private string[] profilesFilter;
    public const string regenerateModeTitle = "RegenerateThumbnails";
    private Guid siteId;

    public LibraryThumbnailsTaskBase()
    {
      this.ExecuteTime = DateTime.UtcNow;
      this.Description = Res.Get<LibrariesResources>().RegeneratingThumbnails;
      this.siteId = SystemManager.CurrentContext.CurrentSite.Id;
    }

    public Guid LibraryId { get; set; }

    public string LibraryProvider { get; set; }

    public string[] ProfilesFilter
    {
      get
      {
        if (this.profilesFilter == null)
          this.profilesFilter = new string[0];
        return this.profilesFilter;
      }
      set => this.profilesFilter = value;
    }

    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.LibraryId = Guid.Parse(strArray[0]);
      this.LibraryProvider = strArray[1];
      this.ProfilesFilter = ((IEnumerable<string>) strArray[2].Split(new char[1]
      {
        ','
      }, StringSplitOptions.RemoveEmptyEntries)).ToArray<string>();
      if (strArray.Length <= 3)
        return;
      this.siteId = new Guid(strArray[3]);
    }

    public override string BuildUniqueKey() => this.GetCustomData();

    public override string GetCustomData() => this.LibraryId.ToString() + ";" + this.LibraryProvider + ";" + string.Join(",", this.ProfilesFilter) + ";" + this.siteId.ToString();

    public override void ExecuteTask()
    {
      using (SiteRegion.FromSiteId(this.siteId))
      {
        string transactionName = this.LibraryId.ToString();
        LibrariesManager manager = LibrariesManager.GetManager(this.LibraryProvider, transactionName);
        try
        {
          Library library = manager.GetLibrary(this.LibraryId);
          if (library.RunningTask != Guid.Empty && library.RunningTask != this.Id)
          {
            SchedulingManager manager1 = SchedulingManager.GetManager();
            ScheduledTaskData scheduledTaskData = manager1.GetTaskData().Where<ScheduledTaskData>((Expression<Func<ScheduledTaskData, bool>>) (t => t.Id == library.RunningTask)).SingleOrDefault<ScheduledTaskData>();
            if (scheduledTaskData != null)
            {
              bool flag = true;
              if (!scheduledTaskData.IsRunning || scheduledTaskData.Status != TaskStatus.Started)
              {
                try
                {
                  manager1.DeleteItem((object) scheduledTaskData);
                  manager1.SaveChanges();
                  library.RunningTask = Guid.Empty;
                  TransactionManager.CommitTransaction(transactionName);
                  flag = false;
                }
                catch
                {
                }
              }
              if (flag)
                throw new Exception("Another task is already running on this library, it has to be finished first before executing this task.");
            }
          }
          if (library.RunningTask == Guid.Empty)
          {
            library.RunningTask = this.Id;
            if (manager.Provider.GetTransaction() is SitefinityOAContext transaction)
            {
              transaction.GetAll<MediaContent>().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.ApplicationName == manager.Provider.ApplicationName && i.Parent == library && (int) i.Status == 0 && i.Uploaded)).UpdateAll<MediaContent>((Expression<Func<ExtensionMethods.UpdateDescription<MediaContent>, ExtensionMethods.UpdateDescription<MediaContent>>>) (u => u.Set<bool>((Expression<Func<MediaContent, bool>>) (i => i.NeedThumbnailsRegeneration), (Expression<Func<MediaContent, bool>>) (i => true))));
            }
            else
            {
              SystemManager.BulkUpdateItems<MediaContent>((IManager) manager, (IEnumerable<MediaContent>) manager.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (i => i.Parent == library && (int) i.Status == 0 && i.Uploaded)), (System.Action<MediaContent>) (i => i.NeedThumbnailsRegeneration = true), 50);
              TransactionManager.CommitTransaction(transactionName);
            }
          }
          List<Guid> list = manager.GetMediaItems().Where<MediaContent>((Expression<Func<MediaContent, bool>>) (x => x.Parent == library && x.NeedThumbnailsRegeneration && (int) x.Status == 0 && x.Uploaded)).Select<MediaContent, Guid>((Expression<Func<MediaContent, Guid>>) (x => x.Id)).ToList<Guid>();
          this.itemsCount = list.Count;
          bool flag1 = false;
          foreach (Guid id in list)
          {
            MediaContent mediaItem = manager.GetMediaItem(id);
            try
            {
              MediaContent live = manager.Lifecycle.GetLive((ILifecycleDataItem) mediaItem) as MediaContent;
              foreach (string cultureName in SystemManager.CurrentContext.AppSettings.Multilingual ? ((IEnumerable<string>) mediaItem.AvailableLanguages).Where<string>((Func<string, bool>) (c => !string.IsNullOrEmpty(c))) : (IEnumerable<string>) new string[1])
              {
                using (new CultureRegion(cultureName))
                {
                  if (mediaItem.IsVectorGraphics() || this.UpdateItem(manager, mediaItem))
                  {
                    if (live != null)
                    {
                      if (!live.IsVectorGraphics())
                      {
                        if (live.FileId == mediaItem.FileId)
                          manager.CopyThumbnails(mediaItem, live);
                        else
                          this.UpdateItem(manager, live);
                      }
                    }
                  }
                }
              }
              mediaItem.NeedThumbnailsRegeneration = false;
              TransactionManager.CommitTransaction(transactionName);
            }
            catch (Exception ex)
            {
              flag1 = true;
              Log.Write((object) "Unable to update MediaContent item '{0}' with ID={1}: {2}".Arrange((object) mediaItem.Title, (object) id, (object) ex.ToString()));
              --this.currentIndex;
            }
            this.UpdateProgress();
          }
          if (flag1)
            throw new Exception("Unable to update some MediaContent. Check trace log for details.");
          library = manager.GetLibrary(this.LibraryId);
          library.RunningTask = Guid.Empty;
          library.NeedThumbnailsRegeneration = false;
          TransactionManager.CommitTransaction(transactionName);
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
    }

    protected abstract bool UpdateItem(LibrariesManager manager, MediaContent content);

    private void UpdateProgress()
    {
      ++this.currentIndex;
      TaskProgressEventArgs eventArgs = new TaskProgressEventArgs()
      {
        Progress = this.currentIndex * 100 / this.itemsCount,
        StatusMessage = ""
      };
      this.OnProgressChanged(eventArgs);
      if (eventArgs.Stopped)
        throw new TaskStoppedException();
    }
  }
}
