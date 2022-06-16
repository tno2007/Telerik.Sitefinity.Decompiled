// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.WcfPermission
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// Represents a communication class between client and server for respresenting permissions.
  /// Each per specific principal (user/role), data provider and action.
  /// Corresponding IDs of client controls representing the allow/deny permissions are optional.
  /// </summary>
  [DataContract]
  public class WcfPermission : IComparable<WcfPermission>
  {
    /// <summary>Constructor (duh!)</summary>
    public WcfPermission()
    {
    }

    /// <summary>Constructor</summary>
    /// <param name="principalType">The Principal type (user/role)</param>
    /// <param name="principalID">The ID of the principal</param>
    /// <param name="principalName">The name of the principal</param>
    /// <param name="principalTitle">The principal title, the one displayed in the UI. If it's a user- would contain the first and last name</param>
    /// <param name="dataProviderName">Name of the data provider related to the permission</param>
    /// <param name="isAllowed">Is the principal allowed to perform the action</param>
    /// <param name="isDenied">Is the principal denied to perform the action</param>
    /// <param name="actionName">The name of the related action</param>
    /// <param name="allowControlClientID">Client ID of the control related to the allow permission for this action and principal</param>
    /// <param name="denyControlClientID">Client ID of the control related to the deny permission for this action and principal</param>
    /// <param name="permissionSetName">Name of the permission set related to this permission</param>
    public WcfPermission(
      string principalType,
      string principalID,
      string principalName,
      string principalTitle,
      string dataProviderName,
      bool isAllowed,
      bool isDenied,
      string actionName,
      string allowControlClientID,
      string denyControlClientID,
      string permissionSetName,
      string securedObjectId)
    {
      this.PrincipalType = principalType;
      this.PrincipalID = principalID;
      this.PrincipalName = principalName;
      this.PrincipalTitle = principalTitle;
      this.DataProviderName = dataProviderName;
      this.IsAllowed = isAllowed;
      this.IsDenied = isDenied;
      this.ActionName = actionName;
      this.AllowControlClientID = allowControlClientID;
      this.DenyControlClientID = denyControlClientID;
      this.PermissionSetName = permissionSetName;
      this.SecuredObjectId = securedObjectId;
    }

    /// <summary>The Principal type (user/role)</summary>
    [DataMember]
    public string PrincipalType { get; set; }

    /// <summary>The ID of the principal</summary>
    [DataMember]
    public string PrincipalID { get; set; }

    /// <summary>The name of the principal</summary>
    [DataMember]
    public string PrincipalName { get; set; }

    /// <summary>
    /// Gets or sets the principal title, the one displayed in the UI. If it's a user- would contain the first and last name.
    /// </summary>
    [DataMember]
    public string PrincipalTitle { get; set; }

    /// <summary>Name of the data provider related to the permission</summary>
    [DataMember]
    public string DataProviderName { get; set; }

    /// <summary>Is the principal allowed to perform the action</summary>
    [DataMember]
    public bool IsAllowed { get; set; }

    /// <summary>Is the principal denied to perform the action</summary>
    [DataMember]
    public bool IsDenied { get; set; }

    /// <summary>The name of the related action</summary>
    [DataMember]
    public string ActionName { get; set; }

    /// <summary>Gets or sets the action title.</summary>
    [DataMember]
    public string ActionTitle { get; set; }

    /// <summary>
    /// Client ID of the control related to the allow permission for this action and principal
    /// </summary>
    [DataMember]
    public string AllowControlClientID { get; set; }

    /// <summary>
    /// Client ID of the control related to the deny permission for this action and principal
    /// </summary>
    [DataMember]
    public string DenyControlClientID { get; set; }

    /// <summary>Name of the permission set related to this permission</summary>
    [DataMember]
    public string PermissionSetName { get; set; }

    /// <summary>Gets or sets the secured object pageId.</summary>
    [DataMember]
    public string SecuredObjectId { get; set; }

    /// <summary>Compares to.</summary>
    /// <param name="other">The other.</param>
    /// <returns></returns>
    public int CompareTo(WcfPermission other) => this.PrincipalTitle.CompareTo(other.PrincipalTitle);
  }
}
