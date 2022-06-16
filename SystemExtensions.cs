// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SystemExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.UI;

namespace Telerik.Sitefinity
{
  public static class SystemExtensions
  {
    internal static string TimeZoneCookieName = "sf_timezoneoffset";
    internal static string IsoFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";

    public static bool IsNullable(this Type type) => type.IsNullableType();

    /// <summary>
    /// This method will set the text on the text control if text is not null or empty,
    /// otherwise it will hide the control (the text control won't be rendered).
    /// </summary>
    /// <param name="textControl">
    /// An instance of the text control on which the text ought to be set. This control will be
    /// hidden if text argument is null or empty.
    /// </param>
    /// <param name="text">The text to be set on the control.</param>
    public static void SetTextOrHide(this ITextControl textControl, string text)
    {
      if (textControl == null)
        return;
      if (string.IsNullOrEmpty(text))
        ((Control) textControl).Visible = false;
      else
        textControl.Text = ControlUtilities.Sanitize(text);
    }

    public static void SetTextOrHide(
      this ITextControl textControl,
      string text,
      string resourceClassId)
    {
      string text1 = text;
      textControl.SetTextOrHide(text1);
    }

    /// <summary>
    /// Converts a date time to string at following format: yyyy-MM-dd hh:mm:ss.
    /// </summary>
    /// <param name="dateTime">The date time to be converted.</param>
    /// <returns>DateTime as string at format yyyy-MM-dd hh:mm:ss.</returns>
    public static string ToLongDateTimeString(this DateTime value) => value.ToString("yyyy-MM-dd hh:mm:ss");

    /// <summary>
    /// Determines whether a date is in range of the specified date values.
    /// </summary>
    /// <param name="value">The value to compare.</param>
    /// <param name="from">Start of interval.</param>
    /// <param name="to">End of interval.</param>
    /// <returns>
    ///     <c>true</c> a date is in range of the specified date values; otherwise, <c>false</c>.
    /// </returns>
    public static bool IsDateInRange(this DateTime value, DateTime from, DateTime to)
    {
      bool flag = false;
      if (DateTime.Compare(value, from) >= 0 && DateTime.Compare(to, value) >= 0)
        flag = true;
      return flag;
    }

    /// <summary>
    /// Converts a <see cref="T:System.DateTime" /> to Sitefinity's UI time.
    /// The time zone is taken from <see cref="P:Telerik.Sitefinity.Services.SystemConfig.UITimeZoneSettings" /> and fallbacks to <see cref="P:System.TimeZoneInfo.Local" />.
    /// </summary>
    public static DateTime ToSitefinityUITime(this DateTime value)
    {
      TimeZoneInfo userTimeZone = UserManager.GetManager().GetUserTimeZone();
      return TimeZoneInfo.ConvertTime(value, userTimeZone);
    }

    /// <summary>
    /// Converts a <see cref="T:System.DateTime" /> to the Sitefinity's UI time or to the local UI time
    /// depending on the selected value in the UserBrowserSettingsForCalculatingDates setting in the UI Time Zone Config in the System config.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns></returns>
    public static DateTime ToSitefinityOrLocaUITime(this DateTime value)
    {
      if (!UserManager.GetManager().GetUserBrowserSettingsForCalculatingDates())
        return value.ToSitefinityUITime();
      if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Request != null && SystemManager.CurrentHttpContext.Request.Cookies != null)
      {
        HttpCookie cookie = SystemManager.CurrentHttpContext.Request.Cookies[SystemExtensions.TimeZoneCookieName];
        int result;
        if (cookie != null && int.TryParse(cookie.Value, out result))
        {
          int num1 = -1 * result;
          long num2 = 600000000L * (long) num1;
          long num3 = value.Ticks + num2;
          if (num3 < DateTime.MinValue.Ticks)
            return DateTime.MinValue;
          return num3 > DateTime.MaxValue.Ticks ? DateTime.MaxValue : value.AddMinutes((double) num1);
        }
      }
      return value.ToLocalTime();
    }

    [Obsolete("Use ToSitefinityUITime instead.")]
    public static DateTime ToLocal(this DateTime value) => value.ToSitefinityUITime();

    /// <summary>
    /// <para>Truncates a DateTime to a specified resolution.</para>
    /// <para>A convenient source for resolution is TimeSpan.TicksPerXXXX constants.</para>
    /// </summary>
    /// <param name="date">The DateTime object to truncate</param>
    /// <param name="resolution">e.g. to round to nearest second, TimeSpan.TicksPerSecond</param>
    /// <returns>Truncated DateTime</returns>
    internal static DateTime Truncate(this DateTime date, long resolution) => new DateTime(date.Ticks - date.Ticks % resolution, date.Kind);

    /// <summary>Trims the seconds.</summary>
    /// <param name="date">The date.</param>
    /// <returns>The date with trimmed seconds and milliseconds.</returns>
    public static DateTime TrimSeconds(this DateTime date) => date.Truncate(600000000L);

    /// <summary>
    /// Returns a human readable representation of a <see cref="T:System.TimeSpan" /> value.
    /// </summary>
    /// <param name="span">The value to be used.</param>
    /// <returns>A human readable string representation.</returns>
    public static string ToReadableString(this TimeSpan span)
    {
      StringBuilder stringBuilder = new StringBuilder();
      Labels labels = Res.Get<Labels>();
      if (span.Days > 0)
        stringBuilder.AppendFormat("{0:0} {1}", (object) span.Days, (object) labels.DaysAbbreviated);
      if (span.Hours > 0)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(", ");
        stringBuilder.AppendFormat("{0:0} {1}", (object) span.Hours, (object) labels.HoursAbbreviated);
      }
      if (span.Minutes > 0)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(", ");
        stringBuilder.AppendFormat("{0:0} {1}", (object) span.Minutes, (object) labels.MinutesAbbreviated);
      }
      if (span.Seconds > 0)
      {
        if (stringBuilder.Length > 0)
          stringBuilder.Append(", ");
        stringBuilder.AppendFormat("{0:0} {1}", (object) span.Seconds, (object) labels.SecondsAbbreviated);
      }
      return stringBuilder.ToString();
    }

    public static TValue GetAndRemove<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
    {
      TValue andRemove = default (TValue);
      if (source.TryGetValue(key, out andRemove))
        source.Remove(key);
      return andRemove;
    }

    public static string UrlEncode(this string source) => HttpUtility.UrlEncode(source);

    public static string UrlDecode(this string source) => HttpUtility.UrlDecode(source);

    public static string UrlTokenEncode(this string source) => HttpServerUtility.UrlTokenEncode(Encoding.UTF8.GetBytes(source));

    public static string UrlTokenDecode(this string source) => Encoding.UTF8.GetString(HttpServerUtility.UrlTokenDecode(source));

    public static string Base64Encode(this string source) => Convert.ToBase64String(Encoding.UTF8.GetBytes(source));

    public static string Base64Decode(this string source) => Encoding.UTF8.GetString(Convert.FromBase64String(source));

    public static bool Contains(
      this NameObjectCollectionBase.KeysCollection source,
      string key,
      StringComparison comparisonType = StringComparison.CurrentCulture)
    {
      foreach (object obj in source)
      {
        if (obj != null && obj.ToString().Equals(key, comparisonType))
          return true;
      }
      return false;
    }

    public static string ToQueryString(
      this NameValueCollection collection,
      bool startWithQuestionMark = true)
    {
      if (collection == null || !collection.HasKeys())
        return string.Empty;
      StringBuilder stringBuilder = new StringBuilder();
      if (startWithQuestionMark)
        stringBuilder.Append("?");
      int num1 = 0;
      NameObjectCollectionBase.KeysCollection keys = collection.Keys;
      foreach (string name in keys)
      {
        int num2 = 0;
        string[] values = collection.GetValues(name);
        foreach (string source in values)
        {
          stringBuilder.Append(name).Append("=").Append(source.UrlEncode());
          if (++num2 < values.Length)
            stringBuilder.Append("&");
        }
        if (++num1 < keys.Count)
          stringBuilder.Append("&");
      }
      return stringBuilder.ToString();
    }

    /// <summary>
    /// Converts a date time to string at following format(ISO): yyyy-MM-ddTHH:mm:ss.fffZ.
    /// </summary>
    /// <param name="dateTime">The date time to be converted.</param>
    /// <returns>DateTime as string at format(ISO) yyyy-MM-ddTHH:mm:ss.fffZ.</returns>
    internal static string ToIsoFormat(this DateTime value, CultureInfo culture = null) => culture != null ? value.ToString(SystemExtensions.IsoFormat, (IFormatProvider) culture) : value.ToString(SystemExtensions.IsoFormat);
  }
}
