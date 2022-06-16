// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Versioning.Data.VersioningMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Versioning.Model;

namespace Telerik.Sitefinity.Versioning.Data
{
  /// <summary>
  /// This class implements the Versioning OpenAccess ORM mappings
  /// </summary>
  public class VersioningMetadataSource : SecuredProviderMetadataSource
  {
    public VersioningMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new VersioningFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
