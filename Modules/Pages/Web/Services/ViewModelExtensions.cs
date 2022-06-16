// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.Web.Services.ViewModelExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Pages;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Pages.Web.Services
{
  /// <summary>Contains extensions for copying ViewModels to models</summary>
  internal static class ViewModelExtensions
  {
    internal static void TransferToNode(
      this WcfPage proxy,
      PageNode n,
      PageFacade pageFacade,
      PageNode sourceNode,
      bool isNew,
      bool isLanguageVersion,
      bool isLanguageVersionOfSplitPage,
      out bool additionalUrlsChanged)
    {
      bool isBackendPage = PageHelper.IsPageNodeBackend(n);
      if (n.Title != (Lstring) proxy.Title.PersistedValue)
        n.Title = (Lstring) proxy.Title.PersistedValue;
      if (proxy.IsExternal)
      {
        if (proxy.LinkedNodeId == Guid.Empty)
        {
          if (n.NodeType != NodeType.OuterRedirect)
            n.NodeType = NodeType.OuterRedirect;
          if (n.LinkedNodeId != Guid.Empty)
            n.LinkedNodeId = Guid.Empty;
          if (n.LinkedNodeProvider != string.Empty)
            n.LinkedNodeProvider = string.Empty;
          if (n.RedirectUrl != (Lstring) proxy.RedirectUrl)
            n.RedirectUrl = (Lstring) proxy.RedirectUrl;
          if (n.OpenNewWindow != proxy.OpenNewWindow)
            n.OpenNewWindow = proxy.OpenNewWindow;
        }
        else
        {
          if (n.NodeType != NodeType.InnerRedirect)
            n.NodeType = NodeType.InnerRedirect;
          if (n.LinkedNodeId != proxy.LinkedNodeId)
            n.LinkedNodeId = proxy.LinkedNodeId;
          if (n.LinkedNodeProvider != proxy.LinkedNodeProvider)
            n.LinkedNodeProvider = proxy.LinkedNodeProvider;
          if (n.RedirectUrl != (Lstring) null)
            n.RedirectUrl = (Lstring) null;
          if (n.OpenNewWindow)
            n.OpenNewWindow = false;
        }
      }
      else if (!proxy.IsGroup)
      {
        if (n.NodeType != NodeType.Standard)
          n.NodeType = NodeType.Standard;
      }
      else if (n.NodeType != NodeType.Group)
        n.NodeType = NodeType.Group;
      if (n.IsBackend && proxy.HasCustomUrl())
      {
        string[] collection = proxy.UrlName.Split('/');
        if (collection.Length > 1 && !collection[1].Equals("sitefinity"))
        {
          List<string> values = new List<string>((IEnumerable<string>) collection);
          values.Insert(1, "sitefinity");
          proxy.UrlName = string.Join("/", (IEnumerable<string>) values);
        }
      }
      if (!proxy.IsGroup && !proxy.IsExternal)
        proxy.CopyStandardPageProperties(pageFacade.PageManager, n, sourceNode, true, isNew, isLanguageVersion, isLanguageVersionOfSplitPage, isBackendPage);
      if (n.IncludeInSearchIndex != proxy.IncludeInSearchIndex)
        n.IncludeInSearchIndex = proxy.IncludeInSearchIndex;
      if (n.ShowInNavigation != proxy.ShowInNavigation)
        n.ShowInNavigation = proxy.ShowInNavigation;
      if (n.AllowParametersValidation != proxy.AllowParameterValidation)
        n.AllowParametersValidation = proxy.AllowParameterValidation;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      List<string> stringList = new List<string>();
      if (proxy.AllowMultipleUrls)
        stringList = ((IEnumerable<string>) proxy.MultipleNavigationNodes.Split(new string[3]
        {
          "\r\n",
          "\r",
          "\n"
        }, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
      additionalUrlsChanged = pageFacade.PageManager.SetPageNodeAdditionalUrls(n, culture, stringList.ToArray());
      if (n.AdditionalUrlsRedirectToDefaultOne != proxy.AdditionalUrlsRedirectToDefaultOne)
        n.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault && u.RedirectToDefault != proxy.AdditionalUrlsRedirectToDefaultOne)).ToList<PageUrlData>().ForEach((Action<PageUrlData>) (url => url.RedirectToDefault = proxy.AdditionalUrlsRedirectToDefaultOne));
      pageFacade.PageManager.SetPageNodeUrlName(n, proxy.UrlName.Trim(), culture);
      if (!proxy.HasCustomUrl())
      {
        string autoUrl = n.BuildUrl(culture);
        foreach (PageUrlData pageUrlData in pageFacade.PageManager.GetUrlsForLanguage(n.Urls.Where<PageUrlData>((Func<PageUrlData, bool>) (u => !u.IsDefault && u.Url == autoUrl)), culture, n.RootNodeId).ToList<PageUrlData>())
        {
          n.Urls.Remove(pageUrlData);
          pageFacade.PageManager.Delete((UrlData) pageUrlData);
        }
      }
      PageData pageData = n.GetPageData();
      if (pageData != null && pageData.Culture == null)
      {
        Telerik.Sitefinity.Abstractions.AppSettings currentSettings = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings;
        if (isBackendPage)
          pageData.Culture = currentSettings.DefaultBackendLanguage.Name;
        else if (pageData.Culture != currentSettings.DefaultFrontendLanguage.Name)
          pageData.Culture = currentSettings.DefaultFrontendLanguage.Name;
      }
      if ((double) n.Priority != (double) proxy.Priority)
        n.Priority = proxy.Priority;
      proxy.UpdateCustomFields(n);
    }

    internal static void UpdateCustomFields(this WcfPage proxy, PageNode page) => ReflectionHelper.DeepCopy((object) proxy.CustomFields, typeof (PageNode), (CreateInstanceDelegate) ((type, propertyPath, source) => type == typeof (PageNode) ? (object) page : TypeSurrogateFactory.Instance.CreateInstance(type, source, propertyPath)), (CanCopyPropertyDelegate) (p => p.Attributes[typeof (DataMemberAttribute)] != null), "");

    /// <summary>
    /// Copies the page properties from the proxy to the page data.
    /// </summary>
    /// <param name="proxy">The proxy.</param>
    /// <param name="pageData">The page data.</param>
    internal static void CopyPagePropertiesTo(
      this WcfPage proxy,
      PageManager pageManager,
      PageData pageData)
    {
      if (proxy.Title != null && pageData.Title != (Lstring) proxy.Title.PersistedValue)
        pageData.Title = (Lstring) proxy.Title.PersistedValue;
      if (proxy.SeoTitle != null && pageData.HtmlTitle != (Lstring) proxy.SeoTitle.PersistedValue)
        pageData.HtmlTitle = (Lstring) proxy.SeoTitle.PersistedValue;
      if (proxy.Keywords != null && pageData.Keywords != (Lstring) proxy.Keywords.PersistedValue)
        pageData.Keywords = (Lstring) proxy.Keywords.PersistedValue;
      if (proxy.Description != null && pageData.Description != (Lstring) proxy.Description.PersistedValue)
        pageData.Description = (Lstring) proxy.Description.PersistedValue;
      if (pageData.Crawlable != proxy.Crawlable)
        pageData.Crawlable = proxy.Crawlable;
      if (pageData.RequireSsl != proxy.RequireSsl)
        pageData.RequireSsl = proxy.RequireSsl;
      if (pageData.EnableViewState != proxy.EnableViewState)
        pageData.EnableViewState = proxy.EnableViewState;
      if (pageData.IncludeScriptManager != proxy.IncludeScriptManager)
        pageData.IncludeScriptManager = proxy.IncludeScriptManager;
      if (pageData.OutputCacheProfile != proxy.OutputCacheProfile)
        pageData.OutputCacheProfile = proxy.OutputCacheProfile;
      if (pageData.CacheOutput != proxy.CacheOutput)
        pageData.CacheOutput = proxy.CacheOutput;
      if (pageData.CacheDuration != proxy.CacheDuration)
        pageData.CacheDuration = proxy.CacheDuration;
      if (pageData.SlidingExpiration != proxy.SlidingExpiration)
        pageData.SlidingExpiration = proxy.SlidingExpiration;
      if (pageData.HeadTagContent != proxy.HeadTagContent)
        pageData.HeadTagContent = proxy.HeadTagContent;
      if (pageData.CodeBehindType != proxy.CodeBehindType)
        pageData.CodeBehindType = proxy.CodeBehindType;
      if (((IRendererCommonData) pageData).Renderer != proxy.Renderer)
        ((IRendererCommonData) pageData).Renderer = proxy.Renderer;
      if (((IRendererCommonData) pageData).TemplateName != proxy.TemplateName)
        ((IRendererCommonData) pageData).TemplateName = proxy.TemplateName;
      if (proxy.Template == null || proxy.Template.Id == Guid.Empty)
        pageData.Template = (PageTemplate) null;
      else if (pageData.Template == null || pageData.Template.Id != proxy.Template.Id)
        pageData.Template = pageManager.GetTemplate(proxy.Template.Id);
      PageDraft master = pageManager.PagesLifecycle.GetMaster(pageData);
      if (master != null && master.IncludeScriptManager != proxy.IncludeScriptManager)
        master.IncludeScriptManager = proxy.IncludeScriptManager;
      if (!pageData.IsPersonalized)
        return;
      IQueryable<PageData> pageDataList = pageManager.GetPageDataList();
      Expression<Func<PageData, bool>> predicate = (Expression<Func<PageData, bool>>) (p => p.PersonalizationMasterId == pageData.Id);
      foreach (PageData pageData1 in pageDataList.Where<PageData>(predicate).ToList<PageData>())
        proxy.CopyPagePropertiesTo(pageManager, pageData1);
    }

    /// <summary>Copies properties from a page view model to page data</summary>
    private static void CopyStandardPageProperties(
      this WcfPage proxy,
      PageManager pageManager,
      PageNode n,
      PageNode sourceNode,
      bool isMultilingual,
      bool isNew,
      bool isLanguageVersion,
      bool isLanguageVersionOfSplitPage,
      bool isBackendPage)
    {
      proxy.ApplyCanonicalUrlSettingsTo(n);
      PageData pageData = pageManager.GetPageData(n);
      proxy.CopyPagePropertiesTo(pageManager, pageData);
      if (pageManager.Provider.GetDirtyItems().Contains((object) pageData))
      {
        ++pageData.Version;
        --pageData.Version;
      }
      if (isNew)
        n.ApprovalWorkflowState = (Lstring) "Draft";
      if (!(isMultilingual & isNew) || !(!isLanguageVersion | isLanguageVersionOfSplitPage) || !(pageData.Culture != SystemManager.CurrentContext.Culture.Name))
        return;
      pageData.Culture = SystemManager.CurrentContext.Culture.Name;
    }

    internal static PageFacade CreateNewPage(
      this WcfPage pageContext,
      FluentSitefinity fluent,
      out PageNode sourceNode,
      Guid rootId)
    {
      PageFacade newPage = (PageFacade) null;
      sourceNode = (PageNode) null;
      bool multilingual = SystemManager.CurrentContext.AppSettings.Multilingual;
      bool flag = pageContext.SourceLanguagePageId != Guid.Empty;
      if (pageContext.IsGroup || pageContext.IsExternal)
      {
        if (multilingual & flag)
        {
          newPage = fluent.Page(pageContext.SourceLanguagePageId);
          sourceNode = newPage.Get();
        }
        else
        {
          PageLocation location = rootId == Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.BackendRootNodeId ? PageLocation.Backend : PageLocation.Frontend;
          newPage = fluent.Page().CreateNewPageGroup(location, pageContext.Id).Done();
        }
      }
      else
      {
        if (multilingual & flag)
        {
          pageContext.Id = Guid.Empty;
          newPage = fluent.Page(pageContext.SourceLanguagePageId);
          sourceNode = newPage.Get();
          if (sourceNode.LocalizationStrategy == LocalizationStrategy.Split && sourceNode.GetPageData(SystemManager.CurrentContext.Culture) == null)
            newPage.PageManager.CreateSystemPageDataForSplitPage(sourceNode, SystemManager.CurrentContext.Culture);
        }
        if (newPage == null)
          newPage = fluent.Page().CreateNewStandardPage(rootId, pageContext.Id).Done();
      }
      return newPage;
    }

    internal static PageFacade GetFacade(
      this WcfPage proxy,
      FluentSitefinity fluent,
      bool isLanguageVersion,
      out bool isNew,
      out PageNode sourceNode)
    {
      sourceNode = (PageNode) null;
      Guid rootId = (proxy.Parent == null ? 0 : (proxy.Parent.IsBackend ? 1 : 0)) == 0 ? proxy.RootId : SiteInitializer.BackendPagesNodeId;
      PageFacade facade;
      if (proxy.Id == Guid.Empty | isLanguageVersion)
      {
        facade = proxy.CreateNewPage(fluent, out sourceNode, rootId);
        isNew = true;
      }
      else if (fluent.Page(proxy.Id).Get() == null)
      {
        facade = proxy.CreateNewPage(fluent, out sourceNode, rootId);
        isNew = true;
      }
      else
      {
        isNew = false;
        facade = fluent.Page(proxy.Id);
      }
      return facade;
    }

    internal static void ApplyCanonicalUrlSettingsTo(this WcfPage wcfPage, PageNode node)
    {
      switch (wcfPage.EnableDefaultCanonicalUrl)
      {
        case WcfPage.CanonicalUrlSettings.Default:
          if (!node.EnableDefaultCanonicalUrl.HasValue)
            break;
          node.EnableDefaultCanonicalUrl = new bool?();
          break;
        case WcfPage.CanonicalUrlSettings.Disabled:
          bool? defaultCanonicalUrl1 = node.EnableDefaultCanonicalUrl;
          bool flag1 = false;
          if (defaultCanonicalUrl1.GetValueOrDefault() == flag1 & defaultCanonicalUrl1.HasValue)
            break;
          node.EnableDefaultCanonicalUrl = new bool?(false);
          break;
        case WcfPage.CanonicalUrlSettings.Enabled:
          bool? defaultCanonicalUrl2 = node.EnableDefaultCanonicalUrl;
          bool flag2 = true;
          if (defaultCanonicalUrl2.GetValueOrDefault() == flag2 & defaultCanonicalUrl2.HasValue)
            break;
          node.EnableDefaultCanonicalUrl = new bool?(true);
          break;
      }
    }

    internal static bool HasCustomUrl(this WcfPage wcfPage) => wcfPage.UrlName.StartsWith("~/");
  }
}
