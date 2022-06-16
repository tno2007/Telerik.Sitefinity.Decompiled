// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.CommentsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Configuration;
using System.Runtime.InteropServices;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>Module Comments settings configuration class.</summary>
  public class CommentsSettings : ConfigElement, ICommentsSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Configuration.CommentsSettings" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    public CommentsSettings(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>Gets or sets who is allowed to post comments.</summary>
    [ConfigurationProperty("postRights", DefaultValue = PostRights.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PostRightsDescription", Title = "PostRightsCaption")]
    [Obsolete("Use CommentsModuleConfig.")]
    public PostRights PostRights
    {
      get => (PostRights) this["postRights"];
      set => this["postRights"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if comments support trackback.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("trackBack")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "TrackBackDescription", Title = "TrackBackCaption")]
    [Browsable(false)]
    public bool? TrackBack
    {
      get => (bool?) this["trackBack"];
      set => this["trackBack"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments have to be approved; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("approveComments")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "ApproveCommentsDescription", Title = "ApproveCommentsCaption")]
    [Obsolete("Use CommentsModuleConfig.")]
    public bool? ApproveComments
    {
      get => (bool?) this["approveComments"];
      set => this["approveComments"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if author of the post will be notified via email when a new comment is submitted.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if author of the post will be notified via email when a new comment is submitted; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("emailAuthor")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EmailAuthorDescription", Title = "EmailAuthorCaption")]
    [Browsable(false)]
    public bool? EmailAuthor
    {
      get => (bool?) this["emailAuthor"];
      set => this["emailAuthor"] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating if content item supports comments.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if content item supports comments; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("allowComments")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowCommentsDescription", Title = "AllowCommentsCaption")]
    [Obsolete("Use CommentsModuleConfig.")]
    public bool? AllowComments
    {
      get => (bool?) this["allowComments"];
      set => this["allowComments"] = (object) value;
    }

    protected override void OnPropertiesInitialized()
    {
      base.OnPropertiesInitialized();
      this.getDefaultValueHandler = new GetDefaultValue(this.GetDefaultValueHandler);
    }

    private object GetDefaultValueHandler(string propertyName)
    {
      if (propertyName == "allowComments")
        return (object) true;
      if (propertyName == "approveComments")
        return (object) false;
      if (propertyName == "emailAuthor")
        return (object) false;
      if (propertyName == "trackBack")
        return (object) false;
      return propertyName == "postRights" ? (object) PostRights.Everyone : (object) null;
    }

    /// <summary>
    /// Organizational struct holding name of the properties used when properties are resolved.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 1)]
    internal struct ConfigProps
    {
      public const string PostRights = "postRights";
      public const string TrackBack = "trackBack";
      public const string AllowComments = "allowComments";
      public const string EmailAuthor = "emailAuthor";
      public const string ApproveComments = "approveComments";
      public const string DateFormat = "dateFormat";
      public const string UseSpamProtectionImage = "useSpamProtectionImage";
      public const string IsMessageFieldRequired = "isMessageFieldRequired";
      public const string DisplayMessageField = "displayMessageField";
      public const string DisplayNameField = "displayNameField";
      public const string IsNameFieldRequired = "isNameFieldRequired";
      public const string DisplayWebSiteField = "displayWebSiteField";
      public const string IsWebSiteFieldRequired = "isWebSiteFieldRequired";
      public const string DisplayEmailField = "displayEmailField";
      public const string IsEmailFieldRequired = "isEmailFieldRequired";
      public const string HideCommentsAfterNumberOfDays = "hideCommentsAfterNumberOfDays";
      public const string NumberOfDaysToHideComments = "numberOfDaysToHideComments";
    }
  }
}
