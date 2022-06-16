// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.CrontabScheduleCalculator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using NCrontab;
using System;

namespace Telerik.Sitefinity.Scheduling
{
  internal class CrontabScheduleCalculator : IScheduleCalculator
  {
    public const string ScheduleSpecType = "crontab";
    private const int maxFieldCount = 6;
    private const int yearFieldIndex = 5;

    public DateTime? GetNextOccurrence(string scheduleSpec, DateTime baseTime)
    {
      int? year;
      CrontabScheduleCalculator.ParseYearField(ref scheduleSpec, out year);
      ValueOrError<CrontabSchedule> valueOrError = CrontabSchedule.TryParse(scheduleSpec);
      if (valueOrError.IsError)
        throw valueOrError.Error;
      DateTime endTime = DateTime.MaxValue;
      if (year.HasValue)
      {
        if (year.Value != baseTime.Year)
          baseTime = new DateTime(year.Value, 1, 1, 0, 0, 0, baseTime.Kind);
        endTime = new DateTime(year.Value, 12, 31, 23, 59, 0, baseTime.Kind);
      }
      DateTime nextOccurrence = valueOrError.Value.GetNextOccurrence(baseTime, endTime);
      return nextOccurrence == endTime ? new DateTime?() : new DateTime?(nextOccurrence);
    }

    private static void ParseYearField(ref string spec, out int? year)
    {
      year = new int?();
      string[] strArray = spec.Split();
      if (strArray.Length > 6)
        throw new FormatException("Too many fields in crontab expression: " + spec);
      if (strArray.Length <= 5)
        return;
      string s = strArray[5];
      if (s != "*")
      {
        int result;
        if (!int.TryParse(s, out result))
          throw new FormatException("Invalid year field in crontab expression: " + s);
        year = new int?(result);
      }
      spec = string.Join(" ", strArray, 0, 5);
    }
  }
}
