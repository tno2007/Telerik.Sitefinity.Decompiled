// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.WidgetsReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class WidgetsReportModel
  {
    public IDictionary<string, int> MvcWidgetsInPage { get; set; }

    public IDictionary<string, int> WebFormsWidgetsInPage { get; set; }

    public IDictionary<string, int> MvcWidgetsInTemplate { get; set; }

    public IDictionary<string, int> WebFormsWidgetsInTemplate { get; set; }

    public IDictionary<string, int> CustomMvcWidgets { get; set; }

    public IDictionary<string, int> CustomWebFormsWidgets { get; set; }

    public IDictionary<string, int> CustomRendererWidgets { get; set; }

    public IDictionary<string, int> RendererWidgets { get; set; }
  }
}
