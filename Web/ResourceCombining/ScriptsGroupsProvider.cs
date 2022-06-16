// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ResourceCombining.ScriptsGroupsProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.ResourceCombining
{
  /// <summary>
  /// Class for generating the groups of script references for each page data
  /// </summary>
  public class ScriptsGroupsProvider
  {
    private const double treshold = 1.618033988749;

    public static IEnumerable<ScriptsGroup> GenerateFrontedScriptGroups()
    {
      PageManager manager = PageManager.GetManager();
      return ScriptsGroupsProvider.GenerateScriptGroups(manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Visible == true)).ToList<PageData>().Where<PageData>((Func<PageData, bool>) (p1 => !p1.NavigationNode.IsBackend)), manager.Provider, SitefinitySiteMap.GetCurrentProvider());
    }

    public static IEnumerable<ScriptsGroup> GenerateBackendScriptGroups()
    {
      PageManager manager = PageManager.GetManager();
      return ScriptsGroupsProvider.GenerateScriptGroups(manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => p.Visible == true)).ToList<PageData>().Where<PageData>((Func<PageData, bool>) (p1 => p1.NavigationNode.IsBackend)), manager.Provider, (SiteMapProvider) BackendSiteMap.GetCurrentProvider());
    }

    private static IEnumerable<ScriptsGroup> GenerateScriptGroups(
      IEnumerable<PageData> pages,
      PageDataProvider pageDataProvider,
      SiteMapProvider siteMapProvider)
    {
      List<PageDataScripts> pagesScripts = new List<PageDataScripts>();
      foreach (PageData page in pages)
      {
        PageDataScripts scripts = new InMemoryPageRender().GetScripts(page, pageDataProvider, siteMapProvider);
        pagesScripts.Add(scripts);
      }
      return (IEnumerable<ScriptsGroup>) ScriptsGroupsProvider.ExtractScriptGroups(pagesScripts);
    }

    private static List<ScriptsGroup> ExtractScriptGroups(
      List<PageDataScripts> pagesScripts)
    {
      if (pagesScripts.Count == 0)
        return new List<ScriptsGroup>();
      pagesScripts.Sort((Comparison<PageDataScripts>) ((p1, p2) => p2.ScriptsCount.CompareTo(p1.ScriptsCount)));
      PageDataScripts pagesScript1 = pagesScripts[0];
      ScriptsGroup scriptsGroup = new ScriptsGroup((IList<ScriptReference>) pagesScript1.Scripts.ToList<ScriptReference>());
      scriptsGroup.PagesInGroup.Add(pagesScript1.PageData);
      pagesScript1.ScriptGroups.Add(scriptsGroup);
      List<ScriptsGroup> scriptGroups = new List<ScriptsGroup>()
      {
        scriptsGroup
      };
      bool flag = true;
      int num = 0;
      for (int index = 1; index < pagesScripts.Count; ++index)
      {
        PageDataScripts pagesScript2 = pagesScripts[index];
        if (pagesScript2.ScriptsCount >= 1)
        {
          HashSet<ScriptReference> scriptReferenceSet = new HashSet<ScriptReference>((IEnumerable<ScriptReference>) pagesScript2.Scripts, (IEqualityComparer<ScriptReference>) new ScriptReferenceComparer());
          HashSet<ScriptReference> other = new HashSet<ScriptReference>(scriptsGroup.Scripts, (IEqualityComparer<ScriptReference>) new ScriptReferenceComparer());
          scriptReferenceSet.IntersectWith((IEnumerable<ScriptReference>) other);
          if (flag)
          {
            num = scriptReferenceSet.Count;
            flag = false;
          }
          if (scriptReferenceSet.Count > 0 && num > 0)
          {
            if ((double) scriptReferenceSet.Count / (double) num + 1.0 > 1.618033988749)
            {
              scriptsGroup.Scripts = (IEnumerable<ScriptReference>) scriptReferenceSet;
            }
            else
            {
              scriptsGroup = new ScriptsGroup((IList<ScriptReference>) pagesScript2.Scripts.ToList<ScriptReference>());
              scriptGroups.Add(scriptsGroup);
              flag = true;
            }
            pagesScript2.ScriptGroups.Add(scriptsGroup);
            scriptsGroup.PagesInGroup.Add(pagesScript2.PageData);
          }
        }
        else
          break;
      }
      return scriptGroups;
    }
  }
}
