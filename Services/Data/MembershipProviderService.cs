// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Data.MembershipProviderService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Linq.Dynamic;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Data
{
  /// <summary>
  /// Provides information about registered user and role providers.
  /// </summary>
  public class MembershipProviderService : IMembershipProviderService
  {
    /// <summary>The web service relative URL.</summary>
    internal const string WebServiceUrl = "Sitefinity/Services/MembershipProviderService";

    private CollectionContext<DataProviderViewModel> GenerateViewCollection(
      IQueryable<DataProviderViewModel> providers,
      string filter)
    {
      int num = providers.Count<DataProviderViewModel>();
      if (!string.IsNullOrEmpty(filter))
        providers = providers.Where<DataProviderViewModel>(filter);
      return new CollectionContext<DataProviderViewModel>((IEnumerable<DataProviderViewModel>) providers)
      {
        TotalCount = num
      };
    }

    /// <inheritdoc />
    public CollectionContext<DataProviderViewModel> GetRoleProviders(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestAuthentication(true);
      return this.GenerateViewCollection(ManagerBase<RoleDataProvider>.StaticProvidersCollection.Select<RoleDataProvider, DataProviderViewModel>((Func<RoleDataProvider, DataProviderViewModel>) (up => new DataProviderViewModel()
      {
        Title = up.Title,
        Name = up.Name
      })).AsQueryable<DataProviderViewModel>(), filter);
    }

    /// <inheritdoc />
    public CollectionContext<DataProviderViewModel> GetUserProviders(
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      ServiceUtility.RequestAuthentication(true);
      return this.GenerateViewCollection(UserManager.GetManager().GetContextProviders().Select<DataProviderBase, DataProviderViewModel>((Func<DataProviderBase, DataProviderViewModel>) (up => new DataProviderViewModel()
      {
        Title = up.Title,
        Name = up.Name
      })).AsQueryable<DataProviderViewModel>(), filter);
    }
  }
}
