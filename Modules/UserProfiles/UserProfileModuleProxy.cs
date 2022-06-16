// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.UserProfileModuleProxy
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Modules.UserProfiles.Configuration;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.UserProfiles
{
  internal class UserProfileModuleProxy : IExportableModule
  {
    private ConfigSection moduleConfig;

    /// <inheritdoc />
    public string ModuleName => new UserProfilesStructureTransfer().Area;

    /// <inheritdoc />
    public ConfigSection ModuleConfig
    {
      get
      {
        if (this.moduleConfig == null)
          this.moduleConfig = (ConfigSection) Config.Get<UserProfilesConfig>();
        return this.moduleConfig;
      }
    }

    /// <inheritdoc />
    public IList<MetaType> GetModuleMetaTypes()
    {
      MetadataManager manager = MetadataManager.GetManager();
      List<MetaType> moduleMetaTypes = new List<MetaType>();
      foreach (Type moduleType in (IEnumerable<Type>) this.GetModuleTypes())
      {
        MetaType metaType = manager.GetMetaType(moduleType);
        if (metaType != null)
          moduleMetaTypes.Add(metaType);
      }
      return (IList<MetaType>) moduleMetaTypes;
    }

    private IList<Type> GetModuleTypes()
    {
      IList<Type> moduleTypes = (IList<Type>) new List<Type>();
      foreach (ConfigElement profileTypesSetting in (ConfigElementCollection) Config.Get<UserProfilesConfig>().ProfileTypesSettings)
      {
        Type type = TypeResolutionService.ResolveType(profileTypesSetting.GetKey(), false);
        if (type != (Type) null)
          moduleTypes.Add(type);
      }
      return moduleTypes;
    }
  }
}
