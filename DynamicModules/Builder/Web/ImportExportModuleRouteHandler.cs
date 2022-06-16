// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.ImportExportModuleRouteHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Web;
using System.Web.Routing;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web
{
  /// <summary>
  /// Route handler which serves the export module file or takes the file for the module import.
  /// </summary>
  internal class ImportExportModuleRouteHandler : IRouteHandler
  {
    /// <summary>Provides the object that processes the request.</summary>
    /// <returns>An object that processes the request.</returns>
    /// <param name="requestContext">
    /// An object that encapsulates information about the request.
    /// </param>
    public IHttpHandler GetHttpHandler(RequestContext requestContext) => (IHttpHandler) new ImportExportModuleHttpHandler();
  }
}
