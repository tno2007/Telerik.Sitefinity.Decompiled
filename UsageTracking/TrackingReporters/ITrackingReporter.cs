// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.UsageTracking.TrackingReporters.ITrackingReporter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.UsageTracking.TrackingReporters
{
  /// <summary>
  /// Interface for reporters that will generate specific report for the the usage details of specific sitefinity part
  /// </summary>
  public interface ITrackingReporter
  {
    /// <summary>
    /// Generate the tracking report describing the usage details of specific sitefinity part
    /// </summary>
    /// <returns>The generated report</returns>
    object GetReport();
  }
}
