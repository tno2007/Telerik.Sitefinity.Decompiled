// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Modules.GenericContent.CommentViewModel
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.Runtime.Serialization;
using Telerik.Sitefinity.GenericContent.Model;
using Telerik.Sitefinity.Model;

namespace Telerik.Sitefinity.Modules.GenericContent
{
  /// <summary>Represents summary information about a comment</summary>
  [Obsolete("This view model represent an obsolete persistent object.")]
  [DataContract]
  public class CommentViewModel : ContentViewModelBase, IDynamicFieldsContainer
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.CommentViewModel" /> class.
    /// </summary>
    public CommentViewModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.CommentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    public CommentViewModel(Comment contentItem, ContentDataProviderBase provider)
      : base((Telerik.Sitefinity.GenericContent.Model.Content) contentItem, provider)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Modules.GenericContent.CommentViewModel" /> class.
    /// </summary>
    /// <param name="contentItem">The content item.</param>
    /// <param name="provider">The provider.</param>
    /// <param name="isEditable">if set to <c>true</c> if the content is editable.</param>
    public CommentViewModel(Comment contentItem, ContentDataProviderBase provider, bool isEditable)
      : this(contentItem, provider)
    {
      this.isEditable = isEditable;
    }

    /// <summary>Gets or sets the name of the author.</summary>
    /// <value>The name of the author.</value>
    [DataMember]
    public string AuthorName
    {
      get => (string) ((Comment) this.ContentItem).AuthorName;
      set => ((Comment) this.ContentItem).AuthorName = (Lstring) value;
    }

    /// <summary>Gets or sets the email of the author</summary>
    /// <value>The email.</value>
    [DataMember]
    public string Email
    {
      get => ((Comment) this.ContentItem).Email;
      set => ((Comment) this.ContentItem).Email = value;
    }

    /// <summary>Gets or sets the website of the author</summary>
    /// <value>The website.</value>
    [DataMember]
    public string Website
    {
      get => ((Comment) this.ContentItem).Website;
      set => ((Comment) this.ContentItem).Website = value;
    }

    /// <summary>Gets or sets the ip address of the author</summary>
    /// <value>The ip address.</value>
    [DataMember]
    public string IpAddress
    {
      get => ((Comment) this.ContentItem).IpAddress;
      set => ((Comment) this.ContentItem).IpAddress = value;
    }

    /// <summary>Gets or sets the status of the comment</summary>
    /// <value>The status.</value>
    [DataMember]
    public string CommentStatus
    {
      get => ((Comment) this.ContentItem).CommentStatus.ToString();
      set => ((Comment) this.ContentItem).CommentStatus = (Telerik.Sitefinity.GenericContent.Model.CommentStatus) Enum.Parse(typeof (Telerik.Sitefinity.GenericContent.Model.CommentStatus), value);
    }

    /// <summary>Gets or sets the content of the comment</summary>
    /// <value>The content.</value>
    [DataMember]
    public Lstring Content
    {
      get => ((Comment) this.ContentItem).Content;
      set => ((Comment) this.ContentItem).Content = value;
    }

    /// <summary>
    /// Get live version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Live version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetLive() => (Telerik.Sitefinity.GenericContent.Model.Content) this.provider.GetLiveBase<Comment>((Comment) this.ContentItem);

    /// <summary>
    /// Get temp version of this.ContentItem using this.provider
    /// </summary>
    /// <returns>Temp version of this.ContentItem</returns>
    protected override Telerik.Sitefinity.GenericContent.Model.Content GetTemp() => (Telerik.Sitefinity.GenericContent.Model.Content) this.provider.GetTempBase<Comment>((Comment) this.ContentItem);
  }
}
