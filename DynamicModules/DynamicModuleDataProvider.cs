// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.DynamicModuleDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.RecycleBin;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules
{
  /// <summary>
  /// This is the base class that all providers that act as providers for dynamic module need to implement.
  /// </summary>
  public abstract class DynamicModuleDataProvider : 
    UrlDataProviderBase,
    ILanguageDataProvider,
    IHierarchyProvider,
    ISecuredFieldsProvider,
    IHtmlFilterProvider,
    ITitleProvider
  {
    private string moduleName;
    private ModuleBuilderManager moduleBuilderManager;

    /// <summary>Commits the provided transaction.</summary>
    [TransactionPermission(typeof (DynamicContent), "General", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void CommitTransaction() => base.CommitTransaction();

    /// <summary>
    /// Flush all dirty and new instances to the database and evict all instances from the local cache.
    /// </summary>
    [TransactionPermission(typeof (DynamicContent), "General", SecurityConstants.TransactionActionType.Updated, new string[] {"Modify"})]
    public override void FlushTransaction() => base.FlushTransaction();

    /// <inheritdoc />
    protected internal override void Initialize(
      string providerName,
      NameValueCollection config,
      Type managerType,
      bool initializeDecorator)
    {
      base.Initialize(providerName, config, managerType, initializeDecorator);
      this.moduleName = config["moduleName"];
    }

    /// <summary>Gets the module builder manager.</summary>
    protected internal ModuleBuilderManager ModuleBuilderManager
    {
      get
      {
        if (this.moduleBuilderManager == null || this.moduleBuilderManager.Provider.IsDisposed)
          this.moduleBuilderManager = ModuleBuilderManager.GetManager();
        return this.moduleBuilderManager;
      }
    }

    /// <summary>
    /// Gets the name of the module to which the current provider is related.
    /// If it is null or empty string then the provider is available in all dynamic modules
    /// </summary>
    /// <value>The name of the module.</value>
    internal new string ModuleName => this.moduleName;

    /// <summary>
    /// Creates a new item of the specified type and returns it.
    /// </summary>
    /// <param name="itemType">The type of the item to be created.</param>
    /// <returns>The newly created item.</returns>
    public abstract DynamicContent CreateDataItem(Type itemType);

    /// <summary>
    /// Create a new item of the specified type with the given id and application name.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <param name="id">Id with which the item should be created.</param>
    /// <param name="applicationName">Name of the application under which the item should be created.</param>
    /// <returns>An instance of the newly created item.</returns>
    public abstract DynamicContent CreateDataItem(
      Type itemType,
      Guid id,
      string applicationName);

    /// <summary>Gets the item (by id) of the specified type.</summary>
    /// <param name="itemType">Type of the item to retrieve.</param>
    /// <param name="id">Id of the item to be retrieved.</param>
    /// <returns>An instance of the item.</returns>
    public abstract DynamicContent GetDataItem(Type itemType, Guid id);

    /// <summary>Gets the query of dynamic data items.</summary>
    /// <param name="itemType">Type of data items for which to get the query.</param>
    /// <returns>The query of <see cref="!:IDynamicDataItem" /> objects.</returns>
    public abstract IQueryable<DynamicContent> GetDataItems(Type itemType);

    /// <summary>
    /// Gets the count of specified type after applying the filter expression.
    /// </summary>
    /// <param name="itemType">Type of the items to count.</param>
    /// <param name="filterExpression">Filter expression (dynamic linq syntax) to apply before performing the count. Pass null if you want to count all items.</param>
    /// <returns>The number of items of specified type that statisfies the filter expression.</returns>
    public abstract int GetCount(Type itemType, string filterExpression);

    /// <summary>Deletes the item.</summary>
    /// <param name="item">Item to be deleted.</param>
    [ParameterPermission("item", "General", new string[] {"Delete"})]
    public abstract void DeleteDataItem(DynamicContent item);

    /// <summary>
    /// Refresh the item with values from the store.
    /// Reads all data and overwrites the dirty fields. The object will be clean, the
    /// changes are lost.
    /// </summary>
    /// <param name="item">The dynamic item</param>
    public virtual void RefreshItem(DynamicContent item) => throw new NotImplementedException();

    /// <summary>
    /// Gets the original value of the specified property name for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="propertyName">Name of the property.</param>
    public virtual T GetOriginalValue<T>(DynamicContent item, string propertyName) => throw new NotImplementedException();

    /// <summary>
    /// Populates the ChildItems property of the given item and all its successors.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childType">The child type is not used anymore. We keep the method overload for backwards compatibility.</param>
    [Obsolete("This method will be removed in future releases. Use the GetChildItems method instead.")]
    public virtual void LoadChildItemsHierarchy(DynamicContent item, Type childType = null) => this.LoadChildItemsHierarchy(item);

    /// <summary>
    /// Populates the ChildItems property of the given item and all its successors.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childType">Type of the children. If no value is provided the type of the children is loaded based on the item. </param>
    [Obsolete("This method will be removed in future releases. Use the GetChildItems method instead.")]
    public virtual void LoadChildItemsHierarchy(DynamicContent item)
    {
      item.SystemChildItems = this.GetChildItems(item, typeof (DynamicContent));
      if (item.SystemChildItems == null || item.SystemChildItems.Count<DynamicContent>() <= 0)
        return;
      foreach (DynamicContent systemChildItem in (IEnumerable<DynamicContent>) item.SystemChildItems)
        this.LoadChildItemsHierarchy(systemChildItem);
    }

    /// <summary>
    /// Gets the child items of the current item of specified type.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="childType">Type of the children. If it is null returns child items for all child types</param>
    /// <returns>The child items of the current item</returns>
    public virtual IQueryable<DynamicContent> GetChildItems(
      DynamicContent item,
      Type childType)
    {
      return this.GetChildItems(new List<Guid>()
      {
        item.Status == ContentLifecycleStatus.Master ? item.Id : item.OriginalContentId
      }, childType);
    }

    /// <summary>
    /// Gets the child items of list of items from specified type.
    /// </summary>
    /// <param name="ids">Parent item IDs</param>
    /// <param name="childType">Type of the children. If it is null returns a collection for all child types</param>
    /// <returns>The child items from all parents</returns>
    public virtual IQueryable<DynamicContent> GetChildItems(
      List<Guid> ids,
      Type childType)
    {
      if (childType == (Type) null)
        throw new ArgumentNullException(nameof (childType));
      if (object.Equals((object) childType, (object) typeof (DynamicContent)))
        throw new InvalidOperationException("Cannot build a query with DynamicContent type. Actual childType is required.");
      return this.GetDataItems(childType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => ids.Contains(i.SystemParentId)));
    }

    /// <inheritdoc />
    public override void RecompileChildrenUrlsHierarchically<T>(IHierarchicalItem item)
    {
      if (!(item is DynamicContent dynamicContent))
        throw new ArgumentOutOfRangeException("item is not DynamicContent");
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      foreach (Type childItemType in DynamicContentExtensions.GetChildItemTypes(item.GetType()))
      {
        foreach (DynamicContent childItem in this.GetChildItems(dynamicContent, childItemType).ToList<DynamicContent>())
        {
          IRecyclableDataItem recyclableDataItem = (IRecyclableDataItem) childItem;
          if (recyclableDataItem == null || !recyclableDataItem.IsDeleted)
          {
            this.RecompileChildItemUrl<T>((ILocatable) childItem, culture);
            this.RecompileChildrenUrlsHierarchically<T>((IHierarchicalItem) childItem);
          }
        }
      }
    }

    /// <summary>
    /// Gets the successor items of specified type for specific parent. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="parentIDs">The parent item for which to retrieve the child items</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <returns>Collection of child items</returns>
    public virtual IQueryable<DynamicContent> GetItemSuccessors(
      DynamicContent parent,
      Type childItemsType)
    {
      IQueryable<DynamicContent> itemSuccessors = Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
      IQueryable<DynamicContent> dataItems = this.GetDataItems(childItemsType);
      Stack<DynamicModuleType> hierarchyInDepth = this.GetHierarchyInDepth(parent.GetType(), childItemsType);
      if (hierarchyInDepth.Count != 0)
      {
        string hierarchyFilterById = DynamicModuleDataProvider.GetHierarchyFilterById(parent.Status == ContentLifecycleStatus.Master ? parent.Id : parent.OriginalContentId, hierarchyInDepth.Count);
        itemSuccessors = dataItems.Where<DynamicContent>(hierarchyFilterById);
      }
      return itemSuccessors;
    }

    /// <summary>
    /// Gets the successor items for collection of parent item IDs by child type. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="parentIDs">The parent item IDs for which to retrieve the child items.</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <param name="parentItemsType">The parent items type</param>
    /// <returns>Collection of child items</returns>
    public virtual IQueryable<DynamicContent> GetItemsSuccessors(
      List<Guid> parentIDs,
      Type childItemsType,
      Type parentItemsType)
    {
      IQueryable<DynamicContent> itemsSuccessors = Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
      if (parentIDs.Count > 0 && childItemsType != (Type) null)
      {
        Stack<DynamicModuleType> hierarchyInDepth = this.GetHierarchyInDepth(parentItemsType, childItemsType);
        if (hierarchyInDepth.Count > 0)
        {
          hierarchyInDepth.Pop();
          while (hierarchyInDepth.Count != 0)
          {
            Type itemType = TypeResolutionService.ResolveType(hierarchyInDepth.Pop().GetFullTypeName());
            if (itemType.FullName != childItemsType.FullName)
              parentIDs = this.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => parentIDs.Contains(i.SystemParentId) && (int) i.Status == 0)).Select<DynamicContent, Guid>((Expression<Func<DynamicContent, Guid>>) (i => i.Id)).ToList<Guid>();
            else
              itemsSuccessors = this.GetDataItems(itemType).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => parentIDs.Contains(i.SystemParentId)));
          }
        }
      }
      return itemsSuccessors;
    }

    /// <summary>
    /// Retrieve content items by their URL, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="url">URL of the item (relative)</param>
    /// <param name="contentTypes">The resolved items will be filtered by provided content types</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <returns>Data items</returns>
    public virtual IEnumerable<IDataItem> GetItemsFromUrl(
      string url,
      IEnumerable<string> contentTypes,
      bool published)
    {
      throw new NotImplementedException();
    }

    /// <inheritdoc />
    public virtual string ApplyFilters(string html) => HtmlFilterProvider.ApplyFilters(html);

    /// <inheritdoc />
    public virtual void ApplyFilters(IDataItem item)
    {
      if (!SystemManager.CurrentHttpContext.Items.Contains((object) "sfContentFilters"))
        return;
      Type type = (item as DynamicContent).GetType();
      List<string> list = ModuleBuilderManager.GetModules().GetTypeByFullName(type.FullName).Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => f.FieldType == FieldType.LongText && !f.DisableLinkParser)).Select<IDynamicModuleField, string>((Func<IDynamicModuleField, string>) (f => f.Name)).ToList<string>();
      PropertyDescriptorCollection properties = new DynamicFieldsTypeDescriptionProvider().GetTypeDescriptor(type, (object) item).GetProperties();
      foreach (string name in list)
      {
        PropertyDescriptor propertyDescriptor = properties[name];
        if (propertyDescriptor != null)
        {
          object html = propertyDescriptor.GetValue((object) item);
          if ((object) (html as Lstring) != null)
          {
            Lstring lstring = html as Lstring;
            lstring.PersistedValue = this.ApplyFilters(lstring.PersistedValue);
          }
          else
            propertyDescriptor.SetValue((object) item, (object) this.ApplyFilters((string) html));
        }
      }
    }

    /// <summary>Creates a language data item</summary>
    public abstract LanguageData CreateLanguageData();

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    public abstract LanguageData CreateLanguageData(Guid id);

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    public abstract LanguageData GetLanguageData(Guid id);

    /// <summary>Gets a query of all language data items</summary>
    /// <returns></returns>
    public abstract IQueryable<LanguageData> GetLanguageData();

    /// <summary>
    /// Override this method in order to return the type of the Parent object of the specified type.
    /// If the type has no parent type, return null.
    /// </summary>
    /// <param name="childType">The child type.</param>
    /// <returns>The parent type</returns>
    public abstract Type GetParentType(Type childType);

    /// <summary>
    /// Override this method in order to return the parent of the specified child object.
    /// </summary>
    /// <param name="child">The child object.</param>
    /// <returns>The parent object.</returns>
    public abstract IDataItem GetParent(IDataItem child);

    /// <summary>
    /// Override this method in order to return the children of the specified parent object.
    /// </summary>
    /// <param name="parent">The parent object.</param>
    /// <returns>List of children.</returns>
    public abstract IList<IDataItem> GetChildren(IDataItem parent);

    /// <summary>
    /// if some of the fields of the specified item are not visible for the current user, then these fields are reset,
    /// otherwise the item is not modified.
    /// </summary>
    /// <param name="item">The item.</param>
    public virtual void ApplyViewFieldPermissions(IDataItem item)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating whether view permissions for fields should be checked.
    /// </summary>
    public bool SuppressViewFieldsPermissionsCheck { get; set; }

    string ITitleProvider.GetTitle(object item, CultureInfo culture = null)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      return item is DynamicContent dynamicContent ? DynamicContentExtensions.GetTitle(dynamicContent, culture) : throw new NotImplementedException("Getting the title is not implemented for type " + item.GetType().FullName);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (DynamicContent), "General", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, out redirectUrl);
    }

    /// <summary>Gets the item from URL.</summary>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="url">The URL.</param>
    /// <param name="published">The published.</param>
    /// <param name="redirectUrl">The redirect URL.</param>
    /// <returns></returns>
    [TypedValuePermission(typeof (DynamicContent), "General", new string[] {"View"})]
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      return base.GetItemFromUrl(itemType, url, published, out redirectUrl);
    }

    private static string GetHierarchyFilterById(Guid id, int hierarchyDepth)
    {
      StringBuilder stringBuilder = new StringBuilder();
      for (int index = 1; index < hierarchyDepth; ++index)
        stringBuilder.Append("SystemParentItem.");
      stringBuilder.Append("Id = {" + id.ToString() + "}");
      return stringBuilder.ToString();
    }

    private Stack<DynamicModuleType> GetHierarchyInDepth(
      Type parentType,
      Type childType)
    {
      return this.ModuleBuilderManager.GetHierarchyInDepth(parentType, childType);
    }
  }
}
