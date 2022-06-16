// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.ResourceManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Globalization;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Data;

namespace Telerik.Sitefinity.Localization
{
  /// <summary>Represents a class for managing resources.</summary>
  public class ResourceManager : ManagerBase<ResourceDataProvider>
  {
    /// <summary>
    /// Initializes a new instance of Res class with the default provider.
    /// </summary>
    public ResourceManager()
    {
    }

    /// <summary>
    /// Initializes a new instance of Res class and sets the specified provider.
    /// </summary>
    /// <param name="providerName">The name of the provider. If empty string or null the default provider is set.</param>
    public ResourceManager(string providerName)
      : base(providerName)
    {
    }

    /// <summary>Gets all registered resource classes.</summary>
    /// <returns>A string array of class names.</returns>
    public virtual string[] GetAllClassIds() => this.Provider.GetAllClassIds();

    /// <summary>Gets all registered resource classes info.</summary>
    /// <returns>A string array of class names.</returns>
    public virtual ObjectInfoAttribute[] GetAllClassInfos() => this.Provider.GetAllClassInfos();

    public virtual ObjectInfoAttribute[] GetAllClassInfos(CultureInfo culture) => this.Provider.GetAllClassInfos(culture);

    /// <summary>
    /// Gets the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object for the specified class pageId, culture and key. If the resource
    /// does not exist, a new empty <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object with specified classId, culture and key
    /// will be created and returned.
    /// </summary>
    /// <param name="classId">The class pageId of the resource.</param>
    /// <param name="cultureInfo">The culture for which the resource should be returned.</param>
    /// <param name="key">The key of the resources that ought to be returned.</param>
    /// <returns><see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object.</returns>
    public virtual ResourceEntry GetResourceOrEmpty(
      CultureInfo cultureInfo,
      string classId,
      string key)
    {
      return this.Provider.GetResourceOrEmpty(cultureInfo, classId, key);
    }

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
      return this.Provider.GetResources<TClass>(culture);
    }

    /// <summary>
    /// Get <see cref="T:System.Linq.IQueryable`1" /> object for querying and sorting <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> items.
    /// This method returns entries from all classes and all cultures combined.
    /// Note, calling this method without where or take clause can result in slow performance.
    /// </summary>
    /// <returns><see cref="T:System.Linq.IQueryable" /></returns>
    public virtual IQueryable<ResourceEntry> GetResources(CultureInfo culture) => this.Provider.GetResources(culture);

    /// <summary>
    /// Get <see cref="T:System.Linq.IQueryable`1" /> object for querying and sorting <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> items.
    /// </summary>
    /// <param name="classId">The key of the resource set to get.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <returns><see cref="T:System.Linq.IQueryable" /></returns>
    public virtual IQueryable<ResourceEntry> GetResources(
      CultureInfo culture,
      string classId)
    {
      return this.Provider.GetResources(culture, classId);
    }

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
    public virtual IQueryable<ResourceEntry> GetResourcesForCultures(
      IQueryable<ResourceEntry> invariantCultureResources,
      params CultureInfo[] cultures)
    {
      return this.Provider.GetResourcesForCultures(invariantCultureResources, cultures);
    }

    /// <summary>
    /// Gets a list of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> objects.
    /// </summary>
    /// <param name="classId">The key of the resource set to get.</param>
    /// <param name="culture">
    /// The <see cref="T:System.Globalization.CultureInfo" /> object that represents the culture for
    /// which the resource is localized.
    /// </param>
    /// <param name="filterExpression">
    /// String representation of the where clause of LINQ query.
    /// </param>
    /// <param name="sortExpression">
    /// String representation of the order by clause of LINQ query.
    /// </param>
    /// <param name="skip">
    /// The number of items to skip from the result set.
    /// </param>
    /// <param name="take">
    /// The number of items to take from the result set.
    /// </param>
    /// <returns>A list of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> objects.</returns>
    public ResourceEntry[] GetResources(
      string culture,
      string classId,
      string filterExpression,
      string sortExpression,
      int skip,
      int take)
    {
      IQueryable<ResourceEntry> source = this.Provider.GetResources(CultureInfo.GetCultureInfo(culture), classId);
      if (!string.IsNullOrEmpty(filterExpression))
        source = source.Where<ResourceEntry>(filterExpression);
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<ResourceEntry>(sortExpression);
      if (skip != 0)
        source = source.Skip<ResourceEntry>(skip);
      if (take != 0)
        source = source.Take<ResourceEntry>(take);
      return source.ToArray<ResourceEntry>();
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
    public virtual ResourceEntry AddItem(
      CultureInfo culture,
      string classId,
      string key,
      string value,
      string description)
    {
      return this.Provider.AddItem(culture, classId, key, value, description);
    }

    /// <summary>Deletes the item.</summary>
    /// <param name="culture">The culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    public virtual void DeleteItem(CultureInfo culture, string classId, string key) => this.Provider.DeleteResourceEntry(culture, classId, key);

    /// <summary>Collection of data provider settings.</summary>
    protected internal override ConfigElementDictionary<string, DataProviderSettings> ProvidersSettings => Config.Get<ResourcesConfig>().Providers;

    /// <summary>Gets the default provider for this manager.</summary>
    protected internal override GetDefaultProvider DefaultProviderDelegate => (GetDefaultProvider) (() => Res.DefaultProviderName);

    /// <summary>The name of the module that this manager belongs to.</summary>
    public override string ModuleName => (string) null;

    /// <summary>Gets a manger instance for the default data provider.</summary>
    /// <returns>The manager instance.</returns>
    public static ResourceManager GetManager() => ManagerBase<ResourceDataProvider>.GetManager<ResourceManager>();

    /// <summary>
    /// Gets a manger instance for the specified data provider.
    /// </summary>
    /// <param name="providerName">The name of the data provider.</param>
    /// <returns>The manager instance.</returns>
    public static ResourceManager GetManager(string providerName) => ManagerBase<ResourceDataProvider>.GetManager<ResourceManager>(providerName);
  }
}
