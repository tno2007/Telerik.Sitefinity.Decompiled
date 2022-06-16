// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Basic.BasicQueryProvider`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Data.Linq.Basic
{
  internal class BasicQueryProvider<TSource> : IQueryProvider
  {
    private IBasicQueryExecutor executor;

    internal BasicQueryProvider(IBasicQueryExecutor executor) => this.executor = executor;

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
      if (typeof (TElement) == typeof (TSource))
        return new BasicQuery<TSource>(this.executor, expression) as IQueryable<TElement>;
      throw new NotSupportedException();
    }

    public IQueryable CreateQuery(Expression expression) => (IQueryable) this.CreateQuery<TSource>(expression);

    TResult IQueryProvider.Execute<TResult>(Expression expression) => (TResult) this.Execute(expression);

    public object Execute(Expression expression)
    {
      object obj = this.executor.Execute(new BasicExpressionVisitor().Execute(expression));
      Type type = obj.GetType();
      if (expression.NodeType == ExpressionType.Call)
      {
        MethodCallExpression methodCallExpression = expression as MethodCallExpression;
        if (methodCallExpression.Method.DeclaringType == typeof (System.Linq.Queryable))
        {
          if (object.Equals((object) methodCallExpression.Method.ReturnType, (object) type) || typeof (IQueryable<TSource>).IsAssignableFrom(methodCallExpression.Method.ReturnType) && typeof (IEnumerable<TSource>).IsAssignableFrom(type))
            return obj;
          if (methodCallExpression.Method.GetParameters().Length == 1 && typeof (IQueryable).IsAssignableFrom(methodCallExpression.Method.GetParameters()[0].ParameterType) && typeof (IQueryable<TSource>).IsAssignableFrom(type))
          {
            object[] parameters = new object[1]{ obj };
            return methodCallExpression.Method.Invoke((object) null, parameters);
          }
          MethodInfo enumerable = this.ConvertToEnumerable(methodCallExpression.Method);
          if (enumerable != (MethodInfo) null)
          {
            object[] parameters = new object[1]{ obj };
            return enumerable.Invoke((object) null, parameters);
          }
        }
      }
      else if (expression.NodeType == ExpressionType.Constant && typeof (IQueryable<TSource>).IsAssignableFrom(expression.Type) && typeof (IEnumerable<TSource>).IsAssignableFrom(type))
        return obj;
      throw new NotSupportedException();
    }

    private MethodInfo ConvertToEnumerable(MethodInfo queryableMethod)
    {
      MethodInfo enumerable = ((IEnumerable<MethodInfo>) typeof (System.Linq.Enumerable).GetMethods(BindingFlags.Static | BindingFlags.Public)).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.Name == queryableMethod.Name)).Where<MethodInfo>((Func<MethodInfo, bool>) (x => x.IsGenericMethod == queryableMethod.IsGenericMethod)).Where<MethodInfo>((Func<MethodInfo, bool>) (x => ((IEnumerable<ParameterInfo>) x.GetParameters()).Count<ParameterInfo>() == 1 && ((IEnumerable<ParameterInfo>) queryableMethod.GetParameters()).Count<ParameterInfo>() == 2 || ((IEnumerable<ParameterInfo>) x.GetParameters()).Count<ParameterInfo>() == ((IEnumerable<ParameterInfo>) queryableMethod.GetParameters()).Count<ParameterInfo>())).FirstOrDefault<MethodInfo>();
      if (enumerable != (MethodInfo) null && queryableMethod.IsGenericMethod)
      {
        Type[] genericArguments = queryableMethod.GetGenericArguments();
        enumerable = enumerable.MakeGenericMethod(genericArguments);
      }
      return enumerable;
    }
  }
}
