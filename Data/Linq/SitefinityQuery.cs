// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Linq.SitefinityQuery
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Data.Linq
{
  /// <summary>
  /// Represents a Linq to Sitefinity query to be used when querying Sitefinity model with LINQ.
  /// </summary>
  /// <remarks>
  /// This class works, both, with OpenAccess and XML providers.
  /// </remarks>
  public static class SitefinityQuery
  {
    /// <summary>
    /// Gets the Sitefinity query for the specified generic type.
    /// </summary>
    /// <typeparam name="TBaseItem">The base type of the data item for which the query ought to be created.</typeparam>
    /// <typeparam name="TDataItem">The specialized type of the data item for which the query ought to be created.</typeparam>
    /// <param name="dataProvider">The instance of the data provider that invoked this query.</param>
    /// <returns>Generic IQueryable object.</returns>
    public static IQueryable<TBaseItem> Get<TBaseItem, TDataItem>(
      DataProviderBase dataProvider)
      where TBaseItem : class
      where TDataItem : class
    {
      return (IQueryable<TBaseItem>) new LinqQuery<TBaseItem>(dataProvider);
    }

    /// <summary>
    /// Gets the Sitefinity query for the specified generic type.
    /// </summary>
    /// <typeparam name="TDataItem">The type of the data item.</typeparam>
    /// <param name="dataProvider">The instance of the data provider that invoked this query.</param>
    /// <returns>Generic IQueryable object.</returns>
    public static IQueryable<TDataItem> Get<TDataItem>(DataProviderBase dataProvider) where TDataItem : class => dataProvider != null ? (IQueryable<TDataItem>) new LinqQuery<TDataItem>(dataProvider) : throw new ArgumentNullException(nameof (dataProvider));

    /// <summary>
    /// Gets the Sitefinity query for the specified generic type.
    /// </summary>
    /// <param name="baseType">The base type of the data item for which the query ought to be created.</param>
    /// <param name="queryType">The specialized type of the data item for which the query ought to be created.</param>
    /// <param name="dataProvider">The instance of the data provider that invoked this query.</param>
    /// <returns>Generic IQueryable object.</returns>
    public static IQueryable Get(
      Type baseType,
      Type queryType,
      DataProviderBase dataProvider)
    {
      return (IQueryable) Activator.CreateInstance(typeof (LinqQuery<,>).MakeGenericType(baseType, queryType), (object[]) new DataProviderBase[1]
      {
        dataProvider
      });
    }

    /// <summary>
    /// Gets the Sitefinity query for the specified generic type.
    /// </summary>
    /// <param name="baseType">The base type of the data item for which the query ought to be created.</param>
    /// <param name="dataProvider">The instance of the data provider that invoked this query.</param>
    /// <returns>Generic IQueryable object.</returns>
    public static IQueryable Get(Type baseType, DataProviderBase dataProvider) => (IQueryable) Activator.CreateInstance(typeof (LinqQuery<>).MakeGenericType(baseType), (object[]) new DataProviderBase[1]
    {
      dataProvider
    });

    /// <summary>Gets Sitefinity query</summary>
    /// <typeparam name="TBaseType">Query to use with IntelliSense</typeparam>
    /// <param name="actualType">Actual type to query against</param>
    /// <param name="dataProvider">Data provider to use</param>
    /// <returns>Sitefinity query</returns>
    public static IQueryable<TBaseType> Get<TBaseType>(
      Type actualType,
      DataProviderBase dataProvider)
    {
      actualType = dataProvider != null ? SitefinityQuery.GetContextType(actualType, dataProvider) : throw new ArgumentNullException(nameof (dataProvider));
      if (typeof (TBaseType) == typeof (DynamicContent))
        return SitefinityQuery.GetDynamicContentQuery<TBaseType>(actualType, dataProvider);
      return (IQueryable<TBaseType>) typeof (LinqQuery<,>).MakeGenericType(typeof (TBaseType), actualType).GetConstructor(new Type[1]
      {
        typeof (DataProviderBase)
      }).Invoke(new object[1]{ (object) dataProvider });
    }

    /// <summary>
    /// This method returns the actual type from the context if it is different from the actual type passed in as a parameter.
    /// When in a single request two metadata sources are added(aggregated) to the same OpenAccessConnection this will result
    /// in two separate assemblies being generated for the artificial types contained in the MetadataSource. The problem is that
    /// in the current request the first time a manager is accessed, its transaction(OaContext) will be stored for the duration of the http request
    /// and it will know only about the first generated assembly and its types. If another manager is accessed, on which the first is dependant(due to some logic),
    /// another assembly will be generated, for whose types the transaction will not know about, which would result in an exception.
    /// </summary>
    /// <param name="actualType">The actual type.</param>
    /// <param name="dataProvider">The data provider.</param>
    /// <returns>The context type if it is different from the actual type.</returns>
    private static Type GetContextType(Type actualType, DataProviderBase dataProvider)
    {
      if (dataProvider is IOpenAccessDataProvider provider)
      {
        IPersistentTypeDescriptor persistentTypeDescriptor = provider.GetContext().PersistentMetaData.GetPersistentTypeDescriptor(actualType.FullName);
        if (persistentTypeDescriptor != null)
        {
          Type describedType = persistentTypeDescriptor.DescribedType;
          if (!object.Equals((object) actualType, (object) describedType))
            actualType = describedType;
        }
      }
      return actualType;
    }

    /// <summary>
    /// Creates a query that will add an additional filter to queried items: only items that have a view
    /// action granted will be returned as a result to this query
    /// </summary>
    /// <typeparam name="TBaseType">Query to use with intellisense</typeparam>
    /// <param name="dataProvider">Data provider to use</param>
    /// <param name="permissionSetName">Permission set whose first action is the view action to check for</param>
    /// <returns>Only items that can be viewed</returns>
    /// <remarks>If <paramref name="setName" /> is <c>null</c>, no security checks will be made.</remarks>
    public static IQueryable<TBaseType> Get<TBaseType>(
      DataProviderBase dataProvider,
      string permissionSetName)
      where TBaseType : class, ISecuredObject
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType> cDisplayClass60 = new SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass60.permissionSetName = permissionSetName;
      if (dataProvider == null)
        throw new ArgumentNullException(nameof (dataProvider));
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      IQueryable<TBaseType> queryable;
      // ISSUE: reference to a compiler-generated field
      if ((!dataProvider.FilterQueriesByViewPermissions || dataProvider.SuppressSecurityChecks || currentIdentity == null || currentIdentity.IsUnrestricted ? 1 : (cDisplayClass60.permissionSetName.IsNullOrWhitespace() ? 1 : 0)) == 0)
      {
        // ISSUE: object of a compiler-generated type is created
        // ISSUE: variable of a compiler-generated type
        SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType> cDisplayClass61 = new SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>();
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.CS\u0024\u003C\u003E8__locals1 = cDisplayClass60;
        List<Guid> list = currentIdentity.Roles.Select<RoleInfo, Guid>((Func<RoleInfo, Guid>) (r => r.Id)).ToList<Guid>();
        list.Add(SecurityManager.CurrentUserId);
        // ISSUE: reference to a compiler-generated field
        cDisplayClass61.principalIDs = list.ToArray();
        IQueryable<TBaseType> source = SitefinityQuery.Get<TBaseType>(dataProvider);
        ParameterExpression parameterExpression1 = Expression.Parameter(typeof (TBaseType), "secObj");
        // ISSUE: method reference
        BinaryExpression left1 = Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_InheritsPermissions))), (Expression) Expression.Constant((object) true, typeof (bool)));
        // ISSUE: method reference
        MethodInfo methodFromHandle1 = (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any));
        // ISSUE: method reference
        Expression[] expressionArray1 = new Expression[2]
        {
          (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Permissions))),
          null
        };
        ParameterExpression parameterExpression2;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        expressionArray1[1] = (Expression) Expression.Lambda<Func<Permission, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Deny % 2 != 0, (Expression) Expression.NotEqual(p.ObjectId, (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Inequality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_SetName))), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.CS\u0024\u003C\u003E8__locals1), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>.permissionSetName), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.principalIDs), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))),
          (Expression) Expression.Property((Expression) parameterExpression2, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_PrincipalId)))
        })), parameterExpression2);
        UnaryExpression right1 = Expression.Not((Expression) Expression.Call((Expression) null, methodFromHandle1, expressionArray1));
        BinaryExpression left2 = Expression.AndAlso((Expression) left1, (Expression) right1);
        // ISSUE: method reference
        MethodInfo methodFromHandle2 = (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any));
        // ISSUE: method reference
        Expression[] expressionArray2 = new Expression[2]
        {
          (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Permissions))),
          null
        };
        ParameterExpression parameterExpression3;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        expressionArray2[1] = (Expression) Expression.Lambda<Func<Permission, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Grant > 0, (Expression) Expression.NotEqual(p.ObjectId, (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Inequality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_SetName))), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.CS\u0024\u003C\u003E8__locals1), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>.permissionSetName), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.principalIDs), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))),
          (Expression) Expression.Property((Expression) parameterExpression3, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_PrincipalId)))
        })), parameterExpression3);
        MethodCallExpression right2 = Expression.Call((Expression) null, methodFromHandle2, expressionArray2);
        BinaryExpression left3 = Expression.AndAlso((Expression) left2, (Expression) right2);
        // ISSUE: method reference
        BinaryExpression left4 = Expression.Equal((Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_InheritsPermissions))), (Expression) Expression.Constant((object) false, typeof (bool)));
        // ISSUE: method reference
        MethodInfo methodFromHandle3 = (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any));
        // ISSUE: method reference
        Expression[] expressionArray3 = new Expression[2]
        {
          (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Permissions))),
          null
        };
        ParameterExpression parameterExpression4;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        expressionArray3[1] = (Expression) Expression.Lambda<Func<Permission, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Deny % 2 != 0, (Expression) Expression.Equal(p.ObjectId, (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_SetName))), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.CS\u0024\u003C\u003E8__locals1), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>.permissionSetName), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.principalIDs), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))),
          (Expression) Expression.Property((Expression) parameterExpression4, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_PrincipalId)))
        })), parameterExpression4);
        UnaryExpression right3 = Expression.Not((Expression) Expression.Call((Expression) null, methodFromHandle3, expressionArray3));
        BinaryExpression left5 = Expression.AndAlso((Expression) left4, (Expression) right3);
        // ISSUE: method reference
        MethodInfo methodFromHandle4 = (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Any));
        // ISSUE: method reference
        Expression[] expressionArray4 = new Expression[2]
        {
          (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Permissions))),
          null
        };
        ParameterExpression parameterExpression5;
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        // ISSUE: field reference
        // ISSUE: type reference
        // ISSUE: method reference
        expressionArray4[1] = (Expression) Expression.Lambda<Func<Permission, bool>>((Expression) Expression.AndAlso((Expression) Expression.AndAlso((Expression) Expression.AndAlso(p.Grant > 0, (Expression) Expression.Equal(p.ObjectId, (Expression) Expression.Property((Expression) parameterExpression1, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (ISecuredObject.get_Id))), false, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Guid.op_Equality)))), (Expression) Expression.Equal((Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_SetName))), (Expression) Expression.Field((Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.CS\u0024\u003C\u003E8__locals1), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>.permissionSetName), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_0<TBaseType>))))), (Expression) Expression.Call((Expression) null, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Enumerable.Contains)), new Expression[2]
        {
          (Expression) Expression.Field((Expression) Expression.Constant((object) cDisplayClass61, typeof (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>)), FieldInfo.GetFieldFromHandle(__fieldref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>.principalIDs), __typeref (SitefinityQuery.\u003C\u003Ec__DisplayClass6_1<TBaseType>))),
          (Expression) Expression.Property((Expression) parameterExpression5, (MethodInfo) MethodBase.GetMethodFromHandle(__methodref (Permission.get_PrincipalId)))
        })), parameterExpression5);
        MethodCallExpression right4 = Expression.Call((Expression) null, methodFromHandle4, expressionArray4);
        BinaryExpression right5 = Expression.AndAlso((Expression) left5, (Expression) right4);
        Expression<Func<TBaseType, bool>> predicate = Expression.Lambda<Func<TBaseType, bool>>((Expression) Expression.OrElse((Expression) left3, (Expression) right5), parameterExpression1);
        queryable = Queryable.Distinct<TBaseType>(source.Where<TBaseType>(predicate));
      }
      else
        queryable = SitefinityQuery.Get<TBaseType>(dataProvider);
      return queryable;
    }

    /// <summary>
    /// Creates a query that will add an additional filter to queried items: only items that have a view
    /// action granted will be returned as a result to this query
    /// </summary>
    /// <typeparam name="TBaseType">Query to use with intellisense</typeparam>
    /// <param name="dataProvider">Data provider to use</param>
    /// <param name="method">Reflection information about the method</param>
    /// <returns>Only items that can be viewed</returns>
    /// <remarks>
    /// <para>
    /// To appropriately use this method, you should apply one the attributes inheriting from
    /// <see cref="T:Telerik.Sitefinity.Security.PermissionAttribute" />. If there is no such attribute,
    /// no security checks will be performed.
    /// </para>
    /// <para>
    /// An effective use of this method will be use the value returned by
    /// <see cref="M:System.Reflection.MethodBase.GetCurrentMethod()" />
    /// as the parameter value for <paramref name="method" />.
    /// </para>
    /// </remarks>
    /// <exception cref="T:System.ArgumentNullException">
    /// When either <paramref name="dataProvider" /> or <paramref name="method" /> is <c>null</c>.
    /// </exception>
    public static IQueryable<TBaseType> Get<TBaseType>(
      DataProviderBase dataProvider,
      MethodBase method)
      where TBaseType : class
    {
      if (dataProvider == null || method == (MethodBase) null)
        throw new ArgumentNullException();
      if (!typeof (ISecuredObject).IsAssignableFrom(typeof (TBaseType)))
        return SitefinityQuery.Get<TBaseType>(dataProvider);
      string str = (string) null;
      TypedEnumeratorPermissionAttribute permissionAttribute1 = method.GetCustomAttributes(typeof (TypedEnumeratorPermissionAttribute), true).Cast<TypedEnumeratorPermissionAttribute>().Where<TypedEnumeratorPermissionAttribute>((Func<TypedEnumeratorPermissionAttribute, bool>) (attr => attr.ItemType == typeof (TBaseType))).SingleOrDefault<TypedEnumeratorPermissionAttribute>();
      if (permissionAttribute1 != null)
        str = permissionAttribute1.PermissionSetName;
      else if (((IEnumerable<object>) method.GetCustomAttributes(typeof (PermissionAttribute), true)).FirstOrDefault<object>() is PermissionAttribute permissionAttribute2)
        str = permissionAttribute2.PermissionSetName;
      if (str == null)
        return SitefinityQuery.Get<TBaseType>(dataProvider);
      return (IQueryable<TBaseType>) typeof (SitefinityQuery).GetMethod(nameof (Get), new Type[2]
      {
        typeof (DataProviderBase),
        typeof (string)
      }).MakeGenericMethod(typeof (TBaseType)).Invoke((object) null, new object[2]
      {
        (object) dataProvider,
        (object) str
      });
    }

    /// <summary>
    /// Get dynamic content query by substituting the actual type as a generic parameter.
    /// </summary>
    /// <typeparam name="TBaseType">The type of the T base type.</typeparam>
    /// <param name="actualType">The actual runtime type.</param>
    /// <returns></returns>
    private static IQueryable<TBaseType> GetDynamicContentQuery<TBaseType>(
      Type actualType,
      DataProviderBase dataProvider)
    {
      MethodInfo methodInfo = typeof (SitefinityQuery).GetMethod("Get", new Type[2]
      {
        typeof (DataProviderBase),
        typeof (string)
      }).MakeGenericMethod(actualType);
      string str = "General";
      object[] parameters = new object[2]
      {
        (object) dataProvider,
        (object) str
      };
      return methodInfo.Invoke((object) null, parameters) as IQueryable<TBaseType>;
    }
  }
}
