// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Web.FormEntriesExportHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Telerik.Sitefinity.Data.Utilities.Exporters;
using Telerik.Sitefinity.Forms.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Forms.Web
{
  /// <summary>
  /// Represents a route handler for exporting Sitefinity form entries.
  /// </summary>
  public class FormEntriesExportHttpHandler : IHttpHandler
  {
    private const string entriesIdsParamName = "entriesIds";
    private static string[] entriesIdsSeparator = new string[1]
    {
      ","
    };

    /// <summary>Gets or sets the name of the form.</summary>
    /// <value>The name of the form.</value>
    public string FormName { get; set; }

    /// <summary>Gets or sets the file to export format.</summary>
    /// <value>The file to export format.</value>
    public string FileToExportFormat { get; set; }

    /// <summary>Gets or sets the type of the exporting items.</summary>
    /// <value>The type of the exporting items.</value>
    public string ExportingItemsTypeName { get; set; }

    /// <summary>Gets or sets the name of the provider.</summary>
    /// <value>The name of the provider.</value>
    public string ProviderName { get; set; }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    public void ProcessRequest(HttpContextBase context)
    {
      this.InitializeParameters(context);
      SystemManager.CurrentHttpContext.Items[(object) "ExportingItemsTypeName"] = (object) this.ExportingItemsTypeName;
      SystemManager.CurrentHttpContext.Items[(object) "SheetName"] = (object) Res.Get<FormsResources>().FormEntries;
      IEnumerable<IDataItem> itemsToExport = this.GetItemsToExport(this.GetItemIdsToExport(context));
      if (itemsToExport.Count<IDataItem>() == 0)
        return;
      IDataItemExporter exporter = DataItemExporter.GetExporter(this.FileToExportFormat);
      exporter.FileName = string.Format("{0}_{1}_{2}", (object) this.FormName, (object) Res.Get<FormsResources>().Form, (object) Res.Get<FormsResources>().Responses);
      exporter.ConfigureResponse(context.ApplicationInstance.Context.Response);
      exporter.ExportToStream(context.ApplicationInstance.Context.Response.OutputStream, itemsToExport, context.ApplicationInstance.Context.Response.ContentEncoding);
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.
    /// </returns>
    public bool IsReusable => false;

    private void InitializeParameters(HttpContextBase context)
    {
      this.FormName = this.GetQueryStringValue(context, "formName", true);
      this.FileToExportFormat = this.GetQueryStringValue(context, "exportAs", true);
      this.ExportingItemsTypeName = this.GetQueryStringValue(context, "itemType", true);
      this.ProviderName = this.GetQueryStringValue(context, "provider", false);
    }

    private string GetQueryStringValue(
      HttpContextBase context,
      string queryStringParamName,
      bool required)
    {
      string str = context.Request.QueryString[queryStringParamName];
      return !(string.IsNullOrEmpty(str) & required) ? str : throw new ArgumentNullException(queryStringParamName);
    }

    private IEnumerable<IDataItem> GetItemsToExport(List<string> idsToExport)
    {
      FormsManager manager = FormsManager.GetManager(this.ProviderName);
      Guid[] idsToExportArray = this.ConstructIds(idsToExport);
      IQueryable<FormEntry> source = manager.GetFormEntries(this.ExportingItemsTypeName);
      if (idsToExportArray.Length != 0)
        source = source.Where<FormEntry>((Expression<Func<FormEntry, bool>>) (item => idsToExportArray.Contains<Guid>(item.Id)));
      source.ToList<FormEntry>().ForEach((Action<FormEntry>) (e => manager.ResolveFormEntrySourceSiteName(e)));
      return source.ToList<FormEntry>().Cast<IDataItem>();
    }

    private Guid[] ConstructIds(List<string> stringIds)
    {
      List<Guid> guidIds = new List<Guid>();
      stringIds.ForEach((Action<string>) (s => guidIds.Add(new Guid(s))));
      return guidIds.ToArray();
    }

    private List<string> GetItemIdsToExport(HttpContextBase context)
    {
      string str = context.Request["entriesIds"];
      return string.IsNullOrEmpty(str) ? new List<string>() : ((IEnumerable<string>) str.Split(FormEntriesExportHttpHandler.entriesIdsSeparator, StringSplitOptions.RemoveEmptyEntries)).ToList<string>();
    }
  }
}
