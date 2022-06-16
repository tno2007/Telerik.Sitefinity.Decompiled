// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenDeviceTracker
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Net;
using System.Web;
using Telerik.Sitefinity.Utilities;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access tokens device tracker</summary>
  /// <remarks>
  /// Multithreaded Singleton:
  /// <a href="https://msdn.microsoft.com/en-us/library/ff650316.aspx">See more</a>
  /// </remarks>
  internal sealed class AccessTokenDeviceTracker
  {
    private static volatile AccessTokenDeviceTracker instance;
    private static object syncRoot = new object();

    private AccessTokenDeviceTracker()
    {
    }

    /// <summary>
    /// Gets an instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenInvitationsEmailSender" /> class.
    /// </summary>
    /// <returns>Instance of the <see cref="T:Telerik.Sitefinity.ProtectionShield.AccessTokenInvitationsEmailSender" /> class.</returns>
    public static AccessTokenDeviceTracker Get()
    {
      if (AccessTokenDeviceTracker.instance == null)
      {
        lock (AccessTokenDeviceTracker.syncRoot)
        {
          if (AccessTokenDeviceTracker.instance == null)
            AccessTokenDeviceTracker.instance = new AccessTokenDeviceTracker();
        }
      }
      return AccessTokenDeviceTracker.instance;
    }

    /// <summary>Track device for specified token</summary>
    /// <param name="token">The token.</param>
    /// <param name="httpRequest">The request.</param>
    public void TrackDevice(string token, HttpRequestBase httpRequest)
    {
      if (token.IsNullOrWhitespace())
        throw new ArgumentException("Token cannot be null, empty or whitespace.");
      IPAddress ipAddress = httpRequest != null ? httpRequest.GetIpAddress() : throw new ArgumentException("Request cannot be null.");
      string userIpAddress = ipAddress != null ? ipAddress.ToString() : string.Empty;
      string userPlatform = httpRequest.GetUserPlatform();
      string fullBrowserName = httpRequest.Browser.Browser + " " + httpRequest.Browser.Version;
      this.CreateAccessTokenDeviceInternal(token, userPlatform, fullBrowserName, userIpAddress);
    }

    private void CreateAccessTokenDeviceInternal(
      string token,
      string platform,
      string fullBrowserName,
      string userIpAddress)
    {
      if (AccessTokenIssuerResolver.ResolveIssuer().GetAccessToken(token) == null)
        return;
      ProtectionShieldManager manager = ProtectionShieldManager.GetManager();
      manager.CreateAccessTokenDevice(token, platform, fullBrowserName, userIpAddress);
      manager.SaveChanges();
    }
  }
}
