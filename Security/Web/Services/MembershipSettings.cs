// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Security.Web.Services.MembershipSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration.Provider;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data.WcfHelpers;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Utilities.MS.ServiceModel.Web;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Security.Web.Services
{
  /// <summary>
  /// The service returning the settings for membership provider.
  /// </summary>
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class MembershipSettings : IMembershipSettings
  {
    /// <summary>
    /// Gets the settings for a membership provider.Returns result in JSON.
    /// </summary>
    /// <param name="provider">The membership provider name.</param>
    /// <returns></returns>
    public MembershipProviderSetting GetMembershipProvider(string provider)
    {
      ServiceUtility.DisableCache();
      return MembershipSettings.GetMembershipProviderInternal(provider);
    }

    /// <summary>
    /// Gets the settings for a membership provider.Returns result in XML.
    /// </summary>
    /// <param name="provider">The membership provider name.</param>
    /// <returns></returns>
    public MembershipProviderSetting GetMembershipProviderInXml(
      string provider)
    {
      ServiceUtility.DisableCache();
      return MembershipSettings.GetMembershipProviderInternal(provider);
    }

    /// <summary>
    /// Recovers the password of user. Returns results in JSON format.
    /// </summary>
    /// <param name="propertyBag">The property bag.</param>
    /// <param name="username">The username.</param>
    /// <param name="provider">The membership provider.</param>
    /// <param name="answer">The password answer.Needed only if membership provider has RequiresQuestionAndAnswer set to true.</param>
    /// <returns>The new password of user</returns>
    public string RecoverPasswordOfUser(string username, string provider, string answer)
    {
      ServiceUtility.DisableCache();
      return MembershipSettings.RecoverPasswordOfUserInternal(username, provider, answer);
    }

    /// <summary>
    /// Recovers the password of user. Returns results in XML format.
    /// </summary>
    /// <param name="username">The username.</param>
    /// <param name="provider">The membership provider.</param>
    /// <param name="answer">The password answer.Needed only if membership provider has RequiresQuestionAndAnswer set to true.</param>
    /// <returns>The new password of user</returns>
    public string RecoverPasswordOfUserInXml(string username, string provider, string answer) => this.RecoverPasswordOfUser(username, provider, answer);

    /// <summary>Changes user's password.</summary>
    /// <param name="passwordChangeData">Old and new password.</param>
    /// <param name="userId">Id of the user.</param>
    /// <param name="provider">The name of the membership provider.</param>
    public void ChangePassword(
      WcfPasswordChangeData passwordChangeData,
      string userId,
      string provider)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      ServiceUtility.DisableCache();
      MembershipSettings.ChangePasswordInternal(new Guid(userId), provider, passwordChangeData.OldPassword, passwordChangeData.NewPassword);
    }

    /// <summary>Gets the roles for users.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<UserRolesItem> GetRolesForUsers(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return MembershipSettings.GetRolesForUsersInternal(provider, sortExpression, skip, take, filter);
    }

    /// <summary>Gets the roles for users in XML.</summary>
    /// <param name="provider">The provider.</param>
    /// <param name="sortExpression">The sortExpression.</param>
    /// <param name="skip">The skip.</param>
    /// <param name="take">The take.</param>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public CollectionContext<UserRolesItem> GetRolesForUsersInXml(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      return MembershipSettings.GetRolesForUsersInternal(provider, sortExpression, skip, take, filter);
    }

    private static MembershipProviderSetting GetMembershipProviderInternal(
      string provider)
    {
      UserManager userManager = new UserManager(provider);
      return new MembershipProviderSetting()
      {
        EnablePasswordReset = userManager.Provider.EnablePasswordReset,
        EnablePasswordRetrieval = userManager.Provider.EnablePasswordRetrieval,
        Name = userManager.Provider.Name,
        RequiresQuestionAndAnswer = userManager.Provider.RequiresQuestionAndAnswer
      };
    }

    private static string RecoverPasswordOfUserInternal(
      string username,
      string provider,
      string answer)
    {
      string newPassword = (string) null;
      username = WcfHelper.DecodeWcfString(username);
      UserManager manager = UserManager.GetManager(provider);
      User user = manager.GetUser(username);
      if (user == null)
      {
        IQueryable<User> source = manager.GetUsers().Where<User>((Expression<Func<User, bool>>) (u => u.Email == username));
        if (source.Count<User>() != 1)
          return string.Empty;
        user = source.First<User>();
      }
      if (manager.EnablePasswordRetrieval)
      {
        try
        {
          newPassword = manager.GetPassword(user.UserName, answer);
        }
        catch
        {
        }
      }
      if (string.IsNullOrEmpty(newPassword) && manager.EnablePasswordReset)
      {
        newPassword = manager.ResetPassword(user.UserName, answer);
        manager.SaveChanges();
      }
      if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(manager.Provider.RecoveryMailAddress))
        UserManager.SendPasswordMail(manager, user, newPassword);
      return newPassword;
    }

    private static void ChangePasswordInternal(
      Guid userId,
      string provider,
      string oldPassword,
      string newPassword)
    {
      using (UserManager manager = UserManager.GetManager(provider))
      {
        User user = manager.GetUser(userId);
        try
        {
          if (!manager.ChangePassword(user, oldPassword, newPassword))
            throw new ProviderException(Res.Get<ErrorMessages>().ChangePasswordGeneralFailureText);
          try
          {
            manager.Provider.SuppressSecurityChecks = true;
            manager.SaveChanges();
          }
          finally
          {
            manager.Provider.SuppressSecurityChecks = false;
          }
          UserManager.SendPasswordMail(manager, user, newPassword);
        }
        catch (NotSupportedException ex)
        {
          throw ex;
        }
        catch (WebProtocolException ex)
        {
          throw ex;
        }
        catch (ProviderException ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex.InnerException);
        }
        catch (ArgumentException ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, ex.Message, ex.InnerException);
        }
        catch (UnauthorizedAccessException ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().YouAreNotAuthorizedTo.Arrange((object) Res.Get<SecurityResources>().ChangeUsersPassword), ex.InnerException);
        }
        catch (Exception ex)
        {
          throw new WebProtocolException(HttpStatusCode.InternalServerError, Res.Get<ErrorMessages>().WCFErrorOnSave, ex.InnerException);
        }
      }
    }

    private static CollectionContext<UserRolesItem> GetRolesForUsersInternal(
      string provider,
      string sortExpression,
      int skip,
      int take,
      string filter)
    {
      throw new NotImplementedException();
    }
  }
}
