// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Web.UI.Comments.ICommentSettingsResolver
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Web.UI.Comments
{
  /// <summary>Resolves comments-related settings</summary>
  public interface ICommentSettingsResolver
  {
    /// <summary>Get the root page for the comments panel</summary>
    /// <param name="contentType">Content type to help resolving</param>
    /// <param name="commentType">Comment type to help resolving</param>
    /// <returns>Id of the page</returns>
    Guid ResolveRootPageId(Type contentType, Type commentType);

    /// <summary>Get the page where comments are displayed</summary>
    /// <param name="contentType">Content type</param>
    /// <param name="commentType">Comment type</param>
    /// <returns>ID of the page to resolve</returns>
    Guid ResolveCommentsPageId(Type contentType, Type commentType);

    /// <summary>
    /// Get the translated title of the comments command panel
    /// </summary>
    /// <param name="contentType">Content type</param>
    /// <param name="commentType">Comment type</param>
    /// <returns>Translated string.</returns>
    string ResolveCommandPanelTitle(Type contentType, Type commentType);
  }
}
