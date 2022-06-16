// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.Permissions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Multisite;
using Telerik.Sitefinity.Multisite.Model;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Taxonomies;
using Telerik.Sitefinity.Utilities;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI.Modules.Selectors;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>The Permissions WCF service</summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  [AllowDynamicFields]
  public class Permissions : IPermissions
  {
    /// <summary>
    /// Permission set names which are hidden from Global permissions. Currently that is the controls permissions because there is a bug that they are not inherited.
    /// </summary>
    private readonly string[] HiddenFromUIPermissionsSetNames = new string[1]
    {
      "Controls"
    };

    /// <summary>Break or restore inheritance</summary>
    /// <param name="securedObjectIdString">Secured object pageId</param>
    /// <param name="securedObjectTypeName">Wcf-encoded secured object type name</param>
    /// <param name="managerTypeName">Wcf-encoded secured object type name</param>
    /// <param name="providerName">Provider name</param>
    /// <param name="BreakInheritance">true to break inheritance, false to restore inheritance</param>
    /// <param name="loseCustomChanges">true to lose added permissions when the inheritance is restored, false to keep them</param>
    /// <returns>Active permissions after the inheritance change</returns>
    public PermissionSetCollectionContext ChangeInheritance(
      string securedObjectIdString,
      string securedObjectTypeName,
      string managerTypeName,
      string providerName,
      string breakInheritanceString,
      string loseCustomChanges,
      string transactionName,
      string dynamicDataProviderName = null)
    {
      bool result1 = false;
      bool result2 = true;
      if (!bool.TryParse(breakInheritanceString, out result1))
        result1 = false;
      if (!bool.TryParse(loseCustomChanges, out result2))
        result2 = true;
      return result1 ? this.BreakInheritance(securedObjectIdString, WcfHelper.DecodeWcfString(securedObjectTypeName), WcfHelper.DecodeWcfString(managerTypeName), providerName, transactionName, dynamicDataProviderName) : this.RestoreInheritance(securedObjectIdString, WcfHelper.DecodeWcfString(securedObjectTypeName), WcfHelper.DecodeWcfString(managerTypeName), providerName, result2, transactionName, dynamicDataProviderName);
    }

    /// <summary>
    /// Saves permissions for given principals for a specific action
    /// </summary>
    /// <param name="principals">List of <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermission" /> permission objects, per user or role</param>
    /// <param name="permissionsSetName">The name of the permission set</param>
    /// <param name="dataProviderName">The name of the data provider for managing the persistent permissions</param>
    /// <param name="permissionObjectRootID">Guid ID of the object for which the permissions are set</param>
    /// <param name="actionName">Name of the action to set permissions for</param>
    /// <param name="managerClassName">Name of the Class for managing the persistent permissions</param>
    /// <param name="securedObjectID">ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).</param>
    /// <param name="securedObjectType">The assembly-qualified name of the type of the secured object, including the assembly name.</param>
    /// <param name="dynamicDataProviderName">Name of the dynamic data provider.</param>
    public void SavePermissionsForSpecificAction(
      List<WcfPermission> principals,
      string permissionsSetName,
      string dataProviderName,
      string permissionObjectRootID,
      string actionName,
      string managerClassName,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName)
    {
      managerClassName = this.DecodeWcfString(managerClassName);
      WcfHelper.ResolveEncodedTypeName(securedObjectType);
      Guid securedObjectId = string.IsNullOrEmpty(securedObjectID) || securedObjectID == Guid.Empty.ToString() ? this.CreateGuidFromString(permissionObjectRootID) : this.CreateGuidFromString(securedObjectID);
      IManager manager = this.GetManager(managerClassName, dataProviderName);
      string inheritanceTransactionName = this.GetPermissionsInheritanceTransactionName();
      IManager managerInTransaction = this.GetMappedManagerInTransaction(manager, dataProviderName, inheritanceTransactionName);
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(managerInTransaction, this.DecodeWcfString(securedObjectType), securedObjectId, dynamicDataProviderName);
      if (securedObject is IDataItem)
        (securedObject as IDataItem).LastModified = DateTime.Now;
      this.DemandPermission(securedObject, SecurityActionTypes.ChangePermissions, permissionsSetName);
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> activePermissions = this.GetActivePermissions(securedObject);
      this.SetGrantDenyForExistingPermissionsAndCreateNonExistingOnes(managerInTransaction, (IList<WcfPermission>) principals, securedObject, permissionsSetName, actionName);
      this.ResetGrantDeny(activePermissions, principals, permissionsSetName, securedObject.Id, actionName);
      this.SecuredContentUpdated(securedObject, managerInTransaction);
      if (securedObject is ControlData controlData)
        ++controlData.Version;
      this.SaveAccessModuleBackendPermissions(managerInTransaction, (IList<WcfPermission>) principals, securedObject);
      TransactionManager.CommitTransaction(inheritanceTransactionName);
      if (securedObject is SecurityRoot securityRoot && securityRoot.Key == "ApplicationSecurityRoot")
        CacheDependency.Notify((IList<CacheDependencyKey>) new List<CacheDependencyKey>()
        {
          new CacheDependencyKey()
          {
            Key = AppPermission.AppPermissionsSubscriptionId.ToString(),
            Type = typeof (SecurityRoot)
          }
        });
      this.NotifyOutputCacheDependencies(manager, securedObject);
    }

    /// <summary>
    /// Saves permissions for given actions for a specific principal
    /// </summary>
    /// <param name="principals">List of <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermission" /> permission objects, per user or role</param>
    /// <param name="permissionsSetName">The name of the permission set</param>
    /// <param name="dataProviderName">The name of the data provider for managing the persistent permissions</param>
    /// <param name="permissionObjectRootID">Guid ID of the object for which the permissions are set</param>
    /// <param name="managerClassName">Name of the Class for managing the persistent permissions</param>
    /// <param name="principalID">ID (Guid to string) of the user/role to use</param>
    /// <param name="securedObjectID">ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).</param>
    /// <param name="securedObjectType">The assembly-qualified name of the type of the secured object, including the assembly name.</param>
    /// <param name="dynamicDataProviderName">Name of the dynamic data provider.</param>
    public void SavePermissionActionsForSpecificPrincipal(
      List<WcfPermission> principals,
      string permissionsSetName,
      string dataProviderName,
      string permissionObjectRootID,
      string managerClassName,
      Guid principalID,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName)
    {
      managerClassName = this.DecodeWcfString(managerClassName);
      Guid securedObjectId = string.IsNullOrEmpty(securedObjectID) || Telerik.Sitefinity.Utilities.Utility.StringToGuid(securedObjectID) == Guid.Empty ? new Guid(permissionObjectRootID) : new Guid(securedObjectID);
      IManager manager = this.GetManager(managerClassName, dataProviderName);
      string inheritanceTransactionName = this.GetPermissionsInheritanceTransactionName();
      IManager managerInTransaction = this.GetMappedManagerInTransaction(manager, dataProviderName, inheritanceTransactionName);
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(managerInTransaction, securedObjectType, securedObjectId, dynamicDataProviderName);
      this.DemandPermission(securedObject, SecurityActionTypes.ChangePermissions);
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> permissions = (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) securedObject.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == principalID));
      foreach (Telerik.Sitefinity.Security.Model.Permission permission in permissions)
      {
        if (permissionsSetName != string.Empty && permission.SetName == permissionsSetName)
        {
          permission.Grant = 0;
          permission.Deny = 0;
        }
      }
      List<string> list1 = principals.AsQueryable<WcfPermission>().Where<WcfPermission>((Expression<Func<WcfPermission, bool>>) (pr => pr.IsAllowed == true && pr.IsDenied == false)).Select<WcfPermission, string>((Expression<Func<WcfPermission, string>>) (pr => pr.ActionName)).ToList<string>();
      List<string> list2 = principals.AsQueryable<WcfPermission>().Where<WcfPermission>((Expression<Func<WcfPermission, bool>>) (pr => pr.IsAllowed == false && pr.IsDenied == true)).Select<WcfPermission, string>((Expression<Func<WcfPermission, string>>) (pr => pr.ActionName)).ToList<string>();
      if (!this.SetActionPermissionsForPrincipal(permissions, principalID, permissionsSetName, list1, list2))
      {
        List<string> stringList = new List<string>();
        string str = permissionsSetName + "_" + securedObject.Id.ToString() + "_" + principalID.ToString();
        if (!stringList.Contains(str))
        {
          this.CreatePermissionForSecuredObject(managerInTransaction, securedObject, principalID, permissionsSetName, (IList<string>) list1, (IList<string>) list2);
          stringList.Add(str);
        }
      }
      this.SecuredContentUpdated(securedObject, managerInTransaction);
      TransactionManager.CommitTransaction(inheritanceTransactionName);
    }

    /// <summary>
    /// Gets a collection of data related to all modules in the system, and their corresponding providers. May be fileterd by aguments.
    /// </summary>
    /// <param name="dataProviderName">Optional- a name of a specific provider</param>
    /// <param name="managerClassName">Optional - Type name of a specific manager class</param>
    /// <param name="securedObjectID">Optional - An Id of a specific secured object</param>
    /// <returns>A collection of data related to all modules in the system, and their corresponding providers</returns>
    public CollectionContext<WcfPermissionModule> GetModules(
      string dataProviderName,
      string managerClassName,
      string securedObjectID)
    {
      managerClassName = this.DecodeWcfString(managerClassName);
      List<WcfPermissionModule> items = new List<WcfPermissionModule>();
      IManager predefinedManager = (IManager) null;
      Type predefinedManagerType = (Type) null;
      if (!string.IsNullOrEmpty(managerClassName) && !string.IsNullOrEmpty(dataProviderName))
        predefinedManager = this.GetManager(managerClassName, dataProviderName);
      if (predefinedManager == null && !string.IsNullOrEmpty(managerClassName) && string.IsNullOrEmpty(dataProviderName))
        predefinedManagerType = Type.GetType(managerClassName);
      Guid guid1 = this.StringToGuid(securedObjectID);
      Guid guid2;
      if (this.GetManager(this.GetApplicationRootManagerType(), this.GetApplicationRootProviderName()) != null)
      {
        if (!string.IsNullOrEmpty(securedObjectID))
        {
          string str1 = securedObjectID;
          guid2 = Guid.Empty;
          string str2 = guid2.ToString();
          if (str1 != str2 && this.GetApplicationRootId() == guid1)
            goto label_11;
        }
        if ((predefinedManager == null || !(predefinedManager.GetType() == this.GetApplicationRootManagerType()) || !(this.GetApplicationRootProviderName() == dataProviderName)) && (!(predefinedManagerType != (Type) null) || !(this.GetApplicationRootManagerType() == predefinedManagerType)))
        {
          if (!string.IsNullOrEmpty(securedObjectID))
          {
            string str3 = securedObjectID;
            guid2 = Guid.Empty;
            string str4 = guid2.ToString();
            if (!(str3 == str4))
              goto label_12;
          }
          if (predefinedManager != null || !(predefinedManagerType == (Type) null))
            goto label_12;
        }
label_11:
        items.Add(this.GetApplicationModule());
      }
label_12:
      TaxonomyManager manager1 = TaxonomyManager.GetManager();
      List<WcfPermissionProvider> permissionProviderList1 = new List<WcfPermissionProvider>();
      foreach (DataProviderBase staticProvider in (Collection<TaxonomyDataProvider>) manager1.StaticProviders)
      {
        List<WcfPermissionProvider> permissionProviderList2 = permissionProviderList1;
        WcfPermissionProvider permissionProvider = new WcfPermissionProvider();
        permissionProvider.ProviderName = staticProvider.Name;
        guid2 = staticProvider.Id;
        permissionProvider.ProviderId = guid2.ToString();
        guid2 = staticProvider.SecurityRoot.Id;
        permissionProvider.SecuredObjectId = guid2.ToString();
        permissionProvider.ProviderTitle = staticProvider.Title;
        permissionProvider.ManagerType = manager1.GetType().FullName;
        permissionProvider.PermissionSetName = "";
        permissionProvider.SecuredObjectType = staticProvider.SecurityRoot.GetType().AssemblyQualifiedName;
        permissionProviderList2.Add(permissionProvider);
      }
      items.Add(new WcfPermissionModule()
      {
        ModuleTitle = Res.Get<TaxonomyResources>().ModuleTitle,
        Providers = permissionProviderList1.ToArray()
      });
      items.AddRange((IEnumerable<WcfPermissionModule>) this.RetrieveModules((IEnumerable<IModule>) SystemManager.ApplicationModules.Values, guid1, predefinedManager, predefinedManagerType, dataProviderName));
      if (SystemManager.IsModuleEnabled("ModuleBuilder"))
      {
        DynamicModuleManager manager2 = DynamicModuleManager.GetManager();
        foreach (IDynamicModule dynamicModule in ModuleBuilderManager.GetModules().Active())
        {
          IEnumerable<DataProviderBase> contextProviders = manager2.GetContextProviders(dynamicModule.Name);
          WcfPermissionModule permissionModule = new WcfPermissionModule()
          {
            ManagerTypeName = typeof (ModuleBuilderManager).FullName,
            ModuleTitle = dynamicModule.Title
          };
          List<WcfPermissionProvider> permissionProviderList3 = new List<WcfPermissionProvider>();
          string name = ModuleBuilderManager.GetManager().Provider.Name;
          foreach (DataProviderBase dataProviderBase in contextProviders)
            permissionProviderList3.Add(new WcfPermissionProvider()
            {
              ProviderTitle = dataProviderBase.Title,
              DynamicDataProviderName = dataProviderBase.Name,
              ProviderId = dataProviderBase.Id.ToString(),
              ProviderName = name,
              SecuredObjectId = dynamicModule.Id.ToString(),
              ManagerType = typeof (ModuleBuilderManager).FullName,
              PermissionSetName = "General",
              SecuredObjectType = typeof (DynamicModule).AssemblyQualifiedName
            });
          permissionModule.Providers = permissionProviderList3.ToArray();
          items.Add(permissionModule);
        }
      }
      items.AddRange((IEnumerable<WcfPermissionModule>) this.RetrieveModules((IEnumerable<IModule>) SystemManager.ServiceModules.Values, guid1, predefinedManager, predefinedManagerType, dataProviderName));
      this.DisableServiceCache();
      return new CollectionContext<WcfPermissionModule>((IEnumerable<WcfPermissionModule>) items);
    }

    /// <summary>Gets providers info for a specific module.</summary>
    /// <param name="moduleName">Name of the related module.</param>
    /// <returns>
    /// A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider">ModuleProvider</see> objects
    /// </returns>
    public CollectionContext<ModuleProvider> GetModuleProviders(
      string moduleName)
    {
      List<ModuleProvider> items = new List<ModuleProvider>((IEnumerable<ModuleProvider>) new ModuleProviderAssociation()
      {
        ModuleName = moduleName
      }.ModuleProviders);
      this.DisableServiceCache();
      return new CollectionContext<ModuleProvider>((IEnumerable<ModuleProvider>) items);
    }

    /// <summary>
    /// Gets system providers info by a specific manager type.
    /// </summary>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <returns>
    /// A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider"> system ModuleProvider</see> objects
    /// </returns>
    public CollectionContext<ModuleProvider> GetManagerSystemProviders(
      string managerClassName)
    {
      IEnumerable<DataProviderBase> providers = this.GetManager(managerClassName).StaticProviders.Where<DataProviderBase>((Func<DataProviderBase, bool>) (x => x.IsSystemProvider()));
      this.DisableServiceCache();
      return new CollectionContext<ModuleProvider>(this.ExtractModuleProviders(providers, managerClassName));
    }

    /// <summary>Gets providers info by a specific manager type.</summary>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <returns>
    /// A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider">ModuleProvider</see> objects
    /// </returns>
    public CollectionContext<ModuleProvider> GetManagerProviders(
      string managerClassName)
    {
      IEnumerable<DataProviderBase> contextProviders = this.GetManager(managerClassName).GetContextProviders();
      this.DisableServiceCache();
      return new CollectionContext<ModuleProvider>(this.ExtractModuleProviders(contextProviders, managerClassName));
    }

    /// <summary>
    /// Gets all permissions for a secured object, grouped by permission sets and actions
    /// </summary>
    /// <param name="commaDelimitedPermissionsSetNames">Names of zero or more permission set to get. If more than one is specified, delimited by comma</param>
    /// <param name="dataProviderName">Name of the data provider</param>
    /// <param name="managerClassName">Name of the manager class</param>
    /// <param name="securedObjectID">Id of the secured object</param>
    /// <param name="securedObjectType">Name of the type of the secured object</param>
    /// <param name="dynamicDataProviderName">Name of the dynamic data provider.</param>
    /// <returns>A collection of WcfPermissionSetPermission items</returns>
    public PermissionSetCollectionContext GetPermissionsForSecuredObject(
      string commaDelimitedPermissionsSetNames,
      string dataProviderName,
      string managerClassName,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName)
    {
      List<WcfPermissionSetPermission> permissions = new List<WcfPermissionSetPermission>();
      managerClassName = this.DecodeWcfString(managerClassName);
      Type type = WcfHelper.ResolveEncodedTypeName(securedObjectType);
      IManager managerInstance = !string.IsNullOrEmpty(managerClassName) || !(type != (Type) null) ? this.GetManager(managerClassName, dataProviderName) : ManagerBase.GetMappedManager(type, dataProviderName);
      Guid guid = this.StringToGuid(securedObjectID);
      bool flag = false;
      if (string.IsNullOrEmpty(securedObjectID) || guid == Guid.Empty)
      {
        if (managerInstance.GetType() == this.GetApplicationRootManagerType() && dataProviderName == this.GetApplicationRootProviderName())
        {
          guid = this.GetApplicationRootId();
          flag = true;
        }
        else
          guid = managerInstance.Provider.SecurityRoot.Id;
      }
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(managerInstance, securedObjectType, guid, dynamicDataProviderName);
      List<string> first = new List<string>();
      if (!string.IsNullOrEmpty(commaDelimitedPermissionsSetNames))
      {
        string str1 = commaDelimitedPermissionsSetNames;
        char[] chArray = new char[1]{ ',' };
        foreach (string str2 in str1.Split(chArray))
        {
          string str3 = str2.Trim();
          if (((IEnumerable<string>) securedObject.SupportedPermissionSets).Contains<string>(str3) && !first.Contains(str3))
            first.Add(str3);
        }
      }
      else if (flag)
        first.Add("Backend");
      else
        first.AddRange((IEnumerable<string>) securedObject.SupportedPermissionSets);
      if (type != (Type) null && typeof (SecurityRoot).IsAssignableFrom(type))
        first = first.Except<string>((IEnumerable<string>) this.HiddenFromUIPermissionsSetNames).ToList<string>();
      List<string> stringList = new List<string>();
      foreach (string str in first)
      {
        string permSet = str;
        ConfigElementDictionary<string, SecurityAction> permissionSetActions = this.GetPermissionSetActions(permSet);
        List<Telerik.Sitefinity.Security.Model.Permission> list = securedObject.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.SetName == permSet)).ToList<Telerik.Sitefinity.Security.Model.Permission>();
        foreach (SecurityAction action in (ConfigElementCollection) permissionSetActions)
        {
          if ((action.Type & SecurityActionTypes.ChangePermissions) > SecurityActionTypes.None && securedObject.ArePermissionChangesAllowedByLicense() && securedObject.IsGranted(permSet, action.Value) && !stringList.Contains(permSet))
            stringList.Add(permSet);
          bool showAction;
          string title = action.GetTitle(securedObject, out showAction);
          if (showAction)
          {
            string actionTitle = title;
            string name = action.Name;
            string description = action.Description;
            List<WcfPermission> wcfPermissionList = new List<WcfPermission>();
            foreach (Telerik.Sitefinity.Security.Model.Permission permission in list)
            {
              string principalName = this.GetPrincipalName(permission.PrincipalId);
              string principalTitle = string.Empty;
              if (this.IsPrincipalUser(permission.PrincipalId))
                principalTitle = UserProfilesHelper.GetUserDisplayName(permission.PrincipalId);
              if (string.IsNullOrEmpty(principalTitle))
                principalTitle = principalName;
              if (!string.IsNullOrEmpty(principalName))
              {
                if (this.IsGranted(permission, action.Name))
                  wcfPermissionList.Add(new WcfPermission(this.IsPrincipalUser(permission.PrincipalId) ? WcfPrincipalType.User.ToString() : WcfPrincipalType.Role.ToString(), permission.PrincipalId.ToString(), principalName, principalTitle, dataProviderName, true, false, name, string.Empty, string.Empty, permSet, permission.ObjectId.ToString()));
                if (this.IsDenied(permission, action.Name))
                  wcfPermissionList.Add(new WcfPermission(this.IsPrincipalUser(permission.PrincipalId) ? WcfPrincipalType.User.ToString() : WcfPrincipalType.Role.ToString(), permission.PrincipalId.ToString(), principalName, principalTitle, dataProviderName, false, true, name, string.Empty, string.Empty, permSet, permission.ObjectId.ToString()));
              }
            }
            wcfPermissionList.Sort();
            permissions.Add(new WcfPermissionSetPermission(permSet, name, actionTitle, description, wcfPermissionList.ToArray(), managerInstance.Provider.Id.ToString()));
          }
        }
      }
      this.DisableServiceCache();
      return new PermissionSetCollectionContext((IEnumerable<WcfPermissionSetPermission>) permissions, securedObject, false, stringList.ToArray(), guid);
    }

    /// <summary>
    /// Gets the collection of permissions per user/role in JSON format.
    /// </summary>
    /// <param name="permissionsSetName">The name of the permission set to retrieve</param>
    /// <param name="dataProviderName">The name of the provider to use</param>
    /// <param name="managerClassName">The full name of the manager class to use</param>
    /// <param name="principalID">ID (Guid to string) of the user/role to use</param>
    /// <param name="securedObjectID">ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).</param>
    /// <param name="securedObjectType">Name of the type of the secured object</param>
    /// <returns>A coillection of <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermissionSetPermission" /> permission set objects</returns>
    public PermissionSetCollectionContext GetPermissionSetsForSpecificPrincipal(
      string permissionsSetName,
      string dataProviderName,
      string managerClassName,
      string principalID,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName)
    {
      List<WcfPermissionSetPermission> permissions = new List<WcfPermissionSetPermission>();
      managerClassName = this.DecodeWcfString(managerClassName);
      IManager manager1 = this.GetManager(managerClassName, dataProviderName);
      Guid principalIDGuid = new Guid(principalID);
      Guid guid = !(securedObjectID != Guid.Empty.ToString()) ? manager1.GetSecurityRoot().Id : this.CreateGuidFromString(securedObjectID);
      bool flag = this.GetApplicationRootManagerType() == manager1.GetType() && this.GetApplicationRootProviderName() == dataProviderName && permissionsSetName == "Backend";
      if (flag)
        guid = this.GetApplicationRootId();
      IList<string> first = (IList<string>) new List<string>();
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(manager1, securedObjectType, guid, dynamicDataProviderName);
      bool isAdmin = SecurityManager.IsPrincipalUser(principalIDGuid) && SecurityManager.IsUserUnrestricted(principalIDGuid) || SecurityManager.IsPrincipalRole(principalIDGuid) && SecurityManager.IsAdministrativeRole(principalIDGuid);
      if (flag)
        first = (IList<string>) new List<string>()
        {
          "Backend"
        };
      else if (!string.IsNullOrEmpty(permissionsSetName))
        first.Add(permissionsSetName);
      else
        first = (IList<string>) securedObject.SupportedPermissionSets;
      if (!string.IsNullOrEmpty(securedObjectType))
      {
        Type c = WcfHelper.ResolveEncodedTypeName(securedObjectType);
        if (c != (Type) null && typeof (SecurityRoot).IsAssignableFrom(c))
          first = (IList<string>) first.Except<string>((IEnumerable<string>) this.HiddenFromUIPermissionsSetNames).ToList<string>();
      }
      List<string> stringList = new List<string>();
      foreach (string permissionSet in (IEnumerable<string>) first)
      {
        string curPermSetName = string.Empty;
        if (string.IsNullOrEmpty(permissionsSetName) || permissionSet == permissionsSetName)
          curPermSetName = permissionSet;
        else if (flag)
          curPermSetName = "Backend";
        if (!string.IsNullOrEmpty(curPermSetName))
        {
          WcfPermissionSetPermission permissionSetPermission;
          if (manager1 is IDynamicModuleSecurityManager && !dynamicDataProviderName.IsNullOrEmpty())
          {
            DynamicModuleManager manager2 = DynamicModuleManager.GetManager(dynamicDataProviderName);
            permissionSetPermission = new WcfPermissionSetPermission(curPermSetName, "", "", "", (WcfPermission[]) null, manager2.Provider.Id.ToString());
          }
          else
            permissionSetPermission = new WcfPermissionSetPermission(curPermSetName, "", "", "", (WcfPermission[]) null, manager1.Provider.Id.ToString());
          List<WcfPermission> wcfPermissionList = new List<WcfPermission>();
          Telerik.Sitefinity.Security.Model.Permission permission = securedObject.GetActivePermissions().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == principalIDGuid && p.SetName == curPermSetName)).FirstOrDefault<Telerik.Sitefinity.Security.Model.Permission>();
          foreach (SecurityAction permissionSetAction in (ConfigElementCollection) this.GetPermissionSetActions(curPermSetName))
          {
            if ((permissionSetAction.Type & SecurityActionTypes.ChangePermissions) > SecurityActionTypes.None && securedObject.ArePermissionChangesAllowedByLicense() && securedObject.IsGranted(permissionSet, permissionSetAction.Value) && !stringList.Contains(permissionSet))
              stringList.Add(permissionSet);
            bool showAction;
            string title = permissionSetAction.GetTitle(securedObject, out showAction);
            if (showAction)
            {
              WcfPermission wcfPermission = new WcfPermission();
              if (isAdmin)
              {
                wcfPermission.IsAllowed = true;
                wcfPermission.IsDenied = false;
              }
              else if (permission != null)
              {
                wcfPermission.IsAllowed = this.IsGranted(permission, permissionSetAction.Name);
                wcfPermission.IsDenied = this.IsDenied(permission, permissionSetAction.Name);
              }
              else
              {
                wcfPermission.IsAllowed = false;
                wcfPermission.IsDenied = false;
              }
              wcfPermission.ActionName = permissionSetAction.Name;
              wcfPermission.ActionTitle = title;
              wcfPermissionList.Add(wcfPermission);
            }
          }
          permissionSetPermission.PermissionSetName = curPermSetName;
          permissionSetPermission.ActionName = "";
          if (wcfPermissionList.Count > 0)
          {
            permissionSetPermission.Permissions = wcfPermissionList.ToArray();
            permissions.Add(permissionSetPermission);
          }
        }
      }
      this.DisableServiceCache();
      return new PermissionSetCollectionContext((IEnumerable<WcfPermissionSetPermission>) permissions, securedObject, isAdmin, stringList.ToArray(), guid);
    }

    /// <summary>Gets the provider usage.</summary>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <param name="dynamicModuleTitle">Dynamic module title</param>
    /// <param name="securedObjectId">The secured object identifier.</param>
    /// <returns></returns>
    public ProviderUsageCollectionContext GetProviderUsage(
      string dataProviderName,
      string managerClassName,
      string dynamicModuleTitle,
      string securedObjectId,
      string securedObjectTypeString)
    {
      List<WcfProviderSite> wcfProviderSiteList = new List<WcfProviderSite>();
      MultisiteManager manager = MultisiteManager.GetManager();
      string dataSourceName;
      if (TypeResolutionService.ResolveType(WcfHelper.DecodeWcfString(securedObjectTypeString), false, true) == typeof (DynamicModule) && !dynamicModuleTitle.IsNullOrEmpty())
      {
        dataSourceName = dynamicModuleTitle;
      }
      else
      {
        managerClassName = this.DecodeWcfString(managerClassName);
        dataSourceName = this.GetManager(managerClassName, dataProviderName).GetType().FullName;
      }
      ParameterExpression parameterExpression;
      // ISSUE: method reference
      List<WcfProviderSite> list = manager.GetSiteDataSourceLinks().Where<SiteDataSourceLink>((Expression<Func<SiteDataSourceLink, bool>>) (l => l.DataSource.Name == dataSourceName && l.DataSource.Provider == dataProviderName)).Select<SiteDataSourceLink, WcfProviderSite>(Expression.Lambda<Func<SiteDataSourceLink, WcfProviderSite>>((Expression) Expression.MemberInit(Expression.New(typeof (WcfProviderSite)), (MemberBinding) Expression.Bind((MethodInfo) MethodBase.GetMethodFromHandle(__methodref (WcfProviderSite.set_SiteName)), )))); //unable to render the statement
      this.DisableServiceCache();
      string securedObjectId1 = securedObjectId;
      return new ProviderUsageCollectionContext((IEnumerable<WcfProviderSite>) list, securedObjectId1);
    }

    private IEnumerable<ModuleProvider> ExtractModuleProviders(
      IEnumerable<DataProviderBase> providers,
      string managerClassName)
    {
      List<ModuleProvider> moduleProviders = new List<ModuleProvider>();
      foreach (DataProviderBase provider in providers)
        moduleProviders.Add(new ModuleProvider(string.Empty, managerClassName, provider.Title, provider.Name, provider.SecurityRoot.Id.ToString(), false, provider.SecurityRoot.GetType().ToString()));
      return (IEnumerable<ModuleProvider>) moduleProviders;
    }

    /// <summary>
    /// Restores the permissions inheritance of a specific secured object.
    /// </summary>
    /// <param name="securedObjectIdString">The secured object id.</param>
    /// <param name="securedObjectTypeName">Name of the secured object type.</param>
    /// <param name="managerTypeName">Name of the manager type.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="loseCustomChanges">if set to <c>true</c>, all the object's permissions (i.e. not the inherited ones) are reset. Otherwise untouched.</param>
    /// <returns>The current object's permissions.</returns>
    protected internal virtual PermissionSetCollectionContext RestoreInheritance(
      string securedObjectIdString,
      string securedObjectTypeName,
      string managerTypeName,
      string providerName,
      bool loseCustomChanges,
      string transactionName = null,
      string dynamicDataProviderName = null)
    {
      string transactionName1 = string.IsNullOrWhiteSpace(transactionName) ? this.GetPermissionsInheritanceTransactionName() : transactionName;
      Guid guidFromString = this.CreateGuidFromString(securedObjectIdString);
      IManager managerInTransaction = this.GetMappedManagerInTransaction(this.GetManager(managerTypeName, providerName), providerName, transactionName1);
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(managerInTransaction, securedObjectTypeName, guidFromString, dynamicDataProviderName);
      if (!managerInTransaction.Provider.SuppressSecurityChecks)
        securedObject.Demand(SecurityActionTypes.ChangePermissions);
      managerInTransaction.RestorePermissionsInheritance(securedObject);
      this.SecuredContentUpdated(securedObject, managerInTransaction);
      if (string.IsNullOrWhiteSpace(transactionName))
        TransactionManager.CommitTransaction(transactionName1);
      PermissionSetCollectionContext collectionContext = this.GetPermissionSetCollectionContext(managerInTransaction, securedObject, true, guidFromString);
      this.DisableServiceCache();
      return collectionContext;
    }

    /// <summary>
    /// Breaks the permissions inheritance of a specific secured object.
    /// </summary>
    /// <param name="securedObjectIdString">The secured object id.</param>
    /// <param name="securedObjectTypeName">Name of the secured object type.</param>
    /// <param name="managerTypeName">Name of the manager type.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <returns>The current object's permissions.</returns>
    protected internal virtual PermissionSetCollectionContext BreakInheritance(
      string securedObjectIdString,
      string securedObjectTypeName,
      string managerTypeName,
      string providerName,
      string transactionName = null,
      string dynamicDataProviderName = null)
    {
      string transactionName1 = string.IsNullOrWhiteSpace(transactionName) ? this.GetPermissionsInheritanceTransactionName() : transactionName;
      Guid guidFromString = this.CreateGuidFromString(securedObjectIdString);
      IManager managerInTransaction = this.GetMappedManagerInTransaction(this.GetManager(managerTypeName, providerName), providerName, transactionName1);
      ISecuredObject securedObject = SecurityUtility.GetSecuredObject(managerInTransaction, securedObjectTypeName, guidFromString, dynamicDataProviderName);
      if (!managerInTransaction.Provider.SuppressSecurityChecks)
        securedObject.Demand(SecurityActionTypes.ChangePermissions);
      managerInTransaction.BreakPermiossionsInheritance(securedObject);
      this.SecuredContentUpdated(securedObject, managerInTransaction);
      if (string.IsNullOrWhiteSpace(transactionName))
        TransactionManager.CommitTransaction(transactionName1);
      PermissionSetCollectionContext collectionContext = this.GetPermissionSetCollectionContext(managerInTransaction, securedObject, true, guidFromString);
      this.DisableServiceCache();
      return collectionContext;
    }

    /// <summary>
    /// Gets the permissions of a secured object, in the form of a PermissionSetCollectionContext.
    /// </summary>
    /// <param name="manager">An instance of the associated manager.</param>
    /// <param name="securedObject">The secured object.</param>
    /// <param name="actual">if set to <c>true</c> returns only the currently effective permissions, according to the object's inheritance status, otherwise returns all the permissions assigned to object.</param>
    /// <returns>The permissions as a PermissionSetCollectionContext</returns>
    protected internal virtual PermissionSetCollectionContext GetPermissionSetCollectionContext(
      IManager manager,
      ISecuredObject securedObject,
      bool actual,
      Guid mainSecuredObjectId)
    {
      List<WcfPermissionSetPermission> permissions = new List<WcfPermissionSetPermission>();
      List<string> stringList = new List<string>();
      foreach (string supportedPermissionSet in securedObject.SupportedPermissionSets)
      {
        string permSet = supportedPermissionSet;
        ConfigElementDictionary<string, SecurityAction> permissionSetActions = this.GetPermissionSetActions(permSet);
        IList<Telerik.Sitefinity.Security.Model.Permission> list = (IList<Telerik.Sitefinity.Security.Model.Permission>) (actual ? (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) securedObject.GetActivePermissions().ToList<Telerik.Sitefinity.Security.Model.Permission>() : (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) securedObject.Permissions).Where<Telerik.Sitefinity.Security.Model.Permission>((Func<Telerik.Sitefinity.Security.Model.Permission, bool>) (p => p.SetName == permSet)).ToList<Telerik.Sitefinity.Security.Model.Permission>();
        foreach (SecurityAction action in (ConfigElementCollection) permissionSetActions)
        {
          bool showAction;
          string title = action.GetTitle(securedObject, out showAction);
          if (showAction)
          {
            if ((action.Type & SecurityActionTypes.ChangePermissions) > SecurityActionTypes.None && securedObject.IsGranted(permSet, action.Value) && !stringList.Contains(permSet))
              stringList.Add(permSet);
            string actionTitle = title;
            string name1 = action.Name;
            string description = action.Description;
            List<WcfPermission> wcfPermissionList1 = new List<WcfPermission>();
            foreach (Telerik.Sitefinity.Security.Model.Permission permission in (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) list)
            {
              string principalName1 = this.GetPrincipalName(permission.PrincipalId);
              string str = string.Empty;
              if (this.IsPrincipalUser(permission.PrincipalId))
                str = UserProfilesHelper.GetUserDisplayName(permission.PrincipalId);
              if (string.IsNullOrEmpty(str))
                str = principalName1;
              if (!string.IsNullOrEmpty(principalName1))
              {
                if (this.IsGranted(permission, action.Name))
                {
                  List<WcfPermission> wcfPermissionList2 = wcfPermissionList1;
                  string principalType = this.IsPrincipalUser(permission.PrincipalId) ? WcfPrincipalType.User.ToString() : WcfPrincipalType.Role.ToString();
                  Guid guid = permission.PrincipalId;
                  string principalID = guid.ToString();
                  string principalName2 = principalName1;
                  string principalTitle = str;
                  string name2 = manager.Provider.Name;
                  string actionName = name1;
                  string empty1 = string.Empty;
                  string empty2 = string.Empty;
                  string permissionSetName = permSet;
                  guid = permission.ObjectId;
                  string securedObjectId = guid.ToString();
                  WcfPermission wcfPermission = new WcfPermission(principalType, principalID, principalName2, principalTitle, name2, true, false, actionName, empty1, empty2, permissionSetName, securedObjectId);
                  wcfPermissionList2.Add(wcfPermission);
                }
                if (this.IsDenied(permission, action.Name))
                {
                  List<WcfPermission> wcfPermissionList3 = wcfPermissionList1;
                  string principalType = this.IsPrincipalUser(permission.PrincipalId) ? WcfPrincipalType.User.ToString() : WcfPrincipalType.Role.ToString();
                  Guid guid = permission.PrincipalId;
                  string principalID = guid.ToString();
                  string principalName3 = principalName1;
                  string principalTitle = str;
                  string name3 = manager.Provider.Name;
                  string actionName = name1;
                  string empty3 = string.Empty;
                  string empty4 = string.Empty;
                  string permissionSetName = permSet;
                  guid = permission.ObjectId;
                  string securedObjectId = guid.ToString();
                  WcfPermission wcfPermission = new WcfPermission(principalType, principalID, principalName3, principalTitle, name3, false, true, actionName, empty3, empty4, permissionSetName, securedObjectId);
                  wcfPermissionList3.Add(wcfPermission);
                }
              }
            }
            permissions.Add(new WcfPermissionSetPermission(permSet, name1, actionTitle, description, wcfPermissionList1.ToArray(), manager.Provider.Id.ToString()));
          }
        }
      }
      return new PermissionSetCollectionContext((IEnumerable<WcfPermissionSetPermission>) permissions, securedObject, false, stringList.ToArray(), mainSecuredObjectId);
    }

    /// <summary>Creates the GUID from string.</summary>
    /// <param name="guidAsAString">The GUID as A string.</param>
    /// <returns>The GUID</returns>
    protected internal virtual Guid CreateGuidFromString(string guidAsAString) => new Guid(guidAsAString);

    /// <summary>
    /// Resets an existing permission (the action is neither granted nor denied) of a secured object, on specific principals and a specific action (if such a permission exists).
    /// </summary>
    /// <param name="originalPermissions">The original permissions of the secured object.</param>
    /// <param name="principals">The principals to reset permissions for.</param>
    /// <param name="permissionsSetName">Name of the permissions set to reset permissions for.</param>
    /// <param name="rootId">The id of the object related by the permission (if the permission is inherited, different than the object id).</param>
    /// <param name="actionName">Name of the action to reset permissions for.</param>
    protected internal virtual void ResetGrantDeny(
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> originalPermissions,
      List<WcfPermission> principals,
      string permissionsSetName,
      Guid rootId,
      string actionName)
    {
      foreach (Telerik.Sitefinity.Security.Model.Permission originalPermission in originalPermissions)
      {
        bool flag = false;
        foreach (WcfPermission principal in principals)
        {
          if (originalPermission.PrincipalId.ToString() == principal.PrincipalID && originalPermission.SetName == permissionsSetName && originalPermission.ObjectId == rootId)
            flag = true;
        }
        if (!flag && originalPermission.ObjectId == rootId && originalPermission.SetName == permissionsSetName)
        {
          this.UngrantActions(originalPermission, actionName);
          this.UndenyActions(originalPermission, actionName);
        }
      }
    }

    /// <summary>
    /// Sets the grant deny for existing permissions and creates non existing ones for multiple principals, and a specific action.
    /// </summary>
    /// <param name="mgr">The Manager to use.</param>
    /// <param name="principals">Information about the for whom the permissions are set.</param>
    /// <param name="secObj">The secured object to set permissions for.</param>
    /// <param name="permissionsSetName">Name of the relevant permissions set.</param>
    /// <param name="actionName">Name of the action to grant or deny.</param>
    /// <returns>True if some of the permissions already existed, and false if they were all created from scratch.</returns>
    protected internal virtual bool SetGrantDenyForExistingPermissionsAndCreateNonExistingOnes(
      IManager mgr,
      IList<WcfPermission> principals,
      ISecuredObject secObj,
      string permissionsSetName,
      string actionName)
    {
      bool nonExistingOnes = false;
      IQueryable<Telerik.Sitefinity.Security.Model.Permission> activePermissions = secObj.GetActivePermissions();
      foreach (WcfPermission principal in (IEnumerable<WcfPermission>) principals)
      {
        List<string> stringList1;
        if (!principal.IsAllowed)
        {
          stringList1 = new List<string>();
        }
        else
        {
          stringList1 = new List<string>();
          stringList1.Add(actionName);
        }
        List<string> allowedActions = stringList1;
        List<string> stringList2;
        if (principal.IsAllowed)
        {
          stringList2 = new List<string>();
        }
        else
        {
          stringList2 = new List<string>();
          stringList2.Add(actionName);
        }
        List<string> deniedActions = stringList2;
        nonExistingOnes = this.SetActionPermissionsForPrincipal((IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) activePermissions, new Guid(principal.PrincipalID), permissionsSetName, allowedActions, deniedActions);
        if (!nonExistingOnes)
          this.CreatePermissionForSecuredObject(mgr, secObj, new Guid(principal.PrincipalID), permissionsSetName, (IList<string>) allowedActions, (IList<string>) deniedActions);
      }
      return nonExistingOnes;
    }

    /// <summary>Saves access backend permissions for modules.</summary>
    /// <param name="manager">The module manager to use.</param>
    /// <param name="principals">Information about the for whom the permissions are set.</param>
    /// <param name="mainSecuredObject">The main secured object.</param>
    protected internal virtual void SaveAccessModuleBackendPermissions(
      IManager manager,
      IList<WcfPermission> principals,
      ISecuredObject mainSecuredObject)
    {
      if (manager == null)
        throw new ArgumentNullException(nameof (manager));
      if (mainSecuredObject == null)
        throw new ArgumentNullException(nameof (mainSecuredObject));
      if (principals == null || principals.Count == 0)
        return;
      principals = (IList<WcfPermission>) principals.Where<WcfPermission>((Func<WcfPermission, bool>) (p => p.IsAllowed)).ToList<WcfPermission>();
      if (principals.Count == 0)
        return;
      if (manager is DynamicModuleManager && mainSecuredObject is IDataItem)
      {
        ModuleBuilderManager manager1 = ModuleBuilderManager.GetManager(ManagerBase<ModuleBuilderDataProvider>.GetDefaultProviderName(), manager.TransactionName);
        string providerName = ObjectFactory.Resolve<IProviderNameResolver>().GetProviderName(((IDataItem) mainSecuredObject).Provider);
        DynamicModuleType dynamicModuleType = manager1.GetDynamicModuleType(mainSecuredObject.GetType());
        ISecuredObject securedObject = DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) manager1, (ISecuredObject) dynamicModuleType, providerName);
        if (securedObject is DynamicModuleType)
        {
          for (DynamicModuleType permissionHolder = (DynamicModuleType) securedObject; permissionHolder != null; permissionHolder = permissionHolder.ParentModuleType)
            this.AddViewBackendLinkActionToPermissionHolder((IManager) manager1, principals, (ISecuredObject) permissionHolder);
        }
        else
          this.AddViewBackendLinkActionToPermissionHolder((IManager) manager1, principals, securedObject);
      }
      else
      {
        Guid id = manager.Provider.GetSecurityRoot().Id;
        ISecuredObject permissionHolder = manager.GetItem(typeof (SecurityRoot), id) as ISecuredObject;
        if (permissionHolder == mainSecuredObject)
          return;
        if (manager is LibrariesManager)
        {
          switch (mainSecuredObject)
          {
            case Telerik.Sitefinity.Libraries.Model.Image _:
            case Telerik.Sitefinity.Libraries.Model.Album _:
              this.AddViewBackendLinkActionToPermissionHolder(manager, principals, permissionHolder, "ImagesSitemapGeneration");
              break;
            case Telerik.Sitefinity.Libraries.Model.Video _:
            case Telerik.Sitefinity.Libraries.Model.VideoLibrary _:
              this.AddViewBackendLinkActionToPermissionHolder(manager, principals, permissionHolder, "VideosSitemapGeneration");
              break;
            case Telerik.Sitefinity.Libraries.Model.Document _:
            case Telerik.Sitefinity.Libraries.Model.DocumentLibrary _:
              this.AddViewBackendLinkActionToPermissionHolder(manager, principals, permissionHolder, "DocumentsSitemapGeneration");
              break;
          }
        }
        else
          this.AddViewBackendLinkActionToPermissionHolder(manager, principals, permissionHolder);
      }
    }

    /// <summary>
    /// Adds the access module backend action to permission holder.
    /// </summary>
    /// <param name="manager">The module manager.</param>
    /// <param name="principals">The principals.</param>
    /// <param name="permissionHolder">The permission holder.</param>
    /// <param name="sitemapGenerationSetName">Name of the sitemap generation set.</param>
    /// <param name="viewBackendLink">The view backend link.</param>
    protected internal virtual void AddViewBackendLinkActionToPermissionHolder(
      IManager manager,
      IList<WcfPermission> principals,
      ISecuredObject permissionHolder,
      string sitemapGenerationSetName = "SitemapGeneration",
      string viewBackendLink = "ViewBackendLink")
    {
      if (!((IEnumerable<string>) permissionHolder.SupportedPermissionSets).Contains<string>(sitemapGenerationSetName))
        return;
      foreach (WcfPermission principal in (IEnumerable<WcfPermission>) principals)
      {
        Guid principalId = Guid.Parse(principal.PrincipalID);
        Telerik.Sitefinity.Security.Model.Permission permission = permissionHolder.GetActivePermissions().SingleOrDefault<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.PrincipalId == principalId && p.ObjectId == permissionHolder.Id && p.SetName == sitemapGenerationSetName));
        if (permission == null)
        {
          permission = manager.CreatePermission(sitemapGenerationSetName, permissionHolder.Id, principalId);
          permissionHolder.Permissions.Add(permission);
        }
        this.GrantActions(permission, viewBackendLink);
        this.UndenyActions(permission, viewBackendLink);
      }
    }

    /// <summary>
    /// Grants or denies actions in a set of permissions for a specific principal, action and object ID, if a matching permission exists in the collection.
    /// </summary>
    /// <param name="permissions">Permissions for which the action is to be granted or denied.</param>
    /// <param name="principalId">ID of the principal to match.</param>
    /// <param name="objectId">ID of the secured object to match.</param>
    /// <param name="permissionSetName">Name of the permission set to match.</param>
    /// <param name="allowedActions">The allowed actions.</param>
    /// <param name="deniedActions">The denied actions.</param>
    /// <returns></returns>
    internal virtual bool SetActionPermissionsForPrincipal(
      IEnumerable<Telerik.Sitefinity.Security.Model.Permission> permissions,
      Guid principalId,
      string permissionSetName,
      List<string> allowedActions,
      List<string> deniedActions)
    {
      bool flag = false;
      foreach (Telerik.Sitefinity.Security.Model.Permission permission in permissions)
      {
        if (permission.PrincipalId == principalId && permission.SetName == permissionSetName)
        {
          flag = true;
          this.GrantActions(permission, allowedActions.ToArray());
          this.UndenyActions(permission, allowedActions.ToArray());
          this.DenyActions(permission, deniedActions.ToArray());
          this.UngrantActions(permission, deniedActions.ToArray());
        }
      }
      return flag;
    }

    /// <summary>
    /// Creates the permission for secured object and allows or denies a specific action for a specific principal.
    /// </summary>
    /// <param name="manager">The manager of the secured object, to be used for permission creation.</param>
    /// <param name="securedObject">The secured object for which the permission is created.</param>
    /// <param name="principalId">The principal pageId to associate with the permission.</param>
    /// <param name="permissionSetName">Name of the permission set to associate with the permission.</param>
    /// <param name="allowedActions">The allowed actions.</param>
    /// <param name="deniedActions">The denied actions.</param>
    internal virtual Telerik.Sitefinity.Security.Model.Permission CreatePermissionForSecuredObject(
      IManager manager,
      ISecuredObject securedObject,
      Guid principalId,
      string permissionSetName,
      IList<string> allowedActions,
      IList<string> deniedActions)
    {
      if (manager.TransactionName.IsNullOrWhitespace())
      {
        string inheritanceTransactionName = this.GetPermissionsInheritanceTransactionName();
        manager = this.GetMappedManagerInTransaction(manager, manager.Provider.Name, inheritanceTransactionName);
      }
      Telerik.Sitefinity.Security.Model.Permission permission = manager.Provider.GetOrCreatePermission(permissionSetName, securedObject.Id, principalId);
      manager.AddPermissionToObject(securedObject, (IManager) null, permission, (string) null);
      string[] array1 = allowedActions.ToArray<string>();
      string[] array2 = deniedActions.ToArray<string>();
      this.GrantActions(permission, array1);
      this.UndenyActions(permission, array1);
      this.DenyActions(permission, array2);
      this.UngrantActions(permission, array2);
      return permission;
    }

    /// <summary>Add a permission to all permission inheritors</summary>
    /// <param name="mgr">Manager instance to use</param>
    /// <param name="secObj">Add <paramref name="perm" /> to this secured object's permission inheritors</param>
    /// <param name="perm">Permission to add</param>
    protected internal virtual void AddPermissionToPermissionInheritors(
      IManager mgr,
      ISecuredObject secObj,
      Telerik.Sitefinity.Security.Model.Permission perm)
    {
      foreach (ISecuredObject permissionsInheritor in mgr.GetPermissionsInheritors(secObj, false, (Type) null))
        permissionsInheritor.Permissions.Add(perm);
    }

    /// <summary>Disables the service cache.</summary>
    internal virtual void DisableServiceCache() => ServiceUtility.DisableCache();

    /// <summary>Gets the application module.</summary>
    /// <returns></returns>
    internal virtual WcfPermissionModule GetApplicationModule()
    {
      IManager manager = this.GetManager(this.GetApplicationRootManagerType(), this.GetApplicationRootProviderName());
      return new WcfPermissionModule()
      {
        Providers = new List<WcfPermissionProvider>()
        {
          new WcfPermissionProvider()
          {
            ProviderName = manager.Provider.Name,
            ProviderId = manager.Provider.Id.ToString(),
            SecuredObjectId = this.GetApplicationRootId().ToString(),
            ProviderTitle = manager.Provider.Title,
            PermissionSetName = "Backend",
            ManagerType = manager.GetType().FullName,
            SecuredObjectType = typeof (SecurityRoot).AssemblyQualifiedName
          }
        }.ToArray(),
        ModuleTitle = Res.Get<SecurityResources>().GlobalActionPermissionsListTitle
      };
    }

    /// <summary>
    /// Retrieves information about the modules registered on the system, by an enumerable collection of IModule objects.
    /// </summary>
    /// <param name="modulesCollection">An enumerable collection of IModule objects.</param>
    /// <param name="securedObjectID">Optional: Get modules by a specific ID.</param>
    /// <param name="predefinedManager">Optional: Get modules by a specific Manager.</param>
    /// <param name="predefinedManagerType">Optional: Get modules by a specific provider type.</param>
    /// <param name="dataProviderName">Optional: Get modules by a specific provider name.</param>
    /// <returns>A list of WcfPermissionModule objects filled with information about the retrieved modules</returns>
    internal virtual List<WcfPermissionModule> RetrieveModules(
      IEnumerable<IModule> modulesCollection,
      Guid securedObjectID,
      IManager predefinedManager,
      Type predefinedManagerType,
      string dataProviderName)
    {
      List<WcfPermissionModule> permissionModuleList = new List<WcfPermissionModule>();
      foreach (IModule modules in modulesCollection)
      {
        if (modules.Startup != StartupType.Disabled)
        {
          WcfPermissionModule permissionModule = new WcfPermissionModule();
          permissionModule.ModuleTitle = modules.Title;
          List<WcfPermissionProvider> permissionProviderList = new List<WcfPermissionProvider>();
          if (modules is ISecuredModule)
          {
            foreach (SecurityRoot securityRoot in (IEnumerable<SecurityRoot>) ((ISecuredModule) modules).GetSecurityRoots())
            {
              if (securityRoot != null)
              {
                IManager manager = this.GetManager(securityRoot.ManagerType, securityRoot.DataProviderName);
                if (manager != null && (securedObjectID != Guid.Empty && securityRoot.Id == securedObjectID || predefinedManager != null && predefinedManager.GetType() == securityRoot.ManagerType && securityRoot.DataProviderName == dataProviderName || predefinedManagerType != (Type) null && securityRoot.ManagerType == predefinedManagerType || securedObjectID == Guid.Empty && predefinedManager == null && predefinedManagerType == (Type) null))
                {
                  WcfPermissionProvider permissionProvider = new WcfPermissionProvider()
                  {
                    ProviderName = securityRoot.DataProviderName,
                    ProviderId = manager.Provider.Id.ToString(),
                    SecuredObjectId = securityRoot.Id.ToString(),
                    ProviderTitle = manager.Provider.Title,
                    ManagerType = manager.GetType().FullName,
                    PermissionSetName = "",
                    SecuredObjectType = securityRoot.GetType().AssemblyQualifiedName
                  };
                  permissionProviderList.Add(permissionProvider);
                }
              }
            }
            if (permissionProviderList.Count > 0)
            {
              permissionModule.Providers = permissionProviderList.ToArray();
              permissionModuleList.Add(permissionModule);
            }
          }
        }
      }
      return permissionModuleList;
    }

    private void NotifyOutputCacheDependencies(IManager mgr, ISecuredObject secObj)
    {
      List<CacheDependencyKey> cacheDependencyKeyList = new List<CacheDependencyKey>();
      ContentDataProviderBase contentProvider = mgr.Provider as ContentDataProviderBase;
      if (contentProvider != null && secObj is SecurityRoot)
        cacheDependencyKeyList.AddRange(((IEnumerable<Type>) contentProvider.GetKnownTypes()).Where<Type>((Func<Type, bool>) (t => typeof (ISecuredObject).IsAssignableFrom(t))).SelectMany<Type, CacheDependencyKey>((Func<Type, IEnumerable<CacheDependencyKey>>) (t => OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(t, contentProvider.ApplicationName))));
      else if (mgr is ModuleBuilderManager)
      {
        List<Tuple<string, string>> source1 = new List<Tuple<string, string>>();
        if (secObj is DynamicModule)
        {
          string providerName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
          IEnumerable<IDynamicModuleType> source2 = ModuleBuilderManager.GetModules().Active().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == ((DynamicModule) secObj).Id)).SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types)).Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.InheritsPermissions));
          source1.AddRange(source2.Select<IDynamicModuleType, Tuple<string, string>>((Func<IDynamicModuleType, Tuple<string, string>>) (t => new Tuple<string, string>(t.FullTypeName, providerName))));
        }
        else if (secObj is DynamicModuleType)
        {
          string defaultProviderName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
          source1.Add(new Tuple<string, string>(((DynamicModuleType) secObj).GetFullTypeName(), defaultProviderName));
        }
        else
        {
          ModuleBuilderManager moduleBuilderManager = (ModuleBuilderManager) mgr;
          if (secObj is DynamicContentProvider)
          {
            source1.AddRange(this.GetAffectedTypeAndProviderPair(moduleBuilderManager, (DynamicContentProvider) secObj));
          }
          else
          {
            string providerName = ManagerBase<DynamicModuleDataProvider>.GetDefaultProviderName();
            source1.AddRange(ModuleBuilderManager.GetModules().Active().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.InheritsPermissions)).SelectMany<IDynamicModule, IDynamicModuleType>((Func<IDynamicModule, IEnumerable<IDynamicModuleType>>) (m => m.Types)).Where<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.InheritsPermissions)).Select<IDynamicModuleType, Tuple<string, string>>((Func<IDynamicModuleType, Tuple<string, string>>) (t => new Tuple<string, string>(t.FullTypeName, providerName))));
            IQueryable<DynamicContentProvider> contentProviders = moduleBuilderManager.GetDynamicContentProviders();
            Expression<Func<DynamicContentProvider, bool>> predicate = (Expression<Func<DynamicContentProvider, bool>>) (p => p.InheritsPermissions && p.ParentSecuredObjectType == typeof (DynamicModule).FullName);
            foreach (DynamicContentProvider dynContentProvider in contentProviders.Where<DynamicContentProvider>(predicate).ToList<DynamicContentProvider>())
              source1.AddRange(this.GetAffectedTypeAndProviderPair(moduleBuilderManager, dynContentProvider));
          }
        }
        cacheDependencyKeyList.AddRange(source1.SelectMany<Tuple<string, string>, CacheDependencyKey>((Func<Tuple<string, string>, IEnumerable<CacheDependencyKey>>) (t => OutputCacheDependencyHelper.GetPublishedContentCacheDependencyKeys(TypeResolutionService.ResolveType(t.Item1), DynamicModuleManager.GetManager(t.Item2).Provider.ApplicationName))));
      }
      if (!cacheDependencyKeyList.Any<CacheDependencyKey>())
        return;
      CacheDependency.Notify((IList<CacheDependencyKey>) cacheDependencyKeyList);
    }

    private IEnumerable<Tuple<string, string>> GetAffectedTypeAndProviderPair(
      ModuleBuilderManager moduleBuilderManager,
      DynamicContentProvider dynContentProvider)
    {
      if (dynContentProvider.ParentSecuredObjectType.Equals(typeof (DynamicModule).FullName))
      {
        IDynamicModule module = ModuleBuilderManager.GetModules().Active().Where<IDynamicModule>((Func<IDynamicModule, bool>) (m => m.Id == dynContentProvider.ParentSecuredObjectId)).FirstOrDefault<IDynamicModule>();
        if (module != null)
        {
          Guid[] types = module.Types.Select<IDynamicModuleType, Guid>((Func<IDynamicModuleType, Guid>) (t => t.Id)).ToArray<Guid>();
          IQueryable<DynamicContentProvider> source = moduleBuilderManager.GetDynamicContentProviders().Where<DynamicContentProvider>((Expression<Func<DynamicContentProvider, bool>>) (p => p.Name == dynContentProvider.Name && types.Contains<Guid>(p.ParentSecuredObjectId) && p.ParentSecuredObjectType == typeof (DynamicModuleType).FullName && p.InheritsPermissions));
          Expression<Func<DynamicContentProvider, Guid>> keySelector = (Expression<Func<DynamicContentProvider, Guid>>) (p => p.ParentSecuredObjectId);
          foreach (IGrouping<Guid, DynamicContentProvider> grouping in (IEnumerable<IGrouping<Guid, DynamicContentProvider>>) source.GroupBy<DynamicContentProvider, Guid>(keySelector))
          {
            IGrouping<Guid, DynamicContentProvider> typeGroup = grouping;
            IDynamicModuleType type = module.Types.FirstOrDefault<IDynamicModuleType>((Func<IDynamicModuleType, bool>) (t => t.Id == typeGroup.Key));
            if (type != null)
            {
              foreach (DynamicContentProvider dynamicContentProvider in (IEnumerable<DynamicContentProvider>) typeGroup)
                yield return new Tuple<string, string>(type.FullTypeName, dynamicContentProvider.Name);
            }
            type = (IDynamicModuleType) null;
          }
        }
        module = (IDynamicModule) null;
      }
      else if (dynContentProvider.ParentSecuredObjectType.Equals(typeof (DynamicModuleType).FullName))
      {
        string str = ModuleBuilderManager.GetActiveTypes().Where<IMetaType>((Func<IMetaType, bool>) (t => t.Id == dynContentProvider.ParentSecuredObjectId)).Select<IMetaType, string>((Func<IMetaType, string>) (t => t.FullTypeName)).FirstOrDefault<string>();
        if (!string.IsNullOrEmpty(str))
          yield return new Tuple<string, string>(str, dynContentProvider.Name);
      }
    }

    /// <summary>
    /// Demand a generic security action. Will work only on properly configured permission sets
    /// </summary>
    /// <param name="securedObject">Secured object to demand for</param>
    /// <param name="type">Generic type of the action to demand</param>
    protected internal virtual void DemandPermission(
      ISecuredObject securedObject,
      SecurityActionTypes type,
      string permissionsSetName = null)
    {
      securedObject.Demand(type, permissionsSetName);
    }

    /// <summary>Returns active permissions only</summary>
    /// <param name="forSecuredObject">Extended secured object</param>
    /// <returns>Unique set of active permissions</returns>
    /// <remarks>This could be a very slow operation if the secured object has many permissions.</remarks>
    internal virtual IEnumerable<Telerik.Sitefinity.Security.Model.Permission> GetActivePermissions(
      ISecuredObject forSecuredObject)
    {
      return (IEnumerable<Telerik.Sitefinity.Security.Model.Permission>) forSecuredObject.GetActivePermissions();
    }

    /// <summary>
    /// Decode a previously encoded string to its original form
    /// </summary>
    /// <param name="strToDecode">The string to decode</param>
    /// <returns>Decoded string</returns>
    internal virtual string DecodeWcfString(string strToDecode) => WcfHelper.DecodeWcfString(strToDecode);

    /// <summary>Get manager from type and provider name</summary>
    /// <param name="managerClassName">Full type name of manager to use</param>
    /// <param name="dataProviderName">Name of the provider. Null or Empty will return the default.</param>
    /// <returns>Manager instance</returns>
    internal virtual IManager GetManager(string managerClassName, string dataProviderName) => ManagerBase.GetManager(this.DecodeWcfString(managerClassName), dataProviderName);

    /// <summary>Get manager using the default provider</summary>
    /// <param name="managerClassName">Full type name of the provider to use</param>
    /// <returns>Manager instance</returns>
    internal virtual IManager GetManager(string managerClassName) => ManagerBase.GetManager(this.DecodeWcfString(managerClassName));

    /// <summary>Get manager from type and provider name</summary>
    /// <param name="managerClass">Type of the manager to get</param>
    /// <param name="dataProviderName">Name of the provider. Null or Empty will return the default.</param>
    /// <returns>Manager instance</returns>
    internal virtual IManager GetManager(Type managerClass, string dataProviderName) => ManagerBase.GetManager(managerClass, dataProviderName);

    /// <summary>
    /// Gets the CLR type of the data manager for this security root.
    /// </summary>
    /// <returns>The CLR type of the data manager for this security root. </returns>
    internal virtual Type GetApplicationRootManagerType() => AppPermission.Root.ManagerType;

    /// <summary>Gets the name of the application root provider.</summary>
    /// <returns>The name of the application root provider.</returns>
    internal virtual string GetApplicationRootProviderName() => AppPermission.Root.DataProviderName;

    /// <summary>Gets the application root id.</summary>
    /// <returns>The application root id.</returns>
    internal virtual Guid GetApplicationRootId() => AppPermission.Root.Id;

    /// <summary>Grants the specified actions to this permissions.</summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to grant.</param>
    internal virtual void GrantActions(Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions) => permission.GrantActions(true, actions);

    /// <summary>
    /// Reset specific granted actions (make sure the principal is not granted those actions).
    /// This action always appends to the existing permission, and does not reset the whole grant value
    /// (otherwise the result would always be Grant=0)
    /// </summary>
    /// <param name="permission">The Permission</param>
    /// <param name="actions">The actions to ungrant.</param>
    internal virtual void UngrantActions(Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions) => permission.UngrantActions(actions);

    /// <summary>Denies the specified actions to this permissions.</summary>
    /// <param name="permission">The permission.</param>
    /// <param name="actions">The actions to deny.</param>
    internal virtual void DenyActions(Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions) => permission.DenyActions(true, actions);

    /// <summary>
    /// Reset specific denied actions (make sure the principal is not denied those actions)
    /// This action always appends to the existing permission, and does not reset the whole deny value
    /// (otherwise the result would always be Deny=0)
    /// </summary>
    /// <param name="permission">The Permission</param>
    /// <param name="actions">The actions to undeny.</param>
    internal virtual void UndenyActions(Telerik.Sitefinity.Security.Model.Permission permission, params string[] actions) => permission.UndenyActions(actions);

    /// <summary>
    /// Checks if the requested action is granted to this permission.
    /// </summary>
    /// <param name="permission">The permission.</param>
    /// <param name="action">The action to check.</param>
    /// <returns>
    /// true if the requested action is granted the specified actions; otherwise, false.
    /// </returns>
    internal virtual bool IsGranted(Telerik.Sitefinity.Security.Model.Permission permission, string action) => permission.IsGranted(action);

    /// <summary>
    /// Checks if the requested action is denied to this permission.
    /// </summary>
    /// <param name="permission">The permission.</param>
    /// <param name="action">The action to check.</param>
    /// <returns>
    /// true if the requested action is denied the specified actions; otherwise, false.
    /// </returns>
    internal virtual bool IsDenied(Telerik.Sitefinity.Security.Model.Permission permission, string action) => permission.IsDenied(action);

    /// <summary>Converts a string to a GUID.</summary>
    /// <param name="idString">String representing a GUID</param>
    /// <returns>A valid GUID, or Guid.Empty if the string does not represent a valid GUID.</returns>
    internal virtual Guid StringToGuid(string idString) => Telerik.Sitefinity.Utilities.Utility.StringToGuid(idString);

    /// <summary>
    /// Retrieves the configuration actions realted to a specific permission set.
    /// </summary>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <returns>The configuration actions realted to a specific permission set.</returns>
    internal virtual ConfigElementDictionary<string, SecurityAction> GetPermissionSetActions(
      string permissionSetName)
    {
      return Config.Get<SecurityConfig>().Permissions[permissionSetName].Actions;
    }

    /// <summary>Gets the name of a principal by ID.</summary>
    /// <param name="principalId">The principal id.</param>
    /// <returns>The name of a principal</returns>
    internal virtual string GetPrincipalName(Guid principalId) => SecurityManager.GetPrincipalName(principalId);

    /// <summary>Finds a user by ID.</summary>
    /// <param name="userId">The user id.</param>
    /// <returns>The user object, if found. Otherwise null.</returns>
    internal virtual User FindUser(Guid userId) => UserManager.FindUser(userId);

    /// <summary>Check if a specific principal represents a user</summary>
    /// <param name="principalId">id of the principal to check</param>
    /// <returns>true if the principal represents a user, false otherwise</returns>
    internal virtual bool IsPrincipalUser(Guid principalId) => SecurityManager.IsPrincipalUser(principalId);

    /// <summary>
    /// Gets the unique name of the permissions inheritance transaction.
    /// Used when there's a chance for updating multiple objects of different types (and providers) with permissions (when inheritance is applied).
    /// </summary>
    /// <returns>The unique name of the permissions inheritance transaction.</returns>
    internal virtual string GetPermissionsInheritanceTransactionName() => "permissionInheritanceTransactionName" + Guid.NewGuid().ToString();

    /// <summary>Gets the mapped manager in transaction.</summary>
    /// <param name="managerInstance">The manager instance.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="transactionName">Name of the transaction.</param>
    /// <returns>Instance or null of the manager doesn't support managed transactions</returns>
    internal virtual IManager GetMappedManagerInTransaction(
      IManager managerInstance,
      string providerName,
      string transactionName)
    {
      return ManagerBase.GetMappedManagerInTransaction(managerInstance, providerName, transactionName);
    }

    /// <summary>
    /// Handles a case where a secured object was updated with new security settings:
    /// Copies the new permissions to the live item.
    /// </summary>
    /// <param name="secObj">The updated secured object.</param>
    /// <param name="manager">Relevant manager instance of the secured object.</param>
    internal virtual void SecuredContentUpdated(ISecuredObject secObj, IManager manager)
    {
      if (secObj is Content cnt && manager.Provider is ContentDataProviderBase provider && cnt.SupportsContentLifecycle)
      {
        Content liveContentBase = provider.GetLiveContentBase(cnt);
        if (cnt.Status == ContentLifecycleStatus.Master && liveContentBase != null)
        {
          ((ISecuredObject) liveContentBase).CopySecurityFrom(secObj, (IDataProviderBase) provider, (IDataProviderBase) provider);
          return;
        }
      }
      if (!(secObj is DynamicContent dynamicContent) || dynamicContent.Status != ContentLifecycleStatus.Master)
        return;
      ILifecycleDataItem live = ((DynamicModuleManager) manager).Lifecycle.GetLive((ILifecycleDataItem) dynamicContent);
      if (live == null)
        return;
      ((ISecuredObject) live).CopySecurityFrom((ISecuredObject) dynamicContent, (IDataProviderBase) manager.Provider, (IDataProviderBase) manager.Provider);
    }
  }
}
