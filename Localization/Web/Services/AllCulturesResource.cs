// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.AllCulturesResource
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Represents the service which works with resources in all available cultures.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class AllCulturesResource : IAllCulturesResource
  {
    /// <summary>Gets the resources.</summary>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<ResourceEntry> GetResources(
      string classId,
      string key,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return AllCulturesResource.GetResourcesInternal(ServiceUtility.DecodeServiceUrl(classId), key, provider, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the resources in XML.</summary>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sort.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<ResourceEntry> GetResourcesInXml(
      string classId,
      string key,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return AllCulturesResource.GetResourcesInternal(ServiceUtility.DecodeServiceUrl(classId), key, provider, sortExpression, skip, take, filter);
    }

    /// <summary>Saves the resource.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="cultureName">The UI culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public ResourceEntry SaveResource(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.SaveAndReturnResource(propertyBag, cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    /// <summary>Saves the resource in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="cultureName">The UI culture.</param>
    /// <param name="classId">The class pageId.</param>
    /// <param name="key">The key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public ResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string cultureName,
      string classId,
      string key,
      string provider)
    {
      return LocalizationResources.SaveAndReturnResource(propertyBag, cultureName, ServiceUtility.DecodeServiceUrl(classId), key, provider);
    }

    private static CollectionContext<ResourceEntry> GetResourcesInternal(
      string classId,
      string key,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ResourceManager manager = Res.GetManager(provider);
      CultureInfo[] andBackendCultures = Config.Get<ResourcesConfig>().FrontendAndBackendCultures;
      IList<ResourceEntry> source1 = (IList<ResourceEntry>) new List<ResourceEntry>();
      ResourceEntry resourceEntry1 = string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(key) ? AllCulturesResource.CreateEmptyResourceEntry(CultureInfo.InvariantCulture) : manager.GetResources(CultureInfo.InvariantCulture, classId).Where<ResourceEntry>("Key = \"" + key + "\"").SingleOrDefault<ResourceEntry>();
      source1.Add(resourceEntry1);
      foreach (CultureInfo cultureInfo in andBackendCultures)
      {
        ResourceEntry resourceEntry2;
        if (string.IsNullOrEmpty(classId) || string.IsNullOrEmpty(key))
        {
          resourceEntry2 = AllCulturesResource.CreateEmptyResourceEntry(cultureInfo);
        }
        else
        {
          resourceEntry2 = manager.GetResources(cultureInfo, classId).Where<ResourceEntry>("Key = \"" + key + "\"").SingleOrDefault<ResourceEntry>();
          if (!object.Equals((object) resourceEntry2.Culture, (object) cultureInfo))
            resourceEntry2 = new ResourceEntry(resourceEntry2.ClassId, cultureInfo, resourceEntry2.Key, string.Empty, string.Empty, resourceEntry2.LastModified);
        }
        if (resourceEntry2 == null)
          resourceEntry2 = AllCulturesResource.CreateEmptyResourceEntry(cultureInfo);
        source1.Add(resourceEntry2);
      }
      IQueryable<ResourceEntry> source2 = source1.AsQueryable<ResourceEntry>();
      if (!string.IsNullOrEmpty(sortExpression))
        source2 = source2.OrderBy<ResourceEntry>(sortExpression);
      int num = source2.Count<ResourceEntry>();
      CollectionContext<ResourceEntry> resourcesInternal = new CollectionContext<ResourceEntry>(!string.IsNullOrEmpty(filter) ? (IEnumerable<ResourceEntry>) source2.Where<ResourceEntry>(filter).Skip<ResourceEntry>(skip).Take<ResourceEntry>(take) : (IEnumerable<ResourceEntry>) source2.Skip<ResourceEntry>(skip).Take<ResourceEntry>(take));
      resourcesInternal.TotalCount = num;
      ServiceUtility.DisableCache();
      return resourcesInternal;
    }

    private static CultureInfo GetCultureInfo(string cultureName) => !string.IsNullOrEmpty(cultureName) ? (!(cultureName.ToUpperInvariant() == "INVARIANT") ? new CultureInfo(cultureName) : CultureInfo.InvariantCulture) : CultureInfo.InvariantCulture;

    private static ResourceEntry CreateEmptyResourceEntry(CultureInfo uiCulture) => new ResourceEntry(string.Empty, uiCulture, string.Empty)
    {
      BuiltIn = true,
      Description = string.Empty,
      LastModified = DateTime.UtcNow,
      Value = string.Empty
    };
  }
}
