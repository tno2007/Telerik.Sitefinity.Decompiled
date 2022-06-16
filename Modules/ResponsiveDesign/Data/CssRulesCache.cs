// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ResponsiveDesign.Data.CssRulesCache
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.ResponsiveDesign.Model;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.ResponsiveDesign.Data
{
  internal class CssRulesCache
  {
    private readonly ResponsiveDesignCache mainCache;
    private IDictionary<Guid, IList<CssRulesCache.Item>> itemsCache;
    private readonly object itemsLock = new object();

    private CssRulesCache(ResponsiveDesignCache mainCache) => this.mainCache = mainCache;

    internal static CssRulesCache GetInstance()
    {
      ResponsiveDesignCache mainCache = ResponsiveDesignCache.GetInstance();
      return mainCache.GetOrAddSubCache("CssRules", (Func<object>) (() => (object) new CssRulesCache(mainCache))) as CssRulesCache;
    }

    internal IEnumerable<CssRulesCache.Item> GetItems(PageDataProxy pageData)
    {
      IDictionary<Guid, IList<CssRulesCache.Item>> cache = this.itemsCache;
      if (cache == null)
      {
        lock (this.itemsLock)
        {
          cache = this.itemsCache;
          if (cache == null)
          {
            cache = this.GenerateItems();
            this.itemsCache = cache;
          }
        }
      }
      IList<CssRulesCache.Item> objList;
      return this.mainCache.GetMediaQueries(pageData, ResponsiveDesignBehavior.TransformLayout).Select<IMediaQuery, Guid>((Func<IMediaQuery, Guid>) (mq => mq.Id)).SelectMany<Guid, CssRulesCache.Item>((Func<Guid, IEnumerable<CssRulesCache.Item>>) (id => cache.TryGetValue(id, out objList) ? (IEnumerable<CssRulesCache.Item>) objList : (IEnumerable<CssRulesCache.Item>) new CssRulesCache.Item[0]));
    }

    private IDictionary<Guid, IList<CssRulesCache.Item>> GenerateItems()
    {
      Dictionary<Guid, IList<CssRulesCache.Item>> items = new Dictionary<Guid, IList<CssRulesCache.Item>>();
      foreach (IMediaQuery mediaQuery in this.mainCache.GetMediaQueries(ResponsiveDesignBehavior.TransformLayout).Where<IMediaQuery>((Func<IMediaQuery, bool>) (mq => mq.AdditionalCssFilePath != null)))
      {
        List<CssRulesCache.Item> objList = new List<CssRulesCache.Item>();
        foreach (IMediaQueryRule mediaQueryRule in mediaQuery.MediaQueryRules)
        {
          CssRulesCache.Item obj = new CssRulesCache.Item(mediaQuery.AdditionalCssFilePath, mediaQueryRule.ResultingRule);
          objList.Add(obj);
        }
        items.Add(mediaQuery.Id, (IList<CssRulesCache.Item>) objList);
      }
      return (IDictionary<Guid, IList<CssRulesCache.Item>>) items;
    }

    internal class Item
    {
      internal Item(string url, string rule)
      {
        this.Url = url.Replace("~/App_Data/Sitefinity/WebsiteTemplates/", "~/Sitefinity/WebsiteTemplates/");
        this.MediaRule = rule.Length > 7 ? rule.Substring(6).Trim() : string.Empty;
      }

      public string Url { get; private set; }

      public string MediaRule { get; private set; }
    }
  }
}
