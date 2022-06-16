// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.JS.PageStatisticsJSClient
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Web.Hosting;
using System.Web.UI;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Statistics;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Clients.JS
{
  /// <summary>
  /// Represents a java script client for logging the visits to Sitefinity pages.
  /// </summary>
  [Obsolete("For tracking user interactions on your web site use Sitefinity Insight.")]
  public class PageStatisticsJSClient : Control
  {
    /// <summary>
    /// Sends server control content to a provided <see cref="T:System.Web.UI.HtmlTextWriter" />
    /// object, which writes the content to be rendered on the client.
    /// </summary>
    /// <param name="writer">The <see cref="T:System.Web.UI.HtmlTextWriter" /> object
    /// that receives the server control content.</param>
    protected override void Render(HtmlTextWriter writer)
    {
      switch (SystemManager.GetStatisticsService().GetPageLogMode())
      {
        case PageVisitLogMode.None:
          return;
        case PageVisitLogMode.PersonalizationTargets:
          if (!this.IsCurrentPagePersonalizationTarget())
            return;
          break;
      }
      PageSiteNode currentNode = SiteMapBase.GetCurrentNode();
      if (currentNode == null || currentNode.IsBackend || currentNode.Framework != PageTemplateFramework.Mvc)
        return;
      string webResourceUrl = this.Page.ClientScript.GetWebResourceUrl(typeof (PageStatisticsJSClient), "Telerik.Sitefinity.Clients.JS.StatsClient.min.js");
      if (!string.IsNullOrEmpty(webResourceUrl))
      {
        writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
        writer.AddAttribute(HtmlTextWriterAttribute.Src, webResourceUrl);
        writer.RenderBeginTag(HtmlTextWriterTag.Script);
        writer.RenderEndTag();
      }
      string str = "StatsClient.LogVisit('" + (object) currentNode.Id + "');";
      writer.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
      writer.RenderBeginTag(HtmlTextWriterTag.Script);
      writer.Write(str);
      writer.RenderEndTag();
    }

    /// <summary>
    /// Called by the ASP.NET page framework to notify server controls that
    /// use composition-based implementation to create any child controls they contain
    /// in preparation for posting back or rendering.
    /// </summary>
    protected override void CreateChildControls()
    {
      base.CreateChildControls();
      string applicationVirtualPath = HostingEnvironment.ApplicationVirtualPath;
      if (!applicationVirtualPath.EndsWith("/"))
        applicationVirtualPath += "/";
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "start-stats-log", "var sf_appPath='" + applicationVirtualPath + "';", true);
      this.Page.ClientScript.RegisterClientScriptResource(typeof (PageStatisticsJSClient), "Telerik.Sitefinity.Clients.JS.StatsClient.min.js");
      this.RegisterLogVisitsScript();
    }

    private void RegisterLogVisitsScript()
    {
      switch (SystemManager.GetStatisticsService().GetPageLogMode())
      {
        case PageVisitLogMode.None:
          return;
        case PageVisitLogMode.PersonalizationTargets:
          if (!this.IsCurrentPagePersonalizationTarget())
            return;
          break;
      }
      PageSiteNode currentNode = SiteMapBase.GetCurrentNode();
      if (currentNode == null || currentNode.IsBackend)
        return;
      string script = string.Format("StatsClient.LogVisit('{0}')", (object) currentNode.Id);
      this.Page.ClientScript.RegisterStartupScript(this.GetType(), "statsClient.LogVisit", script, true);
    }

    private bool IsCurrentPagePersonalizationTarget()
    {
      PageSiteNode currentNode = SiteMapBase.GetCurrentNode();
      if (currentNode == null)
        return false;
      string absoluteUri = SystemManager.CurrentHttpContext.Request.Url.AbsoluteUri;
      return SystemManager.GetPersonalizationService().IsPagePersonalizationTarget(currentNode.Id, absoluteUri);
    }
  }
}
