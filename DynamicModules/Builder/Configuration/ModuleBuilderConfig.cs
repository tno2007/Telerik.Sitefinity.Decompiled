// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Configuration.ModuleBuilderConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Specialized;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.DynamicModules.Builder.Data;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.DynamicModules.Builder.Configuration
{
  /// <summary>
  /// The configuration class for the module builder module.
  /// </summary>
  internal class ModuleBuilderConfig : ModuleConfigBase
  {
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      if (this.Providers.Count != 0)
        return;
      this.Providers.Add(new DataProviderSettings()
      {
        Name = "OpenAccessProvider",
        Description = "The data provider for module builder module based on the Telerik OpenAccess ORM.",
        ProviderType = typeof (OpenAccessModuleBuilderDataProvider),
        Parameters = new NameValueCollection()
        {
          {
            "applicationName",
            "/ModuleBuilder"
          }
        }
      });
    }
  }
}
