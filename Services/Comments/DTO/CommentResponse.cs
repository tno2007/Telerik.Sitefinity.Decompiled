// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentResponse
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentResponse</c>. Represents the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object that should be returned.
  /// </summary>
  public class CommentResponse
  {
    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The key.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the name of the author of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The name of the author.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the body of the message of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The message.</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the date when the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object was created.
    /// </summary>
    /// <value>The date created.</value>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author profile picture URL.
    /// </summary>
    /// <value>The profile picture URL.</value>
    public string ProfilePictureUrl { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author profile picture thumbnail URL.
    /// </summary>
    /// <value>The profile picture thumbnail URL.</value>
    public string ProfilePictureThumbnailUrl { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object status.
    /// </summary>
    /// <value>The status.</value>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author email.
    /// This is sensitive information that is returned if the request is made from authenticated backend user.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object that the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object is associated with.
    /// </summary>
    /// <value>The thread key.</value>
    public string ThreadKey { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author IP address.
    /// This is sensitive information that is returned if the request is made from authenticated backend user.
    /// </summary>
    /// <value>The author ip address.</value>
    public string AuthorIpAddress { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object custom data.
    /// </summary>
    /// <remarks>
    /// This custom data is an extensibility point that can be used to store custom data for the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </remarks>
    /// <value>The custom data.</value>
    public string CustomData { set; get; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object rating.
    /// </summary>
    /// <remarks>
    /// This rating field is used in the context of reviews behaviour.
    /// </remarks>
    /// <value>The custom data.</value>
    public Decimal? Rating { get; set; }
  }
}
