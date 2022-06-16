// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Report.FormsModuleReport
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.Forms.Report
{
  internal class FormsModuleReport
  {
    public string ModuleName { get; set; }

    public int MvcBasedFormsCount { get; set; }

    public int WebFormsBasedFormsCount { get; set; }

    public IEnumerable<FieldTracking> FormFields { get; set; }

    public FormRulesTracking FormRules { get; set; }

    public IEnumerable<Telerik.Sitefinity.Modules.Forms.Report.ConnectorTracking> ConnectorTracking { get; set; }
  }
}
