// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.InlineEditing.ControlLiteralRepresentation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity.InlineEditing
{
  /// <summary>
  /// ControlLiteralRepresentation renders a control outside of its page context and returns its HTML as string.
  /// </summary>
  internal class ControlLiteralRepresentation : Control
  {
    private string renderedHtml;
    private const string ControlStartMark = "<!---@sitefinity-control-start@--->";
    private const string ControlEndMark = "<!---@sitefinity-control-end@--->";
    private const string LinksParser = "LinksParser";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.InlineEditing.ControlLiteralRepresentation" /> class.
    /// </summary>
    /// <param name="originalControl">The original control.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageUrl">The page URL.</param>
    public ControlLiteralRepresentation(Control originalControl, Guid pageId, string pageUrl)
    {
      this.OriginalControl = originalControl;
      this.PageId = pageId;
      this.PageUrl = pageUrl;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.InlineEditing.ControlLiteralRepresentation" /> class.
    /// </summary>
    /// <param name="originalControls">The original controls.</param>
    /// <param name="pageId">The page id.</param>
    /// <param name="pageUrl">The page URL.</param>
    /// <param name="isFrontEndRequest">if set to <c>true</c> simulates front-end request.</param>
    public ControlLiteralRepresentation(
      IEnumerable<Control> originalControls,
      Guid pageId,
      string pageUrl,
      bool isFrontEndRequest = false)
    {
      this.OriginalControls = originalControls;
      this.PageId = pageId;
      this.PageUrl = pageUrl;
      this.IsFrontEndRequest = isFrontEndRequest;
    }

    public event EventHandler BeforeRender;

    public event EventHandler AfterRender;

    /// <summary>Gets or sets the original control.</summary>
    /// <value>The original control.</value>
    public Control OriginalControl { get; set; }

    /// <summary>Gets or sets the collection of original controls.</summary>
    /// <value>The original controls.</value>
    public IEnumerable<Control> OriginalControls { get; set; }

    /// <summary>Gets or sets the page id.</summary>
    /// <value>The page id.</value>
    public Guid PageId { get; set; }

    /// <summary>Gets or sets the page URL.</summary>
    /// <value>The page URL.</value>
    public string PageUrl { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to simulate front-end request or not.
    /// </summary>
    public bool IsFrontEndRequest { get; set; }

    public PageSiteNode PageSiteNode { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether section name of the scripts should be rendered.
    /// </summary>
    /// <value>
    ///   <c>true</c> if section name of the scripts should be rendered; otherwise, <c>false</c>.
    /// </value>
    internal bool RenderScriptSection { get; set; }

    /// <summary>Gets the HTML of the specified control.</summary>
    /// <returns>The rendered HTML</returns>
    public string GetControlHtml()
    {
      string fullHtml = this.GetFullHtml();
      int startIndex = fullHtml.IndexOf("<!---@sitefinity-control-start@--->") + "<!---@sitefinity-control-start@--->".Length;
      int num = fullHtml.IndexOf("<!---@sitefinity-control-end@--->");
      return startIndex > 0 && num > 0 ? fullHtml.Substring(startIndex, num - startIndex).Trim() : string.Empty;
    }

    /// <summary>Gets the HTML of the specified control.</summary>
    /// <returns>A list with rendered HTML for each control</returns>
    public List<string> GetControlsHtml()
    {
      List<string> controlsHtml = new List<string>();
      string fullHtml = this.GetFullHtml();
      foreach (Control originalControl in this.OriginalControls)
      {
        int startIndex = fullHtml.IndexOf("<!---@sitefinity-control-start@--->") + "<!---@sitefinity-control-start@--->".Length;
        int num = fullHtml.LastIndexOf("<!---@sitefinity-control-end@--->");
        if (startIndex > 0 && num > 0)
          controlsHtml = ((IEnumerable<string>) fullHtml.Substring(startIndex, num - startIndex).Trim().Split(new string[1]
          {
            "<!---@sitefinity-control-end@--->"
          }, StringSplitOptions.None)).Select<string, string>((Func<string, string>) (p => p.Replace("<!---@sitefinity-control-start@--->", string.Empty))).ToList<string>();
      }
      return controlsHtml;
    }

    /// <inheritdoc />
    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Method's logic requires more than 65 lines.")]
    protected override void Render(HtmlTextWriter writer)
    {
      if (this.OriginalControl == null && this.OriginalControls == null)
      {
        writer.Write("No control set");
      }
      else
      {
        try
        {
          ITemplate template = ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);
          string str = this.PageUrl;
          Uri result;
          if (Uri.TryCreate(str, UriKind.Absolute, out result))
            str = result.PathAndQuery;
          HttpContext httpContext = RouteHelper.CreateHttpContext(VirtualPathUtility.ToAppRelative(str), (TextWriter) writer);
          httpContext.Items[(object) "AspSession"] = HttpContext.Current.Items[(object) "AspSession"];
          httpContext.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.AddCacheDependencies] = HttpContext.Current.Items[(object) Telerik.Sitefinity.Web.PageRouteHandler.AddCacheDependencies];
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          HttpContext current = HttpContext.Current;
          Guid id = SystemManager.CurrentContext.CurrentSite.Id;
          HttpContext.Current = httpContext;
          SiteRegion siteRegion = SiteRegion.FromSiteId(id);
          try
          {
            HttpContext.Current.User = current.User;
            SystemManager.HttpContextItems[(object) "RadControlRandomNumber"] = (object) 0;
            RequestContext requestContext = RouteHelper.CreateRequestContext(httpContext);
            if (requestContext == null && this.PageSiteNode != null)
              requestContext = RouteHelper.CreateRequestContext(RouteHelper.CreateHttpContext(VirtualPathUtility.ToAppRelative(this.PageSiteNode.Url), (TextWriter) writer));
            httpContext.Request.RequestContext.RouteData = requestContext.RouteData;
            SystemManager.CurrentContext.Culture = culture;
            Page handler = RouteHandlerBase.CreateHandler(requestContext, template);
            if (this.RenderScriptSection)
              SystemManager.CurrentHttpContext.Items[(object) "RenderScriptSection"] = (object) true;
            else
              handler.Controls.Add((Control) new ControlLiteralRepresentation.FakeManager());
            if (!SystemManager.HttpContextItems.Contains((object) "sfContentFilters"))
              SystemManager.HttpContextItems.Add((object) "sfContentFilters", (object) new string[1]
              {
                "LinksParser"
              });
            if (!this.IsFrontEndRequest)
              handler.Init += new EventHandler(this.Page_Init);
            handler.Load += new EventHandler(this.Page_Load);
            httpContext.Handler = (IHttpHandler) handler;
            EventHandler beforeRender = this.BeforeRender;
            if (beforeRender != null)
              beforeRender((object) null, (EventArgs) null);
            handler.ProcessRequest(httpContext);
            EventHandler afterRender = this.AfterRender;
            if (afterRender != null)
              afterRender((object) null, (EventArgs) null);
            if (!this.IsFrontEndRequest)
              handler.Init -= new EventHandler(this.Page_Init);
            handler.Load -= new EventHandler(this.Page_Load);
          }
          finally
          {
            SystemManager.ClearCurrentTransactions();
            siteRegion.Dispose();
            if (HttpContext.Current.Items.Contains((object) "PageDataCacheDependencyName"))
              current.Items[(object) "PageDataCacheDependencyName"] = HttpContext.Current.Items[(object) "PageDataCacheDependencyName"];
            HttpContext.Current = current;
          }
        }
        catch (HttpUnhandledException ex)
        {
          Exceptions.HandleException(ex.InnerException == null ? (Exception) ex : ex.InnerException, ExceptionPolicyName.UnhandledExceptions);
          throw new Exception(Res.Get<ErrorMessages>().ControlRenderingErrorMessage);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
          throw new Exception(Res.Get<ErrorMessages>().ControlRenderingErrorMessage);
        }
      }
    }

    /// <summary>Sets the properties recursively.</summary>
    /// <param name="control">The control.</param>
    /// <param name="clearPreRender">if set to <c>true</c> [clear pre render].</param>
    private void SetPropertiesRecursive(Control control, bool clearPreRender)
    {
      if (control is Telerik.Web.IControl control1)
        control1.RegisterWithScriptManager = false;
      if (!control.HasControls())
        return;
      foreach (Control control2 in control.Controls)
        this.SetPropertiesRecursive(control2, clearPreRender);
    }

    /// <summary>
    /// Gets the full HTML of the page with specified control.
    /// </summary>
    /// <returns>The full HTML of the page</returns>
    private string GetFullHtml()
    {
      if (this.renderedHtml == null)
      {
        using (StringWriter writer1 = new StringWriter())
        {
          using (HtmlTextWriter writer2 = new HtmlTextWriter((TextWriter) writer1))
          {
            this.Render(writer2);
            this.renderedHtml = writer1.ToString();
          }
        }
      }
      return this.renderedHtml;
    }

    private void Page_Load(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      IEnumerable<Control> controls = (IEnumerable<Control>) null;
      if (this.OriginalControl != null)
        controls = (IEnumerable<Control>) new List<Control>()
        {
          this.OriginalControl
        };
      else if (this.OriginalControls != null)
        controls = this.OriginalControls;
      PageManager manager = PageManager.GetManager();
      PageTemplate pageTemplate = manager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == this.PageId));
      IControlsContainer controlsContainer = pageTemplate == null ? (IControlsContainer) manager.GetPageNode(this.PageId).GetPageData() : (IControlsContainer) pageTemplate;
      HtmlForm form = page.Form;
      foreach (ControlData control in controlsContainer.Controls)
      {
        if (control.IsDataSource)
          form.Controls.Add(PageManager.GetManager().LoadControl((ObjectData) control, (CultureInfo) null));
      }
      foreach (Control control in controls)
      {
        this.SetPropertiesRecursive(control, false);
        form.Controls.Add((Control) new LiteralControl("<!---@sitefinity-control-start@--->"));
        form.Controls.Add(control);
        form.Controls.Add((Control) new LiteralControl("<!---@sitefinity-control-end@--->"));
      }
    }

    private void Page_Init(object sender, EventArgs e)
    {
      if (!this.RenderScriptSection)
        PageManager.ConfigureScriptManager((Page) sender, ScriptRef.Empty);
      SystemManager.SetPageDesignMode(true);
      SystemManager.CurrentHttpContext.Items[(object) "sf-lc-status"] = (object) ContentLifecycleStatus.Temp.ToString();
      SystemManager.CurrentHttpContext.Items[(object) "IsInlineEditingMode"] = (object) true;
      SystemManager.CurrentHttpContext.Items[(object) "sfContentFilters"] = (object) new string[1]
      {
        "DraftLinksParser"
      };
    }

    internal class FakeManager : ScriptManager
    {
      protected override void Render(HtmlTextWriter writer)
      {
      }
    }
  }
}
