// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.Comments.Web.Services.CommentViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.Modules.UserProfiles;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Services.Comments;
using Telerik.Sitefinity.Web;

namespace Telerik.Sitefinity.Modules.Comments.Web.Services
{
  [Obsolete("Use Telerik.Sitefinity.Services.Comments.DTO.CommentResponse instead.")]
  [DataContract]
  internal class CommentViewModel
  {
    public CommentViewModel(IComment comment)
    {
      this.Key = comment.Key;
      this.Message = comment.Message;
      this.DateCreated = comment.DateCreated;
      this.Parent = comment.ThreadKey;
      this.Status = comment.Status;
      this.Rating = comment.Rating;
      if (comment.Author == null)
        return;
      this.PopulateAuthorInfo(comment.Author);
    }

    [DataMember]
    public string Key { get; set; }

    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string Message { get; set; }

    [DataMember]
    public Decimal? Rating { get; set; }

    [DataMember]
    public DateTime DateCreated { get; set; }

    [DataMember]
    public string Parent { get; set; }

    [DataMember]
    public string ProfilePictureUrl { get; set; }

    [DataMember]
    public string ProfilePictureThumbnailUrl { get; set; }

    [DataMember]
    public string Status { get; set; }

    protected virtual void PopulateAuthorInfo(IAuthor author)
    {
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
        {
          Telerik.Sitefinity.Libraries.Model.Image image;
          this.ProfilePictureUrl = UserProfilesHelper.GetAvatarImageUrl(result, out image);
          this.ProfilePictureThumbnailUrl = image == null || string.IsNullOrEmpty(image.MediaUrl) ? RouteHelper.ResolveUrl("~/SFRes/images/Telerik.Sitefinity.Resources/Images.DefaultPhoto.png", UrlResolveOptions.Rooted) : image.ThumbnailUrl;
          this.PopulateRegisteredAuthorInfo(cachedUserProfile);
        }
        else
          this.PopulateAnonimusAuthor(author);
      }
    }

    private void PopulateRegisteredAuthorInfo(ICacheUserProfile profile) => this.Name = profile.Nickname;

    private void PopulateAnonimusAuthor(IAuthor author)
    {
      this.Name = author.Name;
      string avatarImageUrl = UserProfilesHelper.GetAvatarImageUrl(Guid.Empty);
      this.ProfilePictureUrl = avatarImageUrl;
      this.ProfilePictureThumbnailUrl = avatarImageUrl;
    }
  }
}
