// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentCreateRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentCreateRequest</c> Represents the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object that should be created.
  /// </summary>
  public class CommentCreateRequest
  {
    /// <summary>
    /// Gets or sets the body of the message of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </summary>
    /// <value>The message.</value>
    public string Message { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author name.
    /// </summary>
    /// <value>The name.</value>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object author email.
    /// </summary>
    /// <value>The email.</value>
    public string Email { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object custom data.
    /// </summary>
    /// <remarks>
    /// This custom data is extensibility point that can be used to store some custom data for the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object.
    /// </remarks>
    /// <value>The custom data.</value>
    public string CustomData { get; set; }

    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IThread" /> object that the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object is associated with.
    /// </summary>
    /// <value>The thread key.</value>
    public string ThreadKey { get; set; }

    /// <summary>
    /// Gets or sets the thread information when the parent thread should be created for this <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" />.
    /// </summary>
    /// <value>The thread.</value>
    public ThreadCreateRequest Thread { get; set; }

    /// <summary>Gets or sets the captcha information.</summary>
    /// <value>The captcha data.</value>
    public CaptchaInfo Captcha { get; set; }

    /// <summary>Gets or sets the rating data.</summary>
    /// <remarks>
    /// This rating data is used in the context of reviews behaviour
    /// </remarks>
    /// <value>The captcha data.</value>
    public Decimal? Rating { get; set; }
  }
}
