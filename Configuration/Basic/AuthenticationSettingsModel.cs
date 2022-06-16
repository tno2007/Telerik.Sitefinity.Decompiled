// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.AuthenticationSettingsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Security.Configuration;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>Represents the model for authentication mode.</summary>
  [DataContract]
  public class AuthenticationSettingsModel
  {
    private SecurityConfig securityConfig;

    /// <summary>Gets or sets the authentication mode.</summary>
    /// <value>Claims or Forms.</value>
    [DataMember]
    public int AuthenticationMode
    {
      get => (int) this.SecurityConfig.AuthenticationMode;
      set => this.SecurityConfig.AuthenticationMode = (Telerik.Sitefinity.Security.Configuration.AuthenticationMode) value;
    }

    /// <summary>Gets the reference to the security config.</summary>
    public SecurityConfig SecurityConfig
    {
      get
      {
        if (this.securityConfig == null)
          this.securityConfig = Config.Get<SecurityConfig>();
        return this.securityConfig;
      }
    }
  }
}
