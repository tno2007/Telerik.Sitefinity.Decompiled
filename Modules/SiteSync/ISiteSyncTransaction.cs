// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSync.ISiteSyncTransaction
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Publishing;

namespace Telerik.Sitefinity.SiteSync
{
  public interface ISiteSyncTransaction
  {
    /// <summary>The type of the main item in the transaction.</summary>
    string Type { get; set; }

    /// <summary>
    /// The data items that are being synchronized with the current transaction.
    /// </summary>
    IEnumerable<WrapperObject> Items { get; set; }

    IDictionary<string, string> Headers { get; }
  }
}
