// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PageHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages.Web.Services.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Configuration;
using Telerik.Sitefinity.Taxonomies.Extensions;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.Services.Extensibility;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>
  /// Page Helper to provide various functionality for pages.
  /// </summary>
  public static class PageHelper
  {
    /// <summary>Predefined meta property mappings.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static readonly Dictionary<string, string> MetaPropertyMappings = new Dictionary<string, string>()
    {
      {
        "OpenGraphTitle",
        "og:title"
      },
      {
        "OpenGraphDescription",
        "og:description"
      },
      {
        "OpenGraphType",
        "og:type"
      },
      {
        "OpenGraphImage",
        "og:image"
      },
      {
        "OpenGraphVideo",
        "og:video"
      },
      {
        "Url",
        "og:url"
      },
      {
        "SiteName",
        "og:site_name"
      },
      {
        "MetaDescription",
        "description"
      }
    };

    /// <summary>Adds SEO-related components to page.</summary>
    /// <param name="page">The page entity.</param>
    /// <param name="data">Data of the page site node.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void AddSeoAndOpenGraphControls(Page page, PageSiteNode data)
    {
      SeoAndOpenGraphElement andOpenGraphConfig = Config.Get<SystemConfig>().SeoAndOpenGraphConfig;
      bool enabledSeo = andOpenGraphConfig.EnabledSEO;
      bool enabledOpenGraph = andOpenGraphConfig.EnabledOpenGraph;
      if (!enabledSeo && !enabledOpenGraph)
        return;
      if (enabledSeo)
        PageHelper.TryAddMetaControlToPage(page, data, new string[2]
        {
          "MetaDescription",
          "Description"
        });
      if (!enabledOpenGraph)
        return;
      PageHelper.TryAddMetaControlToPage(page, data, new string[3]
      {
        "OpenGraphTitle",
        "HtmlTitle",
        "Title"
      }, "property");
      PageHelper.TryAddMetaControlToPage(page, data, new string[3]
      {
        "OpenGraphDescription",
        "MetaDescription",
        "Description"
      }, "property");
      PageHelper.TryAddMetaControlToPage(page, data, new string[1]
      {
        "OpenGraphImage"
      }, "property");
      PageHelper.TryAddMetaControlToPage(page, data, new string[1]
      {
        "OpenGraphVideo"
      }, "property");
      PageHelper.TryAddMetaControlToPage(page, PageHelper.MetaPropertyMappings["Url"], page.GetCanonicalUrlForPage(data), "property");
      PageHelper.TryAddMetaControlToPage(page, PageHelper.MetaPropertyMappings["OpenGraphType"], "website", "property");
      PageHelper.TryAddMetaControlToPage(page, PageHelper.MetaPropertyMappings["SiteName"], SystemManager.CurrentContext.CurrentSite.Name, "property");
    }

    /// <summary>Tries to add a meta control to a specific page.</summary>
    /// <param name="page">The page instance.</param>
    /// <param name="metaPropertyType">Name of the type of the meta property.</param>
    /// <param name="propertyValue">Value of the property.</param>
    /// <param name="propertyName">Name of the property.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void TryAddMetaControlToPage(
      Page page,
      string metaPropertyType,
      string propertyValue,
      string propertyName = "name")
    {
      LiteralControl controlToAdd = new LiteralControl(PageHelper.SetPageOpenGraphProperty(metaPropertyType, propertyValue, propertyName));
      IEnumerable<LiteralControl> source = page.Header.Controls.OfType<LiteralControl>();
      if (metaPropertyType == PageHelper.MetaPropertyMappings["MetaDescription"])
      {
        if (!(page.Header.Description != propertyValue))
          return;
        page.Header.Description = propertyValue;
      }
      else
      {
        if (source.Any<LiteralControl>((Func<LiteralControl, bool>) (control => control.Text == controlToAdd.Text)))
          return;
        foreach (LiteralControl literalControl in source)
        {
          if (literalControl.Text.Contains("\"" + metaPropertyType + "\""))
            literalControl.Text = Regex.Replace(literalControl.Text, PageHelper.SetPageOpenGraphProperty(metaPropertyType, ".*?", propertyName), "");
        }
        page.Header.Controls.Add((Control) controlToAdd);
      }
    }

    /// <summary>Removes meta control from page.</summary>
    /// <param name="page">The page instance.</param>
    /// <param name="metaPropertyType">Meta property type name.</param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static void RemoveMetaControlFromPage(Page page, string metaPropertyType)
    {
      LiteralControl literalControl = page.Header.Controls.OfType<LiteralControl>().FirstOrDefault<LiteralControl>((Func<LiteralControl, bool>) (c => c.Text.Contains("\"" + metaPropertyType + "\"")));
      if (literalControl == null)
        return;
      page.Header.Controls.Remove((Control) literalControl);
    }

    /// <summary>Sets open graph meta property to a page.</summary>
    /// <param name="propertyType">Property type name.</param>
    /// <param name="propertyContent">Property content.</param>
    /// <param name="property">Name of the property.</param>
    /// <returns></returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string SetPageOpenGraphProperty(
      string propertyType,
      string propertyContent,
      string property = "name")
    {
      string str = HttpUtility.HtmlEncode(propertyContent);
      return string.Format((IFormatProvider) CultureInfo.InvariantCulture, "<meta {0}=\"{1}\" content=\"{2}\" />", (object) property, (object) propertyType, (object) str);
    }

    /// <summary>Gets the value of a field.</summary>
    /// <param name="item">Content item instance.</param>
    /// <param name="propertyNames">Array of properties names.</param>
    /// <param name="returnFirstProperty">Whether the method should return the first property.</param>
    /// <returns>Returns the first property unless last parameter of the mothod is set to false.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string GetFieldValue(
      object item,
      string[] propertyNames,
      bool returnFirstProperty = true)
    {
      return PageHelper.GetFieldValue(item, propertyNames, returnFirstProperty, (object) null);
    }

    [EditorBrowsable(EditorBrowsableState.Never)]
    internal static string GetFieldValue(
      object item,
      string[] propertyNames,
      bool returnFirstProperty,
      object fallbackItem)
    {
      int num = 0;
      foreach (string propertyName in propertyNames)
      {
        if (!string.IsNullOrEmpty(propertyName))
        {
          string metaValue = PageHelper.GetMetaValue(item, propertyName);
          if (num == 0 & returnFirstProperty)
            return metaValue;
          if (propertyName == "HtmlTitle" && metaValue == null && fallbackItem != null)
            metaValue = PageHelper.GetMetaValue(fallbackItem, propertyName);
          if (!string.IsNullOrEmpty(metaValue))
            return metaValue;
        }
        ++num;
      }
      return (string) null;
    }

    /// <summary>Gets meta falue for a field of a given item.</summary>
    /// <param name="detailItem">The item.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>The meta value of the field.</returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static string GetMetaValue(object detailItem, string fieldName)
    {
      PropertyDescriptor property = TypeDescriptor.GetProperties(detailItem)[fieldName];
      if (property == null)
        return (string) null;
      if (property is TaxonomyPropertyDescriptor descriptor)
        return PageHelper.GetTextFromTaxa(detailItem, descriptor);
      if (property is PageSiteNidePropertyDescriptor propertyDescriptor)
      {
        string attribute = (propertyDescriptor.Attributes[typeof (MetaFieldAttributeAttribute)] as MetaFieldAttributeAttribute).Attributes["UserFriendlyDataType"];
        if (attribute != null && attribute == UserFriendlyDataType.RelatedMedia.ToString())
          return PageHelper.GetRelatedMediaUrl((IDataItem) property.GetValue(detailItem));
      }
      if (property is RelatedDataPropertyDescriptor)
        return PageHelper.GetRelatedMediaUrl(detailItem.GetRelatedItems(fieldName).FirstOrDefault<IDataItem>());
      object obj = property.GetValue(detailItem);
      return obj == null || obj.ToString().IsNullOrEmpty() ? (string) null : obj.ToString();
    }

    /// <summary>Orders a control collection.</summary>
    /// <param name="controlNodes">List of control nodes.</param>
    /// <returns>The ordered list of control nodes.</returns>
    public static List<ControlData> GetOrderedControlsCollection(
      List<ControlNode> controlNodes)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetOrderedControlsCollection(controlNodes);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="controlContainersOrdered"></param>
    /// <param name="staticPlaceholders"></param>
    /// <returns></returns>
    public static List<ControlNode> GetControlsHierarchical(
      IEnumerable<IControlsContainer> controlContainersOrdered,
      List<string> staticPlaceholders)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetControlsHierarchical(controlContainersOrdered, staticPlaceholders);
    }

    /// <summary>Gets a hirarchical list of provided controls list.</summary>
    /// <param name="controls">The controls list.</param>
    /// <param name="containerIdsOrdered">Ordered list of container ids.</param>
    /// <param name="staticPlaceholders">Static placeholders.</param>
    /// <returns>The hierarchical list for the provided controls.</returns>
    public static List<ControlNode> GetControlsHierarchical(
      List<ControlData> controls,
      List<Guid> containerIdsOrdered,
      List<string> staticPlaceholders)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetControlsHierarchical(controls, containerIdsOrdered, staticPlaceholders);
    }

    /// <summary>Sorts a controls tree.</summary>
    /// <param name="node">Root control node.</param>
    /// <param name="containerIdsOrdered">Ordered list of container ids.</param>
    public static void SortControlsTree(ControlNode node, List<Guid> containerIdsOrdered) => ObjectFactory.Resolve<PageHelperImplementation>().SortControlsTree(node, containerIdsOrdered);

    /// <summary>
    /// This method returns the index at which the given control should be inserted in its placeholder, when the control's
    /// sibling is Guid.Empty. This is normally 0 (e.g. as first control in the placeholder), but not always. If you have
    /// template inheritance, it is possible that more than one control has the same placeholder and Guid.Empty as SiblingId.
    /// In those cases, we must calculate the index based on that.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <param name="insertedEmptySiblingControls">Controls to insert.</param>
    /// <param name="containerIdsOrdered">The ordered container IDs.</param>
    /// <returns>The insert index/</returns>
    public static int GetInsertIndexOfEmptySiblingControl(
      ControlData control,
      Dictionary<string, List<ControlData>> insertedEmptySiblingControls,
      List<Guid> containerIdsOrdered)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetInsertIndexOfEmptySiblingControl(control, insertedEmptySiblingControls, containerIdsOrdered);
    }

    /// <summary>
    /// Gets the theme of the page in all languages using fallback to templates.
    /// </summary>
    /// <param name="pageData">The page to get theme for.</param>
    /// <returns>The theme.</returns>
    public static Dictionary<CultureInfo, string> GetTheme(PageData pageData) => ObjectFactory.Resolve<PageHelperImplementation>().GetTheme(pageData);

    /// <summary>
    /// Gets the theme of the page draft in all languages using fallback to templates.
    /// </summary>
    /// <param name="pageData">The page to get theme for.</param>
    /// <returns>the theme</returns>
    public static Dictionary<CultureInfo, string> GetTheme(
      PageDraft pageDraft,
      PageTemplate baseTemplate)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetTheme(pageDraft, baseTemplate);
    }

    /// <summary>
    /// Gets the theme of the template draft in all languages using fallback to templates.
    /// </summary>
    /// <param name="templateDraft">The template to get theme for.</param>
    /// <returns>The theme</returns>
    public static Dictionary<CultureInfo, string> GetTheme(
      TemplateDraft templateDraft,
      PageTemplate baseTemplate)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetTheme(templateDraft, baseTemplate);
    }

    /// <summary>Gets the theme for a given page template.</summary>
    /// <param name="baseTemplate">The base page template/</param>
    /// <param name="theme">Theme name.</param>
    /// <param name="availableLanguages">The available languages.</param>
    /// <returns>Dictionary of theme name in different cultures.</returns>
    public static Dictionary<CultureInfo, string> GetTheme(
      PageTemplate baseTemplate,
      Lstring theme,
      IEnumerable<CultureInfo> availableLanguages)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetTheme(baseTemplate, theme, availableLanguages);
    }

    /// <summary>Gets theme name for a given culture.</summary>
    /// <param name="themes">Dictionary of theme names per culure.</param>
    /// <param name="language">Desired language.</param>
    /// <returns>The theme name in the desired culture.</returns>
    public static string GetThemeName(IDictionary<CultureInfo, string> themes, CultureInfo language = null) => ObjectFactory.Resolve<PageHelperImplementation>().GetThemeName(themes, language);

    /// <summary>
    /// Gets the page node for the given language. If the node is in SYNCED mode, the same node is returned.
    /// If the node is in SPLIT mode, the linked pages are searched for a page in the given language. If such
    /// page is not found, null is returned. If such page is found, a random(the first) of its nodes is returned.
    /// </summary>
    /// <param name="node">The source node.</param>
    /// <param name="language">The language.</param>
    /// <returns>The page node.</returns>
    [Obsolete("The page node is the same for all langages. There is no language version anymore.")]
    public static PageNode GetPageNodeForLanguage(PageNode node, CultureInfo language) => ObjectFactory.Resolve<PageHelperImplementation>().GetPageNodeForLanguage(node, language);

    [Obsolete("The page node is the same for all langages. There is no language version anymore.")]
    public static PageNode GetPageNodeForLanguage(PageData page, CultureInfo language) => ObjectFactory.Resolve<PageHelperImplementation>().GetPageNodeForLanguage(page, language);

    /// <summary>
    /// Returns a value indicating whether the given page node has a translation in the given language.
    /// </summary>
    /// <param name="node">The node to be checked.</param>
    /// <param name="language">The culture to be looked for.</param>
    /// <returns>If the page node has translation.</returns>
    public static bool PageNodeHasTranslation(PageNode node, CultureInfo language) => ObjectFactory.Resolve<PageHelperImplementation>().PageNodeHasTranslation(node, language);

    /// <summary>
    /// Determines whether the specified node is a node of a backend page.
    /// </summary>
    /// <param name="node">The node to check.</param>
    /// <returns>
    /// 	<c>true</c> if the specified node is a node of a backend page; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPageNodeBackend(PageNode node) => ObjectFactory.Resolve<PageHelperImplementation>().IsPageNodeBackend(node);

    /// <summary>
    /// Determines whether the specified page node is relevant for the specified language.
    /// A page is considered relevant if it is in SYNCED mode or if its UiCulture is equal
    /// to that language.
    /// </summary>
    /// <param name="pageNode">The page node.</param>
    /// <param name="language">The language.</param>
    /// <returns>
    /// 	<c>true</c> if the specified page node is relevant for the specified language; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPageNodeForLanguage(PageNode pageNode, CultureInfo language) => ObjectFactory.Resolve<PageHelperImplementation>().IsPageNodeForLanguage(pageNode, language);

    /// <summary>Filters the pages for language.</summary>
    /// <param name="pageNodes">The page nodes.</param>
    /// <param name="language">The language.</param>
    /// <returns></returns>
    [Obsolete("Page nodes are not filtered for lanugage anymore.")]
    public static IList<PageNode> FilterPagesForLanguage(
      IQueryable<PageNode> pageNodes,
      CultureInfo language)
    {
      return (IList<PageNode>) pageNodes.ToList<PageNode>();
    }

    internal static void AddPageTemplates(
      IEnumerable list,
      Dictionary<string, ITemplate> pageTmps,
      IDictionary<string, ITemplate> layoutTmps)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().AddPageTemplates(list, pageTmps, layoutTmps);
    }

    internal static void ApplyLayouts(
      IList<Dictionary<string, ITemplate>> layouts,
      Page page,
      string theme)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().ApplyLayouts(layouts, page, theme);
    }

    internal static void CreateChildControls(
      IList<ControlBuilder> controls,
      Page page,
      bool ignoreCultures,
      bool addControlIdScript = false)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().CreateChildControls(controls, page, ignoreCultures, addControlIdScript);
    }

    internal static bool IsAccessibleToUser(HttpContextBase context, ControlBuilder builder) => ObjectFactory.Resolve<PageHelperImplementation>().IsAccessibleToUser(context, builder);

    internal static ITemplate GetPageTemplate(
      IDictionary<string, ITemplate> pageTemplates,
      string theme)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetPageTemplate(pageTemplates, theme);
    }

    internal static void ProcessPresentationData(
      IEnumerable<PresentationData> presentation,
      IList<Dictionary<string, ITemplate>> layoutTmps,
      Dictionary<string, ITemplate> pageTmps)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().ProcessPresentationData(presentation, layoutTmps, pageTmps);
    }

    internal static void ProcessTemplates(
      IPageTemplate template,
      List<Dictionary<string, ITemplate>> layoutTmps,
      Dictionary<string, ITemplate> pageTmps,
      List<IControlsContainer> controlConainers,
      bool optimized,
      out string masterPage,
      out bool includeScriptManager)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().ProcessTemplates(template, layoutTmps, pageTmps, controlConainers, optimized, out masterPage, out includeScriptManager);
    }

    internal static void ProcessTemplates(
      IPageTemplate template,
      List<Dictionary<string, ITemplate>> layoutTmps,
      Dictionary<string, ITemplate> pageTmps,
      List<IControlsContainer> controlConainers,
      out string masterPage,
      out bool includeScriptManager)
    {
      PageHelper.ProcessTemplates(template, layoutTmps, pageTmps, controlConainers, false, out masterPage, out includeScriptManager);
    }

    internal static string ResolveDynamicMasterPage(IPageTemplate template) => ObjectFactory.Resolve<PageHelperImplementation>().ResolveDynamicMasterPage(template);

    internal static void ProcessControls(
      IList<ControlData> controls,
      IList<ControlData> placeholders,
      IList<IControlsContainer> controlContainers)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().ProcessControls(controls, placeholders, controlContainers);
    }

    internal static void ProcessControls(
      IList<ControlBuilder> builders,
      IList<IControlsContainer> controlContainers,
      Guid pageDataId = default (Guid))
    {
      ObjectFactory.Resolve<PageHelperImplementation>().ProcessControls(builders, controlContainers, pageDataId);
    }

    internal static List<ControlData> SortControls(
      IEnumerable<IControlsContainer> controlContainers,
      int count)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().SortControls(controlContainers, count);
    }

    internal static void ProcessOverridenControls(
      PageData page,
      IEnumerable<IControlsContainer> controlContainersOrdered)
    {
      List<Guid> guidList = new List<Guid>();
      guidList.Add(page.Id);
      for (PageTemplate pageTemplate = page.Template; pageTemplate != null; pageTemplate = pageTemplate.ParentTemplate)
        guidList.Add(page.Template.Id);
      guidList.Reverse();
      List<ControlData> source = new List<ControlData>();
      foreach (IControlsContainer controlsContainer in controlContainersOrdered)
        source.AddRange(controlsContainer.Controls);
      foreach (IGrouping<Guid, ControlData> grouping in source.Where<ControlData>((Func<ControlData, bool>) (control => control.PlaceHolder == null && control.IsOverridedControl)).GroupBy<ControlData, Guid>((Func<ControlData, Guid>) (c => c.BaseControlId)))
      {
        IGrouping<Guid, ControlData> groupControls = grouping;
        ControlData overridenData = source.Where<ControlData>((Func<ControlData, bool>) (control => control.Id == groupControls.Key)).FirstOrDefault<ControlData>();
        foreach (Guid guid in guidList)
        {
          Guid id = guid;
          ControlData baseData = groupControls.Where<ControlData>((Func<ControlData, bool>) (controlData => (controlData is PageControl ? ((PageControl) controlData).Page.Id : ((Telerik.Sitefinity.Pages.Model.TemplateControl) controlData).Page.Id) == id)).FirstOrDefault<ControlData>();
          if (baseData != null && overridenData != null)
            overridenData.OverrideFrom(baseData);
        }
      }
    }

    internal static void ClearOverridenControls(List<ControlData> controls)
    {
      if (controls == null)
        return;
      foreach (ControlData control in controls)
        control.ClearOverrides();
    }

    /// <summary>Tries to get the full URL path from the cache</summary>
    /// <returns>If the full URL is retrieved from cache.</returns>
    internal static bool TryGetFullUrlFromCache(Guid pageId, string key, out string fullPath) => ObjectFactory.Resolve<PageHelperImplementation>().TryGetFullUrlFromCache(pageId, key, out fullPath);

    /// <summary>Gets a dictionary of child routes.</summary>
    internal static RouteInfoCollection ChildRoutes => ObjectFactory.Resolve<PageHelperImplementation>().ChildRoutes;

    /// <summary>Initializes a partial route handler.</summary>
    /// <param name="obj">The instance of the partial route handler to initialize.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    internal static void SetPartialRouteHandler(
      object obj,
      RequestContext requestContext,
      string routeKey)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().SetPartialRouteHandler(obj, requestContext, routeKey);
    }

    /// <summary>Sets the partial route handler.</summary>
    /// <param name="partHandler">The partial route handler.</param>
    /// <param name="routes">The routes.</param>
    /// <param name="requestContext">The request context.</param>
    /// <param name="routeKey">The route key.</param>
    internal static void SetPartialRouteHandler(
      IPartialRouteHandler partHandler,
      RouteCollection routes,
      RequestContext requestContext,
      string routeKey)
    {
      ObjectFactory.Resolve<PageHelperImplementation>().SetPartialRouteHandler(partHandler, routes, requestContext, routeKey);
    }

    internal static string GetPageEditUrl(string pageUrl) => ObjectFactory.Resolve<PageHelperImplementation>().GetPageEditUrl(pageUrl);

    internal static void DeleteLinkingPageNodes(PageNode linkedNode, PageManager manager) => ObjectFactory.Resolve<PageHelperImplementation>().DeleteLinkingPageNodes(linkedNode, manager);

    /// <summary>
    /// Deletes the page nodes tree with the specified root page node.
    /// </summary>
    /// <param name="rootNode">The root node.</param>
    /// <param name="manager">The manager.</param>
    internal static void DeletePageNodesTree(PageNode pageNode, PageManager manager) => ObjectFactory.Resolve<PageHelperImplementation>().DeletePageNodesTree(pageNode, manager);

    internal static void DeletePersonalizedPages(PageNode pageNode, PageManager manager) => ObjectFactory.Resolve<PageHelperImplementation>().DeletePersonalizedPages(pageNode, manager);

    internal static PlaceHoldersCollection CreateHandlerPlaceHolders(
      Page handler)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().CreateHandlerPlaceHolders(handler);
    }

    internal static List<PageTemplate> GetTemplates(PageData pageData) => ObjectFactory.Resolve<PageHelperImplementation>().GetTemplates(pageData);

    internal static string GetCurrentNodeExtension() => SiteMapBase.GetActualCurrentNode()?.Extension ?? string.Empty;

    /// <summary>
    /// Gets the ids of the segments used for personalization of any widget on the page.
    /// </summary>
    /// <param name="pageDataId">The page data ID.</param>
    /// <param name="templateIds">The template ids.</param>
    /// <param name="pageManager">The page manager.</param>
    /// <returns>
    /// A lookup containing all segments used on the page grouped by control ID
    /// </returns>
    internal static ILookup<Guid, Guid> GetWidgetSegmentIds(
      Guid pageDataId,
      IList<Guid> templateIds,
      PageManager pageManager = null)
    {
      return ObjectFactory.Resolve<PageHelperImplementation>().GetWidgetSegmentIds(pageDataId, templateIds, pageManager);
    }

    internal static MarketingPropertyValue MapPageViewModelToMarketingProperty(
      PageViewModel viewModel)
    {
      if (viewModel.PageVariationViewModels == null)
        return (MarketingPropertyValue) null;
      KeyValuePair<string, PageVariationViewModel> keyValuePair = viewModel.PageVariationViewModels.FirstOrDefault<KeyValuePair<string, PageVariationViewModel>>();
      if (keyValuePair.Value == null)
        return (MarketingPropertyValue) null;
      return new MarketingPropertyValue()
      {
        Description = keyValuePair.Value.Description,
        Link = keyValuePair.Value.Link,
        LinkTitle = keyValuePair.Value.LinkTitle
      };
    }

    private static string GetTextFromTaxa(object item, TaxonomyPropertyDescriptor descriptor)
    {
      string taxaText = descriptor.GetTaxaText(item);
      return !string.IsNullOrEmpty(taxaText) ? taxaText : (string) null;
    }

    private static string GetRelatedMediaUrl(IDataItem relatedItem)
    {
      if (relatedItem != null && relatedItem is MediaContent mediaContent)
      {
        string mediaUrl = mediaContent.MediaUrl;
        if (mediaUrl != null || !string.IsNullOrEmpty(mediaUrl))
          return mediaUrl;
      }
      return (string) null;
    }

    private static void TryAddMetaControlToPage(
      Page page,
      PageSiteNode data,
      string[] propertyNames,
      string property = "name",
      bool returnFirstProperty = false)
    {
      string fieldValue = PageHelper.GetFieldValue((object) data, propertyNames, returnFirstProperty, (object) data.CurrentPageDataItem);
      if (string.IsNullOrEmpty(fieldValue))
        return;
      PageHelper.TryAddMetaControlToPage(page, PageHelper.MetaPropertyMappings[((IEnumerable<string>) propertyNames).First<string>()], fieldValue, property);
    }

    /// <summary>Predefined metadata properties names.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class MetaDataProperties
    {
      public const string MetaTitle = "MetaTitle";
      public const string MetaDescription = "MetaDescription";
      public const string OpenGraphTitle = "OpenGraphTitle";
      public const string OpenGraphDescription = "OpenGraphDescription";
      public const string OpenGraphImage = "OpenGraphImage";
      public const string OpenGraphVideo = "OpenGraphVideo";
      public const string Title = "Title";
      public const string Description = "Description";
      public const string OpenGraphType = "OpenGraphType";
      public const string Url = "Url";
      public const string SiteName = "SiteName";
      public const string HtmlTitle = "HtmlTitle";
    }

    /// <summary>Predefined OpenGraph types.</summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    public struct OpenGraphTypes
    {
      public const string Website = "website";
      public const string Article = "article";
      public const string Video = "video";
      public const string Image = "image";
    }
  }
}
