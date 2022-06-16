// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.PipeUtility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.BackgroundTasks;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  internal static class PipeUtility
  {
    public static string GetItemUrl(WrapperObject parsedItem)
    {
      object obj1 = (object) null;
      object obj2 = (object) null;
      object obj3 = (object) null;
      if (parsedItem.HasProperty("PipeId"))
        obj1 = parsedItem.GetProperty("PipeId");
      if (parsedItem.HasProperty("OriginalItemId"))
        obj2 = parsedItem.GetProperty("OriginalItemId");
      if (parsedItem.HasProperty("Link"))
        obj3 = parsedItem.GetProperty("Link");
      if (obj2 != null && obj1 != null && !obj1.ToString().IsNullOrEmpty() && obj1.ToString().IsGuid() && obj1 != (object) Guid.Empty.ToString() && (obj3 == null || string.IsNullOrEmpty((string) obj3)) && obj3 == null)
      {
        Guid result;
        if (!Guid.TryParse(obj1.ToString(), out result))
          return string.Empty;
        PipeSettings pipeSettings = PipeUtility.GetPipeSettings(result);
        if (pipeSettings != null && pipeSettings is SitefinityContentPipeSettings)
        {
          Guid? backLinksPageId = ((SitefinityContentPipeSettings) pipeSettings).BackLinksPageId;
          if (backLinksPageId.HasValue && SitefinitySiteMap.GetCurrentProvider().FindSiteMapNodeFromKey(backLinksPageId.ToString()) is PageSiteNode siteMapNodeFromKey)
          {
            IManager mappedManager1 = ManagerBase.GetMappedManager(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName);
            Type itemType = TypeResolutionService.ResolveType(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName, false);
            foreach (string providerName in mappedManager1.GetProviderNames(ProviderBindingOptions.NoFilter))
            {
              IManager mappedManager2 = ManagerBase.GetMappedManager(((SitefinityContentPipeSettings) pipeSettings).ContentTypeName, providerName);
              IEnumerator enumerator = mappedManager2.GetItems(itemType, "OriginalContentId == " + obj2.ToString() + " and Status==Live", "", 0, 0).GetEnumerator();
              if (enumerator.MoveNext())
              {
                string empty = string.Empty;
                CultureInfo cultureInfo = CultureInfo.GetCultureInfo(parsedItem.Language);
                CultureInfo culture = SystemManager.CurrentContext.Culture;
                string withoutExtension;
                try
                {
                  SystemManager.CurrentContext.Culture = cultureInfo;
                  withoutExtension = siteMapNodeFromKey.UrlWithoutExtension;
                }
                finally
                {
                  SystemManager.CurrentContext.Culture = culture;
                }
                return RouteHelper.ResolveUrl(withoutExtension, UrlResolveOptions.Absolute) + ((UrlDataProviderBase) mappedManager2.Provider).GetItemUrl((ILocatable) enumerator.Current);
              }
            }
          }
        }
      }
      return obj3 != null ? obj3.ToString() : string.Empty;
    }

    public static PageNode GetPageNode(Guid? pageId)
    {
      ProvidersCollection<PageDataProvider> staticProviders = PageManager.GetManager().StaticProviders;
      PageNode pageNode = (PageNode) null;
      foreach (DataProviderBase dataProviderBase in (Collection<PageDataProvider>) staticProviders)
      {
        pageNode = PageManager.GetManager(dataProviderBase.Name).GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (pn => (Guid?) pn.Id == pageId)).FirstOrDefault<PageNode>();
        if (pageNode != null)
          break;
      }
      return pageNode;
    }

    public static PipeSettings GetPipeSettings(Guid pipeSettingsId)
    {
      PipeSettings pipeSettings = (PipeSettings) null;
      foreach (DataProviderBase staticProvider in (Collection<PublishingDataProviderBase>) PublishingManager.GetManager().StaticProviders)
      {
        pipeSettings = PublishingManager.GetManager(staticProvider.Name).GetPipeSettings().Where<PipeSettings>((Expression<Func<PipeSettings, bool>>) (ps => ps.Id == pipeSettingsId)).FirstOrDefault<PipeSettings>();
        if (pipeSettings != null)
          break;
      }
      return pipeSettings;
    }

    /// <summary>Pushes the data asynchronously.</summary>
    /// <param name="items">The items.</param>
    public static void PushDataAsync<T>(this T pipe, IList<PublishingSystemEventInfo> items) where T : IPipe, IAsyncPushPipe
    {
      Guid id = SystemManager.CurrentContext.CurrentSite.Id;
      PublishingPushDataTask<T> task = new PublishingPushDataTask<T>(items, pipe, id);
      ObjectFactory.Resolve<BackgroundTasksService>().EnqueueTask((Action) (() => task.Run((IBackgroundTaskContext) null)), true);
    }
  }
}
