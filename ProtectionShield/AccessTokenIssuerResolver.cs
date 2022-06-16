// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ProtectionShield.AccessTokenIssuerResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.ProtectionShield.Configuration;

namespace Telerik.Sitefinity.ProtectionShield
{
  /// <summary>Access token issuer resolver class</summary>
  internal class AccessTokenIssuerResolver
  {
    private static readonly Dictionary<Type, IAccessTokenIssuer> IssuersMapping = new Dictionary<Type, IAccessTokenIssuer>();
    private static object syncRoot = new object();

    /// <summary>Resolves the issuer.</summary>
    /// <returns><see cref="T:Telerik.Sitefinity.ProtectionShield.IAccessTokenIssuer" /> instance.</returns>
    public static IAccessTokenIssuer ResolveIssuer()
    {
      string accessTokenIssuer = Config.Get<ProtectionShieldConfig>().DefaultAccessTokenIssuer;
      if (string.IsNullOrWhiteSpace(accessTokenIssuer))
        throw new ArgumentNullException("Default access token issuer cannot be empty");
      ConfigElementDictionary<string, DataProviderSettings> accessTokenIssuers = Config.Get<ProtectionShieldConfig>().AccessTokenIssuers;
      return accessTokenIssuers != null && accessTokenIssuers.ContainsKey(accessTokenIssuer) ? AccessTokenIssuerResolver.InstantiateIssuer((IDataProviderSettings) accessTokenIssuers[accessTokenIssuer], typeof (IAccessTokenIssuer), ExceptionPolicyName.DataProviders) : throw new ArgumentException(string.Format("Access token issuers does not contain a issuer with name: {0}", (object) accessTokenIssuer));
    }

    private static IAccessTokenIssuer InstantiateIssuer(
      IDataProviderSettings issuerSettings,
      Type issuerType,
      ExceptionPolicyName policy)
    {
      IAccessTokenIssuer accessTokenIssuer;
      try
      {
        Type providerType = issuerSettings.ProviderType;
        accessTokenIssuer = issuerType.IsAssignableFrom(providerType) ? AccessTokenIssuerResolver.ResolveIssuerType(providerType) : throw new ArgumentException("Invalid type specified" + " " + issuerType.ToString());
        NameValueCollection parameters = issuerSettings.Parameters;
        accessTokenIssuer.Initialize(parameters);
      }
      catch (Exception ex)
      {
        int policy1 = (int) policy;
        if (!Exceptions.HandleException(ex, (ExceptionPolicyName) policy1))
          return (IAccessTokenIssuer) null;
        throw;
      }
      return accessTokenIssuer;
    }

    private static IAccessTokenIssuer ResolveIssuerType(Type type)
    {
      IAccessTokenIssuer accessTokenIssuer;
      if (!AccessTokenIssuerResolver.IssuersMapping.TryGetValue(type, out accessTokenIssuer))
      {
        lock (AccessTokenIssuerResolver.syncRoot)
        {
          if (!AccessTokenIssuerResolver.IssuersMapping.TryGetValue(type, out accessTokenIssuer))
          {
            if (!ObjectFactory.IsTypeRegistered(type))
              ObjectFactory.Container.RegisterType(type, (LifetimeManager) new ContainerControlledLifetimeManager());
            accessTokenIssuer = (IAccessTokenIssuer) ObjectFactory.Resolve(type);
            AccessTokenIssuerResolver.IssuersMapping.Add(type, accessTokenIssuer);
          }
        }
      }
      return accessTokenIssuer;
    }
  }
}
