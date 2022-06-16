// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.SiteOfflineRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents route handler for Under Construction page.</summary>
  public class SiteOfflineRouteHandler : RouteHandlerBase
  {
    private string message;
    /// <summary>
    /// Specifies the embedded Under Construction page template name.
    /// </summary>
    private const string SiteOfflineTemplate = "Telerik.Sitefinity.Resources.Pages.SiteOffline.aspx";

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Web.SiteOfflineRouteHandler" /> class.
    /// </summary>
    public SiteOfflineRouteHandler(string message)
    {
      this.message = message;
      this.Title = "Site offline";
    }

    internal string Title { get; set; }

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    public override TemplateInfo GetTemplateInfo(RequestContext requestContext) => new TemplateInfo()
    {
      TemplateName = "Telerik.Sitefinity.Resources.Pages.SiteOffline.aspx",
      ControlType = this.GetType()
    };

    protected override void InitializeContent(Page handler, RequestContext requestContext)
    {
      base.InitializeContent(handler, requestContext);
      if (!(handler.FindControl("message") is Label control))
        throw new TemplateException("Telerik.Sitefinity.Resources.Pages.SiteOffline.aspx", "System.Web.UI.WebControls.Label", "message");
      control.Text = this.message;
      (handler.FindControl("title") as HtmlTitle).Text = this.Title ?? this.message;
    }
  }
}
