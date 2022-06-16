// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentUpdateRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentUpdateRequest</c> Represent the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object that will be updated.
  /// </summary>
  public class CommentUpdateRequest
  {
    /// <summary>
    /// Gets or sets the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object key.
    /// </summary>
    /// <value>The key.</value>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the body of the message of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The message.</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object status.
    /// </summary>
    /// <value>The status.</value>
    public string Status { get; set; }

    /// <summary>
    /// Gets or sets the author name of the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the author email of the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the custom data of the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <remarks>
    /// This custom data is an extensibility point that can be used to store some custom data for the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </remarks>
    /// <value>The custom data.</value>
    public string CustomData { set; get; }

    /// <summary>
    /// Gets or sets the rating of the updated <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <remarks>
    /// This rating value is used in the context of reviews behaviour.
    /// </remarks>
    /// <value>The rating data.</value>
    public Decimal? Rating { get; set; }
  }
}
