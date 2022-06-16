// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Builder.Data.ModuleBuilderMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.OpenAccessMapping;

namespace Telerik.Sitefinity.DynamicModules.Builder.Data
{
  /// <summary>
  /// A concrete implementation of the <see cref="T:Telerik.Sitefinity.Data.OA.SecuredProviderMetadataSource" /> for the module builder module.
  /// </summary>
  internal class ModuleBuilderMetadataSource : SecuredProviderMetadataSource
  {
    /// <summary>
    /// Creates a new instance of the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Data.ModuleBuilderMetadataSource" /> type.
    /// </summary>
    /// <param name="context">
    /// An instance of the <see cref="T:Telerik.Sitefinity.Model.IDatabaseMappingContext" /> used to build mappings for the
    /// ecommerce catalog module.
    /// </param>
    public ModuleBuilderMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    /// <summary>
    /// Builds the custom Telerik OpenAccess ORM mappings of the persistent classes for the module builder module.
    /// </summary>
    /// <returns>A collection of the <see cref="T:Telerik.Sitefinity.Model.IOpenAccessFluentMapping" /> objects.</returns>
    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new CommonFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new ModuleBuilderFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
