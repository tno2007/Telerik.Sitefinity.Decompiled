// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.SplitterResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Web.UrlEvaluation;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  /// Resolves the splitter that will be used for splitting a period to time intervals.
  /// </summary>
  public class SplitterResolver
  {
    private BaseIntervalSplitter splitter;

    /// <summary>
    /// Represents the method that retrieve a list of time intervals intervals for a period between start and end date.
    /// </summary>
    /// <param name="startDate">The start date.</param>
    /// <param name="endDate">The end date.</param>
    /// <param name="dateBuildOptions">The grouping date options.</param>
    /// <returns></returns>
    public IList<TrackedInterval> GetIntervals(
      DateTime startDate,
      DateTime endDate,
      DateBuildOptions dateBuildOptions)
    {
      if (DateTime.Compare(startDate, endDate) > 0)
        throw new ArgumentException("The start date should be earlier than the end date.");
      this.SetSplitter(dateBuildOptions);
      this.splitter.StartDate = startDate;
      this.splitter.EndDate = endDate;
      return this.splitter.GetIntervals();
    }

    /// <summary>
    /// Resolves the splitter to be used for time intervals based on grouping date time option.
    /// </summary>
    /// <param name="dateBuildOptions">The date build options.</param>
    /// <returns></returns>
    public void SetSplitter(DateBuildOptions dateBuildOptions)
    {
      switch (dateBuildOptions)
      {
        case DateBuildOptions.YearMonthDay:
          this.splitter = (BaseIntervalSplitter) new DaysIntervalSplitter();
          break;
        case DateBuildOptions.YearMonth:
          this.splitter = (BaseIntervalSplitter) new MonthsIntervalSplitter();
          break;
        case DateBuildOptions.Year:
          this.splitter = (BaseIntervalSplitter) new YearsIntervalSplitter();
          break;
        default:
          throw new NotImplementedException(string.Format("For date build opition {0} a splitter has not been implemented.", (object) dateBuildOptions.ToString()));
      }
    }
  }
}
