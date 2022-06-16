// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.Decorators.IUrlProviderDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent;

namespace Telerik.Sitefinity.Data.Decorators
{
  /// <summary>
  /// Defines interface for content data provider decorators.
  /// </summary>
  public interface IUrlProviderDecorator : ICloneable
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    UrlDataProviderBase DataProvider { get; set; }

    /// <summary>Creates new UrlData.</summary>
    /// <param name="urlType">Type of the URL.</param>
    /// <returns>The new UrlData object.</returns>
    UrlData CreateUrl(Type urlType);

    /// <summary>Creates new UrlData.</summary>
    /// <param name="urlType">Type of the URL.</param>
    /// <param name="pageId">The ID of the new UrlData.</param>
    /// <returns>The new UrlData object.</returns>
    UrlData CreateUrl(Type urlType, Guid id);

    /// <summary>Creates new UrlData.</summary>
    /// <returns>The new content item.</returns>
    T CreateUrl<T>() where T : UrlData, new();

    /// <summary>Creates new UrlData with the specified ID.</summary>
    /// <param name="pageId">The pageId of the new UrlData.</param>
    /// <returns>The new UrlData.</returns>
    T CreateUrl<T>(Guid id) where T : UrlData, new();

    /// <summary>Gets a UrlData with the specified ID.</summary>
    /// <param name="pageId">The ID to search for.</param>
    /// <returns>A UrlData entry.</returns>
    T GetUrl<T>(Guid id) where T : UrlData;

    /// <summary>Gets a query for UrlData.</summary>
    /// <returns>The query for UrlData.</returns>
    IQueryable<T> GetUrls<T>() where T : UrlData;

    /// <summary>Gets a query for UrlData.</summary>
    /// <param name="urlType">The actual type of the UrlData.</param>
    /// <returns>The query for UrlData.</returns>
    IQueryable<UrlData> GetUrls(Type urlType);

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The UrlData to delete.</param>
    void Delete(UrlData item);
  }
}
