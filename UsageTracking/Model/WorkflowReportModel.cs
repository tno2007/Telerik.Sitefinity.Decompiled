// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.Model.WorkflowsReportModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.UsageTracking.Model
{
  internal class WorkflowsReportModel
  {
    public string ModuleName { get; set; }

    public List<WorkflowsReportModel.WorkflowReportModel> Workflows { get; set; }

    public class WorkflowReportModel
    {
      public int LevelsCount { get; set; }

      public List<WorkflowsReportModel.WorkflowScopeReportModel> Scopes { get; set; }
    }

    public class WorkflowScopeReportModel
    {
      public bool ForSpecificSite { get; set; }

      public bool ForSpecificLanguage { get; set; }

      public List<string> ContentTypes { get; set; }
    }
  }
}
