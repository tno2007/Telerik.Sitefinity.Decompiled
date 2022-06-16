// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Ldap.LdapQuery
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Telerik.Sitefinity.Data.Linq.Ldap
{
  /// <summary>LINQ query executed against an LDAP server</summary>
  [DebuggerDisplay("[LdapQuery] {Filter, nq}")]
  public class LdapQuery
  {
    private bool hasCount;
    private StringBuilder filter = new StringBuilder();
    private Collection<SortOptions> orderColumns = new Collection<SortOptions>();
    private Collection<string> selectColumns = new Collection<string>();
    private List<LambdaExpression> expressionsWhere;
    private List<LambdaExpression> expressionsSelect;
    private ElementOperator elementOperator;
    private ConstructorInfo projectedTypeConstructor;

    /// <summary>Create Empty Linq Query</summary>
    public LdapQuery()
    {
    }

    /// <summary>Create Linq Query with specific where clause</summary>
    /// <param name="filterClause"> where clause</param>
    public LdapQuery(string filterClause) => this.filter = new StringBuilder(filterClause);

    /// <summary>LDAP Filter query</summary>
    public StringBuilder Filter
    {
      get => this.filter;
      set => this.filter = value;
    }

    /// <summary>Number of records to skip</summary>
    public int Skip { get; set; }

    /// <summary>Number of records to take</summary>
    public int Take { get; set; }

    /// <summary>Order Columns</summary>
    public Collection<SortOptions> OrderColumns => this.orderColumns;

    /// <summary>Select columns</summary>
    public Collection<string> SelectColumns => this.selectColumns;

    /// <summary>Indicate if the query is count</summary>
    public bool HasCount
    {
      get => this.hasCount;
      set => this.hasCount = value;
    }

    /// <summary>
    /// Used in queryies for userlinks containing filter with UserID
    /// </summary>
    public Guid UserLinkUserId { get; set; }

    /// <summary>Indicates if there are expressions for second pass</summary>
    public bool ExecuteOnSecondPass { get; set; }

    /// <summary>Where Expressions for applying aflter loading data</summary>
    public IList<LambdaExpression> SecondPassExpressionsWhere
    {
      get
      {
        if (this.expressionsWhere == null)
          this.expressionsWhere = new List<LambdaExpression>();
        return (IList<LambdaExpression>) this.expressionsWhere;
      }
    }

    /// <summary>Select Expressions for applying aflter loading data</summary>
    public IList<LambdaExpression> SecondPassExpressionSelect
    {
      get
      {
        if (this.expressionsSelect == null)
          this.expressionsSelect = new List<LambdaExpression>();
        return (IList<LambdaExpression>) this.expressionsSelect;
      }
    }

    /// <summary>What operator is applied on loaded elements</summary>
    public ElementOperator ElementOperator
    {
      get => this.elementOperator;
      set => this.elementOperator = value;
    }

    /// <summary>Constructor of projected types</summary>
    public ConstructorInfo ProjectedTypeConstructor
    {
      get => this.projectedTypeConstructor;
      set => this.projectedTypeConstructor = value;
    }
  }
}
