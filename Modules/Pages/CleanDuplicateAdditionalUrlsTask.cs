// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.CleanDuplicateAdditionalUrlsTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Task for cleanning duplicated and orhaned additional URLs
  /// </summary>
  internal class CleanDuplicateAdditionalUrlsTask : ScheduledTask
  {
    private PageManager manager;

    public CleanDuplicateAdditionalUrlsTask() => this.ExecuteTime = DateTime.UtcNow;

    public int NumberOfAttempts { get; set; }

    internal PageManager Manager
    {
      get
      {
        if (this.manager == null)
          this.manager = PageManager.GetManager();
        return this.manager;
      }
      set => this.manager = value;
    }

    public override void SetCustomData(string customData) => this.NumberOfAttempts = int.Parse(customData);

    public override string GetCustomData() => this.NumberOfAttempts.ToString();

    public override void ExecuteTask()
    {
      --this.NumberOfAttempts;
      string str1 = "Clean duplicated and orhaned additional URLs";
      if (this.NumberOfAttempts >= 0)
        Log.Write((object) string.Format("STARTED TASK: {0}", (object) str1), ConfigurationPolicy.UpgradeTrace);
      PageManager manager1 = this.Manager;
      bool suppressEvents = manager1.Provider.SuppressEvents;
      try
      {
        manager1.Provider.SuppressEvents = true;
        using (new ElevatedModeRegion((IManager) manager1))
        {
          IQueryable<PageUrlData> source = manager1.GetUrls<PageUrlData>().Where<PageUrlData>((Expression<Func<PageUrlData, bool>>) (u => !u.IsDefault));
          int count1 = 20;
          int num1 = source.Count<PageUrlData>();
          int num2 = 0;
          if (num1 > 0)
            num2 = (int) Math.Ceiling((double) num1 / (double) count1);
          int num3 = 0;
          for (int index = 0; index < num2; ++index)
          {
            int count2 = index * count1 - num3;
            foreach (PageUrlData pageUrlData in source.Skip<PageUrlData>(count2).Take<PageUrlData>(count1).ToList<PageUrlData>())
            {
              string str2 = (string) null;
              PageNode node = pageUrlData.Node;
              if (node != null)
              {
                using (SiteRegion.FromSiteMapRoot(node.RootNodeId))
                {
                  if (!node.IsBackend)
                  {
                    if (SiteMapBase.GetSiteMapProviderForPageNode(node) is SiteMapBase providerForPageNode)
                    {
                      CultureInfo cultureByLcid = AppSettings.CurrentSettings.GetCultureByLcid(pageUrlData.Culture);
                      using (new CultureRegion(cultureByLcid))
                      {
                        string rawUrl = UrlPath.ResolveUrl(pageUrlData.Url);
                        using (new SiteMapBase.SiteMapContext(providerForPageNode, manager1.Provider.TransactionName))
                        {
                          if (providerForPageNode.FindSiteMapNodeByExactUrl(rawUrl, out bool _) is PageSiteNode mapNodeByExactUrl)
                          {
                            if (providerForPageNode.FindSiteMapNodeForSpecificLanguage((SiteMapNode) mapNodeByExactUrl, cultureByLcid) is PageSiteNode specificLanguage)
                            {
                              if (!(specificLanguage.Id == node.Id))
                                str2 = specificLanguage.GetTitlesPath(culture: cultureByLcid);
                              else
                                continue;
                            }
                            else
                              continue;
                          }
                          else
                            continue;
                        }
                      }
                    }
                    else
                      continue;
                  }
                }
              }
              string str3 = str2 == null ? string.Format("Delete orphaned additional URL: '{0}'", (object) pageUrlData.Url) : string.Format("Delete duplicated additional URL: '{0}' - the same as the default URL of the page '{1}'", (object) pageUrlData.Url, (object) str2);
              manager1.Delete((UrlData) pageUrlData);
              try
              {
                if (!string.IsNullOrEmpty(manager1.TransactionName))
                  TransactionManager.CommitTransaction(manager1.TransactionName);
                else
                  manager1.SaveChanges();
                Log.Write((object) ("PASSED: " + str3), ConfigurationPolicy.UpgradeTrace);
              }
              catch (Exception ex)
              {
                if (!string.IsNullOrEmpty(manager1.TransactionName))
                  TransactionManager.RollbackTransaction(manager1.TransactionName);
                else
                  manager1.CancelChanges();
                if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
                  throw;
                else
                  Log.Write((object) string.Format("FAILED: {0} ({1})", (object) str3, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
              }
              ++num3;
            }
          }
          Log.Write((object) string.Format("PASSED : {0} - {1} duplicated or orphaned additional URLs have been deleted", (object) str1, (object) num3), ConfigurationPolicy.UpgradeTrace);
        }
      }
      catch (Exception ex)
      {
        if (this.NumberOfAttempts < 0)
        {
          throw;
        }
        else
        {
          Log.Write((object) string.Format("FAILED : {0} - {1}", (object) str1, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
          if (Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          {
            throw;
          }
          else
          {
            if (this.NumberOfAttempts <= 0)
              return;
            SchedulingManager manager2 = SchedulingManager.GetManager();
            CleanDuplicateAdditionalUrlsTask additionalUrlsTask = new CleanDuplicateAdditionalUrlsTask();
            additionalUrlsTask.Id = Guid.NewGuid();
            additionalUrlsTask.ExecuteTime = DateTime.UtcNow.AddMinutes(1.0);
            CleanDuplicateAdditionalUrlsTask task = additionalUrlsTask;
            task.NumberOfAttempts = this.NumberOfAttempts;
            manager2.AddTask((ScheduledTask) task);
            manager2.SaveChanges();
          }
        }
      }
      finally
      {
        manager1.Provider.SuppressEvents = suppressEvents;
      }
    }
  }
}
