// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Dynamic.DynamicExpression
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Data.Linq.Dynamic
{
  /// <summary>
  /// 
  /// </summary>
  public static class DynamicExpression
  {
    /// <summary>
    /// 
    /// </summary>
    /// <param name="resultType"></param>
    /// <param name="expression"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Expression Parse(
      Type resultType,
      string expression,
      params object[] values)
    {
      return new ExpressionParser((ParameterExpression[]) null, expression, values).Parse(resultType);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="itType"></param>
    /// <param name="resultType"></param>
    /// <param name="expression"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static LambdaExpression ParseLambda(
      Type itType,
      Type resultType,
      string expression,
      params object[] values)
    {
      return DynamicExpression.ParseLambda(new ParameterExpression[1]
      {
        Expression.Parameter(itType, "item")
      }, resultType, expression, values);
    }

    public static LambdaExpression ParseLambda(
      Type itType,
      Type dynamicType,
      Type resultType,
      string expression,
      params object[] values)
    {
      return DynamicExpression.ParseLambda(new ParameterExpression[1]
      {
        Expression.Parameter(itType, "item")
      }, dynamicType, resultType, expression, values);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="parameters"></param>
    /// <param name="resultType"></param>
    /// <param name="expression"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static LambdaExpression ParseLambda(
      ParameterExpression[] parameters,
      Type resultType,
      string expression,
      params object[] values)
    {
      return Expression.Lambda(new ExpressionParser(parameters, expression, values).Parse(resultType), parameters);
    }

    public static LambdaExpression ParseLambda(
      ParameterExpression[] parameters,
      Type dynamicType,
      Type resultType,
      string expression,
      params object[] values)
    {
      return Expression.Lambda(new ExpressionParser(dynamicType, parameters, expression, values).Parse(resultType), parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="S"></typeparam>
    /// <param name="expression"></param>
    /// <param name="values"></param>
    /// <returns></returns>
    public static Expression<Func<T, S>> ParseLambda<T, S>(
      string expression,
      params object[] values)
    {
      return (Expression<Func<T, S>>) DynamicExpression.ParseLambda(typeof (T), typeof (S), expression, values);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static Type CreateClass(params DynamicProperty[] properties) => ClassFactory.Instance.GetDynamicClass((IEnumerable<DynamicProperty>) properties);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="properties"></param>
    /// <returns></returns>
    public static Type CreateClass(IEnumerable<DynamicProperty> properties) => ClassFactory.Instance.GetDynamicClass(properties);
  }
}
