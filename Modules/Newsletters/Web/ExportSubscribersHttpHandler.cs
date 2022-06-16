// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Web.ExportSubscribersHttpHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Web
{
  /// <summary>
  /// Http handler that exports subscribers according to specified parameters.
  /// </summary>
  public class ExportSubscribersHttpHandler : IHttpHandler
  {
    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.
    /// </summary>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, false.</returns>
    public bool IsReusable => true;

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the <see cref="T:System.Web.IHttpHandler" /> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, Request, Response, Session, and Server) used to service HTTP requests.</param>
    public void ProcessRequest(HttpContext context) => this.ProcessRequest((HttpContextBase) new HttpContextWrapper(context));

    /// <summary>Processes the request.</summary>
    /// <param name="context">The context.</param>
    /// <exception cref="T:System.Web.HttpException">403;Request denied</exception>
    public void ProcessRequest(HttpContextBase context)
    {
      if (SystemManager.CurrentHttpContext != null)
      {
        if (SystemManager.CurrentHttpContext.User.Identity.IsAuthenticated && SecurityManager.IsBackendUser())
        {
          if (AppPermission.IsGranted(AppAction.ManageNewsletters))
            goto label_4;
        }
        throw new HttpException(403, "Request denied");
      }
label_4:
      SubscriberExporter subscriberExporter = new SubscriberExporter();
      string str1 = (string) null;
      string str2 = (string) null;
      string s = (string) null;
      bool flag = false;
      bool result = false;
      bool.TryParse(context.Request.QueryString["isCSV"], out result);
      Guid[] listIds = (Guid[]) null;
      if (!string.IsNullOrEmpty(context.Request.QueryString["ids"]))
      {
        string[] strArray = context.Request.QueryString["ids"].Split(new string[1]
        {
          ","
        }, StringSplitOptions.RemoveEmptyEntries);
        List<Guid> guidList = new List<Guid>();
        foreach (string g in strArray)
          guidList.Add(new Guid(g));
        listIds = guidList.ToArray();
      }
      bool doNotExportSubscribersSameEmails = bool.Parse(context.Request.QueryString["exportExSub"]);
      bool shouldExportAllSubscribers = bool.Parse(context.Request.QueryString["allSub"]);
      if (result)
      {
        string commaSeparatedList = subscriberExporter.ExportToCommaSeparatedList(listIds, doNotExportSubscribersSameEmails, shouldExportAllSubscribers);
        if (!string.IsNullOrEmpty(commaSeparatedList))
        {
          str1 = "application/CSV";
          str2 = "csv";
          flag = true;
          s = commaSeparatedList;
        }
      }
      else
      {
        string tabSeparatedList = subscriberExporter.ExportToTabSeparatedList(listIds, doNotExportSubscribersSameEmails, shouldExportAllSubscribers);
        if (!string.IsNullOrEmpty(tabSeparatedList))
        {
          str1 = "text/plain";
          str2 = "txt";
          flag = true;
          s = tabSeparatedList;
        }
      }
      if (!flag)
        return;
      HttpResponseBase response = context.Response;
      response.Clear();
      response.Buffer = true;
      if (!flag)
        return;
      response.ContentEncoding = Encoding.UTF8;
      response.Charset = "utf-8";
      response.ContentType = str1;
      response.AddHeader("Content-Disposition", string.Format("attachment;filename=\"Subscribers.{0}\"", (object) str2));
      response.Write(s);
    }
  }
}
