// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.IContentManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Adds Content types specific methods to the base manager interface
  /// </summary>
  public interface IContentManager : IManager, IDisposable, IProviderResolver
  {
    /// <summary>Get url from a locatable item</summary>
    /// <param name="item">Locatable item</param>
    /// <returns>Url of the locatable item</returns>
    string GetItemUrl(ILocatable item);

    /// <summary>Retrieve a content item by its url, ignoring status</summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    IDataItem GetItemFromUrl(Type itemType, string url, out string redirectUrl);

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl);

    /// <summary>Gets the items.</summary>
    /// <typeparam name="TItem">The type of the item.</typeparam>
    /// <returns></returns>
    IQueryable<TItem> GetItems<TItem>() where TItem : IContent;

    /// <summary>Recompiles the URLs of the item.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <remarks>
    /// Adds UrlData to the urls field of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.ILocatable" /> compiled from the item's Provider urlFormat.
    /// </remarks>
    void RecompileItemUrls<TItem>(TItem item) where TItem : ILocatable;

    /// <summary>
    /// Adds an <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> item to the current URLs collection for this item.
    /// </summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The content item.</param>
    /// <param name="url">The URL string value that should be added.</param>
    void AddItemUrl<T>(T item, string url, bool isDefault = true, bool redirectToDefault = false) where T : ILocatable;

    /// <summary>
    /// Removes all urls from the item satisfying the condition that is checked in the predicate function.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    void RemoveItemUrls<TItem>(TItem item, Func<UrlData, bool> predicate) where TItem : ILocatable;

    /// <summary>Clears the Urls collection for this item.</summary>
    /// <param name="excludeDefault">if set to <c>true</c> default urls will not be cleared.</param>
    void ClearItemUrls<TItem>(TItem item, bool excludeDefault = false) where TItem : ILocatable;

    /// <summary>Recompiles the and validate urls.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="content">The content.</param>
    void RecompileAndValidateUrls<TContent>(TContent content) where TContent : ILocatable;

    /// <summary>Validates the URL constraints.</summary>
    /// <typeparam name="TContent">The type of the content.</typeparam>
    /// <param name="item">The item.</param>
    void ValidateUrlConstraints<TContent>(TContent item) where TContent : ILocatable;
  }
}
