// Decompiled with JetBrains decompiler
// Type: Telerik.Sitefinity.Services.Comments.DTO.CommentsBatchDeleteRequest
// Assembly: Telerik.Sitefinity, Version=13.3.7600.0, Culture=neutral, PublicKeyToken=b28c218413bdf563
// MVID: 39C4B52A-B559-4D9C-97D9-CCCF73C3738E
// Assembly location: C:\Programs\Sitefinity\ProjectManager_13_3_7600\_EmptyProject\bin\Telerik.Sitefinity.dll

using System.Collections.Generic;

namespace Telerik.Sitefinity.Services.Comments.DTO
{
  /// <summary>
  /// <c>CommentsBatchDeleteRequest</c> Represent data required to perform batch delete of <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects.
  /// </summary>
  public class CommentsBatchDeleteRequest
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:Telerik.Sitefinity.Services.Comments.DTO.CommentsBatchDeleteRequest" /> class.
    /// </summary>
    public CommentsBatchDeleteRequest() => this.Key = new List<string>();

    /// <summary>
    /// Gets or set array of keys for <see cref="T:Telerik.Sitefinity.Services.Comments.IComment" /> objects which are marked for delete.
    /// </summary>
    /// <value>The keys.</value>
    public List<string> Key { get; set; }
  }
}
