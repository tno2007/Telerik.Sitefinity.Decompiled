// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.Ldap.LdapQueryTranslator`1
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Ldap;
using Telerik.Sitefinity.Security.Ldap.Helpers;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.Linq.Ldap
{
  /// <summary>Translate LINQ query into LDAP Filter</summary>
  /// <typeparam name="TItem">Linq Query Type</typeparam>
  public class LdapQueryTranslator<TItem> : Telerik.Sitefinity.Data.Linq.ExpressionVisitor
  {
    private LdapQuery ldapQuery;
    private bool isParsingQueryForUserLinks;
    private CurrentOperand operand = CurrentOperand.None;
    private bool reverseOrder;
    private bool skipCurrentSubtree;

    /// <summary>Constructor</summary>
    public LdapQueryTranslator() => this.isParsingQueryForUserLinks = typeof (TItem) == typeof (UserLink);

    /// <summary>Builded Ldap Query</summary>
    private LdapQuery Query
    {
      get
      {
        if (this.ldapQuery == null)
          this.ldapQuery = new LdapQuery();
        return this.ldapQuery;
      }
    }

    /// <summary>Translate Expression into LDAP Query</summary>
    /// <param name="expression"></param>
    /// <returns></returns>
    public LdapQuery Translate(Expression expression)
    {
      this.Visit(expression);
      return this.Query;
    }

    private bool VisitNotSupportedMethods(MethodCallExpression m)
    {
      if (m.Method.DeclaringType == typeof (Queryable) && (m.Method.Name == "Average" || m.Method.Name == "Max" || m.Method.Name == "Min" || m.Method.Name == "Sum"))
        throw new NotSupportedException("Ldap does not support " + m.Method.Name);
      return false;
    }

    private bool VisitWhere(MethodCallExpression m)
    {
      if (!(m.Method.DeclaringType == typeof (Queryable)) || !(m.Method.Name == "Where"))
        return false;
      if (this.operand != CurrentOperand.Where)
      {
        this.operand = CurrentOperand.Where;
        LambdaExpression lambdaExpression = (LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1]);
        this.Query.SecondPassExpressionsWhere.Add(lambdaExpression);
        if (this.ProcessNotSupportedObjectMembers(lambdaExpression.Body))
          return true;
        this.FixVisitBoolean(lambdaExpression.Body);
      }
      else
      {
        LambdaExpression lambdaExpression = (LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1]);
        this.Query.SecondPassExpressionsWhere.Add(lambdaExpression);
        if (this.ProcessNotSupportedObjectMembers(lambdaExpression.Body))
          return true;
        int length = this.Query.Filter.Length;
        this.FixVisitBoolean(lambdaExpression.Body);
        if (length != this.Query.Filter.Length && length != 0)
        {
          this.Query.Filter.Insert(0, "(&");
          this.Query.Filter.Append(")");
        }
      }
      return true;
    }

    private bool VisitOrder(MethodCallExpression m)
    {
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "OrderBy")
      {
        this.operand = CurrentOperand.OrderBy;
        this.reverseOrder = false;
        LambdaExpression lambdaExpression = (LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1]);
        if (this.ProcessNotSupportedObjectMembers(lambdaExpression.Body))
          return true;
        this.Visit(lambdaExpression.Body);
        this.operand = CurrentOperand.None;
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "OrderByDescending")
      {
        this.operand = CurrentOperand.OrderBy;
        this.reverseOrder = true;
        LambdaExpression lambdaExpression = (LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1]);
        if (this.ProcessNotSupportedObjectMembers(lambdaExpression.Body))
          return true;
        this.Visit(lambdaExpression.Body);
        this.operand = CurrentOperand.None;
        return true;
      }
      if (!(m.Method.Name == "Reverse"))
        return false;
      foreach (SortOptions orderColumn in this.Query.OrderColumns)
        orderColumn.Ascending = !orderColumn.Ascending;
      return true;
    }

    private bool VisitSelectorMethod(MethodCallExpression m)
    {
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "Select")
      {
        this.operand = CurrentOperand.Select;
        LambdaExpression lambdaExpression = (LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1]);
        this.Query.SecondPassExpressionSelect.Add(lambdaExpression);
        if (this.ProcessNotSupportedObjectMembers(lambdaExpression.Body))
          return true;
        this.Visit(lambdaExpression.Body);
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "FirstOrDefault")
      {
        if (m.Arguments.Count > 1)
          this.Visit(((LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1])).Body);
        this.Query.Skip = 0;
        this.Query.Take = 1;
        this.Query.ElementOperator = ElementOperator.FirstOrDefault;
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "First")
      {
        if (m.Arguments.Count > 1)
          this.Visit(((LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1])).Body);
        this.Query.Skip = 0;
        this.Query.Take = 1;
        this.Query.ElementOperator = ElementOperator.First;
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "SingleOrDefault")
      {
        if (m.Arguments.Count > 1)
          this.Visit(((LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1])).Body);
        this.Query.Skip = 0;
        this.Query.Take = 1;
        this.Query.ElementOperator = ElementOperator.SingleOrDefault;
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "Single")
      {
        if (m.Arguments.Count > 1)
          this.Visit(((LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1])).Body);
        this.Query.Skip = 0;
        this.Query.Take = 1;
        this.Query.ElementOperator = ElementOperator.Single;
        return true;
      }
      if (!(m.Method.DeclaringType == typeof (Queryable)) || !(m.Method.Name == "Any"))
        return false;
      if (m.Arguments.Count > 1)
        this.Visit(((LambdaExpression) Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[1])).Body);
      this.Query.Skip = 0;
      this.Query.Take = 1;
      this.Query.ElementOperator = ElementOperator.Any;
      return true;
    }

    private bool VisitFilterStringMethod(MethodCallExpression m)
    {
      if (m.Method.Name == "Contains")
      {
        if (m.Arguments[0].NodeType == ExpressionType.MemberAccess && ((MemberExpression) m.Arguments[0]).Expression.NodeType == ExpressionType.Constant)
        {
          MemberExpression memberExpression = (MemberExpression) m.Arguments[0];
          ConstantExpression expression = (ConstantExpression) memberExpression.Expression;
          string name = memberExpression.Member.Name;
          IEnumerable enumerable = expression.Value.GetType().GetField(name).GetValue(expression.Value) as IEnumerable;
          int num = 0;
          if (m.Object == null)
          {
            if (this.ProcessNotSupportedObjectMembers(m.Arguments[1]))
              return true;
            foreach (object obj in enumerable)
            {
              this.Query.Filter.Append("(");
              this.Visit(m.Arguments[1]);
              this.Query.Filter.Append("=");
              this.Visit((Expression) Expression.Constant(obj));
              this.Query.Filter.Append(")");
              ++num;
            }
          }
          else
          {
            if (this.ProcessNotSupportedObjectMembers(m.Object))
              return true;
            foreach (object obj in enumerable)
            {
              this.Query.Filter.Append("(");
              this.Visit(m.Object);
              this.Query.Filter.Append("=");
              this.Visit((Expression) Expression.Constant(obj));
              this.Query.Filter.Append(")");
              ++num;
            }
          }
          if (num > 1)
          {
            this.Query.Filter.Insert(0, "|");
            this.Query.Filter.Insert(0, "(");
            this.Query.Filter.Append(")");
          }
        }
        else
        {
          if (this.ProcessNotSupportedObjectMembers(m.Object))
            return true;
          if (this.operand != CurrentOperand.Where)
            throw new NotSupportedException("No support for method Contains outside of where clause");
          this.Query.Filter.Append("(");
          this.Visit(m.Object);
          this.Query.Filter.Append("=*");
          this.Visit(Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[0]));
          this.Query.Filter.Append("*)");
        }
        return true;
      }
      if (m.Method.Name == "StartsWith")
      {
        if (this.ProcessNotSupportedObjectMembers(m.Object))
          return true;
        if (this.operand != CurrentOperand.Where)
          throw new NotSupportedException("No support for method Contains outside of where clause");
        this.Query.Filter.Append("(");
        this.Visit(m.Object);
        this.Query.Filter.Append("=");
        this.Visit(Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[0]));
        this.Query.Filter.Append("*)");
        return true;
      }
      if (!(m.Method.Name == "EndsWith"))
        return false;
      if (this.ProcessNotSupportedObjectMembers(m.Object))
        return true;
      if (this.operand != CurrentOperand.Where)
        throw new NotSupportedException("No support for method Contains outside of where clause");
      this.Query.Filter.Append("(");
      this.Visit(m.Object);
      this.Query.Filter.Append("=*");
      this.Visit(Telerik.Sitefinity.Data.Linq.ExpressionVisitor.StripQuotes(m.Arguments[0]));
      this.Query.Filter.Append(")");
      return true;
    }

    private bool VisitCountSkipTakeMethod(MethodCallExpression m)
    {
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "Count")
      {
        this.Query.HasCount = true;
        return true;
      }
      if (m.Method.DeclaringType == typeof (Queryable) && m.Method.Name == "Skip")
      {
        this.Query.Skip = (int) ((ConstantExpression) m.Arguments[1]).Value;
        return true;
      }
      if (!(m.Method.DeclaringType == typeof (Queryable)) || !(m.Method.Name == "Take"))
        return false;
      this.Query.Take = (int) ((ConstantExpression) m.Arguments[1]).Value;
      return true;
    }

    protected virtual bool VisitToUpperOrToLower(MethodCallExpression m)
    {
      if (m.Method.Name == "ToLower" || m.Method.Name == "ToUpper")
        this.Visit(m.Object);
      return false;
    }

    /// <summary>Visits the method call expression.</summary>
    /// <param name="m">The instance of the <see cref="T:System.Linq.Expressions.MethodCallExpression" /> class.</param>
    /// <returns>The expression after it has been processed.</returns>
    protected override Expression VisitMethodCall(MethodCallExpression m)
    {
      if (m.Arguments.Count != 0 && m.Arguments[0].NodeType != ExpressionType.Constant && m.Object == null)
        this.Visit(m.Arguments[0]);
      if (!this.VisitWhere(m) && !this.VisitOrder(m) && !this.VisitSelectorMethod(m) && !this.VisitFilterStringMethod(m) && !this.VisitCountSkipTakeMethod(m) && !this.VisitToUpperOrToLower(m))
        this.VisitNotSupportedMethods(m);
      return (Expression) m;
    }

    /// <summary>Visits the unary expression.</summary>
    /// <param name="u">The instance of unary expression.</param>
    /// <returns>The unary expression after it has processed it.</returns>
    protected override Expression VisitUnary(UnaryExpression u)
    {
      switch (u.NodeType)
      {
        case ExpressionType.Convert:
          if (this.operand == CurrentOperand.Where)
          {
            if (this.ProcessNotSupportedObjectMembers(u.Operand))
              return (Expression) u;
            this.FixVisitBoolean(u.Operand);
            break;
          }
          throw new NotSupportedException("The unary operator '{0}' is not supported".Arrange((object) u.NodeType));
        case ExpressionType.Not:
          if (this.operand == CurrentOperand.Where)
          {
            if (this.ProcessNotSupportedObjectMembers(u.Operand))
              return (Expression) u;
            this.Query.Filter.Append("(!");
            this.FixVisitBoolean(u.Operand);
            this.Query.Filter.Append(")");
            break;
          }
          throw new NotSupportedException("The unary operator '{0}' is not supported".Arrange((object) u.NodeType));
        default:
          throw new NotSupportedException("The unary operator '{0}' is not supported".Arrange((object) u.NodeType));
      }
      return (Expression) u;
    }

    private void VisitAndAlso(BinaryExpression b)
    {
      int length1 = this.Query.Filter.Length;
      this.FixVisitBoolean(b.Left);
      string str = this.Query.Filter.ToString();
      int length2 = this.Query.Filter.Length;
      this.FixVisitBoolean(b.Right);
      string rightFilter = this.Query.Filter.ToString().Replace(str, string.Empty);
      if (this.Query.Filter.ToString().Contains(str) || length1 == str.Length && length2 == rightFilter.Length)
        return;
      this.FixFilterAfterVisitingBinaryExpresssion(str, rightFilter);
    }

    private void FixFilterAfterVisitingBinaryExpresssion(string leftFilter, string rightFilter)
    {
      if (this.skipCurrentSubtree)
      {
        this.Query.Filter = new StringBuilder(leftFilter);
      }
      else
      {
        if (string.IsNullOrEmpty(leftFilter))
        {
          this.Query.Filter = new StringBuilder(rightFilter);
          return;
        }
        if (string.IsNullOrEmpty(rightFilter))
        {
          this.Query.Filter = new StringBuilder(leftFilter);
          return;
        }
        if (this.Query.Filter.Length <= 2 || this.Query.Filter[0] != '(' || this.Query.Filter[1] != '&')
        {
          this.Query.Filter.Insert(0, "(&");
          this.Query.Filter.Append(")");
        }
        else
          this.Query.Filter = !(leftFilter != string.Empty) ? new StringBuilder(rightFilter) : new StringBuilder(leftFilter.Insert(leftFilter.Length - 1, rightFilter.Substring(0, rightFilter.Length)));
      }
      this.skipCurrentSubtree = false;
    }

    private void VisitOrOrElse(BinaryExpression b)
    {
      this.FixVisitBoolean(b.Left);
      string str1 = this.Query.Filter.ToString();
      this.FixVisitBoolean(b.Right);
      string str2 = this.Query.Filter.ToString().Substring(str1.Length);
      if (this.skipCurrentSubtree)
        this.Query.Filter = new StringBuilder(str1);
      else if (this.Query.Filter.Length <= 2 || this.Query.Filter[0] != '(' || this.Query.Filter[1] != '|')
      {
        this.Query.Filter.Insert(0, "(|");
        this.Query.Filter.Append(")");
      }
      else
        this.Query.Filter = new StringBuilder(str1.Insert(str1.Length - 1, str2.Substring(0, str2.Length)));
      this.skipCurrentSubtree = false;
    }

    private void VisitEqual(BinaryExpression b)
    {
      this.Query.Filter.Append("(");
      this.Visit(b.Left);
      this.Query.Filter.Append("=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")");
    }

    private void VisitNotEqual(BinaryExpression b)
    {
      this.Query.Filter.Append("(!(");
      this.Visit(b.Left);
      this.Query.Filter.Append("=");
      this.Visit(b.Right);
      this.Query.Filter.Append("))");
    }

    private void VisitLessThan(BinaryExpression b)
    {
      this.Query.Filter.Append("(&(");
      this.Visit(b.Left);
      this.Query.Filter.Append("<=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")(!(");
      this.Visit(b.Left);
      this.Query.Filter.Append("=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")))");
    }

    private void VisitLessThanEqual(BinaryExpression b)
    {
      this.Query.Filter.Append("(");
      this.Visit(b.Left);
      this.Query.Filter.Append("<=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")");
    }

    private void VisitGreaterThan(BinaryExpression b)
    {
      this.Query.Filter.Append("(&(");
      this.Visit(b.Left);
      this.Query.Filter.Append(">=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")(!(");
      this.Visit(b.Left);
      this.Query.Filter.Append("=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")))");
    }

    private void VisitGreaterThanEqual(BinaryExpression b)
    {
      this.Query.Filter.Append("(");
      this.Visit(b.Left);
      this.Query.Filter.Append(">=");
      this.Visit(b.Right);
      this.Query.Filter.Append(")");
    }

    /// <summary>Processes the not supported object members.</summary>
    /// <param name="exp">The exp.</param>
    /// <returns></returns>
    private bool ProcessNotSupportedObjectMembers(Expression expression)
    {
      Expression expression1 = expression;
      if (expression1.NodeType == ExpressionType.Convert && ((UnaryExpression) expression1).Operand.NodeType == ExpressionType.MemberAccess)
        expression1 = ((UnaryExpression) expression1).Operand;
      if (expression1.NodeType != ExpressionType.MemberAccess)
        return false;
      MemberInfo memberName = ((MemberExpression) expression1).Member;
      string result = "";
      if (new List<string>()
      {
        "ApplicationName",
        "ManagerInfo",
        "ProviderName",
        "ProviderUserKey"
      }.Any<string>((Func<string, bool>) (e => e.Equals(memberName.Name))) || Config.Get<SecurityConfig>().LdapConnections.LdapMapping.TryReverseMapping(memberName, out result))
        return false;
      this.skipCurrentSubtree = true;
      return true;
    }

    /// <summary>Visits the binary expression.</summary>
    /// <param name="b">The instance of binary expression.</param>
    /// <returns>The instance of the binary expression.</returns>
    protected override Expression VisitBinary(BinaryExpression b)
    {
      if (this.ProcessNotSupportedObjectMembers(b.Left))
        return (Expression) b;
      if (this.isParsingQueryForUserLinks && b.Left is MemberExpression && ((MemberExpression) b.Left).Member.DeclaringType == typeof (UserLink) && ((MemberExpression) b.Left).Member.Name == "UserId")
      {
        this.skipCurrentSubtree = true;
        this.Query.UserLinkUserId = (Guid) Expression.Lambda(b.Right).Compile().DynamicInvoke();
        return (Expression) b;
      }
      switch (b.NodeType)
      {
        case ExpressionType.And:
        case ExpressionType.AndAlso:
          if (!this.ProcessSpecialClauses(b, this.Query))
          {
            this.VisitAndAlso(b);
            break;
          }
          break;
        case ExpressionType.Equal:
          if (!this.ProcessSpecialClauses(b, this.Query))
          {
            this.VisitEqual(b);
            break;
          }
          break;
        case ExpressionType.GreaterThan:
          this.VisitGreaterThan(b);
          break;
        case ExpressionType.GreaterThanOrEqual:
          this.VisitGreaterThanEqual(b);
          break;
        case ExpressionType.LessThan:
          this.VisitLessThan(b);
          break;
        case ExpressionType.LessThanOrEqual:
          this.VisitLessThanEqual(b);
          break;
        case ExpressionType.NotEqual:
          if (!this.ProcessSpecialClauses(b, this.Query))
          {
            this.VisitNotEqual(b);
            break;
          }
          break;
        case ExpressionType.Or:
        case ExpressionType.OrElse:
          this.VisitOrOrElse(b);
          break;
        default:
          throw new NotSupportedException("The binary operator '{0}' is not supported".Arrange((object) b.NodeType));
      }
      return (Expression) b;
    }

    /// <summary>Visits the constant expression.</summary>
    /// <param name="c">The instance of the constant expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected override Expression VisitConstant(ConstantExpression c)
    {
      if (!(c.Value is IQueryable))
      {
        if (c.Value == null)
          throw new NullReferenceException("Constant value can not be null");
        if (c.Type.IsArray)
          return (Expression) c;
        switch (Type.GetTypeCode(c.Value.GetType()))
        {
          case TypeCode.Boolean:
            this.Query.Filter.Append((bool) c.Value ? "TRUE" : "FALSE");
            break;
          case TypeCode.Char:
          case TypeCode.String:
            this.Query.Filter.Append(c.Value);
            break;
        }
        if (c.Type == typeof (Guid))
          this.Query.Filter.Append(((Guid) c.Value).ToLdapFormat());
      }
      return (Expression) c;
    }

    /// <summary>Visits the member access expression.</summary>
    /// <param name="m">The instance of member access expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected override Expression VisitMemberAccess(MemberExpression m)
    {
      string empty = string.Empty;
      if (this.operand == CurrentOperand.OrderBy && m.Expression != null && m.Expression.NodeType == ExpressionType.Parameter)
      {
        string str = Config.Get<SecurityConfig>().LdapConnections.LdapMapping.ReverseMapping(m.Member);
        this.Query.OrderColumns.Add(new SortOptions()
        {
          Column = str,
          Ascending = !this.reverseOrder
        });
        return (Expression) m;
      }
      if (m.Expression == null || m.Expression.NodeType == ExpressionType.Constant)
      {
        if (m.Type.IsArray)
          return (Expression) m;
        object date = Expression.Lambda((Expression) m).Compile().DynamicInvoke();
        if (m.Type == typeof (DateTime))
        {
          this.Query.Filter.Append(LdapUtilities.DateToGeneralizedTime((DateTime) date));
          return (Expression) m;
        }
        if (m.Type == typeof (Guid))
        {
          this.Query.Filter.Append(((Guid) date).ToLdapFormat());
          return (Expression) m;
        }
        if (TypeDescriptor.GetConverter(m.Type).CanConvertTo(typeof (string)))
        {
          this.Query.Filter.Append(date.ToString());
          return (Expression) m;
        }
      }
      this.Query.Filter.Append(Config.Get<SecurityConfig>().LdapConnections.LdapMapping.ReverseMapping(m.Member));
      return (Expression) m;
    }

    /// <summary>Visits the new expression.</summary>
    /// <param name="nex">The instance of new expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected override NewExpression VisitNew(NewExpression nex)
    {
      this.Query.ProjectedTypeConstructor = nex.Constructor;
      string result = string.Empty;
      foreach (Expression expression in nex.Arguments)
      {
        Collection<string> selectColumns = this.Query.SelectColumns;
        if (expression is MemberExpression memberExpression && Config.Get<SecurityConfig>().LdapConnections.LdapMapping.TryReverseMapping(memberExpression.Member, out result) && !selectColumns.Contains(result))
          selectColumns.Add(result);
      }
      return nex;
    }

    /// <summary>
    /// Used for fixing expressions like  where(Boolean_Property)  this is converted into where(Boolean_Property=true)
    /// </summary>
    /// <param name="ex"></param>
    private void FixVisitBoolean(Expression ex)
    {
      if (ex.NodeType == ExpressionType.MemberAccess && ex.Type == typeof (bool))
        this.Visit((Expression) Expression.Equal(ex, (Expression) Expression.Constant((object) true)));
      else
        this.Visit(ex);
    }

    /// <summary>
    /// Gets the LDAP string experssion, base on the LINQ expretssion Type
    /// </summary>
    /// <param name="expType">Type of the exp.</param>
    /// <param name="left">The left expression</param>
    /// <param name="right">The right right</param>
    /// <returns></returns>
    private string GetLdapExpression(ExpressionType expType, string left, string right)
    {
      if (expType != ExpressionType.Equal)
      {
        if (expType != ExpressionType.NotEqual)
          return "";
        return "(!(" + left + "=" + right + "))";
      }
      return "(" + left + "=" + right + ")";
    }

    /// <summary>
    /// Get guid from expression - only for providerUserKey scenarios
    /// </summary>
    /// <param name="exp"></param>
    /// <returns></returns>
    private Guid GetProvideruserKeyValue(Expression exp)
    {
      object g = Expression.Lambda(exp).Compile().DynamicInvoke();
      switch (g)
      {
        case string _:
          return new Guid((string) g);
        case Guid provideruserKeyValue:
          return provideruserKeyValue;
        default:
          throw new NotSupportedException("ProviderUserKey can not be compare with other type except string");
      }
    }

    private bool ProcessSpecialClauses(BinaryExpression exp, LdapQuery query)
    {
      Expression leftExp = exp.Left;
      Expression right = exp.Right;
      if (exp.Left.NodeType == ExpressionType.Convert && ((UnaryExpression) exp.Left).Operand.NodeType == ExpressionType.MemberAccess)
        leftExp = ((UnaryExpression) exp.Left).Operand;
      if (leftExp.NodeType == ExpressionType.MemberAccess)
      {
        if (leftExp == null)
          return false;
        if (((IEnumerable<string>) new string[3]
        {
          "ApplicationName",
          "ManagerInfo",
          "ProviderName"
        }).Any<string>((Func<string, bool>) (e => e.Equals(((MemberExpression) leftExp).Member.Name))))
        {
          query.ExecuteOnSecondPass = true;
          this.operand = CurrentOperand.None;
          return true;
        }
        if (((MemberExpression) leftExp).Member.Name == "ProviderUserKey" && (exp.Right is UnaryExpression || exp.Right is ConstantExpression))
        {
          Guid provideruserKeyValue = this.GetProvideruserKeyValue(exp.Right);
          query.Filter.Append(this.GetLdapExpression(exp.NodeType, LdapAttributeNames.ObjectGuidAttribute, provideruserKeyValue.ToLdapFormat()));
          this.operand = CurrentOperand.None;
          return true;
        }
        if (((MemberExpression) leftExp).Member.Name == "IsApproved")
        {
          int num = (bool) Expression.Lambda(exp.Right).Compile().DynamicInvoke() ? 1 : 0;
          string str = Config.Get<SecurityConfig>().LdapConnections.LdapMapping.ReverseMapping(((MemberExpression) leftExp).Member);
          if (num != 0)
            this.Query.Filter.Append(string.Format("({0}:1.2.840.113556.1.4.803:=2)", (object) str));
          else
            this.Query.Filter.Append(string.Format("(!({0}:1.2.840.113556.1.4.803:=2))", (object) str, (object) 2));
          return true;
        }
      }
      if (exp.Left.NodeType == ExpressionType.Call)
      {
        MemberExpression memberAccess = this.GetMemberAccess((Expression) exp);
        if (memberAccess != null && memberAccess.Member.Name == "ProviderUserKey")
        {
          Guid guid = Guid.NewGuid();
          if (exp.Right.NodeType == ExpressionType.Constant)
          {
            guid = new Guid(((ConstantExpression) exp.Right).Value.ToString());
          }
          else
          {
            if (exp.Right.NodeType != ExpressionType.Call)
              throw new NotSupportedException("Linq query not supported");
            guid = this.GetProvideruserKeyValue(exp.Right);
          }
          query.Filter.Append(this.GetLdapExpression(exp.NodeType, LdapAttributeNames.ObjectGuidAttribute, guid.ToLdapFormat()));
          this.operand = CurrentOperand.None;
          return true;
        }
      }
      return false;
    }

    private MemberExpression GetMemberAccess(Expression exp)
    {
      switch (exp)
      {
        case null:
          return (MemberExpression) null;
        case MethodCallExpression _:
          return this.GetMemberAccess(((MethodCallExpression) exp).Object);
        case MemberExpression _:
          return (MemberExpression) exp;
        default:
          return (MemberExpression) null;
      }
    }
  }
}
