// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SitefinityClaimTypes
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Security.Claims
{
  /// <summary>The URI for claims that Sitefinity STS issues</summary>
  public static class SitefinityClaimTypes
  {
    public const string TokenId = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/tokenid";
    public const string UserId = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/userid";
    public const string Domain = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/domain";
    public const string Role = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/role";
    public const string IssueDate = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/issuedate";
    public const string LastLoginDate = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/lastlogindate";
    public const string Adjusted = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/adjusted";
    public const string StsType = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/ststype";
    public const string IsExternalUser = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/isexternaluser";
    public const string ExternalUserId = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/externaluserid";
    public const string ExternalUserName = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/externalusername";
    public const string ExternalUserNickName = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/externalusernickname";
    public const string ExternalUserEmail = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/externaluseremail";
    public const string ExternalUserPictureUrl = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/externaluserpictureurl";
    public const string RememberMe = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/rememberme";
    public const string SitefinityClaimsPrefix = "http://schemas.sitefinity.com/ws/2011/06/identity/claims/";
    internal const string SitefinityClaimsMapPrefix = "ClaimsMapping:";
  }
}
