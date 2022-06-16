// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterCredentialsManagerBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Twitterizer;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  public abstract class TwitterCredentialsManagerBase
  {
    public abstract IDictionary<string, ITwitterApplication> Applications { get; }

    public abstract IDictionary<string, ITwitterUser> Users { get; }

    public abstract IList<IAssociationItem> Associations { get; }

    public abstract IDictionary<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>> GetAllIndexedByApp();

    public abstract IDictionary<ITwitterApplication, IAssociationItem> GetAssociatedTwitterAppAndDataForUser(
      string userName);

    public abstract IDictionary<ITwitterUser, IAssociationItem> GetAssociatedTwitterUsersAndDataForApp(
      string appName);

    public abstract ITwitterApplication CreateApplication(string applicationName);

    public abstract ITwitterUser CreateUser(string userName);

    public abstract IAssociationItem CreateAssociation(
      string applicationName,
      string userName,
      string token,
      string tokenSecret);

    public abstract OAuthTokens GetTokensForReference(string appName, string userName);
  }
}
