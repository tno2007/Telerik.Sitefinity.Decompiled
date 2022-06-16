// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Formatters.RelativeDateFormatter
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Text;

namespace Telerik.Sitefinity.Abstractions.Formatters
{
  /// <summary>
  /// Helper class for user friendly relative date message for duration period between 2 dates.
  /// </summary>
  internal class RelativeDateFormatter
  {
    private DateTimeFormatting format;

    /// <summary>Gets or sets the format.</summary>
    /// <value>The format.</value>
    public DateTimeFormatting Format
    {
      get
      {
        if (this.format == null)
          this.format = new DateTimeFormatting();
        return this.format;
      }
      set => this.format = value;
    }

    /// <summary>
    /// Gets the duration between start time in parameter and end date time.
    /// </summary>
    /// <param name="forums">The forums.</param>
    /// <param name="startUTCDateTime">The start UTC date time.</param>
    /// <remarks>
    /// The logic of this method goes as follows:
    /// 1) If less than 1 hour =&gt; x minutes ago.
    /// 2) If less than 24 hours -&gt; x hours ago.
    /// 3) If more than 24 hours but less than 6 days =&gt; x day/s and x hours ago
    /// 4) If more than 6 days and in the same year =&gt; Date format such as 15 Feb, 10 Dec etc.
    /// 5) If not in the same year =&gt; Date format such as 15 Feb 2010.
    /// </remarks>
    /// <returns></returns>
    public virtual string ToRelativeDate(DateTime timestamp, DateTime now)
    {
      TimeSpan timeSpan = now - timestamp;
      bool flag = true;
      StringBuilder stringBuilder = new StringBuilder();
      if (timeSpan.TotalMinutes < 1.0)
      {
        stringBuilder.Append(this.Format.DateTimeStrings.LessThanAMintueAgo);
        flag = false;
      }
      else if (timeSpan.TotalHours <= 1.0)
        stringBuilder.Append(this.Format.GetMinutesString(timeSpan.Minutes));
      else if (timeSpan.TotalHours <= 24.0)
        stringBuilder.Append(this.Format.GetHoursString(timeSpan.Hours));
      else if (timeSpan.TotalHours > 24.0 && timeSpan.TotalDays <= 6.0)
      {
        stringBuilder.Append(this.Format.GetDaysString(timeSpan.Days));
        string hoursString = this.Format.GetHoursString(timeSpan.Hours);
        if (!string.IsNullOrEmpty(hoursString))
        {
          stringBuilder.Append(this.Format.GetAndString());
          stringBuilder.Append(hoursString);
        }
      }
      else if (timeSpan.TotalDays > 6.0 && timestamp.Year == now.Year)
      {
        flag = false;
        stringBuilder.Append(timestamp.ToString(this.Format.GetDayMonthFormat()));
      }
      else if (timestamp.Year != now.Year)
      {
        flag = false;
        stringBuilder.Append(timestamp.ToString(this.Format.GetDayMonthYearFormat()));
      }
      if (flag)
        stringBuilder.Append(this.Format.GetAgoString());
      return stringBuilder.ToString();
    }
  }
}
