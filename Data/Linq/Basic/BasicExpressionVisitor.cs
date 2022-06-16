// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Basic.BasicExpressionVisitor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Data.Linq.Basic
{
  internal class BasicExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
  {
    private static readonly string[] SupportedFilterMethods = new string[4]
    {
      "Contains",
      "StartsWith",
      "EndsWith",
      "Equals"
    };
    private QueryArgs queryArgs;
    private string currentQueryableMethod;

    public BasicExpressionVisitor() => this.queryArgs = new QueryArgs();

    internal QueryArgs Execute(Expression expression)
    {
      this.Visit(expression);
      return this.queryArgs;
    }

    protected override Expression VisitMethodCall(MethodCallExpression node)
    {
      if (node.Method.DeclaringType == typeof (System.Linq.Queryable))
      {
        this.currentQueryableMethod = node.Method.Name;
        switch (node.Method.Name)
        {
          case "Any":
            this.currentQueryableMethod = "Where";
            this.queryArgs.LastAction = "Any";
            if (node.Arguments.Count <= 1)
              break;
            goto case "OrderBy";
          case "Count":
          case "LongCount":
            this.currentQueryableMethod = "Where";
            this.queryArgs.LastAction = node.Method.Name;
            if (node.Arguments.Count <= 1)
              break;
            goto case "OrderBy";
          case "First":
          case "FirstOrDefault":
            this.currentQueryableMethod = "Where";
            this.queryArgs.FakePagingArgs.Take = new int?(1);
            this.queryArgs.FakePagingArgs.Skip = new int?(0);
            if (node.Arguments.Count <= 1)
              break;
            goto case "OrderBy";
          case "OrderBy":
          case "OrderByDescending":
          case "Where":
            this.Visit(node.Arguments[1]);
            this.currentQueryableMethod = (string) null;
            break;
          case "Single":
          case "SingleOrDefault":
            this.currentQueryableMethod = "Where";
            this.queryArgs.FakePagingArgs.Take = new int?(2);
            this.queryArgs.FakePagingArgs.Skip = new int?(0);
            if (node.Arguments.Count <= 1)
              break;
            goto case "OrderBy";
          case "Skip":
          case "Take":
            this.ProcessPaging(node.Arguments[1], node.Method.Name);
            break;
          default:
            this.NotSupported("The method {0} is not supported".Arrange((object) node.Method.Name));
            break;
        }
        if (this.queryArgs.QueryType == (Type) null)
          this.queryArgs.QueryType = node.Type;
      }
      return base.VisitMethodCall(node);
    }

    protected override Expression VisitConstant(ConstantExpression node)
    {
      if (node.Type.IsGenericType && node.Type.GetGenericTypeDefinition() == typeof (BasicQuery<>))
      {
        this.SetFakePaging();
        this.SetLastAction();
      }
      return base.VisitConstant(node);
    }

    protected override Expression VisitMember(MemberExpression node)
    {
      if (!(this.currentQueryableMethod == "OrderBy") && !(this.currentQueryableMethod == "OrderByDescending"))
        return base.VisitMember(node);
      if (this.queryArgs.OrderArgs.MemberName != null)
        this.NotSupported("OrderBy is specified more than once");
      this.queryArgs.OrderArgs.Direction = !(this.currentQueryableMethod == "OrderByDescending") ? new QueryArgs.Order.Directions?(QueryArgs.Order.Directions.Ascending) : new QueryArgs.Order.Directions?(QueryArgs.Order.Directions.Descending);
      this.queryArgs.OrderArgs.MemberName = node.Member.Name;
      return (Expression) node;
    }

    protected override Expression VisitLambda<T>(Expression<T> node)
    {
      if (!(this.currentQueryableMethod == "Where") || node.Body.NodeType != ExpressionType.Call)
        return base.VisitLambda<T>(node);
      MethodCallExpression body = node.Body as MethodCallExpression;
      if (body.Method.DeclaringType == typeof (System.Linq.Enumerable) && body.Method.Name == "Contains")
        this.ProcessArrayContains(body);
      if (body.Method.DeclaringType == typeof (string) && ((IEnumerable<string>) BasicExpressionVisitor.SupportedFilterMethods).Contains<string>(body.Method.Name))
        this.ProcessStringFilter(body);
      return (Expression) node;
    }

    protected override Expression VisitBinary(BinaryExpression node)
    {
      if (this.currentQueryableMethod == "Where")
      {
        if (node.Left.NodeType == ExpressionType.MemberAccess)
        {
          string name = (node.Left as MemberExpression).Member.Name;
          object expression = this.EvaluateExpression(node.Right);
          this.queryArgs.AddFilter(new QueryArgs.Member(name), Enum.GetName(typeof (ExpressionType), (object) node.NodeType), expression);
          return (Expression) node;
        }
        if (node.Left.NodeType == ExpressionType.Call)
        {
          MethodCallExpression left = node.Left as MethodCallExpression;
          if (left.Object != null && left.Object.NodeType == ExpressionType.MemberAccess && left.Arguments.Count == 0)
          {
            string expression = this.EvaluateExpression<string>(node.Right);
            this.queryArgs.AddFilter(new QueryArgs.Member((left.Object as MemberExpression).Member.Name, left.Method.Name), Enum.GetName(typeof (ExpressionType), (object) node.NodeType), (object) expression);
            return (Expression) node;
          }
          this.NotSupported("Only one method call (without parameters) is supported");
        }
        if (node.NodeType != ExpressionType.AndAlso)
          this.NotSupported("Only the AndAlso operator is supported");
      }
      return base.VisitBinary(node);
    }

    private void ProcessArrayContains(MethodCallExpression node)
    {
      object obj = (object) null;
      if (node.Arguments[0].NodeType == ExpressionType.MemberAccess || node.Arguments[0].NodeType == ExpressionType.NewArrayInit)
        obj = this.EvaluateExpression(node.Arguments[0]);
      if (node.Arguments[1].NodeType != ExpressionType.MemberAccess || obj == null)
        return;
      this.queryArgs.AddFilter(new QueryArgs.Member((node.Arguments[1] as MemberExpression).Member.Name), node.Method.Name, obj);
    }

    private void ProcessStringFilter(MethodCallExpression node)
    {
      Expression exp = node.Arguments[0];
      if (exp.NodeType != ExpressionType.Constant && exp.NodeType != ExpressionType.Call && exp.NodeType != ExpressionType.MemberAccess)
        return;
      if (node.Object != null && node.Object.NodeType == ExpressionType.MemberAccess)
      {
        string expression = this.EvaluateExpression<string>(exp);
        this.queryArgs.AddFilter(new QueryArgs.Member((node.Object as MemberExpression).Member.Name), node.Method.Name, (object) expression);
      }
      else
      {
        if (node.Object.NodeType != ExpressionType.Call)
          return;
        MethodCallExpression methodCallExpression = node.Object as MethodCallExpression;
        if (methodCallExpression.Object.NodeType != ExpressionType.MemberAccess)
          return;
        string expression = this.EvaluateExpression<string>(exp);
        this.queryArgs.AddFilter(new QueryArgs.Member((methodCallExpression.Object as MemberExpression).Member.Name, methodCallExpression.Method.Name), node.Method.Name, (object) expression);
      }
    }

    private void ProcessPaging(Expression argument, string methodName)
    {
      if (argument.NodeType == ExpressionType.Constant || argument.NodeType == ExpressionType.Call)
      {
        int expression = this.EvaluateExpression<int>(argument);
        if (methodName == "Skip")
        {
          if (this.queryArgs.PagingArgs.Skip.HasValue)
            this.NotSupported(string.Format("{0} is specified more than once.", (object) methodName));
          this.queryArgs.PagingArgs.Skip = new int?(expression);
        }
        else
        {
          if (!(methodName == "Take"))
            return;
          if (this.queryArgs.PagingArgs.Take.HasValue)
            this.NotSupported(string.Format("{0} is specified more than once.", (object) methodName));
          this.queryArgs.PagingArgs.Take = new int?(expression);
        }
      }
      else
        this.NotSupported(string.Format("Method {0} does not support such operations.", (object) methodName));
    }

    private void SetFakePaging()
    {
      if (!this.queryArgs.PagingArgs.Take.HasValue)
        this.queryArgs.PagingArgs.Take = this.queryArgs.FakePagingArgs.Take;
      if (this.queryArgs.PagingArgs.Skip.HasValue)
        return;
      this.queryArgs.PagingArgs.Skip = this.queryArgs.FakePagingArgs.Skip;
    }

    private void SetLastAction()
    {
      if (this.queryArgs.LastAction != null)
        return;
      this.queryArgs.LastAction = "List";
    }

    private TResult EvaluateExpression<TResult>(Expression exp) => (TResult) this.EvaluateExpression(exp);

    private object EvaluateExpression(Expression exp)
    {
      if (exp.NodeType == ExpressionType.Constant && exp is ConstantExpression constantExpression)
        return constantExpression.Value;
      if (exp.NodeType == ExpressionType.MemberAccess)
      {
        MemberExpression memberExpression = (MemberExpression) exp;
        if (memberExpression.Expression.NodeType == ExpressionType.Constant)
        {
          ConstantExpression expression = (ConstantExpression) memberExpression.Expression;
          if (memberExpression.Member.MemberType == MemberTypes.Field)
          {
            FieldInfo member = memberExpression.Member as FieldInfo;
            object obj = member.GetValue(expression.Value);
            return member.FieldType == typeof (Guid) ? obj : (object) (obj as IEnumerable);
          }
        }
      }
      return Expression.Lambda(exp).Compile().DynamicInvoke();
    }

    private void NotSupported(string message) => throw new NotSupportedException(message);
  }
}
