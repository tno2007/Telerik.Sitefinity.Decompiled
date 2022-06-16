// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.DynamicModules.Data.OpenAccessDynamicModuleProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Decorators;
using Telerik.Sitefinity.Data.Events;
using Telerik.Sitefinity.Data.Linq;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Data.OA;
using Telerik.Sitefinity.DynamicModules.Builder.Model;
using Telerik.Sitefinity.DynamicModules.Model;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.GeoLocations.Model;
using Telerik.Sitefinity.Lifecycle;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Model.ContentLinks;
using Telerik.Sitefinity.Modules.GenericContent;
using Telerik.Sitefinity.RelatedData;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Claims;
using Telerik.Sitefinity.Security.Configuration;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Services.Events;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Upgrades.To5100;
using Telerik.Sitefinity.Utilities.TypeConverters;

namespace Telerik.Sitefinity.DynamicModules.Data
{
  /// <summary>
  /// Telerik OpenAccess implementation for the <see cref="T:Telerik.Sitefinity.DynamicModules.DynamicModuleDataProvider" />.
  /// </summary>
  [UrlProviderDecorator(typeof (OpenAccessDynamicModuleUrlProviderDecorator))]
  public class OpenAccessDynamicModuleProvider : 
    DynamicModuleDataProvider,
    IOpenAccessDataProvider,
    IDataProviderBase,
    IDisposable,
    ICloneable,
    IDataProviderEventsCall,
    IOpenAccessMetadataProvider,
    IOrganizableProvider,
    IDataEventProvider,
    IOpenAccessUpgradableProviderExtended,
    IOpenAccessUpgradableProvider,
    IRelatedDataSource
  {
    /// <summary>Gets the items by taxon.</summary>
    /// <param name="taxonId">The taxon id.</param>
    /// <param name="isSingleTaxon">A value indicating if it is a single taxon.</param>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="itemType">Type of the item.</param>
    /// <param name="filterExpression">The filter expression.</param>
    /// <param name="orderExpression">The order expression.</param>
    /// <param name="skip">Items to skip.</param>
    /// <param name="take">Items to take.</param>
    /// <param name="totalCount">The total count.</param>
    /// <returns>IEnumerable of items that are marked with a specified taxon.</returns>
    public IEnumerable GetItemsByTaxon(
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
      this.CurrentTaxonomyProperty = propertyName;
      int? totalCount1 = new int?();
      IQueryable<DynamicContent> items = (IQueryable<DynamicContent>) this.GetItems(itemType, filterExpression, orderExpression, 0, 0, ref totalCount1);
      IQueryable<DynamicContent> source;
      if (isSingleTaxon)
        source = items.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.GetValue<Guid>(this.CurrentTaxonomyProperty) == taxonId));
      else
        source = items.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.GetValue<IList<Guid>>(this.CurrentTaxonomyProperty).Any<Guid>((Func<Guid, bool>) (t => t == taxonId))));
      if (totalCount.HasValue)
        totalCount = new int?(source.Count<DynamicContent>());
      if (skip > 0)
        source = source.Skip<DynamicContent>(skip);
      if (take > 0)
        source = source.Take<DynamicContent>(take);
      return (IEnumerable) source;
    }

    public override object CreateItem(Type itemType) => (object) this.CreateDataItem(itemType);

    public override object CreateItem(Type itemType, Guid id) => (object) this.CreateDataItem(itemType, id, this.ApplicationName);

    public override object GetItem(Type itemType, Guid id) => (object) this.GetDataItem(itemType, id);

    public override object GetItemOrDefault(Type itemType, Guid id) => (object) this.GetDataItems(itemType).FirstOrDefault<DynamicContent>((Expression<Func<DynamicContent, bool>>) (i => i.Id == id));

    public override IEnumerable GetItems(
      Type itemType,
      string filterExpression,
      string orderExpression,
      int skip,
      int take,
      ref int? totalCount)
    {
      return (IEnumerable) DataProviderBase.SetExpressions<DynamicContent>(this.GetDataItems(itemType), filterExpression, orderExpression, new int?(skip), new int?(take), ref totalCount);
    }

    public override void DeleteItem(object item) => this.DeleteDataItem(item as DynamicContent);

    /// <summary>
    /// Creates a new item of the specified type and returns it.
    /// </summary>
    /// <param name="itemType">The type of the item to be created.</param>
    /// <returns>The newly created item.</returns>
    public override DynamicContent CreateDataItem(Type itemType) => this.CreateDataItem(itemType, this.GetNewGuid(), this.ApplicationName);

    /// <summary>
    /// Create a new item of the specified type with the given id and application name.
    /// </summary>
    /// <param name="itemType">Type of the item to create.</param>
    /// <param name="id">Id with which the item should be created.</param>
    /// <param name="applicationName">Name of the application under which the item should be created.</param>
    /// <returns>An instance of the newly created item.</returns>
    public override DynamicContent CreateDataItem(
      Type itemType,
      Guid id,
      string applicationName)
    {
      DynamicModuleType mainSecuredObject = !(itemType == (Type) null) ? this.ModuleBuilderManager.GetDynamicModuleType(itemType) : throw new ArgumentNullException(nameof (itemType));
      ISecuredObject persistedTypePermissionHolder = ObjectFactory.Resolve<IDynamicModulePermissionHolderResolver>().Resolve((ISecuredObject) mainSecuredObject, this.ModuleBuilderManager, (DynamicModuleDataProvider) this);
      this.AuthorizeCreatePermissions(persistedTypePermissionHolder, mainSecuredObject.GetFullTypeName());
      IPersistentTypeDescriptor dynamicTypeDescriptor = this.GetDynamicTypeDescriptor(itemType);
      if (string.IsNullOrEmpty(applicationName))
        applicationName = this.ApplicationName;
      // ISSUE: variable of a boxed type
      __Boxed<Guid> singleFieldIdentityKey = (ValueType) id;
      DynamicContent instance = (DynamicContent) dynamicTypeDescriptor.CreateInstance((object) singleFieldIdentityKey);
      instance.ApplicationName = applicationName;
      instance.Provider = (object) this;
      instance.DateCreated = DateTime.UtcNow;
      instance.LastModified = DateTime.UtcNow;
      instance.PublicationDate = DateTime.UtcNow;
      instance.IncludeInSitemap = true;
      if (instance.Owner == Guid.Empty)
        instance.Owner = SecurityManager.GetCurrentUserId();
      this.CreatePermissionInheritance(persistedTypePermissionHolder, instance);
      if (id != Guid.Empty)
        this.GetContext().Add((object) instance);
      return instance;
    }

    /// <summary>Gets the item (by id) of the specified type.</summary>
    /// <param name="itemType">Type of the item to retrieve.</param>
    /// <param name="id">Id of the item to be retrieved.</param>
    /// <returns>An instance of the item.</returns>
    public override DynamicContent GetDataItem(Type itemType, Guid id)
    {
      itemType = !(itemType == (Type) null) && !(itemType == typeof (DynamicContent)) ? this.GetDynamicTypeDescriptor(itemType).DescribedType : throw new ArgumentException("Item type not correctly specified. Use the item's actual type. DynamicContent and null values are not supported.");
      IQueryable<DynamicContent> source = typeof (SitefinityQuery).GetMethod("Get", new Type[2]
      {
        typeof (DataProviderBase),
        typeof (MethodBase)
      }).MakeGenericMethod(itemType).Invoke((object) null, new object[2]
      {
        (object) this,
        (object) MethodBase.GetCurrentMethod()
      }) as IQueryable<DynamicContent>;
      DynamicContent dataItem = this.GetContext().GetDirtyItems().OfType<DynamicContent>().Where<DynamicContent>((Func<DynamicContent, bool>) (p => p.ApplicationName == this.ApplicationName && p.Id == id)).SingleOrDefault<DynamicContent>();
      if (dataItem == null)
        dataItem = source.Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (p => p.ApplicationName == this.ApplicationName && p.Id == id)).SingleOrDefault<DynamicContent>();
      if (dataItem == null)
        throw new ItemNotFoundException(Res.Get<ErrorMessages>().ItemNotFound.Arrange((object) itemType.Name, (object) id));
      dataItem.Provider = (object) this;
      this.ApplyViewPermissions(ref dataItem);
      if (dataItem == null)
        throw new UnauthorizedAccessException(string.Format(Res.Get<SecurityResources>().NotAuthorizedToDoSetAction, (object) "View", (object) "General"));
      this.ApplyViewFieldPermissions((IDataItem) dataItem);
      return dataItem;
    }

    /// <summary>Gets the query of dynamic data items.</summary>
    /// <param name="itemType">Type of data items for which to get the query.</param>
    /// <returns>The query of <see cref="!:IDynamicDataItem" /> objects.</returns>
    public override IQueryable<DynamicContent> GetDataItems(Type itemType)
    {
      itemType = !(itemType == (Type) null) && !(itemType == typeof (DynamicContent)) ? this.GetDynamicTypeDescriptor(itemType).DescribedType : throw new ArgumentException("Item type not correctly specified. Use the item's actual type. DynamicContent and null values are not supported.");
      return SitefinityQuery.Get<DynamicContent>(itemType, (DataProviderBase) this).Where<DynamicContent>((Expression<Func<DynamicContent, bool>>) (p => p.ApplicationName == this.ApplicationName));
    }

    /// <summary>
    /// Gets the count of specified type after applying the filter expression.
    /// </summary>
    /// <param name="itemType">Type of the items to count.</param>
    /// <param name="filterExpression">Filter expression (dynamic linq syntax) to apply before performing the count. Pass null if you want to count all items.</param>
    /// <returns>The number of items of specified type that statisfies the filter expression.</returns>
    public override int GetCount(Type itemType, string filterExpression) => this.GetDataItems(itemType).Where<DynamicContent>(filterExpression).Count<DynamicContent>();

    /// <summary>Deletes the item.</summary>
    /// <param name="item">Item to be deleted.</param>
    public override void DeleteDataItem(DynamicContent item)
    {
      if (item == null)
        throw new ArgumentNullException(nameof (item));
      foreach (KeyValuePair<string, Address> addressField in item.GetAddressFields())
      {
        if (addressField.Value != null && this.GetContext().GetState((object) addressField.Value) != ObjectState.MaskNoMask)
          this.GetContext().Remove((object) addressField.Value);
      }
      this.DeleteDynamicContentPermissions(item);
      if (item.Status == ContentLifecycleStatus.Master || item.Status == ContentLifecycleStatus.Deleted)
      {
        IQueryable<DynamicContent> dataItems = this.GetDataItems(item.GetType());
        Expression<Func<DynamicContent, bool>> predicate = (Expression<Func<DynamicContent, bool>>) (i => (int) i.Status != (int) item.Status && i.OriginalContentId == item.Id);
        foreach (DynamicContent dynamicContent in (IEnumerable<DynamicContent>) dataItems.Where<DynamicContent>(predicate))
          this.DeleteDataItem(dynamicContent);
      }
      this.GetContext().Remove((object) item);
    }

    /// <summary>
    /// Refresh the item with values from the store.
    /// Reads all data and overwrites the dirty fields. The object will be clean, the
    /// changes are lost.
    /// </summary>
    /// <param name="item">The dynamic item</param>
    public override void RefreshItem(DynamicContent item) => (this.GetTransaction() as SitefinityOAContext).Refresh(RefreshMode.OverwriteChangesFromStore, (object) item);

    /// <summary>
    /// Gets the original value of the specified property name for the specified <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <param name="propertyName">Name of the property.</param>
    public override T GetOriginalValue<T>(DynamicContent item, string propertyName) => ((OpenAccessContextBase) this.GetTransaction()).GetOriginalValue<T>((object) item, propertyName);

    /// <summary>Get a list of types served by this manager</summary>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (DynamicContent)
    };

    /// <summary>Gets a unique key for each data provider base.</summary>
    public override string RootKey => "DynamicModules";

    /// <summary>
    /// Gets or sets the OpenAccess context. Alternative to Database.
    /// </summary>
    /// <value>The context.</value>
    public OpenAccessProviderContext Context { get; set; }

    /// <summary>Gets the meta data source.</summary>
    /// <returns></returns>
    public MetadataSource GetMetaDataSource(IDatabaseMappingContext context) => (MetadataSource) new DynamicModuleMetadataSource(context);

    public override string GetUrlFormat(ILocatable item) => "/[UrlName]";

    /// <summary>
    /// Gets the actual type of the <see cref="T:Telerik.Sitefinity.GenericContent.Model.UrlData" /> implementation for the specified content type.
    /// </summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <returns></returns>
    public override Type GetUrlTypeFor(Type itemType) => itemType;

    /// <summary>Gets the first item that matches the specified URL.</summary>
    /// <param name="itemType">Type of the content item.</param>
    /// <param name="url">The URL to match.</param>
    /// <param name="redirectUrl">The URL to redirect to if there is newer URL.</param>
    /// <returns>The content item that matches the URL.</returns>
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      out string redirectUrl)
    {
      return this.GetItemFromUrl(itemType, url, false, out redirectUrl);
    }

    /// <summary>
    /// Retrieve a content item by its url, optionally returning only items that are visible on the public side
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="url">Url of the item (relative)</param>
    /// <param name="published">If true, will get only Published/Scheduled items - those that are typically visible on the public side.</param>
    /// <param name="redirectUrl">Url to redirect to if the item's url has been changed</param>
    /// <returns>Data item or null</returns>
    public override IDataItem GetItemFromUrl(
      Type itemType,
      string url,
      bool published,
      out string redirectUrl)
    {
      if (itemType == (Type) null)
        throw new ArgumentNullException(nameof (itemType));
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      IEnumerable<UrlData> source = this.GetUrls(itemType).Where<UrlData>((Expression<Func<UrlData, bool>>) (u => u.Url == url)).ToList<UrlData>().Where<UrlData>((Func<UrlData, bool>) (u => u.Parent != null && u.Parent.GetType().FullName == itemType.FullName));
      if (published)
      {
        source = source.Where<UrlData>((Func<UrlData, bool>) (u => ((ILifecycleDataItem) u.Parent).Status == ContentLifecycleStatus.Live && ((ILifecycleDataItem) u.Parent).Visible));
        if (SystemManager.CurrentContext.AppSettings.Multilingual && DynamicTypesHelper.IsTypeLocalizable(itemType))
          source = source.Where<UrlData>((Func<UrlData, bool>) (u =>
          {
            if (((ILifecycleDataItem) u.Parent).PublishedTranslations.Contains(SystemManager.CurrentContext.Culture.Name))
              return true;
            return ((ILifecycleDataItem) u.Parent).PublishedTranslations.Count == 0 && SystemManager.CurrentContext.Culture.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name;
          }));
      }
      UrlData urlData = (UrlData) null;
      if (source.Count<UrlData>() > 0)
      {
        urlData = source.FirstOrDefault<UrlData>((Func<UrlData, bool>) (u => ((ILifecycleDataItem) u.Parent).Status != ContentLifecycleStatus.Deleted && u.Culture == AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture)));
        if (urlData == null && SystemManager.CurrentContext.Culture.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name && DynamicTypesHelper.IsTypeLocalizable(itemType))
          urlData = source.FirstOrDefault<UrlData>((Func<UrlData, bool>) (u => ((ILifecycleDataItem) u.Parent).Status != ContentLifecycleStatus.Deleted && u.Culture == CultureInfo.InvariantCulture.LCID));
      }
      if (urlData != null)
      {
        IDataItem dataItem = urlData.Parent;
        if (!published)
          dataItem = this.GetContentMasterBase(dataItem);
        redirectUrl = !urlData.RedirectToDefault || dataItem == null ? (string) null : this.GetItemUrl((ILocatable) dataItem, CultureInfo.GetCultureInfo(urlData.Culture));
        if (dataItem != null)
        {
          dataItem.Provider = (object) this;
          return dataItem;
        }
      }
      redirectUrl = (string) null;
      return (IDataItem) null;
    }

    /// <inheritdoc />
    public override IEnumerable<IDataItem> GetItemsFromUrl(
      string url,
      IEnumerable<string> contentTypes,
      bool published)
    {
      if (string.IsNullOrEmpty(url))
        throw new ArgumentNullException(nameof (url));
      ContentLifecycleStatus desiredStatus = published ? ContentLifecycleStatus.Live : ContentLifecycleStatus.Master;
      IEnumerable<DynamicContentUrlData> source1 = this.GetUrls<DynamicContentUrlData>().Where<DynamicContentUrlData>((Expression<Func<DynamicContentUrlData, bool>>) (u => contentTypes.Contains<string>(u.ItemType) && u.Url == url)).ToList<DynamicContentUrlData>().Where<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => ((ILifecycleDataItem) u.Parent).Status == desiredStatus));
      if (published)
        source1 = source1.Where<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => ((ILifecycleDataItem) u.Parent).Visible));
      List<DynamicContentUrlData> source2 = new List<DynamicContentUrlData>();
      if (source1.Count<DynamicContentUrlData>() > 0)
      {
        foreach (DynamicContentUrlData dynamicContentUrlData in source1)
        {
          if (dynamicContentUrlData.Culture == AppSettings.CurrentSettings.GetCultureLcid(SystemManager.CurrentContext.Culture) || SystemManager.CurrentContext.Culture.Name == SystemManager.CurrentContext.AppSettings.DefaultFrontendLanguage.Name && dynamicContentUrlData.Culture == CultureInfo.InvariantCulture.LCID && DynamicTypesHelper.IsTypeLocalizable(dynamicContentUrlData.Parent.GetType()))
            source2.Add(dynamicContentUrlData);
        }
      }
      IEnumerable<IDataItem> itemsFromUrl = Enumerable.Empty<IDataItem>();
      if (source2.Count > 0)
        itemsFromUrl = source2.Where<DynamicContentUrlData>((Func<DynamicContentUrlData, bool>) (u => u.Parent != null)).Select<DynamicContentUrlData, IDataItem>((Func<DynamicContentUrlData, IDataItem>) (u => u.Parent)).GroupBy<IDataItem, Guid>((Func<IDataItem, Guid>) (i => i.Id)).Select<IGrouping<Guid, IDataItem>, IDataItem>((Func<IGrouping<Guid, IDataItem>, IDataItem>) (g => g.First<IDataItem>())).Where<IDataItem>((Func<IDataItem, bool>) (d =>
        {
          if (!(d is DynamicContent dataItem2))
            return false;
          this.ApplyViewPermissions(ref dataItem2);
          if (dataItem2 == null)
            throw new UnauthorizedAccessException("One or more of the dynamic items are not accessible for the current user.");
          return dataItem2 != null;
        }));
      return itemsFromUrl;
    }

    internal override UrlData CreateUrl<T>(
      T item,
      string url,
      int culture,
      bool isDefault = true,
      bool redirectoToDefault = false)
    {
      UrlData url1 = this.CreateUrl(item.GetType());
      url1.Url = url;
      url1.Culture = culture;
      url1.Parent = (IDataItem) item;
      url1.IsDefault = isDefault;
      url1.RedirectToDefault = redirectoToDefault;
      ILocatableExtended locatableExtended = (object) item as ILocatableExtended;
      if (!isDefault || locatableExtended == null)
        return url1;
      CultureInfo cultureByLcid = AppSettings.CurrentSettings.GetCultureByLcid(culture);
      locatableExtended.ItemDefaultUrl[cultureByLcid] = url;
      return url1;
    }

    /// <summary>Compiles the item URL for the specified UI culture.</summary>
    /// <typeparam name="T">The generic type of the content.</typeparam>
    /// <param name="item">The item.</param>
    /// <param name="culture">The culture.</param>
    /// <returns>The compiled URL string.</returns>
    public override string CompileItemUrl<T>(T item, CultureInfo culture)
    {
      if ((object) item == null)
        throw new ArgumentNullException(nameof (item));
      if (culture == null)
        throw new ArgumentNullException(nameof (culture));
      string empty = string.Empty;
      string str1;
      if ((object) item is DynamicContent)
      {
        StringBuilder stringBuilder = new StringBuilder();
        for (DynamicContent dynamicItem = (object) item as DynamicContent; dynamicItem != null; dynamicItem = dynamicItem.SystemParentItem)
        {
          MatchEvaluator evaluator = (MatchEvaluator) (m => this.EvaluateUrlPart<DynamicContent>(m, dynamicItem, culture));
          string str2 = Regex.Replace(this.GetUrlFormat((ILocatable) dynamicItem), this.UrlRegEx, evaluator);
          stringBuilder.Insert(0, str2);
        }
        str1 = stringBuilder.ToString();
      }
      else
      {
        MatchEvaluator evaluator = (MatchEvaluator) (m => this.EvaluateUrlPart<T>(m, item, culture));
        str1 = Regex.Replace(this.GetUrlFormat((ILocatable) item), this.UrlRegEx, evaluator);
      }
      return str1.Replace("//", "/");
    }

    /// <summary>Creates a language data item</summary>
    public override LanguageData CreateLanguageData() => this.CreateLanguageData(this.GetNewGuid());

    /// <summary>Creates a language data item</summary>
    /// <param name="id">The id.</param>
    public override LanguageData CreateLanguageData(Guid id)
    {
      LanguageData entity = new LanguageData(this.ApplicationName, id);
      ((IDataItem) entity).Provider = (object) this;
      if (id != Guid.Empty)
        this.GetContext().Add((object) entity);
      return entity;
    }

    /// <summary>Gets language data item by its id</summary>
    /// <param name="id">The id.</param>
    public override LanguageData GetLanguageData(Guid id)
    {
      LanguageData languageData = !(id == Guid.Empty) ? this.GetContext().GetItemById<LanguageData>(id.ToString()) : throw new ArgumentException("Argument 'id' cannot be empty GUID.");
      ((IDataItem) languageData).Provider = (object) this;
      return languageData;
    }

    /// <summary>Gets a query of all language data items</summary>
    public override IQueryable<LanguageData> GetLanguageData()
    {
      string appName = this.ApplicationName;
      return SitefinityQuery.Get<LanguageData>((DataProviderBase) this).Where<LanguageData>((Expression<Func<LanguageData, bool>>) (c => c.ApplicationName == appName));
    }

    /// <summary>
    /// Override this method in order to return the type of the Parent object of the specified type.
    /// If the type has no parent type, return null.
    /// </summary>
    /// <param name="childType">The child type.</param>
    /// <returns>The parent type</returns>
    public override Type GetParentType(Type childType)
    {
      DynamicModuleType persistedType = this.ModuleBuilderManager.GetDynamicModuleTypes().Where<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.TypeNamespace == childType.Namespace && t.TypeName == childType.Name)).FirstOrDefault<DynamicModuleType>();
      if (persistedType != null && persistedType.ParentModuleTypeId != Guid.Empty)
        return TypeResolutionService.ResolveType(this.ModuleBuilderManager.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.Id == persistedType.ParentModuleTypeId)).GetFullTypeName());
      return persistedType != null && persistedType.IsSelfReferencing ? childType : (Type) null;
    }

    /// <summary>
    /// Override this method in order to return the parent of the specified child object.
    /// </summary>
    /// <param name="child">The child object.</param>
    /// <returns>The parent object.</returns>
    public override IDataItem GetParent(IDataItem child)
    {
      DynamicModuleType dynamicModuleType = this.ModuleBuilderManager.GetDynamicModuleTypes().FirstOrDefault<DynamicModuleType>((Expression<Func<DynamicModuleType, bool>>) (t => t.Id == child.Id));
      return dynamicModuleType != null && dynamicModuleType.ParentModuleTypeId != Guid.Empty ? (IDataItem) this.GetDataItem(this.GetParentType(child.GetType()), dynamicModuleType.ParentModuleTypeId) : (IDataItem) null;
    }

    /// <summary>
    /// Override this method in order to return the children of the specified parent object.
    /// </summary>
    /// <param name="parent">The parent object.</param>
    /// <returns>List of children.</returns>
    public override IList<IDataItem> GetChildren(IDataItem parent) => (IList<IDataItem>) null;

    public override void ApplyViewFieldPermissions(IDataItem item)
    {
      if (item == null || this.SuppressSecurityChecks || this.SuppressViewFieldsPermissionsCheck)
        return;
      ((DynamicContent) item).ApplyViewPermissions();
    }

    /// <inheritdoc />
    bool IDataEventProvider.DataEventsEnabled => true;

    /// <inheritdoc />
    bool IDataEventProvider.ApplyDataEventItemFilter(IDataItem item) => item is DynamicContent;

    /// <inheritdoc />
    protected override ICollection<IEvent> GetDataEventItems(
      Func<IDataItem, bool> filterPredicate)
    {
      IList dirtyItems = this.GetDirtyItems();
      List<IDataItem> items = new List<IDataItem>();
      foreach (object obj in (IEnumerable) dirtyItems)
      {
        if (obj is IDataItem dataItem && filterPredicate(dataItem))
          items.Add(dataItem);
      }
      return (ICollection<IEvent>) this.GetDataEvents((ICollection<IDataItem>) items);
    }

    [ApplyNoPolicies]
    protected override IEnumerable<IEvent> GetBeforeCommitEvents(
      Func<IDataItem, bool> filterPredicate)
    {
      if (filterPredicate == null)
        throw new ArgumentNullException(nameof (filterPredicate));
      try
      {
        IList dirtyItems = this.GetDirtyItems();
        List<IEvent> beforeCommitEvents = new List<IEvent>(dirtyItems.Count);
        foreach (object itemInTransaction in (IEnumerable) dirtyItems)
        {
          if (itemInTransaction is IDataItem dataItem && filterPredicate(dataItem))
          {
            SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus(itemInTransaction);
            if (dataItem.Provider == null)
              dataItem.Provider = (object) this;
            this.AddBeforeCommitEvents((ICollection<IEvent>) beforeCommitEvents, dataItem, dirtyItemStatus);
          }
        }
        return (IEnumerable<IEvent>) beforeCommitEvents;
      }
      catch (Exception ex)
      {
        this.RollbackTransaction();
        throw ex;
      }
    }

    /// <inheritdoc />
    [ApplyNoPolicies]
    protected virtual List<IEvent> GetDataEvents(ICollection<IDataItem> items)
    {
      List<IEvent> dataEvents = new List<IEvent>();
      if (this.ShouldRaiseDataEvents() && items != null && items.Any<IDataItem>())
      {
        foreach (IDataItem dataItem in (IEnumerable<IDataItem>) items)
        {
          SecurityConstants.TransactionActionType dirtyItemStatus = this.GetDirtyItemStatus((object) dataItem);
          this.AddEvents((ICollection<IEvent>) dataEvents, dataItem, dirtyItemStatus);
        }
      }
      return dataEvents;
    }

    /// <summary>
    /// Adds events for specified dynamic content, that will be raised before committing transaction.
    /// </summary>
    /// <param name="events">The events collection that is populated.</param>
    /// <param name="dataItem">The data item for which events are created.</param>
    /// <param name="actionType">The state of the item (new, updated, deleted).</param>
    protected virtual void AddBeforeCommitEvents(
      ICollection<IEvent> events,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType)
    {
      if (events == null)
        throw new ArgumentNullException(nameof (events));
      if (dataItem == null)
        throw new ArgumentNullException(nameof (dataItem));
      List<string> source = new List<string>();
      if (dataItem is IHasTrackingContext context)
      {
        source = context.GetLanguages();
        if (context.HasOperation(OperationStatus.Deleted) || context.HasOperation(OperationStatus.MovedToRecycleBin) || context.HasOperation(OperationStatus.MovedToRecycleBinWithParent))
          actionType = SecurityConstants.TransactionActionType.Deleted;
      }
      IDynamicContentBeforeCommitEventFactory commitEventFactory = ObjectFactory.Resolve<IDynamicContentBeforeCommitEventFactory>();
      DynamicContent dataItem1 = dataItem as DynamicContent;
      string providerName = DataEventFactory.GetProviderName(dataItem.Provider);
      if (dataItem1 == null)
        return;
      if (this.IsSiteMultilingual() && source.Any<string>())
      {
        foreach (string language in source)
        {
          IDataEvent dataEvent = commitEventFactory.CreateEvent(dataItem1, actionType, providerName, language);
          events.Add((IEvent) dataEvent);
        }
      }
      else
      {
        IDataEvent dataEvent = commitEventFactory.CreateEvent(dataItem1, actionType, providerName);
        events.Add((IEvent) dataEvent);
      }
    }

    protected override void AddEvents(
      ICollection<IEvent> events,
      IDataItem dataItem,
      SecurityConstants.TransactionActionType actionType)
    {
      List<string> source = new List<string>();
      if (dataItem is IHasTrackingContext context)
      {
        source = context.GetLanguages();
        if (context.HasOperation(OperationStatus.Deleted) || context.HasOperation(OperationStatus.MovedToRecycleBin) || context.HasOperation(OperationStatus.MovedToRecycleBinWithParent))
          actionType = SecurityConstants.TransactionActionType.Deleted;
      }
      if (source.Any<string>() && this.IsSiteMultilingual())
      {
        foreach (string language in source)
        {
          IDataEvent dataEvent = DynamicContentEventFactory.CreateEvent((DynamicContent) dataItem, actionType, language);
          events.Add((IEvent) dataEvent);
        }
      }
      else
      {
        IDataEvent dataEvent = DynamicContentEventFactory.CreateEvent((DynamicContent) dataItem, actionType);
        events.Add((IEvent) dataEvent);
      }
      if (!(dataItem is IGeoLocatable))
        return;
      IEvent @event = GeoLocatableEventFactory.CreateEvent((IGeoLocatable) dataItem, actionType);
      if (@event == null)
        return;
      events.Add(@event);
    }

    /// <summary>Determines whether the site is in multilingual mode.</summary>
    /// <returns></returns>
    internal bool IsSiteMultilingual() => DataExtensions.AppSettings.ContextSettings.Multilingual;

    [ApplyNoPolicies]
    internal bool ShouldRaiseDataEvents() => !SystemManager.Initializing && SystemManager.CurrentHttpContext != null;

    /// <summary>Gets the permissions with a given application name.</summary>
    /// <remarks>
    /// This method is used when we need to get the permissions for <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> or <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicContentProvider" /> from the module builder provider.
    /// The method is used for performance optimization when the <see cref="T:Telerik.Sitefinity.DynamicModules.Model.DynamicContent" /> and the <see cref="T:Telerik.Sitefinity.DynamicModules.Builder.Model.DynamicModuleType" /> are in the same database.
    /// </remarks>
    /// <param name="applicationName">Name of the application.</param>
    /// <returns></returns>
    internal IQueryable<Telerik.Sitefinity.Security.Model.Permission> GetPermissionsInApplication(
      string applicationName)
    {
      return this.GetContext().GetAll<Telerik.Sitefinity.Security.Model.Permission>().Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => p.ApplicationName == applicationName));
    }

    private void AuthorizeCreatePermissions(
      ISecuredObject persistedTypePermissionHolder,
      string persistedTypeFullName)
    {
      if (this.SuppressSecurityChecks)
        return;
      if (!persistedTypePermissionHolder.IsGranted("General", "Create"))
        throw new UnauthorizedAccessException(string.Format("You are not authorized to create {0} items.", (object) persistedTypeFullName));
    }

    [SuppressMessage("Telerik.Sitefinity", "SF1001:AvoidToListOnIQueryable", Justification = "The ToList() is deliberate.")]
    private void CreatePermissionInheritance(
      ISecuredObject persistedTypePermissionHolder,
      DynamicContent dataItem)
    {
      Guid[] permissionHolderPermissionIds = persistedTypePermissionHolder.Permissions.Select<Telerik.Sitefinity.Security.Model.Permission, Guid>((Func<Telerik.Sitefinity.Security.Model.Permission, Guid>) (p => p.Id)).ToArray<Guid>();
      List<Telerik.Sitefinity.Security.Model.Permission> list = this.GetPermissionsInApplication(((IDataItem) persistedTypePermissionHolder).ApplicationName).Where<Telerik.Sitefinity.Security.Model.Permission>((Expression<Func<Telerik.Sitefinity.Security.Model.Permission, bool>>) (p => permissionHolderPermissionIds.Contains<Guid>(p.Id))).ToList<Telerik.Sitefinity.Security.Model.Permission>();
      ((OpenAccessDecorator) this.providerDecorator).CreatePermissionInheritanceAssociation(persistedTypePermissionHolder, (IList<Telerik.Sitefinity.Security.Model.Permission>) list, (ISecuredObject) dataItem);
    }

    private void ApplyViewPermissions(ref DynamicContent dataItem)
    {
      string str = "General";
      SitefinityIdentity currentIdentity = ClaimsManager.GetCurrentIdentity();
      if ((!this.FilterQueriesByViewPermissions || this.SuppressSecurityChecks || currentIdentity == null || currentIdentity.IsUnrestricted ? 1 : (str.IsNullOrWhitespace() ? 1 : 0)) != 0)
        return;
      ISecuredObject securedObject = this.GetSecuredObject(dataItem);
      Guid id = SecurityManager.OwnerRole.Id;
      SecurityAction securityAction = securedObject.GetSecurityAction(str, "View");
      if (securedObject.IsGranted(str, "View"))
        return;
      if (securedObject.IsGranted(str, new Guid[1]{ id }, securityAction.Value))
        dataItem = dataItem.Owner == SecurityManager.CurrentUserId ? dataItem : (DynamicContent) null;
      else
        dataItem = (DynamicContent) null;
    }

    /// <summary>
    /// Gets the secured object for the specified dynamic type.
    /// </summary>
    /// <param name="type">The type.</param>
    public ISecuredObject GetSecuredObject(DynamicContent dataItem) => (ISecuredObject) dataItem;

    private void DeleteDynamicContentPermissions(DynamicContent item)
    {
      this.DeletePermissionsInheritanceAssociation(DynamicPermissionHelper.GetSecuredObject((IDynamicModuleSecurityManager) this.ModuleBuilderManager, (ISecuredObject) this.ModuleBuilderManager.GetDynamicModuleType(item.GetType()), this.Name), (ISecuredObject) item);
      this.providerDecorator.DeletePermissions((object) item);
    }

    /// <inheritdoc />
    public int CurrentSchemaVersionNumber => this.GetAssemblyBuildNumber();

    /// <inheritdoc />
    public void OnUpgrading(UpgradingContext context, int upgradingFromSchemaVersionNumber)
    {
      if (upgradingFromSchemaVersionNumber <= 0 || upgradingFromSchemaVersionNumber >= SitefinityVersion.Sitefinity12_2_IB_7228.Build || context.DatabaseContext.DatabaseType != DatabaseType.SqlAzure && context.DatabaseContext.DatabaseType != DatabaseType.MsSql)
        return;
      string str = "Remove foreign keys for dynamic content";
      try
      {
        string sql = "CREATE TABLE #ForeignKeys\r\n(\r\n    PKTABLE_QUALIFIER sysname, \r\n    PKTABLE_OWNER sysname,\r\n    PKTABLE_NAME sysname,\r\n    PKCOLUMN_NAME sysname, \r\n    FKTABLE_QUALIFIER sysname,\r\n    FKTABLE_OWNER sysname, \r\n    FKTABLE_NAME sysname,\r\n    FKCOLUMN_NAME sysname,\r\n    KEY_SEQ smallint,\r\n    UPDATE_RULE smallint,\r\n    DELETE_RULE smallint,\r\n    FK_NAME sysname,\r\n    PK_NAME sysname,\r\n    DEFERRABILITY smallint\r\n)\r\n \r\nINSERT INTO #ForeignKeys\r\nEXEC sp_fkeys 'sf_dynamic_content'\r\n \r\nDELETE FROM #ForeignKeys\r\nWHERE FKTABLE_NAME LIKE 'sf_%'\r\n \r\nDECLARE @TableName sysname\r\nDECLARE @ForeignKeyName sysname\r\nDECLARE @cmd nvarchar(max)\r\n \r\nSELECT top 1 @TableName=FKTABLE_NAME, @ForeignKeyName=FK_NAME FROM #ForeignKeys\r\n \r\nWHILE @@Rowcount > 0  \r\nBEGIN\r\n    SET @cmd = 'ALTER TABLE ' + @TableName + ' DROP CONSTRAINT ' + @ForeignKeyName + ';'\r\n \r\n    EXEC (@cmd)\r\n \r\n    DELETE FROM #ForeignKeys WHERE FK_NAME=@ForeignKeyName\r\n \r\n    SELECT top 1 @TableName=FKTABLE_NAME, @ForeignKeyName=FK_NAME FROM #ForeignKeys\r\nEND\r\n \r\nDROP table #ForeignKeys";
        context.ExecuteSQL(sql);
        Log.Write((object) string.Format("PASSED: {0}", (object) str), ConfigurationPolicy.UpgradeTrace);
      }
      catch (Exception ex)
      {
        Log.Write((object) string.Format("FAILED: {0} - {1}", (object) str, (object) ex.Message), ConfigurationPolicy.UpgradeTrace);
      }
    }

    /// <inheritdoc />
    public void OnUpgraded(UpgradingContext context, int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber >= SitefinityVersion.Sitefinity7_0.Build)
        return;
      ApprovalRecordsUpgrader.UpgradeDb((OpenAccessContext) context, "sf_dynamic_content", this.GetType().Name, "base_id");
    }

    /// <inheritdoc />
    public void OnSchemaUpgrade(
      OpenAccessConnection oaConnection,
      ISchemaHandler schemaHandler,
      int upgradedFromSchemaVersionNumber)
    {
      if (upgradedFromSchemaVersionNumber >= 4110 || oaConnection.DbType != DatabaseType.MySQL)
        return;
      string ddl = "ALTER TABLE `sf_url_data` ADD INDEX `idx_sf_url_type`(`url`, `item_type`);";
      try
      {
        schemaHandler.ForceExecuteDDLScript(ddl);
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(new Exception("Failed to create index 'idx_sf_url_type' on columns ('url', 'item_type') of 'sf_url_data' table : {0}".Arrange((object) ex.Message), ex), ExceptionPolicyName.IgnoreExceptions))
          return;
        throw;
      }
    }

    /// <inheritdoc />
    public virtual IQueryable<T> GetRelatedItems<T>(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
      where T : IDataItem
    {
      return Queryable.OfType<T>(this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, typeof (T), status, filterExpression, orderExpression, skip, take, ref totalCount, relationDirection));
    }

    /// <inheritdoc />
    public virtual IQueryable GetRelatedItems(
      string itemType,
      string itemProviderName,
      Guid itemId,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount,
      RelationDirection relationDirection = RelationDirection.Child)
    {
      return relationDirection == RelationDirection.Child ? this.GetRelatedItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount) : this.GetRelatingItems(itemType, itemProviderName, itemId, fieldName, relatedItemsType, status, filterExpression, orderExpression, skip, take, ref totalCount);
    }

    /// <inheritdoc />
    public Dictionary<Guid, List<IDataItem>> GetRelatedItems(
      string itemType,
      string itemProviderName,
      List<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable1 = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable1 = queryable1.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      IQueryable<ContentLink> queryable2 = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable1, RelationDirection.Child), fieldName, status);
      IEnumerable<IGrouping<Guid, ContentLink>> source = queryable2.ToList<ContentLink>().GroupBy<ContentLink, Guid>((Func<ContentLink, Guid>) (l => l.ParentItemId));
      Dictionary<Guid, List<IDataItem>> relatedItems = new Dictionary<Guid, List<IDataItem>>();
      if (source.Any<IGrouping<Guid, ContentLink>>())
      {
        List<DynamicContent> list1 = RelatedDataHelper.JoinRelatedItems<DynamicContent>(this.GetDataItems(relatedItemsType), queryable2, status).Distinct<DynamicContent>((IEqualityComparer<DynamicContent>) new DynamicContentComparer()).ToList<DynamicContent>();
        foreach (IGrouping<Guid, ContentLink> grouping in source)
        {
          IGrouping<Guid, ContentLink> linkGroup = grouping;
          IEnumerable<DynamicContent> items = list1.Where<DynamicContent>((Func<DynamicContent, bool>) (ri => linkGroup.Any<ContentLink>((Func<ContentLink, bool>) (l => l.ChildItemId == RelatedDataExtensions.GetId((object) ri)))));
          List<IDataItem> list2 = RelatedDataHelper.SortRelatedItems<DynamicContent>((IEnumerable<ContentLink>) linkGroup, items, status).Cast<IDataItem>().ToList<IDataItem>();
          relatedItems.Add(linkGroup.Key, list2);
        }
      }
      return relatedItems;
    }

    /// <inheritdoc />
    public IEnumerable<IDataItem> GetRelatedItemsList(
      string itemType,
      string itemProviderName,
      Collection<Guid> parentItemIds,
      string fieldName,
      Type relatedItemsType,
      ContentLifecycleStatus? status)
    {
      IQueryable<ContentLink> queryable = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == itemType && cl.ParentItemProviderName == itemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemIds != null && parentItemIds.Count<Guid>() > 0)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => parentItemIds.Contains(cl.ParentItemId)));
      IQueryable<ContentLink> links = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status);
      return (IEnumerable<IDataItem>) RelatedDataHelper.JoinRelatedItems<DynamicContent>(this.GetDataItems(relatedItemsType), links, status).Distinct<DynamicContent>((IEqualityComparer<DynamicContent>) new DynamicContentComparer()).ToList<DynamicContent>();
    }

    private IQueryable GetRelatedItems(
      string parentItemType,
      string parentItemProviderName,
      Guid parentItemId,
      string fieldName,
      Type itemType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemType == parentItemType && cl.ParentItemProviderName == parentItemProviderName && cl.ChildItemProviderName == this.Name));
      if (parentItemId != Guid.Empty)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ParentItemId == parentItemId));
      IQueryable<ContentLink> links = (IQueryable<ContentLink>) RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Child), fieldName, status).OrderBy<ContentLink, float>((Expression<Func<ContentLink, float>>) (cl => cl.Ordinal));
      IQueryable<DynamicContent> source = RelatedDataHelper.JoinRelatedItems<DynamicContent>(this.GetDataItems(itemType), links, status);
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where<DynamicContent>(filterExpression);
      int? nullable;
      if (totalCount.HasValue)
      {
        totalCount = new int?(source.Count<DynamicContent>());
        nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return (IQueryable) Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
      }
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy<DynamicContent>(orderExpression);
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip<DynamicContent>(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take<DynamicContent>(take.Value);
      }
      return (IQueryable) source;
    }

    private IQueryable GetRelatingItems(
      string childItemType,
      string childItemProviderName,
      Guid childItemId,
      string fieldName,
      Type itemType,
      ContentLifecycleStatus? status,
      string filterExpression,
      string orderExpression,
      int? skip,
      int? take,
      ref int? totalCount)
    {
      IQueryable<ContentLink> queryable = SitefinityQuery.Get<ContentLink>((DataProviderBase) this).Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemType == childItemType && cl.ChildItemProviderName == childItemProviderName && cl.ParentItemProviderName == this.Name));
      if (childItemId != Guid.Empty)
        queryable = queryable.Where<ContentLink>((Expression<Func<ContentLink, bool>>) (cl => cl.ChildItemId == childItemId));
      IQueryable<ContentLink> links = RelatedDataHelper.ApplyLinksFilters(RelatedDataHelper.ApplyDeletedLinksFilters(queryable, RelationDirection.Parent), fieldName, status);
      IQueryable<DynamicContent> source = RelatedDataHelper.JoinRelatingItems<DynamicContent>(this.GetDataItems(itemType), links, status);
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where<DynamicContent>(filterExpression);
      int? nullable;
      if (totalCount.HasValue)
      {
        totalCount = new int?(source.Count<DynamicContent>());
        nullable = totalCount;
        int num = 0;
        if (nullable.GetValueOrDefault() == num & nullable.HasValue)
          return (IQueryable) Enumerable.Empty<DynamicContent>().AsQueryable<DynamicContent>();
      }
      if (!string.IsNullOrEmpty(orderExpression))
        source = source.OrderBy<DynamicContent>(orderExpression);
      if (skip.HasValue)
      {
        nullable = skip;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Skip<DynamicContent>(skip.Value);
      }
      if (take.HasValue)
      {
        nullable = take;
        int num = 0;
        if (nullable.GetValueOrDefault() > num & nullable.HasValue)
          source = source.Take<DynamicContent>(take.Value);
      }
      return (IQueryable) source;
    }
  }
}
