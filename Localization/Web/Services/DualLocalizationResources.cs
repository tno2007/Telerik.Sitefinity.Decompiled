// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.DualLocalizationResources
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
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization.Data;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// Service for working with localization resources of two cultures at the same time.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class DualLocalizationResources : IDualLocalizationResources
  {
    /// <summary>Gets the all the resources.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<DualResourceEntry> GetResources(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return DualLocalizationResources.GetResourcesInternal(displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), provider, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the resources in XML.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="sort">The sort.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<DualResourceEntry> GetResourcesInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string provider,
      string sort,
      int skip,
      int take,
      string filter)
    {
      return DualLocalizationResources.GetResourcesInternal(displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), provider, sort, skip, take, filter);
    }

    /// <summary>Gets the resource.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public DualResourceEntry GetResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.GetSingleResource(displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), displayKey, editKey, provider);
    }

    /// <summary>Gets the resource in XML.</summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public DualResourceEntry GetResourceInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.GetSingleResource(displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), displayKey, editKey, provider);
    }

    /// <summary>Saves the resource.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public DualResourceEntry SaveResource(
      string[][] propertyBag,
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.SaveAndReturnResource(propertyBag, displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), displayKey, editKey, provider);
    }

    /// <summary>Saves the resource in XML.</summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public DualResourceEntry SaveResourceInXml(
      string[][] propertyBag,
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.SaveAndReturnResource(propertyBag, displayUICulture, editUICulture, ServiceUtility.DecodeServiceUrl(displayClassId), ServiceUtility.DecodeServiceUrl(editClassId), displayKey, editKey, provider);
    }

    /// <summary>
    /// Deletes the resource and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in JSON format.
    /// </summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public bool DeleteResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.DeleteSingleResource(displayUICulture, editUICulture, displayClassId, editClassId, displayKey, editKey, provider);
    }

    /// <summary>
    /// Deletes the resource and returns true if the resource has been deleted; otherwise false.
    /// Result is returned in XML format.
    /// </summary>
    /// <param name="displayUICulture">The display UI culture.</param>
    /// <param name="editUICulture">The edit UI culture.</param>
    /// <param name="displayClassId">The display class pageId.</param>
    /// <param name="editClassId">The edit class pageId.</param>
    /// <param name="displayKey">The display key.</param>
    /// <param name="editKey">The edit key.</param>
    /// <param name="provider">The provider.</param>
    /// <returns></returns>
    public bool DeleteResourceInXml(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      return DualLocalizationResources.DeleteSingleResource(displayUICulture, editUICulture, displayClassId, editClassId, displayKey, editKey, provider);
    }

    private static CollectionContext<DualResourceEntry> GetResourcesInternal(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ResourceManager manager = Res.GetManager(provider);
      CultureInfo cultureInfo = DualLocalizationResources.GetCultureInfo(displayUICulture);
      IQueryable<ResourceEntry> source1 = !string.IsNullOrEmpty(displayClassId) ? manager.GetResources(cultureInfo, displayClassId) : manager.GetResources(cultureInfo);
      if (!string.IsNullOrEmpty(sortExpression))
        source1 = source1.OrderBy<ResourceEntry>(sortExpression);
      int num = source1.Count<ResourceEntry>();
      IEnumerable<ResourceEntry> source2 = !string.IsNullOrEmpty(filter) ? (IEnumerable<ResourceEntry>) source1.Where<ResourceEntry>(filter).Skip<ResourceEntry>(skip).Take<ResourceEntry>(take) : (IEnumerable<ResourceEntry>) source1.Skip<ResourceEntry>(skip).Take<ResourceEntry>(take);
      IList<DualResourceEntry> items = (IList<DualResourceEntry>) new List<DualResourceEntry>();
      foreach (ResourceEntry displayItem in source2.ToList<ResourceEntry>())
      {
        string classId = string.IsNullOrEmpty(editClassId) ? displayItem.ClassId : editClassId;
        ResourceEntry editItem = manager.GetResources(DualLocalizationResources.GetCultureInfo(editUICulture), classId).Where<ResourceEntry>("Key = \"" + displayItem.Key + "\"").SingleOrDefault<ResourceEntry>();
        items.Add(DualLocalizationResources.MergeItems(displayItem, editItem));
      }
      CollectionContext<DualResourceEntry> resourcesInternal = new CollectionContext<DualResourceEntry>((IEnumerable<DualResourceEntry>) items);
      resourcesInternal.TotalCount = num;
      ServiceUtility.DisableCache();
      return resourcesInternal;
    }

    private static DualResourceEntry SaveAndReturnResource(
      string[][] propertyBag,
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      Dictionary<string, string> dictionary = new Dictionary<string, string>();
      for (int index = 0; index < propertyBag.GetLength(0); ++index)
      {
        if (dictionary.ContainsKey(propertyBag[index][0]))
          throw new WebProtocolException(HttpStatusCode.InternalServerError, "ERROR: The property bag contains duplicate property '{0}', which is not allowed.".Arrange((object) propertyBag[index][0]), (Exception) null);
        dictionary.Add(propertyBag[index][0], propertyBag[index][1]);
      }
      ResourceManager manager = Res.GetManager(provider);
      CultureInfo cultureInfo = DualLocalizationResources.GetCultureInfo(editUICulture);
      if (string.IsNullOrEmpty(editClassId))
        editClassId = dictionary["EditClassId"];
      if (string.IsNullOrEmpty(editKey))
        editKey = dictionary["EditKey"];
      ResourceEntry editItem = manager.GetResources(cultureInfo, editClassId).Where<ResourceEntry>("Key = \"" + editKey + "\"").SingleOrDefault<ResourceEntry>() ?? manager.AddItem(cultureInfo, editClassId, editKey, string.Empty, string.Empty);
      if (dictionary.ContainsKey("EditValue"))
        editItem.Value = dictionary["EditValue"];
      if (dictionary.ContainsKey("EditDescription"))
        editItem.Description = dictionary["EditDescription"];
      try
      {
        manager.SaveChanges();
      }
      catch (Exception ex)
      {
        throw new WebProtocolException(HttpStatusCode.InternalServerError, "ERROR: Item could not have been saved.", ex.InnerException);
      }
      return DualLocalizationResources.MergeItems(manager.GetResources(DualLocalizationResources.GetCultureInfo(displayUICulture), displayClassId).Where<ResourceEntry>("Key = \"" + displayKey).SingleOrDefault<ResourceEntry>(), editItem);
    }

    private static DualResourceEntry GetSingleResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      ResourceManager manager = Res.GetManager(provider);
      ResourceEntry displayItem = manager.GetResources(DualLocalizationResources.GetCultureInfo(displayUICulture), displayClassId).Where<ResourceEntry>("Key = \"" + displayKey + "\"").SingleOrDefault<ResourceEntry>();
      ResourceEntry editItem = manager.GetResources(DualLocalizationResources.GetCultureInfo(editUICulture), editClassId).Where<ResourceEntry>("Key = \"" + editKey + "\"").SingleOrDefault<ResourceEntry>();
      ServiceUtility.DisableCache();
      return DualLocalizationResources.MergeItems(displayItem, editItem);
    }

    private static bool DeleteSingleResource(
      string displayUICulture,
      string editUICulture,
      string displayClassId,
      string editClassId,
      string displayKey,
      string editKey,
      string provider)
    {
      ResourceManager manager = Res.GetManager(provider);
      manager.DeleteItem(DualLocalizationResources.GetCultureInfo(displayUICulture), displayClassId, displayKey);
      manager.DeleteItem(DualLocalizationResources.GetCultureInfo(editUICulture), editClassId, editKey);
      return true;
    }

    private static CultureInfo GetCultureInfo(string cultureName) => !string.IsNullOrEmpty(cultureName) ? (!(cultureName.ToUpperInvariant() == "INVARIANT") ? CultureInfo.GetCultureInfo(cultureName) : CultureInfo.InvariantCulture) : CultureInfo.InvariantCulture;

    private static DualResourceEntry MergeItems(
      ResourceEntry displayItem,
      ResourceEntry editItem)
    {
      return new DualResourceEntry()
      {
        DisplayBuiltIn = displayItem == null || displayItem.BuiltIn,
        DisplayClassId = displayItem == null ? string.Empty : displayItem.ClassId,
        DisplayDescription = displayItem == null ? string.Empty : displayItem.Description,
        DisplayKey = displayItem == null ? string.Empty : displayItem.Key,
        DisplayLastModified = displayItem == null ? DateTime.UtcNow : displayItem.LastModified,
        DisplayUICulture = displayItem == null ? string.Empty : displayItem.CultureName,
        DisplayValue = displayItem == null ? string.Empty : displayItem.Value,
        EditBuiltIn = editItem == null || editItem.BuiltIn,
        EditClassId = editItem == null ? string.Empty : editItem.ClassId,
        EditDescription = editItem == null ? string.Empty : editItem.Description,
        EditKey = editItem == null ? string.Empty : editItem.Key,
        EditLastModified = editItem == null ? DateTime.UtcNow : editItem.LastModified,
        EditUICulture = editItem == null ? string.Empty : editItem.CultureName,
        EditValue = editItem == null ? string.Empty : editItem.Value
      };
    }
  }
}
