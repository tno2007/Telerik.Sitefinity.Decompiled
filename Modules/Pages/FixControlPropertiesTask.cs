// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.FixControlPropertiesTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Modules.Pages
{
  public class FixControlPropertiesTask : ScheduledTask
  {
    public FixControlPropertiesTask() => this.ExecuteTime = DateTime.UtcNow;

    public int NumberOfAttempts { get; set; }

    public Guid LastUpdatedPageId { get; set; }

    public FixControlPropertiesTask.Mode WorkMode { get; set; }

    public override void SetCustomData(string customData)
    {
      string[] strArray = customData.Split(';');
      this.LastUpdatedPageId = Guid.Parse(strArray[0]);
      this.NumberOfAttempts = int.Parse(strArray[1]);
      this.WorkMode = (FixControlPropertiesTask.Mode) int.Parse(strArray[2]);
    }

    public override string GetCustomData() => this.LastUpdatedPageId.ToString() + ";" + (object) this.NumberOfAttempts + ";" + (object) (int) this.WorkMode;

    public override void ExecuteTask()
    {
      string str = "Optimizing control properties persistance, by reducing number of persisted properties";
      --this.NumberOfAttempts;
      PageManager manager1 = PageManager.GetManager();
      bool suppressEvents = manager1.Provider.SuppressEvents;
      try
      {
        manager1.Provider.SuppressEvents = true;
        using (new ElevatedModeRegion((IManager) manager1))
        {
          switch (this.WorkMode)
          {
            case FixControlPropertiesTask.Mode.FlagsOnly:
              this.UpdateControlPropertiesFlags(manager1);
              break;
            default:
              this.DeleteObsoleteControlProps(manager1);
              break;
          }
        }
        Log.Write((object) string.Format("PASSED : {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED : {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
        if (this.NumberOfAttempts > 0)
        {
          SchedulingManager manager2 = SchedulingManager.GetManager();
          FixControlPropertiesTask controlPropertiesTask = new FixControlPropertiesTask();
          controlPropertiesTask.Id = Guid.NewGuid();
          controlPropertiesTask.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
          FixControlPropertiesTask task = controlPropertiesTask;
          task.NumberOfAttempts = this.NumberOfAttempts;
          task.LastUpdatedPageId = this.LastUpdatedPageId;
          task.WorkMode = this.WorkMode;
          manager2.AddTask((ScheduledTask) task);
          manager2.SaveChanges();
          Log.Write((object) string.Format("Scheduling next task for resuming controls properties optimization for {0}", (object) task.ExecuteTime.ToString()), ConfigurationPolicy.UpgradeTrace);
        }
        else
          throw;
      }
      finally
      {
        manager1.Provider.SuppressEvents = suppressEvents;
      }
    }

    private void UpdateControlPropertiesFlags(PageManager manager)
    {
      IQueryable<ControlProperty> source = manager.GetProperties().Include<ControlProperty>((Expression<Func<ControlProperty, object>>) (x => x.ChildProperties)).Include<ControlProperty>((Expression<Func<ControlProperty, object>>) (x => x.ListItems)).Include<ControlProperty>((Expression<Func<ControlProperty, object>>) (x => x.Presentation)).Where<ControlProperty>((Expression<Func<ControlProperty, bool>>) (x => ((IFlagsContainer) x).Flags == 0));
      int count = 500;
      while (true)
      {
        IList<ControlProperty> list = (IList<ControlProperty>) source.Take<ControlProperty>(count).ToList<ControlProperty>();
        if (list.Count != 0)
        {
          foreach (ControlProperty controlProperty in (IEnumerable<ControlProperty>) list)
          {
            IFlagsContainer flagsContainer = (IFlagsContainer) controlProperty;
            if (controlProperty.ListItems.Count > 0)
              flagsContainer.SetFlag(2);
            else if (controlProperty.ChildProperties.Count > 0)
              flagsContainer.SetFlag(4);
            if (controlProperty.Presentation.Count > 0)
              flagsContainer.SetFlag(8);
            flagsContainer.SetFlag(1);
          }
          manager.SaveChanges();
        }
        else
          break;
      }
    }

    private void DeleteObsoleteControlProps(PageManager pageManager)
    {
      int index = 0;
      List<Guid> list = pageManager.GetPageDataList().OrderBy<PageData, Guid>((Expression<Func<PageData, Guid>>) (p => p.Id)).Select<PageData, Guid>((Expression<Func<PageData, Guid>>) (p => p.Id)).ToList<Guid>();
      if (this.LastUpdatedPageId != Guid.Empty)
      {
        Log.Write((object) "Resuming controls properties optimization task", ConfigurationPolicy.UpgradeTrace);
        index = list.IndexOf(this.LastUpdatedPageId) + 1;
      }
      else
        Log.Write((object) "Staring controls properties optimization task", ConfigurationPolicy.UpgradeTrace);
      for (; index < list.Count; ++index)
      {
        PageData pageData;
        try
        {
          pageData = pageManager.GetPageData(list[index]);
        }
        catch (ItemNotFoundException ex)
        {
          continue;
        }
        PageNode navigationNode = pageData.NavigationNode;
        if (navigationNode != null)
        {
          using (SiteRegion.FromSiteMapRoot(navigationNode.RootNodeId))
            this.PersistPageControlsAndCommit(pageManager, pageData);
          this.LastUpdatedPageId = pageData.Id;
        }
      }
    }

    private void PersistPageControlsAndCommit(PageManager manager, PageData page)
    {
      bool flag = this.PersistControls(manager, (IEnumerable<ControlData>) page.Controls);
      foreach (PageDraft draft in (IEnumerable<PageDraft>) page.Drafts)
        this.PersistControls(manager, (IEnumerable<ControlData>) draft.Controls);
      if (!flag)
        return;
      ++page.BuildStamp;
      manager.Provider.CommitTransaction();
    }

    private bool PersistControls(PageManager manager, IEnumerable<ControlData> controls)
    {
      bool flag = false;
      foreach (ControlData control1 in controls)
      {
        if (!control1.IsLayoutControl && Config.Get<ToolboxesConfig>().ValidateWidget(control1) && !control1.ObjectType.StartsWith("Telerik.Sitefinity.Mvc.Proxy.MvcControllerProxy") && control1.Properties.Count != 0 && control1.Properties.Where<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ID")).Count<ControlProperty>() != control1.Properties.Count)
        {
          List<CultureInfo> cultureInfoList = new List<CultureInfo>();
          cultureInfoList.Add(CultureInfo.InvariantCulture);
          if (control1.IsTranslatable)
          {
            foreach (CultureInfo availableLanguage in ControlHelper.GetAvailableLanguages(control1))
            {
              if (!cultureInfoList.Contains(availableLanguage))
                cultureInfoList.Add(availableLanguage);
            }
          }
          Dictionary<CultureInfo, object> dictionary = new Dictionary<CultureInfo, object>();
          try
          {
            foreach (CultureInfo cultureInfo in cultureInfoList)
            {
              Control control2 = manager.LoadControl((ObjectData) control1, this.GetCultureParam(cultureInfo));
              dictionary.Add(cultureInfo, (object) control2);
            }
          }
          catch
          {
            continue;
          }
          if (dictionary.Count > 0)
          {
            try
            {
              manager.ClearProperties((ObjectData) control1);
              control1.SetPersistanceStrategy();
              foreach (CultureInfo key in dictionary.Keys)
                manager.ReadProperties(dictionary[key], (ObjectData) control1, key.Equals((object) CultureInfo.InvariantCulture) ? (CultureInfo) null : key, (object) null);
              manager.Provider.CommitTransaction();
              flag = true;
            }
            catch
            {
              manager.Provider.RollbackTransaction();
            }
          }
        }
      }
      return flag;
    }

    private CultureInfo GetCultureParam(CultureInfo culture) => !culture.Equals((object) CultureInfo.InvariantCulture) ? culture : (CultureInfo) null;

    public enum Mode
    {
      Full = 1,
      FlagsOnly = 2,
    }
  }
}
