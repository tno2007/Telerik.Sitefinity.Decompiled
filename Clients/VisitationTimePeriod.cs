// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Clients.VisitationTimePeriod
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Clients
{
  /// <summary>Defines supported periods of visitation.</summary>
  public enum VisitationTimePeriod
  {
    /// <summary>Visits that happend last day</summary>
    LastDay,
    /// <summary>Visits that happened last week</summary>
    LastWeek,
    /// <summary>Visits that happened last month</summary>
    LastMonth,
    /// <summary>Visits that happend last year</summary>
    LastYear,
    /// <summary>
    /// Visits that happened since the beginning of the tracking
    /// </summary>
    AllTime,
  }
}
