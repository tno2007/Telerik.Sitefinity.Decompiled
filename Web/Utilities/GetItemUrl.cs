// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.Utilities.GetItemUrl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Web.Utilities
{
  /// <summary>
  /// Delegate for processing item information. Must return the item URL.
  /// </summary>
  /// <param name="providerName">The name of data provider.</param>
  /// <param name="id">ID of the item.</param>
  /// <param name="resolveAsAbsoluteUrl">
  /// Specifies if URLs should be resolved as absolute paths.
  /// </param>
  /// <param name="status">Requested status</param>
  /// <returns>Resolved URL.</returns>
  public delegate string GetItemUrl(
    string providerName,
    Guid id,
    bool resolveAsAbsoluteUrl,
    ContentLifecycleStatus status);
}
