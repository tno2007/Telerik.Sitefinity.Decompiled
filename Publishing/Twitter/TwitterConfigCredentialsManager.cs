// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterConfigCredentialsManager
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Twitterizer;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  public class TwitterConfigCredentialsManager : TwitterCredentialsManagerBase
  {
    private TwitterConfig config;

    public TwitterConfigCredentialsManager() => this.config = Config.Get<TwitterConfig>();

    public override IDictionary<string, ITwitterApplication> Applications => (IDictionary<string, ITwitterApplication>) this.config.TwitterApplications.ToDictionary<KeyValuePair<string, TwitterApplication>, string, ITwitterApplication>((Func<KeyValuePair<string, TwitterApplication>, string>) (p => p.Key), (Func<KeyValuePair<string, TwitterApplication>, ITwitterApplication>) (p => (ITwitterApplication) p.Value));

    public override IDictionary<string, ITwitterUser> Users => (IDictionary<string, ITwitterUser>) this.config.TwitterUsers.ToDictionary<KeyValuePair<string, Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser>, string, ITwitterUser>((Func<KeyValuePair<string, Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser>, string>) (p => p.Key), (Func<KeyValuePair<string, Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser>, ITwitterUser>) (p => (ITwitterUser) p.Value));

    public override IList<IAssociationItem> Associations => (IList<IAssociationItem>) this.config.Associations.Cast<IAssociationItem>().ToList<IAssociationItem>();

    public override IDictionary<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>> GetAllIndexedByApp() => (IDictionary<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>) this.Applications.GroupJoin<KeyValuePair<string, ITwitterApplication>, KeyValuePair<ITwitterUser, IAssociationItem>, string, KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>>(this.Users.Join<KeyValuePair<string, ITwitterUser>, IAssociationItem, string, KeyValuePair<ITwitterUser, IAssociationItem>>((IEnumerable<IAssociationItem>) this.Associations, (Func<KeyValuePair<string, ITwitterUser>, string>) (user => user.Key), (Func<IAssociationItem, string>) (assoc => assoc.UserName), (Func<KeyValuePair<string, ITwitterUser>, IAssociationItem, KeyValuePair<ITwitterUser, IAssociationItem>>) ((user, assoc) => new KeyValuePair<ITwitterUser, IAssociationItem>(user.Value, assoc))), (Func<KeyValuePair<string, ITwitterApplication>, string>) (app => app.Key), (Func<KeyValuePair<ITwitterUser, IAssociationItem>, string>) (assocItem => assocItem.Value.AppName), (Func<KeyValuePair<string, ITwitterApplication>, IEnumerable<KeyValuePair<ITwitterUser, IAssociationItem>>, KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>>) ((app, groupResult) => new KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>(app.Value, (IDictionary<ITwitterUser, IAssociationItem>) groupResult.ToDictionary<KeyValuePair<ITwitterUser, IAssociationItem>, ITwitterUser, IAssociationItem>((Func<KeyValuePair<ITwitterUser, IAssociationItem>, ITwitterUser>) (p => p.Key), (Func<KeyValuePair<ITwitterUser, IAssociationItem>, IAssociationItem>) (p => p.Value))))).ToDictionary<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>((Func<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, ITwitterApplication>) (p => p.Key), (Func<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, IDictionary<ITwitterUser, IAssociationItem>>) (p => p.Value));

    public override IDictionary<ITwitterApplication, IAssociationItem> GetAssociatedTwitterAppAndDataForUser(
      string userName)
    {
      return (IDictionary<ITwitterApplication, IAssociationItem>) this.Associations.Where<IAssociationItem>((Func<IAssociationItem, bool>) (assoc => assoc.UserName == userName)).Join<IAssociationItem, KeyValuePair<string, ITwitterApplication>, string, KeyValuePair<ITwitterApplication, IAssociationItem>>((IEnumerable<KeyValuePair<string, ITwitterApplication>>) this.Applications, (Func<IAssociationItem, string>) (assoc => assoc.UserName), (Func<KeyValuePair<string, ITwitterApplication>, string>) (pair => pair.Key), (Func<IAssociationItem, KeyValuePair<string, ITwitterApplication>, KeyValuePair<ITwitterApplication, IAssociationItem>>) ((assoc, pair) => new KeyValuePair<ITwitterApplication, IAssociationItem>(pair.Value, assoc))).ToDictionary<KeyValuePair<ITwitterApplication, IAssociationItem>, ITwitterApplication, IAssociationItem>((Func<KeyValuePair<ITwitterApplication, IAssociationItem>, ITwitterApplication>) (k => k.Key), (Func<KeyValuePair<ITwitterApplication, IAssociationItem>, IAssociationItem>) (p => p.Value));
    }

    public override IDictionary<ITwitterUser, IAssociationItem> GetAssociatedTwitterUsersAndDataForApp(
      string appName)
    {
      return (IDictionary<ITwitterUser, IAssociationItem>) this.Associations.Join((IEnumerable<KeyValuePair<string, ITwitterUser>>) this.Users, (Func<IAssociationItem, string>) (assoc => assoc.UserName), (Func<KeyValuePair<string, ITwitterUser>, string>) (user => user.Key), (assoc, user) => new
      {
        assoc = assoc,
        user = user
      }).Where(_param1 => _param1.assoc.AppName == appName).Select(_param1 => new KeyValuePair<ITwitterUser, IAssociationItem>(_param1.user.Value, _param1.assoc)).ToDictionary<KeyValuePair<ITwitterUser, IAssociationItem>, ITwitterUser, IAssociationItem>((Func<KeyValuePair<ITwitterUser, IAssociationItem>, ITwitterUser>) (k => k.Key), (Func<KeyValuePair<ITwitterUser, IAssociationItem>, IAssociationItem>) (p => p.Value));
    }

    public override ITwitterApplication CreateApplication(string applicationName)
    {
      TwitterApplication element = new TwitterApplication((ConfigElement) this.config.TwitterApplications)
      {
        Name = applicationName
      };
      this.config.TwitterApplications.Add(element);
      return (ITwitterApplication) element;
    }

    public override ITwitterUser CreateUser(string userName)
    {
      Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser element = new Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser((ConfigElement) this.config.TwitterUsers)
      {
        Name = userName
      };
      this.config.TwitterUsers.Add(element);
      return (ITwitterUser) element;
    }

    public override IAssociationItem CreateAssociation(
      string applicationName,
      string userName,
      string token,
      string tokenSecret)
    {
      AssociationItem element = new AssociationItem((ConfigElement) this.config.Associations)
      {
        AppName = applicationName,
        UserName = userName,
        AccessToken = token,
        AccessTokenSecret = tokenSecret
      };
      this.config.Associations.Add(element);
      return (IAssociationItem) element;
    }

    public void DeleteItem(object item)
    {
      switch (item)
      {
        case IAssociationItem _:
          IAssociationItem assoc = (IAssociationItem) item;
          this.config.Associations.Remove(this.config.Associations.Where<AssociationItem>((Func<AssociationItem, bool>) (p => p.AccessToken == assoc.AccessToken && p.AccessTokenSecret == assoc.AccessTokenSecret && p.AppName == assoc.AppName)).FirstOrDefault<AssociationItem>());
          break;
        case ITwitterUser _:
          this.config.TwitterUsers.Remove((Telerik.Sitefinity.Publishing.Twitter.Configuration.TwitterUser) this.config.TwitterUsers.GetElementByKey(((ITwitterUser) item).Name));
          break;
        case ITwitterApplication _:
          this.config.TwitterApplications.Remove((TwitterApplication) this.config.TwitterApplications.GetElementByKey(((ITwitterApplication) item).Name));
          break;
      }
    }

    public void SaveChanges() => Config.GetManager().SaveSection((ConfigSection) this.config);

    public override OAuthTokens GetTokensForReference(string appName, string userName) => this.Associations.Join((IEnumerable<KeyValuePair<string, ITwitterUser>>) this.Users, (Func<IAssociationItem, string>) (assoc => assoc.UserName), (Func<KeyValuePair<string, ITwitterUser>, string>) (user => user.Key), (assoc, user) => new
    {
      assoc = assoc,
      user = user
    }).Join((IEnumerable<KeyValuePair<string, ITwitterApplication>>) this.Applications, _param1 => _param1.assoc.AppName, (Func<KeyValuePair<string, ITwitterApplication>, string>) (app => app.Key), (_param1, app) => new
    {
      \u003C\u003Eh__TransparentIdentifier0 = _param1,
      app = app
    }).Where(_param1 => _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AppName == appName && _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.UserName == userName).Select(_param1 => new OAuthTokens()
    {
      AccessToken = _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AccessToken,
      AccessTokenSecret = _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AccessTokenSecret,
      ConsumerKey = _param1.app.Value.ConsumerKey,
      ConsumerSecret = _param1.app.Value.ConsumerSecret
    }).First<OAuthTokens>();
  }
}
