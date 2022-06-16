// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.ContentDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Represents base class for Generic Content data providers.
  /// </summary>
  public abstract class ContentDataProvider : ContentDataProviderBase, ILanguageDataProvider
  {
    private static Type[] knownTypes;
    private string[] supportedPermissionSets = new string[2]
    {
      "General",
      "SitemapGeneration"
    };
    private IDictionary<string, string> permissionsetObjectTitleResKeys = (IDictionary<string, string>) new Dictionary<string, string>()
    {
      {
        "General",
        "ContentGeneralActionTitle"
      }
    };

    /// <summary>Creates new content item.</summary>
    /// <returns>The new content item.</returns>
    [MethodPermission("General", new string[] {"Create"})]
    public abstract ContentItem CreateContent();

    /// <summary>Creates new content item with the specified ID.</summary>
    /// <param name="id">The id of the new content.</param>
    /// <returns>The new content item.</returns>
    [MethodPermission("General", new string[] {"Create"})]
    public abstract ContentItem CreateContent(Guid id);

    /// <summary>Gets a content with the specified ID.</summary>
    /// <param name="id">The ID to search for.</param>
    /// <returns>A content item.</returns>
    [ValuePermission("General", new string[] {"View"})]
    public abstract ContentItem GetContent(Guid id);

    /// <summary>Gets a query for content items.</summary>
    /// <returns>The query for content items.</returns>
    [EnumeratorPermission("General", new string[] {"View"})]
    public abstract IQueryable<ContentItem> GetContent();

    /// <summary>Deletes the specified item.</summary>
    /// <param name="item">The content item to delete.</param>
    [ParameterPermission("item", "General", new string[] {"Delete"})]
    public abstract void Delete(ContentItem item);

    /// <summary>Demands the commented object permissions.</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="permissionSetName">Name of the permission set.</param>
    /// <param name="actionNames">The action names.</param>
    protected internal override void DemandCommentedObjectPermissions(
      ICommentable commentedItem,
      string permissionSetName,
      params string[] actionNames)
    {
    }

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (ContentDataProvider);

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="urlType">Type of the URL.</param>
    /// <returns></returns>
    public override Type GetUrlTypeFor(Type urlType)
    {
      if (urlType == typeof (ContentItem))
        return typeof (ContentItemUrlData);
      throw new ArgumentException("Unknown type specified.");
    }

    /// <summary>Creates new data item.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public override object CreateItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (ContentItem))
        return (object) this.CreateContent(id);
      if (itemType == typeof (Comment))
        return (object) this.CreateComment(typeof (ContentItem), id);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Gets the data item with the specified ID.
    /// An exception should be thrown if an item with the specified ID does not exist.
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="id">The ID of the item to return.</param>
    /// <returns></returns>
    public override object GetItem(Type itemType, Guid id)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (itemType == typeof (ContentItem))
        return (object) this.GetContent(id);
      if (itemType == typeof (Comment))
        return (object) this.GetComment(id);
      return base.GetItem(itemType, id) ?? throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id)
    {
      if (itemType == typeof (Comment))
      {
        if (!this.EnableCommentsBackwardCompatibility)
          return (object) null;
        try
        {
          return (object) this.GetComment(id);
        }
        catch (Exception ex)
        {
          return (object) null;
        }
      }
      else
      {
        if (!(itemType == typeof (ContentItem)))
          return base.GetItemOrDefault(itemType, id);
        return (object) this.GetContent().Where<ContentItem>((Expression<Func<ContentItem, bool>>) (n => n.Id == id)).FirstOrDefault<ContentItem>();
      }
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
      if (itemType == typeof (ContentItem))
        return (IEnumerable) DataProviderBase.SetExpressions<ContentItem>(this.GetContent(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      if (itemType == typeof (Comment))
        return (IEnumerable) DataProviderBase.SetExpressions<Comment>(this.GetComments(), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
      throw DataProviderBase.GetInvalidItemTypeException(itemType, this.GetKnownTypes());
    }

    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon"></param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns></returns>
    public override IEnumerable GetItemsByTaxon(
      Guid taxonId,
      bool isSingleTaxon,
      string propertyName,
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      if (itemType == typeof (ContentItem))
      {
        this.CurrentTaxonomyProperty = propertyName;
        int? totalCount1 = new int?();
        IQueryable<ContentItem> items = (IQueryable<ContentItem>) this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref totalCount1);
        IQueryable<ContentItem> source;
        if (isSingleTaxon)
          source = items.Where<ContentItem>((Expression<Func<ContentItem, bool>>) (i => i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId));
        else
          source = items.Where<ContentItem>((Expression<Func<ContentItem, bool>>) (i => i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty).Any<Guid>((Func<Guid, bool>) (t => t == taxonId))));
        if (totalCount.HasValue)
          totalCount = new int?(source.Count<ContentItem>());
        if (skip > 0)
          source = source.Skip<ContentItem>(skip);
        if (take > 0)
          source = source.Take<ContentItem>(take);
        return (IEnumerable) source;
      }
      throw DataProviderBase.GetInvalidItemTypeException(itemType, typeof (ContentItem));
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item)
    {
      Type c = item != null ? item.GetType() : throw new ArgumentNullException(nameof (item));
      int num = typeof (ContentItem).IsAssignableFrom(c) ? 1 : (c == typeof (Comment) ? 1 : 0);
      if (typeof (ContentItem).IsAssignableFrom(c))
        this.Delete((ContentItem) item);
      else if (c == typeof (Comment))
        this.Delete((Comment) item);
      if (num == 0)
        throw DataProviderBase.GetInvalidItemTypeException(item.GetType(), this.GetKnownTypes());
      this.providerDecorator.DeletePermissions(item);
    }

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes()
    {
      if (ContentDataProvider.knownTypes == null)
        ContentDataProvider.knownTypes = new Type[2]
        {
          typeof (ContentItem),
          typeof (Comment)
        };
      return ContentDataProvider.knownTypes;
    }

    /// <summary>
    /// Gets the permission sets relevant to this specific secured object.
    /// To be overridden by relevant providers (which involve security roots)
    /// </summary>
    /// <value>The supported permission sets.</value>
    public override string[] SupportedPermissionSets
    {
      get => this.supportedPermissionSets;
      set => this.supportedPermissionSets = value;
    }

    /// <summary>
    /// Gets a dictionary:
    /// Key is a name of a permission set supported by this provider,
    /// Value is a resource key of the SecurityResources title which is to be used for titles of permissions, if defined in resources as placeholders.
    /// </summary>
    /// <value>The permission set object titles.</value>
    public override IDictionary<string, string> PermissionsetObjectTitleResKeys
    {
      get => this.permissionsetObjectTitleResKeys;
      set => this.permissionsetObjectTitleResKeys = value;
    }

    /// <summary>Sets the root permissions.</summary>
    /// <param name="root">The root.</param>
    public override void SetRootPermissions(SecurityRoot root)
    {
      if (root.Permissions != null || root.Permissions.Count > 0)
        root.Permissions.Clear();
      Permission permission1 = this.CreatePermission("General", root.Id, SecurityManager.EveryoneRole.Id);
      permission1.GrantActions(false, "View");
      root.Permissions.Add(permission1);
      Permission permission2 = this.CreatePermission("General", root.Id, SecurityManager.OwnerRole.Id);
      permission2.GrantActions(false, "Modify", "Delete");
      root.Permissions.Add(permission2);
      Permission permission3 = this.CreatePermission("General", root.Id, SecurityManager.EditorsRole.Id);
      permission3.GrantActions(false, "Create", "Modify", "Delete", "ChangeOwner");
      root.Permissions.Add(permission3);
      Permission permission4 = this.CreatePermission("General", root.Id, SecurityManager.AuthorsRole.Id);
      permission4.GrantActions(false, "Create");
      root.Permissions.Add(permission4);
    }

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (ContentItem), "General", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    [TransactionPermission(typeof (ContentItem), "General", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void FlushTransaction() => base.FlushTransaction();

    /// <summary>Creates a language data item</summary>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData();

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    /// <returns></returns>
    public abstract LanguageData GetLanguageData(Guid id);

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public abstract IQueryable<LanguageData> GetLanguageData();
  }
}
