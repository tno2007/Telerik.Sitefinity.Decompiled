// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ContentLinksConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>Defines metadata configuration settings.</summary>
  public class ContentLinksConfig : ModuleConfigBase
  {
    protected override void InitializeDefaultProviders(
      ConfigElementDictionary<string, DataProviderSettings> providers)
    {
      providers.Add(new DataProviderSettings((ConfigElement) providers)
      {
        Name = "OpenAccessDataProvider",
        Description = "A provider that stores contentlinks data using OpenAccess ORM.",
        ProviderType = typeof (OpenAccessContentLinksProvider)
      });
    }
  }
}
