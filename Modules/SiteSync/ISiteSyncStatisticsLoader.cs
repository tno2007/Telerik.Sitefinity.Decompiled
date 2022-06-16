// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ISiteSyncStatisticsLoader
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Publishing;
using Telerik.Sitefinity.SiteSync.Serialization;

namespace Telerik.Sitefinity.SiteSync
{
  internal interface ISiteSyncStatisticsLoader
  {
    IEnumerable<WrapperObject> LoadStatistics(
      ISiteSyncExportContext context,
      object item,
      ICommonItemLoader commonItemLoader,
      ISiteSyncSerializer serializer,
      string itemType,
      string providerName,
      string language);
  }
}
