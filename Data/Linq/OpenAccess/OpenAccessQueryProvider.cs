// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.OpenAccess.OpenAccessQueryProvider`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.OpenAccess;

namespace Telerik.Sitefinity.Data.Linq.OpenAccess
{
  public class OpenAccessQueryProvider<TBaseItem, TDataItem> : 
    ISitefinityQueryProvider,
    IQueryProvider
  {
    private IOpenAccessDataProvider openAccessDataProvider;
    private Expression expression;
    private OpenAccessExpressionVisitor<TBaseItem, TDataItem> expressionVisitor;
    private IQueryable currentQuery;
    private IQueryProvider openAccessQueryProvider;

    public OpenAccessQueryProvider(IOpenAccessDataProvider dataProvider) => this.openAccessDataProvider = dataProvider != null ? dataProvider : throw new ArgumentNullException(nameof (dataProvider));

    public OpenAccessQueryProvider(IOpenAccessDataProvider dataProvider, Expression expression)
      : this(dataProvider)
    {
      this.expression = expression;
    }

    public Expression Expression
    {
      get
      {
        if (this.expression == null)
          this.expression = this.Context.GetAll<TDataItem>().Expression;
        return this.expression;
      }
      set => this.expression = value;
    }

    private OpenAccessContext Context => (OpenAccessContext) this.openAccessDataProvider.GetContext();

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
      this.currentQuery = this.GetOpenAccessQueryProvider<TDataItem>().CreateQuery(this.EnhanceExpression(expression));
      return (IQueryable<TElement>) new LinqQuery<TElement, TDataItem>((DataProviderBase) this.openAccessDataProvider, this.Expression, this.currentQuery, (IQueryProvider) this);
    }

    public IQueryable CreateQuery(Expression expression)
    {
      Expression expression1 = this.EnhanceExpression(expression);
      this.currentQuery = this.HandleConstantQuery(expression1) ?? this.GetOpenAccessQueryProvider<TDataItem>().CreateQuery(expression1);
      return (IQueryable) new LinqQuery<TDataItem, TDataItem>((DataProviderBase) this.openAccessDataProvider, this.Expression, this.currentQuery, (IQueryProvider) this);
    }

    private IQueryable HandleConstantQuery(Expression expression) => expression is ConstantExpression constantExpression && constantExpression.Value.GetType().GetGenericArguments().Length == 1 ? (IQueryable) constantExpression.Value : (IQueryable) null;

    TResult IQueryProvider.Execute<TResult>(Expression expression)
    {
      object obj = this.Execute(expression);
      return obj == null ? default (TResult) : (TResult) obj;
    }

    public object Execute(Expression expression)
    {
      if (this.currentQuery == null || this.currentQuery.Expression != expression)
        this.currentQuery = this.CreateQuery(expression);
      if (expression == null)
        return (object) (this.currentQuery = this.CreateQuery(expression));
      Expression expression1 = this.EnhanceExpression(expression);
      return DataItemEnumerator.EnsureProviderSet(this.GetOpenAccessQueryProvider<TDataItem>().Execute(expression1), (IDataProviderBase) this.openAccessDataProvider);
    }

    private Expression EnhanceExpression(Expression expression)
    {
      if (this.expressionVisitor == null)
        this.expressionVisitor = new OpenAccessExpressionVisitor<TBaseItem, TDataItem>();
      this.Expression = this.expressionVisitor.Enhance(expression);
      return this.Expression;
    }

    private IQueryProvider GetOpenAccessQueryProvider<T>() => this.openAccessQueryProvider ?? (this.openAccessQueryProvider = this.Context.GetAll<T>().Provider);
  }
}
