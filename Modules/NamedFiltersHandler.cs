// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.NamedFiltersHandler
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.Localization;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Modules
{
  public static class NamedFiltersHandler
  {
    public const string LifecyclePublishedDrafts = "LifecyclePublishedDrafts";
    public const string LifecycleNotPublishedDrafts = "LifecycleNotPublishedDrafts";
    public const string LifecyclePublishedItems = "LifecyclePublishedItems";
    public const string PublishedItems = "PublishedItems";
    public const string PublishedDrafts = "PublishedDrafts";
    public const string NotPublishedDrafts = "NotPublishedDrafts";
    public const string Drafts = "Drafts";
    public const string ScheduledDrafts = "ScheduledDrafts";
    public const string LifecycleLiveItems = "LifecycleLiveItems";
    public const string LifecycleTempItems = "LifecycleTempItems";
    public const string ShowRecentLiveItems = "ShowRecentLiveItems";
    public const string StatusFilters = "StatusFilter";

    /// <summary>Gets the filtered items by filter name</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="filterName">Name of the filter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static IQueryable<T> ApplyFilter<T>(
      IQueryable<T> items,
      string filterName,
      CultureInfo culture = null,
      string itemProvider = null)
      where T : Content
    {
      string languageKey = culture.GetLanguageKey();
      switch (filterName)
      {
        case "Drafts":
          return NamedFiltersHandler.FilterWorkflowItems<T>(items, languageKey, ContentLifecycleStatus.Master, ContentUIStatus.Draft.ToString());
        case "LifecycleLiveItems":
          return NamedFiltersHandler.FilterLifecycleItems<T>(items, languageKey, ContentLifecycleStatus.Live);
        case "LifecycleNotPublishedDrafts":
          return NamedFiltersHandler.FilterLifecycleItems<T>(items, languageKey, LifecycleState.None, ContentLifecycleStatus.Master);
        case "LifecyclePublishedDrafts":
          return NamedFiltersHandler.FilterLifecycleItems<T>(items, languageKey, LifecycleState.Published, ContentLifecycleStatus.Master);
        case "LifecyclePublishedItems":
          return NamedFiltersHandler.FilterPublishedLifecycleItems<T>(items, languageKey);
        case "LifecycleTempItems":
          return NamedFiltersHandler.FilterLifecycleItems<T>(items, languageKey, ContentLifecycleStatus.Temp);
        case "NotPublishedDrafts":
          return NamedFiltersHandler.FilterItems<T>(items, languageKey, ContentLifecycleStatus.Master, string.Empty);
        case "PublishedDrafts":
          return NamedFiltersHandler.FilterItems<T>(items, languageKey, ContentLifecycleStatus.Master, "PUBLISHED");
        case "PublishedItems":
          return NamedFiltersHandler.FilterItems<T>(items, languageKey, "PUBLISHED");
        case "ScheduledDrafts":
          return NamedFiltersHandler.FilterWorkflowItems<T>(items, languageKey, ContentLifecycleStatus.Master, ContentUIStatus.Scheduled.ToString());
        case "ShowRecentLiveItems":
          return NamedFiltersHandler.FilterRecentItems<T>(items);
        default:
          if (itemProvider.IsNullOrEmpty() && items is ILinqQuery linqQuery)
            itemProvider = linqQuery.DataProvider.Name;
          return NamedFiltersHandler.ApplyStatusProviderFilter<T>(items, filterName, culture, itemProvider);
      }
    }

    /// <summary>Gets the filtered items by filter name</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="filterName">Name of the filter.</param>
    /// <param name="culture">The culture.</param>
    /// <returns></returns>
    public static IQueryable<T> ApplyFilterToDynamicContent<T>(
      IQueryable<T> items,
      string filterName,
      CultureInfo culture = null,
      string itemProvider = null)
      where T : DynamicContent
    {
      string languageKey = culture.GetLanguageKey();
      if (filterName == "LifecyclePublishedDrafts")
        return NamedFiltersHandler.FilterLifecycleDynamicContentItems<T>(items, languageKey, LifecycleState.Published, ContentLifecycleStatus.Master);
      if (filterName == "PublishedDrafts")
        return NamedFiltersHandler.FilterWorkflowDynamicContentItems<T>(items, ContentLifecycleStatus.Master, ContentUIStatus.Published.ToString());
      if (filterName == "Drafts")
        return NamedFiltersHandler.FilterWorkflowDynamicContentItems<T>(items, ContentLifecycleStatus.Master, ContentUIStatus.Draft.ToString());
      return filterName == "ScheduledDrafts" ? NamedFiltersHandler.FilterWorkflowDynamicContentItems<T>(items, ContentLifecycleStatus.Master, ContentUIStatus.Scheduled.ToString()) : NamedFiltersHandler.ApplyStatusProviderFilter<T>(items, filterName, culture, itemProvider);
    }

    /// <summary>
    /// Filter dynamic content items by state, approval workflow status and language
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <param name="workflowState">State of the workflow.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterWorkflowDynamicContentItems<T>(
      IQueryable<T> items,
      ContentLifecycleStatus status,
      string workflowState)
      where T : DynamicContent
    {
      return items.Where<T>((Expression<Func<T, bool>>) (item => (int) item.Status == (int) status && ((IApprovalWorkflowItem) item).ApprovalWorkflowState == (Lstring) workflowState));
    }

    /// <summary>Filter items by state, content status and language</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="culture">The culture.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterLifecycleDynamicContentItems<T>(
      IQueryable<T> items,
      string language,
      LifecycleState lifecycleState,
      ContentLifecycleStatus status)
      where T : DynamicContent
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T> cDisplayClass30 = new NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.language = language;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.lifecycleState = lifecycleState;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass30.status = status;
      // ISSUE: reference to a compiler-generated field
      if (DataExtensions.AppSettings.ContextSettings.Multilingual && cDisplayClass30.language == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw())
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        return items.Where<T>(Expression.Lambda<Func<T, bool>>((Expression) Expression.AndAlso((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
        {
          ((ILifecycleDataItemGeneric) item).LanguageData,
          (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.AndAlso((Expression) Expression.OrElse(ld.Language == cDisplayClass30.language, (Expression) Expression.AndAlso(ld.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression1, typeof (ILifecycleDataItemGeneric)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ILifecycleDataItem.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (LanguageData.get_ContentState))), typeof (int)), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass30, typeof (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>)), FieldInfo.GetFieldFromHandle(__fieldref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>.lifecycleState), __typeref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>))), typeof (int)))), parameterExpression2)
        }), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (DynamicContent.get_Status))), typeof (int)), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass30, typeof (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>)), FieldInfo.GetFieldFromHandle(__fieldref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>.status), __typeref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass3_0<T>))), typeof (int)))), parameterExpression1));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return items.Where<T>((Expression<Func<T, bool>>) (item => ((ILifecycleDataItemGeneric) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == cDisplayClass30.language && (int) ld.ContentState == (int) cDisplayClass30.lifecycleState)) && (int) item.Status == (int) cDisplayClass30.status));
    }

    /// <summary>Gets the filtered items by filter name</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="filterName">Name of the filter.</param>
    /// <param name="culture">The culture.</param>
    public static IEnumerable<T> ApplyFilter<T>(
      IEnumerable<T> items,
      string filterName,
      CultureInfo culture = null)
      where T : Content
    {
      string language = DataExtensions.AppSettings.ContextSettings.Multilingual ? culture.GetLanguageKey() : DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.Name;
      if (filterName == "LifecycleLiveItems")
        return NamedFiltersHandler.FilterLifecycleItems<T>(items, language, ContentLifecycleStatus.Live);
      if (filterName == "LifecyclePublishedItems")
        return NamedFiltersHandler.FilterPublishedLifecycleItems<T>(items, language);
      throw new ArgumentException("Invalid filter name {0}", filterName);
    }

    /// <summary>Tries to parse the name of the named filter</summary>
    /// <param name="filter">The filter.</param>
    /// <param name="filterName">Name of the filter.</param>
    /// <returns></returns>
    public static bool TryParseFilterName(string filter, out string filterName)
    {
      filterName = (string) null;
      if (string.IsNullOrEmpty(filter) || !filter.StartsWith("[") || !filter.EndsWith("]"))
        return false;
      filterName = filter.Replace("[", string.Empty).Replace("]", string.Empty);
      return true;
    }

    /// <summary>Filter items by state, content status and language</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="culture">The culture.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterLifecycleItems<T>(
      IQueryable<T> items,
      string language,
      LifecycleState lifecycleState,
      ContentLifecycleStatus status)
      where T : Content
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T> cDisplayClass60 = new NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.language = language;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.lifecycleState = lifecycleState;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.status = status;
      // ISSUE: reference to a compiler-generated field
      if (DataExtensions.AppSettings.ContextSettings.Multilingual && cDisplayClass60.language == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw())
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        return items.Where<T>(Expression.Lambda<Func<T, bool>>((Expression) Expression.AndAlso((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
        {
          ((ILifecycleDataItemGeneric) item).LanguageData,
          (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.AndAlso((Expression) Expression.OrElse(ld.Language == cDisplayClass60.language, (Expression) Expression.AndAlso(ld.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression1, typeof (ILifecycleDataItemGeneric)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ILifecycleDataItem.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (LanguageData.get_ContentState))), typeof (int)), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>)), FieldInfo.GetFieldFromHandle(__fieldref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>.lifecycleState), __typeref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>))), typeof (int)))), parameterExpression2)
        }), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Content.get_Status))), typeof (int)), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass60, typeof (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>)), FieldInfo.GetFieldFromHandle(__fieldref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>.status), __typeref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass6_0<T>))), typeof (int)))), parameterExpression1));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return items.Where<T>((Expression<Func<T, bool>>) (item => ((ILifecycleDataItemGeneric) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == cDisplayClass60.language && (int) ld.ContentState == (int) cDisplayClass60.lifecycleState)) && (int) item.Status == (int) cDisplayClass60.status));
    }

    /// <summary>
    /// Filters the lifecycle items, status (Live, Temp, Master) and language
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterLifecycleItems<T>(
      IQueryable<T> items,
      string language,
      ContentLifecycleStatus status)
      where T : Content
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NamedFiltersHandler.\u003C\u003Ec__DisplayClass7_0<T> cDisplayClass70 = new NamedFiltersHandler.\u003C\u003Ec__DisplayClass7_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.language = language;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass70.status = status;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass70.language == DataExtensions.AppSettings.Current.DefaultFrontendLanguage.GetLanguageKeyRaw())
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        return items.Where<T>(Expression.Lambda<Func<T, bool>>((Expression) Expression.AndAlso((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
        {
          ((ILifecycleDataItemGeneric) item).LanguageData,
          (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.OrElse(ld.Language == cDisplayClass70.language, (Expression) Expression.AndAlso(ld.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression1, typeof (ILifecycleDataItemGeneric)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ILifecycleDataItem.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), parameterExpression2)
        }), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Content.get_Status))), typeof (int)), (Expression) Expression.Convert((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass70, typeof (NamedFiltersHandler.\u003C\u003Ec__DisplayClass7_0<T>)), FieldInfo.GetFieldFromHandle(__fieldref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass7_0<T>.status), __typeref (NamedFiltersHandler.\u003C\u003Ec__DisplayClass7_0<T>))), typeof (int)))), parameterExpression1));
      }
      // ISSUE: reference to a compiler-generated field
      // ISSUE: reference to a compiler-generated field
      return items.Where<T>((Expression<Func<T, bool>>) (item => ((ILifecycleDataItemGeneric) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == cDisplayClass70.language)) && (int) item.Status == (int) cDisplayClass70.status));
    }

    /// <summary>
    /// Filters the lifecycle items by language and publish status
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    public static IQueryable<T> FilterPublishedLifecycleItems<T>(
      IQueryable<T> items,
      string language)
      where T : Content
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      NamedFiltersHandler.\u003C\u003Ec__DisplayClass8_0<T> cDisplayClass80 = new NamedFiltersHandler.\u003C\u003Ec__DisplayClass8_0<T>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass80.language = language;
      // ISSUE: reference to a compiler-generated field
      if (DataExtensions.AppSettings.ContextSettings.Multilingual && cDisplayClass80.language == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw())
      {
        ParameterExpression parameterExpression1;
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: reference to a compiler-generated field
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: type reference
        // ISSUE: method reference
        return items.Where<T>(Expression.Lambda<Func<T, bool>>((Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any)), new Expression[2]
        {
          ((ILifecycleDataItemGeneric) item).LanguageData,
          (Expression) Expression.Lambda<Func<LanguageData, bool>>((Expression) Expression.AndAlso((Expression) Expression.OrElse(ld.Language == cDisplayClass80.language, (Expression) Expression.AndAlso(ld.Language == default (string), (Expression) Expression.Equal((Expression) Expression.Property((Expression) Expression.Property((Expression) Expression.Convert((Expression) parameterExpression1, typeof (ILifecycleDataItemGeneric)), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ILifecycleDataItem.get_LanguageData))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ICollection<LanguageData>.get_Count), __typeref (ICollection<LanguageData>))), (Expression) Expression.Constant((object) 1, typeof (int))))), (Expression) Expression.Equal((Expression) Expression.Convert((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (LanguageData.get_ContentState))), typeof (int)), (Expression) Expression.Constant((object) 1, typeof (int)))), parameterExpression2)
        }), parameterExpression1));
      }
      // ISSUE: reference to a compiler-generated field
      return items.Where<T>((Expression<Func<T, bool>>) (item => ((ILifecycleDataItemGeneric) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == cDisplayClass80.language && (int) ld.ContentState == 1))));
    }

    /// <summary>
    /// Filters the lifecycle items by language and publish status
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    public static IEnumerable<T> FilterPublishedLifecycleItems<T>(
      IEnumerable<T> items,
      string language)
      where T : Content
    {
      return DataExtensions.AppSettings.ContextSettings.Multilingual && language == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw() ? items.Where<T>((Func<T, bool>) (item => ((ILifecycleDataItem) (object) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => (ld.Language == language || ld.Language == null && ((ILifecycleDataItem) (object) item).LanguageData.Count == 1) && ld.ContentState == LifecycleState.Published)) && item.Status == ContentLifecycleStatus.Live)) : items.Where<T>((Func<T, bool>) (item => ((ILifecycleDataItem) (object) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == language && ld.ContentState == LifecycleState.Published)) && item.Status == ContentLifecycleStatus.Live));
    }

    /// <summary>
    /// Filter items by state, approval workflow status and language
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <param name="workflowState">State of the workflow.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterWorkflowItems<T>(
      IQueryable<T> items,
      string language,
      ContentLifecycleStatus status,
      string workflowState)
      where T : Content
    {
      return items.Where<T>((Expression<Func<T, bool>>) (item => (int) item.Status == (int) status && ((IApprovalWorkflowItem) item).ApprovalWorkflowState == (Lstring) workflowState));
    }

    /// <summary>Filters the items.</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    /// <param name="status">The status.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterItems<T>(
      IQueryable<T> items,
      string language,
      ContentLifecycleStatus status,
      string state)
      where T : Content
    {
      if (!string.IsNullOrEmpty(state))
        return items.Where<T>((Expression<Func<T, bool>>) (item => (int) item.Status == (int) status && item.ContentState == state));
      return items.Where<T>((Expression<Func<T, bool>>) (item => (int) item.Status == (int) status && (item.ContentState == string.Empty || item.ContentState == default (string))));
    }

    /// <summary>Filters the items by language and state</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    /// <param name="language">The language.</param>
    /// <param name="state">The state.</param>
    /// <returns></returns>
    public static IQueryable<T> FilterItems<T>(
      IQueryable<T> items,
      string language,
      string state)
      where T : Content
    {
      if (!string.IsNullOrEmpty(state))
        return items.Where<T>((Expression<Func<T, bool>>) (item => item.ContentState == state));
      return items.Where<T>((Expression<Func<T, bool>>) (item => item.ContentState == string.Empty || item.ContentState == default (string)));
    }

    /// <summary>Filter items by state, content status and language</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="culture">The culture.</param>
    /// <param name="state">The state.</param>
    public static IEnumerable<T> FilterLifecycleItems<T>(
      IEnumerable<T> items,
      string language,
      ContentLifecycleStatus status)
      where T : Content
    {
      return DataExtensions.AppSettings.ContextSettings.Multilingual && language == DataExtensions.AppSettings.ContextSettings.DefaultFrontendLanguage.GetLanguageKeyRaw() ? items.Where<T>((Func<T, bool>) (item => ((ILifecycleDataItem) (object) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld =>
      {
        if (ld.Language == language)
          return true;
        return ld.Language == null && ((ILifecycleDataItem) (object) item).LanguageData.Count == 1;
      })) && item.Status == status)) : items.Where<T>((Func<T, bool>) (item => ((ILifecycleDataItem) (object) item).LanguageData.Any<LanguageData>((Func<LanguageData, bool>) (ld => ld.Language == language)) && item.Status == status));
    }

    internal static IQueryable<T> ApplyStatusProviderFilter<T>(
      IQueryable<T> items,
      string filterName,
      CultureInfo culture = null,
      string itemProvider = null)
      where T : IDataItem
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      if (itemProvider == null && items is ILinqQuery linqQuery)
        itemProvider = linqQuery.DataProvider.Name;
      IStatusProviderRegistry providerRegistry = SystemManager.StatusProviderRegistry;
      string filterString = filterName;
      Type itemType = items.ElementType;
      if ((object) itemType == null)
        itemType = typeof (T);
      string itemProvider1 = itemProvider;
      IEnumerable<Guid> filter;
      ref IEnumerable<Guid> local = ref filter;
      CultureInfo culture1 = culture;
      if (providerRegistry.TryGetMatchingFilterItemIds(filterString, itemType, itemProvider1, out local, culture1))
        items = items.Where<T>((Expression<Func<T, bool>>) (t => filter.Contains<Guid>(t.Id)));
      return items;
    }

    /// <summary>
    /// Filters items and returns recent 50 by DateCreated DESC.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="items">The items.</param>
    private static IQueryable<T> FilterRecentItems<T>(IQueryable<T> items) where T : Content => Queryable.Take<T>(items.Where<T>((Expression<Func<T, bool>>) (item => (int) item.Status == 2 && item.Visible)).OrderByDescending<T, DateTime>((Expression<Func<T, DateTime>>) (item => item.DateCreated)), 50);

    /// <summary>
    /// Determines whether the specified filter name is a lifecycle filter.
    /// </summary>
    /// <param name="filterName">Name of the filter.</param>
    /// <returns>
    ///   <c>true</c> if the specified filter name is lifecycle filter; otherwise, <c>false</c>.
    /// </returns>
    internal static bool IsLifecycleFilter(string filterName) => filterName == "ShowRecentLiveItems";
  }
}
