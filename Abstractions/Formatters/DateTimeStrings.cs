// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Abstractions.Formatters.DateTimeStrings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Abstractions.Formatters
{
  /// <summary>
  /// A class that will hold string resources for instances of type <see cref="T:Telerik.Sitefinity.Abstractions.Formatters.RelativeDateFormatter" />.
  /// </summary>
  internal class DateTimeStrings
  {
    private string dayMonthFormat;
    private string dayMonthYearFormat;
    private const string defaultDayMonthFormat = "dd MMM";
    private const string defaultDayMonthYearFormat = "dd MMM yyyy";

    /// <summary>Initializes the default strings.</summary>
    public virtual void InitializeDefaultStrings()
    {
      Labels labels = Res.Get<Labels>();
      this.Day = labels.Day;
      this.Days = labels.Days;
      this.Hour = labels.Hour;
      this.Hours = labels.Hours;
      this.Minute = labels.Minute;
      this.Minutes = labels.Minutes;
      this.And = labels.And;
      this.Ago = labels.Ago;
      this.LessThanAMintueAgo = labels.LessThanAMintueAgo;
      this.DayMonthFormat = "dd MMM";
      this.DayMonthYearFormat = "dd MMM yyyy";
    }

    /// <summary>The singular form of the word "second".</summary>
    public string Second { get; set; }

    /// <summary>The plural form of the word "second".</summary>
    public string Seconds { get; set; }

    /// <summary>The singular form of the word "minute".</summary>
    public string Minute { get; set; }

    /// <summary>The plural form of the word "minute".</summary>
    public string Minutes { get; set; }

    /// <summary>The singular form of the word "hour".</summary>
    public string Hour { get; set; }

    /// <summary>The plural form of the word "hour".</summary>
    public string Hours { get; set; }

    /// <summary>The singular form of the word "day".</summary>
    public string Day { get; set; }

    /// <summary>The plural form of the word "day".</summary>
    public string Days { get; set; }

    /// <summary>The word "and".</summary>
    public string And { get; set; }

    /// <summary>The word "ago".</summary>
    public string Ago { get; set; }

    /// <summary>The phrase "less than a minute ago".</summary>
    public string LessThanAMintueAgo { get; set; }

    /// <summary>Gets or sets the day month format.</summary>
    public string DayMonthFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.dayMonthFormat))
          this.dayMonthFormat = "dd MMM";
        return this.dayMonthFormat;
      }
      set => this.dayMonthFormat = value;
    }

    /// <summary>Gets or sets the day month year format.</summary>
    public string DayMonthYearFormat
    {
      get
      {
        if (string.IsNullOrEmpty(this.dayMonthYearFormat))
          this.dayMonthYearFormat = "dd MMM yyyy";
        return this.dayMonthYearFormat;
      }
      set => this.dayMonthYearFormat = value;
    }
  }
}
