// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Scheduling.Data.SchedulingMetadataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Scheduling.Model;
using Telerik.Sitefinity.SyncLock;

namespace Telerik.Sitefinity.Scheduling.Data
{
  /// <summary>
  /// This class implements the Scheduling ORM mappings for OpenAccess
  /// It is used by the Scheduling Data Provider for OpenAccess
  /// </summary>
  public class SchedulingMetadataSource : SecuredProviderMetadataSource
  {
    public SchedulingMetadataSource(IDatabaseMappingContext context)
      : base(context)
    {
    }

    protected override IList<IOpenAccessFluentMapping> BuildCustomMappings()
    {
      IList<IOpenAccessFluentMapping> accessFluentMappingList = base.BuildCustomMappings();
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new SchedulingFluentMapping(this.Context));
      accessFluentMappingList.Add((IOpenAccessFluentMapping) new SyncLockFluentMapping(this.Context));
      return accessFluentMappingList;
    }
  }
}
