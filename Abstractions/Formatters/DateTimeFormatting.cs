// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Formatters.DateTimeFormatting
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Abstractions.Formatters
{
  /// <summary>
  /// A helper class that contains basic methods for retrieving user
  /// friendly date time elements for instances of type <see cref="T:Telerik.Sitefinity.Abstractions.Formatters.RelativeDateFormatter" />.
  /// </summary>
  internal class DateTimeFormatting
  {
    private DateTimeStrings dateTimeStrings;

    /// <summary>Gets or sets the date time strings.</summary>
    /// <value>The date time strings.</value>
    public DateTimeStrings DateTimeStrings
    {
      get
      {
        if (this.dateTimeStrings == null)
        {
          this.dateTimeStrings = new DateTimeStrings();
          this.dateTimeStrings.InitializeDefaultStrings();
        }
        return this.dateTimeStrings;
      }
      set => this.dateTimeStrings = value;
    }

    /// <summary>Gets the and string.</summary>
    /// <returns></returns>
    public string GetAndString() => string.Format(" {0} ", (object) this.DateTimeStrings.And);

    /// <summary>Gets the ago string.</summary>
    /// <returns></returns>
    public string GetAgoString() => string.Format(" {0}", (object) this.DateTimeStrings.Ago);

    /// <summary>Gets the days string.</summary>
    /// <param name="days">The days.</param>
    /// <returns></returns>
    public string GetDaysString(int days)
    {
      if (days == 0)
        return string.Empty;
      return days == 1 ? string.Format("1 {0}", (object) this.DateTimeStrings.Day) : string.Format("{0:0} {1}", (object) days, (object) this.DateTimeStrings.Days);
    }

    /// <summary>Gets the hours string.</summary>
    /// <param name="hours">The hours.</param>
    /// <returns></returns>
    public string GetHoursString(int hours)
    {
      if (hours == 0)
        return string.Empty;
      return hours == 1 ? string.Format("1 {0}", (object) this.DateTimeStrings.Hour) : string.Format("{0:0} {1}", (object) hours, (object) this.DateTimeStrings.Hours);
    }

    /// <summary>Gets the minutes string.</summary>
    /// <param name="minutes">The minutes.</param>
    /// <returns></returns>
    public string GetMinutesString(int minutes)
    {
      if (minutes == 0)
        return string.Empty;
      return minutes == 1 ? string.Format("1 {0}", (object) this.DateTimeStrings.Minute) : string.Format("{0:0} {1}", (object) minutes, (object) this.DateTimeStrings.Minutes);
    }

    /// <summary>Gets the seconds string.</summary>
    /// <param name="seconds">The seconds.</param>
    /// <returns></returns>
    public string GetSecondsString(int seconds)
    {
      if (seconds == 0)
        return string.Empty;
      return seconds == 1 ? string.Format("1 {0}", (object) this.DateTimeStrings.Second) : string.Format("{0:0} {1}", (object) seconds, (object) this.DateTimeStrings.Seconds);
    }

    /// <summary>Gets the day month format.</summary>
    /// <returns></returns>
    public string GetDayMonthFormat() => this.DateTimeStrings.DayMonthFormat;

    /// <summary>Gets the day month year format.</summary>
    /// <returns></returns>
    public string GetDayMonthYearFormat() => this.DateTimeStrings.DayMonthYearFormat;
  }
}
