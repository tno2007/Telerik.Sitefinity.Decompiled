// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.ContentLocations.ContentLocationsDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations.Model;
using Telerik.Sitefinity.Data;

namespace Telerik.Sitefinity.ContentLocations
{
  /// <summary>
  /// Represents a base class for content locations data providers.
  /// </summary>
  public abstract class ContentLocationsDataProvider : DataProviderBase
  {
    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> instance.
    /// </summary>
    internal abstract ContentLocationDataItem CreateLocation();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> with the given id.
    /// </summary>
    /// <param name="locationId">The location id.</param>
    /// <returns></returns>
    internal abstract ContentLocationDataItem CreateLocation(Guid locationId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> for removal.
    /// </summary>
    /// <param name="location">The location to delete.</param>
    internal abstract void Delete(ContentLocationDataItem location);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> object with the given id.
    /// </summary>
    /// <param name="locationId">The location id.</param>
    internal abstract ContentLocationDataItem GetLocation(Guid locationId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationDataItem" /> objects.
    /// </returns>
    internal abstract IQueryable<ContentLocationDataItem> GetLocations();

    /// <summary>
    /// Gets the content filters of a specific content location.
    /// </summary>
    /// <param name="contentLocationId">The id of the content location the filters belong to.</param>
    /// <returns></returns>
    internal virtual IQueryable<ContentLocationFilterDataItem> GetContentFilters(
      Guid contentLocationId)
    {
      return this.GetContentFilters().Where<ContentLocationFilterDataItem>((Expression<Func<ContentLocationFilterDataItem, bool>>) (f => f.ContentLocation.Id == contentLocationId));
    }

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> instance.
    /// </summary>
    internal abstract ContentLocationFilterDataItem CreateContentFilter();

    /// <summary>
    /// Creates a new <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> with the given id.
    /// </summary>
    /// <param name="contentFilterId">The content id.</param>
    /// <returns></returns>
    internal abstract ContentLocationFilterDataItem CreateContentFilter(
      Guid contentFilterId);

    /// <summary>
    /// Marks a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> for removal.
    /// </summary>
    /// <param name="contentFilter">The ContentFilter to delete.</param>
    internal abstract void Delete(ContentLocationFilterDataItem contentFilter);

    /// <summary>
    /// Gets a <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> object with the given id.
    /// </summary>
    /// <param name="contentFilterId">The content filter id.</param>
    internal abstract ContentLocationFilterDataItem GetContentFilter(
      Guid contentFilterId);

    /// <summary>
    /// Gets an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> objects.
    /// </summary>
    /// <returns>
    /// Returns an <see cref="T:System.Linq.IQueryable" /> of <see cref="T:Telerik.Sitefinity.ContentLocations.Model.ContentLocationFilterDataItem" /> objects.
    /// </returns>
    internal abstract IQueryable<ContentLocationFilterDataItem> GetContentFilters();

    /// <inheritdoc />
    public override object GetItem(Type itemType, Guid id)
    {
      if (typeof (ContentLocationDataItem).IsAssignableFrom(itemType))
        return (object) this.GetLocation(id);
      return typeof (ContentLocationFilterDataItem).IsAssignableFrom(itemType) ? (object) this.GetContentFilter(id) : base.GetItem(itemType, id);
    }

    /// <inheritdoc />
    public override void DeleteItem(object item)
    {
      switch (item)
      {
        case ContentLocationDataItem _:
          this.Delete((ContentLocationDataItem) item);
          break;
        case ContentLocationFilterDataItem _:
          this.Delete((ContentLocationFilterDataItem) item);
          break;
        default:
          base.DeleteItem(item);
          break;
      }
    }

    /// <summary>Get a list of types served by this manager</summary>
    public override Type[] GetKnownTypes() => new Type[2]
    {
      typeof (ContentLocationDataItem),
      typeof (ContentLocationFilterDataItem)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => "LocationsDataProvider";
  }
}
