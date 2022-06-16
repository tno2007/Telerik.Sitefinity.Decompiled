// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.IPermissions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;
using Telerik.Sitefinity.Web.UI.Modules.Selectors;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>Interface for the Permissions WCF service</summary>
  [ServiceContract]
  public interface IPermissions
  {
    /// <summary>
    /// Saves permiossions for given principals for a specific action
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
    [WebHelp(Comment = "Inserts or updates a set of principals, allowed or denied to perform a specific action. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{permissionsSetName}/{dataProviderName}/{actionName}/?permissionObjectRootID={permissionObjectRootID}&managerClassName={managerClassName}&securedObjectID={securedObjectID}&securedObjectType={securedObjectType}&dynamicDataProviderName={dynamicDataProviderName}")]
    [OperationContract]
    void SavePermissionsForSpecificAction(
      List<WcfPermission> principals,
      string permissionsSetName,
      string dataProviderName,
      string permissionObjectRootID,
      string actionName,
      string managerClassName,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName);

    /// <summary>
    /// Saves permiossions for given actions for a specific prinfipal
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
    [WebHelp(Comment = "Inserts or updates a set of principals, allowed or denied to perform a specific action. The results are returned in JSON format.")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/{permissionsSetName}/{dataProviderName}/?permissionObjectRootID={permissionObjectRootID}&managerClassName={managerClassName}&principalID={principalID}&securedObjectID={securedObjectID}&securedObjectType={securedObjectType}&dynamicDataProviderName={dynamicDataProviderName}")]
    [OperationContract]
    void SavePermissionActionsForSpecificPrincipal(
      List<WcfPermission> principals,
      string permissionsSetName,
      string dataProviderName,
      string permissionObjectRootID,
      string managerClassName,
      Guid principalID,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName);

    /// <summary>
    /// Gets the collection of permissions per user in JSON format.
    /// </summary>
    /// <param name="permissionsSetName">The name of the permission set to retrieve</param>
    /// <param name="dataProviderName">The name of the provider to use</param>
    /// <param name="managerClassName">The full name of the manager class to use</param>
    /// <param name="principalID">ID (Guid to string) of the user/role to use</param>
    /// <param name="securedObjectID">ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).</param>
    /// <returns>A coillection of <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermission" /> permission objects</returns>
    [WebHelp(Comment = "Gets the collection of permissions per user in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetModules/?dataProviderName={dataProviderName}&securedObjectID={securedObjectID}&managerClassName={managerClassName}")]
    [OperationContract]
    CollectionContext<WcfPermissionModule> GetModules(
      string dataProviderName,
      string managerClassName,
      string securedObjectID);

    /// <summary>Gets providers info for a specific module.</summary>
    /// <param name="moduleName">Name of the related module.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider">ModuleProvider</see> objects</returns>
    [WebHelp(Comment = "Gets the a list of providers' data for a secured object by the object type and ID, in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetModuleProviders/?moduleName={moduleName}")]
    [OperationContract]
    CollectionContext<ModuleProvider> GetModuleProviders(
      string moduleName);

    /// <summary>Gets providers info by a specific manager type.</summary>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider">ModuleProvider</see> objects</returns>
    [WebHelp(Comment = "Gets the a list of providers' data for a secured object by the object's manager type and ID, in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetManagerProviders/?managerClassName={managerClassName}")]
    [OperationContract]
    CollectionContext<ModuleProvider> GetManagerProviders(
      string managerClassName);

    /// <summary>
    /// Gets system providers info by a specific manager type.
    /// </summary>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <returns>A collection of <see cref="T:Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider"> system ModuleProvider</see> objects</returns>
    [WebHelp(Comment = "Gets the a list of providers' data for a secured object by the object's manager type and ID, in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetManagerSystemProviders/?managerClassName={managerClassName}")]
    [OperationContract]
    CollectionContext<ModuleProvider> GetManagerSystemProviders(
      string managerClassName);

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
    [WebHelp(Comment = "Gets the collection of permissions per secured object in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/?permissionsSetName={commaDelimitedPermissionsSetNames}&dataProviderName={dataProviderName}&managerClassName={managerClassName}&securedObjectID={securedObjectID}&securedObjectType={securedObjectType}&dynamicDataProviderName={dynamicDataProviderName}")]
    [OperationContract]
    PermissionSetCollectionContext GetPermissionsForSecuredObject(
      string commaDelimitedPermissionsSetNames,
      string dataProviderName,
      string managerClassName,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName);

    /// <summary>Break or restore inheritance</summary>
    /// <param name="securedObjectIdString">Secured object pageId</param>
    /// <param name="securedObjectTypeName">Wcf-encoded secured object type name</param>
    /// <param name="managerTypeName">Wcf-encoded secured object type name</param>
    /// <param name="providerName">Provider name</param>
    /// <param name="BreakInheritance">true to break inheritance, false to restore inheritance</param>
    /// <param name="loseCustomChanges">true to lose added permissions when the inheritance is restored, false to keep them</param>
    /// <param name="transactionName">Optional parameter: The name of the active transaction in which the manager should be retrieved.</param>
    /// <returns>Active permissions after the inheritance change</returns>
    [WebHelp(Comment = "Break or restore inheritance")]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "/ChangeInheritance/?dataProviderName={providerName}&managerClassName={managerTypeName}&securedObjectID={securedObjectIdString}&securedObjectType={securedObjectTypeName}&break={breakInheritance}&loseCustomChanges={loseCustomChanges}&transactionName={transactionName}&dynamicDataProviderName={dynamicDataProviderName}")]
    [OperationContract]
    PermissionSetCollectionContext ChangeInheritance(
      string securedObjectIdString,
      string securedObjectTypeName,
      string managerTypeName,
      string providerName,
      string breakInheritance,
      string loseCustomChanges,
      string transactionName,
      string dynamicDataProviderName);

    /// <summary>
    /// Gets the collection of permission Sets per user in JSON format.
    /// </summary>
    /// <param name="permissionsSetName">The name of the permission set to retrieve</param>
    /// <param name="dataProviderName">The name of the provider to use</param>
    /// <param name="managerClassName">The full name of the manager class to use</param>
    /// <param name="principalID">ID (Guid to string) of the user/role to use</param>
    /// <param name="securedObjectID">ID of the secured object (optional. If not specified, the SecurityRoot of the manager+provider is used).</param>
    /// <param name="securedObjectType">Name of the type of the secured object</param>
    /// <returns>A coillection of <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermission" /> permission objects</returns>
    [WebHelp(Comment = "Gets the collection of permissions per user in JSON format.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetPermissionSets/?permissionsSetName={permissionsSetName}&dataProviderName={dataProviderName}&managerClassName={managerClassName}&principalID={principalID}&securedObjectID={securedObjectID}&securedObjectType={securedObjectType}&dynamicDataProviderName={dynamicDataProviderName}")]
    [OperationContract]
    PermissionSetCollectionContext GetPermissionSetsForSpecificPrincipal(
      string permissionsSetName,
      string dataProviderName,
      string managerClassName,
      string principalID,
      string securedObjectID,
      string securedObjectType,
      string dynamicDataProviderName);

    /// <summary>Gets the provider usage.</summary>
    /// <param name="dataProviderName">Name of the data provider.</param>
    /// <param name="managerClassName">Name of the manager class.</param>
    /// <returns></returns>
    [WebHelp(Comment = "Gets the a list of sites using the given provider.")]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetProviderUsage/?dataProviderName={dataProviderName}&managerClassName={managerClassName}&dynamicModuleTitle={dynamicModuleTitle}&securedObjectID={securedObjectID}&securedObjectType={securedObjectType}")]
    [OperationContract]
    ProviderUsageCollectionContext GetProviderUsage(
      string dataProviderName,
      string managerClassName,
      string dynamicModuleTitle,
      string securedObjectID,
      string securedObjectType);
  }
}
