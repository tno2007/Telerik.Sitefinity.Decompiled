// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Model.DynamicContentExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Telerik.Sitefinity.ContentLocations;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.ContentLinks;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Descriptors;
using Telerik.Sitefinity.DynamicModules;
using Telerik.Sitefinity.DynamicModules.Builder;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.Fluent;
using Telerik.Sitefinity.Fluent.Permissions;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Libraries.Model;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.Libraries;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Utilities.TypeConverters;
using Telerik.Sitefinity.Web.Utilities;

namespace Telerik.Sitefinity.Model
{
  /// <summary>
  /// Provides public API for managing dynamic content items
  /// </summary>
  public static class DynamicContentExtensions
  {
    /// <summary>Adds an image to specific dynamic conten</summary>
    /// <param name="dynamicContent">The dynamic content to which the image will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="imageId">The id of the image</param>
    /// <param name="librariesProviderName">The name of the curent image provider</param>
    public static void AddImage(
      this DynamicContent dynamicContent,
      string propertyName,
      Guid imageId,
      string librariesProviderName = "")
    {
      Image image = LibrariesManager.GetManager(librariesProviderName).GetImage(imageId);
      dynamicContent.AddImage(propertyName, image);
    }

    /// <summary>Adds an image to specific dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content to which the image will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="img">The image to be added</param>
    public static void AddImage(this DynamicContent dynamicContent, string propertyName, Image img) => DynamicContentExtensions.AddMediaContentLinkInternal(dynamicContent, propertyName, (MediaContent) img);

    /// <summary>Deletes all images for specified dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content</param>
    /// <param name="propertyName">The images property to be cleared</param>
    public static void ClearImages(this DynamicContent dynamicContent, string propertyName)
    {
      ContentLinksManager manager = ContentLinksManager.GetManager();
      foreach (ContentLink contentLink in manager.GetContentLinks(dynamicContent.Id, dynamicContent.GetType(), propertyName).ToList<ContentLink>())
        manager.Delete(contentLink);
      manager.SaveChanges();
    }

    /// <summary>Adds an video to specific dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content to which the video will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="videoId">The id of the video</param>
    /// <param name="librariesProviderName">The name of the curent video provider</param>
    public static void AddVideo(
      this DynamicContent dynamicContent,
      string propertyName,
      Guid videoId,
      string librariesProviderName = "")
    {
      Video video = LibrariesManager.GetManager(librariesProviderName).GetVideo(videoId);
      dynamicContent.AddVideo(propertyName, video);
    }

    /// <summary>Adds a video to specific dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content to which the video will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="video">The video to be added</param>
    public static void AddVideo(
      this DynamicContent dynamicContent,
      string propertyName,
      Video video)
    {
      DynamicContentExtensions.AddMediaContentLinkInternal(dynamicContent, propertyName, (MediaContent) video);
    }

    /// <summary>Deletes all videos for specified dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content</param>
    /// <param name="propertyName">The videos property to be cleared</param>
    public static void ClearVideos(this DynamicContent dynamicContent, string propertyName)
    {
      ContentLinksManager manager = ContentLinksManager.GetManager();
      foreach (ContentLink contentLink in manager.GetContentLinks(dynamicContent.Id, dynamicContent.GetType(), propertyName).ToList<ContentLink>())
        manager.Delete(contentLink);
      manager.SaveChanges();
    }

    /// <summary>Adds a file to specific dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content to which the file will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="fileId">The id of the file</param>
    /// <param name="librariesProviderName">The name of the curent file provider</param>
    public static void AddFile(
      this DynamicContent dynamicContent,
      string propertyName,
      Guid fileId,
      string librariesProviderName = "")
    {
      Document document = LibrariesManager.GetManager(librariesProviderName).GetDocument(fileId);
      dynamicContent.AddFile(propertyName, document);
    }

    /// <summary>Adds a file to specific dynamic content</summary>
    /// <param name="dynamicContent">The dynamic content to which the file will be added</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    /// <param name="doc">The file to be added</param>
    public static void AddFile(
      this DynamicContent dynamicContent,
      string propertyName,
      Document doc)
    {
      DynamicContentExtensions.AddMediaContentLinkInternal(dynamicContent, propertyName, (MediaContent) doc);
    }

    /// <summary>
    /// Deletes all files and documents for specified dynamic content
    /// </summary>
    /// <param name="dynamicContent">The dynamic content</param>
    /// <param name="propertyName">The name of the dynamic content property</param>
    public static void ClearFiles(this DynamicContent dynamicContent, string propertyName)
    {
      ContentLinksManager manager = ContentLinksManager.GetManager();
      foreach (ContentLink contentLink in manager.GetContentLinks(dynamicContent.Id, dynamicContent.GetType(), propertyName).ToList<ContentLink>())
        manager.Delete(contentLink);
      manager.SaveChanges();
    }

    /// <summary>Sets the parent of the dynamic content item.</summary>
    /// <param name="dynamicContent">The dynamic content.</param>
    /// <param name="parentItem">The parent item.</param>
    public static void SetParent(this DynamicContent dynamicContent, DynamicContent parentItem)
    {
      Guid parentId = parentItem.Status != ContentLifecycleStatus.Master ? parentItem.OriginalContentId : parentItem.Id;
      dynamicContent.SetParent(parentId, parentItem.GetType().FullName);
    }

    /// <summary>Sets the parent of the dynamic content item.</summary>
    /// <param name="dynamicContent">The dynamic content.</param>
    /// <param name="parentId">The id of the parent.</param>
    public static void SetParent(
      this DynamicContent dynamicContent,
      Guid parentId,
      string systemParentType)
    {
      if (parentId == Guid.Empty)
      {
        dynamicContent.SystemParentId = Guid.Empty;
        dynamicContent.SystemParentType = (string) null;
        dynamicContent.SystemParentItem = (DynamicContent) null;
      }
      else
      {
        dynamicContent.SystemParentType = string.IsNullOrEmpty(systemParentType) || dynamicContent.IsValidParent(systemParentType) ? systemParentType : throw new Exception(string.Format("Item type {0} is not a child type of {1}", (object) dynamicContent.GetType().FullName, (object) systemParentType));
        dynamicContent.SystemParentId = parentId;
      }
    }

    /// <summary>
    /// Set the default value to all of DynamicContent's properties which are not read only.
    /// </summary>
    /// <param name="dynamicContent">The dynamic content.</param>
    public static void ResetPropertiesToDefaultValue(this DynamicContent dynamicContent)
    {
      foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) dynamicContent))
      {
        if (!property.IsReadOnly)
        {
          object obj = property.PropertyType.IsValueType ? Activator.CreateInstance(property.PropertyType) : (object) null;
          property.SetValue((object) dynamicContent, obj);
        }
      }
    }

    /// <summary>Adds child item to specific parent item</summary>
    /// <param name="parnetItem">The parent item</param>
    /// <param name="childItem">The child item</param>
    public static void AddChildItem(this DynamicContent parnetItem, DynamicContent childItem)
    {
      Guid parentId = parnetItem.Status != ContentLifecycleStatus.Master ? parnetItem.OriginalContentId : parnetItem.Id;
      childItem.SetParent(parentId, parnetItem.GetType().FullName);
    }

    /// <summary>
    /// Gets the child items of specific parent item by child items type
    /// </summary>
    /// <param name="dynamicContent">The parent item</param>
    /// <param name="childItemsType">The type of the child items</param>
    /// <returns>Collection of all child items</returns>
    public static IQueryable<DynamicContent> GetChildItems(
      this DynamicContent dynamicContent,
      string childItemsType)
    {
      Type childItemsType1 = TypeResolutionService.ResolveType(childItemsType);
      return dynamicContent.GetChildItems(childItemsType1);
    }

    /// <summary>
    /// Gets and filters the child items of specific parent item by child items type in the current culture.
    /// </summary>
    /// <param name="dynamicContent">The parent item</param>
    /// <param name="childItemsType">The type of the child items</param>
    /// <param name="filterExpression">Filter expression</param>
    /// <param name="orderExpression">Order expression</param>
    /// <param name="skip">Items to skip</param>
    /// <param name="take">Item to take</param>
    /// <param name="totalCount">Total count of the filtered items</param>
    /// <returns>Collection of all child items</returns>
    public static IEnumerable<DynamicContent> GetChildItems(
      this DynamicContent dynamicContent,
      string childItemsType,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      Type childItemsType1 = TypeResolutionService.ResolveType(childItemsType);
      return dynamicContent.GetChildItems(childItemsType1, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <summary>
    /// Gets the child items of specific parent item by child items type in the current culture.
    /// </summary>
    /// <param name="dynamicContent">The parent item</param>
    /// <param name="childItemsType">The type of the child items</param>
    /// <returns>Collection of all child items</returns>
    public static IQueryable<DynamicContent> GetChildItems(
      this DynamicContent dynamicContent,
      Type childItemsType)
    {
      DataProviderBase provider = dynamicContent.Provider as DataProviderBase;
      IQueryable<DynamicContent> childItems = DynamicModuleManager.GetManager(provider.Name, provider.TransactionName).GetChildItems(dynamicContent, childItemsType);
      IQueryable<DynamicContent> source = DynamicContentExtensions.FilterChildItemsByStatus(dynamicContent, childItems);
      CultureInfo uiCulture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        uiCulture = SystemManager.CurrentContext.Culture;
        source = source.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.PublishedTranslations.Contains(uiCulture.Name)));
      }
      return source;
    }

    /// <summary>
    /// Gets and filters the child items of specific parent item by child items type in the current culture.
    /// </summary>
    /// <param name="dynamicContent">The parent item</param>
    /// <param name="childItemsType">The type of the child items</param>
    /// <param name="filterExpression">Filter expression</param>
    /// <param name="orderExpression">Order expression</param>
    /// <param name="skip">Items to skip</param>
    /// <param name="take">Item to take</param>
    /// <param name="totalCount">Total count of the filtered items</param>
    /// <returns>Collection of all child items</returns>
    public static IEnumerable<DynamicContent> GetChildItems(
      this DynamicContent dynamicContent,
      Type childItemsType,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<DynamicContent> source = dynamicContent.GetChildItems(childItemsType);
      if (!filterExpression.IsNullOrEmpty())
        source = source.Where<DynamicContent>(filterExpression);
      totalCount = new int?(source.Count<DynamicContent>());
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy<DynamicContent>(orderExpression);
      if (skip.HasValue)
      {
        int? nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip<DynamicContent>(skip.Value);
      }
      if (take.HasValue)
      {
        int? nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take<DynamicContent>(take.Value);
      }
      return (IEnumerable<DynamicContent>) source.ToList<DynamicContent>();
    }

    /// <summary>
    /// Gets the successor items of specified type for specific parent. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="dynamicContent">The parent item for which to retrieve the child items.</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <returns>Collection of child items.</returns>
    public static IQueryable<DynamicContent> GetSuccessors(
      this DynamicContent dynamicContent,
      Type childItemsType)
    {
      DataProviderBase provider = dynamicContent.Provider as DataProviderBase;
      IQueryable<DynamicContent> itemSuccessors = DynamicModuleManager.GetManager(provider.Name, provider.TransactionName).GetItemSuccessors(dynamicContent, childItemsType);
      IQueryable<DynamicContent> source = DynamicContentExtensions.FilterChildItemsByStatus(dynamicContent, itemSuccessors);
      CultureInfo uiCulture = (CultureInfo) null;
      if (SystemManager.CurrentContext.AppSettings.Multilingual)
      {
        uiCulture = SystemManager.CurrentContext.Culture;
        source = source.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.PublishedTranslations.Contains(uiCulture.Name)));
      }
      return source;
    }

    /// <summary>
    /// Gets the successor items of specified type for specific parent. The child type does not need to be a direct child of the current parent item.
    /// </summary>
    /// <param name="dynamicContent">The parent item for which to retrieve the child items.</param>
    /// <param name="childItemsType">The type of the child items.</param>
    /// <returns>Collection of child items.</returns>
    public static IQueryable<DynamicContent> GetSuccessors(
      this DynamicContent dynamicContent,
      string childItemsType)
    {
      Type childItemsType1 = TypeResolutionService.ResolveType(childItemsType);
      return dynamicContent.GetSuccessors(childItemsType1);
    }

    /// <summary>
    /// Gets the count of all child items of specific parent from specified type
    /// </summary>
    /// <param name="parentItem">The parent item</param>
    /// <param name="childItemsType">The child items type</param>
    /// <returns>The child items count</returns>
    public static int GetChildItemsCount(this DynamicContent parentItem, Type childItemsType) => parentItem.GetChildItems(childItemsType).Count<DynamicContent>();

    /// <summary>
    /// Gets the count of all child items of specific parent from specified type
    /// </summary>
    /// <param name="parentItem">The parent item</param>
    /// <param name="childItemsType">The child items type</param>
    /// <returns>The child items count</returns>
    public static int GetChildItemsCount(this DynamicContent parentItem, string childItemsType)
    {
      Type childItemsType1 = TypeResolutionService.ResolveType(childItemsType);
      return parentItem.GetChildItems(childItemsType1).Count<DynamicContent>();
    }

    internal static Type GetParentItemType(Type dynamicContentType)
    {
      DynamicContentExtensions.GuardDynamicType(dynamicContentType);
      return TypeDescriptor.GetProperties(dynamicContentType).Find("ParentItem", false)?.PropertyType;
    }

    internal static IEnumerable<Type> GetChildItemTypes(Type dynamicContentType)
    {
      DynamicContentExtensions.GuardDynamicType(dynamicContentType);
      List<TypeSuccessorsPropertyDescriptor> list = TypeDescriptor.GetProperties(dynamicContentType).OfType<TypeSuccessorsPropertyDescriptor>().ToList<TypeSuccessorsPropertyDescriptor>();
      return list != null && list.Count > 0 ? list.Select<TypeSuccessorsPropertyDescriptor, Type>((Func<TypeSuccessorsPropertyDescriptor, Type>) (x => x.PropertyType)) : Enumerable.Empty<Type>();
    }

    private static void GuardDynamicType(Type dynamicContentType)
    {
      if (dynamicContentType == (Type) null)
        throw new ArgumentNullException(nameof (dynamicContentType));
      if (!typeof (DynamicContent).IsAssignableFrom(dynamicContentType))
        throw new ArgumentOutOfRangeException(string.Format("Type {0} is not assignable from DynamicContent", (object) dynamicContentType.FullName));
    }

    /// <summary>
    /// Checks wheather the parent type is valid parent type for this specific item
    /// </summary>
    /// <param name="childItem">The child item</param>
    /// <param name="systemParentType">The parent type</param>
    /// <returns>Boolean value</returns>
    public static bool IsValidParent(this DynamicContent childItem, string systemParentType)
    {
      string childTypeName = childItem.GetType().FullName;
      return ModuleBuilderManager.GetManager().GetChildTypes(TypeResolutionService.ResolveType(systemParentType)).Where<Type>((Func<Type, bool>) (ct => ct.FullName == childTypeName)).Count<Type>() > 0;
    }

    /// <summary>
    /// Checks wheather the parent type is valid parent type for this specific item
    /// </summary>
    /// <param name="childItem">The child item</param>
    /// <param name="parentItem">The parent type</param>
    /// <returns>Boolean value</returns>
    public static bool IsValidParent(this DynamicContent childItem, DynamicContent parentItem)
    {
      string fullName = parentItem.GetType().FullName;
      return childItem.IsValidParent(fullName);
    }

    /// <summary>
    /// Checks wheather the parent type is valid predecessor type for this specific item
    /// </summary>
    /// <param name="childItem">The child item</param>
    /// <param name="systemParentType">The parent type</param>
    /// <returns>Boolean value</returns>
    public static bool IsValidPredecessor(this DynamicContent childItem, string systemParentType) => ModuleBuilderManager.GetManager().GetContentTypesPredecessors(childItem.GetType()).Where<Type>((Func<Type, bool>) (ctp => ctp.FullName == systemParentType)).Count<Type>() > 0;

    /// <summary>
    /// Checks wheather the parent type is valid predecessor type for this specific item
    /// </summary>
    /// <param name="childItem">The child item</param>
    /// <param name="parentItem">The parent type</param>
    /// <returns></returns>
    public static bool IsValidPredecessor(this DynamicContent childItem, DynamicContent parentItem)
    {
      string fullName = parentItem.GetType().FullName;
      return childItem.IsValidPredecessor(fullName);
    }

    /// <summary>Gets the default URL for specified item.</summary>
    /// <param name="item">The item.</param>
    /// <returns>Absolute URL to default item location</returns>
    public static string GetDefaultLocation(this DynamicContent item)
    {
      string defaultLocation = string.Empty;
      if (item != null)
      {
        IContentItemLocation itemDefaultLocation = SystemManager.GetContentLocationServiceInternal().GetItemDefaultLocation((IDataItem) item, (CultureInfo) null);
        if (itemDefaultLocation != null)
          defaultLocation = itemDefaultLocation.ItemAbsoluteUrl;
      }
      return defaultLocation;
    }

    /// <summary>
    /// Gets location URL at specified index for specified item if exists.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="index">The index in the locations service.</param>
    /// <returns>Absolute URL to the requested item location.</returns>
    public static string GetLocationAtIndex(this DynamicContent item, int index)
    {
      string locationAtIndex = string.Empty;
      if (item != null)
      {
        List<IContentItemLocation> list = SystemManager.GetContentLocationServiceInternal().GetItemLocations((IDataItem) item, (CultureInfo) null).ToList<IContentItemLocation>();
        if (index < list.Count)
          locationAtIndex = list.ElementAt<IContentItemLocation>(index).ItemAbsoluteUrl;
      }
      return locationAtIndex;
    }

    /// <summary>
    /// Use this method to condition fluent API execution based on the provided <paramref name="predicate" />.
    /// </summary>
    /// <example>
    /// myItem.When((item) =&gt; item.GetType().Name == "MyType").ManagePermissions().ForUser(userId).Grant().View();
    /// If the variable myItem is of type "MyType" a view permission will be granted for a user with id:
    /// userIs, otherwise no actions will be performed on the myType instance and no exception will be thrown.
    /// </example>
    /// <param name="dynamicContentItem">The dynamic content item.</param>
    /// <param name="predicate">
    /// A function that accepts a dynamic content item and returns a Boolean.</param>
    /// <returns>A common facade for all fluent API for <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /></returns>
    public static DynamicContentFluent When(
      this DynamicContent dynamicContentItem,
      Func<DynamicContent, bool> predicate)
    {
      return predicate != null ? new DynamicContentFluent(dynamicContentItem)
      {
        Enabled = predicate(dynamicContentItem)
      } : throw new ArgumentNullException(nameof (predicate));
    }

    /// <summary>
    /// Exposes permissions related operations with the specified <paramref name="dynamicContent" />.
    /// </summary>
    /// <param name="securedObject">The secured object which permissions will be modified.</param>
    /// <returns>A facade for permissions related operations.</returns>
    public static IPermissionsFacade ManagePermissions(
      this DynamicContent dynamicContent)
    {
      return dynamicContent != null ? new DynamicContentFluent(dynamicContent).ManagePermissions() : throw new ArgumentNullException(nameof (dynamicContent));
    }

    internal static IQueryable<DynamicContent> FilterChildItemsByStatus(
      DynamicContent dynamicContent,
      IQueryable<DynamicContent> childItems)
    {
      if (dynamicContent.Status == ContentLifecycleStatus.Live)
        childItems = childItems.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Visible && (int) i.Status == 2));
      else
        childItems = childItems.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => (int) i.Status == 0));
      return childItems;
    }

    internal static MetaType GetMetaType(this DynamicModuleType moduleType)
    {
      MetaType metaType = moduleType != null ? MetadataManager.GetManager().GetMetaType(moduleType.Id) : throw new ArgumentNullException(nameof (moduleType));
      if (metaType.ClassName != moduleType.TypeName)
        throw new InvalidOperationException("MetaType.ClassName does not equal the DynamicModuleType.TypeName with the same ID.");
      if (!(metaType.Namespace != moduleType.TypeNamespace))
        return metaType;
      throw new InvalidOperationException("MetaType.Namespace does not equal the DynamicModuleType.TypeNamespace with the same ID.");
    }

    internal static string GetHierarchycalTitlesAsBreadcrumb(
      this DynamicContent dynamicContent,
      string separator = " > ",
      bool reversePath = false)
    {
      DynamicContent dynamicContent1 = dynamicContent;
      List<string> values = new List<string>();
      string empty = string.Empty;
      for (; dynamicContent1 != null; dynamicContent1 = dynamicContent1.SystemParentItem)
      {
        string title = DynamicContentExtensions.GetTitle(dynamicContent1);
        if (!string.IsNullOrEmpty(title))
          values.Add(title);
      }
      if (reversePath)
        values.Reverse();
      return string.Join(separator, (IEnumerable<string>) values);
    }

    /// <summary>
    /// Gets the default URL. In monolingual returns the URL for invariant culture.
    /// In multilingual returns the URl for default frontend language if exists, otherwise return the URL for invariant culture.
    /// </summary>
    /// <param name="dynamicContent">The dynamic content.</param>
    /// <returns>The default url data</returns>
    internal static DynamicContentUrlData GetDefaultUrl(
      this DynamicContent dynamicContent)
    {
      DynamicContentUrlData defaultUrl = (DynamicContentUrlData) null;
      if (dynamicContent.Urls != null && dynamicContent.Urls.Count > 0)
      {
        if (SystemManager.CurrentContext.AppSettings.Multilingual)
        {
          int defaultCultureLcid = Telerik.Sitefinity.Abstractions.AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage);
          defaultUrl = dynamicContent.Urls.SingleOrDefault<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => u.IsDefault && u.Culture == defaultCultureLcid)) ?? dynamicContent.Urls.SingleOrDefault<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => u.IsDefault && u.Culture == CultureInfo.InvariantCulture.LCID));
        }
        else
          defaultUrl = dynamicContent.Urls.SingleOrDefault<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => u.IsDefault && u.Culture == CultureInfo.InvariantCulture.LCID));
      }
      return defaultUrl;
    }

    /// <summary>Gets the name of the provider.</summary>
    /// <param name="dynamicContent">Content of the dynamic.</param>
    internal static string GetProviderName(this DynamicContent dynamicContent) => ((DataProviderBase) dynamicContent.Provider).Name;

    /// <summary>
    /// Un-resolves the links included in the long text field of the specified dynamic content.
    /// </summary>
    /// <param name="dynamicContent">The dynamic content.</param>
    internal static void UnresolveLinks(this DynamicContent dynamicContent)
    {
      Type type = dynamicContent.GetType();
      List<string> list = ModuleBuilderManager.GetModules().GetTypeByFullName(type.FullName).Fields.Where<IDynamicModuleField>((Func<IDynamicModuleField, bool>) (f => f.FieldType == FieldType.LongText && !f.DisableLinkParser)).Select<IDynamicModuleField, string>((Func<IDynamicModuleField, string>) (f => f.Name)).ToList<string>();
      PropertyDescriptorCollection properties = new DynamicFieldsTypeDescriptionProvider().GetTypeDescriptor(type, (object) dynamicContent).GetProperties();
      foreach (string name in list)
      {
        PropertyDescriptor propertyDescriptor = properties[name];
        object html = propertyDescriptor.GetValue((object) dynamicContent);
        if ((object) (html as Lstring) != null)
        {
          Lstring lstring = html as Lstring;
          lstring.PersistedValue = LinkParser.UnresolveLinks(lstring.PersistedValue);
        }
        else
          propertyDescriptor.SetValue((object) dynamicContent, (object) LinkParser.UnresolveLinks((string) html));
      }
    }

    /// <summary>Adds content link to the provided media content</summary>
    /// <param name="dynamicContent">The dynamic content to witch content link should be added</param>
    /// <param name="propertyName">The name of the property</param>
    /// <param name="mediaContent">The type of media content to add - Image, Video, Document</param>
    internal static void AddMediaContentLinkInternal(
      DynamicContent dynamicContent,
      string propertyName,
      MediaContent mediaContent)
    {
      DynamicModuleDataProvider provider = (DynamicModuleDataProvider) dynamicContent.Provider;
      ContentLink contentLink = new ContentLink();
      contentLink.ParentItemId = dynamicContent.Id;
      contentLink.ParentItemProviderName = provider.Name;
      contentLink.ParentItemType = dynamicContent.GetType().ToString();
      contentLink.ChildItemId = mediaContent.Id;
      contentLink.ComponentPropertyName = propertyName;
      contentLink.ChildItemAdditionalInfo = mediaContent.ResolveThumbnailUrl();
      contentLink.ChildItemProviderName = (mediaContent.Provider as DataProviderBase).Name;
      contentLink.ChildItemType = mediaContent.GetType().FullName;
      contentLink.ApplicationName = "//";
      if (!(dynamicContent.GetValue(propertyName) is ContentLink[] source))
        source = new ContentLink[0];
      List<ContentLink> list = ((IEnumerable<ContentLink>) source).ToList<ContentLink>();
      list.Insert(0, contentLink);
      ContentLink[] array = list.ToArray();
      dynamicContent.SetValue(propertyName, (object) array);
    }

    internal static string GetTitle(this DynamicContent dynamicContent, CultureInfo culture = null)
    {
      PropertyDescriptor typeMainProperty = ModuleBuilderManager.GetTypeMainProperty(dynamicContent.GetType());
      string empty = string.Empty;
      if (typeMainProperty != null)
      {
        object obj;
        if (typeMainProperty is LstringPropertyDescriptor propertyDescriptor)
        {
          if (culture == null)
            culture = SystemManager.CurrentContext.Culture;
          obj = (object) propertyDescriptor.GetString((object) dynamicContent, culture, true);
        }
        else
          obj = typeMainProperty.GetValue((object) dynamicContent);
        if (obj != null)
          empty = obj.ToString();
      }
      return empty;
    }
  }
}
