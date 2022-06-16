// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Newsletters.Services.DynamicListService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Modules.Newsletters.Composition;
using Telerik.Sitefinity.Modules.Newsletters.Data;
using Telerik.Sitefinity.Modules.Newsletters.DynamicLists;
using Telerik.Sitefinity.Modules.Newsletters.Services.ViewModel;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.Newsletters.Services
{
  /// <summary>
  /// Service for working with dynamic lists of the newsletters module.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class DynamicListService : IDynamicListService
  {
    /// <summary>
    /// Gets a collection of dynamic lists information from a given provider.
    /// </summary>
    /// <param name="dynamicListProviderName">The name of the dynamic list provider.</param>
    /// <returns>A collection context of the <see cref="!:DynamiclistSettingsViewModel" /> type.</returns>
    public CollectionContext<DynamicListInfoViewModel> GetDynamicListsInfo(
      string dynamicListProviderName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetDynamicListsInternal(dynamicListProviderName);
    }

    /// <summary>
    /// Gets a collection of dynamic lists information from a given provider.
    /// </summary>
    /// <param name="dynamicListProviderName">The name of the dynamic list provider.</param>
    /// <returns>A collection context of the <see cref="!:DynamiclistSettingsViewModel" /> type.</returns>
    public CollectionContext<DynamicListInfoViewModel> GetDynamicListsInfoInXml(
      string dynamicListProviderName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      return this.GetDynamicListsInternal(dynamicListProviderName);
    }

    private CollectionContext<DynamicListInfoViewModel> GetDynamicListsInternal(
      string dynamicListProviderName)
    {
      IDynamicListProvider dynamicListProvider = NewslettersManager.GetManager().GetDynamicListProvider(dynamicListProviderName);
      IEnumerable<DynamicListInfo> dynamicLists = dynamicListProvider.GetDynamicLists();
      List<DynamicListInfoViewModel> items = new List<DynamicListInfoViewModel>();
      foreach (DynamicListInfo source in dynamicLists)
      {
        DynamicListInfoViewModel target1 = new DynamicListInfoViewModel();
        Synchronizer.Synchronize(source, target1);
        target1.AvailableProperties.Clear();
        foreach (MergeTag availableProperty in (IEnumerable<MergeTag>) dynamicListProvider.GetAvailableProperties(source.Key))
        {
          MergeTagViewModel mergeTagViewModel = new MergeTagViewModel();
          MergeTagViewModel target2 = mergeTagViewModel;
          Synchronizer.Synchronize(availableProperty, target2);
          target1.AvailableProperties.Add(mergeTagViewModel);
        }
        items.Add(target1);
      }
      return new CollectionContext<DynamicListInfoViewModel>((IEnumerable<DynamicListInfoViewModel>) items)
      {
        TotalCount = items.Count
      };
    }

    private IList<MergeTag> GetPropertiesInternal(
      string listKey,
      string dynamicListProviderName)
    {
      return (IList<MergeTag>) NewslettersManager.GetManager().GetDynamicListProvider(dynamicListProviderName).GetAvailableProperties(listKey).ToList<MergeTag>();
    }
  }
}
