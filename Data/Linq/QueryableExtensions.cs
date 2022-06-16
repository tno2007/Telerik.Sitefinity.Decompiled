// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.QueryableExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data
{
  /// <summary>
  /// Helper class which extends the IQueryable interface with additional methods
  /// </summary>
  public static class QueryableExtensions
  {
    /// <summary>
    /// Returns union of two queries, or the second one if the first is null
    /// </summary>
    /// <typeparam name="T">The type of query objects</typeparam>
    /// <param name="left">The first query</param>
    /// <param name="right">The second query</param>
    /// <returns>The union of the two queries</returns>
    public static IQueryable<T> UnionOrRight<T>(
      this IQueryable<T> left,
      IQueryable<T> right)
    {
      return left == null ? right : Queryable.Union<T>(left, (IEnumerable<T>) right);
    }

    /// <summary>
    /// Returns union of query and IEnumerable, or the IEnumerable as IQuerable if the first is null
    /// </summary>
    /// <typeparam name="T">The type of query objects</typeparam>
    /// <param name="left">The query</param>
    /// <param name="right">The IEnumerable object</param>
    /// <returns>Returns union of the query and the IEnumerable object</returns>
    public static IQueryable<T> UnionOrRight<T>(
      this IQueryable<T> left,
      IEnumerable<T> right)
    {
      return left == null ? right.AsQueryable<T>() : Queryable.Union<T>(left, right);
    }

    /// <summary>
    /// Filters out items that are not translated in <paramref name="culture" /> (i.e. leave only items that are translated)
    /// </summary>
    /// <typeparam name="TDataItem">Localizable type</typeparam>
    /// <param name="source">Query so far</param>
    /// <param name="culture">Culture to filter by</param>
    /// <returns>Query of items that are translated in <paramref name="culture" /></returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="source" /> or <paramref name="culture" /> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">When <typeparamref name="TDataItem" /> is not decorated with <c>RequiredLocalizablePropertyAttribute</c></exception>
    public static IQueryable<TDataItem> WhereQueryTypeIsTranslatedInCulture<TDataItem>(
      this IQueryable<TDataItem> source,
      CultureInfo culture)
      where TDataItem : ILocalizable, IDataItem
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (!appSettings.Multilingual || appSettings.DefaultFrontendLanguage == culture)
        return source;
      string requiredPropertyName = ILocalizableExtensions.GetRequiredPropertyName(typeof (TDataItem));
      return source.WhereLstringCompares<TDataItem>(requiredPropertyName, QueryCompareMode.NotEqual, (string) null, culture);
    }

    public static IQueryable<TDataItem> WhereLstringCompares<TDataItem>(
      this IQueryable<TDataItem> source,
      string propName,
      QueryCompareMode mode,
      string val,
      CultureInfo culture)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (propName == null)
        throw new ArgumentNullException(nameof (propName));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      PropertyInfo property = typeof (TDataItem).GetProperty(propName);
      if (property == (PropertyInfo) null)
        throw new ArgumentException("Property not found");
      if (property.PropertyType != typeof (Lstring))
        throw new ArgumentException("Property is not Lstring");
      PropertyInfo member = (PropertyInfo) new LstringPropertyInfo(property, ILocalizableExtensions.GetLstringFieldName(typeof (TDataItem), propName, culture));
      ParameterExpression parameterExpression = Expression.Parameter(typeof (TDataItem), "cnt");
      MemberExpression left = Expression.MakeMemberAccess((Expression) parameterExpression, (MemberInfo) member);
      ConstantExpression right = Expression.Constant((object) val, typeof (string));
      BinaryExpression body;
      if (mode != QueryCompareMode.Equal)
      {
        if (mode != QueryCompareMode.NotEqual)
          throw new NotImplementedException();
        body = Expression.NotEqual((Expression) left, (Expression) right);
      }
      else
        body = Expression.Equal((Expression) left, (Expression) right);
      Expression<Func<TDataItem, bool>> expression = Expression.Lambda<Func<TDataItem, bool>>((Expression) body, parameterExpression);
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Where", new Type[1]
      {
        typeof (TDataItem)
      }, source.Expression, (Expression) expression);
      return source.Provider.CreateQuery<TDataItem>((Expression) methodCallExpression);
    }

    /// <summary>
    /// Filters out items that are translated in <paramref name="culture" /> (i.e. leave only items that are NOT translated)
    /// </summary>
    /// <typeparam name="TDataItem">Localizable type</typeparam>
    /// <param name="source">Query so far</param>
    /// <param name="culture">Culture to filter by</param>
    /// <returns>Query of items that are NOT translated in <paramref name="culture" /></returns>
    /// <exception cref="T:System.ArgumentNullException">When <paramref name="source" /> or <paramref name="culture" /> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">When <typeparamref name="TDataItem" /> is not decorated with <c>RequiredLocalizablePropertyAttribute</c></exception>
    public static IQueryable<TDataItem> WhereQueryTypeIsNotTranslatedInCulture<TDataItem>(
      this IQueryable<TDataItem> source,
      CultureInfo culture)
      where TDataItem : ILocalizable, IDataItem
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      IAppSettings appSettings = SystemManager.CurrentContext.AppSettings;
      if (!appSettings.Multilingual || appSettings.DefaultFrontendLanguage == culture)
        return Enumerable.Empty<TDataItem>().AsQueryable<TDataItem>();
      string requiredPropertyName = ILocalizableExtensions.GetRequiredPropertyName(typeof (TDataItem));
      return source.WhereLstringCompares<TDataItem>(requiredPropertyName, QueryCompareMode.Equal, (string) null, culture);
    }

    /// <summary>
    /// Extension method that adds the support for querying Lstring properties in any (all available) cultures,
    /// not just the current one.
    /// </summary>
    /// <typeparam name="TDataItem">The type of data item being queried</typeparam>
    /// <param name="source">The original IQueryable object</param>
    /// <param name="predicate">The predicate used for querying</param>
    /// <returns>IQueryable object with appended "any culture" filter</returns>
    public static IQueryable<TDataItem> WhereAnyCulture<TDataItem>(
      this IQueryable<TDataItem> source,
      Expression<Func<TDataItem, bool>> predicate)
      where TDataItem : IDynamicFieldsContainer
    {
      QueryableExtensions.ValidateWhereAnyCulturePredicate<TDataItem>(predicate);
      string propertyNameFromLambda = QueryableExtensions.GetPropertyNameFromLambda<TDataItem>(predicate);
      Expression constantValue = (Expression) Expression.Constant((object) QueryableExtensions.GetConstantValueFromLambda<TDataItem>(predicate));
      MethodInfo method = typeof (Lstring).GetMethod("GetString", new Type[2]
      {
        typeof (CultureInfo),
        typeof (bool)
      });
      Type type = typeof (TDataItem);
      ParameterExpression parameterExpression = Expression.Parameter(type, "p");
      PropertyInfo property = type.GetProperty(propertyNameFromLambda);
      Expression propertyAccess = (Expression) Expression.MakeMemberAccess((Expression) parameterExpression, (MemberInfo) property);
      Expression predicateBody = (Expression) null;
      QueryableExtensions.CreateCultureExpression(CultureInfo.InvariantCulture, propertyAccess, method, constantValue, ref predicateBody);
      foreach (CultureInfo culture in (IEnumerable<CultureInfo>) AppSettings.CurrentSettings.AllLanguages.Values)
        QueryableExtensions.CreateCultureExpression(culture, propertyAccess, method, constantValue, ref predicateBody);
      MethodCallExpression methodCallExpression = Expression.Call(typeof (Queryable), "Where", new Type[1]
      {
        source.ElementType
      }, source.Expression, (Expression) Expression.Lambda<Func<TDataItem, bool>>(predicateBody, parameterExpression));
      return source.Provider.CreateQuery<TDataItem>((Expression) methodCallExpression);
    }

    public static IQueryable Cast(this IQueryable queryable, Type castTo) => (IQueryable) MethodInfoResolver.GetMethodInfo((Expression<Action>) (() => Queryable.Cast<object>(default (IQueryable)))).GetGenericMethodDefinition().MakeGenericMethod(castTo).Invoke((object) null, new object[1]
    {
      (object) queryable
    });

    /// <summary>
    /// Loads the items in the queryable into a list and eagerly loads the objects from the specified associations.
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data item.</typeparam>
    /// <param name="queryable">The queryable.</param>
    /// <param name="nonFilteredQuery">Query with all items. It is important to not have Take applied to this query.</param>
    /// <returns>List of objects with eagerly loaded associations.</returns>
    internal static List<TDataItem> IncludeToList<TDataItem>(
      this IQueryable<TDataItem> queryable,
      IQueryable<TDataItem> nonFilteredQuery)
      where TDataItem : IDataItem
    {
      Guid[] ids = queryable.Select<TDataItem, Guid>((Expression<Func<TDataItem, Guid>>) (t => (t as IDataItem).Id)).ToArray<Guid>();
      Dictionary<Guid, int> dictionary = new Dictionary<Guid, int>(ids.Length);
      for (int index = 0; index < ids.Length; ++index)
        dictionary[ids[index]] = index;
      TDataItem[] source1 = new TDataItem[ids.Length];
      if (ids.Length <= 200)
      {
        IQueryable<TDataItem> source2 = nonFilteredQuery;
        Expression<Func<TDataItem, bool>> predicate = (Expression<Func<TDataItem, bool>>) (t => ids.Contains<Guid>(t.Id));
        foreach (TDataItem dataItem in (IEnumerable<TDataItem>) source2.Where<TDataItem>(predicate))
          source1[dictionary[dataItem.Id]] = dataItem;
      }
      else
      {
        int num = (ids.Length + 200 - 1) / 200;
        for (int index = 0; index < num; ++index)
        {
          Guid[] batch = ((IEnumerable<Guid>) ids).Skip<Guid>(index * 200).Take<Guid>(200).ToArray<Guid>();
          IQueryable<TDataItem> source3 = nonFilteredQuery;
          Expression<Func<TDataItem, bool>> predicate = (Expression<Func<TDataItem, bool>>) (t => batch.Contains<Guid>(t.Id));
          foreach (TDataItem dataItem in (IEnumerable<TDataItem>) source3.Where<TDataItem>(predicate))
            source1[dictionary[dataItem.Id]] = dataItem;
        }
      }
      return ((IEnumerable<TDataItem>) source1).ToList<TDataItem>();
    }

    private static void CreateCultureExpression(
      CultureInfo culture,
      Expression propertyAccess,
      MethodInfo lstringGetValue,
      Expression constantValue,
      ref Expression predicateBody)
    {
      Expression expression1 = (Expression) Expression.Constant((object) culture);
      Expression expression2 = (Expression) Expression.Constant((object) false);
      Expression right = (Expression) Expression.Equal((Expression) Expression.Call(propertyAccess, lstringGetValue, expression1, expression2), constantValue);
      if (predicateBody == null)
        predicateBody = right;
      else
        predicateBody = (Expression) Expression.Or(predicateBody, right);
    }

    private static void ValidateWhereAnyCulturePredicate<TDataItem>(
      Expression<Func<TDataItem, bool>> predicate)
    {
      if (predicate.Body.NodeType != ExpressionType.Equal)
        throw new ArgumentException("WhereAnyCulture extension method supports only one condition. Multiple conditions such as - x.Title = \"Value\" || x.Title = \"Value 1\" - are not supported.");
      PropertyInfo member = ((MemberExpression) ((BinaryExpression) predicate.Body).Left).Member as PropertyInfo;
      if (member == (PropertyInfo) null)
        throw new ArgumentException("The property used with WhereAnyCulture extension method on the type '{0}' cannot be found.", typeof (TDataItem).FullName);
      if (member.PropertyType != typeof (Lstring))
        throw new ArgumentException("WhereAnyCulture extension method can be used only with properties of type Lstring.");
    }

    private static string GetPropertyNameFromLambda<TDataItem>(
      Expression<Func<TDataItem, bool>> predicate)
    {
      return ((MemberExpression) ((BinaryExpression) predicate.Body).Left).Member.Name;
    }

    private static string GetConstantValueFromLambda<TDataItem>(
      Expression<Func<TDataItem, bool>> predicate)
    {
      return ((UnaryExpression) ((BinaryExpression) predicate.Body).Right).Operand.ToString().Trim('"');
    }
  }
}
