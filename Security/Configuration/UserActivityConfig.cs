// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Configuration.UserActivityConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Configuration;
using System.Security;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Security.Configuration
{
  /// <summary>
  /// Configuration section for UserActivityProvider settings.
  /// </summary>
  public class UserActivityConfig : ConfigSection
  {
    /// <summary>Gets a collection of data provider settings.</summary>
    [DescriptionResource(typeof (ConfigDescriptions), "Providers")]
    [ConfigurationProperty("providers")]
    public virtual ConfigElementDictionary<string, DataProviderSettings> Providers => (ConfigElementDictionary<string, DataProviderSettings>) this["providers"];

    protected override void OnPropertiesInitialized()
    {
      bool flag = false;
      foreach (DataProviderSettings providerSettings in (IEnumerable<DataProviderSettings>) this.Providers.Values)
      {
        if (!typeof (OpenAccessUserActivityProvider).IsAssignableFrom(providerSettings.ProviderType))
          throw new SecurityException("Unsupported provider");
        flag = true;
      }
      if (flag)
        return;
      this.Providers.Add(new DataProviderSettings((ConfigElement) this.Providers)
      {
        Name = "OpenAccessUserActivityProvider",
        Description = "Provider that manages UserActivity records",
        ProviderType = typeof (OpenAccessUserActivityProvider)
      });
    }
  }
}
