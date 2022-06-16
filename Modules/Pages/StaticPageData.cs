// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.StaticPageData
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.Modules.Pages
{
  /// <summary>Maintains cached page data.</summary>
  public sealed class StaticPageData : IPageData, IPresentable
  {
    private Dictionary<string, ITemplate> pageTemplates;
    private List<Dictionary<string, ITemplate>> layouts;
    private List<ControlBuilder> controls;
    private bool live = true;
    private ContentLifecycleStatus pageStatus;
    private int pageVersion;
    private bool mustIncludeLayoutCss;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Pages.StaticPageData" /> class.
    /// </summary>
    /// <param name="pageData">The page data.</param>
    /// <param name="provider">The provider.</param>
    public StaticPageData(PageData pageData, PageDataProvider provider)
    {
      this.SetPageDirectivesInternal(pageData);
      this.UiCulture = SystemManager.CurrentContext.Culture.Name;
      List<IControlsContainer> controlsContainerList = new List<IControlsContainer>();
      List<Dictionary<string, ITemplate>> layoutTmps = new List<Dictionary<string, ITemplate>>();
      Dictionary<string, ITemplate> pageTmps = new Dictionary<string, ITemplate>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      PageHelper.ProcessPresentationData(((IPresentable) pageData).Presentation, (IList<Dictionary<string, ITemplate>>) layoutTmps, pageTmps);
      controlsContainerList.Add((IControlsContainer) pageData);
      if (string.IsNullOrEmpty(this.MasterPage))
      {
        string masterPage;
        bool includeScriptManager;
        PageHelper.ProcessTemplates((IPageTemplate) pageData.Template, layoutTmps, pageTmps, controlsContainerList, out masterPage, out includeScriptManager);
        if (includeScriptManager)
          this.IncludeScriptManager = true;
        this.MasterPage = masterPage;
      }
      if (pageTmps.Count > 0)
        this.pageTemplates = pageTmps;
      List<ControlBuilder> builders = new List<ControlBuilder>();
      PageHelper.ProcessControls((IList<ControlBuilder>) builders, (IList<IControlsContainer>) controlsContainerList);
      if (builders.Count > 0)
        this.controls = builders;
      if (layoutTmps.Count > 0)
      {
        this.layouts = layoutTmps;
        this.layouts.Reverse();
      }
      if (this.Themes == null)
        this.Themes = (IDictionary<CultureInfo, string>) PageHelper.GetTheme(pageData);
      this.UrlEvaluationMode = pageData.UrlEvaluationMode;
      this.live = pageData.Visible;
      this.IsPersonalized = pageData.IsPersonalized;
      this.PersonalizationMasterId = pageData.PersonalizationMasterId;
      this.PersonalizationSegmentId = pageData.PersonalizationSegmentId;
    }

    /// <summary>
    /// Gets or sets a value indicating whether this page requires SSL.
    /// </summary>
    /// <value><c>true</c> if [require SSL]; otherwise, <c>false</c>.</value>
    public bool RequireSsl { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the view state of this page is enabled.
    /// The default value is false as opposed to standard ASP.Net page.
    /// </summary>
    /// <value><c>true</c> if [enable view state]; otherwise, <c>false</c>.</value>
    public bool EnableViewState { get; private set; }

    /// <summary>
    /// Indicates that ASP.NET should verify message authentication codes (MAC) in the page's view state when the page is
    /// posted back from the client. true if view state should be MAC checked; otherwise, false. The default is true.
    /// </summary>
    /// <value><c>true</c> if [enable view state MAC]; otherwise, <c>false</c>.</value>
    public bool EnableViewStateMac { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether to cache the rendered output of this page.
    /// </summary>
    /// <value><c>true</c> if [cache output]; otherwise, <c>false</c>.</value>
    [Obsolete("Use OutputCacheProfile property")]
    public bool CacheOutput { get; private set; }

    /// <summary>
    /// The name of the cache settings to associate with the page.
    /// When specified on a page, the value must match the names of one of the available
    /// entries in the outputCacheProfiles element under the outputCacheSettings section.
    /// If the name does not match a profile entry, an exception is thrown.
    /// 
    /// If profile is set all other settings will be overriden.
    /// </summary>
    public string OutputCacheProfile { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether the duration of the cached output is calculated from the last request.
    /// If the value is false the cache will expire at absolute time intervals.
    /// </summary>
    /// <value><c>true</c> if [sliding expiration]; otherwise, <c>false</c>.</value>
    [Obsolete("Use OutputCacheProfile property")]
    public bool SlidingExpiration { get; private set; }

    /// <summary>
    /// Gets or sets the time, in seconds, that the page is cached.
    /// </summary>
    /// <value>The duration of the cache.</value>
    [Obsolete("Use OutputCacheProfile property")]
    public int CacheDuration { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether this page is crawlable.
    /// </summary>
    /// <value><c>true</c> if [crawlable]; otherwise, <c>false</c>.</value>
    public bool Crawlable { get; private set; }

    /// <summary>
    /// Gets or sets a value for custom content to be inserted in head tag.
    /// </summary>
    /// <value>The value for custom content to be inserted in head tag</value>
    public string HeadTagContent { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether validation of events in postback and callback scenarios is enabled.
    /// true if events are being validated; otherwise, false. The default is true.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [enable event validation]; otherwise, <c>false</c>.
    /// </value>
    public bool EnableEventValidation { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether session state is enabled for this page.
    /// The default value is true.
    /// </summary>
    /// <value><c>true</c> if [enable session state]; otherwise, <c>false</c>.</value>
    public bool EnableSessionState { get; private set; }

    /// <summary>
    /// Indicates whether themes are used on the page. true if themes are used; otherwise, false. The default is true.
    /// </summary>
    /// <value><c>true</c> if themes are enabled; otherwise, <c>false</c>.</value>
    public bool EnableTheming { get; private set; }

    /// <summary>
    /// Gets or sets a target URL for redirection if an unhandled page exception occurs.
    /// </summary>
    /// <value>The error page.</value>
    public string ErrorPage { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether request validation should occur.
    /// If true, request validation checks all input data against a hard-coded list of potentially dangerous values.
    /// If a match occurs, an HttpRequestValidationException exception is thrown. The default is true.
    /// </summary>
    /// <value><c>true</c> if [validate request]; otherwise, <c>false</c>.</value>
    public bool ValidateRequest { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether tracing is enabled. true if tracing is enabled; otherwise, false. The default is false.
    /// </summary>
    /// <value><c>true</c> if trace; otherwise, <c>false</c>.</value>
    public bool Trace { get; private set; }

    /// <summary>
    /// Indicates how trace messages are to be displayed for the page when tracing is enabled.
    /// Possible values are SortByTime and SortByCategory. The default, when tracing is enabled, is SortByTime.
    /// </summary>
    /// <value>The trace mode.</value>
    public TraceMode TraceMode { get; private set; }

    /// <summary>Gets or sets the view state encryption.</summary>
    /// <value>The view state encryption.</value>
    public ViewStateEncryptionMode ViewStateEncryption { get; private set; }

    /// <summary>
    /// Gets or sets the culture setting for the page.
    /// The value of this attribute must be a valid culture ID.
    /// </summary>
    /// <value>The culture.</value>
    public string Culture { get; private set; }

    /// <summary>
    /// Gets or sets the user interface (UI) culture setting to use for the page.
    /// Supports any valid UI culture value.
    /// </summary>
    /// <value>The UI culture.</value>
    public string UiCulture { get; private set; }

    /// <summary>
    ///  the name of the encoding scheme used for the HTTP response that contains a page's content.
    ///  The value assigned to this attribute must be a valid encoding name.
    ///  For a list of possible encoding names, see the <see cref="!:Encoding" /> class.
    ///  You can also call the GetEncodings method for a list of possible encoding names and IDs.
    /// </summary>
    /// <value>The response encoding.</value>
    public string ResponseEncoding { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether HTTP response buffering is enabled. true if page buffering is enabled; otherwise, false.
    /// The default is true.
    /// </summary>
    /// <value><c>true</c> if [buffer output]; otherwise, <c>false</c>.</value>
    public bool BufferOutput { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether to return the user to the same position in the client browser after postback.
    /// true if users should be returned to the same position; otherwise, false. The default is false.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [maintain scroll position on postback]; otherwise, <c>false</c>.
    /// </value>
    public bool MaintainScroll { get; private set; }

    /// <summary>
    /// Gets or sets a URL for loading external page from the file system.
    /// </summary>
    /// <value>The URL of the external page.</value>
    public string ExternalPage { get; set; }

    /// <summary>Gets or sets the master page.</summary>
    /// <value>The master page.</value>
    public string MasterPage { get; set; }

    public string Theme => PageHelper.GetThemeName(this.Themes);

    /// <summary>Gets the theme.</summary>
    /// <value>The theme.</value>
    public IDictionary<CultureInfo, string> Themes { get; private set; }

    /// <summary>Gets the ID.</summary>
    /// <value>The pageId.</value>
    public Guid Id { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether to automatically include script manager on the page.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [include script manager]; otherwise, <c>false</c>.
    /// </value>
    public bool IncludeScriptManager { get; set; }

    /// <summary>Gets or sets the last ID used for new control.</summary>
    /// <value>The last control pageId.</value>
    public int LastControlId { get; set; }

    /// <summary>Gets or sets the HTML title.</summary>
    /// <value>The HTML title.</value>
    public string HtmlTitle { get; set; }

    /// <summary>Gets or sets the HTML description.</summary>
    /// <value>The HTML description.</value>
    public string HtmlDescription { get; set; }

    /// <summary>Gets or sets the HTML keywords.</summary>
    /// <value>The HTML keywords.</value>
    public string HtmlKeywords { get; set; }

    IEnumerable<PresentationData> IPresentable.Presentation => throw new NotSupportedException();

    /// <summary>Gets or sets the URL evaluation mode.</summary>
    /// <value>The URL evaluation mode.</value>
    public UrlEvaluationMode UrlEvaluationMode { get; private set; }

    /// <summary>Gets or sets the is personalized.</summary>
    public bool IsPersonalized { get; set; }

    /// <summary>Gets or sets the personalization master id.</summary>
    /// <value>The personalization master id.</value>
    public Guid PersonalizationMasterId { get; set; }

    /// <summary>
    /// Gets or sets the id of the segment for which the page is personalized. If page is a master
    /// (not a personalized version), this value is an empty GUID.
    /// </summary>
    public Guid PersonalizationSegmentId { get; set; }

    /// <summary>Gets the page template.</summary>
    /// <value>The page template.</value>
    public ITemplate GetPageTemplate() => PageHelper.GetPageTemplate((IDictionary<string, ITemplate>) this.pageTemplates, this.Theme);

    /// <summary>Applies the layouts.</summary>
    /// <param name="page">The page.</param>
    public void ApplyLayouts(Page page)
    {
      if (PageRouteHandler.ProcessRequestLicensing(page.Request.RequestContext.HttpContext))
        return;
      PageHelper.ApplyLayouts((IList<Dictionary<string, ITemplate>>) this.layouts, page, this.Theme);
    }

    /// <summary>Creates the child controls.</summary>
    /// <param name="page">The page.</param>
    public void CreateChildControls(Page page, bool isFrontEndPage)
    {
      if (this.IncludeScriptManager)
        RouteHandler.EnsureScriptManager(page);
      if (!string.IsNullOrEmpty(this.HtmlDescription))
        page.Header.Controls.Add((Control) new HtmlMeta()
        {
          Name = "description",
          Content = this.HtmlDescription
        });
      if (!string.IsNullOrEmpty(this.HtmlKeywords))
        page.Header.Controls.Add((Control) new HtmlMeta()
        {
          Name = "keywords",
          Content = this.HtmlKeywords
        });
      PageHelper.CreateChildControls((IList<ControlBuilder>) this.controls, page, false);
    }

    /// <summary>Sets the page properties.</summary>
    /// <param name="page">The page.</param>
    public void SetPageDirectives(Page page)
    {
      page.EnableViewState = this.EnableViewState;
      page.EnableViewStateMac = this.EnableViewStateMac;
      page.EnableEventValidation = this.EnableEventValidation;
      page.EnableTheming = this.EnableTheming;
      page.ErrorPage = this.ErrorPage;
      page.TraceEnabled = this.Trace;
      page.TraceModeValue = this.TraceMode;
      page.ViewStateEncryptionMode = this.ViewStateEncryption;
      if (!string.IsNullOrEmpty(this.ResponseEncoding))
        page.ResponseEncoding = this.ResponseEncoding;
      page.MaintainScrollPositionOnPostBack = this.MaintainScroll;
      ThemeController.SetPageTheme(this.Theme, page);
      page.MasterPageFile = this.MasterPage;
      page.Title = this.HtmlTitle;
    }

    /// <summary>Sets the page directives.</summary>
    private void SetPageDirectivesInternal(PageData pageData)
    {
      this.Id = pageData.Id;
      this.RequireSsl = pageData.RequireSsl;
      this.EnableViewState = pageData.EnableViewState;
      this.IncludeScriptManager = pageData.IncludeScriptManager;
      this.EnableViewStateMac = pageData.EnableViewStateMac;
      this.OutputCacheProfile = pageData.OutputCacheProfile;
      this.Crawlable = pageData.Crawlable;
      this.HeadTagContent = pageData.HeadTagContent;
      this.EnableEventValidation = pageData.EnableEventValidation;
      this.EnableSessionState = pageData.EnableSessionState;
      this.EnableTheming = pageData.EnableTheming;
      this.ErrorPage = pageData.ErrorPage;
      this.ValidateRequest = pageData.ValidateRequest;
      this.Trace = pageData.Trace;
      this.TraceMode = pageData.TraceMode;
      this.ViewStateEncryption = pageData.ViewStateEncryption;
      this.Culture = pageData.Culture;
      this.ResponseEncoding = pageData.ResponseEncoding;
      this.BufferOutput = pageData.BufferOutput;
      this.MaintainScroll = pageData.MaintainScrollPositionOnPostback;
      this.ExternalPage = pageData.ExternalPage;
      this.MasterPage = pageData.MasterPage;
      this.Themes = (IDictionary<CultureInfo, string>) PageHelper.GetTheme(pageData);
      this.IncludeScriptManager = pageData.IncludeScriptManager;
      this.LastControlId = pageData.LastControlId;
      this.HtmlTitle = (string) pageData.HtmlTitle;
      this.HtmlDescription = (string) pageData.Description;
      this.HtmlKeywords = (string) pageData.Keywords;
      this.pageVersion = pageData.Version;
      this.pageStatus = pageData.Status;
    }
  }
}
