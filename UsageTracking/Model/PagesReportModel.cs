// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.PagesReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class PagesReportModel
  {
    public string ModuleName { get; set; }

    public bool IsInlineEditingUsed { get; set; }

    public int TotalPagesCount { get; set; }

    public int StandardPagesCount { get; set; }

    public int RedirectPagesCount { get; set; }

    public int GroupPagesCount { get; set; }

    public PageReportModel MvcPages { get; set; }

    public PageReportModel HybridPages { get; set; }

    public PageReportModel WebFormsPages { get; set; }

    public RendererPageReportModel RendererPages { get; set; }

    public WidgetsReportModel WidgetsInfo { get; set; }
  }
}
