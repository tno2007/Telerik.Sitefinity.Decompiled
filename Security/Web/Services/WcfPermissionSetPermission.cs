// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfPermissionSetPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Wraps a collection of WcfPermission instances into PermissionSets.
  /// </summary>
  [DataContract]
  public class WcfPermissionSetPermission
  {
    /// <summary>Constructor</summary>
    /// <param name="permissionSetName">Name of the permisison set</param>
    /// <param name="actionName">Name of the action</param>
    /// <param name="actionTitle">GUI title of the action</param>
    /// <param name="actionDescription">Description text of the action</param>
    /// <param name="permissions">A list of WcfPermission objects, representing permissions</param>
    /// <param name="providerId">Id of the related provider</param>
    /// <param name="inheritsPermissions">Whether or not the object inherits its permissions from its parent</param>
    /// <param name="canInheritPermissions">Whether the object can inherit its permissions from any parent (for UI purposes)</param>
    public WcfPermissionSetPermission(
      string permissionSetName,
      string actionName,
      string actionTitle,
      string actionDescription,
      WcfPermission[] permissions,
      string providerId)
    {
      this.PermissionSetName = permissionSetName;
      this.ActionName = actionName;
      this.ActionDescription = actionDescription;
      this.Permissions = permissions;
      this.ProviderId = providerId;
      this.ActionTitle = actionTitle;
    }

    /// <summary>Name of the permisison set</summary>
    [DataMember]
    public string PermissionSetName { get; set; }

    /// <summary>Name of the action</summary>
    [DataMember]
    public string ActionName { get; set; }

    /// <summary>GUI title of the action</summary>
    [DataMember]
    public string ActionTitle { get; set; }

    /// <summary>Description text of the action</summary>
    [DataMember]
    public string ActionDescription { get; set; }

    /// <summary>
    /// A list of WcfPermission objects, representing permissions
    /// </summary>
    [DataMember]
    public WcfPermission[] Permissions { get; set; }

    /// <summary>Id of the related provider</summary>
    [DataMember]
    public string ProviderId { get; set; }
  }
}
