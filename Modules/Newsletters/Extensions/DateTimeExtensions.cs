// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.DateTimeExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Modules.Newsletters
{
  /// <summary>
  /// Extension methods for the DateTime object in Newsletters module.
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// Gets the beginning of the current date - including time.
    /// </summary>
    /// <param name="source">The date time object being extended..</param>
    /// <returns>Beggining of the given date.</returns>
    public static DateTime BeginningToday(this DateTime source)
    {
      DateTime now = DateTime.Now;
      int year = now.Year;
      now = DateTime.Now;
      int month = now.Month;
      now = DateTime.Now;
      int day = now.Day;
      return new DateTime(year, month, day, 0, 0, 0);
    }

    /// <summary>
    /// Gets the beginning of the current week - including time.
    /// </summary>
    /// <param name="source">The date time object being extended..</param>
    /// <returns>Beginning of the current week.</returns>
    public static DateTime BeginningThisWeek(this DateTime source)
    {
      DateTime dateTime = DateTime.Now;
      while (dateTime.DayOfWeek != DayOfWeek.Monday)
        dateTime = dateTime.AddDays(-1.0);
      return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);
    }

    /// <summary>
    /// Gets the beginning of the current month - including time.
    /// </summary>
    /// <param name="source">The date time object being extended..</param>
    /// <returns>Beginning of the current month.</returns>
    public static DateTime BeginningThisMonth(this DateTime source)
    {
      DateTime now = DateTime.Now;
      int year = now.Year;
      now = DateTime.Now;
      int month = now.Month;
      return new DateTime(year, month, 1, 0, 0, 0);
    }
  }
}
