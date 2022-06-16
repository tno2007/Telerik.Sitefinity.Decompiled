// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.CommentContract
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Telerik.Sitefinity.Services.Comments
{
  /// <summary>
  /// Represents the comment object that should be returned by the web service.
  /// </summary>
  [DataContract]
  [EditorBrowsable(EditorBrowsableState.Never)]
  public class CommentContract
  {
    /// <summary>
    /// Gets or sets the name of the author of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The name of the author.</value>
    [DataMember]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the body of the message of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The message.</value>
    [DataMember]
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the date when the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object was created.
    /// </summary>
    /// <value>The date created.</value>
    [DataMember]
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author profile picture URL.
    /// </summary>
    /// <value>The profile picture URL.</value>
    [DataMember]
    public string ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author profile picture thumbnail URL.
    /// </summary>
    /// <value>The profile picture thumbnail URL.</value>
    [DataMember]
    public string ProfilePictureThumbnailUrl { get; set; }
  }
}
