// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Content.Web.Services.CommentsService
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using System.ServiceModel.Activation;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Web.Services;

namespace Telerik.Sitefinity.Services.Content.Web.Services
{
  /// <summary>
  /// Defines a service that operates with comments. This service only exposes methods which are not specific to the
  /// Generic Content functionality of comments. This is why it cannot create/delete/get comments.
  /// </summary>
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  [KnownType("GetServiceKnownTypes")]
  public class CommentsService : ICommentsService
  {
    /// <summary>Hides the comment.</summary>
    /// <param name="commentId">The comment pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void HideComment(string commentId, string providerName, string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IManager manager = ManagerBase.GetManager(managerType, providerName);
      if (manager == null)
        throw new Exception("Could not resolve manager by type");
      (manager.GetItem(typeof (Comment), new Guid(commentId)) as Comment).CommentStatus = CommentStatus.Hidden;
      manager.SaveChanges();
    }

    /// <summary>Publishes the comment.</summary>
    /// <param name="commentId">The comment pageId.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void PublishComment(string commentId, string providerName, string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IManager manager = ManagerBase.GetManager(managerType, providerName);
      if (manager == null)
        throw new Exception("Could not resolve manager by type");
      (manager.GetItem(typeof (Comment), new Guid(commentId)) as Comment).CommentStatus = CommentStatus.Published;
      manager.SaveChanges();
    }

    /// <summary>Marks the comment as spam.</summary>
    /// <param name="commentId">The comment ID.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void MarkCommentAsSpam(string commentId, string providerName, string managerType)
    {
      ServiceUtility.RequestBackendUserAuthentication();
      IManager manager = ManagerBase.GetManager(managerType, providerName);
      if (manager == null)
        throw new Exception("Could not resolve manager by type");
      (manager.GetItem(typeof (Comment), new Guid(commentId)) as Comment).CommentStatus = CommentStatus.Spam;
      manager.SaveChanges();
    }

    /// <summary>Blocks the comments coming from the given IP.</summary>
    /// <param name="ip">The ip.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void BlockCommentsForIp(string ip, string providerName, string managerType) => throw new NotImplementedException();

    /// <summary>
    /// Blocks the comments from the user with the specified email.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <param name="providerName">Name of the provider.</param>
    /// <param name="managerType">Type of the manager.</param>
    public void BlockCommentsForEmail(string email, string providerName, string managerType) => throw new NotImplementedException();
  }
}
