// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Pages.PagesModuleProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.Pages.Configuration;
using Telerik.Sitefinity.Pages.Model;

namespace Telerik.Sitefinity.Modules.Pages
{
  internal class PagesModuleProxy : IExportableModule
  {
    private ConfigSection moduleConfig;

    /// <inheritdoc />
    public string ModuleName => new PagesStructureTransfer().Area;

    /// <inheritdoc />
    public ConfigSection ModuleConfig
    {
      get
      {
        if (this.moduleConfig == null)
          this.moduleConfig = (ConfigSection) Config.Get<PagesConfig>();
        return this.moduleConfig;
      }
    }

    /// <inheritdoc />
    public IList<MetaType> GetModuleMetaTypes()
    {
      MetaType metaType = MetadataManager.GetManager().GetMetaType(typeof (PageNode));
      if (metaType == null)
        return (IList<MetaType>) new MetaType[0];
      return (IList<MetaType>) new MetaType[1]
      {
        metaType
      };
    }
  }
}
