// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.OutputCache.HttpDate
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Telerik.Sitefinity.Web.OutputCache
{
  internal static class HttpDate
  {
    private static readonly int[] TensDigit = new int[10]
    {
      0,
      10,
      20,
      30,
      40,
      50,
      60,
      70,
      80,
      90
    };
    private static readonly string[] Months = new string[12]
    {
      "Jan",
      "Feb",
      "Mar",
      "Apr",
      "May",
      "Jun",
      "Jul",
      "Aug",
      "Sep",
      "Oct",
      "Nov",
      "Dec"
    };
    private static readonly sbyte[] MonthIndexTable = new sbyte[64]
    {
      (sbyte) -1,
      (sbyte) 65,
      (sbyte) 2,
      (sbyte) 12,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 8,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 7,
      (sbyte) -1,
      (sbyte) 78,
      (sbyte) -1,
      (sbyte) 9,
      (sbyte) -1,
      (sbyte) 82,
      (sbyte) -1,
      (sbyte) 10,
      (sbyte) -1,
      (sbyte) 11,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 5,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 65,
      (sbyte) 2,
      (sbyte) 12,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 8,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 7,
      (sbyte) -1,
      (sbyte) 78,
      (sbyte) -1,
      (sbyte) 9,
      (sbyte) -1,
      (sbyte) 82,
      (sbyte) -1,
      (sbyte) 10,
      (sbyte) -1,
      (sbyte) 11,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) 5,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1,
      (sbyte) -1
    };

    private static int Atoi2(string s, int startIndex)
    {
      try
      {
        int index = (int) s[startIndex] - 48;
        int num = (int) s[1 + startIndex] - 48;
        return HttpDate.TensDigit[index] + num;
      }
      catch
      {
        throw new FormatException("Atio2Badstring");
      }
    }

    private static int MakeMonth(string s, int startIndex)
    {
      int index = (int) s[2 + startIndex] - 64 & 63;
      sbyte num = HttpDate.MonthIndexTable[index];
      if (num >= (sbyte) 13)
      {
        if (num == (sbyte) 78)
        {
          num = HttpDate.MonthIndexTable[(int) s[1 + startIndex] - 64 & 63] == (sbyte) 65 ? (sbyte) 1 : (sbyte) 6;
        }
        else
        {
          if (num != (sbyte) 82)
            throw new FormatException("MakeMonthBadString");
          num = HttpDate.MonthIndexTable[(int) s[1 + startIndex] - 64 & 63] == (sbyte) 65 ? (sbyte) 3 : (sbyte) 4;
        }
      }
      string month = HttpDate.Months[(int) num - 1];
      if ((int) s[startIndex] == (int) month[0] && (int) s[1 + startIndex] == (int) month[1] && (int) s[2 + startIndex] == (int) month[2] || (int) char.ToUpper(s[startIndex], CultureInfo.InvariantCulture) == (int) month[0] && (int) char.ToLower(s[1 + startIndex], CultureInfo.InvariantCulture) == (int) month[1] && (int) char.ToLower(s[2 + startIndex], CultureInfo.InvariantCulture) == (int) month[2])
        return (int) num;
      throw new FormatException("MakeMonthBadString");
    }

    [SuppressMessage("StyleCopPlus.StyleCopPlusRules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed")]
    public static DateTime UtcParse(string time)
    {
      if (time == null)
        throw new ArgumentNullException(nameof (time));
      int startIndex;
      int day;
      int month;
      int year;
      int hour;
      int minute;
      int second;
      if ((startIndex = time.IndexOf(',')) == -1)
      {
        int num1 = -1;
        int num2 = time.Length + 1;
        while (--num2 > 0)
        {
          int index = ++num1;
          if (time[index] != ' ')
            break;
        }
        if (num2 < 24)
          throw new FormatException("UtcParseDateTimebad");
        day = HttpDate.Atoi2(time, num1 + 8);
        month = HttpDate.MakeMonth(time, num1 + 4);
        year = HttpDate.Atoi2(time, num1 + 20) * 100 + HttpDate.Atoi2(time, num1 + 22);
        hour = HttpDate.Atoi2(time, num1 + 11);
        minute = HttpDate.Atoi2(time, num1 + 14);
        second = HttpDate.Atoi2(time, num1 + 17);
      }
      else
      {
        int num3 = time.Length - startIndex;
        while (--num3 > 0)
        {
          int index = ++startIndex;
          if (time[index] != ' ')
            break;
        }
        if (time[startIndex + 2] != '-')
        {
          if (num3 < 20)
            throw new FormatException("UtilParseDateTimeBad");
          day = HttpDate.Atoi2(time, startIndex);
          month = HttpDate.MakeMonth(time, startIndex + 3);
          year = HttpDate.Atoi2(time, startIndex + 7) * 100 + HttpDate.Atoi2(time, startIndex + 9);
          hour = HttpDate.Atoi2(time, startIndex + 12);
          minute = HttpDate.Atoi2(time, startIndex + 15);
          second = HttpDate.Atoi2(time, startIndex + 18);
        }
        else
        {
          if (num3 < 18)
            throw new FormatException("UtilParseDateTimeBad");
          day = HttpDate.Atoi2(time, startIndex);
          month = HttpDate.MakeMonth(time, startIndex + 3);
          int num4 = HttpDate.Atoi2(time, startIndex + 7);
          year = num4 < 50 ? num4 + 2000 : num4 + 1900;
          hour = HttpDate.Atoi2(time, startIndex + 10);
          minute = HttpDate.Atoi2(time, startIndex + 13);
          second = HttpDate.Atoi2(time, startIndex + 16);
        }
      }
      return new DateTime(year, month, day, hour, minute, second, DateTimeKind.Utc);
    }
  }
}
