// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Twitter.BackendControls.TwitterLandingPageControl
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.UI;
using Telerik.Sitefinity.Data.Metadata;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Metadata.Model;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;
using Telerik.Sitefinity.Web.UI;
using Twitterizer;

namespace Telerik.Sitefinity.Publishing.Twitter.BackendControls
{
  public class TwitterLandingPageControl : Control
  {
    private bool? success;
    private OAuthTokenResponse response;

    protected override void OnInit(EventArgs e)
    {
      base.OnInit(e);
      try
      {
        this.PerformAuth();
      }
      catch
      {
      }
    }

    protected override void OnPreRender(EventArgs e)
    {
      if (this.Message != null && this.success.HasValue)
      {
        bool? success = this.success;
        bool flag = true;
        if (success.GetValueOrDefault() == flag & success.HasValue)
          this.Message.ShowPositiveMessage(string.Format(Res.Get<PublishingMessages>().TwitterAssociationSuccess, (object) this.response.ScreenName));
        else
          this.Message.ShowNegativeMessage(Res.Get<PublishingMessages>().TwitterAssociationFail);
      }
      base.OnPreRender(e);
    }

    public Message Message { get; set; }

    private void CreatePP(string appName)
    {
      IEnumerable<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>> source1 = new TwitterConfigCredentialsManager().GetAllIndexedByApp().Where<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>>((Func<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, bool>) (app => app.Key.Name == appName));
      PublishingManager manager = PublishingManager.GetManager();
      if (manager.GetPublishingPoints().Where<PublishingPoint>((Expression<Func<PublishingPoint, bool>>) (item => item.Name == appName)).FirstOrDefault<PublishingPoint>() != null)
        return;
      PublishingPoint publishingPoint = manager.CreatePublishingPoint();
      MetaType publishingPointDataType = PublishingSystemFactory.CreatePublishingPointDataType();
      publishingPoint.Name = appName;
      publishingPoint.IsActive = true;
      publishingPoint.StorageTypeName = publishingPointDataType.FullTypeName;
      publishingPoint.PublishingPointBusinessObjectName = "Persistent";
      foreach (IDictionary<ITwitterUser, IAssociationItem> dictionary in source1.Select<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, IDictionary<ITwitterUser, IAssociationItem>>((Func<KeyValuePair<ITwitterApplication, IDictionary<ITwitterUser, IAssociationItem>>, IDictionary<ITwitterUser, IAssociationItem>>) (asoc => asoc.Value)))
      {
        ITwitterUser twitterUser = dictionary.Keys.First<ITwitterUser>();
        PipeSettings pipeSettings1 = PublishingSystemFactory.GetPipeSettings("TwitterInboundPipe");
        TwitterPipeSettings pipeSettings2 = manager.CreatePipeSettings(typeof (TwitterPipeSettings)) as TwitterPipeSettings;
        string applicationName1 = pipeSettings2.ApplicationName;
        pipeSettings2.CopyFrom(pipeSettings1);
        pipeSettings2.ApplicationName = applicationName1;
        pipeSettings2.ScheduleType = 1;
        IList<Mapping> pipeMappings = PublishingSystemFactory.GetPipeMappings("TwitterInboundPipe", true);
        pipeSettings2.Mappings.Mappings.Clear();
        foreach (Mapping source2 in (IEnumerable<Mapping>) pipeMappings)
        {
          Mapping mapping = manager.CreateMapping();
          string applicationName2 = mapping.ApplicationName;
          mapping.CopyFrom(source2);
          mapping.ApplicationName = applicationName2;
          pipeSettings2.Mappings.Mappings.Add(mapping);
        }
        pipeSettings2.SearchPattern = "from:" + twitterUser.Name;
        pipeSettings2.MaxItems = 20;
        pipeSettings2.AppNameReference = appName;
        pipeSettings2.UserNameReference = twitterUser.Name;
        publishingPoint.PipeSettings.Add((PipeSettings) pipeSettings2);
      }
      manager.SaveChanges();
      MetadataManager.GetManager().SaveChanges();
      PublishingManager.ReschedulePublishingPointPipes(publishingPoint, "");
    }

    private void PerformAuth()
    {
      if (this.Page.Request.QueryString["oauth_token"] == null || this.Page.Request.QueryString["oauth_verifier"] == null)
      {
        this.success = new bool?();
      }
      else
      {
        this.success = new bool?(false);
        TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
        string appName = TwitterAuthorizationUtility.RecallAppForToken(this.Page.Request.QueryString["oauth_token"]);
        if (appName.IsNullOrEmpty())
          return;
        OAuthTokenResponse accessToken = TwitterAuthorizationUtility.GetAccessToken(credentialsManager.Applications[appName], this.Page.Request.QueryString["oauth_token"], this.Page.Request.QueryString["oauth_verifier"]);
        this.response = accessToken;
        if (credentialsManager.Users.Keys.Cast<string>().Where<string>((Func<string, bool>) (k => k == accessToken.ScreenName)).Count<string>() == 0)
          credentialsManager.CreateUser(accessToken.ScreenName);
        foreach (IAssociationItem associationItem in credentialsManager.Associations.Where<IAssociationItem>((Func<IAssociationItem, bool>) (p => p.AppName == appName && p.UserName == accessToken.ScreenName)).ToList<IAssociationItem>())
          credentialsManager.DeleteItem((object) associationItem);
        credentialsManager.CreateAssociation(appName, accessToken.ScreenName, accessToken.Token, accessToken.TokenSecret);
        credentialsManager.SaveChanges();
        this.CreatePP(appName);
        this.success = new bool?(true);
      }
    }
  }
}
