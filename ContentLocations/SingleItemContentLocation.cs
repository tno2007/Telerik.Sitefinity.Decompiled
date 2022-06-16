// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.SingleItemContentLocation
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.ContentLocations.Model;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Provides content location information in Sitefinity and the ability to check if a particular item matches it.
  /// </summary>
  internal class SingleItemContentLocation : ContentLocation
  {
    private ContentLocationSingleItemFilter filter;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.ContentLocations.SingleItemContentLocation" /> class.
    /// </summary>
    /// <param name="contentLocation">Content location data item</param>
    /// <param name="parent">Content locations container the item belongs to</param>
    /// <param name="filter">Single item location filter</param>
    public SingleItemContentLocation(
      ContentLocationDataItem contentLocation,
      ContentTypeLocationsContainer parent,
      ContentLocationSingleItemFilter filter)
      : base(contentLocation, parent)
    {
      this.filter = filter;
    }

    /// <inheritdoc />
    public override bool IsMatch(Guid itemId) => this.filter.ItemIds.Contains(itemId.ToString());

    /// <inheritdoc />
    public override bool IsSingleItem => true;

    /// <inheritdoc />
    public override int Priority => -1;

    internal new ContentTypeLocationsContainer Parent { get; set; }
  }
}
