// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.UserProfiles.Web.Services.UserProfileTypesService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Web.Script.Serialization;
using Telerik.Sitefinity.Abstractions;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Modules.UserProfiles.Web.Services.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Modules.UserProfiles.Web.Services
{
  /// <summary>
  /// This web service is used to work with <see cref="!:NewsItem" /> objects.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class UserProfileTypesService : IUserProfileTypesService
  {
    /// <summary>
    /// Gets the manager to be used by the service. Concrete implementation of the service must provide this.
    /// </summary>
    /// <param name="providerName">Name of the provider with which the manager ought to be instantiated.</param>
    /// <returns>An instance of the manager.</returns>
    public MetadataManager GetManager(string providerName) => MetadataManager.GetManager(providerName, WcfContext.CurrentTransactionName);

    public CollectionContext<UserProfileTypeViewModel> GetUserProfileTypes(
      string userProfilesFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetUserProfileTypesInternal(userProfilesFilter, sortExpression, skip, take, filter, root);
    }

    public CollectionContext<UserProfileTypeViewModel> GetUserProfileTypesInXml(
      string userProfilesFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      return this.GetUserProfileTypesInternal(userProfilesFilter, sortExpression, skip, take, filter, root);
    }

    public bool BatchDeleteUserProfileTypes(string[] Ids, string providerName) => this.BatchDeleteUserProfileTypesInternal(Ids, providerName);

    public bool BatchDeleteUserProfileTypesInXml(string[] Ids, string providerName) => this.BatchDeleteUserProfileTypesInternal(Ids, providerName);

    public bool DeleteUserProfileType(string profileTypeId, string providerName) => this.DeleteUserProfileTypeInternal(profileTypeId, providerName);

    public bool DeleteUserProfileTypeInXml(
      string profileTypeId,
      string providerName,
      bool duplicate)
    {
      return this.DeleteUserProfileTypeInternal(profileTypeId, providerName);
    }

    public UserProfileTypeContext GetUserProfileType(
      string profileTypeId,
      string providerName,
      bool duplicate)
    {
      return this.GetUserProfileTypeInternal(profileTypeId, providerName);
    }

    public UserProfileTypeContext GetUserProfileTypeInXml(
      string profileTypeId,
      string providerName,
      bool duplicate)
    {
      return this.GetUserProfileTypeInternal(profileTypeId, providerName);
    }

    public UserProfileTypeContext SaveUserProfileType(
      UserProfileTypeContext profileTypeDataContext,
      string profileTypeId,
      string providerName,
      bool duplicate)
    {
      return this.SaveUserProfileTypeInternal(profileTypeDataContext, providerName);
    }

    public UserProfileTypeContext SaveUserProfileTypeInXml(
      UserProfileTypeContext profileTypeDataContext,
      string profileTypeId,
      string providerName,
      bool duplicate)
    {
      return this.SaveUserProfileTypeInternal(profileTypeDataContext, providerName);
    }

    public CollectionContext<UserProfileTypeViewModel> GetUserProfileTypesInternal(
      string userProfilesFilter,
      string sortExpression,
      int skip,
      int take,
      string filter,
      string root)
    {
      int? totalCount = new int?(0);
      IQueryable<UserProfileTypeViewModel> userProfileTypes = UserProfilesHelper.GetUserProfileTypes((string) null);
      DataProviderBase.SetExpressions<UserProfileTypeViewModel>(userProfileTypes, filter, sortExpression, new int?(skip), new int?(take), ref totalCount);
      CollectionContext<UserProfileTypeViewModel> profileTypesInternal = new CollectionContext<UserProfileTypeViewModel>((IEnumerable<UserProfileTypeViewModel>) userProfileTypes);
      profileTypesInternal.TotalCount = totalCount.Value;
      ServiceUtility.DisableCache();
      return profileTypesInternal;
    }

    public UserProfileTypeContext GetUserProfileTypeInternal(
      string profileTypeId,
      string providerName)
    {
      UserProfileTypeViewModel userProfileType = UserProfilesHelper.GetUserProfileType(new Guid(profileTypeId), providerName);
      ServiceUtility.DisableCache();
      return new UserProfileTypeContext()
      {
        Item = userProfileType
      };
    }

    public bool DeleteUserProfileTypeInternal(string profileTypeId, string providerName) => UserProfilesHelper.DeleteUserProfileType(new Guid(profileTypeId), providerName, false);

    public bool BatchDeleteUserProfileTypesInternal(string[] Ids, string providerName)
    {
      foreach (string id in Ids)
        this.DeleteUserProfileType(id, providerName);
      ServiceUtility.DisableCache();
      return true;
    }

    public UserProfileTypeContext SaveUserProfileTypeInternal(
      UserProfileTypeContext profileTypeDataContext,
      string providerName)
    {
      UserProfileTypeViewModel profileTypeData = profileTypeDataContext.Item;
      if (profileTypeData.Id == Guid.Empty)
        UserProfilesHelper.CreateUserProfileType(profileTypeData, providerName);
      else
        UserProfilesHelper.SaveUserProfileType(profileTypeData.Id, profileTypeData, providerName);
      ServiceUtility.DisableCache();
      return new UserProfileTypeContext()
      {
        Item = profileTypeData
      };
    }

    /// <summary>Saves the page in XML.</summary>
    /// <param name="content">The content.</param>
    /// <param name="profileTypeId">The user profile id.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="parentTaxonId">The parent taxon id.</param>
    /// <param name="templateId">The template id.</param>
    /// 
    ///             /// <param name="duplicate">if set to <c>true</c> the page will be duplicated.</param>
    /// <returns></returns>
    public string GetAllProfilesForUser(string userId)
    {
      ProvidersCollection<UserProfileDataProvider> staticProviders = UserProfileManager.GetManager().StaticProviders;
      Dictionary<string, Dictionary<string, object>> dictionary1 = new Dictionary<string, Dictionary<string, object>>();
      foreach (DataProviderBase dataProviderBase in (Collection<UserProfileDataProvider>) staticProviders)
      {
        foreach (UserProfile userProfile in (IEnumerable<UserProfile>) UserProfileManager.GetManager(dataProviderBase.Name).GetUserProfiles(Guid.Parse(userId)))
        {
          Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
          dictionary1.Add(userProfile.GetType().FullName, dictionary2);
          foreach (PropertyDescriptor property in TypeDescriptor.GetProperties((object) userProfile))
            dictionary2.Add(property.Name, property.GetValue((object) userProfile));
        }
      }
      return new JavaScriptSerializer().Serialize((object) dictionary1);
    }

    public string GetAllProfilesForUserInXml(string userId) => throw new NotImplementedException();

    public string SaveAllProfilesForUser(string profileData, string userId) => throw new NotImplementedException();

    public string SaveAllProfilesForUserInXml(string profileData, string userId) => throw new NotImplementedException();
  }
}
