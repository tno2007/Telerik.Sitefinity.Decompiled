// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.SitefinityPage
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>Handles requests to Sitefinity pages.</summary>
  public class SitefinityPage : Page, ISitefinityPage, IPartialRouteHandler
  {
    private bool skipFormVerification;
    private RequestContext requestContext;
    private PlaceHoldersCollection placeHolders;
    private UrlEvaluationMode urlEvaluationMode;
    private PartialRequestContext partialRequestContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.SitefinityPage" /> class.
    /// </summary>
    public SitefinityPage()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.UI.SitefinityPage" /> class with the provided <see cref="P:Telerik.Sitefinity.Web.UI.SitefinityPage.RequestContext" />.
    /// </summary>
    /// <param name="requestContext">An object that encapsulates information about the request.</param>
    /// <param name="skipFormVerification">if set to <c>true</c> [skip form verification].</param>
    public SitefinityPage(RequestContext requestContext, bool skipFormVerification)
    {
      this.requestContext = requestContext;
      this.skipFormVerification = skipFormVerification;
    }

    /// <summary>
    /// Gets or sets <see cref="P:Telerik.Sitefinity.Web.UI.SitefinityPage.RequestContext" /> for the page.
    /// </summary>
    public RequestContext RequestContext
    {
      get => this.requestContext;
      set => this.requestContext = value;
    }

    /// <summary>Gets or sets the content place holders in this page.</summary>
    /// <value>The place holders.</value>
    public PlaceHoldersCollection PlaceHolders
    {
      get => this.placeHolders;
      set => this.placeHolders = value;
    }

    /// <summary>
    /// Gets or sets the URL evaluation mode - URL segments or query string.
    /// This property is used by all controls on a page that have URL Evaluators. Information for interpreting a url
    /// for a specific item or page is passed either through the URL itself or through the QueryString. The
    /// value of this property indicates which one is used.
    /// </summary>
    public UrlEvaluationMode UrlEvaluationMode
    {
      get => this.urlEvaluationMode;
      set => this.urlEvaluationMode = value;
    }

    /// <summary>
    /// Confirms that an <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control is rendered for the specified ASP.NET server control at run time.
    /// </summary>
    /// <param name="control">The ASP.NET server control that is required in the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> control.</param>
    /// <exception cref="T:System.Web.HttpException">
    /// The specified server control is not contained between the opening and closing tags of the <see cref="T:System.Web.UI.HtmlControls.HtmlForm" /> server control at run time.
    /// </exception>
    /// <exception cref="T:System.ArgumentNullException">
    /// The control to verify is null.
    /// </exception>
    public override void VerifyRenderingInServerForm(Control control)
    {
      bool flag = this.Items != null && this.Items[(object) "IsInIndexMode"] != null && (bool) this.Items[(object) "IsInIndexMode"];
      if (this.skipFormVerification || flag)
        return;
      base.VerifyRenderingInServerForm(control);
    }

    string IPartialRouteHandler.Name => "Root";

    RouteCollection IPartialRouteHandler.CreateRoutes() => RouteTable.Routes;

    void IPartialRouteHandler.RegisterChildRouteHandlers(IList<RouteInfo> list)
    {
    }

    PartialRequestContext IPartialRouteHandler.PartialRequestContext
    {
      get
      {
        if (this.partialRequestContext == null)
          this.partialRequestContext = new PartialRequestContext(new PartialHttpContext(this.RequestContext.HttpContext.Request.Path), this.RequestContext.RouteData, string.Empty);
        return this.partialRequestContext;
      }
      set => this.partialRequestContext = value;
    }

    IPartialRouteHandler IPartialRouteHandler.ParentRouteHandler
    {
      get => (IPartialRouteHandler) null;
      set => throw new NotSupportedException();
    }
  }
}
