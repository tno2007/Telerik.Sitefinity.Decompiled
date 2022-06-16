// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.ProtectionShieldRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.ProtectionShield;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents route handler for Protection shield page.</summary>
  public class ProtectionShieldRouteHandler : RouteHandlerBase
  {
    /// <summary>
    /// Specifies the embedded Under Construction page template name.
    /// </summary>
    private const string ShieldEnabledTemplate = "Telerik.Sitefinity.Resources.Pages.ShieldEnabled.aspx";

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    /// <param name="requestContext">The request context</param>
    /// <returns>The template</returns>
    public override TemplateInfo GetTemplateInfo(RequestContext requestContext) => new TemplateInfo()
    {
      TemplateName = "Telerik.Sitefinity.Resources.Pages.ShieldEnabled.aspx",
      ControlType = this.GetType()
    };

    /// <summary>Initializes the content</summary>
    /// <param name="handler">The handler</param>
    /// <param name="requestContext">Request context</param>
    protected override void InitializeContent(Page handler, RequestContext requestContext)
    {
      base.InitializeContent(handler, requestContext);
      if (!(handler.FindControl("message") is Label control))
        throw new TemplateException("Telerik.Sitefinity.Resources.Pages.ShieldEnabled.aspx", "System.Web.UI.WebControls.Label", "message");
      control.Text = Res.Get<ProtectionShieldResources>().SiteCantBeReachedText;
      handler.Title = SystemManager.CurrentContext.CurrentSite.Name;
    }
  }
}
