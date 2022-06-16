// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UrlEvaluation.DateEvaluator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Web.UrlEvaluation
{
  /// <summary>
  /// Evaluates a date in the URL that is in the following format: YYYY/MM/DD
  /// </summary>
  public class DateEvaluator : IUrlEvaluator<DateTime>, IUrlEvaluator
  {
    private string regularExpression;

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, (Type) null, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">The name of the property the will be filtered.</param>
    /// <param name="contentType">The content type.</param>
    /// <param name="values">The values the should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      string urlKeyPrefix,
      out object[] values)
    {
      return this.Evaluate(url, propertyName, contentType, UrlEvaluationMode.Default, urlKeyPrefix, out values);
    }

    /// <summary>
    /// Evaluates the specified URL and returns filter expression if match is found.
    /// </summary>
    /// <param name="url">The URL to evaluate.</param>
    /// <param name="propertyName">Name of the property that will be filtered.</param>
    /// <param name="contentType">The content type of the filtered items.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <param name="values">The values that should be passed to the dynamic LINQ.</param>
    /// <returns>
    /// Filter expression if match is found otherwise returns empty string.
    /// </returns>
    public virtual string Evaluate(
      string url,
      string propertyName,
      Type contentType,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out object[] values)
    {
      List<DateTime> values1;
      string str = this.Evaluate(url, propertyName, urlEvaluationMode, urlKeyPrefix, out values1);
      values = new object[0];
      if (values1.Count <= 0)
        return str;
      values = values1.Select<DateTime, object>((Func<DateTime, object>) (d => (object) d)).ToArray<object>();
      return str;
    }

    public virtual string Evaluate(
      string url,
      string propertyName,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix,
      out List<DateTime> values)
    {
      if (string.IsNullOrEmpty(propertyName))
        throw new ArgumentNullException(nameof (propertyName));
      string empty1 = string.Empty;
      string empty2 = string.Empty;
      string empty3 = string.Empty;
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string parameterName1 = urlKeyPrefix + "year";
        string parameterName2 = urlKeyPrefix + "month";
        string parameterName3 = urlKeyPrefix + "day";
        string sYear = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName1);
        string sMonth = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName2);
        string sDay = SystemManager.CurrentHttpContext.Request.QueryStringGet(parameterName3);
        if (!string.IsNullOrEmpty(sYear))
          return this.SetDates(sYear, sMonth, sDay, out values, propertyName);
        values = new List<DateTime>();
        return string.Empty;
      }
      if (!string.IsNullOrWhiteSpace(url))
      {
        MatchCollection matchCollection = Regex.Matches(url, this.regularExpression);
        if (matchCollection.Count >= 1)
        {
          Match match = matchCollection[0];
          string str1 = match.Groups["urlPrefix"].Value;
          if (str1 == urlKeyPrefix || string.IsNullOrEmpty(str1) && string.IsNullOrEmpty(urlKeyPrefix))
          {
            string str2 = this.SetDates(match.Groups["year"].Value, match.Groups["month"].Value, match.Groups["day"].Value, out values, propertyName);
            RouteHelper.SetUrlParametersResolved();
            return str2;
          }
        }
      }
      values = new List<DateTime>();
      return string.Empty;
    }

    private string SetDates(
      string sYear,
      string sMonth,
      string sDay,
      out List<DateTime> values,
      string propertyName)
    {
      int year = int.Parse(sYear);
      DateTime dateTime1;
      DateTime dateTime2;
      if (string.IsNullOrEmpty(sMonth))
      {
        dateTime1 = new DateTime(year, 1, 1, 0, 0, 0);
        dateTime2 = new DateTime(year, 12, DateTime.DaysInMonth(year, 12), 23, 59, 59);
      }
      else if (string.IsNullOrEmpty(sDay))
      {
        int month = int.Parse(sMonth);
        dateTime1 = new DateTime(year, month, 1, 0, 0, 0);
        dateTime2 = new DateTime(year, month, DateTime.DaysInMonth(year, month), 23, 59, 59);
      }
      else
      {
        int month = int.Parse(sMonth);
        int day = int.Parse(sDay);
        dateTime1 = new DateTime(year, month, day, 0, 0, 0);
        dateTime2 = new DateTime(year, month, day, 23, 59, 59);
      }
      values = new List<DateTime>();
      values.Add(dateTime1);
      values.Add(dateTime2);
      return new StringBuilder().Append("(").Append(propertyName).Append(" >= @0 && ").Append(propertyName).Append(" <= @1)").ToString();
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <typeparam name="TData">The type of the data.</typeparam>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(DateTime data, string urlKeyPrefix) => this.BuildUrl(data, DateBuildOptions.YearMonthDay, urlKeyPrefix);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="data">The data.</param>
    /// <returns>The URL build based on the provided data.</returns>
    public virtual string BuildUrl(DateTime date, DateBuildOptions options, string urlKeyPrefix) => this.BuildUrl(date, options, UrlEvaluationMode.Default, urlKeyPrefix);

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="date">The date.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns></returns>
    public virtual string BuildUrl(
      DateTime date,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return this.BuildUrl(date, DateBuildOptions.YearMonthDay, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>Builds a URL string based on the provided data.</summary>
    /// <param name="date">The date.</param>
    /// <param name="options">The options.</param>
    /// <param name="urlEvaluationMode">The URL evaluation mode.</param>
    /// <returns></returns>
    public virtual string BuildUrl(
      DateTime date,
      DateBuildOptions options,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      if (urlEvaluationMode != UrlEvaluationMode.Default && urlEvaluationMode == UrlEvaluationMode.QueryString)
      {
        string str1 = urlKeyPrefix + "year";
        string str2 = urlKeyPrefix + "month";
        string str3 = urlKeyPrefix + "day";
        QueryStringBuilder current = QueryStringBuilder.Current;
        string name1 = str1;
        int num = date.Year;
        string str4 = num.ToString();
        QueryStringBuilder queryStringBuilder1 = current.Add(name1, str4, true);
        if (options != DateBuildOptions.Year)
        {
          QueryStringBuilder queryStringBuilder2 = queryStringBuilder1;
          string name2 = str2;
          num = date.Month;
          string str5 = num.ToString("00");
          queryStringBuilder2.Add(name2, str5, true);
          if (options == DateBuildOptions.YearMonthDay)
          {
            QueryStringBuilder queryStringBuilder3 = queryStringBuilder1;
            string name3 = str3;
            num = date.Day;
            string str6 = num.ToString("00");
            queryStringBuilder3.Add(name3, str6, true);
          }
        }
        return queryStringBuilder1.Remove(urlKeyPrefix + "page").ToString();
      }
      StringBuilder stringBuilder1 = new StringBuilder("/");
      if (!string.IsNullOrEmpty(urlKeyPrefix))
      {
        stringBuilder1.Append("!");
        stringBuilder1.Append(urlKeyPrefix);
        stringBuilder1.Append("/");
      }
      stringBuilder1.Append(date.Year);
      if (options != DateBuildOptions.Year)
      {
        stringBuilder1.Append("/");
        StringBuilder stringBuilder2 = stringBuilder1;
        int num = date.Month;
        string str7 = num.ToString("00");
        stringBuilder2.Append(str7);
        if (options == DateBuildOptions.YearMonthDay)
        {
          stringBuilder1.Append("/");
          StringBuilder stringBuilder3 = stringBuilder1;
          num = date.Day;
          string str8 = num.ToString("00");
          stringBuilder3.Append(str8);
        }
      }
      return stringBuilder1.ToString();
    }

    string IUrlEvaluator.BuildUrl(object data, string urlKeyPrefix) => this.BuildUrl((DateTime) data, urlKeyPrefix);

    string IUrlEvaluator.BuildUrl(
      object data,
      UrlEvaluationMode urlEvaluationMode,
      string urlKeyPrefix)
    {
      return this.BuildUrl((DateTime) data, DateBuildOptions.YearMonthDay, urlEvaluationMode, urlKeyPrefix);
    }

    /// <summary>
    /// Initializes this instance with the provided configuration information.
    /// </summary>
    /// <param name="config"></param>
    public virtual void Initialize(NameValueCollection config)
    {
      this.regularExpression = config != null ? config["regularExpression"] : throw new ArgumentNullException(nameof (config));
      if (!string.IsNullOrEmpty(this.regularExpression))
        return;
      this.regularExpression = "^/?(!(?<urlPrefix>\\w+)/)?(?<year>[0-2]{1}\\d{3})/(?<month>[0-1]{1}[0-9]{1})/?(?<day>[0-3]{1}[0-9]{1})(?:/|\\z)$|^/?(!(?<urlPrefix>\\w+)/)?(?<year>[0-2]{1}\\d{3})/(?<month>[0-1]{1}[0-9]{1})(?:/|\\z)$|^/?(!(?<urlPrefix>\\w+)/)?(?<year>[0-2]{1}\\d{3})(?:/|\\z)$";
    }
  }
}
