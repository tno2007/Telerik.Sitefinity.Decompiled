// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.StartupRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;
using System.Web.UI;
using Telerik.Sitefinity.Web.Compilation;

namespace Telerik.Sitefinity.Web
{
  /// <summary>Represents route handler for login page.</summary>
  public class StartupRouteHandler : IRouteHandler
  {
    /// <summary>Specifies the embedded startup page template name.</summary>
    public const string StartupTemplate = "Telerik.Sitefinity.Resources.Pages.Startup.aspx";

    public IHttpHandler GetHttpHandler(RequestContext requestContext)
    {
      requestContext.HttpContext.Server.ScriptTimeout = 600;
      return (IHttpHandler) CompilationHelpers.LoadControl<Page>("~/SFRes/" + "Telerik.Sitefinity.Resources.Pages.Startup.aspx");
    }
  }
}
