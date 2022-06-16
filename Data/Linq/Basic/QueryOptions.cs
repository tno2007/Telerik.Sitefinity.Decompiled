// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Basic.QueryArgs
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Data.Linq.Basic
{
  /// <summary>
  /// Holds the arguments that describe the query to be executed.
  /// </summary>
  public class QueryArgs
  {
    private IList<QueryArgs.Filter> filters;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.Basic.QueryArgs" /> class.
    /// </summary>
    public QueryArgs()
    {
      this.filters = (IList<QueryArgs.Filter>) new List<QueryArgs.Filter>(0);
      this.PagingArgs = new QueryArgs.Paging();
      this.OrderArgs = new QueryArgs.Order();
      this.FakePagingArgs = new QueryArgs.Paging();
    }

    /// <summary>Gets the arguments for pagination.</summary>
    public QueryArgs.Paging PagingArgs { get; private set; }

    /// <summary>Gets the arguments for the ordering of the query.</summary>
    public QueryArgs.Order OrderArgs { get; private set; }

    /// <summary>Gets the last action that the query contains.</summary>
    public string LastAction { get; internal set; }

    /// <summary>Gets the filters for the query.</summary>
    public IList<QueryArgs.Filter> Filters
    {
      get
      {
        if (this.filters == null)
          this.filters = (IList<QueryArgs.Filter>) new List<QueryArgs.Filter>();
        return this.filters;
      }
    }

    /// <summary>Gets the return type of the query.</summary>
    public Type QueryType { get; internal set; }

    internal QueryArgs.Paging FakePagingArgs { get; private set; }

    internal void AddFilter(QueryArgs.Member member, string expType, object value) => this.Filters.Add(new QueryArgs.Filter(member, expType, value));

    /// <summary>
    /// A class, used for representing the query filter expressions.
    /// </summary>
    public class Filter
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.Basic.QueryArgs.Filter" /> class.
      /// </summary>
      /// <param name="member">The member on which to filter.</param>
      /// <param name="expType">The type of the expression.</param>
      /// <param name="value">The value to filter on.</param>
      public Filter(QueryArgs.Member member, string expType, object value)
      {
        this.Member = member;
        this.Action = expType;
        this.Value = value;
      }

      /// <summary>Gets the name of the member on which to filter.</summary>
      public QueryArgs.Member Member { get; internal set; }

      /// <summary>Gets the type of the action.</summary>
      public string Action { get; internal set; }

      /// <summary>Gets the value to filter on.</summary>
      public object Value { get; internal set; }
    }

    /// <summary>
    /// A class, used for representing the query order expression.
    /// </summary>
    public class Order
    {
      /// <summary>
      /// Gets the name of the member on which to execute the order.
      /// </summary>
      public string MemberName { get; internal set; }

      /// <summary>Gets the direction on which to order.</summary>
      public QueryArgs.Order.Directions? Direction { get; internal set; }

      /// <summary>Used to specify the direction of an OrderBy clause.</summary>
      public enum Directions
      {
        /// <summary>Ascending direction.</summary>
        Ascending,
        /// <summary>Descending direction.</summary>
        Descending,
      }
    }

    /// <summary>A class, used for representing member expressions.</summary>
    public class Member
    {
      /// <summary>
      /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Data.Linq.Basic.QueryArgs.Member" /> class.
      /// </summary>
      /// <param name="name">The name of the member.</param>
      /// <param name="action">The action that was executed on the member.</param>
      public Member(string name, string action = null)
      {
        this.Name = name;
        this.Action = action;
      }

      /// <summary>Gets the member name.</summary>
      public string Name { get; internal set; }

      /// <summary>Gets the name of the action.</summary>
      public string Action { get; internal set; }

      /// <summary>Implicitly returns the name of the member.</summary>
      /// <param name="member">The member to convert.</param>
      /// <returns>The member name</returns>
      public static implicit operator string(QueryArgs.Member member) => member.Name;
    }

    /// <summary>
    /// A class, used for representing the query paging(skip/take) expression.
    /// </summary>
    public class Paging
    {
      /// <summary>Gets the amount of items to skip.</summary>
      public int? Skip { get; internal set; }

      /// <summary>Gets the amount of items to take.</summary>
      public int? Take { get; internal set; }
    }

    /// <summary>Holds the constants.</summary>
    public class Constants
    {
      /// <summary>Holds the constants for IQueryable methods.</summary>
      public class Queryable
      {
        /// <summary>Represents the Count method.</summary>
        public const string Count = "Count";
        /// <summary>Represents the ToList() method.</summary>
        public const string List = "List";
        /// <summary>Represents the Any() method.</summary>
        public const string Any = "Any";
        internal const string Skip = "Skip";
        internal const string Take = "Take";
        internal const string Where = "Where";
        internal const string OrderBy = "OrderBy";
        internal const string OrderByDescending = "OrderByDescending";
        internal const string First = "First";
        internal const string FirstOrDefault = "FirstOrDefault";
        internal const string Single = "Single";
        internal const string SingleOrDefault = "SingleOrDefault";
        internal const string LongCount = "LongCount";
      }

      /// <summary>Holds the constants for String supported filters.</summary>
      public class String
      {
        /// <summary>Represents the Contains("sample") method.</summary>
        public const string Contains = "Contains";
        /// <summary>Represents the StartsWith("sample") method.</summary>
        public const string StartsWith = "StartsWith";
        /// <summary>Represents the EndsWith("sample") method.</summary>
        public const string EndsWith = "EndsWith";
        /// <summary>Represents the Equals("sample") method.</summary>
        public const string Equals = "Equals";
      }

      /// <summary>
      /// Holds the constants for IEnumerable supported filters.
      /// </summary>
      public class Enumerable
      {
        /// <summary>Represents the Contains() method.</summary>
        public const string Contains = "Contains";
      }
    }
  }
}
