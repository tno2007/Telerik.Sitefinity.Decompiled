// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentDeleteRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentDeleteRequest</c> Represent the request that deletes a single <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object by key.
  /// </summary>
  public class CommentDeleteRequest
  {
    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object that should be deleted.
    /// </summary>
    /// <value>The key.</value>
    public string Key { get; set; }
  }
}
