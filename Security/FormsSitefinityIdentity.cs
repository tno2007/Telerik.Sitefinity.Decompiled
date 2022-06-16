// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.FormsSitefinityIdentity
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Security;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Security
{
  /// <summary>Identity for Forms authentication</summary>
  public class FormsSitefinityIdentity : SitefinityIdentity
  {
    /// <summary>Gets the Forms Authentication ticket.</summary>
    public FormsAuthenticationTicket Ticket { get; private set; }

    /// <summary>Gets the last log-in timestamp as string.</summary>
    internal string LastLoginTimeStampString { get; private set; }

    /// <summary>
    /// Gets a value indicating whether roles have changed since the cookie was created.
    /// </summary>
    /// <value><c>true</c> if roles have changed; otherwise, <c>false</c>.</value>
    internal bool RolesChanged { get; private set; }

    /// <summary>
    /// Gets the date and time that the roles cookie was issued.
    /// </summary>
    internal DateTime RolesCookieIssueDate { get; private set; }

    /// <summary>
    /// Gets the date and time when the roles cookie will expire.
    /// </summary>
    internal DateTime RolesCookieExpireDate { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the roles cookie has expired.
    /// </summary>
    private bool IsRolesCookieExpired => this.RolesCookieIssueDate < SecurityManager.LastPermissionChange.ToUniversalTime() || this.RolesCookieExpireDate < DateTime.UtcNow;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FormsSitefinityIdentity" /> class from an authentication ticket.
    /// </summary>
    /// <param name="ticket">The Forms Authentication ticket.</param>
    public FormsSitefinityIdentity(FormsAuthenticationTicket ticket)
    {
      if (ticket != null)
      {
        this.isAuthenticated = true;
        this.authenticationType = "Forms";
        this.Ticket = ticket;
        ClaimsManager.SetName((ClaimsIdentity) this, this.Ticket.Name);
        string[] strArray = this.Ticket.UserData.Split(';');
        ClaimsManager.SetUserId((ClaimsIdentity) this, strArray[0]);
        ClaimsManager.SetMembershipProvider((ClaimsIdentity) this, strArray[1]);
        this.LastLoginTimeStampString = strArray.Length > 2 ? strArray[2] : string.Empty;
        ClaimsManager.SetLastLoginDate((ClaimsIdentity) this, SecurityManager.ParseLoginTimeStamp(this.LastLoginTimeStampString));
        ClaimsManager.SetSitefinityRoles((ClaimsIdentity) this, this.GetRoles());
      }
      else
        this.SetAnonymous();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Security.FormsSitefinityIdentity" /> class from an identity.
    /// </summary>
    /// <param name="identity">An existing identity</param>
    public FormsSitefinityIdentity(ClaimsIdentity identity)
      : base(identity)
    {
      if (!this.IsAuthenticated)
        return;
      this.RenewRolesCookie();
    }

    /// <summary>
    /// Returns a new instance of <see cref="T:Telerik.Sitefinity.Security.FormsSitefinityIdentity" /> class containing the same properties/claims.
    /// </summary>
    /// <returns>An instance of <see cref="T:Telerik.Sitefinity.Security.FormsSitefinityIdentity" /></returns>
    public override ClaimsIdentity Clone() => (ClaimsIdentity) new FormsSitefinityIdentity((ClaimsIdentity) this);

    /// <summary>Serializes the roles of the identity.</summary>
    /// <returns>Empty string if there are no Roles, otherwise the serialized roles.</returns>
    public string SerializeRoles()
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (this.Roles != null)
      {
        StringBuilder stringBuilder2 = stringBuilder1;
        DateTime dateTime = this.RolesCookieIssueDate;
        string str1 = dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder2.Append(str1);
        stringBuilder1.Append(";");
        StringBuilder stringBuilder3 = stringBuilder1;
        dateTime = this.RolesCookieExpireDate;
        string str2 = dateTime.ToString("u", (IFormatProvider) CultureInfo.InvariantCulture);
        stringBuilder3.Append(str2);
        stringBuilder1.Append(";");
        stringBuilder1.Append(this.UserId.ToString());
        stringBuilder1.Append(";");
        foreach (RoleInfo role in this.Roles)
        {
          stringBuilder1.Append((object) role.Id);
          stringBuilder1.Append(";");
          stringBuilder1.Append(role.Name);
          stringBuilder1.Append(";");
          stringBuilder1.Append(role.Provider);
          stringBuilder1.Append(";");
        }
      }
      return stringBuilder1.ToString();
    }

    /// <summary>Gets the identity roles</summary>
    /// <returns>Returns a roles collection</returns>
    protected override IEnumerable<RoleInfo> GetRoles()
    {
      if (SystemManager.CurrentHttpContext != null && SystemManager.CurrentHttpContext.Request != null && SystemManager.CurrentHttpContext.Request.Cookies != null)
      {
        string serialized = string.Empty;
        HttpCookie cookie = SystemManager.CurrentHttpContext.Request.Cookies[Config.Get<SecurityConfig>().RolesCookieName];
        if (cookie != null)
        {
          if (!string.IsNullOrWhiteSpace(cookie.Value))
          {
            try
            {
              serialized = SecurityManager.DecryptData(cookie.Value);
            }
            catch (CryptographicException ex)
            {
              SecurityManager.DeleteAuthCookies();
            }
          }
        }
        if (!string.IsNullOrWhiteSpace(serialized))
        {
          IEnumerable<RoleInfo> roles;
          if (this.TryDeserializeRolesFromRolesCookieString(serialized, out roles))
          {
            this.RenewRolesCookieIfExpired();
            return roles;
          }
          SecurityManager.DeleteAuthCookies();
        }
      }
      this.RenewRolesCookie();
      return base.GetRoles();
    }

    private bool TryDeserializeRolesFromRolesCookieString(
      string serialized,
      out IEnumerable<RoleInfo> roles)
    {
      string[] strArray = serialized.Split(';');
      Guid result;
      if (strArray.Length < 2 || !Guid.TryParse(strArray[2], out result) || result != this.UserId)
      {
        roles = (IEnumerable<RoleInfo>) null;
        return false;
      }
      this.RolesCookieIssueDate = DateTime.Parse(strArray[0], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
      this.RolesCookieExpireDate = DateTime.Parse(strArray[1], (IFormatProvider) CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal);
      List<RoleInfo> roleInfoList = new List<RoleInfo>();
      if (strArray.Length > 3)
      {
        for (int index = 3; index < strArray.Length; index += 3)
        {
          if (strArray.Length >= index + 3)
          {
            string input = strArray[index];
            string str1 = strArray[index + 1];
            string str2 = strArray[index + 2];
            Guid empty = Guid.Empty;
            ref Guid local = ref empty;
            if (Guid.TryParse(input, out local) && !string.IsNullOrWhiteSpace(str1) && !string.IsNullOrWhiteSpace(str2))
              roleInfoList.Add(new RoleInfo()
              {
                Id = empty,
                Name = str1,
                Provider = str2
              });
          }
        }
      }
      roles = (IEnumerable<RoleInfo>) roleInfoList;
      return true;
    }

    private void RenewRolesCookieIfExpired()
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      if (this.IsRolesCookieExpired)
      {
        this.RenewRolesCookie();
      }
      else
      {
        if (!securityConfig.RolesCookieSlidingExpiration)
          return;
        DateTime utcNow = DateTime.UtcNow;
        if (!(utcNow - this.RolesCookieIssueDate > this.RolesCookieExpireDate - utcNow))
          return;
        this.RenewRolesCookie();
      }
    }

    private void RenewRolesCookie()
    {
      SecurityConfig securityConfig = Config.Get<SecurityConfig>();
      this.RolesCookieIssueDate = DateTime.UtcNow;
      this.RolesCookieExpireDate = this.RolesCookieIssueDate.Add(securityConfig.RolesCookieTimeout);
      this.RolesChanged = true;
    }
  }
}
