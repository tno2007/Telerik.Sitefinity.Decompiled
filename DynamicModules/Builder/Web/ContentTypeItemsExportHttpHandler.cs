// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Web.ContentTypeItemsExportHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Telerik.Sitefinity.Data.Utilities.Exporters;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Builder.Web
{
  public class ContentTypeItemsExportHttpHandler : IHttpHandler
  {
    private DynamicModule dynamicModule;

    /// <summary>Gets or set the module name.</summary>
    /// <value>The name of the form.</value>
    [Obsolete("The type of the exported items is now loaded dynamicaly.")]
    public string ContentType { get; set; }

    /// <summary>Gets or sets the file to export format.</summary>
    /// <value>The file to export format.</value>
    public string FileToExportFormat { get; set; }

    /// <summary>Gets or sets the type of the exporting items.</summary>
    /// <value>The type of the exporting items.</value>
    [Obsolete("The type of the exported items is now loaded dynamicaly.")]
    public string ExportingItemsTypeName { get; set; }

    /// <summary>Gets or sets the content type id.</summary>
    /// <value>The content type id.</value>
    public Guid ContentTypeId { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
    public bool IsReusable => false;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      this.InitializeParameters(context);
      if (this.dynamicModule == null)
        return;
      SystemManager.CurrentHttpContext.Items[(object) "ExportingItemsTypeName"] = (object) null;
      SystemManager.CurrentHttpContext.Items[(object) "SheetName"] = (object) null;
      DynamicModuleManager manager = DynamicModuleManager.GetManager(this.ProviderName);
      IEnumerable<IDataItem> itemsToExport = this.GetItemsToExport(manager);
      if (itemsToExport.Count<IDataItem>() == 0)
        return;
      string fileName = this.GetFileName(manager.Provider.Title, this.dynamicModule.Title);
      IDataItemExporter exporter = DataItemExporter.GetExporter(this.FileToExportFormat);
      exporter.FileName = fileName;
      exporter.ConfigureResponse(context.ApplicationInstance.Context.Response);
      exporter.ExportToStream(context.ApplicationInstance.Context.Response.OutputStream, itemsToExport, context.ApplicationInstance.Context.Response.ContentEncoding);
    }

    private void InitializeParameters(HttpContextBase context)
    {
      this.FileToExportFormat = this.GetQueryStringValue(context, "exportAs", true);
      this.ProviderName = this.GetQueryStringValue(context, "provider", false);
      string input = context.Request.RequestContext.RouteData.Values["moduleId"].ToString();
      if (input.IsNullOrEmpty())
        return;
      ModuleBuilderManager manager = ModuleBuilderManager.GetManager();
      Guid result;
      Guid.TryParse(input, out result);
      this.dynamicModule = manager.GetDynamicModule(result);
      if (this.dynamicModule == null)
        return;
      manager.LoadDynamicModuleGraph(this.dynamicModule);
    }

    private string GetQueryStringValue(
      HttpContextBase context,
      string queryStringParamName,
      bool required)
    {
      string str = context.Request.QueryString[queryStringParamName];
      return !(string.IsNullOrEmpty(str) & required) ? str : throw new ArgumentNullException(queryStringParamName);
    }

    private IEnumerable<IDataItem> GetItemsToExport(DynamicModuleManager manager)
    {
      SystemManager.CurrentContext.Culture = CultureInfo.InvariantCulture;
      List<IDataItem> itemsToExport = new List<IDataItem>();
      foreach (DynamicModuleType type in this.dynamicModule.Types)
      {
        string name = string.Format("{0}.{1}", (object) type.TypeNamespace, (object) type.TypeName);
        IEnumerable<DynamicContent> collection = manager.GetDataItems(TypeResolutionService.ResolveType(name)).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (item => (int) item.Status == 2 && item.Visible)).ToList<DynamicContent>().Where<DynamicContent>((Func<DynamicContent, bool>) (item => !item.IsDeleted));
        itemsToExport.AddRange((IEnumerable<IDataItem>) collection);
      }
      return (IEnumerable<IDataItem>) itemsToExport;
    }

    private string GetFileName(string providerTitle, string dynamicModuleTitle)
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append(providerTitle);
      if (!stringBuilder.ToString().EndsWith(dynamicModuleTitle))
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append("_");
        stringBuilder.Append(dynamicModuleTitle);
      }
      stringBuilder.Replace(" ", "_");
      stringBuilder.Append("_");
      stringBuilder.Append(Res.Get<DynamicModuleResources>().Items);
      return Regex.Replace(stringBuilder.ToString(), "[^a-zA-Z0-9_\\s-]+", "").Trim().Trim('_');
    }
  }
}
