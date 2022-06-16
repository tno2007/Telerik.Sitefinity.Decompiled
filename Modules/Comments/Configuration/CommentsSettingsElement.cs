// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Configuration.CommentsSettingsElement
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Web.Configuration;

namespace Telerik.Sitefinity.Modules.Comments.Configuration
{
  internal class CommentsSettingsElement : ConfigElement
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.Comments.Configuration.CommentsSettingsElement" /> class.
    /// </summary>
    /// <param name="parent">The parent element.</param>
    /// <remarks>
    /// ConfigElementCollection generally needs to have a parent, however, sometimes it is necessary
    /// to create a collection in memory only which is then used later on in the context of a parent.
    /// Therefore, is the element is of ConfigElementCollection, exception for a non existing parent
    /// will not be thrown.
    /// </remarks>
    public CommentsSettingsElement(ConfigElement parent)
      : base(parent)
    {
    }

    /// <summary>
    /// Gets or sets a value indicating if content item supports comments.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if content item supports comments; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("AllowComments", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "AllowCommentsDescription", Title = "AllowCommentsCaption")]
    public bool AllowComments
    {
      get => (bool) this[nameof (AllowComments)];
      set => this[nameof (AllowComments)] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether threads on the commentable type require authentication by default.
    /// </summary>
    [ConfigurationProperty("RequiresAuthentication", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentableTypeRequiresAuthenticationDescription", Title = "CommentableTypeRequiresAuthenticationCaption")]
    public bool RequiresAuthentication
    {
      get => (bool) this[nameof (RequiresAuthentication)];
      set => this[nameof (RequiresAuthentication)] = (object) value;
    }

    /// <summary>
    /// Gets or sets whether threads on the commentable type require approval by default.
    /// </summary>
    [ConfigurationProperty("RequiresApproval")]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentableTypeRequiresApprovalDescription", Title = "CommentableTypeRequiresApprovalCaption")]
    public bool RequiresApproval
    {
      get => (bool) this[nameof (RequiresApproval)];
      set => this[nameof (RequiresApproval)] = (object) value;
    }

    /// <summary>
    /// Gets whether comments will allow subscription for email notifications.
    /// </summary>
    [ConfigurationProperty("AllowSubscription", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "CommentsAllowSubscriptionDescription", Title = "CommentsAllowSubscriptionCaption")]
    public bool AllowSubscription
    {
      get => (bool) this[nameof (AllowSubscription)];
      set => this[nameof (AllowSubscription)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to restrict ratings to one from user by thread.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> the comments are restricted to one from user by thread; otherwise, <c>false</c>.
    /// </value>
    [ConfigurationProperty("EnableRatings", DefaultValue = false)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableRatingsDescription", Title = "EnableRatingsCaption")]
    public bool EnableRatings
    {
      get => (bool) this[nameof (EnableRatings)];
      set => this[nameof (EnableRatings)] = (object) value;
    }

    /// <summary>
    /// Gets or sets a value indicating whether to allow creating of Thread's without a valid ID relation to the associated type.
    /// </summary>
    /// <value>
    /// When set to <c>true</c> the comments will create Thread only if the associated, <c>false</c>.
    /// </value>
    [ConfigurationProperty("EnableThreadCreationByConvension", DefaultValue = true)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "EnableThreadCreationByConvension", Title = "EnableThreadCreationByConvension")]
    public bool EnableThreadCreationByConvension
    {
      get => (bool) this[nameof (EnableThreadCreationByConvension)];
      set => this[nameof (EnableThreadCreationByConvension)] = (object) value;
    }

    private static class ConfigProps
    {
      public const string AllowComments = "AllowComments";
      public const string RequiresAuthentication = "RequiresAuthentication";
      public const string RequiresApproval = "RequiresApproval";
      public const string AllowSubscription = "AllowSubscription";
      public const string EnableRatings = "EnableRatings";
      public const string EnableThreadCreationByConvension = "EnableThreadCreationByConvension";
    }
  }
}
