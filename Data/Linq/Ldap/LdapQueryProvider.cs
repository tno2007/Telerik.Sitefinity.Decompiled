// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Ldap.LdapQueryProvider`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.DirectoryServices.Protocols;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Security.Ldap.Helpers;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.Linq.Ldap
{
  /// <summary>Main Class for executing expression against LDAP</summary>
  /// <typeparam name="TBaseItem"></typeparam>
  /// <typeparam name="TDataItem"></typeparam>
  public class LdapQueryProvider<TBaseItem, TDataItem> : ISitefinityQueryProvider, IQueryProvider
  {
    private DataProviderBase dataProvider;
    private LdapBuilder ldapBuilder;

    /// <summary>Create a new instance</summary>
    /// <param name="dataProvider">The instance of data provider that has instantiated this query provider.</param>
    public LdapQueryProvider(DataProviderBase dataProvider) => this.dataProvider = dataProvider;

    protected virtual LdapBuilder LdapBuilder
    {
      get
      {
        if (this.ldapBuilder == null)
          this.ldapBuilder = LdapBuilder.GetBuilder();
        return this.ldapBuilder;
      }
    }

    public Expression Expression => (Expression) null;

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression) => (IQueryable<TElement>) new LinqQuery<TElement, TDataItem>(this.dataProvider, expression);

    public IQueryable CreateQuery(Expression expression)
    {
      Type c = typeof (TDataItem);
      Type elementType = TypeSystem.GetElementType(expression.Type);
      Type type = !elementType.IsAssignableFrom(c) ? elementType : c;
      try
      {
        return (IQueryable) Activator.CreateInstance(typeof (LinqQuery<>).MakeGenericType(type), (object) this.dataProvider, (object) expression);
      }
      catch (TargetInvocationException ex)
      {
        throw ex.InnerException;
      }
    }

    TResult IQueryProvider.Execute<TResult>(Expression expression) => (TResult) this.Execute<TResult>(expression);

    public object Execute(Expression expression)
    {
      if (typeof (TDataItem) != typeof (TBaseItem))
        return this.Execute<TBaseItem>(expression);
      IEnumerable<MethodInfo> source = ((IEnumerable<MethodInfo>) this.GetType().GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (method => method.Name == nameof (Execute) && method.IsGenericMethod));
      MethodInfo method1 = ((MethodCallExpression) expression).Method;
      Type type1;
      if (method1.IsGenericMethod)
      {
        Type type2 = ((IEnumerable<Type>) ((MethodCallExpression) expression).Method.ReturnType.GetGenericArguments()).FirstOrDefault<Type>();
        if ((object) type2 == null)
          type2 = method1.ReturnType;
        type1 = type2;
      }
      else
        type1 = method1.ReturnType;
      return source.FirstOrDefault<MethodInfo>().MakeGenericMethod(type1).Invoke((object) this, (object[]) new Expression[1]
      {
        expression
      });
    }

    /// <summary>Execute Expression against LDAP</summary>
    /// <typeparam name="TResult">expected result type</typeparam>
    /// <param name="expression">LINQ expression</param>
    /// <returns></returns>
    public object Execute<TResult>(Expression expression)
    {
      LdapQuery ldapQuery = this.GetLdapQuery(expression);
      if (ldapQuery.HasCount)
        return (object) this.GetCount(ldapQuery);
      IEnumerable source = this.LoadData(ldapQuery);
      if (ldapQuery.ElementOperator == ElementOperator.Any)
        return (object) source.GetEnumerator().MoveNext();
      switch (ldapQuery.ElementOperator)
      {
        case ElementOperator.First:
          TResult result1 = source.Cast<TResult>().First<TResult>();
          if ((object) result1 is IDataItem)
            ((object) result1 as IDataItem).Provider = (object) this.dataProvider;
          return (object) result1;
        case ElementOperator.FirstOrDefault:
          TResult result2 = source.Cast<TResult>().FirstOrDefault<TResult>();
          if ((object) result2 != null && (object) result2 is IDataItem)
            ((object) result2 as IDataItem).Provider = (object) this.dataProvider;
          return (object) result2;
        case ElementOperator.Single:
          TResult result3 = source.Cast<TResult>().Single<TResult>();
          if ((object) result3 is IDataItem)
            ((object) result3 as IDataItem).Provider = (object) this.dataProvider;
          return (object) result3;
        case ElementOperator.SingleOrDefault:
          TResult result4 = source.Cast<TResult>().SingleOrDefault<TResult>();
          if ((object) result4 != null && (object) result4 is IDataItem)
            ((object) result4 as IDataItem).Provider = (object) this.dataProvider;
          return (object) result4;
        default:
          return typeof (TResult) is IDataItem ? (object) new ProviderEnumerable<TDataItem>((object) this.dataProvider, source.Cast<TDataItem>()) : (object) source;
      }
    }

    /// <summary>Load data From LDAP</summary>
    /// <param name="query"></param>
    /// <returns></returns>
    private IEnumerable LoadData(LdapQuery query)
    {
      IEnumerable<TDataItem> source = (IEnumerable<TDataItem>) null;
      if (typeof (TDataItem) == typeof (User))
        source = (IEnumerable<TDataItem>) this.LoadUsers(query).Cast<TDataItem>().ToList<TDataItem>();
      if (typeof (TDataItem) == typeof (Role))
        source = (IEnumerable<TDataItem>) this.LoadRoles(query).Cast<TDataItem>().ToList<TDataItem>();
      if (typeof (TDataItem) == typeof (UserLink))
        source = (IEnumerable<TDataItem>) this.LoadUserLinks(query).Cast<TDataItem>().ToList<TDataItem>();
      if (query.ExecuteOnSecondPass)
      {
        foreach (LambdaExpression lambdaExpression in (IEnumerable<LambdaExpression>) query.SecondPassExpressionsWhere)
        {
          Delegate predicate = lambdaExpression.Compile();
          source = source.Where<TDataItem>((Func<TDataItem, bool>) predicate);
        }
      }
      IEnumerable enumerable = (IEnumerable) source;
      if (query.SecondPassExpressionSelect.Count > 0)
      {
        MethodInfo methodInfo = ((IEnumerable<MethodInfo>) this.GetType().GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (mi => mi.Name == "ExecuteSelectStatement")).First<MethodInfo>();
        foreach (LambdaExpression lambdaExpression in (IEnumerable<LambdaExpression>) query.SecondPassExpressionSelect)
        {
          Delegate @delegate = lambdaExpression.Compile();
          Type type1 = ((IEnumerable<Type>) lambdaExpression.Type.GetGenericArguments()).First<Type>();
          Type type2 = ((IEnumerable<Type>) lambdaExpression.Type.GetGenericArguments()).Last<Type>();
          enumerable = (IEnumerable) methodInfo.MakeGenericMethod(type1, type2).Invoke((object) this, new object[2]
          {
            (object) enumerable,
            (object) @delegate
          });
        }
      }
      return enumerable;
    }

    /// <summary>
    /// Used for executing select statement agaist collections
    /// </summary>
    /// <typeparam name="Data">collection items type</typeparam>
    /// <typeparam name="ReturnType">result type</typeparam>
    /// <param name="collection">collection of items </param>
    /// <param name="selectStatement">select statement</param>
    /// <returns></returns>
    public IEnumerable ExecuteSelectStatement<Data, ReturnType>(
      IEnumerable collection,
      Delegate selectStatement)
    {
      return (IEnumerable) new ProviderEnumerable<ReturnType>((object) this.dataProvider, collection.Cast<Data>().Select<Data, ReturnType>((Func<Data, ReturnType>) selectStatement));
    }

    /// <summary>Load usesrs from ldap</summary>
    /// <param name="query">LDAP query </param>
    /// <returns>collection of users</returns>
    private IEnumerable<User> LoadUsers(LdapQuery query) => ((ILdapProviderMarker) this.dataProvider).LdapFacade.UserSearch(query).Select<SearchResultEntry, User>((Func<SearchResultEntry, User>) (user => this.LdapBuilder.Build<User>(user, ((LdapMembershipProvider) this.dataProvider).ManagerInfo, this.dataProvider.ApplicationName)));

    private int GetCount(LdapQuery query)
    {
      if (this.dataProvider is ILdapProviderMarker dataProvider)
      {
        if (typeof (TDataItem) == typeof (User))
          return dataProvider.LdapFacade.UserSearch(query).Count<SearchResultEntry>();
        if (typeof (TDataItem) == typeof (Role))
          return dataProvider.LdapFacade.RoleSearch(query).Count<SearchResultEntry>();
      }
      return typeof (TDataItem) == typeof (UserLink) ? this.LoadUserLinks(query).Count<UserLink>() : 0;
    }

    /// <summary>Load roles from ldap</summary>
    /// <param name="query">LDAP query </param>
    /// <returns>collections of roles </returns>
    private IEnumerable<Role> LoadRoles(LdapQuery query)
    {
      ILdapFacade ldapFacade = ((ILdapProviderMarker) this.dataProvider).LdapFacade;
      List<Role> roleList = new List<Role>();
      IEnumerable<SearchResultEntry> source = ldapFacade.RoleSearch(query);
      IEnumerable<SearchResultEntry> searchResultEntries = (IEnumerable<SearchResultEntry>) null;
      StringBuilder stringBuilder = new StringBuilder("(|");
      foreach (SearchResultEntry searchResultEntry in source)
      {
        stringBuilder.Append("(memberOf=");
        stringBuilder.Append(searchResultEntry.DistinguishedName);
        stringBuilder.Append(")");
      }
      if (source.Count<SearchResultEntry>() != 0)
      {
        stringBuilder.Append(")");
        searchResultEntries = ldapFacade.UserSearch(new LdapQuery()
        {
          Filter = stringBuilder
        });
      }
      foreach (SearchResultEntry entry1 in source)
      {
        Role role = this.LdapBuilder.Build<Role>(entry1, ((LdapRoleProvider) this.dataProvider).ManagerInfo, this.dataProvider.ApplicationName);
        List<UserLink> userLinkList = new List<UserLink>();
        foreach (SearchResultEntry entry2 in searchResultEntries)
        {
          foreach (string str in entry2.Attributes[LdapAttributeNames.memberOf].GetValues(typeof (string)).Cast<string>())
          {
            if (entry1.DistinguishedName == str)
            {
              UserLink userLink = this.LdapBuilder.Build<UserLink>(entry2, ((LdapRoleProvider) this.dataProvider).UserManagerInfo, this.dataProvider.ApplicationName);
              userLinkList.Add(userLink);
              break;
            }
          }
        }
        userLinkList.ForEach((Action<UserLink>) (ul => ul.Role = role));
        foreach (UserLink userLink in userLinkList)
          role.Users.Add(userLink);
        roleList.Add(role);
      }
      return (IEnumerable<Role>) roleList;
    }

    /// <summary>Load user links from ldap</summary>
    /// <param name="query">LDAP query</param>
    /// <returns>collections of user links</returns>
    private IEnumerable<UserLink> LoadUserLinks(LdapQuery query)
    {
      Guid userLinkUserId = query.UserLinkUserId;
      if (query.UserLinkUserId != Guid.Empty)
        return ((LdapRoleProvider) this.dataProvider).GetUserLinks(query.UserLinkUserId);
      List<UserLink> source1 = new List<UserLink>();
      int skip = query.Skip;
      int take = query.Take;
      query.Skip = 0;
      query.Take = 0;
      foreach (Role loadRole in this.LoadRoles(query))
        source1.AddRange((IEnumerable<UserLink>) loadRole.Users);
      IQueryable<UserLink> source2 = source1.AsQueryable<UserLink>();
      if (skip > 0)
        source2 = source2.Skip<UserLink>(skip);
      if (take > 0)
        source2 = source2.Take<UserLink>(take);
      return (IEnumerable<UserLink>) source2;
    }

    /// <summary>Parse Expression into LDAP query</summary>
    /// <param name="expression">LINQ expression</param>
    /// <returns>ldap query</returns>
    private LdapQuery GetLdapQuery(Expression expression) => new LdapQueryTranslator<TDataItem>().Translate(expression);
  }
}
