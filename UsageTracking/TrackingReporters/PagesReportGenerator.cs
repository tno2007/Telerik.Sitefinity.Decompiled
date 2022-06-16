// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.TrackingReporters.PagesReportGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.UsageTracking.Model;

namespace Telerik.Sitefinity.UsageTracking.TrackingReporters
{
  internal class PagesReportGenerator
  {
    internal const string PagesModuleName = "PagesModule";

    public PagesReportModel GenerateReport()
    {
      PagesReportModel report = new PagesReportModel()
      {
        ModuleName = "PagesModule"
      };
      Guid rootId = DataExtensions.AppSettings.BackendRootNodeId;
      PageManager manager = PageManager.GetManager();
      IQueryable<PageNode> source = manager.GetPageNodes().Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.Id != rootId && p.RootNodeId != rootId && !p.IsDeleted)).Include<PageNode>((Expression<Func<PageNode, object>>) (n => n.PageDataList));
      int num = source.Count<PageNode>();
      report.TotalPagesCount = num;
      report.RedirectPagesCount = source.Count<PageNode>((Expression<Func<PageNode, bool>>) (p => (int) p.NodeType == 3 || (int) p.NodeType == 4));
      report.GroupPagesCount = source.Count<PageNode>((Expression<Func<PageNode, bool>>) (p => (int) p.NodeType == 1));
      IQueryable<PageNode> queryable = source.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => (int) p.NodeType == 0));
      report.StandardPagesCount = queryable.Count<PageNode>();
      report.MvcPages = this.GetPageReport(queryable, PageTemplateFramework.Mvc);
      report.HybridPages = this.GetPageReport(queryable, PageTemplateFramework.Hybrid, true);
      report.WebFormsPages = this.GetPageReport(queryable, PageTemplateFramework.WebForms);
      report.RendererPages = this.GetPageReportForRenderer(queryable);
      report.IsInlineEditingUsed = this.GetInlineEditingSettings();
      report.WidgetsInfo = this.GetWidgetsReport(manager, queryable);
      return report;
    }

    private WidgetsReportModel GetWidgetsReport(
      PageManager pageManager,
      IQueryable<PageNode> standardPages)
    {
      WidgetsReportModel widgetsModel = new WidgetsReportModel();
      IQueryable<PageControl> controls1 = pageManager.GetControls<PageControl>();
      IQueryable<TemplateControl> controls2 = pageManager.GetControls<TemplateControl>();
      this.PopulateWebFormsWidgetsInfo(widgetsModel, controls1, controls2);
      this.PopulteMvcWidgetsInfo(widgetsModel, controls1, controls2);
      this.PopulateRendererWidgetsInfo(widgetsModel, controls1);
      return widgetsModel;
    }

    private void PopulteMvcWidgetsInfo(
      WidgetsReportModel widgetsModel,
      IQueryable<PageControl> pageControlData,
      IQueryable<TemplateControl> templateControlData)
    {
      widgetsModel.MvcWidgetsInTemplate = (IDictionary<string, int>) templateControlData.Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc"))).Include<TemplateControl>((Expression<Func<TemplateControl, object>>) (x => x.Properties)).Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value.StartsWith("Telerik.Sitefinity"))))).GroupBy<TemplateControl, string>((Expression<Func<TemplateControl, string>>) (cd => cd.Caption)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<TemplateControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
      widgetsModel.MvcWidgetsInPage = (IDictionary<string, int>) pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc"))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Properties)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && p.Value.StartsWith("Telerik.Sitefinity"))))).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (cd => cd.Caption)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
      Dictionary<string, int> dictionary1 = templateControlData.Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc"))).Include<TemplateControl>((Expression<Func<TemplateControl, object>>) (x => x.Properties)).Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && !p.Value.StartsWith("Telerik.Sitefinity"))))).GroupBy<TemplateControl, string>((Expression<Func<TemplateControl, string>>) (cd => cd.Caption)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<TemplateControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
      Dictionary<string, int> dictionary2 = pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc"))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Properties)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.Properties.Any<ControlProperty>((Func<ControlProperty, bool>) (p => p.Name == "ControllerName" && !p.Value.StartsWith("Telerik.Sitefinity"))))).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (cd => cd.Caption)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
      widgetsModel.CustomMvcWidgets = (IDictionary<string, int>) dictionary2.Concat<KeyValuePair<string, int>>((IEnumerable<KeyValuePair<string, int>>) dictionary1).ToLookup<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (pair => pair.Key), (Func<KeyValuePair<string, int>, int>) (pair => pair.Value)).ToDictionary<IGrouping<string, int>, string, int>((Func<IGrouping<string, int>, string>) (group => group.Key), (Func<IGrouping<string, int>, int>) (group => group.Sum()));
    }

    private void PopulateWebFormsWidgetsInfo(
      WidgetsReportModel widgetsModel,
      IQueryable<PageControl> pageControlData,
      IQueryable<TemplateControl> templateControlData)
    {
      widgetsModel.WebFormsWidgetsInTemplate = (IDictionary<string, int>) templateControlData.Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity") && !cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc") && !cd.ObjectType.StartsWith("Telerik.Sitefinity.Frontend"))).Include<TemplateControl>((Expression<Func<TemplateControl, object>>) (x => x.Page)).Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => cd.Page.Category != SiteInitializer.BackendRootNodeId)).GroupBy<TemplateControl, string>((Expression<Func<TemplateControl, string>>) (cd => cd.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<TemplateControl>()
      }).ToDictionary(g => g.Key, g => g.Count);
      widgetsModel.WebFormsWidgetsInPage = (IDictionary<string, int>) pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.ObjectType.StartsWith("Telerik.Sitefinity") && !cd.ObjectType.StartsWith("Telerik.Sitefinity.Mvc") && !cd.ObjectType.StartsWith("Telerik.Sitefinity.Frontend"))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Page)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => ((IRendererCommonData) cd.Page).Renderer == default (string))).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.Page.NavigationNode.RootNodeId != SiteInitializer.BackendRootNodeId)).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (t => t.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(g => g.Key, g => g.Count);
      Dictionary<string, int> dictionary1 = templateControlData.Where<TemplateControl>((Expression<Func<TemplateControl, bool>>) (cd => !cd.ObjectType.StartsWith("Telerik.Sitefinity"))).GroupBy<TemplateControl, string>((Expression<Func<TemplateControl, string>>) (cd => cd.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<TemplateControl>()
      }).ToDictionary(g => g.Key, g => g.Count);
      Dictionary<string, int> dictionary2 = pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => !cd.ObjectType.StartsWith("Telerik.Sitefinity"))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Page)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => ((IRendererCommonData) cd.Page).Renderer == default (string))).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (t => t.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(g => g.Key, g => g.Count);
      widgetsModel.CustomWebFormsWidgets = (IDictionary<string, int>) dictionary2.Concat<KeyValuePair<string, int>>((IEnumerable<KeyValuePair<string, int>>) dictionary1).ToLookup<KeyValuePair<string, int>, string, int>((Func<KeyValuePair<string, int>, string>) (pair => pair.Key), (Func<KeyValuePair<string, int>, int>) (pair => pair.Value)).ToDictionary<IGrouping<string, int>, string, int>((Func<IGrouping<string, int>, string>) (group => group.Key), (Func<IGrouping<string, int>, int>) (group => group.Sum()));
    }

    private void PopulateRendererWidgetsInfo(
      WidgetsReportModel widgetsModel,
      IQueryable<PageControl> pageControlData)
    {
      string[] existingRendererWidgets = new string[5]
      {
        "ContentBlock",
        "Section",
        "Image",
        "Button",
        "Navigation"
      };
      widgetsModel.RendererWidgets = (IDictionary<string, int>) pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => existingRendererWidgets.Contains<string>(cd.ObjectType))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Page)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.Page.Template == default (object) && ((IRendererCommonData) cd.Page).Renderer != default (string))).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (cd => cd.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
      widgetsModel.CustomRendererWidgets = (IDictionary<string, int>) pageControlData.Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => !existingRendererWidgets.Contains<string>(cd.ObjectType))).Include<PageControl>((Expression<Func<PageControl, object>>) (x => x.Page)).Where<PageControl>((Expression<Func<PageControl, bool>>) (cd => cd.Page.Template == default (object) && ((IRendererCommonData) cd.Page).Renderer != default (string))).GroupBy<PageControl, string>((Expression<Func<PageControl, string>>) (cd => cd.ObjectType)).Select(g => new
      {
        Key = g.Key,
        Count = g.Count<PageControl>()
      }).ToDictionary(c => c.Key, c => c.Count);
    }

    private bool GetInlineEditingSettings()
    {
      PagesConfig pagesConfig = Config.Get<PagesConfig>();
      return pagesConfig.EnableBrowseAndEdit.HasValue && pagesConfig.EnableBrowseAndEdit.HasValue && pagesConfig.EnableBrowseAndEdit.Value;
    }

    private PageReportModel GetPageReport(
      IQueryable<PageNode> standardPages,
      PageTemplateFramework framework,
      bool includeItemsWithoutTemplate = false)
    {
      IQueryable<PageNode> queryable = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => (int) pd.Template.Framework == (int) framework))));
      int num1 = queryable.Count<PageNode>();
      int num2 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Visible && (int) pd.Template.Framework == (int) framework)))).Count<PageNode>();
      if (includeItemsWithoutTemplate)
      {
        int num3 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Template == default (object))))).Count<PageNode>();
        int num4 = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Visible && pd.Template == default (object))))).Count<PageNode>();
        num1 += num3;
        num2 += num4;
      }
      return new PageReportModel()
      {
        TotalCount = num1,
        LiveCount = num2,
        PageCacheSettings = new PageCacheSettings()
        {
          StandardCaching = this.GetCountOfPagesWithCacheProfile(queryable, "Standard Caching"),
          NoCaching = this.GetCountOfPagesWithCacheProfile(queryable, "No Caching"),
          LongCaching = this.GetCountOfPagesWithCacheProfile(queryable, "Long Caching"),
          AnyLocation = this.GetCountOfPagesWithCacheProfile(queryable, "Any Location"),
          AsSetForTheWholeSite = this.GetCountOfPagesWithCacheProfile(queryable, string.Empty)
        }
      };
    }

    private RendererPageReportModel GetPageReportForRenderer(
      IQueryable<PageNode> standardPages)
    {
      IQueryable<PageNode> source = standardPages.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => ((IRendererCommonData) pd).Renderer != default (string)))));
      int num1 = source.Count<PageNode>();
      int num2 = source.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.Visible)))).Count<PageNode>();
      IEnumerable<string> strings = ((IEnumerable<string>) source.Select<PageNode, PageData>((Expression<Func<PageNode, PageData>>) (p => p.PageDataList.First<PageData>((Func<PageData, bool>) (pd => ((IRendererCommonData) pd).Renderer != default (string))))).Select<PageData, string>((Expression<Func<PageData, string>>) (x => ((IRendererCommonData) x).Renderer)).ToArray<string>()).Distinct<string>();
      Dictionary<string, int> dictionary = new Dictionary<string, int>();
      foreach (string str in strings)
      {
        string renderer = str;
        int num3 = source.Count<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => ((IRendererCommonData) pd).Renderer == renderer))));
        dictionary.Add(renderer, num3);
      }
      return new RendererPageReportModel()
      {
        TotalCount = num1,
        LiveCount = num2,
        PagesByRenderer = dictionary
      };
    }

    private int GetCountOfPagesWithCacheProfile(
      IQueryable<PageNode> pagesQuery,
      string cacheProfile)
    {
      return pagesQuery.Where<PageNode>((Expression<Func<PageNode, bool>>) (p => p.PageDataList.Any<PageData>((Func<PageData, bool>) (pd => pd.OutputCacheProfile == cacheProfile)))).Count<PageNode>();
    }
  }
}
