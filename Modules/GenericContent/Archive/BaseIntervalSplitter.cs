// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Archive.BaseIntervalSplitter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Modules.GenericContent.Archive
{
  /// <summary>
  /// Base abstract class that should be inherited for implementing a custom time frame splitter.
  /// </summary>
  public abstract class BaseIntervalSplitter
  {
    /// <summary>
    /// Gets or sets the start date of time frame interval to be split.
    /// </summary>
    /// <value>The start date.</value>
    public virtual DateTime StartDate { get; set; }

    /// <summary>
    /// Gets or sets the end date of time frame interval to be split.
    /// </summary>
    /// <value>The end date.</value>
    public virtual DateTime EndDate { get; set; }

    /// <summary>
    /// Retrieves a list with interval items for a time period defined with start date and end date.
    /// </summary>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Archive.TrackedInterval" /></returns>
    public virtual IList<TrackedInterval> GetIntervals()
    {
      List<TrackedInterval> intervals = new List<TrackedInterval>();
      for (DateTime dateTime = this.StartDate; DateTime.Compare(dateTime, this.EndDate) < 0; dateTime = this.IncrementDate(dateTime, 1))
        intervals.Add(new TrackedInterval()
        {
          StartDate = dateTime,
          EndDate = this.IncrementDate(dateTime, 1)
        });
      return (IList<TrackedInterval>) intervals;
    }

    /// <summary>
    /// Increments the date depending on the type of the splitter.
    /// </summary>
    /// <param name="dateTime">The date time.</param>
    /// <param name="steps">The steps.</param>
    /// <returns></returns>
    public abstract DateTime IncrementDate(DateTime dateTime, int steps);
  }
}
