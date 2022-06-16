// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsNotificationSubscription
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Notifications;

namespace Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend
{
  /// <summary>
  /// Represents the notification subscription links avaialble in the CommentsWidget
  /// </summary>
  public class CommentsNotificationSubscription : NotificationSubscriptionControl
  {
    private CommentsSettingsElement threadConfig;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend.CommentsNotificationSubscription" /> class.
    /// </summary>
    public CommentsNotificationSubscription()
    {
      this.SubscribeAnchorText = Res.Get("CommentsResources", "SubscribeToComments");
      this.UnsubscribeLiteralText = Res.Get("CommentsResources", "CurrentlySubscribed");
      this.UnsubscribeAnchorText = Res.Get("Labels", "Unsubscribe");
      this.SuccessfullySubscribedLiteralText = Res.Get("CommentsResources", "SuccessfullySubscribed");
      this.SuccessfullySubscribedAnchorText = Res.Get("Labels", "Unsubscribe");
      this.SuccessfullyUnsubscribedLiteralText = Res.Get("CommentsResources", "SuccessfullyUnsubscribed");
      this.SuccessfullyUnsubscribedAnchorText = Res.Get("Labels", "Subscribe");
    }

    /// <summary>
    /// Gets or sets the thread key that will be used for association of the comment.
    /// </summary>
    public string ThreadType { set; get; }

    /// <summary>Gets the Comments Settings element</summary>
    private CommentsSettingsElement ThreadConfig
    {
      get
      {
        if (this.threadConfig == null)
          this.threadConfig = CommentsUtilities.GetThreadConfigByType(this.ThreadType);
        return this.threadConfig;
      }
    }

    /// <summary>
    /// The script control type name passed to the <see cref="!:ScriptControlDescriptor" />. It defaults to the full name
    /// of the current object class. E.g. can be overriden to reuse the script of some of the base classes and just customize
    /// some controls server-side.
    /// </summary>
    protected override string ScriptDescriptorTypeName => typeof (NotificationSubscriptionControl).FullName;

    /// <summary>The subscribe URL</summary>
    public override string SubscribeUrl
    {
      get
      {
        string str = this.Page.ResolveUrl("~/RestApi/comments-api");
        return !str.IsNullOrEmpty() ? str + "/notifications/subscribe/" : base.SubscribeUrl;
      }
      set => base.SubscribeUrl = value;
    }

    /// <summary>The unsubscribe URL</summary>
    public override string UnsubscribeUrl
    {
      get
      {
        string str = this.Page.ResolveUrl("~/RestApi/comments-api");
        return !str.IsNullOrEmpty() ? str + "/notifications/unsubscribe/" : base.UnsubscribeUrl;
      }
      set => base.UnsubscribeUrl = value;
    }

    /// <summary>The check subscription status url</summary>
    public override string CheckSubscriptionStatusUrl
    {
      get
      {
        string str = this.Page.ResolveUrl("~/RestApi/comments-api");
        return !str.IsNullOrEmpty() ? str + "/notifications" : base.CheckSubscriptionStatusUrl;
      }
      set => base.CheckSubscriptionStatusUrl = value;
    }

    protected override void InitializeControls(GenericContainer container)
    {
      if (this.ThreadConfig.EnableRatings)
      {
        this.SubscribeAnchorText = Res.Get("CommentsResources", "SubscribeToReviews");
        this.UnsubscribeLiteralText = Res.Get("CommentsResources", "CurrentlySubscribedToReviews");
        this.SuccessfullySubscribedLiteralText = Res.Get("CommentsResources", "SuccessfullySubscribedToReviews");
        this.SuccessfullyUnsubscribedLiteralText = Res.Get("CommentsResources", "SuccessfullyUnsubscribedFromReviews");
      }
      base.InitializeControls(container);
    }
  }
}
