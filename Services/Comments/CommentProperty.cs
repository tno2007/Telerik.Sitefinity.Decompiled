// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.CommentProperty
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Model;
using Telerik.Sitefinity.Modules.Comments;
using Telerik.Sitefinity.Modules.Comments.Configuration;
using Telerik.Sitefinity.Services.Comments.DTO;
using Telerik.Sitefinity.Web.Services.Extensibility;

namespace Telerik.Sitefinity.Services.Comments
{
  /// <summary>
  /// A calculated property for retrieving Comments of items.
  /// </summary>
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class CommentProperty : CalculatedProperty
  {
    /// <inheritdoc />
    public override Type ReturnType => typeof (IEnumerable<CommentContract>);

    /// <inheritdoc />
    public override IDictionary<object, object> GetValues(
      IEnumerable items,
      IManager manager)
    {
      Dictionary<object, object> values = new Dictionary<object, object>();
      foreach (IDataItem key in items)
      {
        if (CommentsUtilities.GetThreadConfigByType(this.ParentType.FullName).AllowComments)
        {
          IEnumerable<CommentContract> comments = this.GetComments(key.Id);
          values.Add((object) key, (object) comments);
        }
        else
          values.Add((object) key, (object) null);
      }
      return (IDictionary<object, object>) values;
    }

    /// <summary>Gets the comments of the item by given id.</summary>
    /// <param name="id">The id of the item.</param>
    /// <returns>The comments.</returns>
    private IEnumerable<CommentContract> GetComments(Guid id)
    {
      CommentsModuleConfig commentsModuleConfig = Config.Get<CommentsModuleConfig>();
      bool enablePaging = commentsModuleConfig.EnablePaging;
      int commentsPerPage = commentsModuleConfig.CommentsPerPage;
      bool areNewestOnTop = commentsModuleConfig.AreNewestOnTop;
      CultureInfo culture = SystemManager.CurrentContext.Culture;
      string str = id.ToString() + "_" + culture.Name;
      CommentsFilter request = new CommentsFilter()
      {
        ThreadKey = str,
        SortDescending = areNewestOnTop
      };
      if (enablePaging)
        request.Take = commentsPerPage;
      CollectionResponse<CommentResponse> collectionResponse = new CommentWebService().Get(request);
      List<CommentContract> comments = new List<CommentContract>();
      foreach (CommentResponse commentResponse in collectionResponse.Items)
      {
        CommentContract commentContract = new CommentContract()
        {
          DateCreated = commentResponse.DateCreated,
          Name = commentResponse.Name,
          ProfilePictureUrl = commentResponse.ProfilePictureUrl,
          ProfilePictureThumbnailUrl = commentResponse.ProfilePictureThumbnailUrl,
          Message = commentResponse.Message
        };
        comments.Add(commentContract);
      }
      return (IEnumerable<CommentContract>) comments;
    }
  }
}
