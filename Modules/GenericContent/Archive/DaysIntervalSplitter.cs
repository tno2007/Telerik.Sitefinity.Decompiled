// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.DaysIntervalSplitter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  /// The class splits a time period at 1 day time intervals.
  /// </summary>
  public class DaysIntervalSplitter : BaseIntervalSplitter
  {
    private DateTime startDate;

    /// <summary>
    /// Gets or sets the start date of time frame interval to be splited.
    /// </summary>
    /// <value>The start date.</value>
    public override DateTime StartDate
    {
      get => this.startDate;
      set
      {
        DateTime dateTime = value;
        this.startDate = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
      }
    }

    /// <summary>
    /// Increments the date depending on the type of the splitter.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="steps">The steps.</param>
    /// <returns></returns>
    public override DateTime IncrementDate(DateTime dateTime, int steps)
    {
      int hour = dateTime.Hour;
      int minute = dateTime.Minute;
      int second = dateTime.Second;
      return hour == 23 || minute == 59 || second == 59 ? new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0) + new TimeSpan(steps, 0, 0, 0) : new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59) + new TimeSpan(steps - 1, 0, 0, 0);
    }
  }
}
