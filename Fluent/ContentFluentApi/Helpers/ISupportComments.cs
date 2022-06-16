// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Fluent.ContentFluentApi.ISupportComments`2
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using Telerik.Sitefinity.GenericContent.Model;

namespace Telerik.Sitefinity.Fluent.ContentFluentApi
{
  /// <summary>
  /// Provides a common interface for facades that support comments
  /// </summary>
  /// <typeparam name="TParentFacade">Type of the facade which implements this interface</typeparam>
  /// <typeparam name="TCommentedItem">Type of the content item that has comments (e.g. a blog post)</typeparam>
  public interface ISupportComments<TParentFacade, TCommentedItem>
    where TParentFacade : BaseFacade
    where TCommentedItem : Content
  {
    /// <summary>Get a facade that gets a comment by id and manages it</summary>
    /// <param name="id">ID of the comment to retrieve</param>
    /// <returns>Facade that manages individual comments</returns>
    CommentsSingularFacade<TParentFacade, TCommentedItem> Comment(Guid id);

    /// <summary>Get a facade that manages single comment entries</summary>
    /// <returns>Facade that manages single comments</returns>
    CommentsSingularFacade<TParentFacade, TCommentedItem> Comment();

    /// <summary>
    /// Get a facade that manages the comments of the content item loaded into the parent facade
    /// </summary>
    /// <returns>Facade that manages this item's comments</returns>
    CommentsPluralFacade<TParentFacade, TCommentedItem> Comments();
  }
}
