// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.SiteSettings.Basic.CommentsSettingsContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent.Configuration;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;

namespace Telerik.Sitefinity.SiteSettings.Basic
{
  [Obsolete("CommentsRestService does not return data of this type.")]
  [DataContract]
  public class CommentsSettingsContract : 
    ISettingsDataContract,
    IGlobalCommentsSettings,
    ICommentsSettings
  {
    protected IGlobalCommentsSettings globalCommentsSettings;

    /// <summary>Gets or sets who is allowed to post comments.</summary>
    [DataMember]
    public PostRights PostRights { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments have to be approved; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool? ApproveComments { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to use spam protection image.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> spam protection image is used; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool UseSpamProtectionImage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to hide comments after specified number of days. The number of days is specified with value of property <see cref="P:Telerik.Sitefinity.SiteSettings.Basic.CommentsSettingsContract.NumberOfDaysToHideComments" />.
    /// The property depends on property
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments are hidden after specified number of days; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool HideCommentsAfterNumberOfDays { get; set; }

    /// <summary>
    /// Gets or sets the number of days after which to hide comments.
    /// </summary>
    /// <value>The number of days to hide comments.</value>
    [DataMember]
    public int NumberOfDaysToHideComments { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display name field.
    /// </summary>
    /// <value>When set to <c>true</c> name field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayNameField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the name field is required.
    /// </summary>
    /// <value><c>true</c> if name field is required; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool IsNameFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display email field.
    /// </summary>
    /// <value>When set to <c>true</c> email field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayEmailField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the email field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the email field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsEmailFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display website field.
    /// </summary>
    /// <value>When set to <c>true</c> website field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayWebSiteField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the website field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if website field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsWebSiteFieldRequired { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to display message field.
    /// </summary>
    /// <value>When set to <c>true</c> message field is displayed; otherwise, <c>false</c>.</value>
    [DataMember]
    public bool DisplayMessageField { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the message field is required.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if the message field is required; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public bool IsMessageFieldRequired { get; set; }

    public void LoadDefaults(bool forEdit = false)
    {
      Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings commentsSettings = !forEdit ? Config.Get<CommentsConfig>().CommentsSettings : ConfigManager.GetManager().GetSection<CommentsConfig>().CommentsSettings;
      this.PostRights = commentsSettings.PostRights;
      this.ApproveComments = commentsSettings.ApproveComments;
      this.UseSpamProtectionImage = commentsSettings.UseSpamProtectionImage;
      this.HideCommentsAfterNumberOfDays = commentsSettings.HideCommentsAfterNumberOfDays;
      this.NumberOfDaysToHideComments = commentsSettings.NumberOfDaysToHideComments;
      this.IsNameFieldRequired = commentsSettings.IsNameFieldRequired;
      this.IsEmailFieldRequired = commentsSettings.IsEmailFieldRequired;
      this.IsWebSiteFieldRequired = commentsSettings.IsWebSiteFieldRequired;
      this.IsMessageFieldRequired = commentsSettings.IsMessageFieldRequired;
      this.DisplayEmailField = commentsSettings.DisplayEmailField;
      this.DisplayNameField = commentsSettings.DisplayNameField;
      this.DisplayMessageField = commentsSettings.DisplayMessageField;
      this.DisplayWebSiteField = commentsSettings.DisplayWebSiteField;
      this.IsMessageFieldRequired = commentsSettings.IsMessageFieldRequired;
    }

    public void SaveDefaults()
    {
      ConfigManager manager = ConfigManager.GetManager();
      CommentsConfig section = manager.GetSection<CommentsConfig>();
      Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings commentsSettings = section.CommentsSettings;
      commentsSettings.PostRights = this.PostRights;
      commentsSettings.ApproveComments = this.ApproveComments;
      commentsSettings.UseSpamProtectionImage = this.UseSpamProtectionImage;
      commentsSettings.HideCommentsAfterNumberOfDays = this.HideCommentsAfterNumberOfDays;
      commentsSettings.NumberOfDaysToHideComments = this.NumberOfDaysToHideComments;
      commentsSettings.IsNameFieldRequired = this.IsNameFieldRequired;
      commentsSettings.IsEmailFieldRequired = this.IsEmailFieldRequired;
      commentsSettings.IsWebSiteFieldRequired = this.IsWebSiteFieldRequired;
      commentsSettings.IsMessageFieldRequired = this.IsMessageFieldRequired;
      commentsSettings.DisplayNameField = this.DisplayNameField;
      commentsSettings.DisplayEmailField = this.DisplayEmailField;
      commentsSettings.DisplayMessageField = this.DisplayMessageField;
      commentsSettings.DisplayWebSiteField = this.DisplayWebSiteField;
      commentsSettings.IsMessageFieldRequired = this.IsMessageFieldRequired;
      manager.SaveSection((ConfigSection) section);
    }

    public bool? TrackBack
    {
      get => this.GlobalCommentsSettings.TrackBack;
      set
      {
      }
    }

    public bool? AllowComments
    {
      get => this.GlobalCommentsSettings.AllowComments;
      set
      {
      }
    }

    public bool? EmailAuthor
    {
      get => this.GlobalCommentsSettings.EmailAuthor;
      set
      {
      }
    }

    protected IGlobalCommentsSettings GlobalCommentsSettings
    {
      get
      {
        if (this.globalCommentsSettings == null)
          this.globalCommentsSettings = (IGlobalCommentsSettings) Config.GetByPath<Telerik.Sitefinity.Modules.GenericContent.Configuration.GlobalCommentsSettings>("/CommentsConfig/commentsSettings");
        return this.globalCommentsSettings;
      }
    }
  }
}
