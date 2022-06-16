// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.PageDataContext
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Microsoft.Practices.EnterpriseLibrary.Caching;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Contains metadata information for PageData objects</summary>
  internal class PageDataContext
  {
    private readonly PageDataProxy item;
    private readonly Dictionary<CultureInfo, PageDataProxy> items;

    internal PageDataContext() => this.item = PageDataProxy.Empty;

    public PageDataContext(IList<PageData> pageDataList, PageManager manager, Guid pageNodeId)
    {
      if (pageDataList != null && pageDataList.Count > 0)
      {
        if (pageDataList.Count > 1)
        {
          this.items = new Dictionary<CultureInfo, PageDataProxy>();
          foreach (PageData pageData in (IEnumerable<PageData>) pageDataList)
          {
            PageDataProxy pageDataProxy = new PageDataProxy(pageData, manager);
            CultureInfo key = pageData.Culture == null ? CultureInfo.InvariantCulture : CultureInfo.GetCultureInfo(pageData.Culture);
            if (!this.items.ContainsKey(key))
              this.items.Add(key, pageDataProxy);
          }
        }
        else
          this.item = new PageDataProxy(pageDataList[0], manager);
        this.SetVariationKeys(pageDataList, manager);
        this.PageNodeId = pageNodeId;
      }
      else
      {
        this.item = new PageDataProxy((PageData) null, manager);
        this.VariationKeys = (IDictionary<string, List<string>>) new Dictionary<string, List<string>>();
      }
    }

    public PageDataProxy Current => this.GetPageData();

    internal IDictionary<string, List<string>> VariationKeys { get; private set; }

    internal Guid PageNodeId { get; private set; }

    public IList<ICacheItemExpiration> GetCacheDependencyObjects()
    {
      List<ICacheItemExpiration> dependencies = new List<ICacheItemExpiration>();
      if (this.items != null)
      {
        foreach (PageDataProxy pageData in this.items.Values)
          this.AddCacheDependencyObjects((IList<ICacheItemExpiration>) dependencies, pageData);
      }
      else
        this.AddCacheDependencyObjects((IList<ICacheItemExpiration>) dependencies, this.item);
      return (IList<ICacheItemExpiration>) dependencies;
    }

    public PageDataProxy GetPageData(CultureInfo culture = null)
    {
      if (this.items == null)
        return this.item;
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      PageDataProxy pageDataProxy;
      return this.items.TryGetValue(culture, out pageDataProxy) || this.items.TryGetValue(CultureInfo.InvariantCulture, out pageDataProxy) ? pageDataProxy : this.items.First<KeyValuePair<CultureInfo, PageDataProxy>>().Value;
    }

    internal void Init(PageDataProxy item)
    {
    }

    private void AddCacheDependencyObjects(
      IList<ICacheItemExpiration> dependencies,
      PageDataProxy pageData)
    {
      if (pageData.TemplatesIds != null)
      {
        foreach (Guid templatesId in (IEnumerable<Guid>) pageData.TemplatesIds)
          dependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (PageTemplate), templatesId));
      }
      if (!(pageData.Id != Guid.Empty))
        return;
      dependencies.Add((ICacheItemExpiration) new DataItemCacheDependency(typeof (PageDraft), pageData.Id));
    }

    private void SetVariationKeys(IList<PageData> pageDataList, PageManager manager)
    {
      List<Guid> list1 = pageDataList.Where<PageData>((Func<PageData, bool>) (p => p.IsPersonalized)).Select<PageData, Guid>((Func<PageData, Guid>) (p => p.Id)).ToList<Guid>();
      this.VariationKeys = (IDictionary<string, List<string>>) new Dictionary<string, List<string>>();
      if (list1.Count<Guid>() <= 0)
        return;
      PageNode navigationNode = pageDataList.First<PageData>().NavigationNode;
      ISite site = (ISite) null;
      if (navigationNode != null)
        site = PageManager.GetSite(navigationNode);
      if (site == null)
        site = SystemManager.CurrentContext.CurrentSite;
      foreach (IGrouping<string, VariationTypeKeyInfo> source in this.GetVariationTypeInfos(manager, (IEnumerable<Guid>) list1, site).GroupBy<VariationTypeKeyInfo, string>((Func<VariationTypeKeyInfo, string>) (p => p.Culture)))
      {
        string key = source.Key;
        if (string.IsNullOrWhiteSpace(key))
          key = site.DefaultCulture.Name;
        List<string> list2 = source.Select<VariationTypeKeyInfo, string>((Func<VariationTypeKeyInfo, string>) (p => p.Key)).Distinct<string>().ToList<string>();
        if (this.VariationKeys.ContainsKey(key))
        {
          foreach (string str in list2)
          {
            if (!this.VariationKeys[key].Contains(str))
              this.VariationKeys[key].Add(str);
          }
        }
        else
          this.VariationKeys[key] = list2;
      }
    }

    private IEnumerable<VariationTypeKeyInfo> GetVariationTypeInfos(
      PageManager manager,
      IEnumerable<Guid> masterIds,
      ISite site)
    {
      return manager.GetPageDataList().Where<PageData>((Expression<Func<PageData, bool>>) (p => masterIds.Contains<Guid>(p.PersonalizationMasterId))).ToList<PageData>().SelectMany<PageData, VariationTypeKeyInfo>((Func<PageData, IEnumerable<VariationTypeKeyInfo>>) (p =>
      {
        List<string> source;
        if (!string.IsNullOrEmpty(p.Culture) && p.LanguageData.Count == 0)
          source = new List<string>() { p.Culture };
        else
          source = p.GetAvailableCultures(site).Select<CultureInfo, string>((Func<CultureInfo, string>) (c => c.Name)).ToList<string>();
        return source.Select<string, VariationTypeKeyInfo>((Func<string, VariationTypeKeyInfo>) (c => new VariationTypeKeyInfo()
        {
          Culture = c,
          Key = p.VariationTypeKey
        }));
      }));
    }
  }
}
