// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.UsersUtils
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Provides utility functions for Users</summary>
  public static class UsersUtils
  {
    internal static User GetUserByEmailOrUsername(
      UserManager manager,
      string emailOrUsername,
      bool searchOnlyLocalUsers)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      UsersUtils.\u003C\u003Ec__DisplayClass0_0 cDisplayClass00 = new UsersUtils.\u003C\u003Ec__DisplayClass0_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass00.emailOrUsername = emailOrUsername;
      IQueryable<User> users = manager.GetUsers();
      ParameterExpression parameterExpression1;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      IQueryable<User> source = users.Where<User>(Expression.Lambda<Func<User, bool>>((Expression) Expression.Equal((Expression) Expression.Call(x.Email, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass00, typeof (UsersUtils.\u003C\u003Ec__DisplayClass0_0)), FieldInfo.GetFieldFromHandle(__fieldref (UsersUtils.\u003C\u003Ec__DisplayClass0_0.emailOrUsername))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>())), parameterExpression1));
      if (searchOnlyLocalUsers)
        source = source.Where<User>((Expression<Func<User, bool>>) (u => u.Password != default (string) && u.Salt != default (string)));
      if (source.Count<User>() > 0)
        return source.First<User>();
      if (source.Count<User>() != 0)
        return (User) null;
      ParameterExpression parameterExpression2;
      // ISSUE: method reference
      // ISSUE: field reference
      // ISSUE: method reference
      return users.Where<User>(Expression.Lambda<Func<User, bool>>((Expression) Expression.Equal((Expression) Expression.Call(x.UserName, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>()), (Expression) Expression.Call((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass00, typeof (UsersUtils.\u003C\u003Ec__DisplayClass0_0)), FieldInfo.GetFieldFromHandle(__fieldref (UsersUtils.\u003C\u003Ec__DisplayClass0_0.emailOrUsername))), (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (string.ToLower)), Array.Empty<Expression>())), parameterExpression2)).FirstOrDefault<User>();
    }

    internal static User GetUserByExternalProviderAndId(
      UserManager manager,
      string externalProviderName,
      string externalId)
    {
      return manager.GetUsers().FirstOrDefault<User>((Expression<Func<User, bool>>) (u => u.ExternalProviderName == externalProviderName && u.ExternalId == externalId));
    }
  }
}
