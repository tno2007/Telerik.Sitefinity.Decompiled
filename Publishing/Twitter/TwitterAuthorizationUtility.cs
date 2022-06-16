// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.TwitterAuthorizationUtility
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Web;
using Twitterizer;

namespace Telerik.Sitefinity.Publishing.Twitter
{
  public static class TwitterAuthorizationUtility
  {
    private static Dictionary<string, string> pendingRequestTokens = new Dictionary<string, string>();

    public static string GetAuthorizationUriForApp(ITwitterApplication app, string callbackUrl)
    {
      string token = OAuthUtility.GetRequestToken(app.ConsumerKey, app.ConsumerSecret, callbackUrl).Token;
      TwitterAuthorizationUtility.RememberRequest(app, token);
      return OAuthUtility.BuildAuthorizationUri(token, false).AbsoluteUri;
    }

    public static string RecallAppForToken(string token) => !TwitterAuthorizationUtility.PendingRequestTokens.ContainsKey(token) ? (string) null : TwitterAuthorizationUtility.PendingRequestTokens[token];

    public static bool TestApplication(ITwitterApplication app)
    {
      try
      {
        OAuthUtility.GetRequestToken(app.ConsumerKey, app.ConsumerSecret, TwitterAuthorizationUtility.TwitterCallBackUrl);
      }
      catch
      {
        return false;
      }
      return true;
    }

    public static OAuthTokenResponse GetAccessToken(
      ITwitterApplication app,
      string requestToken,
      string verifier)
    {
      return OAuthUtility.GetAccessToken(app.ConsumerKey, app.ConsumerSecret, requestToken, verifier);
    }

    public static string TwitterCallBackUrl => RouteHelper.ResolveUrl("~/", UrlResolveOptions.Absolute) + "Sitefinity/Administration/Settings/Basic/Twitter/";

    private static void RememberRequest(ITwitterApplication app, string token)
    {
      Dictionary<string, string> pendingRequestTokens = TwitterAuthorizationUtility.PendingRequestTokens;
      pendingRequestTokens.Add(token, app.Name);
      TwitterAuthorizationUtility.PendingRequestTokens = pendingRequestTokens;
    }

    private static Dictionary<string, string> PendingRequestTokens
    {
      get
      {
        lock (TwitterAuthorizationUtility.pendingRequestTokens)
          return TwitterAuthorizationUtility.pendingRequestTokens;
      }
      set
      {
        lock (TwitterAuthorizationUtility.pendingRequestTokens)
          TwitterAuthorizationUtility.pendingRequestTokens = value;
      }
    }
  }
}
