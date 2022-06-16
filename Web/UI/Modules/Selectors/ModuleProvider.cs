// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Modules.Selectors.ModuleProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Web.UI.Modules.Selectors
{
  /// <summary>
  /// A class to relate a provider listed in the providers combo with manager/provider/secured object
  /// </summary>
  [DataContract]
  public class ModuleProvider
  {
    /// <summary>Constructor</summary>
    /// <param name="permissionSetName">Name of the permission set</param>
    /// <param name="managerTypeName">Type of the associated manager, converted to string</param>
    /// <param name="providerTitle">UI Title of the provider</param>
    /// <param name="providerName">Name of the provider</param>
    /// <param name="securedObjectRootId">ID of the secured object</param>
    /// <param name="isSelectedProvider">Indicates whether this provider is selected</param>
    /// <param name="securedObjectType">Type of the secured object</param>
    public ModuleProvider(
      string permissionSetName,
      string managerTypeName,
      string providerTitle,
      string providerName,
      string securedObjectRootId,
      bool isSelectedProvider,
      string securedObjectType)
    {
      this.PermissionSetName = permissionSetName;
      this.ManagerTypeName = managerTypeName;
      this.ProviderTitle = providerTitle;
      this.ProviderName = providerName;
      this.SecuredObjectRootId = securedObjectRootId;
      this.IsSelectedProvider = isSelectedProvider;
      this.SecuredObjectType = securedObjectType;
    }

    /// <summary>Name of the permission set</summary>
    [DataMember]
    public string PermissionSetName { get; set; }

    /// <summary>Type of the associated manager, converted to string</summary>
    [DataMember]
    public string ManagerTypeName { get; set; }

    /// <summary>UI Title of the provider</summary>
    [DataMember]
    public string ProviderTitle { get; set; }

    /// <summary>Name of the provider</summary>
    [DataMember]
    public string ProviderName { get; set; }

    /// <summary>ID of the secured object</summary>
    [DataMember]
    public string SecuredObjectRootId { get; set; }

    /// <summary>Indicates whether this provider is selected</summary>
    public bool IsSelectedProvider { get; set; }

    /// <summary>Type of the secured object</summary>
    [DataMember]
    public string SecuredObjectType { get; set; }
  }
}
