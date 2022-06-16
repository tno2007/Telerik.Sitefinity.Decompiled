// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.SiteSyncTransaction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  internal abstract class SiteSyncTransaction : ISiteSyncTransaction
  {
    private readonly Lazy<Dictionary<string, string>> headers = new Lazy<Dictionary<string, string>>();

    /// <inheritdoc />
    public string Type { get; set; }

    /// <inheritdoc />
    public IEnumerable<WrapperObject> Items { get; set; }

    public IDictionary<string, string> Headers => (IDictionary<string, string>) this.headers.Value;
  }
}
