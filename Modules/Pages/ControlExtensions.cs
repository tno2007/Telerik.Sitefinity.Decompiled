// Decompiled with JetBrains decompiler
// Type: System.Web.UI.ControlExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Web.Routing;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace System.Web.UI
{
  /// <summary>Provides System.Web.UI.Control extension methods.</summary>
  public static class ControlExtensions
  {
    /// <summary>
    /// Determines whether the control is in Sitefinity design mode.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// 	<c>true</c> if the control is in design mode; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDesignMode(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return SystemManager.IsDesignMode;
    }

    /// <summary>
    /// Determines whether the control is in Sitefinity preview mode.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// 	<c>true</c> if the control is in preview mode; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsPreviewMode(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return SystemManager.IsPreviewMode;
    }

    /// <summary>
    /// Determines whether the control is in Sitefinity inline editing mode.
    /// </summary>
    /// <param name="control">The control.</param>
    /// <returns>
    /// 	<c>true</c> if the control is in inline editing mode; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsInlineEditingMode(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return SystemManager.IsInlineEditingMode;
    }

    /// <summary>
    /// Determines whether the inline editing functionality is enabled
    /// </summary>
    /// <returns></returns>
    public static bool InlineEditingIsEnabled()
    {
      if (!SecurityManager.IsBackendUser() || !Config.Get<PagesConfig>().EnableBrowseAndEdit.HasValue || !Config.Get<PagesConfig>().EnableBrowseAndEdit.Value)
        return false;
      return AppPermission.IsGranted(AppAction.UseBrowseAndEdit);
    }

    /// <summary>
    /// Determines whether this control will produce output when rendered by in memory by the search engine.
    /// </summary>
    public static IndexRenderModes GetIndexRenderMode(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (!control.IsIndexingMode())
        return IndexRenderModes.Normal;
      if (control is ISearchIndexBehavior searchIndexBehavior)
        return searchIndexBehavior.ExcludeFromSearchIndex ? IndexRenderModes.NoOutput : IndexRenderModes.Normal;
      IndexRenderModeAttribute renderModeAttribute = control.GetType().GetCustomAttributes(true).OfType<IndexRenderModeAttribute>().LastOrDefault<IndexRenderModeAttribute>();
      return renderModeAttribute != null ? renderModeAttribute.Mode : IndexRenderModes.NoOutput;
    }

    /// <summary>
    /// Determines whether this control is rendered by the search engine.
    /// </summary>
    public static bool IsIndexingMode(this Control control) => control.Page != null && control.Page.Items.Contains((object) "IsInIndexMode") && (bool) control.Page.Items[(object) "IsInIndexMode"];

    /// <summary>Determines whether this instance is backend.</summary>
    /// <returns>
    /// 	<c>true</c> if this instance is backend; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsBackend() => ((Control) null).IsBackend();

    /// <summary>
    /// Determines whether the specified control is running in the Sitefinity's backend area.
    /// </summary>
    /// <param name="control">The control.Can be null</param>
    /// <returns>
    /// 	<c>true</c> if the specified control is backend; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsBackend(this Control control) => SystemManager.IsBackendRequest(out CultureInfo _);

    /// <summary>
    /// Gets a portion from the URL that is specific to the content item.
    /// </summary>
    /// <returns></returns>
    public static string GetUrlParameterString(this Control control, bool excludePrefixedParams) => control != null ? RouteHelper.GetUrlParameterString(control.GetUrlParameters(), excludePrefixedParams) : throw new ArgumentNullException(nameof (control));

    /// <summary>Gets the query string (as a string).</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static string GetQueryString(this Control control)
    {
      NameValueCollection nameValueCollection = control != null ? control.GetQueryStringParameters() : throw new ArgumentNullException(nameof (control));
      if (nameValueCollection == null)
        return (string) null;
      string[] allKeys = nameValueCollection.AllKeys;
      QueryStringBuilder queryStringBuilder = new QueryStringBuilder();
      foreach (string name in allKeys)
        queryStringBuilder.Add(name, nameValueCollection[name]);
      return queryStringBuilder.ToString();
    }

    /// <summary>Gets the URL data.</summary>
    /// <returns></returns>
    public static string[] GetUrlParameters(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (control.Page == null || control.Page.Request == null)
        return (string[]) null;
      RequestContext requestContext = control.Page.Request.RequestContext;
      if ((requestContext.RouteData.Values == null || requestContext.RouteData.Values.Count == 0) && control.Page is ISitefinityPage)
        requestContext = ((ISitefinityPage) control.Page).RequestContext;
      return requestContext.RouteData.Values["Params"] as string[];
    }

    /// <summary>Gets the URL data.</summary>
    /// <returns></returns>
    internal static string[] GetUrlParameters(this Page page)
    {
      if (page == null)
        throw new ArgumentNullException(nameof (page));
      return page.Request != null ? page.Request.RequestContext.RouteData.Values["Params"] as string[] : (string[]) null;
    }

    /// <summary>Gets the query string parameters.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static NameValueCollection GetQueryStringParameters(
      this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      return control.Page != null ? control.Page.GetRequestContext().RouteData.Values["Query"] as NameValueCollection : (NameValueCollection) null;
    }

    /// <summary>Gets the URL evaluation mode.</summary>
    /// <param name="control">The control.</param>
    /// <returns></returns>
    public static UrlEvaluationMode GetUrlEvaluationMode(this Control control)
    {
      if (control == null)
        throw new ArgumentNullException(nameof (control));
      if (control.Page != null)
      {
        object urlEvaluationMode = control.Page.Items[(object) "SF_PageUrlEvaluationMode"];
        if (urlEvaluationMode != null)
          return (UrlEvaluationMode) urlEvaluationMode;
      }
      return UrlEvaluationMode.Default;
    }

    /// <summary>Determines whether we are in browse and edit mode</summary>
    /// <param name="control">The control.</param>
    public static bool IsInBrowseAndEditMode(this Control control) => !SystemManager.IsDesignMode && !SystemManager.RenderInMemory && ControlExtensions.BrowseAndEditIsEnabled();

    /// <summary>
    /// Determines whether the brows and edit functionality is enabled
    /// </summary>
    /// <returns></returns>
    public static bool BrowseAndEditIsEnabled()
    {
      if (!SecurityManager.IsBackendUser() || !Config.Get<PagesConfig>().EnableBrowseAndEdit.Value)
        return false;
      return AppPermission.IsGranted(AppAction.UseBrowseAndEdit);
    }

    /// <summary>Sets the default browse and edit commands.</summary>
    /// <param name="control">The control.</param>
    public static void SetDefaultBrowseAndEditCommands(this IBrowseAndEditable control)
    {
      BrowseAndEditableInfo browseAndEditableInfo = control.BrowseAndEditableInfo;
      if (browseAndEditableInfo == null)
        return;
      BrowseAndEditBehaviorFactory.CreateConfigurationStrategy().Configure((object) control, browseAndEditableInfo.PageId);
    }

    internal static IList<RequiresEmbeddedWebResourceAttribute> GetRequiredEmbeddedWebResourceAttributes(
      this Control control)
    {
      return ControlExtensions.GetRequiredEmbeddedWebResourceAttributes(control.GetType());
    }

    internal static IList<RequiresEmbeddedWebResourceAttribute> GetRequiredEmbeddedWebResourceAttributes(
      Type controlType)
    {
      return (IList<RequiresEmbeddedWebResourceAttribute>) controlType.GetCustomAttributes(typeof (RequiresEmbeddedWebResourceAttribute), false).OfType<RequiresEmbeddedWebResourceAttribute>().ToList<RequiresEmbeddedWebResourceAttribute>();
    }
  }
}
