// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.OpenAccess.OpenAccessExpressionVisitor`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Data.Linq.OpenAccess
{
  public class OpenAccessExpressionVisitor<TBaseItem, TDataItem> : Telerik.Sitefinity.Data.Linq.ExpressionVisitor
  {
    private readonly Dictionary<Expression, Expression> replacements;
    private bool firstIncludeDone;
    private static bool fallbackOnOrderByInMultilingual = true;
    private readonly HashSet<Expression> alreadyVisited = new HashSet<Expression>();

    static OpenAccessExpressionVisitor()
    {
      string str = ConfigurationManager.AppSettings.Get("sf:OAExpressionVisitorFlags");
      if (string.IsNullOrEmpty(str) || !str.Equals("1", StringComparison.OrdinalIgnoreCase))
        return;
      OpenAccessExpressionVisitor<TBaseItem, TDataItem>.fallbackOnOrderByInMultilingual = false;
    }

    public OpenAccessExpressionVisitor() => this.replacements = new Dictionary<Expression, Expression>();

    public Expression Enhance(Expression expression)
    {
      Expression expression1 = this.Visit(expression);
      this.alreadyVisited.Add(expression1);
      return expression1;
    }

    protected override Expression Visit(Expression exp) => this.alreadyVisited.Contains(exp) ? exp : base.Visit(exp);

    protected override Expression VisitMemberAccess(MemberExpression m)
    {
      bool result = false;
      Expression expression = this.TryConvertMember(m, out result);
      if (result)
        return expression;
      if (m.Expression != null)
      {
        if (m.Type == typeof (Lstring) || this.IsDynamicLstring(m))
          return (Expression) this.GetLstringMethodCall(m);
        if (m.Type.IsEnum)
          return (Expression) Expression.Convert(base.VisitMemberAccess(m), typeof (int));
      }
      return base.VisitMemberAccess(m);
    }

    protected override Expression VisitLambda(LambdaExpression lambda)
    {
      Type[] genericArguments = lambda.Type.GetGenericArguments();
      bool flag = false;
      for (int index = 0; index < genericArguments.Length; ++index)
      {
        Type type = this.ResolveType(genericArguments[index]);
        flag |= type != genericArguments[index];
        genericArguments[index] = type;
      }
      IList<ParameterExpression> parameters = this.RewriteParameters((IList<ParameterExpression>) lambda.Parameters);
      if (!(flag | parameters != lambda.Parameters))
        return base.VisitLambda(lambda);
      Type type1 = (Type) null;
      switch (genericArguments.Length)
      {
        case 1:
          type1 = typeof (Func<>);
          break;
        case 2:
          type1 = typeof (Func<,>);
          break;
        case 3:
          type1 = typeof (Func<,,>);
          break;
        case 4:
          type1 = typeof (Func<,,,>);
          break;
        case 5:
          type1 = typeof (Func<,,,,>);
          break;
        case 6:
          type1 = typeof (Func<,,,,,>);
          break;
      }
      Type delegateType = type1.MakeGenericType(genericArguments);
      Expression body = this.Visit(lambda.Body);
      return parameters != lambda.Parameters ? this.Visit((Expression) Expression.Lambda(delegateType, body, (IEnumerable<ParameterExpression>) parameters)) : (Expression) Expression.Lambda(delegateType, body, (IEnumerable<ParameterExpression>) parameters);
    }

    protected override NewExpression VisitNew(NewExpression nex)
    {
      Type objB = this.ResolveType(nex.Constructor.DeclaringType);
      if (object.Equals((object) nex.Constructor.DeclaringType, (object) objB))
        return base.VisitNew(nex);
      ConstructorInfo constructor = objB.GetConstructor(((IEnumerable<ParameterInfo>) nex.Constructor.GetParameters()).Select<ParameterInfo, Type>((Func<ParameterInfo, Type>) (p => p.ParameterType)).ToArray<Type>());
      IEnumerable<Expression> arguments = (IEnumerable<Expression>) this.VisitExpressionList(nex.Arguments);
      return nex.Members != null ? Expression.New(constructor, arguments, (IEnumerable<MemberInfo>) nex.Members) : Expression.New(constructor, arguments);
    }

    protected override Expression VisitMethodCall(MethodCallExpression m)
    {
      if (this.ShouldSkipVisitMethod(m))
        return (Expression) m;
      if (!this.NeedsToRewriteMethod(m))
        return base.VisitMethodCall(m);
      if (m.Method.Name == "Include" && m.Method.DeclaringType.Namespace == "Telerik.OpenAccess.Query")
        return this.VisitMethodInclude(m);
      ReadOnlyCollection<Expression> args = (ReadOnlyCollection<Expression>) null;
      Type[] genericArguments = m.Method.GetGenericArguments();
      bool flag1 = false;
      if ((m.Method.Name == "OrderByDescending" || m.Method.Name == "OrderBy") && OpenAccessExpressionVisitor<TBaseItem, TDataItem>.fallbackOnOrderByInMultilingual && SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        args = this.GetOrderByMultilingualArgs(m, genericArguments);
        flag1 |= args != null;
      }
      if (args == null)
      {
        args = this.VisitExpressionList(m.Arguments);
        flag1 |= args != m.Arguments;
      }
      if (this.NeedsToRewriteLstringToStringMethod(m))
        return this.VisitLstring(m, args);
      if (m.Method.Name == "GetValue" && object.Equals((object) m.Method.DeclaringType, (object) typeof (DataExtensions)))
        return this.RewriteToOpenAcccessSyntax(m, ref args, genericArguments);
      bool flag2 = false;
      for (int index = 0; index < ((IEnumerable<Type>) genericArguments).Count<Type>(); ++index)
      {
        Type type = this.ResolveType(genericArguments[index]);
        flag2 |= genericArguments[index] != type;
        genericArguments[index] = type;
      }
      if (((typeof (IQueryable).IsAssignableFrom(m.Method.DeclaringType) ? 1 : (m.Method.DeclaringType == typeof (Queryable) ? 1 : 0)) & (flag2 ? 1 : 0)) != 0)
      {
        MethodInfo method = m.Method;
        if (m.Method.IsGenericMethodDefinition)
          method = m.Method.MakeGenericMethod(genericArguments);
        else if (m.Method.IsGenericMethod)
          method = m.Method.GetGenericMethodDefinition().MakeGenericMethod(genericArguments);
        return (Expression) Expression.Call(method, (IEnumerable<Expression>) args);
      }
      Expression instance = this.Visit(m.Object);
      return instance != m.Object | flag1 ? (Expression) Expression.Call(instance, m.Method, (IEnumerable<Expression>) args) : (Expression) m;
    }

    private Expression RewriteToOpenAcccessSyntax(
      MethodCallExpression m,
      ref ReadOnlyCollection<Expression> args,
      Type[] genericArguments)
    {
      if (genericArguments.Length == 0)
        throw new NotSupportedException("NonGeneric method 'object GetValue(string fieldName)' is not supported in LINQ queries. Use generic 'TValue GetValue<TValue>(string fieldName)' instead.");
      Type[] genericArguments1 = m.Method.GetGenericArguments();
      if (args[1] is ConstantExpression constantExpression)
      {
        string str = constantExpression.Value as string;
        if (args[0] is ParameterExpression parameterExpression)
        {
          PropertyDescriptor propertyDescriptor1 = TypeDescriptor.GetProperties(parameterExpression.Type).Find(str, false);
          if (propertyDescriptor1 != null && propertyDescriptor1 is ILocalizablePropertyDescriptor)
          {
            CultureInfo culture = SystemManager.CurrentContext.Culture;
            bool flag = !(propertyDescriptor1 is MetafieldPropertyDescriptor propertyDescriptor2) || propertyDescriptor2.MetaField.IsLocalizable;
            LstringPropertyDescriptor propertyDescriptor3 = propertyDescriptor1 as LstringPropertyDescriptor;
            if (propertyDescriptor3 == null & flag)
              str = LstringPropertyDescriptor.GetFieldNameForCulture(str, culture);
            else if (propertyDescriptor3 != null)
              str = propertyDescriptor3.GetFieldName(culture);
            args = new ReadOnlyCollection<Expression>((IList<Expression>) new Expression[2]
            {
              (Expression) parameterExpression,
              (Expression) Expression.Constant((object) str)
            });
          }
        }
      }
      if (genericArguments1 != null && genericArguments1.Length == 1 && (((IEnumerable<Type>) genericArguments1).First<Type>() == typeof (Lstring) || ((IEnumerable<Type>) genericArguments1).First<Type>() == typeof (ChoiceOption)))
        genericArguments1[0] = typeof (string);
      return (Expression) Expression.Call(typeof (Telerik.OpenAccess.ExtensionMethods), "FieldValue", genericArguments1, args.ToArray<Expression>());
    }

    private Expression VisitLstring(
      MethodCallExpression m,
      ReadOnlyCollection<Expression> args)
    {
      if (m.Method.Name == "get_Item")
      {
        MemberExpression memberExpression = (MemberExpression) m.Object;
        ConstantExpression constantExpression = Expression.Constant((object) LstringPropertyDescriptor.GetFieldNameForCulture(memberExpression.Member.Name, new CultureInfo(args[0].ToString().Trim('"'))));
        return (Expression) Expression.Call(typeof (Telerik.OpenAccess.ExtensionMethods), "FieldValue", new Type[1]
        {
          typeof (string)
        }, memberExpression.Expression, (Expression) constantExpression);
      }
      MethodInfo method = (MethodInfo) null;
      List<Expression> arguments = new List<Expression>((IEnumerable<Expression>) args);
      foreach (MethodInfo methodInfo in ((IEnumerable<MethodInfo>) typeof (string).GetMethods()).Where<MethodInfo>((Func<MethodInfo, bool>) (c => c.Name == m.Method.Name && ((IEnumerable<ParameterInfo>) c.GetParameters()).Count<ParameterInfo>() == m.Arguments.Count)))
      {
        bool flag = true;
        ParameterInfo[] parameters = methodInfo.GetParameters();
        arguments.Clear();
        int num = ((IEnumerable<ParameterInfo>) parameters).Count<ParameterInfo>();
        for (int index = 0; index < num; ++index)
        {
          if (parameters[index].ParameterType == m.Arguments[index].Type)
          {
            arguments.Add((Expression) Expression.Convert(m.Arguments[index], parameters[index].ParameterType));
          }
          else
          {
            flag = false;
            break;
          }
        }
        if (flag)
        {
          method = methodInfo;
          break;
        }
      }
      MethodCallExpression exp = Expression.Call((Expression) Expression.Convert(m.Object, typeof (string)), method, arguments.ToArray());
      if (!(m.Method.Name == "Contains") && !(m.Method.Name == "StartsWith") && !(m.Method.Name == "EndsWith") || !SystemManager.CurrentContext.AppSettings.Multilingual || !SystemManager.CurrentContext.IsServiceRequest)
        return this.Visit((Expression) exp);
      CultureInfo culture1 = SystemManager.CurrentContext.Culture;
      MemberExpression m1 = m.Object.NodeType != ExpressionType.MemberAccess ? (m.Object as UnaryExpression).Operand as MemberExpression : m.Object as MemberExpression;
      Expression left1 = (Expression) Expression.Call((Expression) this.GetLstringMethodCall(m1, culture1), method, (IEnumerable<Expression>) arguments);
      List<CultureInfo> cultureInfoList = new List<CultureInfo>()
      {
        culture1
      };
      foreach (CultureInfo fallbackCulture in LstringPropertyDescriptor.GetFallbackCultures(culture1))
      {
        Expression left2 = (Expression) null;
        foreach (CultureInfo culture2 in cultureInfoList)
        {
          BinaryExpression right = Expression.MakeBinary(ExpressionType.Equal, (Expression) this.GetLstringMethodCall(m1, culture2), (Expression) Expression.Constant((object) null));
          left2 = left2 != null ? (Expression) Expression.AndAlso(left2, (Expression) right) : (Expression) right;
        }
        MethodCallExpression right1 = Expression.Call((Expression) this.GetLstringMethodCall(m1, fallbackCulture), method, (IEnumerable<Expression>) arguments);
        BinaryExpression right2 = Expression.MakeBinary(ExpressionType.AndAlso, left2, (Expression) right1);
        left1 = (Expression) Expression.MakeBinary(ExpressionType.OrElse, left1, (Expression) right2);
        cultureInfoList.Add(fallbackCulture);
      }
      return left1;
    }

    private Expression VisitMethodInclude(MethodCallExpression m)
    {
      Expression expression = this.Visit(m.Arguments[0]);
      if (!this.firstIncludeDone)
      {
        this.firstIncludeDone = true;
        LambdaExpression lambdaExpression = m.Arguments[1] is UnaryExpression unaryExpression ? unaryExpression.Operand as LambdaExpression : (LambdaExpression) null;
        if (lambdaExpression != null && lambdaExpression.Body.NodeType != ExpressionType.Constant)
        {
          ParameterExpression parameterExpression = Expression.Parameter(lambdaExpression.Parameters[0].Type, "p");
          expression = (Expression) Expression.Call(m.Object, m.Method, expression, (Expression) Expression.Lambda(lambdaExpression.Type, (Expression) Expression.Constant((object) "*"), parameterExpression));
        }
      }
      return expression == m.Arguments[0] ? (Expression) m : (Expression) Expression.Call(m.Object, m.Method, expression, m.Arguments[1]);
    }

    protected override MemberInfo VisitMemberInfo(MemberInfo toVisit)
    {
      Type objB = this.ResolveType(toVisit.DeclaringType);
      return object.Equals((object) toVisit.DeclaringType, (object) objB) ? toVisit : ((IEnumerable<MemberInfo>) objB.GetMember(toVisit.Name)).FirstOrDefault<MemberInfo>((Func<MemberInfo, bool>) (m => m.MemberType == toVisit.MemberType));
    }

    protected override Expression VisitUnary(UnaryExpression u) => (u.Type == typeof (Lstring) || u.Operand.Type == typeof (Lstring) || u.Operand.Type == typeof (ChoiceOption)) && u.NodeType == ExpressionType.Convert || this.IsDynamicLstring(u) ? this.Visit(u.Operand) : base.VisitUnary(u);

    protected override Expression VisitBinary(BinaryExpression b)
    {
      if (b.Left.Type == typeof (Lstring) || this.IsDynamicLstring(b))
      {
        Expression left1 = this.Visit(b.Left);
        Expression expression1 = this.Visit(b.Right);
        if (left1.Type == typeof (string) && (expression1.Type == typeof (Lstring) || expression1 is MemberExpression && this.IsDynamicLstring((MemberExpression) expression1)) && expression1 is ConstantExpression constantExpression && constantExpression.Value == null)
          expression1 = (Expression) Expression.Constant((object) null, typeof (string));
        Expression expression2 = this.Visit((Expression) b.Conversion);
        if (left1 != b.Left || expression1 != b.Right || expression2 != b.Conversion)
        {
          if (b.NodeType == ExpressionType.Coalesce && b.Conversion != null)
            throw new NotSupportedException();
          ExpressionType nodeType = b.NodeType;
          BinaryExpression expression3 = Expression.MakeBinary(nodeType, left1, expression1);
          if (SystemManager.CurrentContext.AppSettings.Multilingual && SystemManager.CurrentContext.AppSettings.LanguageFallback)
          {
            MemberExpression left2 = b.Left as MemberExpression;
            CultureInfo culture = SystemManager.CurrentContext.Culture;
            if (culture != CultureInfo.InvariantCulture)
            {
              bool flag = false;
              int num = 0;
              while (true)
              {
                if (AppSettings.CurrentSettings.AllLanguages.ContainsKey(culture.LCID))
                {
                  if (num > 0 && (b.Left.Type == typeof (Lstring) || this.IsDynamicLstring(b)))
                    expression3 = this.GetCultureFallbackExpression(expression3, left2, expression1, nodeType, culture);
                  if (culture.Equals((object) SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage))
                    flag = true;
                }
                if (culture.Parent != null && !culture.IsNeutralCulture)
                {
                  culture = culture.Parent;
                  ++num;
                }
                else
                  break;
              }
              if (flag && (b.Left.Type == typeof (Lstring) || this.IsDynamicLstring(b)))
                expression3 = this.GetCultureFallbackExpression(expression3, left2, expression1, nodeType, CultureInfo.InvariantCulture);
              return (Expression) expression3;
            }
          }
          return (Expression) expression3;
        }
      }
      return base.VisitBinary(b);
    }

    protected override Expression VisitParameter(ParameterExpression p)
    {
      Expression expression;
      return this.replacements.TryGetValue((Expression) p, out expression) ? expression : base.VisitParameter(p);
    }

    protected override Expression VisitConstant(ConstantExpression c) => c.Type.IsEnum ? (Expression) Expression.Constant((object) (int) Enum.Parse(c.Type, c.Value.ToString())) : base.VisitConstant(c);

    private bool ShouldSkipVisitMethod(MethodCallExpression m)
    {
      if (m.Method.Name == "Join")
      {
        if (typeof (DynamicContent).IsAssignableFrom(m.Method.GetGenericArguments()[1]))
          return true;
      }
      foreach (MethodCallExpression m1 in m.Arguments.Where<Expression>((Func<Expression, bool>) (x => x.NodeType == ExpressionType.Call)).Cast<MethodCallExpression>())
      {
        if (this.ShouldSkipVisitMethod(m1))
          return true;
      }
      return false;
    }

    private IList<ParameterExpression> RewriteParameters(
      IList<ParameterExpression> parameters)
    {
      List<ParameterExpression> parameterExpressionList = (List<ParameterExpression>) null;
      bool flag = false;
      for (int index = 0; index < parameters.Count; ++index)
      {
        ParameterExpression parameter = parameters[index];
        Type type = this.ResolveType(parameter.Type);
        if (type != parameter.Type)
        {
          ParameterExpression parameterExpression = Expression.Parameter(type, "re-" + parameter.Name);
          this.replacements[(Expression) parameter] = (Expression) parameterExpression;
          if (parameterExpressionList == null)
            parameterExpressionList = new List<ParameterExpression>();
          parameterExpressionList.Add(parameterExpression);
          flag = true;
        }
        else
        {
          if (parameterExpressionList == null)
            parameterExpressionList = new List<ParameterExpression>();
          parameterExpressionList.Add(parameter);
        }
      }
      return flag ? (IList<ParameterExpression>) parameterExpressionList : parameters;
    }

    /// <summary>Determines weather method needs to be rewriten.</summary>
    /// <param name="m">Method call expression.</param>
    /// <returns>True if method needs to be rewritten; otherwise false.</returns>
    private bool NeedsToRewriteMethod(MethodCallExpression m)
    {
      switch (m.Method.Name)
      {
        case "Any":
          return typeof (IQueryable).IsAssignableFrom(m.Arguments[0].Type);
        case "Contains":
          Type[] genericArguments = m.Method.DeclaringType.GetGenericArguments();
          return ((IEnumerable<Type>) genericArguments).Count<Type>() == 1 ? genericArguments[0] == typeof (Guid) : this.NeedsToRewriteLstringToStringMethod(m);
        case "EndsWith":
        case "Matches":
        case "StartsWith":
          return this.NeedsToRewriteLstringToStringMethod(m);
        case "Equals":
          return this.NeedsToRewriteLstringToStringMethod(m);
        case "FieldValue":
          return false;
        case "Visit":
          return false;
        default:
          return !m.Method.Name.StartsWith("get_") && !m.Method.Name.StartsWith("set_") || m.Method.Name == "get_Item" && (m.Method.DeclaringType == typeof (Lstring) || this.IsDynamicLstring(m));
      }
    }

    private bool IsDynamicLstring(MethodCallExpression m) => m.Object is MemberExpression && this.IsDynamicLstring((MemberExpression) m.Object);

    private bool IsDynamicLstring(BinaryExpression m) => m.Left is MemberExpression && this.IsDynamicLstring((MemberExpression) m.Left);

    private bool IsDynamicLstring(UnaryExpression m) => m.Operand is MemberExpression && this.IsDynamicLstring((MemberExpression) m.Operand);

    private bool IsDynamicLstring(MemberExpression exp)
    {
      if (exp.Expression != null && typeof (DynamicContent).IsAssignableFrom(exp.Expression.Type))
      {
        string name = exp.Member.Name;
        PropertyDescriptor property = TypeDescriptor.GetProperties(exp.Expression.Type)[name];
        if (property != null && property.PropertyType == typeof (Lstring))
          return true;
      }
      return false;
    }

    /// <summary>
    /// Determines weather the method on the Lstring type should be rewritten to the string type.
    /// </summary>
    /// <param name="m">Method call expression.</param>
    /// <returns>True if method should be rewritten; otherwise false.</returns>
    private bool NeedsToRewriteLstringToStringMethod(MethodCallExpression m)
    {
      if ((m.Method.Name == "Contains" || m.Method.Name == "StartsWith" || m.Method.Name == "EndsWith") && m.Object != null && m.Object.NodeType == ExpressionType.Convert && SystemManager.CurrentContext.IsServiceRequest)
      {
        Expression operand = (m.Object as UnaryExpression).Operand;
        if (operand != null && operand.NodeType == ExpressionType.MemberAccess && (operand as MemberExpression).Type == typeof (Lstring))
          return true;
      }
      if (!(m.Method.DeclaringType == typeof (Lstring)) && !this.IsDynamicLstring(m))
        return false;
      switch (m.Method.Name)
      {
        case "Contains":
        case "EndsWith":
        case "Equals":
        case "Matches":
        case "StartsWith":
        case "ToUpper":
        case "get_Item":
          return true;
        default:
          throw new NotSupportedException(string.Format("Method '{0}' is not supported on the '{1}' type.", (object) m.Method.Name, (object) m.Method.DeclaringType));
      }
    }

    /// <summary>
    /// Rewrites the arguments of the method that needs to be removed to the arguments
    /// of the Where method.
    /// </summary>
    /// <param name="arguments">The list of arguments.</param>
    /// <returns>Method call to the Where method.</returns>
    private MethodCallExpression RewriteMethodCallToWhereMethod(
      IList<Expression> arguments)
    {
      return Expression.Call(typeof (Queryable), "Where", new Type[1]
      {
        this.ResolveType(arguments[0].Type.GetGenericArguments()[0])
      }, arguments.ToArray<Expression>());
    }

    /// <summary>
    /// Tries to cast member access to an actual type, when supported.
    /// </summary>
    /// <param name="m">MemberExpression to be cast.</param>
    /// <returns>Modified expression.</returns>
    private Expression TryConvertMember(MemberExpression m, out bool result)
    {
      result = false;
      Type returnType = this.ResolveType(m.Member.DeclaringType);
      if ((m.Type.IsInterface || m.Type.IsAbstract) && returnType != m.Member.DeclaringType)
      {
        PropertyInfo property = returnType.GetProperty(m.Member.Name, returnType);
        if (property == (PropertyInfo) null)
          property = returnType.GetProperty(m.Member.Name);
        if (property != (PropertyInfo) null)
        {
          MemberExpression memberExpression = Expression.MakeMemberAccess(this.Visit(m.Expression), (MemberInfo) property);
          object[] customAttributes = memberExpression.Member.GetCustomAttributes(typeof (ActualTypeAttribute), true);
          if (customAttributes.Length == 0)
            return (Expression) memberExpression;
          Type actualType = ((ActualTypeAttribute) customAttributes[0]).ActualType;
          result = true;
          return (Expression) Expression.Convert((Expression) memberExpression, actualType);
        }
      }
      return (Expression) m;
    }

    /// <summary>
    /// This method resolves the type to a proper type in order to make the query work.
    /// </summary>
    /// <param name="type">The type being examined.</param>
    /// <returns>A resolved type.</returns>
    private Type ResolveType(Type type)
    {
      if (typeof (Lstring) == type)
        return typeof (string);
      if (typeof (TDataItem).BaseType == type)
        return typeof (TDataItem);
      if (type.IsGenericType)
      {
        Type[] genericArguments = type.GetGenericArguments();
        Type[] typeArray = new Type[genericArguments.Length];
        bool flag = false;
        for (int index = 0; index < genericArguments.Length; ++index)
        {
          typeArray[index] = this.ResolveType(genericArguments[index]);
          if (!object.Equals((object) genericArguments[index], (object) typeArray[index]))
            flag = true;
        }
        if (flag)
          return type.GetGenericTypeDefinition().MakeGenericType(typeArray);
      }
      return type;
    }

    private BinaryExpression GetCultureFallbackExpression(
      BinaryExpression expression,
      MemberExpression left,
      Expression right,
      ExpressionType expressionType,
      CultureInfo culture)
    {
      if (right is MemberExpression member && !this.IsParameter(member))
      {
        right = (Expression) Expression.Constant(this.GetValue(member));
      }
      else
      {
        LambdaExpression lambdaExpression = Expression.Lambda(right);
        try
        {
          right = (Expression) Expression.Constant(lambdaExpression.Compile().DynamicInvoke());
        }
        catch (Exception ex)
        {
        }
      }
      MethodCallExpression lstringMethodCall = this.GetLstringMethodCall(left, culture);
      BinaryExpression right1 = Expression.MakeBinary(expressionType, (Expression) lstringMethodCall, right);
      return expression == null ? right1 : Expression.MakeBinary(ExpressionType.OrElse, (Expression) expression, (Expression) right1);
    }

    private object GetValue(MemberExpression member) => Expression.Lambda<Func<object>>((Expression) Expression.Convert((Expression) member, typeof (object))).Compile()();

    private bool IsParameter(MemberExpression member)
    {
      Expression expression;
      for (; member != null; member = expression as MemberExpression)
      {
        expression = member.Expression;
        if (expression != null)
        {
          if (expression.NodeType == ExpressionType.Parameter)
            return true;
        }
        else
          break;
      }
      return false;
    }

    private MethodCallExpression GetLstringMethodCall(
      MemberExpression m,
      CultureInfo culture = null)
    {
      if (culture == null)
        culture = SystemManager.CurrentContext.Culture;
      MethodInfo method = MethodInfoResolver.GetMethodInfo((Expression<System.Action>) (() => default (object).FieldValue<object>(default (string)))).GetGenericMethodDefinition().MakeGenericMethod(typeof (string));
      string empty = string.Empty;
      string str = !(TypeDescriptor.GetProperties(this.GetType().GenericTypeArguments[1]).Find(m.Member.Name, true) is LstringPropertyDescriptor propertyDescriptor) ? (!(m.Type == typeof (Lstring)) ? this.GetDynamicLstringFieldName(m.Member, culture) : this.GetLstringFieldName(m.Member, culture)) : propertyDescriptor.GetFieldName(culture);
      Expression expression = m.Expression;
      ConstantExpression constantExpression = Expression.Constant((object) str);
      return Expression.Call(method, expression, (Expression) constantExpression);
    }

    private string GetLstringFieldName(MemberInfo member, CultureInfo culture)
    {
      if (culture == null && SystemManager.CurrentContext.AppSettings.Multilingual)
        culture = SystemManager.CurrentContext.Culture;
      if (culture == null || culture.Equals((object) AppSettings.CurrentSettings.DefaultFrontendLanguage))
      {
        LStringPropertyAttribute propertyAttribute = (LStringPropertyAttribute) ((IEnumerable<object>) member.GetCustomAttributes(typeof (LStringPropertyAttribute), true)).FirstOrDefault<object>();
        if (propertyAttribute != null)
          return propertyAttribute.InvariantCultureField;
      }
      return LstringPropertyDescriptor.GetFieldNameForCulture(member.Name, culture);
    }

    private string GetDynamicLstringFieldName(MemberInfo member, CultureInfo culture)
    {
      if (culture == null && SystemManager.CurrentContext.AppSettings.Multilingual)
        culture = SystemManager.CurrentContext.Culture;
      return DynamicLstringPropertyDescriptor.GetFieldNameForCulture(member.Name, culture);
    }

    private ReadOnlyCollection<Expression> GetOrderByMultilingualArgs(
      MethodCallExpression m,
      Type[] genericArguments)
    {
      ReadOnlyCollection<Expression> multilingualArgs = (ReadOnlyCollection<Expression>) null;
      if (m.Arguments.Count == 2 && genericArguments.Length == 2)
      {
        List<Expression> expressionList = new List<Expression>(2);
        expressionList.Add(this.Visit(m.Arguments[0]));
        Expression exp = m.Arguments[1];
        Expression expression = (Expression) null;
        ReadOnlyCollection<ParameterExpression> expressionParameters;
        MemberExpression operationArgument = this.GetMemberExpressionOfOrderByOperationArgument(exp, out expressionParameters);
        if (operationArgument != null && (operationArgument.Type == typeof (Lstring) || this.IsDynamicLstring(operationArgument)))
        {
          CultureInfo culture = SystemManager.CurrentContext.Culture;
          CultureInfo invariantCulture = CultureInfo.InvariantCulture;
          if (invariantCulture != culture)
          {
            MethodCallExpression lstringMethodCall = this.GetLstringMethodCall(operationArgument, culture);
            ConditionalExpression conditionalExpression = Expression.Condition((Expression) Expression.MakeBinary(ExpressionType.Equal, (Expression) lstringMethodCall, (Expression) Expression.Constant((object) null)), (Expression) this.GetLstringMethodCall(operationArgument, invariantCulture), (Expression) lstringMethodCall);
            Type[] typeArray = new Type[genericArguments.Length];
            for (int index = 0; index < genericArguments.Length; ++index)
              typeArray[index] = this.ResolveType(genericArguments[index]);
            Type delegateType = typeof (Func<,>).MakeGenericType(typeArray);
            IList<ParameterExpression> parameterExpressionList = this.RewriteParameters((IList<ParameterExpression>) expressionParameters);
            ConditionalExpression body = conditionalExpression;
            IList<ParameterExpression> parameters = parameterExpressionList;
            LambdaExpression operand = Expression.Lambda(delegateType, (Expression) body, (IEnumerable<ParameterExpression>) parameters);
            expression = (Expression) Expression.MakeUnary(exp.NodeType, (Expression) operand, exp.Type);
          }
        }
        if (expression == null)
          expression = this.Visit(exp);
        expressionList.Add(expression);
        multilingualArgs = expressionList.AsReadOnly();
      }
      return multilingualArgs;
    }

    private MemberExpression GetMemberExpressionOfOrderByOperationArgument(
      Expression argument,
      out ReadOnlyCollection<ParameterExpression> expressionParameters)
    {
      MemberExpression operationArgument = (MemberExpression) null;
      expressionParameters = (ReadOnlyCollection<ParameterExpression>) null;
      if (argument is UnaryExpression unaryExpression && unaryExpression.Operand is LambdaExpression operand)
      {
        expressionParameters = operand.Parameters;
        if (operand.Body != null && operand.Body.NodeType == ExpressionType.MemberAccess)
          operationArgument = operand.Body as MemberExpression;
        else if (operand.Body is UnaryExpression body)
          operationArgument = body.Operand as MemberExpression;
      }
      return operationArgument;
    }

    private delegate string lStringDelegate(string fieldName);
  }
}
