// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ControlLiteralRepresentation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Modules.Forms;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// ControlLiteralRepresentation renders a control outside of its page context and returns its HTML as string.
  /// </summary>
  public class ControlLiteralRepresentation : Control
  {
    private string renderedHtml;
    private const string ControlStartMark = "<!---@sitefinity-control-start@--->";
    private const string ControlEndMark = "<!---@sitefinity-control-end@--->";

    /// <summary>Gets or sets the original control.</summary>
    /// <value>The original control.</value>
    public Control OriginalControl { get; set; }

    /// <summary>Gets or sets the state.</summary>
    /// <value>The state.</value>
    public ZoneEditorWebServiceArgs State { get; set; }

    public List<HtmlLink> CssLinkControls { get; protected set; }

    public Page RenderedPage { get; protected set; }

    /// <summary>
    /// Gets or sets a value indicating whether to check the original control and insert an "empty"
    /// control(icon+link) for original controls implementing ICustomWidgetVisualization.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if [check and insert empty control]; otherwise, <c>false</c>.
    /// </value>
    public bool CheckAndInsertEmptyControl { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlLiteralRepresentation" /> class.
    /// </summary>
    public ControlLiteralRepresentation() => this.CheckAndInsertEmptyControl = false;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.ControlLiteralRepresentation" /> class.
    /// </summary>
    /// <param name="originalControl">The original control.</param>
    /// <param name="url">The URL.</param>
    public ControlLiteralRepresentation(Control originalControl, ZoneEditorWebServiceArgs state)
    {
      this.OriginalControl = originalControl;
      this.State = state;
    }

    /// <summary>Sets the properties recursively.</summary>
    /// <param name="control">The control.</param>
    /// <param name="clearPreRender">if set to <c>true</c> [clear pre render].</param>
    protected void SetPropertiesRecursive(Control control, bool clearPreRender)
    {
      if (control is Telerik.Web.IControl control1)
        control1.RegisterWithScriptManager = false;
      if (!control.HasControls())
        return;
      foreach (Control control2 in control.Controls)
        this.SetPropertiesRecursive(control2, clearPreRender);
    }

    /// <summary>Renders the control to the specified HTML writer.</summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object that receives the control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      this.CssLinkControls = new List<HtmlLink>();
      if (this.OriginalControl == null)
      {
        writer.Write("No control set");
      }
      else
      {
        try
        {
          ITemplate template = ControlUtilities.GetTemplate(RouteHandler.EmptyTemplateInfo);
          string virtualPath = this.State.Url;
          if (virtualPath.IndexOf('?') > -1)
            virtualPath = virtualPath.Substring(0, virtualPath.IndexOf('?'));
          HttpContext httpContext1 = RouteHelper.CreateHttpContext(VirtualPathUtility.ToAppRelative(virtualPath), (TextWriter) writer);
          httpContext1.Items[(object) "AspSession"] = HttpContext.Current.Items[(object) "AspSession"];
          if (HttpContext.Current.Items.Contains((object) "sf_use_embedded_backend_theme"))
            httpContext1.Items[(object) "sf_use_embedded_backend_theme"] = HttpContext.Current.Items[(object) "sf_use_embedded_backend_theme"];
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          HttpContext current = HttpContext.Current;
          Guid id = SystemManager.CurrentContext.CurrentSite.Id;
          HttpContext.Current = httpContext1;
          SiteRegion siteRegion = SiteRegion.FromSiteId(id);
          try
          {
            HttpContext.Current.User = current.User;
            SystemManager.HttpContextItems[(object) "RadControlRandomNumber"] = (object) 0;
            HttpContextWrapper httpContext2 = new HttpContextWrapper(httpContext1);
            httpContext2.Items[(object) "sfCurrentBackendCulture"] = (object) culture;
            RouteData routeData = RouteTable.Routes.GetRouteData((HttpContextBase) httpContext2);
            if (routeData == null)
              return;
            RequestContext requestContext = new RequestContext((HttpContextBase) httpContext2, routeData);
            SystemManager.CurrentContext.Culture = culture;
            ITemplate pageTemplate = template;
            Page handler = RouteHandlerBase.CreateHandler(requestContext, pageTemplate);
            handler.Items[(object) "DesignMediaType"] = (object) this.State.MediaType;
            handler.AppRelativeVirtualPath = SystemManager.CurrentHttpContext.Request.AppRelativeCurrentExecutionFilePath;
            if (this.State != null && this.State.Id != Guid.Empty)
              SystemManager.HttpContextItems[(object) "FormControlId"] = (object) this.State.Id;
            handler.Controls.Add((Control) new ControlLiteralRepresentation.FakeManager());
            this.RenderedPage = handler;
            handler.Init += new EventHandler(this.page_Init);
            handler.Load += new EventHandler(this.page_Load);
            handler.InitComplete += new EventHandler(this.page_InitComplete);
            httpContext1.Items.Add((object) "sfFrontEndControlRender", (object) true);
            httpContext1.Handler = (IHttpHandler) handler;
            handler.ProcessRequest(httpContext1);
            handler.Init -= new EventHandler(this.page_Init);
            handler.Load -= new EventHandler(this.page_Load);
            handler.InitComplete -= new EventHandler(this.page_InitComplete);
          }
          finally
          {
            SystemManager.ClearCurrentTransactions();
            siteRegion.Dispose();
            HttpContext.Current = current;
          }
        }
        catch (HttpUnhandledException ex)
        {
          Exception exceptionToHandle = ex.InnerException == null ? (Exception) ex : ex.InnerException;
          Exceptions.HandleException(exceptionToHandle, ExceptionPolicyName.UnhandledExceptions);
          writer.Write(exceptionToHandle.Message);
        }
        catch (Exception ex)
        {
          Exceptions.HandleException(ex, ExceptionPolicyName.UnhandledExceptions);
          writer.Write(ex.Message);
        }
      }
    }

    private void page_InitComplete(object sender, EventArgs e)
    {
      RouteHandler.EnsureSitefinityResourceManager((Page) sender);
      SitefinityStyleSheetManager.GetCurrent((Page) sender).StyleSheetsRendered += new EventHandler(this.ControlLiteralRepresentation_StyleSheetsRendered);
    }

    private void ControlLiteralRepresentation_StyleSheetsRendered(object sender, EventArgs e)
    {
      List<HtmlLink> htmlLinkList = new List<HtmlLink>();
      foreach (Control control in this.RenderedPage.Header.Controls)
      {
        if (control is HtmlLink)
          htmlLinkList.Add((HtmlLink) control);
      }
      this.CssLinkControls = htmlLinkList;
    }

    public string GetFullHtml()
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

    public string GetControlHtml()
    {
      string fullHtml = this.GetFullHtml();
      int startIndex = fullHtml.IndexOf("<!---@sitefinity-control-start@--->") + "<!---@sitefinity-control-start@--->".Length;
      int num = fullHtml.IndexOf("<!---@sitefinity-control-end@--->");
      return startIndex > 0 && num > 0 ? fullHtml.Substring(startIndex, num - startIndex).Trim() : string.Empty;
    }

    private void page_Load(object sender, EventArgs e)
    {
      Page page = (Page) sender;
      Control originalControl = this.OriginalControl;
      this.SetPropertiesRecursive(originalControl, false);
      SystemManager.SetPageDesignMode(true);
      IControlsContainer controlsContainer;
      switch (this.State.MediaType)
      {
        case DesignMediaType.Page:
        case DesignMediaType.NewsletterCampaign:
        case DesignMediaType.NewsletterTemplate:
          PageManager manager = PageManager.GetManager();
          PageTemplate pageTemplate1 = manager.GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == this.State.PageId));
          controlsContainer = pageTemplate1 == null ? (IControlsContainer) manager.GetDraft<PageDraft>(this.State.PageId) : (IControlsContainer) pageTemplate1;
          break;
        case DesignMediaType.Template:
          PageTemplate pageTemplate2 = PageManager.GetManager().GetTemplates().FirstOrDefault<PageTemplate>((Expression<Func<PageTemplate, bool>>) (t => t.Id == this.State.PageId));
          controlsContainer = pageTemplate2 == null ? (IControlsContainer) PageManager.GetManager().GetDraft<TemplateDraft>(this.State.PageId) : (IControlsContainer) pageTemplate2;
          break;
        case DesignMediaType.Form:
          controlsContainer = (IControlsContainer) FormsManager.GetManager().GetDraft(this.State.PageId);
          break;
        default:
          throw new ArgumentException();
      }
      HtmlForm form = page.Form;
      form.Controls.Add((Control) new LiteralControl("<!---@sitefinity-control-start@--->"));
      foreach (ControlData control in controlsContainer.Controls)
      {
        if (control.IsDataSource)
          form.Controls.Add(PageManager.GetManager().LoadControl((ObjectData) control, (CultureInfo) null));
      }
      form.Controls.Add(originalControl);
      form.Controls.Add((Control) new LiteralControl("<!---@sitefinity-control-end@--->"));
    }

    private void page_Init(object sender, EventArgs e) => PageManager.ConfigureScriptManager((Page) sender, ScriptRef.Empty);

    internal class FakeManager : ScriptManager
    {
      protected override void Render(HtmlTextWriter writer)
      {
      }
    }
  }
}
