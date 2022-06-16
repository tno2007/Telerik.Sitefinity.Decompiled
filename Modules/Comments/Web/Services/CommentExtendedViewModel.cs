// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentExtendedViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Linq;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Comments;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.CommentResponse instead.")]
  [DataContract]
  internal class CommentExtendedViewModel : CommentViewModel
  {
    public CommentExtendedViewModel(ICommentService cs, IComment comment, IThread thread)
      : this(comment, thread)
    {
      this.CommentsCountByThread = cs.GetComments(this.ThreadKey).Count<IComment>();
    }

    public CommentExtendedViewModel(IComment comment, IThread thread)
      : base(comment)
    {
      this.ThreadTitle = thread.Title;
      this.ThreadKey = comment.ThreadKey;
      this.Language = thread.Language;
      this.AuthorIpAddress = comment.AuthorIpAddress;
      this.ContentLocationPreviewUrl = CommentsUtilities.GetCommentedItemUrl(thread);
    }

    [DataMember]
    public string Email { get; set; }

    [DataMember]
    public string ThreadTitle { get; set; }

    [DataMember]
    public string ThreadKey { get; set; }

    [DataMember]
    public string Language { get; set; }

    [DataMember]
    public int CommentsCountByThread { get; set; }

    [DataMember]
    public string AuthorIpAddress { get; set; }

    [DataMember]
    public string ContentLocationPreviewUrl { get; set; }

    protected override void PopulateAuthorInfo(IAuthor author)
    {
      base.PopulateAuthorInfo(author);
      if (author.Key.IsNullOrEmpty())
      {
        this.PopulateAnonimusAuthor(author);
      }
      else
      {
        Guid result;
        if (!Guid.TryParse(author.Key, out result))
          throw new InvalidOperationException();
        ICacheUserProfile cachedUserProfile = (ICacheUserProfile) UserManager.GetCachedUserProfile(result);
        if (cachedUserProfile != null)
          this.PopulateRegisteredAuthorInfo(cachedUserProfile);
        else
          this.PopulateAnonimusAuthor(author);
      }
    }

    private void PopulateRegisteredAuthorInfo(ICacheUserProfile profile) => this.Email = profile.Email;

    private void PopulateAnonimusAuthor(IAuthor author) => this.Email = author.Email;
  }
}
