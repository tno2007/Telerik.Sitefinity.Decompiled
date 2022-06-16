// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Web.LocalizationCultures
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Localization.Configuration;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Configuration.Web
{
  /// <summary>
  /// WCF Rest service for the localization "resources" resource.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class LocalizationCultures : ILocalizationCultures
  {
    /// <summary>
    /// Gets the collection <see cref="T:Telerik.Sitefinity.Localization.Configuration.CultureElement" /> objects in JSON format, which represent
    /// the cultures used by the current installation and defined through the configuration.
    /// </summary>
    /// <param name="sortExpression">The sort expression used to order cultures.</param>
    /// <param name="skip">The number of cultures to skip before taking the subset.</param>
    /// <param name="take">The number of cultures to take in the subset.</param>
    /// <param name="filter">The filter expression in dynamic LINQ format used to format cultures.</param>
    /// <param name="provider">
    /// The name of the configuration provider used to retrieved the list of cultures.
    /// </param>
    /// <returns>
    /// <see cref="T:Telerik.Sitefinity.Web.Services.CollectionContext`1" /> object with the list of CultureElement
    /// objects.
    /// </returns>
    public CollectionContext<CultureElement> GetCultures(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return LocalizationCultures.GetCulturesInternal(sortExpression, skip, take, filter, provider);
    }

    private static CollectionContext<CultureElement> GetCulturesInternal(
      string sortExpression,
      int skip,
      int take,
      string filter,
      string provider)
    {
      ConfigManager manager = Config.GetManager(provider);
      List<CultureElement> source = new List<CultureElement>();
      List<CultureElement> collection1 = new List<CultureElement>((IEnumerable<CultureElement>) manager.GetSection<ResourcesConfig>().BackendCultures.Values);
      List<CultureElement> collection2 = new List<CultureElement>((IEnumerable<CultureElement>) manager.GetSection<ResourcesConfig>().Cultures.Values);
      source.AddRange((IEnumerable<CultureElement>) collection2);
      source.AddRange((IEnumerable<CultureElement>) collection1);
      List<CultureElement> list = source.GroupBy<CultureElement, string>((Func<CultureElement, string>) (x => x.UICulture)).Select<IGrouping<string, CultureElement>, CultureElement>((Func<IGrouping<string, CultureElement>, CultureElement>) (g => g.FirstOrDefault<CultureElement>())).ToList<CultureElement>();
      list.Insert(0, new CultureElement((ConfigElement) Config.Get<ResourcesConfig>().Cultures)
      {
        Key = "Invariant",
        UICulture = CultureInfo.InvariantCulture.Name
      });
      IQueryable<CultureElement> queryable = list.AsQueryable<CultureElement>();
      int num = queryable.Count<CultureElement>();
      if (!string.IsNullOrEmpty(filter))
        queryable = queryable.Where<CultureElement>(filter);
      if (skip > 0)
        queryable = queryable.Skip<CultureElement>(skip);
      if (take > 0)
        queryable = queryable.Take<CultureElement>(take);
      if (!string.IsNullOrEmpty(sortExpression))
        queryable = queryable.OrderBy<CultureElement>(sortExpression);
      ServiceUtility.DisableCache();
      return new CollectionContext<CultureElement>((IEnumerable<CultureElement>) queryable)
      {
        TotalCount = num
      };
    }
  }
}
