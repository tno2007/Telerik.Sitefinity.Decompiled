// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.ContentHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Versioning;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Modules
{
  public static class ContentHelper
  {
    public const string StatusRegex = ".*(status)(\\s)*=(\\s)*(live).*";
    public const string VisibleRegex = ".*(visible)(\\s)*=(\\s)*(true).*";

    internal static void DeleteVersionItem(VersionManager versionManager, Guid itemId)
    {
      Item obj = versionManager.Provider.GetItems().Where<Item>((Expression<Func<Item, bool>>) (e => e.Id == itemId)).FirstOrDefault<Item>();
      if (obj == null)
        return;
      versionManager.DeleteItem((object) obj);
    }

    /// <summary>Formats the sort value applying the sort direction.</summary>
    /// <param name="value">The sort value.</param>
    /// <param name="direction">The sort direction.</param>
    /// <returns></returns>
    public static string FormatSortValue(string value, SortDirection direction) => direction == SortDirection.Ascending ? string.Format("{0} ASC", (object) value) : string.Format("{0} DESC", (object) value);

    /// <summary>
    /// Formatteds the sort text adding sort direction to be displayed.
    /// </summary>
    /// <param name="value">The sort value.</param>
    /// <param name="direction">The sort direction.</param>
    /// <returns></returns>
    public static string FormatSortText(string value, SortDirection direction)
    {
      ContentResources contentResources = Res.Get<ContentResources>();
      return direction == SortDirection.Ascending ? string.Format("{0} {1}", (object) value, (object) contentResources.AZ) : string.Format("{0} {1}", (object) value, (object) contentResources.ZA);
    }

    /// <summary>
    /// Adapts the multilingual filter expression to incorporate the implementation of IlifecycleDataItem
    /// if the filter contains expression for filtering published items, then it is modified to use LanguageData property of IlifecycleDataItem to filter published items
    /// </summary>
    /// <param name="currentFilter">The current filter.</param>
    /// <param name="culture"> the culture on witch you  </param>
    /// <param name="withFallback"> Specifies weather the filter should be with fallback to the parent language </param>
    /// <returns>if the specified culture is not null then returns the modified filter where published items are filtered by culture, otherwise returns the same filter</returns>
    public static string AdaptMultilingualFilterExpressionRaw(
      string currentFilter,
      CultureInfo culture,
      bool withFallback)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      Regex regex1 = new Regex(".*(status)(\\s)*=(\\s)*(live).*", RegexOptions.IgnoreCase);
      Regex regex2 = new Regex(".*(visible)(\\s)*=(\\s)*(true).*", RegexOptions.IgnoreCase);
      string str1 = currentFilter;
      if (!string.IsNullOrEmpty(currentFilter) && regex1.IsMatch(currentFilter) && regex2.IsMatch(currentFilter))
      {
        string empty = string.Empty;
        CultureInfo frontendLanguage = SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage;
        string str2;
        if (culture.Equals((object) frontendLanguage))
          str2 = string.Format("PublishedTranslations.Count = 0 OR PublishedTranslations.Contains(\"{0}\")", (object) culture);
        else if (withFallback)
        {
          str2 = "PublishedTranslations.Count = 0";
          CultureInfo cultureInfo = culture;
          while (true)
          {
            if (cultureInfo.Equals((object) CultureInfo.InvariantCulture))
              cultureInfo = frontendLanguage;
            str2 += string.Format(" OR PublishedTranslations.Contains(\"{0}\")", (object) cultureInfo.Name);
            if (!cultureInfo.Equals((object) frontendLanguage))
              cultureInfo = cultureInfo.Parent;
            else
              break;
          }
        }
        else
          str2 = string.Format("PublishedTranslations.Contains(\"{0}\")", (object) culture);
        str1 = string.Format("({0}) AND ({1})", (object) currentFilter, (object) str2);
      }
      return str1;
    }

    /// <summary>
    /// Adapts the multilingual filter expression to incorporate the implementation of IlifecycleDataItem
    /// if the filter contains expression for filtering published items, then it is modified to use LanguageData property of IlifecycleDataItem to filter published items
    /// </summary>
    /// <param name="currentFilter">The current filter.</param>
    /// <param name="culture"> the culture on witch you  </param>
    /// <returns>if the specified culture is not null then returns the modified filter where published items are filtered by culture, otherwise returns the same filter</returns>
    public static string AdaptMultilingualFilterExpressionRaw(
      string currentFilter,
      CultureInfo culture)
    {
      return ContentHelper.AdaptMultilingualFilterExpressionRaw(currentFilter, culture, false);
    }

    /// <summary>
    /// Adapts the multilingual filter expression to incorporate the implementation of IlifecycleDataItem
    /// if the filter contains expression for filtering published items, then it is modified to use LanguageData property of IlifecycleDataItem to filter published items
    /// </summary>
    /// <param name="currentFilter">The current filter.</param>
    /// <returns>Modified filter where published items are filtered by culture</returns>
    public static string AdaptMultilingualFilterExpression(string currentFilter) => ContentHelper.AdaptMultilingualFilterExpressionRaw(currentFilter, SystemManager.CurrentContext.Culture);

    internal static string GetExpirationTimeCaptionInHours(double expirationTimeInHours)
    {
      List<string> captionComponents = ContentHelper.GetExpirationTimeCaptionComponents(expirationTimeInHours);
      string timeCaptionInHours = string.Empty;
      if (captionComponents.Count == 1)
        timeCaptionInHours = captionComponents[0];
      else if (captionComponents.Count == 2)
        timeCaptionInHours = captionComponents[0] + " " + Res.Get<Labels>().And + " " + captionComponents[1];
      else if (captionComponents.Count == 3)
        timeCaptionInHours = captionComponents[0] + ", " + captionComponents[1] + " " + Res.Get<Labels>().And + " " + captionComponents[2];
      return timeCaptionInHours;
    }

    internal static bool TryParseSortExpressionName(
      string sortExpression,
      out string sortExpressionName)
    {
      sortExpressionName = (string) null;
      string[] source = sortExpression.Split(' ');
      if (((IEnumerable<string>) source).Count<string>() != 2)
        return false;
      sortExpressionName = source[0];
      return true;
    }

    private static List<string> GetExpirationTimeCaptionComponents(double expirationTimeInHours)
    {
      TimeSpan timeSpan = TimeSpan.FromHours(expirationTimeInHours);
      List<string> expirationTimeCaptionComponents = new List<string>();
      ContentHelper.GetExpirationTimeCaptionComponent(timeSpan.Days, Res.Get<Labels>().Day, Res.Get<Labels>().Days, expirationTimeCaptionComponents);
      ContentHelper.GetExpirationTimeCaptionComponent(timeSpan.Hours, Res.Get<Labels>().Hour, Res.Get<Labels>().Hours, expirationTimeCaptionComponents);
      ContentHelper.GetExpirationTimeCaptionComponent(timeSpan.Minutes, Res.Get<Labels>().Minute, Res.Get<Labels>().Minutes, expirationTimeCaptionComponents);
      return expirationTimeCaptionComponents;
    }

    private static void GetExpirationTimeCaptionComponent(
      int elementsCount,
      string singularExpression,
      string multiplaExpression,
      List<string> expirationTimeCaptionComponents)
    {
      if (elementsCount <= 0)
        return;
      if (elementsCount == 1)
        expirationTimeCaptionComponents.Add(elementsCount.ToString() + " " + singularExpression);
      else
        expirationTimeCaptionComponents.Add(elementsCount.ToString() + " " + multiplaExpression);
    }
  }
}
