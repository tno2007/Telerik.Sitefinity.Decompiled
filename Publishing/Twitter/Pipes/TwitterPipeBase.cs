// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Publishing.Pipes.TwitterPipeBase
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using LinqToTwitter;
using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Model.Publishing.Model.Twitter;
using Telerik.Sitefinity.Publishing.Model;
using Telerik.Sitefinity.Publishing.Twitter;
using Telerik.Sitefinity.Publishing.Twitter.Configuration;

namespace Telerik.Sitefinity.Publishing.Pipes
{
  /// <summary>Tweeter Pipe</summary>
  public class TwitterPipeBase : BasePipe<TwitterPipeSettings>
  {
    private IDefinitionField[] definitionFields;

    /// <summary>Gets the name.</summary>
    /// <value>The name.</value>
    public override string Name => "Twitter";

    /// <summary>
    /// Defines the data structure of the medium this pipe works with
    /// </summary>
    /// <value></value>
    public override IDefinitionField[] Definition
    {
      get
      {
        if (this.definitionFields == null)
          this.definitionFields = PublishingSystemFactory.GetPipeDefinitions(this.Name);
        return this.definitionFields;
      }
    }

    /// <summary>Gets or sets the publishing point.</summary>
    /// <value>The publishing point.</value>
    public virtual IPublishingPointBusinessObject PublishingPoint { get; set; }

    /// <summary>Gets the converted items for mapping.</summary>
    /// <param name="items">The items.</param>
    /// <returns></returns>
    public virtual IEnumerable<WrapperObject> GetConvertedItemsForMapping(
      params object[] items)
    {
      List<WrapperObject> convertedItemsForMapping = new List<WrapperObject>();
      foreach (object theInstance in items)
      {
        WrapperObject wrapperObject = new WrapperObject(this.PipeSettings, theInstance);
        convertedItemsForMapping.Add(wrapperObject);
      }
      return (IEnumerable<WrapperObject>) convertedItemsForMapping;
    }

    /// <summary>Initializes the specified pipe settings.</summary>
    /// <param name="pipeSettings">The pipe settings.</param>
    public override void Initialize(PipeSettings pipeSettings)
    {
      this.PipeSettingsInternal = (TwitterPipeSettings) pipeSettings;
      this.PublishingPoint = PublishingSystemFactory.GetPublishingPoint(this.PipeSettingsInternal.PublishingPoint);
    }

    /// <summary>
    /// Determines whether this instance [can process item] the specified item.
    /// </summary>
    /// <param name="item">The item.</param>
    /// <returns>
    /// 	<c>true</c> if this instance [can process item] the specified item; otherwise, <c>false</c>.
    /// </returns>
    public override bool CanProcessItem(object item) => true;

    /// <summary>Gets the pipe settings short description.</summary>
    /// <param name="initSettings">The init settings.</param>
    /// <returns></returns>
    public override string GetPipeSettingsShortDescription(PipeSettings initSettings)
    {
      string str = string.Format("http://twitter.com/{0}", (object) this.PipeSettingsInternal.UserNameReference);
      return string.Format("{0} <a href=\"{1}\" target=\"_blank\">{1}</a>", (object) Res.Get<PublishingMessages>().TwitterPipeSettingsShortDescription, (object) str);
    }

    /// <summary>Twitter Pipe Settings</summary>
    [Obsolete("Use the PipeSettingsInternal property instead.")]
    public TwitterPipeSettings TwitterPipeSettings => this.PipeSettingsInternal;

    /// <summary>Create Linq To Twitter Autorizer</summary>
    /// <returns></returns>
    protected SingleUserAuthorizer GetAutorizer()
    {
      SingleUserAuthorizer autorizer = new SingleUserAuthorizer();
      TwitterConfigCredentialsManager credentialsManager = new TwitterConfigCredentialsManager();
      autorizer.Credentials = (IOAuthCredentials) (credentialsManager.Associations.Join((IEnumerable<KeyValuePair<string, ITwitterUser>>) credentialsManager.Users, (Func<IAssociationItem, string>) (assoc => assoc.UserName), (Func<KeyValuePair<string, ITwitterUser>, string>) (user => user.Key), (assoc, user) => new
      {
        assoc = assoc,
        user = user
      }).Join((IEnumerable<KeyValuePair<string, ITwitterApplication>>) credentialsManager.Applications, _param1 => _param1.assoc.AppName, (Func<KeyValuePair<string, ITwitterApplication>, string>) (app => app.Key), (_param1, app) => new
      {
        \u003C\u003Eh__TransparentIdentifier0 = _param1,
        app = app
      }).Where(_param1 => _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AppName == this.PipeSettingsInternal.AppNameReference && _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.UserName == this.PipeSettingsInternal.UserNameReference).Select(_param1 => new InMemoryCredentials()
      {
        OAuthToken = _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AccessToken,
        AccessToken = _param1.\u003C\u003Eh__TransparentIdentifier0.assoc.AccessTokenSecret,
        ConsumerKey = _param1.app.Value.ConsumerKey,
        ConsumerSecret = _param1.app.Value.ConsumerSecret
      }).FirstOrDefault<InMemoryCredentials>() ?? throw new InvalidOperationException("Invalid Twitter Credentials"));
      autorizer.Authorize();
      return autorizer;
    }
  }
}
