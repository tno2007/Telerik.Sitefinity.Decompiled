// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.CommentsViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Localization;

namespace Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data
{
  /// <summary>View-Model class for comments</summary>
  [DataContract(Name = "CommentsViewModel", Namespace = "Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data")]
  public class CommentsViewModel : ContentViewModelBase
  {
    private new bool isEditable;
    private new bool isDeletable;

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.Web.Services.Data.CommentsViewModel" /> class.
    /// </summary>
    /// <param name="comment">The comment.</param>
    /// <param name="prov">The prov.</param>
    public CommentsViewModel(Comment comment, ContentDataProviderBase prov)
    {
      this.ContentItem = (Telerik.Sitefinity.GenericContent.Model.Content) comment;
      this.provider = prov;
      this.Author = (string) comment.AuthorName;
      this.AuthorName = (string) comment.AuthorName;
      this.CommentedItemID = comment.CommentedItemID;
      this.CommentedItemType = comment.CommentedItemType;
      this.Content = (string) comment.Content;
      this.Email = comment.Email;
      this.IpAddress = comment.IpAddress;
      this.CommentStatus = comment.CommentStatus.ToString();
      this.Website = comment.Website;
      if (comment.CommentedItem is Telerik.Sitefinity.GenericContent.Model.Content commentedItem)
      {
        this.IsAuthorComment = comment.Owner == commentedItem.Owner;
        this.isEditable = true;
        this.isDeletable = true;
      }
      string str;
      switch (comment.CommentStatus)
      {
        case Telerik.Sitefinity.GenericContent.Model.CommentStatus.Published:
          str = Res.Get<ContentResources>().Published;
          break;
        case Telerik.Sitefinity.GenericContent.Model.CommentStatus.Hidden:
          str = Res.Get<ContentResources>().Hidden;
          break;
        case Telerik.Sitefinity.GenericContent.Model.CommentStatus.Spam:
          str = Res.Get<ContentResources>().Spam;
          break;
        default:
          throw new NotImplementedException();
      }
      this.CommentStatusText = str;
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetLive() => this.provider.GetLiveBase<Telerik.Sitefinity.GenericContent.Model.Content>(this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetTemp() => this.provider.GetTempBase<Telerik.Sitefinity.GenericContent.Model.Content>(this.ContentItem);

    /// <summary>Gets or sets the author.</summary>
    /// <value>The author.</value>
    [DataMember]
    public override string Author { get; set; }

    /// <summary>Gets or sets the id of the commented item.</summary>
    [DataMember]
    public Guid CommentedItemID { get; set; }

    /// <summary>Gets or sets the type of the commented item.</summary>
    /// <value>The type of the commented item.</value>
    [DataMember]
    public string CommentedItemType { get; set; }

    /// <summary>Gets or sets the text message of the comment.</summary>
    [DataMember]
    public string Content { get; set; }

    /// <summary>Gets or sets the email.</summary>
    [DataMember]
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the ip address from which this comment was posted.
    /// </summary>
    [DataMember]
    public string IpAddress { get; set; }

    /// <summary>Gets or sets the author of the comment.</summary>
    [DataMember]
    public string AuthorName { get; set; }

    /// <summary>Gets or sets the status of the comment.</summary>
    [DataMember]
    public string CommentStatus { get; set; }

    /// <summary>Translated status</summary>
    [DataMember]
    public string CommentStatusText { get; set; }

    /// <summary>Gets or sets the website.</summary>
    [DataMember]
    public string Website { get; set; }

    /// <summary>Checks if the auhtor himself made the comment.</summary>
    /// <returns>
    /// 	<c>true</c> if the auhtor himself made the comment; otherwise, <c>false</c>.
    /// </returns>
    [DataMember]
    public bool IsAuthorComment { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this item is editable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this item is editable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public override bool IsEditable => this.isEditable;

    /// <summary>
    /// Gets or sets a value indicating whether this item is deletable.
    /// </summary>
    /// <value>
    /// 	<c>true</c> if this item is deletable; otherwise, <c>false</c>.
    /// </value>
    [DataMember]
    public override bool IsDeletable => this.isDeletable;
  }
}
