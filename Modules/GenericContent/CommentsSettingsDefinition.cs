// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.CommentsSettingsDefinition
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Modules.GenericContent.Contracts;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.Definitions;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>
  /// Definition class that contains comments settings properties.
  /// </summary>
  [Obsolete("The backend UI for comments is configurable via CommentsModuleDefinitions.")]
  public class CommentsSettingsDefinition : 
    DefinitionBase,
    ICommentsSettingsDefinition,
    IDefinition,
    ICommentsSettings
  {
    private PostRights postRights;
    private bool? trackBack;
    private bool? allowComments;
    private bool? emailAuthor;
    private bool? approveComments;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.CommentsSettingsDefinition" /> class.
    /// </summary>
    public CommentsSettingsDefinition()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.CommentsSettingsDefinition" /> class.
    /// </summary>
    /// <param name="configDefinition">The config definition.</param>
    public CommentsSettingsDefinition(ConfigElement configDefinition)
      : base(configDefinition)
    {
    }

    /// <summary>Gets the definition.</summary>
    /// <returns></returns>
    public CommentsSettingsDefinition GetDefinition() => this;

    /// <summary>
    /// Gets or sets value indicating who is allowed to post comments.
    /// </summary>
    /// <value>The post comments allowance.</value>
    public PostRights PostRights
    {
      get => this.ResolveProperty<PostRights>(nameof (PostRights), this.postRights);
      set => this.postRights = value;
    }

    /// <summary>
    /// Gets or sets a value indicating the comments support trackback.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    public bool? TrackBack
    {
      get => this.ResolveProperty<bool?>(nameof (TrackBack), this.trackBack);
      set => this.trackBack = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the content item support comments.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    public bool? AllowComments
    {
      get => this.ResolveProperty<bool?>(nameof (AllowComments), this.allowComments);
      set => this.allowComments = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if author of the post will be notified via email when a new comment is submitted.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    public bool? EmailAuthor
    {
      get => this.ResolveProperty<bool?>(nameof (EmailAuthor), this.emailAuthor);
      set => this.emailAuthor = value;
    }

    /// <summary>
    /// Gets or sets a value indicating if the comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    public bool? ApproveComments
    {
      get => this.ResolveProperty<bool?>(nameof (ApproveComments), this.approveComments);
      set => this.approveComments = value;
    }
  }
}
