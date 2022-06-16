// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.DynamicQueryable
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  /// <summary>
  /// 
  /// </summary>
  public static class DynamicQueryable
  {
    /// <summary>used only in unit tests</summary>
    public static event DynamicQueryable.ExpressionBuilded OnExpressionBuilded;

    private static void RiseOnExpressionBuilded(Expression ex)
    {
      if (DynamicQueryable.OnExpressionBuilded == null)
        return;
      DynamicQueryable.OnExpressionBuilded(ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IQueryable<T> Where<T>(
      this IQueryable<T> source,
      string predicate,
      params object[] values)
    {
      return (IQueryable<T>) source.Where(predicate, values);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IQueryable Where(
      this IQueryable source,
      string predicate,
      params object[] values)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (predicate == null)
        throw new ArgumentNullException(nameof (predicate));
      LambdaExpression lambda = DynamicExpression.ParseLambda(source.ElementType, typeof (bool), predicate, values);
      MethodCallExpression ex = Expression.Call(typeof (Queryable), nameof (Where), new Type[1]
      {
        source.ElementType
      }, source.Expression, (Expression) Expression.Quote((Expression) lambda));
      DynamicQueryable.RiseOnExpressionBuilded((Expression) ex);
      return source.Provider.CreateQuery((Expression) ex);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="selector"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IQueryable Select(
      this IQueryable source,
      string selector,
      params object[] values)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (selector == null)
        throw new ArgumentNullException(nameof (selector));
      LambdaExpression lambda = DynamicExpression.ParseLambda(source.ElementType, (Type) null, selector, values);
      return source.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), nameof (Select), new Type[2]
      {
        source.ElementType,
        lambda.Body.Type
      }, source.Expression, (Expression) Expression.Quote((Expression) lambda)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="ordering"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IQueryable<T> OrderBy<T>(
      this IQueryable<T> source,
      string ordering,
      params object[] values)
    {
      return (IQueryable<T>) source.OrderBy(ordering, values);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="ordering"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static IQueryable OrderBy(
      this IQueryable source,
      string ordering,
      params object[] values)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (ordering == null)
        throw new ArgumentNullException(nameof (ordering));
      ParameterExpression[] parameters = new ParameterExpression[1]
      {
        Expression.Parameter(source.ElementType, "item")
      };
      IEnumerable<DynamicOrdering> ordering1 = new ExpressionParser(parameters, ordering, values).ParseOrdering();
      Expression expression = source.Expression;
      string str1 = nameof (OrderBy);
      string str2 = "OrderByDescending";
      foreach (DynamicOrdering dynamicOrdering in ordering1)
      {
        expression = (Expression) Expression.Call(typeof (Queryable), dynamicOrdering.Ascending ? str1 : str2, new Type[2]
        {
          source.ElementType,
          dynamicOrdering.Selector.Type
        }, expression, (Expression) Expression.Quote((Expression) Expression.Lambda(dynamicOrdering.Selector, parameters)));
        str1 = "ThenBy";
        str2 = "ThenByDescending";
      }
      return source.Provider.CreateQuery(expression);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IQueryable Take(this IQueryable source, int count)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return source.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), nameof (Take), new Type[1]
      {
        source.ElementType
      }, source.Expression, (Expression) Expression.Constant((object) count)));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static IQueryable Skip(this IQueryable source, int count)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return source.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), nameof (Skip), new Type[1]
      {
        source.ElementType
      }, source.Expression, (Expression) Expression.Constant((object) count)));
    }

    /// <summary>
    /// Groups the elements of a sequence according to a specified key selector function and projects the elements for each group by using a specified function.
    /// </summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable" /> whose elements to group.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <param name="elementSelector">A function to map each source element to an element in grouping.</param>
    /// <param name="values">Parameters in the query</param>
    /// <returns>Queryable that contains a sequence of objects.</returns>
    public static IQueryable GroupBy(
      this IQueryable source,
      string keySelector,
      string elementSelector,
      params object[] values)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      if (keySelector == null)
        throw new ArgumentNullException(nameof (keySelector));
      if (elementSelector == null)
        throw new ArgumentNullException(nameof (elementSelector));
      LambdaExpression lambda1 = DynamicExpression.ParseLambda(source.ElementType, (Type) null, keySelector, values);
      LambdaExpression lambda2 = DynamicExpression.ParseLambda(source.ElementType, (Type) null, elementSelector, values);
      return source.Provider.CreateQuery((Expression) Expression.Call(typeof (Queryable), nameof (GroupBy), new Type[3]
      {
        source.ElementType,
        lambda1.Body.Type,
        lambda2.Body.Type
      }, source.Expression, (Expression) Expression.Quote((Expression) lambda1), (Expression) Expression.Quote((Expression) lambda2)));
    }

    /// <summary>Determines whether a sequence contains any elements.</summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable" /> to check for being empty</param>
    /// <returns>true if the source sequence contains any elements; otherwise, false.</returns>
    public static bool Any(this IQueryable source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return (bool) source.Provider.Execute((Expression) Expression.Call(typeof (Queryable), nameof (Any), new Type[1]
      {
        source.ElementType
      }, source.Expression));
    }

    /// <summary>Returns the number of elements in a sequence.</summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable" /> that contains the elements to be counted.</param>
    /// <returns>The number of elements in the input sequence.</returns>
    public static int Count(this IQueryable source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return (int) source.Provider.Execute((Expression) Expression.Call(typeof (Queryable), nameof (Count), new Type[1]
      {
        source.ElementType
      }, source.Expression));
    }

    /// <summary>Returns the first element of a sequence.</summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable" /> to return the first element of. </param>
    /// <param name="itemType">The type of the elements of <paramref name="source" /></param>
    /// <returns>The first element in <paramref name="source" />.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null</exception>
    /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
    public static object First(this IQueryable source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return source.Provider.Execute((Expression) Expression.Call(typeof (Queryable), nameof (First), new Type[1]
      {
        source.ElementType
      }, source.Expression));
    }

    /// <summary>
    /// Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable" /> to return the first element of. </param>
    /// <param name="itemType">The type of the elements of <paramref name="source" /></param>
    /// <returns>Null if source is empty; otherwise, the first element in source.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source" /> is null</exception>
    public static object FirstOrDefault(this IQueryable source)
    {
      if (source == null)
        throw new ArgumentNullException(nameof (source));
      return source.Provider.Execute((Expression) Expression.Call(typeof (Queryable), nameof (FirstOrDefault), new Type[1]
      {
        source.ElementType
      }, source.Expression));
    }

    /// <summary>Used only for unit tests</summary>
    /// <param name="buildedExpression">built expression</param>
    public delegate void ExpressionBuilded(Expression buildedExpression);
  }
}
