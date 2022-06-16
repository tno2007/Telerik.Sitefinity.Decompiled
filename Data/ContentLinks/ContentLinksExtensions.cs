// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Data.ContentLinks.ContentLinksExtensions
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.Data.ContentLinks
{
  /// <summary>Extension methods for work with content links</summary>
  public static class ContentLinksExtensions
  {
    /// <summary>
    /// Creates a content link between parent and child data items
    /// </summary>
    /// <param name="parentDataItem">The parent data item.</param>
    /// <param name="childDataItem">The child data item.</param>
    /// <returns></returns>
    public static ContentLink CreateContentLink(
      this IDataItem parentDataItem,
      IDataItem childDataItem)
    {
      if (parentDataItem == null)
        throw new ArgumentNullException(nameof (parentDataItem));
      if (childDataItem == null)
        throw new ArgumentNullException(nameof (childDataItem));
      string str1 = !(parentDataItem.Provider is DataProviderBase provider1) ? string.Empty : provider1.Name;
      string str2 = !(childDataItem.Provider is DataProviderBase provider2) ? string.Empty : provider2.Name;
      return new ContentLink(parentDataItem.Id, childDataItem.Id)
      {
        ParentItemProviderName = str1,
        ChildItemProviderName = str2,
        ParentItemType = parentDataItem.GetType().FullName,
        ChildItemType = childDataItem.GetType().FullName
      };
    }

    /// <summary>
    /// Sets the attributes that specify the default content link value
    /// </summary>
    /// <param name="field">The field.</param>
    /// <param name="dataItem">The data item.</param>
    public static void SetDefaultContentLinkValueAttributes(
      this MetaField field,
      IDataItem dataItem)
    {
      if (string.IsNullOrEmpty(field.LinkedContentProvider) && field.ClrType != typeof (ContentLink).FullName)
        throw new ApplicationException("Default content link value can be set only to fields that are Content Link.");
      string key = "DefaultItemId";
      MetaFieldAttribute metaFieldAttribute1 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute1 == null)
      {
        metaFieldAttribute1 = new MetaFieldAttribute();
        metaFieldAttribute1.Name = key;
        field.MetaAttributes.Add(metaFieldAttribute1);
      }
      key = "DefaultItemProviderName";
      MetaFieldAttribute metaFieldAttribute2 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute2 == null)
      {
        metaFieldAttribute2 = new MetaFieldAttribute();
        metaFieldAttribute2.Name = key;
        field.MetaAttributes.Add(metaFieldAttribute2);
      }
      key = "DefaultItemTypeName";
      MetaFieldAttribute metaFieldAttribute3 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute3 == null)
      {
        metaFieldAttribute3 = new MetaFieldAttribute();
        metaFieldAttribute3.Name = key;
        field.MetaAttributes.Add(metaFieldAttribute3);
      }
      string str = !(dataItem.Provider is DataProviderBase provider) ? string.Empty : provider.Name;
      metaFieldAttribute1.Value = dataItem.Id.ToString();
      metaFieldAttribute2.Value = str;
      metaFieldAttribute3.Value = dataItem.GetType().FullName;
    }

    /// <summary>
    /// Removes the attributes used to spevify the default item for a content link
    /// </summary>
    /// <param name="field">The field.</param>
    public static void RemoveDefaultContentLinkValueAttributes(this MetaField field)
    {
      if (string.IsNullOrEmpty(field.LinkedContentProvider) && field.ClrType != typeof (ContentLink).FullName)
        throw new ApplicationException("Default content link value can be set only to fields that are Content Link.");
      string key = "DefaultItemId";
      MetaFieldAttribute metaFieldAttribute1 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute1 != null)
        field.MetaAttributes.Remove(metaFieldAttribute1);
      key = "DefaultItemProviderName";
      MetaFieldAttribute metaFieldAttribute2 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute2 != null)
        field.MetaAttributes.Remove(metaFieldAttribute2);
      key = "DefaultItemTypeName";
      MetaFieldAttribute metaFieldAttribute3 = field.MetaAttributes.SingleOrDefault<MetaFieldAttribute>((Func<MetaFieldAttribute, bool>) (mf => mf.Name == key));
      if (metaFieldAttribute3 == null)
        return;
      field.MetaAttributes.Remove(metaFieldAttribute3);
    }

    /// <summary>Gets the default value for content link.</summary>
    /// <param name="type">The type.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <returns></returns>
    public static IDataItem GetDefaultValueForContentLink(
      this Type type,
      string propertyName)
    {
      return ContentLinksExtensions.ResolveDataItem((TypeDescriptor.GetProperties(type)[propertyName] as ContentLinksPropertyDescriptor).DefaultChildItem);
    }

    /// <summary>Gets the default value for content link.</summary>
    /// <param name="parentItem">The parent item.</param>
    /// <param name="propertyName">Name of the property which is content link</param>
    /// <returns></returns>
    public static IDataItem GetDefaultValueForContentLink(
      this IDataItem parentItem,
      string propertyName)
    {
      return ContentLinksExtensions.ResolveDataItem((TypeDescriptor.GetProperties((object) parentItem)[propertyName] as ContentLinksPropertyDescriptor).DefaultChildItem);
    }

    /// <summary>Gets the data item.</summary>
    /// <param name="defaultChildItem">The default child item.</param>
    /// <returns></returns>
    public static IDataItem ResolveDataItem(ChildItem defaultChildItem)
    {
      if (defaultChildItem == null)
        return (IDataItem) null;
      Type itemType = TypeResolutionService.ResolveType(defaultChildItem.ItemTypeName);
      return (!string.IsNullOrEmpty(defaultChildItem.ItemProviderName) ? ManagerBase.GetMappedManager(itemType, defaultChildItem.ItemProviderName) : ManagerBase.GetMappedManager(itemType)).GetItem(itemType, new Guid(defaultChildItem.ItemId)) as IDataItem;
    }

    /// <summary>Creates a linked content field.</summary>
    /// <param name="fieldName">The name of the field.</param>
    /// <param name="contentLinksProviderName">Name of the content links provider.</param>
    /// <param name="relationshipType">Type of the relationship - 1 to 1 or 1 to many.</param>
    public static MetaField CreateContentLinkField(
      string fieldName,
      string contentLinksProviderName,
      MetadataManager manager,
      RelationshipType relationshipType = RelationshipType.OneToOne)
    {
      MetaField metafield = manager.CreateMetafield(fieldName);
      if (string.IsNullOrEmpty(contentLinksProviderName))
        contentLinksProviderName = Config.Get<ContentLinksConfig>().DefaultProvider;
      metafield.LinkedContentProvider = contentLinksProviderName;
      metafield.IsTwoWayContentLink = false;
      metafield.IsSingleTaxon = relationshipType == RelationshipType.OneToOne;
      metafield.ClrType = typeof (ContentLink).FullName;
      return metafield;
    }

    /// <summary>
    /// Gets the linked(child) item from the content link
    /// <remarks>
    /// Uses the mapped manager for the child item type
    /// </remarks>
    /// </summary>
    /// <param name="contentLink">The content link.</param>
    /// <param name="throwExceptionIfNotFound">if set to <c>true</c> [throw exception if not found].</param>
    /// <returns></returns>
    public static object GetLinkedItem(
      this ContentLink contentLink,
      bool throwExceptionIfNotFound = false,
      bool supressSecurityChecks = false)
    {
      IManager mappedManager = ManagerBase.GetMappedManager(contentLink.ChildItemType, contentLink.ChildItemProviderName);
      if (!supressSecurityChecks)
        return ContentLinksExtensions.GetLinkedItem(contentLink, throwExceptionIfNotFound, mappedManager);
      using (new ElevatedModeRegion(mappedManager))
        return ContentLinksExtensions.GetLinkedItem(contentLink, throwExceptionIfNotFound, mappedManager);
    }

    private static object GetLinkedItem(
      ContentLink contentLink,
      bool throwExceptionIfNotFound,
      IManager manager)
    {
      if (throwExceptionIfNotFound)
        return manager.GetItem(TypeResolutionService.ResolveType(contentLink.ChildItemType), contentLink.ChildItemId);
      try
      {
        return manager.GetItem(TypeResolutionService.ResolveType(contentLink.ChildItemType), contentLink.ChildItemId);
      }
      catch (ItemNotFoundException ex)
      {
        return (object) null;
      }
    }

    /// <summary>
    /// Copies the content link from one object to another, by cloning the data of the Content link and changing the parent id to the new destination object
    /// </summary>
    /// <param name="fieldName">Name of the field.</param>
    /// <param name="source">The source.</param>
    /// <param name="destination">The destination.</param>
    /// <remarks>source and destination should be of the same type</remarks>
    public static void CopyContentLink(string fieldName, IDataItem source, IDataItem destination)
    {
      IDynamicFieldsContainer dataItem1 = source as IDynamicFieldsContainer;
      IDynamicFieldsContainer dataItem2 = destination as IDynamicFieldsContainer;
      if (source == null || destination == null)
        throw new ArgumentException("CopyContentLink - both arguments(source, destination) should not be null");
      if (source.GetType() != destination.GetType())
        throw new ArgumentException(string.Format("CopyContentLink item arguments should be of the same type. {0} different from {1}", (object) source.GetType().FullName, (object) destination.GetType().FullName));
      string fieldName1 = fieldName;
      if (dataItem1.GetValue(fieldName1) is ContentLink source1)
        dataItem2.SetValue(fieldName, (object) new ContentLink(source1)
        {
          ParentItemId = destination.Id
        });
      else
        dataItem2.SetValue(fieldName, (object) null);
    }

    internal static void CopyContentLinkProperties(this ContentLink source, ContentLink target)
    {
      source.ApplicationName = target.ApplicationName;
      source.ChildItemAdditionalInfo = target.ChildItemAdditionalInfo;
      source.ChildItemId = target.ChildItemId;
      source.ChildItemProviderName = target.ChildItemProviderName;
      source.ChildItemType = target.ChildItemType;
      source.ComponentPropertyName = target.ComponentPropertyName;
      source.Ordinal = target.Ordinal;
      source.ParentItemAdditionalInfo = target.ParentItemAdditionalInfo;
      source.ParentItemId = target.ParentItemId;
      source.ParentItemProviderName = target.ParentItemProviderName;
      source.ParentItemType = target.ParentItemType;
    }
  }
}
