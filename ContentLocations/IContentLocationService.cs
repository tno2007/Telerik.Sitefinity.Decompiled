// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.IContentLocationService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Defines functions for resolving content items locations
  /// </summary>
  public interface IContentLocationService
  {
    /// <summary>
    /// Gets the default (canonical) location where the specified item could be opened.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="itemProvider">The item provider.</param>
    /// <param name="itemId">The item id.</param>
    /// <param name="culture">The culture.</param>
    /// <returns><typeparamref name="IContentItemLocation" /> item, or null if no location is found.</returns>
    IContentItemLocation GetItemDefaultLocation(
      Type itemType,
      string itemProvider,
      Guid itemId,
      CultureInfo culture = null);

    /// <summary>
    /// Gets the default (canonical) location where the specified item could be opened.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns><typeparamref name="IContentItemLocation" /> item, or null if no location is found.</returns>
    IContentItemLocation GetItemDefaultLocation(
      IDataItem item,
      CultureInfo culture = null);

    IEnumerable<IContentItemLocation> GetItemLocations(
      Type itemType,
      string itemProvider,
      Guid itemId,
      CultureInfo culture = null);

    IEnumerable<IContentItemLocation> GetItemLocations(
      IDataItem item,
      CultureInfo culture = null);
  }
}
