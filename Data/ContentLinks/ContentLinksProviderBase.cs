// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ContentLinksProviderBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Model.ContentLinks;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>
  /// Represents the basic provider for storing content links
  /// </summary>
  public abstract class ContentLinksProviderBase : DataProviderBase
  {
    private Type[] acceptedTypes = new Type[1]
    {
      typeof (ContentLink)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => "ContentLinksProvider";

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>Gets a content link by id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract ContentLink GetContentLink(Guid id);

    /// <summary>Gets the content links.</summary>
    /// <returns></returns>
    public abstract IQueryable<ContentLink> GetContentLinks();

    /// <summary>Deletes the specified content link.</summary>
    /// <param name="contentLink">The content link.</param>
    public abstract void Delete(ContentLink contentLink);

    /// <summary>Creates the content link.</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public abstract ContentLink CreateContentLink(
      string propertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType);

    /// <summary>Creates the content link.</summary>
    /// <param name="id">The id of the created content link</param>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="childItemId">The child item id.</param>
    /// <param name="parentItemProviderName">Name of the parent item provider.</param>
    /// <param name="childItemProviderName">Name of the child item provider.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="childItemType">Type of the child item.</param>
    /// <returns></returns>
    public abstract ContentLink CreateContentLink(
      Guid id,
      string propertyName,
      Guid parentItemId,
      Guid childItemId,
      string parentItemProviderName,
      string childItemProviderName,
      Type parentItemType,
      Type childItemType);

    /// <summary>Gets the content links.</summary>
    /// <param name="parentItemId">The parent item id.</param>
    /// <param name="parentItemType">Type of the parent item.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public abstract IQueryable<ContentLink> GetContentLinks(
      Guid parentItemId,
      Type parentItemType,
      string propertyName);

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The pageId.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="pageId">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (ContentLink))
        return (object) this.GetContentLink(id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.acceptedTypes);
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (!(itemType == typeof (ContentLink)))
        return base.GetItemOrDefault(itemType, id);
      return (object) this.GetContentLinks().Where<ContentLink>((Expression<Func<ContentLink, bool>>) (t => t.Id == id)).FirstOrDefault<ContentLink>();
    }

    /// <summary>Gets the items.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="totalCount">Total count of the items that are filtered by <paramref name="filterExpression" /></param>
    /// <returns></returns>
    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (ContentLink))
        return (IEnumerable) DataProviderBase.SetExpressions<ContentLink>(this.GetContentLinks(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.acceptedTypes);
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      if (!(item is ContentLink))
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.acceptedTypes);
      this.Delete((ContentLink) item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => this.acceptedTypes;
  }
}
