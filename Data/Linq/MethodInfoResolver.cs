// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.MethodInfoResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Contains method for retrieving <see cref="T:System.Reflection.MethodInfo" /> from lambda expressions.
  /// </summary>
  public static class MethodInfoResolver
  {
    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo(Expression<Action> expression) => MethodInfoResolver.GetMethodInfo((LambdaExpression) expression);

    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <typeparam name="T">Type of method's parameter.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo<T>(Expression<Action<T>> expression) => MethodInfoResolver.GetMethodInfo((LambdaExpression) expression);

    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <typeparam name="T1">Type of method's first parameter.</typeparam>
    /// <typeparam name="T2">Type of method's second parameter.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo<T1, T2>(Expression<Action<T1, T2>> expression) => MethodInfoResolver.GetMethodInfo((LambdaExpression) expression);

    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <typeparam name="T">Type of method's parameter.</typeparam>
    /// <typeparam name="TResult">Method's return type.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo<T, TResult>(
      Expression<Func<T, TResult>> expression)
    {
      return MethodInfoResolver.GetMethodInfo((LambdaExpression) expression);
    }

    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <typeparam name="T1">Type of method's first parameter.</typeparam>
    /// <typeparam name="T2">Type of method's second parameter.</typeparam>
    /// <typeparam name="TResult">Method's return type.</typeparam>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo<T1, T2, TResult>(
      Expression<Func<T1, T2, TResult>> expression)
    {
      return MethodInfoResolver.GetMethodInfo((LambdaExpression) expression);
    }

    /// <summary>
    /// Given a lambda expression that calls a method, returns the method info.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>Retrieved method info.</returns>
    public static MethodInfo GetMethodInfo(LambdaExpression expression)
    {
      if (!(expression.Body is MethodCallExpression body))
        throw new ArgumentException("Invalid Expression. Expression should consist of a Method call only.");
      return body.Method;
    }
  }
}
