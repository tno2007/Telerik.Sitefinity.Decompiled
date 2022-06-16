// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Common.GenericItemsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel.Activation;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Common
{
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class GenericItemsService : IGenericItemsService
  {
    public CollectionContext<WcfItemBase> GetGenericItems(
      string queryTypeString,
      string wcfItemTypeString,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string providerName,
      bool allProviders,
      bool ignoreAdminUsers)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      HashSet<WcfItemBase> items1 = new HashSet<WcfItemBase>();
      if (wcfItemTypeString == null)
        wcfItemTypeString = queryTypeString;
      Type type = TypeResolutionService.ResolveType(wcfItemTypeString, false, true);
      if (type == (Type) null)
        type = WcfHelper.ResolveEncodedTypeName(wcfItemTypeString, true, true);
      Type itemType = TypeResolutionService.ResolveType(queryTypeString, false, true);
      if (itemType == (Type) null)
        type = WcfHelper.ResolveEncodedTypeName(queryTypeString, true, true);
      IManager mgr = ManagerBase.GetMappedManager(itemType, providerName);
      IProviderResolver providerResolver;
      if ((IManager) (providerResolver = (IProviderResolver) mgr) != null && !providerResolver.GetContextProviders().Any<DataProviderBase>((Func<DataProviderBase, bool>) (p => p.Name == mgr.Provider.Name)))
        mgr = ManagerBase.GetMappedManager(itemType, (string) null);
      if (!((IEnumerable<string>) mgr.GetSecurityRoot().GetActivePermissionActionsForCurrentUser()).Any<string>())
        ProtectedRoute.HandleItemViewNotAllowed(SystemManager.CurrentHttpContext, "Access denied");
      ConstructorInfo constructor = type.GetConstructor(new Type[1]
      {
        itemType
      });
      int? totalCount1 = new int?(0);
      if (!allProviders)
      {
        IEnumerable items2;
        if (ignoreAdminUsers)
        {
          string adminUsersFilter = this.GetNonAdminUsersFilter();
          if (!string.IsNullOrEmpty(filter))
          {
            if (!string.IsNullOrEmpty(adminUsersFilter))
              filter = "(" + filter + ") and (" + adminUsersFilter + ")";
          }
          else if (!string.IsNullOrEmpty(adminUsersFilter))
            filter = adminUsersFilter;
          items2 = mgr.GetItems(itemType, filter, sortExpression, skip, take, ref totalCount1);
        }
        else
          items2 = mgr.GetItems(itemType, filter, sortExpression, skip, take, ref totalCount1);
        foreach (object obj in items2)
        {
          WcfItemBase wcfItemBase;
          if (constructor != (ConstructorInfo) null)
          {
            wcfItemBase = (WcfItemBase) constructor.Invoke(new object[1]
            {
              obj
            });
          }
          else
          {
            if (!(type == itemType))
              throw new ArgumentException(string.Format("{0} should be of the same type of {1}, or have a constructor which accepts an item of type {1} as an argument", (object) queryTypeString, (object) wcfItemTypeString));
            wcfItemBase = (WcfItemBase) obj;
          }
          items1.Add(wcfItemBase);
        }
      }
      else
      {
        bool flag = false;
        int? totalCount2 = new int?(0);
        foreach (DataProviderBase contextProvider in mgr.GetContextProviders())
        {
          if (!(contextProvider.ProviderGroup == "System"))
          {
            IManager mappedManager = ManagerBase.GetMappedManager(itemType, contextProvider.Name);
            skip -= totalCount1.Value;
            if (skip < 0)
              skip = 0;
            IEnumerable items3;
            if (ignoreAdminUsers)
            {
              string adminUsersFilter = this.GetNonAdminUsersFilter();
              if (!string.IsNullOrEmpty(filter))
              {
                if (!string.IsNullOrEmpty(adminUsersFilter))
                  filter = "(" + filter + ") and (" + adminUsersFilter + ")";
              }
              else if (!string.IsNullOrEmpty(adminUsersFilter))
                filter = adminUsersFilter;
              items3 = mappedManager.GetItems(itemType, filter, sortExpression, skip, take, ref totalCount2);
            }
            else
              items3 = mappedManager.GetItems(itemType, filter, sortExpression, skip, take, ref totalCount2);
            int? nullable = totalCount1;
            int num = totalCount2.Value;
            totalCount1 = nullable.HasValue ? new int?(nullable.GetValueOrDefault() + num) : new int?();
            if (!flag)
            {
              foreach (object obj in items3)
              {
                WcfItemBase wcfItemBase;
                if (constructor != (ConstructorInfo) null)
                {
                  wcfItemBase = (WcfItemBase) constructor.Invoke(new object[1]
                  {
                    obj
                  });
                }
                else
                {
                  if (!(type == itemType))
                    throw new ArgumentException(string.Format("{0} should be of the same type of {1}, or have a constructor which accepts an item of type {1} as an argument", (object) queryTypeString, (object) wcfItemTypeString));
                  wcfItemBase = (WcfItemBase) obj;
                }
                items1.Add(wcfItemBase);
                if (items1.Count == take)
                {
                  flag = true;
                  break;
                }
              }
            }
          }
        }
      }
      ServiceUtility.DisableCache();
      return new CollectionContext<WcfItemBase>((IEnumerable<WcfItemBase>) items1)
      {
        TotalCount = totalCount1.Value
      };
    }

    private string GetNonAdminUsersFilter()
    {
      RoleManager manager = RoleManager.GetManager(SecurityManager.AdminRole.Provider);
      Guid adminRoleId = SecurityManager.AdminRole.Id;
      Guid[] array = manager.Provider.GetUserLinks().Where<UserLink>((Expression<Func<UserLink, bool>>) (l => l.Role.Id == adminRoleId)).Select<UserLink, Guid>((Expression<Func<UserLink, Guid>>) (l => l.UserId)).ToArray<Guid>();
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 0; index < array.Length; ++index)
      {
        stringBuilder.Append("Id != (");
        stringBuilder.Append((object) array[index]);
        stringBuilder.Append(")");
        if (index + 1 != array.Length)
          stringBuilder.Append(" and ");
      }
      return stringBuilder.ToString();
    }
  }
}
