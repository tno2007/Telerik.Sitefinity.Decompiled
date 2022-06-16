// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.Services.TwitterCredentialsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Publishing.Web.Services.Data;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Publishing.Twitter.Services
{
  [ServiceContract]
  [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true, InstanceContextMode = InstanceContextMode.Single)]
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
  public class TwitterCredentialsService
  {
    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?")]
    public CollectionContext<KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>> GetAllIndexedByApp()
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IEnumerable<KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>> keyValuePairs = new TwitterConfigCredentialsManager().GetAllIndexedByApp().Select<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>>((Func<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>>) (pair =>
      {
        KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>> keyValuePair = pair;
        WcfTwitterApplication key = new WcfTwitterApplication(keyValuePair.Key);
        keyValuePair = pair;
        Dictionary<WcfTwitterUser, WcfAssociationItem> dictionary = keyValuePair.Value.ToDictionary<KeyValuePair<ITwitterUser, IAssociationItem>, WcfTwitterUser, WcfAssociationItem>((Func<KeyValuePair<ITwitterUser, IAssociationItem>, WcfTwitterUser>) (p => new WcfTwitterUser(p.Key)), (Func<KeyValuePair<ITwitterUser, IAssociationItem>, WcfAssociationItem>) (p => new WcfAssociationItem(p.Value)));
        return new KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>(key, (IDictionary<WcfTwitterUser, WcfAssociationItem>) dictionary);
      }));
      ServiceUtility.DisableCache();
      return new CollectionContext<KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>>(keyValuePairs)
      {
        TotalCount = keyValuePairs.Count<KeyValuePair<WcfTwitterApplication, IDictionary<WcfTwitterUser, WcfAssociationItem>>>()
      };
    }

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "?appName={appName}")]
    public DataItemContext<WcfTwitterApplication> GetApp(
      string appName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      if (credentialsManager.Applications.ContainsKey(appName))
      {
        ITwitterApplication application = credentialsManager.Applications[appName];
        return new DataItemContext<WcfTwitterApplication>()
        {
          Item = new WcfTwitterApplication(application),
          ItemType = application.GetType().ToString()
        };
      }
      ServiceUtility.DisableCache();
      return (DataItemContext<WcfTwitterApplication>) null;
    }

    [OperationContract]
    [WebInvoke(Method = "PUT", ResponseFormat = WebMessageFormat.Json, UriTemplate = "?")]
    public DataItemContext<WcfTwitterApplication> SaveApp(
      DataItemContext<WcfTwitterApplication> applicationContext)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      if (!TwitterAuthorizationUtility.TestApplication((ITwitterApplication) applicationContext.Item))
        throw new Exception("The supplied credentials are wrong. Please verify your input.");
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      string originalName = applicationContext.Item.OriginalAppName;
      ITwitterApplication application;
      if (!originalName.IsNullOrEmpty())
      {
        if (applicationContext.Item.Name != originalName && credentialsManager.Applications.ContainsKey(originalName))
        {
          credentialsManager.DeleteItem((object) credentialsManager.Applications[originalName]);
          foreach (IAssociationItem associationItem in credentialsManager.Associations.Where<IAssociationItem>((Func<IAssociationItem, bool>) (p => p.AppName == originalName)).ToList<IAssociationItem>())
            associationItem.AppName = applicationContext.Item.Name;
          application = credentialsManager.CreateApplication(applicationContext.Item.Name);
        }
        else
          application = credentialsManager.Applications[applicationContext.Item.Name];
      }
      else
        application = credentialsManager.CreateApplication(applicationContext.Item.Name);
      applicationContext.Item.CopyTo(ref application);
      using (new FileSystemModeRegion())
        credentialsManager.SaveChanges();
      applicationContext.Item = new WcfTwitterApplication(application);
      ServiceUtility.DisableCache();
      return applicationContext;
    }

    [OperationContract]
    [WebInvoke(Method = "DELETE", ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteApp/?appName={appName}")]
    public bool Delete(string appName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      foreach (KeyValuePair<ITwitterUser, IAssociationItem> keyValuePair in (IEnumerable<KeyValuePair<ITwitterUser, IAssociationItem>>) credentialsManager.GetAssociatedTwitterUsersAndDataForApp(appName))
      {
        KeyValuePair<ITwitterUser, IAssociationItem> x = keyValuePair;
        credentialsManager.DeleteItem((object) x.Value);
        if (credentialsManager.Associations.Where<IAssociationItem>((Func<IAssociationItem, bool>) (p => p.UserName == x.Key.Name)).Count<IAssociationItem>() == 0)
          credentialsManager.DeleteItem((object) x.Key);
      }
      credentialsManager.DeleteItem((object) credentialsManager.Applications[appName]);
      using (new FileSystemModeRegion())
        credentialsManager.SaveChanges();
      ServiceUtility.DisableCache();
      return true;
    }

    [OperationContract]
    [WebGet(ResponseFormat = WebMessageFormat.Json, UriTemplate = "AssociateUser/?appName={appName}")]
    public string AssociateUser(string appName)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      ServiceUtility.DisableCache();
      return SystemManager.CurrentHttpContext.Server.UrlEncode(TwitterAuthorizationUtility.GetAuthorizationUriForApp(credentialsManager.Applications[appName], TwitterAuthorizationUtility.TwitterCallBackUrl));
    }
  }
}
