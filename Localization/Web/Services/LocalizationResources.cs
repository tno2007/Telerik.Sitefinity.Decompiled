// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.LocalizationResources
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// WCF Rest service for the localization "resources" resource.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class LocalizationResources : ILocalizationResources
  {
    private const string ValuePropertyName = "Value";
    private const string DescriptionPropertyName = "Description";
    private const string KeyPropertyName = "Key";

    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> in JSON format.
    /// </summary>
    /// <param name="cultureName">Name of the culture for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.</param>
    /// <param name="classId">The pageId of the class for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.</param>
    /// <param name="provider">The name of the resource provider from which the resources should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved resources.</param>
    /// <param name="skip">The number of resources to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of resources to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved resources.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with resource entry items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<ResourceEntry> GetResources(
      string cultureName,
      string classId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return LocalizationResources.GetResourcesInternal(cultureName, ServiceUtility.DecodeServiceUrl(classId), provider, sortExpression, skip, take, filter);
    }

    /// <summary>
    /// Gets the collection of <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> in XML format.
    /// </summary>
    /// <param name="cultureName">Name of the culture for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.</param>
    /// <param name="classId">The pageId of the class for which the <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> should be retrieved.</param>
    /// <param name="provider">The name of the resource provider from which the resources should be retrieved.</param>
    /// <param name="sortExpression">The sort expression used to order the retrieved resources.</param>
    /// <param name="skip">The number of resources to skip before populating the collection (used primarily for paging).</param>
    /// <param name="take">The maximum number of resources to take in the collection (used primarily for paging).</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to filter the retrieved resources.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with resource entry items and other information about the retrieved collection.
    /// </returns>
    public CollectionContext<ResourceEntry> GetResourcesInXml(
      string cultureName,
      string classId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return LocalizationResources.GetResourcesInternal(cultureName, ServiceUtility.DecodeServiceUrl(classId), provider, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the single resource entry in JSON format.</summary>
    /// <param name="cultureName">Name of the culture to which the resource is defined for.</param>
    /// <param name="classId">The pageId of the class to which the resource belongs to.</param>
    /// <param name="key">The key of the resource.</param>
    /// <param name="provider">The name of the resource provider from which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be retrieved.</param>
    /// <returns>ResourceEntry object.</returns>
    public ResourceEntry GetResource(
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.GetSingleResource(cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    /// <summary>Gets the single resource entry in XML format.</summary>
    /// <param name="cultureName">Name of the culture to which the resource is defined for.</param>
    /// <param name="classId">The pageId of the class to which the resource belongs to.</param>
    /// <param name="key">The key of the resource.</param>
    /// <param name="provider">The name of the resource provider from which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be retrieved.</param>
    /// <returns>ResourceEntry object.</returns>
    public ResourceEntry GetResourceInXml(
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.GetSingleResource(cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    /// <summary>
    /// Saves the resource and returns the saved version of the resources in JSON format.
    /// </summary>
    /// <param name="propertyBag">The array of ResourceEntry properties that should be persisted. The first array contains
    /// properties, while the second array holds property name in its first dimension and
    /// property value in its second dimension.</param>
    /// <param name="cultureName">Name of the culture for which the resource should be saved.</param>
    /// <param name="classId">The pageId of the class for which the resource should be saved.</param>
    /// <param name="key">The key of the resource for which the resource should be saved.</param>
    /// <param name="provider">The name of the resource provider on which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be saved.</param>
    /// <returns>
    /// Newly created or updated <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> object in JSON format.
    /// </returns>
    /// <remarks>
    /// If the resource to be saved does not exist, new resource will be created. If the resource,
    /// however, does exist the existing resource will be update.
    /// </remarks>
    public ResourceEntry SaveResource(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.SaveAndReturnResource(propertyBag, cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    /// <summary>
    /// Saves the resource and returns the saved version of the resources in XML format.
    /// </summary>
    /// <param name="propertyBag">The array of ResourceEntry properties that should be persisted. The first array contains
    /// properties, while the second array holds property name in its first dimension and
    /// property value in its second dimension.</param>
    /// <param name="cultureName">Name of the culture for which the resource should be saved.</param>
    /// <param name="classId">The pageId of the class for which the resource should be saved.</param>
    /// <param name="key">The key of the resource for which the resource should be saved.</param>
    /// <param name="provider">The name of the resource provider on which the <see cref="T:Telerik.Sitefinity.Localization.Data.ResourceEntry" /> should be saved.</param>
    /// <returns>
    /// Newly created or updated ResourceEntry object in XML format.
    /// </returns>
    /// <remarks>
    /// If the resource to be saved does not exist, new resource will be created. If the resource,
    /// however, does exist the existing resource will be update.
    /// </remarks>
    public ResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.SaveAndReturnResource(propertyBag, cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    /// <summary>
    /// Deletes the resource entry and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The name of the resource provider from which the resource entry should be deleted.</param>
    /// <returns></returns>
    public bool DeleteResource(string cultureName, string classId, string key, string provider) => LocalizationResources.DeleteSingleResource(cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);

    /// <summary>
    /// Deletes the resource entry and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="cultureName">Name of the culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The name of the resource provider from which the resource entry should be deleted.</param>
    /// <returns></returns>
    public bool DeleteResourceInXml(
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.DeleteSingleResource(cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    private static CollectionContext<ResourceEntry> GetResourcesInternal(
      string cultureName,
      string classId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ResourceManager manager = Res.GetManager(provider);
      CultureInfo cultureInfo = LocalizationResources.GetCultureInfo(cultureName);
      IQueryable<ResourceEntry> source = !string.IsNullOrEmpty(classId) ? manager.GetResources(cultureInfo, classId) : manager.GetResources(cultureInfo);
      if (!string.IsNullOrEmpty(sortExpression))
        source = source.OrderBy<ResourceEntry>(sortExpression);
      if (!string.IsNullOrEmpty(filter))
        source = source.Where<ResourceEntry>(filter);
      int num = source.Count<ResourceEntry>();
      CollectionContext<ResourceEntry> resourcesInternal = new CollectionContext<ResourceEntry>((IEnumerable<ResourceEntry>) source.Skip<ResourceEntry>(skip).Take<ResourceEntry>(take));
      resourcesInternal.TotalCount = num;
      ServiceUtility.DisableCache();
      return resourcesInternal;
    }

    /// <summary>Saves the and return resource.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="cultureName">Name of the culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public static ResourceEntry SaveAndReturnResource(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      for (int index = 0; index < propertyBag.GetLength(0); ++index)
      {
        if (dictionary.ContainsKey(propertyBag[index][0]))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFPropertyDuplicate.Arrange((object) propertyBag[index][0]), (Exception) null);
        dictionary.Add(propertyBag[index][0], propertyBag[index][1]);
      }
      ResourceManager manager = Res.GetManager(provider);
      CultureInfo cultureInfo = LocalizationResources.GetCultureInfo(cultureName);
      if (string.IsNullOrEmpty(classId))
        classId = dictionary["ClassId"];
      if (string.IsNullOrEmpty(key))
        key = dictionary["Key"];
      string predicate = string.Format("{0} = \"{1}\"", (object) "Key", (object) key);
      ResourceEntry resourceEntry = manager.GetResources(cultureInfo, classId).Where<ResourceEntry>(predicate).SingleOrDefault<ResourceEntry>();
      if (resourceEntry == null || !object.Equals((object) resourceEntry.Culture, (object) cultureInfo))
        resourceEntry = manager.AddItem(cultureInfo, classId, key, string.Empty, string.Empty);
      if (dictionary.ContainsKey("Value"))
        resourceEntry.Value = dictionary["Value"];
      if (dictionary.ContainsKey("Description"))
        resourceEntry.Description = dictionary["Description"];
      try
      {
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        if (!Exceptions.HandleException(ex, ExceptionPolicyName.IgnoreExceptions))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
        throw;
      }
      ServiceUtility.DisableCache();
      return resourceEntry;
    }

    private static ResourceEntry GetSingleResource(
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      ResourceEntry resourceOrEmpty = Res.GetManager(provider).GetResourceOrEmpty(LocalizationResources.GetCultureInfo(cultureName), classId, key);
      ServiceUtility.DisableCache();
      return resourceOrEmpty;
    }

    private static bool DeleteSingleResource(
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      ResourceManager manager = Res.GetManager(provider);
      manager.DeleteItem(LocalizationResources.GetCultureInfo(cultureName), classId, key);
      manager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    private static CultureInfo GetCultureInfo(string cultureName) => !string.IsNullOrEmpty(cultureName) ? (!cultureName.Equals("invariant", StringComparison.OrdinalIgnoreCase) ? CultureInfo.GetCultureInfo(cultureName) : CultureInfo.InvariantCulture) : CultureInfo.InvariantCulture;
  }
}
