// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Basic.UserAuthenticationSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.SiteSettings.Basic
{
  /// <summary>
  /// Represents the contract for authentication mode settings.
  /// </summary>
  [DataContract]
  public class UserAuthenticationSettingsContract : ISettingsDataContract
  {
    /// <summary>Gets or sets the authentication mode.</summary>
    /// <value>Claims or Forms.</value>
    [DataMember]
    public int AuthenticationMode { get; set; }

    public void LoadDefaults(bool forEdit = false) => this.AuthenticationMode = (int) (!forEdit ? Config.Get<SecurityConfig>() : ConfigManager.GetManager().GetSection<SecurityConfig>()).AuthenticationMode;

    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      SecurityConfig section = manager.GetSection<SecurityConfig>();
      section.AuthenticationMode = (Telerik.Sitefinity.Security.Configuration.AuthenticationMode) this.AuthenticationMode;
      manager.SaveSection((ConfigSection) section);
      SystemManager.RestartApplication(OperationReason.FromKey("UserAuthenticationChange"), SystemRestartFlags.AttemptFullRestart);
    }
  }
}
