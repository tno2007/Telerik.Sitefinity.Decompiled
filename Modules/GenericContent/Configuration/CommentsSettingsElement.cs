// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Configuration.CommentsDefinitionConfig
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Configuration;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Web.Configuration;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.GenericContent.Configuration
{
  /// <summary>
  /// The configuration element for comments settings properties.
  /// </summary>
  [Obsolete("The backend UI for comments is configurable via CommentsModuleDefinitions.")]
  public class CommentsDefinitionConfig : 
    DefinitionConfigElement,
    ICommentsSettingsDefinition,
    IDefinition,
    ICommentsSettings
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Configuration.CommentsDefinitionConfig" /> class.
    /// </summary>
    /// <param name="element">The element.</param>
    public CommentsDefinitionConfig(ConfigElement element)
      : base(element)
    {
    }

    /// <summary>Gets the comments settings definition.</summary>
    /// <returns>An instance of comments settings definition</returns>
    public override DefinitionBase GetDefinition() => (DefinitionBase) new CommentsSettingsDefinition((ConfigElement) this);

    /// <summary>Gets or sets who is allowed to post comments.</summary>
    [ConfigurationProperty("postRights", DefaultValue = PostRights.None)]
    [ObjectInfo(typeof (ConfigDescriptions), Description = "PostRightsDescription", Title = "PostRightsCaption")]
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
    public bool? AllowComments
    {
      get => (bool?) this["allowComments"];
      set => this["allowComments"] = (object) value;
    }
  }
}
