// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Configuration.Basic.CommentsSettingsModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;

namespace Telerik.Sitefinity.Configuration.Basic
{
  /// <summary>Represents the model for basic comments settings.</summary>
  [DataContract]
  [Obsolete("Use CommentsSettingsContract instead.")]
  public class CommentsSettingsModel
  {
    private CommentsConfig commentsConfigSection;

    /// <summary>Gets or sets who is allowed to post comments.</summary>
    [DataMember]
    public PostRights PostRights
    {
      get => this.CommentsConfigSection.CommentsSettings.PostRights;
      set => this.CommentsConfigSection.CommentsSettings.PostRights = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments have to be approved; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool? ApproveComments
    {
      get => this.CommentsConfigSection.CommentsSettings.ApproveComments;
      set => this.CommentsConfigSection.CommentsSettings.ApproveComments = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool UseSpamProtectionImage
    {
      get => this.CommentsConfigSection.CommentsSettings.UseSpamProtectionImage;
      set => this.CommentsConfigSection.CommentsSettings.UseSpamProtectionImage = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to hide comments after specified number of days. The number of days is specified with value of property <see cref="P:Telerik.Sitefinity.Configuration.Basic.CommentsSettingsModel.NumberOfDaysToHideComments" />.
    /// The property depends on property
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments are hidden after specified number of days; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool HideCommentsAfterNumberOfDays
    {
      get => this.CommentsConfigSection.CommentsSettings.HideCommentsAfterNumberOfDays;
      set => this.CommentsConfigSection.CommentsSettings.HideCommentsAfterNumberOfDays = value;
    }

    /// <summary>
    /// Gets or sets the number of days after which to hide comments.
    /// </summary>
    /// <value>The number of days to hide comments.</value>
    [DataMember]
    public int NumberOfDaysToHideComments
    {
      get => this.CommentsConfigSection.CommentsSettings.NumberOfDaysToHideComments;
      set => this.CommentsConfigSection.CommentsSettings.NumberOfDaysToHideComments = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display name field.
    /// </summary>
    /// <value>When set to <c>true</c> name field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayNameField
    {
      get => this.CommentsConfigSection.CommentsSettings.DisplayNameField;
      set => this.CommentsConfigSection.CommentsSettings.DisplayNameField = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the name field is required.
    /// </summary>
    /// <value><c>true</c> if name field is required; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsNameFieldRequired
    {
      get => this.CommentsConfigSection.CommentsSettings.IsNameFieldRequired;
      set => this.CommentsConfigSection.CommentsSettings.IsNameFieldRequired = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display email field.
    /// </summary>
    /// <value>When set to <c>true</c> email field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayEmailField
    {
      get => this.CommentsConfigSection.CommentsSettings.DisplayEmailField;
      set => this.CommentsConfigSection.CommentsSettings.DisplayEmailField = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the email field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the email field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsEmailFieldRequired
    {
      get => this.CommentsConfigSection.CommentsSettings.IsEmailFieldRequired;
      set => this.CommentsConfigSection.CommentsSettings.IsEmailFieldRequired = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display website field.
    /// </summary>
    /// <value>When set to <c>true</c> website field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayWebSiteField
    {
      get => this.CommentsConfigSection.CommentsSettings.DisplayWebSiteField;
      set => this.CommentsConfigSection.CommentsSettings.DisplayWebSiteField = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the website field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if website field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsWebSiteFieldRequired
    {
      get => this.CommentsConfigSection.CommentsSettings.IsWebSiteFieldRequired;
      set => this.CommentsConfigSection.CommentsSettings.IsWebSiteFieldRequired = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to display message field.
    /// </summary>
    /// <value>When set to <c>true</c> message field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayMessageField
    {
      get => this.CommentsConfigSection.CommentsSettings.DisplayMessageField;
      set => this.CommentsConfigSection.CommentsSettings.DisplayMessageField = value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether the message field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the message field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsMessageFieldRequired
    {
      get => this.CommentsConfigSection.CommentsSettings.IsMessageFieldRequired;
      set => this.CommentsConfigSection.CommentsSettings.IsMessageFieldRequired = value;
    }

    /// <summary>Gets the comments config section.</summary>
    /// <value>The comments config section.</value>
    public CommentsConfig CommentsConfigSection
    {
      get
      {
        if (this.commentsConfigSection == null)
          this.commentsConfigSection = ConfigManager.GetManager().GetSection<CommentsConfig>();
        return this.commentsConfigSection;
      }
    }
  }
}
