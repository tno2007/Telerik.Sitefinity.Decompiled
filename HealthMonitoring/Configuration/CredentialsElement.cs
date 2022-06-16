// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.HealthMonitoring.Configuration.CredentialsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;

namespace Telerik.Sitefinity.HealthMonitoring.Configuration
{
  public class CredentialsElement : ConfigurationElement
  {
    [ConfigurationProperty("domain", DefaultValue = "")]
    public string Domain
    {
      get => (string) this["domain"];
      set => this["domain"] = (object) value;
    }

    [ConfigurationProperty("userName", DefaultValue = "")]
    public string UserName
    {
      get => (string) this["userName"];
      set => this["userName"] = (object) value;
    }

    [ConfigurationProperty("password", DefaultValue = "")]
    public string Password
    {
      get => (string) this["password"];
      set => this["password"] = (object) value;
    }
  }
}
