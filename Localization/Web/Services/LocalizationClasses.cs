// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Localization.Web.LocalizationClasses
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Localization.Web
{
  /// <summary>
  /// WCF Rest service for the localization "resources" resource.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class LocalizationClasses : ILocalizationClasses
  {
    /// <summary>
    /// Gets the collection of localization classes in JSON format.
    /// </summary>
    /// <param name="sortExpression">The sort expression used to sortExpression the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <param name="provider">The localization provider used to retrieve the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of localization classes.
    /// </returns>
    public CollectionContext<string> GetClasses(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      return LocalizationClasses.GetClassesInternal(sortExpression, skip, take, filter, provider);
    }

    /// <summary>
    /// Gets the collection of localization classes in XML format.
    /// </summary>
    /// <param name="sortExpression">The sort expression used to sort the localization classes.</param>
    /// <param name="skip">The number of classes to skip before taking the subset.</param>
    /// <param name="take">The number of classes to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ form used to filter the classes.</param>
    /// <param name="provider">The localization provider used to retrieve the classes.</param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the collection of localization classes.
    /// </returns>
    public CollectionContext<string> GetClassesInXml(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      return LocalizationClasses.GetClassesInternal(sortExpression, skip, take, filter, provider);
    }

    private static CollectionContext<string> GetClassesInternal(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      IQueryable<string> queryable = new List<string>((IEnumerable<string>) Res.GetManager(provider).GetAllClassIds()).AsQueryable<string>();
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<string>(filter);
      int num = queryable.Count<string>();
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<string>(sortExpression);
      if (skip > 0)
        queryable = queryable.Skip<string>(skip);
      if (take > 0)
        queryable = queryable.Take<string>(take);
      CollectionContext<string> classesInternal = new CollectionContext<string>((IEnumerable<string>) queryable);
      classesInternal.TotalCount = num;
      classesInternal.IsGeneric = true;
      ServiceUtility.DisableCache();
      return classesInternal;
    }
  }
}
