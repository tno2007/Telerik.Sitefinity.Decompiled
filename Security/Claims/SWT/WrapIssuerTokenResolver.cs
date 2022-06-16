// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Claims.SWT.WrapIssuerTokenResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.ServiceModel.Security;
using System.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Security.Claims.SWT
{
  /// <summary>
  /// An IssuerTokenResolver responsible for handling WRAP tokens
  /// </summary>
  public class WrapIssuerTokenResolver : IssuerTokenResolver
  {
    private static readonly IDictionary<string, SecurityKey> keyMap = (IDictionary<string, SecurityKey>) new Dictionary<string, SecurityKey>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
    private static bool initialized = false;

    protected override bool TryResolveSecurityKeyCore(
      SecurityKeyIdentifierClause keyIdentifierClause,
      out SecurityKey key)
    {
      this.Initialize();
      if (keyIdentifierClause is KeyNameIdentifierClause identifierClause)
      {
        string keyName = identifierClause.KeyName;
        if (!WrapIssuerTokenResolver.keyMap.TryGetValue(keyName, out key))
        {
          string str = VirtualPathUtility.RemoveTrailingSlash(ClaimsManager.CurrentAuthenticationModule.GetIssuer());
          if (!keyName.Equals(str, StringComparison.OrdinalIgnoreCase) || !WrapIssuerTokenResolver.keyMap.TryGetValue("http://localhost", out key))
            throw new ConfigurationException("Missing configuration for the issuer of security tokens \"{0}\"".Arrange((object) keyName));
        }
        return true;
      }
      key = (SecurityKey) null;
      return false;
    }

    private void Initialize()
    {
      if (WrapIssuerTokenResolver.initialized)
        return;
      lock (WrapIssuerTokenResolver.keyMap)
      {
        if (WrapIssuerTokenResolver.initialized)
          return;
        foreach (SecurityTokenKeyElement securityTokenKeyElement in (IEnumerable<SecurityTokenKeyElement>) Config.Get<SecurityConfig>().SecurityTokenIssuers.Values)
        {
          string key = VirtualPathUtility.RemoveTrailingSlash(securityTokenKeyElement.Realm);
          byte[] symmetricKey = securityTokenKeyElement.Encoding != BinaryEncoding.Hexadecimal ? Convert.FromBase64String(securityTokenKeyElement.Key) : SecurityManager.HexToByte(securityTokenKeyElement.Key);
          WrapIssuerTokenResolver.keyMap.Add(key, (SecurityKey) new InMemorySymmetricSecurityKey(symmetricKey));
        }
        WrapIssuerTokenResolver.initialized = true;
      }
    }
  }
}
