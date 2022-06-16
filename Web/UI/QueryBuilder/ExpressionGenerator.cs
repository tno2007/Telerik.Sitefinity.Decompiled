// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.ExpressionGenerator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Web.Model;

namespace Telerik.Sitefinity.Web.UI
{
  /// <summary>
  /// Generates an expression tree from a given QueryInfo object. Used by the QueryBuilder when filtering items.
  /// </summary>
  public class ExpressionGenerator
  {
    private QueryData queryData;

    /// <summary>
    /// Creates a new instance of ExpressionGenerator with the given QueryInfo
    /// </summary>
    /// <param name="queryData">The QueryData object to use when generating the expression</param>
    public ExpressionGenerator(QueryData queryData) => this.queryData = queryData;

    /// <summary>Generates an expression tree for the QueryInfo</summary>
    /// <returns></returns>
    public Expression Generate()
    {
      QueryItem[] queryItemArray = this.queryData != null ? this.queryData.QueryItems : throw new ArgumentNullException("queryData");
      return queryItemArray.Length != 0 ? this.GenerateInternal(queryItemArray) : throw new ArgumentException("The query is empty");
    }

    /// <summary>
    /// Recursively generate an exrpession given a collection of items or a single query item
    /// </summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    protected Expression GenerateInternal(params QueryItem[] items)
    {
      if (items.Length == 1 && !items[0].IsGroup)
      {
        Type type = Type.GetType(items[0].Condition.FieldType);
        Expression left = (Expression) Expression.Parameter(type, items[0].Condition.FieldName);
        if (items[0].Value == null)
          throw new ArgumentException("Please speficy a value for all conditions");
        Expression right;
        try
        {
          right = (Expression) Expression.Constant((object) items[0].Value, type);
        }
        catch (ArgumentException ex)
        {
          throw new ArgumentException("The selected value does not match the type of the property");
        }
        return (Expression) Expression.MakeBinary(this.GetExpressionTypeByOperator(items[0].Condition.Operator), left, right);
      }
      if (items.Length == 1 && items[0].IsGroup)
        return this.GenerateInternal(this.queryData.GetImmediateChildren(items[0]).ToArray<QueryItem>());
      if (items.Length == 2)
      {
        Expression left = this.GenerateInternal(items[0]);
        Expression right = this.GenerateInternal(items[1]);
        return (Expression) Expression.MakeBinary(this.GetExpressionTypeByJoin(items[1].Join), left, right);
      }
      Expression left1 = this.GenerateInternal(((IEnumerable<QueryItem>) items).Take<QueryItem>(items.Length - 1).ToArray<QueryItem>());
      Expression right1 = this.GenerateInternal(items[items.Length - 1]);
      return (Expression) Expression.MakeBinary(this.GetExpressionTypeByJoin(items[items.Length - 1].Join), left1, right1);
    }

    protected ExpressionType GetExpressionTypeByOperator(string operatorName)
    {
      if (operatorName == "=")
        return ExpressionType.Equal;
      if (operatorName == "<>")
        return ExpressionType.NotEqual;
      if (operatorName == ">")
        return ExpressionType.GreaterThan;
      if (operatorName == ">=")
        return ExpressionType.GreaterThanOrEqual;
      if (operatorName == "<")
        return ExpressionType.LessThan;
      return operatorName == "<=" ? ExpressionType.LessThanOrEqual : ExpressionType.Equal;
    }

    /// <summary>Gets the expression type by join.</summary>
    /// <param name="joinType">Type of the join.</param>
    /// <returns></returns>
    protected ExpressionType GetExpressionTypeByJoin(string joinType) => joinType == "AND" || !(joinType == "OR") ? ExpressionType.AndAlso : ExpressionType.OrElse;
  }
}
