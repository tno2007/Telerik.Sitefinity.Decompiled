// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.SitefinityEnvironmentElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.Configuration
{
  /// <summary>
  /// Represents the <c>environment</c> configuration element within the <c>configuration/telerik/sitefinity</c>
  /// configuration section, used to specify settings like a specific hosting platform.
  /// </summary>
  public class SitefinityEnvironmentElement : ConfigurationElement
  {
    /// <summary>
    /// Specifies a hosting platform that requires specific handling by Sitefinity, e.g. Windows Azure Platform.
    /// </summary>
    [ConfigurationProperty("platform")]
    public SitefinityEnvironmentPlatform Platform
    {
      get => (SitefinityEnvironmentPlatform) this["platform"];
      set => this["platform"] = (object) value;
    }

    [ConfigurationProperty("flags")]
    public SitefinityEnvironmentFlags Flags
    {
      get => (SitefinityEnvironmentFlags) this["flags"];
      set => this["flags"] = (object) value;
    }
  }
}
