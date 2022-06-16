// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Contracts.ICommentsSettings
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Contracts
{
  /// <summary>
  /// Contract for module comments settings configuration properties.
  /// </summary>
  [Obsolete("Settings for comments are in CommentsModuleConfig.")]
  public interface ICommentsSettings
  {
    /// <summary>Gets or sets who is allowed to post comments.</summary>
    PostRights PostRights { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if comments support trackback.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments support trackback; otherwise, <c>false</c>.
    /// </value>
    bool? TrackBack { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if content item supports comments.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if content item supports comments; otherwise, <c>false</c>.
    /// </value>
    bool? AllowComments { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if author of the post will be notified via email when a new comment is submitted.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if author of the post will be notified via email when a new comment is submitted; otherwise, <c>false</c>.
    /// </value>
    bool? EmailAuthor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating if comments have to be approved in order to be displayed.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if comments have to be approved; otherwise, <c>false</c>.
    /// </value>
    bool? ApproveComments { get; set; }
  }
}
