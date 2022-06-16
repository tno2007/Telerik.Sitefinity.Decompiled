// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.ReindexTask
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Web.Services;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Scheduling;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Search;

namespace Telerik.Sitefinity.Publishing
{
  /// <summary>The scheduled task for indexing content</summary>
  internal class ReindexTask : ScheduledTask
  {
    internal static readonly string Name = typeof (ReindexTask).FullName;

    public override string TaskName => ReindexTask.Name;

    public ReindexTaskSettings Settings { get; set; }

    public override string GetCustomData() => this.Settings.ToString();

    internal static ReindexTaskSettings GetTaskSettings(ScheduledTaskData task) => ReindexTaskSettings.Parse(task.TaskData);

    public override void SetCustomData(string customData) => this.Settings = ReindexTaskSettings.Parse(customData);

    public override void ExecuteTask()
    {
      try
      {
        Guid pointGuid = this.Settings.PublishingPointId;
        string publishingPointProvider = this.Settings.PublishingPointProvider;
        Guid siteId = this.Settings.SiteId;
        SitefinityContextBase currentContext = SystemManager.CurrentContext;
        using (new SiteRegion(!(siteId != Guid.Empty) ? currentContext.CurrentSite : currentContext.MultisiteContext.GetSiteById(siteId)))
        {
          SystemManager.CurrentHttpContext.Items[(object) "RunIndexingSynchronously"] = (object) true;
          ProvidersCollection<PublishingDataProviderBase> staticProviders = PublishingManager.GetManager().StaticProviders;
          SearchIndexPipeSettings settings1 = (SearchIndexPipeSettings) null;
          string providerName = (string) null;
          foreach (PublishingDataProviderBase dataProviderBase in (Collection<PublishingDataProviderBase>) staticProviders)
          {
            settings1 = PublishingManager.GetManager(dataProviderBase.Name).GetPipeSettings<SearchIndexPipeSettings>().Where<SearchIndexPipeSettings>((Expression<Func<SearchIndexPipeSettings, bool>>) (ps => ps.PublishingPoint.Id == pointGuid)).FirstOrDefault<SearchIndexPipeSettings>();
            if (settings1 != null)
            {
              providerName = dataProviderBase.Name;
              break;
            }
          }
          if (settings1 != null)
          {
            this.GetService().DeleteIndex(settings1.CatalogName);
            IEnumerable<IFieldDefinition> indexDefinitions = PublishingAdminService.GetIndexDefinitions((PipeSettings) settings1);
            this.GetService().CreateIndex(settings1.CatalogName, indexDefinitions);
            if (settings1.PublishingPoint.Settings.ItemFilterStrategy == PublishingItemFilter.All && !settings1.PublishingPoint.IsActive)
            {
              PublishingManager manager = PublishingManager.GetManager(publishingPointProvider);
              PublishingPoint publishingPoint = manager.GetPublishingPoint(settings1.PublishingPoint.Id);
              if (!publishingPoint.IsActive)
              {
                publishingPoint.IsActive = true;
                manager.SaveChanges();
              }
            }
            if (!string.IsNullOrWhiteSpace(settings1.PublishingPoint.InboundPipesTemplate))
              PublishingSystemFactory.UpdatePublishingPointBasedOnTemplate(settings1.PublishingPoint.Id, providerName);
          }
          Action<int, int, PipeSettings> action = (Action<int, int, PipeSettings>) ((currentStep, totalSteps, settings) =>
          {
            this.UpdateProgress(currentStep * 100 / totalSteps, string.Format("Indexing {0}", (object) this.GetContentNameFromSettings(settings)));
            string pipeSettingsKey = this.GetPipeSettingsKey(settings);
            if (!this.Settings.ProcessedPipes.Contains(pipeSettingsKey))
              this.Settings.ProcessedPipes.Add(pipeSettingsKey);
            this.PersistState();
          });
          Predicate<PipeSettings> predicate = (Predicate<PipeSettings>) (settings => !this.Settings.ProcessedPipes.Contains(this.GetPipeSettingsKey(settings)));
          PublishingManager.InvokeInboundPushPipes(pointGuid, providerName ?? publishingPointProvider, action, predicate);
        }
      }
      catch (Exception ex)
      {
        Log.Write((object) ex.Message, ConfigurationPolicy.ErrorLog);
      }
    }

    private string GetPipeSettingsKey(PipeSettings settings) => settings is SitefinityContentPipeSettings contentPipeSettings && !string.IsNullOrEmpty(contentPipeSettings.ContentTypeName) ? contentPipeSettings.ContentTypeName : this.GetContentNameFromSettings(settings);

    private string GetContentNameFromSettings(PipeSettings settings)
    {
      WcfPipeSettings wcfPipeSettings = new WcfPipeSettings(settings, settings.GetProviderName());
      return wcfPipeSettings.ContentName ?? wcfPipeSettings.UIName;
    }

    private ISearchService GetService() => ServiceBus.ResolveService<ISearchService>();
  }
}
