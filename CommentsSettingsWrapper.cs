// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.CommentsSettingsWrapper
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Services;
using Telerik.Sitefinity.SiteSettings.Basic;

namespace Telerik.Sitefinity
{
  /// <summary>
  /// Comments settings wrapper class that will be used to resolve properties for comments on public site.
  /// </summary>
  [Obsolete("Settings for comments are in CommentsModuleConfig.")]
  public class CommentsSettingsWrapper : IGlobalCommentsSettings, ICommentsSettings
  {
    protected IGlobalCommentsSettings globalCommentsSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.CommentsSettingsWrapper" /> c lass.
    /// </summary>
    /// <param name="dataItem">The data item.</param>
    /// <param name="commentSettingsDefinition">The comment settings definition.</param>
    public CommentsSettingsWrapper(
      IDataItem dataItem,
      ICommentsSettingsDefinition commentSettingsDefinition)
    {
      this.DataItem = dataItem != null ? dataItem as Content : throw new ArgumentNullException(nameof (dataItem));
      this.CommentSettingsDefinition = commentSettingsDefinition;
    }

    /// <summary>Gets or sets the comment settings definition.</summary>
    /// <value>The comment settings definition.</value>
    protected ICommentsSettingsDefinition CommentSettingsDefinition { get; private set; }

    /// <summary>Gets the instance of the data item.</summary>
    public Content DataItem { get; private set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display name field.
    /// </summary>
    /// <value>When set to <c>true</c> name field is displayed; otherwise, <c>false</c>.</value>
    public bool DisplayNameField
    {
      get => this.GlobalCommentsSettings.DisplayNameField;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the name field is required.
    /// </summary>
    /// <value><c>true</c> if name field is required; otherwise, <c>false</c>.</value>
    public bool IsNameFieldRequired
    {
      get => this.GlobalCommentsSettings.IsNameFieldRequired;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display email field.
    /// </summary>
    /// <value>When set to <c>true</c> email field is displayed; otherwise, <c>false</c>.</value>
    public bool DisplayEmailField
    {
      get => this.GlobalCommentsSettings.DisplayEmailField;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the email field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the email field is required; otherwise, <c>false</c>.
    /// </value>
    public bool IsEmailFieldRequired
    {
      get => this.GlobalCommentsSettings.IsEmailFieldRequired;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display website field.
    /// </summary>
    /// <value>When set to <c>true</c> website field is displayed; otherwise, <c>false</c>.</value>
    public bool DisplayWebSiteField
    {
      get => this.GlobalCommentsSettings.DisplayWebSiteField;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the website field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if website field is required; otherwise, <c>false</c>.
    /// </value>
    public bool IsWebSiteFieldRequired
    {
      get => this.GlobalCommentsSettings.IsWebSiteFieldRequired;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display message field.
    /// </summary>
    /// <value>When set to <c>true</c> message field is displayed; otherwise, <c>false</c>.</value>
    public bool DisplayMessageField
    {
      get => this.GlobalCommentsSettings.DisplayMessageField;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the message field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the message field is required; otherwise, <c>false</c>.
    /// </value>
    public bool IsMessageFieldRequired
    {
      get => this.GlobalCommentsSettings.IsMessageFieldRequired;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    public bool UseSpamProtectionImage
    {
      get => Config.Get<CommentsModuleConfig>().UseSpamProtectionImage;
      set
      {
      }
    }

    /// <summary>Gets or sets who is allowed to post comments.</summary>
    public PostRights PostRights
    {
      get
      {
        PostRights postRights = this.CommentSettingsDefinition.PostRights;
        if (postRights == PostRights.None)
          postRights = this.DataItem.CommentsSettingsResolver.ResolveProperty<PostRights>(nameof (PostRights));
        return postRights;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to hide comments after specified number of days. The number of days is specified with value of property <see cref="P:Telerik.Sitefinity.CommentsSettingsWrapper.NumberOfDaysToHideComments" />.
    /// The property depends on property
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments are hidden after specified number of days; otherwise, <c>false</c>.
    /// </value>
    public bool HideCommentsAfterNumberOfDays
    {
      get => this.GlobalCommentsSettings.HideCommentsAfterNumberOfDays;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if comments support trackback.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    public bool? TrackBack
    {
      get
      {
        bool? trackBack = this.CommentSettingsDefinition.TrackBack;
        if (!trackBack.HasValue)
          trackBack = this.DataItem.CommentsSettingsResolver.ResolveProperty<bool?>(nameof (TrackBack));
        return trackBack;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if content item supports comments.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if content item supports comments; otherwise, <c>false</c>.
    /// </value>
    public bool? AllowComments
    {
      get => this.DataItem.AllowComments;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if author of the post will be notified via email when a new comment is submitted.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if author of the post will be notified via email when a new comment is submitted; otherwise, <c>false</c>.
    /// </value>
    public bool? EmailAuthor
    {
      get
      {
        bool? emailAuthor = this.CommentSettingsDefinition.EmailAuthor;
        if (!emailAuthor.HasValue)
          emailAuthor = this.DataItem.CommentsSettingsResolver.ResolveProperty<bool?>(nameof (EmailAuthor));
        return emailAuthor;
      }
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets a value indicating if comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments have to be approved; otherwise, <c>false</c>.
    /// </value>
    public bool? ApproveComments
    {
      get => this.DataItem.ApproveComments;
      set
      {
      }
    }

    /// <summary>
    /// Gets or sets the number of days after which to hide comment item.
    /// </summary>
    /// <value>The number of days to hide comments.</value>
    public int NumberOfDaysToHideComments
    {
      get => this.GlobalCommentsSettings.NumberOfDaysToHideComments;
      set
      {
      }
    }

    /// <summary>Gets the global comments settings.</summary>
    /// <value>The global comments settings.</value>
    protected IGlobalCommentsSettings GlobalCommentsSettings
    {
      get
      {
        if (this.globalCommentsSettings == null)
          this.globalCommentsSettings = SystemManager.CurrentContext.GetSetting<CommentsSettingsContract, IGlobalCommentsSettings>();
        return this.globalCommentsSettings;
      }
    }
  }
}
