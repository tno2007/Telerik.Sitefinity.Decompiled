// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.ExpressionVisitor
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Base expression visitor class which is to be derived by all LINQ translator classes.
  /// </summary>
  public abstract class ExpressionVisitor
  {
    private StringBuilder sb;

    protected virtual StringBuilder ExpressionBuilder
    {
      get
      {
        if (this.sb == null)
          this.sb = new StringBuilder();
        return this.sb;
      }
    }

    /// <summary>Visits the specified expression.</summary>
    /// <param name="exp">The expression to be visited.</param>
    /// <returns>The expression that was returned by the specific visitor.</returns>
    protected virtual Expression Visit(Expression exp)
    {
      if (exp == null)
        return exp;
      switch (exp.NodeType)
      {
        case ExpressionType.Add:
        case ExpressionType.AddChecked:
        case ExpressionType.And:
        case ExpressionType.AndAlso:
        case ExpressionType.ArrayIndex:
        case ExpressionType.Coalesce:
        case ExpressionType.Divide:
        case ExpressionType.Equal:
        case ExpressionType.ExclusiveOr:
        case ExpressionType.GreaterThan:
        case ExpressionType.GreaterThanOrEqual:
        case ExpressionType.LeftShift:
        case ExpressionType.LessThan:
        case ExpressionType.LessThanOrEqual:
        case ExpressionType.Modulo:
        case ExpressionType.Multiply:
        case ExpressionType.MultiplyChecked:
        case ExpressionType.NotEqual:
        case ExpressionType.Or:
        case ExpressionType.OrElse:
        case ExpressionType.RightShift:
        case ExpressionType.Subtract:
        case ExpressionType.SubtractChecked:
          return this.VisitBinary((BinaryExpression) exp);
        case ExpressionType.ArrayLength:
        case ExpressionType.Convert:
        case ExpressionType.ConvertChecked:
        case ExpressionType.Negate:
        case ExpressionType.NegateChecked:
        case ExpressionType.Not:
        case ExpressionType.Quote:
        case ExpressionType.TypeAs:
          return this.VisitUnary((UnaryExpression) exp);
        case ExpressionType.Call:
          return this.VisitMethodCall((MethodCallExpression) exp);
        case ExpressionType.Conditional:
          return this.VisitConditional((ConditionalExpression) exp);
        case ExpressionType.Constant:
          return this.VisitConstant((ConstantExpression) exp);
        case ExpressionType.Invoke:
          return this.VisitInvocation((InvocationExpression) exp);
        case ExpressionType.Lambda:
          return this.VisitLambda((LambdaExpression) exp);
        case ExpressionType.ListInit:
          return this.VisitListInit((ListInitExpression) exp);
        case ExpressionType.MemberAccess:
          return this.VisitMemberAccess((MemberExpression) exp);
        case ExpressionType.MemberInit:
          return this.VisitMemberInit((MemberInitExpression) exp);
        case ExpressionType.New:
          return (Expression) this.VisitNew((NewExpression) exp);
        case ExpressionType.NewArrayInit:
        case ExpressionType.NewArrayBounds:
          return this.VisitNewArray((NewArrayExpression) exp);
        case ExpressionType.Parameter:
          return this.VisitParameter((ParameterExpression) exp);
        case ExpressionType.TypeIs:
          return this.VisitTypeIs((TypeBinaryExpression) exp);
        default:
          throw new Exception(string.Format("Unhandled expression type: '{0}'", (object) exp.NodeType));
      }
    }

    /// <summary>Visits the binding.</summary>
    /// <param name="binding">The binding.</param>
    /// <returns></returns>
    protected virtual MemberBinding VisitBinding(MemberBinding binding)
    {
      switch (binding.BindingType)
      {
        case MemberBindingType.Assignment:
          return (MemberBinding) this.VisitMemberAssignment((MemberAssignment) binding);
        case MemberBindingType.MemberBinding:
          return (MemberBinding) this.VisitMemberMemberBinding((MemberMemberBinding) binding);
        case MemberBindingType.ListBinding:
          return (MemberBinding) this.VisitMemberListBinding((MemberListBinding) binding);
        default:
          throw new Exception(string.Format("Unhandled binding type '{0}'", (object) binding.BindingType));
      }
    }

    /// <summary>Visits the element initializer.</summary>
    /// <param name="initializer">The initializer.</param>
    /// <returns></returns>
    protected virtual ElementInit VisitElementInitializer(ElementInit initializer)
    {
      ReadOnlyCollection<Expression> arguments = this.VisitExpressionList(initializer.Arguments);
      return arguments != initializer.Arguments ? Expression.ElementInit(initializer.AddMethod, (IEnumerable<Expression>) arguments) : initializer;
    }

    /// <summary>Visits the unary expression.</summary>
    /// <param name="u">The instance of unary expression.</param>
    /// <returns>The expression after it has been processed.</returns>
    protected virtual Expression VisitUnary(UnaryExpression u)
    {
      Expression operand = this.Visit(u.Operand);
      return operand != u.Operand ? (Expression) Expression.MakeUnary(u.NodeType, operand, u.Type, u.Method) : (Expression) u;
    }

    /// <summary>Visits the binary expression.</summary>
    /// <param name="b">The instance of binary expression.</param>
    /// <returns>Binary expression after it has been processed.</returns>
    protected virtual Expression VisitBinary(BinaryExpression b)
    {
      Expression left = this.Visit(b.Left);
      Expression right = this.Visit(b.Right);
      Expression conversion = this.Visit((Expression) b.Conversion);
      if (left == b.Left && right == b.Right && conversion == b.Conversion)
        return (Expression) b;
      return b.NodeType == ExpressionType.Coalesce && b.Conversion != null ? (Expression) Expression.Coalesce(left, right, conversion as LambdaExpression) : (Expression) Expression.MakeBinary(b.NodeType, left, right, b.IsLiftedToNull, b.Method);
    }

    /// <summary>Visits the TypeBinaryExpression.</summary>
    /// <param name="b">The instance of TypeBinaryExpression.</param>
    /// <returns>The TypeBinaryExpression after it has been processed.</returns>
    protected virtual Expression VisitTypeIs(TypeBinaryExpression b)
    {
      Expression expression = this.Visit(b.Expression);
      return expression != b.Expression ? (Expression) Expression.TypeIs(expression, b.TypeOperand) : (Expression) b;
    }

    /// <summary>Visits the constant expression.</summary>
    /// <param name="c">The instance of the constant expression.</param>
    /// <returns>The expression itself.</returns>
    protected virtual Expression VisitConstant(ConstantExpression c) => (Expression) c;

    /// <summary>Visits the conditional expression.</summary>
    /// <param name="c">The instance of conditional expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitConditional(ConditionalExpression c)
    {
      Expression test = this.Visit(c.Test);
      Expression ifTrue = this.Visit(c.IfTrue);
      Expression ifFalse = this.Visit(c.IfFalse);
      return test != c.Test || ifTrue != c.IfTrue || ifFalse != c.IfFalse ? (Expression) Expression.Condition(test, ifTrue, ifFalse) : (Expression) c;
    }

    /// <summary>Visits the parameter expression.</summary>
    /// <param name="p">The instance of parameter expression.</param>
    /// <returns>The expression itself</returns>
    protected virtual Expression VisitParameter(ParameterExpression p) => (Expression) p;

    /// <summary>Visits the member access expression.</summary>
    /// <param name="m">The instance of member expression.</param>
    /// <returns>Expression itself after it has been processed.</returns>
    protected virtual Expression VisitMemberAccess(MemberExpression m)
    {
      Expression expression = this.Visit(m.Expression);
      return expression != m.Expression ? (Expression) Expression.MakeMemberAccess(expression, m.Member) : (Expression) m;
    }

    /// <summary>Visits the method call expression.</summary>
    /// <param name="m">The instance of method call expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitMethodCall(MethodCallExpression m)
    {
      Expression instance = this.Visit(m.Object);
      IEnumerable<Expression> arguments = (IEnumerable<Expression>) this.VisitExpressionList(m.Arguments);
      return instance != m.Object || arguments != m.Arguments ? (Expression) Expression.Call(instance, m.Method, arguments) : (Expression) m;
    }

    /// <summary>Visits the expression list.</summary>
    /// <param name="original">The original expressions list.</param>
    /// <returns>The processed expression list.</returns>
    protected virtual ReadOnlyCollection<Expression> VisitExpressionList(
      ReadOnlyCollection<Expression> original)
    {
      List<Expression> expressionList = (List<Expression>) null;
      int index1 = 0;
      for (int count = original.Count; index1 < count; ++index1)
      {
        Expression expression = this.Visit(original[index1]);
        if (expressionList != null)
          expressionList.Add(expression);
        else if (expression != original[index1])
        {
          expressionList = new List<Expression>(count);
          for (int index2 = 0; index2 < index1; ++index2)
            expressionList.Add(original[index2]);
          expressionList.Add(expression);
        }
      }
      return expressionList != null ? expressionList.AsReadOnly() : original;
    }

    /// <summary>Visits the member assignment expression.</summary>
    /// <param name="assignment">The instance of member assigment expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual MemberAssignment VisitMemberAssignment(
      MemberAssignment assignment)
    {
      Expression expression = this.Visit(assignment.Expression);
      MemberInfo member = this.VisitMemberInfo(assignment.Member);
      return expression != assignment.Expression || member != assignment.Member ? Expression.Bind(member, expression) : assignment;
    }

    /// <summary>Visits the member member binding.</summary>
    /// <param name="binding">The instance of MemberMemberBinding.</param>
    /// <returns>Member binding itself after it has been processed.</returns>
    protected virtual MemberMemberBinding VisitMemberMemberBinding(
      MemberMemberBinding binding)
    {
      IEnumerable<MemberBinding> bindings = this.VisitBindingList(binding.Bindings);
      MemberInfo member = this.VisitMemberInfo(binding.Member);
      return bindings != binding.Bindings || member != binding.Member ? Expression.MemberBind(member, bindings) : binding;
    }

    /// <summary>Visits the member list binding.</summary>
    /// <param name="binding">The member list binding.</param>
    /// <returns>Member list binding after it has been processed.</returns>
    protected virtual MemberListBinding VisitMemberListBinding(
      MemberListBinding binding)
    {
      IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(binding.Initializers);
      MemberInfo member = this.VisitMemberInfo(binding.Member);
      return initializers != binding.Initializers || member != binding.Member ? Expression.ListBind(member, initializers) : binding;
    }

    protected virtual MemberInfo VisitMemberInfo(MemberInfo toVisit) => toVisit;

    /// <summary>Visits the binding list.</summary>
    /// <param name="original">The original binding list.</param>
    /// <returns>The binding list after the original has been processed.</returns>
    protected virtual IEnumerable<MemberBinding> VisitBindingList(
      ReadOnlyCollection<MemberBinding> original)
    {
      List<MemberBinding> memberBindingList = (List<MemberBinding>) null;
      int index1 = 0;
      for (int count = original.Count; index1 < count; ++index1)
      {
        MemberBinding memberBinding = this.VisitBinding(original[index1]);
        if (memberBindingList != null)
          memberBindingList.Add(memberBinding);
        else if (memberBinding != original[index1])
        {
          memberBindingList = new List<MemberBinding>(count);
          for (int index2 = 0; index2 < index1; ++index2)
            memberBindingList.Add(original[index2]);
          memberBindingList.Add(memberBinding);
        }
      }
      return (IEnumerable<MemberBinding>) memberBindingList ?? (IEnumerable<MemberBinding>) original;
    }

    /// <summary>Visits the element initializer list.</summary>
    /// <param name="original">The original element initializer list.</param>
    /// <returns>The element initializer list after it has been processed.</returns>
    protected virtual IEnumerable<ElementInit> VisitElementInitializerList(
      ReadOnlyCollection<ElementInit> original)
    {
      List<ElementInit> elementInitList = (List<ElementInit>) null;
      int index1 = 0;
      for (int count = original.Count; index1 < count; ++index1)
      {
        ElementInit elementInit = this.VisitElementInitializer(original[index1]);
        if (elementInitList != null)
          elementInitList.Add(elementInit);
        else if (elementInit != original[index1])
        {
          elementInitList = new List<ElementInit>(count);
          for (int index2 = 0; index2 < index1; ++index2)
            elementInitList.Add(original[index2]);
          elementInitList.Add(elementInit);
        }
      }
      return (IEnumerable<ElementInit>) elementInitList ?? (IEnumerable<ElementInit>) original;
    }

    /// <summary>Visits the lambda expression.</summary>
    /// <param name="lambda">The instance of lambda expression.</param>
    /// <returns>The lambda expression itself after it has been processed.</returns>
    protected virtual Expression VisitLambda(LambdaExpression lambda)
    {
      Expression body = this.Visit(lambda.Body);
      return body != lambda.Body ? (Expression) Expression.Lambda(lambda.Type, body, (IEnumerable<ParameterExpression>) lambda.Parameters) : (Expression) lambda;
    }

    /// <summary>Visits the new expression.</summary>
    /// <param name="nex">The instance of new expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual NewExpression VisitNew(NewExpression nex)
    {
      IEnumerable<Expression> arguments = (IEnumerable<Expression>) this.VisitExpressionList(nex.Arguments);
      if (arguments == nex.Arguments)
        return nex;
      return nex.Members != null ? Expression.New(nex.Constructor, arguments, (IEnumerable<MemberInfo>) nex.Members) : Expression.New(nex.Constructor, arguments);
    }

    /// <summary>Visits the member init expression.</summary>
    /// <param name="init">The instance of member init expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitMemberInit(MemberInitExpression init)
    {
      NewExpression newExpression = this.VisitNew(init.NewExpression);
      IEnumerable<MemberBinding> bindings = this.VisitBindingList(init.Bindings);
      return newExpression != init.NewExpression || bindings != init.Bindings ? (Expression) Expression.MemberInit(newExpression, bindings) : (Expression) init;
    }

    /// <summary>Visits the list init expression.</summary>
    /// <param name="init">The instance of list init expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitListInit(ListInitExpression init)
    {
      NewExpression newExpression = this.VisitNew(init.NewExpression);
      IEnumerable<ElementInit> initializers = this.VisitElementInitializerList(init.Initializers);
      return newExpression != init.NewExpression || initializers != init.Initializers ? (Expression) Expression.ListInit(newExpression, initializers) : (Expression) init;
    }

    /// <summary>Visits the new array expression.</summary>
    /// <param name="na">The instance of the new array expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitNewArray(NewArrayExpression na)
    {
      IEnumerable<Expression> expressions = (IEnumerable<Expression>) this.VisitExpressionList(na.Expressions);
      if (expressions == na.Expressions)
        return (Expression) na;
      return na.NodeType == ExpressionType.NewArrayInit ? (Expression) Expression.NewArrayInit(na.Type.GetElementType(), expressions) : (Expression) Expression.NewArrayBounds(na.Type.GetElementType(), expressions);
    }

    /// <summary>Visits the invocation expression.</summary>
    /// <param name="iv">The instance of the invocation expression.</param>
    /// <returns>The expression itself after it has been processed.</returns>
    protected virtual Expression VisitInvocation(InvocationExpression iv)
    {
      IEnumerable<Expression> arguments = (IEnumerable<Expression>) this.VisitExpressionList(iv.Arguments);
      Expression expression = this.Visit(iv.Expression);
      return arguments != iv.Arguments || expression != iv.Expression ? (Expression) Expression.Invoke(expression, arguments) : (Expression) iv;
    }

    public virtual void ApplyAnyCulture()
    {
    }

    /// <summary>
    /// Determins whether the object when translated to string query needs to be wrapped in
    /// quotation marks.
    /// </summary>
    /// <param name="objectValue">The object which is being translated to a string.</param>
    /// <returns>True if object needs quotes; otherwise false.</returns>
    protected virtual bool NeedsQuotes(object objectValue) => this.NeedsQuotesInternal(objectValue.GetType());

    protected virtual bool NeedsQuotes(MemberInfo memberInfo)
    {
      switch (memberInfo.MemberType)
      {
        case MemberTypes.Field:
          return this.NeedsQuotesInternal(((FieldInfo) memberInfo).FieldType);
        case MemberTypes.Property:
          return this.NeedsQuotesInternal(((PropertyInfo) memberInfo).PropertyType);
        default:
          throw new NotSupportedException();
      }
    }

    private bool NeedsQuotesInternal(Type type) => type == typeof (string) || type == typeof (DateTime) || type == typeof (Guid);

    public static Expression StripQuotes(Expression e)
    {
      while (e.NodeType == ExpressionType.Quote)
        e = ((UnaryExpression) e).Operand;
      return e;
    }

    /// <summary>Gets the value of a field.</summary>
    /// <param name="instance">The instance of an object that contains the field.</param>
    /// <param name="fieldName">Name of the field for which value ought to be retrieved.</param>
    /// <returns>The value of the field of the given name on the given instance.</returns>
    protected static object GetField(object instance, string fieldName) => instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public).GetValue(instance) ?? (object) string.Empty;

    /// <summary>
    /// Determines whether [is field enumerable] [the specified instance].
    /// </summary>
    /// <param name="instance">The instance.</param>
    /// <param name="fieldName">Name of the field.</param>
    /// <returns>
    /// 	<c>true</c> if [is field enumerable] [the specified instance]; otherwise, <c>false</c>.
    /// </returns>
    protected static bool IsFieldEnumerable(object instance, string fieldName)
    {
      FieldInfo field = instance.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public);
      return !(field == (FieldInfo) null) && !(field.FieldType == typeof (string)) && typeof (IEnumerable).IsAssignableFrom(field.FieldType);
    }
  }
}
