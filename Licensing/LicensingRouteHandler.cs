// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Licensing.LicensingRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web.Routing;
using Telerik.Sitefinity.Web;
using Telerik.Sitefinity.Web.UI.Templates;

namespace Telerik.Sitefinity.Licensing
{
  public class LicensingRouteHandler : RouteHandlerBase
  {
    /// <summary>Specifies the embedded startup page template name.</summary>
    public const string StartupTemplate = "Telerik.Sitefinity.Resources.Pages.Licensing.aspx";

    /// <summary>
    /// Defines the page template and resources loaded for the current request.
    /// </summary>
    public override TemplateInfo GetTemplateInfo(RequestContext requestContext) => new TemplateInfo()
    {
      TemplateName = "Telerik.Sitefinity.Resources.Pages.Licensing.aspx",
      ControlType = this.GetType()
    };
  }
}
