// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.FieldSettingsResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor.Permissions;

namespace Telerik.Sitefinity.Web.Services.Contracts.Operations.Pages.PropertyEditor
{
  internal class FieldSettingsResolver
  {
    public static FieldSettings GetPermissionsForLongTextField()
    {
      ISecuredObject securityRoot = LibrariesManager.GetManager().Provider.SecurityRoot;
      return new FieldSettings()
      {
        AllowCreate = securityRoot.IsSecurityActionTypeGranted("Image", SecurityActionTypes.Manage)
      };
    }

    public static FieldSettings GetPermissions(
      TaxonomyManager manager,
      Guid taxonomyId)
    {
      using (new ElevatedModeRegion((IManager) manager))
      {
        taxonomyId = MultisiteTaxonomiesResolver.GetMultisiteTaxonomiesResolver(manager).ResolveSiteTaxonomyId(taxonomyId);
        ISecuredObject taxonomy = manager.GetTaxonomy(taxonomyId) as ISecuredObject;
        return new FieldSettings()
        {
          AllowCreate = taxonomy.IsSecurityActionTypeGranted(SecurityActionTypes.Modify),
          AllowView = taxonomy.IsSecurityActionTypeGranted(SecurityActionTypes.View)
        };
      }
    }

    public static FieldSettings GetPermissions(Guid siteMapRootNodeId)
    {
      FieldSettings permissions = new FieldSettings()
      {
        AllowCreate = false
      };
      PageNode pageNode = PageManager.GetManager().GetPageNodes().FirstOrDefault<PageNode>((Expression<Func<PageNode, bool>>) (n => n.Id == siteMapRootNodeId));
      if (pageNode != null)
        permissions.AllowView = (pageNode.IsGranted("Pages", "View") ? 1 : 0) != 0;
      return permissions;
    }

    public static FieldSettings GetPermissions(Type clrType, string providerName)
    {
      if (providerName == "sf-site-default-provider")
        providerName = RelatedDataHelper.ResolveProvider(clrType.FullName);
      bool flag1 = false;
      bool flag2 = false;
      if (((IEnumerable<string>) new string[3]
      {
        "Image",
        "Video",
        "Document"
      }).Contains<string>(clrType.Name))
      {
        if (providerName != "sf-any-site-provider")
        {
          IManager mappedManager = ManagerBase.GetMappedManager(clrType, providerName);
          flag1 = mappedManager.Provider.SecurityRoot.IsSecurityActionTypeGranted(clrType.Name, SecurityActionTypes.Manage);
          flag2 = mappedManager.Provider.SecurityRoot.IsSecurityActionTypeGranted(clrType.Name, SecurityActionTypes.View);
        }
        else
        {
          IEnumerable<DataProviderBase> allProviders = RelatedDataHelper.ResolveProviders(clrType.FullName);
          FieldSettingsResolver.SetRelatedDataPropertyPermissionByAction(SecurityActionTypes.Manage, allProviders, clrType, ref flag1);
          FieldSettingsResolver.SetRelatedDataPropertyPermissionByAction(SecurityActionTypes.View, allProviders, clrType, ref flag2);
        }
      }
      else
      {
        string providerName1 = providerName;
        if (providerName == "sf-any-site-provider")
          providerName1 = RelatedDataHelper.ResolveProvider(clrType.FullName);
        IManager mappedManager = ManagerBase.GetMappedManager(clrType, providerName1);
        ISecuredObject securityRoot = FieldSettingsResolver.GetSecurityRoot(mappedManager, clrType);
        if (!securityRoot.IsSecurityActionSupported(SecurityActionTypes.Manage))
        {
          flag1 = securityRoot.IsGranted(SecurityActionTypes.Create);
        }
        else
        {
          string permissionSet = FieldSettingsResolver.GetPermissionSet(clrType, securityRoot);
          int num = securityRoot.IsSecurityActionSupported(permissionSet, SecurityActionTypes.Manage) ? 1 : 0;
          bool flag3 = securityRoot.IsSecurityActionTypeGranted(SecurityActionTypes.Manage);
          if (num != 0)
          {
            if (flag3)
              flag1 = true;
          }
          else
            flag1 = securityRoot.IsSecurityActionTypeGranted(permissionSet, SecurityActionTypes.Create);
        }
        flag2 = securityRoot.IsGranted(SecurityActionTypes.View, (string) null, new List<string>()
        {
          "DynamicFields"
        });
        if (flag1)
        {
          Type parentType = FieldSettingsResolver.GetParentType(clrType);
          if (parentType != (Type) null)
          {
            int? totalCount = new int?(1);
            if (typeof (Content).IsAssignableFrom(parentType))
              mappedManager.GetItems(parentType, (string) null, (string) null, 0, 2, ref totalCount);
            else if (typeof (DynamicContent).IsAssignableFrom(parentType))
              totalCount = new int?(mappedManager.GetItems(parentType, (string) null, (string) null, 0, 0).Cast<DynamicContent>().Where<DynamicContent>((Func<DynamicContent, bool>) (x => !x.IsDeleted && x.Status == ContentLifecycleStatus.Master)).Count<DynamicContent>());
            int? nullable = totalCount;
            int num = 0;
            if (nullable.GetValueOrDefault() == num & nullable.HasValue)
              flag1 = false;
          }
        }
      }
      return new FieldSettings()
      {
        AllowCreate = flag1,
        AllowView = flag2
      };
    }

    internal static string GetPermissionSet(Type clrType, ISecuredObject securedObject)
    {
      string permissionSet = clrType.Name;
      if (!((IEnumerable<string>) securedObject.SupportedPermissionSets).Contains<string>(permissionSet))
        permissionSet = string.Empty;
      return permissionSet;
    }

    internal static Type GetParentType(Type itemType)
    {
      SitefinityType type = SystemManager.TypeRegistry.GetType(itemType.FullName);
      return type == null ? (Type) null : TypeResolutionService.ResolveType(type.Parent, false);
    }

    internal static ISecuredObject GetSecurityRoot(IManager manager, Type itemType)
    {
      if (!(manager is DynamicModuleManager))
        return manager.Provider.SecurityRoot;
      IDynamicModuleType dynamicModuleType = FieldSettingsResolver.GetDynamicModuleType(itemType);
      return DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) ModuleBuilderManager.GetManager(), (ISecuredObject) dynamicModuleType);
    }

    private static IDynamicModuleType GetDynamicModuleType(Type itemType) => ModuleBuilderManager.GetModules().SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types)).SingleOrDefault<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.FullTypeName == itemType.FullName));

    private static void SetRelatedDataPropertyPermissionByAction(
      SecurityActionTypes securityActionType,
      IEnumerable<DataProviderBase> allProviders,
      Type clrType,
      ref bool value)
    {
      foreach (DataProviderBase allProvider in allProviders)
      {
        if (allProvider.SecurityRoot.IsSecurityActionTypeGranted(clrType.Name, securityActionType))
        {
          value = true;
          break;
        }
      }
    }
  }
}
