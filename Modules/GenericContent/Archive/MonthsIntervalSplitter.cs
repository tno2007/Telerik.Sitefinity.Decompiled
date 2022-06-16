// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.MonthsIntervalSplitter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  ///  The class splits a time period at 1 months time intervals.
  /// </summary>
  public class MonthsIntervalSplitter : BaseIntervalSplitter
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
    public override DateTime IncrementDate(DateTime dateTime, int steps) => dateTime.AddMonths(steps);
  }
}
