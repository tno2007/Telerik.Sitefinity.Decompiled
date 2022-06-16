// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Extensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData.Web.UI;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.OutputCache;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.NavigationControls;

namespace Telerik.Sitefinity.Web
{
  /// <summary>
  /// Extension methods for the web area of the Sitefinity (controls, pages, requests...)
  /// </summary>
  public static class Extensions
  {
    private const string BreadcrumbExtenderKey = "sf_breadcrumb_extender";

    /// <summary>
    /// Extension method which extends the control so that each control can return the RequestContext
    /// object used by Routing Engine.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static RequestContext GetRequestContext(this Control control) => control.Page != null && control.Page.Request != null ? control.Page.Request.RequestContext : (RequestContext) null;

    /// <summary>Gets the place holders in this page.</summary>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    public static PlaceHoldersCollection GetPlaceHolders(this Page page)
    {
      if (page is ISitefinityPage sitefinityPage)
        return sitefinityPage.PlaceHolders;
      return SystemManager.CurrentHttpContext != null ? (PlaceHoldersCollection) SystemManager.CurrentHttpContext.Items[(object) "sf_PlaceHolders"] : (PlaceHoldersCollection) null;
    }

    /// <summary>Sets the place holders.</summary>
    /// <param name="page">The page.</param>
    /// <param name="placeHolders">The place holders.</param>
    public static void SetPlaceHolders(this Page page, PlaceHoldersCollection placeHolders)
    {
      if (page is ISitefinityPage sitefinityPage)
        sitefinityPage.PlaceHolders = placeHolders;
      if (SystemManager.CurrentHttpContext == null)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "sf_PlaceHolders"] = (object) placeHolders;
    }

    /// <summary>
    /// Gets the nearest <see cref="T:System.Web.UI.IDataItemContainer" /> up the control hierarchy, <c>null</c> otherwise.
    /// </summary>
    /// <param name="control">The control to start the search from.</param>
    /// <returns>The data item container.</returns>
    public static IDataItemContainer GetDataItemContainer(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      IDataItemContainer dataItemContainer;
      for (dataItemContainer = (IDataItemContainer) null; dataItemContainer == null && control != null; control = control.Parent)
        dataItemContainer = control as IDataItemContainer;
      return dataItemContainer;
    }

    /// <summary>
    /// Gets the data item of the nearest <see cref="T:System.Web.UI.IDataItemContainer" /> up the control hierarchy, <c>null</c> otherwise.
    /// </summary>
    /// <param name="control">The control to start the search from.</param>
    /// <returns>The data item.</returns>
    internal static object GetDataItem(this Control control) => control.GetDataItemContainer()?.DataItem;

    /// <summary>
    /// Gets the data item of the nearest data item container up the control hierarchy, <c>null</c> otherwise.
    /// </summary>
    /// <param name="control">The related data view to start the search from.</param>
    /// <returns>The data item.</returns>
    internal static object GetRelatedDataItem(this IRelatedDataView relatedDataView)
    {
      if (relatedDataView == null)
        throw new ArgumentNullException("control");
      if (!(relatedDataView is Control control))
        throw new InvalidOperationException("RelatedDataView is not a control");
      object relatedDataItem = (object) null;
      while (relatedDataItem == null)
      {
        switch (control)
        {
          case null:
            goto label_10;
          case IDataItemContainer _:
            relatedDataItem = ((IDataItemContainer) control).DataItem;
            goto label_10;
          case NavigationNodeContainer _:
            relatedDataItem = ((NavigationNodeContainer) control).DataItem;
            goto label_10;
          default:
            control = control.Parent;
            continue;
        }
      }
label_10:
      return relatedDataItem;
    }

    /// <summary>
    /// Returns the first control in the parent chain that matches the specified type or null if there is no match.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static THost GetHostControl<THost>(this Control control) where THost : Control
    {
      for (; control != null; control = control.Parent)
      {
        if (control is THost hostControl)
          return hostControl;
      }
      return default (THost);
    }

    public static THost GetHostControlLoose<THost>(this Control control) where THost : class
    {
      for (; control != null; control = control.Parent)
      {
        if (control is THost)
          return control as THost;
      }
      return default (THost);
    }

    /// <summary>Gets the presentation data for image.</summary>
    /// <param name="presentable">The presentable.</param>
    /// <param name="name">The name.</param>
    /// <returns></returns>
    public static string GetImage(this IPresentable presentable, string name) => presentable.GetImage(name, (string) null);

    /// <summary>Gets the presentation data for image.</summary>
    /// <param name="presentable">The presentable.</param>
    /// <param name="name">The name.</param>
    /// <param name="theme">The theme.</param>
    /// <returns></returns>
    public static string GetImage(this IPresentable presentable, string name, string theme) => presentable.GetPresentationData("IMAGE_URL", name, theme);

    /// <summary>
    /// Gets the presentation data for the specified type and name for the currently configured theme.
    /// </summary>
    /// <param name="presentable">The presentable item.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="name">The name of the data.</param>
    /// <returns></returns>
    public static string GetPresentationData(
      this IPresentable presentable,
      string dataType,
      string name)
    {
      return presentable.GetPresentationData(dataType, name, (string) null);
    }

    /// <summary>
    /// Gets the presentation data for the specified type, name and theme.
    /// If theme parameter is null or empty string the currently configured theme will be used and if the
    /// presentable item doesn't contain data for the current theme the method will search for a default one.
    /// The method returns empty string if no match is found.
    /// </summary>
    /// <param name="presentable">The presentable.</param>
    /// <param name="dataType">Type of the data.</param>
    /// <param name="name">The name.</param>
    /// <param name="theme">The theme.</param>
    /// <returns></returns>
    public static string GetPresentationData(
      this IPresentable presentable,
      string dataType,
      string name,
      string theme)
    {
      if (presentable.Presentation != null)
      {
        IEnumerable<PresentationData> presentationDatas = presentable.Presentation.Where<PresentationData>((Func<PresentationData, bool>) (p => p.DataType == dataType && p.Name == name));
        string str = string.IsNullOrEmpty(theme) ? Config.Get<AppearanceConfig>().BackendTheme : theme;
        foreach (PresentationData presentationData in presentationDatas)
        {
          if (str.Equals(presentationData.Theme, StringComparison.OrdinalIgnoreCase))
            return presentationData.Data;
        }
        if (string.IsNullOrEmpty(theme))
        {
          foreach (PresentationData presentationData in presentationDatas)
          {
            if (string.IsNullOrEmpty(presentationData.Theme) || presentationData.Theme.Equals("Default", StringComparison.OrdinalIgnoreCase))
              return presentationData.Data;
          }
        }
      }
      return string.Empty;
    }

    public static string GetFileNameFromResourceName(string resourceName, bool hasExtension = true)
    {
      if (string.IsNullOrEmpty(resourceName))
        return (string) null;
      int num1 = hasExtension ? resourceName.LastIndexOf('.') : resourceName.Length;
      if (num1 == -1)
        return resourceName;
      int num2 = resourceName.LastIndexOf('.', num1 - 1);
      return num2 == -1 ? resourceName : resourceName.Substring(num2 + 1);
    }

    public static ScriptReference GetScriptReference(
      this IScriptControl scriptCotnrol,
      string resourceName,
      string assemblyName)
    {
      return new ScriptReference(resourceName, assemblyName);
    }

    /// <summary>Registers the breadcrumb extender.</summary>
    /// <param name="page">The page.</param>
    /// <param name="extender">The extender.</param>
    public static void RegisterBreadcrumbExtender(this Page page, IBreadcrumExtender extender) => page.Items[(object) "sf_breadcrumb_extender"] = (object) extender;

    /// <summary>Gets the breadcrumb extender.</summary>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    public static IBreadcrumExtender GetBreadcrumbExtender(this Page page) => page.Items[(object) "sf_breadcrumb_extender"] as IBreadcrumExtender;

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string QueryStringGet(this System.Web.HttpRequest request, string parameterName) => request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName);

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator used for validation of the parameter value.</typeparam>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string QueryStringGet<TValidator>(this System.Web.HttpRequest request, string parameterName) where TValidator : CacheVariationParamValidator, new() => request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName, (Func<CacheVariationParamValidator>) (() => (CacheVariationParamValidator) new TValidator()));

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    /// <param name="getValidator">A delegate returning the validator instance used for validation of the parameter value.</param>
    public static string QueryStringGet(
      this System.Web.HttpRequest request,
      string parameterName,
      Func<CacheVariationParamValidator> getValidator)
    {
      return request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName, getValidator);
    }

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string QueryStringGet(this HttpRequestBase request, string parameterName) => request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName);

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator used for validation of the parameter value.</typeparam>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string QueryStringGet<TValidator>(
      this HttpRequestBase request,
      string parameterName)
      where TValidator : CacheVariationParamValidator, new()
    {
      return request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName, (Func<CacheVariationParamValidator>) (() => (CacheVariationParamValidator) new TValidator()));
    }

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    /// <param name="getValidator">A delegate returning the validator instance used for validation of the parameter value.</param>
    public static string QueryStringGet(
      this HttpRequestBase request,
      string parameterName,
      Func<CacheVariationParamValidator> getValidator)
    {
      return request.QueryString.ParamGet(CacheVariationParameterSource.QueryString, parameterName, getValidator);
    }

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string ParamsGet(this System.Web.HttpRequest request, string parameterName) => request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName);

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator used for validation of the parameter value.</typeparam>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string ParamsGet<TValidator>(this System.Web.HttpRequest request, string parameterName) where TValidator : CacheVariationParamValidator, new() => request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName, (Func<CacheVariationParamValidator>) (() => (CacheVariationParamValidator) new TValidator()));

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    /// <param name="getValidator">A delegate returning the validator instance used for validation of the parameter value.</param>
    public static string ParamsGet(
      this System.Web.HttpRequest request,
      string parameterName,
      Func<CacheVariationParamValidator> getValidator)
    {
      return request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName, getValidator);
    }

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string ParamsGet(this HttpRequestBase request, string parameterName) => request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName);

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <typeparam name="TValidator">The type of the validator used for validation of the parameter value.</typeparam>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    public static string ParamsGet<TValidator>(this HttpRequestBase request, string parameterName) where TValidator : CacheVariationParamValidator, new() => request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName, (Func<CacheVariationParamValidator>) (() => (CacheVariationParamValidator) new TValidator()));

    /// <summary>
    /// Gets the query string value and registers output cache variation for the query string parameter.
    /// </summary>
    /// <param name="request">The http request.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    /// <param name="getValidator">A delegate returning the validator instance used for validation of the parameter value.</param>
    public static string ParamsGet(
      this HttpRequestBase request,
      string parameterName,
      Func<CacheVariationParamValidator> getValidator)
    {
      return request.Params.ParamGet(CacheVariationParameterSource.Params, parameterName, getValidator);
    }

    /// <summary>
    /// Registers the custom output cache variation for current page output cache.
    /// </summary>
    /// <param name="page">The page.</param>
    /// <param name="cacheVariation">The cache variation.</param>
    public static void RegisterCustomOutputCacheVariation(
      this Page page,
      ICustomOutputCacheVariation cacheVariation)
    {
      PageRouteHandler.RegisterCustomOutputCacheVariation(cacheVariation);
    }

    /// <summary>Gets the page template related image.</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <returns></returns>
    public static Image GetRelatedImage(this PageTemplate pageTemplate) => PageTemplateHelper.GetRelatedImage(pageTemplate) ?? PageTemplateHelper.RelateDefaultThumbnailImage(pageTemplate);

    /// <summary>Gets the page template big thumbnail URL.</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="relatedImage">The related image.</param>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    public static string GetBigThumbnailUrl(
      this PageTemplate pageTemplate,
      Image relatedImage,
      Page page = null)
    {
      if (relatedImage == null)
        relatedImage = pageTemplate.GetRelatedImage();
      if (relatedImage != null)
        return Extensions.GetImageThumbnailUrl(relatedImage, 260, 240);
      string image = pageTemplate.GetImage("icon");
      string path = !string.IsNullOrEmpty(image) ? image : "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif";
      return page == null ? ControlUtilities.ResolveResourceUrlWithoutPage(path) : ControlUtilities.ResolveResourceUrl(path, page);
    }

    /// <summary>Gets the page template small thumbnail URL.</summary>
    /// <param name="pageTemplate">The page template.</param>
    /// <param name="relatedImage">The related image.</param>
    /// <param name="page">The page.</param>
    /// <returns></returns>
    public static string GetSmallThumbnailUrl(
      this PageTemplate pageTemplate,
      Image relatedImage,
      Page page = null)
    {
      if (relatedImage == null)
        relatedImage = pageTemplate.GetRelatedImage();
      if (relatedImage != null)
        return Extensions.GetImageThumbnailUrl(relatedImage, 42, 39);
      string image = pageTemplate.GetImage("icon");
      string smallImagePath = Extensions.GetSmallImagePath(!string.IsNullOrEmpty(image) ? image : "Telerik.Sitefinity.Resources.Themes.Light.Images.PageTemplates.Custom.gif");
      return page == null ? ControlUtilities.ResolveResourceUrlWithoutPage(smallImagePath) : ControlUtilities.ResolveResourceUrl(smallImagePath, page);
    }

    public static string ProcessText(this IHasTextMode control, string text)
    {
      string str = text;
      switch (control.TextMode)
      {
        case TextMode.Encode:
          str = HttpUtility.HtmlEncode(text);
          break;
        case TextMode.Sanitize:
          str = ControlUtilities.Sanitize(text);
          break;
        case TextMode.SanitizeUrl:
          str = ControlUtilities.SanitizeUrl(text);
          break;
      }
      return str;
    }

    /// <summary>
    /// Gets the parameter value and registers output cache variation for it.
    /// </summary>
    /// <param name="queryStringCollection">The query string collection.</param>
    /// <param name="parameterName">The query string parameter name.</param>
    private static string ParamGet(
      this NameValueCollection paramCollection,
      CacheVariationParameterSource source,
      string parameterName,
      Func<CacheVariationParamValidator> getValidator = null)
    {
      HttpContextBase currentHttpContext = SystemManager.CurrentHttpContext;
      if (currentHttpContext == null || !currentHttpContext.Items.Contains((object) "ServedPageNode"))
        return paramCollection[parameterName];
      string key = ParamOutputCacheVariation.GenerateKey(parameterName, source);
      object obj = currentHttpContext.Items[(object) key];
      if (obj != null)
        return obj as string;
      string paramValue = paramCollection[parameterName];
      CacheVariationParamValidator validator = getValidator != null ? getValidator() : (CacheVariationParamValidator) null;
      PageRouteHandler.RegisterCustomOutputCacheVariation((ICustomOutputCacheVariation) new ParamOutputCacheVariation(key, source, validator), currentHttpContext);
      if (!paramValue.IsNullOrEmpty() && (validator == null || validator.Validate(paramValue)))
      {
        currentHttpContext.Items[(object) key] = (object) paramValue;
        return paramValue;
      }
      currentHttpContext.Items[(object) key] = new object();
      return (string) null;
    }

    private static string GetSmallImagePath(string imagePath)
    {
      string smallImagePath = string.Empty;
      string[] strArray = imagePath.Split(new char[1]{ '.' }, StringSplitOptions.RemoveEmptyEntries);
      if (strArray.Length > 1)
      {
        string oldValue = strArray[strArray.Length - 2];
        if (!oldValue.IsNullOrEmpty())
        {
          string newValue = "sml_" + oldValue;
          smallImagePath = imagePath.Replace(oldValue, newValue);
        }
      }
      return smallImagePath;
    }

    private static string GetImageThumbnailUrl(Image image, int maxWidth, int maxHeight)
    {
      bool generateAbsoluteUrls = Config.Get<SystemConfig>().SiteUrlSettings.GenerateAbsoluteUrls;
      return image.ResolveMediaUrl(new Dictionary<string, string>()
      {
        {
          "MaxWidth",
          maxWidth.ToString()
        },
        {
          "MaxHeight",
          maxHeight.ToString()
        },
        {
          "ScaleUp",
          false.ToString()
        },
        {
          "Quality",
          "High"
        },
        {
          "Method",
          "ResizeFitToAreaArguments"
        }
      }, generateAbsoluteUrls);
    }
  }
}
