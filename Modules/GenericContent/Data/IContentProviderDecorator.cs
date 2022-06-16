// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Data.IContentProviderDecorator
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Modules.GenericContent.Data
{
  /// <summary>
  /// Defines interface for content data provider decorators.
  /// </summary>
  public interface IContentProviderDecorator : ICloneable
  {
    /// <summary>Gets or sets the data provider.</summary>
    /// <value>The data provider.</value>
    ContentDataProviderBase DataProvider { get; set; }

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    void BlockCommentsForEmail(string email);

    /// <summary>Blocks the comments coming from the given IP</summary>
    /// <param name="ip">The ip.</param>
    void BlockCommentsForIP(string ip);

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <returns></returns>
    Comment CreateComment(ICommentable commentedItem);

    /// <summary>Creates a comment for the specified commented item</summary>
    /// <param name="commentedItem">The commented item.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    Comment CreateComment(ICommentable commentedItem, Guid commentId);

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <param name="commentId">The comment item id.</param>
    /// <returns></returns>
    Comment CreateComment(Type commentedItemType, Guid commentedItemId, Guid commentId);

    /// <summary>
    /// Creates a comment for the commented item with the given type and pageId.
    /// </summary>
    /// <param name="commentedItemType">Type of the commented item.</param>
    /// <param name="commentedItemId">The commented item pageId.</param>
    /// <returns></returns>
    Comment CreateComment(Type commentedItemType, Guid commentedItemId);

    /// <summary>Deletes the specified comment.</summary>
    /// <param name="comment">The comment.</param>
    /// <returns></returns>
    void Delete(Comment comment);

    /// <summary>Gets a comment by the specified commentId.</summary>
    /// <param name="commentId">The comment id.</param>
    /// <returns></returns>
    Comment GetComment(Guid commentId);

    /// <summary>Gets an IQueryable of comments.</summary>
    /// <returns>IQueryable of comments</returns>
    IQueryable<Comment> GetComments();

    /// <summary>
    /// Gets an IQueryable of comments for specified id of item that implements <see cref="T:Telerik.Sitefinity.GenericContent.Model.ICommentable" />.
    /// </summary>
    /// <returns>IQueryable of comments</returns>
    IQueryable<Comment> GetComments(Guid commentableId);

    /// <summary>
    /// Gets or sets the IDs of the parents of the commented item passed as an argument
    /// </summary>
    /// <param name="content">The content.</param>
    /// <returns></returns>
    IList<Guid> GetParentGroupIds(Comment content);
  }
}
