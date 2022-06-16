// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Basic.BasicQuery`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Telerik.Sitefinity.Data.Linq.Basic
{
  internal class BasicQuery<TSource> : 
    IOrderedQueryable<TSource>,
    IQueryable<TSource>,
    IEnumerable<TSource>,
    IEnumerable,
    IQueryable,
    IOrderedQueryable
  {
    private Expression expression;
    private IQueryProvider queryProvider;
    private IBasicQueryExecutor executor;

    internal BasicQuery(IBasicQueryExecutor executor) => this.executor = executor;

    internal BasicQuery(IBasicQueryExecutor executor, Expression expression)
      : this(executor)
    {
      this.expression = expression;
    }

    public IEnumerator<TSource> GetEnumerator() => this.Provider.Execute<IEnumerable<TSource>>(this.Expression).GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public Type ElementType => typeof (TSource);

    public Expression Expression
    {
      get
      {
        if (this.expression == null)
          this.expression = (Expression) Expression.Constant((object) this);
        return this.expression;
      }
    }

    public IQueryProvider Provider
    {
      get
      {
        if (this.queryProvider == null)
          this.queryProvider = (IQueryProvider) new BasicQueryProvider<TSource>(this.executor);
        return this.queryProvider;
      }
    }
  }
}
