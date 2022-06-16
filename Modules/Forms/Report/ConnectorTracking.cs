// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Forms.Report.ConnectorTracking
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Modules.Forms.Report
{
  /// <summary>
  /// Represents a model for tracking connectors linked with a form
  /// </summary>
  internal class ConnectorTracking
  {
    /// <summary>Gets or sets the connector name</summary>
    public string ConnectorName { get; set; }

    /// <summary>Gets or sets the forms count</summary>
    public int FormsCount { get; set; }

    /// <summary>Gets or sets the form responses count</summary>
    public int FormResponsesCount { get; set; }
  }
}
