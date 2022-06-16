// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Multisite.ISiteDataSource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;

namespace Telerik.Sitefinity.Multisite
{
  internal interface ISiteDataSource
  {
    Guid Id { get; }

    string Name { get; }

    string Provider { get; }

    string Title { get; }

    Guid OwnerSiteId { get; }

    bool IsDynamic { get; }

    IEnumerable<Guid> Sites { get; }
  }
}
