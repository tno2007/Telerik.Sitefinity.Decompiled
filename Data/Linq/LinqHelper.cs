// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.LinqHelper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Data.Linq
{
  public class LinqHelper
  {
    internal static LinqHelper.ObjectActivator<T> GetActivator<T>(
      ConstructorInfo constructor)
    {
      ParameterInfo[] parameters = constructor.GetParameters();
      ParameterExpression array = Expression.Parameter(typeof (object[]), "args");
      Expression[] expressionArray = new Expression[parameters.Length];
      for (int index1 = 0; index1 < parameters.Length; ++index1)
      {
        Expression index2 = (Expression) Expression.Constant((object) index1);
        Type parameterType = parameters[index1].ParameterType;
        Expression expression = (Expression) Expression.Convert((Expression) Expression.ArrayIndex((Expression) array, index2), parameterType);
        expressionArray[index1] = expression;
      }
      return (LinqHelper.ObjectActivator<T>) Expression.Lambda(typeof (LinqHelper.ObjectActivator<T>), (Expression) Expression.New(constructor, expressionArray), array).Compile();
    }

    internal static bool IsProjection<TBaseItem, TDataItem>(ConstructorInfo constructorInfo) => constructorInfo != (ConstructorInfo) null || typeof (TBaseItem) != typeof (TDataItem);

    internal static bool IsAnonTypeProjection(ConstructorInfo constructorInfo) => constructorInfo != (ConstructorInfo) null;

    internal static void SetProperty(object instance, string propertyName, object value)
    {
      PropertyInfo property = instance.GetType().GetProperty(propertyName);
      if (property == (PropertyInfo) null)
        return;
      property.SetValue(instance, value, (object[]) null);
    }

    internal static bool IsPropertyValueMethodCallExpression(Expression ex) => ex.NodeType == ExpressionType.Call && ((MethodCallExpression) ex).Method.Name == "PropertyValue";

    internal static string HandlePropertyValueMethodCallExpression(Expression ex)
    {
      if (!LinqHelper.IsPropertyValueMethodCallExpression(ex))
        throw new Exception("TODO");
      return (string) ((ConstantExpression) ((MethodCallExpression) ex).Arguments[1]).Value;
    }

    internal static string MemberName<T>(Expression<Func<T, object>> expression)
    {
      if (expression.NodeType == ExpressionType.Lambda)
      {
        Expression expression1 = expression.Body;
        if (expression1.NodeType == ExpressionType.Convert)
          expression1 = ((UnaryExpression) expression1).Operand;
        if (expression1.NodeType == ExpressionType.MemberAccess)
          return ((MemberExpression) expression1).Member.Name;
        if (expression1.NodeType == ExpressionType.Call)
          return ((MethodCallExpression) expression1).Method.Name;
      }
      return (string) null;
    }

    internal delegate T ObjectActivator<T>(params object[] args);
  }
}
