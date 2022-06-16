// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentGetRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using ServiceStack;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentGetRequest</c> Represent the request to get a single <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object by key.
  /// </summary>
  public class CommentGetRequest : IReturn<CommentResponse>, IReturn
  {
    /// <summary>
    /// Gets or sets the key of the <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> object that should be returned.
    /// </summary>
    /// <value>The key.</value>
    public string Key { set; get; }
  }
}
