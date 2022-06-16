// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Data.ResourceDataProvider
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Telerik.Microsoft.Practices.Unity;
using Telerik.Microsoft.Practices.Unity.InterceptionExtension;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Localization.Data
{
  /// <summary>
  /// Represents base class for localizable resources data providers.
  /// IMPORTANT! This data provider is not intercepted by security and event policies, for performance reasons,
  /// therefore security should be handled manually in the actual provider implementations.
  /// </summary>
  [ApplyNoPolicies]
  public abstract class ResourceDataProvider : DataProviderBase
  {
    private static object resourceLock = new object();
    private static object resourceSetLock = new object();
    private static IDictionary<string, IDictionary<CultureInfo, IDictionary<string, string>>> cache = SystemManager.CreateStaticCache<string, IDictionary<CultureInfo, IDictionary<string, string>>>();

    /// <summary>Gets a unique key for each data provider base.</summary>
    /// <value></value>
    public override string RootKey => nameof (ResourceDataProvider);

    /// <summary>
    /// Gets a value indicating whether to check for updates for the provider during the installation.
    /// </summary>
    /// <value><c>true</c> if [check for updates]; otherwise, <c>false</c>.</value>
    public override bool CheckForUpdates => false;

    /// <summary>
    /// Gets the value of the <see cref="T:System.String" /> resource localized for the specified culture.
    /// NOTE! This method does not fallback to parent culture.
    /// If the requested key is not found, the method returns null.
    /// </summary>
    /// <param name="classId">The name of the resource class.</param>
    /// <param name="key">The key of the resource to get.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized. Note that if the resource is not localized for this culture,
    /// the lookup will fall back using the culture's <see cref="P:System.Globalization.CultureInfo.Parent" />
    /// property, stopping after checking in the neutral culture.
    /// 
    /// If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> is obtained using the
    /// culture's <see cref="!:Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture" /> property.
    /// </param>
    /// <returns>
    /// The value of the resource, localized for the specified culture.
    /// If the key is not found, null is returned.
    /// </returns>
    public virtual string GetString(CultureInfo culture, string classId, string key)
    {
      IDictionary<CultureInfo, IDictionary<string, string>> dictionary1;
      if (ResourceDataProvider.cache.TryGetValue(classId, out dictionary1))
      {
        if (dictionary1 != null)
        {
          IDictionary<string, string> dictionary2;
          if (dictionary1.TryGetValue(culture, out dictionary2))
          {
            string str;
            if (dictionary2 != null && dictionary2.TryGetValue(key, out str))
              return str;
          }
          else
          {
            lock (ResourceDataProvider.resourceLock)
            {
              if (!dictionary1.ContainsKey(culture))
              {
                IDictionary<string, string> dictionary3 = this.LoadResource(culture, classId);
                dictionary1.Add(culture, dictionary3);
              }
            }
            return this.GetString(culture, classId, key);
          }
        }
        return (string) null;
      }
      lock (ResourceDataProvider.resourceSetLock)
      {
        if (!ResourceDataProvider.cache.ContainsKey(classId))
        {
          IDictionary<CultureInfo, IDictionary<string, string>> resourceSet = this.GetResourceSet(classId);
          ResourceDataProvider.cache.Add(classId, resourceSet);
        }
      }
      return this.GetString(culture, classId, key);
    }

    /// <summary>
    /// Clears all string resources from the cache for the specified class ID and culture.
    /// </summary>
    /// <param name="classId">The name of the resource class.</param>
    /// <param name="culture"><see cref="T:System.Globalization.CultureInfo" /></param>
    public virtual void RefreshResource(CultureInfo culture, string classId)
    {
      IDictionary<CultureInfo, IDictionary<string, string>> dictionary;
      if (!ResourceDataProvider.cache.TryGetValue(classId, out dictionary))
        return;
      if (dictionary == null)
      {
        lock (ResourceDataProvider.resourceSetLock)
          ResourceDataProvider.cache.Remove(classId);
      }
      else
      {
        if (dictionary == null || !dictionary.ContainsKey(culture))
          return;
        lock (ResourceDataProvider.resourceSetLock)
          dictionary.Remove(culture);
      }
    }

    /// <summary>Gets all registered resource classes.</summary>
    /// <returns>A string array of class names.</returns>
    public virtual string[] GetAllClassIds()
    {
      List<string> stringList = new List<string>();
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (Resource)))
        stringList.Add(registerEventArgs.Name);
      return stringList.ToArray();
    }

    /// <summary>Gets all registered resource classes info.</summary>
    /// <returns>A string array of class names.</returns>
    public virtual ObjectInfoAttribute[] GetAllClassInfos()
    {
      List<ObjectInfoAttribute> objectInfoAttributeList = new List<ObjectInfoAttribute>();
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (Resource)))
      {
        Type type = registerEventArgs.TypeTo;
        if ((object) type == null)
          type = registerEventArgs.TypeFrom;
        object[] customAttributes = type.GetCustomAttributes(typeof (ObjectInfoAttribute), false);
        if (customAttributes.Length != 0)
          objectInfoAttributeList.Add((ObjectInfoAttribute) customAttributes[0]);
        else
          objectInfoAttributeList.Add(new ObjectInfoAttribute(registerEventArgs.Name));
      }
      return objectInfoAttributeList.ToArray();
    }

    public virtual ObjectInfoAttribute[] GetAllClassInfos(CultureInfo culture)
    {
      List<ObjectInfoAttribute> objectInfoAttributeList = new List<ObjectInfoAttribute>();
      foreach (RegisterEventArgs registerEventArgs in ObjectFactory.GetArgsForType(typeof (Resource)))
      {
        Type type = registerEventArgs.TypeTo;
        if ((object) type == null)
          type = registerEventArgs.TypeFrom;
        object[] customAttributes = type.GetCustomAttributes(typeof (ObjectInfoAttribute), false);
        if (customAttributes.Length != 0)
          objectInfoAttributeList.Add((ObjectInfoAttribute) customAttributes[0]);
        else
          objectInfoAttributeList.Add(new ObjectInfoAttribute(registerEventArgs.Name));
      }
      return objectInfoAttributeList.ToArray();
    }

    /// <summary>
    /// Creates new instance of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> and adds it to the current transaction.
    /// The item is persisted when the transaction is committed.
    /// </summary>
    /// <param name="classId">The key of the resource set this entry belongs to.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="key">The key by which this entry is accessed.</param>
    /// <param name="value">The localized string.</param>
    /// <param name="description">The value for this entry.</param>
    public abstract ResourceEntry AddItem(
      CultureInfo culture,
      string classId,
      string key,
      string value,
      string description);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object for the specified class pageId, culture and key.
    /// </summary>
    /// <param name="classId">The class pageId of the resource.</param>
    /// <param name="culture">The culture for which the resource should be returned.</param>
    /// <param name="key">The key of the resources that ought to be returned.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object.</returns>
    public abstract ResourceEntry GetResource(
      CultureInfo culture,
      string classId,
      string key);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object for the specified class pageId, culture and key. If the resource
    /// does not exist, a new empty <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object with specified classId, culture and key
    /// will be created and returned.
    /// </summary>
    /// <param name="classId">The class pageId of the resource.</param>
    /// <param name="culture">The culture for which the resource should be returned.</param>
    /// <param name="key">The key of the resources that ought to be returned.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object.</returns>
    public abstract ResourceEntry GetResourceOrEmpty(
      CultureInfo culture,
      string classId,
      string key);

    /// <summary>
    /// Get <see cref="T:System.Collections.Generic.IEnumerable`1" /> object for querying and sorting <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> items.
    /// This method returns entries from all classes and all cultures combined.
    /// Note, calling this method without where or take clause can result in slow performance.
    /// </summary>
    /// <param name="culture">The culture.</param>
    /// <returns><see cref="T:System.Linq.IQueryable" /></returns>
    public abstract IQueryable<ResourceEntry> GetResources(
      CultureInfo culture);

    /// <summary>
    /// Get <see cref="T:System.Linq.IQueryable`1" /> object for querying and sorting <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> items.
    /// </summary>
    /// <typeparam name="TClass">The key of the resource set to get.</typeparam>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <returns><see cref="T:System.Linq.IQueryable" /></returns>
    public virtual IQueryable<ResourceEntry> GetResources<TClass>(
      CultureInfo culture)
      where TClass : Resource
    {
      return this.GetResources(culture, typeof (TClass).Name);
    }

    /// <summary>
    /// Get <see cref="T:System.Linq.IQueryable" /> object for querying and sorting <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> items.
    /// </summary>
    /// <param name="classId">The key of the resource set to get.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <returns><see cref="T:System.Linq.IOrderedQueryable`1" /></returns>
    public abstract IQueryable<ResourceEntry> GetResources(
      CultureInfo culture,
      string classId);

    /// <summary>
    /// Gets <see cref="T:System.Linq.IQueryable`1" /> object with the localized versions of
    /// the invariant culture resources for the specified cultures.
    /// </summary>
    /// <remarks>
    /// This method will populate non-existing locales with the empty ResourceEntry objects.
    /// </remarks>
    /// <param name="invariantCultureResources">
    /// The list of invariant culture resources for which the localized version will be populated.
    /// </param>
    /// <param name="cultures">
    /// The list of cultures for which the localized versions should be populated.
    /// </param>
    /// <returns>
    /// <see cref="T:System.Linq.IQueryable`1" /> object that contains the list of resources for all
    /// the specified cultures.
    /// </returns>
    public abstract IQueryable<ResourceEntry> GetResourcesForCultures(
      IQueryable<ResourceEntry> invariantCultureResources,
      params CultureInfo[] cultures);

    /// <summary>
    /// Loads a dictionary of string keys and values from external file if such is defined for the
    /// specified culture, otherwise returns null.
    /// </summary>
    /// <param name="classId">The key of the resource set to get.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized. Note that if the resource is not localized for this culture,
    /// the lookup will fall back using the culture's <see cref="P:System.Globalization.CultureInfo.Parent" />
    /// property, stopping after checking in the neutral culture.
    /// 
    /// If this value is null, the <see cref="T:System.Globalization.CultureInfo" /> is obtained using the
    /// culture's <see cref="!:Telerik.Sitefinity.Services.SystemManager.CurrentContext.Culture" /> property.
    /// </param>
    /// <returns>A dictionary of keys and values.</returns>
    public abstract IDictionary<string, string> LoadResource(
      CultureInfo culture,
      string classId);

    /// <summary>
    /// Creates an empty dictionary of cultures and corresponding resources for the specified class ID
    /// if external file is defined for the specified class, otherwise returns null.
    /// </summary>
    /// <param name="classId">The key of the resource set to get.</param>
    /// <returns>A dictionary of cultures and corresponding resources if class is defined, otherwise returns null.</returns>
    public abstract IDictionary<CultureInfo, IDictionary<string, string>> GetResourceSet(
      string classId);

    /// <summary>Deletes the resource entry.</summary>
    /// <param name="culture">The culture of the resource entry to be deleted.</param>
    /// <param name="classId">The class pageId of the resource entry to be deleted.</param>
    /// <param name="key">The key of the resource entry to be deleted.</param>
    public abstract void DeleteResourceEntry(CultureInfo culture, string classId, string key);

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
    public override object GetItem(Type itemType, Guid id) => throw new NotSupportedException();

    /// <summary>
    /// Get item by primary key without throwing exceptions if it doesn't exist
    /// </summary>
    /// <param name="itemType">Type of the item to get</param>
    /// <param name="id">Primary key</param>
    /// <returns>Item or default value</returns>
    public override object GetItemOrDefault(Type itemType, Guid id) => throw new NotSupportedException();

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
      throw new NotSupportedException();
    }

    /// <summary>
    /// Marks the provided persistent item for deletion.
    /// The item is deleted form the storage when the transaction is committed.
    /// </summary>
    /// <param name="item">The item to be deleted.</param>
    public override void DeleteItem(object item) => throw new NotSupportedException();

    /// <summary>Get a list of types served by this manager</summary>
    /// <returns></returns>
    public override Type[] GetKnownTypes() => new Type[1]
    {
      typeof (ResourceEntry)
    };
  }
}
