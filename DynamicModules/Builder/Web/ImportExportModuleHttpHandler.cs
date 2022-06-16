// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.ImportExportModuleHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack.Text;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.DynamicModules.Builder.ExportImport;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Utilities.Zip;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web
{
  /// <summary>
  /// Http handler which serves the files that are generated
  /// by the export module functionality.
  /// </summary>
  internal class ImportExportModuleHttpHandler : IHttpHandler
  {
    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" />
    /// instance.
    /// </summary>
    /// <returns>
    /// true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that
    /// implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">
    /// An <see cref="T:System.Web.HttpContext" /> object that provides
    /// references to the intrinsic server objects (for example, Request, Response, Session,
    /// and Server) used to service HTTP requests.
    /// </param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that
    /// implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">
    /// An <see cref="T:System.Web.HttpContext" /> object that provides
    /// references to the intrinsic server objects (for example, Request, Response, Session,
    /// and Server) used to service HTTP requests.
    /// </param>
    public void ProcessRequest(HttpContextBase context)
    {
      string str = context.Request.RequestContext.RouteData.Values["action"].ToString();
      Guid moduleId = new Guid(context.Request.RequestContext.RouteData.Values["moduleId"].ToString());
      using (ModuleBuilderManager manager = ModuleBuilderManager.GetManager())
      {
        if (str == "export")
          this.ExportModule(manager, moduleId, context);
        else if (str == "import")
        {
          try
          {
            this.ImportModule(context, moduleId);
          }
          catch (Exception ex)
          {
            ex.Data[(object) "Message"] = (object) ex.Message;
            string s = JsonSerializer.SerializeToString<IDictionary>(ex.Data);
            context.Response.Write(s);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
          }
        }
        else
          context.Response.Write("Specified action is not supported.");
      }
    }

    private void ImportModule(HttpContextBase context, Guid moduleId)
    {
      using (ZipFile dynamicModuleZip = ZipFile.Read(context.Request.Files[0].InputStream))
        ModuleImporter.ImportModule(dynamicModuleZip, moduleId);
    }

    private void ExportModule(
      ModuleBuilderManager moduleBuilderManager,
      Guid moduleId,
      HttpContextBase context)
    {
      DynamicModule dynamicModule = moduleBuilderManager.GetDynamicModules().Where<DynamicModule>((Expression<Func<DynamicModule, bool>>) (m => m.Id == moduleId)).SingleOrDefault<DynamicModule>();
      if (dynamicModule == null)
        context.Response.Write("Module that is to be exported cannot be found.");
      moduleBuilderManager.LoadDynamicModuleGraph(dynamicModule);
      context.Response.ContentType = "application/zip";
      context.Response.AddHeader(SitefinityExtensions.RemoveCRLF("Content-Disposition"), SitefinityExtensions.RemoveCRLF("attachment; filename={0}.zip".Arrange((object) dynamicModule.Name)));
      ModuleExporter.ExportModuleAsZip(dynamicModule, context.Response.OutputStream);
    }
  }
}
