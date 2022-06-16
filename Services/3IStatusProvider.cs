// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.IStatusProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;

namespace Telerik.Sitefinity.Services
{
  /// <summary>Status provider</summary>
  internal interface IStatusProvider
  {
    /// <summary>Gets provider status name</summary>
    string Name { get; }

    /// <summary>
    /// Gets behaviour of this provider, e.g. Workflow or Additional
    /// </summary>
    StatusBehaviour Behaviour { get; }

    /// <summary>
    /// Gets priority of this provider, relating to possitioning the status in the list of all additional statuses
    /// </summary>
    int Priority { get; }

    bool IsTypeSupported(Type itemType);

    IItemStatusData GetItem(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid id);

    IEnumerable<IItemStatusData> GetItems(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid[] ids);

    IEnumerable<IItemStatusData> GetItemsByFilter(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      string filterName);

    IWarningData GetWarning(
      Type itemType,
      string itemProvider,
      CultureInfo culture,
      string rootKey,
      Guid id);

    /// <summary>
    /// Gets the status filters, that are supported by the provider.
    /// </summary>
    /// <returns>The status filters.</returns>
    IEnumerable<IStatusFilter> GetFilters();
  }
}
