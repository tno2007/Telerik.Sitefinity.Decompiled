// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Extensions.SitefinityModuleExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data.Statistic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Modules.Extensions
{
  /// <summary>
  /// Contains extension methods for <see cref="T:Telerik.Sitefinity.Services.IModule" />
  /// </summary>
  public static class SitefinityModuleExtensions
  {
    /// <summary>
    /// Gets all the landing <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> ids for each of the module`s registered content types.
    /// </summary>
    /// <param name="module">Sitefinity module</param>
    /// <returns><see cref="T:System.Collections.Generic.IDictionary`2" /> that contains the landing <see cref="T:Telerik.Sitefinity.Pages.Model.PageNode" /> id of each type.</returns>
    public static IDictionary<Type, Guid> GetTypeLandingPageMappings(
      this IModule module)
    {
      IDictionary<Type, Guid> landingPageMappings = (IDictionary<Type, Guid>) null;
      if (module is ContentModuleBase)
      {
        ContentModuleBase module1 = (ContentModuleBase) module;
        landingPageMappings = !(module1.LandingPageId != Guid.Empty) ? module1.GetTypeLandingPagesMapping() : (IDictionary<Type, Guid>) module1.GetKnownTypes().ToDictionary<Type, Type, Guid>((Func<Type, Type>) (t => t), (Func<Type, Guid>) (t => module.LandingPageId));
      }
      else if (module is DynamicAppModule)
      {
        DynamicAppModule dynamicModule = (DynamicAppModule) module;
        IDynamicModule dynamicModule1 = ModuleBuilderManager.GetModules().Active().FirstOrDefault<IDynamicModule>((Func<IDynamicModule, bool>) (x => x.Name == dynamicModule.Name));
        if (dynamicModule1 != null)
          landingPageMappings = (IDictionary<Type, Guid>) dynamicModule1.Types.ToDictionary<IDynamicModuleType, Type, Guid>((Func<IDynamicModuleType, Type>) (t => TypeResolutionService.ResolveType(t.FullTypeName)), (Func<IDynamicModuleType, Guid>) (t => t.PageId));
      }
      else if (module is IStatisticSupportModule)
        landingPageMappings = (IDictionary<Type, Guid>) ((IStatisticSupportModule) module).GetTypeInfos().ToDictionary<IStatisticSupportTypeInfo, Type, Guid>((Func<IStatisticSupportTypeInfo, Type>) (t => t.Type), (Func<IStatisticSupportTypeInfo, Guid>) (t => t.LandingPages.Single<StatisticLandingPageInfo>().PageId));
      return landingPageMappings;
    }
  }
}
