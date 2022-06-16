// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.Data.SerializableDynamicMetaObject
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Telerik.Sitefinity.Publishing.Twitter.Services.Data
{
  public class SerializableDynamicMetaObject : DynamicMetaObject
  {
    private Type objType;

    public SerializableDynamicMetaObject(
      Expression expression,
      BindingRestrictions restrictions,
      object value)
      : base(expression, restrictions, value)
    {
      this.objType = value.GetType();
    }

    public override DynamicMetaObject BindGetMember(GetMemberBinder binder)
    {
      Expression expression = this.Expression;
      SerializableDynamicObject serializableDynamicObject = (SerializableDynamicObject) this.Value;
      ConstantExpression constantExpression = Expression.Constant((object) binder.Name);
      MethodInfo method = this.objType.GetMethod("getValue", BindingFlags.Instance | BindingFlags.NonPublic);
      return new DynamicMetaObject((Expression) Expression.Call((Expression) Expression.Convert(expression, this.objType), method, (Expression) constantExpression), BindingRestrictions.GetTypeRestriction(expression, this.objType));
    }

    public override DynamicMetaObject BindSetMember(
      SetMemberBinder binder,
      DynamicMetaObject value)
    {
      Expression expression = this.Expression;
      ConstantExpression constantExpression = Expression.Constant((object) binder.Name);
      UnaryExpression unaryExpression = Expression.Convert(value.Expression, typeof (object));
      MethodInfo method = this.objType.GetMethod("setValue", BindingFlags.Instance | BindingFlags.NonPublic);
      return new DynamicMetaObject((Expression) Expression.Call((Expression) Expression.Convert(expression, this.objType), method, (Expression) constantExpression, (Expression) unaryExpression), BindingRestrictions.GetTypeRestriction(expression, this.objType));
    }

    public override IEnumerable<string> GetDynamicMemberNames() => ((SerializableDynamicObject) this.Value).getDynamicMemberNames();
  }
}
