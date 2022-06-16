// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfPermissionProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Wraps a collection of WcfPermissionSetPermission (representing a permission set), for a specific provider
  /// </summary>
  [DataContract]
  public class WcfPermissionProvider
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermissionProvider" /> class.
    /// </summary>
    public WcfPermissionProvider()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.Web.Services.WcfPermissionProvider" /> class.
    /// </summary>
    /// <param name="providerTitle">GUI Title of the provider.</param>
    /// <param name="providerId">Id of the provider.</param>
    /// <param name="securedObjectId">Id of the Secured object related to the provider.</param>
    /// <param name="securedObjectType">Type of the Secured object related to the provider.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager class related to the provider.</param>
    /// <param name="permissionSets">Permission sets related to the provider.</param>
    /// <param name="permissionSetName">Specifying a specific permission set related to the provider.</param>
    public WcfPermissionProvider(
      string providerTitle,
      string providerId,
      string securedObjectId,
      string securedObjectType,
      string providerName,
      string managerType,
      WcfPermissionSetPermission[] permissionSets,
      string permissionSetName)
    {
      this.ProviderTitle = providerTitle;
      this.ProviderId = providerId;
      this.SecuredObjectId = securedObjectId;
      this.SecuredObjectType = securedObjectType;
      this.ProviderName = providerName;
      this.ManagerType = managerType;
      this.PermissionSets = permissionSets;
      this.PermissionSetName = permissionSetName;
    }

    /// <summary>GUI Title of the provider</summary>
    [DataMember]
    public string ProviderTitle { get; set; }

    /// <summary>Id of the provider</summary>
    [DataMember]
    public string ProviderId { get; set; }

    /// <summary>Id of the Secured object related to the provider</summary>
    [DataMember]
    public string SecuredObjectId { get; set; }

    /// <summary>Type of the Secured object related to the provider</summary>
    [DataMember]
    public string SecuredObjectType { get; set; }

    /// <summary>Name of the provider</summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>Gets or sets the name of the dynamic module provider.</summary>
    /// <value>The name of the dynamic module provider.</value>
    [DataMember]
    public string DynamicDataProviderName { get; set; }

    /// <summary>Type of the manager class related to the provider</summary>
    [DataMember]
    public string ManagerType { get; set; }

    /// <summary>Permission sets related to the provider</summary>
    [DataMember]
    public WcfPermissionSetPermission[] PermissionSets { get; set; }

    /// <summary>
    /// Specifying a specific permission set related to the provider
    /// </summary>
    [DataMember]
    public string PermissionSetName { get; set; }
  }
}
